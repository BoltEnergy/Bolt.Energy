#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif

using System;
using System.Web;
using Com.Comm100.Framework.Common;
using System.Text.RegularExpressions;

namespace Com.Comm100.Forum.UI
{
    public class UrlRewrite : IHttpModule
    {
        public void Dispose()
        {
        }
        public void Init(HttpApplication application)
        {
            application.BeginRequest += new EventHandler(this.ApplicationBeginRequest);
        }
        private void ApplicationBeginRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            //context.Response.
            string url = context.Request.RawUrl;
            if (url.Contains(".aspx"))
            {
                try
                {                    
                    //t or f rewrite
                    string tempUrl ="";
                    if (url.Contains(".aspx?UrlReferrer="))
                    {   
                        int index = url.IndexOf("UrlReferrer=");
                        tempUrl =url.Substring(index);
                        url = url.Substring(0,index-1);                        
                    }
                    Regex regularExpressions = new Regex(@"/.*_([tf])(\d*)\.aspx\??(.*)", RegexOptions.IgnoreCase);
                    Match match = regularExpressions.Match(url,url.LastIndexOf("/"));
                    if (match.Success)
                    {
                        if (!"".Equals(match.Groups[3].Value))
                        {
                            tempUrl= "&"+match.Groups[3].Value+tempUrl;
                        }
                        if ("t".Equals(match.Groups[1].Value) || "T".Equals(match.Groups[1].Value))
                        {
                            context.RewritePath(@"~/Topic.aspx?topicId=" + match.Groups[2] + tempUrl);
                            //context.Response.Redirect(@"~/Topic.aspx?topicId=" + match.Groups[2] + tempUrl);
                        }
                        else
                        {
                            context.RewritePath(@"~/default.aspx?forumId=" + match.Groups[2] + tempUrl);
                            //context.Response.Redirect(@"~/default.aspx?forumId=" + match.Groups[2] + tempUrl);
                        }
                    }
                    else
                    {
                        if (tempUrl != "")
                        {
                            url = url +"?"+ tempUrl;
                            context.RewritePath(url);
                            //context.Response.Redirect(url);
                        }

                    }
                }
                catch (Exception exp)
                {
                    LogHelper.WriteExceptionLog(exp);//page show
                }
            }
        }
    }
}
