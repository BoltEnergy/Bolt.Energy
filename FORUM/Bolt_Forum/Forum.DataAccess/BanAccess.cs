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
    public class BanAccess
    {

        #region read
        public static DataTable GetBanById(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id)
        {
            StringBuilder strSQL = new StringBuilder("select a.Id, a.StartDate,a.EndDate,a.Note,a.[OperatedUserOrOperatorId],a.[Type],a.UserOrOperatorId,a.startIP,a.endIP,b.Name Name, a.IfDeleted, a.DeleteDate ");
            strSQL.Append(string.Format(" from t_Forum_Ban a left join t_User{0} b on a.UserOrOperatorId=b.Id where a.Id=@Id and a.SiteId=@SiteId", conn.SiteId));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@SiteId", conn.SiteId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }
        public static DataTable GetBansOfIp(SqlConnectionWithSiteId conn, SqlTransaction transaction, Int64 ip)
        {
            StringBuilder strSQL = new StringBuilder("select Id, StartDate,EndDate,Note,[OperatedUserOrOperatorId],[Type],UserOrOperatorId,startIP,endIP,IfDeleted,DeleteDate ");
            strSQL.Append(" from t_Forum_Ban where (@ip between startIP and endIP)");
            strSQL.Append(" and SiteId=@SiteId and [Type]=@type");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@ip", ip);
            cmd.Parameters.AddWithValue("@SiteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@type", Convert.ToInt16(EnumBanType.IP));

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetBanOfIp(SqlConnectionWithSiteId conn, SqlTransaction transaction, Int64 ip)
        {
            StringBuilder strSQL = new StringBuilder("select Id, StartDate,EndDate,Note,[OperatedUserOrOperatorId],[Type],UserOrOperatorId,StartIP,EndIP,IfDeleted,DeleteDate ");
            strSQL.Append(" from t_Forum_Ban where (@ip between StartIP and EndIP) and (StartDate <= @date and EndDate >= @date)");
            strSQL.Append(" and SiteId=@SiteId and [Type]=@type and IfDeleted = 0");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@ip", ip);
            cmd.Parameters.AddWithValue("@date", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@SiteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@type", Convert.ToInt16(EnumBanType.IP));

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetBansOfUserOrOperator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        {

            StringBuilder strSQL = new StringBuilder("select a.Id, a.StartDate,a.EndDate,a.Note,a.[OperatedUserOrOperatorId],a.[Type],a.UserOrOperatorId,a.startIP,a.endIP,b.Name as Name, a.IfDeleted, a.DeleteDate ");

            strSQL.Append(" from t_Forum_Ban a left join t_User" +conn.SiteId.ToString() + " b on a.UserOrOperatorId=b.Id where a.UserOrOperatorId=@userOrOperatorId");
            strSQL.Append(" and SiteId=@SiteId and [Type]=@type");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@SiteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@type", Convert.ToInt16(EnumBanType.User));

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetNotDeletedBanOfUserOrOperator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        {

            StringBuilder strSQL = new StringBuilder("select a.Id, a.StartDate,a.EndDate,a.Note,a.[OperatedUserOrOperatorId],a.[Type],a.UserOrOperatorId,a.startIP,a.endIP,b.Name as Name, a.IfDeleted, a.DeleteDate ");

            strSQL.Append(" from t_Forum_Ban a left join t_User" + conn.SiteId.ToString() + " b on a.UserOrOperatorId=b.Id where a.UserOrOperatorId=@userOrOperatorId");
            strSQL.Append(" and SiteId=@SiteId and [Type]=@type and a.IfDeleted = 0");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@SiteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@type", Convert.ToInt16(EnumBanType.User));

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }


        public static DataTable GetNotDeletedBansOfUserOrOperator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from t_Forum_Ban where SiteId=" + conn.SiteId + " and UserOrOperatorId=@userOrOperatorId and [Type]=@type and IfDeleted=0");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@type", Convert.ToInt16(EnumBanType.User));

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        #region Query
        public static DataTable GetBansByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, EnumBanType type, long ip, string name, string orderField, string orderDirection)
        {
            StringBuilder strSQL = new StringBuilder("select * from (");
            strSQL.Append(string.Format("select ROW_NUMBER() over(order by {0} {1} ) as rIdx,", orderField, orderDirection));
            strSQL.Append(" * ");
            strSQL.Append(" from t_Forum_Ban where SiteId=@SiteId and IfDeleted=0");
            if (ip != 0 || name != string.Empty)
            {
                strSQL.Append(string.Format(" and((Type={0} and UserOrOperatorId in (select Id from t_User{1} where Name=@name)) ", Convert.ToInt16(EnumBanType.User), conn.SiteId));
                strSQL.Append(string.Format(" or (Type={0} and {1} between startIP and endIP))", Convert.ToInt16(EnumBanType.IP), ip));
            }
            else
            {
                strSQL.Append(string.Format(" and ([Type]={0} or([Type]={1} and UserOrOperatorId in (select Id from t_User{2})))", Convert.ToInt16(EnumBanType.IP), Convert.ToInt16(EnumBanType.User), conn.SiteId));
            }
            int startNum = (pageIndex - 1) * pageSize + 1;
            int endNum = pageIndex * pageSize;
            strSQL.Append(string.Format(") returnV where rIdx between {0} and {1}", startNum, endNum));
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@SiteId", conn.SiteId);
            if (ip != 0 || name != string.Empty)
            {
                cmd.Parameters.AddWithValue("@name", name);
            }
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }
        public static int GetCountOfBansByQuery(SqlConnectionWithSiteId conn, SqlTransaction transaction, EnumBanType type, long ip, string name)
        {
            StringBuilder strSQL = new StringBuilder("select count(Id) from t_Forum_Ban ");
            strSQL.Append("where SiteId=@siteId and IfDeleted=0");
            if (ip != 0 || name != string.Empty)
            {
                strSQL.Append(string.Format(" and((Type={0} and UserOrOperatorId in (select Id from t_User{1} where Name like '%' + @name + '%' escape '/')) ", Convert.ToInt16(EnumBanType.User), conn.SiteId));
                strSQL.Append(string.Format(" or (Type={0} and {1} between startIP and endIP))", Convert.ToInt16(EnumBanType.IP), ip));
            }
            else
            {
                strSQL.Append(string.Format(" and ([Type]={0} or([Type]={1} and UserOrOperatorId in (select Id from t_User{2})))", Convert.ToInt16(EnumBanType.IP), Convert.ToInt16(EnumBanType.User), conn.SiteId));
            }
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@SiteId", conn.SiteId);
            if (ip != 0 || name != string.Empty)
            {
                cmd.Parameters.AddWithValue("@name", name);
            }
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        #endregion
        #endregion

        #region add
        public static int AddBan(SqlConnectionWithSiteId conn, SqlTransaction transaction, DateTime startDate, DateTime endDate, string note, int operatedUserOrOperatorId, Int64 banStartIP, Int64 banEndIP)
        {
            StringBuilder strSQL = new StringBuilder("Insert into t_Forum_Ban(SiteId,StartDate,EndDate,Note,OperatedUserOrOperatorId,Type,StartIP,EndIP) ");
            strSQL.Append("values(@SiteId, @StartDate,@EndDate,@Note,@OperatedUserOrOperatorId,@Type,@StartIP,@EndIP)");
            strSQL.Append("select @@identity");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@SiteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@Note", note);
            cmd.Parameters.AddWithValue("@OperatedUserOrOperatorId", operatedUserOrOperatorId);
            cmd.Parameters.AddWithValue("@Type", Convert.ToInt16(EnumBanType.IP));
            cmd.Parameters.AddWithValue("@StartIP", banStartIP);
            cmd.Parameters.AddWithValue("@EndIP", banEndIP);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int AddBan(SqlConnectionWithSiteId conn, SqlTransaction transaction, DateTime startDate, DateTime endDate, string note, int operatedUserOrOperatorId, int userOrOperatorId)
        {
            StringBuilder strSQL = new StringBuilder("Insert into t_Forum_Ban(SiteId,StartDate,EndDate,Note,OperatedUserOrOperatorId,Type,UserOrOperatorId) ");
            strSQL.Append("values(@SiteId, @StartDate,@EndDate,@Note,@OperatedUserOrOperatorId,@Type,@UserOrOperatorId)");
            strSQL.Append("select @@identity");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@SiteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@Note", note);
            cmd.Parameters.AddWithValue("@OperatedUserOrOperatorId", operatedUserOrOperatorId);
            cmd.Parameters.AddWithValue("@Type", Convert.ToInt16(EnumBanType.User));
            cmd.Parameters.AddWithValue("@UserOrOperatorId", userOrOperatorId);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        #endregion

        #region update
        public static void UpdateBan(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id, DateTime startDate, DateTime endDate, string note, int OperatedUserOrOperatorId, Int64 banStartIP, Int64 banEndIP)
        {
            StringBuilder strSQL = new StringBuilder("Update t_Forum_Ban set ");
            strSQL.Append("StartDate= @startDate, ");
            strSQL.Append("EndDate=@endDate, ");
            strSQL.Append("Note = @note , ");
            strSQL.Append("OperatedUserOrOperatorId=@operatedUserOrOperatorId, ");
            strSQL.Append("StartIP=@startIP, ");
            strSQL.Append("EndIP=@endIP ");
            strSQL.Append("where Id=@id and SiteId = @siteId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@note", note);
            cmd.Parameters.AddWithValue("@operatedUserOrOperatorId", OperatedUserOrOperatorId);
            cmd.Parameters.AddWithValue("@startIP", banStartIP);
            cmd.Parameters.AddWithValue("@endIP", banEndIP);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.ExecuteNonQuery();
        }

        public static void UpdateBan(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id, DateTime startDate, DateTime endDate, string note, int operatedUserOrOperatorId, int userOrOperatorId)
        {
            StringBuilder strSQL = new StringBuilder("Update t_Forum_Ban set ");
            strSQL.Append("StartDate= @startDate, ");
            strSQL.Append("EndDate=@endDate, ");
            strSQL.Append("Note = @note , ");
            strSQL.Append("OperatedUserOrOperatorId=@operatedUserOrOperatorId, ");
            strSQL.Append("UserOrOperatorId=@userOrOperatorId ");
            strSQL.Append(" where Id=@id and SiteId = @siteId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@note", note);
            cmd.Parameters.AddWithValue("@operatedUserOrOperatorId", operatedUserOrOperatorId);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region delete
        public static void DeleteBan(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id)
        {

            DateTime deleteDate = DateTime.UtcNow;
            StringBuilder strSQL = new StringBuilder("update t_Forum_Ban set IfDeleted=1,DeleteDate=@deleteDate where Id = @id and SiteId = @siteId;");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@deleteDate", deleteDate);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);

            cmd.ExecuteNonQuery();
        }
        #endregion

        public static int GetCountOfBanByUserOrOperatorIdAndDate(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, DateTime date)
        {

            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("select count(Id) from t_Forum_Ban where SiteId=" + conn.SiteId);
            strSQL.Append(" and UserOrOperatorId=@userOrOperatorId and (StartDate <= @date and EndDate >= @date)");
            strSQL.Append(" and IfDeleted='false' and Type=@type");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@type",Convert.ToInt16(EnumBanType.User));

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void DeleteBansByUserOrOperatorId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        {

            DateTime deleteDate = DateTime.UtcNow;
            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("update t_Forum_Ban set IfDeleted=1,DeleteDate=@deleteDate where SiteId=" + conn.SiteId + " and UserOrOperatorId=@userOrOperatorId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@deleteDate", deleteDate);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);

            cmd.ExecuteNonQuery();
        }
    }
}
