#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Web;
using System.Configuration;
using System.Web.SessionState;
using System.Reflection;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Framework.SessionControl
{
    public class SessionIdDomain : IHttpModule
    {
        private string _rootDomain = string.Empty;

        public void Dispose()
        {
        }

        public void Init(HttpApplication application)
        {
            try
            {
                _rootDomain = ConfigurationManager.AppSettings["domain"];
            }
            catch (Exception exp)
            {
                LogHelper.WriteExceptionLog(exp);
            }
            application.EndRequest += new EventHandler(this.ApplicationEndRequest);
        }

        private void ApplicationEndRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            for (int i = 0; i < context.Response.Cookies.Count; i++)
            {
                if ("ASP.NET_SessionId".Equals(context.Response.Cookies[i].Name))
                {
                    context.Response.Cookies[i].Domain = _rootDomain;
                }
            }
        }
    }
}
