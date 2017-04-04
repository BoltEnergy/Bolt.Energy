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
    public class UserReputationGroupsWithPermissionCheck : UserReputationGroups
    {
        UserOrOperator _operatingUserOrOperator;

        public UserReputationGroupsWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public void Delete(int id)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            base.Delete(id,_operatingUserOrOperator);
        }

        public override int Add(string name, string description, int limitedBegin, int limitedExpire, int icoRepeat)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            return base.Add(name, description, limitedBegin, limitedExpire, icoRepeat);
        }

        public UserReputationGroupWithPermissionCheck GetUserReputationGroupOfUserOrOperator(int userOrOperatorId)
        {
            return base.GetUserReputationGroupOfUserOrOperator(_operatingUserOrOperator, userOrOperatorId);
        }
    }
}
