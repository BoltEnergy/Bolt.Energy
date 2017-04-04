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
using Com.Comm100.Framework.Database;
using System.Data.SqlClient;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.Process
{
    public class AttachmentProcess
    {
        public static bool IfHasAttachmentByTopicId(
         int operatingUserOrOperatorId, int siteId, int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                PostsOfTopicWithPermissionCheck postsOfTopic = new PostsOfTopicWithPermissionCheck(conn, transaction, topicId, operatingUserOrOperator);
                PostWithPermissionCheck post = postsOfTopic.GetFirstPost();
                AttachmentsOfPostWithPermissionCheck attachmentsOfPost = new AttachmentsOfPostWithPermissionCheck(
                    conn, transaction, operatingUserOrOperator, post.PostId);

                return attachmentsOfPost.IfHasAttachment();
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
        public static AttachmentWithPermissionCheck GetAttachmentById(
            int operatingUserOrOperatorId, int siteId,int attachId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                AttachmentWithPermissionCheck attachment = new AttachmentWithPermissionCheck(
                    conn, transaction, operatingUserOrOperator, attachId);
                return attachment;
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

        public static AttachmentWithPermissionCheck[] GetAllAttachmentsOfPost(
            int operatingUserOrOperatorId, int siteId,
            int postId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                AttachmentsOfPostWithPermissionCheck attachmentsOfPost = new AttachmentsOfPostWithPermissionCheck(
                    conn, transaction, operatingUserOrOperator, postId);
                return attachmentsOfPost.GetAllAttachments();
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

        public static byte[] GetDownloadFileOfAttachment(
            int operatingUserOrOperatorId, int siteId, int attachId,out AttachmentWithPermissionCheck attachment)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                AttachmentWithPermissionCheck attachment1 = new AttachmentWithPermissionCheck(
                    conn, transaction, operatingUserOrOperator, attachId);
                attachment = attachment1;
                return attachment.GetDownloadFile();
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


        public static int AddAttachment(
           int operatingUserOrOperatorId, int siteId,int forumId, int attachmentsOfPostCount,
           int postId, int uploadUserOrOperatorId, int size, byte[] attachment,
            bool ifPayScoreRequired, int score, string name, string description,
            Guid guid,EnumAttachmentType type)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.
                    CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                AttachmentsBase attachmentsBase = null;
                if (type == EnumAttachmentType.AttachToPost)
                {
                    attachmentsBase = new
                        AttachmentsOfPostWithPermissionCheck(
                        conn, transaction, operatingUserOrOperator, postId);
                }
                else if(type == EnumAttachmentType.AttachToDraft)
                {
                    attachmentsBase = new
                        AttachmentsOfDraftWithPermissionCheck(
                        conn, transaction, operatingUserOrOperator,postId);
                }
                return attachmentsBase.Add(forumId, attachmentsOfPostCount, attachment, size, uploadUserOrOperatorId, ifPayScoreRequired,
                    score,name,description,guid);
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

        public static void DeleteAttachment(
            int operatingUserOrOperatorId, int siteId,
           int attachId,int forumId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.
                    CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                AttachmentWithPermissionCheck attachment = new AttachmentWithPermissionCheck(
                    conn, transaction, operatingUserOrOperator, attachId);
                attachment.Delete(forumId);
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

        public static AttachmentWithPermissionCheck[] GetTempAttachmentsOfUser(
            int operatingUserOrOperatorId, int siteId,
            int userId,Guid guid,EnumAttachmentType type)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.
                    CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                AttachmentsOfUserOrOperatorWithPermissionCheck attachmentOfUser = new AttachmentsOfUserOrOperatorWithPermissionCheck(
                    conn, transaction, operatingUserOrOperator, userId);
                return attachmentOfUser.GetAllTempAttachments(operatingUserOrOperator,guid,type);
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

        public static AttachmentWithPermissionCheck[] GetDraftAttachmentsOfTopic(
            int operatingUserOrOperatorId, int siteId,
            int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.
                    CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                DraftWithPermissionCheck draft = new DraftWithPermissionCheck(conn,
                    transaction, topicId, -1, operatingUserOrOperator);
                AttachmentsOfDraftWithPermissionCheck attachmentOfDraft = new AttachmentsOfDraftWithPermissionCheck(
                    conn, transaction, operatingUserOrOperator, draft.DraftId);
                return attachmentOfDraft.GetAllAttachments();
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

        public static void DeleteTempAttachmentsOfUser(int operatingUserOrOperatorId, int siteId,
           int userId,EnumAttachmentType type)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.
                    CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                AttachmentsOfUserOrOperatorWithPermissionCheck attachmentOfUser = new AttachmentsOfUserOrOperatorWithPermissionCheck(
                    conn, transaction, operatingUserOrOperator, userId);
                attachmentOfUser.DeleteAllTempAttachments(operatingUserOrOperator,type);
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
