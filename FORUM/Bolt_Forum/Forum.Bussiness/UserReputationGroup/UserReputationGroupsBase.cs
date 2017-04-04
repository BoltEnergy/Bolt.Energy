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
    public abstract class UserReputationGroupsBase
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        public UserReputationGroupsBase(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            this._conn = conn;
            this._transaction = transaction;
        }

        protected UserReputationGroupWithPermissionCheck CreateGroupObject(DataRow dr, UserOrOperator operaingUserOrOperator)
        {
            int Id = Convert.ToInt32(dr["Id"]);
            string name = dr["Name"].ToString();
            string description = dr["Description"].ToString();
            int limitedBegin = Convert.ToInt32(dr["LimitedBegin"]);
            int limitedExpire = Convert.ToInt32(dr["LimitedExpire"]);
            int icoRepeat = Convert.ToInt32(dr["IcoRepeat"]);
            return new UserReputationGroupWithPermissionCheck(_conn, _transaction, operaingUserOrOperator, Id, name, description, limitedBegin, limitedExpire, icoRepeat);
        }
    }
}
