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
     public class SubscribeAccess
    {
         public static DataTable GetSubscribeByTopicIdAndUserId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, int userOrOperatorId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select b.Id TopicId, b.Subject, b.PostUserOrOperatorId, c.Name PostUserOrOperatorName,");
            strSQL.Append(" b.IfDeleted PostUserOrOperatorIfDeleted, b.PostTime, b.LastPostId, b.LastPostTime, b.LastPostUserOrOperatorId,");
            strSQL.Append(" d.Name LastPostUserOrOperatorName, d.IfDeleted LastPostUserOrOperatorIfDeleted, b.NumberOfReplies, b.NumberOfHits,");
            strSQL.Append(" a.UserId UserOrOperatorId, b.IfMarkedAsAnswer, b.IfClosed,b.forumId ForumId, b.ParticipatorIds, a.CreateDate SubscribeDate");
            strSQL.Append(" from (select * from t_Forum_Subscribe" + conn.SiteId + " where UserId = @UserOrOperatorId and TopicId = @TopicId) a");
            strSQL.Append(" inner join t_Forum_Topic" + conn.SiteId + " b on a.TopicId = b.Id");
            strSQL.Append(" inner join t_User" + conn.SiteId + " c on  b.PostUserOrOperatorId = c.Id");
            strSQL.Append(" inner join t_User" + conn.SiteId + " d on  b.LastPostUserOrOperatorId = d.Id ;");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@UserOrOperatorId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@TopicId", topicId);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
         public static DataTable GetSubscribesQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, int pageIndex, int pageSize, string keyword)
        {
            keyword = CommonFunctions.SqlReplace(keyword);
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select d.Id TopicId, d.Subject, d.PostUserOrOperatorId, e.Name  PostUserOrOperatorName,");
            strSQL.Append(" d.IfDeleted PostUserOrOperatorIfDeleted, d.PostTime, d.LastPostId, d.LastPostTime, d.LastPostUserOrOperatorId,");
            strSQL.Append(" f.Name LastPostUserOrOperatorName, d.IfDeleted LastPostUserOrOperatorIfDeleted, d.NumberOfReplies, d.NumberOfHits,");
            strSQL.Append(" d.forumId ForumId, d.UserId UserOrOperatorId, d.IfMarkedAsAnswer, d.IfClosed, d.ParticipatorIds, d.CreateDate SubscribeDate");
            strSQL.Append(" from (  select * from (  select  ROW_NUMBER() over(order by LastPostTime desc) row, * from");
            strSQL.Append(" ( select * from t_Forum_Subscribe" + conn.SiteId + " where UserId = @UserOrOperatorId ) a");
            strSQL.Append(" inner join t_Forum_Topic" + conn.SiteId + " b on a.TopicId = b.Id ");
            strSQL.Append(" and [Subject] like '%' + @Subject +'%' escape '/') c");
            strSQL.Append(string.Format(" where row between {0} and {1} ", startRowNum, endRowNum));           
            strSQL.Append(" ) d inner join t_User" + conn.SiteId + " e on  d.PostUserOrOperatorId =e.Id");
            strSQL.Append(" inner join t_User" + conn.SiteId + " f on d.LastPostUserOrOperatorId = f.Id;");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@UserOrOperatorId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@Subject", keyword);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        public static int GetCountOfSubscribes(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, string keyword)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select  COUNT(*) allrows from (select * from t_Forum_Subscribe" + conn.SiteId + " where UserId=@UserOrOperatorId) a ");
            strSQL.Append(" inner join t_Forum_Topic" + conn.SiteId + " b on a.TopicId = b.Id");
            if (keyword != "")
                strSQL.Append(" where  [Subject] like '%' + @Subject +'%' escape '/' ;");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@UserOrOperatorId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@Subject", keyword);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        public static void DeleteSubscribe(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, int userOrOperatorId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("delete t_Forum_Subscribe" + conn.SiteId + " where UserId=@UserOrOperatorId and TopicId=@TopicId;");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@UserOrOperatorId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@TopicId", topicId);         
            cmd.ExecuteNonQuery();
        }
        public static void AddSubscribe(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, int userOrOperatorId)
        {
            //StringBuilder strSQL = new StringBuilder();
            //strSQL.Append(string.Format("insert into [t_Forum_Subscribe{0}] values(@topicId,@userId,@date)",conn.SiteId));
            //SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            //cmd.Parameters.AddWithValue("@userId", userOrOperatorId);
            //cmd.Parameters.AddWithValue("@topicId", topicId);
            //cmd.Parameters.AddWithValue("@date", DateTime.UtcNow);
            //cmd.ExecuteNonQuery();
        }
        public static bool IfUserSubscribeTopic(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int topicId, int userOrOperatorId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select top 1 * from [t_Forum_Subscribe{0}] where TopicId=@topicId and UserId=@userId",conn.SiteId));
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            SqlDataReader dr = cmd.ExecuteReader();
            bool result = dr.HasRows;
            dr.Close();
            return result;
        }

        public static int GetCountOfSubscribesByTopicId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select count(*) from t_Forum_Subscribe" + conn.SiteId + " where TopicId=@topicId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable GetUserOrOperatorsByTopicId(
            SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select * from t_User{0} a, (select UserId From [t_Forum_Subscribe{0}] where TopicId = @topicId)b ",conn.SiteId));
            strSQL.Append("where a.Id = b.UserId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
    }
}
