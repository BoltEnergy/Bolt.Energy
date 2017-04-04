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
    public class PayHistoriesOfAttachment : PayHistoriesBase
    {
        private int _attachmentId;
        public int AttachmentId
        {
            get { return this._attachmentId; }
        }

        public PayHistoriesOfAttachment(SqlConnectionWithSiteId conn, SqlTransaction transaction,int attachmentId)
            : base(conn, transaction)
        {
            _attachmentId = attachmentId;
        }

        public void Add(UserOrOperator user,int userId, int score, DateTime date)
        {
            AttachmentWithPermissionCheck attachment;
            CheckAddPayHistoryOfAttachment(user, userId, out attachment);
            user.DecreaseScore(score);
            UserOrOperator Author = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction,
                null, attachment.UploadUserOrOperatorId);
            Author.IncreaseScore(score);
            PayHistoryAccess.PayHistoryAdd(_conn, _transaction, EnumPayType.Attachment, 
                userId, _attachmentId, score, date);
        }
        private void CheckAddPayHistoryOfAttachment(UserOrOperator user,int userId,out AttachmentWithPermissionCheck attachment)
        {
            CommFun.CheckCommonPermissionInUI(user);
            attachment = new AttachmentWithPermissionCheck(_conn, _transaction, user, _attachmentId);
            PostWithPermissionCheck post = new PostWithPermissionCheck(_conn, _transaction, attachment.PostId, user);
            if (post.IfDeleted)
                ExceptionHelper.ThrowPostNotExistException(post.PostId);
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, post.TopicId, user);
            if (topic.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(topic.TopicId);
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, topic.ForumId, user);
            if (!CommFun.IfAdminInUI(user) && !CommFun.IfModeratorInUI(_conn, _transaction, forum.ForumId, user)
                && CommFun.IfForumIsClosedInForumPage(forum))
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(user.DisplayName);

            //close score feature
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, user);
            if (!forumFeature.IfEnableScore)
                ExceptionHelper.ThrowForumSettingsCloseScoreFunctio();
            //user's score < attachment score
            UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, user, userId);
            if (userOrOperator.Score < attachment.Score)
            {
                ExceptionHelper.ThrowForumUserOrOperatorScoreIsNotEnoughException();
            }
            if (IfPaid(userId))
            {
                ExceptionHelper.ThrowForumUserOrOperatorHavePaidException();
            }
            

        }
        public bool IfPaid(int userId)
        {
            return PayHistoryAccess.IfUserPaid(_conn, _transaction, EnumPayType.Attachment,
                userId, _attachmentId);
        }

        public void Delete()
        {
            PayHistoryAccess.DeleteHistroies(_conn, _transaction, EnumPayType.Attachment, _attachmentId);
        }

    }
}
