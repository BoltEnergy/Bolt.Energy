#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.Bussiness
{
    public class AttachmentWithPermissionCheck : Attachment
    {
        UserOrOperator _operatingUserOrOperator;

        public AttachmentWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int id)
            : base(conn, transaction, id)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public AttachmentWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int id, 
            int postId, int uploadUserOrOperatorId, int size, byte[] attachment,
            bool ifPayScoreRequired, int score,string name,string description,int type)
            :base(conn, transaction, id, postId, uploadUserOrOperatorId, size, attachment, ifPayScoreRequired, score,name,description,type)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public void Pay()
        { }

        public void Delete(int forumId)
        {
            bool ifModerator = CommFun.IfModeratorInUI(_conn, _transaction, forumId, _operatingUserOrOperator);
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);

            /*If Allow View Forum Permission*/
            CommFun.UserPermissionCache().CheckIfAllowViewForumPermission(forumId, _operatingUserOrOperator,ifModerator);
            /*If Allow View Topic/Post*/
            CommFun.UserPermissionCache().CheckIfAllowViewTopicPermission(forumId, _operatingUserOrOperator, ifModerator);
            /*If Allow Post Topic/Post*/
            CommFun.UserPermissionCache().CheckIfAllowPostTopicOrPostPermission(forumId, _operatingUserOrOperator, ifModerator);

            /*Allow Attach*/
            CommFun.UserPermissionCache().CheckIfAllowAttachmentPermission(_operatingUserOrOperator, ifModerator);

            base.Delete();
        }

        public void DeleteWithNoPermissionCheck()
        {
            base.Delete();
        }


        public void Update(int forumId,int postId, int score, string description, EnumAttachmentType type)
        {
            bool ifModerator = CommFun.IfModeratorInUI(_conn, _transaction, forumId, _operatingUserOrOperator);
            /*If Allow View Forum Permission*/
            CommFun.UserPermissionCache().CheckIfAllowViewForumPermission(forumId, _operatingUserOrOperator, ifModerator);
            /*If Allow View Topic/Post*/
            CommFun.UserPermissionCache().CheckIfAllowViewTopicPermission(forumId, _operatingUserOrOperator, ifModerator);
            /*If Allow Post Topic/Post*/
            CommFun.UserPermissionCache().CheckIfAllowPostTopicOrPostPermission(forumId, _operatingUserOrOperator, ifModerator);

            /*Allow Attach*/
            CommFun.UserPermissionCache().CheckIfAllowAttachmentPermission(_operatingUserOrOperator, ifModerator);

            base.Update(postId,score,description,type);
        }

        public byte[] GetDownloadFile()
        {
            //post deleted
            PostWithPermissionCheck post = new PostWithPermissionCheck(_conn, _transaction, PostId, _operatingUserOrOperator);
            if(post.IfDeleted)
                ExceptionHelper.ThrowPostNotExistException(post.PostId);
            //topic deleted
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn,_transaction,post.TopicId,_operatingUserOrOperator);
            if(topic.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(topic.TopicId);
            //forum deleted
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, topic.ForumId, _operatingUserOrOperator);
            //forum closed
            if(!CommFun.IfAdminInUI(_operatingUserOrOperator) &&
                !CommFun.IfModeratorInUI(_conn,_transaction,topic.ForumId,_operatingUserOrOperator)&&
                CommFun.IfForumIsClosedInForumPage(forum))
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);

            if(_operatingUserOrOperator != null)
                CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
       
            PayHistoriesOfAttachment history = new PayHistoriesOfAttachment(_conn, _transaction, this.Id);
            ForumFeature feature = new ForumFeatureWithPermissionCheck(_conn, _transaction, _operatingUserOrOperator);
            bool IfUserPayAttach =  CommFun.IfAdminInUI(_operatingUserOrOperator) ||
                                    CommFun.IfModeratorInUI(_conn,_transaction,topic.ForumId,_operatingUserOrOperator)||
                                    (_operatingUserOrOperator!=null && history.IfPaid(_operatingUserOrOperator.Id))||
                                    (_operatingUserOrOperator != null && (_operatingUserOrOperator.Id == this.UploadUserOrOperatorId)) ||
                                    (this.Score == 0)||
                                    !feature.IfEnableScore;
            if (IfUserPayAttach == false)
            {
                ExceptionHelper.ThrowForumUserOrOperatorHaveNotPaidException();
            }

            return this.AttachmentFile;
        }
    }
}
