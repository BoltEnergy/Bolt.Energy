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
    public abstract  class Favorite : TopicBase
    {
        private int _userOrOperatorId;
        private int _forumId;
        //  private string _forumPath;   //_forumPath   is given value in UI. so it is quit!
        private bool _ifMarkedAsAnswer;
        private bool _ifClosed;
        private int[] _participatorIds;
        private bool _ifParticipant;
        #region properties
        public int UserOrOpeartorId
        {
            get { return this._userOrOperatorId; }
        }
        public int CurrentForumId
        {
            get { return this._forumId; }
        }
     
        public bool IfMarkedAsAnswer
        {
            get { return this._ifMarkedAsAnswer; }
        }
        public bool IfClosed
        {
            get { return this._ifClosed; }
        }
        public int[] ParticipatorIds 
        {
            get
            {
                return this._participatorIds;
            }
        }
        public bool IfParticipant
        {
            get
            {
                return this._ifParticipant;
            }
        }
        #endregion
        public Favorite(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, int userOrOperatorId)
            : base(conn, transaction)
        {   
            DataTable table = FavoriteAccess.GetFavoritesByTopicIdAndUserId (conn, transaction, topicId, userOrOperatorId);
            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowForumFavoriteNotExistException(topicId);
           
            }
            else
            {
                _userOrOperatorId = userOrOperatorId;
                _forumId = Convert.ToInt32(table.Rows[0]["CurrentForumId"]);
                _topicId = Convert.ToInt32(table.Rows[0]["TopicId"]);
              
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
                _ifParticipant = table.Rows[0]["ParticipatorIds"].ToString().Contains(userOrOperatorId.ToString()) ? true : false;
            }         
        }
        public Favorite(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, string subject, DateTime postTime,
            int postUserOrOperatorId, string postUserOrOperatorName, bool postUserOrOperatorIfDeleted,  int lastPostId, 
            DateTime lastPostTime, int lastPostUserOrOperatorId, string lastPostUserOrOperatorName, bool lastPostUserOrOperatorIfDeleted,
            int numberOfReplies, int numberOfHits,  int userOrOperatorId, int forumId,
            bool ifMarkedAsAnswer, bool ifClosed, int[] participatorIds, bool ifParticipant)
            : base(topicId, subject, postTime, postUserOrOperatorId, postUserOrOperatorName, postUserOrOperatorIfDeleted,
           lastPostId, lastPostTime, lastPostUserOrOperatorId,lastPostUserOrOperatorName, lastPostUserOrOperatorIfDeleted,
             numberOfReplies, numberOfHits)
        {
            this._conn = conn;
            this._transaction = transaction;
            this._userOrOperatorId = userOrOperatorId;
            this._forumId = forumId;           
            this._ifMarkedAsAnswer = ifMarkedAsAnswer;
            this._ifClosed = ifClosed;
            this._participatorIds = participatorIds;
            this._ifParticipant = ifParticipant;
        }     

        public virtual void Delete()
        {
            CheckIfEnableFavorite();
            FavoriteAccess.DeleteFavorite(this._conn, this._transaction , this._userOrOperatorId, this._topicId);
         
        }

        public static void Add(SqlConnectionWithSiteId conn,SqlTransaction transaction,int userId,int topicId,int forumId)
        {            
            FavoriteAccess.AddFavorite(conn, transaction, userId, topicId,forumId);
            
        }
    
        #region private Function CheckIfEnableFavorite()
        private void CheckIfEnableFavorite()
        {
            ForumFeatureWithPermissionCheck forumfeature = new ForumFeatureWithPermissionCheck(_conn, _transaction , null);
            if (!forumfeature.IfEnableFavorite)
            {
                ExceptionHelper.ThrowForumSettingsCloseFavoriteFunction(); 
            }
        }

        #endregion
    }
}
