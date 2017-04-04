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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Database;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class ForumBase
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        protected int _forumId;
        protected int _categoryId;
        protected string _name;
        protected Int16 _status;
        protected string _description;
        protected int _orderId;
        #endregion

        #region properties
        public int ForumId
        {
            get { return this._forumId; }
        }
        public int CategoryId
        {
            get { return this._categoryId; }
        }
        public int OrderId
        {
            get { return this._orderId; }
        }
        public string Name
        {
            get { return this._name; }
        }
        public EnumForumStatus Status
        {
            get { return (EnumForumStatus)this._status; }
        }
        public string Description
        {
            get { return this._description; }
        }
        #endregion

        public ForumBase(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            _conn = conn;
            _transaction = transaction;
        }
    }
}
