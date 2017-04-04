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
    public class BanFactory
    {
        public static BanBase GetBanById(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id, UserOrOperator operatingUserOrOperator)
        {
            BanBase ban;
            DataTable table = BanAccess.GetBanById(conn, transaction, id);
            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowForumBanNotExistException(id);
            }
            if (Convert.ToInt16(table.Rows[0]["Type"]) == Convert.ToInt16(EnumBanType.IP))
            {
                ban = new BanIPWithPermissionCheck(conn, transaction, operatingUserOrOperator,id,
                    Convert.ToDateTime(table.Rows[0]["StartDate"]),
                    Convert.ToDateTime(table.Rows[0]["EndDate"]),
                    Convert.ToString(table.Rows[0]["note"]),
                    Convert.ToInt32(table.Rows[0]["OperatedUserOrOperatorId"]),
                    Convert.ToInt64(table.Rows[0]["StartIP"]),
                    Convert.ToInt64(table.Rows[0]["EndIP"]),
                    Convert.ToBoolean(table.Rows[0]["IfDeleted"]),
                    Convert.ToDateTime(table.Rows[0]["DeleteDate"]));
            }
            else
            {
                //int banUserId=Convert.ToInt32(table.Rows[0]["UserOrOperatorId"]);
                //string banUserName = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, banUserId).DisplayName;
                ban = new BanUserOrOperatorWithPermissionCheck(conn, transaction, operatingUserOrOperator, id,
                    Convert.ToDateTime(table.Rows[0]["StartDate"]),
                    Convert.ToDateTime(table.Rows[0]["EndDate"]),
                    Convert.ToString(table.Rows[0]["Note"]),
                    Convert.ToInt32(table.Rows[0]["OperatedUserOrOperatorId"]),
                    Convert.ToInt32(table.Rows[0]["UserOrOperatorId"]), 
                    Convert.ToString(table.Rows[0]["Name"]),
                    Convert.ToBoolean(table.Rows[0]["IfDeleted"]),
                    Convert.ToDateTime(table.Rows[0]["DeleteDate"]));
            }
            return ban;
        }

        public static BanBase GetBanByUserOrOperatorId(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
        {
            BanBase ban = null;
            DataTable table = BanAccess.GetNotDeletedBanOfUserOrOperator(conn, transaction, operatingUserOrOperator.Id);
            if (table.Rows.Count == 0)
            {
                return null;
                //ExceptionHelper.ThrowForumBanedUserNotExistException(operatingUserOrOperator.Id);
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (Convert.ToDateTime(table.Rows[i]["EndDate"]) > DateTime.UtcNow)
                {
                    ban = new BanUserOrOperatorWithPermissionCheck(conn, transaction, operatingUserOrOperator, Convert.ToInt32(table.Rows[i]["Id"]),
                        Convert.ToDateTime(table.Rows[i]["StartDate"]),
                        Convert.ToDateTime(table.Rows[i]["EndDate"]),
                        Convert.ToString(table.Rows[i]["Note"]),
                        Convert.ToInt32(table.Rows[i]["OperatedUserOrOperatorId"]),
                        Convert.ToInt32(table.Rows[i]["UserOrOperatorId"]),
                        Convert.ToString(table.Rows[i]["Name"]),
                        Convert.ToBoolean(table.Rows[i]["IfDeleted"]),
                        Convert.ToDateTime(table.Rows[i]["DeleteDate"]));
                    break;
                    //return ban;
                }
            }
            return ban;
        }

        public static BanBase GetBanByIP(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, long ip)
        {
            BanBase ban = null;
            DataTable table = BanAccess.GetBanOfIp(conn, transaction, ip);
            if (table.Rows.Count == 0)
            {
                return null;
                //ExceptionHelper.ThrowForumBanedIPNotExistException(ip);
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (Convert.ToDateTime(table.Rows[i]["EndDate"]) > DateTime.UtcNow)
                {
                    ban = new BanIPWithPermissionCheck(conn, transaction, operatingUserOrOperator, Convert.ToInt32(table.Rows[i]["Id"]),
                            Convert.ToDateTime(table.Rows[i]["StartDate"]),
                            Convert.ToDateTime(table.Rows[i]["EndDate"]),
                            Convert.ToString(table.Rows[i]["note"]),
                            Convert.ToInt32(table.Rows[i]["OperatedUserOrOperatorId"]),
                            Convert.ToInt64(table.Rows[i]["StartIP"]),
                            Convert.ToInt64(table.Rows[i]["EndIP"]),
                            Convert.ToBoolean(table.Rows[i]["IfDeleted"]),
                            Convert.ToDateTime(table.Rows[i]["DeleteDate"]));
                    break;
                    //return ban;
                }
            }

            return ban;
        }      
    }
}
