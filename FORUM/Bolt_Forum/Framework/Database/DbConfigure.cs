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
using System.Data.Common;
using System.Configuration;

namespace Com.Comm100.Framework.Database
{
    public class DbConfigure
    {
        public static string strConnectionString = "";//"Data Source={0};Initial Catalog={1};Integrated Security=True;Min Pool Size={2};Max Pool Size={3};";

        private static string Server;
        private static string DatabaseName;
        private static bool IfIntegratedSecurity;
        private static string UserId;
        private static string Password;
        private static int MaxPoolSize;
        private static int MinPoolSize;

        public static void Init(string server, string databaseName, bool isIntegratedSecurity, string userID, string password, int maxPoolSize, int minPoolSize)
        {
            DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
            dbConnectionStringBuilder.Add("Data Source", server);
            dbConnectionStringBuilder.Add("Initial Catalog", databaseName);
            if (isIntegratedSecurity == true)
            {
                dbConnectionStringBuilder.Add("Integrated Security", "True");
            }
            else
            {
                dbConnectionStringBuilder.Add("Integrated Security", "False");
                dbConnectionStringBuilder.Add("User ID", userID);
                dbConnectionStringBuilder.Add("Password", password);
            }
            dbConnectionStringBuilder.Add("Pooling", "true");
            dbConnectionStringBuilder.Add("Min Pool Size", minPoolSize);
            dbConnectionStringBuilder.Add("Max Pool Size", maxPoolSize);

            DbConfigure.strConnectionString = ConfigurationManager.ConnectionStrings["BoltForumConnectionString"].ConnectionString; //dbConnectionStringBuilder.ConnectionString;

            //Save parameters
            DbConfigure.Server = server;
            DbConfigure.DatabaseName = databaseName;
            DbConfigure.IfIntegratedSecurity = isIntegratedSecurity;
            DbConfigure.UserId = userID;
            DbConfigure.Password = password;
            DbConfigure.MaxPoolSize = maxPoolSize;
            DbConfigure.MinPoolSize = minPoolSize;
        }

        /*Commented by techtier code not in use*/

        //public static string GetConnectionStringForSiteDatabase(int siteId)
        //{
        //    DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
        //    dbConnectionStringBuilder.Add("Data Source", DbConfigure.Server);
        //    dbConnectionStringBuilder.Add("Initial Catalog", DbHelper.GetDBName(siteId));
        //    if (DbConfigure.IfIntegratedSecurity == true)
        //    {
        //        dbConnectionStringBuilder.Add("Integrated Security", "True");
        //    }
        //    else
        //    {
        //        dbConnectionStringBuilder.Add("Integrated Security", "False");
        //        dbConnectionStringBuilder.Add("User ID", DbConfigure.UserId);
        //        dbConnectionStringBuilder.Add("Password", DbConfigure.Password);
        //    }
        //    dbConnectionStringBuilder.Add("Pooling", "true");
        //    dbConnectionStringBuilder.Add("Min Pool Size", DbConfigure.MinPoolSize);
        //    dbConnectionStringBuilder.Add("Max Pool Size", DbConfigure.MaxPoolSize);

        //    return dbConnectionStringBuilder.ConnectionString;       
        //} 
    }
}
