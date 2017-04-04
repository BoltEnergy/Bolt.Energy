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
    public abstract class UserGroupsBase
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        public UserGroupsBase(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            this._conn = conn;
            this._transaction = transaction;
        }

        protected UserGroupWithPermissionCheck CreateGroupObject(DataRow dr, UserOrOperator operaingUserOrOperator)
        {
            int Id = Convert.ToInt32(dr["Id"]);
            string name = dr["Name"].ToString();
            string description = dr["Description"].ToString();
            bool ifAllForumUsersGroup = Convert.ToBoolean(dr["IfAllForumUsersGroup"]);
            return new UserGroupWithPermissionCheck(_conn, _transaction, operaingUserOrOperator, Id, name, description, ifAllForumUsersGroup);
        }

        protected UserGroupOfForumWithPermissionCheck CreateGroupOfForumObject(
            DataRow dr, UserOrOperator operaingUserOrOperator,int forumId)
        {
            int Id = Convert.ToInt32(dr["Id"]);
            string name = dr["Name"].ToString();
            string description = dr["Description"].ToString();
            //bool ifAllForumUsersGroup = Convert.ToBoolean(dr["IfAllForumUsersGroup"]);
            return new UserGroupOfForumWithPermissionCheck(
                _conn, _transaction, operaingUserOrOperator,forumId,Id,
                name, description);
        }
    }
}
