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
using Com.Comm100.Framework.FieldLength;

namespace Com.Comm100.Forum.UI.AdminPanel.ReputationGroup
{
    public partial class AddReputationGroup : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        protected override void InitLanguage()
        {
            try
            {
                btnSave1.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnSave2.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnCancel1.Text = Proxy[EnumText.enumForum_Public_ButtonCancel];
                btnCancel2.Text = Proxy[EnumText.enumForum_Public_ButtonCancel];
                lblTitle.Text = Proxy[EnumText.enumForum_Reputation_TitleNewReputationGroup];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Reputation_SubtitleNewReputationGroup];
                ValidRequiredName.ErrorMessage = Proxy[EnumText.enumForum_Reputation_ErrorNameRequire];
                ValidRequiredIcoRepeat.ErrorMessage = Proxy[EnumText.enumForum_Reputation_ErrorRankRequire];
                ValidRangeIcoRepeat.ErrorMessage = Proxy[EnumText.enumForum_Reputation_ErrorRankRange];
                ValidRangeEnd.ErrorMessage = Proxy[EnumText.enumForum_Reputation_ErrorRangeEnd];
                ValidRangeStart.ErrorMessage = Proxy[EnumText.enumForum_Reputation_ErrorRangeStart];
                ValidRequiredRangeStart.ErrorMessage = Proxy[EnumText.enumForum_Reputation_ErrorRangeStartRequire];
                ValidRequiredRangeEnd.ErrorMessage = Proxy[EnumText.enumForum_Reputation_ErrorRangeEndRequire];
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumReputationGroups);
                Master.Page.Title = Proxy[EnumText.enumForum_Reputation_TitleNewReputationGroup];
                txtName.MaxLength = ForumDBFieldLength.Group_nameFieldLength;
                txtDescription.MaxLength = ForumDBFieldLength.Group_descriptionFieldLength;
                CheckReputationGroupFunction();
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Reputation_PageNewReputationErrorLoad] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }
        protected void CheckReputationGroupFunction()
        {
            ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(this.SiteId, this.UserOrOperatorId);
            if (!forumFeature.IfEnableReputation)
                ExceptionHelper.ThrowForumSettingsCloseReputationFunctio();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UserReputationGroupWithPermissionCheck[] groups=UserReputationGroupProcess.GetAllGroups(SiteId, CurrentUserOrOperator.UserOrOperatorId);
                for(int i = 0; i < groups.Length; i++)
                {
                    if (txtName.Text == groups[i].Name)
                    {
                        this.lblMessage.Text = Proxy[EnumText.enumForum_Reputation_ErrorNewReputationExist];
                        return;
                    }
                }
                UserReputationGroupProcess.Add(txtName.Text, txtDescription.Text, int.Parse(txtBegin.Text),
                    int.Parse(txtExpire.Text), int.Parse(txtIcoRepeat.Text), SiteId, CurrentUserOrOperator.UserOrOperatorId);

                Response.Redirect("~/adminpanel/reputationgroup/ReputationGroups.aspx?SiteId=" + SiteId, false);
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Reputation_PageNewReputationErrorAdd] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("ReputationGroups.aspx?SiteId=" + SiteId, false);
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_RedirectError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }
    }
}
