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
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.ASPNETState;

namespace Com.Comm100.Forum.Bussiness
{
    public class PostWithPermissionCheck : Post
    {
        UserOrOperator _operatingUserOrOperator;

        //private bool _ifCanEdit;
        //private bool _ifCanDelete;
        //private bool _ifCanUnMark;
        //private bool _ifCanMark;
        //private bool _ifCanQuote;

        //public override bool IfCanEdit
        //{
        //    get
        //    {
        //        return _ifCanEdit;
        //    }
        //}
        //public override bool IfCanDelete
        //{
        //    get
        //    {
        //        return _ifCanDelete;
        //    }
        //}
        //public override bool IfCanQuote
        //{
        //    get
        //    {
        //        return _ifCanQuote;
        //    }
        //}
        //public override bool IfCanUnMark
        //{
        //    get
        //    {
        //        return _ifCanUnMark;
        //    }
        //}
        //public override bool IfCanMarkAsAnswer
        //{
        //    get
        //    {
        //        return _ifCanMark;
        //    }
        //}


        public PostWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction, postId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public PostWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId, UserOrOperator operatingUserOrOperator,
            int topicId, bool ifTopic, int layer, string subject, string content, int postUserOrOperatorId, string postUserOrOperatorName, bool ifPostUserOrOperatorCustomizeAvatar,
            string postUserOrOperatorSystemAvatar, string postUserOrOperatorCustomizeAvatar, string postUserOrOperatorSignature, bool ifPostUserOrOperatorDeleted,
            int postUserOrOperatorNumberOfPosts, DateTime postUserOrOperatorJoinedTime, DateTime postTime, bool ifAnswer, int lastEditUserOrOperatorId,
            string lastEditUserOrOperatorName, bool ifLastEditUserOrOperatorDeleted, DateTime lastEditTime, string textContent)
            : base(conn, transaction, postId, topicId, ifTopic, layer, subject, content, postUserOrOperatorId, postUserOrOperatorName, ifPostUserOrOperatorCustomizeAvatar, postUserOrOperatorSystemAvatar,
            postUserOrOperatorCustomizeAvatar, postUserOrOperatorSignature, ifPostUserOrOperatorDeleted, postUserOrOperatorNumberOfPosts, postUserOrOperatorJoinedTime,
            postTime, ifAnswer, lastEditUserOrOperatorId, lastEditUserOrOperatorName, ifLastEditUserOrOperatorDeleted, lastEditTime, textContent)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;

            //bool ifcanEdit = false;
            //if (IfAdmin() || IfModerators() || (IfOwnPostAndNoReply() && !IfTopicClosed()))
            //{
            //    ifcanEdit = true;
            //}
            //_ifCanEdit = ifcanEdit;

            //bool ifcanDelete = false;
            //if (IfAdmin() || IfModerators())
            //{
            //    ifcanDelete = true;
            //}
            //this._ifCanDelete = ifcanDelete;

            //bool ifCanUnMark = false;
            //if (IfAdmin() || IfModerators() || IfOwnTopic() && !IfTopicClosed() && !ifTopic && IfAnswer)
            //{
            //    ifCanUnMark = true;
            //}
            //this._ifCanUnMark = ifCanUnMark;

            //bool ifCanMark = false;
            //if (IfAdmin() || IfModerators() || IfOwnTopic() && !IfTopicClosed() && !IfTopic && !IfAnswer)
            //{
            //    ifCanMark = true;
            //}
            //this._ifCanMark = ifCanMark;

