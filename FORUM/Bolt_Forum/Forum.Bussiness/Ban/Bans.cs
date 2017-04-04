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
    public abstract class Bans
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        public Bans(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            this._conn = conn;
            this._transaction = transaction;
        }

        protected BanBase CreateBanObject(DataRow dr, UserOrOperator operatingUserOrOperator)
        {
            BanBase ban;
            if (Convert.ToInt16(dr["Type"]) == Convert.ToInt16(EnumBanType.IP))
            {
                ban = new BanIPWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, Convert.ToInt32(dr["Id"]),
                    Convert.ToDateTime(dr["StartDate"]),
                    Convert.ToDateTime(dr["EndDate"]),
                    Convert.ToString(dr["note"]),
                    Convert.ToInt32(dr["OperatedUserOrOperatorId"]),
                    Convert.ToInt64(dr["StartIP"]),
                    Convert.ToInt64(dr["EndIP"]),
                    Convert.ToBoolean(dr["IfDeleted"]),
                    Convert.ToDateTime(dr["DeleteDate"]));
            }
            else
            {
                int banUserId = Convert.ToInt32(dr["UserOrOperatorId"]);
                string banUserName = Convert.ToString(((DataTable)UserAccess.GetUserOrOperatorById(_conn, _transaction, banUserId)).Rows[0]["Name"]);
                ban = new BanUserOrOperatorWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, 
                    Convert.ToInt32(dr["Id"]),
                    Convert.ToDateTime(dr["StartDate"]),
                    Convert.ToDateTime(dr["EndDate"]),
                    Convert.ToString(dr["Note"]),
                    Convert.ToInt32(dr["OperatedUserOrOperatorId"]),
                    banUserId, banUserName,
                    Convert.ToBoolean(dr["IfDeleted"]),
                    Convert.ToDateTime(dr["DeleteDate"]));
            }
            return ban;
        }
    }
}
