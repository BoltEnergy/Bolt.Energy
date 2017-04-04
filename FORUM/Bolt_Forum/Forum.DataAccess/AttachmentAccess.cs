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
    public class AttachmentAccess
    {
        public static bool IfHasAttachment(
          SqlConnectionWithSiteId conn, SqlTransaction transaction,
          int postId,int type)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select top 1 * from [t_Forum_Attachment{0}] where AttachId=@postId and type=@type", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Values
            cmd.Parameters.AddWithValue("@postId", postId);
            cmd.Parameters.AddWithValue("@type", type);
            #endregion
            SqlDataReader sdr = cmd.ExecuteReader();
            bool result = sdr.HasRows;
            sdr.Close();
            return result;
        }

        public static DataTable GetAttachmentById(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int Id)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select * from [t_Forum_Attachment{0}] where Id=@Id",conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Values
            cmd.Parameters.AddWithValue("@Id", Id);
            #endregion

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return dt;
        }

        public static int AddAttachement(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int postId, int uploadUserOrOperatorId, 
            int size, byte[] attachment,
            bool ifPayScoreRequired, int score,
            string name,string description,Guid guid,int type)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("insert into [t_Forum_Attachment{0}](AttachId,OriginalName,Attachment,Size,UserId,IfPayScoreRequired,Score,Description,Type,Guid)", conn.SiteId));
            strSQL.Append(" values(@postId,@name,@attachment,@size,@uploadUserOrOperatorId,@ifPayScoreRequired,@score,@description,@type,@guid)");
            strSQL.Append(";select @@IDENTITY");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            #region Add Values
            cmd.Parameters.AddWithValue("@postId", postId);
            cmd.Parameters.AddWithValue("@uploadUserOrOperatorId", uploadUserOrOperatorId);
            cmd.Parameters.AddWithValue("@size", size);
            cmd.Parameters.AddWithValue("@attachment", attachment);
            cmd.Parameters.AddWithValue("@ifPayScoreRequired", ifPayScoreRequired);
            cmd.Parameters.AddWithValue("@score", score);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@guid", guid.ToString().Replace("-", ""));
            #endregion

           return Convert.ToInt32(Convert.ToInt32(cmd.ExecuteScalar()));
        }

        public static void UpdateAttachment(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int Id,int topicId, int scroe,string description,int type)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("update [t_Forum_Attachment{0}]",conn.SiteId));
            strSQL.Append(" set AttachId=@topicId, score=@scroe, [Description]=@description,[Type]=@type");
            strSQL.Append(" where Id=@Id");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Values
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@topicId", topicId);
            cmd.Parameters.AddWithValue("@scroe", scroe);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@type", type);
            #endregion

            cmd.ExecuteNonQuery();
        }

        public static void DeleteAttahcment(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int Id)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("delete [t_Forum_Attachment{0}] where Id=@Id", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Values
            cmd.Parameters.AddWithValue("@Id", Id);
            #endregion

            cmd.ExecuteNonQuery();
        }

        //public static void DeleteAttachmentHistory(SqlConnectionWithSiteId conn, SqlTransaction transaction,
        //    int Id)
        //{
        //    StringBuilder strSQL = new StringBuilder();

        //    strSQL.Append("delete [t_Forum_AttachmentPayHistory200000] where AttachmentId=@Id");

        //    SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
        //    #region Add Values
        //    cmd.Parameters.AddWithValue("@Id", Id);
        //    #endregion

        //    cmd.ExecuteNonQuery();
        //}


        public static DataTable GetAllAttachmentsOfPost(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int postId,int type)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select * from [t_Forum_Attachment{0}] where AttachId=@postId and type=@type order by id", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Values
            cmd.Parameters.AddWithValue("@postId", postId);
            cmd.Parameters.AddWithValue("@type", type);
            #endregion

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return dt;
        }

        public static DataTable GetAllTempAttachmentsByUser(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int userId,Guid guid,int type)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select * from [t_Forum_Attachment{0}] where UserId=@userId and AttachId<=0 and [GUID]=@guid and type=@type order by id", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Values
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@guid", guid.ToString().Replace("-", ""));
            cmd.Parameters.AddWithValue("@type", type);
            #endregion

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return dt;
        }

        public static DataTable GetAllTempAttachmentsByUser(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int userId, int type)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select * from [t_Forum_Attachment{0}] where UserId=@userId and AttachId<=0 and type=@type order by id", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Values
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@type", type);
            #endregion

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return dt;
        }
    }
}
