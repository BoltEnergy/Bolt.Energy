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
    public class UserOrOperatorAvatarPermission : UserOrOperatorPermissionBase, IAvatarPermission
    {
        private bool _ifAllowCustomizeAvatar;

        public bool IfAllowCustomizeAvatar 
        {
            get { return this._ifAllowCustomizeAvatar; }
        }

        public UserOrOperatorAvatarPermission(SqlConnection generalConn, SqlTransaction generalTransaction, SqlConnectionWithSiteId siteConn, SqlTransaction siteTransaction, int userOrOperatorId)
            : base(generalConn, generalTransaction, siteConn, siteTransaction, userOrOperatorId)
        { }

        public void CheckPermission()
        { }
    }
}
