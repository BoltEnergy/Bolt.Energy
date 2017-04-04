
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
using System.Web.Security;
using System.Web.SessionState;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.UI.Common;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;

namespace Com.Comm100.Forum.UI
{
    public class Global : System.Web.HttpApplication
    {
       
        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                Com.Comm100.Forum.Language.LanguageHelper.LoadLanguage();
                Com.Comm100.Language.LanguageHelper.LoadLanguage();
            }
            catch (Exception exp)
            {
                string message = exp.Message;
            }
            //Intial Database configuration
            GlobalController.InitDatabase();

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //SessionUser currentUser = new SessionUser(1, 200000, true, 0, EnumApplicationType.enumForum);
            //Session["CurrentUser"] = currentUser;

            //string loginIp = Request.ServerVariables["remote_addr"];
            //string strTimezoneOffset = CommonFunctions.ReadCookies("TimezoneOffset");
            //double timezoneOffset = strTimezoneOffset.Length == 0 ? 0 : Convert.ToDouble(strTimezoneOffset);

            //UserPermissionCache userPermissionList;
            //SessionUser sessionUser = LoginAndRegisterProcess.UserOrOperatorLogin(
            //    200000, "Gavin@163.com", "1", loginIp, timezoneOffset, out userPermissionList);
            //Session["CurrentUser"] = sessionUser;
            //SiteSession.UserPermissionList = userPermissionList;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}