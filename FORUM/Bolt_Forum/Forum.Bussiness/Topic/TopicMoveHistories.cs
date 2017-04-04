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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class TopicMoveHistories
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        private int _topicId;

        public int TopicId
        {
            get { return this._topicId; }
        }

        public TopicMoveHistories(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {
            _conn = conn;
            _transaction = transaction;
            _topicId = topicId;
        }

        protected void Add(DateTime moveDate, int moveUserOrOperatorId)
        { }

        public TopicWithPermissionCheck GetLastMovedTopicInForum(UserOrOperator operatingUserOrOperator,int forumId)
        {
            DataTable table = TopicAccess.GetLastMovedTopicInForum(_conn, _transaction, forumId, _topicId);
            if (table.Rows.Count == 0)
               return null;

            #region Data Init
            int topicId = Convert.ToInt32(table.Rows[0]["Id"]);
            //int forumId = Convert.ToInt32(table.Rows[0]["ForumId"]);
           string forumName = Convert.ToString(table.Rows[0]["ForumName"]);
           string subject = Convert.ToString(table.Rows[0]["Subject"]);
           int postUserOrOperatorId = table.Rows[0]["PostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[0]["PostUserOrOperatorId"]);
           string postUserOrOperatorName = table.Rows[0]["PostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[0]["PostUserOrOperatorName"]);
           bool postUserOrOperatorIfDeleted = table.Rows[0]["PostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[0]["PostUserOrOperatorIfDeleted"]);
           DateTime postTime = Convert.ToDateTime(table.Rows[0]["PostTime"]);
           int lastPostId = Convert.ToInt32(table.Rows[0]["LastPostId"]);
           DateTime lastPostTime = Convert.ToDateTime(table.Rows[0]["LastPostTime"]);
           int lastPostUserOrOperatorId = table.Rows[0]["LastPostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[0]["LastPostUserOrOperatorId"]);
           string lastPostUserOrOperatorName = table.Rows[0]["LastPostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[0]["LastPostUserOrOperatorName"]);
           bool lastPostUserOrOperatorIfDeleted = table.Rows[0]["LastPostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[0]["LastPostUserOrOperatorIfDeleted"]);
           int numberOfReplies = 0;//PostAccess.GetCountOfNotDeletedPostsByTopicId(_conn, _transaction, _topicId);
           int numberOfHits = 0;//Convert.ToInt32(table.Rows[0]["NumberOfHits"]);
           bool ifClosed = Convert.ToBoolean(table.Rows[0]["IfClosed"]);
           bool ifMarkedAsAnswer = Convert.ToBoolean(table.Rows[0]["IfMarkedAsAnswer"]);
           bool ifSticky = Convert.ToBoolean(table.Rows[0]["IfSticky"]);

           int[] participatorIds = new int[]{};// StringHelper.GetIntArrayFromString(table.Rows[0]["ParticipatorIds"].ToString(), ',');
           //bool ifHasDraft = IfTopicHasDraft();

           /*2.0*/
           bool ifFeatured = Convert.ToBoolean(table.Rows[0]["IfFeatured"]);
           bool ifMoveHistory = Convert.ToBoolean(table.Rows[0]["IfMoveHistory"]);
           bool ifContainsPoll = Convert.ToBoolean(table.Rows[0]["IfContainsPoll"]);
           bool ifReplyRequired = Convert.ToBoolean(table.Rows[0]["IfReplyRequired"]);
           bool ifPayScoreRequired = Convert.ToBoolean(table.Rows[0]["IfPayScoreRequired"]);
           int score = Convert.ToInt32(table.Rows[0]["Score"]);
           int locateTopicId = Convert.ToInt32(table.Rows[0]["LocateTopicId"]);
           bool ifDeleted = Convert.ToBoolean(table.Rows[0]["TopicIfDeleted"]);
            #endregion

           return new TopicWithPermissionCheck(_conn, _transaction, topicId, operatingUserOrOperator, forumId, forumName, subject, postUserOrOperatorId, postUserOrOperatorName, postUserOrOperatorIfDeleted,
               postTime, lastPostId, lastPostTime, lastPostUserOrOperatorId, lastPostUserOrOperatorName, lastPostUserOrOperatorIfDeleted, numberOfReplies, numberOfHits, ifClosed, ifMarkedAsAnswer,
               ifSticky, participatorIds);
        }

        public void LogicDeleteAll()
        { }

        public void DeleteAll()
        { }
    }
}
