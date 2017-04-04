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
    public abstract class UserGroupPermissionForForum : GroupPermissionForForumBase
    {
        public UserGroupPermissionForForum(SqlConnectionWithSiteId conn, SqlTransaction transaction, int groupId, int forumId)
            : base(conn, transaction)
        {
            base._groupId = groupId;
            base._forumId = forumId;
            DataTable table = GroupPermissionAccess.GetUserGroupPermissionByGroupAndForumId(groupId, forumId, conn, transaction);
            if (table.Rows.Count <= 0)
            {
                ExceptionHelper.ThrowForumPermissionNotExistWithGroupIdAndForumId(groupId, forumId);
            }
            else
            {
                this._ifAllowViewForum=Convert.ToBoolean(table.Rows[0]["IfAllowViewForum"]);
                this._ifAllowViewTopic=Convert.ToBoolean(table.Rows[0]["IfAllowViewTopic"]);
                this._ifAllowPost=Convert.ToBoolean(table.Rows[0]["IfAllowPost"]);
                this._minIntervalForPost=Convert.ToInt32(table.Rows[0]["MinIntervalForPost"]);
                this._maxLengthOfPost=Convert.ToInt32(table.Rows[0]["MaxLengthOfPost"]);
                this._ifPostNotNeedModeration=Convert.ToBoolean(table.Rows[0]["IfPostNotNeedModeration"]);
                //this._ifAllowHTML=Convert.ToBoolean(table.Rows[0]["IfAllowHtml"]);
                this._ifAllowUrl=Convert.ToBoolean(table.Rows[0]["IfAllowUrl"]);
                this._ifAllowUploadImage=Convert.ToBoolean(table.Rows[0]["IfAllowUploadImage"]);
            }
        }

        private void CheckIfEnableGroupPermission(UserOrOperator operatingUserOrOperator)
        {
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, operatingUserOrOperator);
            if (!forumFeature.IfEnableGroupPermission)
                ExceptionHelper.ThrowForumSettingsCloseGroupPermissionFunction();
        }
        protected void Update(UserOrOperator operatingUserOrOperator,
            bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost, int minIntervalForPost, int maxLengthOfPost,
            bool ifPostNotNeedModeration, bool ifAllowUrl, bool ifAllowUploadImage)
        {
            CheckIfEnableGroupPermission(operatingUserOrOperator);
            GroupPermissionAccess.UpdatePermission(_conn, _transaction, _groupId, _forumId, ifAllowViewForum, ifAllowViewTopic, ifAllowPost, minIntervalForPost, maxLengthOfPost, ifPostNotNeedModeration, ifAllowUrl, ifAllowUploadImage);
        }

        protected void Delete(UserOrOperator operatingUserOrOperator)
        {
            CheckIfEnableGroupPermission(operatingUserOrOperator);
            GroupPermissionAccess.DeletePermission(_groupId, _forumId, _conn, _transaction);
        }
    }
}
