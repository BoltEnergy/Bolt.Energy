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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.FieldLength;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class Abuse
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        private int _id;
        private int _postId;
        private string _postSubject;
        private int _postUserOrOperatortId;
        private string _postUserOrOperatorName;
        private int _abuseUserOrOperatorId;
        private string _abuseUserOrOperatorName;
        //private string _forumPathOfPost;
        private EnumAbuseStatus _status;
        private DateTime _date;
        private string _note;
        #endregion

        #region properties
        public int Id
        {
            get { return this._id; }
        }
        public int PostId
        {
            get { return this._postId; }
        }
        public string PostSubject
        {
            get { return this._postSubject; }
        }
        public int PostUserOrOperatorId
        {
            get { return this._postUserOrOperatortId; }
        }
        public string PostUserOrOperatorName
        {
            get { return this._postUserOrOperatorName; }
        }
        public int AbuseUserOrOperatorId
        {
            get { return this._abuseUserOrOperatorId; }
        }
        public string AbuseUserOrOperatorName
        {
            get { return this._abuseUserOrOperatorName; }
        }
        //public string ForumPathOfPost
        //{
        //    get { return this._forumPathOfPost; }
        //}
        public EnumAbuseStatus Status
        {
            get { return (EnumAbuseStatus)this._status; }
        }
        public DateTime Date
        {
            get { return this._date; }
        }
        public string Note
        {
            get { return this._note; }
        }
        #endregion

        public Abuse(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id)
        {
            _conn = conn;
            _transaction = transaction;
            _id = id;
            DataTable dt = AbuseAccess.GetAbuseById(_conn, _transaction, id);
            if (dt.Rows.Count <= 0)
                ExceptionHelper.ThrowForumAbuseNotExistException(id);
            else
            {
                _postId = Convert.ToInt32(dt.Rows[0]["PostId"]);
                _postSubject = Convert.ToString(dt.Rows[0]["Subject"]);
                _postUserOrOperatortId = Convert.ToInt32(dt.Rows[0]["PostUserOrOperatorId"]);
                _postUserOrOperatorName = Convert.ToString(dt.Rows[0]["PostUserOrOperatorName"]);
                _abuseUserOrOperatorId = Convert.ToInt32(dt.Rows[0]["UserOrOperatorId"]);
                _abuseUserOrOperatorName = Convert.ToString(dt.Rows[0]["AbuseUserOrOperatorName"]);
                //_forumPathOfPost = Convert.ToString(dt.Rows[0]["ForumPathOfPost"]); 
                _status = (EnumAbuseStatus)Enum.Parse(typeof(EnumAbuseStatus), Convert.ToString((dt.Rows[0]["Status"])));
                _date = Convert.ToDateTime(dt.Rows[0]["Date"]);
                _note = Convert.ToString(dt.Rows[0]["Note"]);
            }
        }

        public Abuse(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int id, int postId,string postSubject, int postUserOrOperatortId,
            string postUserOrOperatorName, int abuseUserOrOperatorId, string abuseUserOrOperatorName
            , EnumAbuseStatus status, DateTime date, string note)
        {
            _conn = conn;
            _transaction = transaction;
            _id = id;
            _postId = postId;
            _postSubject = postSubject;
            _postUserOrOperatortId = postUserOrOperatortId;
            _postUserOrOperatorName = postUserOrOperatorName;
            _abuseUserOrOperatorId = abuseUserOrOperatorId;
            _abuseUserOrOperatorName = abuseUserOrOperatorName;
            //_forumPathOfPost = Convert.ToString(dt.Rows[0]["ForumPathOfPost"]); 
            _status = status;
            _date = date;
            _note = note;
        }

        private static void CheckFieldsLength(string note)
        {
            if (note.Length > ForumDBFieldLength.Abuse_noteFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Note", ForumDBFieldLength.Abuse_noteFieldLength);
        }

        public static int Add(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int postId, int abuseUserOrOperatorId,DateTime date, string note)
        {
            CheckFieldsLength(note);
            return AbuseAccess.AddAbuse(conn, transaction, postId, abuseUserOrOperatorId, note, date);
        }

        public virtual void Approve()
        {
            if (this._status == EnumAbuseStatus.Approved) return;
            AbuseAccess.UpdateAbuseStatus(_conn, _transaction, _id, EnumAbuseStatus.Approved);
            PostAccess.SetPostSpam(_conn, _transaction, _postId);
        }

        public virtual void Refuse()
        {
            if (this._status == EnumAbuseStatus.Refused) return;
            AbuseAccess.UpdateAbuseStatus(_conn, _transaction, _id, EnumAbuseStatus.Refused);
        }
    }
}
