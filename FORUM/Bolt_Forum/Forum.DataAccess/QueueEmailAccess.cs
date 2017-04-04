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
    public class QueueEmailAccess
    {
        public static int AddQueueEmail(SqlConnection conn, SqlTransaction transaction, int siteId, int postId, EnumQueueEmailType type, DateTime createDate)
        {
            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("insert t_Forum_QueueEmail(SiteId,PostId,Type,Status,CreateDate)");
            strSQL.Append("values(@siteId,@postId,@type,@status,@createDate);");
            strSQL.Append("select @@identity");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn, transaction);
            cmd.Parameters.AddWithValue("@siteId", siteId);
            cmd.Parameters.AddWithValue("@postId", postId);
            cmd.Parameters.AddWithValue("@type", Convert.ToInt16(type));
            cmd.Parameters.AddWithValue("@status", Convert.ToInt16(EnumQueueEmailStatus.Scheduled));
            cmd.Parameters.AddWithValue("@createDate", createDate);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable GetQueueEmailById(SqlConnection conn, SqlTransaction transaction, int id)
        {
            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("select * from t_Forum_QueueEmail where Id=@id");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn, transaction);
            cmd.Parameters.AddWithValue("@id", id);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static void UpdateQueueEmailStatus(SqlConnection conn, SqlTransaction transaction, int id, EnumQueueEmailStatus status)
        {
            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("update t_Forum_QueueEmail set Status=@status where Id=@id");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn, transaction);
            cmd.Parameters.AddWithValue("@status", Convert.ToInt16(status));
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public static void DeleteQueueEmail(SqlConnection conn, SqlTransaction transaction, int id)
        {
            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("delete t_Forum_QueueEmail  where Id=@id");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn, transaction);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public static DataTable GetAllScheduledQueueEmails(SqlConnection conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("select * from t_Forum_QueueEmail where Status=@scheduledStatus order by CreateDate asc");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn, transaction);
            cmd.Parameters.AddWithValue("@scheduledStatus", Convert.ToInt16(EnumQueueEmailStatus.Scheduled));

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
    }
}
