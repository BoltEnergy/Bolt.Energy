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
    public partial class EditReputationGroup : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        protected override void InitLanguage()
        {
            try
            {
                btnSave1.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnSave2.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnCancel1.Text = Proxy[EnumText.enumForum_Public_ButtonCancel];
                btnCancel2.Text = Proxy[EnumText.enumForum_Public_ButtonCancel];
                lblTitle.Text = Proxy[EnumText.enumForum_Reputation_TitleEditReputationGroup];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Reputation_SubtitleEditReputationGroup];
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
                Master.Page.Title = Proxy[EnumText.enumForum_Reputation_TitleEditReputationGroup];
                CheckQueryString("groupid");
                CheckReputationGroupPermissionFunction();
                if (!IsPostBack)
                {
                    txtName.MaxLength = ForumDBFieldLength.Group_nameFieldLength;
                    txtDescription.MaxLength = ForumDBFieldLength.Group_descriptionFieldLength;
                    bind();
                }
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Reputation_PageEditReputationGroupErrorLoad] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }
        protected void CheckReputationGroupPermissionFunction()
        {
            ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(this.SiteId, this.UserOrOperatorId);
            if (!forumFeature.IfEnableReputation)
                ExceptionHelper.ThrowForumSettingsCloseReputationFunctio();
        }

        void bind()
        {
            UserReputationGroupWithPermissionCheck group = UserReputationGroupProcess.GetGroupById(GroupId, SiteId, CurrentUserOrOperator.UserOrOperatorId);

            this.txtBegin.Text = group.LimitedBegin.ToString();
            this.txtDescription.Text = group.Description;
            this.txtExpire.Text = group.LimitedExpire.ToString();
            this.txtIcoRepeat.Text = group.IcoRepeat.ToString();
            this.txtName.Text = group.Name;
            ViewState["GroupName"] = group.Name;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UserReputationGroupWithPermissionCheck[] groups = UserReputationGroupProcess.GetAllGroups(SiteId, CurrentUserOrOperator.UserOrOperatorId);
                for (int i = 0; i < groups.Length; i++)
                {
                    if (txtName.Text == groups[i].Name && txtName.Text != ViewState["GroupName"].ToString())
                    {
                        this.lblMessage.Text = Proxy[EnumText.enumForum_Reputation_ErrorNewReputationExist];
                        return;
                    }
                }
                UserReputationGroupProcess.Update(GroupId,txtName.Text, txtDescription.Text, int.Parse(txtBegin.Text),
                    int.Parse(txtExpire.Text), int.Parse(txtIcoRepeat.Text), SiteId, CurrentUserOrOperator.UserOrOperatorId);

                Response.Redirect("~/adminpanel/reputationgroup/ReputationGroups.aspx?SiteId=" + SiteId, false);
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Reputation_PageEditReputationGroupErrorEdit] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }

        int GroupId
        {
            get
            {
                return int.Parse(Request.QueryString["groupid"]);
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
