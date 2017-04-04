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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.UI.AdminPanel;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Language;

namespace Forum.UI.AdminPanel.Settings
{
    public partial class RegistrationSettings : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {

        private string Save;
        private string Retrun;
        private string ErrorLoad;
        private string ErrorGetRegistrationSetting;
        private string SuccessfullySaved;
        private string ErrorSave;
        
        protected override void InitLanguage()
        {
            try
            {
                Master.Page.Title = Proxy[EnumText.enumForum_Settings_TitleRegistrationSettings];
                Save = Proxy[EnumText.enumForum_Settings_ButtonSave];
                Retrun = Proxy[EnumText.enumForum_Forums_ButtonReturn];
                ErrorLoad = Proxy[EnumText.enumForum_Settings_PageRegistrationSettingsErrorLoad];
                ErrorGetRegistrationSetting = Proxy[EnumText.enumForum_Settings_PageRegistrationSettingsErrorGetRegistrationSetting];
                
                lblTitle.Text=Proxy[EnumText.enumForum_Settings_TitleRegistrationSettings];
                lblSubTitle.Text=Proxy[EnumText.enumForum_Settings_SubtitleRegistrationSettings];

                SuccessfullySaved = Proxy[EnumText.enumForum_Settings_PageRegistrationSettingsSuccessfullySaved];
                ErrorSave = Proxy[EnumText.enumForum_Settings_PageRegistrationSettingsErrorSave];
                ValidRangeMinLengthOfDisplayName.ErrorMessage = Proxy[EnumText.enumForum_Settings_ErrorMinLengthOfDisplayNameRange];
                ValidRequiredMinLengthOfDisplayName.ErrorMessage = Proxy[EnumText.enumForum_Settings_ErrorMinLengthOfDisplayNameRequire];
                ValidRangeMaxLengthOfDisplayName.ErrorMessage = Proxy[EnumText.enumForum_Settings_ErrorMaxLengthOfDisplayNameRange];
                ValidRequiredMaxLengthOfDisplayName.ErrorMessage = Proxy[EnumText.enumForum_Settings_ErrorMaxLengthOfDisplayNameRequire];
                revDisplayNameExpression.ErrorMessage = Proxy[EnumText.enumForum_Settings_ErrorDisolayNameRegularExpression];
            }
            catch (Exception ex)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {                   
                    ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumSiteSettings);
                    Page.Form.DefaultButton = btnSaveRegistrationSetting1.UniqueID;
                    if (!IsPostBack)
                    {
                        btnSaveRegistrationSetting1.Text = Save;
                        btnSaveRegistrationSetting2.Text = Save;
                        btnReturn1.Text = Retrun;
                        btnReturn2.Text = Retrun;
                        getRegistrationSetting();
                    }
                }
                catch (Exception exp)
                {
                    this.lblError.Visible = true;
                    this.lblError.Text = ErrorLoad + exp.Message;                   
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }

        

        protected void getRegistrationSetting()
        {
            try
            {
                RegistrationSettingWithPermissionCheck registrationSetting = SettingsProcess.GetRegistrationSettingBySiteId(CurrentUserOrOperator.UserOrOperatorId, SiteId);

                this.chkModerateNewMembers.Checked = registrationSetting.IfModerateNewUser;
                this.chkVerifyEmail.Checked = registrationSetting.IfVerifyEmail;
                this.chkAllowNewUser.Checked = registrationSetting.IfAllowNewUser;
                this.txtDisplayNameMinLength.Text = registrationSetting.DisplayNameMinLength.ToString();
                this.txtDisplayNameMaxLength.Text = registrationSetting.DisplayNameMaxLength.ToString();
                string notAllowedDisplayName = "";
                if (registrationSetting.IllegalDisplayNames != null && registrationSetting.IllegalDisplayNames.Length > 0)
                {
                    for (int i = 0; i < registrationSetting.IllegalDisplayNames.Length; i++)
                    {
                        notAllowedDisplayName = notAllowedDisplayName + "," + registrationSetting.IllegalDisplayNames[i];
                    }
                    notAllowedDisplayName = notAllowedDisplayName.Substring(1);
                }
                this.txtNotAllowedDisplayName.Text = notAllowedDisplayName;
                this.txtDisplayNameExpression.Text = registrationSetting.DisplayNameRegularExpression;
                this.txtDisplayNameExpressionInstruction.Text = registrationSetting.DisplayNameInstruction;
                this.heGreetingMessage.Text = registrationSetting.GreetingMessage;
                this.txtAgreement.Value = registrationSetting.Agreement;
            }
            catch (Exception exp)
            {                
                this.lblError.Text = ErrorGetRegistrationSetting + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnSaveRegistrationSetting_Click(object sender, EventArgs e)
        {
            try
            {
                bool ifModerateNewUser = chkModerateNewMembers.Checked;
                bool ifVerifyEmail = chkVerifyEmail.Checked;
                bool ifAllowedNewUser = chkAllowNewUser.Checked;
                int displayNameMinLength = Convert.ToInt32(txtDisplayNameMinLength.Text.Trim());
                int displayNameMaxLength = Convert.ToInt32(txtDisplayNameMaxLength.Text.Trim());
                string notAllowedDisplayName = txtNotAllowedDisplayName.Text;
                string displayNameExpression = txtDisplayNameExpression.Text;
                string displayNameExpressionInstruction = txtDisplayNameExpressionInstruction.Text;
                string greetingMessage = heGreetingMessage.Text;
                string agreement = txtAgreement.Value;
                SettingsProcess.UpdateRegistrationSetting(UserOrOperatorId, SiteId, ifModerateNewUser, ifVerifyEmail, ifAllowedNewUser, displayNameMinLength, displayNameMaxLength,
                    notAllowedDisplayName, displayNameExpression, displayNameExpressionInstruction, greetingMessage, agreement);
                Response.Redirect("~/AdminPanel/Settings/Settings.aspx?SiteId=" + SiteId);
            }
            catch (Exception exp)
            {                
                this.lblError.Text = ErrorSave + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/AdminPanel/Settings/Settings.aspx?SiteId=" + SiteId);
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_Public_RedirectError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }
        }
    }
}
