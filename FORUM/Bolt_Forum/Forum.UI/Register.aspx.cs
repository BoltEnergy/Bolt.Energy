
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
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Forum.Language;
using System.Text.RegularExpressions;
using Com.Comm100.Forum.UI;

namespace Forum.UI
{
    public partial class Register : Com.Comm100.Forum.UI.UIBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
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
            this.btnRegister.Focus();
            Response.BufferOutput = true;
            Response.Expires = 0;
            Response.ExpiresAbsolute = DateTime.UtcNow.AddMinutes(-1);
            Response.CacheControl = "no-cache";
            this.btnTestExistEmail.Focus();
            this.btnTestExistEmail.Attributes.Add("onblur", "javascript:document.getElementById('" + this.lblTestResult.ClientID + "').innerHTML = '';");
            if (!this.IsPostBack)
            {
                //if (this.PreviousPage == null)
                //{
                //    //Response.Redirect("Register.aspx?siteid=" + SiteId, false);
                //}

                //if (Request.UrlReferrer != null && (Request.UrlReferrer.ToString().ToLower().IndexOf("pre_register.aspx") < 0 &&  Request.UrlReferrer.ToString().ToLower().IndexOf("register.aspx") < 0))
                //{
                //    //Response.Redirect(this.UrlWithAuthorityAndApplicationPath + "Pre_Register.aspx?siteid=" + SiteId.ToString(), false);
                //}
               
                string siteLocal = Request.Url.Authority.ToString();
                btnRegister.Text = Proxy[EnumText.enumForum_Register_ButtonCompleteRegister];
                
                RegularExpressionTxtEmail.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorMailFormat];
                RequiredTxtEmail.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorEmailRequired];
                RequiredTxtConfirmEmail.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorRetypeEmailRequired];
                CompareEmail.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorMatchMails];
                RequiredTxtDisplayName.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorDisplayNameRequired];
                RequiredTxtPassword.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorPasswordRequired];
                RequiredTxtConfirmPassword.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorRetypePasswordRequired];
                ComparePassword.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorMatchPasswords];
                RequiredtxtImageVerification.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorVerificationCodeRequired];
                btnTestExistEmail.Text = Proxy[EnumText.enumForum_Register_ButtonEmailVerification];

                this.txtEmail.Attributes.Add("onkeyup", "if(event.keyCode==13) changeRememberEmailFormatVerify(this,'" + this.RequiredTxtEmail .ClientID + "','" + this.RegularExpressionTxtEmail.ClientID + "');");
                this.txtConfirmEmail.Attributes.Add("onkeyup", "if(event.keyCode==13) changeRememberEmailCompireVerify(this,'" + this.txtEmail.ClientID + "','" + this.RequiredTxtConfirmEmail.ClientID + "','" + this.CompareEmail.ClientID + "');");

                this.txtConfirmEmail.MaxLength = ForumDBFieldLength.User_emailFieldLength;
                this.txtConfirmPassword.MaxLength = ForumDBFieldLength.User_passwordFieldLength;
                //this.txtDisplayName.MaxLength = ForumDBFieldLength.User_nameFieldLength;
                this.txtEmail.MaxLength = ForumDBFieldLength.User_emailFieldLength;
                this.txtPassword.MaxLength = ForumDBFieldLength.User_passwordFieldLength;
                
                /*2.0*/
                RegistrationSetting registerSetting = SettingsProcess.GetRegistrationSettingBySiteId(UserOrOperatorId,SiteId);
                SetDisplayNameMinMax(registerSetting);
               
            }

            if (!IfError)
            {
                try
                {
                    SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                    Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Register_PageTitlePreRegister], System.Web.HttpUtility.HtmlEncode(siteSetting.SiteName));
                }
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_Register_PageRegisterErrorLoad] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);

                    IfError = true;
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string applicationPath = Request.ApplicationPath;

            string urlReferrer = "";
            try
            {
                CheckQueryString("UrlReferrer");
                urlReferrer = Request.QueryString["UrlReferrer"];
            }
            catch (Exception)
            {
                urlReferrer = "";
            }

            if (!IfError)
            {
                try
                {
                    if (Session["RegisterVerificationCode"] != null)
                    {
                        int siteId = SiteId;

                        string email = txtEmail.Text;
                        string name = txtDisplayName.Text;

                        ///
                        ///The encoding to password and ip is in process layer
                        ///
                        string password = txtPassword.Text;
                        string userIp = Request.ServerVariables["remote_addr"];

                        string verificationCode = Session["RegisterVerificationCode"].ToString();
                        string imageVerification = txtImageVerification.Text.Trim();

                        int userId;

                        ///
                        ///The argument "operatorId" is 0.
                        ///Set ifOperater with false.
                        ///
                        RegistrationSettingWithPermissionCheck registrationSetting = SettingsProcess.GetRegistrationSettingBySiteId(UserOrOperatorId, SiteId);

                        bool moderateStatus = registrationSetting.IfModerateNewUser;
                        bool verifyEmail = registrationSetting.IfVerifyEmail;

                        if (verificationCode == imageVerification)
                        {
                            ///
                            ///moderateStatus and verifyEmailStatus is keep consistent between URL and UserRegister.
                            ///
                            userId = LoginAndRegisterProcess.UserRegister(siteId, email, name, password, userIp, moderateStatus, verifyEmail);

                            if (verifyEmail == true)
                            {
                                string orignPassword = this.txtPassword.Text;
                                LoginAndRegisterProcess.SendVerificationEmail(siteId, email, orignPassword);
                            }

                            if (verifyEmail == false && moderateStatus == false)
                            {
                                string cookieTimezoneOffset = CommonFunctions.ReadCookies("TimezoneOffset");
                                double timezoneOffset = cookieTimezoneOffset == null || cookieTimezoneOffset == "" ? 0 : Convert.ToDouble(cookieTimezoneOffset);
                                UserPermissionCache userPermissionList;
                                SessionUser user = LoginAndRegisterProcess.UserOrOperatorLogin(siteId, email, password, userIp, timezoneOffset,false,false,out userPermissionList);

                               SiteSession.CurrentUser = user;
                                SiteSession.UserPermissionList = userPermissionList;
                            }

                            ///
                            ///Need to Check with the applicationPath
                            ///

                            string currentUrl = this.Request.Url.ToString().Replace("https:", "http:");

                            string targetUrl = this.UrlWithAuthorityAndApplicationPath + "Post_Register.aspx"
                                    + "?UrlReferrer=" + System.Web.HttpUtility.UrlEncode(urlReferrer)
                                    + "&IfModerateNewUser=" + System.Web.HttpUtility.UrlEncode(Convert.ToInt16(moderateStatus).ToString())
                                    + "&IfVerifyEmail=" + System.Web.HttpUtility.UrlEncode(Convert.ToInt16(verifyEmail).ToString())
                                    + "&step=3"
                                    + "&siteid=" + siteId.ToString();

                            Response.Write("<script language='javascript'>window.location='" + targetUrl + "';</script>");
                            Response.End();

                        }
                        else
                        {
                            lblVerificationCode.Text = Proxy[EnumText.enumForum_Register_ErrorVerificationCode];
                        }
                    }
                    else
                    {
                        lblVerificationCode.Text = Proxy[EnumText.enumForum_Register_ErrorVerificationCode];
                    }
                }
                catch (System.Threading.ThreadAbortException)
                {
                }
                catch (Exception exp)
                {
                    ///
                    ///The exception message may be wrong.
                    ///
                    lblMessage.Text = Proxy[EnumText.enumForum_Register_PageRegisterErrorRegister] + exp.Message;
                    IfError = true;
                    LogHelper.WriteExceptionLog(exp);

                    string script = string.Format("<script>alert(\"" + Proxy[EnumText.enumForum_Register_PageRegisterErrorRegister] + "{0}\")</script>", exp.Message);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
                }
            }
        }

        protected void btnTestExistEmail_Click(object sender, EventArgs e)
        {
            string emailValue = this.txtEmail.Text;
            bool ifEmailExist = LoginAndRegisterProcess.IfExistEmail(emailValue, this.SiteId);

            if (ifEmailExist)
            {
                this.lblTestResult.CssClass = "errorMsg";
                this.lblTestResult.Text = emailValue + Proxy[EnumText.enumForum_Register_ErrorEmailUsed];
            }
            else
            {
                this.lblTestResult.CssClass = "successMsg";
                this.lblTestResult.Text = emailValue + Proxy[EnumText.enumForum_Register_ErrorEmailNotUsed];
            }
        }

        protected void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        /*2.0 */
        private void SetDisplayNameMinMax(RegistrationSetting registerSetting)
        {
            if (string.IsNullOrEmpty(registerSetting.DisplayNameRegularExpression))
                this.revtxtDisplayName.Visible = false;
            else
            {
                try
                {
                    this.revtxtDisplayName.ValidationExpression = registerSetting.DisplayNameRegularExpression;
                    this.revtxtDisplayName.ErrorMessage = registerSetting.DisplayNameInstruction;
                }
                catch 
                { 
                    this.revtxtDisplayName.ValidationExpression = ""; 
                    this.revtxtDisplayName.Visible = false;
                }
            }
            this.txtDisplayName.MaxLength = registerSetting.DisplayNameMaxLength;
        }
    }
}
