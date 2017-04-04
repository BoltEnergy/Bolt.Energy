#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2009                                                                               //
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
    public class UserGroupAccess
    {
        public static int AddGroup(string name, string description, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("insert into t_forum_group(");

            strSQL.Append("name,description,siteId) ");
            strSQL.Append("values (");
            strSQL.Append("@name,@description,@siteId);");
            strSQL.Append("select @@indentity;");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
           
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int AddGroup(EnumUserGroupType type, string name, string description, bool ifAllForumUsersGroup, int limitedBegin, int limitedExpire, int icoRepeat, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("insert into t_forum_group(");

            strSQL.Append("type,name,description,ifAllForumUsersGroup,limitedBegin,limitedExpire,icoRepeat,siteId) ");
            strSQL.Append("values (");
            strSQL.Append("@type,@name,@description,@ifAllForumUsersGroup,@limitedBegin,@limitedExpire,@icoRepeat,@siteId);");
            strSQL.Append("select @@indentity;");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@ifAllForumUsersGroup", ifAllForumUsersGroup);
            cmd.Parameters.AddWithValue("@limitedBegin", limitedBegin);
            cmd.Parameters.AddWithValue("@limitedExpire", limitedExpire);
            cmd.Parameters.AddWithValue("@icoRepeat", icoRepeat);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void UpdateGroup(int id,string name, string description, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("update t_forum_gorup set ");
            strSQL.Append("name = @name, ");
            strSQL.Append("description = @description ");

            strSQL.Append("where id = @id");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
             
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public static void Delete(int id, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("DELETE T_FORUM_GROUP WHERE ID = @ID");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.ExecuteNonQuery();
        }

        public static DataTable GetAllUserGroups(SqlConnectionWithSiteId conn)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select * from t_forum_group where siteId = @siteId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn);

            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);

            DataTable table = new DataTable();

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetForumAllUserGroup( SqlConnectionWithSiteId conn)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select * from t_forum_group where siteId = @siteId and IfAllForumUsersGroup = @IfAllForumUsersGroup");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn);

            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@IfAllForumUsersGroup", true);

            DataTable table = new DataTable();

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static int GetCountGroupMember(int groupId, SqlConnectionWithSiteId conn)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select count(*) from ");
            strSQL.Append(string.Format("t_forum_MemberOfUserGroup{0} ", conn.SiteId));
            strSQL.Append("where groupid = @groupid");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn);

            cmd.Parameters.AddWithValue("@groupid", groupId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
