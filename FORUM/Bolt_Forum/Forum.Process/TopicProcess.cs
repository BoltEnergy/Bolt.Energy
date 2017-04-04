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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.Process
{
    public class TopicProcess
    {
        public static TopicWithPermissionCheck[] GetTopicsWhichExistDraftByPaging(int siteId, int operatingUserOrOperatorId, bool ifOperator, int pageIndex, int pageSize, string subjectKeyWord, string strOrder)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                string trimSubjectKeyWord = subjectKeyWord.Trim();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                DraftsWithPermissionCheck drafts = new DraftsWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                
                return drafts.GetTopicsWhichExistDraftByPaging(pageIndex, pageSize, trimSubjectKeyWord, strOrder);
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

        public static TopicWithPermissionCheck[] GetTopicsByForumIdAndPaging(
            int siteId, int operatingUserOrOperatorId,int forumId, int pageIndex, int pageSize,
            out int count)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicsOfForumWithPermissionCheck tmpTopics = new TopicsOfForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);
                
                return tmpTopics.GetTopicsByPaging(pageIndex, pageSize,forumId,out count);
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

        public static Int32 GetCountOfTopicByForumId(int siteId, int operatingUserOrOperatorId,int forumId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicsOfForumWithPermissionCheck tmpTopics = new TopicsOfForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);

                return tmpTopics.GetCountOfTopicsByForumId();
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

        public static Int32 GetCountOfFeaturedTopicByForumId(int siteId, int operatingUserOrOperatorId, int forumId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicsOfForumWithPermissionCheck tmpTopics = new TopicsOfForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);

                return tmpTopics.GetCountOfFeaturedTopicsByForumId();
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

        public static TopicWithPermissionCheck[] GetTopicsByForumIdAndPagingWithoutWaitingForModeration(
            int siteId, int operatingUserOrOperatorId, int forumId, int pageIndex, int pageSize,
            out int count)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicsOfForumWithPermissionCheck tmpTopics = new TopicsOfForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);

                return tmpTopics.GetTopicsByPagingWithoutWaitingForModeration(pageIndex, pageSize, forumId, out count);
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

        /* update by techtier on 3/1/2017  for adding sorting*/
        public static TopicWithPermissionCheck[] GetTopicsByForumIdAndPagingWithoutWaitingForModerationByManualSort(
         int siteId, int operatingUserOrOperatorId, int forumId, int pageIndex, int pageSize,string sortKeyword,
         out int count)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicsOfForumWithPermissionCheck tmpTopics = new TopicsOfForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);

                return tmpTopics.GetTopicsByPagingWithoutWaitingForModerationByManualSort(pageIndex, pageSize, forumId, sortKeyword, out count);
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


        public static TopicWithPermissionCheck[] GetTopicsByForumIdAndPagingWithoutWaitingForModerationByManualMyPost(
        int siteId, int operatingUserOrOperatorId, int forumId, int pageIndex, int pageSize,
        out int count)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicsOfForumWithPermissionCheck tmpTopics = new TopicsOfForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);

                return tmpTopics.GetTopicsByPagingWithoutWaitingForModerationByManualMyPost(pageIndex, pageSize, forumId, operatingUserOrOperatorId, out count);
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

     
        /*made by techtier for serch*/
        public static TopicWithPermissionCheck[] GetTopicsByForumIdAndPagingWithoutWaitingForModerationByManualSearch(
         int siteId, int operatingUserOrOperatorId, int forumId, int pageIndex, int pageSize, string searchKeyword,
         out int count)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicsOfForumWithPermissionCheck tmpTopics = new TopicsOfForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);

                return tmpTopics.GetTopicsByPagingWithoutWaitingForModerationByManualSearch(pageIndex, pageSize, forumId, searchKeyword, out count);
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

        public static TopicWithPermissionCheck[] GetFeaturedTopicsByForumIdAndPaging(
            int siteId, int operatingUserOrOperatorId, int forumId, int pageIndex, int pageSize
            , out int count)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicsOfForumWithPermissionCheck tmpTopics = new TopicsOfForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);

                return tmpTopics.GetFeaturedTopicsByPaging(pageIndex, pageSize,out count,forumId);
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

        public static TopicWithPermissionCheck[] GetFeaturedTopicsByForumIdAndPagingWithoutWaitingForModeration(
            int siteId, int operatingUserOrOperatorId, int forumId, int pageIndex, int pageSize
            , out int count)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicsOfForumWithPermissionCheck tmpTopics = new TopicsOfForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);

                return tmpTopics.GetFeaturedTopicsByPagingWithoutWaitingForModeration(pageIndex, pageSize, out count, forumId);
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

        public static TopicWithPermissionCheck[] GetTopicsByAdvancedSearchWithForumIdAndPaging(out int searchResultCount, int siteId, int operatingUserOrOperatorId, bool ifOperator, int forumId, int pageIndex, int pageSize, string subject1, string displayName1, DateTime startTime, DateTime endTime, string status, bool ifSticky, bool ifAnswered)
        {
            string subject = subject1.TrimStart();
            subject = subject.TrimEnd();
            subject = subject.Replace(" ", "%");
            string displayName = displayName1.Trim();
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                SearchWithPermissionCheck search = new SearchWithPermissionCheck(conn, transaction, subject, displayName, startTime, endTime, status, ifSticky, ifAnswered, operatingUserOrOperator);

                return search.GetTopicsByPagingAndSearchOptionsWithForumId(out searchResultCount, pageIndex, pageSize, forumId);
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

        public static TopicWithPermissionCheck[] GetTopicsByAdvancedSearchWithCategoryIdAndPaging(out int searchResultCount, int siteId, int operatingUserOrOperatorId, bool ifOperator, int categoryId, int pageIndex, int pageSize, string subject1, string displayName1, DateTime startTime, DateTime endTime, string status, bool ifSticky, bool ifAnswered)
        {
            string subject = subject1.TrimStart();
            subject = subject.TrimEnd();
            subject = subject.Replace(" ", "%");
            string displayName = displayName1.Trim();
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                SearchWithPermissionCheck search = new SearchWithPermissionCheck(conn, transaction, subject, displayName, startTime, endTime, status, ifSticky, ifAnswered, operatingUserOrOperator);
                
                return search.GetTopicsByPagingAndSearchOptionsWithCategoryId(out searchResultCount, pageIndex, pageSize, categoryId);
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

        public static TopicWithPermissionCheck[] GetTopicsByAdvancedSearchAndPagingWithoutId(out int searchResultCount, int siteId, int operatingUserOrOperatorId, bool ifOperator, int pageIndex, int pageSize, string subject1, string displayName1, DateTime startTime, DateTime endTime, string status, bool ifSticky, bool ifAnswered)
        {
            string subject = subject1.TrimStart();
            subject = subject.TrimEnd();
            subject = subject.Replace(" ", "%");
            string displayName = displayName1.Trim();
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUsrOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                SearchWithPermissionCheck search = new SearchWithPermissionCheck(conn, transaction, subject, displayName, startTime, endTime, status, ifSticky, ifAnswered, operatingUsrOrOperator);
                
                return search.GetTopicsByPagingAndSearchOptionsWithoutId(out searchResultCount, pageIndex, pageSize);
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

        public static TopicWithPermissionCheck GetTopicByTopicId(int siteId, int operatingUserOrOperatorId,int TopicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicWithPermissionCheck tmpTopic = new TopicWithPermissionCheck(conn, transaction, TopicId, operatingUserOrOperator);

                return tmpTopic;
            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        

        public static int GetCountOfTopicsByForumId(int siteId, int operatingUserOrOperatorId, int forumId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                ForumWithPermissionCheck tmpForum = new ForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);

                return tmpForum.NumberOfTopics;
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

        public static TopicWithPermissionCheck AddTopic(int siteId, int operatingUserOrOperatorId,
            bool ifOperator, int forumId, string subject, string content, 
            int score, bool ifReplyRequired, bool IfPayScoreRequired,
            bool ifContainsPoll, bool ifMultipleChoice, int maxChoices, bool ifSetDeadline,
            DateTime startDate, DateTime endDate, string[] options,
            int[] attachIds,int[] scores,string[] descriptions)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            DateTime postTime = DateTime.UtcNow;

            subject = subject.Trim();
            content = content.Trim();
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                TopicsOfForumWithPermissionCheck topics = new TopicsOfForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);
                int topicId = topics.Add(subject, postTime, content, score, 
                    ifReplyRequired, IfPayScoreRequired,
                    ifContainsPoll, ifMultipleChoice, maxChoices, ifSetDeadline,
                startDate, endDate, options,attachIds,scores,descriptions);

                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn, transaction, topicId, operatingUserOrOperator);
                
                transaction.Commit();

                return topic;
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

        public static void UpdateTopic(int siteId, int operatingUserOrOperatorId, bool ifOperator,
            int topicId, string subject, string content,
            bool ifSetDeadline,DateTime endDate, 
            int[] attachIds, int[] scores, string[] descriptions,
            int[] toDeleteAttachIds
            )
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            DateTime postTime = DateTime.UtcNow;

            subject = subject.Trim();
            content = content.Trim();
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn, transaction, topicId, operatingUserOrOperator);
                
                topic.Update(subject, content,
                    ifSetDeadline,endDate,
                    attachIds,scores,descriptions,
                    toDeleteAttachIds
                    );

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

        public static void DeleteTopic(int siteId, int operatingUserOrOperatorId, bool ifOperator, int topicId)
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

                topic.Delete();

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

        public static void SetTopicFeatured(int siteId, int operatingUserOrOperatorId, bool ifOperator,int forumId, int topicId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn, null, topicId, operatingUserOrOperator);

                topic.Featured(forumId);
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

        public static void SetTopicUnFeatured(int siteId, int operatingUserOrOperatorId, bool ifOperator, int forumId,int topicId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn, null, topicId, operatingUserOrOperator);

                topic.UnFeatured(forumId);
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
        public static void CloseTopic(int siteId, int operatingUserOrOperatorId, bool ifOperator, int topicId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn, null, topicId, operatingUserOrOperator);

                topic.Close();
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

        public static void ReopenTopic(int siteId, int operatingUserOrOperatorId, bool ifOperator, int topicId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn, null, topicId, operatingUserOrOperator);

                topic.Reopen();
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

        public static void MoveTopic(int siteId, int operatingUserOrOperatorId, bool ifOperator, int topicId, int forumId)
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

                topic.Move(forumId);

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

        public static void MoveTopicInUI(int siteId, int operatingUserOrOperatorId, bool ifOperator, int topicId, int forumId)
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

                topic.MoveTopic(forumId);

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

        public static TopicWithPermissionCheck GetLastMovedTopicInForum(int siteId, int operatingUserOrOperatorId, int topicId, int forumId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                //transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);

                TopicMoveHistoriesWithPermissionCheck history = new TopicMoveHistoriesWithPermissionCheck(conn, transaction, operatingUserOrOperator,
                    topicId);
                return history.GetLastMovedTopicInForum(forumId);

                //transaction.Commit();
            }
            catch (System.Exception)
            {
               // DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void SetTopicSticky(int siteId, int operatingUserOrOperatorId, bool ifOperator, int topicId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn, null, topicId, operatingUserOrOperator);

                topic.SetSticky();
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

        public static void SetTopicUnSticky(int siteId, int operatingUserOrOperatorId, bool ifOperator, int topicId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn, null, topicId, operatingUserOrOperator);

                topic.UnSticky();
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

        public static void ReadTopic(int siteId, int operatingUserOrOperatorId, bool ifOperator, int topicId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = operatingUserOrOperatorId == 0 ? null : UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn, null, topicId, operatingUserOrOperator);

                topic.Read();
                topic.IncreaseNumberOfHitsByOne();
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

        public static TopicWithPermissionCheck[] GetNotDeletedTopicsByQueryAndPagingInTopicsOfSite(
            int operatingUserOrOperatorId, int siteId,
            string keywords,string name, int forumId,
            DateTime startDate, DateTime endDate, int pageIndex, int pageSize,
            string orderField, string orderMethod, out int countOfTopics)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                    conn, null, null, operatingUserOrOperatorId);
                TopicsBase topicsOfSite = TopicsFactory.CreateTopic(
                    forumId, conn, transaction, operatingUserOrOperator);
                
                TopicWithPermissionCheck[] topics = topicsOfSite.GetNotDeletedTopicsByQueryAndPaging(
                    operatingUserOrOperator,keywords, name, 
                    startDate, endDate, pageIndex, pageSize,
                    orderField, orderMethod, out countOfTopics);
                return topics;
            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void LogicDeleteTopic( int operatingUserOrOperatorId, int siteId,int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn,transaction,topicId,operatingUserOrOperator);
                topic.LogicDeleteTopicAndFirstPost();
            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void RestoreTopic( int operatingUserOrOperatorId, int siteId,int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn, transaction, topicId, operatingUserOrOperator);
                topic.Restore();
            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static string GetTopicPath(int operatingUserOrOperatorId, int siteId, int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicWithPermissionCheck tmpTopic = new TopicWithPermissionCheck(conn, transaction, topicId, operatingUserOrOperator);
                if (tmpTopic.ForumId <= 0)//0 is annoucement, for test
                    return "";
                ForumWithPermissionCheck forum = new ForumWithPermissionCheck(
                    conn,transaction,tmpTopic.ForumId,operatingUserOrOperator);
                CategoryWithPermissionCheck category = new CategoryWithPermissionCheck(
                    conn, transaction, forum.CategoryId, operatingUserOrOperator);
                return category.Name + "/" + forum.Name;
            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static TopicBase CreateTopic(
            int operatingUserOrOperatorId, int siteId, 
            int topicId,out bool ifAnnoucement)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicsOfSiteWithPermissionCheck topicsOfSite = new TopicsOfSiteWithPermissionCheck(conn, transaction,
                     operatingUserOrOperator);

                return TopicFactory.CreateTopic(conn, transaction, topicId, operatingUserOrOperator, out ifAnnoucement);
            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static TopicWithPermissionCheck[] GetTopicsByAdvancedSearchAndPaging(
            out int searchResultCount, int siteId, int operatingUserOrOperatorId,
            int pageIndex, int pageSize, 
            string keywords, DateTime startTime, DateTime endTime,
            bool ifAllForums,bool ifCategory,bool ifForum,int id,int searchMethod)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUsrOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                    conn, null, null, operatingUserOrOperatorId);
                SearchWithPermissionCheck search = new SearchWithPermissionCheck(
                    conn, transaction,operatingUsrOrOperator,
                    keywords,ifAllForums,ifCategory,ifForum,id,
                    startTime, endTime, searchMethod);

                return search.GetTopicsByPagingAndSearchOptions(out searchResultCount, pageIndex, pageSize,
                    operatingUsrOrOperator);
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

        #region Moderator Panel
        public static TopicWithPermissionCheck[] GetNotDeletedTopicsByModeratorWithQueryAndPaging(
            int siteId, int operatingUserOrOperatorId, 
            string name, string keywords,DateTime startTime, DateTime endTime, int forumId,
            int pageIndex, int pageSize, string orderField, string orderMethod)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                TopicsOfSiteWithPermissionCheck topicsOfSite = new TopicsOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return topicsOfSite.GetNotDeletedTopicsByModeratorWithQueryAndPaging(operatingUserOrOperator, operatingUserOrOperatorId,forumId, keywords, name, startTime, endTime, pageIndex, pageSize, orderField, orderMethod);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }

        }

        public static int GetCountOfNotDeletedTopicsByModeratorWithQuery(
            int siteId, int operatingUserOrOperatorId,
           string name, string keywords, DateTime startTime, DateTime endTime,int forumId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                TopicsOfSiteWithPermissionCheck topicsOfSite = new TopicsOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return topicsOfSite.GetCountOfNotDeletedTopicsByModeratorWithQuery(operatingUserOrOperatorId, keywords, name, startTime, endTime,forumId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        #endregion

    }
}
