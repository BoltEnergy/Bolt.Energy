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
using Com.Comm100.Framework.FieldLength;
using System.Web;
using Com.Comm100.Framework.ASPNETState;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class TopicBase
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        protected int _topicId;
        protected string _subject;
        protected int _postUserOrOperatorId;
        protected string _postUserOrOperatorName;
        protected bool _postUserOrOperatorIfDeleted;
        protected DateTime _postTime;
        protected int _lastPostId;
        protected DateTime _lastPostTime;
        protected int _lastPostUserOrOperatorId;
        protected string _lastPostUserOrOperatorName;
        protected bool _lastPostUserOrOperatorIfDeleted;
        protected int _numberOfReplies;
        protected int _numberOfHits;
        
        #endregion

        #region properties
        public int TopicId
        {
            get { return this._topicId; }
        }
        public string Subject
        {
            get { return this._subject; }
        }
        public int PostUserOrOperatorId
        {
            get { return this._postUserOrOperatorId; }
        }
        public string PostUserOrOperatorName
        {
            get { return this._postUserOrOperatorName; }
        }
        public bool PostUserOrOperatorIfDeleted
        {
            get { return this._postUserOrOperatorIfDeleted; }
        }
        public DateTime PostTime
        {
            get { return this._postTime; }
        }
        public int LastPostId
        {
            get { return this._lastPostId; }
        }
        public DateTime LastPostTime
        {
            get { return this._lastPostTime; }
        }
        public int LastPostUserOrOperatorId
        {
            get { return this._lastPostUserOrOperatorId; }
        }
        public string LastPostUserOrOperatorName
        {
            get { return this._lastPostUserOrOperatorName; }
        }
        public bool LastPostUserOrOperatorIfDeleted
        {
            get { return this._lastPostUserOrOperatorIfDeleted; }
        }
        public int NumberOfReplies
        {
            get { return this._numberOfReplies; }
        }
        public int NumberOfHits
        {
            get { return this._numberOfHits; }
        }
        
        #endregion

        public TopicBase(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            _conn = conn;
            _transaction = transaction;
        }
        public TopicBase(int id, string subject, DateTime postTime,
            int postUserOrOperatorId, string postUserOrOperatorName, bool postUserOrOperatorIfDeleted,
            int lastPostId, DateTime lastPostTime, int lastPostUserOrOperatorId,
            string lastPostUserOrOperatorName, bool lastPostUserOrOperatorIfDeleted,
            int numberOfReplies, int numberOfHits)
        {
            _topicId = id;
            _subject = subject;
            _postUserOrOperatorId = postUserOrOperatorId;
            _postTime = postTime;
            _postUserOrOperatorName = postUserOrOperatorName;
            _postUserOrOperatorIfDeleted = postUserOrOperatorIfDeleted;
            _lastPostId = lastPostId;
            _lastPostTime = lastPostTime;
            _lastPostUserOrOperatorId = lastPostUserOrOperatorId;
            _lastPostUserOrOperatorName = lastPostUserOrOperatorName;
            _lastPostUserOrOperatorIfDeleted = lastPostUserOrOperatorIfDeleted;

            _numberOfReplies = numberOfReplies;
            _numberOfHits = numberOfHits;

        }

        protected static void CheckFieldsLength(string subject, string content)
        {
            if (subject.Length == 0)
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("Subject");
            else
            {
                if (subject.Length > ForumDBFieldLength.Topic_subjectFieldLength)
                    ExceptionHelper.ThrowSystemFieldLengthExceededException("Subject", ForumDBFieldLength.Topic_subjectFieldLength);
            }
            //if (content.Length > ForumDBFieldLength.Topic_contentFieldLength)
            //    ExceptionHelper.ThrowSystemFieldLengthExceededException("Content", ForumDBFieldLength.Topic_contentFieldLength);
        }
      
        protected PostsOfTopicWithPermissionCheck GetPosts(UserOrOperator operatingOperator)
        {
            return new PostsOfTopicWithPermissionCheck(this._conn, this._transaction, this._topicId, operatingOperator);
        }
    }
}