            //bool ifCanQuote = false;
            //if (IfAdmin() || IfModerators() || !IfTopicClosed())
            //{
            //    ifCanQuote = true;
            //}
            //this._ifCanQuote = ifCanQuote;
        }

        public PostWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId, UserOrOperator operatingUserOrOperator,
           int topicId, bool ifTopic, int layer, string subject, string content,
          int postUserOrOperatorId, string postUserOrOperatorName, bool ifPostUserOrOperatorCustomizeAvatar, string postUserOrOperatorSystemAvatar,
          string postUserOrOperatorCustomizeAvatar, string postUserOrOperatorSignature, bool ifPostUserOrOperatorDeleted, int postUserOrOperatorNumberOfPosts,
          DateTime postUserOrOperatorJoinedTime, DateTime postTime, bool ifAnswer, int lastEditUserOrOperatorId, string lastEditUserOrOperatorName,
          bool ifLastEditUserOrOperatorDeleted, DateTime lastEditTime, string textContent, bool ifDeleted, short moderationStatus)
            : base(conn, transaction, postId, topicId, ifTopic, layer, subject, content, postUserOrOperatorId, postUserOrOperatorName, ifPostUserOrOperatorCustomizeAvatar, postUserOrOperatorSystemAvatar,
            postUserOrOperatorCustomizeAvatar, postUserOrOperatorSignature, ifPostUserOrOperatorDeleted, postUserOrOperatorNumberOfPosts, postUserOrOperatorJoinedTime,
            postTime, ifAnswer, lastEditUserOrOperatorId, lastEditUserOrOperatorName, ifLastEditUserOrOperatorDeleted, lastEditTime, textContent,
            ifDeleted, moderationStatus)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;

            //bool ifcanEdit = false;
            //if (IfAdmin() || IfModerators() || (IfOwnPostAndNoReply() && !IfTopicClosed()))
            //{
            //    ifcanEdit = true;
            //}
            //_ifCanEdit = ifcanEdit;

            //bool ifcanDelete = false;
            //if (IfAdmin() || IfModerators())
            //{
            //    ifcanDelete = true;
            //}
            //this._ifCanDelete = ifcanDelete;

            //bool ifCanUnMark = false;
            //if (IfAdmin() || IfModerators() || IfOwnTopic() && !IfTopicClosed() && !ifTopic && IfAnswer)
            //{
            //    ifCanUnMark = true;
            //}
            //this._ifCanUnMark = ifCanUnMark;

            //bool ifCanMark = false;
            //if (IfAdmin() || IfModerators() || IfOwnTopic() && !IfTopicClosed() && !IfTopic && !IfAnswer)
            //{
            //    ifCanMark = true;
            //}
            //this._ifCanMark = ifCanMark;

            //bool ifCanQuote = false;
            //if (IfAdmin() || IfModerators() || !IfTopicClosed())
            //{
            //    ifCanQuote = true;
            //}
            //this._ifCanQuote = ifCanQuote;
        }

        public void Update(string subject, string content,
            DateTime editTime, int[] attachIds, int[] scores, string[] descriptions,
            int forumId, int[] toDeleteAttachIds)
        {
            bool ifModerator = CommFun.IfModeratorInUI(_conn, _transaction, forumId, _operatingUserOrOperator);
            if (this.IfDeleted)
                ExceptionHelper.ThrowPostNotExistException(PostId);
            CheckTopicInForum(forumId);
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, forumId, _operatingUserOrOperator);
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            //CheckUpdatePermission();
            CommFun.CheckPostStatusWhenEditTopicOrPost(_conn, _transaction,
                this, forumId, _operatingUserOrOperator);
            /*2.0*/
            if (CommFun.IfGuest())
                ExceptionHelper.ThrowUserNotLoginException();
            else if (!IfAdmin()
               && !IfModerators()
               && (CommFun.IfForumIsClosedInForumPage(forum)
                || !IfOwnPostAndNoReply()))
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);

            /*Min Interval Time for Posting*/
            PostsOfUserOrOperatorWithPermissionCheck postsOfUser = new PostsOfUserOrOperatorWithPermissionCheck(
                _conn, _transaction, _operatingUserOrOperator, _operatingUserOrOperator.Id);

            CommFun.CommonPostOrTopicNewOrEditPermissionWithNoPostModerationCheck(postsOfUser, ref content, forumId, _operatingUserOrOperator, ifModerator);

            /*Post Moderation Not Required*/
            bool ifNeedModeration = !CommFun.UserPermissionCache().IfPostModerationNotRequiredPermission(forumId, _operatingUserOrOperator, ifModerator);

            base.Update(forumId, subject, content, _operatingUserOrOperator.Id, editTime, _operatingUserOrOperator,
                attachIds, scores, descriptions, toDeleteAttachIds, ifNeedModeration);
        }

        public void UpdateAnnoucementPost(string subject, string content, int editUserOrOperatorId,
           DateTime editTime, int forumId)
        {
            bool ifModerator = CommFun.IfModeratorInUI(_conn, _transaction, forumId, _operatingUserOrOperator);
            //CheckAnnoucementInForum(forumId);
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, forumId, _operatingUserOrOperator);
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            CommFun.CheckPostStatusWhenEditTopicOrPost(_conn, _transaction,this,forumId,
                 _operatingUserOrOperator);
            /*2.0*/
            if (CommFun.IfGuest())
                ExceptionHelper.ThrowUserNotLoginException();
            else if (!IfAdmin()
               && !IfModeratorsOfAnnoucement(forumId)
               && (CommFun.IfForumIsClosedInForumPage(forum)
                || !IfOwnPostAndNoReply()))
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);

            /*Min Interval Time for Posting*/
            PostsOfUserOrOperatorWithPermissionCheck postsOfUser = new PostsOfUserOrOperatorWithPermissionCheck(
                _conn, _transaction, _operatingUserOrOperator, _operatingUserOrOperator.Id);

            CommFun.CommonPostOrTopicNewOrEditPermissionWithNoPostModerationCheck(postsOfUser, ref content, forumId, _operatingUserOrOperator, ifModerator);

            /*Post Moderation Not Required*/
            bool ifNeedModeration = !CommFun.UserPermissionCache().IfPostModerationNotRequiredPermission(forumId, _operatingUserOrOperator, ifModerator);

            base.UpdateAnnoucementPost(subject, content, _operatingUserOrOperator.Id, editTime, _operatingUserOrOperator, ifNeedModeration);
        }

        private void CheckTopicInForum(int forumId)
        {
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, TopicId, _operatingUserOrOperator);
            if (forumId != topic.ForumId)
            {
                ExceptionHelper.ThrowTopicNotExistException(TopicId);
            }
            if (topic.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(topic.TopicId);
        }

        private void CheckAnnoucementInForum(int forumId)
        {
            AnnouncementWithPermissionCheck annoucement = new AnnouncementWithPermissionCheck(_conn, _transaction, _operatingUserOrOperator, TopicId);
            ForumWithPermissionCheck[] forums = annoucement.GetForums().GetAllForums();
            bool ifContainAnnoucementForum = (from a in forums
                                              select a.ForumId).ToList().Contains(forumId);
            if (!ifContainAnnoucementForum)
            {
                ExceptionHelper.ThrowAnnouncementNotExsitException(TopicId);
            }
        }

        private void CheckUpdatePermission()
        {
            //if (_operatingUserOrOperator == null)
            //{
            //    ExceptionHelper.ThrowUserNotLoginException();
            //}
            //else if (!IfAdmin() && !IfModerators() && (IfForumLockedOrHidden() || IfTopicClosed() || !IfOwnPostAndNoReply()))
            //    ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            if (IfModerators())
            {
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(this._conn, this._transaction, base.TopicId, _operatingUserOrOperator);
                CommFun.CheckModeratorPanelCommonPermission(_operatingUserOrOperator, topic.ForumId);
            }
            else if (IfAdmin())
            {
                CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            }
            else if (!IfOwnPostAndNoReply())
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        public void MarkAsAnswer()
        {
            CheckMarkAsAnswerOrUnMarkPermission();
            base.MarkAsAnswer(_operatingUserOrOperator);
        }

        private void CheckMarkAsAnswerOrUnMarkPermission()
        {
            //if (_operatingUserOrOperator == null)
            //{
            //    ExceptionHelper.ThrowUserNotLoginException();
            //}
            //else if (!IfAdmin() && !IfModerators() && (IfForumLockedOrHidden() || IfTopicClosed() || !IfOwnTopic()))
            //    ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            if (this.IfDeleted)
                ExceptionHelper.ThrowPostNotExistException(PostId);
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, TopicId, _operatingUserOrOperator);
            if (topic.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(TopicId);
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, topic.ForumId, _operatingUserOrOperator);
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            if (!CommFun.IfAdminInUI(_operatingUserOrOperator) &&
                !CommFun.IfModeratorInUI(_conn, _transaction, forum.ForumId, _operatingUserOrOperator) &&
                ((CommFun.IfForumIsClosedInForumPage(forum) || topic.IfClosed ||
                !(_operatingUserOrOperator.Id == topic.PostUserOrOperatorId))))
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        public void UnMarkAsAnswer()
        {
            CheckMarkAsAnswerOrUnMarkPermission();
            base.UnMarkAsAnswer(_operatingUserOrOperator);
        }

        //private void CheckUnmarkAsAnswerPermission()
        //{
        //    if (_operatingUserOrOperator == null)
        //    {
        //        ExceptionHelper.ThrowUserNotLoginException();
        //    }
        //    else if (!IfAdmin() && !IfModerators() && (IfForumLockedOrHidden() || IfTopicClosed() || !IfOwnTopic()))
        //        ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        //}

        public void Delete()
        {
            CheckPermission();
            base.Delete(_operatingUserOrOperator);
        }

        public void DeletePostOrTopic()
        {
            //CheckPermission();
            CheckRestoreOrDeletePostOfTopic();
            base.DeletePostOrTopic(_operatingUserOrOperator);
        }

        public void DeletePostOfAnnoucement(int forumId)
        {
            CheckDeletePostOfAnnoucementPermission(forumId);
            base.DeletePostOfAnnoucement(_operatingUserOrOperator, forumId);
        }

        public void CheckDeletePostOfAnnoucementPermission(int forumId)
        {
            AnnouncementWithPermissionCheck annoucement = new AnnouncementWithPermissionCheck(_conn, _transaction, _operatingUserOrOperator,
                TopicId);
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, forumId, _operatingUserOrOperator);
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            if (!CommFun.IfAdminInUI(_operatingUserOrOperator) && !CommFun.IfModeratorInUI(_conn, _transaction, forumId, _operatingUserOrOperator))
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        //private void CheckDeletePermission()
        //{
        //    if (_operatingUserOrOperator == null)
        //    {
        //        ExceptionHelper.ThrowOperatorNotLoginException();
        //    }
        //    else if (!IfAdmin() && !IfModerators())
        //        ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        //}

        public override void UpdateForumId(int forumId)
        {
            CheckPermission();
            base.UpdateForumId(forumId);
        }

        private void CheckPermission()
        {
            if (IfModerators())
            {
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(this._conn, this._transaction, base.TopicId, _operatingUserOrOperator);
                CommFun.CheckModeratorPanelCommonPermission(_operatingUserOrOperator, topic.ForumId);
            }
            else if (IfAdmin())
            {
                CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            }
        }
        //private void CheckUpdateForumIdPermission()
        //{
        //    if (_operatingUserOrOperator == null)
        //    {
        //        ExceptionHelper.ThrowOperatorNotLoginException();
        //    }
        //    else if (!IfAdmin() && !IfModerators())
        //        ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        //}

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
            //    if (_operatingUserOrOperator.IfForumAdmin)
            //        ifAdmin = true;
            //}
            //return ifAdmin;
            return CommFun.IfAdminInUI(_operatingUserOrOperator);
        }

        private bool IfModerators()
        {
            //TopicWithPermissionCheck topic = new TopicWithPermissionCheck(this._conn, this._transaction, base.TopicId, _operatingUserOrOperator);
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
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(this._conn, this._transaction, base.TopicId, _operatingUserOrOperator);
            //TopicBase topicBase = TopicFactory.CreateTopic(_conn, _transaction, TopicId, _operatingUserOrOperator);
            return CommFun.IfModeratorInUI(_conn, _transaction, topic.ForumId, _operatingUserOrOperator);
        }

        private bool IfModeratorsOfAnnoucement(int forumId)
        {
            return CommFun.IfModeratorInUI(_conn, _transaction, forumId, _operatingUserOrOperator);

        }

        private bool IfOwnPostAndNoReply()
        {
            //TopicWithPermissionCheck tmpTopic = new TopicWithPermissionCheck(base._conn, base._transaction, base.TopicId, _operatingUserOrOperator);
            bool ifAnnoucement;
            TopicBase topicOrAnnoucement = TopicFactory.CreateTopic(_conn, _transaction, TopicId, _operatingUserOrOperator, out ifAnnoucement);

            bool ifOwnAndNoReply = false;
            if (_operatingUserOrOperator != null)
            {
                if (this._operatingUserOrOperator.Id == base.PostUserOrOperatorId && (topicOrAnnoucement.NumberOfReplies == 0 || base.PostId == topicOrAnnoucement.LastPostId))
                {
                    ifOwnAndNoReply = true;
                }
            }
            return ifOwnAndNoReply;
        }

        private bool IfOwnTopic()
        {
            TopicWithPermissionCheck tmpTopic = new TopicWithPermissionCheck(base._conn, base._transaction, base.TopicId, _operatingUserOrOperator);

            bool ifOwnTopic = false;
            if (_operatingUserOrOperator != null)
            {
                if (this._operatingUserOrOperator.Id == tmpTopic.PostUserOrOperatorId)
                {
                    ifOwnTopic = true;
                }
            }
            return ifOwnTopic;
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

        private bool IfForumLockedOrHidden()
        {
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, TopicId, _operatingUserOrOperator);
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, topic.ForumId, _operatingUserOrOperator);

            bool ifForumLockedOrHidden = false;
            if (forum.Status == EnumForumStatus.Lock || forum.Status == EnumForumStatus.Hide)
            {
                ifForumLockedOrHidden = true;
            }
            return ifForumLockedOrHidden;
        }

        private bool IfForumLockedOrHiddenOfAnnoucement(int fourmId)
        {
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, fourmId, _operatingUserOrOperator);

            bool ifForumLockedOrHidden = false;
            if (forum.Status == EnumForumStatus.Lock || forum.Status == EnumForumStatus.Hide)
            {
                ifForumLockedOrHidden = true;
            }
            return ifForumLockedOrHidden;
        }

        /*----------------------2.0------------------------*/
        public AbusesOfPostWithPermissionCheck GetAbuses()
        {
            return base.GetAbuses(_operatingUserOrOperator);
        }

        public void LogicDelete()
        {
            //CheckPermission();
            if (this.IfDeleted)
                ExceptionHelper.ThrowPostNotExistException(PostId);
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(
                    _conn, _transaction, TopicId, _operatingUserOrOperator);
            if (topic.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(TopicId);
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, topic.ForumId,
                _operatingUserOrOperator);

            CommFun.CheckAdminPanelOrModeratorPanelCommomPermission(_operatingUserOrOperator, topic.ForumId);
            base.LogicDelete(_operatingUserOrOperator);
        }

        public void DeletePostOfAnnoucementOrTopic(int forumId)
        {
            CommFun.CheckAdminPanelOrModeratorPanelCommomPermission(_operatingUserOrOperator, forumId);
            bool ifAnnoucement;
            TopicBase topicbase = TopicFactory.CreateTopic(_conn, _transaction, TopicId, _operatingUserOrOperator,
               out ifAnnoucement);
            CheckDeletePostOfAnnoucementOrTopicPermission(forumId, ifAnnoucement, topicbase);
            if (!ifAnnoucement)
                this.LogicDelete(_operatingUserOrOperator);
            else
                this.DeletePostOfAnnoucement(_operatingUserOrOperator, forumId);
        }

        private void CheckDeletePostOfAnnoucementOrTopicPermission(int forumId,
            bool ifAnnoucement, TopicBase topicBase)
        {
            if (!ifAnnoucement)
            {
                //post deleted
                if (this.IfDeleted)
                    ExceptionHelper.ThrowPostNotExistException(this.PostId);
                //topic deleted
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, TopicId, _operatingUserOrOperator);
                if (topic.IfDeleted)
                    ExceptionHelper.ThrowTopicNotExistException(TopicId);
                //topic moved
                if (topic.ForumId != forumId)
                    ExceptionHelper.ThrowPostNotExistException(PostId);
            }
            else
            {
                ModeratorsOfAnnoucement moderatorsOfAnnouncement = new ModeratorsOfAnnoucement(_conn,
                    _transaction,TopicId);
                bool ifModeratorOfAnnouncement = moderatorsOfAnnouncement.IfModerator(_operatingUserOrOperator);
                if (ifModeratorOfAnnouncement == false)
                    ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, forumId,
                _operatingUserOrOperator);
        }

        public void LogicDeletePostsOfTopic()
        {
            //post deleted
            if (this.IfDeleted)
                ExceptionHelper.ThrowPostNotExistException(this.PostId);
            //topic deleted
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, TopicId, _operatingUserOrOperator);
            if (topic.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(TopicId);
            //forum deleted
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, topic.ForumId, _operatingUserOrOperator);

            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);

            if (!CommFun.IfAdminInUI(_operatingUserOrOperator) &&
                !CommFun.IfModeratorInUI(_conn, _transaction, forum.ForumId, _operatingUserOrOperator))
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            base.LogicDelete(_operatingUserOrOperator);
        }

        public void Restore()
        {
            CheckPermission();
            base.Restore(_operatingUserOrOperator);
        }

        public void ResotrePostOfTopic()
        {
            CheckRestoreOrDeletePostOfTopic();
            base.Restore(_operatingUserOrOperator);
        }

        private void CheckRestoreOrDeletePostOfTopic()
        {
            //post deleted
            PostWithPermissionCheck post = this;//new PostWithPermissionCheck(_conn, _transaction, PostId, _operatingUserOrOperator);
            if (!post.IfDeleted)
            {
                ExceptionHelper.ThrowForumPostNotInRecycleBinException();
            }

            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, TopicId, _operatingUserOrOperator);
            //if ((post.IfTopic && !post.IfDeleted) || topic.IfDeleted)
            //    ExceptionHelper.ThrowTopicNotExistException(TopicId);
            if (!post.IfTopic)
            {
                if (topic.IfDeleted == true)
                    ExceptionHelper.ThrowTopicNotExistException(TopicId);
            }
            

            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, topic.ForumId, _operatingUserOrOperator);

            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            if (!CommFun.IfAdminInUI(_operatingUserOrOperator) &&
                !CommFun.IfModeratorInUI(_conn, _transaction, forum.ForumId, _operatingUserOrOperator))
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        }

        public AttachmentsOfPostWithPermissionCheck GetAttachments()
        {
            return base.GetAttachments(_operatingUserOrOperator);
        }

        public void RefuseModerationByAdminOrModerator(int forumId)
        {
            CommFun.CheckAdminPanelOrModeratorPanelCommomPermission(_operatingUserOrOperator, forumId);
            CheckRefuseOrAcceptModerationWhenAdmin(forumId);
            if (this.ModerationStatus != EnumPostOrTopicModerationStatus.WaitingForModeration)
            {
                ExceptionHelper.ThrowForumPostNotWaitingForModerationException();
            }
            base.RefuseModeration(_operatingUserOrOperator);
        }

        public void AcceptModerationByAdminOrModerator(int forumId)
        {
            CommFun.CheckAdminPanelOrModeratorPanelCommomPermission(_operatingUserOrOperator, forumId);
            CheckRefuseOrAcceptModerationWhenAdmin(forumId);
            if (this.ModerationStatus != EnumPostOrTopicModerationStatus.WaitingForModeration &&
                this.ModerationStatus != EnumPostOrTopicModerationStatus.Rejected)
            {
                ExceptionHelper.ThrowForumPostNotWaingForModerationOrRejectedException();
            }
            base.AcceptModeration(_operatingUserOrOperator);
        }

        private void CheckRefuseOrAcceptModerationWhenAdmin(int forumId)
        {
            //post deleted
            PostWithPermissionCheck post = this;//new PostWithPermissionCheck(_conn, _transaction, PostId, _operatingUserOrOperator);
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
                // topic Moved
                if (topic.ForumId != forumId)
                    ExceptionHelper.ThrowPostNotExistException(PostId);
            }
            else
            {
                //forum is not exist
                forum = new ForumWithPermissionCheck(_conn, _transaction, forumId, _operatingUserOrOperator);
                bool ifAdmin = CommFun.IfAdminInUI(_operatingUserOrOperator);
                //not moderator of annoucement
                ModeratorsOfAnnoucement moderatorsOfAnnouncement = new ModeratorsOfAnnoucement(_conn, _transaction,
                    TopicId);
               
                bool ifModeratorOfAnnouncement = moderatorsOfAnnouncement.IfModerator(_operatingUserOrOperator);
                if (ifAdmin == false)
                {
                    if (ifModeratorOfAnnouncement == false)
                        ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
                }
            }
        }

        public void AcceptModeration(int forumId)
        {
            CheckRefuseOrAcceptModeration(forumId);
            base.AcceptModeration(_operatingUserOrOperator);
        }

        public void RefuseModeration(int forumId)
        {
            CheckRefuseOrAcceptModeration(forumId);
            base.RefuseModeration(_operatingUserOrOperator);
        }

        private void CheckRefuseOrAcceptModeration(int forumId)
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
            //if (post.ModerationStatus != EnumPostOrTopicModerationStatus.WaitingForModeration)
            //{
            //    ExceptionHelper.ThrowForumPostNotWaitingForModerationException();
            //}
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            if (!CommFun.IfAdminInUI(_operatingUserOrOperator) &&
                !CommFun.IfModeratorInUI(_conn, _transaction, forumId, _operatingUserOrOperator))
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        }

        public bool IfAbused()
        {
            return base.IfAbused(_operatingUserOrOperator);
        }

        public EnumPostAbuseStatus GetAbuseStatus()
        {
            return GetAbuseStatus(_operatingUserOrOperator);
        }

        public EnumPostAbuseStatus GetAbuseStatusOfUser(int userId)
        {
            return GetAbuseStatusOfUser(_operatingUserOrOperator, userId);
        }
    }
}
