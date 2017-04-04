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
    public class ForumAccess
    {
        public static DataTable GetForumsByCategoryId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select ");
            strSQL.Append("forum.*,lpuser.Name as [LastPostCreatedUserOrOperatorName],lpuser.IfDeleted as [LastPostCreatedUserOrOperatorIfDeleted] ");
            strSQL.Append("from t_Forum_Forum forum ");
            strSQL.Append(string.Format("left join t_User{0} lpuser ", conn.SiteId));
            strSQL.Append("on forum.LastPostCreatedUserOrOperatorId = lpuser.Id ");
            strSQL.Append("where forum.CategoryId=@CategoryId ");
            strSQL.Append("order by forum.OrderId asc");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn,transaction);
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetNotHiddenForumsByCategoryId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select ");
            strSQL.Append("forum.*,lpuser.name as [LastPostCreatedUserOrOperatorName],lpuser.IfDeleted as [LastPostCreatedUserOrOperatorIfDeleted] ");
            strSQL.Append("from t_Forum_Forum forum ");
            strSQL.Append(string.Format("left join t_User{0} lpuser ", conn.SiteId));
            strSQL.Append("on forum.LastPostCreatedUserOrOperatorId = lpuser.Id ");
            strSQL.Append("where forum.CategoryId=@CategoryId and forum.Status != 1 ");
            strSQL.Append("order by forum.OrderId asc");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn,transaction);
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetForumByForumId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select ");
            strSQL.Append("forum.Id, CategoryId,OrderId,forum.Name,[Status],NumberOfTopics,NumberOfPosts,[IfAllowPostNeedingReplayTopic],[IfAllowPostNeedingPayTopic],");
            strSQL.Append("forum.[Description],LastPostId,LastPostTopicId,LastPostSubject,LastPostCreatedUserOrOperatorId,lpuser.Name as [LastPostCreatedUserOrOperatorName],lpuser.IfDeleted as [lastPostCreatedUserOrOperatorIfDeleted],LastPostPostTime ");
            strSQL.Append("from t_Forum_Forum forum ");
            strSQL.Append("left join t_User" + conn.SiteId + " lpuser ");
            strSQL.Append("on forum.LastPostCreatedUserOrOperatorId = lpuser.Id ");
            strSQL.Append("where forum.Id=@Id ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn,transaction);
            cmd.Parameters.AddWithValue("@Id", forumId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }        

        public static int AddForum(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, string name, string description,bool ifAllowPostNeedingReplayTopic,bool ifAllowPostNeedingPayTopic)
        {

            StringBuilder strSQL = new StringBuilder("update t_Forum_Forum set OrderId = OrderId +1 where CategoryId = @categoryId;");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);

            cmd.ExecuteNonQuery();

            strSQL.Remove(0, strSQL.Length);
            strSQL.Append("insert into t_Forum_Forum (CategoryId, OrderId, Name, Description,");
            strSQL.Append(" Status, NumberOfTopics, NumberOfPosts, LastPostId,LastPostTopicId, LastPostSubject, LastPostCreatedUserOrOperatorId, LastPostPostTime, ");
            strSQL.Append(" [IfAllowPostNeedingReplayTopic],[ifAllowPostNeedingPayTopic])");
            strSQL.Append(" values(@categoryId, @orderId, @name, @description,");
            strSQL.Append(" @status, @numberOfTopics, @numberOfPosts, @lastPostId,@lastPostTopicId, @lastPostSubject, @lastPostCreatedUserOrOperatorId, @lastPostPostTime,");
            strSQL.Append(" @ifAllowPostNeedingReplayTopic,@ifAllowPostNeedingPayTopic);");
            strSQL.Append(" select @@identity");

            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            cmd.Parameters.AddWithValue("@orderId", 0);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@ifAllowPostNeedingReplayTopic", ifAllowPostNeedingReplayTopic);
            cmd.Parameters.AddWithValue("@ifAllowPostNeedingPayTopic", ifAllowPostNeedingPayTopic);
            cmd.Parameters.AddWithValue("@status", Convert.ToInt16(EnumForumStatus.Open));
            cmd.Parameters.AddWithValue("@numberOfTopics", 0);
            cmd.Parameters.AddWithValue("@numberOfPosts", 0);
            cmd.Parameters.AddWithValue("@lastPostId", 0);
            cmd.Parameters.AddWithValue("@lastPostTopicId", 0);
            cmd.Parameters.AddWithValue("@lastPostSubject", string.Empty);
            cmd.Parameters.AddWithValue("@lastPostCreatedUserOrOperatorId", 0);
            cmd.Parameters.AddWithValue("@lastPostPostTime", "1900-01-01");

            int forumId = Convert.ToInt32(cmd.ExecuteScalar());

            return forumId;
        }

        public static void UpdateForum(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, string name, string description, int categoryId, EnumForumStatus forumStatus, bool ifAllowPostNeedingReplayTopic, bool ifAllowPostNeedingPayTopic)
        {

            StringBuilder strSQL = new StringBuilder("Update t_Forum_Forum  set ");
            strSQL.Append("Name = @name, Description = @description, CategoryId = @categoryId, Status = @status, ");
            strSQL.Append("IfAllowPostNeedingReplayTopic=@ifAllowPostNeedingReplayTopic, IfAllowPostNeedingPayTopic=@ifAllowPostNeedingPayTopic ");
            strSQL.Append(" where Id = @forumId;");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            cmd.Parameters.AddWithValue("@status", Convert.ToInt16(forumStatus));
            cmd.Parameters.AddWithValue("@ifAllowPostNeedingReplayTopic", ifAllowPostNeedingReplayTopic);
            cmd.Parameters.AddWithValue("@ifAllowPostNeedingPayTopic", ifAllowPostNeedingPayTopic);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            cmd.ExecuteNonQuery();

        }
        public static void UpdateForum(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, string name, string description, int oldCategoryId, int newCategoryId, EnumForumStatus forumStatus)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select OrderId From t_Forum_Forum Where Id=@forumId;");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            int oldOrderId = Convert.ToInt32(cmd.ExecuteScalar());

            strSQL.Remove(0, strSQL.Length);
            strSQL.Append("update t_Forum_Forum set OrderId = OrderId-1 where OrderId > @oldOrderId and CategoryId = @oldCategoryId;");
            strSQL.Append("update t_Forum_Forum set OrderId = OrderId+1 where CategoryId = @newCategoryId;");
            strSQL.Append("update t_Forum_Forum set OrderId = @orderID where Id = @forumId;");

            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@oldOrderId", oldOrderId);
            cmd.Parameters.AddWithValue("@oldCategoryId", oldCategoryId);
            cmd.Parameters.AddWithValue("@newCategoryId", newCategoryId);
            cmd.Parameters.AddWithValue("@orderId", 0);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            cmd.ExecuteNonQuery();

            strSQL = new StringBuilder("Update t_Forum_Forum  set ");
            strSQL.Append("Name = @name, Description = @description, CategoryId = @categoryId, Status = @status where Id = @forumId;");

            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@categoryId", newCategoryId);
            cmd.Parameters.AddWithValue("@status", Convert.ToInt16(forumStatus));
            cmd.Parameters.AddWithValue("@forumId", forumId);

            cmd.ExecuteNonQuery();

        }
        public static void DeleteForum(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select CategoryId, OrderId From t_Forum_Forum Where Id=@forumId;");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            int categoryId = Convert.ToInt32(table.Rows[0]["CategoryId"]);
            int orderId = Convert.ToInt32(table.Rows[0]["OrderId"]);


            strSQL.Remove(0, strSQL.Length);
            strSQL = new StringBuilder("Delete t_Forum_Forum where Id = @forumId;");
            strSQL.Append("update t_Forum_Forum set OrderId = OrderId-1 where OrderId > @orderId and CategoryId = @categoryId;");

            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.Parameters.AddWithValue("@orderId", orderId);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);

            cmd.ExecuteNonQuery();

            strSQL.Remove(0, strSQL.Length);
            strSQL = new StringBuilder("Delete t_Forum_Moderator where ForumId = @forumId;");

            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            cmd.ExecuteNonQuery();

        }

        public static void DeleteForumsByCategoryId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId)
        {

            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("delete t_forum_forum ");
            strSQL.Append("where ");
            strSQL.Append("categoryid = @categoryid ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@categoryid", categoryId);

            cmd.ExecuteNonQuery();
        }

        public static void SortForumsUp(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, int forumId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select OrderId From t_Forum_Forum Where Id=@forumId;");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            int orderId = Convert.ToInt32(cmd.ExecuteScalar());

            strSQL.Remove(0, strSQL.Length);
            strSQL.Append("update t_Forum_Forum set OrderId = OrderId+1 where OrderId = @orderId and CategoryId = @categoryId;");
            strSQL.Append("update t_Forum_Forum set OrderId = OrderId-1 where Id=@forumId;");
            
            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            cmd.Parameters.AddWithValue("@orderId", orderId - 1);

            cmd.ExecuteNonQuery();
        }
        public static void SortForumsDown(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, int forumId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select OrderId From t_Forum_Forum Where Id=@forumId;");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            int orderId = Convert.ToInt32(cmd.ExecuteScalar());

            strSQL.Remove(0, strSQL.Length);
            
            strSQL.Append("update t_Forum_Forum set OrderId = OrderId-1 where OrderId = @orderId and CategoryId = @categoryId;");
            strSQL.Append("update t_Forum_Forum set OrderId = OrderId+1 where Id=@forumId;");

            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            cmd.Parameters.AddWithValue("@orderId", orderId + 1);


            cmd.ExecuteNonQuery();
        }
        public static int GetCountOfForumsByCategoryId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId)
        {

            StringBuilder strSQL = new StringBuilder("select count(1) from t_Forum_Forum where CategoryId=@CategoryId ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);

            DataTable table = new DataTable();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void UpdateNumberOfTopics(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int numberOfTopics)
        {

            StringBuilder strSQL = new StringBuilder("Update t_Forum_Forum set ");
            strSQL.Append("NumberOfTopics = @numberOfTopics where Id=@forumId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@numberOfTopics", numberOfTopics);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateNumberOfPosts(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int numberOfPosts)
        {

            StringBuilder strSQL = new StringBuilder("Update t_Forum_Forum set ");
            strSQL.Append("NumberOfPosts = @numberOfPosts where Id=@forumId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@numberOfPosts", numberOfPosts);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateLastCreateInfo(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int lastCreateUserOrOperatorId, DateTime lastCreateTime, int lastPostId,int lastPostTopicId, string lastPostSubject)
        {

            StringBuilder strSQL = new StringBuilder("Update t_Forum_Forum set ");
            strSQL.Append("LastPostId = @lastPostId,LastPostTopicId=@LastPostTopicId, LastPostSubject = @lastPostSubject, LastPostCreatedUserOrOperatorId = @lastCreateUserOrOperatorId, LastPostPostTime = @lastCreateTime where Id=@forumId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@lastPostId", lastPostId);
            cmd.Parameters.AddWithValue("@lastPostTopicId", lastPostTopicId);
            cmd.Parameters.AddWithValue("@lastPostSubject", lastPostSubject);
            cmd.Parameters.AddWithValue("@lastCreateUserOrOperatorId", lastCreateUserOrOperatorId);
            cmd.Parameters.AddWithValue("@lastCreateTime", lastCreateTime);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            cmd.ExecuteNonQuery();
        }

        /*--------------------------2.0------------------------------*/
        public static int GetCountOfForumsById(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id)
        {
            return 0;
        }

        public static DataTable GetAllForumsOfAnnoucement(SqlConnectionWithSiteId conn, SqlTransaction transaction, int AnnoucementId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from t_Forum_Forum where Id in(");
            strSQL.Append("select CategoryOrForumId from t_Forum_Announcement where TopicId=@id and SiteId=@siteId)");
            //StringBuilder strSQL = new StringBuilder();
            //strSQL.Append("select * from t_Forum_Announcement where TopicId=@Id and SiteId=@siteId");
            
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn,transaction);
            cmd.Parameters.AddWithValue("@id", AnnoucementId);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetAllForumsOfModeratorId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int moderatorId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from t_Forum_Forum where Id in(");
            strSQL.Append("select ForumId from t_Forum_Moderator where UserOrOperatorId=@moderatorId and SiteId=@siteId) ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@moderatorId", moderatorId);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static bool IfInheritPermission(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" select IfInheritPermission from t_Forum_Forum where Id=@forumId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            return Convert.ToBoolean(cmd.ExecuteScalar());
        }

        public static void UpdateIfInheritPermission(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, bool ifInheritPermission)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" Update t_Forum_Forum set IfInheritPermission=@ifInheritPermission where Id=@forumId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ifInheritPermission", ifInheritPermission);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.ExecuteNonQuery();
        }
    }
}
