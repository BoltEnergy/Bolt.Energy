#if OPENSOURCE
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public class BansOfUserOrOperatorWithPermissionCheck : BansOfUserOrOperator
    {
        UserOrOperator _operatingUserOrOperator;

        public BansOfUserOrOperatorWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int userOrOperatorId)
            : base(conn, transaction, userOrOperatorId)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public BanUserOrOperatorWithPermissionCheck[] GetAllBans()
        {
            return base.GetAllBans(_operatingUserOrOperator);
        }

        public BanUserOrOperatorWithPermissionCheck[] GetAllNotDeletedBans()
        {
            return base.GetAllNotDeletedBans(_operatingUserOrOperator);
        }
    }
}
