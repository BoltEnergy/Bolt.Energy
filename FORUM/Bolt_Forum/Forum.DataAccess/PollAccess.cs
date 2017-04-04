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
using Com.Comm100.Framework.Database;
using System.Data.SqlClient;

namespace Com.Comm100.Forum.DataAccess
{
    public class PollAccess
    {
        public static void AddPollVoteHistory(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int id,int userId)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("insert into [t_Forum_PollVoteHistory{0}](PollId,VoteUserOrOperatorId) values(@pollId,@userId)", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@pollId", id);
            cmd.Parameters.AddWithValue("@userId", userId);

            cmd.ExecuteNonQuery();
        }

        public static int GetCountOfPollVoteHistoryByUser(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int id, int userId)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select count(*) from [t_Forum_PollVoteHistory{0}] where pollId=@pollId and [VoteUserOrOperatorId]=@userId", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@pollId", id);
            cmd.Parameters.AddWithValue("@userId", userId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int GetCountOfPollvoteHistory(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int id)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select count(*) from [t_Forum_PollVoteHistory{0}] where pollId=@pollId", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@pollId", id);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable GetPollOptionById(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int id)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select * from [t_Forum_PollOption{0}] where Id=@id",conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@id", id);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static void AddPollOption(
             SqlConnectionWithSiteId conn, SqlTransaction transaction,
             int pollId, string optionText, int orderNum)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("insert into [t_Forum_PollOption{0}](PollId,OptionText,OrderNum,Votes)",conn.SiteId));
            strSQL.Append(" values(@pollId,@optionText,@orderNum,'0')");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@pollId", pollId);
            cmd.Parameters.AddWithValue("@optionText", optionText);
            cmd.Parameters.AddWithValue("@orderNum", orderNum);

            cmd.ExecuteNonQuery();
        }

        public static void UpdatePollOption(
             SqlConnectionWithSiteId conn, SqlTransaction transaction,
             int id, string optionText, int orderNum)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("update [t_Forum_PollOption{0}]",conn.SiteId));
            strSQL.Append(" set OrderNum=@orderNum,OptionText=@optionText where Id=@id");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@optionText", optionText);
            cmd.Parameters.AddWithValue("@orderNum", orderNum);

            cmd.ExecuteNonQuery();
        }

        public static void DeletePollOption(
            SqlConnectionWithSiteId conn, SqlTransaction transaction, int id)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("delete [t_Forum_PollOption{0}] where Id=@id",conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public static DataTable GetPollOptionsByTopicId(
            SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select * from [t_Forum_PollOption{0}]",conn.SiteId));
            strSQL.Append(" where PollId=@topicId order by OrderNum");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetPollByTopicId(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int topicId)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select * from [t_Forum_Poll{0}] where TopicId=@topicId",conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static void AddPoll(
             SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int topicId, bool ifMultipleChoice, int maxChoices, bool ifSetDeadline,
            DateTime startDate, DateTime endDate)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("insert into [t_Forum_Poll{0}](TopicId,IfMulitipleChoice,MaxChoices,IfSetDeadline,StartDate,EndDate)",conn.SiteId));
            strSQL.Append(" values(@topicId,@ifMultipleChoice,@maxChoices,@ifSetDeadline,@startDate,@endDate)");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);
            cmd.Parameters.AddWithValue("@ifMultipleChoice", ifMultipleChoice);
            cmd.Parameters.AddWithValue("@maxChoices", maxChoices);
            cmd.Parameters.AddWithValue("@ifSetDeadline", ifSetDeadline);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);

            cmd.ExecuteNonQuery();
        }

        public static void UpdatePoll(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
           int topicId, bool ifMultipleChoice, int maxChoices, bool ifSetDeadline,
           DateTime endDate)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("update [t_Forum_Poll{0}]",conn.SiteId));
            strSQL.Append(" set IfMulitipleChoice=@ifMultipleChoice,[MaxChoices]=@maxChoices,IfSetDeadline=@ifSetDeadline,");
            strSQL.Append(" EndDate=@endDate where TopicId =@topicId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);
            cmd.Parameters.AddWithValue("@ifMultipleChoice", ifMultipleChoice);
            cmd.Parameters.AddWithValue("@maxChoices", maxChoices);
            cmd.Parameters.AddWithValue("@ifSetDeadline", ifSetDeadline);
            //cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);

            cmd.ExecuteNonQuery();
        }

        public static void DeletePoll(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int topicId)
        {
            StringBuilder strSQL = new StringBuilder();

            //strSQL.Append(" and a.Id = @id");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            cmd.ExecuteNonQuery();
        }

        public static void VotePollOption(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,int optionId,int topicId)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("update [t_Forum_PollOption{0}]",conn.SiteId));
            strSQL.Append(" set Votes = Votes+1 where PollId=@topicId and Id=@optionId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@optionId", optionId);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            cmd.ExecuteNonQuery();
        }

        public static int GetCountOfOptionsByTopicId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select count(Id) from t_Forum_PollOption" + conn.SiteId + " where PollId=@topicId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@topicId", topicId);

            
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
