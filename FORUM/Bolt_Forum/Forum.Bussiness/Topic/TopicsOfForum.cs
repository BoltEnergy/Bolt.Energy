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


namespace Com.Comm100.Forum.Bussiness
{
    public abstract class TopicsOfForum : TopicsBase
    {
        private int _forumId;

        public int ForumId
        {
            get { return this._forumId; }
        }

        public TopicsOfForum(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
            : base(conn, transaction)
        {
            _forumId = forumId;
        }

        private ForumWithPermissionCheck GetForum(UserOrOperator operatingOperator)
        {
            return new ForumWithPermissionCheck(_conn, _transaction, _forumId, operatingOperator);
        }

        public virtual int Add(string subject, DateTime postTime, string content, UserOrOperator operatingOperator,
            int score, bool ifReplyRequired, bool ifPayScoreRequired,
            bool ifContainsPoll, bool ifMultipleChoice, int maxChoices, bool ifSetDeadline,
            DateTime startDate, DateTime endDate, string[] options,
            int[] attachIds, int[] scores, string[] descriptions, bool ifTopicModeration)
        {
            ForumWithPermissionCheck forum = this.GetForum(operatingOperator);
            if (!forum.IfAllowPostNeedingPayTopic && ifPayScoreRequired)
            {
                ExceptionHelper.ThrowForumPostNeedingPayTopicNotAllowException();
            }
            else if (!forum.IfAllowPostNeedingReplayTopic && ifReplyRequired)
            {
                ExceptionHelper.ThrowForumPostNeedingReplayTopicNotAllowException();
            }

            int topicId = Topic.Add(_conn, _transaction, operatingOperator, ForumId,
                subject, operatingOperator.Id, postTime, content
                , score, ifReplyRequired, ifPayScoreRequired,
                ifContainsPoll, ifMultipleChoice, maxChoices, ifSetDeadline,
                startDate, endDate, options, attachIds, scores, descriptions, ifTopicModeration);

            forum.IncreaseNumberOfTopicsByOne();

            /*2.0 stategy */
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingOperator, _conn.SiteId);
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, topicId, operatingOperator);
            scoreStrategySetting.UseAfterPostTopic(topic, operatingOperator);

            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingOperator, _conn.SiteId);
            reputationStrategySetting.UseAfterPostTopic(topic, operatingOperator);
            return topicId;
        }

        public TopicWithPermissionCheck[] GetAllTopics(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = new DataTable();
            table = TopicAccess.GetAllTopics(_conn, _transaction, _forumId);
            TopicWithPermissionCheck[] topics = new TopicWithPermissionCheck[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                topics[i] = CreateTopicObject(table.Rows[i], operatingUserOrOperator);
            }
            return topics;
        }


        public virtual TopicWithPermissionCheck[] GetTopicsByPaging(int pageIndex, int pageSize,
            UserOrOperator operatingUserOrOperator, out int count)
        {
            count = TopicAccess.GetCountOfTopicsBytForumId(_conn, _transaction, _forumId);
            int tmpPageIndex = pageIndex;
            if (count <= ((pageIndex-1) * pageSize))
                tmpPageIndex = 1;
            DataTable table = TopicAccess.GetTopicsByForumIdAndPaging(_conn, _transaction, _forumId, tmpPageIndex, pageSize);
            TopicWithPermissionCheck[] topics = new TopicWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                topics[i] = CreateTopicObject2(table.Rows[i], operatingUserOrOperator);
            }
            return topics;

        }

        public virtual Int32 GetCountOfTopicsByForumId()
        {
            return TopicAccess.GetCountOfTopicsBytForumId(_conn, _transaction, _forumId);
        }

        public virtual Int32 GetCountOfFeaturedTopicsByForumId()
        {
            return TopicAccess.GetCountOfFeaturedTopicsBytForumId(_conn, _transaction, _forumId);
        }

        public virtual TopicWithPermissionCheck[] GetTopicsByPagingWithoutWaitingForModeration(int pageIndex, int pageSize,
            UserOrOperator operatingUserOrOperator, out int count)
        {
            count = TopicAccess.GetCountOfTopicsByForumIdWithoutWaitingForModeration(_conn, _transaction, _forumId);
            int tmpPageIndex = pageIndex;
            if (count <= ((pageIndex - 1) * pageSize))
                tmpPageIndex = 1;
            DataTable table = TopicAccess.GetTopicsByForumIdAndPagingWithoutWaitingForModeration(_conn, _transaction, _forumId, tmpPageIndex, pageSize);
            TopicWithPermissionCheck[] topics = new TopicWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                topics[i] = CreateTopicObject2(table.Rows[i], operatingUserOrOperator);
            }
            return topics;
            //return table;
        }

        /* update by techtier on 3/1/2017 for adding sorting by most popular */
        public virtual TopicWithPermissionCheck[] GetTopicsByPagingWithoutWaitingForModerationByManualSort(int pageIndex, int pageSize,
            UserOrOperator operatingUserOrOperator,string sortKeyword, out int count)
        {
            count = TopicAccess.GetCountOfTopicsByForumIdWithoutWaitingForModeration(_conn, _transaction, _forumId);
            int tmpPageIndex = pageIndex;
            if (count <= ((pageIndex - 1) * pageSize))
                tmpPageIndex = 1;
            DataTable table = TopicAccess.GetTopicsByForumIdAndPagingWithoutWaitingForModerationByManualSort(_conn, _transaction, _forumId, tmpPageIndex, pageSize, sortKeyword);
            TopicWithPermissionCheck[] topics = new TopicWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                topics[i] = CreateTopicObject2(table.Rows[i], operatingUserOrOperator);
            }
            return topics;
        }

        public virtual TopicWithPermissionCheck[] GetTopicsByPagingWithoutWaitingForModerationByManualMyPost(int pageIndex, int pageSize,
            UserOrOperator operatingUserOrOperator, int operatingUserOrOperatorId, out int count)
        {
            count = TopicAccess.GetCountOfTopicsByForumIdWithoutWaitingForModeration(_conn, _transaction, _forumId);
            int tmpPageIndex = pageIndex;
            if (count <= ((pageIndex - 1) * pageSize))
                tmpPageIndex = 1;
            DataTable table = TopicAccess.GetTopicsByForumIdAndPagingWithoutWaitingForModerationByManualMyPost(_conn, _transaction, _forumId, tmpPageIndex, pageSize, operatingUserOrOperatorId);
            TopicWithPermissionCheck[] topics = new TopicWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                topics[i] = CreateTopicObject2(table.Rows[i], operatingUserOrOperator);
            }
            return topics;
        }
        
        //techtier for search
        public virtual TopicWithPermissionCheck[] GetTopicsByPagingWithoutWaitingForModerationByManualSearch(int pageIndex, int pageSize,
          UserOrOperator operatingUserOrOperator, string searchKeyword, out int count)
        {
            count = TopicAccess.GetCountOfTopicsByForumIdWithoutWaitingForModerationSearch(_conn, _transaction, _forumId, searchKeyword);
            int tmpPageIndex = pageIndex;
            if (count <= ((pageIndex - 1) * pageSize))
                tmpPageIndex = 1;
            DataTable table = TopicAccess.GetTopicsByForumIdAndPagingWithoutWaitingForModerationByManualSearch(_conn, _transaction, _forumId, tmpPageIndex, pageSize, searchKeyword);
            TopicWithPermissionCheck[] topics = new TopicWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                topics[i] = CreateTopicObject2(table.Rows[i], operatingUserOrOperator);
            }
            return topics;
        }


        public virtual TopicWithPermissionCheck[] GetFeaturedTopicsByPaging(
            int pageIndex, int pageSize, UserOrOperator operatingUserOrOperator, out int count)
        {
            count = TopicAccess.GetCountOfFeaturedTopicsByForumIdAndPaging(_conn, _transaction, _forumId);
            DataTable table = new DataTable();
            table = TopicAccess.GetFeaturedTopicsByForumIdAndPaging(_conn, _transaction, _forumId, pageIndex, pageSize);
            TopicWithPermissionCheck[] topics = new TopicWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                topics[i] = CreateTopicObject2(table.Rows[i], operatingUserOrOperator);
            }
            return topics;

        }

        public virtual TopicWithPermissionCheck[] GetFeaturedTopicsByPagingWithoutWaitingForModeration(
            int pageIndex, int pageSize, UserOrOperator operatingUserOrOperator, out int count)
        {
            count = TopicAccess.GetCountOfFeaturedTopicsByForumIdAndPaging(_conn, _transaction, _forumId);
            DataTable table = new DataTable();
            table = TopicAccess.GetFeaturedTopicsByForumIdAndPagingWithoutWaitingForModeration(_conn, _transaction, _forumId, pageIndex, pageSize);
            TopicWithPermissionCheck[] topics = new TopicWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                topics[i] = CreateTopicObject2(table.Rows[i], operatingUserOrOperator);
            }
            return topics;

        }

        public virtual void Delete(int topicId, UserOrOperator operatingOperator)
        {
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, topicId, operatingOperator);

            topic.DeleteTopicsOfForum();
        }

        public void DeleteAllTopics(UserOrOperator operatingOperator)
        {
            TopicWithPermissionCheck[] topicArray = this.GetAllTopics(operatingOperator);

            foreach (TopicWithPermissionCheck topic in topicArray)
            {
                this.Delete(topic.TopicId, operatingOperator);
            }
        }

        /*-------------------------2.0-----------------------*/
        public override TopicWithPermissionCheck[] GetNotDeletedTopicsByQueryAndPaging(
       UserOrOperator operatingUserOrOperator, string keywords, string name,
         DateTime startDate, DateTime endDate, int pageIndex, int pageSize,
         string orderField, string orderMethod, out int countOfTopics)
        {
            countOfTopics = TopicAccess.GetCountOfNotDeletedTopicsOfForumByQueryAndPaging(
                _conn, _transaction, name, startDate, endDate, _forumId, keywords);
            DataTable dt = TopicAccess.GetNotDeletedTopicsOfForumByQueryAndPaging(
                _conn, _transaction, name, startDate, endDate, pageIndex, pageSize, _forumId, keywords, orderField, orderMethod);
            List<TopicWithPermissionCheck> topics = new List<TopicWithPermissionCheck>();
            foreach (DataRow dr in dt.Rows)
            {
                #region Init Data
                int topicId = Convert.ToInt32(dr["Id"]);
                int TopicforumId = Convert.ToInt32(dr["ForumId"]);
                string subject = Convert.ToString(dr["Subject"]);
                int postUserOrOperatorId = Convert.ToInt32(dr["PostUserOrOperatorId"]);
                DateTime postTime = Convert.ToDateTime(dr["PostTime"]);
                string postUserOrOperatorName = Convert.ToString(dr["PostUserOrOperatorName"]);
                DateTime lastPostTime = Convert.ToDateTime(dr["LastPostTime"]);
                int lastPostUserOrOperatorId = Convert.ToInt32(dr["LastPostUserOrOperatorId"]);
                string lastPostUserOrOperatorName = Convert.ToString(dr["LastPostUserName"]);
                bool ifClosed = Convert.ToBoolean(dr["ifClosed"]);
                bool ifSticky = Convert.ToBoolean(dr["ifSticky"]);
                bool ifMarkedAsAnswer = Convert.ToBoolean(dr["ifMarkedAsAnswer"]);

                int numberOfReplies = Convert.ToInt32(dr["NumberOfReplies"]);
                int numberOfHits = Convert.ToInt32(dr["NumberOfHits"]);
                #endregion

                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction,
                    topicId, operatingUserOrOperator, TopicforumId, "", subject, postUserOrOperatorId, postUserOrOperatorName,
                    false, postTime, -1, lastPostTime, lastPostUserOrOperatorId, lastPostUserOrOperatorName,
                    false, numberOfReplies, numberOfHits, ifClosed, ifMarkedAsAnswer, ifSticky, new int[] { });

                topics.Add(topic);
            }
            return topics.ToArray<TopicWithPermissionCheck>();
        }

        //public abstract override TopicWithPermissionCheck[] GetNotDeletedTopicsByQueryAndPaging(
        //    string keywords, int posterId, DateTime startDate, DateTime endDate, int pageIndex, int pageSize, out int countOfTopics);
    }
}
