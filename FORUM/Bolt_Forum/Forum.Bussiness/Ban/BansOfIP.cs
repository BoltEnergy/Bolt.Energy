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
    public class BansOfIP : Bans
    {
        private Int64 _ip;
        public Int64 IP
        {
            get { return this._ip; }
        }

        public BansOfIP(SqlConnectionWithSiteId conn, SqlTransaction transaction, Int64 ip)
            : base(conn, transaction)
        {
            _ip = ip;
        }

        protected BanIPWithPermissionCheck[] GetAllBans(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = new DataTable();
            table = BanAccess.GetBansOfIp(_conn, _transaction,_ip);
            List<BanIPWithPermissionCheck> bans = new List<BanIPWithPermissionCheck>();
            foreach (DataRow dr in table.Rows)
            {
                BanIPWithPermissionCheck banIp = (BanIPWithPermissionCheck)this.CreateBanObject(dr, operatingUserOrOperator);
                bans.Add(banIp);
            }

            return bans.ToArray<BanIPWithPermissionCheck>();
        }
    }
}
