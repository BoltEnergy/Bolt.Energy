
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
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.Bussiness
{
    public class CategoriesWithPermissionCheck :  Categories
    {
        UserOrOperator _operatingUserOrOperator;

        public CategoriesWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }
        private void CheckPermission()
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
        }

        public new int Add(string name, string description,EnumCategoryStatus categoryStatus)
        {
            CheckPermission();
            return base.Add(name, description, categoryStatus);
        }

        public new void Sort(int id, EnumSortMoveDirection sortDirection)
        {
            CheckPermission();
            base.Sort(id, sortDirection);
        }

        public void Delete(int categoryId)
        {           
            base.Delete(categoryId, this._operatingUserOrOperator);
        }

        public CategoryWithPermissionCheck[] GetAllCategories()
        {
            return base.GetAllCategories(this._operatingUserOrOperator);
        }

        public CategoryWithPermissionCheck[] GetNotClosedCategories()
        {
            return base.GetNotClosedCategories(this._operatingUserOrOperator);
        }
    }
}
