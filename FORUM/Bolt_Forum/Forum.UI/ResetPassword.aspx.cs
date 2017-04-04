
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
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI
{
    public partial class ResetPassword : Com.Comm100.Forum.UI.UIBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string applicationPath = Request.ApplicationPath;

            if (!IfError)
            {
                Response.BufferOutput = true;
                Response.Expires = 0;
                Response.ExpiresAbsolute = DateTime.UtcNow.AddMinutes(-1);
                Response.CacheControl = "no-cache";
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

#if OPENSOURCE
#else
                    CheckQueryString("siteId");
#endif
                    CheckQueryString("email");
                    CheckQueryString("forgetPasswordGuidTag");

                    int siteId = this.SiteId; //Convert.ToInt32(Request.QueryString["siteId"]);
                    string email = Convert.ToString(Request.QueryString["email"]).Trim();
                    string forgetPasswordguidTag = Request.QueryString["forgetPasswordGuidTag"].Trim();

                    if (!LoginAndRegisterProcess.IfForgetPasswordGuidTagMatch(siteId, email, forgetPasswordguidTag))
                    {
                        this.tblEdit.Visible = false;
                        this.tblError.Visible = true;
                    }

                    SiteSetting siteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                    Master.Page.Title = string.Format(Proxy[EnumText.enumForum_User_BrowerTitleResetPassword], System.Web.HttpUtility.HtmlEncode(siteSetting.SiteName));
                    ComparePassword.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorRetypePsswordMatch];
                    RequiredTxtRetypePassword.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorRetypePasswordRequired];
                    btnResetPassword.Text = Proxy[EnumText.enumForum_User_ButtonResetPassword];
                    RequiredTxtPassword.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorNewPasswordRequired];
                    txtEmail.Text = email;
                }
                catch (Exception exp)
                {
                    lblMessage.Text = exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    IfError = true;
                }
            }
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            //string applicationPath = Request.ApplicationPath;
            //string path = "";

            if (!IfError)
            {
                try
                {
#if OPENSOURCE
#else
                    CheckQueryString("siteId");
#endif
                    CheckQueryString("email");

                    int siteId = this.SiteId; //Convert.ToInt32(Request.QueryString["siteId"]);
                    string email = Request.QueryString["email"];

                    string password = txtPassword.Text.ToString();

                    LoginAndRegisterProcess.ResetPasswordForFindPassword(siteId, email, password);

                    //if (applicationPath == "/")
                    //{
                    //    path = applicationPath;
                    //}
                    //else
                    //{
                    //    path = applicationPath + "/";
                    //}

                    //string script = string.Format("<script>alert('Reset password succeed.'); window.location.href = '{0}Login.aspx'</script>", path);

                    string script = string.Format("<script>alert('" + Proxy[EnumText.enumForum_User_AlertResetPasswordSucceed] + "'); window.location.href = 'Login.aspx?siteid=" + this.SiteId + "';</script>");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
                }
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_User_PageResetPasswordErrorReset] + exp.Message;
                    IfError = true;
                    LogHelper.WriteExceptionLog(exp);

                    string script = string.Format("<script>alert(\"{0}\")</script>", exp.Message);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
                }
            }
        }
    }
}
