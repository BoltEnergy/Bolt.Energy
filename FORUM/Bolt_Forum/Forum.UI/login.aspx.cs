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
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Com.Comm100.Forum.UI
{
    public partial class login : Com.Comm100.Forum.UI.UIBasePage
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

        protected override void InitLanguage()
        {
            try
            {
                SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                Page.Title = string.Format(Proxy[EnumText.enumForum_Login_PageTitle], Proxy[EnumText.enumForum_login_TitleUser], System.Web.HttpUtility.HtmlEncode(siteSetting.SiteName));

                requiredFieldValidatorEmail.ErrorMessage = this.Proxy[EnumText.enumForum_Login_ErrorEmailRequired];
                regularExpressionValidatorEmail.ErrorMessage = this.Proxy[EnumText.enumForum_Login_EmailFormat];
                requiredFieldValidatorPassword.ErrorMessage = this.Proxy[EnumText.enumForum_Login_ErrorPasswordRequired];
                // requiredFieldValidatorCode.ErrorMessage = this.Proxy[EnumText.enumForum_Login_ErrorVerificationCodeRequired];
                // customValidatorCode.ErrorMessage = this.Proxy[EnumText.enumForum_Login_ErrorVerification];

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
                /*
                txtVerification.MaxLength = 4;

                HyperLink1.NavigateUrl = "~/FindPassword.aspx?siteId=" + this.SiteId.ToString();
                //HyperLink2.NavigateUrl = "~/Pre_Register.aspx?siteid=" + this.SiteId.ToString();
                HyperLink2.NavigateUrl = "~/Register.aspx?siteid=" + this.SiteId.ToString();
                */

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
                                /*update on 2/3/2017 by techtier for redirecting to user in form page direct*/
                                //Response.Redirect("~/Default.aspx?siteid=" + this.SiteId, false);
                                Response.Redirect("~/default.aspx", false);
                            }
                        }
                        if (Request.Cookies["RememberMe"] != null)
                        {
                            txtUserName.Text = Request.Cookies["RememberMe"].Value;
                            //CheckBox1.Checked = true;
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
                            /*update on 2/3/2017 by techtier for redirecting to user in form page direct*/
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

        protected void btnLogin1_Click(object sender, EventArgs e)
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

                    if (string.IsNullOrEmpty(sessionUser.UserName))
                        sessionUser.UserName = txtUserName.Text;

                    SiteSession.CurrentUser = sessionUser;
                    SiteSession.UserPermissionList = userPermissionList;

                    this.CurrentUserOrOperator = sessionUser;
                    //if (CheckBox1.Checked)
                    //{
                    //    HttpCookie cookie = new HttpCookie("RememberMe", txtUserName.Text);
                    //    cookie.Expires = DateTime.Now.AddMonths(12);
                    //    Response.Cookies.Add(cookie);
                    //}
                    //else
                    //{
                    //    HttpCookie cookie = Response.Cookies["RememberMe"];
                    //    cookie.Expires = DateTime.Now.AddMonths(-12);
                    //    Response.Cookies.Add(cookie);
                    //}
                    this.CheckIfUserOrOperatorBanned(0);
                    // tableLogin.Visible = false;
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

                    //-------Done by techtier for after successfull login home page link will redirection forum page instead of default-------// 
                    //string script = string.Format(@"<script>setTimeout(""window.location = '{0}'"",5000)</script>", ResolveUrl(url));/*5000*/
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
                    //lnkBack.NavigateUrl = url;
                    Response.Redirect("~/default.aspx", false);

                    //lnkHome.NavigateUrl = "~/Default.aspx?siteid=" + this.SiteId;/*older code*/
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
                //if (txtVerification.Text != Session["RegisterVerificationCode"].ToString())
                //{
                //    args.IsValid = false;
                //}
                //else
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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string jsonResponse = string.Empty;
                try
                {
                    #region MyRegion
                    /*
                    
		            string InputURL = string.Format(WebUtility.GetAppSetting(Constants.WK_CheckLogin), txtUserName.Text, txtPassword.Text, "");
                    var client = new RestClient();
                    client.EndPoint = InputURL;
                    client.Method = HttpVerb.GET;
                    client.PostData = "";
                    jsonResponse = client.MakeRequest();

                  
                   if (!string.IsNullOrEmpty(jsonResponse) && jsonResponse.Split('|').Length == 3)
                   {                        string[] arr = jsonResponse.Split('|');
                       if (arr[0] == "200")
                       {
                           string userIp = "";
                               try
                               {
                                   userIp = Request.ServerVariables[Constants.Remote_address];
                               }
                               catch (Exception ex)
                               {

                               }

                           LoginUserBE user = new LoginUserBE
                           {
                               Email = txtUserName.Text,
                               FirstName = arr[1],
                               LastName = arr[2],
                               IpAddress = userIp
                           };

                           SiteSession.DirectLoginUser = user;

                           directlogin objDirectLogin = new directlogin();
                           objDirectLogin.DirectUserlogin();

                           new directlogin().AddUpdateUserInfoInDB(user);

                       }
                   } 

                     */

                    #endregion

                    UserBE currUser = new MongoCon().Validateuser(txtUserName.Text, txtPassword.Text);

                    if (currUser != null)
                    {
                        string userIp = "";
                        try
                        {
                            userIp = Request.ServerVariables[Constants.Remote_address];
                        }
                        catch (Exception ex)
                        {

                        }

                        LoginUserBE user = new LoginUserBE
                        {
                            Email = txtUserName.Text,
                            FirstName = currUser.firstName,
                            LastName = currUser.lastName,
                            IpAddress = userIp
                        };

                        SiteSession.DirectLoginUser = user;
                       
                        DirectUserlogin();

                       
                    }
                    else
                        lblMessage.Text = "Your email or password is incorrect.";
                }
                catch (Exception ex)
                {
                    jsonResponse = ex.Message;
                }
            }
        }

        private Int32 AddUpdateUserInfoInDB(LoginUserBE user)
        {
            UserDAL objuser = new UserDAL();
            int userid = objuser.AddUpdateUserInfo(user);
            if (userid == 0)
            {
                string userIp = user.IpAddress;
                if (userIp == "")
                {
                    try
                    {
                        userIp = Request.ServerVariables[Constants.Remote_address];
                    }
                    catch (Exception ex)
                    {

                    }
                }

                RegistrationSettingWithPermissionCheck registrationSetting = SettingsProcess.GetRegistrationSettingBySiteId(UserOrOperatorId, SiteId);

                bool moderateStatus = registrationSetting.IfModerateNewUser;
                bool verifyEmail = registrationSetting.IfVerifyEmail;

                userid = LoginAndRegisterProcess.UserRegister(0, user.Email, user.FirstName + " " + user.LastName, WebUtility.GetAppSetting(Constants.WK_UserPassword), userIp, moderateStatus, verifyEmail);
            }

            return userid;
        }

        private void DirectUserlogin()
        {
            try
            {
                string useremail = SiteSession.DirectLoginUser.Email;
                string loginIp = SiteSession.DirectLoginUser.IpAddress;

                if (loginIp == "")
                {
                    try
                    {
                        loginIp = Request.ServerVariables[Constants.Remote_address];
                    }
                    catch { }
                }


                string strTimezoneOffset = CommonFunctions.ReadCookies("TimezoneOffset");
                double timezoneOffset = strTimezoneOffset.Length == 0 ? 0 : Convert.ToDouble(strTimezoneOffset);

                AddUpdateUserInfoInDB(SiteSession.DirectLoginUser);

                UserPermissionCache userPermissionList;
                SessionUser sessionUser = LoginAndRegisterProcess.UserOrOperatorLoginDirect(
                    this.SiteId, useremail, "", loginIp, timezoneOffset, false, false, out userPermissionList);

                if (string.IsNullOrEmpty(sessionUser.UserName))
                    sessionUser.UserName = useremail;

                SiteSession.CurrentUser = sessionUser;
                SiteSession.UserPermissionList = userPermissionList;
                this.CurrentUserOrOperator = sessionUser;
                try
                {
                    this.CheckIfUserOrOperatorBanned(0);
                }
                catch { }

                string url = "";
                if (Request.QueryString["ReturnUrl"] != null)
                {
                    //Response.Redirect(Request.QueryString["ReturnUrl"], false);
                    url = Request.QueryString["ReturnUrl"];
                }
                else
                {
                    //Response.Redirect(ViewState["UrlReferrer"].ToString(), false);
                    url = Convert.ToString(ViewState["UrlReferrer"]);
                }
                url = url.ToLower();
                if (!Com.Comm100.Forum.UI.Common.WebUtility.CanReturnPreUrl(url))
                {
                    url = this.UrlWithAuthorityAndApplicationPath + "Default.aspx?siteid=" + this.SiteId;
                }

                SiteSession.Remove(Constants.SS_DirectLoginUser);
                if (string.IsNullOrEmpty(url))
                    Response.Redirect("mytopics.aspx", false);
                else
                    Response.Redirect(url, false);
            }
            catch (Exception)
            {
                Response.Redirect(WebUtility.GetAppSetting(Constants.WK_BaseURL));
            }
        }

        private void ConnectDB()
        {
            ForumTopicDAL obj = new ForumTopicDAL();

            DataTable dt = obj.GetAllUser();
        }

    }
}
