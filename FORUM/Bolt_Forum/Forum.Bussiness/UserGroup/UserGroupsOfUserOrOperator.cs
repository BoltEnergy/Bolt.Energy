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
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.Bussiness
{
    public class UserGroupsOfUserOrOperator : UserGroupsBase
    {
        private int _userOrOperatorId;
        public int UserOrOperatorId
        {
            get { return this._userOrOperatorId; }
        }

        public UserGroupsOfUserOrOperator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
            : base(conn, transaction)
        {
            this._userOrOperatorId = userOrOperatorId;
        }

        protected UserGroupWithPermissionCheck[] GetAllUserGroups(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = GroupAccess.GetUserGroupsWhichContainExistUserOrOperator(_conn, _transaction, _userOrOperatorId);

            UserGroupWithPermissionCheck[] groups = new UserGroupWithPermissionCheck[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                groups[i] = CreateGroupObject(table.Rows[i], operatingUserOrOperator);
            }

            return groups;
        }

        public void DeleteAllRelation()
        {
 
        }
    }
}
