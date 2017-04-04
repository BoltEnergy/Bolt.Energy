#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2009                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif

using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.UI.Common;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Language;
using Com.Comm100.Language;

namespace Com.Comm100.Forum.UI
{
    public class UIBasePage : BasePage
    {
        

        

        protected override void InitializeCulture()
        {
            base.InitializeCulture();

            //Get Custom Language
            string languageName = LanguageHelper.GetLanguageName((Com.Comm100.Language.EnumLanguage)Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["EnumLanguage"]));
            //Set to cultureInfo
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(languageName);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(languageName);
        }


        #region InitLanguage

        protected virtual void InitLanguage() { }

        #endregion InitLanguage



        #region Page error

        private bool _ifError = false;

        public bool IfError
        {
            get { return this._ifError; }
            set { this._ifError = value; }
        }

        #endregion

        /*Get User Permission List*/
        public UserPermissionCache UserPermissionCache
        {
            get
            {
                if (Session["UserPermissionList"] != null)
                    return Session["UserPermissionList"] as UserPermissionCache;
                else
                    throw new Exception("用户没有权限列表");
            }
        }
        public UserForumPermissionItem UserForumPermissionList(int forumId)
        {
            return this.UserPermissionCache.UserForumPermissionsList[forumId]
                as UserForumPermissionItem;
        }

        public virtual bool IfValidateForumClosed { get { return true; } }

        private int _visitingForumId = 0;
        private int VisitingForumId
        {
            get { return _visitingForumId; }
        }

        private EnumForumStatus _visitingForumStatus;
        private EnumForumStatus VisitingForumStatus
        {
            get { return _visitingForumStatus; }
        }

        private bool _ifSiteClosed;
        public bool IfSiteClosed
        {
            get { return _ifSiteClosed; }
        }

        /*Gavin 2.0 */
        private bool _ifSiteOnlyVisit;
        public bool IfSiteOnlyVisit
        { get { return _ifSiteOnlyVisit; } }
        /*Gavin 2.0*/
        private bool _ifAllowGuestUserViewForum;
        public bool IfAllowGuestUserViewForum
        { get { return _ifAllowGuestUserViewForum; } }

        private bool _ifValidateForumHidden;
        public bool IfValidateForumHidden
        {
            get { return _ifValidateForumHidden; }
        }

        private bool _ifValidateForumLock;
        public bool IfValidateForumLock
        {
            get { return _ifValidateForumLock; }
        }

        /*Gavin 2.0*/
        private bool _ifValidateGuestAllowViewForum;
        public bool IfValidateGuestAllowViewForum
        { get { return _ifValidateGuestAllowViewForum; } }

        private SiteSettingWithPermissionCheck _siteSettingBase;
        public SiteSettingWithPermissionCheck SiteSettingBase
        {
            get { return _siteSettingBase; }
        }

