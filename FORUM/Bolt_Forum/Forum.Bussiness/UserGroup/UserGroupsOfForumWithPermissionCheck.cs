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
    public class UserGroupsOfForumWithPermissionCheck : UserGroupsOfForum
    {
        UserOrOperator _operaingOperator;

        public UserGroupsOfForumWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingOperator, int forumId)
            : base(conn, transaction, forumId)
        {
            _operaingOperator = operatingOperator;
        }

        public UserGroupOfForumWithPermissionCheck[] GetAllUserGroups()
        {
            return base.GetAllUserGroups(_operaingOperator);
        }

        
        public void Delete(int groupId)
        {
            CommFun.CheckAdminPanelOrModeratorPanelCommomPermission(_operaingOperator, base.ForumId);
            base.Delete(groupId, _operaingOperator);
        }

      
        public void Add(int groupId)
        {
            CommFun.CheckAdminPanelOrModeratorPanelCommomPermission(_operaingOperator, base.ForumId);
            base.Add(groupId, _operaingOperator);
        }

        public void UpdateUserGroups(List<int> groupIds,List<GroupPermission> permissions)
        {
            CommFun.CheckAdminPanelOrModeratorPanelCommomPermission(_operaingOperator,ForumId);
            base.UpdateGroups(_operaingOperator, groupIds, permissions);
        }
        //used in deleting forum
        public void DeleteAll()
        {
            CommFun.CheckAdminPanelOrModeratorPanelCommomPermission(_operaingOperator,base.ForumId);
            base.DeleteAll();
        }
    }
}
