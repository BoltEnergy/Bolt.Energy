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
    public class ServerProcess
    {
        public static QueueEmail[] GetAllScheduledQueueEmails()
        {
            SqlConnection conn = null;
            try
            {
                conn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(conn);
                QueueEmails queueEmails = new QueueEmails(conn, null);
                return queueEmails.GetAllScheduledQueueEmails();
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

        public static QueueEmail GetQueueEmailById(int queueEmailId)
        {
            SqlConnection conn = null;
            try
            {
                conn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(conn);
                QueueEmail queueEmail = new QueueEmail(conn, null, queueEmailId);
                return queueEmail;
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

        public static void UpdateQueueEmailStatusToRunning(int queueEmailId)
        {
            SqlConnection conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(conn);
                transaction = conn.BeginTransaction();
                QueueEmail queueEmail = new QueueEmail(conn, transaction, queueEmailId);
                queueEmail.UpdateStatusToRunning();
                transaction.Commit();
            }
            catch (Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void UpdateQueueEmailStatusToScheduled(int queueEmailId)
        {
            SqlConnection conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(conn);
                transaction = conn.BeginTransaction();
                QueueEmail queueEmail = new QueueEmail(conn, transaction, queueEmailId);
                queueEmail.UpdateStatusToScheduled();
                transaction.Commit();
            }
            catch (Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void DeleteQueueEmail(int queueEmailId)
        {
            SqlConnection conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(conn);
                transaction = conn.BeginTransaction();
                QueueEmail queueEmail = new QueueEmail(conn, transaction, queueEmailId);
                queueEmail.Delete();
                transaction.Commit();
            }
            catch (Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static List<string> GetRecipientsByQueueEmailId(int queueEmailId)
        {
            SqlConnection conn = null;
            try
            {
                conn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(conn);
                QueueEmail queueEmail = new QueueEmail(conn, null, queueEmailId);
                return queueEmail.GetRecipients().GetAllRecipients();
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

        public static void DeleteRecipient(int queueEmailId, string emailAddress)
        {
            SqlConnection conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(conn);
                transaction = conn.BeginTransaction();
                QueueEmail queueEmail = new QueueEmail(conn, transaction, queueEmailId);
                QueueEmailRecipients recipients = queueEmail.GetRecipients();
                recipients.Delete(emailAddress);
                transaction.Commit();
            }
            catch (Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static TopicWithPermissionCheck GetTopicAndForumInfoByPostId(int siteId, int postId, out string postContent, out string forumName)
        {
            SqlConnection generalConn = null;
            SqlConnectionWithSiteId siteConn = null;
            try
            {
                generalConn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(generalConn);
                siteConn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(siteConn);
                PostWithPermissionCheck post = new PostWithPermissionCheck(siteConn, null, postId, null);
                postContent = post.Content;
                SiteSettingWithPermissionCheck siteSetting = new SiteSettingWithPermissionCheck(siteConn, null, generalConn, null, null);
                forumName = siteSetting.SiteName;
                return new TopicWithPermissionCheck(siteConn, null, post.TopicId, null);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(generalConn);
                DbHelper.CloseConn(siteConn);
            }
        }

        public static TopicWithPermissionCheck GetTopicByPostId(int siteId, int postId)
        {
            SqlConnectionWithSiteId siteConn = null;
            try
            {
                siteConn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(siteConn);
                PostWithPermissionCheck post = new PostWithPermissionCheck(siteConn, null, postId, null);
                return new TopicWithPermissionCheck(siteConn, null, post.TopicId, null);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(siteConn);
            }
        }

        public static UserOrOperator GetUserOrOperatorByEmail(int siteId, string email)
        {
            SqlConnectionWithSiteId conn = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                return UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, email);
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
    }
}
