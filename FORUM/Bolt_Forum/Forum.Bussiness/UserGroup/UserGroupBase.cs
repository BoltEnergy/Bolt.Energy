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
    public abstract class UserGroupBase
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        protected int _groupId;
        protected string _name;
        protected string _description;
        #endregion

        #region properties
        public int UserGroupId
        {
            get { return this._groupId; }
        }
        public string Name
        {
            get { return this._name; }
        }
        public string Description
        {
            get { return this._description; }
        }
        #endregion

        public UserGroupBase(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            _conn = conn;
            _transaction = transaction;
        }
    }
}
