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
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.DataAccess
{
    public class TopicAccess
    {
        public static DataTable GetTopicByTopicId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {
            //StringBuilder strSQL = new StringBuilder("select a.*,b.Name as [PostUserOrOperatorName],a.IfDeleted as [TopicIfDeleted],b.IfDeleted as [PostUserOrOperatorIfDeleted],c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted],dbo.fn_GetTopicPromotionCount(@Id) as [TotalPromotion]");
            //strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a ");
            //strSQL.Append(" left join t_User" + conn.SiteId + " b on b.Id = a.PostUserOrOperatorId");
            //strSQL.Append(" left join t_User" + conn.SiteId + " c on c.Id = a.LastPostUserOrOperatorId");
            //strSQL.Append(" where a.Id=@Id");
            //SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            //cmd.Parameters.AddWithValue("@Id", topicId);

            /*added for topic detail page*/
            SqlCommand cmd = new SqlCommand("Forum_GetTopicsDetailById", conn.SqlConn, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", topicId);
            
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfTopicsBytForumId(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int forumId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select count(*) from t_Forum_Topic{0} topic",conn.SiteId));
            strSQL.Append(" where topic.ForumId = @ForumId and topic.IfDeleted='false' and topic.ModerationStatus = 2");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int GetCountOfFeaturedTopicsBytForumId(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int forumId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select COUNT(*) from t_Forum_Topic{0} where ForumId = @forumId and [IfFeatured]='true' and [IfDeleted]='false' and [ModerationStatus] = 2", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int GetCountOfTopicsByForumIdWithoutWaitingForModeration(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int forumId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select count(*) from t_Forum_Topic{0} topic", conn.SiteId));
            strSQL.Append(" where topic.forumid = @ForumId and topic.IfDeleted='false' and topic.ModerationStatus = 2");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int GetCountOfTopicsByForumIdWithoutWaitingForModerationSearch(SqlConnectionWithSiteId conn, SqlTransaction transaction,
           int forumId, string searchKeyword)
        {
            //StringBuilder strSQL = new StringBuilder();
            //strSQL.Append(string.Format("select count(*) from t_Forum_Topic{0} topic", conn.SiteId));
            //strSQL.Append(" where topic.forumid = @ForumId and topic.IfDeleted='false' and topic.ModerationStatus = 2");
            //SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            //cmd.Parameters.AddWithValue("@ForumId", forumId);

            SqlCommand cmd = new SqlCommand("Forum_GetTopicsBySearchCount", conn.SqlConn, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@Searchkeword", searchKeyword);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable GetTopicsByForumIdAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int pageIndex, int pageSize)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;

            //StringBuilder strSQL = new StringBuilder();
            //strSQL.Append("select a.*, b.Name PostUserOrOperatorName, b.IfDeleted PostUserOrOperatorIfDeleted, c.Name LastPostUserOrOperatorName, c.IfDeleted LastPostUserOrOperatorIfDeleted ");
            //strSQL.Append("from ( ");
            //strSQL.Append("select * from ( ");
            //strSQL.Append("select ");
            //strSQL.Append("Row_Number() over(order by case when PromotionCount is null then 0 end, PromotionCount desc) row, topic.*");//IfSticky//order by  PromotionCount desc, LastPostTime desc, topic.Id desc
            //strSQL.Append(string.Format(" from t_Forum_Topic{0} topic ", conn.SiteId));
            //strSQL.Append("where topic.ForumId = @ForumId and topic.IfDeleted='false' and topic.ModerationStatus = 2");
            //strSQL.Append(" ) t ");
            //strSQL.Append(string.Format("where row between {0} and {1} ", startRowNum, endRowNum));
            //strSQL.Append(" ) a ");
            //strSQL.Append(string.Format("left join t_User{0} b ", conn.SiteId));
            //strSQL.Append("on b.Id = a.PostUserOrOperatorId ");
            //strSQL.Append(string.Format("left join t_User{0} c ", conn.SiteId));
            //strSQL.Append("on c.Id = a.LastPostUserOrOperatorId");

            //SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            SqlCommand cmd = new SqlCommand("Forum_GetTopicsByForumIdAndPaging", conn.SqlConn, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@StartRowNum", startRowNum);
            cmd.Parameters.AddWithValue("@EndRowNum", endRowNum);


            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        //fetching data 
        public static DataTable GetTopicsByForumIdAndPagingWithoutWaitingForModeration(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int pageIndex, int pageSize)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;

            //StringBuilder strSQL = new StringBuilder();
            //strSQL.Append("select a.*,dbo.fn_GetTopicPromotionCount(a.id) TotalPromotion, b.Name PostUserOrOperatorName, b.IfDeleted PostUserOrOperatorIfDeleted, c.Name LastPostUserOrOperatorName, c.IfDeleted LastPostUserOrOperatorIfDeleted ");
            //strSQL.Append("from ( ");
            //strSQL.Append("select * from ( ");
            //strSQL.Append("select ");
            //strSQL.Append("Row_Number() over(order by case when PromotionCount is null then 0 end, PromotionCount desc) row, topic.*");/*IfSticky*///select a.*, b.Name PostUserOrOperatorName, b.IfDeleted PostUserOrOperatorIfDeleted, c.Name LastPostUserOrOperatorName, c.IfDeleted LastPostUserOrOperatorIfDeleted from ( select * from ( select Row_Number() over(order by PromotionCount desc, LastPostTime desc, topic.Id desc) row, topic.* from t_Forum_Topic0 topic where topic.ForumId = @ForumId and topic.IfDeleted='false' and topic.ModerationStatus = 2 ) t where row between 1 and 20  ) a left join t_User0 b on b.Id = a.PostUserOrOperatorId left join t_User0 c on c.Id = a.LastPostUserOrOperatorId
            //strSQL.Append(string.Format(" from t_Forum_Topic{0} topic ", conn.SiteId));
            //strSQL.Append("where topic.ForumId = @ForumId and topic.IfDeleted='false' and topic.ModerationStatus = 2");
            //strSQL.Append(" ) t ");
            //strSQL.Append(string.Format("where row between {0} and {1} ", startRowNum, endRowNum));
            //strSQL.Append(" ) a ");
            //strSQL.Append(string.Format("left join t_User{0} b ", conn.SiteId));
            //strSQL.Append("on b.Id = a.PostUserOrOperatorId ");
            //strSQL.Append(string.Format("left join t_User{0} c ", conn.SiteId));
            //strSQL.Append("on c.Id = a.LastPostUserOrOperatorId");

            //SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            SqlCommand cmd = new SqlCommand("Forum_GetTopicsByForumIdWithoutWaitingForModeration", conn.SqlConn, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@StartRowNum", startRowNum);
            cmd.Parameters.AddWithValue("@EndRowNum", endRowNum);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;

         }
        /* update by techtier on 3/1/2017 for adding sorting by most popular */
        public static DataTable GetTopicsByForumIdAndPagingWithoutWaitingForModerationByManualSort(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int pageIndex, int pageSize, string sortKeyword)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select a.*, b.Name PostUserOrOperatorName, b.IfDeleted PostUserOrOperatorIfDeleted, c.Name LastPostUserOrOperatorName, c.IfDeleted LastPostUserOrOperatorIfDeleted,dbo.fn_GetTopicPromotionCount(a.id)	AS	TotalPromotion ");
            strSQL.Append("from ( ");
            strSQL.Append("select * from ( ");
            strSQL.Append("select ");
            strSQL.Append("Row_Number() over(order by " + sortKeyword + ") row, topic.*");/* changes in line 141 (order by numberofhits) */
            strSQL.Append(string.Format(" from t_Forum_Topic{0} topic ", conn.SiteId));
            strSQL.Append("where topic.ForumId = @ForumId and topic.IfDeleted='false' and topic.ModerationStatus = 2");
            strSQL.Append(" ) t ");
            strSQL.Append(string.Format("where row between {0} and {1} ", startRowNum, endRowNum));
            strSQL.Append(" ) a ");
            strSQL.Append(string.Format("left join t_User{0} b ", conn.SiteId));
            strSQL.Append("on b.Id = a.PostUserOrOperatorId ");
            strSQL.Append(string.Format("left join t_User{0} c ", conn.SiteId));
            strSQL.Append("on c.Id = a.LastPostUserOrOperatorId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@ForumId", forumId);

            //SqlCommand cmd = new SqlCommand("Forum_GettopicsByforumIdAndPagingWithoutWaitingFormoderationByManualSort", conn.SqlConn, transaction);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Keyword", sortKeyword);
            //cmd.Parameters.AddWithValue("@ForumId", forumId);
            //cmd.Parameters.AddWithValue("@startRowNum", startRowNum);
            //cmd.Parameters.AddWithValue("@endRowNum", endRowNum);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }


        public static DataTable GetTopicsByForumIdAndPagingWithoutWaitingForModerationByManualMyPost(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int pageIndex, int pageSize, int operatingUserOrOperatorId)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;

            //StringBuilder strSQL = new StringBuilder();
            //strSQL.Append("select a.*, b.Name PostUserOrOperatorName, b.IfDeleted PostUserOrOperatorIfDeleted, c.Name LastPostUserOrOperatorName, c.IfDeleted LastPostUserOrOperatorIfDeleted,dbo.fn_GetTopicPromotionCount(a.id)	AS	TotalPromotion ");
            //strSQL.Append("from ( ");
            //strSQL.Append("select * from ( ");
            //strSQL.Append("select ");
            //strSQL.Append("Row_Number() over(order by topic.LastPostTime) row, topic.*");/* changes in line 141 (order by numberofhits) */
            //strSQL.Append(string.Format(" from t_Forum_Topic{0} topic ", conn.SiteId));
            //strSQL.Append("where topic.ForumId = @ForumId and topic.IfDeleted='false' and topic.ModerationStatus = 2");
            //strSQL.Append(" ) t ");
            //strSQL.Append(string.Format("where row between {0} and {1} ", startRowNum, endRowNum));
            //strSQL.Append(" ) a ");
            //strSQL.Append(string.Format("left join t_User{0} b ", conn.SiteId));
            //strSQL.Append("on b.Id = a.PostUserOrOperatorId ");
            //strSQL.Append(string.Format("left join t_User{0} c ", conn.SiteId));
            //strSQL.Append("on c.Id = a.LastPostUserOrOperatorId");

            //SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            //cmd.Parameters.AddWithValue("@ForumId", forumId);

            SqlCommand cmd = new SqlCommand("Forum_GetTopicsofMyPostbyUserId", conn.SqlConn, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", operatingUserOrOperatorId);
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@startRowNum", startRowNum);
            cmd.Parameters.AddWithValue("@endRowNum", endRowNum);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        
        //techtier for search
        public static DataTable GetTopicsByForumIdAndPagingWithoutWaitingForModerationByManualSearch(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int pageIndex, int pageSize, string searchKeyword)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;

            //StringBuilder strSQL = new StringBuilder();
            //strSQL.Append("select a.*, b.Name PostUserOrOperatorName, b.IfDeleted PostUserOrOperatorIfDeleted, c.Name LastPostUserOrOperatorName, c.IfDeleted LastPostUserOrOperatorIfDeleted,dbo.fn_GetTopicPromotionCount(a.id)	AS	TotalPromotion ");
            //strSQL.Append("from ( ");
            //strSQL.Append("select * from ( ");
            //strSQL.Append("select ");
            //strSQL.Append("Row_Number() over(order by " + searchKeyword + " desc) row, topic.*");/* changes in line 141 (order by numberofhits) */
            //strSQL.Append(string.Format(" from t_Forum_Topic{0} topic ", conn.SiteId));
            //strSQL.Append("where topic.ForumId = @ForumId and topic.IfDeleted='false' and topic.ModerationStatus = 2");
            //strSQL.Append(" ) t ");
            //strSQL.Append(string.Format("where row between {0} and {1} ", startRowNum, endRowNum));
            //strSQL.Append(" ) a ");
            //strSQL.Append(string.Format("left join t_User{0} b ", conn.SiteId));
            //strSQL.Append("on b.Id = a.PostUserOrOperatorId ");
            //strSQL.Append(string.Format("left join t_User{0} c ", conn.SiteId));
            //strSQL.Append("on c.Id = a.LastPostUserOrOperatorId");

            //SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            //cmd.Parameters.AddWithValue("@ForumId", forumId);

            SqlCommand cmd = new SqlCommand("Forum_GetTopicsBySearch", conn.SqlConn, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Searchkeword", searchKeyword);
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@startRowNum", startRowNum);
            cmd.Parameters.AddWithValue("@endRowNum", endRowNum);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

            /*Selecting data for admin user*/
        public static DataTable GetFeaturedTopicsByForumIdAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int pageIndex, int pageSize)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;

            //StringBuilder strSQL = new StringBuilder();
            //strSQL.Append("select a.*, b.Name PostUserOrOperatorName, b.IfDeleted PostUserOrOperatorIfDeleted, c.Name LastPostUserOrOperatorName, c.IfDeleted LastPostUserOrOperatorIfDeleted ");
            //strSQL.Append("from ( ");
            //strSQL.Append("select * from ( ");
            //strSQL.Append("select ");
            //strSQL.Append("Row_Number() over(order by IfSticky desc, LastPostTime desc, topic.Id desc) row, topic.*");
            //strSQL.Append(string.Format(" from t_Forum_Topic{0} topic ", conn.SiteId));
            //strSQL.Append("where topic.ForumId = @ForumId and topic.IfDeleted='false' and topic.[IfFeatured]='true' and topic.ModerationStatus = 2");
            //strSQL.Append(" ) t ");
            //strSQL.Append(string.Format("where row between {0} and {1} ", startRowNum, endRowNum));
            //strSQL.Append(" ) a ");
            //strSQL.Append(string.Format("left join t_User{0} b ", conn.SiteId));
            //strSQL.Append("on b.Id = a.PostUserOrOperatorId ");
            //strSQL.Append(string.Format("left join t_User{0} c ", conn.SiteId));
            //strSQL.Append("on c.Id = a.LastPostUserOrOperatorId");

            //SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            SqlCommand cmd = new SqlCommand("Forum_GetTopicsByForumIdAndPaging", conn.SqlConn, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@StartRowNum", startRowNum);
            cmd.Parameters.AddWithValue("@EndRowNum", endRowNum);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        /*Select data for normal user*/
        public static DataTable GetFeaturedTopicsByForumIdAndPagingWithoutWaitingForModeration(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int pageIndex, int pageSize)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;

            //StringBuilder strSQL = new StringBuilder();
            //strSQL.Append("select a.*, b.Name PostUserOrOperatorName, b.IfDeleted PostUserOrOperatorIfDeleted, c.Name LastPostUserOrOperatorName, c.IfDeleted LastPostUserOrOperatorIfDeleted ");
            //strSQL.Append("from ( ");
            //strSQL.Append("select * from ( ");
            //strSQL.Append("select ");
            //strSQL.Append("Row_Number() over(order by IfSticky desc, LastPostTime desc, topic.Id desc) row, topic.*");
            //strSQL.Append(string.Format(" from t_Forum_Topic{0} topic ", conn.SiteId));
            //strSQL.Append("where topic.ForumId = @ForumId and topic.IfDeleted='false' and topic.[IfFeatured]='true' and topic.ModerationStatus = 2");
            //strSQL.Append(" ) t ");
            //strSQL.Append(string.Format("where row between {0} and {1} ", startRowNum, endRowNum));
            //strSQL.Append(" ) a ");
            //strSQL.Append(string.Format("left join t_User{0} b ", conn.SiteId));
            //strSQL.Append("on b.Id = a.PostUserOrOperatorId ");
            //strSQL.Append(string.Format("left join t_User{0} c ", conn.SiteId));
            //strSQL.Append("on c.Id = a.LastPostUserOrOperatorId");

            //SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            SqlCommand cmd = new SqlCommand("Forum_GetTopicsByForumIdWithoutWaitingForModeration", conn.SqlConn, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@StartRowNum", startRowNum);
            cmd.Parameters.AddWithValue("@EndRowNum", endRowNum);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }


        public static int GetCountOfFeaturedTopicsByForumIdAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select COUNT(*) from t_Forum_Topic{0} where ForumId = @forumId and [IfFeatured]='true' and [ModerationStatus] = 2", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int GetCountOfFeaturedTopicsByForumIdAndPagingWithoutWaitingForModeration(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select COUNT(*) from t_Forum_Topic{0} where ForumId = @forumId and [IfFeatured]='true' and [ModerationStatus] = 2", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }


        #region GetTopicsBySearchOptionsAndForumId
        public static DataTable GetTopicsBySearchInForumAndAnsweredWithOneStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int pageIndex, int pageSize, string subject, string displayName, DateTime startTime, DateTime endTime, bool ifTopicClosed, bool ifSticky)
        {
            int excludeRecordsCount = (pageIndex - 1) * pageSize;

            StringBuilder strSQL = new StringBuilder(" select top " + pageSize.ToString() + " a.*,b.Name as [PostUserOrOperatorName],b.IfDeleted as [PostUserOrOperatorIfDeleted],");
            strSQL.Append(" c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.LastPostUserOrOperatorId = c.Id");
            strSQL.Append(" where a.ForumId=@ForumId and a.Subject like @subject escape '/'  and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (a.IfClosed=@ifTopicClosed or a.IfMarkedAsAnswer='true') and (@ifSticky=0 or a.IfSticky=@ifSticky)  ");
            strSQL.Append(" and a.Id not in ");
            strSQL.Append(" (select top " + excludeRecordsCount.ToString() + " ID from t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" where ForumId=@ForumId and Subject like @subject escape '/'  and PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/') ");
            strSQL.Append(" and PostTime>@startTime and PostTime<@endTime ");
            strSQL.Append(" and (IfClosed=@ifTopicClosed or IfMarkedAsAnswer='true') and (@ifSticky=0 or IfSticky=@ifSticky)  ");
            strSQL.Append(" order by Id desc)");
            strSQL.Append(" order by a.Id desc ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifTopicClosed", ifTopicClosed);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        public static int GetCountOfTopicsBySearchInForumAndAnsweredWithOneStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int pageIndex, int pageSize,
           string subject, string displayName, DateTime startTime, DateTime endTime, bool ifTopicClosed, bool ifSticky)
        {

            StringBuilder strSQL = new StringBuilder(" select count(a.Id)");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" where a.ForumId=@ForumId and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/')");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (a.IfClosed=@ifTopicClosed or a.IfMarkedAsAnswer='true') and (@ifSticky=0 or a.IfSticky=@ifSticky)  ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifTopicClosed", ifTopicClosed);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            return (int)cmd.ExecuteScalar();
        }

        public static DataTable GetTopicsBySearchInForumAndAnsweredWithBothStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int pageIndex, int pageSize,
           string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {
            int excludeRecordsCount = (pageIndex - 1) * pageSize;

            StringBuilder strSQL = new StringBuilder(" select top " + pageSize.ToString() + " a.*,b.Name as [PostUserOrOperatorName],b.IfDeleted as [PostUserOrOperatorIfDeleted],");
            strSQL.Append(" c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.LastPostUserOrOperatorId = c.Id");
            strSQL.Append(" where a.ForumId=@ForumId and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or a.IfSticky=@ifSticky) and (a.IfMarkedAsAnswer='true' or a.IfClosed='true' or a.IfClosed='false') ");
            strSQL.Append(" and a.Id not in ");
            strSQL.Append(" (select top " + excludeRecordsCount.ToString() + " ID from t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" where ForumId=@ForumId and Subject like @subject escape '/' and PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' ) ");
            strSQL.Append(" and PostTime>@startTime and PostTime<@endTime ");
            strSQL.Append(" and (@ifSticky=0 or IfSticky=@ifSticky) and (IfMarkedAsAnswer='true' or IfClosed='true' or IfClosed='false') ");
            strSQL.Append(" order by Id desc)");
            strSQL.Append(" order by  a.Id desc ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfTopicsBySearchInForumAndAnsweredWithBothStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int pageIndex, int pageSize,
           string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {

            StringBuilder strSQL = new StringBuilder(" select count(a.Id)");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" where a.ForumId=@ForumId and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/')");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or a.IfSticky=@ifSticky) and (a.IfMarkedAsAnswer='true' or a.IfClosed='true'or a.IfClosed='false') ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            //cmd.Parameters.AddWithValue("@ifAnswered", ifAnswered);
            return (int)cmd.ExecuteScalar(); ;
        }

        public static DataTable GetTopicsBySearchInForumAndAnsweredWithoutStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int pageIndex, int pageSize,
           string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {
            int excludeRecordsCount = (pageIndex - 1) * pageSize;

            StringBuilder strSQL = new StringBuilder(" select top " + pageSize.ToString() + " a.*,b.Name as [PostUserOrOperatorName],b.IfDeleted as [PostUserOrOperatorIfDeleted],");
            strSQL.Append(" c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.LastPostUserOrOperatorId = c.Id");
            strSQL.Append(" where a.ForumId=@ForumId and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or a.IfSticky=@ifSticky) and a.IfMarkedAsAnswer='true' ");
            strSQL.Append(" and a.Id not in ");
            strSQL.Append(" (select top " + excludeRecordsCount.ToString() + " ID from t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" where ForumId=@ForumId and Subject like @subject escape '/' and PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' ) ");
            strSQL.Append(" and PostTime>@startTime and PostTime<@endTime ");
            strSQL.Append(" and (@ifSticky=0 or IfSticky=@ifSticky) and IfMarkedAsAnswer='true' ");
            strSQL.Append(" order by Id desc)");
            strSQL.Append(" order by  a.Id desc ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfTopicsBySearchInForumAndAnsweredWithoutStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int pageIndex, int pageSize,
           string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {

            StringBuilder strSQL = new StringBuilder(" select count(a.Id)");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" where a.ForumId=@ForumId and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/')");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or a.IfSticky=@ifSticky) and a.IfMarkedAsAnswer='true' ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            return (int)cmd.ExecuteScalar();
        }

        public static DataTable GetTopicsBySearchInForumAndNotAnsweredWithOneStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int pageIndex, int pageSize, string subject, string displayName, DateTime startTime, DateTime endTime, bool ifTopicClosed, bool ifSticky)
        {
            int excludeRecordsCount = (pageIndex - 1) * pageSize;

            StringBuilder strSQL = new StringBuilder(" select top " + pageSize.ToString() + " a.*,b.Name as [PostUserOrOperatorName],b.IfDeleted as [PostUserOrOperatorIfDeleted],");
            strSQL.Append(" c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.LastPostUserOrOperatorId = c.Id");
            strSQL.Append(" where a.ForumId=@ForumId and a.Subject like @subject escape '/'  and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and a.IfClosed=@ifTopicClosed  and (@ifSticky=0 or a.IfSticky=@ifSticky)  ");
            strSQL.Append(" and a.Id not in ");
            strSQL.Append(" (select top " + excludeRecordsCount.ToString() + " ID from t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" where ForumId=@ForumId and Subject like @subject escape '/'  and PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/') ");
            strSQL.Append(" and PostTime>@startTime and PostTime<@endTime ");
            strSQL.Append(" and IfClosed=@ifTopicClosed  and (@ifSticky=0 or IfSticky=@ifSticky)  ");
            strSQL.Append(" order by Id desc)");
            strSQL.Append(" order by a.Id desc ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifTopicClosed", ifTopicClosed);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        public static int GetCountOfTopicsBySearchInForumAndNotAnsweredWithOneStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int pageIndex, int pageSize,
           string subject, string displayName, DateTime startTime, DateTime endTime, bool ifTopicClosed, bool ifSticky)
        {

            StringBuilder strSQL = new StringBuilder(" select count(a.Id)");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" where a.ForumId=@ForumId and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/')");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and a.IfClosed=@ifTopicClosed and (@ifSticky=0 or a.IfSticky=@ifSticky)  ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifTopicClosed", ifTopicClosed);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            return (int)cmd.ExecuteScalar();
        }

        public static DataTable GetTopicsBySearchInForumAndNotAnsweredWithBothStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int pageIndex, int pageSize,
           string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {
            int excludeRecordsCount = (pageIndex - 1) * pageSize;

            StringBuilder strSQL = new StringBuilder(" select top " + pageSize.ToString() + " a.*,b.Name as [PostUserOrOperatorName],b.IfDeleted as [PostUserOrOperatorIfDeleted],");
            strSQL.Append(" c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.LastPostUserOrOperatorId = c.Id");
            strSQL.Append(" where a.ForumId=@ForumId and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or a.IfSticky=@ifSticky) and (a.IfClosed='true' or a.IfClosed='false') ");
            strSQL.Append(" and a.Id not in ");
            strSQL.Append(" (select top " + excludeRecordsCount.ToString() + " ID from t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" where ForumId=@ForumId and Subject like @subject escape '/' and PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' ) ");
            strSQL.Append(" and PostTime>@startTime and PostTime<@endTime ");
            strSQL.Append(" and (@ifSticky=0 or IfSticky=@ifSticky) and (IfClosed='true' or IfClosed='false') ");
            strSQL.Append(" order by Id desc)");
            strSQL.Append(" order by  a.Id desc ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfTopicsBySearchInForumAndNotAnsweredWithBothStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int pageIndex, int pageSize,
           string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {

            StringBuilder strSQL = new StringBuilder(" select count(a.Id)");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" where a.ForumId=@ForumId and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/')");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or a.IfSticky=@ifSticky) and (a.IfClosed='true'or a.IfClosed='false') ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            return (int)cmd.ExecuteScalar();
        }
        #endregion

        #region GetTopicsBySearchOptionsAndCategoryId
        public static DataTable GetTopicsBySearchInCategoryAndAnsweredWithOneStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, int pageIndex, int pageSize, string subject, string displayName, DateTime startTime, DateTime endTime, bool ifTopicClosed, bool ifSticky)
        {
            int excludeRecordsCount = (pageIndex - 1) * pageSize;

            StringBuilder strSQL = new StringBuilder(" select top " + pageSize.ToString() + " a.*,b.Name as [PostUserOrOperatorName],b.IfDeleted as [PostUserOrOperatorIfDeleted],");
            strSQL.Append(" c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.LastPostUserOrOperatorId = c.Id");
            strSQL.Append(" where a.ForumId in (select Id from t_Forum_Forum where CategoryId=@categoryId)");
            strSQL.Append(" and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/')");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (a.IfClosed=@ifTopicClosed or a.IfMarkedAsAnswer='true') and (@ifSticky=0 or a.IfSticky=@ifSticky)  ");
            strSQL.Append(" and a.Id not in ");
            strSQL.Append(" (select top " + excludeRecordsCount.ToString() + " ID from t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" where ForumId in (select Id from t_Forum_Forum where CategoryId=@categoryId)");
            strSQL.Append(" and Subject like @subject escape '/' and PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and PostTime>@startTime and PostTime<@endTime ");
            strSQL.Append(" and (IfClosed=@ifTopicClosed or IfMarkedAsAnswer='true') and (@ifSticky=0 or IfSticky=@ifSticky)  ");
            strSQL.Append(" order by Id desc )");
            strSQL.Append(" order by a.Id desc ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifTopicClosed", ifTopicClosed);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        public static int GetCountOfTopicsBySearchInCategoryAndAnsweredWithOneStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, int pageIndex, int pageSize, string subject, string displayName, DateTime startTime, DateTime endTime, bool ifTopicClosed, bool ifSticky)
        {

            StringBuilder strSQL = new StringBuilder(" select count(a.Id)");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" where a.ForumId in (select Id from t_Forum_Forum where CategoryId=@categoryId)");
            strSQL.Append(" and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (a.IfClosed=@ifTopicClosed or a.IfMarkedAsAnswer='true') and (@ifSticky=0 or a.IfSticky=@ifSticky)  ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifTopicClosed", ifTopicClosed);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            return (int)cmd.ExecuteScalar();
        }

        public static DataTable GetTopicsBySearchInCategoryAndAnsweredWithBothStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, int pageIndex, int pageSize,
           string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {
            int excludeRecordsCount = (pageIndex - 1) * pageSize;

            StringBuilder strSQL = new StringBuilder(" select top " + pageSize.ToString() + " a.*,b.Name as [PostUserOrOperatorName],b.IfDeleted as [PostUserOrOperatorIfDeleted],");
            strSQL.Append(" c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.LastPostUserOrOperatorId = c.Id");
            strSQL.Append(" where a.ForumId in ( select Id from t_Forum_Forum where CategoryId=@categoryId )");
            strSQL.Append(" and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or a.IfSticky=@ifSticky) and (a.IfMarkedAsAnswer='true' or a.IfClosed='true' or a.IfClosed='false' ) ");
            strSQL.Append(" and a.Id not in ");
            strSQL.Append(" (select top " + excludeRecordsCount.ToString() + " ID from t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" where ForumId in (select Id from t_Forum_Forum where CategoryId=@categoryId)");
            strSQL.Append(" and Subject like @subject escape '/' and PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and PostTime>@startTime and PostTime<@endTime ");
            strSQL.Append(" and (@ifSticky=0 or IfSticky=@ifSticky) and (IfMarkedAsAnswer='true' or IfClosed='true' or IfClosed='false' ) ");
            strSQL.Append(" order by Id desc )");
            strSQL.Append(" order by a.Id desc ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfTopicsBySearchInCategoryAndAnsweredWithBothStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, int pageIndex, int pageSize, string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {

            StringBuilder strSQL = new StringBuilder(" select count(a.Id)");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" where a.ForumId in (select Id from t_Forum_Forum where CategoryId=@categoryId)");
            strSQL.Append(" and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/')");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or a.IfSticky=@ifSticky) and (a.IfMarkedAsAnswer='true' or a.IfClosed='true' or a.IfClosed='false' ) ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            return (int)cmd.ExecuteScalar();
        }

        public static DataTable GetTopicsBySearchInCategoryAndAnsweredWithoutStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, int pageIndex, int pageSize,
          string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {
            int excludeRecordsCount = (pageIndex - 1) * pageSize;

            StringBuilder strSQL = new StringBuilder(" select top " + pageSize.ToString() + " a.*,b.Name as [PostUserOrOperatorName],b.IfDeleted as [PostUserOrOperatorIfDeleted],");
            strSQL.Append(" c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.LastPostUserOrOperatorId = c.Id");
            strSQL.Append(" where a.ForumId in ( select Id from t_Forum_Forum where CategoryId=@categoryId )");
            strSQL.Append(" and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or a.IfSticky=@ifSticky) and a.IfMarkedAsAnswer='true' ");
            strSQL.Append(" and a.Id not in ");
            strSQL.Append(" (select top " + excludeRecordsCount.ToString() + " ID from t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" where ForumId in (select Id from t_Forum_Forum where CategoryId=@categoryId)");
            strSQL.Append(" and Subject like @subject escape '/' and PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and PostTime>@startTime and PostTime<@endTime ");
            strSQL.Append(" and (@ifSticky=0 or IfSticky=@ifSticky) and IfMarkedAsAnswer='true' ");
            strSQL.Append(" order by Id desc )");
            strSQL.Append(" order by a.Id desc ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfTopicsBySearchInCategoryAndAnsweredWithoutStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, int pageIndex, int pageSize, string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {

            StringBuilder strSQL = new StringBuilder(" select count(a.Id)");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" where a.ForumId in (select Id from t_Forum_Forum where CategoryId=@categoryId)");
            strSQL.Append(" and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/')");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or IfSticky=@ifSticky) and a.IfMarkedAsAnswer='true' ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            return (int)cmd.ExecuteScalar();
        }


        public static DataTable GetTopicsBySearchInCategoryAndNotAnsweredWithOneStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, int pageIndex, int pageSize, string subject, string displayName, DateTime startTime, DateTime endTime, bool ifTopicClosed, bool ifSticky)
        {
            int excludeRecordsCount = (pageIndex - 1) * pageSize;

            StringBuilder strSQL = new StringBuilder(" select top " + pageSize.ToString() + " a.*,b.Name as [PostUserOrOperatorName],b.IfDeleted as [PostUserOrOperatorIfDeleted],");
            strSQL.Append(" c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.LastPostUserOrOperatorId = c.Id");
            strSQL.Append(" where a.ForumId in (select Id from t_Forum_Forum where CategoryId=@categoryId)");
            strSQL.Append(" and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/')");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and a.IfClosed=@ifTopicClosed and (@ifSticky=0 or a.IfSticky=@ifSticky)  ");
            strSQL.Append(" and a.Id not in ");
            strSQL.Append(" (select top " + excludeRecordsCount.ToString() + " ID from t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" where ForumId in (select Id from t_Forum_Forum where CategoryId=@categoryId)");
            strSQL.Append(" and Subject like @subject escape '/' and PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and PostTime>@startTime and PostTime<@endTime ");
            strSQL.Append(" and IfClosed=@ifTopicClosed and (@ifSticky=0 or IfSticky=@ifSticky)  ");
            strSQL.Append(" order by Id desc )");
            strSQL.Append(" order by a.Id desc ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifTopicClosed", ifTopicClosed);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        public static int GetCountOfTopicsBySearchInCategoryAndNotAnsweredWithOneStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, int pageIndex, int pageSize, string subject, string displayName, DateTime startTime, DateTime endTime, bool ifTopicClosed, bool ifSticky)
        {

            StringBuilder strSQL = new StringBuilder(" select count(a.Id)");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" where a.ForumId in (select Id from t_Forum_Forum where CategoryId=@categoryId)");
            strSQL.Append(" and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and a.IfClosed=@ifTopicClosed and (@ifSticky=0 or a.IfSticky=@ifSticky)  ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifTopicClosed", ifTopicClosed);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            return (int)cmd.ExecuteScalar();
        }

        public static DataTable GetTopicsBySearchInCategoryAndNotAnsweredWithBothStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, int pageIndex, int pageSize,
           string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {
            int excludeRecordsCount = (pageIndex - 1) * pageSize;

            StringBuilder strSQL = new StringBuilder(" select top " + pageSize.ToString() + " a.*,b.Name as [PostUserOrOperatorName],b.IfDeleted as [PostUserOrOperatorIfDeleted],");
            strSQL.Append(" c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.LastPostUserOrOperatorId = c.Id");
            strSQL.Append(" where a.ForumId in ( select Id from t_Forum_Forum where CategoryId=@categoryId )");
            strSQL.Append(" and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or a.IfSticky=@ifSticky) and (a.IfClosed='true' or a.IfClosed='false' ) ");
            strSQL.Append(" and a.Id not in ");
            strSQL.Append(" (select top " + excludeRecordsCount.ToString() + " ID from t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" where ForumId in (select Id from t_Forum_Forum where CategoryId=@categoryId)");
            strSQL.Append(" and Subject like @subject escape '/' and PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and PostTime>@startTime and PostTime<@endTime ");
            strSQL.Append(" and (@ifSticky=0 or IfSticky=@ifSticky) and (IfClosed='true' or IfClosed='false') ");
            strSQL.Append(" order by Id desc )");
            strSQL.Append(" order by a.Id desc ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfTopicsBySearchInCategoryAndNotAnsweredWithBothStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, int pageIndex, int pageSize, string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {

            StringBuilder strSQL = new StringBuilder(" select count(a.Id)");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" where a.ForumId in (select Id from t_Forum_Forum where CategoryId=@categoryId)");
            strSQL.Append(" and a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/')");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or a.IfSticky=@ifSticky) and (a.IfClosed='true' or a.IfClosed='false' ) ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            return (int)cmd.ExecuteScalar();
        }
        #endregion

        #region GetTopicsBySearchOptionsWithoutAnyId
        public static DataTable GetTopicsBySearchInAllAndAnsweredWithOneStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, string subject, string displayName, DateTime startTime, DateTime endTime, bool ifTopicClosed, bool ifSticky)
        {
            int excludeRecordsCount = (pageIndex - 1) * pageSize;

            StringBuilder strSQL = new StringBuilder(" select top " + pageSize.ToString() + " a.*,b.Name as [PostUserOrOperatorName],b.IfDeleted as [PostUserOrOperatorIfDeleted],");
            strSQL.Append(" c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.LastPostUserOrOperatorId = c.Id");
            strSQL.Append(" where a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/')");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (a.IfClosed=@ifTopicClosed or a.IfMarkedAsAnswer='true') and (@ifSticky=0 or a.IfSticky=@ifSticky)  ");
            strSQL.Append(" and a.Id not in ");
            strSQL.Append(" (select top " + excludeRecordsCount.ToString() + " ID from t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" where Subject like @subject escape '/' and PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and PostTime>@startTime and PostTime<@endTime ");
            strSQL.Append(" and (IfClosed=@ifTopicClosed or IfMarkedAsAnswer='true') and (@ifSticky=0 or IfSticky=@ifSticky)  ");
            strSQL.Append(" order by Id desc )");
            strSQL.Append(" order by a.Id desc ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifTopicClosed", ifTopicClosed);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        public static int GetCountOfTopicsBySearchInAllAndAnsweredWithOneStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, string subject, string displayName, DateTime startTime, DateTime endTime, bool ifTopicClosed, bool ifSticky)
        {

            StringBuilder strSQL = new StringBuilder(" select count(a.Id)");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" where a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (a.IfClosed=@ifTopicClosed or a.IfMarkedAsAnswer='true') and (@ifSticky=0 or a.IfSticky=@ifSticky)  ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifTopicClosed", ifTopicClosed);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            return (int)cmd.ExecuteScalar();
        }

        public static DataTable GetTopicsBySearchInAllAndAnsweredWithBothStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize,
           string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {
            int excludeRecordsCount = (pageIndex - 1) * pageSize;

            StringBuilder strSQL = new StringBuilder(" select top " + pageSize.ToString() + " a.*,b.Name as [PostUserOrOperatorName],b.IfDeleted as [PostUserOrOperatorIfDeleted],");
            strSQL.Append(" c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.LastPostUserOrOperatorId = c.Id");
            strSQL.Append(" where a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or a.IfSticky=@ifSticky) and (a.IfMarkedAsAnswer='true' or a.IfClosed='true' or a.IfClosed='false' ) ");
            strSQL.Append(" and a.Id not in ");
            strSQL.Append(" (select top " + excludeRecordsCount.ToString() + " ID from t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" where Subject like @subject escape '/' and PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and PostTime>@startTime and PostTime<@endTime ");
            strSQL.Append(" and (@ifSticky=0 or IfSticky=@ifSticky) and (IfMarkedAsAnswer='true' or IfClosed='true' or IfClosed='false' ) ");
            strSQL.Append(" order by Id desc )");
            strSQL.Append(" order by a.Id desc ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfTopicsBySearchInAllAndAnsweredWithBothStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {

            StringBuilder strSQL = new StringBuilder(" select count(a.Id)");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" where a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/')");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or a.IfSticky=@ifSticky) and (a.IfMarkedAsAnswer='true' or a.IfClosed='true' or a.IfClosed='false' ) ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            return (int)cmd.ExecuteScalar();
        }

        public static DataTable GetTopicsBySearchInAllAndAnsweredWithoutStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize,
          string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {
            int excludeRecordsCount = (pageIndex - 1) * pageSize;

            StringBuilder strSQL = new StringBuilder(" select top " + pageSize.ToString() + " a.*,b.Name as [PostUserOrOperatorName],b.IfDeleted as [PostUserOrOperatorIfDeleted],");
            strSQL.Append(" c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.LastPostUserOrOperatorId = c.Id");
            strSQL.Append(" where a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or a.IfSticky=@ifSticky) and a.IfMarkedAsAnswer='true' ");
            strSQL.Append(" and a.Id not in ");
            strSQL.Append(" (select top " + excludeRecordsCount.ToString() + " ID from t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" where Subject like @subject escape '/' and PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and PostTime>@startTime and PostTime<@endTime ");
            strSQL.Append(" and (@ifSticky=0 or IfSticky=@ifSticky) and IfMarkedAsAnswer='true' ");
            strSQL.Append(" order by Id desc )");
            strSQL.Append(" order by a.Id desc ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfTopicsBySearchInAllAndAnsweredWithoutStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {

            StringBuilder strSQL = new StringBuilder(" select count(a.Id)");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" where a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/')");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or IfSticky=@ifSticky) and IfMarkedAsAnswer='true' ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            return (int)cmd.ExecuteScalar();
        }

        public static DataTable GetTopicsBySearchInAllAndNotAnsweredWithOneStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, string subject, string displayName, DateTime startTime, DateTime endTime, bool ifTopicClosed, bool ifSticky)
        {
            int excludeRecordsCount = (pageIndex - 1) * pageSize;


            StringBuilder strSQL = new StringBuilder(" select top " + pageSize.ToString() + " a.*,b.Name as [PostUserOrOperatorName],b.IfDeleted as [PostUserOrOperatorIfDeleted],");
            strSQL.Append(" c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.LastPostUserOrOperatorId = c.Id");
            strSQL.Append(" where a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/')");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and a.IfClosed=@ifTopicClosed and (@ifSticky=0 or a.IfSticky=@ifSticky)  ");
            strSQL.Append(" and a.Id not in ");
            strSQL.Append(" (select top " + excludeRecordsCount.ToString() + " ID from t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" where Subject like @subject escape '/' and PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and PostTime>@startTime and PostTime<@endTime ");
            strSQL.Append(" and IfClosed=@ifTopicClosed and (@ifSticky=0 or IfSticky=@ifSticky)  ");
            strSQL.Append(" order by Id desc )");
            strSQL.Append(" order by a.Id desc ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifTopicClosed", ifTopicClosed);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        public static int GetCountOfTopicsBySearchInAllAndNotAnsweredWithOneStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, string subject, string displayName, DateTime startTime, DateTime endTime, bool ifTopicClosed, bool ifSticky)
        {

            StringBuilder strSQL = new StringBuilder(" select count(a.Id)");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" where a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and IfClosed=@ifTopicClosed and (@ifSticky=0 or IfSticky=@ifSticky)  ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifTopicClosed", ifTopicClosed);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            return (int)cmd.ExecuteScalar();
        }

        public static DataTable GetTopicsBySearchInAllAndNotAnsweredWithBothStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize,
           string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {
            int excludeRecordsCount = (pageIndex - 1) * pageSize;

            StringBuilder strSQL = new StringBuilder(" select top " + pageSize.ToString() + " a.*,b.Name as [PostUserOrOperatorName],b.IfDeleted as [PostUserOrOperatorIfDeleted],");
            strSQL.Append(" c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.PostUserOrOperatorId = b.Id");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.LastPostUserOrOperatorId = c.Id");
            strSQL.Append(" where a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or a.IfSticky=@ifSticky) and (a.IfClosed='true' or a.IfClosed='false' ) ");
            strSQL.Append(" and a.Id not in ");
            strSQL.Append(" (select top " + excludeRecordsCount.ToString() + " ID from t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" where Subject like @subject escape '/' and PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/' )");
            strSQL.Append(" and PostTime>@startTime and PostTime<@endTime ");
            strSQL.Append(" and (@ifSticky=0 or IfSticky=@ifSticky) and (IfClosed='true' or IfClosed='false') ");
            strSQL.Append(" order by Id desc )");
            strSQL.Append(" order by a.Id desc ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfTopicsBySearchInAllAndNotAnsweredWithBothStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, string subject, string displayName, DateTime startTime, DateTime endTime, bool ifSticky)
        {

            StringBuilder strSQL = new StringBuilder(" select count(a.Id)");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a");
            strSQL.Append(" where a.Subject like @subject escape '/' and a.PostUserOrOperatorId in");
            strSQL.Append(" (select Id from t_User" + conn.SiteId);
            strSQL.Append(" where Name like @displayName escape '/')");
            strSQL.Append(" and a.PostTime>@startTime and a.PostTime<@endTime");
            strSQL.Append(" and (@ifSticky=0 or a.IfSticky=@ifSticky) and (a.IfClosed='true' or a.IfClosed='false' ) ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);


            return (int)cmd.ExecuteScalar();
        }

        #endregion


        public static DataTable GetTopicsWhichExistDraftByPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageindex, int pageSize, string subjectKeyWord, string strOrder)
        {
            int excludeRecordsCount = (pageindex - 1) * pageSize;

            subjectKeyWord = subjectKeyWord.Replace("~", "~~").Replace("%", "~%").Replace("[", "~[").Replace("]", "~]").Replace("^", "~^").Replace("_", "~_");

            StringBuilder strSQL = new StringBuilder("select top " + pageSize.ToString() + " a.*,c.[Name] as PostUserOrOperatorName,c.[IfDeleted] as PostUserOrOperatorIfDeleted,d.[Name] as LastPostUserOrOperatorName,d.[IfDeleted] as LastPostUserOrOperatorIfDeleted ");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a inner join t_Forum_Draft" + conn.SiteId + " b on a.Id=b.TopicId ");
            strSQL.Append(" and a.IfDeleted='false' and a.Id not in(select top " + excludeRecordsCount + " t_Forum_Topic" + conn.SiteId + ".Id from t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" inner join t_Forum_Draft" + conn.SiteId + " on t_Forum_Topic" + conn.SiteId + ".Id=t_Forum_Draft" + conn.SiteId + ".TopicId order by PostTime desc)");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on a.PostUserOrOperatorId=c.Id ");
            strSQL.Append(" left join t_User" + conn.SiteId + " d on d.Id=a.LastPostUserOrOperatorId");
            strSQL.Append(" where a.Subject like '%'+@displayName+'%' escape '~' ");
            strSQL.Append(" order by PostTime desc");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@displayName", subjectKeyWord);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfTopicsWhichExistDraft(SqlConnectionWithSiteId conn, SqlTransaction transaction, string subjectKeyWord)
        {
            subjectKeyWord = subjectKeyWord.Replace("~", "~~").Replace("%", "~%").Replace("[", "~[").Replace("]", "~]").Replace("^", "~^").Replace("_", "~_");


            StringBuilder strSQL = new StringBuilder("select count(1) from t_Forum_Topic" + conn.SiteId + " a left join t_Forum_Draft" + conn.SiteId + " b on a.Id=b.TopicId ");
            strSQL.Append(" where a.Id=b.TopicId and a.IfDeleted='false' and");
            strSQL.Append(" a.Subject like '%'+@displayName+'%' escape '~' ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@displayName", subjectKeyWord);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable GetTopicsByPostUserOrOperatorIdAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postUserOrOperatorId, int pageIndex, int pageSize)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;

            StringBuilder strSQL = new StringBuilder("select a.*, b.Name PostUserOrOperatorName, b.IfDeleted PostUserOrOperatorIfDeleted, c.Name as LastPostUserOrOperatorName, c.IfDeleted as LastPostUserOrOperatorIfDeleted ");
            strSQL.Append(" from ( ");
            strSQL.Append("select * ");
            strSQL.Append("from ( ");
            strSQL.Append("select Row_Number() over(order by topic.PostTime desc, topic.Id desc) row, topic.*");
            strSQL.Append(string.Format(" from t_Forum_Topic{0} topic ", conn.SiteId));
            strSQL.Append("where topic.PostUserOrOperatorId = @PostUserId");
            strSQL.Append(" ) t ");
            strSQL.Append(string.Format("where t.row between {0} and {1}", startRowNum, endRowNum));
            strSQL.Append(" ) a ");
            strSQL.Append(string.Format("left join t_User{0} b ", conn.SiteId));
            strSQL.Append("on b.Id = a.PostUserOrOperatorId ");
            strSQL.Append(string.Format("left join t_User{0} c ", conn.SiteId));
            strSQL.Append("on c.Id = a.LastPostUserOrOperatorId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@PostUserID", postUserOrOperatorId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfTopicsByPostUserOrOperatorId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postUserOrOperatorId)
        {

            StringBuilder strSQL = new StringBuilder("select COUNT(Id)from t_Forum_Topic" + conn.SiteId + " where PostUserOrOperatorId=@postUserOrOperatorId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@postUserOrOperatorId", postUserOrOperatorId);

            return (int)cmd.ExecuteScalar();

        }

        public static DataTable GetTopicsByReplyUserOrOperatorIdAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int replyUserOrOperatorId, int pageIndex, int pageSize)
        {
            int begin = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;

            StringBuilder strSQL = new StringBuilder("select t.*,u1.Name as PostUserOrOperatorName,u1.IfDeleted as PostUserOrOperatorIfDeleted,u2.Name as LastPostUserOrOperatorName,u2.IfDeleted as LastPostUserOrOperatorIfDeleted from");
            strSQL.Append(" (select");
            strSQL.Append(" ROW_NUMBER() over (order by topic.LastPostTime desc) rw,topic.*");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId.ToString() + " topic");
            strSQL.Append(" join (select post.TopicId");
            strSQL.Append(" from t_Forum_Post" + conn.SiteId.ToString() + " post");
            strSQL.Append(" where post.PostUserOrOperatorId = @postUserOrOperatorId");
            strSQL.Append(" group by post.TopicId)post");
            strSQL.Append(" on topic.Id=post.TopicId)t");
            strSQL.Append(" left join t_User" + conn.SiteId.ToString() + " u1");
            strSQL.Append(" on t.PostUserOrOperatorId=u1.Id");
            strSQL.Append(" left join t_User" + conn.SiteId.ToString() + " u2");
            strSQL.Append(" on t.LastPostUserOrOperatorId=u2.Id");
            strSQL.Append(" where rw between " + begin.ToString() + " and " + end.ToString());
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@postUserOrOperatorId", replyUserOrOperatorId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfTopicsByReplyUserOrOperatorId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int replyUserOrOperatorId)
        {

            StringBuilder strSQL = new StringBuilder("select COUNT(c.Id)from(select distinct a.Id from t_Forum_Topic" + conn.SiteId.ToString() + " a inner join t_Forum_Post" + conn.SiteId.ToString());
            strSQL.Append(" b on a.Id=b.TopicId and b.PostUserOrOperatorId=@postUserOrOeratorId)c");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@postUserOrOeratorId", replyUserOrOperatorId);

            return (int)cmd.ExecuteScalar();
        }

        public static int AddTopic(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int forumId, string subject, int postUserOrOperatorId, DateTime postTime, string content,
            int score, bool ifContainsPoll,bool IfReplyRequired, bool IfPayScoreRequired,bool ifTopicModeration)
        {

            StringBuilder strSQL = new StringBuilder("select LastLoginIP from t_User" + conn.SiteId.ToString() + " where Id = @postUserOrOperatorId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@postUserOrOperatorId", postUserOrOperatorId);
            Double postIP = Convert.ToInt64(cmd.ExecuteScalar());
            strSQL = new StringBuilder("select Name from t_Forum_Forum where Id = @forumId");
            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            String forumName = Convert.ToString(cmd.ExecuteScalar());
            strSQL = new StringBuilder("Insert into t_Forum_Topic" + conn.SiteId.ToString());
            strSQL.Append(" ([ForumId],[ForumName],[Subject],[PostUserOrOperatorId],[PostTime],[PostIP],[LastPostId],[LastPostTime]");
            strSQL.Append(",[LastPostUserOrOperatorId],[NumberOfReplies],[NumberOfHits],[IfClosed],[IfMarkedAsAnswer],[IfSticky]");
            strSQL.Append(",[ParticipatorIds],[IfHasDraft],[IfDeleted],[Type],[IfMoveHistory],[LocateTopicId],[MoveDate]");
            strSQL.Append(",[MoveUserOrOperatorId],[IfFeatured],[IfContainsPoll],[ModerationStatus],[AnnouncementStartDate]");
            strSQL.Append(",[AnnouncementEndDate],[IfPayScoreRequired],[Score],[IfReplyRequired]) values ");
            strSQL.Append(" (@ForumId,@ForumName,@Subject,@PostUserOrOperatorId,@PostTime,@PostIP,@LastPostId,@LastPostTime");
            strSQL.Append(",@LastPostUserOrOperatorId,@NumberOfReplies,@NumberOfHits,@IfClosed,@IfMarkedAsAnswer,@IfSticky");
            strSQL.Append(",@ParticipatorIds,@IfHasDraft,@IfDeleted,@Type,@IfMoveHistory,@LocateTopicId,@MoveDate");
            strSQL.Append(",@MoveUserOrOperatorId,@IfFeatured,@IfContainsPoll,@ModerationStatus,@AnnouncementStartDate");
            strSQL.Append(",@AnnouncementEndDate,@IfPayScoreRequired,@Score,@IfReplyRequired)");
            strSQL.Append("select @@identity");
            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@ForumName", forumName);
            cmd.Parameters.AddWithValue("@Subject", subject);
            cmd.Parameters.AddWithValue("@PostUserOrOperatorId", postUserOrOperatorId);
            cmd.Parameters.AddWithValue("@PostTime", postTime);
            cmd.Parameters.AddWithValue("@PostIP", postIP);
            cmd.Parameters.AddWithValue("@LastPostId", 0);
            cmd.Parameters.AddWithValue("@LastPostTime", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@LastPostUserOrOperatorId", 0);
            cmd.Parameters.AddWithValue("@NumberOfReplies", 0);
            cmd.Parameters.AddWithValue("@NumberOfHits", 0);
            cmd.Parameters.AddWithValue("@IfClosed", false);
            cmd.Parameters.AddWithValue("@IfMarkedAsAnswer", false);
            cmd.Parameters.AddWithValue("@IfSticky", false);
            cmd.Parameters.AddWithValue("@ParticipatorIds", postUserOrOperatorId);
            cmd.Parameters.AddWithValue("@IfHasDraft", false);
            cmd.Parameters.AddWithValue("@IfDeleted", false);
            cmd.Parameters.AddWithValue("@Type", 0);
            cmd.Parameters.AddWithValue("@IfMoveHistory", false);
            cmd.Parameters.AddWithValue("@LocateTopicId", 0);
            cmd.Parameters.AddWithValue("@MoveDate", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@MoveUserOrOperatorId", 0);
            cmd.Parameters.AddWithValue("@IfFeatured", false);
            cmd.Parameters.AddWithValue("@IfContainsPoll", ifContainsPoll);
            if (ifTopicModeration == true)
                cmd.Parameters.AddWithValue("@ModerationStatus", 0);
            else
                cmd.Parameters.AddWithValue("@ModerationStatus", 2);
            cmd.Parameters.AddWithValue("@AnnouncementStartDate", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@AnnouncementEndDate", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@IfPayScoreRequired", IfPayScoreRequired);
            cmd.Parameters.AddWithValue("@Score", score);
            cmd.Parameters.AddWithValue("@IfReplyRequired", IfReplyRequired);




            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int AddTopicWithMoved(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int forumId, string subject, int postUserOrOperatorId, DateTime postTime, string content,
            int score, bool ifContainsPoll, bool IfPayScoreRequired, bool IfReplyRequired,int moveToTopicId,
            int userId,DateTime moveDate)
        {
            StringBuilder strSQL = new StringBuilder("select LastLoginIP from t_User" + conn.SiteId.ToString() + " where Id = @postUserOrOperatorId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@postUserOrOperatorId", postUserOrOperatorId);
            Double postIP = Convert.ToInt64(cmd.ExecuteScalar());
            strSQL = new StringBuilder("select Name from t_Forum_Forum where Id = @forumId");
            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            String forumName = Convert.ToString(cmd.ExecuteScalar());
            strSQL = new StringBuilder("Insert into t_Forum_Topic" + conn.SiteId.ToString());
            strSQL.Append(" ([ForumId],[ForumName],[Subject],[PostUserOrOperatorId],[PostTime],[PostIP],[LastPostId],[LastPostTime]");
            strSQL.Append(",[LastPostUserOrOperatorId],[NumberOfReplies],[NumberOfHits],[IfClosed],[IfMarkedAsAnswer],[IfSticky]");
            strSQL.Append(",[ParticipatorIds],[IfHasDraft],[IfDeleted],[Type],[IfMoveHistory],[LocateTopicId],[MoveDate]");
            strSQL.Append(",[MoveUserOrOperatorId],[IfFeatured],[IfContainsPoll],[ModerationStatus],[AnnouncementStartDate]");
            strSQL.Append(",[AnnouncementEndDate],[IfPayScoreRequired],[Score],[IfReplyRequired]) values ");
            strSQL.Append(" (@ForumId,@ForumName,@Subject,@PostUserOrOperatorId,@PostTime,@PostIP,@LastPostId,@LastPostTime");
            strSQL.Append(",@LastPostUserOrOperatorId,@NumberOfReplies,@NumberOfHits,@IfClosed,@IfMarkedAsAnswer,@IfSticky");
            strSQL.Append(",@ParticipatorIds,@IfHasDraft,@IfDeleted,@Type,@IfMoveHistory,@LocateTopicId,@MoveDate");
            strSQL.Append(",@MoveUserOrOperatorId,@IfFeatured,@IfContainsPoll,@ModerationStatus,@AnnouncementStartDate");
            strSQL.Append(",@AnnouncementEndDate,@IfPayScoreRequired,@Score,@IfReplyRequired)");
            strSQL.Append("select @@identity");
            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ForumId", forumId);
            cmd.Parameters.AddWithValue("@ForumName", forumName);
            cmd.Parameters.AddWithValue("@Subject", subject);
            cmd.Parameters.AddWithValue("@PostUserOrOperatorId", postUserOrOperatorId);
            cmd.Parameters.AddWithValue("@PostTime", postTime);
            cmd.Parameters.AddWithValue("@PostIP", postIP);
            cmd.Parameters.AddWithValue("@LastPostId", 0);
            cmd.Parameters.AddWithValue("@LastPostTime", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@LastPostUserOrOperatorId", 0);
            cmd.Parameters.AddWithValue("@NumberOfReplies", 0);
            cmd.Parameters.AddWithValue("@NumberOfHits", 0);
            cmd.Parameters.AddWithValue("@IfClosed", false);
            cmd.Parameters.AddWithValue("@IfMarkedAsAnswer", false);
            cmd.Parameters.AddWithValue("@IfSticky", false);
            cmd.Parameters.AddWithValue("@ParticipatorIds", postUserOrOperatorId);
            cmd.Parameters.AddWithValue("@IfHasDraft", false);
            cmd.Parameters.AddWithValue("@IfDeleted", false);
            cmd.Parameters.AddWithValue("@Type", 0);
            cmd.Parameters.AddWithValue("@IfMoveHistory", true);
            cmd.Parameters.AddWithValue("@LocateTopicId", moveToTopicId);
            cmd.Parameters.AddWithValue("@MoveDate", moveDate);
            cmd.Parameters.AddWithValue("@MoveUserOrOperatorId", userId);
            cmd.Parameters.AddWithValue("@IfFeatured", false);
            cmd.Parameters.AddWithValue("@IfContainsPoll", ifContainsPoll);
            cmd.Parameters.AddWithValue("@ModerationStatus", 2);
            cmd.Parameters.AddWithValue("@AnnouncementStartDate", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@AnnouncementEndDate", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@IfPayScoreRequired", IfPayScoreRequired);
            cmd.Parameters.AddWithValue("@Score", score);
            cmd.Parameters.AddWithValue("@IfReplyRequired", IfReplyRequired);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable GetLastMovedTopicInForum(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int forumId, int LocateTopicId)
        {
              StringBuilder strSQL = new StringBuilder("select top 1 a.*,b.Name as [PostUserOrOperatorName],a.IfDeleted as [TopicIfDeleted],b.IfDeleted as [PostUserOrOperatorIfDeleted],c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] ");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a ");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on b.Id = a.PostUserOrOperatorId");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on c.Id = a.LastPostUserOrOperatorId");
            strSQL.Append(" where a.LocateTopicId=@LocateTopicId and a.ForumId=@forumId order by a.MoveDate");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@LocateTopicId", LocateTopicId);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetMovedTopicsOfTopic(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int LocateTopicId)
        {
            StringBuilder strSQL = new StringBuilder("select a.*,b.Name as [PostUserOrOperatorName],b.IfDeleted as [PostUserOrOperatorIfDeleted],c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] ");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a ");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on b.Id = a.PostUserOrOperatorId");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on c.Id = a.LastPostUserOrOperatorId");
            strSQL.Append(" where a.LocateTopicId=@LocateTopicId and IfMoveHistory='true'");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@LocateTopicId", LocateTopicId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static void DeleteTopic(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {

            StringBuilder strSQL = new StringBuilder("Delete t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" where Id = @topicId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            cmd.ExecuteNonQuery();
        }

        public static void DeleteTopicsByForumId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
        {

            StringBuilder strSQL = new StringBuilder("Delete t_Forum_Topic" + conn.SiteId);
            strSQL.Append(" where ForumId = @forumId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateTopic(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId,
            string subject, string content, int score, bool ifReplyRequired, bool IfPayScoreRequired,
            bool ifContainsPoll)
        {

            StringBuilder strSQL = new StringBuilder("Update t_Forum_Topic" + conn.SiteId.ToString() + " set");
            strSQL.Append(" Subject = @subject,[Score]=@score,[IfReplyRequired]=@ifReplyRequired,");
            strSQL.Append("[IfPayScoreRequired]=@IfPayScoreRequired,[IfContainsPoll]=@ifContainsPoll");
            strSQL.Append(" where Id=@topicId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@topicId", topicId);
            cmd.Parameters.AddWithValue("@score", score);
            cmd.Parameters.AddWithValue("@ifReplyRequired", ifReplyRequired);
            cmd.Parameters.AddWithValue("@IfPayScoreRequired", IfPayScoreRequired);
            cmd.Parameters.AddWithValue("@ifContainsPoll", ifContainsPoll);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateTopicStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, bool ifClosed, bool ifMarkedAsAnswer)
        {

            StringBuilder strSQL = new StringBuilder("Update t_Forum_Topic" + conn.SiteId + " set");
            strSQL.Append(" IfClosed = @ifClosed, IfMarkedAsAnswer = @ifMarkedAsAnswer where Id = @topicId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ifClosed", ifClosed);
            cmd.Parameters.AddWithValue("@ifMarkedAsAnswer", ifMarkedAsAnswer);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateTopicFeatureStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, bool ifFeatured)
        {
            
            StringBuilder strSQL = new StringBuilder("Update t_Forum_Topic" + conn.SiteId + " set");
            strSQL.Append(" [IfFeatured]=@ifFeatured where Id = @topicId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ifFeatured", ifFeatured);
            cmd.Parameters.AddWithValue("@topicId", topicId);
            
            cmd.ExecuteNonQuery();
        }

        public static void UpdateTopicStickyStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, bool ifSticky)
        {

            StringBuilder strSQL = new StringBuilder("Update t_Forum_Topic" + conn.SiteId + " set");
            strSQL.Append(" IfSticky = @ifSticky where Id = @topicId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ifSticky", ifSticky);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateTopicForumIdAndForumName(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, int forumId, string forumName)
        {

            StringBuilder strSQL = new StringBuilder("Update t_Forum_Topic" + conn.SiteId + " set");
            strSQL.Append(" ForumId = @forumId, ForumName = @forumName where Id = @topicId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.Parameters.AddWithValue("@forumName", forumName);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateTopicForumNameByForumId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, string forumName)
        {

            StringBuilder strSQL = new StringBuilder(" update t_Forum_Topic" + conn.SiteId + " set");
            strSQL.Append(" ForumName = @forumName where ForumId = @forumId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumName", forumName);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            cmd.ExecuteNonQuery();


        }
        public static void UpdateTopicNumberOfReplies(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, int numberOfReplies)
        {

            StringBuilder strSQL = new StringBuilder("Update t_Forum_Topic" + conn.SiteId.ToString() + " set");
            strSQL.Append(" NumberOfReplies=@numberOfReplies where Id=@topicId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@numberOfReplies", numberOfReplies);
            cmd.Parameters.AddWithValue("@topicId", topicId);


            cmd.ExecuteNonQuery();
        }

        public static void UpdateTopicNumberOfHits(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, int numberOfHits)
        {

            StringBuilder strSQL = new StringBuilder("Update t_Forum_Topic" + conn.SiteId.ToString() + " set");
            strSQL.Append(" NumberOfHits = @numberOfHits where Id = @topicId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@numberOfHits", numberOfHits);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateTopicLastPostInfo(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, int lastPostUserOrOperatorId, int lastPostId, DateTime lastPostTime)
        {

            StringBuilder strSQL = new StringBuilder("Update t_Forum_Topic" + conn.SiteId + " set");
            strSQL.Append(" LastPostId=@LastPostId, LastPostTime = @lastPostTime, LastPostUserOrOperatorId = @lastPostUserOrOperatorId where Id=@topicId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@LastPostId", lastPostId);
            cmd.Parameters.AddWithValue("@lastPostTime", lastPostTime);
            cmd.Parameters.AddWithValue("@lastPostUserOrOperatorId", lastPostUserOrOperatorId);
            cmd.Parameters.AddWithValue("@topicId", topicId);


            cmd.ExecuteNonQuery();
        }

        public static void UpdateTopicParticipatorIds(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, int[] participatorIds)
        {
            string participators = "";
            for (int i = 0; i < participatorIds.Length - 1; i++)
            {
                participators = participators + Convert.ToString(participatorIds[i]) + ",";

            }
            participators = participators + Convert.ToString(participatorIds[participatorIds.Length - 1]);


            StringBuilder strSQL = new StringBuilder("update t_Forum_Topic" + conn.SiteId + " set ParticipatorIds = @participators where Id = @topicId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@participators", participators);
            cmd.Parameters.AddWithValue("@topicId", topicId);


            cmd.ExecuteNonQuery();
        }


        public static DataTable GetAllTopics(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
        {

            StringBuilder strSQL = new StringBuilder("select a.*,b.Name as [PostUserOrOperatorName],b.IfDeleted as [PostUserOrOperatorIfDeleted],c.Name as [LastPostUserOrOperatorName],c.IfDeleted as [LastPostUserOrOperatorIfDeleted] ");
            strSQL.Append(" from t_Forum_Topic" + conn.SiteId + " a ");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on b.Id = a.PostUserOrOperatorId");
            strSQL.Append(" left join t_User" + conn.SiteId + " c on c.Id = a.LastPostUserOrOperatorId");
            strSQL.Append(" where a.ForumId=@forumId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        /*-----------------------------2.0------------------------------*/
        public static DataTable GetNotDeletedTopicsOfForumByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            string name, DateTime startDate, DateTime endDate, int pageIndex, int pageSize, int forumId, string queryConditions,
            string orderField, string orderMethod)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            if (orderField == "LastPostTime")
                orderField = "a.LastPostTime";
            else if (orderField == "Name")
                orderField = "c.Name";

            string subject = CommonFunctions.SqlReplace(queryConditions);

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from ");
            strSQL.Append(string.Format("(select ROW_NUMBER() over(order by {0} {1}) as row,", orderField, orderMethod));
            //strSQL.Append(string.Format(" a.*, b.Name as PostUserOrOperatorName from [t_Forum_Topic{0}] a , t_User{0} b", conn.SiteId));
            //strSQL.Append(" where a.IfDeleted='false'");
            strSQL.Append("a.*, c.Name as PostUserOrOperatorName,d.Name as LastPostUserName from");
            strSQL.Append(string.Format(" (select b.* from  [t_Forum_Topic{0}] b where b.Id not in", conn.SiteId));
            strSQL.Append(string.Format(" (select topicid from t_Forum_Announcement)) a, t_User{0} c,t_User{0} d", conn.SiteId));
            strSQL.Append(" where a.IfDeleted='false' and a.PostUserOrOperatorId=c.Id");
            strSQL.Append(" and a.LastPostUserOrOperatorId = d.Id");

            if (startDate > new DateTime()) // not Init date
                strSQL.Append(" and PostTime >= @startDate");
            if (endDate > new DateTime()) // not Init date
                strSQL.Append(" and PostTime <= @endDate");
            if (name!="")//-1 is null
                strSQL.Append(" and c.Name=@name");
           
            strSQL.Append(" and ForumId=@forumId");
            if (subject != "")
                strSQL.Append(" and [Subject] like '%' + @subject + '%' escape '/'");
            strSQL.Append(") t");
            strSQL.Append(" where row between @startIndex and @endIndex");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Value
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.Parameters.AddWithValue("@orderField", orderField);
            cmd.Parameters.AddWithValue("@orderMethod", orderMethod);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);
            #endregion
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetAllNotDeletedTopicsOfForumByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction,
           string name, DateTime startDate, DateTime endDate, int pageIndex, int pageSize, string queryConditions,
           string orderField, string orderMethod)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            if (orderField == "Last Post Time")
                orderField = "a.LastPostTime";
            else if (orderField == "Name")
                orderField = "c.Name";

            string subject = CommonFunctions.SqlReplace(queryConditions);

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from ");
            strSQL.Append(string.Format("(select ROW_NUMBER() over(order by {0} {1}) as row,", orderField, orderMethod));
            //strSQL.Append(string.Format(" a.*, b.Name as PostUserOrOperatorName from [t_Forum_Topic{0}] a , t_User{0} b", conn.SiteId));
            //strSQL.Append(" where a.IfDeleted='false'");
            strSQL.Append("a.*, c.Name as PostUserOrOperatorName,d.Name as LastPostUserName from");
            strSQL.Append(string.Format(" (select b.* from  [t_Forum_Topic{0}] b where b.Id not in", conn.SiteId));
            strSQL.Append(string.Format(" (select topicid from t_Forum_Announcement)) a, t_User{0} c,t_User{0} d", conn.SiteId));
            strSQL.Append(" where a.IfDeleted='false' and a.PostUserOrOperatorId=c.Id");
            strSQL.Append(" and a.LastPostUserOrOperatorId = d.Id");

            if (startDate > new DateTime()) // not Init date
                strSQL.Append(" and PostTime >= @startDate");
            if (endDate > new DateTime()) // not Init date
                strSQL.Append(" and PostTime <= @endDate");
            if (name != "")//-1 is null
                strSQL.Append(" and c.Name=@name");
            
            if (subject != "")
                strSQL.Append(" and [Subject] like '%' + @subject + '%' escape '/'");
            strSQL.Append(") t");
            strSQL.Append(" where row between @startIndex and @endIndex");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Value
            cmd.Parameters.AddWithValue("@orderField", orderField);
            cmd.Parameters.AddWithValue("@orderMethod", orderMethod);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);
            #endregion
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static int GetCountOfNotDeletedTopicsOfForumByQueryAndPaging(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
           string name, DateTime startDate, DateTime endDate, int forumId, string queryConditions)
        {
            string subject = CommonFunctions.SqlReplace(queryConditions);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select count (*) from");
            strSQL.Append(string.Format(" (select b.* from  [t_Forum_Topic{0}] b where b.Id not in", conn.SiteId));
            strSQL.Append(string.Format(" (select topicid from t_Forum_Announcement)) a, t_User{0} c,t_User{0} d", conn.SiteId));
            strSQL.Append(" where a.IfDeleted='false' and a.PostUserOrOperatorId=c.Id");
            strSQL.Append(" and a.LastPostUserOrOperatorId = d.Id");

            if (startDate > new DateTime()) // not Init date
                strSQL.Append(" and PostTime >= @startDate");
            if (endDate > new DateTime()) // not Init date
                strSQL.Append(" and PostTime <= @endDate");
            if (name!="")//-1 is null
                strSQL.Append(" and c.name=@name");
            
            strSQL.Append(" and ForumId=@forumId");
            if (subject != "")
                strSQL.Append(" and [Subject] like '%' + @subject + '%' escape '/'");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Value
            cmd.Parameters.AddWithValue("@forumId", forumId);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@subject", subject);
            #endregion
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int GetCountOfAllNotDeletedTopicsOfForumByQueryAndPaging(
           SqlConnectionWithSiteId conn, SqlTransaction transaction,
          string name, DateTime startDate, DateTime endDate,string queryConditions)
        {
            string subject = CommonFunctions.SqlReplace(queryConditions);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select count (*) from");
            strSQL.Append(string.Format(" (select b.* from  [t_Forum_Topic{0}] b where b.Id not in", conn.SiteId));
            strSQL.Append(string.Format(" (select topicid from t_Forum_Announcement)) a, t_User{0} c,t_User{0} d", conn.SiteId));
            strSQL.Append(" where a.IfDeleted='false' and a.PostUserOrOperatorId=c.Id");
            strSQL.Append(" and a.LastPostUserOrOperatorId = d.Id");

            if (startDate > new DateTime()) // not Init date
                strSQL.Append(" and PostTime >= @startDate");
            if (endDate > new DateTime()) // not Init date
                strSQL.Append(" and PostTime <= @endDate");
            if (name != "")//-1 is null
                strSQL.Append(" and c.name=@name");

            if (subject != "")
                strSQL.Append(" and [Subject] like '%' + @subject + '%' escape '/'");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Value
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@subject", subject);
            #endregion
            return Convert.ToInt32(cmd.ExecuteScalar());
        }


        public static void LogicDelete(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("update [t_Forum_Topic{0}] set IfDeleted='true' where Id=@topicId", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            cmd.ExecuteNonQuery();
        }

        public static void Restore(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("update [t_Forum_Topic{0}] set IfDeleted='false' where Id=@topicId", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            cmd.ExecuteNonQuery();
        }

        public static void RefuseModeration(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("update [t_Forum_Topic{0}] set [ModerationStatus]=1 where Id=@topicId", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            cmd.ExecuteNonQuery();
        }

        public static void AcceptModeration(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("update [t_Forum_Topic{0}] set [ModerationStatus]=2 where Id=@topicId", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            cmd.ExecuteNonQuery();
        }

        public static DataTable GetNotDeletedTopicsOfSiteByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, string queryConditions)
        {
            return null;
        }

        public static int GetCountOfNotDeletedTopicsOfSiteByQuery(
            SqlConnectionWithSiteId conn, SqlTransaction transaction, string queryConditions)
        {
            return 0;
        }

        public static bool IfAnnoucement(
            SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select * from t_Forum_Announcement where TopicId =@topicId and SiteId=@siteId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);

            SqlDataReader sr = cmd.ExecuteReader();
            bool ifannoucement = sr.Read();
            sr.Close();
            return ifannoucement;
        }
              

        /*Advanced Search*/
        public static DataTable GetTopicsByAdvancedSearch(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int pageIndex, int pageSize, string keywords, bool ifAllForums, bool ifCategory,
            bool ifForum, int id, DateTime BeginDate, DateTime EndDate, int searchMethod, List<int> forumIdsHaveNoPermission)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from");
            strSQL.Append(" (select ROW_NUMBER() over(order by PostTime asc) as row,");
            strSQL.Append("f.*,c.Name as [PostUserOrOperatorName],c.IfDeleted as [PostUserOrOperatorIfDeleted],");
            strSQL.Append("d.Name as [LastPostUserOrOperatorName],d.IfDeleted as [LastPostUserOrOperatorIfDeleted]");
            strSQL.Append(" from(");
            strSQL.Append(string.Format(" select distinct TopicId from [t_Forum_Post{0}] a, t_Forum_Topic{0} b", conn.SiteId));
            strSQL.Append(string.Format(" where a.TopicId=b.Id and a.TopicId not in (select distinct TopicId from t_Forum_Announcement where SiteId={0}) ", conn.SiteId));
            /*not (waiting for moderation, refuse moderation, approve abuse,deleted,moved)*/
            strSQL.Append(" and a.ModerationStatus='2' and b.IfMoveHistory='false' and a.IfDeleted='false' and b.IfDeleted='false' and a.IfSpam='false'");
            /*forum with have permission*/
            if (forumIdsHaveNoPermission != null && forumIdsHaveNoPermission.Count > 0)
            {
                StringBuilder cmd1 = new StringBuilder();
                foreach (int forumId in forumIdsHaveNoPermission)
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
                strSQL.Append(" and b.ForumId in(select id from t_Forum_Forum where CategoryId=@id)");
            else if (ifForum)
                strSQL.Append(" and b.ForumId=@id");
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

            strSQL.Append(string.Format(") e, t_Forum_Topic{0} f,t_User{0} c, t_User{0} d", conn.SiteId));
            strSQL.Append(" where e.TopicId=f.Id and f.PostUserOrOperatorId = c.Id and f.LastPostUserOrOperatorId = d.Id");
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

        public static int GetCountOfTopicsByAdvancedSearch(SqlConnectionWithSiteId conn, SqlTransaction transaction,
          int pageIndex, int pageSize, string keywords, bool ifAllForums, bool ifCategory,
          bool ifForum, int id, DateTime BeginDate, DateTime EndDate, int searchMethod, List<int> forumIdsHaveNoPermission)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select COUNT(*)");
            strSQL.Append(" from(");
            strSQL.Append(string.Format(" select distinct TopicId from [t_Forum_Post{0}] a, t_Forum_Topic{0} b", conn.SiteId));
            strSQL.Append(string.Format(" where a.TopicId=b.Id and a.TopicId not in (select distinct TopicId from t_Forum_Announcement where SiteId={0}) ", conn.SiteId));
            /*not (waiting for moderation, refuse moderation, approve abuse,deleted,moved)*/
            strSQL.Append(" and a.ModerationStatus='2' and b.IfMoveHistory='false' and a.IfDeleted='false' and b.IfDeleted='false' and a.IfSpam='false'");
            /*forum with have permission*/
            if (forumIdsHaveNoPermission != null && forumIdsHaveNoPermission.Count > 0)
            {
                StringBuilder cmd1 = new StringBuilder();
                foreach (int forumId in forumIdsHaveNoPermission)
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
                strSQL.Append(" and b.ForumId in(select id from t_Forum_Forum where CategoryId=@id)");
            else if (ifForum)
                strSQL.Append(" and b.ForumId=@id");
            /*Search Method*/
            if (searchMethod == 0)
                strSQL.Append(" and ((a.[Subject] like '%' + @keywords + '%' escape '/') or (a.TextContent like '%' + @keywords + '%' escape '/'))");
            else if (searchMethod == 1)
                strSQL.Append(" and a.TextContent like '%' + @keywords + '%' escape '/'");
            else if (searchMethod == 2)
                strSQL.Append(" and a.[Subject] like '%' + @keywords + '%' escape '/' and a.ifTopic='true'");
            else if (searchMethod == 3)
                strSQL.Append(" and a.TextContent like '%' + @keywords + '%' escape '/' and a.ifTopic='true'");

            strSQL.Append(string.Format(") e, t_Forum_Topic{0} f,t_User{0} c, t_User{0} d", conn.SiteId));
            strSQL.Append(" where e.TopicId=f.Id and f.PostUserOrOperatorId = c.Id and f.LastPostUserOrOperatorId = d.Id");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            if (BeginDate != new DateTime())
                cmd.Parameters.AddWithValue("@BeginDate", BeginDate);
            if (EndDate != new DateTime())
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@keywords", keywords);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        #region Moderator Panel
        public static DataTable GetNotDeletedTopicsByModeratorWithQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int moderatorId,
            string name, DateTime startDate, DateTime endDate, int forumId, string keywords, int pageIndex, int pageSize, string orderField, string orderDirection)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            if (orderField == "LastPostTime")
                orderField = "a.LastPostTime";
            else if (orderField == "Name")
                orderField = "c.Name";

            string subject = CommonFunctions.SqlReplace(keywords);

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from ");
            strSQL.Append(string.Format("(select ROW_NUMBER() over(order by {0} {1}) as row,", orderField, orderDirection));
            //strSQL.Append(string.Format(" a.*, b.Name as PostUserOrOperatorName from [t_Forum_Topic{0}] a , t_User{0} b", conn.SiteId));
            //strSQL.Append(" where a.IfDeleted='false'");
            strSQL.Append("a.*, c.Name as PostUserOrOperatorName,d.Name as LastPostUserName from");
            strSQL.Append(string.Format(" (select b.* from  [t_Forum_Topic{0}] b where b.Id not in", conn.SiteId));
            strSQL.Append(string.Format(" (select topicid from t_Forum_Announcement)) a, t_User{0} c,t_User{0} d", conn.SiteId));
            strSQL.Append(" where a.IfDeleted='false' and a.PostUserOrOperatorId=c.Id");
            strSQL.Append(" and a.LastPostUserOrOperatorId = d.Id");

            if (startDate > new DateTime()) // not Init date
                strSQL.Append(" and PostTime >= @startDate");
            if (endDate > new DateTime()) // not Init date
                strSQL.Append(" and PostTime <= @endDate");
            if (name !="")//-1 is null
                strSQL.Append(" and c.Name=@name");
            
            strSQL.Append(" and ForumId=@forumId");
            if (subject != "")
                strSQL.Append(" and [Subject] like '%' + @subject + '%' escape '/'");
            strSQL.Append(") t");
            strSQL.Append(" where row between @startIndex and @endIndex");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Value
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);
            //cmd.Parameters.AddWithValue("@orderField", orderField);
            //cmd.Parameters.AddWithValue("@orderMethod", orderDirection);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);
            #endregion
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetAllNotDeletedTopicsByModeratorWithQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int moderatorId,
            string name, DateTime startDate, DateTime endDate, string keywords, int pageIndex, int pageSize, string orderField, string orderDirection)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            if (orderField == "Last Post Time")
                orderField = "a.LastPostTime";
            else if (orderField == "Name")
                orderField = "c.Name";

            string subject = CommonFunctions.SqlReplace(keywords);

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from ");
            strSQL.Append(string.Format("(select ROW_NUMBER() over(order by {0} {1}) as row,", orderField, orderDirection));
            //strSQL.Append(string.Format(" a.*, b.Name as PostUserOrOperatorName from [t_Forum_Topic{0}] a , t_User{0} b", conn.SiteId));
            //strSQL.Append(" where a.IfDeleted='false'");
            strSQL.Append("a.*, c.Name as PostUserOrOperatorName,d.Name as LastPostUserName from");
            strSQL.Append(string.Format(" (select b.* from  [t_Forum_Topic{0}] b where b.Id not in", conn.SiteId));
            strSQL.Append(string.Format(" (select topicid from t_Forum_Announcement)) a, t_User{0} c,t_User{0} d", conn.SiteId));
            strSQL.Append(" where a.IfDeleted='false' and a.PostUserOrOperatorId=c.Id");
            strSQL.Append(" and a.LastPostUserOrOperatorId = d.Id");

            if (startDate > new DateTime()) // not Init date
                strSQL.Append(" and PostTime >= @startDate");
            if (endDate > new DateTime()) // not Init date
                strSQL.Append(" and PostTime <= @endDate");
            if (name != "")//-1 is null
                strSQL.Append(" and c.Name=@name");
            
            strSQL.Append(" and ForumId in (select ForumId from t_Forum_Moderator where UserOrOperatorId=@moderatorId) ");
            if (subject != "")
                strSQL.Append(" and [Subject] like '%' + @subject + '%' escape '/'");
            strSQL.Append(") t");
            strSQL.Append(" where row between @startIndex and @endIndex");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Value
            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);
            //cmd.Parameters.AddWithValue("@orderField", orderField);
            //cmd.Parameters.AddWithValue("@orderMethod", orderDirection);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);
            #endregion
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }


        public static int GetCountOfNotDeletedTopicsByModeratorWithQuery(
            SqlConnectionWithSiteId conn, SqlTransaction transaction, int moderatorId, string queryConditions,
            DateTime startDate, DateTime endDate, string name, int forumId)
        {
            string subject = CommonFunctions.SqlReplace(queryConditions);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select count (*) from");
            strSQL.Append(string.Format(" (select b.* from  [t_Forum_Topic{0}] b where b.Id not in", conn.SiteId));
            strSQL.Append(string.Format(" (select topicid from t_Forum_Announcement)) a, t_User{0} c,t_User{0} d", conn.SiteId));
            strSQL.Append(" where a.IfDeleted='false' and a.PostUserOrOperatorId=c.Id");
            strSQL.Append(" and a.LastPostUserOrOperatorId = d.Id");

            if (startDate > new DateTime()) // not Init date
                strSQL.Append(" and PostTime >= @startDate");
            if (endDate > new DateTime()) // not Init date
                strSQL.Append(" and PostTime <= @endDate");
            if (name!="")//-1 is null
                strSQL.Append(" and c.Name=@name");

            strSQL.Append(" and ForumId=@forumId");

            if (subject != "")
                strSQL.Append(" and [Subject] like '%' + @subject + '%' escape '/'");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Value
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@subject", subject);
            #endregion
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int GetCountOfAllNotDeletedTopicsByModeratorWithQuery(
            SqlConnectionWithSiteId conn, SqlTransaction transaction, int moderatorId, string queryConditions,
            DateTime startDate, DateTime endDate, string name)
        {
            string subject = CommonFunctions.SqlReplace(queryConditions);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select count (*) from");
            strSQL.Append(string.Format(" (select b.* from  [t_Forum_Topic{0}] b where b.Id not in", conn.SiteId));
            strSQL.Append(string.Format(" (select topicid from t_Forum_Announcement)) a, t_User{0} c,t_User{0} d", conn.SiteId));
            strSQL.Append(" where a.IfDeleted='false' and a.PostUserOrOperatorId=c.Id");
            strSQL.Append(" and a.LastPostUserOrOperatorId = d.Id");

            if (startDate > new DateTime()) // not Init date
                strSQL.Append(" and PostTime >= @startDate");
            if (endDate > new DateTime()) // not Init date
                strSQL.Append(" and PostTime <= @endDate");
            if (name != "")//-1 is null
                strSQL.Append(" and c.Name=@name");
            
            strSQL.Append(" and ForumId in (select ForumId from t_Forum_Moderator where UserOrOperatorId=@moderatorId) ");

            if (subject != "")
                strSQL.Append(" and [Subject] like '%' + @subject + '%' escape '/'");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Value
            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);
            if (startDate > new DateTime())
                cmd.Parameters.AddWithValue("@startDate", startDate);
            if (endDate > new DateTime())
                cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@subject", subject);
            #endregion
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        #endregion
    }
}
