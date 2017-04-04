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
    public class UserGroupOfForumWithPermissionCheck : UserGroupOfForum
    {
        UserOrOperator _operatingUserOrOperator;

        public UserGroupOfForumWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int forumId, int groupId)
            : base(conn, transaction, groupId, forumId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public UserGroupOfForumWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int forumId, int groupId, string name, string description)
            : base(conn, transaction, groupId, forumId, name, description)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        private void CheckPermission()
        {
            CommFun.CheckAdminPanelOrModeratorPanelCommomPermission(_operatingUserOrOperator,base.ForumId);
        }

        public override void Delete(UserOrOperator operatingUserOrOperator)
        {
            CheckPermission();
            base.Delete(operatingUserOrOperator);
        }

        public UserGroupPermissionForForumWithPermissionCheck GetPermission()
        {
            return base.GetPermission(_operatingUserOrOperator);
        }

    }
}
