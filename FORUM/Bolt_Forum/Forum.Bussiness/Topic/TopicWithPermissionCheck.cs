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
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.Bussiness
{
    public class TopicWithPermissionCheck : Topic
    {
        UserOrOperator _operatingUserOrOperator;

        public override bool IfParticipant
        {
            get
            {
                bool blTmp = false;
                if (base.ParticipatorIds != null)
                {
                    for (int i = 0; i < base.ParticipatorIds.Length; i++)
                    {

                        if (_operatingUserOrOperator != null)
                        {
                            if (_operatingUserOrOperator.Id == ParticipatorIds[i])
                            {
                                blTmp = true;
                                break;
                            }
                        }
                    }
                }
                return blTmp;/*need to archive*/
            }
        }

        public TopicWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction, topicId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public TopicWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, UserOrOperator operatingUserOrOperator,
            int forumId, string forumName, string subject, int postUserOrOperatorId, string postUserOrOperatorName, bool postUserOrOperatorIfDeleted,
                DateTime postTime, int lastPostId, DateTime lastPostTime, int lastPostUserOrOperatorId, string lastPostUserOrOperatorName, bool lastPostUserOrOperatorIfDeleted, int numberOfReplies, int numberOfHits,
                bool ifClosed, bool ifMarkedAsAnswer, bool ifSticky, int[] participatorIds)

            : base(conn, transaction, topicId, forumId, forumName, subject, postUserOrOperatorId, postUserOrOperatorName, postUserOrOperatorIfDeleted, postTime, lastPostId, lastPostTime, lastPostUserOrOperatorId,
            lastPostUserOrOperatorName, lastPostUserOrOperatorIfDeleted, numberOfReplies, numberOfHits, ifClosed, ifMarkedAsAnswer, ifSticky, participatorIds)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }
        /*2.0*/
        public TopicWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, UserOrOperator operatingUserOrOperator, int forumId, string forumName, string subject, int postUserOrOperatorId, string postUserOrOperatorName, bool postUserOrOperatorIfDeleted,
              DateTime postTime, int lastPostId, DateTime lastPostTime, int lastPostUserOrOperatorId, string lastPostUserOrOperatorName, bool lastPostUserOrOperatorIfDeleted, int numberOfReplies, int numberOfHits,
              bool ifClosed, bool ifMarkedAsAnswer, bool ifSticky, int[] participatorIds, bool ifDeleted, short moderationStatus,
              bool ifPayScoreRequired, int score, bool ifMoveHistory, int locateTopicId, int locateForumId, DateTime moveDate,
              int moveUserOrOperatorId, bool ifFeatured, bool ifContainsPoll, bool ifReplyRequired, int totalPromotion)

            : base(conn, transaction, topicId, forumId, forumName, subject, postUserOrOperatorId, postUserOrOperatorName, postUserOrOperatorIfDeleted, postTime, lastPostId, lastPostTime, lastPostUserOrOperatorId,
            lastPostUserOrOperatorName, lastPostUserOrOperatorIfDeleted, numberOfReplies, numberOfHits, ifClosed, ifMarkedAsAnswer, ifSticky, participatorIds,
            ifDeleted, moderationStatus, ifPayScoreRequired, score, ifMoveHistory, locateTopicId, locateForumId, moveDate, moveUserOrOperatorId, ifFeatured,
            ifContainsPoll, ifReplyRequired, totalPromotion)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public void Update(string subject, string content,
            bool ifSetDeadline, DateTime endDate,
            int[] attachIds, int[] scores, string[] descriptions,
            int[] toDeleteAttachIds
            )
        {
            if (this.IfDeleted)
            {
                ExceptionHelper.ThrowTopicNotExistException(_topicId);
            }
            PostsOfTopicWithPermissionCheck postsOfTopic = base.GetPosts(_operatingUserOrOperator);
            CommFun.CheckPostStatusWhenEditTopicOrPost(_conn, _transaction, postsOfTopic.GetFirstPost(), this.ForumId, _operatingUserOrOperator);

            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, ForumId, _operatingUserOrOperator);

            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            //CheckUpdatePermission();
            /*2.0*/
            if (CommFun.IfGuest())
                ExceptionHelper.ThrowUserNotLoginException();
            else if (!IfAdmin()//!CommFun.UserPermissionCache().IfAdministrator
                && !IfModerators()//!CommFun.UserPermissionCache().IfModerator(ForumId)
                && (CommFun.IfForumIsClosedInForumPage(forum)
                //|| base.IfClosed 
                || !IfOwnTopicAndNoReply()))
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);

            base.Update(subject, content, _operatingUserOrOperator,
                ifSetDeadline, endDate,
                attachIds, scores, descriptions,
                ForumId, toDeleteAttachIds
                );

        }

        private void CheckUpdatePermission()
        {
            if (IfModerators())
            {
                CommFun.CheckModeratorPanelCommonPermission(_operatingUserOrOperator, ForumId);
            }
            else if (IfAdmin())
            {
                CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            }
            else if (!IfOwnTopicAndNoReply())
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        public void Delete()
        {
            CheckRestoreOrDeletePermission();
            base.Delete(_operatingUserOrOperator);
        }

        public void DeleteWithNoPermissionCheck()
        {
            base.Delete(_operatingUserOrOperator);
        }

        public void DeleteTopicsOfForum()
        {
            CheckDeleteTopicsOfForumPermission();
            base.Delete(_operatingUserOrOperator);
        }

        //private void CheckDeletePermission()
        //{
        //    if (_operatingUserOrOperator == null)
        //    {
        //        ExceptionHelper.ThrowOperatorNotLoginException();
        //    }
        //    else if (!IfAdmin() && !IfModerators())
        //    {
        //        ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        //    }
        //}

        public override void Close()
        {
            CheckClosePermission();
            base.Close();
        }

        private void CheckClosePermission()
        {
            //if (_operatingUserOrOperator == null)
            //{
            //    ExceptionHelper.ThrowOperatorNotLoginException();
            //}
            //else if (!IfAdmin() && !IfModerators())
            //{
            //    ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            //}
            //topic deleted
            if (this.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(_topicId);
            //forum deleted
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, ForumId, _operatingUserOrOperator);
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            if (CommFun.IfModeratorInUI(_conn, _transaction, forum.ForumId, _operatingUserOrOperator)
                || CommFun.IfAdminInUI(_operatingUserOrOperator)
                || (_postUserOrOperatorId == _operatingUserOrOperator.Id))
                return;
            else
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        }

        public override void Reopen()
        {
            CheckReopenPermission();
            base.Reopen();
        }

        private void CheckReopenPermission()
        {
            //if (_operatingUserOrOperator == null)
            //{
            //    ExceptionHelper.ThrowOperatorNotLoginException();
            //}
            //else if (!IfAdmin() && !IfModerators())
            //{
            //    ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            //}
            //topic deleted
            if (this.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(_topicId);
            //forum deleted
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, ForumId, _operatingUserOrOperator);
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            if (CommFun.IfModeratorInUI(_conn, _transaction, forum.ForumId, _operatingUserOrOperator)
                || CommFun.IfAdminInUI(_operatingUserOrOperator)
                || (_postUserOrOperatorId == _operatingUserOrOperator.Id))
                return;
            else
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        }

        public void Move(int forumId)
        {
            CheckMovePermission(forumId);
            base.Move(forumId, _operatingUserOrOperator);
        }

        private void CheckMovePermission(int forumId)
        {
            CommFun.CheckAdminPanelOrModeratorPanelCommomPermission(_operatingUserOrOperator, ForumId);

            //topic deleted
            if (this.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(_topicId);
            //forum now deleted
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction,
                ForumId, _operatingUserOrOperator);
            //forum move to deleted
            ForumWithPermissionCheck forumMoveTo = new ForumWithPermissionCheck(_conn, _transaction,
                forumId, _operatingUserOrOperator);

            //if (!CommFun.IfAdminInUI(_operatingUserOrOperator) &&
            //    !CommFun.IfModeratorInUI(_conn, _transaction, ForumId, _operatingUserOrOperator))
            //    ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        }

        public void MoveTopic(int forumId)
        {
            CheckMoveTopicPermission(forumId);
            base.Move(forumId, _operatingUserOrOperator);
        }

        private void CheckMoveTopicPermission(int forumId)
        {
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);

            //topic deleted
            if (this.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(_topicId);
            //forum now deleted
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, ForumId, _operatingUserOrOperator);
            //forum move to deleted
            ForumWithPermissionCheck forumMoveTo = new ForumWithPermissionCheck(_conn, _transaction, forumId, _operatingUserOrOperator);

            if (!CommFun.IfAdminInUI(_operatingUserOrOperator) &&
                !CommFun.IfModeratorInUI(_conn, _transaction, ForumId, _operatingUserOrOperator))
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        }

        public void SetSticky()
        {
            CheckStickyPermission();
            base.SetSticky(_operatingUserOrOperator);
        }

        private void CheckStickyPermission()
        {
            //if (_operatingUserOrOperator == null)
            //{
            //    ExceptionHelper.ThrowOperatorNotLoginException();
            //}
            //else if (!IfAdmin() && !IfModerators())
            //{
            //    ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            //}
            //topic deleted
            if (this.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(_topicId);
            //forum deleted
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, ForumId, _operatingUserOrOperator);
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            if (CommFun.IfModeratorInUI(_conn, _transaction, forum.ForumId, _operatingUserOrOperator)
                || CommFun.IfAdminInUI(_operatingUserOrOperator))
                return;
            else
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        }

        public override void UnSticky()
        {
            CheckUnStickyPermission();
            base.UnSticky();
        }

        private void CheckUnStickyPermission()
        {
            //if (_operatingUserOrOperator == null)
            //{
            //    ExceptionHelper.ThrowOperatorNotLoginException();
            //}
            //else if (!IfAdmin() && !IfModerators())
            //{
            //    ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            //}
            if (this.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(_topicId);
            //forum deleted
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, ForumId, _operatingUserOrOperator);
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            if (CommFun.IfModeratorInUI(_conn, _transaction, forum.ForumId, _operatingUserOrOperator)
                || CommFun.IfAdminInUI(_operatingUserOrOperator))
                return;
            else
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        }

        public override void MarkedAsAnswer()
        {
            CheckMarkedAndUnmarkAsAnswerPermission();
            base.MarkedAsAnswer();
        }
        public override void UnMarkedAsAnswer()
        {
            CheckMarkedAndUnmarkAsAnswerPermission();
            base.UnMarkedAsAnswer();
        }
        public void CheckMarkedAndUnmarkAsAnswerPermission()
        {
            if (_operatingUserOrOperator == null)
            {
                ExceptionHelper.ThrowUserNotLoginException();
            }
            else if (!IfAdmin() && !IfModerators() && (IfForumLockedOrHidden() || base.IfClosed || !IfOwnTopic()))
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        public override void SaveDraft(string subject, string content, UserOrOperator operatingOperator,
            DateTime createTime, int[] attachIds, int[] scores, string[] descriptions, int[] toDeleteAttachIds)
        {
            bool ifModerator = CommFun.IfModeratorInUI(_conn, _transaction, ForumId, operatingOperator);
            CheckSaveDraftPermission();
            PostsOfUserOrOperatorWithPermissionCheck postsOfUser = new PostsOfUserOrOperatorWithPermissionCheck(_conn, _transaction,
                _operatingUserOrOperator, _operatingUserOrOperator.Id);

            CommFun.CommonPostOrTopicNewOrEditPermissionWithNoPostModerationCheck(postsOfUser, ref content, ForumId, _operatingUserOrOperator, ifModerator);

            base.SaveDraft(subject, content, operatingOperator, createTime, attachIds, scores, descriptions, toDeleteAttachIds);
        }

        private void CheckSaveDraftPermission()
        {
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);

            if (this.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(TopicId);
            if (!(_operatingUserOrOperator as OperatorWithPermissionCheck).IfAdmin && this.IfClosed)
                ExceptionHelper.ThrowTopicIsClosedException(TopicId);
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, ForumId, _operatingUserOrOperator);
            //operator user have permission
            if (!(_operatingUserOrOperator is OperatorWithPermissionCheck))
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
            //operator user is not admin,check forum close
            if (!(_operatingUserOrOperator as OperatorWithPermissionCheck).IfAdmin &&
                CommFun.IfForumIsClosedInForumPage(forum))
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        public override DraftWithPermissionCheck AddDraft(string subject, string content, UserOrOperator operatingOperator, DateTime createTime)
        {
            CheckAddDraftPermission();
            return base.AddDraft(subject, content, operatingOperator, createTime);
        }

        private void CheckAddDraftPermission()
        {
            if (_operatingUserOrOperator == null)
            {
                ExceptionHelper.ThrowOperatorNotLoginException();
            }
            else if (!(_operatingUserOrOperator is OperatorWithPermissionCheck))
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        public int PostDraft(bool ifTopic, string subject, DateTime PostTime, string content)
        {
            CheckPostDraftPermission();
            return base.PostDraft(_conn, _transaction, ifTopic, subject, _operatingUserOrOperator.Id, PostTime, content);
        }

        private void CheckPostDraftPermission()
        {
            if (_operatingUserOrOperator == null)
            {
                ExceptionHelper.ThrowOperatorNotLoginException();
            }
            else if (!(_operatingUserOrOperator is OperatorWithPermissionCheck))
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        private void CheckGetDraftPermission()
        {
            if (_operatingUserOrOperator == null)
            {
                ExceptionHelper.ThrowOperatorNotLoginException();
            }
            else if (!(_operatingUserOrOperator is OperatorWithPermissionCheck))
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        public PostWithPermissionCheck GetAnswer()
        {
            return base.GetAnswer(_operatingUserOrOperator);
        }

        public void IncreaseNumberOfRepliesByOne(bool ifTopic)
        {
            CheckIncreaseNumberOfRepliesByOnePermission();
            base.IncreaseNumberOfRepliesByOne(ifTopic, _operatingUserOrOperator);
        }

        private void CheckIncreaseNumberOfRepliesByOnePermission()
        {
            if (_operatingUserOrOperator == null)
            {
                ExceptionHelper.ThrowUserNotLoginException();
            }
            else if (!IfAdmin() && !IfModerators() && (IfForumLockedOrHidden() || base.IfClosed))
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        public void IncreaseNumberOfHitsByOne()
        {
            base.IncreaseNumberOfHitsByOne(_operatingUserOrOperator);
        }

        public void DecreaseNumberOfRepliesByOne()
        {
            CheckDecreaseNumberOfRepliesByOnePermission();
            base.DecreaseNumberOfRepliesByOne(_operatingUserOrOperator);
        }

        private void CheckDecreaseNumberOfRepliesByOnePermission()
        {
            if (_operatingUserOrOperator == null)
            {
                ExceptionHelper.ThrowUserNotLoginException();
            }
            else if (!IfAdmin() && !IfModerators())
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        public void UpdateLastPostInfo(int lastPostUserOrOperatorId, int lastPostId, DateTime lastPostTime, string subject)
        {
            base.UpdateLastPostInfo(lastPostUserOrOperatorId, lastPostId, lastPostTime, subject, _operatingUserOrOperator);
        }

        public PostsOfTopicWithPermissionCheck GetPosts()
        {
            return base.GetPosts(_operatingUserOrOperator);
        }

        public override void Read()
        {
            base.Read();
        }

        public override void UpdateParticipatorIds(int newParticipatorId)
        {
            base.UpdateParticipatorIds(newParticipatorId);
        }

        public void DeleteDraft()
        {
            CheckDeleteDraftPermission();
            if (this.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(TopicId);
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction,
                ForumId, _operatingUserOrOperator);
            if (!this.IfHasDraft)
                ExceptionHelper.ThrowDraftNotExistException(_topicId);

            base.DeleteDraft(this._operatingUserOrOperator);
        }

        //public override int GetPostIndex(int postId)
        //{
        //    return base.GetPostIndex(postId);
        //}

        private void CheckDeleteDraftPermission()
        {
            if (_operatingUserOrOperator == null)
            {
                ExceptionHelper.ThrowOperatorNotLoginException();
            }
            else if (!(_operatingUserOrOperator is OperatorWithPermissionCheck))
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        private bool IfAdmin()
        {
            //bool ifAdmin = false;
            //if (_operatingUserOrOperator != null)
            //{
            //    if (CommFun.IfOperator(_operatingUserOrOperator))
            //    {
            //        if (((OperatorWithPermissionCheck)_operatingUserOrOperator).IfAdmin == true)
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
            //ModeratorsWithPermisisonCheck moderators = new ModeratorsWithPermisisonCheck(this._conn, this._transaction, ForumId, _operatingUserOrOperator);
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
            return CommFun.IfModeratorInUI(_conn, _transaction, ForumId, _operatingUserOrOperator);

        }
        private bool IfOwnTopic()
        {
            bool ifOwnTopic = false;
            if (_operatingUserOrOperator != null)
            {
                if (this._operatingUserOrOperator.Id == PostUserOrOperatorId)
                {
                    ifOwnTopic = true;
                }
            }
            return ifOwnTopic;
        }

        private bool IfOwnTopicAndNoReply()
        {
            if (this._operatingUserOrOperator.Id == PostUserOrOperatorId && base.NumberOfReplies == 0)
            {
                return true;
            }
            else
                return false;
        }

        private bool IfForumLockedOrHidden()
        {
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(base._conn, base._transaction, base.ForumId, _operatingUserOrOperator);

            bool ifForumLockedOrHidden = false;
            if (forum.Status == EnumForumStatus.Lock || forum.Status == EnumForumStatus.Hide)
            {
                ifForumLockedOrHidden = true;
            }
            return ifForumLockedOrHidden;
        }

        /*----------------2.0--------------------*/
        public void LogicDelete()
        {
            CheckPermission();
            base.LogicDelete(_operatingUserOrOperator);
        }

        public void Restore()
        {
            CheckRestoreOrDeletePermission();
            base.Restore(_operatingUserOrOperator);
        }

        private void CheckRestoreOrDeletePermission()
        {
            //topic must deleted
            if (!this.IfDeleted)
            {
                ExceptionHelper.ThrowForumTopicNotInRecycleBinException(TopicId);
            }
            //forum deleted
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, ForumId, _operatingUserOrOperator);

            if (IfModerators())
            {
                CommFun.CheckModeratorPanelCommonPermission(_operatingUserOrOperator, ForumId);
            }
            else
            {
                if (IfAdmin())
                {
                    CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
                }
            }
        }

        private void CheckDeleteTopicsOfForumPermission()
        {
            //forum deleted
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, ForumId, _operatingUserOrOperator);

            if (IfModerators())
            {
                CommFun.CheckModeratorPanelCommonPermission(_operatingUserOrOperator, ForumId);
            }
            else
            {
                if (IfAdmin())
                {
                    CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
                }
            }
        }

        public void LogicDeleteTopicAndFirstPost()
        {
            CheckLogicDeleteTopicAndFirstPostPermission();
            base.LogicDeleteTopicAndFirstPost(_operatingUserOrOperator);
        }

        private void CheckLogicDeleteTopicAndFirstPostPermission()
        {
            CommFun.CheckAdminPanelOrModeratorPanelCommomPermission(_operatingUserOrOperator, ForumId);
            //topic deleted
            if (this.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(_topicId);
            //forum deleted
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, ForumId, _operatingUserOrOperator);
        }

        private void CheckPermission()
        {
            //topic deleted
            if (this.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(_topicId);
            //forum deleted
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, ForumId, _operatingUserOrOperator);

            if (IfModerators())
            {
                CommFun.CheckModeratorPanelCommonPermission(_operatingUserOrOperator, ForumId);
            }
            else
            {
                if (IfAdmin())
                {
                    CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
                }
            }
        }
        public void Pay()
        { }

        public int GetCountOfFavoriteTimes()
        {
            return base.GetCountOfFavoriteTimes(_operatingUserOrOperator);
        }

        public int GetCountOfSubscribeTimes()
        {
            return base.GetCountOfSubscribeTimes(_operatingUserOrOperator);
        }

        public void Featured(int forumId)
        {
            CheckFeauturedOrUnFeautured(forumId);
            base.Featured(_operatingUserOrOperator);
        }

        public void UnFeatured(int forumId)
        {
            CheckFeauturedOrUnFeautured(forumId);
            base.UnFeatured();
        }

        public void AcceptModeration()
        {
            base.AcceptModeration(_operatingUserOrOperator);
        }

        private void CheckFeauturedOrUnFeautured(int forumId)
        {
            //forum deleted
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, forumId, _operatingUserOrOperator);

            //topic deleted
            if (this.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(_topicId);

            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            if (!CommFun.IfModeratorInUI(_conn, _transaction, forum.ForumId, _operatingUserOrOperator) && !CommFun.IfAdminInUI(_operatingUserOrOperator))
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }
    }
}


