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
using Com.Comm100.Forum.Language;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.UI.Common;

namespace Forum.UI
{
    public partial class EditTopic : ForumBasePage
    {
        #region Permission Check
        //private bool IfAllowViewForum()
        //{
        //    if (IfAdmin() || IfModerator(ForumId))
        //        return true;
        //    return this.UserForumPermissionList(ForumId).IfAllowViewForum;
        //}
        //private bool IfAllowViewTopicOrPost()
        //{
        //    if (IfAdmin() || IfModerator(ForumId))
        //        return true;
        //    return this.UserForumPermissionList(ForumId).IfAllowViewTopic;
        //}
        //private bool IfAllowPostTopicOrPost()
        //{
        //    if (IfAdmin() || IfModerator(ForumId))
        //        return true;
        //    return this.UserForumPermissionList(ForumId).IfAllowPost;
        //}
        //private bool IfAllowAttach()
        //{
        //    if (IfAdmin() || IfModerator(ForumId))
        //        return true;
        //    return this.UserPermissionCache.IfAllowUploadAttachment;
        //}
        ////private bool IfAllow
        //private bool IfAllowHtmlPermission()
        //{
        //    if (IfAdmin() || IfModerator(ForumId))
        //        return true;
        //    return this.UserForumPermissionList(ForumId).IfAllowHTML;
        //}
        #endregion

