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
    public class CategoryWithPermissionCheck : Category
    {
        UserOrOperator _operatingUserOrOperator;

        public CategoryWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction, categoryId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public CategoryWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, UserOrOperator operatingUserOrOperator, int orderId, string name, string description,EnumCategoryStatus categoryStatus)
            : base(conn, transaction, categoryId, orderId, name, description,categoryStatus)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        private void CheckPermission()
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
        }

        public new void Update(string name, string description,EnumCategoryStatus categoryStatus)
        {
            CheckPermission();
            base.Update(name, description,categoryStatus);
        }
        
        public new void Delete()
        {
            CheckPermission();
            base.Delete();
        }

        public ForumsOfCategoryWithPermissionCheck GetForums()
        {
            return base.GetForums(this._operatingUserOrOperator);
        }
    }
}
