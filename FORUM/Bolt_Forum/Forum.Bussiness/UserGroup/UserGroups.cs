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
    public abstract class UserGroups : UserGroupsBase
    {
        public UserGroups(SqlConnectionWithSiteId conn, SqlTransaction transaction)
            : base(conn, transaction)
        { }

        private void CheckIfEnableUserGroupFeature(UserOrOperator operatingUserOrOperator)
        {
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, operatingUserOrOperator);
            if (!forumFeature.IfEnableGroupPermission)
                ExceptionHelper.ThrowForumSettingsCloseGroupPermissionFunction();
        }
        
        public virtual int GetCountOfUserGroupsByName(string name)
        {
            return GroupAccess.GetCountOfUserGroupsByName(name, _conn, _transaction);
        }
        protected int Add(string name, string description, UserOrOperator operatingUserOrOperator)
        {
            CheckIfEnableUserGroupFeature(operatingUserOrOperator);
            return UserGroup.Add(name, description, _conn, _transaction);
        }

        protected void Delete(int groupId,UserOrOperator operatingUserOrOperator)
        {
            CheckIfEnableUserGroupFeature(operatingUserOrOperator);
            //delete group
            UserGroupWithPermissionCheck ug = new UserGroupWithPermissionCheck(_conn, _transaction, null, groupId);
            ug.Delete(operatingUserOrOperator);
        }

        public UserGroupWithPermissionCheck GetGroupById(int groupId, UserOrOperator operatingUserOrOperator)
        {
            DataTable table = GroupAccess.GetGroupById(groupId, _conn, _transaction);
            UserGroupWithPermissionCheck userGroup=null;
            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowForumUserGroupNotExistWithId(groupId);
            }
            else
            {
                DataRow r = table.Rows[0];

                userGroup= CreateGroupObject(r, operatingUserOrOperator);
            }
            return userGroup;
        }

        public virtual UserGroupWithPermissionCheck[] GetAllUserGroups(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = GroupAccess.GetAllUserGroups(this._conn, this._transaction);

            UserGroupWithPermissionCheck[] groups = new UserGroupWithPermissionCheck[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                groups[i] = CreateGroupObject(table.Rows[i], operatingUserOrOperator);
            }
            return groups;
        }

        public virtual UserGroupWithPermissionCheck[] GetGroupsByForumId(int forumId, UserOrOperator operatingUserOrOperator)
        {
            DataTable table = GroupAccess.GetGroupsByForumIdAndType(EnumUserGroupType.UserGroup, forumId, _conn, _transaction);

            UserGroupWithPermissionCheck[] groups = new UserGroupWithPermissionCheck[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                groups[i] = CreateGroupObject(table.Rows[i], operatingUserOrOperator);
            }
            return groups;
        }

        public virtual UserGroupWithPermissionCheck[] GetGroupsNotInForum(int forumId, UserOrOperator operatingUserOrOperator)
        {
            DataTable table = GroupAccess.GetGroupsNotInForum(forumId, EnumUserGroupType.UserGroup, _conn, _transaction);

            UserGroupWithPermissionCheck[] groups = new UserGroupWithPermissionCheck[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                groups[i] = CreateGroupObject(table.Rows[i], operatingUserOrOperator);
            }
            return groups;
        }

        public virtual UserGroupWithPermissionCheck[] GetUserGroupsExceptAllForumUser(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = GroupAccess.GetUserGroupsExceptAllForumUser(_conn, _transaction);

            UserGroupWithPermissionCheck[] groups = new UserGroupWithPermissionCheck[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                groups[i] = CreateGroupObject(table.Rows[i], operatingUserOrOperator);
            }
            return groups;
        }


    }
}
