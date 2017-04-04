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
    public class FavoriteAccess
    {
        public static DataTable GetFavoritesByTopicIdAndUserId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, int userOrOperatorId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select b.Id TopicId, b.Subject, b.PostUserOrOperatorId, c.Name PostUserOrOperatorName,");
            strSQL.Append(" b.IfDeleted PostUserOrOperatorIfDeleted, b.PostTime, b.LastPostId, b.LastPostTime, b.LastPostUserOrOperatorId,");
            strSQL.Append(" d.Name LastPostUserOrOperatorName, d.IfDeleted LastPostUserOrOperatorIfDeleted, b.NumberOfReplies, b.NumberOfHits,");
            strSQL.Append(" a.UserId UserOrOperatorId, a.ForumId as CurrentForumId , b.IfMarkedAsAnswer, b.IfClosed, b.ParticipatorIds");
            strSQL.Append(" from (select * from t_Forum_Favorite" + conn.SiteId + " where UserId = @UserOrOperatorId and TopicId = @TopicId) a");
            strSQL.Append(" inner join t_Forum_Topic" + conn.SiteId + " b on a.TopicId = b.Id");
            strSQL.Append(" inner join t_User" + conn.SiteId + " c on  b.PostUserOrOperatorId = c.Id");
            strSQL.Append(" inner join t_User" + conn.SiteId + " d on  b.LastPostUserOrOperatorId = d.Id ;");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region  cmd.Parameters.AddWithValue()
            cmd.Parameters.AddWithValue("@TopicId", topicId);
            cmd.Parameters.AddWithValue("@UserOrOperatorId", userOrOperatorId);
            #endregion
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;

        }
        public static DataTable GetFavoritesAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, int pageIndex, int pageSize)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select d.Id TopicId, d.Subject, d.PostUserOrOperatorId, e.Name  PostUserOrOperatorName,");
            strSQL.Append(" e.IfDeleted PostUserOrOperatorIfDeleted, d.PostTime, d.LastPostId, d.LastPostTime, d.LastPostUserOrOperatorId,");
            strSQL.Append(" f.Name LastPostUserOrOperatorName, f.IfDeleted LastPostUserOrOperatorIfDeleted, d.NumberOfReplies, d.NumberOfHits,");
            strSQL.Append(" d.PostUserOrOperatorId UserOrOperatorId, d.CurrentForumId , d.IfMarkedAsAnswer, d.IfClosed, d.ParticipatorIds");
            strSQL.Append(" from (  select * from (  select  ROW_NUMBER() over(order by LastPostTime desc) row, a.ForumId as CurrentForumId,b.* from");
            strSQL.Append(" ( select * from t_Forum_Favorite" + conn.SiteId + " where UserId = @UserOrOperatorId ) a");
            strSQL.Append(" inner join t_Forum_Topic" + conn.SiteId + " b on a.TopicId = b.Id and b.IfDeleted =0 ) c");
            strSQL.Append(string.Format(" where row between {0} and {1}) d", startRowNum, endRowNum));
            strSQL.Append(" inner join t_User" + conn.SiteId + " e on  d.PostUserOrOperatorId =e.Id");
            strSQL.Append(" inner join t_User" + conn.SiteId + " f on d.LastPostUserOrOperatorId = f.Id;");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region  cmd.Parameters.AddWithValue()
            cmd.Parameters.AddWithValue("@UserOrOperatorId", userOrOperatorId);
            #endregion
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        public static int GetCountOfFavorites(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" select COUNT(*) allrows from t_Forum_Favorite" + conn.SiteId + " a");
            strSQL.Append(" inner join t_Forum_Topic" + conn.SiteId + " b on a.UserId=@UserOrOperatorId and a.TopicId = b.Id and b.IfDeleted = 0");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@UserOrOperatorId", userOrOperatorId);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        public static void DeleteFavorite(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, int topicId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("delete t_Forum_Favorite" + conn.SiteId + " where UserId=@UserOrOperatorId and TopicId = @TopicId;");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@UserOrOperatorId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@TopicId", topicId);
            cmd.ExecuteNonQuery();
        }

        public static void DeleteFavoriteByTopicId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("delete [t_Forum_Favorite{0}] where TopicId=@TopicId",conn.SiteId));
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@TopicId", topicId);
            cmd.ExecuteNonQuery();
        }

        public static void DeleteFavoriteByAnnoucementId(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int topicId,int forumId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("delete [t_Forum_Favorite{0}] where TopicId=@TopicId and ForumId=@ForumId", conn.SiteId));
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@TopicId", topicId);
            cmd.ExecuteNonQuery();
        }

        public static void AddFavorite(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int userOrOperatorId, int topicId, int forumId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("insert into [t_Forum_Favorite{0}](TopicId,UserId,ForumId) values(@topicId,@userId,@forumId)", conn.SiteId));
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@topicId", topicId);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.ExecuteNonQuery();
        }

        public static bool IfUserFavoriteTopic(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int userOrOperatorId, int topicId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select top 1 * from [t_Forum_Favorite{0}]", conn.SiteId));
            strSQL.Append(" where TopicId=@topicId and UserId=@userId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            SqlDataReader dr = cmd.ExecuteReader();
            bool result = dr.HasRows;
            dr.Close();
            return result;
        }

        public static int GetCountOfFavoritesByTopicId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select count(*) from t_Forum_Favorite" + conn.SiteId + " where TopicId=@topicId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

    }
}
