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
    public class ForumPermissionManager
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        private int _forumId;
        private bool _ifInheritPermission;

        public int ForumId
        {
            get { return this._forumId; }
        }
        public bool IfInheritPermission
        {
            get { return this._ifInheritPermission; }
        }

        public ForumPermissionManager(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
        {
            _conn = conn;
            _transaction = transaction;
            this._forumId = forumId;
            _ifInheritPermission = ForumAccess.IfInheritPermission(conn, transaction, forumId);
        }
        
        public UserForumPermissionItem GetUserForumPermissionItem(int userOrOperatorId)
        {
            return null;
        }
    }
}
