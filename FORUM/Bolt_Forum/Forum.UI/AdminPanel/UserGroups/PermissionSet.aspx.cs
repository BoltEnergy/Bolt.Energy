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

namespace Com.Comm100.Forum.UI.AdminPanel.UserGroups
{
    public partial class PermissionSet : AdminBasePage
    {
        string SuccessfullySaved;
        protected override void InitLanguage()
        {
            try
            {
                SuccessfullySaved = Proxy[EnumText.enumForum_Permission_PermissionSaveSucceeded];
                lblTitle.Text = Proxy[EnumText.enumForum_UserGroups_TitlePermissionSettings];
                lblSubTitle.Text = Proxy[EnumText.enumForum_UserGroups_SubtitlePermissionSettings];
                btnSave1.Text = Proxy[EnumText.enumForum_UserGroups_ButtonSave];
                btnSave2.Text = Proxy[EnumText.enumForum_UserGroups_ButtonSave];
                btnCancel1.Text = Proxy[EnumText.enumForum_UserGroups_ButtonCancel];
                btnCancel2.Text = Proxy[EnumText.enumForum_UserGroups_ButtonCancel];
                #region permission item tooltip
                this.lblViewForum.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipAllowViewForum];
                this.lblViewTopic.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipAllowViewTopicOrPost];
                this.lblPost.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipAllowPostTopicOrPost];
                this.lblAvatar.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipAllowCustomizeAvatar];
                this.lblSignature.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipMaxSignature];

                this.lblAllowLinkSignature.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipAllowInsertSignatureLink];
                this.lblAllowImageSignature.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipAllowInsertSignatureImage];
                this.lblIntervalPost.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipMinInterValTimeForPosting];
                this.lblPostLength.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipMaxLengthOfTopicOrPost];
                this.lblURL.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipAllowLink];
                this.lblImage.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipAllowInsertImage];
                this.lblAttachment.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipAttachment];
                this.lblMaxAttachmentsOnPost.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipMaxAttachmentsOnePost];
                this.lblMaxSizeOfAttachment.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipMaxSizeOfAttachment];
                this.lblMaxSizeOfAllAttachments.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipMaxSizeOfAllAttachments];
                this.lblMaxMessage.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipMaxMessage];
                this.lblSearch.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipSearch];
                this.lblIntervalSearch.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipIntervalSearch];
                this.lblPostNotModeration.Text = Proxy[EnumText.enumForum_Permission_GroupToolTipPostModerationNotNeeded];
                #endregion
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
                CheckQueryString("groupid");
                ViewState["groupId"] = Request.QueryString["groupid"];
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumUserGroups);
                Master.Page.Title = Proxy[EnumText.enumForum_UserGroups_TitlePermissionSettings];
                CheckGroupPermissionFunction();
                if (!IsPostBack)
                {
                    txtMaxSignature.MaxLength = Int32.MaxValue.ToString().Length - 1;
                    txtMinIntervalPost.MaxLength = Int32.MaxValue.ToString().Length - 1;
                    txtMaxPostLength.MaxLength = Int32.MaxValue.ToString().Length - 1;
                    txtMaxAttachmentsOnePost.MaxLength = Int32.MaxValue.ToString().Length - 1;
                    txtMaxSizeOfAttachment.MaxLength = Int32.MaxValue.ToString().Length - 3;
                    txtMaxSizeOfAllAttachments.MaxLength = Int32.MaxValue.ToString().Length - 3;
                    txtMaxMessage.MaxLength = Int32.MaxValue.ToString().Length - 1;
                    txtMinIntervalSearch.MaxLength = Int32.MaxValue.ToString().Length - 1;
                    bind();
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "checkViewForum", "checkViewForumOrTopic();", true);
                this.chbViewForum.Attributes.Add("onclick", "javascript:checkViewForumOrTopic()");
                this.chbViewTopic.Attributes.Add("onclick", "javascript:checkViewForumOrTopic()");
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_PagePermissionSettingsErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void CheckGroupPermissionFunction()
        {
            ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(this.SiteId, this.UserOrOperatorId);
            if (!forumFeature.IfEnableGroupPermission)
                ExceptionHelper.ThrowForumSettingsCloseGroupPermissionFunction();
        }

        void bind()
        {
            int userGroupId = Convert.ToInt32(ViewState["groupId"]);
            UserGroupWithPermissionCheck userGroup = UserGroupProcess.GetGroupById(SiteId, CurrentUserOrOperator.UserOrOperatorId, userGroupId);
            lblUserGroup.Text = Server.HtmlEncode(userGroup.Name);
            UserGroupPermissionWithPermissionCheck permission = UserGroupProcess.GetGroupPermissionByGroupId(userGroupId, SiteId, CurrentUserOrOperator.UserOrOperatorId);
            this.chbAttachment.Checked = permission.IfAllowUploadAttachment;
            this.chbAvatar.Checked = permission.IfAllowCustomizeAvatar;
            //this.chbHTML.Checked = permission.IfAllowHTML;
            this.chbImage.Checked = permission.IfAllowUploadImage;
            this.chbPost.Checked = permission.IfAllowPost;
            if (permission.IfPostNotNeedModeration)
                this.chbPostNotModeration.Checked = false;
            else
                this.chbPostNotModeration.Checked = true;
            this.chbSearch.Checked = permission.IfAllowSearch;
            this.chbURL.Checked = permission.IfAllowUrl;
            this.chbViewForum.Checked = permission.IfAllowViewForum;
            this.chbViewTopic.Checked = permission.IfAllowViewTopic;
            this.txtMaxAttachmentsOnePost.Text = permission.MaxCountOfAttacmentsForOnePost.ToString();
            this.txtMaxMessage.Text = permission.MaxCountOfMessageSendOneDay.ToString();
            this.txtMaxPostLength.Text = permission.MaxLengthOfPost.ToString();
            this.txtMaxSignature.Text = permission.MaxLengthofSignature.ToString();
            this.txtMaxSizeOfAllAttachments.Text = (permission.MaxSizeOfAllAttachments).ToString();
            this.txtMaxSizeOfAttachment.Text = (permission.MaxSizeOfOneAttachment).ToString();
            this.txtMinIntervalPost.Text = permission.MinIntervalForPost.ToString();
            this.txtMinIntervalSearch.Text = permission.MinIntervalForSearch.ToString();

            //this.chbAllowHTMLSignature.Checked = permission.IfSignatureAllowHTML;
            this.chbAllowLinkSignature.Checked = permission.IfSignatureAllowUrl;
            this.chbAllowImageSignature.Checked = permission.IfSignatureAllowInsertImage;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckTextBoxInput();
                int userGroupId = Convert.ToInt32(ViewState["groupId"]);
                bool ifPostModeration;
                if (chbPostNotModeration.Checked)
                    ifPostModeration = false;
                else
                    ifPostModeration = true;
                UserGroupProcess.UpdateGroupPermission(userGroupId, SiteId, chbViewForum.Checked, chbViewTopic.Checked, chbPost.Checked,
                    chbAvatar.Checked, int.Parse(txtMaxSignature.Text),
                    chbAllowLinkSignature.Checked,chbAllowImageSignature.Checked,
                    Convert.ToInt32(txtMinIntervalPost.Text), Convert.ToInt32(txtMaxPostLength.Text), 
                    chbURL.Checked, chbImage.Checked, chbAttachment.Checked,
                    Convert.ToInt32(txtMaxAttachmentsOnePost.Text), Convert.ToInt32(txtMaxSizeOfAttachment.Text), Convert.ToInt32(txtMaxSizeOfAllAttachments.Text),
                    Convert.ToInt32(txtMaxMessage.Text), chbSearch.Checked, Convert.ToInt32(txtMinIntervalSearch.Text),
                    ifPostModeration, CurrentUserOrOperator.UserOrOperatorId);
                this.lblSuccess.Text = SuccessfullySaved;
                this.lblSuccess.Visible = true;
                Response.Redirect("UserGroup.aspx?siteid=" + SiteId.ToString(), false);
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_PagePermissionSettingsErrorEdit] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void CheckTextBoxInput()
        {
            int result;
            if (!Int32.TryParse(txtMaxSignature.Text, out result))
            {
                txtMaxSignature.Focus();
                ExceptionHelper.ThrowPublicInputStringIsNotAnIntegerException(txtMaxSignature.Text);
            }
            else if (!Int32.TryParse(txtMinIntervalPost.Text, out result))
            {
                txtMinIntervalPost.Focus();
                ExceptionHelper.ThrowPublicInputStringIsNotAnIntegerException(txtMinIntervalPost.Text);
            }
            else if (!Int32.TryParse(txtMaxPostLength.Text, out result))
            {
                txtMaxPostLength.Focus();
                ExceptionHelper.ThrowPublicInputStringIsNotAnIntegerException(txtMaxPostLength.Text);
            }
            else if (!Int32.TryParse(txtMaxAttachmentsOnePost.Text, out result))
            {
                txtMaxAttachmentsOnePost.Focus();
                ExceptionHelper.ThrowPublicInputStringIsNotAnIntegerException(txtMaxAttachmentsOnePost.Text);
            }
            else if (!Int32.TryParse(txtMaxSizeOfAttachment.Text, out result))
            {
                txtMaxSizeOfAttachment.Focus();
                ExceptionHelper.ThrowPublicInputStringIsNotAnIntegerException(txtMaxSizeOfAttachment.Text);
            }
            else if (!Int32.TryParse(txtMaxSizeOfAllAttachments.Text, out result))
            {
                txtMaxSizeOfAttachment.Focus();
                ExceptionHelper.ThrowPublicInputStringIsNotAnIntegerException(txtMaxSizeOfAllAttachments.Text);
            }
            else if (!Int32.TryParse(txtMaxMessage.Text, out result))
            {
                txtMaxMessage.Focus();
                ExceptionHelper.ThrowPublicInputStringIsNotAnIntegerException(txtMaxMessage.Text);
            }
            else if (!Int32.TryParse(txtMinIntervalSearch.Text, out result))
            {
                txtMinIntervalSearch.Focus();
                ExceptionHelper.ThrowPublicInputStringIsNotAnIntegerException(txtMinIntervalSearch.Text);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("UserGroup.aspx?siteid=" + SiteId.ToString(), false);
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_RedirectError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
    }
}
