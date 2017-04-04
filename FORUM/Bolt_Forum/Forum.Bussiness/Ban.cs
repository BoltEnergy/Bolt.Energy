#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2009                                                                               //
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

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class Ban
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region protected fields
        protected int _id;
        protected DateTime _startDate;
        protected DateTime _endDate;
        protected string _note;
        #endregion

        #region properties
        public int Id
        {
            get { return this._id; }
        }
        public DateTime StartDate
        {
            get { return this._startDate; }
        }
        public DateTime EndDate
        {
            get { return this._endDate; }
        }
        public string Note
        {
            get { return this._note; }
        }
        #endregion

        public Ban(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        { }

        public CategoriesOfBanWithPermissionCheck GetBanCategories()
        {
            return null;
        }

        public ForumsOfBanWithPermissionCheck GetBanForums()
        {
            return null;
        }
    }
}
