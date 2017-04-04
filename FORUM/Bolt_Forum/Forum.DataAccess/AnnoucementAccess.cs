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
    public class AnnoucementAccess
    {
        public static DataTable GetAllAnnoucementsOfForum(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int forumId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select a.*,b.*,c.Name as LastPostUserOrOperatorName,");
            strSQL.Append("c.IfDeleted as LastPostUserOrOperatorIfDeleted,d.Name as PostUserOrOperatorName,");
            strSQL.Append("d.IfDeleted as PostUserOrOperatorIfDeleted");
            strSQL.Append(string.Format(" from t_Forum_Topic{0} a,[t_Forum_Announcement] b,",conn.SiteId));
            strSQL.Append(string.Format("t_User{0} c,t_User{0} d",conn.SiteId));
            strSQL.Append(" where CategoryOrForumId=@forumId and b.SiteId=siteId and");
            strSQL.Append(" a.Id = b.TopicId and a.LastPostUserOrOperatorId = c.Id and a.PostUserOrOperatorId = d.Id");
            strSQL.Append(" order by a.PostTime");
            //strSQL.Append("select * from t_Forum_Topic200000 a,[t_Forum_Announcement] b");
            //strSQL.Append(" where CategoryOrForumId=@forumId and b.SiteId=@siteId and");
            //strSQL.Append(" a.Id = b.TopicId order by a.PostTime");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetAnnoucementsOfSiteByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            string subject, int forumId, int pageindex, int pageSize, string orderField, string orderMethod)
        {
            int startIndex = (pageindex - 1) * pageSize + 1;
            int endIndex = pageindex * pageSize;

            subject = CommonFunctions.SqlReplace(subject);

            if (orderField == "CreateUser")
                orderField = "c.Name";
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from");
            strSQL.Append(string.Format("(select ROW_NUMBER() over(order by {0} {1}) as row,", orderField, orderMethod));
            strSQL.Append(string.Format("a.*,c.Name from t_Forum_Post{0} a right join", conn.SiteId));
            strSQL.Append(string.Format("(select distinct TopicId from t_Forum_Announcement where SiteId={0}) b on a.TopicId = b.TopicId and a.IfTopic=1", conn.SiteId));
            strSQL.Append(string.Format(" left join [t_User{0}] c on a.PostUserOrOperatorId = c.id", conn.SiteId));
            if (forumId >= 0)
                strSQL.Append(string.Format(" left join t_Forum_Announcement d on a.Id=d.TopicId and d.SiteId={0}", conn.SiteId));
            strSQL.Append(" where 1=1");
            if (subject != "")
                strSQL.Append(" and ([Subject] like '%' + @subject + '%' escape '/' or Content like '%' + @subject + '%' escape '/')");
            if (forumId >= 0)
                strSQL.Append(" and d.CategoryOrForumId=@forumId");
            strSQL.Append(") t");
            strSQL.Append(" where row between @startIndex and @endIndex");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfAnnoucementsOfSiteByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            string subject, int forumId)
        {
            subject = CommonFunctions.SqlReplace(subject);

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select count (*) from t_Forum_Post{0} a right join", conn.SiteId));
            strSQL.Append(string.Format("(select distinct TopicId from t_Forum_Announcement where SiteId={0}) b on a.TopicId = b.TopicId and a.IfTopic=1", conn.SiteId));
            strSQL.Append(string.Format(" left join [t_User{0}] c on a.PostUserOrOperatorId = c.id", conn.SiteId));
            if (forumId >= 0)
                strSQL.Append(string.Format(" left join t_Forum_Announcement d on a.Id=d.TopicId and d.SiteId={0}", conn.SiteId));
            strSQL.Append(" where 1=1");
            if (subject != "")
                strSQL.Append(" and ([Subject] like '%' + @subject + '%' escape '/' or Content like '%' + @subject + '%' escape '/')");
            if (forumId >= 0)
                strSQL.Append(" and d.CategoryOrForumId=@forumId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int AddAnnoucement(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            string subject, int postUserOrOperatorId, DateTime PostTime)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("insert into t_Forum_Topic{0}", conn.SiteId));
            strSQL.Append("([Subject],[PostUserOrOperatorId],");
            strSQL.Append("[PostTime])");
            strSQL.Append(" values(@Subject,@PostUserOrOperatorId,@PostTime)");
            strSQL.Append("select @@identity");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Values
            cmd.Parameters.AddWithValue("@Subject", subject);
            cmd.Parameters.AddWithValue("@PostUserOrOperatorId", postUserOrOperatorId);
            cmd.Parameters.AddWithValue("@PostTime", PostTime);
            #endregion
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int UpdateAnnoucement(SqlConnectionWithSiteId conn, SqlTransaction transaction, int AnnoucementId,
           string subject, int postUserOrOperatorId, DateTime PostTime)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("Update [t_Forum_Topic{0}] set [Subject]=@subject,", conn.SiteId));
            strSQL.Append("[PostUserOrOperatorId]=@postUserOrOperatorId,[PostTime]=@PostTime");
            strSQL.Append(" where Id=@AnnoucementId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Values
            cmd.Parameters.AddWithValue("@Subject", subject);
            cmd.Parameters.AddWithValue("@PostUserOrOperatorId", postUserOrOperatorId);
            cmd.Parameters.AddWithValue("@PostTime", PostTime);
            
            cmd.Parameters.AddWithValue("@AnnoucementId", AnnoucementId);
            #endregion
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void AddForumsAndAnnoucementRelateion(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int AnnoucementId, int ForumId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" insert into [t_Forum_Announcement](TopicId,SiteId,CategoryOrForumId) values(@AnnoucementId,@siteId,@ForumId);");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Values
            cmd.Parameters.AddWithValue("@AnnoucementId", AnnoucementId);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@ForumId", ForumId);
            #endregion
            cmd.ExecuteNonQuery();
            //return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void DeleteAnnoucement(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int AnnoucementId)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("delete t_Forum_Topic{0} where id=@AnnoucementId;", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Values
            cmd.Parameters.AddWithValue("@AnnoucementId", AnnoucementId);
            #endregion
            cmd.ExecuteNonQuery();

        }

        public static void DeleteAllForumsAndAnnoucementRelationWithAnnouncement(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int AnnoucementId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("delete t_Forum_Announcement where TopicId=@AnnoucementId and SiteId=@siteId;");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Values
            cmd.Parameters.AddWithValue("@AnnoucementId", AnnoucementId);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            #endregion
            cmd.ExecuteNonQuery();
        }

        //public static void DeleteForumsAndAnnouncementRelation(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int topicId)
        //{
        //    StringBuilder strSQL = new StringBuilder();
        //    strSQL.Append("delete t_Forum_Announcement where TopicId=@topicId and CategoryOrForumId=@forumId ");
        //    SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
        //    cmd.Parameters.AddWithValue("@topicId", topicId);
        //    cmd.Parameters.AddWithValue("@forumId", forumId);
        //    cmd.ExecuteNonQuery();
        //}
        #region Moderator Panel
        public static DataTable GetAnnouncementsByModeratorWithQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int moderatorId,string subject, int pageIndex, int pageSize, string orderField, string orderDirection)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            subject = CommonFunctions.SqlReplace(subject);
            if (orderField == "CreateUser")
                orderField = "u.Name";
            /*
            select * from (
                select ROW_NUMBER() over(order by u.Name desc)as row ,t.*,u.Name
                from t_Forum_Topic200000 t 
                    inner join ( 
		                select distinct TopicId 
		                from t_Forum_Announcement a inner join t_Forum_Moderator m on a.CategoryOrForumId=m.ForumId 
		                where ModeratorId=1 )V on t.Id=V.TopicId 
		            left join t_User200000 u on t.PostUserOrOperatorId=u.Id
                where t.[Subject] like '%a%') returnV 
            where row between 1 and 3 
             */
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from (");
            strSQL.Append(string.Format(" select ROW_NUMBER() over(order by {0} {1})as row ,t.*,u.Name ", orderField, orderDirection));
            strSQL.Append(string.Format(" from t_Forum_Topic{0} t ", conn.SiteId));
            strSQL.Append(string.Format(" inner join ( "));
            strSQL.Append(string.Format(" select distinct TopicId "));
            strSQL.Append(string.Format(" from t_Forum_Announcement a inner join t_Forum_Moderator m on a.CategoryOrForumId=m.ForumId "));
            strSQL.Append(string.Format(" where UserOrOperatorId=@moderatorId )V on t.Id=V.TopicId "));
            strSQL.Append(string.Format(" left join t_User{0} u on t.PostUserOrOperatorId=u.Id",conn.SiteId));
            strSQL.Append(string.Format(" where 1=1 "));
            if (subject != "")
                strSQL.Append(" and t.[Subject] like '%' + @subject + '%' escape '/'");
            strSQL.Append(" ) returnV " );
            strSQL.Append(" where row between @startIndex and @endIndex");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        public static int GetCountOfAnnouncementsByModeratorWithQuery(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int moderatorId, string subject)
        {
            subject = CommonFunctions.SqlReplace(subject);
            StringBuilder strSQL = new StringBuilder();
            /*
                select COUNT(*)
                from t_Forum_Topic200000 t 
	                inner join (
		                select distinct TopicId
		                from t_Forum_Announcement a inner join t_Forum_Moderator m on a.CategoryOrForumId=m.ForumId
		                where ModeratorId=1 )V on t.Id=V.TopicId
	                left join t_User200000 u on t.PostUserOrOperatorId=u.Id
	            where 1=1 and t.[Subject] like '%a%' 
             
             */
            strSQL.Append(string.Format(" select COUNT(*) "));
            strSQL.Append(string.Format(" from t_Forum_Topic{0} t ", conn.SiteId));
            strSQL.Append(string.Format(" inner join ( "));
            strSQL.Append(string.Format(" select distinct TopicId "));
            strSQL.Append(string.Format(" from t_Forum_Announcement a inner join t_Forum_Moderator m on a.CategoryOrForumId=m.ForumId "));
            strSQL.Append(string.Format(" where UserOrOperatorId=@moderatorId )V on t.Id=V.TopicId "));
            strSQL.Append(string.Format(" left join t_User{0} u on t.PostUserOrOperatorId=u.Id ", conn.SiteId));
            strSQL.Append(string.Format(" where 1=1 "));
            if (subject != "")
                strSQL.Append(" and t.[Subject] like '%' + @subject + '%' escape '/'");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        public static void DeleteForumsAndAnnouncementRelationByModeratorWithAnnouncement(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId,int moderatorId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("delete t_Forum_Announcement where TopicId=@topicId ");
            strSQL.Append(" and CategoryOrForumId in (select ForumId from t_Forum_Moderator where UserOrOperatorId=@moderatorId)");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);
            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);
            cmd.ExecuteNonQuery();
        }

        public static void DeleteForumsAndAnnouncementRelation(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int topicId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("delete t_Forum_Announcement where TopicId=@topicId and CategoryOrForumId=@forumId and SiteId={0}", conn.SiteId));
            //strSQL.Append(" and CategoryOrForumId in (select ForumId from t_Forum_Moderator where UserOrOperatorId=@moderatorId)");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.ExecuteNonQuery();
        }

        public static bool CheckAnnouncementRelation(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select COUNT(*) from t_Forum_Announcement where TopicId=@topicId");
            SqlCommand cmd=new SqlCommand(strSQL.ToString(),conn.SqlConn,transaction);;
            cmd.Parameters.AddWithValue("@topicId", topicId);
            return Convert.ToInt32(cmd.ExecuteScalar())==0 ? false : true;
        } 
        #endregion
    }
}
