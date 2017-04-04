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
    public abstract class AbusesBase
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        public AbusesBase(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            this._conn = conn;
            this._transaction = transaction;
        }

        protected AbuseWithPermissionCheck CreateAbuseObject(DataRow dr, UserOrOperator operatingUserOrOperator)
        {
            int id = Convert.ToInt32(dr["Id"]);
            int postId = Convert.ToInt32(dr["PostId"]);
            string postSubject = Convert.ToString(dr["Subject"]);
            int postUserOrOperatorId = Convert.ToInt32(dr["PostUserOrOperatorId"]);
            string postUserOrOperatorName = Convert.ToString(dr["PostUserOrOperatorName"]);
            int abuseUserOrOperatorId = Convert.ToInt32(dr["UserOrOperatorId"]);
            string abuseUserOrOperatorName = Convert.ToString(dr["AbuseUserOrOperatorName"]);
            EnumAbuseStatus status = (EnumAbuseStatus)Convert.ToInt32(dr["Status"]);
            DateTime abuseDate = Convert.ToDateTime(dr["Date"]);
            string note = Convert.ToString(dr["Note"]);
            AbuseWithPermissionCheck abuse = new AbuseWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, id, postId, postSubject,
                postUserOrOperatorId, postUserOrOperatorName, abuseUserOrOperatorId, abuseUserOrOperatorName, status, abuseDate, note);
            return abuse;
        }
    }
}
