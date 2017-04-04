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
    public abstract class UserReputationGroupsOfForum : UserReputationGroupsBase
    {
        private int _forumId;
        public int ForumId
        {
            get { return this._forumId; }
        }

        public UserReputationGroupsOfForum(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
            :base(conn, transaction)
        {
            _forumId = forumId;
        }

        private bool IfUserReputationGroupExist(int groupId)
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
                ExceptionHelper.ThrowForumReputationGroupOfForumHaveExistedWithGroupIdAndForumId(groupId, _forumId);
            else
            {
                GroupOfForumAccess.AddGroupToForum(groupId, _forumId, _conn, _transaction);
                UserReputationGroupPermissionWithPermissionCheck userReputationGroupPermission = new UserReputationGroupPermissionWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, groupId);
                GroupPermissionAccess.AddPermission(_conn, _transaction, groupId, _forumId,
                    userReputationGroupPermission.IfAllowViewForum,
                    userReputationGroupPermission.IfAllowViewTopic,
                    userReputationGroupPermission.IfAllowPost,
                    userReputationGroupPermission.MinIntervalForPost,
                    userReputationGroupPermission.MaxLengthOfPost,
                    userReputationGroupPermission.IfPostNotNeedModeration,
                    //userReputationGroupPermission.IfAllowHTML,
                    userReputationGroupPermission.IfAllowUrl,
                    userReputationGroupPermission.IfAllowUploadImage);
            }
        }

        protected void Delete(int groupId,UserOrOperator operatingUserOrOperator)
        {
            UserReputationGroupOfForumWithPermissionCheck urgf = new UserReputationGroupOfForumWithPermissionCheck(_conn, _transaction, operatingUserOrOperator,groupId,_forumId);
            urgf.Delete();

            UserReputationGroupPermissionForForumWithPermissionCheck urgpf = new UserReputationGroupPermissionForForumWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, groupId, _forumId);
            urgpf.Delete();
        }

        protected void DeleteAll()
        {
            GroupOfForumAccess.DeleteAllGroupFromForumRelation(_forumId, _conn, _transaction);
            GroupPermissionAccess.DeleteAllPermissionFromForum(_forumId, _conn, _transaction);
        }

        protected void UpdateGroups(UserOrOperator operatingUserOrOperator, List<int> groupIds, List<GroupPermission> permissions)
        {
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, operatingUserOrOperator);
            if (forumFeature.IfEnableReputationPermission)
            {
                UserReputationGroupOfForumWithPermissionCheck[] groupsExist = this.GetAllGroups(operatingUserOrOperator);
                List<int> groupIdsExist = new List<int>();
                for (int i = 0; i < groupsExist.Length; i++)
                {
                    groupIdsExist.Add(groupsExist[i].GroupId);
                }
                #region Add Group & Update Permission
                for (int i = 0; i < groupIds.Count; i++)
                {
                    int groupId = groupIds[i];
                    if (IfUserReputationGroupExist(groupId))
                    {
                        if (!groupIdsExist.Contains(groupIds[i]))
                            this.Add(groupId, operatingUserOrOperator);

                        var lists = from GroupPermission r in permissions
                                    where r.GroupId == groupId
                                    select r;
                        GroupPermission p = lists.First() as GroupPermission;

                        UserReputationGroupPermissionForForumWithPermissionCheck userGroupPermisisonForForum = new UserReputationGroupPermissionForForumWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, groupId, _forumId);
                        userGroupPermisisonForForum.Update(p.IfAllowViewForum, p.IfAllowViewTopic, p.IfAllowPost, p.MinIntervalTimeForPosting, p.MaxLengthOfTopicPost, p.IfPostModerationNotRequired, p.IfAllowUrl, p.IfAllowInsertImage);
                    }
                }
                #endregion

                #region Delete Group From Forum
                for (int i = 0; i < groupIdsExist.Count; i++)
                {
                    int groupId = groupIdsExist[i];
                    if (!groupIds.Contains(groupId))
                        this.Delete(groupId, operatingUserOrOperator);
                }
                #endregion
            }
        }

        protected UserReputationGroupOfForumWithPermissionCheck[] GetAllGroups(UserOrOperator operatingOperator)
        {
            DataTable table = GroupAccess.GetGroupsByForumIdAndType(
                            EnumUserGroupType.UserReputationGroup,_forumId, _conn, _transaction);

            List<UserReputationGroupOfForumWithPermissionCheck> userGroups = new List<UserReputationGroupOfForumWithPermissionCheck>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow r = table.Rows[i];
                UserReputationGroupOfForumWithPermissionCheck usergroup =  
                    new UserReputationGroupOfForumWithPermissionCheck(_conn, _transaction, operatingOperator,
                    Convert.ToInt32(r["id"]),_forumId, r["name"].ToString(), r["description"].ToString(), Convert.ToInt32(r["limitedBegin"]),
                    Convert.ToInt32(r["limitedExpire"]), Convert.ToInt32(r["icoRepeat"]));
                userGroups.Add(usergroup);
            }
            return userGroups.ToArray<UserReputationGroupOfForumWithPermissionCheck>();
        }
    }
}
