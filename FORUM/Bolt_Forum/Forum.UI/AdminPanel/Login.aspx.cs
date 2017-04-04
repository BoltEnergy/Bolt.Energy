
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
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Common;
using System.Web.Configuration;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel
{
    public partial class Login : Com.Comm100.Forum.UI.UIBasePage
    {
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
        public override bool IfValidateUserBanned
        {
            get
            {
                return false;
            }
        }


        private string errTimeout;
        private string errLoad;
        private string errLogin;

        protected override void InitLanguage()
        {
            try
            {
                SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Login_PageTitle], Proxy[EnumText.enumForum_login_TitleAdminLogin], System.Web.HttpUtility.HtmlEncode(siteSetting.SiteName));

                requiredFieldValidatorEmail.ErrorMessage = this.Proxy[EnumText.enumForum_Login_ErrorEmailRequired];
                regularExpressionValidatorEmail.ErrorMessage = this.Proxy[EnumText.enumForum_Login_EmailFormat];
                requiredFieldValidatorPassword.ErrorMessage = this.Proxy[EnumText.enumForum_Login_ErrorPasswordRequired];
                requiredFieldValidatorCode.ErrorMessage = this.Proxy[EnumText.enumForum_Login_ErrorVerificationCodeRequired];
                customValidatorCode.ErrorMessage = this.Proxy[EnumText.enumForum_Login_ErrorVerification];

                btnLogin.Text = this.Proxy[EnumText.enumForum_Login_ButtonLogin];

                errTimeout = this.Proxy[EnumText.enumForum_Login_SessionOut];
                errLoad = this.Proxy[EnumText.enumForum_Login_PageLoginErrorLogin];
                errLogin = this.Proxy[EnumText.enumForum_Login_PageLoginErrorLoginSite];
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = ex.Message;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (this.IfError) return;
            try
            {
#if OPENSOURCE
#else
                if (Request.ServerVariables["SERVER_PORT"] == "80")
                {
                    string qstring;
                    string httpsurl;
                    qstring = Request.Url.AbsoluteUri;
                    httpsurl = qstring.Replace("http", "https");
                    Response.Redirect(httpsurl, false);
                }

                string httpsScript = "<script language=\"javascript\" type=\"text/javascript\">replaceHttps2Http();</script>";
                this.phHttps.Controls.Add(new LiteralControl(httpsScript));
#endif
                txtUserName.MaxLength = ForumDBFieldLength.User_emailFieldLength;
                txtPassword.MaxLength = ForumDBFieldLength.User_passwordFieldLength;
                txtVerification.MaxLength = 4;

                HyperLink1.NavigateUrl = "~/FindPassword.aspx?siteId=" + this.SiteId.ToString();
                
                if (Request.QueryString["action"] == null || Request.QueryString["action"].ToLower() == "login")
                {
                    if (!IsPostBack)
                    {
                        UserOrOperator user = UserProcess.GetNotDeletedUserOrOperatorById(this.SiteId, this.UserOrOperatorId);
                        if (user != null)
                        {
                            txtUserName.Text = user.Email;
                            txtUserName.ReadOnly = true;
                        }
                    }
                    if (Request.QueryString["Timeout"] != null)
                    {
                        lblMessage.Text = errTimeout;
                    }
                    if (Request.UrlReferrer != null)
                    {
                        ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        ViewState["UrlReferrer"] = "~/Default.aspx?siteid=" + this.SiteId;
                    }
                }
                else if (Request.QueryString["action"].ToLower() == "logout")
                {
                    Session.Remove("CurrentUser");
                    Session.Remove("UserPermissionList");
                    Response.Redirect(Request.UrlReferrer.ToString(), false);                    
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = errLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (this.IfError) return;
                try
                {
                    string loginIp = Request.ServerVariables["remote_addr"];
                    string strTimezoneOffset = CommonFunctions.ReadCookies("TimezoneOffset");
                    double timezoneOffset = strTimezoneOffset.Length == 0 ? 0 : Convert.ToDouble(strTimezoneOffset);
                    /*2.0 add user permission list*/
                    UserPermissionCache userPermissionList;
                    //credentials check
                    SessionUser sessionUser = LoginAndRegisterProcess.UserOrOperatorLogin(
                        this.SiteId, txtUserName.Text, txtPassword.Text, loginIp, timezoneOffset,true,false,out userPermissionList);
                    if (sessionUser.IfOperator)
                    {
                        sessionUser.IfAdminLogin = true;
                    }
                    else
                    {
                        sessionUser.IfForumAdministratorLogin = true;
                    }
                    //assigning the session user after checking the credentials
                    if (CurrentUserOrOperator != null)
                        if (CurrentUserOrOperator.IfModeratorLogin == true)
                            sessionUser.IfModeratorLogin = true;
                   SiteSession.CurrentUser = sessionUser;

                    string currentUrl = this.Request.Url.ToString().Replace("https:", "http:");
                    string targetUrl = "";

                    if (Request.QueryString["ReturnUrl"] == null)
                    {
                        targetUrl = "Dashboard.aspx?siteid=" + SiteId;
                    }
                    else
                    {
                        targetUrl = Request.QueryString["ReturnUrl"];
                        if (!Com.Comm100.Forum.UI.Common.WebUtility.CanReturnPreUrl(targetUrl))
                        {
                            targetUrl = "Dashboard.aspx?siteid=" + SiteId;
                        }
                    }

                    Uri uri = new Uri(new Uri(currentUrl), targetUrl);

                    Response.Redirect(uri.ToString(), false);
                   
                }
                catch (System.Threading.ThreadAbortException)
                {
                }
                catch (Exception exp)
                {
                    lblMessage.Text = errLogin + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    string script = string.Format("<script>alert(\"{0}\")</script>", exp.Message);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
                    //this.IfError = true;
                }
            }
        }

        protected void customValidatorCode_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Session["RegisterVerificationCode"] != null)
            {
                if (txtVerification.Text != Session["RegisterVerificationCode"].ToString())
                {
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
            }
            else
            {
                args.IsValid = false;
            }
        }
    }
}
