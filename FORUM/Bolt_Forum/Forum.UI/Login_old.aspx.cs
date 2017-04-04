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
using Com.Comm100.Forum.UI.Common;
using System.Data;

namespace Com.Comm100.Forum.UI
{
    public partial class Login_old : Com.Comm100.Forum.UI.UIBasePage
    {
        private string errTimeout;
        private string errLoad;
        private string errLogin;

        public override bool IfValidateForumClosed
        {
            get
            {
                if (Request.QueryString["action"] != null && Request.QueryString["action"].ToLower() == "logout")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        protected override void InitLanguage()
        {
            try
            {
                SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Login_PageTitle], Proxy[EnumText.enumForum_login_TitleUser], System.Web.HttpUtility.HtmlEncode(siteSetting.SiteName));

                requiredFieldValidatorEmail.ErrorMessage = this.Proxy[EnumText.enumForum_Login_ErrorEmailRequired];
                regularExpressionValidatorEmail.ErrorMessage = this.Proxy[EnumText.enumForum_Login_EmailFormat];
                requiredFieldValidatorPassword.ErrorMessage = this.Proxy[EnumText.enumForum_Login_ErrorPasswordRequired];
                // requiredFieldValidatorCode.ErrorMessage = this.Proxy[EnumText.enumForum_Login_ErrorVerificationCodeRequired];
                // customValidatorCode.ErrorMessage = this.Proxy[EnumText.enumForum_Login_ErrorVerification];

                lnkBack.Text = this.Proxy[EnumText.enumForum_Login_LinkBackPage];
                lnkHome.Text = this.Proxy[EnumText.enumForum_Login_LinkHomePage];
                lnkUserPanel.Text = this.Proxy[EnumText.enumForum_Login_LinkUserControlPanel];
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
                txtUserName.MaxLength = ForumDBFieldLength.User_emailFieldLength;
                txtPassword.MaxLength = ForumDBFieldLength.User_passwordFieldLength;
                txtVerification.MaxLength = 4;

                HyperLink1.NavigateUrl = "~/FindPassword.aspx?siteId=" + this.SiteId.ToString();
                /*Updated on 7/3/2017 by surinder sor redirecting the user direct to register page*/
                //HyperLink2.NavigateUrl = "~/Pre_Register.aspx?siteid=" + this.SiteId.ToString();
                HyperLink2.NavigateUrl = "~/Register.aspx?siteid=" + this.SiteId.ToString();

                string currentUrl1 = "";
                if (Request.QueryString["ReturnUrl"] != null)
                    currentUrl1 = System.Web.HttpUtility.UrlDecode(Request.QueryString["ReturnUrl"]);



                if (Request.QueryString["action"] == null || Request.QueryString["action"].ToLower() == "login")
                {
                    if (!IsPostBack)
                    {
                        if (this.CurrentUserOrOperator != null)
                        {
                            if (Request.QueryString["ReturnUrl"] != null)
                            {
                                Response.Redirect(Request.QueryString["ReturnUrl"], false);
                            }
                            else
                            {
                                /*update on 2/3/2017 by surinder for redirecting to user in form page direct*/
                                //Response.Redirect("~/Default.aspx?siteid=" + this.SiteId, false);
                                Response.Redirect("~/default.aspx", false);
                            }
                        }
                        if (Request.Cookies["RememberMe"] != null)
                        {
                            txtUserName.Text = Request.Cookies["RememberMe"].Value;
                            CheckBox1.Checked = true;
                        }
                        if (Request.QueryString["Timeout"] != null)
                        {
                            lblMessage.Text = errTimeout;
                        }
                        if (Request.UrlReferrer != null)
                        {
                            ViewState["UrlReferrer"] = Request.UrlReferrer.OriginalString;
                        }
                        else
                        {
                            /*update on 2/3/2017 by surinder for redirecting to user in form page direct*/
                            //ViewState["UrlReferrer"] = "~/Default.aspx?siteid=" + this.SiteId;
                            ViewState["UrlReferrer"] = "~/default.aspx";
                        }
                    }
                }
                else if (Request.QueryString["action"].ToLower() == "logout")
                {
                    Session.Remove("CurrentUser");
                    Session.Remove("UserPermissionList");
                    //Session.Abandon();
                    string userBannedPageUrl = "userbanned.aspx";
                    string ipBannedPageUrl = "ipbanned.aspx";
                    string emailverificationPageUrl = "emailverification.aspx";
                    if (Request.UrlReferrer != null && !Request.UrlReferrer.ToString().ToLower().Contains(emailverificationPageUrl) && !Request.UrlReferrer.ToString().ToLower().Contains(userBannedPageUrl) && !Request.UrlReferrer.ToString().ToLower().Contains(ipBannedPageUrl))
                    {
                        string[] temp = Request.UrlReferrer.OriginalString.Split('#');
                        Response.Redirect(temp[0], false);
                    }
                    else
                    {
                        Response.Redirect("~/Default.aspx?siteid=" + this.SiteId, false);
                    }
                }
#if OPENSOURCE
#else
                if (Request.ServerVariables["SERVER_PORT"] == "80")
                {
                    string qstring;
                    string httpsurl;
                    qstring = Request.Url.AbsoluteUri;
                    httpsurl = qstring.Replace("http:", "https:");
                    Response.Redirect(httpsurl, false);
                }

                string httpsScript = "<script language=\"javascript\" type=\"text/javascript\">replaceHttps2Http();</script>";
                this.phHttps.Controls.Add(new LiteralControl(httpsScript));

#endif
            }
            catch (Exception exp)
            {
                lblMessage.Text = errLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }

            if (!this.IsPostBack)
            {
                this.txtUserName.Attributes.Add("onkeyup", "if(event.keyCode==13) changeRememberEmailFormatVerify(this,'" + this.requiredFieldValidatorEmail.ClientID + "','" + this.regularExpressionValidatorEmail.ClientID + "');");
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
                    UserPermissionCache userPermissionList;
                    SessionUser sessionUser = LoginAndRegisterProcess.UserOrOperatorLogin(
                        this.SiteId, txtUserName.Text, txtPassword.Text, loginIp, timezoneOffset, false, false, out userPermissionList);

                    if(string.IsNullOrEmpty(sessionUser.UserName))
                        sessionUser.UserName = txtUserName.Text;

                    SiteSession.CurrentUser = sessionUser;
                    SiteSession.UserPermissionList = userPermissionList;

                    this.CurrentUserOrOperator = sessionUser;
                    if (CheckBox1.Checked)
                    {
                        HttpCookie cookie = new HttpCookie("RememberMe", txtUserName.Text);
                        cookie.Expires = DateTime.Now.AddMonths(12);
                        Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        HttpCookie cookie = Response.Cookies["RememberMe"];
                        cookie.Expires = DateTime.Now.AddMonths(-12);
                        Response.Cookies.Add(cookie);
                    }
                    this.CheckIfUserOrOperatorBanned(0);
                    tableLogin.Visible = false;
                    tableInfo.Visible = true;
                    string url = "";
                    if (Request.QueryString["ReturnUrl"] != null)
                    {
                        //Response.Redirect(Request.QueryString["ReturnUrl"], false);
                        url = Request.QueryString["ReturnUrl"];
                    }
                    else
                    {
                        //Response.Redirect(ViewState["UrlReferrer"].ToString(), false);
                        url = ViewState["UrlReferrer"].ToString();
                    }
                    url = url.ToLower();
                    if (!Com.Comm100.Forum.UI.Common.WebUtility.CanReturnPreUrl(url))
                    {
                        url = this.UrlWithAuthorityAndApplicationPath + "Default.aspx?siteid=" + this.SiteId;
                    }

                    //-------Done by surinder for after successfull login home page link will redirection forum page instead of default-------// 
                    //string script = string.Format(@"<script>setTimeout(""window.location = '{0}'"",5000)</script>", ResolveUrl(url));/*5000*/
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
                    //lnkBack.NavigateUrl = url;
                    Response.Redirect("~/default.aspx", false);

                    //lnkHome.NavigateUrl = "~/Default.aspx?siteid=" + this.SiteId;/*older code*/
                    lnkHome.NavigateUrl = "~/default.aspx";
                    lnkUserPanel.NavigateUrl = "~/UserPanel/UserProfileEdit.aspx?siteid=" + this.SiteId;

                }
                catch (Exception exp)
                {
                    lblMessage.Text = errLogin + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
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

            args.IsValid = true;
        }

        

        private void ConnectDB()
        {
            ForumTopicDAL obj = new ForumTopicDAL();

            DataTable dt = obj.GetAllUser();
        }

    }
}
