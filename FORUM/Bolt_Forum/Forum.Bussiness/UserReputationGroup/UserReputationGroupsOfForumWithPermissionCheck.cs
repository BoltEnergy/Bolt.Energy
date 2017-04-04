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
    public class UserReputationGroupsOfForumWithPermissionCheck : UserReputationGroupsOfForum
    {
        UserOrOperator _operatingOperator;

        public UserReputationGroupsOfForumWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingOperator, int forumId)
            : base(conn, transaction, forumId)
        {
            _operatingOperator = operatingOperator;
        }

        public UserReputationGroupOfForumWithPermissionCheck[] GetAllGroups()
        {
            return base.GetAllGroups(_operatingOperator);
        }
        public void Add(int groupId)
        {
            CheckPermission();
            base.Add(groupId, _operatingOperator);
        }

        public void Delete(int groupId)
        {
            CheckPermission();
            base.Delete(groupId, _operatingOperator);
        }

        public void DeleteAll()
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingOperator);
            base.DeleteAll();
        }
        public void UpdateReputationGroups(List<int> groupIds, List<GroupPermission> permissions)
        {
            CheckPermission();
            base.UpdateGroups(_operatingOperator, groupIds, permissions);
        }

        private void CheckPermission()
        {
            CommFun.CheckAdminPanelOrModeratorPanelCommomPermission(_operatingOperator, ForumId);
        }
    }
}
