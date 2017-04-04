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
    public class UserOfOutMessageAccess
    {
        public static void AddUserOfOutMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int outMessageId, int userOrOperatorId)
        {

            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("insert into t_Forum_UserOfOutMessage" + conn.SiteId + "(OutMessageId,UserId)values(@outMessageId,@userOrOperatorId)");


            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@outMessageId", outMessageId);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);

            cmd.ExecuteNonQuery();
        }
        public static void AddUsersOfOutMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int outMessageId, List<int> userOrOperatorIds)
        {
            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("create table  #tmp_userOfOutMessage ([OutMessageId] int not null, [UserId] int not null);");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.ExecuteNonQuery();          
            foreach (int userId in userOrOperatorIds)
            {            
                strSQL = new StringBuilder("");
                strSQL.Append("insert  #tmp_userOfOutMessage values( @OutMessageId, @UserId );");
                cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
                cmd.Parameters.AddWithValue("@OutMessageId", outMessageId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.ExecuteNonQuery();
            }
            strSQL = new StringBuilder("");
            strSQL.Append("insert t_Forum_UserOfOutMessage" + conn.SiteId + " select * from  #tmp_userOfOutMessage; ");
            strSQL.Append("drop table #tmp_userOfOutMessage; ");
            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.ExecuteNonQuery();
            

        }

        //public static void AddUserOfOutMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction,
        //    int outMessageId, int userOrOperatorId)
        //{
        //    StringBuilder strSQL = new StringBuilder();
        //    strSQL.Append(string.Format("Insert [t_Forum_UserOfOutMessage{0}] values(@outMessageId,@userOrOperatorId);", conn.SiteId));
        //    SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
        //    cmd.Parameters.AddWithValue("@outMessageId", outMessageId);
        //    cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);
        //    cmd.ExecuteNonQuery();
        //}

    }
}
