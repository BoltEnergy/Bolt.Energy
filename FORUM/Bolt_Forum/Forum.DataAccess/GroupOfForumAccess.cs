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
    public class GroupOfForumAccess
    {
        public static void AddGroupToForum(int groupId, int forumId, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("insert into t_forum_groupOfForum(GroupId,ForumId) ");
            strSQL.Append("values (@groupId,@forumId)");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@groupId", groupId);
            cmd.Parameters.AddWithValue("@forumId", forumId);

            cmd.ExecuteNonQuery();
        }

        public static void DeleteGroupForumRalation(int groupId, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("DELETE T_FORUM_GROUPofforum WHERE groupID = @groupid ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@groupid", groupId);

            cmd.ExecuteNonQuery();
        }

        public static void DeleteAllGroupFromForumRelation(int forumId, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("DELETE t_Forum_GroupOfForum where ForumId=@forumId ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.ExecuteNonQuery();
        }

        public static void DeleteGroupFromForumRalation(int groupId, int forumId, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("DELETE T_FORUM_GROUPofforum WHERE groupID = @groupid ");
            strSQL.Append("and forumid = @forumid ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@groupid", groupId);
            cmd.Parameters.AddWithValue("@forumid", forumId);

            cmd.ExecuteNonQuery();
        }
    }
}
