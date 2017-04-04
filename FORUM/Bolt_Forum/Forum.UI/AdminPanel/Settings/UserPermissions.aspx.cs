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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.Settings
{
    public partial class UserPermissions : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        string ErrorLoad;
        string ErrorSave;
        string SuccessfullySaved;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_UserPermission_ErrorLoad];
                ErrorSave = Proxy[EnumText.enumForum_Permission_ErrorSave];
                SuccessfullySaved = Proxy[EnumText.enumForum_Permission_SuccessfullySaved];
                lblTitle.Text = Proxy[EnumText.enumForum_Permission_Title];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Permission_SubTitle];
                btnSave1.Text = Proxy[EnumText.enumForum_Permission_ButtonSave];
                btnSave2.Text = Proxy[EnumText.enumForum_Permission_ButtonSave];
                btnCancel1.Text = Proxy[EnumText.enumForum_Permission_ButtonCancel];
                btnCancel2.Text = Proxy[EnumText.enumForum_Permission_ButtonCancel];
                #region Validator ErrorMessage
                string OnlyDigitalErrorMessage ="<br />"+ Proxy[EnumText.enumForum_Permission_OnlyDigitalErrorMessage];
                this.revIntervalPosting.ErrorMessage = OnlyDigitalErrorMessage;
                this.revMaxAttachmentsInOnePost.ErrorMessage = OnlyDigitalErrorMessage;
                this.revMaxMessagesOnDay.ErrorMessage = OnlyDigitalErrorMessage;
                this.revMaxSizeOfAllAttachments.ErrorMessage = OnlyDigitalErrorMessage;
                this.revMaxSizeOfAttachment.ErrorMessage = OnlyDigitalErrorMessage;
                this.revMinIntervalSearching.ErrorMessage = OnlyDigitalErrorMessage;
                this.revPostLength.ErrorMessage = OnlyDigitalErrorMessage;
                this.revSignatureLength.ErrorMessage = OnlyDigitalErrorMessage;
                #endregion
                #region permission item tooltip
                this.lblViewForum.Text = Proxy[EnumText.enumForum_UserPermission_HelpAllowViewForum];
                this.lblViewTopic.Text = Proxy[EnumText.enumForum_UserPermission_HelpAllowViewTopicOrPost];
                this.lblPost.Text = Proxy[EnumText.enumForum_UserPermission_HelpAllowPostTopicOrPost];
                this.lblAvatar.Text = Proxy[EnumText.enumForum_UserPermission_HelpAllowCustomizeAvatar];
                this.lblSignature.Text = Proxy[EnumText.enumForum_UserPermission_HelpMaxlengthofSignature];

                this.lblAllowLinkSignature.Text = Proxy[EnumText.enumForum_UserPermission_HelpAllowInsertLink];
                this.lblAllowImageSignature.Text = Proxy[EnumText.enumForum_UserPermission_HelpAllowInsertImage];
                this.lblIntervalPost.Text = Proxy[EnumText.enumForum_UserPermission_HelpMinPostIntervalTime];
                this.lblPostLength.Text = Proxy[EnumText.enumForum_UserPermission_HelpMaxLengthofTopicOrPost];
                this.lblURL.Text = Proxy[EnumText.enumForum_UserPermission_HelpAllowInsertLink];
                this.lblImage.Text = Proxy[EnumText.enumForum_UserPermission_HelpAllowInsertImage];
                this.lblAttachment.Text = Proxy[EnumText.enumForum_UserPermission_HelpAllowAttachment];
                this.lblMaxAttachmentsOnPost.Text = Proxy[EnumText.enumForum_UserPermission_HelpMaxAttachmentsinOnePost];
                this.lblMaxSizeOfAttachment.Text = Proxy[EnumText.enumForum_UserPermission_HelpMaxSizeofeachAttachment];
                this.lblMaxSizeOfAllAttachments.Text = Proxy[EnumText.enumForum_UserPermission_HelpMaxSizeofalltheAttachments];
                this.lblMaxMessage.Text = Proxy[EnumText.enumForum_UserPermission_HelpMaxMessagesinOneDay];
                this.lblSearch.Text = Proxy[EnumText.enumForum_UserPermission_HelpAllowSearch];
                this.lblIntervalSearch.Text = Proxy[EnumText.enumForum_UserPermission_HelpMinSearchIntervalTime];
                this.lblPostNotModeration.Text = Proxy[EnumText.enumForum_UserPermission_HelpPostModerationRequired];
                #endregion
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_Permission_InitializingLanguageError] + exp.Message;
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
                    Master.Page.Title = Proxy[EnumText.enumForum_Permission_Title];
                    Page.Form.DefaultButton = this.btnSave1.UniqueID;
                    if (!IsPostBack)
                    {
                        txtSignature.MaxLength = Int32.MaxValue.ToString().Length - 1;
                        txtIntervalPost.MaxLength = Int32.MaxValue.ToString().Length - 1;
                        txtPostLength.MaxLength = Int32.MaxValue.ToString().Length - 1;
                        txtMaxAttachmentsOnePost.MaxLength = Int32.MaxValue.ToString().Length - 1;
                        txtMaxSizeOfAttachment.MaxLength = Int32.MaxValue.ToString().Length - 3;
                        txtMaxSizeOfAllAttachments.MaxLength = Int32.MaxValue.ToString().Length - 3;
                        txtMessage.MaxLength = Int32.MaxValue.ToString().Length - 1;
                        txtIntervalSearch.MaxLength = Int32.MaxValue.ToString().Length - 1;
                        GetUserPermissions();
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "checkViewForum", "checkViewForumOrTopic();", true);
                    this.chbViewForum.Attributes.Add("onclick", "javascript:checkViewForumOrTopic()");
                    this.chbViewTopic.Attributes.Add("onclick", "javascript:checkViewForumOrTopic()");
            
                }
                catch (Exception exp)
                {
                    this.lblError.Text = ErrorLoad + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }

        protected void GetUserPermissions()
        {
            UserPermissionSettingWithPermissionCheck userPermission = SettingsProcess.GetUserPermission(SiteId, UserOrOperatorId);
            this.chbViewForum.Checked = userPermission.IfAllowViewForum;
            this.chbViewTopic.Checked = userPermission.IfAllowViewTopic;
            this.chbPost.Checked = userPermission.IfAllowPost;
            this.chbAvatar.Checked = userPermission.IfAllowCustomizeAvatar;
            this.chbLinkSignature.Checked = userPermission.IfSignatureAllowUrl;
            this.chbImageSignature.Checked = userPermission.IfSignatureAllowInsertImage;
            this.txtSignature.Text = Convert.ToString(userPermission.MaxLengthofSignature);
            this.txtIntervalPost.Text = Convert.ToString(userPermission.MinIntervalForPost);
            this.txtPostLength.Text = Convert.ToString(userPermission.MaxLengthOfPost);
            
            this.chbURL.Checked = userPermission.IfAllowUrl;
            this.chbImage.Checked = userPermission.IfAllowUploadImage;
            this.chbAttachment.Checked = userPermission.IfAllowUploadAttachment;
            this.txtMaxAttachmentsOnePost.Text = Convert.ToString(userPermission.MaxCountOfAttacmentsForOnePost);
            this.txtMaxSizeOfAttachment.Text = Convert.ToString(userPermission.MaxSizeOfOneAttachment);
            this.txtMaxSizeOfAllAttachments.Text = Convert.ToString(userPermission.MaxSizeOfAllAttachments);
            this.txtMessage.Text = Convert.ToString(userPermission.MaxCountOfMessageSendOneDay);
            this.chbSearch.Checked = userPermission.IfAllowSearch;
            this.txtIntervalSearch.Text = Convert.ToString(userPermission.MinIntervalForSearch);
            if (userPermission.IfPostNotNeedModeration)
                this.chbPostNotModeration.Checked = false;
            else
                this.chbPostNotModeration.Checked = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool ifAllowViewForum = this.chbViewForum.Checked;
                bool ifAllowViewTopic = this.chbViewTopic.Checked ;
                bool ifAllowPost = this.chbPost.Checked;
                bool ifAllowCustomizeAvatar = this.chbAvatar.Checked;
                int maxLengthofSignature = Convert.ToInt32(this.txtSignature.Text);
                int minIntervalForPost = Convert.ToInt32(this.txtIntervalPost.Text);
                int maxLengthOfPost = Convert.ToInt32(this.txtPostLength.Text);
                //bool ifAllowHTML = this.chbHTML.Checked;
                bool ifAllowUrl = this.chbURL.Checked;
                bool ifAllowUploadImage = this.chbImage.Checked;
                bool ifAllowUploadAttachment = this.chbAttachment.Checked;
                int maxCountOfAttacmentsForOnePost = Convert.ToInt32(this.txtMaxAttachmentsOnePost.Text);
                int maxSizeOfOneAttachment = Convert.ToInt32(this.txtMaxSizeOfAttachment.Text);
                int maxSizeOfAllAttachments = Convert.ToInt32(this.txtMaxSizeOfAllAttachments.Text);
                int maxCountOfMessageSendOneDay = Convert.ToInt32(this.txtMessage.Text);
                bool ifAllowSearch = this.chbSearch.Checked;
                int minIntervalForSearch = Convert.ToInt32(this.txtIntervalSearch.Text);
                bool IfPostNotNeedModeration ;
                if (this.chbPostNotModeration.Checked)
                    IfPostNotNeedModeration = false;
                else
                    IfPostNotNeedModeration = true;
                bool ifSignatureAllowUrl = this.chbLinkSignature.Checked;
                bool ifSignatureAllowInsertImage=this.chbImageSignature.Checked;
                SettingsProcess.UpdateUserPermission(this.SiteId, this.UserOrOperatorId,ifAllowViewForum,ifAllowViewTopic,ifAllowPost,minIntervalForPost,maxLengthOfPost,IfPostNotNeedModeration,ifAllowCustomizeAvatar,maxLengthofSignature,
                     ifSignatureAllowUrl,  ifSignatureAllowInsertImage,ifAllowUrl,ifAllowUploadImage,ifAllowUploadAttachment,maxCountOfAttacmentsForOnePost,maxSizeOfOneAttachment,maxSizeOfAllAttachments,maxCountOfMessageSendOneDay,ifAllowSearch,minIntervalForSearch);
                lblSuccess.Text = SuccessfullySaved;
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
            Response.Redirect(string.Format("Settings.aspx?siteId={0}", this.SiteId));
        }
    }
}
