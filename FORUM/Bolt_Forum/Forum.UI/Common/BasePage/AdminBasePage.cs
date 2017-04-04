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
using Com.Comm100.Framework.Common;
using System.Web.Security;
using Com.Comm100.Framework.Enum;
using System.Web.Configuration;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;

namespace Com.Comm100.Forum.UI.AdminPanel
{
    public class AdminBasePage : Com.Comm100.Forum.UI.UIBasePage
    {
        protected override void OnInit(EventArgs e)
        {
#if OPENSOURCE
#else
            CheckQueryString("SiteId");
#endif
            this.ClearPageCache();

            base.OnInit(e);
        }
        protected void ClearPageCache()
        {
            Response.BufferOutput = true;
            Response.Expires = 0;
            Response.ExpiresAbsolute = DateTime.UtcNow.AddMinutes(-1);
            Response.CacheControl = "no-cache";
        }
        public SessionUser CurrentUserOrOperator
        {
            get
            {
                try
                {
                    SessionUser currentUser = null;
                    if (Session["CurrentUser"] != null && this.UserOrOperatorId > 0)
                    {
                        currentUser = (SessionUser)Session["CurrentUser"];
                        if (!currentUser.IfOperator)
                        {
                            UserOrOperator userOrOperator = UserProcess.GetNotDeletedUserOrOperatorById(SiteId, currentUser.UserOrOperatorId);
                            if (!userOrOperator.IfForumAdmin)
                                currentUser = null;
                        }
                    }
                    return currentUser;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public override bool IfValidateForumClosed
        {
            get
            {
                return false;
            }
        }

        public override bool IfValidateIPBanned
        {
            get
            {
                return false;
            }
        }

        protected override void OnPreLoad(EventArgs e)
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
                    Response.Redirect(string.Format("~/AdminPanel/Login.aspx?Timeout=&ReturnUrl={0}&siteId={1}", HttpUtility.UrlEncode(Request.Url.PathAndQuery), SiteId));
                }
            }
            if (this.CurrentUserOrOperator == null || this.CurrentUserOrOperator.UserOrOperatorId == 0 || (!this.CurrentUserOrOperator.IfAdminLogin && !this.CurrentUserOrOperator.IfForumAdministratorLogin))
            {
                string currentUrl = HttpUtility.UrlEncode(this.Request.Url.PathAndQuery);
                Response.Redirect(string.Format("~/AdminPanel/Login.aspx?ReturnUrl={0}&siteid={1}", currentUrl, SiteId));
            }
            InitUserPermissionCache();
        }

        protected override void OnPreInit(EventArgs e)
        {
#if OPENSOURCE
#else
            if ((SiteProcess.GetSiteAppTypesById(SiteId) & Convert.ToInt32(EnumApplicationType.enumForum)) != Convert.ToInt32(EnumApplicationType.enumForum))
            {
                if (Session["CurrentUser"] == null)
                    Response.Redirect(string.Format("{0}/Login.aspx", WebConfigurationManager.AppSettings["AdminUrl"]), false);
                else
                    Response.Redirect(string.Format("{0}/AdminPanel/Dashboard.aspx", WebConfigurationManager.AppSettings["AdminUrl"]), false);

            }
#endif
            base.OnPreInit4AdminPanel(e);
        }

        private void InitUserPermissionCache()
        {
            if (this.CurrentUserOrOperator != null && this.UserPermissionCache == null)
            {
                SiteSession.UserPermissionList = UserProcess.GetUserPermissionCacheById(SiteId,
                            CurrentUserOrOperator.UserOrOperatorId);
            }
        }
    }
}
