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
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public class PostsOfTopicWithPermissionCheck : PostsOfTopic
    {
        UserOrOperator _operatingUserOrOperator;

        public PostsOfTopicWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction, topicId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public int Add(
            bool ifTopic, bool ifReplaceDraft,string subject, DateTime postTime, string content,
            int[] attachIds, int[] scores, string[] descriptions, int forumId)
        {
            bool ifModerator = CommFun.IfModeratorInUI(_conn, _transaction, forumId, _operatingUserOrOperator);
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);

            CheckAddPostOfTopicPermission(forumId);

            PostsOfUserOrOperatorWithPermissionCheck postsOfUser = new PostsOfUserOrOperatorWithPermissionCheck(
                _conn, _transaction, _operatingUserOrOperator, _operatingUserOrOperator.Id);

            CommFun.CommonPostOrTopicNewOrEditPermissionWithNoPostModerationCheck(postsOfUser,ref content, forumId,
                _operatingUserOrOperator, ifModerator);

            /*Post Moderation Not Required*/
            bool ifNeedModeration = !CommFun.UserPermissionCache().IfPostModerationNotRequiredPermission(forumId, _operatingUserOrOperator, ifModerator);

            return base.Add(forumId, ifTopic, ifReplaceDraft,subject, _operatingUserOrOperator, postTime, content,
                attachIds, scores, descriptions, ifNeedModeration);
        }

        //public int Add(
        //    bool ifTopic, string subject, DateTime postTime, string content,
        //    int[] attachIds, int[] scores, string[] descriptions)
        //{
        //    //CheckAddPermission();
        //    CommFun.CheckUserOrOperator(_operatingUserOrOperator);

        //    return base.Add(ifTopic, subject, _operatingUserOrOperator, postTime, content,
        //        attachIds, scores, descriptions, false);
        //}

        public int AddPostWithMoved(bool ifTopic, string subject,int postUserOrOperatorId, DateTime postTime, string content)
        {
            CommFun.CheckUserOrOperator(_operatingUserOrOperator);
            return base.AddPostWithMoved(ifTopic, subject, _operatingUserOrOperator, postUserOrOperatorId, postTime, content);
        }

        public int AddAnnouncementPost(bool ifTopic, string subject, DateTime postTime, string content, int forumId)
        {
            bool ifModerator = CommFun.IfModeratorInUI(_conn, _transaction, forumId, _operatingUserOrOperator);
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);

            CheckAddPostOfAnnoucementPermission(forumId);

            PostsOfUserOrOperatorWithPermissionCheck postsOfUser = new PostsOfUserOrOperatorWithPermissionCheck(
                _conn, _transaction, _operatingUserOrOperator, _operatingUserOrOperator.Id);

            CommFun.CommonPostOrTopicNewOrEditPermissionWithNoPostModerationCheck(postsOfUser,ref content, forumId, _operatingUserOrOperator, ifModerator);

            /*Post Moderation Not Required*/
            bool ifNeedModeration = !CommFun.UserPermissionCache().IfPostModerationNotRequiredPermission(forumId, _operatingUserOrOperator, ifModerator);
            return base.AddAnnoucementPost(ifTopic, subject, _operatingUserOrOperator, postTime, content, ifNeedModeration);
        }

        public int AddAnnouncementPost(bool ifTopic, string subject, DateTime postTime, string content)
        {
            CommFun.CheckUserOrOperator(_operatingUserOrOperator);
            return base.AddAnnoucementPost(ifTopic, subject, _operatingUserOrOperator, postTime, content, false);
        }

        public void CheckAddPostOfTopicPermission(int forumId)
        {
            //if (CommFun.IfGuest())
            //{
            //    ExceptionHelper.ThrowUserNotLoginException();
            //}
            //else if (!_operatingUserOrOperator.IfForumAdmin
            //    && !IfModerators()//!CommFun.UserPermissionCache().IfModerator(forumId)
            //    )
            //{
            //    if (IfForumLockedOrHidden(forumId))
            //    {
            //        ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            //    }
            //    else if (IfTopicClosed())
            //    {
            //        ExceptionHelper.ThrowTopicIsClosedException(base.TopicId);
            //    }
            //}
            //topic deleted/closed
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, TopicId, _operatingUserOrOperator);
            if (topic.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(TopicId);
            if (!CommFun.IfAdminInUI(_operatingUserOrOperator) &&
                !CommFun.IfModeratorInUI(_conn, _transaction, forumId, _operatingUserOrOperator) &&
                topic.IfClosed)
            {
                ExceptionHelper.ThrowTopicIsClosedException(TopicId);
            }
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, forumId, _operatingUserOrOperator);
            //forum deleted/closed
            if (!CommFun.IfAdminInUI(_operatingUserOrOperator) &&
                !CommFun.IfModeratorInUI(_conn, _transaction, forumId, _operatingUserOrOperator) &&
                CommFun.IfForumIsClosedInForumPage(forum))
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        public void CheckAddPostOfAnnoucementPermission(int forumId)
        {
            AnnouncementWithPermissionCheck announcement = new AnnouncementWithPermissionCheck(_conn, _transaction, _operatingUserOrOperator,
                TopicId);
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, forumId, _operatingUserOrOperator);
            //forum deleted/closed
            if (!CommFun.IfAdminInUI(_operatingUserOrOperator) &&
                !CommFun.IfModeratorInUI(_conn, _transaction, forumId, _operatingUserOrOperator))
            {
                if (CommFun.IfForumIsClosedInForumPage(forum))
                    ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        public void CheckAddPermission()
        {
            if (_operatingUserOrOperator == null)
            {
                ExceptionHelper.ThrowUserNotLoginException();
            }
            else if (!IfAdmin() && !IfModerators())
            {
                if (IfForumLockedOrHidden())
                {
                    ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
                }
                else if (IfTopicClosed())
                {
                    ExceptionHelper.ThrowTopicIsClosedException(base.TopicId);
                }
            }

        }

        public PostWithPermissionCheck GetFirstPost()
        {
            return base.GetFirstPost(this._operatingUserOrOperator);
        }

        public PostWithPermissionCheck GetLastPost()
        {
            return base.GetLastPost(this._operatingUserOrOperator);
        }

        public void Delete(int postId)
        {
            CommFun.CheckUserOrOperator(_operatingUserOrOperator);
            //CommFun.CheckIfUserOrOperatorBanned(_operatingUserOrOperator, forumId);
            CheckDeletePermission();
            base.Delete(postId, _operatingUserOrOperator);
        }

        public void DeleteAllPosts()
        {
            CommFun.CheckUserOrOperator(_operatingUserOrOperator);
            //CommFun.CheckIfUserOrOperatorBanned(_operatingUserOrOperator, forumId);
            CheckDeletePermission();
            base.DeleteAllPosts(_operatingUserOrOperator);
        }

        public void DeleteAllPostsOfAnnoucement()
        {
            CommFun.CheckUserOrOperator(_operatingUserOrOperator);
            //CommFun.CheckIfUserOrOperatorBanned(_operatingUserOrOperator, forumId);
            CheckDeletePermission();
            base.DeleteAllPostsOfAnnoucement(_operatingUserOrOperator);
        }

        private void CheckDeletePermission()
        {
            if (_operatingUserOrOperator == null)
            {
                ExceptionHelper.ThrowOperatorNotLoginException();
            }
            else if (!IfAdmin() && !IfModerators())
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        }

        public void MoveAllPosts(int forumId)
        {
            CommFun.CheckUserOrOperator(_operatingUserOrOperator);
            // CommFun.CheckIfUserOrOperatorBanned(_operatingUserOrOperator, forumId);
            CheckMovePermission();
            base.MoveAllPosts(forumId, this._operatingUserOrOperator);
        }

        private void CheckMovePermission()
        {
             TopicWithPermissionCheck topic = new TopicWithPermissionCheck(this._conn, this._transaction, base.TopicId, _operatingUserOrOperator);
            if (_operatingUserOrOperator == null)
            {
                ExceptionHelper.ThrowOperatorNotLoginException();
            }
            else if (!CommFun.IfAdminInUI(_operatingUserOrOperator) &&
                    !CommFun.IfModeratorInUI(_conn,_transaction,topic.ForumId,_operatingUserOrOperator))//(!IfAdmin() && !IfModerators())
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        }

        public PostWithPermissionCheck[] GetPostsByPaging(int pageIdex, int pageSize, out int postscount, int forumId)
        {
            ///*Allow View Froum*/
            //if (CommFun.IfGuest())
            //{
            //    GuestUserPermissionSettingWithPermissionCheck guestUser = new GuestUserPermissionSettingWithPermissionCheck(
            //   _conn, _transaction, _operatingUserOrOperator);
            //    if (!guestUser.IfAllowGuestUserViewForum)
            //        ExceptionHelper.ThrowForumPostNoPermissionException();
            //}
            //else
            //{
            //    CommFun.UserPermissionCache().CheckIfAllowViewForumPermission(
            //        forumId, _operatingUserOrOperator);
            //}
            ///*Allow View Topic/Post*/
            //if (!CommFun.IfGuest())
            //CommFun.UserPermissionCache().CheckIfAllowViewTopicPermission(forumId, _operatingUserOrOperator);
            return base.GetPostsByPaging(pageIdex, pageSize, this._operatingUserOrOperator, out postscount, forumId);
        }
        public PostWithPermissionCheck[] GetPostsByPaging(int pageIdex, int pageSize, out int postscount, int forumId, bool ifAnnoucement)
        {
            return base.GetPostsByPaging(pageIdex, pageSize, this._operatingUserOrOperator, out postscount, forumId);
        }

        public int GetCountOfPostsByPaging(int forumId)
        {
            return base.GetCountOfPostsByPaging(this._operatingUserOrOperator, forumId);
        }

        public PostWithPermissionCheck[] GetPostsByPaging(int pageIdex, int pageSize)
        {
            return base.GetPostsByPaging(pageIdex, pageSize, this._operatingUserOrOperator);
        }

        public int GetCountOfPostsByPaging()
        {
            return base.GetCountOfPostsByPaging();
        }

        public PostWithPermissionCheck GetAnswer()
        {
            return base.GetAnswer(this._operatingUserOrOperator);
        }

        public TopicWithPermissionCheck GetTopic()
        {
            return base.GetTopic(_operatingUserOrOperator);
        }

        public override int GetPostIndex(int postId)
        {
            return base.GetPostIndex(postId);
        }

        private bool IfAdmin()
        {
            //bool ifAdmin = false;
            //if (_operatingUserOrOperator != null)
            //{
            //    if (CommFun.IfOperator(_operatingUserOrOperator))
            //    {
            //        if (((OperatorWithPermissionCheck)_operatingUserOrOperator).IfAdmin)
            //        {
            //            ifAdmin = true;
            //        }
            //    }
            //}
            //return ifAdmin;
            return CommFun.IfAdminInUI(_operatingUserOrOperator);
        }

        private bool IfModerators()
        {
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(this._conn, this._transaction, base.TopicId, _operatingUserOrOperator);
            //ModeratorsWithPermisisonCheck moderators = new ModeratorsWithPermisisonCheck(this._conn, this._transaction, topic.ForumId, _operatingUserOrOperator);
            //Moderator[] moderator = moderators.GetAllModerators();

            //bool ifModerator = false;
            //for (int i = 0; i < moderator.Length; i++)
            //{
            //    if (_operatingUserOrOperator != null)
            //    {
            //        if (moderator[i].Id == this._operatingUserOrOperator.Id)
            //        {
            //            ifModerator = true;
            //            break;
            //        }
            //    }
            //}

            //return ifModerator;
            return CommFun.IfModeratorInUI(_conn, _transaction, topic.ForumId, _operatingUserOrOperator);
        }

        private bool IfTopicClosed()
        {
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, TopicId, _operatingUserOrOperator);

            bool ifTopicClosed = false;
            if (topic.IfClosed)
            {
                ifTopicClosed = true;
            }
            return ifTopicClosed;
        }

        private bool IfForumLockedOrHidden(int forumId)
        {
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, TopicId, _operatingUserOrOperator);
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, forumId, _operatingUserOrOperator);

            bool ifForumLockedOrHidden = false;
            if (forum.Status == EnumForumStatus.Lock || forum.Status == EnumForumStatus.Hide)
            {
                ifForumLockedOrHidden = true;
            }
            return ifForumLockedOrHidden;
        }
        private bool IfForumLockedOrHidden()
        {
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, TopicId, _operatingUserOrOperator);
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, TopicId, _operatingUserOrOperator);

            bool ifForumLockedOrHidden = false;
            if (forum.Status == EnumForumStatus.Lock || forum.Status == EnumForumStatus.Hide)
            {
                ifForumLockedOrHidden = true;
            }
            return ifForumLockedOrHidden;
        }

        /*-------------------------2.0------------------------*/
        public override PostWithPermissionCheck[] GetNotDeletedPostsByQueryAndPaging(
            UserOrOperator operatingUserOrOperator,
            string keywords, string name, DateTime startDate,
            DateTime endDate, int pageIndex, int pageSize, string orderFiled, string orderMethod, out int CountOfPosts)
        {
            return base.GetNotDeletedPostsByQueryAndPaging(operatingUserOrOperator, keywords, name, startDate,
                endDate, pageIndex, pageSize, orderFiled, orderMethod, out CountOfPosts);
        }
    }
}
