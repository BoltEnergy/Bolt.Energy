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
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.Bussiness
{
    public class FavoritesWithPermissionCheck : Favorites
    {
        UserOrOperator _operatingUserOrOperator;

        public FavoritesWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int userOrOperatorId)
            :base(conn, transaction, userOrOperatorId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }


        public void Delete(UserOrOperator operatingOperator,int forumId, int topicId)
        {
            CommFun.CheckCommonPermissionInUI(operatingOperator);
            CheckBasePermission(forumId,topicId);
            base.Delete(operatingOperator, topicId);
        }

        public FavoriteWithPermissionCheck[] GetFavoritesByPaging(out int count, int pageIndex, int pageSize)
        {
            return base.GetFavoritesByPaging(out count, pageIndex, pageSize, _operatingUserOrOperator);
        }

        public void Add(UserOrOperator operatingOperator, int forumId,int topicId)
        {
            CommFun.CheckCommonPermissionInUI(operatingOperator);
            CheckBasePermission(forumId,topicId);
            base.Add(operatingOperator, topicId,forumId);
        }

        private void CheckBasePermission(int forumId,int topicId)
        {
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction,forumId, _operatingUserOrOperator);

            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, topicId, _operatingUserOrOperator);
            if(topic.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(topicId); 
            if (CommFun.IfGuest())
                ExceptionHelper.ThrowUserNotLoginException();
            else if (!IfAdmin()
                    && !CommFun.IfModeratorInUI(_conn,_transaction,forumId,_operatingUserOrOperator)
                    && (CommFun.IfForumIsClosedInForumPage(forum)))
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        }

        private bool IfAdmin()
        {
            return CommFun.IfAdminInUI(_operatingUserOrOperator);
        }

        private bool IfModeratorsOfTopic(TopicWithPermissionCheck topic)
        {
           // TopicWithPermissionCheck topic = new TopicWithPermissionCheck(this._conn, this._transaction, topicId, _operatingUserOrOperator);
            return CommFun.IfModeratorInUI(_conn, _transaction, topic.ForumId, _operatingUserOrOperator);
        }

        private bool IfModeratorsOfTopic(int topicId)
        {
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(this._conn, this._transaction, topicId, _operatingUserOrOperator);
            return CommFun.IfModeratorInUI(_conn, _transaction, topic.ForumId, _operatingUserOrOperator);
        }
    }
}
