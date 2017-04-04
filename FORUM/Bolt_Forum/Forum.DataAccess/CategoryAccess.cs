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
using Com.Comm100.Framework.FieldLength;

namespace Com.Comm100.Forum.DataAccess
{
    public class CategoryAccess
    {
        public static DataTable GetAllCategorys(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select Id,SiteId,OrderId,Name,Status,[Description] from t_Forum_Category");
            strSQL.Append(" where SiteId=@SiteId Order by OrderId;");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@SiteId", conn.SiteId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetNotClosedCategorys(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select Id,SiteId,OrderId,Name,Status,[Description] from t_Forum_Category");
            strSQL.Append(" where SiteId=@SiteId and Status='0' Order by OrderId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@SiteId", conn.SiteId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetCategoryByCategoryId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int cagegoryId)
        {

            StringBuilder strSQL = new StringBuilder("select Id,SiteId,OrderId,Name,Status,[Description] from t_Forum_Category where Id=@Id and SiteId=@SiteId Order by OrderId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@Id", cagegoryId);
            cmd.Parameters.AddWithValue("@SiteId", conn.SiteId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static int AddCategory(SqlConnectionWithSiteId conn, SqlTransaction transaction, string name,EnumCategoryStatus categoryStatus,string description)
        {

            StringBuilder strSQL = new StringBuilder("update t_Forum_Category set OrderId=orderId+1 where SiteId=@siteId;");
            strSQL.Append("Insert into t_Forum_Category (SiteId, OrderId, Name,Status, Description)");
            strSQL.Append("values(@siteId, @orderId, @name,@status,@description); ");
            strSQL.Append("select @@identity");


            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@orderId", 0);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@status", Convert.ToInt16(categoryStatus));
            cmd.Parameters.AddWithValue("@description", description);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void UpdateCategory(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, string name,EnumCategoryStatus categoryStatus, string description)
        {

            StringBuilder strSQL = new StringBuilder("Update t_Forum_Category set ");
            strSQL.Append("Name = @name, Description = @description , Status=@status where Id=@id and SiteId = @siteId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@status", Convert.ToInt16(categoryStatus));
            cmd.Parameters.AddWithValue("@id", categoryId);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);

            cmd.ExecuteNonQuery();
        }

        public static void DeleteCategory(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId)
        {

            StringBuilder strSQL = new StringBuilder("select OrderId From t_Forum_Category Where Id=@categoryId and SiteId = @siteId;");

            strSQL.Append("Delete t_Forum_Category where Id = @categoryId and SiteId = @siteId;");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);

            int orderIdResult = Convert.ToInt32(cmd.ExecuteScalar());
            strSQL.Remove(0, strSQL.Length);

            strSQL.Append("update t_Forum_Category set OrderId = OrderId-1 where OrderId > @orderId and SiteId = @siteId");

            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@orderId", orderIdResult);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);


            cmd.ExecuteNonQuery();    

        }

        public static void IncreaseSortCategorys(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("Select orderId From t_Forum_Category Where Id=@id and SiteId = @siteId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@id", categoryId);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            int oldOrderId = Convert.ToInt32(cmd.ExecuteScalar());

            strSQL.Remove(0, strSQL.Length);

            strSQL.Append("Update t_Forum_Category set OrderId = OrderId+1 where SiteId = @siteId and OrderId=@orderId;");
            strSQL.Append("Update t_Forum_Category set OrderId = OrderId-1 where Id=@id and Siteid = @siteId;");

            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@id", categoryId);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@orderId", oldOrderId - 1);


            cmd.ExecuteNonQuery();
        }

        public static void DecreaseSortCategorys(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("Select orderId From t_Forum_Category Where Id=@id and SiteId = @siteId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@id", categoryId);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            int oldOrderId = Convert.ToInt32(cmd.ExecuteScalar());

            strSQL.Remove(0, strSQL.Length);

            strSQL.Append("Update t_Forum_Category set OrderId = OrderId-1 where SiteId = @siteId and OrderId=@orderId;");
            strSQL.Append("Update t_Forum_Category set OrderId = OrderId+1 where Id=@id and SiteId = @siteId;");

            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@id", categoryId);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@orderId", oldOrderId + 1);

            cmd.ExecuteNonQuery();
        }

        /*--------------------------2.0------------------------------*/
        public static int GetCountOfCategoriesById(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id)
        {
            return 0;
        }
    }
}
