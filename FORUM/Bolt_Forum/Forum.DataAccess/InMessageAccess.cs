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
    public class InMessageAccess
    {
        public static int AddInMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction, string subject, string message, DateTime createDate, int fromUserOrOperatorId, int toUserOrOperatorId)
        {

            StringBuilder strSQL = new StringBuilder("");
            #region build sql
            strSQL.Append("insert t_Forum_InMessage" + conn.SiteId + "(Subject,Message,CreateDate,FromUserId,ToUserId)");
            strSQL.Append("values(@subject,@message,@createDate,@fromUserOrOperatorId,@toUserOrOperatorId);");
            strSQL.Append("select @@identity");
            #endregion build sql

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region add parameters
            cmd.Parameters.AddWithValue("@subject", subject);
            cmd.Parameters.AddWithValue("@message", message);
            cmd.Parameters.AddWithValue("@createDate", createDate);
            cmd.Parameters.AddWithValue("@fromUserOrOperatorId", fromUserOrOperatorId);
            cmd.Parameters.AddWithValue("@toUserOrOperatorId", toUserOrOperatorId);
            #endregion add parameters

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void AddInMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            string subject, string message, DateTime createDate,
            int fromUserOrOperatorId, int[] toUserOrOperatorIds,int[] toGroups,int[] toReputationGroups,
            bool ifAdminGroup, bool ifModeratorGroup,bool ifAllRegisterUserGroup)
        {
            /*create temp table*/
            StringBuilder strSQL1 = new StringBuilder();
            strSQL1.Append("create table [#inmessage] ([Subject] nvarchar(128) null,");
            strSQL1.Append("[Message] nvarchar(3072) null,");
            strSQL1.Append("[CreateDate] datetime null,");
            strSQL1.Append("[FromUserId] int null,");
            strSQL1.Append("[ToUserId] int null,");
            strSQL1.Append("[IfView] bit null);");
            SqlCommand cmd1 = new SqlCommand(strSQL1.ToString(), conn.SqlConn, transaction);
            cmd1.ExecuteNonQuery();

           /*Insert UserIds*/
            StringBuilder strSQL2 = new StringBuilder();
            strSQL2.Append("insert [#inmessage]([ToUserId]) ( ");
            strSQL2.Append(string.Format("select Id from t_User{0} where ",conn.SiteId));
            #region Conditions
            StringBuilder condition = new StringBuilder();
            if (ifAdminGroup)
                condition.Append(" or IfForumAdmin = 'true' or IfAdmin = 'true'");
            if (ifModeratorGroup)
                condition.Append(" or Id in (select UserOrOperatorId from t_Forum_Moderator)");
            if (ifAllRegisterUserGroup)
                condition.Append(" or UserType = 2");
            if (toUserOrOperatorIds != null && toUserOrOperatorIds.Length !=0)
            {
                foreach(int userId in toUserOrOperatorIds)
                    condition.Append(string.Format(" or Id={0}",userId));
            }
            if (toGroups != null && toGroups.Length != 0)
            {
                condition.Append(string.Format(" or Id in (select UserId from t_Forum_MemberOfUserGroup{0} where ",conn.SiteId));
                StringBuilder GroupCondition = new StringBuilder();
                foreach(int groupId in toGroups)
                    GroupCondition.Append(string.Format(" or GroupId={0}", groupId));
                condition.Append(GroupCondition.ToString().Remove(0,4));//remove ' or '
                condition.Append(")");
            }
            if (toReputationGroups != null && toReputationGroups.Length != 0)
            {
                
                condition.Append(string.Format(" or Id in (select a.Id from t_User{0} as a inner join", conn.SiteId));
                condition.Append("(select * from t_Forum_Group where ");
                StringBuilder ReputationGroupCondition = new StringBuilder();
                foreach (int reputationGroupId in toReputationGroups)
                {
                    ReputationGroupCondition.Append(string.Format(" or Id={0}",reputationGroupId));
                }
                condition.Append(ReputationGroupCondition.ToString().Remove(0, 4));
                condition.Append(") as b on a.ForumReputation<=b.LimitedExpire and a.ForumReputation>=b.LimitedBegin)");
                
            }
            
            strSQL2.Append(condition.ToString().Remove(0, 4));//remove ' or '
            #endregion
            strSQL2.Append(");");
            SqlCommand cmd2 = new SqlCommand(strSQL2.ToString(), conn.SqlConn, transaction);
            cmd2.ExecuteNonQuery();
            /*Update temp table Infor*/
            StringBuilder strSQL3 = new StringBuilder();
            strSQL3.Append("update [#inmessage] set Subject=@subject, Message=@message,CreateDate=@createDate,FromUserId=@fromUserId,IfView='false';");
            SqlCommand cmd3 = new SqlCommand(strSQL3.ToString(), conn.SqlConn, transaction);
            cmd3.Parameters.AddWithValue("@subject", subject);
            cmd3.Parameters.AddWithValue("@message", message);
            cmd3.Parameters.AddWithValue("@createDate", createDate);
            cmd3.Parameters.AddWithValue("@fromUserId", fromUserOrOperatorId);
            cmd3.ExecuteNonQuery();
            /*Copy temp table Infor to real table*/
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("insert [t_Forum_InMessage{0}] (Subject,Message,CreateDate,FromUserId,ToUserId,IfView) select * from [#inmessage];", conn.SiteId));
            strSQL.Append("drop table [#inmessage];");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.ExecuteNonQuery();
        }

        public static DataTable GetInMessageById(SqlConnectionWithSiteId conn, SqlTransaction transaction, int inMessageId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select a.*, b.Name FromUserName, c.Name ToUserName  from");
            strSQL.Append(" t_Forum_InMessage" + conn.SiteId + " a, t_User" + conn.SiteId + " b, t_User" + conn.SiteId + " c");
            strSQL.Append(" where a.Id=@InMessageId and a.FromUserId = b.Id and a.ToUserId = c.Id ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@InMessageId", inMessageId);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        public static DataTable GetInMessagesByPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, int pageIndex, int pageSize)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select b.*, c.Name FromUserName, d.Name ToUserName  from");
            strSQL.Append(" (select *from (select ROW_NUMBER() over(order by CreateDate desc) row, * ");
            strSQL.Append(" from t_Forum_InMessage" + conn.SiteId + " where ToUserId=@UserOrOperatorId ) a");
            strSQL.Append(string.Format(" where row between {0} and {1}) b,", startRowNum, endRowNum));
            strSQL.Append(" t_User" + conn.SiteId + " c, t_User" + conn.SiteId + " d where  b.FromUserId = c.Id and b.ToUserId = d.Id ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@UserOrOperatorId", userOrOperatorId);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        public static int GetCountOfInmessges(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" select COUNT(*) allrows from t_Forum_InMessage" + conn.SiteId + " where ToUserId=@UserOrOperatorId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@UserOrOperatorId", userOrOperatorId);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        public static int GetCountOfUnReadInMessages(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int userOrOperatorId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" select COUNT(*) allrows from t_Forum_InMessage" + conn.SiteId + " where ToUserId=@UserOrOperatorId and ifView='false'");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@UserOrOperatorId", userOrOperatorId);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        public static void DeleteInMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int inMessageId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("delete t_Forum_InMessage" + conn.SiteId + " where Id = @InMessageId;");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@InMessageId", inMessageId);
            cmd.ExecuteNonQuery();
        }
        public static void UpdateIfView(SqlConnectionWithSiteId conn, SqlTransaction transaction, int inMessageId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("update t_Forum_InMessage" + conn.SiteId + " set IfView=@IfView where Id = @InMessageId;");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@IfView", true);
            cmd.Parameters.AddWithValue("@InMessageId", inMessageId);
            cmd.ExecuteNonQuery();

        }
    }
}
