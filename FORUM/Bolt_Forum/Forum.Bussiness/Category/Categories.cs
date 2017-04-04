#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class Categories : CategoriesBase
    {
        public Categories(SqlConnectionWithSiteId conn, SqlTransaction transaction)
            :base(conn, transaction)
        {
            
        }

        protected virtual CategoryWithPermissionCheck[] GetAllCategories(UserOrOperator operatingOperator)
        {
            DataTable table = new DataTable();
            table = CategoryAccess.GetAllCategorys(_conn, _transaction);
            CategoryWithPermissionCheck[] categories = new CategoryWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                categories[i] = new CategoryWithPermissionCheck(_conn, _transaction, 
                    Convert.ToInt32(table.Rows[i]["Id"]),
                    operatingOperator, 
                    Convert.ToInt32(table.Rows[i]["OrderId"]), 
                    Convert.ToString(table.Rows[i]["Name"]),
                    Convert.ToString(table.Rows[i]["Description"]),
                    (Convert.ToInt16(table.Rows[i]["Status"])==0 ? EnumCategoryStatus.Normal : EnumCategoryStatus.Close));                
            }
            return categories;
          
        }

        protected virtual CategoryWithPermissionCheck[] GetNotClosedCategories(UserOrOperator operatingOperator)
        {
            DataTable table = new DataTable();
            table = CategoryAccess.GetNotClosedCategorys(_conn, _transaction);
            CategoryWithPermissionCheck[] categories = new CategoryWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                categories[i] = new CategoryWithPermissionCheck(_conn, _transaction,
                    Convert.ToInt32(table.Rows[i]["Id"]),
                    operatingOperator,
                    Convert.ToInt32(table.Rows[i]["OrderId"]),
                    Convert.ToString(table.Rows[i]["Name"]),
                    Convert.ToString(table.Rows[i]["Description"]),
                    (Convert.ToInt16(table.Rows[i]["Status"]) == 0 ? EnumCategoryStatus.Normal : EnumCategoryStatus.Close));
            }
            return categories;

        }

        protected virtual int Add(string name, string description,EnumCategoryStatus categoryStatus)
        {
            return Category.Add(_conn, _transaction, name, description,categoryStatus);
        }

        protected virtual void Sort(int id, EnumSortMoveDirection sortDirection)
        {
            if (sortDirection == EnumSortMoveDirection.Up)
            {
                CategoryAccess.IncreaseSortCategorys(_conn, _transaction, id);
            }
            else
            {
                CategoryAccess.DecreaseSortCategorys(_conn, _transaction, id);
            }
        }

        protected virtual void Delete(int categoryId, UserOrOperator operatingOperator)
        {
            CategoryWithPermissionCheck category = new CategoryWithPermissionCheck(this._conn, this._transaction, categoryId, operatingOperator);

            ForumsOfCategoryWithPermissionCheck forums = category.GetForums();
            if (forums.GetCountOfAllForums() == 0)
            {
                category.Delete();
            }
            else
            {
                ExceptionHelper.ThrowCategoryIsUsingException(categoryId);
            }

        }
    }
}
