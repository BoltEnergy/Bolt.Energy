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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness 
{
    public abstract class AbusesOfSite : AbusesBase
    {
        public AbusesOfSite(SqlConnectionWithSiteId conn, SqlTransaction transaction)
            :base(conn, transaction)
        {
        }

        protected virtual AbuseWithPermissionCheck[] GetAbusesByQueryAndPaging(UserOrOperator operatingUserOrOperator,
            string keyword, EnumAbuseStatus Status, bool ifAllStatus, int pageIndex, int pageSize,
            string orderField, string orderMethod,out int count)
        {
            count = AbuseAccess.GetCountOfAbusesByQueryAndPaging(_conn,_transaction,
                keyword,Status,ifAllStatus);
            DataTable dt = AbuseAccess.GetAbusesByQueryAndPaging(_conn, _transaction,
                keyword, Status, ifAllStatus, pageIndex, pageSize, orderField, orderMethod);
            List<AbuseWithPermissionCheck> abuses = new List<AbuseWithPermissionCheck>();
            foreach (DataRow dr in dt.Rows)
            {
                abuses.Add(CreateAbuseObject(dr, operatingUserOrOperator));
            }
            return abuses.ToArray<AbuseWithPermissionCheck>();
        }

        protected AbuseWithPermissionCheck[] GetAbusesByModeratorWithQueryAndPaging(UserOrOperator operatingUserOrOperator, int moderatorId,
            string keyword, EnumAbuseStatus Status, bool ifAllStatus, int pageIndex, int pageSize, string orderField, string orderMethod)
        {
            DataTable dt = AbuseAccess.GetAbusesByModeratorWithQueryAndPaging(_conn, _transaction,moderatorId,
                keyword, Status, ifAllStatus, pageIndex, pageSize, orderField, orderMethod);
            List<AbuseWithPermissionCheck> abuses = new List<AbuseWithPermissionCheck>();
            foreach (DataRow dr in dt.Rows)
            {
                abuses.Add(CreateAbuseObject(dr, operatingUserOrOperator));
            }
            return abuses.ToArray<AbuseWithPermissionCheck>();
        }

        protected int GetCountOfAbusesByModeratorWithQuery(int moderatorId, string keyword, EnumAbuseStatus status, bool ifAllStatus)
        {
            return AbuseAccess.GetCountOfAbusesByModeratorWithQuery(_conn, _transaction, moderatorId, keyword, status, ifAllStatus);
        }
    }
}
