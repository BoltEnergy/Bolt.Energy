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
    public class GroupAccess
    {
        public static int GetCountOfUserGroupsByName(string name, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select COUNT(Id) from t_Forum_Group where SiteId=@siteId and Type=@userGroupType and Name=@name;");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn , transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@userGroupType", EnumUserGroupType.UserGroup);

            return Convert.ToInt32(cmd.ExecuteScalar());

        }

        public static int GetCountOfReputationGroupsByName(SqlConnectionWithSiteId conn, SqlTransaction transaction, string name)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select COUNT(Id) from t_Forum_Group where SiteId=@siteId and Type=@reputationGroupType and Name=@name;");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@reputationGroupType", EnumUserGroupType.UserReputationGroup);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int AddUserGroup(string name, string description, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("insert into t_forum_group(");

            strSQL.Append("Name,[Description],SiteId,[Type]) ");
            strSQL.Append("values (");
            strSQL.Append("@name,@description,@siteId,@type);");
            strSQL.Append("select @@IDENTITY");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@type", Convert.ToInt16(EnumUserGroupType.UserGroup));

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int AddReputationGroup(string name, string description, bool ifAllForumUsersGroup, int limitedBegin, int limitedExpire, int icoRepeat, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("insert into t_forum_group(");
            strSQL.Append("type,name,description,ifAllForumUsersGroup,limitedBegin,limitedExpire,icoRepeat,siteId) ");
            strSQL.Append("values (");
            strSQL.Append("@type,@name,@description,@ifAllForumUsersGroup,@limitedBegin,@limitedExpire,@icoRepeat,@siteId);");
            strSQL.Append("select @@identity;");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@type", Convert.ToInt16(EnumUserGroupType.UserReputationGroup));
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@ifAllForumUsersGroup", ifAllForumUsersGroup);
            cmd.Parameters.AddWithValue("@limitedBegin", limitedBegin);
            cmd.Parameters.AddWithValue("@limitedExpire", limitedExpire);
            cmd.Parameters.AddWithValue("@icoRepeat", icoRepeat);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void UpdateUserGroup(int id, string name, string description, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("update t_forum_group set ");
            strSQL.Append("name = @name, ");
            strSQL.Append("description = @description ");

            strSQL.Append("where id = @id");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateReputationGroup(int groupId, string name, string description, int limitedBegin, int limitedExpire, int icoRepeat, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("update t_forum_group set ");
            strSQL.Append("name = @name, ");
            strSQL.Append("description = @description,");
            strSQL.Append("limitedBegin = @limitedBegin,");
            strSQL.Append("limitedExpire = @limitedExpire,");
            strSQL.Append("icoRepeat = @icoRepeat ");
            strSQL.Append("where id = @id");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@id", groupId);
            cmd.Parameters.AddWithValue("@limitedBegin", limitedBegin);
            cmd.Parameters.AddWithValue("@limitedExpire", limitedExpire);
            cmd.Parameters.AddWithValue("@icoRepeat", icoRepeat);

            cmd.ExecuteNonQuery();
        }

        public static void DeleteGroup(int id, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("DELETE T_FORUM_GROUP WHERE ID = @id");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public static DataTable GetAllUserGroups(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {

            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select * from t_forum_group where siteId = @siteId and Type=@type order by IfAllForumUsersGroup desc, Name asc");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@type", Convert.ToInt16(EnumUserGroupType.UserGroup));

            DataTable table = new DataTable();

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetAllReputationGroups(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {

            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select * from t_forum_group where siteId = @siteId and Type=@type order by LimitedBegin desc");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@type", Convert.ToInt16(EnumUserGroupType.UserReputationGroup));

            DataTable table = new DataTable();

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table; 
        }

        public static DataTable GetGroupsByForumIdAndType(
            EnumUserGroupType groupType, int forumId, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select * from t_forum_Group g ");
            strSQL.Append("join t_forum_groupofForum gf ");
            strSQL.Append("on g.id = gf.groupid ");
            strSQL.Append("where gf.forumid = @forum ");
            strSQL.Append("and g.type = @type");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@forum", forumId);
            cmd.Parameters.AddWithValue("@type", groupType);

            DataTable table = new DataTable();

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetGroupsNotInForum(int forumId, EnumUserGroupType groupType, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select * from t_forum_group g ");
            strSQL.Append("where ");
            strSQL.Append("g.type = @type ");
            strSQL.Append("and ");
            strSQL.Append("(select count(*) from t_forum_groupofforum f ");
            strSQL.Append("where f.groupid = g.id and f.forumid = @forumid) < 1");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@type", groupType);
            cmd.Parameters.AddWithValue("@forumid", forumId);

            DataTable table = new DataTable();

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetGroupById(int groupId, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select * from t_forum_group ");
            strSQL.Append("where id = @id ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@id", groupId);

            DataTable table = new DataTable();

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static int GetUserReputationGroupByUserOrOperator(int userId, 
            SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select b.Id from t_User{0} a, t_Forum_Group b ",conn.SiteId));
            strSQL.Append("where a.Id=@id and b.Type = 1 and b.LimitedBegin <= a.ForumReputation ");
            strSQL.Append("and b.LimitedExpire >= a.ForumReputation");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@id", userId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable GetForumAllUserGroup(SqlConnectionWithSiteId conn)
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

        public static DataTable GetUserGroupsExceptAllForumUser(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {

            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("select * from t_Forum_Group where IfAllForumUsersGroup = 0 and Type = @type and SiteId=@siteId order by Name asc");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@type", Convert.ToInt16(EnumUserGroupType.UserGroup));
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);

            DataTable table = new DataTable();

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetUserGroupsWhichContainExistUserOrOperator(
            SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select distinct a.* from t_Forum_Group a, t_Forum_MemberOfUserGroup{0} b",conn.SiteId));
            strSQL.Append(string.Format(" where a.Type=@type and a.SiteId={0}",conn.SiteId));
            strSQL.Append(" and ((a.Id=b.GroupId and b.UserId=@userOrOperatorId) or (a.[IfAllForumUsersGroup]='true'))");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@type", Convert.ToInt16(EnumUserGroupType.UserGroup));
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetUserGroupsOfForumByForumId(
            SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select b.* from [t_Forum_GroupOfForum] a, t_Forum_Group b");
            strSQL.Append(" where b.SiteId=@siteId and a.GroupId=b.Id and b.Type=@type and a.ForumId=@forumId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@type", Convert.ToInt16(EnumUserGroupType.UserGroup));
            cmd.Parameters.AddWithValue("@forumId", forumId);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetUserGroupsOfOutMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int outMessageId)
        {
            Int16 userGroup = Convert.ToInt16(EnumUserGroupType.UserGroup);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select b.* from");
            strSQL.Append(" t_Forum_GroupOfOutMessage" + conn.SiteId + " a, t_Forum_Group b where a.OutMessageId = @OutMessageId and a.GroupId = b.Id and b.Type=@UserGroup; ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@OutMessageId", outMessageId);
            cmd.Parameters.AddWithValue("@UserGroup", userGroup);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetUserReputationGroupsOfOutMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int outMessageId)
        {
            Int16 reputationGroup = Convert.ToInt16(EnumUserGroupType.UserReputationGroup);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select b.* from");
            strSQL.Append(" t_Forum_GroupOfOutMessage" + conn.SiteId + " a, t_Forum_Group b where a.OutMessageId = @OutMessageId and a.GroupId = b.Id and b.Type=@UserReputationGroup; ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@OutMessageId", outMessageId);
            cmd.Parameters.AddWithValue("@UserReputationGroup", reputationGroup);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfReputationGroupsByLimitedBeginAndLimitedExpire(SqlConnectionWithSiteId conn, SqlTransaction transaction, int limitedBegin,int limitedExpire)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select COUNT(Id) from t_Forum_Group where SiteId=@siteId and Type=@type ");
            strSQL.Append(" and ( (LimitedBegin <= @limitedBegin and LimitedExpire >= @limitedBegin) ");
            strSQL.Append(" or (LimitedBegin <= @limitedExpire and LimitedExpire >= @limitedExpire) ");
            strSQL.Append(" or (LimitedBegin >= @limitedBegin and LimitedExpire <= @limitedExpire) )");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@type", Convert.ToInt16(EnumUserGroupType.UserReputationGroup));
            cmd.Parameters.AddWithValue("@limitedBegin", limitedBegin);
            cmd.Parameters.AddWithValue("@limitedExpire", limitedExpire);
            //cmd.Parameters.AddWithValue("@groupId", GroupId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int GetCountOfReputationGroupsByLimitedBeginAndLimitedExpireOfEdit(SqlConnectionWithSiteId conn, SqlTransaction transaction, int limitedBegin, int limitedExpire,int groupId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select COUNT(Id) from t_Forum_Group where SiteId=@siteId and Type=@type ");
            strSQL.Append(" and ( (LimitedBegin <= @limitedBegin and LimitedExpire >= @limitedBegin) ");
            strSQL.Append(" or (LimitedBegin <= @limitedExpire and LimitedExpire >= @limitedExpire) ");
            strSQL.Append(" or (LimitedBegin >= @limitedBegin and LimitedExpire <= @limitedExpire) ) and Id!=@groupId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@type", Convert.ToInt16(EnumUserGroupType.UserReputationGroup));
            cmd.Parameters.AddWithValue("@limitedBegin", limitedBegin);
            cmd.Parameters.AddWithValue("@limitedExpire", limitedExpire);
            cmd.Parameters.AddWithValue("@groupId", groupId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }


        public static bool CheckGroupOfForumExist(SqlConnectionWithSiteId conn, SqlTransaction transaction, int groupId, int forumId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" select COUNT(*) from t_Forum_GroupOfForum where groupId=@groupId and forumId=@forumId ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@groupId", groupId);
            cmd.Parameters.AddWithValue("@forumId", forumId);
            int Num=Convert.ToInt32(cmd.ExecuteScalar());
            return Num == 0 ? false : true;
        }
        public static bool IfAllRegisterUserGroup(SqlConnectionWithSiteId conn, SqlTransaction transaction, int groupId)
        {
            
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" select COUNT(*) from t_Forum_Group where Id=@id and IfAllForumUsersGroup=1");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@id", groupId);
            int Num = Convert.ToInt32(cmd.ExecuteScalar());
            return Num == 0 ? false : true;
        }
    }
}
