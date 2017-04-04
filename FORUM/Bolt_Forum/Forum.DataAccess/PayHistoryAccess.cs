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
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.DataAccess
{
    
    public class PayHistoryAccess
    {
        public static void PayHistoryAdd(
            SqlConnectionWithSiteId conn, SqlTransaction transaction, 
            EnumPayType type, int userId, int itemId, int score, DateTime date)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("insert into [t_Forum_PayHistory{0}](Type,ItemId,UserId,Score,Date)",conn.SiteId));
            strSQL.Append(" values(@type,@itemId,@userId,@score,@date)");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@score", score);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@itemId", itemId);
            cmd.Parameters.AddWithValue("@type",Convert.ToInt32(type));

            cmd.ExecuteNonQuery();
        }

        public static bool IfUserPaid(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            EnumPayType type,int userId,int itemId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select * from [t_Forum_PayHistory{0}]",conn.SiteId));
            strSQL.Append(" where [ItemId]=@itemId and [UserId]=@userId and [Type]=@type");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@itemId", itemId);
            cmd.Parameters.AddWithValue("@type", Convert.ToInt32(type));

            SqlDataReader dr = cmd.ExecuteReader();
            bool result = dr.HasRows;
            dr.Close();
            return result;
        }

        public static void DeleteHistroies(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            EnumPayType type,int itemId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("delete [t_Forum_PayHistory{0}]",conn.SiteId));
            strSQL.Append(" where [ItemId]=@itemId and [Type]=@type");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@itemId", itemId);
            cmd.Parameters.AddWithValue("@type", Convert.ToInt32(type));

            cmd.ExecuteNonQuery();
        }

    }
}
