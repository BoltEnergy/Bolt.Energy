
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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using System.Web.UI.HtmlControls;
using Com.Comm100.Forum.Language;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.UI.Common;

namespace Forum.UI
{
    public partial class AddTopic : ForumBasePage
    {
        #region Permission Check Display
        private void CheckIfViewEditTopicPage()
        {
            if (this.IfGuest)
                //Response.Redirect("Login.aspx?siteId=" + SiteId,false);
                Response.Redirect(Com.Comm100.Forum.UI.Common.WebUtility.GetAppSetting("BoltLoginURL"),false);
            if (!this.UserPermissionInForum.IfAllowViewForum || !this.UserPermissionInForum.IfAllowViewTopicOrPost
                || !this.UserPermissionInForum.IfAllowPostTopicOrPost)
            {
                ExceptionHelper.ThrowForumUserHaveNoPermissionToVisit();
            }

        }
        private bool IfAttachmentAreaDisplay()
        {
            return this.UserPermissionInForum.IfAllowAttachment;
        }
        private bool IfContentEditorDisplay()
        {
            return true;//return this.UserPermissionInForum.IfAllowHTML;
        }
        private bool HtmlEditorInsertLinkButtonDisplay()
        {
            if (this.UserPermissionInForum.IfAllowURL)
                return true;
            else
                return false;
        }
        private bool HtmlEditorRemoveLinkButtonDisplay()
        {
            if (this.UserPermissionInForum.IfAllowURL)
                return true;
            else
                return false;
        }
        private bool HtmlEditorInsertImageButtonDisplay()
        {
            if (this.UserPermissionInForum.IfAllowInsertImage)
                return true;
            else
                return false;
        }
        #endregion

        #region Page Display Init
        private void CheckIfViewEditTopicPageInit()
        {
            CheckIfViewEditTopicPage();
        }
        private void AttachmentAreaInit()
        {
            /*Attach Area*/
            if (IfAttachmentAreaDisplay() == true)
                trAttachment.Visible = true;
            else
                trAttachment.Visible = false;
        }
        private void HtmlControlInit()
        {
            if (!this.IfContentEditorDisplay())
            {
                HTMLEditor.Mode = "text";
            }
            else
                HTMLEditor.Mode = "bandbyidorname";

            if (HtmlEditorInsertImageButtonDisplay())
            {
                this.HTMLEditor.IfAllowInsertImage = true;
            }
            else
            {
                this.HTMLEditor.IfAllowInsertImage = false;
            }
            if (HtmlEditorInsertLinkButtonDisplay())
            {
                this.HTMLEditor.IfAllowInsertLink = true;
            }
            else
            {
                this.HTMLEditor.IfAllowInsertLink = false;
            }
            if (HtmlEditorRemoveLinkButtonDisplay())
            {
                this.HTMLEditor.IfAllowInsertLink = true;
            }
            else
            {
                this.HTMLEditor.IfAllowInsertLink = false;
            }
        }
        #endregion

