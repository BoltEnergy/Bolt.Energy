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
using System.IO;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public class UsersOrOperatorsOfOutMessage : UsersOrOperatorsBase
    {
        private int _outMessageId;
        public int OutMessageId
        {
            get { return this._outMessageId; }
        }

        public UsersOrOperatorsOfOutMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int outMessageId)
            : base(conn, transaction)
        {
            this._outMessageId = outMessageId;
        }

        public UserOrOperator[] GetAllUsersOrOperators(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = UserAccess.GetRecieversOfOutMessage(_conn, _transaction, OutMessageId);
            List<UserOrOperator> UserOrOperators = new List<UserOrOperator>();
            foreach (DataRow  dr in table.Rows)
            {
                UserOrOperator tmpUserOrOperator = CreateUserOrOperatorObject(dr, operatingUserOrOperator);
                UserOrOperators.Add(tmpUserOrOperator);
            }
            return UserOrOperators.ToArray<UserOrOperator>();
        }

        public void Add(int userOrOperatorId)
        {
            UserOfOutMessageAccess.AddUserOfOutMessage(_conn, _transaction, _outMessageId, userOrOperatorId);
        }
        public void Add(List<int> userOrOperatorIds)
        {
            if (userOrOperatorIds == null) return;
            foreach(int userOrOperatorId in userOrOperatorIds)
                UserOfOutMessageAccess.AddUserOfOutMessage(_conn, _transaction, _outMessageId, userOrOperatorId);
        }
      
    }
}
