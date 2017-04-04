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
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Enum;

namespace Com.Comm100.Forum.Process
{
    public class DraftProcess
    {
        public static void DeleteDraftByTopicId(int siteId, int operatingUserOrOperatorId, bool ifOperator, int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn, transaction, topicId, operatingUserOrOperator);
                topic.DeleteDraft();
                transaction.Commit();
            }
            catch (System.Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static DraftWithPermissionCheck GetDraftByTopicId(int siteId, int operatingUserOrOperatorId, bool ifOperator, int TopicId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);

                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                return new DraftWithPermissionCheck(conn, null, TopicId,0, operatingUserOrOperator);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void SaveDraft(int siteId, int operatingUserOrOperatorId,
            bool ifOperator, int topicId, string subject, string content,
            int[] attachIds, int[] scores, string[] descriptions,int[] toDeleteAttachIds)
        {
            SqlConnectionWithSiteId conn = null;
            DateTime currentTime = DateTime.UtcNow;

            subject = subject.Trim();
            content = content.Trim();
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn, null, topicId, operatingUserOrOperator);
                topic.SaveDraft(subject, content,operatingUserOrOperator, currentTime,attachIds,
                    scores,descriptions,toDeleteAttachIds);

            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static int GetCountOfDrafs(int siteId, int operatingUserOrOperatorId, bool ifOperator, string keyWords)
        {
            SqlConnectionWithSiteId conn = null;
            
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                string trimKeyWords = keyWords.Trim();
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                DraftsWithPermissionCheck drafts = new DraftsWithPermissionCheck(conn, null, operatingUserOrOperator);
                int recordsCount = drafts.GetCountOfDrafts(trimKeyWords);
                return recordsCount;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
    }
}
