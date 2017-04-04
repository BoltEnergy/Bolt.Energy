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
    public abstract class UserReputationGroupPermissionForForum : GroupPermissionForForumBase
    {
        public UserReputationGroupPermissionForForum(SqlConnectionWithSiteId conn, SqlTransaction transaction, int groupId, int forumId)
            : base(conn, transaction)
        {
            DataTable table = GroupPermissionAccess.GetUserGroupPermissionByGroupAndForumId(groupId, forumId, conn, transaction);
            if (table.Rows.Count <= 0)
            {
                ExceptionHelper.ThrowForumPermissionNotExistWithGroupIdAndForumId(groupId, forumId);
            }
            else
            {
                DataRow dr = table.Rows[0];
                this._groupId = groupId;
                this._forumId = forumId;
                this._ifAllowViewForum = Convert.ToBoolean(dr["IfAllowViewForum"]);
                this._ifAllowViewTopic = Convert.ToBoolean(dr["IfAllowViewTopic"]);
                this._ifAllowPost = Convert.ToBoolean(dr["IfAllowPost"]);
                this._minIntervalForPost = Convert.ToInt32(dr["MinIntervalForPost"]);
                this._maxLengthOfPost = Convert.ToInt32(dr["MaxLengthOfPost"]);
                this._ifPostNotNeedModeration = Convert.ToBoolean(dr["IfPostNotNeedModeration"]);
                //this._ifAllowHTML = Convert.ToBoolean(dr["IfAllowHtml"]);
                this._ifAllowUrl = Convert.ToBoolean(dr["IfAllowUrl"]);
                this._ifAllowUploadImage = Convert.ToBoolean(dr["IfAllowUploadImage"]);
            }
        }
        private void CheckIfEnableReputationPermission(UserOrOperator operatingUserOrOperator)
        {
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, operatingUserOrOperator);
            if (!forumFeature.IfEnableReputation)
                ExceptionHelper.ThrowForumSettingsCloseReputationFunctio();
            if (!forumFeature.IfEnableReputationPermission)
                ExceptionHelper.ThrowForumSettingsCloseReputationPermissionFunction();
        }

        protected void Update(UserOrOperator operatingUserOrOperator,bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost, int minIntervalForPost, int maxLengthOfPost,
                bool ifPostNotNeedModeration, bool ifAllowUrl, bool ifAllowUploadImage)
        {
            CheckIfEnableReputationPermission(operatingUserOrOperator);
            GroupPermissionAccess.UpdatePermission(_conn, _transaction, _groupId, _forumId, ifAllowViewForum, ifAllowViewTopic, ifAllowPost, minIntervalForPost, maxLengthOfPost, ifPostNotNeedModeration, ifAllowUrl, ifAllowUploadImage);
        }

        protected void Delete(UserOrOperator operatingUserOrOperator)
        {
            CheckIfEnableReputationPermission(operatingUserOrOperator);
            GroupPermissionAccess.DeletePermission(_groupId, _forumId, _conn, _transaction);
        }
    }
}
