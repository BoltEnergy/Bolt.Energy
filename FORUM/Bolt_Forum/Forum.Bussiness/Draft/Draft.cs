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
using Com.Comm100.Framework.FieldLength;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class Draft
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        private int _draftId;
        private int _topicId;
        private string _subject;
        private string _content;
        private int _createOperatorId;
        private string _createOperatorName;
        private DateTime _createTime;
        private int _lastUpdateOperatorId;
        private string _lastUpdateOperatorName;
        private DateTime _lastUpdateTime;
        
        #endregion

        #region property
        public int DraftId
        {
            get { return this._draftId; }
        }
        public int TopicId
        {
            get { return this._topicId; }
        }
        public string Subject
        {
            get { return this._subject; }
        }
        public string Content
        {
            get { return this._content; }
        }
        public int CreateOperatorId
        {
            get { return this._createOperatorId; }
        }
        public string CreateOperatorName
        {
            get { return _createOperatorName; }
        }
        public DateTime CreateTime
        {
            get { return this._createTime; }
        }
        public int LastUpdateOperatorId
        {
            get { return this._lastUpdateOperatorId; }
        }
        public string LastUpdateOperatorName
        {
            get { return this._lastUpdateOperatorName; }
        }
        public DateTime LastUpdateTime
        {
            get { return this._lastUpdateTime; }
        }
        #endregion

        public Draft(SqlConnectionWithSiteId conn, SqlTransaction transaction, int draftId)
        {
            _conn = conn;
            _transaction = transaction;
            DataTable table = DraftAccess.GetDraftByDraftId(conn, transaction, draftId);

            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowDraftNotExistException(draftId);
            }
            else
            {
                _draftId = draftId;
                _topicId = Convert.ToInt32(table.Rows[0]["TopicId"]);
                _subject = Convert.ToString(table.Rows[0]["Subject"]);
                _content = Convert.ToString(table.Rows[0]["Content"]);
                _createOperatorId = Convert.ToInt32(table.Rows[0]["CreateOperatorId"]);
                _createOperatorName = Convert.ToString(table.Rows[0]["CreateOperatorName"]);
                _createTime = Convert.ToDateTime(table.Rows[0]["CreateTime"]);
                _lastUpdateOperatorId = Convert.ToInt32(table.Rows[0]["LastUpdateOperatorId"]);
                _lastUpdateOperatorName = Convert.ToString(table.Rows[0]["LastUpdateOperatorName"]);
                _lastUpdateTime = Convert.ToDateTime(table.Rows[0]["LastUpdateTime"]);
            }
        }

        public Draft(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId,int nothing)
        {
            _conn = conn;
            _transaction = transaction;
            DataTable table = DraftAccess.GetDraftByTopicId(conn, transaction, topicId);

            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowDraftNotExistInTopicException(topicId);
            }
            else
            {
                _draftId = Convert.ToInt32(table.Rows[0]["Id"]);
                _topicId = Convert.ToInt32(table.Rows[0]["TopicId"]);
                _subject = Convert.ToString(table.Rows[0]["Subject"]);
                _content = Convert.ToString(table.Rows[0]["Content"]);
                _createOperatorId = Convert.ToInt32(table.Rows[0]["CreateOperatorId"]);
                _createOperatorName = Convert.ToString(table.Rows[0]["CreateOperatorName"]);
                _createTime = Convert.ToDateTime(table.Rows[0]["CreateTime"]);
                _lastUpdateOperatorId = Convert.ToInt32(table.Rows[0]["LastUpdateOperatorId"]);
                _lastUpdateOperatorName = Convert.ToString(table.Rows[0]["LastUpdateOperatorName"]);
                _lastUpdateTime = Convert.ToDateTime(table.Rows[0]["LastUpdateTime"]);
            }
        }

        public Draft(SqlConnectionWithSiteId conn, SqlTransaction transaction, int draftId, int topicId, string subject, string content,
            int createOperatorId, string createOperatorName, DateTime createTime, int lastUpdateOperateorId, string lastUpdateOperatorName, DateTime lastUpdateTime)
        {
            _conn = conn;
            _transaction = transaction;
            _draftId = draftId;
            _topicId = topicId;
            _subject = subject;
            _content = content;
            _createOperatorId = createOperatorId;
            _createOperatorName = createOperatorName;
            _createTime = createTime;
            _lastUpdateOperatorId = lastUpdateOperateorId;
            _lastUpdateOperatorName = lastUpdateOperatorName;
            _lastUpdateTime = lastUpdateTime;
        }

        protected virtual void Update(string subject, string content, DateTime updateTime,UserOrOperator operatingOperator)
        {
            CheckFieldsLength(subject, content);
            DraftAccess.UpdateDraft(_conn, _transaction, _draftId, subject, content, operatingOperator.Id, updateTime);
        }

        private static void CheckFieldsLength(string subject, string content)
        {
            if (subject.Length == 0)
            {
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("Subject");
            }
            else
            {
                if (subject.Length > ForumDBFieldLength.Draft_subjectFieldLength)
                { 
                  ExceptionHelper.ThrowSystemFieldLengthExceededException("Subject",ForumDBFieldLength.Draft_subjectFieldLength);  
                }
            }
            if (content.Length > ForumDBFieldLength.Draft_contentFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Content", ForumDBFieldLength.Draft_contentFieldLength);
            }
        }

        public virtual void Delete()
        {
            DraftAccess.DeleteDraftByTopicId(_conn, _transaction, _topicId);
        }

        public static int Add(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, string subject, string content, int createOperatorId, DateTime createTime)
        {
            CheckFieldsLength(subject, content);
            return DraftAccess.AddDraft(conn, transaction, topicId, subject, content, createOperatorId, createTime);
        }

        protected virtual TopicWithPermissionCheck GetTopic(UserOrOperator operatingOperator)
        {
            return new TopicWithPermissionCheck(_conn, _transaction, _topicId, operatingOperator);
        }

    }
}
