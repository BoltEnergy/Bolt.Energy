#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
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
    public class GuestUserViewForumPermission : GuestUserPermissionBase, IViewForumPermission
    {
        private int _forumId;
        private bool _ifAllowViewForum;

        public bool IfAllowViewForum
        {
            get { return this._ifAllowViewForum; }
        }
        public int ForumId
        {
            get { return this._forumId; }
        }

        public GuestUserViewForumPermission(SqlConnection generalConn, SqlTransaction generalTransaction, SqlConnectionWithSiteId siteConn, SqlTransaction siteTransaction, Int64 ip, int forumId)
            : base(generalConn, generalTransaction, siteConn, siteTransaction, ip)
        { }
    }
}
