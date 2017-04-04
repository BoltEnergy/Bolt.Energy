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
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public class AdministratorsWithPermissionCheck : Administrators
    {
        UserOrOperator _operatingUserOrOperator;

        public AdministratorsWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
            :base(conn, transaction)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public override void Add(int userOrOperatorId)
        {
            CheckPermission();
            base.Add(userOrOperatorId);
        }

        private void CheckPermission()
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
        }

        public void Delete(int userOrOperatorId)
        {
            CheckPermission();
            base.Delete(userOrOperatorId, _operatingUserOrOperator);
        }

        public AdministratorWithPermissionCheck[] GetAllAdministrators(string orderField, string order)
        {
            return base.GetAllAdministrators(orderField, order, _operatingUserOrOperator);
        }
        public AdministratorWithPermissionCheck[] GetAllAdministrators()
        {
            return base.GetAllAdministrators(_operatingUserOrOperator);
        }
    }
}
