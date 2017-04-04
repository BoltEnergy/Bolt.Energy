#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2009                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public class UserOrOperatorSearchPermission : UserOrOperatorPermissionBase, ISearchPermission
    {
        private bool _ifAllowSearch;
        private int _minIntervalForSearch;

        public bool IfAllowSearch 
        {
            get { return this._ifAllowSearch; }
        }
        public int MinIntervalForSearch 
        {
            get { return this._minIntervalForSearch; }
        }

        public UserOrOperatorSearchPermission(SqlConnection generalConn, SqlTransaction generalTransaction, SqlConnectionWithSiteId siteConn, SqlTransaction siteTransaction, int userOrOperatorId)
            : base(generalConn, generalTransaction, siteConn, siteTransaction, userOrOperatorId)
        { }

        public void CheckPermission()
        { }
    }
}