        private bool CanVisitForumsAndTopics()
        {

            bool canVisit = true;
            if (VisitingForumStatus != EnumForumStatus.Open && !IfAdmin() && !IfModerator())
            {
                canVisit = false;
            }
            return canVisit;
        }

        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);

            // Require Login
            if (!(this is Com.Comm100.Forum.UI.Login) && !(this is Com.Comm100.Forum.UI.Siteclosed) && this.UserOrOperatorId <= 0)
            {
                try
                {
                    //SiteSettingWithPermissionCheck siteSettings = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, this.UserOrOperatorId);

                    //if (siteSettings.IfForumRequireLogin)
                    //{
                    //    Response.Redirect("~/Login.aspx?siteid=" + this.SiteId, false);
                    //}
                    /*Gavin 2.0*/
                    if (IfAllowGuestUserViewForum == false && IfValidateGuestAllowViewForum)
                    {
                        Response.Redirect("~/Login.aspx?siteid=" + this.SiteId, false);

                    }
                }
                catch (Exception exp)
                {
                    string errText = "<span class=\"ErrorLabel\">" + exp.Message.Replace("'", "\\'") + "</span>";
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "BasePageErrorWriter", "document.write('<span style=\"color: red\">Error: " + errText + "</span>');alert('Error: " + exp.Message.Replace("'", "\\'") + "');", true);
                    this.IfError = true;
                }
            }
            // //////////////////////

            if (!this.IfError)
            {
                try
                {
                    if (this.IfValidateForumClosed && this.IfSiteClosed)
                    {
                        string url = System.Web.HttpUtility.UrlEncode(System.Web.HttpContext.Current.Request.Url.PathAndQuery);
                        Response.Redirect("~/Siteclosed.aspx?siteId=" + SiteId + "&ReturnUrl=" + url, false);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    /*Gavin 2.0 If forum Closed, redirect to closed infor page*/
                    else if (this.IfValidateForumHidden && !this.CanVisitForumsAndTopics()
                        && VisitingForumStatus == EnumForumStatus.Hide)
                    {
                        string applicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                        if (applicationPath != "/")
                        {
                            applicationPath = applicationPath + "/";
                        }
                        //Response.Redirect("~/ForumIsClosed.aspx?siteId=" + SiteId);
                        //Response.Write("<script language=javascript>alert('Error: Forum with ForumId = " + this._visitingForumId.ToString() + " is hidden.');window.location='" + applicationPath + "Default.aspx?siteId=" + SiteId + "';</script>");
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    /*Gavin 2.0 If forum Closed, redirect to closed infor page*/
                    else if (this.IfValidateForumLock && !this.CanVisitForumsAndTopics()
                        && VisitingForumStatus == EnumForumStatus.Lock)
                    {
                        string applicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                        if (applicationPath != "/")
                        {
                            applicationPath = applicationPath + "/";
                        }
                        Response.Redirect("~/ForumIsClosed.aspx?siteId=" + SiteId);
                        //Response.Write("<script language=javascript>alert('Error: Forum with ForumId = " + this._visitingForumId.ToString() + " is locked.');window.location='" + applicationPath + "Default.aspx?siteId=" + SiteId + "';</script>");
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                }
                catch (System.Threading.ThreadAbortException)
                {
                }
                catch (Exception exp)
                {
                    string errText = "<span class=\"ErrorLabel\">" + exp.Message.Replace("'", "\\'") + "</span>";
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "BasePageErrorWriter", "document.write('<span style=\"color: red\">Error: " + errText + "</span>');alert('Error: " + exp.Message.Replace("'", "\\'") + "');", true);
                    this.IfError = true;
                }
            }

            this.InitLanguage();
        }

        #region SessionUser

        private SessionUser _currentUserOrOperator;

        public SessionUser CurrentUserOrOperator
        {
            get
            {
                SessionUser currentUser = null;
                if (this._currentUserOrOperator != null)
                {
                    if (this._currentUserOrOperator.SiteId == this.SiteId)
                    {
                        currentUser = this._currentUserOrOperator;
                        string key = string.Format(ConstantsHelper.CacheKey_InactivedOrDeletedUserId, SiteId, currentUser.UserOrOperatorId);
                        if (Cache[key] != null)
                        {
                            //Cache.Remove(key);
                            List<string> sessionIdList = Cache[key] as List<string>;
                            if (!sessionIdList.Contains(Session.SessionID))
                            {
                                Session.Remove("CurrentUser");
                                Session.Abandon();
                                currentUser = null;
                                this._currentUserOrOperator = null;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "UserOrOperatorIsDeletedOrInactivedScript", string.Format("<script>alert('{0}')</script>", "Error: Current user is inactive or deleted."));

                                sessionIdList.Add(Session.SessionID);
                            }
                        }
                    }
                    else
                    {
                        Session.Remove("CurrentUser");
                        Session.Abandon();
                    }
                }
                return currentUser;
            }
            set { this._currentUserOrOperator = value; }
        }
        public int UserOrOperatorId
        {
            get
            {
                int userOrOperatorId = 0;
                if (this.CurrentUserOrOperator != null)
                {
                    userOrOperatorId = this.CurrentUserOrOperator.UserOrOperatorId;
                }
                return userOrOperatorId;
            }
        }

        public bool IfGuest
        {
            get
            {
                if (this.CurrentUserOrOperator == null)
                    return true;
                else
                    return false;
            }
        }

        public bool IfOperator
        {
            get
            {
                bool ifOperator = false;
                if (this.CurrentUserOrOperator != null)
                {
                    ifOperator = this.CurrentUserOrOperator.IfOperator;
                }
                return ifOperator;
            }
        }
        #endregion

        #region Init
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            if (!this.IfError)
            {
                try
                {
                    this.InitCurrentUserOrOperator();

                    if (!(Page is Com.Comm100.Forum.UI.AdminPanel.AdminBasePage || Page is Com.Comm100.Forum.UI.ModeratorPanel.ModeratorBasePage))
                    {
                        string url = System.Web.HttpContext.Current.Request.RawUrl;
                        System.Text.RegularExpressions.Regex regularExpressions = new
                            System.Text.RegularExpressions.Regex(@"/.*\.aspx\?.*TemplateId=(\d*).*",
                            System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        System.Text.RegularExpressions.Match match = regularExpressions.Match(url);
                        if (match.Success)
                        {
                            StyleTemplateWithPermissionCheck styleTemplateForPreview = StyleProcess.GetTemplateByTemplateId(SiteId, 0, Convert.ToInt32(match.Groups[1].Value));
                            this.Theme = styleTemplateForPreview.Path;
                        }
                        else
                        {
                            StyleTemplateWithPermissionCheck styleTemplate = StyleProcess.GetStyleTemplateBySiteId(0, SiteId);
                            this.Theme = styleTemplate.Path;
                        }
                    }

                    this.InitSiteStatus();
                    this.InitGuestUserPermissionsSetting();
                    this.InitIfValidateForumHidden();
                    this.InitIfValidateForumLock();
                    this.InitIfValidateGuestAllowViewForum();
                    this.InitVisitingForumId();
                    this.InitVisitingForumStatus();
                    this.InitTimezoneOffset();

                }
                catch (Exception exp)
                {
                    if ((exp is ExceptionWithCode) && ((ExceptionWithCode)exp).GetErrorCode() == EnumErrorCode.enumSiteNotExist)
                    {
                        Response.Write("<script language='javascript'>alert('Error: Site " + SiteId + " does not exist.');window.location='http://www.comm100.com/forum';</script>");
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        string errText = "<span class=\"ErrorLabel\">" + exp.Message.Replace("'", "\\'") + "</span>";
                        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "BasePageErrorWriter", "document.write('<span style=\"color: red\">Error: " + errText + "</span>');alert('Error: " + exp.Message.Replace("'", "\\'") + "');", true);
                        this.IfError = true;
                    }
                }
            }
        }

        protected void OnPreInit4AdminPanel(EventArgs e)
        {
            base.OnPreInit(e);
            this.InitCurrentUserOrOperator();
            this.InitSiteStatus();
            this.InitIfValidateForumHidden();
            this.InitIfValidateForumLock();
            this.InitIfValidateGuestAllowViewForum();
            this.InitVisitingForumId();
            this.InitVisitingForumStatus();
            this.InitTimezoneOffset();
        }
        private void InitCurrentUserOrOperator()
        {
            if (Session["CurrentUser"] != null)
            {
                this._currentUserOrOperator = Session["CurrentUser"] as SessionUser;
            }
        }
        private void InitSiteStatus()
        {
            /*Gavin 2.0 change for add new status*/
            EnumSiteStatus siteStatus = WebUtility.GetSiteStatus(SiteId, out _siteSettingBase);
            if (siteStatus == EnumSiteStatus.Close)
            { this._ifSiteOnlyVisit = false; this._ifSiteClosed = true; }
            else if (siteStatus == EnumSiteStatus.VisitOnly)
            { this._ifSiteOnlyVisit = true; this._ifSiteClosed = false; }
            else
            { this._ifSiteOnlyVisit = false; this._ifSiteClosed = false; }

        }
        /*Gavin 2.0*/
        private void InitGuestUserPermissionsSetting()
        {
            WebUtility.GetGuestUserPermissions(SiteId, out _ifAllowGuestUserViewForum);
        }
        private void InitIfValidateForumHidden()
        {
            this._ifValidateForumHidden = WebUtility.IfValidateForumHidden();
        }
        private void InitIfValidateForumLock()
        {
            this._ifValidateForumLock = WebUtility.IfValidateForumLock();
        }
        private void InitVisitingForumId()
        {
            this._visitingForumId = WebUtility.GetVisitingForumId(SiteId, UserOrOperatorId, IfOperator);
        }
        private void InitVisitingForumStatus()
        {
            if (this._visitingForumId > 0)
            {
                this._visitingForumStatus = WebUtility.GetVisitingForumStatus(_visitingForumId, SiteId);
            }
        }
        /*Gavin 2.0*/
        private void InitIfValidateGuestAllowViewForum()
        {
            this._ifValidateGuestAllowViewForum = WebUtility.IfValidateGuestAllowViewForum();
        }

        #endregion

        #region IfAdmin & IfModerator

        public bool IfModerator()
        {
            bool ifModerator = false;

            Moderator[] moderator = ForumProcess.GetModeratorsByForumId(this._visitingForumId, SiteId, UserOrOperatorId, IfOperator);
            for (int i = 0; i < moderator.Length; i++)
            {
                if (UserOrOperatorId != 0)
                {
                    if (moderator[i].Id == UserOrOperatorId)
                    {
                        ifModerator = true;
                        break;
                    }
                }
            }
            return ifModerator;
        }

        public bool IfModerator(int forumId)
        {
            //bool ifModerator = false;

            //Moderator[] moderator = ForumProcess.GetModeratorsByForumId(forumId, SiteId, UserOrOperatorId, IfOperator);
            //for (int i = 0; i < moderator.Length; i++)
            //{
            //    if (UserOrOperatorId != 0)
            //    {
            //        if (moderator[i].Id == UserOrOperatorId)
            //        {
            //            ifModerator = true;
            //            break;
            //        }
            //    }
            //}
            //return ifModerator;
            return this.UserForumPermissionList(forumId).IfModerator;
        }

        public bool IfAdmin()
        {
            //bool ifAdmin = false;
            //UserOrOperator operatingOperator = UserProcess.GetNotDeletedUserOrOperatorById(SiteId, UserOrOperatorId);
            //if (operatingOperator != null)
            //{
            //    if (operatingOperator.IfForumAdmin == true)
            //    {
            //        ifAdmin = true;
            //    }
            //}
            //return ifAdmin;
            return this.UserPermissionCache.IfAdministrator;
        }

        public bool IfModeratorInSite()
        {
            bool ifModerator = false;
            UserOrOperator operatingOperator = UserProcess.GetNotDeletedUserOrOperatorById(SiteId, UserOrOperatorId);
            if (operatingOperator != null)
            {
                ifModerator = UserProcess.IfModerator(SiteId, UserOrOperatorId);
            }
            return ifModerator;
        }

        #endregion

        

    }
}
