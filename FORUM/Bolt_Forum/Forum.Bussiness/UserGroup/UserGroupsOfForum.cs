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
    public abstract class UserGroupsOfForum : UserGroupsBase
    {
        private int _forumId;

        public int ForumId
        {
            get { return _forumId; }
        }

        public UserGroupsOfForum(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
            : base(conn, transaction)
        {
            this._forumId = forumId;
        }

        private bool IfUserGroupExist(int groupId)
        {
            DataTable table = GroupAccess.GetGroupById(groupId, _conn, _transaction);
            if (table.Rows.Count <= 0)
                return false;
            else
                return true;
        }

        protected void Add(int groupId,UserOrOperator operatingUserOrOperator)
        {
            if (GroupAccess.CheckGroupOfForumExist(_conn, _transaction, groupId, _forumId))
                ExceptionHelper.ThrowForumUserGroupOfForumHaveExistedWithGroupIdAndForumId(groupId, _forumId);
            else
            {
                GroupOfForumAccess.AddGroupToForum(groupId, _forumId, _conn, _transaction);
                UserGroupPermissionWithPermissionCheck userGroupPermission = new UserGroupPermissionWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, groupId);
                GroupPermissionAccess.AddPermission(_conn, _transaction, groupId, _forumId,
                    userGroupPermission.IfAllowViewForum,
                    userGroupPermission.IfAllowViewTopic,
                    userGroupPermission.IfAllowPost,
                    userGroupPermission.MinIntervalForPost,
                    userGroupPermission.MaxLengthOfPost,
                    userGroupPermission.IfPostNotNeedModeration,
                    //userGroupPermission.IfAllowHTML,
                    userGroupPermission.IfAllowUrl,
                    userGroupPermission.IfAllowUploadImage);
            }
        }

        protected void Delete(int groupId, UserOrOperator operatingUserOrOperator)
        {
            UserGroupOfForumWithPermissionCheck ugf = new UserGroupOfForumWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, _forumId, groupId);
            ugf.Delete(operatingUserOrOperator) ;

            UserGroupPermissionForForumWithPermissionCheck ugpf = new UserGroupPermissionForForumWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, groupId, _forumId);
            ugpf.Delete();
        }

        protected void DeleteAll()
        {
            GroupOfForumAccess.DeleteAllGroupFromForumRelation(_forumId, _conn, _transaction);
            GroupPermissionAccess.DeleteAllPermissionFromForum(_forumId, _conn, _transaction);
        }

        protected void UpdateGroups(UserOrOperator operatingUserOrOperator,List<int> groupIds,List<GroupPermission> permissions)
        {
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, operatingUserOrOperator);
            if (forumFeature.IfEnableGroupPermission)
            {
                UserGroupOfForumWithPermissionCheck[] groupsExist= this.GetAllUserGroups(operatingUserOrOperator);
                List<int> groupIdsExist = new List<int>();
                for (int i = 0; i < groupsExist.Length; i++)
                {
                    groupIdsExist.Add(groupsExist[i].UserGroupId);
                }
                #region Add Group & Update Permission
                for (int i = 0; i < groupIds.Count; i++)
                {
                    int groupId=groupIds[i];
                    if(IfUserGroupExist(groupId))
                    {
                        if (!groupIdsExist.Contains(groupIds[i]))
                            this.Add(groupId, operatingUserOrOperator);

                        var lists = from GroupPermission r in permissions
                                    where r.GroupId == groupId
                                    select r;
                        GroupPermission p = lists.First() as GroupPermission;

                        UserGroupPermissionForForumWithPermissionCheck userGroupPermisisonForForum = new UserGroupPermissionForForumWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, groupId, _forumId);
                        userGroupPermisisonForForum.Update(p.IfAllowViewForum, p.IfAllowViewTopic, p.IfAllowPost, p.MinIntervalTimeForPosting, p.MaxLengthOfTopicPost, p.IfPostModerationNotRequired, p.IfAllowUrl, p.IfAllowInsertImage);
                    }
                }
                #endregion

                #region Delete Group
                for (int i = 0; i < groupIdsExist.Count; i++)
                {
                    int groupId = groupIdsExist[i];
                    if (!groupIds.Contains(groupId))
                        this.Delete(groupId, operatingUserOrOperator);
                }
                #endregion
            }
        }

        protected UserGroupOfForumWithPermissionCheck[] GetAllUserGroups(UserOrOperator operatingOperator)
        {
            DataTable table = GroupAccess.GetUserGroupsOfForumByForumId(_conn, _transaction, _forumId);

            List<UserGroupOfForumWithPermissionCheck> groups = new List<UserGroupOfForumWithPermissionCheck>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                UserGroupOfForumWithPermissionCheck group = CreateGroupOfForumObject(table.Rows[i], operatingOperator, _forumId);
                groups.Add(group);
            }

            return groups.ToArray<UserGroupOfForumWithPermissionCheck>();
        }
    }
}
