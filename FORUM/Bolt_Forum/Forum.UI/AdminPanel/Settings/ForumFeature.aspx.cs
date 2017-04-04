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
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Language;
namespace Com.Comm100.Forum.UI.AdminPanel.Settings
{
    public partial class ForumFeature : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        protected override void InitLanguage()
        {
            try
            {
                Master.Page.Title = Proxy[EnumText.enumForum_Settings_TitleForumFeature];
                btnSave1.Text = Proxy[EnumText.enumForum_Settings_ButtonSave];
                btnSave2.Text = Proxy[EnumText.enumForum_Settings_ButtonSave];
                lblTitle.Text = Proxy[EnumText.enumForum_Settings_TitleForumFeature];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Settings_SubTitleForumFeature];
                chxMessage.Text = Proxy[EnumText.enumForum_Settings_FieldEnableMessage];
                chxFavorite.Text = Proxy[EnumText.enumForum_Settings_FieldEnableFavorite];
                //chxSubscribe.Text = Proxy[EnumText.enumForum_Settings_FieldEnableSubscribe];
                chxScore.Text = Proxy[EnumText.enumForum_Settings_FieldEnableScore];
                chxReputation.Text = Proxy[EnumText.enumForum_Settings_FieldEnableReputation];
                chxHotTopic.Text = Proxy[EnumText.enumForum_Settings_FieldEnableHotTopic];
                chxGroupPermission.Text = Proxy[EnumText.enumForum_Settings_FieldEnableGroupPermission];
                lblRP.Text = Proxy[EnumText.enumForum_Settings_FieldEnableReputationPermission];
                //imgViewMessage.ToolTip = Proxy[EnumText.enumForum_Settings_HelpEnableMessage];
                //imgViewSubscribe.ToolTip = Proxy[EnumText.enumForum_Settings_HelpEnableSubscribe];
                //imgViewFavorite.ToolTip = Proxy[EnumText.enumForum_Settings_HelpEnableFavorite];
                //imgViewReputationPermission.ToolTip = Proxy[EnumText.enumForum_Settings_HelpEnableReputationPermission];
                //imgViewScore.ToolTip = Proxy[EnumText.enumForum_Settings_HelpEnableScore];
                //imgViewReputation.ToolTip = Proxy[EnumText.enumForum_Settings_HelpEnableReputation];
                //imgViewGroupPermission.ToolTip = Proxy[EnumText.enumForum_Settings_HelpEnableGroupPermission];
                //imgViewHotTopic.ToolTip = Proxy[EnumText.enumForum_Settings_HelpEnableHotTopic];
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + ex.Message;
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
                        ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumForumFeature);
                        GetForumFeature();
                    }
                    this.chxReputation.Attributes.Add("onclick", "javascript:contactReputationAndReputationPermission();");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Init",
                "contactReputationAndReputationPermission();", true);
                }

                catch (Exception exp)
                {
                    this.lblError.Text = Proxy[EnumText.enumForum_Settings_PageForumFeatureErrorLoad] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }
        protected void GetForumFeature()
        {
            try
            {
                ForumFeatureWithPermissionCheck ForumFeature = SettingsProcess.GetForumFeature(this.SiteId, this.UserOrOperatorId);
                chxMessage.Checked = ForumFeature.IfEnableMessage;
                chxFavorite.Checked = ForumFeature.IfEnableFavorite;
                //chxSubscribe.Checked = ForumFeature.IfEnableSubscribe;
                chxScore.Checked = ForumFeature.IfEnableScore;
                chxReputation.Checked = ForumFeature.IfEnableReputation;
                chxHotTopic.Checked = ForumFeature.IfEnableHotTopic;
                chxGroupPermission.Checked = ForumFeature.IfEnableGroupPermission;
                chxReputationPermission.Checked = ForumFeature.IfEnableReputationPermission;

            }
            catch (Exception exp)
            {
                lblError.Text = Proxy[EnumText.enumForum_Settings_PageForumFeatureErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void btnSave(object sender, EventArgs e)
        {
            try
            {
                bool ifEnableMessage = chxMessage.Checked;
                bool ifEnableFavorite = chxFavorite.Checked;
                bool ifEnableSubscribe = false; //chxSubscribe.Checked;
                bool ifEnableScore = chxScore.Checked;
                bool ifEnableReputation = chxReputation.Checked;
                bool ifEnableHotTopic = chxHotTopic.Checked;
                bool ifEnableGroupPermission = chxGroupPermission.Checked;
                bool ifEnableReputationPermission = chxReputationPermission.Checked;
                SettingsProcess.UpdateForumFeature(this.SiteId, this.UserOrOperatorId, ifEnableMessage, ifEnableFavorite,
                    ifEnableSubscribe, ifEnableScore, ifEnableReputation, ifEnableHotTopic, ifEnableGroupPermission, ifEnableReputationPermission);
                lblSuccess.Text = Proxy[EnumText.enumForum_Settings_PageForumFeatureSuccessSave];
                ((AdminMasterPage)Master).SetMenuVisible();
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_Settings_PageForumFeatureErrorSave] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }

        }

    }
}
