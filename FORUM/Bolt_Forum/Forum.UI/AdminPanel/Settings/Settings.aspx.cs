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

namespace Com.Comm100.Forum.UI.AdminPanel.Settings
{
    public partial class Settings : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        private string Title;
        private string ErrorLoad;
        protected override void InitLanguage()
        {
            try
            {
                Master.Page.Title = Proxy[EnumText.enumForum_AdminMenu_Settings];
                this.lblTitle.Text = Proxy[EnumText.enumForum_AdminMenu_Settings];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Settings_SubitleSettings];
                hlSiteOption.Text = Proxy[EnumText.enumForum_Settings_TitleSiteSettings];
                hlRegistration.Text = Proxy[EnumText.enumForum_Settings_TitleRegistrationSettings];
                hlHeaderAndFooter.Text = Proxy[EnumText.enumForum_Settings_HlHeaderAndFooter];
                hlTemplate.Text = Proxy[EnumText.enumForum_Settings_HlTemplate];
                hlGuestUserPermission.Text = Proxy[EnumText.enumForum_Settings_HlGuestUserPermission];
                hlUserPermission.Text = Proxy[EnumText.enumForum_Settings_HlUserPermission];
                hlReputationStrategy.Text = Proxy[EnumText.enumForum_Settings_HlReputationStrategy];
                hlScoreStrategy.Text = Proxy[EnumText.enumForum_Settings_HlScoreStrategy];
                hlProhibitedWords.Text = Proxy[EnumText.enumForum_Settings_HlProhibitedWords];
                hlHotTopic.Text = Proxy[EnumText.enumForum_Settings_HlHotTopic];
                hlSMTP.Text = Proxy[EnumText.enumForum_Settings_HlSMTP];
#if OPENSOURCE
                hlSMTP.Text = Proxy[EnumText.enumForum_Settings_HlSMTP];
#endif
                ErrorLoad = Proxy[EnumText.enumForum_Settings_PageSettingsErrorLoad];
            }
            catch (Exception ex)
            {
                this.lblError.Text = ex.Message;

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumSiteSettings);
                    if (!IsPostBack)
                    {
                        #region link
                        hlSiteOption.NavigateUrl = "~/AdminPanel/Settings/SiteSettings.aspx?SiteId=" + SiteId;
                        hlRegistration.NavigateUrl = "~/AdminPanel/Settings/RegistrationSettings.aspx?SiteId=" + SiteId;
                        hlHeaderAndFooter.NavigateUrl = "~/AdminPanel/Styles/HeaderFooterSetting.aspx?SiteId=" + SiteId;
                        hlTemplate.NavigateUrl = "~/AdminPanel/Styles/TemplateSetting.aspx?SiteId=" + SiteId;
                        hlGuestUserPermission.NavigateUrl = "~/AdminPanel/Settings/GuestUserPermissions.aspx?SiteId=" + SiteId;
                        hlUserPermission.NavigateUrl = "~/AdminPanel/Settings/UserPermissions.aspx?SiteId=" + SiteId;
                        hlReputationStrategy.NavigateUrl = "~/AdminPanel/Settings/ReputationStrategy.aspx?SiteId=" + SiteId;
                        hlScoreStrategy.NavigateUrl = "~/AdminPanel/Settings/ScoreStrategy.aspx?SiteId=" + SiteId;
                        hlProhibitedWords.NavigateUrl = "~/AdminPanel/Settings/ProhibitedWords.aspx?SiteId=" + SiteId;
                        hlHotTopic.NavigateUrl = "~/AdminPanel/Settings/HotTopicStrategy.aspx?SiteId=" + SiteId;
                        hlSMTP.NavigateUrl = "~/AdminPanel/Settings/SMTPSettings.aspx?SiteId=" + SiteId;
#if OPENSOURCE
                hlSMTP.NavigateUrl = "~/AdminPanel/Settings/SMTPSettings.aspx?SiteId=" + SiteId;
#endif
                        #endregion

                        #region link visible
                        ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(SiteId, CurrentUserOrOperator.UserOrOperatorId);
                        if (forumFeature.IfEnableGroupPermission) trUserPermission.Visible = false;
                        if (!forumFeature.IfEnableReputationPermission) trReputationStrategy.Visible = false;
                        if (!forumFeature.IfEnableScore) trScoreStrategy.Visible = false;
                        if (!forumFeature.IfEnableHotTopic) trHotTopic.Visible = false;
#if OPENSOURCE
                        trSMTP.Visible = false;
#else
                        trSMTP.Visible = false;
#endif
                        #endregion
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


    }
}
