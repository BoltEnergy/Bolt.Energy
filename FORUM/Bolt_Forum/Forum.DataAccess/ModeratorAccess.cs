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
    public class ModeratorAccess
    {
        public static DataTable GetModeratorByUserOrOperatorIdAndForumId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int userOrOperatorId)
        {


            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append(" select a.ForumId,b.* from t_Forum_Moderator a ");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.UserOrOperatorId=b.Id");
            strSQL.Append(" where a.SiteId=@siteId and a.ForumId=@forumId and a.UserOrOperatorId=@userOrOperatorId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);


            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int AddModerator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int userOrOperatorId)
        {

            StringBuilder strSQL = new StringBuilder("insert into t_Forum_Moderator (SiteId, ForumId, UserOrOperatorId)");
            strSQL.Append(" values(@siteId, @forumId, @userOrOperatorId);");
            strSQL.Append(" select @@identity");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);

            return Convert.ToInt32(cmd.ExecuteScalar());

        }
        public static void Delete(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int userOrOperatorId)
        {

            StringBuilder strSQL = new StringBuilder("delete t_Forum_Moderator where SiteId=@siteId and ForumId = @forumId and UserOrOperatorId = @userOrOperatorId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);

            cmd.ExecuteNonQuery();
        }

        public static void DeleteAllModerators(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
        {

            StringBuilder strSQL = new StringBuilder("delete t_Forum_Moderator where SiteId=@siteId and ForumId = @forumId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            cmd.ExecuteNonQuery();
        }
        public static DataTable GetModeratorsByForumId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select ");
            strSQL.Append("moderator.Id as[ModerId],moderator.SiteId,moderator.ForumId,moderator.UserOrOperatorId,[user].* ");
            strSQL.Append("from t_Forum_Moderator moderator ");
            strSQL.Append(string.Format("join t_User{0} [user] ", conn.SiteId));
            strSQL.Append("on moderator.UserOrOperatorId = [user].Id and [user].IfDeleted='false'");
            strSQL.Append("where moderator.ForumId=@ForumId and [user].IfActive='true' and [user].IfDeleted='false' order by [user].Name asc");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ForumId", forumId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetModeratorByAnnoucementId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int annoucementId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select * from t_User{0} c, (select distinct UserOrOperatorId",conn.SiteId));
            strSQL.Append(" from [t_Forum_Moderator] a,[t_Forum_Announcement] b");
            strSQL.Append(string.Format(" where a.ForumId = b.CategoryOrForumId and a.SiteId={0} and b.SiteId={0} and b.TopicId=@annoucementId) d", conn.SiteId));
            strSQL.Append(" where c.Id = d.UserOrOperatorId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@annoucementId", annoucementId);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static bool IfModeratorOfAnnoucement(SqlConnectionWithSiteId conn, SqlTransaction transaction, int annoucementId,int operatoringUserOrOperatorId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select count(*)", conn.SiteId));
            strSQL.Append(" from [t_Forum_Moderator] a,[t_Forum_Announcement] b");
            strSQL.Append(string.Format(" where a.ForumId = b.CategoryOrForumId and a.SiteId={0} and b.SiteId={0} and b.TopicId=@annoucementId and a.UserOrOperatorId=@userId", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@annoucementId", annoucementId);
            cmd.Parameters.AddWithValue("@userId",operatoringUserOrOperatorId);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count != 0)
                return true;
            else
                return false;
        }

        public static bool IfModeratorOfForum(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int forumId, int operatoringUserOrOperatorId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select count(*)", conn.SiteId));
            strSQL.Append(" from [t_Forum_Moderator] a");
            strSQL.Append(string.Format(" where a.ForumId = @forumId and a.SiteId={0} and a.UserOrOperatorId=@userId", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.Parameters.AddWithValue("@userId", operatoringUserOrOperatorId);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count != 0)
                return true;
            else
                return false;
        }

        public static int GetCountOfModeratorsByUserOrOperatorId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        {

            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append(" select COUNT(a.Id) from t_Forum_Moderator a ");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.UserOrOperatorId=b.Id ");
            strSQL.Append(" where b.IfDeleted=0 and a.SiteId=@siteId and a.UserOrOperatorId=@userOrOperatorId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int GetCountOfModeratorsByUserOrOperatorIdAnForumId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, int forumId)
        {

            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append(" select COUNT(a.Id) from t_Forum_Moderator a ");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.UserOrOperatorId=b.Id ");
            strSQL.Append(" where b.IfDeleted=0 and a.SiteId=@siteId and a.UserOrOperatorId=@userOrOperatorId and a.ForumId=@forumId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