        #region Permission Check Display
        private void CheckIfViewEditTopicPage()
        {
            if (this.IfGuest)
                Response.Redirect("Login.aspx?siteId=" + SiteId, false);
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
        private bool IfAttachmentUploadControlDisplay()
        {
            return this.UserPermissionInForum.IfAllowAttachment;
        }
        private bool IfContentEditorDisplay()
        {
            return true;
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
       // protected bool IfShowAttaachAreaButNoAllowAttachPermission { get; set; }
        private bool IfShowAttachAreaInit(int countOfAttachmentShown)
        {
            /*Attach Area*/
            bool IfShowAttahArea;
            if (countOfAttachmentShown > 0)
            {
                IfShowAttahArea = true;
            }
            else
            {
                if (IfAttachmentAreaDisplay())
                    IfShowAttahArea = true;
                else
                    IfShowAttahArea = false;
            }
            ///*If Show Attach Area But No Allow Attach Permission*/
            //if (IfShowAttahArea && !IfAttachmentAreaDisplay())
            //    IfShowAttaachAreaButNoAllowAttachPermission = true;
            //else
            //    IfShowAttaachAreaButNoAllowAttachPermission = false;
            return IfShowAttahArea;
        }
        private void AttachmentAreaInit(int countOfAttachmentShown)
        {
            if (IfShowAttachAreaInit(countOfAttachmentShown) == false)
                trAttachment.Visible = false;
            /*Attach Upload Area*/
            if (!IfAttachmentUploadControlDisplay())
                file.Visible = false;
        }
        private void HtmlControlInit()
        {
            if (!this.IfContentEditorDisplay())
                HTMLEditor.Mode = "text";
            else
                HTMLEditor.Mode = "bandbyidorname";

            if (HtmlEditorInsertImageButtonDisplay())
            {
                HTMLEditor.IfAllowInsertImage = true;
            }
            else
            {
                HTMLEditor.IfAllowInsertImage = false;
            }
            if (HtmlEditorInsertLinkButtonDisplay())
            {
                HTMLEditor.IfAllowInsertLink = true;
            }
            else
            {
                HTMLEditor.IfAllowInsertLink = false;
            }
            if (HtmlEditorRemoveLinkButtonDisplay())
            {
                HTMLEditor.IfAllowInsertLink = true;
            }
            else
            {
                HTMLEditor.IfAllowInsertLink = false;
            }
        }
        #endregion

        #region Page Property
        private enum EnumPostSettings { Normal, NeedRelay, NeedPay };
        private EnumPostSettings PostSetting { get; set; }
        public Guid uniqueGuid { get { return (Guid)(ViewState["guid"]); } set { ViewState["guid"] = value; } }
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
        public string IfShowPayScoreText
        {
            get
            {
                if (!this.radioPay.Checked)
                    return "display: none";
                else
                    return "";
            }
        }

        public int TopicId { get { return Convert.ToInt32(ViewState["topicId"]); } }
        public int PostId { get { return Convert.ToInt32(ViewState["postId"]); } }
        public int ForumId
        {
            get { return Convert.ToInt32(ViewState["froumId"]); }
            set { ViewState["froumId"] = value; }
        }
        /*For Attachment*/
        public List<int> LastAttachsIds { get; set; }
        public List<int> LastAttachsScoresList { get; set; }
        public List<string> LastAttachsDescriptionsList { get; set; }
        /*For Poll Options*/
        public int PollOptionsCount { get; set; }//{ get{return Convert.ToInt32(ViewState["PollOptionsCount"]);} set{ViewState["PollOptionsCount"]=value;} }
        public List<int> LastPollOptionIds { get; set; }
        public List<string> LastPollOPtionTexts { get; set; }
        public bool IfAnnoucement { get; set; }
        /*Deleted Attachment Ids*/
        public List<int> AttachDeletedIds
        {
            get 
            {
                if (ViewState["AttachDeletedIds"] == null)
                    return new List<int>();
                else
                    return (ViewState["AttachDeletedIds"] as List<int>); 
            }
            set { ViewState["AttachDeletedIds"] = value; }
        }
        /*Deleted PollOptions Ids*/
        public List<int> OptionsDeletedIds
        {
            get
            {
                if (ViewState["OptionsDeletedIds"] == null)
                    return new List<int>();
                else
                    return (ViewState["OptionsDeletedIds"] as List<int>);
            }
            set { ViewState["OptionsDeletedIds"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    this.CheckQueryString("postId");
                    ViewState["postId"] = Convert.ToInt32(Request.QueryString["postId"]);

                    SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                    PostWithPermissionCheck post = PostProcess.GetPostByPostId(this.SiteId, this.UserOrOperatorId, this.IfOperator, Convert.ToInt32(ViewState["postId"]));
                    Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Topic_BrowerTitleEditTopic], System.Web.HttpUtility.HtmlEncode(post.Subject), siteSetting.SiteName);

                    this.Page.SetFocus(btnSubmit);

                    /*2.0*/
                    bool ifAnnoucement;
                    TopicBase topicBase = TopicProcess.CreateTopic(this.UserOrOperatorId, SiteId, post.TopicId, out ifAnnoucement);
                    this.IfAnnoucement = ifAnnoucement;
                    if (!ifAnnoucement)
                    {
                        this.ForumId = (topicBase as TopicWithPermissionCheck).ForumId;
                    }
                    else
                    {
                        this.ForumId = Convert.ToInt32(WebUtility.GetAppSetting("forumId"));
                        this.trAdvancedTitle.Visible = false;
                        this.trAttachment.Visible = false;
                    }

                    CheckIfViewEditTopicPageInit();

                    HtmlControlInit();

                    CheckPostStatusWhenEditTopicOrPost(post);

                    if (!IsPostBack)
                    {
                        uniqueGuid = Guid.NewGuid();
                        txtSubject.MaxLength = ForumDBFieldLength.Topic_subjectFieldLength;
                        //HTMLEditor.MaxLength = ForumDBFieldLength.Topic_contentFieldLength;

                        txtSubject.Text = post.Subject;
                        HTMLEditor.Text = post.Content;

                        if (post.IfTopic == true)
                            lblEdit.Text = Proxy[EnumText.enumForum_Topic_LabelEditTopic];
                        else
                            lblEdit.Text = Proxy[EnumText.enumForum_Topic_LabelEditPost];

                        this.btnUpload.Text = Proxy[EnumText.enumForum_EditTopicOrPost_ButtonUpload];
                        btnSubmit.Visible = true;
                        btnCancel.Visible = true;
                        btnSubmit.Text = Proxy[EnumText.enumForum_Topic_ButtonSubmit];
                        btnCancel.Text = Proxy[EnumText.enumForum_Topic_ButtonCancel];
                        RequiredtxtSubject.ErrorMessage = Proxy[EnumText.enumForum_Topic_ErrorSubjectRequired];

                        ViewState["ifTopic"] = post.IfTopic;
                        ViewState["topicId"] = post.TopicId;
                        ViewState["url"] = string.Format("~/Topic.aspx?topicId={0}&siteId={1}&forumId={2}", post.TopicId, SiteId, this.ForumId);

                        PageInit(topicBase, post);
                    }

                    
                }
                catch (Exception exp)
                {
                    LogHelper.WriteExceptionLog(exp);

                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageEditTopicOrPostErrorLoading] + exp.Message;

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
                    String subject = txtSubject.Text;
                    String content = HTMLEditor.Text;

                    if (Convert.ToBoolean(ViewState["ifTopic"]))
                    {
                        bool ifSetDeadline; DateTime startDate; DateTime endDate;
                        GetPollsInfor(out ifSetDeadline, out startDate, out endDate);
                        GetLastAttahmentsInfor(-1);

                        TopicProcess.UpdateTopic(this.SiteId, this.UserOrOperatorId, this.IfOperator,
                            Convert.ToInt32(ViewState["topicId"]), subject, content,
                            ifSetDeadline, endDate,
                            this.LastAttachsIds.ToArray<int>(), this.LastAttachsScoresList.ToArray<int>(),
                            this.LastAttachsDescriptionsList.ToArray<string>(),
                            this.AttachDeletedIds.ToArray<int>()
                            );

                    }
                    else
                    {
                        if (!this.IfAnnoucement)
                        {
                            GetLastAttahmentsInfor(-1);
                            PostProcess.UpdatePost(this.SiteId, this.UserOrOperatorId, this.IfOperator,
                                Convert.ToInt32(ViewState["postId"]), subject, content,
                                this.LastAttachsIds.ToArray<int>(), this.LastAttachsScoresList.ToArray<int>(),
                                this.LastAttachsDescriptionsList.ToArray<string>(), ForumId,
                                this.AttachDeletedIds.ToArray<int>());
                        }
                        else
                        {
                            PostProcess.UpdatePost(SiteId, this.UserOrOperatorId, PostId, subject, content, ForumId);
                        }
                    }

                    Response.Redirect(Convert.ToString(ViewState["url"]) + "&postId=" + Convert.ToInt32(ViewState["postId"]) + "&goToPost=true&a=1#Post" + Convert.ToInt32(ViewState["postId"]), false);
                }
               
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageEditTopicOrPostErrorEdit] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);

