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
    public class PostProcess
    {
        public static PostWithPermissionCheck[] GetPostsByTopicIdAndPaging(
            int siteId, int operatingUserOrOperatorId, int topicId, int pageIndex, int pageSize,
            out int postscount,int forumId,bool ifAnnoucement)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                PostsOfTopicWithPermissionCheck posts = new PostsOfTopicWithPermissionCheck(conn, transaction, topicId, operatingUserOrOperator);
                if (ifAnnoucement)
                    return posts.GetPostsByPaging(pageIndex, pageSize, out postscount, forumId, true);
                else
                    return posts.GetPostsByPaging(pageIndex, pageSize, out postscount, forumId);

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

        public static PostWithPermissionCheck[] GetPostsByTopicIdAndPaging(
    int siteId, int operatingUserOrOperatorId, int topicId, int pageIndex, int pageSize
    )
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                PostsOfTopicWithPermissionCheck posts = new PostsOfTopicWithPermissionCheck(conn, transaction, topicId, operatingUserOrOperator);
                return posts.GetPostsByPaging(pageIndex, pageSize);

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

        public static int GetCountOfPostsByTopicIdByPaging(int siteId, int operatingUserOrOperatorId, int topicId,int forumId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                PostsOfTopicWithPermissionCheck postsOfTopic = new PostsOfTopicWithPermissionCheck(
                    conn, transaction, topicId, operatingUserOrOperator);
                return postsOfTopic.GetCountOfPostsByPaging(forumId);
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

        public static int GetCountOfPostsByTopicId(int siteId, int operatingUserOrOperatorId, bool ifOperator, int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn, transaction, topicId, operatingUserOrOperator);
                int count = topic.NumberOfReplies + 1;

                return count;
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

       
        /// <param name="siteId"></param>
        /// <param name="operatingUserOrOperatorId"></param>
        /// <param name="ifOperator"></param>
        /// <param name="postId"></param>
        public static PostWithPermissionCheck GetPostByPostId(int siteId, int operatingUserOrOperatorId, bool ifOperator, int postId)
        {
            SqlConnectionWithSiteId conn = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                PostWithPermissionCheck post = new PostWithPermissionCheck(conn, null, postId, operatingUserOrOperator);

                return post;
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

        public static int AddPost(int siteId, int operatingUserOrOperatorId, bool ifOperator,
            int topicId, bool ifTopic,bool ifReplaceDraft, string subject, string content,
            int[] attachIds, int[] scores, string[] descriptions,int forumId)
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

                PostsOfTopicWithPermissionCheck posts = new PostsOfTopicWithPermissionCheck(conn, transaction, topicId, operatingUserOrOperator);

                int postId = posts.Add(ifTopic,ifReplaceDraft, subject, postTime, content, attachIds, scores, descriptions,forumId);

                transaction.Commit();

                return postId;
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

        public static int AddPost(int siteId, int operatingUserOrOperatorId, bool ifOperator, int forumId,
        int topicId, bool ifTopic, string subject, string content,
        int[] attachIds, int[] scores, string[] descriptions)
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

                PostsOfTopicWithPermissionCheck posts = new PostsOfTopicWithPermissionCheck(conn, transaction, topicId, operatingUserOrOperator);

                //int postId = posts.Add(ifTopic, subject, postTime, content, attachIds, scores, descriptions,forumId);

                transaction.Commit();

                return -1;
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

        public static int AddAnnouncementPost(int siteId, int operatingUserOrOperatorId,
           int topicId,int forumId, bool ifTopic, string subject, string content)
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

                PostsOfTopicWithPermissionCheck posts = new PostsOfTopicWithPermissionCheck(conn, transaction, topicId, operatingUserOrOperator);

                int postId = posts.AddAnnouncementPost(ifTopic, subject, postTime, content, forumId);

                transaction.Commit();

                return postId;
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

        public static void UpdatePost(int siteId, int operatingUserOrOperatorId, bool ifOperator,
            int postId, string subject, string content, int[] attachIds, int[] scores, string[] descriptions,
            int forumId,int[] toDeletedAttachIds)
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

                PostWithPermissionCheck post = new PostWithPermissionCheck(conn, transaction, postId, operatingUserOrOperator);
                post.Update(subject, content, postTime, attachIds, scores, descriptions,forumId,toDeletedAttachIds);

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

        public static void UpdatePost(int siteId,int operatingUserOrOperatorId,int postId,string subject,
            string content,int forumId)
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

                PostWithPermissionCheck post = new PostWithPermissionCheck(conn, transaction, postId, operatingUserOrOperator);
                post.UpdateAnnoucementPost(subject, content,operatingUserOrOperator.Id,postTime,forumId);

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

        public static void DeletePost(int siteId, int operatingUserOrOperatorId, int postId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                PostWithPermissionCheck post = new PostWithPermissionCheck(conn, transaction, postId, operatingUserOrOperator);

                post.DeletePostOrTopic();
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

        public static void DeletePostOfAnnoucementOrTopic(int siteId, int operatingUserOrOperatorId,
            int forumId, int postId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                PostWithPermissionCheck post = new PostWithPermissionCheck(conn, transaction, postId, operatingUserOrOperator);
                post.DeletePostOfAnnoucementOrTopic(forumId);
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

        public static void DeletePostOfAnnoucment(int siteId, int operatingUserOrOperatorId,int forumId,int annoucementId, int postId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                PostsOfTopicWithPermissionCheck postsOfTopic = new PostsOfTopicWithPermissionCheck(
                    conn, transaction, annoucementId, operatingUserOrOperator);

                postsOfTopic.DeleteOfAnnoucement(forumId,postId,operatingUserOrOperator);
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
        public static void MarkAsAnswer(int siteId, int operatingUserOrOperatorId, bool ifOperator, int postId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                PostWithPermissionCheck post = new PostWithPermissionCheck(conn, transaction, postId, operatingUserOrOperator);

                post.MarkAsAnswer();

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

        public static void UnMarkAsAnswer(int siteId, int operatingUserOrOperatorId, bool ifOperator, int postId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                PostWithPermissionCheck post = new PostWithPermissionCheck(conn, transaction, postId, operatingUserOrOperator);

                post.UnMarkAsAnswer();

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

        public static PostWithPermissionCheck GetAnswerByTopicId(int siteId, int operatingUserOrOperatorId, bool ifOperator, int topicId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn, null, topicId, operatingUserOrOperator);
                PostWithPermissionCheck answer = topic.GetAnswer();

                return answer;
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

        public static int GetPostIndexInTopic(int siteId, int operatingUserOrOperatorId, bool ifOperator, int postId, int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                PostsOfTopicWithPermissionCheck posts = new PostsOfTopicWithPermissionCheck(conn, transaction, topicId, operatingUserOrOperator);
                return posts.GetPostIndex(postId);
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

        public static PostWithPermissionCheck[] GetNotDeletedRejectedPostsOfSiteByQueryAndPaging(
            int siteId, int operatingUserOrOperatorId, int pageIndex, int pageSize,
           string queryConditions, string orderField, string orderMethod, out int CountOfPosts)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                PostsOfSiteWithPermissionCheck postsOfSite = new PostsOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return postsOfSite.GetNotDeletedRejectedPostsOfSiteByQueryAndPaging(pageIndex, pageSize, queryConditions, orderField, orderMethod, out CountOfPosts);
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

        public static PostWithPermissionCheck[] GetNotDeletedModerationPostsOfSiteByQueryAndPaging(
           int siteId, int operatingUserOrOperatorId, int pageIndex, int pageSize,
          string queryConditions, string orderField, string orderMethod, out int CountOfPosts)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                PostsOfSiteWithPermissionCheck postsOfSite = new PostsOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return postsOfSite.GetNotDeletedModerationPostsOfSiteByQueryAndPaging(pageIndex, pageSize, queryConditions, orderField, orderMethod, out CountOfPosts);
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

        public static void RefusePostModeration(int siteId, int operatingUserOrOperatorId, int forumId,int postId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                PostWithPermissionCheck post = new PostWithPermissionCheck(conn, transaction, postId, operatingUserOrOperator);
                post.RefuseModeration(forumId);
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

        public static void AcceptPostModeration(int siteId, int operatingUserOrOperatorId, int forumId,int postId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                PostWithPermissionCheck post = new PostWithPermissionCheck(conn, transaction, postId, operatingUserOrOperator);
                post.AcceptModeration(forumId);
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

        public static void RefusePostModerationByAdminOrModerator(int siteId, int operatingUserOrOperatorId, int forumId, int postId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                PostWithPermissionCheck post = new PostWithPermissionCheck(conn, transaction, postId, operatingUserOrOperator);
                post.RefuseModerationByAdminOrModerator(forumId);
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

        public static void AcceptPostModerationByAdminOrModerator(int siteId, int operatingUserOrOperatorId,int forumId, int postId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                PostWithPermissionCheck post = new PostWithPermissionCheck(conn, transaction, postId, operatingUserOrOperator);
                post.AcceptModerationByAdminOrModerator(forumId);
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
        public static PostWithPermissionCheck[] GetNotDeletedModerationPostsByModeratorWithQueryAndPaging(int siteId, int operatingUserOrOperatorId, string queryConditions, int pageIndex, int pageSize, string orderField, string orderDirection, EnumPostOrTopicModerationStatus status)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                PostsOfSiteWithPermissionCheck postsOfSite = new PostsOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return postsOfSite.GetNotDeletedModerationPostsByModeratorWithQueryAndPaging(operatingUserOrOperatorId, queryConditions, pageIndex, pageSize, orderField, orderDirection, status);
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
        public static int GetCountOfNoDeletedModerationPostsByModeratorWithQuery(int siteId, int operatingUserOrOperatorId, string queryConditions, EnumPostOrTopicModerationStatus status)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                PostsOfSiteWithPermissionCheck postsOfSite = new PostsOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return postsOfSite.GetCountOfNoDeletedModerationPostsByModeratorWithQuery(operatingUserOrOperatorId, queryConditions, status);
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
        public static PostWithPermissionCheck[] GetNotDeltedPostsByModeratorWithQueryAndPaging(
            int siteId, int operatingUserOroPeratorId, string queryConditions, 
             string name, DateTime startDate, DateTime endDate, int forumId, int pageIndex, int pageSize, string orderField, string orderDirection)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOroPeratorId);
                PostsOfSiteWithPermissionCheck postsOfSite = new PostsOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return postsOfSite.GetNotDeletedPostsByModeratorWithQueryAndPaging(operatingUserOroPeratorId, queryConditions,name, startDate, endDate, forumId, pageIndex, pageSize, orderField, orderDirection);
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
        public static int GetCountOfNotDeletedPostsByModeratorWithQuery(int siteId, int operatingUserOrOperatorId, string queryConditions
            ,  string name, DateTime startDate, DateTime endDate, int forumId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                PostsOfSiteWithPermissionCheck postsOfSite = new PostsOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return postsOfSite.GetCountOfNotDeletePostsByModeratorWithQuery(operatingUserOrOperatorId, queryConditions,  name, startDate, endDate, forumId);
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
        public static PostWithPermissionCheck[] GetDeletedPostsByModeratorWithQueryAndPaging(int siteId
            , int operatingUserOrOperatorId, string keywords, string name, DateTime startDate, DateTime endDate, int pageIndex, int pageSize, string orderField, string orderDirection)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                PostsOfSiteWithPermissionCheck postsOfSite = new PostsOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return postsOfSite.GetDeletedPostsByModeratorWithQueryAndPaging(operatingUserOrOperatorId, keywords, name, startDate, endDate, pageIndex, pageSize, orderField, orderDirection);
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
        public static int GetCountOfDeletedPostsByModeratorWithQuery(int siteId
            , int operatingUserOrOperatorId, string keywords, string name, DateTime startDate, DateTime endDate)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                PostsOfSiteWithPermissionCheck postsOfSite = new PostsOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return postsOfSite.GetCountOfDeletedPostsByModeratorWithQuery(operatingUserOrOperatorId, keywords, name, startDate, endDate);
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

        public static PostWithPermissionCheck[] GetNotDeletedPostsByQueryAndPaging(
            int siteId, int operatingUserOrOperatorId,
            string keywords, int topicId, string name, DateTime startDate,
            DateTime endDate, int pageIndex, int pageSize, string orderFiled,
            string orderMethod, out int CountOfPosts, int forumId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                PostsBase postsbase = PostsFactory.CreatePost(forumId, topicId, conn, transaction, operatingUserOrOperator);
                return postsbase.GetNotDeletedPostsByQueryAndPaging(operatingUserOrOperator, keywords,  name, startDate,
                    endDate, pageIndex, pageSize, orderFiled, orderMethod, out CountOfPosts);
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

        public static void LogicDeletePost(int siteId, int operatingUserOrOperatorId, int postId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                PostWithPermissionCheck post = new PostWithPermissionCheck(conn, transaction, postId, operatingUserOrOperator);
                post.LogicDelete();
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

        public static void LogicDeletePostOfTopic(int siteId, int operatingUserOrOperatorId, int postId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                PostWithPermissionCheck post = new PostWithPermissionCheck(conn, transaction, postId, operatingUserOrOperator);
                post.LogicDeletePostsOfTopic();
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

        public static void RestorePost(int siteId, int operatingUserOrOperatorId, int postId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                PostWithPermissionCheck post = new PostWithPermissionCheck(conn, transaction, postId, operatingUserOrOperator);
                post.Restore();
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

        public static PostWithPermissionCheck[] GetDeletedPostsByQueryAndPaging(
          int siteId, int operatingUserOrOperatorId,
            string keywords, string name, DateTime startDate, DateTime endDate,
            int pageIndex, int pageSize, string orderFiled, string orderMethod, out int CountOfPosts)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                PostsOfSiteWithPermissionCheck postsOfSite = new PostsOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return postsOfSite.GetDeletedPostsByQueryAndPaging(keywords, name, startDate,
                    endDate, pageIndex, pageSize, orderFiled, orderMethod, out CountOfPosts);
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


        /*Advance Search-Search Posts*/
        public static PostWithPermissionCheck[] GetPostsByAdvancedSearchAndPaging(
         out int searchResultCount, int siteId, int operatingUserOrOperatorId,
         int pageIndex, int pageSize,
         string keywords, DateTime startTime, DateTime endTime,
         bool ifAllForums, bool ifCategory, bool ifForum, int id, int searchMethod)
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
                    conn, transaction, operatingUsrOrOperator,
                    keywords, ifAllForums, ifCategory, ifForum, id,
                    startTime, endTime, searchMethod);

                return search.GetPostsByPagingAndSearchOptions(out searchResultCount, pageIndex, pageSize,
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
        /*Advance Search-Search User's Posts*/
        public static PostWithPermissionCheck[] GetUsersPostsByAdvancedSearchAndPaging(
        out int searchResultCount, int siteId, int operatingUserOrOperatorId,
        int pageIndex, int pageSize, int searchUserId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUsrOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                    conn, null, null, operatingUserOrOperatorId);
                // UserOrOperator searchUser = UserOrOperatorFactory.CreateUserOrOperator(conn
                //SearchWithPermissionCheck search = new SearchWithPermissionCheck(
                //    conn, transaction, operatingUsrOrOperator,
                //    keywords, ifAllForums, ifCategory, ifForum, id,
                //    startTime, endTime, searchMethod);

                //return search.GetPostsByPagingAndSearchOptions(out searchResultCount, pageIndex, pageSize,
                //    operatingUsrOrOperator);
                searchResultCount = 0;
                return null;
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

        public static bool IfUserReplyTopic(int siteId, int operatingUserOrOperatorId,
            int topicId, int userId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                PostsOfTopicWithPermissionCheck postsOfTopic = new PostsOfTopicWithPermissionCheck(conn, transaction,
                    topicId, operatingUserOrOperator);
                return postsOfTopic.IfUserReplyTopic(userId);
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


        public static PostWithPermissionCheck GetFirstPostOfTopic(int siteId, int topicId,int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                PostsOfTopicWithPermissionCheck postsOfTopic = new PostsOfTopicWithPermissionCheck(conn, transaction,
                    topicId, operatingUserOrOperator);
                return postsOfTopic.GetFirstPost();
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
