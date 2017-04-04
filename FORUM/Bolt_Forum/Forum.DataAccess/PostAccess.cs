#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Common;
using System.Collections.Generic;

namespace Com.Comm100.Forum.DataAccess
{
    public class PostAccess
    {
        public static DataTable GetPostsByTopicIdAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, int pageIndex, int pageSize,
            bool IfShowAbusingPost, bool IfShowWatingModerationPost,bool ifShowDeleted)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select a.*, b.Name PostUserOrOperatorName, b.Posts PostUserOrOperatorNumberOfPosts, b.joinedTime PostUserOrOperatorJoinedTime, b.IfDeleted IfPostUserOrOperatorDeleted, b.Signature, ");
            strSQL.Append("b.IfCustomizeAvatar IfPostUserOrOperatorCustomizeAvatar, b.SystemAvatar PostUserOrOperatorSystemAvatar, b.CustomizeAvatar PostUserOrOperatorCustomizeAvatar, ");
            strSQL.Append("c.Name as LastEditUserOrOperatorName,c.IfDeleted as IfLastEditUserOrOperatorDeleted ");
            strSQL.Append("from ( ");
            strSQL.Append("select * from ( ");
            strSQL.Append("select ");
            strSQL.Append("Row_Number() over(order by post.Layer asc ,post.Id desc) row, post.*");
            strSQL.Append(string.Format(" from t_Forum_Post{0} post ", conn.SiteId));
            strSQL.Append("where post.TopicId = @TopicId");
            if (!ifShowDeleted)
                strSQL.Append(" and post.IfDeleted != 'true'");
            if(!IfShowAbusingPost)
                strSQL.Append(string.Format(" and post.Id not in (select distinct PostId from t_Forum_Abuse where SiteId={0} and Status='0')",conn.SiteId));
            if (!IfShowWatingModerationPost)
                strSQL.Append(" and post.ModerationStatus <> '0'");
            strSQL.Append(" ) p ");
            strSQL.Append(string.Format("where row between {0} and {1} ", startRowNum, endRowNum));
            strSQL.Append(" ) a ");
            strSQL.Append(string.Format("left join t_User{0} b ", conn.SiteId));
            strSQL.Append("on b.Id = a.PostUserOrOperatorId ");
            strSQL.Append(string.Format("left join t_User{0} c ", conn.SiteId));
            strSQL.Append("on c.Id = a.LastEditUserOrOperatorId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@TopicId", topicId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }


