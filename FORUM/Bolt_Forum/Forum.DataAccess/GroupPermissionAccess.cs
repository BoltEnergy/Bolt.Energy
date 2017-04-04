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
    public class GroupPermissionAccess
    {
        static readonly string FIELDS =
            "GroupId,IfAllowViewForum,IfAllowViewTopic,IfAllowPost,IfAllowCustomizeAvatar,MaxLengthofSignature,IfSignatureAllowUrl,IfSignatureAllowUploadImage,MinIntervalForPost,MaxLengthOfPost,IfAllowUrl,IfAllowUploadImage,IfAllowUploadAttachment,MaxCountOfAttacmentsForOnePost,MaxSizeOfOneAttachment,MaxSizeOfAllAttachments,MaxCountOfMessageSendOneDay,IfAllowSearch,MinIntervalForSearch,IfPostNotNeedModeration";

        #region Add
        public static int AddPermission(
            int groupId, bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost,
            bool ifAllowCustomizeAvatar, int maxLengthofSignature, 
            bool ifSignatureAllowUrl,bool ifSignatureAllowInsertImage,
            int minIntervalForPost,int maxLengthOfPost,
            bool ifAllowUrl, bool ifAllowUploadImage, 
            bool IfAllowUploadAttachment,int maxCountOfAttacmentsForOnePost, int maxSizeOfOneAttachment, int MaxSizeOfAllAttachments,
            int maxCountOfMessageSendOneDay, bool ifAllowSearch, int minIntervalForSearch, bool ifPostNotNeedModeration,
            SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            #region sql
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("insert into t_Forum_GroupPermission ");
            strSQL.Append("(");
            strSQL.Append(FIELDS);
            strSQL.Append(") values(");

            string[] fieldList = FIELDS.Split(',');

            for (int i = 0; i < fieldList.Count(); i++)
            {
                if (i == 0)
                    strSQL.Append("@" + fieldList[i]);
                else
                    strSQL.Append(string.Format(",@{0}", fieldList[i]));
            }

            strSQL.Append(");select @@identity;");
            #endregion
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region parameters
            cmd.Parameters.AddWithValue("@groupId", groupId);
            cmd.Parameters.AddWithValue("@ifAllowViewForum", ifAllowViewForum);
            cmd.Parameters.AddWithValue("@ifAllowViewTopic", ifAllowViewTopic);
            cmd.Parameters.AddWithValue("@ifAllowPost", ifAllowPost);
            cmd.Parameters.AddWithValue("@ifAllowCustomizeAvatar", ifAllowCustomizeAvatar);
            cmd.Parameters.AddWithValue("@maxLengthofSignature", maxLengthofSignature);
            //cmd.Parameters.AddWithValue("@IfSignatureAllowHTML", true);
            cmd.Parameters.AddWithValue("@IfSignatureAllowUrl", ifSignatureAllowUrl);
            cmd.Parameters.AddWithValue("@IfSignatureAllowUploadImage", ifSignatureAllowInsertImage);
            cmd.Parameters.AddWithValue("@minIntervalForPost", minIntervalForPost);
            cmd.Parameters.AddWithValue("@maxLengthOfPost", maxLengthOfPost);
            cmd.Parameters.AddWithValue("@ifAllowHTML", true);
            cmd.Parameters.AddWithValue("@ifAllowUrl", ifAllowUrl);
            cmd.Parameters.AddWithValue("@IfAllowUploadAttachment", IfAllowUploadAttachment);
            cmd.Parameters.AddWithValue("@ifAllowUploadImage", ifAllowUploadImage);
            cmd.Parameters.AddWithValue("@maxCountOfAttacmentsForOnePost", maxCountOfAttacmentsForOnePost);
            cmd.Parameters.AddWithValue("@maxSizeOfOneAttachment", maxSizeOfOneAttachment);
            cmd.Parameters.AddWithValue("@MaxSizeOfAllAttachments", MaxSizeOfAllAttachments);
            cmd.Parameters.AddWithValue("@maxCountOfMessageSendOneDay", maxCountOfMessageSendOneDay);
            cmd.Parameters.AddWithValue("@ifAllowSearch", ifAllowSearch);
            cmd.Parameters.AddWithValue("@minIntervalForSearch", minIntervalForSearch);
            cmd.Parameters.AddWithValue("@ifPostNotNeedModeration", ifPostNotNeedModeration);
            #endregion
            return Convert.ToInt32(cmd.ExecuteNonQuery());
        }
        public static void AddPermission(SqlConnectionWithSiteId conn, SqlTransaction transaction, int groupId, int forumId, bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost, int minIntervalForPost, int maxLengthOfPost,
                bool ifPostNotNeedModeration, bool ifAllowUrl, bool ifAllowUploadImage)
        {
            StringBuilder strSQL = new StringBuilder();
            #region SQL
            strSQL.Append(" insert into t_forum_groupPermission ");
            strSQL.Append(" (GroupId,ForumId,IfAllowViewForum,IfAllowViewTopic,IfAllowPost,MinIntervalForPost,MaxLengthOfPost,IfPostNotNeedModeration,IfAllowUrl,IfAllowUploadImage) ");
            strSQL.Append(" values(@groupId,@forumId,@ifAllowViewForum, @ifAllowViewTopic,@ifAllowPost ,@minIntervalForPost,@maxLengthOfPost,@ifPostNotNeedModeration,@ifAllowUrl,@ifAllowUploadImage) ");
            #endregion
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region parameters
            cmd.Parameters.AddWithValue("@ifAllowViewForum", ifAllowViewForum);
            cmd.Parameters.AddWithValue("@ifAllowViewTopic", ifAllowViewTopic);
            cmd.Parameters.AddWithValue("@ifAllowPost", ifAllowPost);
            cmd.Parameters.AddWithValue("@minIntervalForPost", minIntervalForPost);
            cmd.Parameters.AddWithValue("@maxLengthOfPost", maxLengthOfPost);
            cmd.Parameters.AddWithValue("@ifPostNotNeedModeration", ifPostNotNeedModeration);
            //cmd.Parameters.AddWithValue("@ifAllowHTML", true);
            cmd.Parameters.AddWithValue("@ifAllowUrl", ifAllowUrl);
            cmd.Parameters.AddWithValue("@ifAllowUploadImage", ifAllowUploadImage);
            //cmd.Parameters.AddWithValue("@minIntervalForSearch", minIntervalForSearch);
            cmd.Parameters.AddWithValue("@groupId", groupId);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            #endregion
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Update
        public static void UpdatePermission(
            int groupId, bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost,
            bool ifAllowCustomizeAvatar, int maxLengthofSignature,bool ifSignatureAllowUrl,bool ifSignatureAllowInsertImage,
            int minIntervalForPost,int maxLengthOfPost, 
            bool ifAllowUrl, bool ifAllowUploadImage, 
            bool IfAllowUploadAttachment,int maxCountOfAttacmentsForOnePost, int maxSizeOfOneAttachment, int MaxSizeOfAllAttachments,
            int maxCountOfMessageSendOneDay,bool ifAllowSearch, int minIntervalForSearch, bool ifPostNotNeedModeration,
            SqlConnectionWithSiteId conn, SqlTransaction transaction
            )
        {
            #region sql
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("update t_forum_groupPermission set ");

            string[] fieldList = FIELDS.Split(',');

            for (int i = 1; i < fieldList.Count(); i++)
            {
                if(i <fieldList.Count()-1)
                    strSQL.Append(string.Format("{0} = @{0},", fieldList[i])); 
                else
                    strSQL.Append(string.Format("{0} = @{0} ", fieldList[i])); 
            }

            strSQL.Append("where ");
            strSQL.Append("groupId = @groupId");
            #endregion
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region parameters
            cmd.Parameters.AddWithValue("@groupId", groupId);
            cmd.Parameters.AddWithValue("@ifAllowViewForum", ifAllowViewForum);
            cmd.Parameters.AddWithValue("@ifAllowViewTopic", ifAllowViewTopic);
            cmd.Parameters.AddWithValue("@ifAllowPost", ifAllowPost);
            cmd.Parameters.AddWithValue("@ifAllowCustomizeAvatar", ifAllowCustomizeAvatar);
            cmd.Parameters.AddWithValue("@maxLengthofSignature", maxLengthofSignature);
            cmd.Parameters.AddWithValue("@IfSignatureAllowHTML", true);
            cmd.Parameters.AddWithValue("@IfSignatureAllowUrl", ifSignatureAllowUrl);
            cmd.Parameters.AddWithValue("@IfSignatureAllowUploadImage", ifSignatureAllowInsertImage);
            cmd.Parameters.AddWithValue("@minIntervalForPost", minIntervalForPost);
            cmd.Parameters.AddWithValue("@maxLengthOfPost", maxLengthOfPost);
            cmd.Parameters.AddWithValue("@ifAllowHTML", true);
            cmd.Parameters.AddWithValue("@ifAllowUrl", ifAllowUrl);
            cmd.Parameters.AddWithValue("@IfAllowUploadAttachment", IfAllowUploadAttachment);
            cmd.Parameters.AddWithValue("@ifAllowUploadImage", ifAllowUploadImage);
            cmd.Parameters.AddWithValue("@maxCountOfAttacmentsForOnePost", maxCountOfAttacmentsForOnePost);
            cmd.Parameters.AddWithValue("@maxSizeOfOneAttachment", maxSizeOfOneAttachment);
            cmd.Parameters.AddWithValue("@MaxSizeOfAllAttachments", MaxSizeOfAllAttachments);
            cmd.Parameters.AddWithValue("@maxCountOfMessageSendOneDay", maxCountOfMessageSendOneDay);
            cmd.Parameters.AddWithValue("@ifAllowSearch", ifAllowSearch);
            cmd.Parameters.AddWithValue("@minIntervalForSearch", minIntervalForSearch);
            cmd.Parameters.AddWithValue("@ifPostNotNeedModeration", ifPostNotNeedModeration);
            #endregion

            cmd.ExecuteNonQuery();
        }
        public static void UpdatePermission(SqlConnectionWithSiteId conn, SqlTransaction transaction, int groupId, int forumId, bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost, int minIntervalForPost, int maxLengthOfPost,
                bool ifPostNotNeedModeration, bool ifAllowUrl, bool ifAllowUploadImage)
        {
            StringBuilder strSQL = new StringBuilder();
            #region SQL
            strSQL.Append(" update t_forum_groupPermission set ");
            strSQL.Append(" IfAllowViewForum = @ifAllowViewForum, ");
            strSQL.Append(" IfAllowViewTopic=@ifAllowViewTopic, ");
            strSQL.Append(" IfAllowPost=@ifAllowPost, ");
            strSQL.Append(" MinIntervalForPost=@minIntervalForPost, ");
            strSQL.Append(" MaxLengthOfPost=@maxLengthOfPost, ");
            strSQL.Append(" IfPostNotNeedModeration=@ifPostNotNeedModeration, ");
            //strSQL.Append(" IfAllowHtml=@ifAllowHTML, ");
            strSQL.Append(" IfAllowUrl=@ifAllowUrl, ");
            strSQL.Append(" IfAllowUploadImage=@ifAllowUploadImage ");
            //strSQL.Append(" IfAllowSearch=@ifAllowSearch, ");
            //strSQL.Append(" MinInterVALfOrSearch=@minIntervalForSearch");
            strSQL.Append(" where GroupId=@groupId");
            strSQL.Append(" and ForumId=@forumId");
            #endregion 
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region parameters
            cmd.Parameters.AddWithValue("@ifAllowViewForum", ifAllowViewForum);
            cmd.Parameters.AddWithValue("@ifAllowViewTopic", ifAllowViewTopic);
            cmd.Parameters.AddWithValue("@ifAllowPost", ifAllowPost);
            cmd.Parameters.AddWithValue("@minIntervalForPost", minIntervalForPost);
            cmd.Parameters.AddWithValue("@maxLengthOfPost", maxLengthOfPost);
            cmd.Parameters.AddWithValue("@ifPostNotNeedModeration", ifPostNotNeedModeration);
            //cmd.Parameters.AddWithValue("@ifAllowHTML", true);
            cmd.Parameters.AddWithValue("@ifAllowUrl", ifAllowUrl);
            cmd.Parameters.AddWithValue("@ifAllowUploadImage", ifAllowUploadImage);
            //cmd.Parameters.AddWithValue("@ifAllowSearch", ifAllowSearch);
            //cmd.Parameters.AddWithValue("@minIntervalForSearch", minIntervalForSearch);
            cmd.Parameters.AddWithValue("@groupId", groupId);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            #endregion
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Read
        public static DataTable GetUserGroupPermissionByGroupId
            (int groupId, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select * from t_forum_grouppermission ");
            strSQL.Append("where groupid = @groupId and forumid = @forumId");

            DataTable table = new DataTable();

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@groupId", groupId);
            cmd.Parameters.AddWithValue("@forumId", 0);
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }
        public static DataTable GetUserGroupPermissionByGroupAndForumId(int groupId,int forumId, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" select GroupId,ForumId,IfAllowViewForum,IfAllowViewTopic,IfAllowPost,MinIntervalForPost,MaxLengthOfPost,IfPostNotNeedModeration,IfAllowUrl,IfAllowUploadImage ");
            strSQL.Append(" from t_Forum_GroupPermission where groupId=@groupId and forumId=@forumId");
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@groupId", groupId);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        #endregion

        #region Delete
        public static void DeletePermission(int groupId,int forumId,SqlConnectionWithSiteId conn,SqlTransaction transaction)
        {
            StringBuilder strSQL=new StringBuilder();
            strSQL.Append(" delete t_forum_groupPermission ");
            strSQL.Append(" where groupId=@groupId and forumId=@forumId ");
            SqlCommand cmd=new SqlCommand(strSQL.ToString(),conn.SqlConn,transaction);
            cmd.Parameters.AddWithValue("@groupId",groupId);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.ExecuteNonQuery();
        }
        public static void DeletePermission(int groupId, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" delete t_forum_groupPermission ");
            strSQL.Append(" where groupId=@groupId ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@groupId", groupId);
            cmd.ExecuteNonQuery();
        }
        //used in deleting forum.
        public static void DeleteAllPermissionFromForum(int forumId, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" delete t_forum_groupPermission ");
            strSQL.Append(" where ForumId=@forumId ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region site Settings User Permission
        public static DataTable GetUserPermission(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from t_Forum_GroupPermission ");
            strSQL.Append("where GroupId = ");
            strSQL.Append("(select top 1 Id from t_Forum_Group ");
            strSQL.Append("where SiteId=@siteId and [IfAllForumUsersGroup]=1)");

            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId",conn.SiteId);

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static void UpdateUserPermission(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost,
            bool ifAllowCustomizeAvatar, int maxLengthofSignature, bool ifSignatureAllowUrl, bool ifSignatureAllowInsertImage, int minIntervalForPost,
            int maxLengthOfPost, bool ifAllowUrl, bool ifAllowUploadImage, bool IfAllowUploadAttachment,
            int maxCountOfAttacmentsForOnePost, int maxSizeOfOneAttachment, int MaxSizeOfAllAttachments, int maxCountOfMessageSendOneDay,
            bool ifAllowSearch, int minIntervalForSearch, bool ifPostNotNeedModeration)
        {
            #region sql
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("update t_forum_groupPermission set ");

            string[] fieldList = FIELDS.Split(',');

            for (int i = 1; i < fieldList.Count(); i++)
            {
                if (i < fieldList.Count()-1)
                    strSQL.Append(string.Format("{0} = @{0},", fieldList[i]));
                else
                    strSQL.Append(string.Format("{0} = @{0} ", fieldList[i]));
            }

            strSQL.Append(" where groupId = ");
            strSQL.Append("(select top 1 Id from t_Forum_Group ");
            strSQL.Append(" where SiteId=@siteId and [IfAllForumUsersGroup]=1) ");
            strSQL.Append(" and ForumId=0 ");
            #endregion
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region parameters
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@IfAllowViewForum", ifAllowViewForum);
            cmd.Parameters.AddWithValue("@IfAllowViewTopic", ifAllowViewTopic);
            cmd.Parameters.AddWithValue("@IfAllowPost", ifAllowPost);
            cmd.Parameters.AddWithValue("@IfAllowCustomizeAvatar", ifAllowCustomizeAvatar);
            cmd.Parameters.AddWithValue("@MaxLengthofSignature", maxLengthofSignature);
            cmd.Parameters.AddWithValue("@IfSignatureAllowHTML", true);
            cmd.Parameters.AddWithValue("@IfSignatureAllowUrl", ifSignatureAllowUrl);
            cmd.Parameters.AddWithValue("@IfSignatureAllowUploadImage", ifSignatureAllowInsertImage);
            cmd.Parameters.AddWithValue("@MinIntervalForPost", minIntervalForPost);
            cmd.Parameters.AddWithValue("@MaxLengthOfPost", maxLengthOfPost);
            cmd.Parameters.AddWithValue("@IfAllowHTML", true);
            cmd.Parameters.AddWithValue("@IfAllowUrl", ifAllowUrl);
            cmd.Parameters.AddWithValue("@IfAllowUploadAttachment", IfAllowUploadAttachment);
            cmd.Parameters.AddWithValue("@IfAllowUploadImage", ifAllowUploadImage);
            cmd.Parameters.AddWithValue("@MaxCountOfAttacmentsForOnePost", maxCountOfAttacmentsForOnePost);
            cmd.Parameters.AddWithValue("@MaxSizeOfOneAttachment", maxSizeOfOneAttachment);
            cmd.Parameters.AddWithValue("@MaxSizeOfAllAttachments", MaxSizeOfAllAttachments);
            cmd.Parameters.AddWithValue("@MaxCountOfMessageSendOneDay", maxCountOfMessageSendOneDay);
            cmd.Parameters.AddWithValue("@IfAllowSearch", ifAllowSearch);
            cmd.Parameters.AddWithValue("@MinIntervalForSearch", minIntervalForSearch);
            cmd.Parameters.AddWithValue("@IfPostNotNeedModeration", ifPostNotNeedModeration);
            #endregion
            cmd.ExecuteNonQuery();
        }
        #endregion

    }
}
