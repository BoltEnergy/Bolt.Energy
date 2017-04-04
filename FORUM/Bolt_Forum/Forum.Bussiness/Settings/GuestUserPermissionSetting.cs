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
    public abstract class GuestUserPermissionSetting
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        private int _siteId;
        private bool _ifAllowGuestUserViewForum;
        private bool _ifAllowGuestUserSearch;
        private int _guestUserSearchInterval;
        #endregion

        #region properties
        public int SiteId
        {
            get { return this._siteId; }
        }
        public bool IfAllowGuestUserViewForum
        {
            get { return this._ifAllowGuestUserViewForum; }
        }
        public bool IfAllowGuestUserSearch
        {
            get { return this._ifAllowGuestUserSearch; }
        }
        public int GuestUserSearchInterval
        {
            get { return this._guestUserSearchInterval; }
        }
        #endregion

        public GuestUserPermissionSetting(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            _conn = conn;
            _transaction = transaction;
            _siteId = conn.SiteId;
            DataTable table = ConfigAccess.GetGuestUserPermission(conn, transaction);
            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowForumGuestUserPermissionSettingNotExist();
            }
            else
            {
                _ifAllowGuestUserViewForum = Convert.ToBoolean(table.Rows[0]["IfAllowGuestUserViewForum"]);
                _ifAllowGuestUserSearch = Convert.ToBoolean(table.Rows[0]["IfAllowGuestUserSearch"]);
                _guestUserSearchInterval = Convert.ToInt32(table.Rows[0]["GuestUserSearchInterval"]);
            }
 
        }

        protected void Update(bool ifAllowGuestUserViewForum, bool ifAllowGuestUserSearch, int guestUserSearchInterval)
        {
            ConfigAccess.UpdateGuestUserPermission(_conn, _transaction, ifAllowGuestUserViewForum, ifAllowGuestUserSearch, guestUserSearchInterval);
        }
    }
}
