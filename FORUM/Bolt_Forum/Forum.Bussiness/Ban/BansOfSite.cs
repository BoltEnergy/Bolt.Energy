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
using Com.Comm100.Framework.FieldLength;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class BansOfSite : Bans
    {
        public BansOfSite(SqlConnectionWithSiteId conn, SqlTransaction transaction)
            :base(conn, transaction)
        {
        }

        private bool IfCanAdd(int banUserOrOperatorId)
        {
            return BanAccess.GetCountOfBanByUserOrOperatorIdAndDate(_conn, _transaction, banUserOrOperatorId, DateTime.UtcNow) <= 0;
        }

        public virtual int Add(DateTime startDate, DateTime endDate, string note, int operatedUserOrOperatorId, Int64 banStartIP, Int64 banEndIP)
        {
            return BanBase.Add(_conn, _transaction, startDate, endDate, note, operatedUserOrOperatorId, banStartIP, banEndIP);
        }

        public virtual int Add(DateTime startDate, DateTime endDate, string note,int operatedUserOrOperatorId, int banUserOrOperatorId)
        {
            UsersOrOperatorsOfSite usersOrOperators = new UsersOrOperatorsOfSite(_conn, _transaction);
            if (!usersOrOperators.IfUserOrOperatorExist(banUserOrOperatorId)) 
                ExceptionHelper.ThrowUserIdNotExist(banUserOrOperatorId); //throw Exception
            if (!IfCanAdd(banUserOrOperatorId)) 
                ExceptionHelper.ThrowForumBanCannotAddWithUserId(banUserOrOperatorId); //throw Exception
            return BanBase.Add(_conn, _transaction, startDate, endDate, note, operatedUserOrOperatorId, banUserOrOperatorId);
        }

        public virtual BanBase[] GetBansByQueryAndPaging(UserOrOperator operatingUserOrOperator,int pageIndex,int pageSize,EnumBanType type,long ip,string name,string orderField, string orderDirection)
        {
            DataTable table = new DataTable();
            table = BanAccess.GetBansByQueryAndPaging(_conn, _transaction, pageIndex, pageSize, type, ip, name, orderField, orderDirection);
            List<BanBase> bans = new List<BanBase>();
            foreach (DataRow dr in table.Rows)
            {
                if (dr != null)
                {
                    BanBase ban = this.CreateBanObject(dr, operatingUserOrOperator);
                    bans.Add(ban);
                }
            }
            return bans.ToArray<BanBase>();
        }

        public virtual int GetCountOfBansByQuery(EnumBanType type, long ip, string name)
        {
            return BanAccess.GetCountOfBansByQuery(_conn, _transaction, type, ip, name);
        }

        public virtual int GetCountOfBanByIP(EnumBanType type, long ip)
        {
            DataTable table = BanAccess.GetBanOfIp(_conn, _transaction, ip);

            return table.Rows.Count;

        }
    }
}
