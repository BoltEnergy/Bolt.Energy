#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Data.SqlClient;

namespace Com.Comm100.Framework.Database
{

    public class DbHelper
    {
        private static readonly string BaseDBName = "Comm100.Forum";        

        public static string GetDBName(int siteId)
        {
            return BaseDBName;
        }

        public static SqlConnectionWithSiteId GetSqlConnection(int siteID)
        {
            return new SqlConnectionWithSiteId(siteID, new SqlConnection(DbConfigure.strConnectionString));
        }

        public static SqlConnection GetGeneralDatabaeConnectin()
        {
            return new SqlConnection(DbConfigure.strConnectionString);
        }

        public static void OpenConn(SqlConnectionWithSiteId conn)
        {
            if (conn.SqlConn.State == ConnectionState.Open)
            {
            }
            else
            {
                conn.SqlConn.Open();
                if (conn.SiteId > 0)    
                {
                    conn.SqlConn.ChangeDatabase(GetDBName(conn.SiteId));
                }
            }
        }

        public static void OpenConn(SqlConnection conn)
        {
            if (conn.State == ConnectionState.Open)
            {
            }
            else
            {
                conn.Open();
            }
        }

        public static void CloseConn(SqlConnectionWithSiteId conn)
        {
            if (conn != null)
            {
                conn.SqlConn.Dispose();
            }
        }

        public static void CloseConn(SqlConnection conn)
        {
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void RollbackTransaction(SqlTransaction transaction)
        {
            if (transaction != null)
            {
                transaction.Rollback();
            }
        }

    }
}
