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
    public class UserGroupsOfOutMessage : UserGroupsBase
    {
        private int _outMessageId;
        public int OutMessageId
        {
            get { return this._outMessageId; }
        }

        public UserGroupsOfOutMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int outMessageId)
            : base(conn, transaction)
        {
            _outMessageId = outMessageId;
        }

        public UserGroupWithPermissionCheck[] GetAllGroups(UserOrOperator operatingUserOrOperator)
        {
           DataTable table = GroupAccess.GetUserGroupsOfOutMessage(_conn, _transaction, OutMessageId);
           List<UserGroupWithPermissionCheck> UserGroups = new List<UserGroupWithPermissionCheck>();
           foreach (DataRow  dr in table.Rows)
           {
               UserGroupWithPermissionCheck tmpUserGroup = CreateGroupObject(dr, operatingUserOrOperator);
               UserGroups.Add(tmpUserGroup);
           }
           return UserGroups.ToArray<UserGroupWithPermissionCheck>();
        }
        public void Add(List<int> userGroupIds)
        {
            if (userGroupIds == null)
                return;
            foreach (int userGroupId in userGroupIds)
            {
                GroupOfOutMessageAccess.AddGroupOfOutMessage(_conn, _transaction, OutMessageId, userGroupId);
            }
        }

    }
}
