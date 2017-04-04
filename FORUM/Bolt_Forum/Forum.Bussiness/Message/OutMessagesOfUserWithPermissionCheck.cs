﻿#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2009                                                                               //
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
    public class OutMessagesOfUserWithPermissionCheck : OutMessagesOfUser
    {
        IUserOrOperator _operatingUserOrOperator;

        public OutMessagesOfUserWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, IUserOrOperator operatingUserOrOperator, int userOrOperatorId)
            : base(conn, transaction, userOrOperatorId)
        { }

        public UserGroupsOfOutMessageWithPermissionCheck GetUserGroups()
        {
            return base.GetUserGroups(_operatingUserOrOperator);
        }

        public UserReputationGroupsOfOutMessageWithPermissionCheck GetUserReputationGroups()
        {
            return base.GetUserReputationGroups(_operatingUserOrOperator);
        }

    }
}
