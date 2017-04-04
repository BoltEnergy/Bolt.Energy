using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Com.Comm100.Framework.Database;

namespace Com.Comm100.Forum.Bussiness
{
    public class TopicPromoted
    {
        public static int InsertPromoteData(int UserOrOperatorId, int TopicId)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BoltForumConnectionString"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("Forum_InsertPromoteData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserId", UserOrOperatorId);
                cmd.Parameters.AddWithValue("TopicId", TopicId);
                conn.Open();
                int ResponseValue = cmd.ExecuteNonQuery();
                return ResponseValue;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static int GetPromotedVote(int UserOrOperatorId, int TopicId)
        {
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["BoltForumConnectionString"].ConnectionString))
                {

                    using (var sqlCmd = new SqlCommand("Forum_CountPromotedVote", sqlConnection))
                    {

                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@UserId", UserOrOperatorId);
                        sqlCmd.Parameters.AddWithValue("@TopicId", TopicId);
                        SqlParameter outVote = new SqlParameter("@Promoted", SqlDbType.NVarChar, 100) { Direction = ParameterDirection.Output };
                        sqlCmd.Parameters.Add(outVote);
                        sqlConnection.Open();
                        sqlCmd.ExecuteNonQuery();
                        int Vote;
                        Vote = Convert.ToInt32(outVote.Value);
                        return Vote;
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        public static DataTable GetInfroForTopicDetailPage(int UserOrOperatorId, int TopicId, int forumId, int pageIndex, int pageSize)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BoltForumConnectionString"].ConnectionString);
            try
            {
                //SqlCommand cmd = new SqlCommand("Forum_InsertPromoteData", conn);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("UserId", UserOrOperatorId);
                //cmd.Parameters.AddWithValue("TopicId", TopicId);
                //conn.Open();
                //int ResponseValue = cmd.ExecuteNonQuery();
                //return ResponseValue;

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


                //SqlCommand cmd = new SqlCommand("Forum_GetTopicsByForumIdWithoutWaitingForModeration", conn.SqlConn, transaction);
                SqlCommand cmd = new SqlCommand("Forum_GetTopicsDetail", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TopicId", TopicId);
                cmd.Parameters.AddWithValue("@ForumId", forumId);
                cmd.Parameters.AddWithValue("@StartRowNum", startRowNum);
                cmd.Parameters.AddWithValue("@EndRowNum", endRowNum);
                conn.Open();

                DataTable table = new DataTable();
                table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
                return table;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
    }
}