        public static int GetCountOfPostByTopicId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId,
            bool IfShowAbusingPost, bool IfShowWatingModerationPost,bool ifShowDeleted)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select count(*)");
            strSQL.Append(" from t_Forum_Post" + conn.SiteId);
            strSQL.Append(" a");
            strSQL.Append(" where a.TopicId = @topicId");
            if (!ifShowDeleted)
                strSQL.Append(" and a.IfDeleted != 'true'");
             if(!IfShowAbusingPost)
                strSQL.Append(string.Format(" and a.Id not in (select distinct PostId from t_Forum_Abuse where SiteId={0} and Status='0')",conn.SiteId));
            if (!IfShowWatingModerationPost)
                strSQL.Append(" and a.ModerationStatus <> '0'");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            return Convert.ToInt32( cmd.ExecuteScalar());
        }

        public static DataTable GetPostsByTopicId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select a.*,b.Name as PostUserOroperatorName,b.Posts as PostUserOrOperatorNumberOfPosts,");
            strSQL.Append("b.joinedTime as PostUserOrOperatorJoinedTime,b.IfDeleted as IfPostUserOrOperatorDeleted,b.Signature,");
            strSQL.Append("b.IfCustomizeAvatar as IfPostUserOrOperatorCustomizeAvatar,b.SystemAvatar as PostUserOrOperatorSystemAvatar,");
            strSQL.Append("b.CustomizeAvatar as PostUserOrOperatorCustomizeAvatar,c.Name as LastEditUserOrOperatorName,c.IfDeleted as [ifLastEditUserOrOperatorDeleted]");
            strSQL.Append(" from t_Forum_Post" + conn.SiteId);
            strSQL.Append(" a left join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.LastEditUserOrOperatorId = c.Id");
            strSQL.Append(" where a.TopicId = @topicId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetNotDeletedPostsByTopicId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select a.*,b.Name as PostUserOroperatorName,b.Posts as PostUserOrOperatorNumberOfPosts,");
            strSQL.Append("b.joinedTime as PostUserOrOperatorJoinedTime,b.IfDeleted,b.Signature,");
            strSQL.Append("b.IfCustomizeAvatar as IfPostUserOrOperatorCustomizeAvatar,b.SystemAvatar as PostUserOrOperatorSystemAvatar,");
            strSQL.Append("b.CustomizeAvatar as PostUserOrOperatorCustomizeAvatar,c.Name as LastEditUserOrOperatorName,c.IfDeleted as [ifLastEditUserOrOperatorDeleted]");
            strSQL.Append(" from t_Forum_Post" + conn.SiteId);
            strSQL.Append(" a left join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.LastEditUserOrOperatorId = c.Id");
            strSQL.Append(" where a.TopicId = @topicId and a.[IfDeleted]='false'");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetPostByPostId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select a.*,b.Name as PostUserOroperatorName,b.Posts as PostUserOrOperatorNumberOfPosts,");
            strSQL.Append("b.joinedTime as PostUserOrOperatorJoinedTime,b.IfDeleted as IfPostUserOrOperatorDeleted,b.Signature,");
            strSQL.Append("b.IfCustomizeAvatar as IfPostUserOrOperatorCustomizeAvatar,b.SystemAvatar as PostUserOrOperatorSystemAvatar,");
            strSQL.Append("b.CustomizeAvatar as PostUserOrOperatorCustomizeAvatar,c.Name as LastEditUserOrOperatorName,c.IfDeleted as [ifLastEditUserOrOperatorDeleted]");
            //strSQL.Append("select a.*,b.Name as [postUserOrOperatorName],b.Posts, b.JoinedTime");
            //strSQL.Append(",b.IfCustomizeAvatar,b.SystemAvatar,b.CustomizeAvatar,b.IfDeleted,b.Signature,");
            //strSQL.Append("c.Name as [lastEditUserOrOperatorName],c.IfDeleted as [IfLastEditUserOrOperatorDeleted]");
            strSQL.Append(" from t_Forum_Post" + conn.SiteId.ToString() + " a");
            strSQL.Append(" left join t_User" + conn.SiteId.ToString() + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId.ToString() + " c on a.LastEditUserOrOperatorId = c.Id");
            strSQL.Append(" where a.Id = @postId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@postId", postId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetAnswerByTopicId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {

            StringBuilder strSQL = new StringBuilder("select a.*,b.Name as [postUserOrOperatorName],b.Posts, b.JoinedTime");
            strSQL.Append(",b.IfCustomizeAvatar,b.SystemAvatar,b.CustomizeAvatar,b.IfDeleted,b.Signature, c.Name as [lastEditUserOrOperatorName],c.IfDeleted as [ifLastEditUserOrOperatorDeleted] from t_Forum_Post" + conn.SiteId.ToString() + " a");
            strSQL.Append(" left join t_User" + conn.SiteId.ToString() + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId.ToString() + " c on a.LastEditUserOrOperatorId = c.Id");
            strSQL.Append(" where a.TopicId = @topicId and a.IfAnswer = 1");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static int AddPost(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId,
            bool ifTopic, string subject, int postUserOrOperatorId, DateTime postTime, string content,
            string textContent, int moderationStatus)
        {
            StringBuilder strSQL = null;

            SqlCommand cmd = null;

            strSQL = new StringBuilder("select ForumId from t_Forum_Topic" + conn.SiteId.ToString() + " where Id = @topicId");

            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            int forumId = Convert.ToInt32(cmd.ExecuteScalar());


            strSQL = new StringBuilder("select MAX(Layer) from t_Forum_Post" + conn.SiteId.ToString() + " where TopicId = @topicId");

            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            object maxLayer = cmd.ExecuteScalar();

            int layer = maxLayer is System.DBNull ? 1 : Convert.ToInt32(maxLayer) + 1;


            strSQL = new StringBuilder("Insert into t_Forum_Post" + conn.SiteId.ToString() + " (TopicId,ForumId,IfTopic,IfAnswer,Layer,Subject,Content,PostUserOrOperatorId,");
            strSQL.Append("PostTime,LastEditTime,LastEditUserOrOperatorId,TextContent,IfDeleted,ModerationStatus) ");
            strSQL.Append("values (@topicId,@forumId,@ifTopic,0,@layer,@subject,@content,@postUserOrOperatorId,");
            strSQL.Append("@postTime,@lastEditTime,@lastEditUserOrOperatorId,@textContent,@ifDeleted,@moderationStatus);");
            strSQL.Append("select @@identity");

            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.Parameters.AddWithValue("@ifTopic", ifTopic);
            cmd.Parameters.AddWithValue("@layer", layer);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@content", content);
            cmd.Parameters.AddWithValue("@postUserOrOperatorId", postUserOrOperatorId);
            cmd.Parameters.AddWithValue("@postTime", postTime);
            cmd.Parameters.AddWithValue("@lastEditTime", postTime);
            cmd.Parameters.AddWithValue("@lastEditUserOrOperatorId", postUserOrOperatorId);
            cmd.Parameters.AddWithValue("@textContent", textContent);
            cmd.Parameters.AddWithValue("@ifDeleted", false);
            cmd.Parameters.AddWithValue("@moderationStatus", moderationStatus);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void UpdatePost(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int postId, string subject, string content, int editUserOrOperatorId, DateTime editTime,
            EnumPostOrTopicModerationStatus moderationStatus)
        {

            StringBuilder strSQL = new StringBuilder("Update t_Forum_Post" + conn.SiteId.ToString() + " set");
            strSQL.Append(" Subject = @subject, Content = @content, LastEditTime = @lastEditTime, LastEditUserOrOperatorId = @lastEditUserOrOperatorId,");
            strSQL.Append(" ModerationStatus=@status where Id=@postId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@content", content);
            cmd.Parameters.AddWithValue("@lastEditTime", editTime);
            cmd.Parameters.AddWithValue("@lastEditUserOrOperatorId", editUserOrOperatorId);
            cmd.Parameters.AddWithValue("@status",(int)moderationStatus);
            cmd.Parameters.AddWithValue("@postId", postId);


            cmd.ExecuteNonQuery();
        }

        public static void SetPostSpam(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("update [t_Forum_Post{0}]",conn.SiteId));
            strSQL.Append(" set IfSpam='true' where id=@postId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@postId", postId);

            cmd.ExecuteNonQuery();
        }

        public static void DeletePost(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId)
        {

            StringBuilder strSQL = new StringBuilder("Delete t_Forum_Post" + conn.SiteId + " where Id = @postId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@postId", postId);

            cmd.ExecuteNonQuery();
        }

        public static void DeleteAllPosts(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {

            StringBuilder strSQL = new StringBuilder("Delete t_Forum_Post" + conn.SiteId + " where TopicId = @topicId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            cmd.ExecuteNonQuery();
        }

        public static void UpdatePostIfAnswerStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId, bool ifAnswer)
        {

            StringBuilder strSQL = new StringBuilder("Update t_Forum_Post" + conn.SiteId + " set");
            strSQL.Append(" IfAnswer = @ifAnswer");
            strSQL.Append(" where Id = @postId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ifAnswer", ifAnswer);
            cmd.Parameters.AddWithValue("@postId", postId);


            cmd.ExecuteNonQuery();
        }

        public static DataTable GetNextPost(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select top 1 * from ");
            strSQL.Append(string.Format("t_Forum_Post{0} ", conn.SiteId));
            strSQL.Append("where ");
            strSQL.Append("Id > @postId ");
            strSQL.Append("order by Id asc ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn);
            cmd.Parameters.AddWithValue("@postId", postId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return null;
        }

        public static DataTable GetFirstPostByTopicId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select a.*,b.Name as PostUserOroperatorName,b.Posts as PostUserOrOperatorNumberOfPosts,");
            strSQL.Append("b.joinedTime as PostUserOrOperatorJoinedTime,b.IfDeleted IfPostUserOrOperatorDeleted,b.Signature,");
            strSQL.Append("b.IfCustomizeAvatar as IfPostUserOrOperatorCustomizeAvatar,b.SystemAvatar as PostUserOrOperatorSystemAvatar,");
            strSQL.Append("b.CustomizeAvatar as PostUserOrOperatorCustomizeAvatar,c.Name as LastEditUserOrOperatorName,c.IfDeleted as [ifLastEditUserOrOperatorDeleted]");

            //strSQL.Append("select a.*,b.Name as [postUserOrOperatorName],b.Posts, b.JoinedTime");
            //strSQL.Append(",b.IfCustomizeAvatar,b.SystemAvatar,b.CustomizeAvatar,b.IfDeleted,b.Signature, c.Name as [lastEditUserOrOperatorName],c.IfDeleted as [IfLastEditUserOrOperatorDeleted]");
            strSQL.Append(" from t_Forum_Post" + conn.SiteId.ToString() + " a");
            strSQL.Append(" left join t_User" + conn.SiteId.ToString() + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId.ToString() + " c on a.LastEditUserOrOperatorId = c.Id");
            strSQL.Append(" where a.TopicId = @topicId and a.IfTopic = 1");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }
        public static DataTable GetLastPostByTopicId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {

            StringBuilder strSQL = new StringBuilder("select top 1 a.*,b.Name as [postUserOrOperatorName],b.Posts, b.JoinedTime");
            strSQL.Append(",b.IfCustomizeAvatar,b.SystemAvatar,b.CustomizeAvatar,b.IfDeleted as IfPostUserOrOperatorDeleted,b.Signature, c.Name as [lastEditUserOrOperatorName],c.IfDeleted as [ifLastEditUserOrOperatorDeleted] from t_Forum_Post" + conn.SiteId.ToString() + " a");
            strSQL.Append(" left join t_User" + conn.SiteId.ToString() + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId.ToString() + " c on a.LastEditUserOrOperatorId = c.Id");
            strSQL.Append(" where a.TopicId = @topicId order by Id desc");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetLastPostByForumId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
        {

            StringBuilder strSQL = new StringBuilder("select top 1 a.*,b.Name as [postUserOrOperatorName],b.Posts, b.JoinedTime");
            strSQL.Append(",b.IfCustomizeAvatar,b.SystemAvatar,b.CustomizeAvatar,b.IfDeleted,b.Signature, c.Name as [lastEditUserOrOperatorName],c.IfDeleted as [IfLastEditUserOrOperatorDeleted] from t_Forum_Post" + conn.SiteId.ToString() + " a");
            strSQL.Append(" left join t_User" + conn.SiteId.ToString() + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId.ToString() + " c on a.LastEditUserOrOperatorId = c.Id");
            strSQL.Append(" where a.ForumId = @forumId order by Id desc");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static void UpdateForumId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId, int forumId)
        {

            StringBuilder strSQL = new StringBuilder("Update t_Forum_Post" + conn.SiteId.ToString() + " set");
            strSQL.Append(" ForumId = @forumId where Id = @postId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.Parameters.AddWithValue("@postId", postId);

            cmd.ExecuteNonQuery();
        }

        public static int GetPostIndexInTopic(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId, int topicId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select row from ( ");
            strSQL.Append("select ");
            strSQL.Append("Row_Number() over(order by Layer asc ,post.Id desc) row, post.Id PostId ");
            strSQL.Append(string.Format("from t_Forum_Post{0} post ", conn.SiteId));
            strSQL.Append("where post.TopicId = @topicId ");
            strSQL.Append(") selectedPost ");
            strSQL.Append("where selectedPost.PostId = @postId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);
            cmd.Parameters.AddWithValue("@postId", postId);


            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        /*-----------------------2.0------------------------------*/
        public static DataTable GetNotDeletedPostsOfSiteByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, string queryConditions)
        {
            return null;
        }

        public static int GetCountOfNotDeletedPostsOfSiteByQuery(SqlConnectionWithSiteId conn, SqlTransaction transaction, string queryConditions)
        {
            return 0;
        }

        public static DataTable GetNotDeletedPostsOfForumByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, int forumId, string queryConditions)
        {
            return null;
        }

        public static int GetCountOfNotDeletedPostsOfForumByQuery(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, string queryConditions)
        {
            return 0;
        }

        public static DataTable GetNotDeletedPostsOfTopicByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, int topicId, string queryConditions)
        {
            return null;
        }

        public static int GetCountOfNotDeletedPostsOfTopicByQuery(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, string queryConditions)
        {
            return 0;
        }


        /*Topic list Replies Number*/
        public static int GetCountOfNotDeletedPostsByTopicId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {            
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select Count(*) from [t_Forum_Post{0}] ", conn.SiteId));
            strSQL.Append(" where TopicId = @topicId and IfDeleted='false' and ModerationStatus = 2 and IfTopic = 0");
            

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@topicId", topicId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        /*Forum list Posts Number*/
        public static int GetCountOfNotDeletedPostsByForumId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select Count(*) from [t_Forum_Post{0}] a, [t_Forum_Topic{0}] b ", conn.SiteId));
            strSQL.Append(" where a.TopicId = b.Id and a.ForumId = @forumId and a.IfDeleted='false' and a.ModerationStatus = 2 and b.IfMoveHistory = 0");


            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@forumId", forumId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        /*Forum list Topics Number*/
        public static int GetCountOfNotDeletedTopicsFirstPostByForumId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select Count(*) from [t_Forum_Post{0}] a, [t_Forum_Topic{0}] b ", conn.SiteId));
            strSQL.Append(" where a.TopicId = b.Id and a.ForumId = @forumId and a.IfDeleted='false' and a.ModerationStatus = 2 and a.IfTopic = 1");// and b.IfMoveHistory = 0");


            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@forumId", forumId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable GetDeletedPostsOfSiteByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, string queryConditions)
        {
            return null;
        }

        public static int GetCountOfDeletedPostsOfSiteByQuery(SqlConnectionWithSiteId conn, SqlTransaction transaction, string queryConditions)
        {
            return 0;
        }

        public static DataTable GetNotDeletedModerationPostsOfSiteByQueryAndPaging(
            SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize,
            string queryConditions, string orderField, string orderMethod)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            string subject = CommonFunctions.SqlReplace(queryConditions);
            StringBuilder strSQL = new StringBuilder();

            if (orderField == "Name")
                orderField = "b." + orderField;
            else if (orderField == "PostTime")
                orderField = "a." + orderField;

            strSQL.Append("select * from ");
            strSQL.Append(string.Format("(select ROW_NUMBER() over(order by {0} {1}) as row,", orderField, orderMethod));
            strSQL.Append(string.Format(" a.*, b.Name as PostUserOrOperatorName from [t_Forum_Post{0}] a ,t_User{0} b, t_Forum_Topic{0} c", conn.SiteId));
            strSQL.Append(" where a.PostUserOrOperatorId=b.Id and a.ModerationStatus=0 and a.IfDeleted='false' and ((a.TopicId = c.Id and c.IfDeleted='false') or (a.TopicId = c.Id and a.IfTopic='true'))");
            if (subject != "")
                strSQL.Append(" and (a.[Subject] like '%' + @subject + '%' escape '/' or a.Content like '%' + @subject + '%' escape '/')");
            strSQL.Append(") t where row between @startIndex and @endIndex");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfNotDeletedModerationPostsOfSiteByQueryAndPaging(
         SqlConnectionWithSiteId conn, SqlTransaction transaction,
         string queryConditions)
        {
            string subject = CommonFunctions.SqlReplace(queryConditions);
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select Count(*) from [t_Forum_Post{0}] a ,t_User{0} b, t_Forum_Topic{0} c", conn.SiteId));
            strSQL.Append(" where a.PostUserOrOperatorId=b.Id and a.ModerationStatus=0 and a.IfDeleted='false' and ((a.TopicId = c.Id and c.IfDeleted='false') or (a.TopicId = c.Id and a.IfTopic='true'))");
            if (subject != "")
                strSQL.Append(" and (a.[Subject] like '%' + @subject + '%' escape '/' or a.Content like '%' + @subject + '%' escape '/')");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@subject", subject);


            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable GetNotDeletedRejectedPostsOfSiteByQueryAndPaging(
           SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize,
           string queryConditions, string orderField, string orderMethod)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            string subject = CommonFunctions.SqlReplace(queryConditions);
            StringBuilder strSQL = new StringBuilder();

            if (orderField == "Name")
                orderField = "b." + orderField;
            else if (orderField == "PostTime")
                orderField = "a." + orderField;

            strSQL.Append("select * from ");
            strSQL.Append(string.Format("(select ROW_NUMBER() over(order by {0} {1}) as row,", orderField, orderMethod));
            strSQL.Append(string.Format(" a.*, b.Name as PostUserOrOperatorName from [t_Forum_Post{0}] a ,t_User{0} b, t_Forum_Topic{0} c", conn.SiteId));
            strSQL.Append(" where a.PostUserOrOperatorId=b.Id and a.ModerationStatus=1 and a.IfDeleted='false' and ((a.TopicId = c.Id and c.IfDeleted='false') or (a.TopicId = c.Id and a.IfTopic='true'))");
            if (subject != "")
                strSQL.Append(" and (a.[Subject] like '%' + @subject + '%' escape '/' or a.Content like '%' + @subject + '%' escape '/')");
            strSQL.Append(") t");
            strSQL.Append(" where row between @startIndex and @endIndex");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);
            cmd.Parameters.AddWithValue("@orderField", orderField);
            cmd.Parameters.AddWithValue("@orderMethod", orderMethod);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfNotDeletedRejectedPostsOfSiteByQueryAndPaging(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            string queryConditions)
        {

            string subject = CommonFunctions.SqlReplace(queryConditions);
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select count(*) from [t_Forum_Post{0}] a ,t_User{0} b, t_Forum_Topic{0} c", conn.SiteId));
            strSQL.Append(" where a.PostUserOrOperatorId=b.Id and a.ModerationStatus=1 and a.IfDeleted='false' and ((a.TopicId = c.Id and c.IfDeleted='false') or (a.TopicId = c.Id and a.IfTopic='true'))");
            if (subject != "")
                strSQL.Append(" and (a.[Subject] like '%' + @subject + '%' escape '/' or a.Content like '%' + @subject + '%' escape '/')");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@subject", subject);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void RefuseModeration(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("update [t_Forum_Post{0}] set [ModerationStatus]=@status where Id=@postId", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@postId", postId);
            cmd.Parameters.AddWithValue("@status", Convert.ToInt32(EnumPostOrTopicModerationStatus.Rejected));

            cmd.ExecuteNonQuery();
        }

        public static void AcceptModeration(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("update [t_Forum_Post{0}] set [ModerationStatus]=@status where Id=@postId", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@postId", postId);
            cmd.Parameters.AddWithValue("@status", Convert.ToInt32(EnumPostOrTopicModerationStatus.Accepted));

            cmd.ExecuteNonQuery();
        }

        public static DataTable GetNotDeletedPostsByQueryAndPagingOfSite(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            string keywords, string name, DateTime startDate, DateTime endDate,
            int pageIndex, int pageSize, string sortFiled, string sortMothod, int forumId)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            if (sortFiled == "PostUser")
                sortFiled = "b.Name";
            keywords = CommonFunctions.SqlReplace(keywords);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from");
            strSQL.Append(string.Format("(select ROW_NUMBER() over(order by {0} {1}) as row,", sortFiled, sortMothod));
            strSQL.Append(string.Format("a.*,b.Name from [t_Forum_Post{0}] a,[t_User{0}] b where a.PostUserOrOperatorId = b.Id", conn.SiteId));
            strSQL.Append(" and a.IfTopic='false' and a.IfDeleted='false'");
            //if (topicId >= 0)
            //    strSQL.Append(" and a.TopicId=@topicId");
            if (keywords != "")
                strSQL.Append(" and ((a.[Subject] like '%' + @keywords +'%' escape '/') or (a.TextContent like '%' + @keywords + '%' escape '/'))");
            if (name !="")
                strSQL.Append(" and b.Name = @name");
            
            strSQL.Append(" and a.ForumId = @forumId");
            if (startDate > new DateTime())
                strSQL.Append(" and a.PostTime >= @startDate");
            if (endDate > new DateTime())
                strSQL.Append(" and a.PostTime <= @endDate");
            strSQL.Append(") t");
            strSQL.Append(" where row between @startIndex and @endIndex");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            #region Add Value
            cmd.Parameters.AddWithValue("@sortFiled", sortFiled);
            cmd.Parameters.AddWithValue("@sortMothod", sortMothod);
            cmd.Parameters.AddWithValue("@keywords", keywords);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            //cmd.Parameters.AddWithValue("@topicId", topicId);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);
            #endregion

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return dt;
        }

        public static DataTable GetAllNotDeletedPostsByQueryAndPagingOfSite(
           SqlConnectionWithSiteId conn, SqlTransaction transaction,
           string keywords, string name, DateTime startDate, DateTime endDate,
           int pageIndex, int pageSize, string sortFiled, string sortMothod)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            if (sortFiled == "PostUser")
                sortFiled = "b.Name";
            else
                sortFiled = "a." + sortFiled;
            keywords = CommonFunctions.SqlReplace(keywords);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from");
            strSQL.Append(string.Format("(select ROW_NUMBER() over(order by {0} {1}) as row,", sortFiled, sortMothod));
            strSQL.Append(string.Format("a.*,b.Name from [t_Forum_Post{0}] a,[t_User{0}] b, t_Forum_Topic{0} c where a.PostUserOrOperatorId = b.Id", conn.SiteId));
            strSQL.Append(" and a.IfTopic='false' and a.IfDeleted='false' and c.[IfMoveHistory]='false' and a.TopicId not in (select TopicId from [t_Forum_Announcement]) and ((c.Id=a.TopicId and c.IfDeleted='false') or (c.Id=a.TopicId and a.IfTopic='true'))");
            //if (topicId >= 0)
            //    strSQL.Append(" and a.TopicId=@topicId");
            if (keywords != "")
                strSQL.Append(" and ((a.[Subject] like '%' + @keywords +'%' escape '/') or (a.TextContent like '%' + @keywords + '%' escape '/'))");
            if (name != "")
                strSQL.Append(" and b.Name = @name");
            
            
            if (startDate > new DateTime())
                strSQL.Append(" and a.PostTime >= @startDate");
            if (endDate > new DateTime())
                strSQL.Append(" and a.PostTime <= @endDate");
            strSQL.Append(") t");
            strSQL.Append(" where row between @startIndex and @endIndex");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            #region Add Value
            cmd.Parameters.AddWithValue("@sortFiled", sortFiled);
            cmd.Parameters.AddWithValue("@sortMothod", sortMothod);
            cmd.Parameters.AddWithValue("@keywords", keywords);
            cmd.Parameters.AddWithValue("@name", name);
            //cmd.Parameters.AddWithValue("@topicId", topicId);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);
            #endregion

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return dt;
        }


        public static int GetCountOfNotDeletedPostsByQueryAndPagingOfSite(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            string keywords, string name, DateTime startDate, DateTime endDate,
            int forumId)
        {
            keywords = CommonFunctions.SqlReplace(keywords);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select count(*) from [t_Forum_Post{0}] a,[t_User{0}] b where a.PostUserOrOperatorId = b.Id", conn.SiteId));
            strSQL.Append(" and a.IfTopic='false' and a.IfDeleted='false'");
            //if (topicId >= 0)
            //    strSQL.Append(" and a.TopicId=@topicId");
            if (keywords != "")
                strSQL.Append(" and ((a.[Subject] like '%' + @keywords +'%' escape '/') or (a.TextContent like '%' + @keywords + '%' escape '/'))");
            if (name !="")
                strSQL.Append(" and b.Name = @name");

            strSQL.Append(" and a.ForumId = @forumId");
            if (startDate > new DateTime())
                strSQL.Append(" and a.PostTime >= @startDate");
            if (endDate > new DateTime())
                strSQL.Append(" and a.PostTime <= @endDate");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            #region Add Value
            cmd.Parameters.AddWithValue("@keywords", keywords);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            //cmd.Parameters.AddWithValue("@topicId", topicId);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            #endregion

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int GetCountOfAllNotDeletedPostsByQueryAndPagingOfSite(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            string keywords, string name, DateTime startDate, DateTime endDate)
        {
            keywords = CommonFunctions.SqlReplace(keywords);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select count(*) from [t_Forum_Post{0}] a,[t_User{0}] b, t_Forum_Topic{0} c where a.PostUserOrOperatorId = b.Id", conn.SiteId));
            strSQL.Append(" and a.IfTopic='false' and a.IfDeleted='false' and c.[IfMoveHistory]='false' and a.TopicId not in (select TopicId from [t_Forum_Announcement]) and  ((c.Id=a.TopicId and c.IfDeleted='false') or (c.Id=a.TopicId and a.IfTopic='true'))");
            //if (topicId >= 0)
            //    strSQL.Append(" and a.TopicId=@topicId");
            if (keywords != "")
                strSQL.Append(" and ((a.[Subject] like '%' + @keywords +'%' escape '/') or (a.TextContent like '%' + @keywords + '%' escape '/'))");
            if (name != "")
                strSQL.Append(" and b.Name = @name");
           
            if (startDate > new DateTime())
                strSQL.Append(" and a.PostTime >= @startDate");
            if (endDate > new DateTime())
                strSQL.Append(" and a.PostTime <= @endDate");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            #region Add Value
            cmd.Parameters.AddWithValue("@keywords", keywords);
            cmd.Parameters.AddWithValue("@name", name);
            //cmd.Parameters.AddWithValue("@topicId", topicId);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            #endregion

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void LogicDeletePost(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int postId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("update [t_Forum_Post{0}] set IfDeleted='true' where Id=@postId", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@postId", postId);

            cmd.ExecuteNonQuery();
        }

        public static void RestorePost(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int postId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("  update [t_Forum_Post{0}] set IfDeleted='false' where Id=@postId", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@postId", postId);

            cmd.ExecuteNonQuery();
        }

        public static DataTable GetDeletedPostsByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            string keywords, string name, DateTime startDate, DateTime endDate,
            int pageIndex, int pageSize, string orderFiled, string orderMethod)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            if (orderFiled == "PostUser")
                orderFiled = "b.Name";
            else
                orderFiled = "a." + orderFiled;

            keywords = CommonFunctions.SqlReplace(keywords);

            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select * from ");
            strSQL.Append(string.Format("(select ROW_NUMBER() over(order by {0} {1}) as row,", orderFiled, orderMethod));
            strSQL.Append(" a.*,b.Name as PostUserOrOperatorName");
            strSQL.Append(string.Format(" from [t_Forum_Post{0}] a,t_User{0} b,t_Forum_Topic{0} c", conn.SiteId));
            strSQL.Append(" where a.[IfDeleted] = 'true' and c.[IfMoveHistory]='false' and a.PostUserOrOperatorId=b.Id and ((c.IfDeleted='false' and a.TopicId=c.Id) Or (a.IfTopic='true' and a.TopicId=c.Id))");
            if (startDate > new DateTime())
                strSQL.Append(" and a.PostTime >= @startDate");
            if (endDate > new DateTime())
                strSQL.Append(" and a.PostTime <=@endDate");
            if (keywords != "")
                strSQL.Append(" and ((a.[Subject] like '%' + @keywords + '%' escape '/') or (a.TextContent like '%' + @keywords + '%' escape '/'))");
            if (name!="")
                strSQL.Append(" and b.Name=@name");
            strSQL.Append(") t");
            strSQL.Append(" where row between @startIndex and @endIndex");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@orderFiled", orderFiled);
            cmd.Parameters.AddWithValue("@orderMethod", orderMethod);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@keywords", keywords);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return dt;
        }

        public static int GetCountOfDeletedPostsByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction,
          string keywords, string name, DateTime startDate, DateTime endDate)
        {

            keywords = CommonFunctions.SqlReplace(keywords);

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select count(*) as PostUserOrOperatorName");
            strSQL.Append(string.Format(" from [t_Forum_Post{0}] a,t_User{0} b,t_Forum_Topic{0} c", conn.SiteId));
            strSQL.Append(" where a.[IfDeleted] = 'true' and c.[IfMoveHistory]='false' and a.PostUserOrOperatorId=b.Id and ((c.IfDeleted='false' and a.TopicId=c.Id) Or (a.IfTopic='true' and a.TopicId=c.Id))");
            if (startDate > new DateTime())
                strSQL.Append(" and a.PostTime >= @startDate");
            if (endDate > new DateTime())
                strSQL.Append(" and a.PostTime <=@endDate");
            if (keywords != "")
                strSQL.Append(" and ((a.[Subject] like '%' + @keywords + '%' escape '/') or (a.TextContent like '%' + @keywords + '%' escape '/'))");
            if (name!="")
                strSQL.Append(" and b.Name=@name");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@keywords", keywords);
            cmd.Parameters.AddWithValue("@name", name);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }


        /*Advanced Search*/
        public static DataTable GetPostsByAdvancedSearch(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int pageIndex, int pageSize, string keywords, bool ifAllForums, bool ifCategory,
            bool ifForum, int id, DateTime BeginDate, DateTime EndDate, int searchMethod,List<int> forumIdsNoPermission)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            keywords = CommonFunctions.SqlReplace(keywords);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from");
            strSQL.Append(" (select ROW_NUMBER() over(order by PostTime asc) as row,");
            strSQL.Append(" e.*,f.Name as [PostUserOrOperatorName],f.IfDeleted as [PostUserOrOperatorIfDeleted],f.JoinedTime as [PostUserOrOperatorJoinedTime]");
            strSQL.Append(" from(");
            strSQL.Append(string.Format(" select a.* from [t_Forum_Post{0}] a, t_Forum_Topic{0} b", conn.SiteId));
            strSQL.Append(string.Format(" where a.TopicId=b.Id and a.TopicId not in (select distinct TopicId from t_Forum_Announcement where SiteId='{0}')", conn.SiteId));
            //strSQL.Append(string.Format(" and Id not in (select PostId from t_Forum_Abuse where Status <> '2' and SiteId='{0}')", conn.SiteId));
            /*not (waiting for moderation, refuse moderation, approve abuse,deleted,moved)*/
            strSQL.Append(" and a.ModerationStatus='2'and a.[IfSpam]='false' and b.IfMoveHistory='false' and a.IfDeleted='false' and b.IfDeleted='false'");
            /*forum with have permission*/
            if (forumIdsNoPermission != null && forumIdsNoPermission.Count > 0)
            {
                StringBuilder cmd1 = new StringBuilder();
                foreach (int forumId in forumIdsNoPermission)
                {
                    cmd1.Append(string.Format(" b.ForumId!='{0}' and", forumId));
                }
                strSQL.Append(string.Format(" and ({0})", cmd1.ToString().Remove(cmd1.Length - 4)));
            }
            /*PostTime*/
            if (BeginDate != new DateTime())
                strSQL.Append(" and a.PostTime >= @BeginDate");
            if (EndDate != new DateTime())
                strSQL.Append(" and a.PostTime <= @EndDate");
            /*Forum Or Category Or All*/
            if (ifAllForums) { }
            else if (ifCategory)
                strSQL.Append(" and a.ForumId in(select id from t_Forum_Forum where CategoryId=@id)");
            else if (ifForum)
                strSQL.Append(" and a.ForumId=@id");
            /*Search Method*/
            if (searchMethod == 0)
                strSQL.Append(" and ((a.[Subject] like '%' + @keywords + '%' escape '/') or (a.TextContent like '%' + @keywords + '%' escape '/'))");
            else if (searchMethod == 1)
                strSQL.Append(" and a.TextContent like '%' + @keywords + '%' escape '/'");
            else if (searchMethod == 2)
                strSQL.Append(" and a.[Subject] like '%' + @keywords + '%' escape '/' and a.ifTopic='true'");
            else if (searchMethod == 3)
                strSQL.Append(" and a.TextContent like '%' + @keywords + '%' escape '/' and a.ifTopic='true'");
            //else if (searchMethod == 2)
            //    strSQL.Append(" and a.[Subject] like '%' + @keywords + '%' escape '/'");
            //else if (searchMethod == 3)
            //    strSQL.Append(" and ((a.[Subject] like '%' + @keywords + '%' escape '/') or (a.TextContent like '%' + @keywords + '%' escape '/')) and a.ifTopic='true'");

            strSQL.Append(string.Format(") e, t_User{0} f", conn.SiteId));
            strSQL.Append(" where e.PostUserOrOperatorId = f.Id");
            strSQL.Append(") t where row between @startIndex and @endIndex");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            if (BeginDate != new DateTime())
                cmd.Parameters.AddWithValue("@BeginDate", BeginDate);
            if (EndDate != new DateTime())
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@keywords", keywords);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfPostsByAdvancedSearch(SqlConnectionWithSiteId conn, SqlTransaction transaction,
          int pageIndex, int pageSize, string keywords, bool ifAllForums, bool ifCategory,
          bool ifForum, int id, DateTime BeginDate, DateTime EndDate, int searchMethod, List<int> forumIdsNoPermission)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            keywords = CommonFunctions.SqlReplace(keywords);
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select COUNT(*)");
            strSQL.Append(" from(");
            strSQL.Append(string.Format(" select a.* from [t_Forum_Post{0}] a, t_Forum_Topic{0} b", conn.SiteId));
            strSQL.Append(string.Format(" where a.TopicId=b.Id and a.TopicId not in (select distinct TopicId from t_Forum_Announcement where SiteId='{0}')", conn.SiteId));
            //strSQL.Append(string.Format(" and Id not in (select PostId from t_Forum_Abuse where Status <> '2' and SiteId='{0}')", conn.SiteId));
            /*not (waiting for moderation, refuse moderation, approve abuse,deleted,moved)*/
            strSQL.Append(" and a.ModerationStatus='2'and a.[IfSpam]='false' and b.IfMoveHistory='false' and a.IfDeleted='false' and b.IfDeleted='false'");
            /*forum with have permission*/
            if (forumIdsNoPermission != null && forumIdsNoPermission.Count > 0)
            {
                StringBuilder cmd1 = new StringBuilder();
                foreach (int forumId in forumIdsNoPermission)
                {
                    cmd1.Append(string.Format(" b.ForumId!='{0}' and", forumId));
                }
                strSQL.Append(string.Format(" and ({0})", cmd1.ToString().Remove(cmd1.Length - 4)));
            }
            //strSQL.Append(string.Format(" select * from [t_Forum_Post{0}]", conn.SiteId));
            //strSQL.Append(string.Format(" where TopicId not in (select distinct TopicId from t_Forum_Announcement where SiteId='{0}')", conn.SiteId));
            //strSQL.Append(string.Format(" and Id not in (select PostId from t_Forum_Abuse where Status <> '2' and SiteId='{0}')",conn.SiteId));
            //strSQL.Append(" and ModerationStatus='2'");
            /*PostTime*/
            if (BeginDate != new DateTime())
                strSQL.Append(" and a.PostTime >= @BeginDate");
            if (EndDate != new DateTime())
                strSQL.Append(" and a.PostTime <= @EndDate");
            /*Forum Or Category Or All*/
            if (ifAllForums) { }
            else if (ifCategory)
                strSQL.Append(" and a.ForumId in(select id from t_Forum_Forum where CategoryId=@id)");
            else if (ifForum)
                strSQL.Append(" and a.ForumId=@id");
            /*Search Method*/
            if (searchMethod == 0)
                strSQL.Append(" and ((a.[Subject] like '%' + @keywords + '%' escape '/') or (a.TextContent like '%' + @keywords + '%' escape '/'))");
            else if (searchMethod == 1)
                strSQL.Append(" and a.TextContent like '%' + @keywords + '%' escape '/'");
            else if (searchMethod == 2)
                strSQL.Append(" and a.[Subject] like '%' + @keywords + '%' escape '/' and a.ifTopic='true'");
            else if (searchMethod == 3)
                strSQL.Append(" and a.TextContent like '%' + @keywords + '%' escape '/' and a.ifTopic='true'");

            strSQL.Append(string.Format(") e, t_User{0} f", conn.SiteId));
            strSQL.Append(" where e.PostUserOrOperatorId = f.Id");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            if (BeginDate != new DateTime())
                cmd.Parameters.AddWithValue("@BeginDate", BeginDate);
            if (EndDate != new DateTime())
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@keywords", keywords);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool IfUserReplyTopic(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int topicId, int userId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select  top 1 * from [t_Forum_Post{0}]", conn.SiteId));
            strSQL.Append(" where TopicId=@topicId and PostUserOrOperatorId=@userId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);
            cmd.Parameters.AddWithValue("@userId", userId);

            SqlDataReader dr = cmd.ExecuteReader();
            bool result = dr.HasRows;
            dr.Close();
            return result;

        }

        /*User's Posts*/
        public static DataTable GetNotDeletedPostsOfUserOrOperator(SqlConnectionWithSiteId conn, SqlTransaction transaction,
             string keywords, int userId, DateTime startDate, DateTime endDate,
            int pageIndex, int pageSize)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            keywords = CommonFunctions.SqlReplace(keywords);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select t.*,u.Name as PostUserOrOperatorName from ");
            strSQL.Append("(select ROW_NUMBER() over(order by PostTime desc) as row,");
            strSQL.Append(string.Format(" * from t_Forum_Post{0} where PostUserOrOperatorId = @userId and IfDeleted='false'", conn.SiteId));
            strSQL.Append(string.Format(" and TopicId not in ((select distinct TopicId from t_Forum_Announcement where SiteId = {0}) union (select Id from t_Forum_Topic{0} b where IfDeleted = 1)) ", conn.SiteId));

            if (keywords != "")
                strSQL.Append(" and (([Subject] like '%' + @keywords + '%' escape '/') or ([TextContent] like '%' + @keywords + '%' escape '/'))");
            if (startDate != new DateTime())
                strSQL.Append(" and PostTime >=@startDate");
            if (endDate != new DateTime())
                strSQL.Append(" and PostTime <=@endDate");
            strSQL.Append(string.Format(") t  left join t_User{0} u on t.PostUserOrOperatorId = u.Id where row between @startIndex and @endIndex",conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@keywords", keywords);
            if (startDate != new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate != new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return dt;
        }

        public static int GetCountOfNotDeletedPostsOfUserOrOperator(SqlConnectionWithSiteId conn, SqlTransaction transaction,
          string keywords, int userId, DateTime startDate, DateTime endDate)
        {
            keywords = CommonFunctions.SqlReplace(keywords);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select count (*) from t_Forum_Post{0} where PostUserOrOperatorId = @userId and IfDeleted='false'", conn.SiteId));
            strSQL.Append(string.Format(" and TopicId not in ((select distinct TopicId from t_Forum_Announcement where SiteId = {0}) union (select Id from t_Forum_Topic{0} b where IfDeleted = 1)) ", conn.SiteId));

            if (keywords != "")
                strSQL.Append(" and (([Subject] like '%' + @keywords + '%' escape '/') or ([TextContent] like '%' + @keywords + '%' escape '/'))");
            if (startDate != new DateTime())
                strSQL.Append(" and PostTime >=@startDate");
            if (endDate != new DateTime())
                strSQL.Append(" and PostTime <=@endDate");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@keywords", keywords);
            if (startDate != new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate != new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DateTime GetLastPostTime(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select Top 1 LastEditTime from [t_Forum_Post{0}]", conn.SiteId));
            strSQL.Append(" where PostUserOrOperatorId=@userId order by LastEditTime desc");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userId", userId);

            return Convert.ToDateTime(cmd.ExecuteScalar());
        }

        #region Moderator Panel
        public static DataTable GetNotDeletedModerationPostsByModeratorWithQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int moderatorId, string queryConditions, int pageIndex, int pageSize, string orderField, string orderDirection, EnumPostOrTopicModerationStatus status)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            string subject = CommonFunctions.SqlReplace(queryConditions);
            StringBuilder strSQL = new StringBuilder();
            if (orderField.ToLower() == "name")
                orderField = "u." + orderField;
            else
                orderField = "p." + orderField;
            strSQL.Append("select * from( ");
            strSQL.Append(string.Format(" select ROW_NUMBER() over(order by {0} {1}) as row, p.*,u.Name ", orderField, orderDirection));
            strSQL.Append(string.Format(" from t_Forum_Post{0} p ", conn.SiteId));
            strSQL.Append(string.Format(" inner join t_Forum_Topic{0} q on p.TopicId = q.Id and q.[IfMoveHistory]='false' and (q.IfDeleted='false' or p.IfTopic='true')", conn.SiteId));
            strSQL.Append(" inner join (select * from t_Forum_Moderator where UserOrOperatorId=@moderatorId)m on p.ForumId=m.ForumId ");
            strSQL.Append(string.Format(" left join t_User{0} u on p.PostUserOrOperatorId=u.Id ", conn.SiteId));
            strSQL.Append(" where p.ModerationStatus=@status and p.IfDeleted='false' ");
            if (subject != "")
                strSQL.Append(" and (p.[Subject] like '%' + @subject + '%' escape '/' or p.Content like  '%' + @subject + '%' escape '/')");
            strSQL.Append(" ) returnV ");
            strSQL.Append(" where row between @startIndex and @endIndex ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);
            cmd.Parameters.AddWithValue("@status", Convert.ToInt16(status));
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfNotDeletedModerationPostsByModeratorWithQuery(SqlConnectionWithSiteId conn, SqlTransaction transaction, int moderatorId, string queryConditions, EnumPostOrTopicModerationStatus status)
        {
            string subject = CommonFunctions.SqlReplace(queryConditions);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select COUNT(*) ");
            strSQL.Append(string.Format(" from t_Forum_Post{0} p ", conn.SiteId));
            strSQL.Append(" inner join (select * from t_Forum_Moderator where UserOrOperatorId=@moderatorId)m on p.ForumId = m.ForumId ");
            strSQL.Append(string.Format(" inner join t_Forum_Topic{0} q on p.TopicId = q.Id and q.[IfMoveHistory]='false' and (q.IfDeleted='false' or p.IfTopic='true')", conn.SiteId));
            strSQL.Append(string.Format(" left join t_User{0} u on p.PostUserOrOperatorId=u.Id ", conn.SiteId));
            strSQL.Append(" where p.ModerationStatus=@status and p.IfDeleted='false' ");
            if (subject != "")
                strSQL.Append(" and (p.[Subject] like '%' + @subject + '%' escape '/' or p.Content like  '%' + @subject + '%' escape '/')");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);
            cmd.Parameters.AddWithValue("@status", Convert.ToInt16(status));
            cmd.Parameters.AddWithValue("@subject", subject);
            DataTable table = new DataTable();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable GetNotDeletedPostsByModeratorWithQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int moderatorId,  string keywords,string name, DateTime startDate, DateTime endDate, int forumId,
            int pageIndex, int pageSize, string orderField, string orderDirection)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            if (orderField == "PostUser")
                orderField = "b.Name";
            else
                orderField = "a." + orderField;
            keywords = CommonFunctions.SqlReplace(keywords);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from");
            strSQL.Append(string.Format("(select ROW_NUMBER() over(order by {0} {1}) as row,", orderField, orderDirection));
            strSQL.Append(string.Format("a.*,b.Name from [t_Forum_Post{0}] a,[t_User{0}] b, t_Forum_Topic{0} c where a.PostUserOrOperatorId = b.Id", conn.SiteId));
            strSQL.Append(" and a.IfTopic='false' and a.IfDeleted='false' and c.[IfMoveHistory]='false' and a.TopicId=c.Id and (c.IfDeleted='false' or a.IfTopic='true')");
            //if (topicId >= 0)
            //    strSQL.Append(" and a.TopicId=@topicId");
            if (keywords != "")
                strSQL.Append(" and ((a.[Subject] like '%' + @keywords +'%' escape '/') or (a.TextContent like '%' + @keywords + '%' escape '/'))");
            if (name!="")
                strSQL.Append(" and b.Name = @name");
            
            strSQL.Append(" and a.ForumId = @forumId");
            
            if (startDate > new DateTime())
                strSQL.Append(" and a.PostTime >= @startDate");
            if (endDate > new DateTime())
                strSQL.Append(" and a.PostTime <= @endDate");
            strSQL.Append(") t");
            strSQL.Append(" where row between @startIndex and @endIndex");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            #region Add Value
            //cmd.Parameters.AddWithValue("@sortFiled", sortFiled);
            //cmd.Parameters.AddWithValue("@sortMothod", sortMothod);
            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);
            cmd.Parameters.AddWithValue("@keywords", keywords);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            //cmd.Parameters.AddWithValue("@topicId", topicId);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);
            #endregion

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return dt;
        }

        public static DataTable GetAllNotDeletedPostsByModeratorWithQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction,
           int moderatorId, string keywords, string name, DateTime startDate, DateTime endDate,
           int pageIndex, int pageSize, string orderField, string orderDirection)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            if (orderField == "PostUser")
                orderField = "b.Name";
            else
                orderField = "a." + orderField;
            keywords = CommonFunctions.SqlReplace(keywords);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from");
            strSQL.Append(string.Format("(select ROW_NUMBER() over(order by {0} {1}) as row,", orderField, orderDirection));
            strSQL.Append(string.Format("a.*,b.Name from [t_Forum_Post{0}] a,[t_User{0}] b, t_Forum_Topic{0} c where a.PostUserOrOperatorId = b.Id", conn.SiteId));
            strSQL.Append(" and a.IfTopic='false' and a.IfDeleted='false' and c.[IfMoveHistory]='false' and a.TopicId=c.Id and (c.IfDeleted='false' or a.IfTopic='true')");
            //if (topicId >= 0)
            //    strSQL.Append(" and a.TopicId=@topicId");
            if (keywords != "")
                strSQL.Append(" and ((a.[Subject] like '%' + @keywords +'%' escape '/') or (a.TextContent like '%' + @keywords + '%' escape '/'))");
            if (name != "")
                strSQL.Append(" and b.Name = @name");
            
            strSQL.Append(" and a.ForumId in (select ForumId from t_Forum_Moderator where UserOrOperatorId=@moderatorId) ");
            if (startDate > new DateTime())
                strSQL.Append(" and a.PostTime >= @startDate");
            if (endDate > new DateTime())
                strSQL.Append(" and a.PostTime <= @endDate");
            strSQL.Append(") t");
            strSQL.Append(" where row between @startIndex and @endIndex");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            #region Add Value
            //cmd.Parameters.AddWithValue("@sortFiled", sortFiled);
            //cmd.Parameters.AddWithValue("@sortMothod", sortMothod);
            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);
            cmd.Parameters.AddWithValue("@keywords", keywords);
            cmd.Parameters.AddWithValue("@name", name);
            //cmd.Parameters.AddWithValue("@topicId", topicId);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);
            #endregion

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return dt;
        }

        public static int GetCountOfNotDeletedPostsByModeratorWithQuery(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int moderatorId,  string keywords, string name, DateTime startDate, DateTime endDate, int forumId)
        {
            keywords = CommonFunctions.SqlReplace(keywords);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select count(*) from [t_Forum_Post{0}] a,[t_User{0}] b, t_Forum_Topic{0} c where a.PostUserOrOperatorId = b.Id", conn.SiteId));
            strSQL.Append(" and a.IfTopic='false' and a.IfDeleted='false' and c.[IfMoveHistory]='false' and a.TopicId=c.Id and (c.IfDeleted='false' or a.IfTopic='true')");
            //if (topicId >= 0)
            //    strSQL.Append(" and a.TopicId=@topicId");
            if (keywords != "")
                strSQL.Append(" and ((a.[Subject] like '%' + @keywords +'%' escape '/') or (a.TextContent like '%' + @keywords + '%' escape '/'))");
            if (name!="")
                strSQL.Append(" and b.Name = @name");
            if (forumId >= 0)
                strSQL.Append(" and a.ForumId = @forumId");
            else
                strSQL.Append(" and a.ForumId in (select ForumId from t_Forum_Moderator where UserOrOperatorId=@moderatorId)");
            if (startDate > new DateTime())
                strSQL.Append(" and a.PostTime >= @startDate");
            if (endDate > new DateTime())
                strSQL.Append(" and a.PostTime <= @endDate");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            #region Add Value
            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);
            cmd.Parameters.AddWithValue("@keywords", keywords);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            //cmd.Parameters.AddWithValue("@topicId", topicId);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            #endregion

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int GetCountOfAllNotDeletedPostsByModeratorWithQuery(SqlConnectionWithSiteId conn, SqlTransaction transaction,
         int moderatorId, string keywords, string name, DateTime startDate, DateTime endDate)
        {
            keywords = CommonFunctions.SqlReplace(keywords);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select count(*) from [t_Forum_Post{0}] a,[t_User{0}] b, t_Forum_Topic{0} c where a.PostUserOrOperatorId = b.Id", conn.SiteId));
            strSQL.Append(" and a.IfTopic='false' and a.IfDeleted='false' and c.[IfMoveHistory]='false' and a.TopicId=c.Id and (c.IfDeleted='false' or a.IfTopic='true')");
            //if (topicId >= 0)
            //    strSQL.Append(" and a.TopicId=@topicId");
            if (keywords != "")
                strSQL.Append(" and ((a.[Subject] like '%' + @keywords +'%' escape '/') or (a.TextContent like '%' + @keywords + '%' escape '/'))");
            if (name != "")
                strSQL.Append(" and b.Name = @name");
            strSQL.Append(" and a.ForumId in (select ForumId from t_Forum_Moderator where UserOrOperatorId=@moderatorId)");
            if (startDate > new DateTime())
                strSQL.Append(" and a.PostTime >= @startDate");
            if (endDate > new DateTime())
                strSQL.Append(" and a.PostTime <= @endDate");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            #region Add Value
            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);
            cmd.Parameters.AddWithValue("@keywords", keywords);
            cmd.Parameters.AddWithValue("@name", name);
            //cmd.Parameters.AddWithValue("@topicId", topicId);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            #endregion

            return Convert.ToInt32(cmd.ExecuteScalar());
        }


        public static DataTable GetDeletedPostsByModeratorWithQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int moderatorId, DateTime startDate, DateTime endDate, string keywords, string name, int pageIndex, int pageSize, string orderField, string orderDirection)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            if (orderField == "PostUser")
                orderField = "u.Name";
            else
                orderField = "p." + orderField;
            keywords = CommonFunctions.SqlReplace(keywords);

            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select * from ");
            strSQL.Append(string.Format("(select ROW_NUMBER() over(order by {0} {1}) as row,", orderField, orderDirection));
            strSQL.Append(" p.*,u.Name");
            strSQL.Append(string.Format(" from t_Forum_Post{0} p inner join t_Forum_Topic{0} q", conn.SiteId));
            strSQL.Append(" on p.TopicId = q.Id and (q.IfDeleted='false' or p.IfTopic='true') and q.[IfMoveHistory]='false'");
            strSQL.Append(" inner join t_Forum_Moderator m on p.ForumId=m.ForumId and UserOrOperatorId=@moderatorId ");
            strSQL.Append(string.Format(" left join t_User{0} u on p.PostUserOrOperatorId=u.Id  ", conn.SiteId));
            strSQL.Append(" where p.[IfDeleted] = 'true' ");
            if (startDate > new DateTime())
                strSQL.Append(" and p.PostTime >= @startDate");
            if (endDate > new DateTime())
                strSQL.Append(" and p.PostTime <=@endDate");
            if (keywords != "")
                strSQL.Append(" and (([Subject] like '%' + @keywords + '%' escape '/') or (TextContent like '%' + @keywords + '%' escape '/'))");
            if (name!="")
                strSQL.Append(" and u.Name=@name");
            strSQL.Append(") t");
            strSQL.Append(" where row between @startIndex and @endIndex");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);
            cmd.Parameters.AddWithValue("@keywords", keywords);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return dt;
        }

        public static int GetCountOfDeletedPostsByModeratorWithQuery(SqlConnectionWithSiteId conn
            , SqlTransaction transaction, int moderatorId, DateTime startDate, DateTime endDate
            , string keywords, string name)
        {
            keywords = CommonFunctions.SqlReplace(keywords);

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select count(*) ");
            strSQL.Append(string.Format(" from [t_Forum_Post{0}] p inner join t_Forum_Topic{0} q", conn.SiteId));
            strSQL.Append(" on p.TopicId = q.Id and (q.IfDeleted='false' or p.IfTopic='true') and q.[IfMoveHistory]='false'");
            strSQL.Append(" inner join t_Forum_Moderator m on p.ForumId=m.ForumId and m.UserOrOperatorId=@moderatorId ");
            strSQL.Append(string.Format(" left join t_User{0} u on p.PostUserOrOperatorId=u.Id ", conn.SiteId));
            strSQL.Append(" where p.[IfDeleted] = 'true' ");
            if (startDate > new DateTime())
                strSQL.Append(" and p.PostTime >= @startDate");
            if (endDate > new DateTime())
                strSQL.Append(" and p.PostTime <=@endDate");
            if (keywords != "")
                strSQL.Append(" and (([Subject] like '%' + @keywords + '%' escape '/') or (TextContent like '%' + @keywords + '%' escape '/'))");
            if (name!="")
                strSQL.Append(" and u.Name=@name");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);
            cmd.Parameters.AddWithValue("@keywords", keywords);
            cmd.Parameters.AddWithValue("@name", name);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        #endregion
    }
}
