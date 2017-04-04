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
    public class QueueEmailRecipientsAccess
    {
        public static DataTable GetAllRecipientsByQueueEmailId(SqlConnection conn, SqlTransaction transaction, int queueEmailId)
        {
            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("select * from t_Forum_QueueEmailRecipients where QueueEmailId=@queueEmailId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn, transaction);
            cmd.Parameters.AddWithValue("@queueEmailId", queueEmailId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static void CreateRecipientsList(SqlConnection conn, SqlTransaction transaction, int siteId, int topicId, int queueEmailId)
        {
            StringBuilder strSQL = null;
            SqlCommand cmd = null;
            #region Get subscriber emails
            string SiteDataBaseName = DbHelper.GetDBName(siteId);
            strSQL = new StringBuilder("");
            strSQL.Append(" select b.Email into #emails from " + SiteDataBaseName+ ".dbo." + "t_Forum_Subscribe" + siteId + " a ");
            strSQL.Append(" left join " + SiteDataBaseName + ".dbo." + "t_User" + siteId + " b on a.UserId=b.Id ");
            strSQL.Append(" where a.TopicId=" + topicId);
            cmd = new SqlCommand(strSQL.ToString(), conn);
            cmd.ExecuteNonQuery();
            #endregion Get subscriber emails

            #region Insert to recipients
            strSQL = new StringBuilder("");
            strSQL.Append(" select MIN(Email) as [Email] into #recipients from #emails group by Email ");
            strSQL.Append(" insert t_Forum_QueueEmailRecipients(QueueEmailId,Email)values(@queueEmailId,(select * from #recipients)) ");
            cmd = new SqlCommand(strSQL.ToString(), conn, transaction);
            cmd.Parameters.AddWithValue("@queueEmailId", queueEmailId);
            cmd.ExecuteNonQuery();
            #endregion Insert to recipients
        }

        public static void DeleteRecipient(SqlConnection conn, SqlTransaction transaction, int queueEmailId, string emailAddress)
        {
            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("delete t_Forum_QueueEmailRecipients where QueueEmailId=@queueEmailId and Email=@emailAddress");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn, transaction);
            cmd.Parameters.AddWithValue("@queueEmailId", queueEmailId);
            cmd.Parameters.AddWithValue("@emailAddress", emailAddress);

            cmd.ExecuteNonQuery();
        }

        public static void DeleteRecipientsByQueueEmailId(SqlConnection conn, SqlTransaction transaction, int queueEmailId)
        {
            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("delete t_Forum_QueueEmailRecipients where QueueEmailId=@queueEmailId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn, transaction);
            cmd.Parameters.AddWithValue("@queueEmailId", queueEmailId);

            cmd.ExecuteNonQuery();
        }
    }
}
