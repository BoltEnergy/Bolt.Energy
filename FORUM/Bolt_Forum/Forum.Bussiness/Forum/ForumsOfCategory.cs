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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Database;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class ForumsOfCategory : ForumsBase
    {
        private int _categoryId;

        public int CategoryId
        {
            get { return this._categoryId; }
        }

        public ForumsOfCategory(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId)
            :base(conn, transaction)
        {
            this._conn = conn;
            this._transaction = transaction;
            this._categoryId = categoryId;
        }

        public virtual ForumWithPermissionCheck[] GetAllForums(UserOrOperator operatingOperator)
        {
            DataTable table = new DataTable();
            table = ForumAccess.GetForumsByCategoryId(_conn, _transaction, _categoryId);
            ForumWithPermissionCheck[] forums = new ForumWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                
                //forums[i] = new ForumWithPermissionCheck(_conn, _transaction, Convert.ToInt32(table.Rows[i]["Id"]), operatingOperator,
                //    Convert.ToInt32(table.Rows[i]["CategoryId"]), Convert.ToInt32(table.Rows[i]["OrderId"]),
                //    Convert.ToString(table.Rows[i]["Name"]), (EnumForumStatus)Convert.ToInt16((table.Rows[i]["Status"])),
                //    Convert.ToInt32(table.Rows[i]["NumberOfTopics"]), Convert.ToInt32(table.Rows[i]["NumberOfPosts"]),
                //    Convert.ToString(table.Rows[i]["Description"]), Convert.ToInt32(table.Rows[i]["LastPostId"]),
                //    Convert.ToInt32(table.Rows[i]["LastPostTopicId"]),
                //    Convert.ToString(table.Rows[i]["LastPostSubject"]), 
                //    table.Rows[i]["LastPostCreatedUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[i]["LastPostCreatedUserOrOperatorId"]),
                //    table.Rows[i]["LastPostCreatedUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[i]["LastPostCreatedUserOrOperatorName"]),
                //    table.Rows[i]["LastPostCreatedUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[i]["LastPostCreatedUserOrOperatorIfDeleted"]), 
                //    Convert.ToDateTime(table.Rows[i]["LastPostPostTime"]),
                //    Convert.ToBoolean(table.Rows[i]["IfAllowPostNeedingReplayTopic"]) , 
                //    Convert.ToBoolean(table.Rows[i]["IfAllowPostNeedingPayTopic"]));

                
                forums[i] = new ForumWithPermissionCheck(_conn, _transaction, Convert.ToInt32(table.Rows[i]["Id"]), operatingOperator,
                    Convert.ToInt32(table.Rows[i]["CategoryId"]), Convert.ToInt32(table.Rows[i]["OrderId"]),
                    Convert.ToString(table.Rows[i]["Name"]), (EnumForumStatus)Convert.ToInt16((table.Rows[i]["Status"])),
                    PostAccess.GetCountOfNotDeletedTopicsFirstPostByForumId(_conn, _transaction, Convert.ToInt32(table.Rows[i]["Id"])), PostAccess.GetCountOfNotDeletedPostsByForumId(_conn, _transaction, Convert.ToInt32(table.Rows[i]["Id"])),
                    Convert.ToString(table.Rows[i]["Description"]), Convert.ToInt32(table.Rows[i]["LastPostId"]),
                    Convert.ToInt32(table.Rows[i]["LastPostTopicId"]),
                    Convert.ToString(table.Rows[i]["LastPostSubject"]),
                    table.Rows[i]["LastPostCreatedUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[i]["LastPostCreatedUserOrOperatorId"]),
                    table.Rows[i]["LastPostCreatedUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[i]["LastPostCreatedUserOrOperatorName"]),
                    table.Rows[i]["LastPostCreatedUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[i]["LastPostCreatedUserOrOperatorIfDeleted"]),
                    Convert.ToDateTime(table.Rows[i]["LastPostPostTime"]),
                    Convert.ToBoolean(table.Rows[i]["IfAllowPostNeedingReplayTopic"]),
                    Convert.ToBoolean(table.Rows[i]["IfAllowPostNeedingPayTopic"]));
            }
            return forums;

        }

        public virtual int GetCountOfAllForums()
        {
            return ForumAccess.GetCountOfForumsByCategoryId(_conn, _transaction, _categoryId);            
        }

        public virtual ForumWithPermissionCheck[] GetAllNotHiddenForums(UserOrOperator operatingOperator)
        {
            DataTable table = new DataTable();
            table = ForumAccess.GetNotHiddenForumsByCategoryId(_conn, _transaction, _categoryId);
            ForumWithPermissionCheck[] forums = new ForumWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                int lastposttopicid = Convert.ToInt32(table.Rows[i]["LastPostTopicId"]);
                forums[i] = new ForumWithPermissionCheck(_conn, _transaction, Convert.ToInt32(table.Rows[i]["Id"]),operatingOperator,
                    Convert.ToInt32(table.Rows[i]["CategoryId"]), Convert.ToInt32(table.Rows[i]["OrderId"]),
                    Convert.ToString(table.Rows[i]["Name"]), (EnumForumStatus)Convert.ToInt16((table.Rows[i]["Status"])),
                    Convert.ToInt32(table.Rows[i]["NumberOfTopics"]), Convert.ToInt32(table.Rows[i]["NumberOfPosts"]),
                    Convert.ToString(table.Rows[i]["Description"]), Convert.ToInt32(table.Rows[i]["LastPostId"]),
                    Convert.ToInt32(table.Rows[i]["LastPostTopicId"]),
                    Convert.ToString(table.Rows[i]["LastPostSubject"]), 
                    table.Rows[i]["LastPostCreatedUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[i]["LastPostCreatedUserOrOperatorId"]),
                    table.Rows[i]["LastPostCreatedUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[i]["LastPostCreatedUserOrOperatorName"]),
                    table.Rows[i]["LastPostCreatedUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[i]["LastPostCreatedUserOrOperatorIfDeleted"]),
                    Convert.ToDateTime(table.Rows[i]["LastPostPostTime"]),
                    Convert.ToBoolean(table.Rows[i]["IfAllowPostNeedingReplayTopic"]),
                    Convert.ToBoolean(table.Rows[i]["IfAllowPostNeedingPayTopic"]));
            }
            return forums;

        }

        public int Add(string name, string description, int[] moderatorIds, UserOrOperator operatingOperator,bool ifAllowPostNeedingReplayTopic,bool ifAllowPostNeedingPayTopic)
        {
            if (moderatorIds != null)
            {
                foreach (int mId in moderatorIds)
                {
                    UserOrOperator user = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, null, mId);
                    /*2.0 stategy */
                    ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                        _conn, _transaction, null, _conn.SiteId);
                    scoreStrategySetting.UserBeforeAddModerator(user);
                    /*2.0 reputation strategy*/
                    ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                        _conn, _transaction, null, _conn.SiteId);
                    reputationStrategySetting.UserBeforeAddModerator(user);
                }
            }
            
            int forumId = Forum.Add(_conn, _transaction, _categoryId, name, description, ifAllowPostNeedingReplayTopic, ifAllowPostNeedingPayTopic);

            ModeratorsWithPermisisonCheck tmpModerators = new ModeratorsWithPermisisonCheck(_conn, _transaction, forumId, operatingOperator);

            for (int i = 0; i < moderatorIds.Length; i++)
                tmpModerators.Add(moderatorIds[i]);

           
            return forumId;
        }

        public void Delete(int forumId, UserOrOperator operatingOperator)
        {
            TopicsOfForumWithPermissionCheck tmpTopicsWithPermissionCheck = new TopicsOfForumWithPermissionCheck(_conn, _transaction, forumId, operatingOperator);
            TopicWithPermissionCheck[] tmpTopics = tmpTopicsWithPermissionCheck.GetAllTopics();

            tmpTopicsWithPermissionCheck.DeleteAllTopics();

            ModeratorsWithPermisisonCheck moderators = new ModeratorsWithPermisisonCheck(this._conn, this._transaction, forumId, operatingOperator);
            ModeratorWithPermissionCheck[] amoderators = moderators.GetAllModerators();
            
            moderators.DeleteAllModerators();

            AnnouncementsOfForumWithPermissionCheck announcementsOfForum = new AnnouncementsOfForumWithPermissionCheck(_conn,_transaction,operatingOperator,forumId);
            announcementsOfForum.DeleteAllAnnouncement(operatingOperator);

            UserGroupsOfForumWithPermissionCheck userGroupOfForum = new UserGroupsOfForumWithPermissionCheck(_conn, _transaction, operatingOperator, forumId);
            userGroupOfForum.DeleteAll();

            UserReputationGroupsOfForumWithPermissionCheck userReputationGroupsOfForum = new UserReputationGroupsOfForumWithPermissionCheck(_conn, _transaction, operatingOperator, forumId);
            userReputationGroupsOfForum.DeleteAll();

            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, forumId, operatingOperator);

            forum.Delete();

            if (amoderators != null)
            {
                foreach (ModeratorWithPermissionCheck moderatorTemp in amoderators)
                {
                    int mId = moderatorTemp.Id;
                    UserOrOperator user = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, null, mId);
                    /*2.0 stategy */
                    ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                        _conn, _transaction, null, _conn.SiteId);
                    scoreStrategySetting.UserAfterRemoveModerator(user);
                    /*2.0 reputation strategy*/
                    ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                        _conn, _transaction, null, _conn.SiteId);
                    reputationStrategySetting.UserAfterRemoveModerator(user);
                }
            }
        }

        public virtual CategoryWithPermissionCheck GetCategory(UserOrOperator operatingOperator)
        {
            return new CategoryWithPermissionCheck(_conn, _transaction, _categoryId, operatingOperator);
        }


    }
}
