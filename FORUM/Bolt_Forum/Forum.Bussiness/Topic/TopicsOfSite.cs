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
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.ASPNETState;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class TopicsOfSite : TopicsBase
    {
        public TopicsOfSite(SqlConnectionWithSiteId conn, SqlTransaction transaction)
            : base(conn, transaction)
        { }

        public override TopicWithPermissionCheck[] GetNotDeletedTopicsByQueryAndPaging(
         UserOrOperator operatingUserOrOperator, string keywords, string name,
           DateTime startDate, DateTime endDate, int pageIndex, int pageSize,
           string orderField, string orderMethod, out int countOfTopics)
        {
            countOfTopics = TopicAccess.GetCountOfAllNotDeletedTopicsOfForumByQueryAndPaging(
                _conn, _transaction, name, startDate, endDate, keywords);
            DataTable dt = TopicAccess.GetAllNotDeletedTopicsOfForumByQueryAndPaging(
                _conn, _transaction, name, startDate, endDate, pageIndex, pageSize,keywords, orderField, orderMethod);
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

                int numberOfReplies = PostAccess.GetCountOfNotDeletedPostsByTopicId(_conn, _transaction, topicId); //Convert.ToInt32(dr["NumberOfReplies"]);
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

        //public abstract override TopicWithPermissionCheck[] GetNotDeletedTopicsByQueryAndPaging(string keywords, int posterId, DateTime startDate, DateTime endDate, int pageIndex, int pageSize, out int countOfTopics);

        public TopicWithPermissionCheck[] GetNotDeletedTopicsByModeratorWithQueryAndPaging(
            UserOrOperator operatingUserOrOperator, int moderatorId,int forumId, string keywords, string name,
            DateTime startDate, DateTime endDate, int pageIndex, int pageSize,
            string orderField, string orderMethod)
        {
            DataTable dt;
            if (forumId >= 0)
                dt = TopicAccess.GetNotDeletedTopicsByModeratorWithQueryAndPaging(
                _conn, _transaction, moderatorId, name, startDate, endDate, forumId, keywords, pageIndex, pageSize, orderField, orderMethod);
            else
                dt = TopicAccess.GetAllNotDeletedTopicsByModeratorWithQueryAndPaging(_conn
                    , _transaction, moderatorId, name, startDate, endDate, keywords, pageIndex, pageSize, orderField, orderMethod);
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

                int numberOfReplies = PostAccess.GetCountOfNotDeletedPostsByTopicId(_conn, _transaction, topicId);//Convert.ToInt32(dr["NumberOfReplies"]);
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

        public int GetCountOfNotDeletedTopicsByModeratorWithQuery(
            int moderatorId, string keywords, string name, DateTime startDate, DateTime endDate,int forumId)
        {
            if (forumId >= 0)
                return TopicAccess.GetCountOfNotDeletedTopicsByModeratorWithQuery(_conn, _transaction
                    , moderatorId, keywords, startDate, endDate, name, forumId);
            else
                return TopicAccess.GetCountOfAllNotDeletedTopicsByModeratorWithQuery(_conn, _transaction
                    , moderatorId, keywords, startDate, endDate, name);
        }
    }
}
