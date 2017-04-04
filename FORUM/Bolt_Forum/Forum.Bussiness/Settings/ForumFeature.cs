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
    public abstract class ForumFeature
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        private int _siteId;
        private bool _ifEnableMessage;
        private bool _ifEnableFavorite;
        private bool _ifEnableSubscribe;
        private bool _ifEnableScore;
        private bool _ifEnableReputation;
        private bool _ifEnableHotTopic;
        private bool _ifEnableGroupPermission;
        private bool _ifEnableReputationPermission;
        #endregion

        #region properties
        public int SiteId
        {
            get { return this._siteId; }
        }
        public bool IfEnableMessage
        {
            get { return this._ifEnableMessage; }
        }
        public bool IfEnableFavorite
        {
            get { return this._ifEnableFavorite; }
        }
        public bool IfEnableSubscribe
        {
            get { return this._ifEnableSubscribe; }
        }
        public bool IfEnableScore
        {
            get { return this._ifEnableScore; }
        }
        public bool IfEnableReputation
        {
            get { return this._ifEnableReputation; }
        }
        public bool IfEnableHotTopic
        {
            get { return this._ifEnableHotTopic; }
        }
        public bool IfEnableGroupPermission
        {
            get { return this._ifEnableGroupPermission; }
        }
        public bool IfEnableReputationPermission
        {
            get { return this._ifEnableReputationPermission; }
        }
        #endregion

        public ForumFeature(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            this._conn = conn;
            this._transaction = transaction;
            this._siteId = conn.SiteId;

            DataTable table = ConfigAccess.GetForumFeature(conn, transaction);

            if (table.Rows.Count <= 0)
            {
                ExceptionHelper.ThrowForumFeatureNotExist();
            }
            else
            {
                this._ifEnableMessage = Convert.ToBoolean(table.Rows[0]["IfEnableMessage"]);
                this._ifEnableFavorite = Convert.ToBoolean(table.Rows[0]["IfEnableFavorite"]);
                this._ifEnableSubscribe = Convert.ToBoolean(table.Rows[0]["IfEnableSubscribe"]);
                this._ifEnableScore = Convert.ToBoolean(table.Rows[0]["IfEnableScore"]);
                this._ifEnableReputation = Convert.ToBoolean(table.Rows[0]["IfEnableReputation"]);
                this._ifEnableHotTopic = Convert.ToBoolean(table.Rows[0]["IfEnableHotTopic"]);
                this._ifEnableGroupPermission = Convert.ToBoolean(table.Rows[0]["IfEnableGroupPermission"]);
                this._ifEnableReputationPermission = Convert.ToBoolean(table.Rows[0]["IfEnableReputationPermission"]);
            }
        }

        public void Update(bool ifEnableMessage, bool ifEnableFavorite, bool ifEnableSubscribe, bool ifEnableScore, bool ifEnableReputation,
            bool ifEnableHotTopic, bool ifEnableGroupPermission, bool ifEnableReputationPermission)
        {
            SettingsAccess.UpdateForumFeature(_conn, _transaction, ifEnableMessage, ifEnableFavorite, ifEnableSubscribe, ifEnableScore, ifEnableReputation,
            ifEnableHotTopic, ifEnableGroupPermission, ifEnableReputationPermission);

        }
       
    }
}
