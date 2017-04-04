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
    public class ReputationGroupAccess
    {
        public static void AddGroupsOfOutMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int outMessageId, List<int> reputationGroupIds)
        {
            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("create table  #tmp_GroupOfOutMessage ([OutMessageId] int not null, [GroupId] int not null);");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.ExecuteNonQuery();
            foreach (int groupId in reputationGroupIds)
            {
                strSQL = new StringBuilder("");
                strSQL.Append("insert  #tmp_GroupOfOutMessage values( @OutMessageId, @GroupId );");
                cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
                cmd.Parameters.AddWithValue("@OutMessageId", outMessageId);
                cmd.Parameters.AddWithValue("@GroupId", groupId);
                cmd.ExecuteNonQuery();
            }
            strSQL = new StringBuilder("");
            strSQL.Append("insert into t_Forum_GroupOfOutMessage" + conn.SiteId + " select * from  #tmp_GroupOfOutMessage; ");
            strSQL.Append("drop table #tmp_GroupOfOutMessage; ");
            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.ExecuteNonQuery();
 
 
        }

        public static void AddGroupOfOutMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int outMessageId, int reputationGroupId)
        {
            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append(string.Format("Insert into [t_Forum_GroupOfOutMessage{0}](OutMessageId,GroupId) values(@outMessageId,@reputationGroupId);", conn.SiteId));
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@outMessageId", outMessageId);
            cmd.Parameters.AddWithValue("@reputationGroupId", reputationGroupId);
            cmd.ExecuteNonQuery();
        }


    }
}
