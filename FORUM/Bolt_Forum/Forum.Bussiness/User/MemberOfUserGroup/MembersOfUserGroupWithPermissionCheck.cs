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
    public class MembersOfUserGroupWithPermissionCheck : MembersOfUserGroup
    {
        UserOrOperator _operatingUserOrOperator;

        public MembersOfUserGroupWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int userGroupId)
            : base(conn, transaction, userGroupId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public void Delete(int userOrOperatorId)
        {
            CheckPermission();
            base.Delete(userOrOperatorId, _operatingUserOrOperator);
        }

        private void CheckPermission()
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
        }

        public override void Add(int userOrOperatorId)
        {
            CheckPermission();
            base.Add(userOrOperatorId);
        }

        public MemberOfUserGroupWithPermissionCheck[] GetMembersByQueryAndPaging(int pageIndex, int pageSize, string emailOrDisplayNameKeyword, string orderField, out int recordCount)
        {
            return base.GetMembersByQueryAndPaging(pageIndex, pageSize, emailOrDisplayNameKeyword, orderField, out recordCount, _operatingUserOrOperator);
        }
    }
}
