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
using System.Web;
using Com.Comm100.Framework.Database;
using System.Web.Configuration;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.UI.Common
{
    public class GlobalController
    {
        public static void InitDatabase()
        {
            //Get database configuration from ini file
#if OPENSOURCE
            string databaseName = WebConfigurationManager.AppSettings["forumDatabaseName"];
#else
            string databaseName = WebConfigurationManager.AppSettings["databaseName"];
#endif
            DbConfigure.Init(WebConfigurationManager.AppSettings["serverDbName"],
            databaseName,
            Convert.ToBoolean(WebConfigurationManager.AppSettings["isIntegratedSecurity"]),
            WebConfigurationManager.AppSettings["userId"],
            WebConfigurationManager.AppSettings["password"],
            Convert.ToInt32(WebConfigurationManager.AppSettings["maxPoolSize"]),
            Convert.ToInt32(WebConfigurationManager.AppSettings["minPoolSize"]));
        }

        //public static void InitTranscriptEmailSmtpConfig()
        //{           
        //    try
        //    {
        //        TranscriptEmail.InitSmtpConfig(WebConfigurationManager.AppSettings["smtpServer"],
        //           WebConfigurationManager.AppSettings["smtpUserName"],
        //           WebConfigurationManager.AppSettings["smtpPassword"],
        //           Convert.ToBoolean(WebConfigurationManager.AppSettings["smtpEnableSsl"]),
        //           Convert.ToInt32(WebConfigurationManager.AppSettings["smtpPort"]),
        //           WebConfigurationManager.AppSettings["smtpFromAddress"]);
        //    }
        //    catch (Exception exp)
        //    {
        //        LogHelper.WriteExceptionLog(exp);
        //    }
        //}

        public static bool CheckGeneralDatabaseVersion()
        {
            try
            {
                //Check General Database Version

                return true;
            }
            catch (Exception exp)
            {
                LogHelper.WriteExceptionLog(exp);
            }

            return false;
        }
    }
}
