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
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.UI.AdminPanel;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Language;

namespace Forum.UI.AdminPanel.Drafts
{
    public partial class DraftEdit : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        public Guid uniqueGuid { get { return (Guid)(ViewState["guid"]); } set { ViewState["guid"] = value; } }
        /*For Attachment*/
        public List<int> LastAttachsIds { get; set; }
        public List<int> LastAttachsScoresList { get; set; }
        public List<string> LastAttachsDescriptionsList { get; set; }

        
        string ErrorLoad;
        string ErrorLoadTopicAndPost;
        string ErrorLoadPost;
        string ErrorSaveDraft;
        string ErrorPostDraft;
        string ErrorUploadAttachment;
        string ErrorLoadAttachment;
        string ErrorSubmit;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Topic_PageEditDraftErrorLoadpage];
                ErrorLoadTopicAndPost=Proxy[EnumText.enumForum_Topic_PageEditDraftErrorLoadTopicAndPost];
                ErrorLoadPost = Proxy[EnumText.enumForum_Topic_PageEditDraftErrorLoadPost];
                ErrorSaveDraft = Proxy[EnumText.enumForum_Topic_ErrorSaveDraft];
                ErrorPostDraft = Proxy[EnumText.enumForum_Topic_ErrorPostDraft];
                ErrorUploadAttachment = Proxy[EnumText.enumForum_Topic_PageEditDraftErrorUploadAttachment];
                ErrorLoadAttachment = Proxy[EnumText.enumForum_Topic_PageEditDraftErrorLoadAttachment];
                ErrorSubmit = Proxy[EnumText.enumForum_Topic_PageAddTopicErrorSubmit];
                this.lblTitle.Text = Proxy[EnumText.enumForum_Topic_TitleDraftEdit];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Topic_SubtitleDraftEdit];
                btnSave.Text = Proxy[EnumText.enumForum_Topic_ButtonSave];
                btnPost.Text = Proxy[EnumText.enumForum_Topic_ButtonPost];
                rfvSubject.ErrorMessage = Proxy[EnumText.enumForum_Topic_ErrorSubjectRequired];
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + exp.Message;
            }
        }

        public int TopicId
        {
            get
            {
                CheckQueryString("topicId");
                return Convert.ToInt32(Request.QueryString["topicId"]);
            }
        }

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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CheckQueryString("topicId");
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumDraftManage);
                Master.Page.Title = Proxy[EnumText.enumForum_Topic_TitleDraftEdit];
                if (!IsPostBack)
                {
                    uniqueGuid = Guid.NewGuid();
                    int topicId = Convert.ToInt32(Request.QueryString["topicId"]);
                    aspnetPager.PageIndex = 0;
                    ViewState["Id"] = topicId;
                    this.txtSubject.MaxLength = ForumDBFieldLength.Draft_subjectFieldLength;
                    this.HTMLEditor1.MaxLength = ForumDBFieldLength.Draft_contentFieldLength;        
                    if (Request.QueryString["pageindex"] != null && Request.QueryString["pagesize"] != null)
                    {
                        aspnetPager.PageIndex = Convert.ToInt32(Request.QueryString["pageindex"]);
                        aspnetPager.PageSize = Convert.ToInt32(Request.QueryString["pagesize"]);
                    }
                    RefreshData();
                    RefreashAttachmentsData();
                    BindDraft();
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }

        }

        private void BindDraft()
        {
            int topicId = Convert.ToInt32(ViewState["Id"]);
            Draft draft = DraftProcess.GetDraftByTopicId(SiteId, UserOrOperatorId, IfOperator, topicId);
            txtSubject.Text = draft.Subject;
            HTMLEditor1.Text = draft.Content;
            draftEditInfo.Text = "<br /><span class='DraftEditLabel'>" + string.Format(Proxy[EnumText.enumForum_Topic_fieldEditInformation], System.Web.HttpUtility.HtmlEncode(draft.LastUpdateOperatorName), DateTimeHelper.DateFormate(draft.LastUpdateTime)) + "</span>";

        }



        private void RefreshData()
        {
            try
            {
                int topicId = Convert.ToInt32(ViewState["Id"]);
                int recordCount = PostProcess.GetCountOfPostsByTopicId(SiteId, UserOrOperatorId, IfOperator, topicId);
                aspnetPager.CWCDataBind(repeaterTopicAndPosts,
                    PostProcess.GetPostsByTopicIdAndPaging(SiteId, UserOrOperatorId, topicId,
                    aspnetPager.PageIndex + 1, aspnetPager.PageSize),
                    recordCount);

            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoadTopicAndPost + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void aspnetPager_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
        {
            try
            {
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoadPost + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void aspnetPager_ChangePageSize(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.ChangePageSizeEventArgs ce)
        {
            try
            {
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoadPost + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int topicId = Convert.ToInt32(ViewState["Id"]);
                string subject = this.txtSubject.Text;
                string content = this.HTMLEditor1.Text;
                aspnetPager.PageIndex = 0;
                GetLastAttahmentsInfor(-1);
                DraftProcess.SaveDraft(SiteId, UserOrOperatorId,
                    IfOperator, TopicId, subject, content,
                    this.LastAttachsIds.ToArray<int>(), this.LastAttachsScoresList.ToArray<int>(),
                    this.LastAttachsDescriptionsList.ToArray<string>(),
                    this.AttachDeletedIds.ToArray<int>());
                Response.Redirect("DraftList.aspx?siteid=" + SiteId, false);
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorSaveDraft + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                int topicId = Convert.ToInt32(ViewState["Id"]);
                string subject = this.txtSubject.Text;
                string content = this.HTMLEditor1.Text;
                aspnetPager.PageIndex = 0;
                GetLastAttahmentsInfor(-1);
                //PostProcess.AddPost(,SiteId, UserOrOperatorId, IfOperator, TopicId, false, subject, content,
                //            this.LastAttachsIds.ToArray<int>(), this.LastAttachsScoresList.ToArray<int>(),
                //            this.LastAttachsDescriptionsList.ToArray<string>());
                Response.Redirect("DraftList.aspx?siteid=" + SiteId, false);
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorPostDraft + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }


        #region Attachments
        protected void btnUpload_Click(object sender, EventArgs e)
        {

            try
            {
                GetLastAttahmentsInfor(-1);
                ///*Add Attachment to DataBase*/
                //if (this.file.FileBytes.Length != 0)
                //{
                //    string name = this.file.FileName + "_" + DateTime.UtcNow.ToString("yyyyMMddhhmmss");
                //    int attachId = AttachmentProcess.AddAttachment(this.UserOrOperatorId, SiteId,
                //        -1, this.UserOrOperatorId, this.file.FileBytes.Length
                //        , this.file.FileBytes, false, 0, name, "", uniqueGuid, EnumAttachmentType.AttachToPost);
                //    this.LastAttachsIds.Add(attachId);
                //    this.LastAttachsScoresList.Add(0);
                //    this.LastAttachsDescriptionsList.Add("");
                //}
                string content = this.HTMLEditor1.Text;
                string subject = this.txtSubject.Text;
                RefreshData();
                RefreashAttachmentsData();
                this.HTMLEditor1.Text = content;
                this.txtSubject.Text = subject;
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>window.location='#divUploadTempAttachmentList';</script>");
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorUploadAttachment+ exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }

        }
        private void RefreashAttachmentsData()
        {
            List<AttachmentWithPermissionCheck> attachments = new List<AttachmentWithPermissionCheck>();
            /*Draft Attachment*/
            TopicWithPermissionCheck tmpTopic = TopicProcess.GetTopicByTopicId(this.SiteId, this.UserOrOperatorId,
                TopicId);
            if (tmpTopic.IfHasDraft)
            {
                AttachmentWithPermissionCheck[] draftAttachements = AttachmentProcess.GetDraftAttachmentsOfTopic(
                    UserOrOperatorId, SiteId, TopicId);
                attachments.AddRange(draftAttachements);
            }
            /*Get Data From DataBase*/
            AttachmentWithPermissionCheck[] attachmentsTemp = AttachmentProcess.GetTempAttachmentsOfUser(
                this.UserOrOperatorId, SiteId, this.UserOrOperatorId, uniqueGuid, EnumAttachmentType.AttachToPost);
            attachments.AddRange(attachmentsTemp);
            /*Data Bind*/
            var attachmentsNotDeleted = from attach in attachments
                                        where !this.AttachDeletedIds.Contains(attach.Id)
                                        select attach;
            this.rptPostAttachmentsList.DataSource = attachmentsNotDeleted;
            this.rptPostAttachmentsList.DataBind();
        }

        protected void rptPostAttachmentsList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            try
            {
                AttachmentWithPermissionCheck attach = e.Item.DataItem as
                    AttachmentWithPermissionCheck;
                HiddenField hdId = e.Item.FindControl("hdAttachId") as HiddenField;
                hdId.Value = attach.Id.ToString();
                TextBox tbScore = e.Item.FindControl("tbScore") as TextBox;
                if (!IsPostBack)
                {
                    tbScore.Text = attach.Score.ToString();
                }
                else
                {
                    if (this.LastAttachsScoresList.Count - 1 >= e.Item.ItemIndex)
                        tbScore.Text = LastAttachsScoresList[e.Item.ItemIndex].ToString();
                    else
                        tbScore.Text = "0";/*default*/
                }
                tbScore.Attributes.Add("onkeydown", string.Format("TextKeyDown('{0}');", tbScore.ClientID));
                TextBox tbDescription = e.Item.FindControl("tbDescription") as TextBox;
                if (!IsPostBack)
                {
                    tbDescription.Text = attach.Description;
                }
                else
                {
                    if (this.LastAttachsDescriptionsList.Count - 1 >= e.Item.ItemIndex)
                        tbDescription.Text = LastAttachsDescriptionsList[e.Item.ItemIndex];
                    else
                        tbDescription.Text = "";/*default*/
                }

                (e.Item.FindControl("revDownLoadScore") as RegularExpressionValidator).ControlToValidate = tbScore.ID;
                (e.Item.FindControl("rfvDownloadScore") as RequiredFieldValidator).ControlToValidate = tbScore.ID;
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoadAttachment+ exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }

        }

        protected void rptPostAttachmentsList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            try
            {
                int attachmentId = Convert.ToInt32(e.CommandArgument);
                GetLastAttahmentsInfor(attachmentId);
                if (e.CommandName == "Delete")
                {
                    //AttachmentProcess.DeleteAttachment(this.UserOrOperatorId, SiteId, attachmentId);
                    List<int> attachIds = this.AttachDeletedIds;
                    attachIds.Add(attachmentId);
                    attachIds.Distinct();
                    this.AttachDeletedIds = attachIds;
                }
                string content = this.HTMLEditor1.Text;
                string subject = this.txtSubject.Text;
                RefreshData();
                RefreashAttachmentsData();
                this.HTMLEditor1.Text = content;
                this.txtSubject.Text = subject;
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>window.location='#divUploadTempAttachmentList';</script>");
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorSubmit + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }

        }
        private void GetLastAttahmentsInfor(int deletedAttachmentId)
        {
            List<int> AttachIds = new List<int>();
            List<int> Scores = new List<int>();
            List<string> descriptions = new List<string>();
            foreach (RepeaterItem ri in this.rptPostAttachmentsList.Items)
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
        #endregion
    }
}
