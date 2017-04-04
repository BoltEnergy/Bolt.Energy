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
    public class UserGroupWithPermissionCheck : UserGroup
    {
        UserOrOperator _operatingUserOrOperator;

        public UserGroupWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int groupId)
            : base(conn, transaction, groupId)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public UserGroupWithPermissionCheck(
            SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator,
            int id, string name, string description, bool ifAllForumUsersGroup)
            : base(conn, transaction, id, name, description, ifAllForumUsersGroup)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public void Update(string name, string description)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            base.Update(name, description,_operatingUserOrOperator);
        }

        public void Delete()
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            base.Delete(_operatingUserOrOperator);
        }

        public MembersOfUserGroupWithPermissionCheck GetMembers()
        {
            return base.GetMembers(_operatingUserOrOperator);
        }

        public UserGroupPermissionWithPermissionCheck GetPermission()
        {
            return base.GetPermission(_operatingUserOrOperator);
        }

        public UserGroupOfForumWithPermissionCheck MakeThisInForum(int forumId)
        {
            return base.MakeThisInForum(forumId, _operatingUserOrOperator);
        }
    }
}
