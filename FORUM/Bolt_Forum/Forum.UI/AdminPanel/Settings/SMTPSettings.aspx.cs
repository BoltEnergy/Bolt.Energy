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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Language;
namespace Com.Comm100.Forum.UI.AdminPanel.Settings
{
    public partial class SMTPSettings : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        string ErrorLoad;
        string ErrorSave;
        string SuccessfullySaved;
        string SmtpPort;

        protected override void InitLanguage()
        {
            try
            {
                Master.Page.Title = Proxy[EnumText.enumForum_SMTPSettings_TitleSMTPSettings];
                lblTitle.Text = Proxy[EnumText.enumForum_SMTPSettings_TitleSMTPSettings];
                lblSubTitle.Text = Proxy[EnumText.enumForum_SMTPSettings_SubtitleSMTPSettings];
                btnSave.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnSave1.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnReturn1.Text = Proxy[EnumText.enumForum_Forums_ButtonReturn];
                btnReturn2.Text = Proxy[EnumText.enumForum_Forums_ButtonReturn];
                requiredFieldValidatorSMTPServer.ErrorMessage = Proxy[EnumText.enumForum_SMTPSettings_ErrorRequireSMTPServer];
                requiredFieldValidatorSMTPPort.ErrorMessage = Proxy[EnumText.enumForum_SMTPSettings_ErrorRequireSMTPPort];
                requiredFieldValidatorUserName.ErrorMessage = Proxy[EnumText.enumForum_SMTPSettings_ErrorRequireUserName];
                requiredFieldValidatorPassword.ErrorMessage = Proxy[EnumText.enumForum_SMTPSettings_ErrorRequirePassword];
                ErrorLoad = Proxy[EnumText.enumForum_SMTPSettings_PageSMTPSettingsErrorLoad];
                ErrorSave = Proxy[EnumText.enumForum_SMTPSettings_PageSMTPSettingsErrorSave];
                SuccessfullySaved = Proxy[EnumText.enumForum_SMTPSettings_PageSMTPSettingsSuccessSave];
                SmtpPort = "25";
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumSiteSettings);
                    Page.Form.DefaultButton = this.btnSave1.UniqueID;
                    if (!IsPostBack)
                    {
                        GetSMTPSettings();
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>checkAuthentication('" + chbAuthentication.ClientID + "', '" + Label1.ClientID + "', '" + Label2.ClientID + "', '" + txtUserName.ClientID + "', '" + txtPassword.ClientID + "', '" + requiredFieldValidatorUserName.ClientID + "', '" + requiredFieldValidatorPassword.ClientID + "');</script>");
                    chbAuthentication.Attributes.Add("onclick", "checkAuthentication('" + chbAuthentication.ClientID + "', '" + Label1.ClientID + "', '" + Label2.ClientID + "', '" + txtUserName.ClientID + "', '" + txtPassword.ClientID + "', '" + requiredFieldValidatorUserName.ClientID + "', '" + requiredFieldValidatorPassword.ClientID + "');");
                }
                catch (Exception exp)
                {
                    this.lblError.Text = ErrorLoad + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }

        protected void GetSMTPSettings()
        {
            SMTPSettingWithPermissionCheck smtpSetting = SettingsProcess.GetSMTP(this.SiteId, this.UserOrOperatorId);
            this.txtSMTPServer.Text = smtpSetting.SMTPServer;
            this.txtSMTPPort.Text = smtpSetting.SMTPPort.ToString();
            this.chbAuthentication.Checked = smtpSetting.IfAuthentication;
            this.txtUserName.Text = smtpSetting.SMTPUserName;
            this.txtPassword.Text = smtpSetting.SMTPPassword;
            this.txtSenderEmailAddress.Text = smtpSetting.FromEmailAddress;
            this.txtSenderName.Text = smtpSetting.FromName;
            this.chbSSL.Checked = smtpSetting.IfSSL;
            if (this.txtSMTPPort.Text == "")
            {
                this.txtSMTPPort.Text = SmtpPort;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string smtpServer = this.txtSMTPServer.Text;
                int smtpPort = Convert.ToInt32(this.txtSMTPPort.Text);
                bool ifAuthentication = this.chbAuthentication.Checked;
                string smtpUserName = this.txtUserName.Text;
                string smtpPassword = this.txtPassword.Text;
                string fromEmailAddress = this.txtSenderEmailAddress.Text;
                string fromName = this.txtSenderName.Text;
                bool ifSSL = this.chbSSL.Checked;
                SettingsProcess.UpdateSMTP(this.SiteId, this.UserOrOperatorId, smtpServer, smtpPort, ifAuthentication, smtpUserName, smtpPassword, fromEmailAddress, fromName, ifSSL);
                //lblSuccess.Text = SuccessfullySaved;
                Response.Redirect(string.Format("Settings.aspx?siteId={0}", this.SiteId));
            }
            catch (Exception exp)
            {
                lblError.Text = ErrorSave + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("Settings.aspx?siteId={0}", this.SiteId));
            }
            catch (Exception exp)
            {
                lblError.Text = Proxy[EnumText.enumForum_Public_RedirectError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
    }
}
