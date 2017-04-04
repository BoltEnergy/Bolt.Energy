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
using Com.Comm100.Framework.ASPNETState;
using System.Web.Security;
using System.Web.Configuration;
using Com.Comm100.Forum.Process;

namespace Com.Comm100.Forum.UI.ModeratorPanel
{
    public class ModeratorBasePage : Com.Comm100.Forum.UI.UIBasePage
    {
        protected override void OnInit(EventArgs e)
        {
            try
            {
                this.ClearPageCache();

                base.OnInit(e);
            }
            catch (Exception exp)
            {
                string errText = "<span class=\"ErrorLabel\">" + exp.Message.Replace("'", "\\'") + "</span>";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "BasePageErrorWriter", "document.write('<span style=\"color: red\">Error: " + errText + "</span>');alert('Error: " + exp.Message.Replace("'", "\\'") + "');", true);
                this.IfError = true;
            }
        }
        protected void ClearPageCache()
        {
            Response.BufferOutput = true;
            Response.Expires = 0;
            Response.ExpiresAbsolute = DateTime.UtcNow.AddMinutes(-1);
            Response.CacheControl = "no-cache";
        }
        protected SessionUser CurrentUserOrOperator
        {
            get
            {
                SessionUser currentUser = null;
                if (Session["CurrentUser"] != null)
                {
                    currentUser = (SessionUser)Session["CurrentUser"];
                    if (!this.IfModeratorInSite())
                    {
                        currentUser = null;
                    }
                }
                return currentUser;
            }
        }

        //public override bool IfValidateForumClosed
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}

        protected override void OnPreLoad(EventArgs e)
        {
            try
            {
                base.OnPreLoad(e);
                if (Session.IsNewSession)
                {
                    string sCookieHeader = Request.Headers["Cookie"];
                    if (null != sCookieHeader && sCookieHeader.IndexOf("ASP.NET_SessionId") >= 0)
                    {
                        if (Request.IsAuthenticated)
                        {
                            FormsAuthentication.SignOut();
                        }
                        Response.Redirect(string.Format("~/ModeratorPanel/Login.aspx?Timeout=&ReturnUrl={0}&siteId={1}", HttpUtility.UrlEncode(Request.Url.PathAndQuery), SiteId));
                    }
                }
                if (this.CurrentUserOrOperator == null || this.CurrentUserOrOperator.UserOrOperatorId == 0 || !this.CurrentUserOrOperator.IfModeratorLogin)
                {
                    string currentUrl = HttpUtility.UrlEncode(this.Request.Url.PathAndQuery);
                    Response.Redirect(string.Format("~/ModeratorPanel/Login.aspx?siteid={1}", currentUrl, SiteId));
                }
            }
            catch (Exception exp)
            {
                string errText = "<span class=\"ErrorLabel\">" + exp.Message.Replace("'", "\\'") + "</span>";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "BasePageErrorWriter", "document.write('<span style=\"color: red\">Error: " + errText + "</span>');alert('Error: " + exp.Message.Replace("'", "\\'") + "');", true);
                this.IfError = true;
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            try
            {
#if OPENSOURCE
#else
                if ((SiteProcess.GetSiteAppTypesById(SiteId) & Convert.ToInt32(Com.Comm100.Framework.Enum.EnumApplicationType.enumForum)) != Convert.ToInt32(Com.Comm100.Framework.Enum.EnumApplicationType.enumForum))
                {
                    if (Session["CurrentUser"] == null)
                        Response.Redirect(string.Format("{0}/Login.aspx", WebConfigurationManager.AppSettings["AdminUrl"]), false);
                    else
                        Response.Redirect(string.Format("{0}/Announcement/Announcements.aspx", WebConfigurationManager.AppSettings["AdminUrl"]), false);

                }
#endif
                /*****OnPreInit4ModeratorPanel******/
                base.OnPreInit4AdminPanel(e);
            }
            catch (Exception exp)
            {
                string errText = "<span class=\"ErrorLabel\">" + exp.Message.Replace("'", "\\'") + "</span>";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "BasePageErrorWriter", "document.write('<span style=\"color: red\">Error: " + errText + "</span>');alert('Error: " + exp.Message.Replace("'", "\\'") + "');", true);
                this.IfError = true;
            }
        }
    }
}
