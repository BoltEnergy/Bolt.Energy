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
    public class UserOrOperatorHTMLPermission : UserOrOperatorPermissionBase, IHTMLPermission
    {
        private bool _ifAllowHTML;
        private bool _ifAllowUrl;

        public bool IfAllowHTML 
        {
            get { return this._ifAllowHTML; }
        }
        public bool IfAllowUrl 
        {
            get { return this._ifAllowUrl; }
        }

        public UserOrOperatorHTMLPermission(SqlConnection generalConn, SqlTransaction generalTransaction, SqlConnectionWithSiteId siteConn, SqlTransaction siteTransaction, int userOrOperatorId)
            : base(generalConn, generalTransaction, siteConn, siteTransaction, userOrOperatorId)
        { }

        public void CheckPermission()
        { }
    }
}
