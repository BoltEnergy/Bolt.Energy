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
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class Category
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        private int _categoryId;
        private int _orderId;
        private string _name;
        private string _description;
        /*----------------2.0------------------*/
        private EnumCategoryStatus _status;
        #endregion

        #region property
        public int CategoryId
        {
            get { return this._categoryId; }
        }
        public int OrderId
        {
            get { return this._orderId; }
        }
        public string Name
        {
            get { return this._name; }
        }
        public string Description
        {
            get { return this._description; }
        }
        /*----------------2.0------------------*/
        public EnumCategoryStatus Status
        {
            get { return this._status; }
        }
        #endregion

        public Category(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId)
        {
            this._conn = conn;
            this._transaction = transaction;

            DataTable table = CategoryAccess.GetCategoryByCategoryId(_conn, _transaction, categoryId);

            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowCategoryNotExistException(categoryId);
            }
            else
            {
                _categoryId = categoryId;
                _orderId = Convert.ToInt32(table.Rows[0]["OrderId"]);
                _name = Convert.ToString(table.Rows[0]["Name"]);
                _description = Convert.ToString(table.Rows[0]["Description"]);
                _status = (Convert.ToInt16(table.Rows[0]["Status"]) == 0 ? EnumCategoryStatus.Normal : EnumCategoryStatus.Close);
            }
 
        }

        public Category(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, int orderId, string name, string description,EnumCategoryStatus categoryStatus)
        {
            this._conn = conn;
            this._transaction = transaction;
            this._categoryId = categoryId;
            this._orderId = orderId;
            this._name = name;
            this._description = description;
            this._status = categoryStatus;

 
        }

        public static int Add(SqlConnectionWithSiteId conn, SqlTransaction transaction, string name ,string description,EnumCategoryStatus categoryStatus)
        {
            CheckFieldsLength(name, description);
            return CategoryAccess.AddCategory(conn, transaction, name,categoryStatus, description);          
        }

        protected virtual void Update(string name, string description,EnumCategoryStatus categoryStatus)
        {
            CheckFieldsLength(name, description);
            CategoryAccess.UpdateCategory(_conn, _transaction, _categoryId, name,categoryStatus, description);
        }

        private static void CheckFieldsLength(string name, string description)
        {
            if (name.Length == 0)
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("Name");
            else
            {
                if (name.Length > ForumDBFieldLength.Category_nameFieldLength)
                    ExceptionHelper.ThrowSystemFieldLengthExceededException("Name", ForumDBFieldLength.Category_nameFieldLength);
            }
            if (description.Length > ForumDBFieldLength.Category_descriptionFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Description", ForumDBFieldLength.Category_descriptionFieldLength);

        }

        protected virtual ForumsOfCategoryWithPermissionCheck GetForums(UserOrOperator operatingOperator)
        {
            return new ForumsOfCategoryWithPermissionCheck(_conn, _transaction, _categoryId, operatingOperator);
        }

        protected virtual void Delete()
        {
            CategoryAccess.DeleteCategory(_conn, _transaction, _categoryId);
        }
    }
}
