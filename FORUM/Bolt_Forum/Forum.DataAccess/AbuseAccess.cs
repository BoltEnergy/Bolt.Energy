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
    public class AbuseAccess
    {
        public static DataTable GetAbuseById(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select a.*,b.*,c.Name as AbuseUserOrOperatorName,d.Name as PostUserOrOperatorName");
            strSQL.Append(string.Format(" from t_Forum_Abuse a, t_Forum_Post{0} b,t_User{0} c,t_User{0} d",conn.SiteId));
            strSQL.Append(" where a.PostId = b.Id and a.UserOrOperatorId = c.Id and b.PostUserOrOperatorId=d.Id");
            strSQL.Append(" and a.Id = @id");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@id", id);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int AddAbuse(SqlConnectionWithSiteId conn, SqlTransaction transaction, 
            int postId, int abuseUserOrOperatorId, string note, DateTime abuseDate)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("insert into [t_Forum_Abuse](SiteId,PostId,UserOrOperatorId,Status,Date,Note)");
            strSQL.Append(" values(@siteId,@postId,@abuseUserId,@status,@abuseDate,@note);select @@IDENTITY;");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@postId", postId);
            cmd.Parameters.AddWithValue("@abuseUserId", abuseUserOrOperatorId);
            cmd.Parameters.AddWithValue("@abuseDate", abuseDate);
            cmd.Parameters.AddWithValue("@note", note);
            cmd.Parameters.AddWithValue("@status", Convert.ToInt32(EnumAbuseStatus.Pending));

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void UpdateAbuseStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int id, EnumAbuseStatus status)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("update [t_Forum_Abuse]");
            strSQL.Append(" set [Status]=@status where Id=@id and SiteId=@siteId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@status", Convert.ToInt32(status));
            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();
        }

        public static int GetCountOfAbusesByPostIdAndPostUserOrOperatorId(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int postId, int postUserOrOperatorId)
        {
            return 0;
        }

        public static bool IfPostAbusedHasStuats(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int postId,EnumAbuseStatus status)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select top 1 * from t_Forum_Abuse where PostId=@postId and SiteId=@siteId and [Status]=@status");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@postId", postId);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@status", Convert.ToInt32(status));

            SqlDataReader dr = cmd.ExecuteReader();
            bool result = dr.HasRows;
            dr.Close();
            return result;
        }

        public static DataTable GetAllAbusesOfPost(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int postId)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select * from t_Forum_Abuse where PostId=@postId and SiteId=@siteId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@postId", postId);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return dt;
        }

        public static DataTable GetAbusesByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            string keyword, EnumAbuseStatus status, bool ifAllStatus, int pageIndex, int pageSize,
            string orderField, string orderMethod)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            if (orderField == "AbuseUserOrOperatorName")
                orderField = "b.Name";
            else if (orderField == "PostUserOrOperatorName")
                orderField = "c.Name";
            string subject = CommonFunctions.SqlReplace(keyword);

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from");
            strSQL.Append(string.Format("(select ROW_NUMBER() over(order by {0} {1}) as row,", orderField, orderMethod));
            strSQL.Append(" c.[Subject],a.*,c.PostUserOrOperatorId as PostUserOrOperatorId");
            strSQL.Append(string.Format(",b.Name as AbuseUserOrOperatorName,c.Name as PostUserOrOperatorName from t_Forum_Abuse a, t_User{0} b, ", conn.SiteId));
            strSQL.Append(string.Format("(select d.Name, f.* from t_User{0} d,t_Forum_Post{0} f where d.Id = f.PostUserOrOperatorId) c, t_Forum_Topic{0} d", conn.SiteId));
            strSQL.Append(" where a.PostId = c.Id and a.UserOrOperatorId = b.Id and c.IfDeleted='false' and ((c.TopicId=d.Id and d.IfDeleted='false') or (c.TopicId=d.Id and c.IfTopic='true'))");

            if (!ifAllStatus)
                strSQL.Append(" and [Status]=@Status");
            if (subject != "")
                strSQL.Append(" and c.[Subject] like '%' + @subject +'%' escape '/'");
            strSQL.Append(") t where row between @startIndex and @endIndex");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);
            cmd.Parameters.AddWithValue("@orderField", orderField);
            cmd.Parameters.AddWithValue("@orderMethod", orderMethod);
            cmd.Parameters.AddWithValue("@Status", Convert.ToInt32(status));

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfAbusesByQueryAndPaging(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
             string keyword, EnumAbuseStatus status, bool ifAllStatus)
        {
            string subject = CommonFunctions.SqlReplace(keyword);

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select count(*) from t_Forum_Abuse a, t_User{0} b, ", conn.SiteId));
            strSQL.Append(string.Format("(select d.Name, f.* from t_User{0} d,t_Forum_Post{0} f where d.Id = f.PostUserOrOperatorId) c, t_Forum_Topic{0} d", conn.SiteId));
            strSQL.Append(" where a.PostId = c.Id and a.UserOrOperatorId = b.Id and c.IfDeleted='false' and ((c.TopicId=d.Id and d.IfDeleted='false') or (c.IfTopic='true' and c.TopicId=d.Id))");

            if (!ifAllStatus)
                strSQL.Append(" and [Status]=@Status");
            if (subject != "")
                strSQL.Append(" and c.[Subject] like '%' + @subject +'%' escape '/'");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@Status", Convert.ToInt32(status));

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable GetAbusesByModeratorWithQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction,int moderatorId,
            string keyword, EnumAbuseStatus status, bool ifAllStatus, int pageIndex, int pageSize, string orderField, string orderMethod)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            if (orderField == "AbuseUserOrOperatorName")
                orderField = "b.Name";
            else if (orderField == "PostUserOrOperatorName")
                orderField = "c.Name";
            string subject = CommonFunctions.SqlReplace(keyword);

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from");
            strSQL.Append(string.Format("(select ROW_NUMBER() over(order by {0} {1}) as row,", orderField, orderMethod));
            strSQL.Append(" c.[Subject],a.*,b.Id as PostUserOrOperatorId");
            strSQL.Append(string.Format(",b.Name as AbuseUserOrOperatorName,c.Name as PostUserOrOperatorName from t_Forum_Abuse a, t_User{0} b, t_Forum_Topic{0} p, ", conn.SiteId));
            strSQL.Append(string.Format("(select d.Name, f.* from t_User{0} d,t_Forum_Post{0} f where d.Id = f.PostUserOrOperatorId and exists (select ForumId from t_Forum_Moderator m where  m.ForumId=f.ForumId and m.UserOrOperatorId=@moderatorId)) c", conn.SiteId));
            strSQL.Append(" where a.PostId = c.Id and a.UserOrOperatorId = b.Id and c.IfDeleted='false' and p.Id=c.TopicId and p.IfDeleted='false'");

            if (!ifAllStatus)
                strSQL.Append(" and [Status]=@Status");
            if (subject != "")
                strSQL.Append(" and [Subject] like '%' + @subject +'%' escape '/'");
            strSQL.Append(") t where row between @startIndex and @endIndex");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);
            cmd.Parameters.AddWithValue("@orderField", orderField);
            cmd.Parameters.AddWithValue("@orderMethod", orderMethod);
            cmd.Parameters.AddWithValue("@Status", Convert.ToInt32(status));

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfAbusesByModeratorWithQuery(SqlConnectionWithSiteId conn, SqlTransaction transaction, int moderatorId,
            string keyword, EnumAbuseStatus status, bool ifAllStatus)
        {
            string subject = CommonFunctions.SqlReplace(keyword);

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select count(*) from t_Forum_Abuse a, t_User{0} b, t_Forum_Topic{0} p, ", conn.SiteId));
            strSQL.Append(string.Format("(select d.Name, f.* from t_User{0} d,t_Forum_Post{0} f where d.Id = f.PostUserOrOperatorId and exists (select * from t_Forum_Moderator m where  m.ForumId=f.ForumId and m.UserOrOperatorId=@moderatorId)) c", conn.SiteId));
            strSQL.Append(" where a.PostId = c.Id and a.UserOrOperatorId = b.Id and c.IfDeleted='false' and p.Id=c.TopicId and p.IfDeleted='false'");

            if (!ifAllStatus)
                strSQL.Append(" and [Status]=@Status");
            if (subject != "")
                strSQL.Append(" and [Subject] like '%' + @subject +'%' escape '/'");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@Status", Convert.ToInt32(status));

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int GetCountOfNotCanceledAbusesByPostId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId)
        {
            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("select count(Id) from t_Forum_Abuse where SiteId=@siteId and PostId=@postId and Status<>@refused");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@postId", postId);
            cmd.Parameters.AddWithValue("@refused", EnumAbuseStatus.Refused);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int GetCountOfAbusesByPostId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId)
        {
            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("select count(Id) from t_Forum_Abuse where SiteId=@siteId and PostId=@postId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@postId", postId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable GetAbusesByPostId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId)
        {
            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("select a.*,b.Name as [AbuseUserOrOperatorName],");
            strSQL.Append(" c.[Subject],c.PostUserOrOperatorId,c.PostUserOrOperatorName ");
            strSQL.Append(" from t_Forum_Abuse a ");
            strSQL.Append(" inner join t_User" + conn.SiteId + " b on a.UserOrOperatorId=b.Id ");
            strSQL.Append(" inner join ");
            strSQL.Append(" (select a.Id as [PostId],a.[Subject],a.PostUserOrOperatorId, ");
            strSQL.Append(" b.Name as [PostUserOrOperatorName],a.PostTime from t_Forum_Post" + conn.SiteId + " a ");
            strSQL.Append(" inner join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId=b.Id ");
            strSQL.Append(" where a.IfDeleted=0) c on a.PostId=c.PostId");
            strSQL.Append(" where a.PostId=@postId ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@postId", postId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetAbusesOfUserByPostId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId,int userId)
        {
            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("select a.*,b.Name as [AbuseUserOrOperatorName],");
            strSQL.Append(" c.[Subject],c.PostUserOrOperatorId,c.PostUserOrOperatorName ");
            strSQL.Append(" from t_Forum_Abuse a ");
            strSQL.Append(" inner join t_User" + conn.SiteId + " b on a.UserOrOperatorId=b.Id ");
            strSQL.Append(" inner join ");
            strSQL.Append(" (select a.Id as [PostId],a.[Subject],a.PostUserOrOperatorId, ");
            strSQL.Append(" b.Name as [PostUserOrOperatorName],a.PostTime from t_Forum_Post" + conn.SiteId + " a ");
            strSQL.Append(" inner join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId=b.Id ");
            strSQL.Append(" where a.IfDeleted=0) c on a.PostId=c.PostId");
            strSQL.Append(" where a.PostId=@postId and a.[UserOrOperatorId]=@userId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@postId", postId);
            cmd.Parameters.AddWithValue("@userId",userId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
    }
}
