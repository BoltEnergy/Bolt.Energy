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
    public class MemberOfUserGroupAccess
    {
        public static DataTable GetNotDeletedMemberByUserOrOperatorIdAndUserGroupId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, int userGroupId)
        {


            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("select * from t_Forum_MemberOfUserGroup" + conn.SiteId + " a ");
            strSQL.Append(" inner join t_User" + conn.SiteId + " b on a.UserId = b.Id");
            strSQL.Append(" where a.GroupId = @userGroupId and a.UserId=@userOrOperatorId and b.IfDeleted=0");


            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userGroupId", userGroupId);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static void AddMember(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, int userGroupId)
        {

            StringBuilder strSQL = new StringBuilder("insert t_Forum_MemberOfUserGroup" + conn.SiteId + "(UserId, GroupId)values(@userOrOperatorId,@userGroupId)");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userGroupId", userGroupId);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);

            cmd.ExecuteNonQuery();
        }

        public static void DeleteMember(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, int userGroupId)
        {

            StringBuilder strSQL = new StringBuilder("delete t_Forum_MemberOfUserGroup" + conn.SiteId + " where UserId=@userOrOperatorId and GroupId=@userGroupId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userGroupId", userGroupId);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);

            cmd.ExecuteNonQuery();
        }

        public static int GetCountGroupMember(SqlConnectionWithSiteId conn, SqlTransaction transaction, int groupId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select count(b.Id) from t_Forum_MemberOfUserGroup" + conn.SiteId + " a");
            strSQL.Append(" left join t_User" + conn.SiteId + " b on a.UserId=b.Id ");
            strSQL.Append(" where b.IfDeleted=0 and b.IfVerified=1 and b.UserType<>@typeOfContact  and a.GroupId=@groupId");

            //b.IfVerified=(case UserType when @typeOfUser then 1 else 0 end)

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@typeOfUser", Convert.ToInt16(EnumUserType.User));
            cmd.Parameters.AddWithValue("@typeOfContact", Convert.ToInt16(EnumUserType.Contact));
            cmd.Parameters.AddWithValue("@groupId", groupId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable GetMembersByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userGroupId, string emailOrDisplayNameKeyword, int pageIndex, int pageSize, string orderField)
        {
            emailOrDisplayNameKeyword = CommonFunctions.SqlReplace(emailOrDisplayNameKeyword);
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;


            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append(" select * from ( ");
            strSQL.Append(" select ROW_NUMBER() over(order by " + orderField + ") as [row], b.*,a.GroupId from t_Forum_MemberOfUserGroup" + conn.SiteId + " a");
            strSQL.Append(" inner join t_User" + conn.SiteId + " b on a.UserId=b.Id");
            strSQL.Append(" where a.GroupId=@groupId and b.IfDeleted=0 and b.UserType<>@typeOfContact");
            strSQL.Append(" and (b.Name like '%'+@emailOrDisplayNameKeyword+'%' escape '/' or b.Email like '%'+@emailOrDisplayNameKeyword+'%' escape '/' ) ");
            strSQL.Append(" ) t where row between @startRowNum and @endRowNum ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@groupId", userGroupId);
            cmd.Parameters.AddWithValue("@typeOfContact", Convert.ToInt16(EnumUserType.Contact));
            cmd.Parameters.AddWithValue("@emailOrDisplayNameKeyword", emailOrDisplayNameKeyword);
            cmd.Parameters.AddWithValue("@startRowNum", startRowNum);
            cmd.Parameters.AddWithValue("@endRowNum", endRowNum);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfMembersByQuery(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userGroupId, string emailOrDisplayNameKeyword)
        {

            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append(" select count(b.Id) from t_Forum_MemberOfUserGroup" + conn.SiteId + " a");
            strSQL.Append(" inner join t_User" + conn.SiteId + " b on a.UserId=b.Id");
            strSQL.Append(" where a.GroupId=@groupId and b.IfDeleted=0 and b.UserType<>@typeOfContact");
            strSQL.Append(" and (b.Name like '%'+@emailOrDisplayNameKeyword+'%' escape '/' or b.Email like '%'+@emailOrDisplayNameKeyword+'%' escape '/' ) ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@groupId", userGroupId);
            cmd.Parameters.AddWithValue("@typeOfContact", Convert.ToInt16(EnumUserType.Contact));
            cmd.Parameters.AddWithValue("@emailOrDisplayNameKeyword", emailOrDisplayNameKeyword);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void DeleteMembersByUserGroupId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userGroupId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("delete t_Forum_MemberOfUserGroup" + conn.SiteId + " where GroupId=@userGroupId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userGroupId", userGroupId);

            cmd.ExecuteNonQuery();
        }
    }
}
