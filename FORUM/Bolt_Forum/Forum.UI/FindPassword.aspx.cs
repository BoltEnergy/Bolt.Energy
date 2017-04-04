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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI
{
    public partial class FindPassword : Com.Comm100.Forum.UI.UIBasePage
    {
        
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IfError)
            {
                this.RegularExpressionTxtEmailToReset.ErrorMessage = Proxy[EnumText.enumForum_Login_PageFindPasswordErrorEmailFormat];
                this.RequiredFieldTxtEmailToReset.ErrorMessage = Proxy[EnumText.enumForum_Login_PageFindPasswordErrorEmailRequired];

                try
                {
                    
                    SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                    Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Login_BrowerTitleFindPassword], System.Web.HttpUtility.HtmlEncode(siteSetting.SiteName));
                    this.btnToSendEmail.Text = Proxy[EnumText.enumForum_Login_ButtonSend];

                    this.Form.DefaultButton = btnToSendEmail.UniqueID;

                    this.txtEmailToResetPassword.Attributes.Add("onchange", "javascript:document.getElementById('"+this.lblErrorEmail.ClientID+"').innerText = '';");
                    //this.RegularExpressionTxtEmailToReset.ErrorMessage = this.Proxy[EnumText.enumForum_Login_PageFindPasswordErrorEmailFormat];
                 }
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_Login_PageFindPasswordErrorLoading] + exp.Message;
                    IfError = true;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
        }

        protected void btnToSendEmail_Click(object sender, EventArgs e)
        {
            if (!IfError)
            {
                try
                {
                    int siteId = this.SiteId;

                    string email = txtEmailToResetPassword.Text.ToString();

                    if (!LoginAndRegisterProcess.IfExistEmail(email, siteId))
                    {
                        lblErrorEmail.Text = Proxy[EnumText.enumForum_Login_PageFindPasswordErrorUnregisteredEmail];
                    }
                    else if (LoginAndRegisterProcess.IfVerified(siteId, email))
                    {
                        LoginAndRegisterProcess.SendResetPasswordEmail(siteId, email);

                        Response.Redirect("~/SendResetPasswordEmail.aspx?siteId=" + SiteId, false);
                    }
                    else
                    {
                        lblErrorEmail.Text = Proxy[EnumText.enumForum_Login_PageFindPasswordErrorEmailNotVerified];
                    }
                }
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_Login_PageFindPasswordErrorFindingPassword] + exp.Message;
                    IfError = true;
                    LogHelper.WriteExceptionLog(exp);

                    string script = string.Format("<script>alert(\"{0}\")</script>", exp.Message);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
                }
            }
        }

    }
}
