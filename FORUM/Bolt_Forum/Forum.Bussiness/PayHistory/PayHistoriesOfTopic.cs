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
    public class PayHistoriesOfTopic : PayHistoriesBase
    {
        private int _topicId;
        public int TopicId
        {
            get { return this._topicId; }
        }

        public PayHistoriesOfTopic(SqlConnectionWithSiteId conn, SqlTransaction transaction,int topicId)
            :base(conn, transaction)
        {
            _topicId = topicId;
        }

        public void Add(UserOrOperator user,int userId, int score, DateTime date)
        {
            //close score 
            ForumFeature feature = new ForumFeatureWithPermissionCheck(_conn, _transaction, user);
            if (!feature.IfEnableScore)
                return;
            //have paid
            if (IfPaid(userId))
                return;
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn,_transaction,_topicId,user);
            if (topic.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(topic.TopicId);
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, topic.ForumId, user);
            if (!CommFun.IfAdminInUI(user) && !CommFun.IfModeratorInUI(_conn, _transaction, forum.ForumId, user) &&
                CommFun.IfForumIsClosedInForumPage(forum))
            {
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(user.DisplayName);
            }
            
            CommFun.CheckCommonPermissionInUI(user);
            //user's score not enough
            if (user.Score < score)
            {
                ExceptionHelper.ThrowForumUserOrOperatorScoreIsNotEnoughException();
            }
            user.DecreaseScore(score);
            UserOrOperator Author = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction,
                null, topic.PostUserOrOperatorId);
            Author.IncreaseScore(score);
            PayHistoryAccess.PayHistoryAdd(_conn, _transaction, EnumPayType.Topic, 
                userId, _topicId, score,date);
        }

        public bool IfPaid(int userId)
        {
            return PayHistoryAccess.IfUserPaid(_conn, _transaction, EnumPayType.Topic, userId, _topicId);
        }
    }
}
