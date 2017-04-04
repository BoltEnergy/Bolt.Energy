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
    public abstract class UserReputationGroups : UserReputationGroupsBase
    {
        public UserReputationGroups(SqlConnectionWithSiteId conn, SqlTransaction transaction)
            : base(conn, transaction)
        { }

        #region Private Function CheckIfEnableReputation
        private void CheckIfEnableReputation()
        {
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, null);
            if (!forumFeature.IfEnableReputation)
                ExceptionHelper.ThrowForumSettingsCloseReputationFunctio();
            //if (!forumFeature.IfEnableReputationPermission)
            //    ExceptionHelper.ThrowForumSettingsCloseGroupPermissionFunction();
        }
        #endregion Private Function CheckIfEnableReputation

        public virtual int Add(string name, string description, int limitedBegin, int limitedExpire, int icoRepeat)
        {
            CheckIfEnableReputation();
            return UserReputationGroupWithPermissionCheck.Add(name, description, limitedBegin, limitedExpire, icoRepeat, _conn, _transaction);
        }

        public void Delete(int groupId,UserOrOperator operatingUserOrOperator)
        {
            CheckIfEnableReputation();
            UserReputationGroupWithPermissionCheck userReputationGroup = new UserReputationGroupWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, groupId);
            userReputationGroup.Delete();
        }

        public virtual UserReputationGroupWithPermissionCheck[] GetAllGroups(UserOrOperator operatingOperator)
        {

            DataTable table = GroupAccess.GetAllReputationGroups(_conn, _transaction);

            UserReputationGroupWithPermissionCheck[] userGroups = new UserReputationGroupWithPermissionCheck[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow r = table.Rows[i];

                userGroups[i] = new UserReputationGroupWithPermissionCheck(_conn, _transaction, operatingOperator,
                    Convert.ToInt32(r["id"]), r["name"].ToString(), r["description"].ToString(), Convert.ToInt32(r["limitedBegin"]),
                    Convert.ToInt32(r["limitedExpire"]), Convert.ToInt32(r["icoRepeat"]));                    
            }

            return userGroups;
        }

        public UserReputationGroupWithPermissionCheck GetUserReputationGroupOfUserOrOperator(UserOrOperator operatingUserOrOperator, int userOrOperatorId)
        {
            int reputationId = GroupAccess.GetUserReputationGroupByUserOrOperator(userOrOperatorId, _conn, _transaction);
            if (reputationId <= 0)
                return null;
            else
                return new UserReputationGroupWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, reputationId);
        }

        public virtual UserReputationGroupWithPermissionCheck[] GetGroupsByForumId(int forumId, UserOrOperator operatingUserOrOperator)
        {
            DataTable table = GroupAccess.GetGroupsByForumIdAndType(EnumUserGroupType.UserReputationGroup, forumId, _conn, _transaction);

            UserReputationGroupWithPermissionCheck[] groups = new UserReputationGroupWithPermissionCheck[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                groups[i] = this.CreateGroupObject(table.Rows[i], operatingUserOrOperator);
            }
            return groups;
        }

        public virtual UserReputationGroupWithPermissionCheck[] GetGroupsNotInForum(int forumId, UserOrOperator operatingUserOrOperator)
        {
            DataTable table = GroupAccess.GetGroupsNotInForum(forumId, EnumUserGroupType.UserReputationGroup, _conn, _transaction);

            UserReputationGroupWithPermissionCheck[] groups = new UserReputationGroupWithPermissionCheck[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                groups[i] = CreateGroupObject(table.Rows[i], operatingUserOrOperator);
            }
            return groups;
        }
    }
}
