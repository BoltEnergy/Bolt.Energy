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
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.Bussiness
{
    public class AbusesOfPostWithPermissionCheck : AbusesOfPost
    {
        UserOrOperator _operatingUserOrOperator;

        public AbusesOfPostWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int postId)
            : base(conn, transaction, postId)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        private void CheckAddPermission()
        { }

        public int Add(int forumId,int abuseUserOrOperatorId, string note, DateTime abuseDate)
        {
            CheckAbuse(forumId);
            return base.Add(abuseUserOrOperatorId, note, abuseDate,_operatingUserOrOperator);
        }

        private void CheckAbuse(int forumId)
        {
            //post deleted
            PostWithPermissionCheck post = new PostWithPermissionCheck(_conn, _transaction, PostId, _operatingUserOrOperator);
            if (post.IfDeleted)
                ExceptionHelper.ThrowPostNotExistException(PostId);
            bool IfAnnoucement;
            //annoucement or topic is not exsit
            TopicBase topicbase = TopicFactory.CreateTopic(_conn, _transaction, post.TopicId, _operatingUserOrOperator, out IfAnnoucement);
            ForumWithPermissionCheck forum;
            if (!IfAnnoucement)
            {
                //topic deleted
                TopicWithPermissionCheck topic = topicbase as TopicWithPermissionCheck;
                if (topic.IfDeleted)
                    ExceptionHelper.ThrowTopicNotExistException(post.TopicId);
                //forum is not exsit
                forum = new ForumWithPermissionCheck(_conn, _transaction, topic.ForumId, _operatingUserOrOperator);
            }
            else
            {
                //forum is not exsit
                forum = new ForumWithPermissionCheck(_conn, _transaction, forumId, _operatingUserOrOperator);
            }
            if (post.GetAbuseStatusOfUser(_operatingUserOrOperator.Id) == EnumPostAbuseStatus.AbusedAndPending)
            {
                ExceptionHelper.ThrowForumPostIsAbuseingException();
            }
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            if (!CommFun.IfAdminInUI(_operatingUserOrOperator) &&
                !CommFun.IfModeratorInUI(_conn, _transaction, forumId, _operatingUserOrOperator)&&
                CommFun.IfForumIsClosedInForumPage(forum))
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        }

        public AbuseWithPermissionCheck[] GetAllAbuses()
        {
            return base.GetAllAbuses(_operatingUserOrOperator);
        }

        public AbuseWithPermissionCheck[] GetAllAbusesOfUser(int userId)
        {
            return base.GetAllAbusesOfUser(_operatingUserOrOperator, userId);
        }

        public void ApproveAubsesOfPost(int forumId)
        {
            CheckWhenRefuseOrApprovalPost(forumId);
            base.ApproveAubsesOfPost(_operatingUserOrOperator);
        }

        public void RefuseAbusesOfPost(int forumId)
        {
            CheckWhenRefuseOrApprovalPost(forumId);
            base.RefuseAbusesOfPost(_operatingUserOrOperator);
        }

        private void CheckWhenRefuseOrApprovalPost(int forumId)
        {
            //post deleted
            PostWithPermissionCheck post = new PostWithPermissionCheck(_conn, _transaction, PostId, _operatingUserOrOperator);
            if (post.IfDeleted)
                ExceptionHelper.ThrowPostNotExistException(PostId);
            bool IfAnnoucement;
            //annoucement or topic is not exsit
            TopicBase topicbase = TopicFactory.CreateTopic(_conn, _transaction, post.TopicId, _operatingUserOrOperator, out IfAnnoucement);
            ForumWithPermissionCheck forum;
            if (!IfAnnoucement)
            {
                //topic deleted
                TopicWithPermissionCheck topic = topicbase as TopicWithPermissionCheck;
                if (topic.IfDeleted)
                    ExceptionHelper.ThrowTopicNotExistException(post.TopicId);
                //forum is not exsit
                forum = new ForumWithPermissionCheck(_conn, _transaction, topic.ForumId, _operatingUserOrOperator);
            }
            else
            {
                //forum is not exsit
                forum = new ForumWithPermissionCheck(_conn, _transaction, forumId, _operatingUserOrOperator);
            }
            if (post.GetAbuseStatus() != EnumPostAbuseStatus.AbusedAndPending)
            {
                ExceptionHelper.ThrowForumPostNotAbusedException();
            }
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            if (!CommFun.IfAdminInUI(_operatingUserOrOperator) &&
                !CommFun.IfModeratorInUI(_conn, _transaction, forumId, _operatingUserOrOperator))
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        }
    }
}
