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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Database;


namespace Com.Comm100.Forum.DataAccess
{
    public class DraftAccess
    {
        public static int AddDraft(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, string subject, string content, int createOperatorId, DateTime createTime)
        {

            StringBuilder strSQL = new StringBuilder("insert into t_Forum_Draft"+conn.SiteId);
            strSQL.Append("(TopicId,Subject,Content,CreateOperatorId, CreateTime, LastUpdateOperatorId, LastUpdateTime) values (@topicId,@subject,@content,@createOperatorId,@createTime,@lastUpdateOperatorId,@lastUpdateTime);");
            strSQL.Append("select @@identity");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(),conn.SqlConn,transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@content", content);
            cmd.Parameters.AddWithValue("@createOperatorId", createOperatorId);
            cmd.Parameters.AddWithValue("@createTime", createTime);
            cmd.Parameters.AddWithValue("@lastUpdateOperatorId", createOperatorId);
            cmd.Parameters.AddWithValue("@lastUpdateTime", createTime);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void UpdateDraft(SqlConnectionWithSiteId conn, SqlTransaction transaction, int draftId, string subject, string content, int updateOperatorId, DateTime updateTime)
        {

            StringBuilder strSQL = new StringBuilder("update t_Forum_Draft"+conn.SiteId);
            strSQL.Append(" set Subject=@subject, Content=@content, LastUpdateOperatorId=@updateOperatorId, LastUpdateTime=@updateTime where Id=@draftId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(),conn.SqlConn,transaction);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@content", content);
            cmd.Parameters.AddWithValue("@updateOperatorId", updateOperatorId);
            cmd.Parameters.AddWithValue("@updateTime", updateTime);
            cmd.Parameters.AddWithValue("@draftId", draftId);

            cmd.ExecuteNonQuery();
        }

        public static void DeleteDraftByDraftId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int draftId)
        {

            StringBuilder strSQL = new StringBuilder("delete t_Forum_Draft" + conn.SiteId);
            strSQL.Append(" where Id = @draftId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@draftId", draftId);


            cmd.ExecuteNonQuery();
        }

        public static void DeleteDraftByTopicId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {

            StringBuilder strSQL = new StringBuilder("delete t_Forum_Draft" + conn.SiteId);
            strSQL.Append(" where TopicId = @topicId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);


            cmd.ExecuteNonQuery();
        }

        public static void DeleteDraftsByForumId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
        {

            StringBuilder strSQL = new StringBuilder("delete t_Forum_Draft"+conn.SiteId);
            strSQL.Append(" from t_Forum_Draft" + conn.SiteId + " a, t_Forum_Forum b,t_Forum_Topic" + conn.SiteId + " c");
            strSQL.Append(" where a.TopicId=c.Id and b.Id = c.ForumId=@forumId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);


            cmd.ExecuteNonQuery();
        }

        public static DataTable GetDraftByDraftId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int draftId)
        {

            StringBuilder strSQL = new StringBuilder("select a.*,b.Name as LastUpdateOperatorName,c.Name as CreateOperatorName from t_Forum_Draft" + conn.SiteId);
            strSQL.Append(" a left join t_User" + conn.SiteId + " b on a.LastUpdateOperatorId=b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.CreateOperatorId=c.Id where a.Id=@draftId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(),conn.SqlConn,transaction);
            cmd.Parameters.AddWithValue("@draftId", draftId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetDraftByTopicId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {

            StringBuilder strSQL = new StringBuilder("select a.*,b.Name as LastUpdateOperatorName,c.Name as CreateOperatorName from t_Forum_Draft" + conn.SiteId);
            strSQL.Append(" a left join t_User" + conn.SiteId + " b on a.LastUpdateOperatorId=b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.CreateOperatorId=c.Id where a.TopicId=@topicId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId",topicId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(),LoadOption.Upsert);
            return table;
        }
    }
}
