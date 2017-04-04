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
    public abstract class HotTopicStrategySetting
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        private int _siteId;
        private int _parameterGreaterThanOrEqualViews;
        private int _parameterGreaterThanOrEqualPosts;
        private EnumLogical _logicalBetweenViewsAndPosts;
        #endregion

        #region properties
        public int SiteId
        {
            get { return this._siteId; }
        }
        public int ParameterGreaterThanOrEqualViews
        {
            get { return this._parameterGreaterThanOrEqualViews; }
        }
        public int ParameterGreaterThanOrEqualPosts
        {
            get { return this._parameterGreaterThanOrEqualPosts; }
        }
        public EnumLogical LogicalBetweenViewsAndrPosts
        {
            get { return (EnumLogical)this._logicalBetweenViewsAndPosts; }
        }
        #endregion

        public HotTopicStrategySetting(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            _conn = conn;
            _transaction = transaction;
            DataTable table = ConfigAccess.GetHotTopicStrategy(conn,transaction);
            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowForumHotTopicStrategySettingNotExist();
            }
            else
            {
                _siteId = conn.SiteId;
                _parameterGreaterThanOrEqualViews = Convert.ToInt32(table.Rows[0]["HotTopicParameterGreaterThanOrEqualViews"]);
                _parameterGreaterThanOrEqualPosts = Convert.ToInt32(table.Rows[0]["HotTopicParameterGreaterThanOrEqualPosts"]);
                _logicalBetweenViewsAndPosts = Convert.ToInt16(table.Rows[0]["HotTopicLogicalBetweenViewsAndrPosts"]) == 0 ? EnumLogical.AND : EnumLogical.OR;
            }
        }

        private void CheckIfEnableHotTopic(UserOrOperator operatingUserOrOperator)
        {
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, operatingUserOrOperator);
            if (!forumFeature.IfEnableHotTopic)
                ExceptionHelper.ThrowForumSettingsCloseHotTopicFunction();
        }

        public virtual void Update(UserOrOperator operatingUserOrOperator,int parameterGreaterThanOrEqualViews, int parameterGreaterThanOrEqualPosts, EnumLogical logicalBetweenViewsAndPosts)
        {
            CheckIfEnableHotTopic(operatingUserOrOperator);
            ConfigAccess.UpdateHotTopicStrategy(_conn, _transaction, parameterGreaterThanOrEqualViews, parameterGreaterThanOrEqualPosts, logicalBetweenViewsAndPosts);
        }
    }
}
