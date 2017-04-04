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
using System.Data.SqlClient;
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.Bussiness
{
    public class TopicsOfForumWithPermissionCheck : TopicsOfForum
    {
        UserOrOperator _operatingUserOrOperator;

        public TopicsOfForumWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction, forumId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public int Add(string subject, DateTime postTime, string content,
            int score, bool ifReplyRequired, bool IfPayScoreRequired,
            bool ifContainsPoll, bool ifMultipleChoice, int maxChoices, bool ifSetDeadline,
            DateTime startDate, DateTime endDate, string[] options, int[] attachIds, int[] scores, string[] descriptions)
        {
            bool ifModerator = CommFun.IfModeratorInUI(_conn, _transaction, ForumId, _operatingUserOrOperator);
            CommFun.CheckUserOrOperator(_operatingUserOrOperator);
            CommFun.CheckIfUserOrOperatorBanned(_operatingUserOrOperator,ForumId);
            checkAddPermission(ForumId);

            /*Post Moderation Not Required*/
            bool ifNeedModeration = !CommFun.UserPermissionCache().IfPostModerationNotRequiredPermission(ForumId, _operatingUserOrOperator, ifModerator);
            return base.Add(subject, postTime, content, _operatingUserOrOperator,
                score, ifReplyRequired, IfPayScoreRequired, ifContainsPoll, ifMultipleChoice, maxChoices, ifSetDeadline,
                startDate, endDate, options, attachIds, scores, descriptions, ifNeedModeration);
        }
        private void checkAddPermission(int forumId)
        {
            if (CommFun.IfGuest())
            {
                ExceptionHelper.ThrowUserNotLoginException();
            }
            else if (!IfAdmin()//!_operatingUserOrOperator.IfForumAdmin
                && !IfModerators()//!CommFun.UserPermissionCache().IfModerator(forumId)
                && CommFun.IfForumIsClosedInForumPage(_conn,_transaction,_operatingUserOrOperator,ForumId))
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }
        private void checkAddPermission()
        {
            if (_operatingUserOrOperator == null)
            {
                ExceptionHelper.ThrowUserNotLoginException();
            }
            else if (!IfAdmin() && !IfModerators() && IfForumLockedOrHidden())
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        public void Delete(int topicId)
        {
            CommFun.CheckUserOrOperator(_operatingUserOrOperator);
            //CommFun.CheckIfUserOrOperatorBanned(_operatingUserOrOperator, forumId);
            CheckDeletePermission();
            base.Delete(topicId, this._operatingUserOrOperator);
        }

        private void CheckDeletePermission()
        {
            if (_operatingUserOrOperator == null)
            {
                ExceptionHelper.ThrowOperatorNotLoginException();
            }
            else if (!IfAdmin() && !IfModerators())
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        public void DeleteAllTopics()
        {
            CommFun.CheckUserOrOperator(_operatingUserOrOperator);
            //CommFun.CheckIfUserOrOperatorBanned(_operatingUserOrOperator, forumId);
            CheckDeleteAllTopicsPermission();
            base.DeleteAllTopics(this._operatingUserOrOperator);
        }

        private void CheckDeleteAllTopicsPermission()
        {
            if (_operatingUserOrOperator == null)
            {
                ExceptionHelper.ThrowOperatorNotLoginException();
            }
            else if (!IfAdmin())
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        private void CheckMovePermission()
        {
            if (_operatingUserOrOperator == null)
            {
                ExceptionHelper.ThrowOperatorNotLoginException();
            }
            else if (!IfAdmin() && !IfModerators())
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        public TopicWithPermissionCheck[] GetAllTopics()
        {
            return base.GetAllTopics(this._operatingUserOrOperator);
        }

        public TopicWithPermissionCheck[] GetTopicsByPaging(
            int pageIndex, int pageSize, int forumId,out int count)
        {
            
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
            return base.GetTopicsByPaging(pageIndex, pageSize, this._operatingUserOrOperator,out count);
        }

        public Int32 GetCountOfTopicsByForumId()
        {
            return base.GetCountOfTopicsByForumId();
        }

        public Int32 GetCountOfFeaturedTopicsByForumId()
        {
            return base.GetCountOfTopicsByForumId();
        }

        public TopicWithPermissionCheck[] GetTopicsByPagingWithoutWaitingForModeration(
            int pageIndex, int pageSize, int forumId, out int count)
        {
            return base.GetTopicsByPagingWithoutWaitingForModeration(pageIndex, pageSize, this._operatingUserOrOperator, out count);
        }
        /* update by techtier on 3/1/2017  for adding sorting by most popular */
        public TopicWithPermissionCheck[] GetTopicsByPagingWithoutWaitingForModerationByManualSort(
           int pageIndex, int pageSize, int forumId,string sortKeyword, out int count)
        {
            return base.GetTopicsByPagingWithoutWaitingForModerationByManualSort(pageIndex, pageSize, this._operatingUserOrOperator, sortKeyword, out count);
        }

        public TopicWithPermissionCheck[] GetTopicsByPagingWithoutWaitingForModerationByManualMyPost(
           int pageIndex, int pageSize, int forumId, int operatingUserOrOperatorId, out int count)
        {
            return base.GetTopicsByPagingWithoutWaitingForModerationByManualMyPost(pageIndex, pageSize, this._operatingUserOrOperator, operatingUserOrOperatorId, out count);
        }

        //techtier for search
        public TopicWithPermissionCheck[] GetTopicsByPagingWithoutWaitingForModerationByManualSearch(
         int pageIndex, int pageSize, int forumId, string searchKeyword, out int count)
        {
            return base.GetTopicsByPagingWithoutWaitingForModerationByManualSearch(pageIndex, pageSize, this._operatingUserOrOperator, searchKeyword, out count);
        }
        
        public TopicWithPermissionCheck[] GetFeaturedTopicsByPaging(int pageIndex, int pageSize, out int count,
            int forumId)
        {
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
            return base.GetFeaturedTopicsByPaging(pageIndex, pageSize, this._operatingUserOrOperator, out count);
        }

        public TopicWithPermissionCheck[] GetFeaturedTopicsByPagingWithoutWaitingForModeration(int pageIndex, int pageSize, out int count,
            int forumId)
        {
            return base.GetFeaturedTopicsByPagingWithoutWaitingForModeration(pageIndex, pageSize, this._operatingUserOrOperator, out count);
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

        private bool IfForumLockedOrHidden()
        {
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(base._conn, base._transaction, base.ForumId, _operatingUserOrOperator);
            return (forum.Status == EnumForumStatus.Lock || forum.Status == EnumForumStatus.Hide) ? true : false;
        }

        /*------------------------2.0---------------------------*/
        public override TopicWithPermissionCheck[] GetNotDeletedTopicsByQueryAndPaging(
            UserOrOperator operatingUserOrOperator, string keywords, string name,
            DateTime startDate, DateTime endDate, int pageIndex, int pageSize,
            string orderField, string orderMethod, out int countOfTopics
            )
        {
            return base.GetNotDeletedTopicsByQueryAndPaging(
                operatingUserOrOperator, keywords, name, startDate, endDate, pageIndex, pageSize,
                orderField, orderMethod, out countOfTopics);
        }
    }
}
