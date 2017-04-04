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
    public class UserReputationGroupsOfOutMessage : UserReputationGroupsBase
    {
        private int _outMessageId;
        public int OutMessageId
        {
            get { return this._outMessageId; }
        }

        public UserReputationGroupsOfOutMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int outMessageId)
            : base(conn, transaction)
        {
            _outMessageId = outMessageId;
        }

        public UserReputationGroupWithPermissionCheck[] GetAllGroups(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = GroupAccess.GetUserReputationGroupsOfOutMessage(_conn, _transaction, OutMessageId);
            List<UserReputationGroupWithPermissionCheck> UserReputationGroups = new List<UserReputationGroupWithPermissionCheck>();
            foreach (DataRow dr in table.Rows)
            {
                UserReputationGroupWithPermissionCheck tmpUserReputationGroup = CreateGroupObject(dr, operatingUserOrOperator);
                UserReputationGroups.Add(tmpUserReputationGroup);
            }
            return UserReputationGroups.ToArray < UserReputationGroupWithPermissionCheck>();
        }
        public void Add(List<int> reputationGroupIds)
        {
            if (reputationGroupIds == null) return;
            foreach (int reputationGroupId in reputationGroupIds)
            {
                ReputationGroupAccess.AddGroupOfOutMessage(_conn, _transaction, _outMessageId, reputationGroupId);
            }
        }
    }
}
