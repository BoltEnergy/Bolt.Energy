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
    public abstract class UsersOrOperatorsOfUserGroup : UsersOrOperatorsBase
    {
        private int _groupId;
        public int GroupId
        {
            get { return this._groupId; }
        }

        public UsersOrOperatorsOfUserGroup(SqlConnectionWithSiteId conn, SqlTransaction transaction, int groupId)
            :base(conn, transaction)
        {

        }

        public int GetCount()
        {
            return 0;
        }

        protected UserOrOperator[] GetUsersOrOperatorsByQueryAndPaging(UserOrOperator operatingUserOrOperator, int pageIndex, int pageSize, string keyword)
        {
            return null;
        }
    }
}
