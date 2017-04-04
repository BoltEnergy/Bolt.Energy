
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
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.UI.AdminPanel;
using Com.Comm100.Framework.Enum.Forum;
using System.Web.Configuration;
using Com.Comm100.Forum.Language;

namespace Forum.UI.AdminPanel.Settings
{
    public partial class SiteSettings : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {

        private string ErrorGetSiteSetting;
        private string ErrorOpenSite;
        private string ErrorCloseSite;
        private string ErrorLoad;
        private string ErrorSiteName;
        private string ErrorReason;
        private string Save;
        private string Return;
        //private string Normal;
        protected override void InitLanguage()
        {
            try
            {
                Master.Page.Title = Proxy[EnumText.enumForum_Settings_TitleSiteSettings];
                ErrorGetSiteSetting = Proxy[EnumText.enumForum_Settings_PageSiteSettingsErrorGetSiteSetting];
                ErrorOpenSite = Proxy[EnumText.enumForum_Settings_PageSiteSettingsErrorOpenSite];
                ErrorCloseSite = Proxy[EnumText.enumForum_Settings_PageSiteSettingsErrorCloseSite];
                ErrorLoad = Proxy[EnumText.enumForum_Settings_PageSiteSettingsErrorLoad];
                ErrorSiteName = Proxy[EnumText.enumForum_Settings_ErrorSiteName];
                ErrorReason = Proxy[EnumText.enumForum_Settings_ErrorReason];
                Save = Proxy[EnumText.enumForum_Settings_ButtonSave];
                Return = Proxy[EnumText.enumForum_Forums_ButtonReturn];
                lblTitle.Text=Proxy[EnumText.enumForum_Settings_TitleSiteSettings];
                lblSubTitle.Text=Proxy[EnumText.enumForum_Settings_SubtitleSiteSettings];
                ValidRequiredPageSize.ErrorMessage = Proxy[EnumText.enumForum_Settings_ErrorPageSizeInteger];
                ValidRangePageSize.ErrorMessage = Proxy[EnumText.enumForum_Settings_ErrorPageSizeRange];
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
                    if (!IsPostBack)
                    {
#if OPENSOURCE
#else
                        trContactDetails.Visible = false;
#endif
                        ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumSiteSettings);
                        txtSiteName.MaxLength = ForumDBFieldLength.SiteSetting_forumNameFieldLength;                     
                        btnSaveSiteSetting1.Text = Save;
                        btnSaveSiteSetting2.Text = Save;                        
                        this.btnReturn1.Text = Return;
                        this.btnReturn2.Text = Return;
                        requiredFieldValidatorSiteName.ErrorMessage = ErrorSiteName;
                        getSiteSetting();
                    }
                }
                catch (Exception exp)
                {                    
                    this.lblError.Text = ErrorLoad + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
         }
        
        protected void getSiteSetting()
        {            
            try
            {             
                SiteSettingWithPermissionCheck tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, this.UserOrOperatorId);
                if (tmpSiteSetting.SiteStatus == EnumSiteStatus.Close) radioClose.Checked = true;
                else if (tmpSiteSetting.SiteStatus == EnumSiteStatus.Normal) radioOpen.Checked = true;
                else if (tmpSiteSetting.SiteStatus == EnumSiteStatus.VisitOnly) radioVisitOnly.Checked = true;
                txtSiteName.Text = System.Web.HttpUtility.HtmlDecode(tmpSiteSetting.SiteName);
                txtMetaKeywords.Text = System.Web.HttpUtility.HtmlDecode(tmpSiteSetting.MetaKeywords);
                txtMetaDescription.Text = System.Web.HttpUtility.HtmlDecode(tmpSiteSetting.MetaDescription);
                txtPageSize.Text = tmpSiteSetting.PageSize.ToString();
                heCloseReason.Text = tmpSiteSetting.CloseReason;
//#if OPENSOURCE
//                heContactDetails.Value = tmpSiteSetting.ContactDetail;
//#endif

                if (tmpSiteSetting.SiteStatus == EnumSiteStatus.Close)
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>CloseSite();</script>");
                    
                }
                else
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>OpenSite();</script>");
                }
            }
            catch (Exception exp)
            {
                lblError.Text = ErrorGetSiteSetting + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }     

        protected void btnSave(object sender, EventArgs e)
        {          
            try
            {
                EnumSiteStatus siteStatus = EnumSiteStatus.Normal;
                if (radioClose.Checked) siteStatus = EnumSiteStatus.Close;
                else if (radioOpen.Checked) siteStatus = EnumSiteStatus.Normal;
                else if (radioVisitOnly.Checked) siteStatus = EnumSiteStatus.VisitOnly;
                string siteName = txtSiteName.Text;
                string metaKeywords = txtMetaKeywords.Text;
                string metaDescription = txtMetaDescription.Text;
                int pageSize = Convert.ToInt32(txtPageSize.Text.Trim());
                string closeReason = heCloseReason.Text;
//#if OPENSOURCE
//                string contactDetails = txtContactDetails.Value;
//                SettingsProcess.UpdateSiteSetting(SiteId, UserOrOperatorId, siteName, metaKeywords, metaDescription, pageSize, siteStatus, closeReason, contactDetails);
//#else
                SettingsProcess.UpdateSiteSetting(SiteId, UserOrOperatorId, siteName, metaKeywords, metaDescription, pageSize, siteStatus, closeReason);
//#endif
                Response.Redirect("~/AdminPanel/Settings/Settings.aspx?SiteId=" + SiteId, false);
               // if (siteStatus == EnumSiteStatus.Close)
               // {
               //     this.ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>CloseSite();</script>");
               // }
               // else
               // {
               //     this.ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>OpenSite();</script>");
               //}
               // this.lblSuccess.Text = Proxy[EnumText.enumForum_Settings_PageSiteSettingsSuccessSiteClose];
            }
            catch (Exception exp)
            {
                this.lblError.Text = ErrorCloseSite + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/AdminPanel/Settings/Settings.aspx?SiteId=" + SiteId,false);
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_Public_RedirectError]+ exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }
        }
    }
}
