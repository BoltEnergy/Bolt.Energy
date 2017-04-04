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
    public abstract class TopicsBase
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        public TopicsBase(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {

            _conn = conn;
            _transaction = transaction;
        }

        protected TopicWithPermissionCheck CreateTopicObject(DataRow dr, UserOrOperator operatingUserOrOperator)
        {
            TopicWithPermissionCheck topic = null;

            if (dr != null)
            {
                //topic = new TopicWithPermissionCheck(_conn, _transaction, Convert.ToInt32(dr["Id"]), operatingUserOrOperator, Convert.ToInt32(dr["ForumId"]),
                //       Convert.ToString(dr["ForumName"]), Convert.ToString(dr["Subject"]),
                //       dr["PostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(dr["PostUserOrOperatorId"]),
                //       dr["PostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(dr["PostUserOrOperatorName"]),
                //       dr["PostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(dr["PostUserOrOperatorIfDeleted"]),
                //       Convert.ToDateTime(dr["PostTime"]),
                //       Convert.ToInt32(dr["LastPostId"]),
                //       Convert.ToDateTime(dr["LastPostTime"]),
                //       dr["LastPostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(dr["LastPostUserOrOperatorId"]),
                //       dr["LastPostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(dr["LastPostUserOrOperatorName"]),
                //       dr["LastPostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(dr["LastPostUserOrOperatorIfDeleted"]),
                //       Convert.ToInt32(dr["NumberOfReplies"]), Convert.ToInt32(dr["NumberOfHits"]),
                //       Convert.ToBoolean(dr["IfClosed"]), Convert.ToBoolean(dr["IfMarkedAsAnswer"]),
                //       Convert.ToBoolean(dr["IfSticky"]), StringHelper.GetIntArrayFromString(dr["ParticipatorIds"].ToString(), ','));

                topic = new TopicWithPermissionCheck(_conn, _transaction, Convert.ToInt32(dr["Id"]), operatingUserOrOperator, Convert.ToInt32(dr["ForumId"]),
                       Convert.ToString(dr["ForumName"]), Convert.ToString(dr["Subject"]),
                       dr["PostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(dr["PostUserOrOperatorId"]),
                       dr["PostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(dr["PostUserOrOperatorName"]),
                       dr["PostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(dr["PostUserOrOperatorIfDeleted"]),
                       Convert.ToDateTime(dr["PostTime"]),
                       Convert.ToInt32(dr["LastPostId"]),
                       Convert.ToDateTime(dr["LastPostTime"]),
                       dr["LastPostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(dr["LastPostUserOrOperatorId"]),
                       dr["LastPostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(dr["LastPostUserOrOperatorName"]),
                       dr["LastPostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(dr["LastPostUserOrOperatorIfDeleted"]),
                       PostAccess.GetCountOfNotDeletedPostsByTopicId(_conn, _transaction, Convert.ToInt32(dr["Id"])), Convert.ToInt32(dr["NumberOfHits"]),
                       Convert.ToBoolean(dr["IfClosed"]), Convert.ToBoolean(dr["IfMarkedAsAnswer"]),
                       Convert.ToBoolean(dr["IfSticky"]), StringHelper.GetIntArrayFromString(dr["ParticipatorIds"].ToString(), ','));

            }
            return topic;
        }

        protected TopicWithPermissionCheck CreateTopicObject2(DataRow dr, UserOrOperator operatingUserOrOperator)
        {
            TopicWithPermissionCheck topic = null;
            if (dr != null)
            {
                topic = new TopicWithPermissionCheck(_conn, _transaction, Convert.ToInt32(dr["Id"]), operatingUserOrOperator, Convert.ToInt32(dr["ForumId"]),
                       Convert.ToString(dr["ForumName"]), Convert.ToString(dr["Subject"]),
                       dr["PostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(dr["PostUserOrOperatorId"]),
                       dr["PostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(dr["PostUserOrOperatorName"]),
                       dr["PostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(dr["PostUserOrOperatorIfDeleted"]),
                       Convert.ToDateTime(dr["PostTime"]),
                       Convert.ToInt32(dr["LastPostId"]),
                       Convert.ToDateTime(dr["LastPostTime"]),
                       dr["LastPostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(dr["LastPostUserOrOperatorId"]),
                       dr["LastPostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(dr["LastPostUserOrOperatorName"]),
                       dr["LastPostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(dr["LastPostUserOrOperatorIfDeleted"]),
                       PostAccess.GetCountOfNotDeletedPostsByTopicId(_conn,_transaction,Convert.ToInt32(dr["Id"])), Convert.ToInt32(dr["NumberOfHits"]),
                       Convert.ToBoolean(dr["IfClosed"]), Convert.ToBoolean(dr["IfMarkedAsAnswer"]),
                       Convert.ToBoolean(dr["IfSticky"]), StringHelper.GetIntArrayFromString(dr["ParticipatorIds"].ToString(), ','),
                       Convert.ToBoolean(dr["IfDeleted"]),
                       Convert.ToInt16(dr["ModerationStatus"]),
                       Convert.ToBoolean(dr["IfPayScoreRequired"]),
                       Convert.ToInt32(dr["Score"]),
                       Convert.ToBoolean(dr["IfMoveHistory"]),
                       -1,-1,//locateTopicId,LocateForumId,
                       Convert.ToDateTime(dr["MoveDate"]),
                       Convert.ToInt32(dr["MoveUserOrOperatorId"]),
                       Convert.ToBoolean(dr["IfFeatured"]),
                       Convert.ToBoolean(dr["IfContainsPoll"]),
                       Convert.ToBoolean(dr["IfReplyRequired"]),
                       Convert.ToInt32(dr["TotalPromotion"]));
            }
            return topic;
        }

        public abstract TopicWithPermissionCheck[] GetNotDeletedTopicsByQueryAndPaging(
            UserOrOperator operatingUserOrOperator, string keywords, string name,
           DateTime startDate, DateTime endDate, int pageIndex, int pageSize,
           string orderField, string orderMethod, out int countOfTopics);

        //public abstract TopicWithPermissionCheck[] GetNotDeletedTopicsByQueryAndPaging(string keywords, int posterId, DateTime startDate, DateTime endDate, int pageIndex, int pageSize, out int countOfTopics);
    }
}
