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
    public class UserGroupPermissionForForumWithPermissionCheck : UserGroupPermissionForForum
    {
        UserOrOperator _operatingUserOrOperator;

        public UserGroupPermissionForForumWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int groupId, int forumId)
            : base(conn, transaction, groupId, forumId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }
        private void CheckPermission(UserOrOperator operatingUserOrOperator)
        {
            CommFun.CheckAdminPanelOrModeratorPanelCommomPermission(_operatingUserOrOperator,_forumId);
        }
        public void Update(bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost, int minIntervalForPost, int maxLengthOfPost, bool ifPostNotNeedModeration, bool ifAllowUrl, bool ifAllowUploadImage)
        {
            CheckPermission(_operatingUserOrOperator);
            base.Update(_operatingUserOrOperator,ifAllowViewForum, ifAllowViewTopic, ifAllowPost, minIntervalForPost, maxLengthOfPost, ifPostNotNeedModeration, ifAllowUrl, ifAllowUploadImage);
        }

        public void Delete()
        {
            CheckPermission(_operatingUserOrOperator);
            base.Delete(_operatingUserOrOperator);
        }
    }
}
