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
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.Settings
{
    public partial class GuestUserPermissions : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        string ErrorLoad;
        string ErrorSave;
        string SuccessfullySaved;
        protected override void  InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Settings_PageGuestUserPermissionsErrorLoad];
                ErrorSave = Proxy[EnumText.enumForum_Settings_PageGuestUserPermissionsErrorSave];
                SuccessfullySaved = Proxy[EnumText.enumForum_Settings_PageHeaderFooterSettingSuccessSave];
                lblTitle.Text = Proxy[EnumText.enumForum_Settings_TitleGuestUserPermissions];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Settings_SubtitleGuestUserPermissions];
                btnSave1.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnSave2.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnReturn1.Text = Proxy[EnumText.enumForum_Forums_ButtonReturn];
                btnReturn2.Text = Proxy[EnumText.enumForum_Forums_ButtonReturn];
                this.rvSearchInterval.ErrorMessage = Proxy[EnumText.enumForum_Settings_ErrorSearchInterval];
                //ImageViewForum.ToolTip = Proxy[EnumText.enumForum_Settings_HelpAllowViewForum];
               // ImageSearch.ToolTip = Proxy[EnumText.enumForum_Settings_HelpAllowSearch];
               // ImageSearchInterval.ToolTip = Proxy[EnumText.enumForum_Settings_HelpMinIntervalTimeSearching];
                this.lblViewForum.Text = Proxy[EnumText.enumForum_Settings_HelpAllowViewForum];
                this.lblSearch.Text = Proxy[EnumText.enumForum_Settings_HelpAllowSearch];
                this.lblIntervalSearch.Text = Proxy[EnumText.enumForum_Settings_HelpMinIntervalTimeSearching];

            }
            catch(Exception exp)
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
                    Master.Page.Title = Proxy[EnumText.enumForum_Settings_TitleGuestUserPermissions];
                    Page.Form.DefaultButton = this.btnSave1.UniqueID;
                    if (!IsPostBack)
                    {
                        GetUserPermissionSetting();
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

        protected void GetUserPermissionSetting()
        {
            GuestUserPermissionSettingWithPermissionCheck guestUserPermission = SettingsProcess.GetGuestUserPermission(this.SiteId, this.UserOrOperatorId);
            this.ckbViewForum.Checked = guestUserPermission.IfAllowGuestUserViewForum;
            this.ckbSearch.Checked = guestUserPermission.IfAllowGuestUserSearch;
            this.txtSearchInterval.Text = Convert.ToString(guestUserPermission.GuestUserSearchInterval);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool ifAllowGuestUserViewForum = this.ckbViewForum.Checked;
                bool ifAllowGuestUserSearch = this.ckbSearch.Checked;
                int guestUserSearchInterval = Convert.ToInt32(this.txtSearchInterval.Text);
                SettingsProcess.UpdateGuestUserPermission(this.SiteId, this.UserOrOperatorId, ifAllowGuestUserViewForum, ifAllowGuestUserSearch, guestUserSearchInterval);
                //this.lblSuccess.Text = SuccessfullySaved;
                Response.Redirect(string.Format("Settings.aspx?siteId={0}", this.SiteId), false);
            }
            catch (Exception exp)
            {
                lblError.Text = ErrorSave +exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("Settings.aspx?siteId={0}", this.SiteId), false);
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
