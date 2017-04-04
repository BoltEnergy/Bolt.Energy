#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif

using System;
using System.Collections.Generic;
using System.Web;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.UI.Common;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Forum.UI;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI
{
    public class UIBasePage : BasePage
    {
        private bool _ifAdmin = false;
        private bool _ifModerator = false;
       
        /*Get User Permission List*/
        public UserPermissionCache UserPermissionCache
        {
            get
            {
                if (SiteSession.UserPermissionList != null)
                    return SiteSession.UserPermissionList as UserPermissionCache;
                else
                {
                    if (this.CurrentUserOrOperator != null)
                    {
                        SiteSession.UserPermissionList = UserProcess.GetUserPermissionCacheById(SiteId,
                            CurrentUserOrOperator.UserOrOperatorId);
                        return SiteSession.UserPermissionList as UserPermissionCache;
                    }
                }
                //return null;
                throw new Exception(Proxy[EnumText.enumForum_Topic_MessagePermissionDenied]);
            }
        }
        public UserForumPermissionItem UserForumPermissionList(int forumId)
        {
            if (!this.UserPermissionCache.UserForumPermissionsList.ContainsKey(forumId))
            {
                //SiteSession.UserPermissionList = null;//reset null
                return new UserForumPermissionItem(this.UserOrOperatorId, forumId, false, false,
                    false, false, int.MaxValue, int.MinValue, false, false, false);
            }
            return this.UserPermissionCache.UserForumPermissionsList[forumId]
                as UserForumPermissionItem;
        }

        public virtual bool IfValidateForumClosed { get { return true; } }
        public virtual bool IfValidateIPBanned { get { return true; } }
        public virtual bool IfValidateUserBanned { get { return true; } }

        protected int _visitingForumId = 0;
        protected int VisitingForumId
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

        private bool _ifValidateCategoryClose;
        public bool IfValidateCategoryClose
        {
            get { return _ifValidateCategoryClose; }
        }



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

        protected ProhibitedWordsSettingWithPermissionCheck _prohibitedWords;
        public string ReplaceProhibitedWords(string content)
        {
            return _prohibitedWords.ReplaceProhibitedWords(content);
        }
        public string HtmlReplaceProhibitedWords(string htmlContent)
        {
            return _prohibitedWords.HrmlReplaceProhibitedWords(htmlContent);
        }

        private SiteSettingWithPermissionCheck _siteSettingBase;
        public SiteSettingWithPermissionCheck SiteSettingBase
        {
            get { return _siteSettingBase; }
        }

        private bool CanVisitCategoriesClosed()
        {
            bool canVisit = false;
            CategoryWithPermissionCheck category = CategoryProcess.GetCategoryByForumId(
                UserOrOperatorId, SiteId, _visitingForumId);
            if (category.Status != EnumCategoryStatus.Close)
                return true;
            if (IfAdmin() || IfModerator(_visitingForumId))
                return true;
            return canVisit;
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

            CheckIfIPBanned();//2.0 Check If IP Banned
            CheckIfNewRegistratioAllowed();
            this.CheckUserOrOperatorBase();
            // Require Login
            if (/*!(this is Com.Comm100.Forum.UI.Login) && !(this is Com.Comm100.Forum.UI.Siteclosed) && */this.UserOrOperatorId <= 0)
            {
                try
                {
                    /*Gavin 2.0*/
                    if (!IfAllowGuestUserViewForum && IfValidateGuestAllowViewForum)
                    {
                        Response.Redirect("~/Login.aspx?siteid=" + this.SiteId, false);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
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
                        Response.Redirect("~/Siteclosed.aspx?siteId=" + SiteId + "&ReturnUrl=" + url, true);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    else if (this.IfValidateCategoryClose && !this.CanVisitCategoriesClosed())
                    {
                        string applicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                        if (applicationPath != "/")
                        {
                            applicationPath = applicationPath + "/";
                        }
                        Response.Redirect("~/ForumIsClosed.aspx?siteId=" + SiteId);
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
                ///**************User Login************/
                //SessionUser currentUser1 = new SessionUser(1, 200000, true, 0, EnumApplicationType.enumForum);
                //currentUser1.IfAdminLogin = true;
                //Session["CurrentUser"] = currentUser1;

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
                                Session.Remove("UserPermissionList");
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
                        Session.Remove("UserPermissionList");
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
                    this.InitSiteStatus();
                    this.InitGuestUserPermissionsSetting();
                    this.InitIfValidateCategoryClose();
                    this.InitIfValidateForumHidden();
                    this.InitIfValidateForumLock();
                    this.InitIfValidateGuestAllowViewForum();
                    this.InitVisitingForumId();
                    this.InitVisitingForumStatus();
                    this.InitTimezoneOffset();
                    this.InitProhibitedWords();
                    this.InitIfModerator();
                    this.InitIfAdmin();
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
            this.InitIfValidateCategoryClose();
            this.InitIfValidateForumHidden();
            this.InitIfValidateForumLock();
            this.InitIfValidateGuestAllowViewForum();
            this.InitVisitingForumId();
            this.InitVisitingForumStatus();
            this.InitTimezoneOffset();
            this.InitIfModerator();
            this.InitIfAdmin();
        }
        private void InitCurrentUserOrOperator()
        {
            if (Session["CurrentUser"] != null)
            {
                this._currentUserOrOperator =SiteSession.CurrentUser as SessionUser;
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
        private void InitProhibitedWords()
        {
            _prohibitedWords = SettingsProcess.GetProhibitedWords(SiteId, UserOrOperatorId);
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

        private void InitIfValidateCategoryClose()
        {
            this._ifValidateCategoryClose = WebUtility.IfValidateCategoryClose();
        }
        #endregion

        #region IfAdmin & IfModerator

        private void InitIfModerator()
        {
            if (this._visitingForumId > 0)
            {
                Moderator[] moderator = ForumProcess.GetModeratorsByForumId(this._visitingForumId, SiteId, UserOrOperatorId, IfOperator);
                for (int i = 0; i < moderator.Length; i++)
                {
                    if (UserOrOperatorId != 0)
                    {
                        if (moderator[i].Id == UserOrOperatorId)
                        {
                            this._ifModerator = true;
                            break;
                        }
                    }
                }
            }
        }

        public bool IfModerator()
        {
            return _ifModerator;
        }

        public bool IfModerator(int forumId)
        {
            bool ifModerator = false;

            Moderator[] moderator = ForumProcess.GetModeratorsByForumId(forumId, SiteId, UserOrOperatorId, IfOperator);
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

        public bool IfModeratorOfAnnoucement(int annoucementId)
        {
            return AnnoucementProcess.IfModeratorOfAnnoucement(annoucementId, SiteId, UserOrOperatorId);
        }

        public bool IfAdmin()
        {
            return _ifAdmin;
            //return this.UserPermissionCache.IfAdministrator;
        }
        private void InitIfAdmin()
        {
            try
            {
                UserOrOperator operatingOperator = UserProcess.GetNotDeletedUserOrOperatorById(SiteId, UserOrOperatorId);
                if (operatingOperator != null)
                {
                    if (operatingOperator is OperatorWithPermissionCheck)
                    {
                        if ((operatingOperator as OperatorWithPermissionCheck).IfAdmin == true)
                            _ifAdmin = true;
                        return;
                    }

                    if (operatingOperator.IfForumAdmin == true)
                    {
                        _ifAdmin = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IfModeratorInSite()
        {
            bool ifModerator = false;
            if (UserOrOperatorId > 0)
            {
                UserOrOperator operatingUserOrOperator = UserProcess.GetNotDeletedUserOrOperatorById(SiteId, UserOrOperatorId);
                if (operatingUserOrOperator != null)
                {
                    ifModerator = UserProcess.IfModerator(SiteId, UserOrOperatorId);
                }
            }
            return ifModerator;
           
        }

        #endregion

        #region Check Function
        private void CheckIfNewRegistratioAllowed()
        {
            try
            {
                if (this is Pre_Register || this is Register)
                {
                    RegistrationSettingWithPermissionCheck registerSetting = SettingsProcess.GetRegistrationSettingBySiteId(
                        UserOrOperatorId, SiteId);
                    if (!registerSetting.IfAllowNewUser)
                    {
#if OPENSOURCE
                        Response.Redirect("Default.aspx", false);
#else
                        Response.Redirect("Default.aspx?siteId=" + SiteId, false);
#endif

                    }
                }
            }
            catch (Exception exp)
            {
                string errText = "<span class=\"ErrorLabel\">" + exp.Message.Replace("'", "\\'") + "</span>";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "BasePageErrorWriter", "document.write('<span style=\"color: red\">Error: " + errText + "</span>');alert('Error: " + exp.Message.Replace("'", "\\'") + "');", true);
                this.IfError = true;
            }
        }
        #endregion
        #region Check User Or Operator

        protected void CheckUserOrOperatorBase()
        {
            try
            {
                if (this._currentUserOrOperator != null)
                {
                    UserOrOperator userOrOperator = UserProcess.GetUserOrOpertorById(SiteId,
                        this._currentUserOrOperator.UserOrOperatorId);
                    if (userOrOperator == null)
                    {
                        ExceptionHelper.ThrowForumOperatingUserOrOperatorCanNotBeNullException();
                    }
                }
            }
            catch (Exception exp)
            {
                Session.Remove("CurrentUser");
                Session.Remove("UserPermissionList");
                //string errText = "<span class=\"ErrorLabel\">" + exp.Message.Replace("'", "\\'") + "</span>";
                string js = "alert('Error: " + exp.Message.Replace("'", "\\'") + "');"
                + "window.location.href ='" + this.UrlWithAuthorityAndApplicationPath + "Login.aspx?siteid=" + this.SiteId + "'";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "BasePageErrorWriter", js, true);
            }

            try
            {
                if (this._currentUserOrOperator != null)
                {
                    UserOrOperator userOrOperator = UserProcess.GetUserOrOpertorById(SiteId,
                        this._currentUserOrOperator.UserOrOperatorId);
                    this.CheckUserOrOperator(userOrOperator);
                    this.CheckIfUserOrOperatorBanned(_visitingForumId);
                }
            }
            catch (Exception exp)
            {
                //Session.Remove("CurrentUser");
                //Session.Remove("UserPermissionList");
                //string errText = "<span class=\"ErrorLabel\">" + exp.Message.Replace("'", "\\'") + "</span>";
                string js = "alert('Error: " + exp.Message.Replace("'", "\\'") + "');";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "BasePageErrorWriter", js, true);
            }
        }



        #region Public Function CheckUserOrOperator
        public void CheckUserOrOperator(UserOrOperator operatingUserOrOperator)
        {            
            if (operatingUserOrOperator.IfDeleted)
            {
                ExceptionHelper.ThrowForumUserOrOperatorNotExistWithNameException(operatingUserOrOperator.DisplayName);
            }
            else if (!operatingUserOrOperator.IfActive)
            {
                ExceptionHelper.ThrowForumUserOrOperatorNotActiveWithNameException(operatingUserOrOperator.DisplayName);
            }
        }
        #endregion

        #region Public Function IfBannedInUI
        /*Jason 2.0 Add Check IP Banned */
        protected void CheckIfIPBanned()
        {
            try
            {
                //if (IfValidateIPBanned)
                //{
                //    string userIp = Request.ServerVariables["remote_addr"];
                //    long ip = IpHelper.DottedIP2LongIP(userIp);
                //    int BannedIPCount = BanProcess.GetCountOfBanByIP(this.UserOrOperatorId, this.SiteId, EnumBanType.IP, ip);
                //    if (BannedIPCount > 0)
                //    {
                //        Response.Redirect("~/IPBanned.aspx", false);
                //    }
                //}
                if(IfIPBanned())
                    Response.Redirect(string.Format("~/IPBanned.aspx?siteId={0}",this.SiteId.ToString()), false);
            }
            catch (Exception exp)
            {
                string errText = "<span class=\"ErrorLabel\">" + exp.Message.Replace("'", "\\'") + "</span>";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "BasePageErrorWriter", "document.write('<span style=\"color: red\">Error: " + errText + "</span>');alert('Error: " + exp.Message.Replace("'", "\\'") + "');", true);
                this.IfError = true;
            }
        }

        protected virtual bool IfIPBanned()
        {
            bool ifIpBanned = false;
            if (IfValidateIPBanned)
            {
                string userIp = Request.ServerVariables["remote_addr"];
                long ip = IpHelper.DottedIP2LongIP(userIp);
                int BannedIPCount = BanProcess.GetCountOfBanByIP(this.UserOrOperatorId, this.SiteId, EnumBanType.IP, ip);
                if (BannedIPCount > 0)
                {
                    ifIpBanned = true;
                }
            }
            return ifIpBanned;
        }
        public bool CheckIfBannedInUI(int forumId)
        {
            return UserProcess.CheckIfBannedInUI(SiteId, this.UserOrOperatorId, forumId);
        }
        public void CheckIfUserOrOperatorBanned(int forumId)
        {
            //if (IfValidateUserBanned)
            //{
            //    if (CheckIfBannedInUI(forumId))
            //        Response.Redirect("~/UserBanned.aspx", false);
            //}
           if( IfUserBanned(forumId))
               Response.Redirect(string.Format("~/UserBanned.aspx?siteId={0}",this.SiteId.ToString()), false);
        }
        protected virtual bool IfUserBanned(int forumId)
        {
            bool ifUserBanned = false;
            if (IfValidateUserBanned)
            {
                if (CheckIfBannedInUI(forumId))
                    ifUserBanned = true;
            }
            return ifUserBanned;
        }
        #endregion Public Function IfBannedInUI
        #endregion
    }
}
