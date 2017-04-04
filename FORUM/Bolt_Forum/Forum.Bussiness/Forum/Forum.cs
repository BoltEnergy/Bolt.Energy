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
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;

namespace Com.Comm100.Forum.Bussiness
{
    public class Forum : ForumBase
    {

        #region private fields
        private int _numberOfTopics;
        private int _numberOfPosts;
        private int _lastPostId;
        private int _lastPostTopicId;
        private string _lastPostSubject;
        private int _lastPostCreatedUserOrOperatorId;
        private string _lastPostCreatedUserOrOperatorName;
        private bool _lastPostCreatedUserOrOperatorIfDeleted;
        private DateTime _lastPostPostTime;
        /*----------------2.0------------------*/
        private bool _ifAllowPostNeedingReplayTopic;
        private bool _ifAllowPostNeedingPayTopic;
        //private bool _ifInheritPermission;
        #endregion

        #region property
        public int NumberOfTopics
        {
            get { return this._numberOfTopics; }
        }
        public int NumberOfPosts
        {
            get { return this._numberOfPosts; }
        }
        public int LastPostId
        {
            get { return this._lastPostId; }
        }
        public int LastPostTopicId
        {
            get { return this._lastPostTopicId; }
        }
        public string LastPostSubject
        {
            get { return this._lastPostSubject; }
        }
        public int LastPostCreatedUserOrOperatorId
        {
            get { return this._lastPostCreatedUserOrOperatorId; }
        }
        public string LastPostCreatedUserOrOperatorName
        {
            get { return this._lastPostCreatedUserOrOperatorName; }
        }
        public bool LastPostCreatedUserOrOperatorIfDeleted
        {
            get { return this._lastPostCreatedUserOrOperatorIfDeleted; }
        }
        public DateTime LastPostPostTime
        {
            get { return this._lastPostPostTime; }
        }
        /*-------------------2.0--------------------*/
        public bool IfAllowPostNeedingReplayTopic
        {
            get { return this._ifAllowPostNeedingReplayTopic; }
        }
        public bool IfAllowPostNeedingPayTopic
        {
            get { return this._ifAllowPostNeedingPayTopic; }
        }
        //public bool IfInheritPermission
        //{
        //    get { return this._ifInheritPermission; }
        //}
        #endregion