                    //string script = string.Format("<script>alert(\"" + Proxy[EnumText.enumForum_Topic_PageEditTopicOrPostErrorEdit] + "{0}\");</script>", exp.Message);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);

                    this.IfError = true;
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Convert.ToString(ViewState["url"]), false);
        }

        /*2.0*/

        private void PageInit(TopicBase topic, PostWithPermissionCheck post)
        {
           
            bool ifTopic = post.IfTopic;
            AttachmentProcess.DeleteTempAttachmentsOfUser(this.UserOrOperatorId, SiteId, this.UserOrOperatorId, EnumAttachmentType.AttachToPost);
            /*Add js*/
            this.txtMulitiple.Attributes.Add("onkeyup", string.Format("TextKeyUp('{0}');", txtMulitiple.ClientID));
            this.file.Attributes.Add("onchange", string.Format("document.getElementById('{0}').click();", btnUpload.ClientID));
            this.btnUpload.Style.Value = "display:none";

            if (ifTopic)
            {
                /*Init Topic's Post Setttings*/
                PostSettingsInit(topic as TopicWithPermissionCheck);
                /*Init Poll Creation*/
                PollCreationInit(topic as TopicWithPermissionCheck);
                /*Init Poll Options List*/
                RefreashPollOptionsListData();
            }
            else
            {
                /**/
                this.trpollcreation.Visible = false;
                this.trpostsetting.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
            /*Init Attachments List*/
            int countOfAttachmentsShown;
            RefreashAttachmentsData(out countOfAttachmentsShown);
            AttachmentAreaInit(countOfAttachmentsShown);
        }
        private void PostSettingsInit(TopicWithPermissionCheck topic)
        {
            if (topic.IfReplyRequired)
                this.rdReply.Checked = true;
            else if (!topic.IfPayScoreRequired && !topic.IfReplyRequired)
                this.rdNormal.Checked = true;
            else
            {
                this.radioPay.Checked = true;
            }
        }
        private void PollCreationInit(TopicWithPermissionCheck topic)
        {
            Poll poll = PollProcess.GetPollByTopicId(
                this.UserOrOperatorId, SiteId, topic.TopicId);
            this.txtMulitiple.Text = poll.MaxChoices.ToString();
            this.chkPollDate.Checked = poll.IfSetDeadline;
            this.txtPollDate.Text = poll.EndDate.ToString("MM-dd-yyyy");
            this.chkPollCreation.Checked = topic.IfContainsPoll;
            if (!topic.IfContainsPoll)
                trpollcreation.Visible = false;
        }
        private void RefreashPollOptionsListData()
        {
            PollOption[] Options = PollProcess.GetPollOptionsByTopicId(
                this.UserOrOperatorId, SiteId, TopicId);
            string text = "";
            foreach(var option in Options)
            {
               text += option.OptionText + "\n";
            }
            this.tbPollOptions.Value = text;
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
      
        private void RefreashAttachmentsData(out int countOfAttachmentShown)
        {
            AttachmentWithPermissionCheck[] atatchmentsNotDeleted = GetAttachmentsData();
            this.rptAttachments.DataSource = atatchmentsNotDeleted;
            this.rptAttachments.DataBind();

            countOfAttachmentShown = atatchmentsNotDeleted.Length;
        }

        private AttachmentWithPermissionCheck[] GetAttachmentsData()
        {
            /*Get Data From DataBase*/
            List<AttachmentWithPermissionCheck> attachments = new List<AttachmentWithPermissionCheck>();
            AttachmentWithPermissionCheck[] attachmentsOfPost = AttachmentProcess.GetAllAttachmentsOfPost(
                this.UserOrOperatorId, SiteId, PostId);
            AttachmentWithPermissionCheck[] attachmentsOfUser = AttachmentProcess.GetTempAttachmentsOfUser(
                this.UserOrOperatorId, SiteId, this.UserOrOperatorId, uniqueGuid, EnumAttachmentType.AttachToPost);
            attachments.AddRange(attachmentsOfPost);
            attachments.AddRange(attachmentsOfUser);
            /*Data Bind*/
            var atatchmentsNotDeleted = from AttachmentWithPermissionCheck attach in attachments
                                        //from int deletedId in this.AttachDeletedIds
                                        where !this.AttachDeletedIds.Contains(attach.Id)
                                        select attach;
            return atatchmentsNotDeleted.ToArray<AttachmentWithPermissionCheck>();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    int attachmentsOfPostCount = GetAttachmentsData().Length;
                    GetLastAttahmentsInfor(-1);
                    /*Add Attachment to DataBase*/
                    if (this.file.FileBytes.Length != 0)
                    {
                        string name = this.file.FileName;
                        int attachId = AttachmentProcess.AddAttachment(this.UserOrOperatorId, SiteId, ForumId, attachmentsOfPostCount,
                            -1, this.UserOrOperatorId, this.file.FileBytes.Length
                            , this.file.FileBytes, false, 0, name, "", uniqueGuid, EnumAttachmentType.AttachToPost);
                        this.LastAttachsIds.Add(attachId);
                        this.LastAttachsScoresList.Add(0);
                        this.LastAttachsDescriptionsList.Add("");
                    }
                    int count = 0;
                    RefreashAttachmentsData(out count);
                }
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_EditTopicOrPost_ErrorUploadingAttachment] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);

                    this.IfError = true;
                }
            }
        }

        #region Attachments Repeate Event
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
                    if (!IsPostBack)
                        tbScore.Text = attach.Score.ToString();
                    else
                    {
                        if (this.LastAttachsScoresList.Count - 1 >= e.Item.ItemIndex)
                            tbScore.Text = LastAttachsScoresList[e.Item.ItemIndex].ToString();
                        else
                            tbScore.Text = "0";/*default*/
                    }
                    tbScore.Attributes.Add("onkeyup", string.Format("TextKeyUp('{0}');", tbScore.ClientID));
                    TextBox tbDescription = e.Item.FindControl("tbDescription") as TextBox;
                    tbDescription.MaxLength = ForumDBFieldLength.Attachment_descriptionFieldLength;
                    if (!IsPostBack)
                        tbDescription.Text = attach.Description;
                    else
                    {
                        if (this.LastAttachsDescriptionsList.Count - 1 >= e.Item.ItemIndex)
                            tbDescription.Text = LastAttachsDescriptionsList[e.Item.ItemIndex];
                        else
                            tbDescription.Text = "";/*default*/
                    }

                    (e.Item.FindControl("rfvDownloadScore") as RequiredFieldValidator).ControlToValidate = tbScore.ID;
                    (e.Item.FindControl("cvDownloadScore") as CompareValidator).ControlToValidate = tbScore.ID;

                    if (!IfAttachmentAreaDisplay())//show attachment but no allow attach permission
                    {
                        tbDescription.ReadOnly = true;
                        tbScore.ReadOnly = true;
                        e.Item.FindControl("imgDelete").Visible = false;
                    }
                }
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_EditTopicOrPost_ErrorLoadingAttachment] + exp.Message;
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
                            List<int> attachIds = this.AttachDeletedIds;
                            attachIds.Add(attachmentId);
                            attachIds.Distinct();
                            this.AttachDeletedIds = attachIds;
                        }
                        catch (Exception exp)
                        {
                            this.lblMessage.Text = Proxy[EnumText.enumForum_EditTopicOrPost_ErrorDeletingAttachment] + exp.Message;
                        }
                    }
                    int count = 0;
                    RefreashAttachmentsData(out count);
                }
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_EditTopicOrPost_ErrorLoadingAttachment] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);

                    this.IfError = true;
                }
            }
        }
        #endregion

        private void GetPollsInfor(
            out bool ifSetDeadline,
            out DateTime startDate, out DateTime endDate)
        {
            ifSetDeadline = this.chkPollDate.Checked;
            startDate = DateTime.UtcNow;
            if (!string.IsNullOrEmpty(this.txtPollDate.Text.Trim()))
            {
                //end date is yyyy-MM-dd 23:59:59
                endDate = Convert.ToDateTime(this.txtPollDate.Text.Trim()).AddDays(1).AddSeconds(-1).ToUniversalTime();
            }
            else
                endDate = DateTime.UtcNow;
        }


        private void CheckPostStatusWhenEditTopicOrPost(PostWithPermissionCheck post)
        {
            if (post.ModerationStatus == EnumPostOrTopicModerationStatus.Rejected
                && !UserPermissionInForum.IfAdmin
                && !UserPermissionInForum.IfModerator)
            {
                ExceptionHelper.ThrowForumPostModerationStautsIsRejected();
            }
            if (AbuseProcess.GetPostAbusedStuats(this.UserOrOperatorId,SiteId,PostId)
                == EnumPostAbuseStatus.AbusedAndApproved)
            {
                ExceptionHelper.ThrowForumPostAbuseStautsIsAbusedAndApprovedException();
            }
        }

    }
}
