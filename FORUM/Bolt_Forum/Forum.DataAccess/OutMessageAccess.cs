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
    public class OutMessageAccess
    {
        public static int AddOutMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction, string subject, string message, DateTime createDate, int fromUserOrOperatorId)
        {

            StringBuilder strSQL = new StringBuilder("");
            #region build sql
            strSQL.Append("insert t_Forum_OutMessage" + conn.SiteId + "(Subject,Message,CreateDate,FromUserId)");
            strSQL.Append("values(@subject,@message,@createDate,@fromUserOrOperatorId);");
            strSQL.Append("select @@identity");
            #endregion build sql

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region add parameters
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@message", message);
            cmd.Parameters.AddWithValue("@createDate", createDate);
            cmd.Parameters.AddWithValue("@fromUserOrOperatorId", fromUserOrOperatorId);
            #endregion add parameters

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable GetOutMessageById(SqlConnectionWithSiteId conn, SqlTransaction transaction, int outMessageId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select a.*, b.Name FromUserName from");
            strSQL.Append(" t_Forum_OutMessage" + conn.SiteId + " a, t_User" + conn.SiteId + " b, t_User" + conn.SiteId + " c");
            strSQL.Append(" where a.Id=@OutMessageId and a.FromUserId = b.Id;");   
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction); 
            cmd.Parameters.AddWithValue("@OutMessageId", outMessageId);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        public static DataTable GetOutMessagesByPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, int pageIndex, int pageSize)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select b.*, c.Name FromUserName from");
            strSQL.Append(" (select *from (select ROW_NUMBER() over(order by CreateDate desc) row, * ");
            strSQL.Append(" from t_Forum_OutMessage" + conn.SiteId + " where FromUserId=@UserOrOperatorId ) a");
            strSQL.Append(string.Format(" where row between {0} and {1}) b,", startRowNum, endRowNum));
            strSQL.Append(" t_User" + conn.SiteId + " c where  b.FromUserId = c.Id; ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region  cmd.Parameters.AddWithValue()
            cmd.Parameters.AddWithValue("@UserOrOperatorId", userOrOperatorId);
            #endregion
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        public static int GetCountOfOutMessages(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" select COUNT(*) allrows from t_Forum_OutMessage" + conn.SiteId + " where FromUserId=@UserOrOperatorId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@UserOrOperatorId", userOrOperatorId);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        public static void DeleteOutMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int OutMessageId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("delete t_Forum_OutMessage" + conn.SiteId + " where Id = @OutMessageId;");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);           
            cmd.Parameters.AddWithValue("@OutMessageId", OutMessageId);
            cmd.ExecuteNonQuery();
        }
        public static int GetCountOfOutMessagesByTimeUnit(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId,DateTime Today)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select COUNT(*) allrows from t_Forum_outMessage" + conn.SiteId + " where FromUserId=@UserOrOperatorId and ");
            strSQL.Append("CreateDate >@Today");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@UserOrOperatorId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@Today",Today);
            
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

    }

}
