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
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class MessageBase
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        protected int _id; 
        protected string _subject;
        protected string _message;
        protected DateTime _createDate;
        protected int _fromUserOrOperatorId; 
        protected string _fromUserOrOperatorDisplayName;
        #endregion

        #region properties
        public int Id
        {
            get { return this._id; }
        }
        public string Subject
        {
            get { return this._subject; }
        }
        public string Message
        {
            get { return this._message; }
        }
        public DateTime CreateDate
        {
            get { return this._createDate; }
        }
        public int FromUserOrOperatorId
        {
            get { return this._fromUserOrOperatorId; }
        }
        public string FromUserOrOperatorDisplayName
        {
            get { return this._fromUserOrOperatorDisplayName; }
        }
        #endregion

        public MessageBase(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            this._conn = conn;
            this._transaction = transaction;
        }
        public abstract void Delete();
        
    }
}