        #region Page Property
        protected int ForumId
        {
            get { return Convert.ToInt32(ViewState["forumId"]); }
        }
        private enum EnumPostSettings { Normal, NeedRelay, NeedPay };
        private EnumPostSettings PostSetting { get; set; }
        public string IfShowPollConection
        {
            get
            {
                if (!this.chkPollCreation.Checked)
                    return "display: none";
                else
                    return "";
            }
        }
        public List<int> LastAttachsIds { get; set; }
        public List<int> LastAttachsScoresList { get; set; }
        public List<string> LastAttachsDescriptionsList { get; set; }
        public Guid uniqueGuid { get { return (Guid)(ViewState["guid"]); } set { ViewState["guid"] = value; } }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, this.UserOrOperatorId);
                    Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Topic_BrowerTitleAddTopic], siteSetting.SiteName);

                    this.Page.SetFocus(btnSubmit);

                    //hiding the search button
                    ((Button)this.Master.FindControl("BtnSearch")).Visible = false;

                    if (!IsPostBack)
                    {
                        // CheckQueryString("forumId");
                        ViewState["forumId"] = WebUtility.GetAppSetting("forumId");// Convert.ToInt32(Request.QueryString["forumId"]);

                        CheckQueryString("pageType");

                        if (Request.QueryString["pageType"].ToLower().Equals("forum"))
                            ViewState["url"] = string.Format("~/default.aspx?forumId={0}&siteId={1}", Convert.ToInt32(ViewState["forumId"]), SiteId);

                        else
                        {
                            CheckQueryString("topicId");

                            ViewState["url"] = string.Format("~/Topic.aspx?topicId={0}&siteId={1}&forumId={2}", Request.QueryString["topicId"], SiteId, ViewState["forumId"]);
                        }

                        txtSubject.MaxLength = ForumDBFieldLength.Topic_subjectFieldLength;
                        //HTMLEditor.MaxLength = ForumDBFieldLength.Topic_contentFieldLength;

                        btnUpload.Text = Proxy[EnumText.enumForum_AddTopic_ButtonUpload];
                        btnSubmit.Visible = true;
                        btnCancel.Visible = true;
                        btnSubmit.Text = Proxy[EnumText.enumForum_Topic_ButtonSubmit];
                        btnCancel.Text = Proxy[EnumText.enumForum_Topic_ButtonCancel];
                        RequiredtxtSubject.ErrorMessage = Proxy[EnumText.enumForum_Topic_ErrorSubjectRequired];

                        PageInit();
                        uniqueGuid = Guid.NewGuid();
                        this.txtSubject.Focus();
                        HTMLEditor.Text = "";//this is for '/t' bug
                    }

                    CheckIfViewEditTopicPageInit();
                    HtmlControlInit();
                    AttachmentAreaInit();
                }
                catch (Exception exp)
                {
                    LogHelper.WriteExceptionLog(exp);

                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageAddTopicErrorPageLoading] + exp.Message;

                    this.IfError = true;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    if (!string.IsNullOrEmpty(HTMLEditor.Text.Trim()))
                    {
                        String subject = txtSubject.Text;
                        String content = HTMLEditor.Text;
                        int score; bool ifReplyRequired; bool IfPayScoreRequired; bool ifContainsPoll; bool ifMultipleChoice;
                        int maxChoices; bool ifSetDeadline; DateTime startDate; DateTime endDate; string[] options;
                        /*Add New Topic*/
                        AddNewTopic(out score, out ifReplyRequired, out IfPayScoreRequired,
                            out ifContainsPoll, out ifMultipleChoice, out maxChoices, out ifSetDeadline,
                            out startDate, out endDate, out options);
                        GetLastAttahmentsInfor(-1);
                        TopicWithPermissionCheck topic = TopicProcess.AddTopic(this.SiteId, this.UserOrOperatorId,
                            this.IfOperator, Convert.ToInt32(ViewState["forumId"]), subject, content,
                            score, ifReplyRequired, IfPayScoreRequired,
                            ifContainsPoll, ifMultipleChoice, maxChoices, ifSetDeadline,
                            startDate, endDate, options,
                            this.LastAttachsIds.ToArray<int>(), this.LastAttachsScoresList.ToArray<int>(),
                            this.LastAttachsDescriptionsList.ToArray<string>());

                        Response.Redirect(
                            string.Format("~/Topic.aspx?topicId={0}&forumId={1}&siteid={2}", topic.TopicId, ViewState["forumId"], SiteId), false);
                    }
                    else
                    {

                        //LogHelper.WriteExceptionLog(exp);
                        string script = string.Format("<script>alert(\"{0}\");</script>", "Comment Box Cannot be Empty");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);

                        this.IfError = true;
                    }
                }
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageAddTopicErrorSubmit] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);

                    string script = string.Format("<script>alert(\"" + Proxy[EnumText.enumForum_Topic_PageAddTopicErrorSubmit] + "{0}\");</script>", exp.Message);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);

                    this.IfError = true;
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect(Convert.ToString(ViewState["url"]), false);
        }

        /*2.0*/
        private void AddNewTopic(
            out int score, out bool ifReplyRequired, out bool IfPayScoreRequired,
            out bool ifContainsPoll, out bool ifMultipleChoice, out int maxChoices, out bool ifSetDeadline,
            out DateTime startDate, out DateTime endDate, out string[] options)
        {
            GetPostSettings(out score, out ifReplyRequired, out IfPayScoreRequired);
            GetPollsInfor(out ifContainsPoll, out ifMultipleChoice, out maxChoices, out ifSetDeadline,
                out startDate, out endDate, out options);
        }

        private void GetPostSettings(out int Score, out bool ifReplyRequired, out bool IfPayScoreRequired)
        {
            Score = 0;
            ifReplyRequired = false; IfPayScoreRequired = false;
            if (this.rdNormal.Checked)
                this.PostSetting = EnumPostSettings.Normal;
            else if (this.rdReply.Checked)
            { this.PostSetting = EnumPostSettings.NeedRelay; ifReplyRequired = true; }
            else
            {
                this.PostSetting = EnumPostSettings.NeedPay;
                Score = Convert.ToInt32(this.txtPay.Value);
                IfPayScoreRequired = true;
            }
        }

        private void GetPollsInfor(
            out bool ifContainsPoll, out bool ifMultipleChoice, out int maxChoices, out bool ifSetDeadline,
            out DateTime startDate, out DateTime endDate, out string[] options)
        {
            ifContainsPoll = this.chkPollCreation.Checked;
            if (!string.IsNullOrEmpty(this.txtMulitiple.Text.Trim()))
                maxChoices = Convert.ToInt32(this.txtMulitiple.Text.Trim());
            else
                maxChoices = 1;
            if (maxChoices > 1) { ifMultipleChoice = true; }
            else { ifMultipleChoice = false; }
            ifSetDeadline = this.chkPollDate.Checked;
            startDate = DateTime.UtcNow;
            if (!string.IsNullOrEmpty(this.txtPollDate.Text.Trim()))
            {
                //end date is yyyy-MM-dd 23:59:59
                endDate = Convert.ToDateTime(this.txtPollDate.Text.Trim()).AddDays(1).AddSeconds(-1).ToUniversalTime();
            }
            else
                endDate = DateTime.UtcNow;
            if (this.tbPollOptions.Value.Trim() != "")
                options = this.tbPollOptions.Value.Replace("\r\n", "\n").Trim('\n').Split('\n');
            else
                options = null;
        }

        private void UpdateAttachmentInfor()
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    int AttachmentsOfPostCount = GetAttachmentData().Length;
                    GetLastAttahmentsInfor(-1);
                    /*Add Attachment to DataBase*/
                    if (this.file.FileBytes.Length != 0)
                    {
                        string name = this.file.FileName;
                        int attachId = AttachmentProcess.AddAttachment(this.UserOrOperatorId, SiteId, ForumId, AttachmentsOfPostCount,
                            -1, this.UserOrOperatorId, this.file.FileBytes.Length
                            , this.file.FileBytes, false, 0, name, "", uniqueGuid, EnumAttachmentType.AttachToPost);
                        this.LastAttachsIds.Add(attachId);
                        this.LastAttachsScoresList.Add(0);
                        this.LastAttachsDescriptionsList.Add("");
                    }
                    RefreashAttachmentsData();
                }
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_AddTopic_ErrorUploadAttachment] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);

                    this.IfError = true;
                }
            }
        }

        private void PageInit()
        {
            this.txtMulitiple.Attributes.Add("onkeyup", string.Format("TextKeyUp('{0}');", txtMulitiple.ClientID));
            this.txtPay.Attributes.Add("onkeyup", string.Format("TextKeyUp('{0}');", txtPay.ClientID));
            this.file.Attributes.Add("onchange", string.Format("document.getElementById('{0}').click();", btnUpload.ClientID));
            this.btnUpload.Style.Value = "display:none";

            AttachmentProcess.DeleteTempAttachmentsOfUser(this.UserOrOperatorId, SiteId, this.UserOrOperatorId,
                EnumAttachmentType.AttachToPost);

            /*forum post setting Init*/
            ForumWithPermissionCheck forum = ForumProcess.GetForumByForumId(SiteId, UserOrOperatorId, ForumId);
            if (!forum.IfAllowPostNeedingPayTopic)
                this.fdpay.Visible = false;
            if (!forum.IfAllowPostNeedingReplayTopic)
                this.fdReply.Visible = false;
        }

        private void GetLastAttahmentsInfor(int deletedAttachmentId)
        {
            List<int> AttachIds = new List<int>();
            List<int> Scores = new List<int>();
            List<string> descriptions = new List<string>();
            foreach (RepeaterItem ri in this.rptAttachments.Items)
            {
                int AttachId = Convert.ToInt32((ri.FindControl("hdAttachId") as HiddenField).Value);
                int score = Convert.ToInt32((ri.FindControl("tbScore") as TextBox).Text);
                string description = (ri.FindControl("tbDescription") as TextBox).Text;
                if (deletedAttachmentId != AttachId)
                {
                    AttachIds.Add(AttachId);
                    Scores.Add(score);
                    descriptions.Add(description);
                }
            }
            this.LastAttachsIds = AttachIds;
            this.LastAttachsDescriptionsList = descriptions;
            this.LastAttachsScoresList = Scores;
        }
        private void RefreashAttachmentsData()
        {
            AttachmentWithPermissionCheck[] attachments = GetAttachmentData();
            /*Data Bind*/
            this.rptAttachments.DataSource = attachments;
            this.rptAttachments.DataBind();
        }

        private AttachmentWithPermissionCheck[] GetAttachmentData()
        {
            /*Get Data From DataBase*/
            AttachmentWithPermissionCheck[] attachments = AttachmentProcess.GetTempAttachmentsOfUser(
                this.UserOrOperatorId, SiteId, this.UserOrOperatorId, uniqueGuid, EnumAttachmentType.AttachToPost);
            return attachments;
        }

        protected void rptAttachments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    AttachmentWithPermissionCheck attach = e.Item.DataItem as
                        AttachmentWithPermissionCheck;
                    HiddenField hdId = e.Item.FindControl("hdAttachId") as HiddenField;
                    hdId.Value = attach.Id.ToString();
                    TextBox tbScore = e.Item.FindControl("tbScore") as TextBox;
                    if (this.LastAttachsScoresList.Count - 1 >= e.Item.ItemIndex)
                        tbScore.Text = LastAttachsScoresList[e.Item.ItemIndex].ToString();
                    else
                        tbScore.Text = "0";/*defalut*/
                    tbScore.Attributes.Add("onkeyup", string.Format("TextKeyUp('{0}');", tbScore.ClientID));
                    TextBox tbDescription = e.Item.FindControl("tbDescription") as TextBox;
                    tbDescription.MaxLength = ForumDBFieldLength.Attachment_descriptionFieldLength;

                    if (this.LastAttachsDescriptionsList.Count - 1 >= e.Item.ItemIndex)
                        tbDescription.Text = LastAttachsDescriptionsList[e.Item.ItemIndex];
                    else
                        tbDescription.Text = "";/*default*/

                    (e.Item.FindControl("cvDownloadScore") as CompareValidator).ControlToValidate = tbScore.ID;
                    (e.Item.FindControl("rfvDownloadScore") as RequiredFieldValidator).ControlToValidate = tbScore.ID;
                }
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_AddTopic_ErrorLoadingAttachments] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);

                    this.IfError = true;
                }
            }
        }

        protected void rptAttachments_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    int attachmentId = Convert.ToInt32(e.CommandArgument);
                    GetLastAttahmentsInfor(attachmentId);
                    if (e.CommandName == "Delete")
                    {
                        try
                        {
                            AttachmentProcess.DeleteAttachment(this.UserOrOperatorId, SiteId, attachmentId, ForumId);
                        }
                        catch (Exception exp)
                        {
                            lblMessage.Text = Proxy[EnumText.enumForum_AddTopic_ErrorDeleteAttachment] + exp.Message;
                            LogHelper.WriteExceptionLog(exp);
                        }
                    }
                    RefreashAttachmentsData();
                }
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_AddTopic_ErrorLoadingAttachments] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);

                    this.IfError = true;
                }
            }
        }
    }

}
