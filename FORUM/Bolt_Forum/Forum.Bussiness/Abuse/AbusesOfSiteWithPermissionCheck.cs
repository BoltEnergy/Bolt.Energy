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
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.Bussiness
{
    public class AbusesOfSiteWithPermissionCheck : AbusesOfSite
    {
        UserOrOperator _operatingUserOrOperator;

        public AbusesOfSiteWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
            :base(conn, transaction)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public AbuseWithPermissionCheck[] GetAbusesByQueryAndPaging(
            string keyword, EnumAbuseStatus Status, bool ifAllStatus, int pageIndex, int pageSize,
            string orderField, string orderMethod,out int count)
        {
            return base.GetAbusesByQueryAndPaging(
                _operatingUserOrOperator, keyword,Status,ifAllStatus, pageIndex, pageSize,
                orderField,orderMethod,out count);
        }

        public AbuseWithPermissionCheck[] GetAbusesByModeratorWithQueryAndPaging(
            int moderatorId, string keyword, EnumAbuseStatus status, bool ifAllStatus, int pageIndex, int pageSize,
            string orderField, string orderMethod)
        {
            return base.GetAbusesByModeratorWithQueryAndPaging(_operatingUserOrOperator, moderatorId, keyword, status, ifAllStatus, pageIndex, pageSize, orderField, orderMethod);
        }

        public int GetCountOfAbusesByModeratorWithQuery(int moderatorId,string keyword,EnumAbuseStatus status,bool ifAllStatus)
        {
            return base.GetCountOfAbusesByModeratorWithQuery(moderatorId, keyword, status, ifAllStatus);
        }
    }
}
