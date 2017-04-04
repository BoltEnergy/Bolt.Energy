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
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class Drafts
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        public Drafts(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            _conn = conn;
            _transaction = transaction;
        }

        public virtual TopicWithPermissionCheck[] GetTopicsWhichExistDraftByPaging(int pageindex, int pageSize, string subjectKeyWord, string strOrder,  UserOrOperator operatingOperator)
        {
            DataTable table = TopicAccess.GetTopicsWhichExistDraftByPaging(_conn, _transaction, pageindex, pageSize, subjectKeyWord, strOrder);
            TopicWithPermissionCheck[] topics = new TopicWithPermissionCheck[table.Rows.Count];
            
            for (int i = 0; i < table.Rows.Count; i++)
            {

                string[] strTemp = Convert.ToString(table.Rows[0]["ParticipatorIds"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                int[] participatorIds = new int[strTemp.Length];
                int j = 0;
                if (strTemp.Length == 0)
                {
                    participatorIds = null;
                }
                else
                {
                    foreach (string str in strTemp)
                    {
                        participatorIds[j++] = Convert.ToInt32(str);
                    }
                }

                int topicId = Convert.ToInt32(table.Rows[i]["Id"]);
                int forumId = Convert.ToInt32(table.Rows[i]["ForumId"]);
                string fourmName = Convert.ToString(table.Rows[i]["ForumName"]);
                string subject = Convert.ToString(table.Rows[i]["Subject"]);
                int postUserOrOperatorId = table.Rows[i]["PostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[i]["PostUserOrOperatorId"]);
                string postUserOrOperatorName = table.Rows[i]["PostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[i]["PostUserOrOperatorName"]);
                bool postUserOrOperatorIfDeleted = table.Rows[i]["PostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[i]["PostUserOrOperatorIfDeleted"]);
                DateTime postTime = Convert.ToDateTime(table.Rows[i]["PostTime"]);
                int lastPostId = Convert.ToInt32(table.Rows[i]["LastPostId"]);
                DateTime lastPostTime = Convert.ToDateTime(table.Rows[i]["LastPostTime"]);
                int lastPostUserOrOperatorId = table.Rows[i]["LastPostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[i]["LastPostUserOrOperatorId"]);
                string lastPostUserOrOperatorName = table.Rows[i]["LastPostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[i]["LastPostUserOrOperatorName"]);
                bool lastPostUserOrOperatorIfDeleted = table.Rows[i]["LastPostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[i]["LastPostUserOrOperatorIfDeleted"]);
                int numberOfReplies = Convert.ToInt32(table.Rows[i]["NumberOfReplies"]);
                int numberOfHits = Convert.ToInt32(table.Rows[i]["NumberOfHits"]);
                bool ifClosed = Convert.ToBoolean(table.Rows[i]["IfClosed"]);
                bool ifMarkedAsAnswer = Convert.ToBoolean(table.Rows[i]["IfMarkedAsAnswer"]);
                bool ifSticky = Convert.ToBoolean(table.Rows[i]["IfSticky"]);

                topics[i] = new TopicWithPermissionCheck(_conn, _transaction, topicId, operatingOperator, forumId, fourmName, subject, postUserOrOperatorId, postUserOrOperatorName, postUserOrOperatorIfDeleted, postTime, lastPostId, lastPostTime, lastPostUserOrOperatorId, lastPostUserOrOperatorName, lastPostUserOrOperatorIfDeleted, numberOfReplies, numberOfHits, ifClosed, ifMarkedAsAnswer, ifSticky, participatorIds);
            }
            return topics;
        }

        public virtual int GetCountOfDrafts(string keyWords)
        {
            int recordsCount = TopicAccess.GetCountOfTopicsWhichExistDraft(_conn, _transaction, keyWords);
            return recordsCount;
        }

    }
}