        public Forum(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
            : base(conn, transaction)
        {
            this._conn = conn;
            this._transaction = transaction;
            DataTable table = ForumAccess.GetForumByForumId(_conn, _transaction, forumId);

            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowForumNotExistException(forumId);

            }
            else
            {
                _forumId = forumId;
                _categoryId = Convert.ToInt32(table.Rows[0]["CategoryId"]);
                _orderId = Convert.ToInt32(table.Rows[0]["OrderId"]);
                _name = Convert.ToString(table.Rows[0]["Name"]);
                _status = Convert.ToInt16(table.Rows[0]["Status"]);
                _numberOfTopics = Convert.ToInt32(table.Rows[0]["NumberOfTopics"]);
                _numberOfPosts = Convert.ToInt32(table.Rows[0]["NumberOfPosts"]);
                _description = Convert.ToString(table.Rows[0]["Description"]);
                _lastPostId = Convert.ToInt32(table.Rows[0]["LastPostId"]);
                _lastPostTopicId = Convert.ToInt32(table.Rows[0]["LastPostTopicId"]);
                _lastPostSubject = Convert.ToString(table.Rows[0]["LastPostSubject"]);
                _lastPostCreatedUserOrOperatorId = table.Rows[0]["LastPostCreatedUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[0]["LastPostCreatedUserOrOperatorId"]);
                _lastPostCreatedUserOrOperatorName = table.Rows[0]["LastPostCreatedUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[0]["LastPostCreatedUserOrOperatorName"]);
                _lastPostCreatedUserOrOperatorIfDeleted = table.Rows[0]["LastPostCreatedUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[0]["LastPostCreatedUserOrOperatorIfDeleted"]);
                _lastPostPostTime = Convert.ToDateTime(table.Rows[0]["LastPostPostTime"]);
                _ifAllowPostNeedingPayTopic = Convert.ToBoolean(table.Rows[0]["IfAllowPostNeedingPayTopic"]);
                _ifAllowPostNeedingReplayTopic = Convert.ToBoolean(table.Rows[0]["IfAllowPostNeedingReplayTopic"]);

            }

        }

        public Forum(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int categoryId, int orderId, string name,
            EnumForumStatus forumStatus, int numberOfTopics, int numberOfPosts, string description, int lastPostId, int lastPostTopicId, string lastPostSubject, int lastPostCreatedUserOrOperatorId,
            string lastPostCreateUserOrOperatorName, bool lastPostCreatedUserOrOperatorIfDeleted, DateTime lastPostPostTime, bool ifAllowPostNeedingReplayTopic, bool ifAllowPostNeedingPayTopic)
            : base(conn, transaction)
        {
            this._conn = conn;
            this._transaction = transaction;

            this._forumId = forumId;
            this._categoryId = categoryId;
            this._orderId = orderId;
            this._name = name;
            this._status = Convert.ToInt16(forumStatus);
            this._description = description;
            this._numberOfTopics = numberOfTopics;
            this._numberOfPosts = numberOfPosts;
            this._lastPostId = lastPostId;
            this._lastPostTopicId = lastPostTopicId;
            this._lastPostSubject = lastPostSubject;
            this._lastPostCreatedUserOrOperatorId = lastPostCreatedUserOrOperatorId;
            this._lastPostCreatedUserOrOperatorName = lastPostCreateUserOrOperatorName;
            this._lastPostCreatedUserOrOperatorIfDeleted = lastPostCreatedUserOrOperatorIfDeleted;
            this._lastPostPostTime = lastPostPostTime;
            this._ifAllowPostNeedingPayTopic = ifAllowPostNeedingPayTopic;
            this._ifAllowPostNeedingReplayTopic = ifAllowPostNeedingPayTopic;

        }

        public static int Add(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, string name, string description, bool ifAllowPostNeedingReplayTopic, bool ifAllowPostNeedingPayTopic)
        {
            CheckFieldsLength(name, description);

            return ForumAccess.AddForum(conn, transaction, categoryId, name, description, ifAllowPostNeedingReplayTopic, ifAllowPostNeedingPayTopic);
        }

        public virtual void Update(string name, string description, int oldCategoryId, int newCategoryId, EnumForumStatus forumStatus, int[] moderatorIds, UserOrOperator operatingOperator, bool ifAllowPostNeedingReplayTopic, bool ifAllowPostNeedingPayTopic)
        {
            CheckFieldsLength(name, description);
            if (oldCategoryId == newCategoryId)
                ForumAccess.UpdateForum(_conn, _transaction, _forumId, name, description, newCategoryId, forumStatus, ifAllowPostNeedingReplayTopic, ifAllowPostNeedingPayTopic);
            else
                ForumAccess.UpdateForum(_conn, _transaction, _forumId, name, description, oldCategoryId, newCategoryId, forumStatus);


            ModeratorsWithPermisisonCheck tmpModerators = new ModeratorsWithPermisisonCheck(_conn, _transaction, _forumId, operatingOperator);
            ModeratorWithPermissionCheck[] moderators = tmpModerators.GetAllModerators();
            List<int> moderatorNew = new List<int>(moderatorIds);
            List<int> moderatorRemove = new List<int>();
            if (moderators != null)
            {
                foreach (ModeratorWithPermissionCheck m in moderators)
                {
                    if (moderatorNew.Contains(m.Id))
                    {
                        moderatorNew.Remove(m.Id);
                    }
                    else
                    {
                        moderatorRemove.Add(m.Id);
                    }
                }
            }
            foreach (int mId in moderatorNew)
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

            tmpModerators.DeleteAllModerators();

            for (int i = 0; i < moderatorIds.Length; i++)
                tmpModerators.Add(moderatorIds[i]);
           
            TopicAccess.UpdateTopicForumNameByForumId(_conn, _transaction, ForumId, name);

            foreach (int mId in moderatorRemove)
            {
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
        public void Delete()
        {
            ForumAccess.DeleteForum(this._conn, this._transaction, this._forumId);
        }
        public static void CheckFieldsLength(string name, string description)
        {
            if (name.Length == 0)
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("Name");
            else
            {
                if (name.Length > ForumDBFieldLength.Forum_nameFieldLength)
                    ExceptionHelper.ThrowSystemFieldLengthExceededException("Name", ForumDBFieldLength.Forum_nameFieldLength);
            }

            if (description.Length > ForumDBFieldLength.Forum_descriptionFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Description", ForumDBFieldLength.Forum_descriptionFieldLength);
        }

        public virtual void IncreaseNumberOfTopicsByOne()
        {
            ForumAccess.UpdateNumberOfTopics(this._conn, this._transaction, this._forumId, this._numberOfTopics + 1);
        }

        public virtual void IncreaseNumberOfPostsByOne()
        {
            ForumAccess.UpdateNumberOfPosts(this._conn, this._transaction, this._forumId, this._numberOfPosts + 1);
        }

        public virtual void IncreaseNumberOfPosts(int numberOfPosts)
        {
            ForumAccess.UpdateNumberOfPosts(this._conn, this._transaction, this._forumId, this._numberOfPosts + numberOfPosts);
        }

        public virtual void DecreaseNumberOfTopicsByOne()
        {
            ForumAccess.UpdateNumberOfTopics(this._conn, this._transaction, this._forumId, this._numberOfTopics - 1);
        }

        public virtual void DecreaseNumberOfPostsByOne()
        {
            ForumAccess.UpdateNumberOfPosts(this._conn, this._transaction, this._forumId, this._numberOfPosts - 1);
        }

        public virtual void DecreaseNumberOfPosts(int numberOfPosts)
        {
            ForumAccess.UpdateNumberOfPosts(this._conn, this._transaction, this._forumId, this._numberOfPosts - numberOfPosts);
        }

        public virtual ModeratorsWithPermisisonCheck GetModerators(UserOrOperator operatingOperator)
        {
            return new ModeratorsWithPermisisonCheck(_conn, _transaction, _forumId, operatingOperator);
        }

        public virtual void UpdateLastCreateInfo(int lastCreateUserOrOperator, DateTime lastCreateTime, int lastPostId, int lastPostTopicId, string lastPostSubject)
        {
            ForumAccess.UpdateLastCreateInfo(this._conn, this._transaction, this._forumId, lastCreateUserOrOperator, lastCreateTime, lastPostId, lastPostTopicId, lastPostSubject);
        }

        public virtual PostWithPermissionCheck GetLastPost(UserOrOperator operatoringOperator)
        {
            DataTable table = new DataTable();
            table = PostAccess.GetLastPostByForumId(this._conn, this._transaction, this._forumId);
            PostWithPermissionCheck post = null;
            if (table.Rows.Count != 0)
            {
                post = new PostWithPermissionCheck(this._conn, this._transaction, Convert.ToInt32(table.Rows[0]["Id"]), operatoringOperator,
                    Convert.ToInt32(table.Rows[0]["TopicId"]), Convert.ToBoolean(table.Rows[0]["IfTopic"]), Convert.ToInt32(table.Rows[0]["Layer"]),
                    Convert.ToString(table.Rows[0]["Subject"]), Convert.ToString(table.Rows[0]["Content"]), Convert.ToInt32(table.Rows[0]["PostUserOrOperatorId"]),
                    Convert.ToString(table.Rows[0]["postUserOrOperatorName"]), Convert.ToBoolean(table.Rows[0]["IfCustomizeAvatar"]),
                    Convert.ToString(table.Rows[0]["SystemAvatar"]), Convert.ToString(table.Rows[0]["CustomizeAvatar"]), Convert.ToString(table.Rows[0]["Signature"]), Convert.ToBoolean(table.Rows[0]["IfDeleted"]),
                    Convert.ToInt32(table.Rows[0]["Posts"]), Convert.ToDateTime(table.Rows[0]["JoinedTime"]), Convert.ToDateTime(table.Rows[0]["PostTime"]), true,
                    Convert.ToInt32(table.Rows[0]["LastEditUserOrOperatorId"]), Convert.ToString(table.Rows[0]["lastEditUserOrOperatorName"]),
                    Convert.ToBoolean(table.Rows[0]["IfLastEditUserOrOperatorDeleted"]),
                    Convert.ToDateTime(table.Rows[0]["LastEditTime"]), "");

            }
            return post;
        }

        public virtual void Sort(EnumSortMoveDirection sortDirection)
        {
            if (sortDirection == EnumSortMoveDirection.Up)
                ForumAccess.SortForumsUp(_conn, _transaction, _categoryId, _forumId);
            else if (sortDirection == EnumSortMoveDirection.Down)
                ForumAccess.SortForumsDown(_conn, _transaction, _categoryId, _forumId);
        }

        /*-------------------------2.0-----------------------------*/

        protected UserGroupsOfForumWithPermissionCheck GetUserGroups(UserOrOperator operatingOperator)
        {
            return null;
        }

        protected UserReputationGroupsOfForumWithPermissionCheck GetUserReputationGroups(UserOrOperator operatingOperator)
        {
            return null;
        }

        protected PostsOfForumWithPermissionCheck GetPosts(UserOrOperator operatingUserOrOperator)
        {
            return new PostsOfForumWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, _forumId);
        }

        protected CategoryWithPermissionCheck GetCategory(UserOrOperator operatingUserOrOperator)
        {

            return new CategoryWithPermissionCheck(_conn, _transaction, _categoryId, operatingUserOrOperator);
        }

        protected TopicsOfForumWithPermissionCheck GetTopics(UserOrOperator operatingUserOrOperator)
        {
            return new TopicsOfForumWithPermissionCheck(
                _conn, _transaction, _forumId, operatingUserOrOperator);
        }

        public ForumPermissionManager GetPermissionManager()
        {
            return new ForumPermissionManager(_conn, _transaction, _forumId);
        }

        public void UpdateIfInheritPermission(bool ifInheritPermission)
        {
            ForumAccess.UpdateIfInheritPermission(_conn, _transaction, _forumId, ifInheritPermission);
        }

        protected AnnouncementsOfForumWithPermissionCheck GetAnnouncements(UserOrOperator operatingUserOrOperator)
        {
            return null;
        }
    }
}
