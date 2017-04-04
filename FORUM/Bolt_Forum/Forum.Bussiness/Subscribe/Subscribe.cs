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
    public abstract class Subscribe : TopicBase
    {
        private int _forumId;
        private int _userOrOperatorId;
        private bool _ifMarkedAsAnswer;
        private bool _ifClosed;
        private DateTime _subscribeDate;
        private int[] _participatorIds;
        public int ForumId 
        {
            get { return this._forumId;}
        }
        public int UserOrOperatorId
        {
            get { return this._userOrOperatorId; }
        }
        public bool IfMarkedAsAnswer
        {
            get { return this._ifMarkedAsAnswer; }
        }
        public bool IfClosed
        {
            get { return this._ifClosed; }
        }
        public DateTime SubscribeDate
        {
            get { return this._subscribeDate; }
        }
        public int[] ParticipatorIds
        {
            get
            {
                return this._participatorIds;
            }
        }
        public abstract bool IfParticipant
        {
            get;
        }

        public Subscribe(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, int userOrOperatorId)
            : base(conn, transaction)
        {
            DataTable table = DataAccess.SubscribeAccess.GetSubscribeByTopicIdAndUserId(conn, transaction, topicId, userOrOperatorId);
            if (table.Rows.Count == 0)
            {               
                ExceptionHelper.ThrowForumSubscribeNotExistException(topicId);
            }
            else
            {
                _forumId = Convert.ToInt32(table.Rows[0]["ForumId"]);
                _userOrOperatorId = userOrOperatorId;
                _ifMarkedAsAnswer = Convert.ToBoolean(table.Rows[0]["IfMarkedAsAnswer"]);
                _ifClosed = Convert.ToBoolean(table.Rows[0]["IfClosed"]);
                string[] tempIds = Convert.ToString(table.Rows[0]["ParticipatorIds"]).Split(',');
                List<int> ids = new List<int>();
                for (int i = 0; i < tempIds.Length; i++)
                {
                    if (tempIds[i] != "")
                    {
                        ids.Add(Convert.ToInt32(tempIds[i]));
                    }
                }
                _participatorIds = ids.ToArray<int>();
                _subscribeDate = Convert.ToDateTime(table.Rows[0]["SubscribeDate"]);

            }

        }
         public Subscribe(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, string subject, DateTime postTime,
            int postUserOrOperatorId, string postUserOrOperatorName, bool postUserOrOperatorIfDeleted,  int lastPostId, 
            DateTime lastPostTime, int lastPostUserOrOperatorId, string lastPostUserOrOperatorName, bool lastPostUserOrOperatorIfDeleted,
            int numberOfReplies, int numberOfHits, int forumId, int userOrOperatorId, 
            bool ifMarkedAsAnswer, bool ifClosed, int[] participatorIds,DateTime subscribeDate)
            : base(topicId, subject, postTime, postUserOrOperatorId, postUserOrOperatorName, postUserOrOperatorIfDeleted,
           lastPostId, lastPostTime, lastPostUserOrOperatorId,lastPostUserOrOperatorName, lastPostUserOrOperatorIfDeleted,
             numberOfReplies, numberOfHits)
        {
            this._conn = conn;
            this._transaction = transaction;
            this._forumId = forumId;
            this._userOrOperatorId = userOrOperatorId;           
            this._ifMarkedAsAnswer = ifMarkedAsAnswer;
            this._ifClosed = ifClosed;
            this._participatorIds = participatorIds;                
            this._subscribeDate = subscribeDate;
        }     
        public void Delete(int topicId)
        {
            CheckIfEnableSubscribe();
            DataAccess.SubscribeAccess.DeleteSubscribe(this._conn , this._transaction , topicId, this.UserOrOperatorId);
        }
        public static void Add(SqlConnectionWithSiteId conn,SqlTransaction transaction,int userId,int topicId)
        {          
            SubscribeAccess.AddSubscribe(conn, transaction, topicId, userId);
        }
        #region private Function CheckIfEnableSubscribe()
        private void CheckIfEnableSubscribe()
        {
            ForumFeatureWithPermissionCheck forumfeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, null);
            if (!forumfeature.IfEnableSubscribe)
            {
                ExceptionHelper.ThrowForumSettingsCloseSubscribeFunction();
            }
        }

        #endregion
    }
}
