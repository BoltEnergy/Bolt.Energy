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
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.WebControls;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Language;
using Com.Comm100.Forum.UI.Common;
using System.Web.UI.HtmlControls;
using Com.Comm100.Framework.Database;




namespace Forum.UI
{
    public partial class TopicTest : ForumBasePage
    {
        #region Permission Check
        private bool CheckIfModerator()
        {
            if (this.IfGuest)
                return false;
            return this.UserPermissionInForum.IfModerator;
        }
        #endregion

        #region Permission Check Display
        private void CheckViewTopicOrAnnoucement()
        {
            if (!this.UserPermissionInForum.IfAllowViewForum || !this.UserPermissionInForum.IfAllowViewTopicOrPost)
            {
                ExceptionHelper.ThrowForumUserHaveNoPermissionToVisit();
            }

        }
        private bool IfNewTopicButtonDisplay()
        {
            if (IfGuest)//guest user can view,clcik go to login page
                return true;
            if (this.UserPermissionInForum.IfAllowViewForum && this.UserPermissionInForum.IfAllowViewTopicOrPost &&
                this.UserPermissionInForum.IfAllowPostTopicOrPost)
                return true;
            else
                return false;
        }
        private bool IfPostReplyButtonDisplay()
        {
            if (this.UserPermissionInForum.IfAllowViewForum && this.UserPermissionInForum.IfAllowViewTopicOrPost &&
                this.UserPermissionInForum.IfAllowPostTopicOrPost)
                return true;
            else
                return false;
        }
        private bool IfEditButtonDisplay()
        {
            if (this.UserPermissionInForum.IfAllowViewForum && this.UserPermissionInForum.IfAllowViewTopicOrPost &&
               this.UserPermissionInForum.IfAllowPostTopicOrPost)
                return true;
            else
                return false;
        }
        private bool IfQuoteButtonDisplay()
        {
            if (this.UserPermissionInForum.IfAllowViewForum && this.UserPermissionInForum.IfAllowViewTopicOrPost &&
              this.UserPermissionInForum.IfAllowPostTopicOrPost)
                return true;
            else
                return false;
        }

        #region Quick Replay
        private bool IfQuickReplayButtonDisplay()
        {
            if (IfGuest)//guest user can view,click go to login
                return true;
            if (this.UserPermissionInForum.IfAllowViewForum && this.UserPermissionInForum.IfAllowViewTopicOrPost &&
              this.UserPermissionInForum.IfAllowPostTopicOrPost)
                return true;
            else
                return false;
        }
        private bool IfQuickReplay_ContentEditorDisplay()
        {
            if (this.UserPermissionInForum.IfAllowViewForum && this.UserPermissionInForum.IfAllowViewTopicOrPost &&
              this.UserPermissionInForum.IfAllowPostTopicOrPost)
                return true;
            else
                return false;
        }
        private bool IfQuickReplay_ContentEditorInsertLinkButtonDisplay()
        {
            if (this.UserPermissionInForum.IfAllowViewForum && this.UserPermissionInForum.IfAllowViewTopicOrPost &&
             this.UserPermissionInForum.IfAllowPostTopicOrPost
             && this.UserPermissionInForum.IfAllowURL)
                return true;
            else
                return false;
        }
        private bool IfQuickReplay_ContentEditorRemoveLinkButtonDisplay()
        {
            if (this.UserPermissionInForum.IfAllowViewForum && this.UserPermissionInForum.IfAllowViewTopicOrPost &&
             this.UserPermissionInForum.IfAllowPostTopicOrPost
             && this.UserPermissionInForum.IfAllowURL)
                return true;
            else
                return false;
        }
        private bool IfQuickReplay_ContentEditorInsertImageButtonDisplay()
        {
            if (this.UserPermissionInForum.IfAllowViewForum && this.UserPermissionInForum.IfAllowViewTopicOrPost &&
             this.UserPermissionInForum.IfAllowPostTopicOrPost
             && this.UserPermissionInForum.IfAllowInsertImage)
                return true;
            else
                return false;
        }
        #endregion

        #region Replay
        private bool IfReplayAreaDisplay()
        {
            if (this.UserPermissionInForum.IfAllowViewForum && this.UserPermissionInForum.IfAllowViewTopicOrPost &&
              this.UserPermissionInForum.IfAllowPostTopicOrPost)
                return true;
            else
                return false;
        }
        protected bool IfReplay_ContentEditorDisplay()
        {
            if (this.UserPermissionInForum.IfAllowViewForum && this.UserPermissionInForum.IfAllowViewTopicOrPost &&
              this.UserPermissionInForum.IfAllowPostTopicOrPost)
                return true;
            else
                return false;
        }
        private bool IfReplay_ContentEditorInsertLinkButtonDisplay()
        {
            if (this.UserPermissionInForum.IfAllowViewForum && this.UserPermissionInForum.IfAllowViewTopicOrPost &&
             this.UserPermissionInForum.IfAllowPostTopicOrPost
             && this.UserPermissionInForum.IfAllowURL)
                return true;
            else
                return false;
        }
        private bool IfReplay_ContentEditorRemoveLinkButtonDisplay()
        {
            if (this.UserPermissionInForum.IfAllowViewForum && this.UserPermissionInForum.IfAllowViewTopicOrPost &&
             this.UserPermissionInForum.IfAllowPostTopicOrPost
             && this.UserPermissionInForum.IfAllowURL)
                return true;
            else
                return false;
        }
        private bool IfReplay_ContentEditorInsertImageButtonDisplay()
        {
            if (this.UserPermissionInForum.IfAllowViewForum && this.UserPermissionInForum.IfAllowViewTopicOrPost &&
             this.UserPermissionInForum.IfAllowPostTopicOrPost
             && this.UserPermissionInForum.IfAllowInsertImage)
                return true;
            else
                return false;
        }
        private bool IfReplay_AttachmentAreaDisplay()
        {
            if (this.UserPermissionInForum.IfAllowViewForum && this.UserPermissionInForum.IfAllowViewTopicOrPost &&
             this.UserPermissionInForum.IfAllowPostTopicOrPost && this.UserPermissionInForum.IfAllowAttachment)
                return true;
            else
                return false;
        }
        private bool IfReply_AttachmentUploadControlDisplay()
        {
            return this.UserPermissionInForum.IfAllowAttachment;
        }
        #endregion
        #endregion

        #region Page Display Init
        private void Reply_AttachmentAreaInitIfNotOperatorOrNotHaveDraft()
        {
            this.trUploadAttachmentList.Visible = this.UserPermissionInForum.IfAllowAttachment;
            /*Attachment Upload Control*/
            this.file.Visible = IfReply_AttachmentUploadControlDisplay();
        }
        private void Reply_AttachmentAreaInitWhenOperatorHaveDarft(int countOfAttachmentShown)
        {
            /*Attach Area*/
            bool IfShowAttahArea;
            if (countOfAttachmentShown > 0)
            {
                IfShowAttahArea = true;
            }
            else
            {
                if (IfReplay_AttachmentAreaDisplay())
                    IfShowAttahArea = true;
                else
                    IfShowAttahArea = false;
            }
            if (IfShowAttahArea == false)
                trUploadAttachmentList.Visible = false;
            /*Attachment Upload Control*/
            this.file.Visible = IfReply_AttachmentUploadControlDisplay();
        }
        private void Reply_HtmlControlInit()
        {
            if (!this.IfReplay_ContentEditorDisplay())
                HTMLEditor.Mode = "text";
            else
                HTMLEditor.Mode = "bandbytype";

            if (this.IfReplay_ContentEditorInsertImageButtonDisplay())
            {
                this.HTMLEditor.IfAllowInsertImage = true;
            }
            else
            {
                this.HTMLEditor.IfAllowInsertImage = false;
            }
            if (this.IfReplay_ContentEditorInsertLinkButtonDisplay())
            {
                this.HTMLEditor.IfAllowInsertLink = true;
            }
            else
            {
                this.HTMLEditor.IfAllowInsertLink = false;
            }
            if (this.IfReplay_ContentEditorRemoveLinkButtonDisplay())
            {
                this.HTMLEditor.IfAllowInsertLink = true;
            }
            else
            {
                this.HTMLEditor.IfAllowInsertLink = false;
            }
        }
        private void Quickreply_HtmlControlInit()
        {
            if (!this.IfQuickReplay_ContentEditorDisplay())
                HTMLEditor.Mode = "text";
            else
                HTMLEditor.Mode = "bandbytype";

            if (this.IfQuickReplay_ContentEditorInsertImageButtonDisplay())
            {
                this.HTMLEditor.IfAllowInsertImage = true;
            }
            else
            {
                this.HTMLEditor.IfAllowInsertImage = false;
            }
            if (this.IfQuickReplay_ContentEditorInsertLinkButtonDisplay())
            {
                this.HTMLEditor.IfAllowInsertLink = true;
            }
            else
            {
                this.HTMLEditor.IfAllowInsertLink = false;
            }
            if (this.IfQuickReplay_ContentEditorRemoveLinkButtonDisplay())
            {
                this.HTMLEditor.IfAllowInsertLink = true;
            }
            else
            {
                this.HTMLEditor.IfAllowInsertLink = false;
            }
        }

        // use in Main Page
        private void ButtonOrAreaDisplyWhenSiteIsVisitOnlyInMainPageInit()
        {
            if (IfButtonOrAreaDisplayWhenSiteIsVisitOnly() == false)
            {
                this.hyperLnkNewTopic.Visible = false;
                this.lnkBtnLoggedinReply.Visible = false;
                this.lnkBtnUnLoggedinReply.Visible = false;
                this.imgBtnFavorite.Visible = false;
                this.imgBtnUnFavorite.Visible = false;
                this.panelReplyTable.Visible = false;
            }
        }
        // use in unmark,vote
        private bool IfButtonOrAreaDisplayWhenSiteIsVisitOnly()
        {
            if (this.IfSiteOnlyVisit &&
               !(this.UserPermissionInForum.IfAdmin || this.UserPermissionInForum.IfModerator))
                return false;
            else
                return true;
        }
        //use in Tool Bar
        private void ButtonOrAreaDisplyWhenSiteIsVisitOnlyInToolBarInit(RepeaterItemEventArgs e)
        {
            if (IfButtonOrAreaDisplayWhenSiteIsVisitOnly() == false)
            {
                e.Item.FindControl("PHCloseButton").Visible = false;
                e.Item.FindControl("PHReOpenButton").Visible = false;
                e.Item.FindControl("placeHolderEdit").Visible = false;
                e.Item.FindControl("placeHolderMarkAsAnswer").Visible = false;
                e.Item.FindControl("placeHolderQuote").Visible = false;
                e.Item.FindControl("placeHolderQuickReply").Visible = false;
            }
        }
        #endregion

        #region Page Property
        protected int TopicId
        {
            get { return Convert.ToInt32(ViewState["topicId"]); }
        }

        protected int ForumId
        {
            get
            {
                return Convert.ToInt32(ViewState["forumId"]);
            }
            set { ViewState["forumId"] = value; }
        }

        protected bool GoToPost
        {
            get { return Convert.ToBoolean(ViewState["goToPost"]); }
        }

        public bool IfPollMulitipleChoice { get; set; }

        public int PollMulitipleChoiceCount { get; set; }

        public bool IfShowPollInfor { get; set; }

        public bool IfUserPayTopic { get; set; }

        public bool IfUserRelayTopic { get; set; }

        public bool IfShowAttachmentsList { get; set; }

        public int AllVotesResults { get; set; }

        public int AllVotesHistories { get; set; }

        public bool IfUserHaveVoted { get; set; }

        public bool IfPollDateExpried { get; set; }

        public string TopicSubject { get; set; }

        public int UserALLScores { get; set; }
        /*Attachment List*/
        public List<int> LastAttachsIds { get; set; }
        public List<int> LastAttachsScoresList { get; set; }
        public List<string> LastAttachsDescriptionsList { get; set; }
        /*If Annoucement*/
        public bool IfAnnoucement { get; set; }
        /*If Moved Topic*/
        public bool IfMovedTopic { get; set; }
        public bool IfDeleted { get; set; }
        public Guid uniqueGuid { get { return (Guid)(ViewState["guid"]); } set { ViewState["guid"] = value; } }
        #endregion

        #region Button Show Logic
        public bool IfCanEdit(PostWithPermissionCheck post, TopicBase topicOrAnnoucement)
        {
            bool ifcanEdit = false;
            if (this.UserPermissionInForum.IfAdmin || this.UserPermissionInForum.IfModerator
                || (IfOwnPostAndNoReply(post, topicOrAnnoucement) && !IfTopicClosed()))
            {
                ifcanEdit = true;
            }
            return ifcanEdit;
        }
        public bool IfCanDelete()
        {
            bool ifcanDelete = false;
            if (this.UserPermissionInForum.IfAdmin || this.UserPermissionInForum.IfModerator)
            {
                ifcanDelete = true;
            }
            return ifcanDelete;
        }
        public bool IfCanUnMark(PostWithPermissionCheck post, TopicWithPermissionCheck topic)
        {
            bool ifCanUnMark = false;
            if (this.UserPermissionInForum.IfAdmin || this.UserPermissionInForum.IfModerator
                || IfOwnTopic(topic) && !IfTopicClosed() && !post.IfTopic && post.IfAnswer)
            {
                ifCanUnMark = true;
            }
            return ifCanUnMark;
        }
        public bool IfCanMark(PostWithPermissionCheck post, TopicWithPermissionCheck topic)
        {
            bool ifCanMark = false;
            if (this.UserPermissionInForum.IfAdmin || this.UserPermissionInForum.IfModerator
                || IfOwnTopic(topic) && !IfTopicClosed() && !post.IfTopic && !post.IfAnswer)
            {
                ifCanMark = true;
            }
            return ifCanMark;
        }
        public bool IfCanQuote()
        {
            bool ifCanQuote = false;
            if (this.UserPermissionInForum.IfAdmin || this.UserPermissionInForum.IfModerator
                || !IfTopicClosed())
            {
                ifCanQuote = true;
            }
            return ifCanQuote;
        }
        private bool IfOwnPostAndNoReply(PostWithPermissionCheck post, TopicBase topicOrAnnoucement)
        {
            //bool ifAnnoucement;
            //TopicBase topicOrAnnoucement = TopicProcess.CreateTopic(UserOrOperatorId,SiteId,TopicId, out ifAnnoucement);

            bool ifOwnAndNoReply = false;
            if (this.CurrentUserOrOperator != null)
            {
                if (this.UserOrOperatorId == post.PostUserOrOperatorId &&
                    (post.PostId == topicOrAnnoucement.LastPostId))//topicOrAnnoucement.NumberOfReplies == 0 ||
                {
                    ifOwnAndNoReply = true;
                }
            }
            return ifOwnAndNoReply;
        }

        private bool IfOwnTopic(TopicWithPermissionCheck tmpTopic)
        {
            if (this.IfAnnoucement)
                return false;

            bool ifOwnTopic = false;
            if (this.CurrentUserOrOperator != null)
            {
                if (this.UserOrOperatorId == tmpTopic.PostUserOrOperatorId)
                {
                    ifOwnTopic = true;
                }
            }
            return ifOwnTopic;
        }
        #endregion
        private static int PromotedVoteValue;
        protected override void InitLanguage()
        {
            base.InitLanguage();
            this.imgBtnFavorite.Title = Proxy[EnumText.enumForum_Topic_ToolTipFavorite];
            this.imgBtnUnFavorite.Title = Proxy[EnumText.enumForum_Topic_ToolTipUnfavorite];
            this.lnkBtnFeatured.Title = Proxy[EnumText.enumForum_Topic_Page_Button_Featured];
            this.lnkBtnUnFeatured.Title = Proxy[EnumText.enumForum_Topic_Page_Button_UnFeatured];
            this.btnUpload.Text = Proxy[EnumText.enumForum_Topic_ButtonUpload];
            this.btnClose.Text = Proxy[EnumText.enumForum_Topic_ButtonClose];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    CheckQueryString("topicId");
                    //ViewState["forumId"] = Convert.ToInt32(Request.QueryString["forumId"]);
                    ViewState["topicId"] = Convert.ToInt32(Request.QueryString["topicId"]);
                    ViewState["goToPost"] = false;
                    /*2.0*/
                    bool ifAnnoucement;
                    TopicBase topicBase = TopicProcess.CreateTopic(this.UserOrOperatorId, SiteId, TopicId, out ifAnnoucement);
                    this.IfAnnoucement = ifAnnoucement;
                    if (!ifAnnoucement)
                    {
                        TopicWithPermissionCheck topic = topicBase as TopicWithPermissionCheck;
                        this.ForumId = topic.ForumId;
                        this.IfMovedTopic = topic.IfMoveHistory;
                        this.IfDeleted = topic.IfDeleted;
                        if (this.IfMovedTopic)
                        {
                            TopicWithPermissionCheck localTopic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, TopicId);
                            if (!this.UserPermissionInForum.IfAdmin && !this.UserPermissionInForum.IfModerator && localTopic.IfDeleted)
                            {
                                ExceptionHelper.ThrowTopicNotExistException(localTopic.TopicId);
                            }
                        }
                    }
                    else
                    {
                        CheckQueryString("forumId");
                        this.ForumId = Convert.ToInt32(Request.QueryString["forumId"]);
                        this.IfDeleted = false;
                    }
                    if (!this.IfGuest)
                    {
                        UserOrOperator user = UserProcess.GetUserOrOpertorById(SiteId, UserOrOperatorId);
                        this.UserALLScores = user.Score;
                    }

                    CheckViewTopicOrAnnoucement();

                    Reply_HtmlControlInit();

                    #region Image Src
                    this.aspnetPagertop.ItemName = Proxy[EnumText.enumForum_Public_Post];
                    this.aspnetPagertop.ItemsName = Proxy[EnumText.enumForum_Public_Posts];
                    this.aspnetPager.ItemName = Proxy[EnumText.enumForum_Public_Post];
                    this.aspnetPager.ItemsName = Proxy[EnumText.enumForum_Public_Posts];
                    #endregion

                    if (!this.IsPostBack)
                    {
                        uniqueGuid = Guid.NewGuid();
                        string postBackAction = Request.QueryString["action"];

                        if (postBackAction != null)
                        {
                            int postId = Convert.ToInt32(Request.QueryString["postId"]);

                            if (this.IfGuest)
                            {
                                Response.Redirect("~/login.aspx?siteId=" + SiteId, false);
                                return;
                            }

                            #region Post Back Action
                            if (postBackAction == "delete")
                            {
                                try
                                {
                                    PostWithPermissionCheck post = PostProcess.GetPostByPostId(SiteId, UserOrOperatorId, IfOperator, postId);
                                    if (!this.IfAnnoucement)
                                        PostProcess.LogicDeletePostOfTopic(SiteId, UserOrOperatorId, postId);
                                    else
                                        PostProcess.DeletePostOfAnnoucment(SiteId, UserOrOperatorId, ForumId, TopicId, postId);
                                    if (post.IfTopic)
                                        Response.Redirect("~/Forum.aspx?forumId=" + ForumId + "&siteId=" + SiteId, false);
                                    else
                                        RefreashTopicNotAddViewNum(postId);
                                }
                                catch (ExceptionWithCode exp)
                                {
                                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderDeletingPost];
                                    HandleExceptionWithCode(exp, exceptionMethod);
                                }
                                catch (Exception exp1)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorDeletePost] + exp1.Message + "\");</script>");
                                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorDeletingPost] + exp1.Message;
                                    LogHelper.WriteExceptionLog(exp1);
                                    this.IfError = true;
                                }
                            }
                            else if (postBackAction == "mark")
                            {
                                try
                                {
                                    PostProcess.MarkAsAnswer(SiteId, UserOrOperatorId, IfOperator, postId);
                                    RefreashTopicNotAddViewNum(postId);
                                }
                                catch (ExceptionWithCode exp)
                                {
                                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderMarkingPost];
                                    HandleExceptionWithCode(exp, exceptionMethod);
                                }
                                catch (Exception exp1)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorMarkingPost] + exp1.Message + "\");</script>");
                                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorMarkingPost] + exp1.Message;
                                    LogHelper.WriteExceptionLog(exp1);
                                    this.IfError = true;
                                }
                            }
                            else if (postBackAction == "unMark")
                            {
                                try
                                {
                                    //PostWithPermissionCheck tmppost = PostProcess.GetPostByPostId(SiteId, UserOrOperatorId, IfOperator, postId);
                                    //if (tmppost.IfAnswer)
                                    //{
                                    PostProcess.UnMarkAsAnswer(SiteId, UserOrOperatorId, IfOperator, postId);
                                    RefreashTopicNotAddViewNum(postId);
                                    //}
                                    //else
                                    //{
                                    // Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert('" + Proxy[EnumText.enumForum_Topic_AlterPostAlreadyUmmarkd] + "');</script>");
                                    //}
                                }
                                catch (ExceptionWithCode exp)
                                {
                                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderUnmarkingPost];
                                    HandleExceptionWithCode(exp, exceptionMethod);
                                }
                                catch (Exception exp1)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorUnmarkingPost] + exp1.Message + "\");</script>");
                                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorUnmarkingPost] + exp1.Message;
                                    LogHelper.WriteExceptionLog(exp1);
                                    this.IfError = true;
                                }
                            }
                            else if (postBackAction == "poll")
                            {
                                try
                                {
                                    string[] strOptionIds = Convert.ToString(Request.QueryString["optionId"]).Trim(';').Split(';');
                                    List<int> optionIds = new List<int>();
                                    foreach (string strOptionId in strOptionIds)
                                    {
                                        if (strOptionId != "")
                                            optionIds.Add(Convert.ToInt32(strOptionId));
                                    }
                                    PollProcess.VotePollOption(this.UserOrOperatorId, SiteId, ForumId, optionIds.ToArray<int>(), TopicId);
                                    RefreashTopicNotAddViewNum();
                                }
                                catch (ExceptionWithCode exp)
                                {
                                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderPollingTopic];
                                    HandleExceptionWithCode(exp, exceptionMethod);
                                }
                                catch (Exception exp1)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorPollingTopic] + exp1.Message + "\");</script>");
                                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorPollingTopic] + exp1.Message;
                                    LogHelper.WriteExceptionLog(exp1);
                                    this.IfError = true;
                                }
                            }

                            else if (postBackAction == "paytopic")
                            {
                                try
                                {
                                    int topicId = Convert.ToInt32(Request.QueryString["topicId"]);
                                    if (topicId != -1)
                                    {
                                        TopicWithPermissionCheck topic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, topicId);
                                        PayHistroyProcess.AddTopicPayHistroy(this.UserOrOperatorId, SiteId,
                                            topic.TopicId, this.UserOrOperatorId, topic.Score, DateTime.UtcNow);
                                    }
                                    RefreashTopicNotAddViewNum();
                                }
                                catch (ExceptionWithCode exp)
                                {
                                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderPayingTopic];
                                    HandleExceptionWithCode(exp, exceptionMethod);
                                }
                                catch (Exception exp1)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorPayingTopic] + exp1.Message + "\");</script>");
                                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorPayingTopic] + exp1.Message;
                                    LogHelper.WriteExceptionLog(exp1);
                                    this.IfError = true;
                                }
                            }
                            else if (postBackAction == "approvalmoderation")
                            {
                                try
                                {
                                    PostProcess.AcceptPostModeration(SiteId, UserOrOperatorId, ForumId, postId);
                                    RefreashTopicNotAddViewNum(postId);
                                }
                                catch (ExceptionWithCode exp)
                                {
                                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderApprovalingModeration];
                                    HandleExceptionWithCode(exp, exceptionMethod);
                                }
                                catch (Exception exp1)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorApprovalingModeration] + exp1.Message + "\");</script>");
                                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorApprovalingModeration] + exp1.Message;
                                    LogHelper.WriteExceptionLog(exp1);
                                    this.IfError = true;
                                }
                            }
                            else if (postBackAction == "unapprovalmoderation")
                            {
                                try
                                {
                                    PostProcess.RefusePostModeration(SiteId, UserOrOperatorId, ForumId, postId);
                                    RefreashTopicNotAddViewNum(postId);
                                }
                                catch (ExceptionWithCode exp)
                                {
                                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderUnApprovalingModeration];
                                    HandleExceptionWithCode(exp, exceptionMethod);
                                }
                                catch (Exception exp1)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorUnApprovalingModeration] + exp1.Message + "\");</script>");
                                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorUnApprovalingModeration] + exp1.Message;
                                    LogHelper.WriteExceptionLog(exp1);
                                    this.IfError = true;
                                }
                            }
                            else if (postBackAction == "refuseabuse")
                            {
                                try
                                {
                                    AbuseProcess.RefuseAbusesOfPost(this.UserOrOperatorId, SiteId, ForumId, postId);
                                    RefreashTopicNotAddViewNum(postId);
                                }
                                catch (ExceptionWithCode exp)
                                {
                                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderRefusingAbuse];
                                    HandleExceptionWithCode(exp, exceptionMethod);
                                }
                                catch (Exception exp1)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorRefusingAbuse] + exp1.Message + "\");</script>");
                                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorRefusingAbuse] + exp1.Message;
                                    LogHelper.WriteExceptionLog(exp1);
                                    this.IfError = true;
                                }
                            }
                            else if (postBackAction == "approvalabuse")
                            {
                                try
                                {
                                    AbuseProcess.ApproveAbsesOfPost(this.UserOrOperatorId, SiteId, ForumId, postId);
                                    RefreashTopicNotAddViewNum(postId);
                                }
                                catch (ExceptionWithCode exp)
                                {
                                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderApprovalingAbuse];
                                    HandleExceptionWithCode(exp, exceptionMethod);
                                }
                                catch (Exception exp1)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorApprovalingAbuse] + exp1.Message + "\");</script>");
                                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorApprovalingAbuse] + exp1.Message;
                                    LogHelper.WriteExceptionLog(exp1);
                                    this.IfError = true;
                                }
                            }
                            else if (postBackAction == "unban")
                            {
                                try
                                {
                                    int UnBanUserId = Convert.ToInt32(Request.QueryString["userId"]);
                                    BanProcess.LiftUserOrOperatorBan(SiteId, this.UserOrOperatorId, ForumId, UnBanUserId);
                                    RefreashTopicNotAddViewNum(postId);
                                }
                                catch (ExceptionWithCode exp)
                                {
                                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderUnBanning];
                                    HandleExceptionWithCode(exp, exceptionMethod);
                                }
                                catch (Exception exp1)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorUnBanning]
                                     + exp1.Message + "\");</script>");
                                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorUnBanning] + exp1.Message;
                                    LogHelper.WriteExceptionLog(exp1);
                                    this.IfError = true;
                                }
                            }
                            else if (postBackAction == "reopen")
                            {
                                try
                                {
                                    lnkBtnReopen_Click(null, null);
                                    RefreashTopicNotAddViewNum();
                                }
                                catch (ExceptionWithCode exp)
                                {
                                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderReOpenning];
                                    HandleExceptionWithCode(exp, exceptionMethod);
                                }
                                catch (Exception exp1)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorReOpenning]
                                     + exp1.Message + "\");</script>");
                                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorReOpenning] + exp1.Message;
                                    LogHelper.WriteExceptionLog(exp1);
                                    this.IfError = true;
                                }
                            }
                            else if (postBackAction == "close")
                            {
                                try
                                {
                                    lnkBtnClose_Click(null, null);
                                    RefreashTopicNotAddViewNum();
                                }
                                catch (ExceptionWithCode exp)
                                {
                                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderCloseing];
                                    HandleExceptionWithCode(exp, exceptionMethod);
                                }
                                catch (Exception exp1)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorCloseing]
                                     + exp1.Message + "\");</script>");
                                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorCloseing] + exp1.Message;
                                    LogHelper.WriteExceptionLog(exp1);
                                    this.IfError = true;
                                }
                            }
                            else if (postBackAction == "deletepermanently")
                            {
                                try
                                {
                                    PostWithPermissionCheck post = PostProcess.GetPostByPostId(
                                        SiteId, UserOrOperatorId, IfOperator, postId);
                                    PostProcess.DeletePost(SiteId, UserOrOperatorId, postId);
                                    if (post.IfTopic)
                                        Response.Redirect("~/Forum.aspx?forumId=" + ForumId + "&siteId=" + SiteId, false);
                                    else
                                        RefreashTopicNotAddViewNum();
                                }
                                catch (ExceptionWithCode exp)
                                {
                                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderDeleteingPermanently];
                                    HandleExceptionWithCode(exp, exceptionMethod);
                                }
                                catch (Exception exp1)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" +
                                        Proxy[EnumText.enumForum_Topic_PageTopicErrorDeleteingPermanently]
                                     + exp1.Message + "\");</script>");
                                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorDeleteingPermanently] + exp1.Message;
                                    LogHelper.WriteExceptionLog(exp1);
                                    this.IfError = true;
                                }
                            }
                            else if (postBackAction == "restore")
                            {
                                try
                                {
                                    PostProcess.RestorePost(SiteId, UserOrOperatorId, postId);
                                    RefreashTopicNotAddViewNum(postId);
                                }
                                catch (ExceptionWithCode exp)
                                {
                                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderRestoring];
                                    HandleExceptionWithCode(exp, exceptionMethod);
                                }
                                catch (Exception exp1)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" +
                                        Proxy[EnumText.enumForum_Topic_PageTopicErrorRestoring]
                                     + exp1.Message + "\");</script>");
                                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorRestoring] + exp1.Message;
                                    LogHelper.WriteExceptionLog(exp1);
                                    this.IfError = true;
                                }
                            }
                        }
                        else
                        {
                            if (Request.QueryString["a"] == null && Request.QueryString["b"] == null)
                            {
                                TopicProcess.ReadTopic(SiteId, UserOrOperatorId, IfOperator, TopicId);
                            }
                        }
                            #endregion

                        if (Request.QueryString["goToPost"] == "true")
                        {
                            ViewState["goToPost"] = true;
                        }
                        this.RefreshData();
                        AttachmentProcess.DeleteTempAttachmentsOfUser(this.UserOrOperatorId, SiteId, this.UserOrOperatorId, EnumAttachmentType.AttachToPost);
                    }
                    else
                    {
                        ReashDataWhenPostBack();
                    }
                    ButtonOrAreaDisplyWhenSiteIsVisitOnlyInMainPageInit();
                }
                catch (System.Threading.ThreadAbortException)
                {
                }
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorLoadingPostList] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }

        protected void RefreshData()
        {
            Moderator[] tmpModeratorArray = null;

            tmpModeratorArray = ForumProcess.GetModeratorsByForumId(
                ForumId, this.SiteId, this.UserOrOperatorId, IfOperator);

            ViewState["ModeratorsLength"] = tmpModeratorArray.Length;
            this.RepeaterModerator.DataSource = tmpModeratorArray;
            this.RepeaterModerator.DataBind();

            int recordsCount = PostProcess.GetCountOfPostsByTopicIdByPaging(
                this.SiteId, this.UserOrOperatorId, TopicId, ForumId);

            setPageIndex(recordsCount);
            //int recordsCount;
            PostWithPermissionCheck[] tmpPostArray = null;
            tmpPostArray = PostProcess.GetPostsByTopicIdAndPaging(
                this.SiteId, this.UserOrOperatorId, TopicId, aspnetPager.PageIndex + 1, aspnetPager.PageSize,
                out recordsCount, ForumId, this.IfAnnoucement);

            aspnetPagertop.CWCDataBind(RepeaterTopic, tmpPostArray, recordsCount);
            aspnetPager.CWCDataBind(RepeaterTopic, tmpPostArray, recordsCount);

            SetUIControlAttritutes();
            /*Getting the promotion topic wise count*/
            PromotedVoteValue = TopicPromoted.GetPromotedVote(-1, TopicId);
            //Promotebtn.Text = "Promote [" + GetPromotedVote(0, TopicId) + "]";
            Promotebtn.Text = "Promote [" + PromotedVoteValue + "]";

        }

        private void SetUIControlAttritutes()
        {
            #region page title and topic title display
            SiteSetting tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, this.UserOrOperatorId);
            TopicWithPermissionCheck tmpTopic = TopicProcess.GetTopicByTopicId(this.SiteId, this.UserOrOperatorId, TopicId);

            bool IfTopicMarkedAsAnswer = false;
            if (tmpTopic.IfMarkedAsAnswer)
            {
                PostWithPermissionCheck answer = PostProcess.GetAnswerByTopicId(
               SiteId, UserOrOperatorId, IfOperator, tmpTopic.TopicId);
                if (this.UserPermissionInForum.IfAdmin || this.UserPermissionInForum.IfModerator
                    || !answer.IfDeleted)
                    IfTopicMarkedAsAnswer = true;
            }

            if (!this.IfAnnoucement && tmpTopic.ForumId != ForumId)
            {
                ExceptionHelper.ThrowTopicHasBeenMovedException(TopicId);
            }

            Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Topic_BrowerTitleTopic], System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(tmpTopic.Subject)), System.Web.HttpUtility.HtmlEncode(tmpSiteSetting.SiteName));
            lblTopicTitle.Text = System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(tmpTopic.Subject));
            lblTopicTitle2.Text = lblTopicTitle.Text;
            lblTopicTitle.ToolTip = System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(tmpTopic.Subject));
            lblTopicTitle2.ToolTip = lblTopicTitle.ToolTip;

            imgStatus.Visible = true;
            if (tmpTopic.IfClosed)
            {
                imgStatus.ImageUrl = this.ImagePath + "/status/32_32/close.gif";
                imgStatus.ToolTip = Proxy[EnumText.enumForum_Topic_NoteClosedTopic];
            }
            else if (IfTopicMarkedAsAnswer)
            {
                imgStatus.ImageUrl = this.ImagePath + "/status/32_32/mark.gif";
                imgStatus.ToolTip = Proxy[EnumText.enumForum_Topic_NoteMarkedTopic];
            }
            else
            {
                imgStatus.ImageUrl = this.ImagePath + "/status/32_32/normal.gif";
                imgStatus.ToolTip = Proxy[EnumText.enumForum_Topic_NoteNormalTopic];
            }
            #endregion

            #region new topic and post reply display
            if (!this.IfAnnoucement)
            {
                hyperLnkNewTopic.HRef = "~/AddTopic.aspx?topicId=" + TopicId + "&pageType=post&siteId=" + SiteId + "&forumId=" + ForumId;
                hyperLnkNewTopic.Title = Proxy[EnumText.enumForum_Topic_HelpNewTopic];
            }
            else
                hyperLnkNewTopic.Visible = false;

            if (this.CurrentUserOrOperator == null || this.UserOrOperatorId == 0)
            {
                if (!IfTopicClosed())
                {
                    lnkBtnUnLoggedinReply.Visible = true;
                }
            }
            else if (this.UserPermissionInForum.IfAdmin || this.UserPermissionInForum.IfModerator || !IfTopicClosed())
            {
                panelReplyTable.Visible = true;
                lnkBtnLoggedinReply.Visible = true;
            }

            lnkBtnLoggedinReply.Title = Proxy[EnumText.enumForum_Topic_HelpPostReply];
            lnkBtnUnLoggedinReply.Title = Proxy[EnumText.enumForum_Topic_HelpPostReply];
            /*2.0*/
            if (!IfNewTopicButtonDisplay())
                hyperLnkNewTopic.Visible = false;
            if (!IfReplayAreaDisplay())
                panelReplyTable.Visible = false;
            if (!IfPostReplyButtonDisplay())
                lnkBtnLoggedinReply.Visible = false;

            #endregion

            #region topic operation display
            if (this.UserPermissionInForum.IfAdmin || this.UserPermissionInForum.IfModerator)
            {
                lnkBtnDelete.Visible = true;
                lnkBtnMove.Visible = true;

                if (tmpTopic.IfClosed)
                {
                    lnkBtnReopen.Visible = true;
                }
                else
                {
                    lnkBtnClose.Visible = true;
                }
                //Done By techtier 
                if (tmpTopic.IfSticky)
                {
                    lnkBtnUnSticky.Visible = true;
                }
                else
                {
                    lnkBtnSticky.Visible = true;
                }

                /*2.0*/
                if (this.CurrentUserOrOperator != null || this.UserOrOperatorId != 0)
                {
                    this.lnkBtnFeatured.Visible = !tmpTopic.IfFeatured;
                    this.lnkBtnUnFeatured.Visible = tmpTopic.IfFeatured;
                }

                /*If Annoucement*/
                if (this.IfAnnoucement)
                {
                    this.lnkBtnUnSticky.Visible = false;
                    this.lnkBtnSticky.Visible = false;
                    this.lnkBtnFeatured.Visible = false;
                    this.lnkBtnUnFeatured.Visible = false;
                    this.lnkBtnMove.Visible = false;
                    this.lnkBtnClose.Visible = false;
                }
            }

            lnkBtnDelete.Title = Proxy[EnumText.enumForum_Topic_TooltipDeleteTopic];
            lnkBtnClose.Title = Proxy[EnumText.enumForum_Topic_TooltipCloseTopic];
            lnkBtnReopen.Title = Proxy[EnumText.enumForum_Topic_TooltipReopenTopic];
            lnkBtnMove.Title = Proxy[EnumText.enumForum_Topic_TooltipMoveTopic];
            lnkBtnSticky.Title = Proxy[EnumText.enumForum_Topic_TooltipStickyTopic];
            lnkBtnUnSticky.Title = Proxy[EnumText.enumForum_Topic_TooltipUnstickyTopic];
            #endregion

            #region go to answer display
            if (IfTopicMarkedAsAnswer)
            {
                lnkBtnGoToAnswer.Visible = true;
            }
            if (this.IfAnnoucement)
                lnkBtnGoToAnswer.Visible = false;

            lnkBtnGoToAnswer.ToolTip = Proxy[EnumText.enumForum_Topic_TooltipGotoAnswer];
            #endregion

            #region panelReplyTable display
            txtSubject.MaxLength = txtSubject1.MaxLength = ForumDBFieldLength.Post_subjectFieldLength;
            HTMLEditor.MaxLength = txtContent.MaxLength = this.UserPermissionInForum.MaxLengthofTopicOrPost;//ForumDBFieldLength.Post_contentFieldLength;
            RequiredTxtSubject.ErrorMessage = Proxy[EnumText.enumForum_Topic_ErrorSubjectRequired];
            RequiredTxtSubject1.ErrorMessage = Proxy[EnumText.enumForum_Topic_ErrorSubjectRequired];

            string subject = Proxy[EnumText.enumForum_Public_TextRe] + tmpTopic.Subject;
            if (subject.Length > txtSubject.MaxLength)
            {
                subject = subject.Substring(0, txtSubject.MaxLength - 3) + "...";
            }

            if (IfOperator)
            {
                if (tmpTopic.IfHasDraft)
                {
                    DraftWithPermissionCheck draft = DraftProcess.GetDraftByTopicId(SiteId, UserOrOperatorId, IfOperator, tmpTopic.TopicId);
                    txtSubject.Text = draft.Subject;//txtSubject1.Text =
                    HTMLEditor.Text = draft.Content;//txtContent.Text = 
                    txtSubject1.Text = subject;
                    txtContent.Text = "";

                    draftEditInfo.Text = "<span class='promptMsg'>" + Proxy[EnumText.enumForum_Topic_FieldDraftEditBy] + System.Web.HttpUtility.HtmlEncode(draft.LastUpdateOperatorName) + " " + Proxy[EnumText.enumForum_Public_TextAt] + " " + DateTimeHelper.DateFormate(draft.LastUpdateTime) + "</span>";
                    //draftEditInfo2.Text = "<span class='promptMsg'>" + Proxy[EnumText.enumForum_Topic_FieldDraftEditBy] + System.Web.HttpUtility.HtmlEncode(draft.LastUpdateOperatorName) + " " + Proxy[EnumText.enumForum_Public_TextAt] + " " + DateTimeHelper.DateFormate(draft.LastUpdateTime) + "</span>";
                    //GetLastAttahmentsInfor(-1);
                    if (!IsPostBack)
                    {
                        int countOfAttachments;
                        RefreashAttachmentsData(out countOfAttachments);
                        Reply_AttachmentAreaInitWhenOperatorHaveDarft(countOfAttachments);
                    }
                    else
                    {
                        Reply_AttachmentAreaInitWhenOperatorHaveDarft(GetAttachmentsData().Count);
                    }

                }
                else
                {
                    txtSubject.Text = txtSubject1.Text = ReplaceProhibitedWords(subject);
                    HTMLEditor.Text = txtContent.Text = "";
                    Reply_AttachmentAreaInitIfNotOperatorOrNotHaveDraft();
                }
            }
            else
            {
                txtSubject.Text = txtSubject1.Text = ReplaceProhibitedWords(subject);
                HTMLEditor.Text = txtContent.Text = "";

                this.btnSaveDraft.Visible = false;
                //this.btnSwiftReplySaveDraft.Visible = false;
                Reply_AttachmentAreaInitIfNotOperatorOrNotHaveDraft();
            }
            if (this.IfAnnoucement)
                this.trUploadAttachmentList.Visible = false;
            #endregion

            #region submit button text disply
            btnSubmit.Text = Proxy[EnumText.enumForum_Topic_ButtonSubmit];
            btnSaveDraft.Text = Proxy[EnumText.enumForum_Topic_ButtonSaveDraft];
            btnSwiftReplySubmit.Text = Proxy[EnumText.enumForum_Topic_ButtonSubmit];
            //btnSwiftReplySaveDraft.Text = Proxy[EnumText.enumForum_Topic_ButtonSaveDraft];
            if (this.IfAnnoucement)
            {
                btnSaveDraft.Visible = false;
                //btnSwiftReplySaveDraft.Visible = false;
            }
            #endregion

            /*If Topic Moved Init*/
            if (this.IfMovedTopic)
                IfTopicMovedBaseInforInit();
            if (this.IfDeleted)
                IfTopicDeletedBaseInforInit();

            this.file.Attributes.Add("onchange", string.Format("document.getElementById('{0}').click();", btnUpload.ClientID));
            this.btnUpload.Style.Value = "display:none";
        }
        protected void RepeaterTopic_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                #region Base topic Band /*Topic 1.0 code */
                PostWithPermissionCheck post = (PostWithPermissionCheck)e.Item.DataItem;
                //TopicWithPermissionCheck topic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, TopicId);
                bool ifAnnoucement;
                TopicBase topic = TopicProcess.CreateTopic(this.UserOrOperatorId, SiteId, TopicId, out ifAnnoucement);
                this.IfAnnoucement = ifAnnoucement;

                string currentUrl = this.Request.Url.PathAndQuery;
                currentUrl += "#aReplySubject";
                currentUrl = System.Web.HttpUtility.UrlEncode(currentUrl);

                string lastPost = "<a name='bottom'></a>";
                if (topic.LastPostId == post.PostId)
                {
                    PlaceHolder placeHolderLastPost = e.Item.FindControl("placeHolderLastPost") as PlaceHolder;
                    placeHolderLastPost.Controls.Add(new LiteralControl(lastPost));
                }
                if (!this.IfAnnoucement)
                {
                    string showUnmark = "";
                    if (IfCanUnMark(post, topic as TopicWithPermissionCheck) && IfButtonOrAreaDisplayWhenSiteIsVisitOnly())
                    {
                        string html = "javascript:if (confirm(\"" + Proxy[EnumText.enumForum_Topic_ConfirmUnMarked]
                            + "\")) {window.location.href=\"Topic.aspx?action=unMark&postId=" + post.PostId + "&forumId="
                            + (topic as TopicWithPermissionCheck).ForumId + "&siteId=" + SiteId + "&topicId=" + post.TopicId + "&b=1\";}";

                        showUnmark = string.Format(
                                                    "<div class='answer_layer'>" +
                                                    "    {2}" +
                                                    "</div>" +
                                                    "<div class='buttons'>" +
                                                    "    <ul class='buttons_menu'>" +
                                                    "       <li><a class='unmark_link' href='{0}' title='{3}'><span class='unmark'>{1}</span>" +
                                                    "           </a></li><li class='li_end'></li>" +
                                                    "    </ul>" +
                                                    "</div>" +
                                                    "<div class='clear'>" +
                                                    "</div>", html, 
                                                    Proxy[EnumText.enumForum_Topic_UnMarkedButton], 
                                                    Proxy[EnumText.enumForum_Topic_FieldPostMarkAsAnswer],
                                                    Proxy[EnumText.enumForum_Topic_UnMarkedButton]);
                    }
                    else
                    {
                        showUnmark = "<div class=\"answer_layer\">" + Proxy[EnumText.enumForum_Topic_FieldPostMarkAsAnswer] + "</div><div class='clear'></div>";
                    }
                    if (post.IfAnswer == true)
                    {
                        PlaceHolder placeHolderUnmark = e.Item.FindControl("placeHolderUnmark") as PlaceHolder;
                        placeHolderUnmark.Controls.Add(new LiteralControl(showUnmark));
                    }
                }

                string showSignature = "<div style='padding-top:20px;' class='signature_line'></div><div class='clear'></div><div class=\"signature\">" + post.PostUserOrOperatorSignature + "</div>";
                if (post.PostUserOrOperatorSignature != "")
                {
                    PlaceHolder placeHolderSignature = e.Item.FindControl("placeHolderSignature") as PlaceHolder;
                    placeHolderSignature.Controls.Add(new LiteralControl(this.HtmlReplaceProhibitedWords(showSignature)));
                }

                //string showLastEdit = "<div class='divEdited'>" + Proxy[EnumText.enumForum_Topic_FieldPostEditedBy];
                string showLastEdit = "";
                if (post.IfLastEditUserOrOperatorDeleted)
                {
                    //showLastEdit += "<font color='gray'>" + Proxy[EnumText.enumForum_Public_DeletedUser] + "</font>";

                    showLastEdit = "<div class='edited'>" + string.Format(Proxy[EnumText.enumForum_Topic_FieldPostEditedInfo],
                        this.ReplaceProhibitedWords(post.LastEditUserOrOperatorName),
                        //Proxy[EnumText.enumForum_Public_DeletedUser], 
                        DateTimeHelper.DateFormate(post.LastEditTime));
                }
                else
                {
                    string lastPosterName = "<a class='user_link' href=\"User_Profile.aspx?userId=" + post.LastEditUserOrOperatorId + "&siteId=" + SiteId + "\" target=\"_blank\">" + this.ReplaceProhibitedWords(post.LastEditUserOrOperatorName) + "</a>";

                    showLastEdit = "<div class='edited'>" + string.Format(Proxy[EnumText.enumForum_Topic_FieldPostEditedInfo], lastPosterName, DateTimeHelper.DateFormate(post.LastEditTime));

                }
                if (post.LastEditTime > post.PostTime)
                {
                    PlaceHolder placeHolderLastEdit = e.Item.FindControl("placeHolderLastEdit") as PlaceHolder;
                    placeHolderLastEdit.Controls.Add(new LiteralControl(showLastEdit));
                }

                string editHtml = "<li class='next'><a class='edit_link' href='{0}' title='{2}'><span class='edit'>{1}</span></a></li><li class='li_end'></li>";
                string showEdit;
                if (IfAnnoucement && post.IfTopic == true)
                {
                    showEdit = string.Format(editHtml, "javascript:window.location.href=\"AdminPanel/Announcement/EditAnnouncement.aspx?siteId=" + SiteId + "&Id=" + TopicId + "\";",
                        Proxy[EnumText.enumForum_Topic_EditPostButton], Proxy[EnumText.enumForum_Topic_EditPostButton]);
                    if (this.UserPermissionInForum.IfAdmin)
                    {
                        PlaceHolder placeHolderEdit = e.Item.FindControl("placeHolderEdit") as PlaceHolder;
                        placeHolderEdit.Controls.Add(new LiteralControl(showEdit));
                    }
                }
                else
                {
                    showEdit = string.Format(editHtml, "javascript:editPost(" + post.PostId + ");", Proxy[EnumText.enumForum_Topic_EditPostButton],
                        Proxy[EnumText.enumForum_Topic_EditPostButton]);
                    //string showEdit = "<img style='cursor: pointer' src='" + this.GetButtonIMGDir() + "icon_post_edit.gif" + "' alt='" + Proxy[EnumText.enumForum_Topic_HelpEditPost] + "' onclick='javascript:editPost(" + post.PostId + ");' />&nbsp;";
                    if (IfCanEdit(post, topic) == true && IfEditButtonDisplay())// CheckIfAllowPostTopicOrPost(ForumId))
                    {
                        PlaceHolder placeHolderEdit = e.Item.FindControl("placeHolderEdit") as PlaceHolder;
                        placeHolderEdit.Controls.Add(new LiteralControl(showEdit));
                    }
                }


                string showDelete = string.Format("<li class='next'><a class='delete1_link' href='{0}' title='{2}'><span class='delete1'>{1}</span> </a></li><li class='li_end'></li>", 
                    "javascript:deletePost(" + post.PostId + ");", 
                    Proxy[EnumText.enumForum_Topic_DeletePostButton],
                    Proxy[EnumText.enumForum_Topic_DeletePostButton]);
                //string showDelete = "<img style='cursor: pointer' src='" + this.GetButtonIMGDir() + "icon_post_delete.gif" + "' alt='" + Proxy[EnumText.enumForum_Topic_HelpDeletePost] + "' onclick='javascript:deletePost(" + post.PostId + ");' />&nbsp;";
                if (!(post.IfTopic && this.IfAnnoucement) && IfCanDelete() == true)//post.IfTopic == false && 
                {
                    PlaceHolder placeHolderDelete = e.Item.FindControl("placeHolderDelete") as PlaceHolder;
                    placeHolderDelete.Controls.Add(new LiteralControl(showDelete));
                }
                if (!this.IfAnnoucement)
                {
                    string showMarkAsAnswer = "";
                    if ((topic as TopicWithPermissionCheck).IfMarkedAsAnswer == true)
                    {
                        showMarkAsAnswer = string.Format("<li class='next'><a class='edit_link' href=\"{0}\" title='{2}'><span class='edit'>{1}</span></a></li><li class='li_end'></li>", 
                            "javascript:markAsAnswerInsteadOfOld(" + post.PostId + ");",
                            Proxy[EnumText.enumForum_Topic_HelpMarkButton],
                            Proxy[EnumText.enumForum_Topic_HelpMarkButton]);
                        //showMarkAsAnswer = "<img style='cursor: pointer' src=\"" + this.GetButtonIMGDir() + "icon_post_markasanswer.gif" + "\" alt=\"" + Proxy[EnumText.enumForum_Topic_HelpMarkButton] + "\" onclick=\"javascript:markAsAnswerInsteadOfOld(" + post.PostId + ");\" />&nbsp;";
                    }
                    else
                    {
                        showMarkAsAnswer = string.Format("<li class='next'><a class='edit_link' href=\"{0}\" title='{2}'><span class='edit'>{1}</span></a></li><li class='li_end'></li>", 
                            "javascript:markAsAnswer(" + post.PostId + ");", 
                            Proxy[EnumText.enumForum_Topic_HelpMarkButton],
                            Proxy[EnumText.enumForum_Topic_HelpMarkButton]);
                        //showMarkAsAnswer = "<img style='cursor: pointer' src=\"" + this.GetButtonIMGDir() + "icon_post_markasanswer.gif" + "\" alt=\"" + Proxy[EnumText.enumForum_Topic_HelpMarkButton] + "\" onclick=\"javascript:markAsAnswer(" + post.PostId + ");\" />&nbsp;";
                    }
                    if (post.IfTopic == false && this.IfCanMark(post, topic as TopicWithPermissionCheck) == true && post.IfAnswer == false)
                    {
                        PlaceHolder placeHolderMarkAsAnswer = e.Item.FindControl("placeHolderMarkAsAnswer") as PlaceHolder;
                        placeHolderMarkAsAnswer.Controls.Add(new LiteralControl(showMarkAsAnswer));
                    }
                }

                string showQuote = string.Format("<li class='next'><a class='quote_link' href=\"{0}\" title='{2}'><span class='quote'>{1}</span></a></li><li class='li_end'></li>",
                    "javascript:quote('" + post.PostId + "');",
                    Proxy[EnumText.enumForum_Topic_HelpQuoteButton],
                    Proxy[EnumText.enumForum_Topic_HelpQuoteButton]);
                //string showQuote = "<img style='cursor: pointer' src=\"" + this.GetButtonIMGDir() + "icon_post_quote.gif" + "\" alt=\"" + Proxy[EnumText.enumForum_Topic_HelpQuoteButton] + "\" onclick=\"javascript:quote('" + post.PostId + "');\" />&nbsp;";
                if (IfCanQuote() == true && this.CurrentUserOrOperator != null
                    && this.CurrentUserOrOperator.UserOrOperatorId != 0 && this.IfQuoteButtonDisplay())// && CheckIfAllowPostTopicOrPost(ForumId))
                {
                    PlaceHolder placeHolderQuote = e.Item.FindControl("placeHolderQuote") as PlaceHolder;
                    placeHolderQuote.Controls.Add(new LiteralControl(showQuote));
                }

                string showQuickReply = "";
                if (this.CurrentUserOrOperator != null && this.UserOrOperatorId != 0)
                {
                    showQuickReply = string.Format("<li class='next'><a class='reply_link' href=\"{0}\" title='{2}'><span class='reply'>{1}</span></a></li><li class='li_end'></li>",
                        "javascript:showWindow('divThickInner','divThickOuter');IfShowDropDownList('false');",
                        Proxy[EnumText.enumForum_Topic_QuickReplyButton],
                        Proxy[EnumText.enumForum_Topic_QuickReplyButton]);
                    //showQuickReply = "<img style='cursor: pointer' src=\"" + this.GetButtonIMGDir() + "icon_quick_reply.gif\" alt=\"" + Proxy[EnumText.enumForum_Topic_HelpQuickReplyButton] + "\" onclick=\"javascript:showWindow('divThickInner','divThickOuter');\" />";
                }
                else
                {
                    showQuickReply = string.Format("<li class='next'><a class='reply_link' href=\"javascript:UnloginQuickReply();\" title='{1}'><span class='reply'>{0}</span></a></li><li class='li_end'></li>",
                        Proxy[EnumText.enumForum_Topic_QuickReplyButton],
                        Proxy[EnumText.enumForum_Topic_QuickReplyButton]);

                    //showQuickReply = string.Format("<li class='next'><a class='reply_link' href=\"{0}\"><span class='reply'>{1}</span></a></li>", "javascript:window.location.href='Login.aspx?siteId=" + SiteId + "&ReturnUrl=" + currentUrl + "';", Proxy[EnumText.enumForum_Topic_QuickReplyButton]);
                    //showQuickReply = "<img style='cursor: pointer' src=\"" + this.GetButtonIMGDir() + "icon_quick_reply.gif\" alt=\"" + Proxy[EnumText.enumForum_Topic_HelpQuickReplyButton] + "\" onclick=\"window.location.href='Login.aspx?siteId=" + SiteId + "&ReturnUrl=" + currentUrl + "';\" />";
                }
                if (IfCanQuote() == true && IfQuickReplayButtonDisplay())//&& CheckIfAllowPostTopicOrPost(ForumId))
                {
                    PlaceHolder placeHolderQuickReply = e.Item.FindControl("placeHolderQuickReply") as PlaceHolder;
                    placeHolderQuickReply.Controls.Add(new LiteralControl(showQuickReply));
                }
                #endregion

                bool ifPostAbused = AbuseProcess.IfPostAbused(
                    UserOrOperatorId, SiteId, post.PostId);

                PostsAttachmentListInit(post, e);
                /*2.0 Topic Init*/
                TopicBaseInforInit(post, topic, e, ifPostAbused);
                /*Topic Subscirbe and Favorite Button Init*/
                SubscirbeandFavoriteButtonInit(topic);
                /*Post Base Information Init*/
                PostBaseInforInit(post, e, ifPostAbused);
                /*Post Tool Bar Init*/
                PostToolBarInit(post, topic, e, ifPostAbused);
            }
        }

        #region page
        protected void aspnetPager_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
        {
            if (!this.IfError)
            {
                try
                {
                    ASPNetPager o = sender as ASPNetPager;

                    currentAspNetPage.Value = o.ID;

                    RefreshData();
                }
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorLoadingPostList] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }

        private void setPageIndex(int recordsCount)
        {
            aspnetPagertop.ExtraIndex = aspnetPager.ExtraIndex = "forumId" + ForumId + "topicId" + TopicId + "siteId" + SiteId;

            if (GoToPost)
            {
                CheckQueryString("postId");

                int postId = Convert.ToInt32(Request.QueryString["postId"]);
                int pageIndex = 0;
                if (postId == -1)
                {
                    pageIndex = (recordsCount - 1) / aspnetPager.PageSize;
                }
                else
                {
                    int postIndexInTopic = PostProcess.GetPostIndexInTopic(SiteId, this.UserOrOperatorId, IfOperator, postId, TopicId);
                    pageIndex = (postIndexInTopic - 1) / aspnetPager.PageSize;

                }

                aspnetPagertop.PageIndex = aspnetPager.PageIndex = pageIndex;
                ViewState["goToPost"] = false;
            }
            else if (currentAspNetPage.Value.Equals("aspnetPager"))
            {
                if (aspnetPager.PageIndex > 0 && aspnetPager.PageIndex * aspnetPager.PageSize == recordsCount)
                {
                    aspnetPager.PageIndex = aspnetPager.PageIndex - 1;
                }
                aspnetPagertop.PageIndex = aspnetPager.PageIndex;
            }
            else
            {
                if (aspnetPagertop.PageIndex > 0 && aspnetPagertop.PageIndex * aspnetPagertop.PageSize == recordsCount)
                {
                    aspnetPagertop.PageIndex = aspnetPagertop.PageIndex - 1;
                }
                aspnetPager.PageIndex = aspnetPagertop.PageIndex;
            }
        }
        #endregion page

        #region Buttons Click Event

        protected void Favorite_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    FavoriteProcess.AddFavorite(SiteId, UserOrOperatorId, ForumId, TopicId);
                    //string strhtml = string.Format("<script language=javascript>window.location='{1}';alert('{0}');</script>", Proxy[EnumText.enumForum_Topic_Page_FavoriateSuccess],
                    //    "Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1");
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", strhtml);
                    Response.Redirect("~/Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1", false);
                }
                catch (ExceptionWithCode exp)
                {
                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderFavoritingTopic];
                    HandleExceptionWithCode(exp, exceptionMethod);
                }
                catch (Exception exp1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorFavoritingTopic] + exp1.Message + "\");</script>");
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorFavoritingTopic] + exp1.Message;
                    LogHelper.WriteExceptionLog(exp1);
                    this.IfError = true;
                }
            }
        }

        protected void UnFavorite_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    FavoriteProcess.DeleteFavorite(SiteId, UserOrOperatorId, ForumId, TopicId);
                    //string strhtml = string.Format("<script language=javascript>window.location='{1}';alert('{0}');</script>", Proxy[EnumText.enumForum_Topic_Page_UnFavoriateSuccess],
                    //    "Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1");
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", strhtml);
                    Response.Redirect("~/Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1", false);
                }
                catch (ExceptionWithCode exp)
                {
                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderUnFavoritingTopic];
                    HandleExceptionWithCode(exp, exceptionMethod);
                }
                catch (Exception exp1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorUnFavoritingTopic] + exp1.Message + "\");</script>");
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorUnFavoritingTopic] + exp1.Message;
                    LogHelper.WriteExceptionLog(exp1);
                    this.IfError = true;
                }
            }
        }

        protected void lnkBtnGoToAnswer_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    TopicWithPermissionCheck tmpTopic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, TopicId);
                    PostWithPermissionCheck answer = PostProcess.GetAnswerByTopicId(this.SiteId, this.UserOrOperatorId, this.IfOperator, TopicId);
                    RefreashTopicNotAddViewNum(answer.PostId);
                    //Response.Redirect("~/Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + tmpTopic.ForumId + "&postId=" + answer.PostId + "&b=1&goToPost=true#Post" + answer.PostId, false);
                }
                catch (Exception exp)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorGoToAnswer] + exp.Message + "\");</script>");
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorGoToAnswer] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }

        protected void lnkBtnUnLoggedinReply_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    string currentUrl = this.Request.Url.PathAndQuery;
                    currentUrl += "#aReplySubject";
                    currentUrl = System.Web.HttpUtility.UrlEncode(currentUrl);
                    Response.Redirect("~/Login.aspx?siteId=" + SiteId + "&ReturnUrl=" + currentUrl, false);
                }
                catch (Exception exp)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorReply] + exp.Message + "\");</script>");
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorReply] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }

        /*2.0*/
        protected void lnkBtnFeatured_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    //TopicWithPermissionCheck tmpTopic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, TopicId);
                    //this.ForumId = tmpTopic.ForumId;
                    TopicProcess.SetTopicFeatured(SiteId, UserOrOperatorId, IfOperator, ForumId, TopicId);
                    Response.Redirect("~/Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1", false);
                }
                catch (ExceptionWithCode exp)
                {
                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderFeaturingTopic];
                    HandleExceptionWithCode(exp, exceptionMethod);
                }
                catch (Exception exp1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorFeaturingTopic] + exp1.Message + "\");</script>");
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorFeaturingTopic] + exp1.Message;
                    LogHelper.WriteExceptionLog(exp1);
                    this.IfError = true;
                }
            }
        }

        protected void lnkBtnUnFeatured_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    //TopicWithPermissionCheck tmpTopic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, TopicId);
                    //this.ForumId = tmpTopic.ForumId;
                    TopicProcess.SetTopicUnFeatured(SiteId, UserOrOperatorId, IfOperator, ForumId, TopicId);
                    Response.Redirect("~/Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1", false);
                }
                catch (ExceptionWithCode exp)
                {
                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicExpHeaderUnFeaturingTopic];
                    HandleExceptionWithCode(exp, exceptionMethod);
                }
                catch (Exception exp1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorUnFeaturingTopic] + exp1.Message + "\");</script>");
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorUnFeaturingTopic] + exp1.Message;
                    LogHelper.WriteExceptionLog(exp1);
                    this.IfError = true;
                }
            }
        }


        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    if (!this.IfAnnoucement)
                    {
                        //TopicWithPermissionCheck tmpTopic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, TopicId);
                        //this.ForumId = tmpTopic.ForumId;
                        TopicProcess.LogicDeleteTopic(UserOrOperatorId, SiteId, TopicId);
                        Response.Redirect("~/Forum.aspx?forumId=" + ForumId + "&siteId=" + SiteId, false);
                    }
                    else
                    {
                        AnnoucementProcess.DeleteAnnouncementByModeratorOrAdmin(this.SiteId, this.UserOrOperatorId, ForumId, TopicId);
                        Response.Redirect("~/Forum.aspx?forumId=" + ForumId + "&siteId=" + SiteId, false);
                    }
                }
                catch (ExceptionWithCode exp)
                {
                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_PageTopicErrorDeleteTopic];
                    HandleExceptionWithCode(exp, exceptionMethod);
                }
                catch (Exception exp1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorDeleteTopic] + exp1.Message + "\");</script>");
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorDeleteTopic] + exp1.Message;
                    LogHelper.WriteExceptionLog(exp1);
                    this.IfError = true;
                }
            }
        }

        protected void lnkBtnClose_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    if (!this.IfAnnoucement)
                    {
                        //TopicWithPermissionCheck tmpTopic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, TopicId);
                        ////this.ForumId = tmpTopic.ForumId;

                        //if (tmpTopic.IfClosed)
                        //{
                        //    Response.Write("<script language=javascript>alert('" + Proxy[EnumText.enumForum_Topic_TitleQuickReply] + "');window.location.href='Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1';</script>");
                        //}
                        //else
                        //{
                        TopicProcess.CloseTopic(SiteId, UserOrOperatorId, IfOperator, TopicId);
                        Response.Redirect("~/Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1", false);
                        //}
                    }
                }
                catch (ExceptionWithCode exp)
                {
                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_ErrorHeaderClosingTopic];
                    HandleExceptionWithCode(exp, exceptionMethod);
                }
                catch (Exception exp1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_ErrorClosingTopic] + exp1.Message + "\");</script>");
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_ErrorClosingTopic] + exp1.Message;
                    LogHelper.WriteExceptionLog(exp1);
                    this.IfError = true;
                }
            }
        }

        protected void lnkBtnReopen_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    if (!this.IfAnnoucement)
                    {
                        //TopicWithPermissionCheck tmpTopic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, TopicId);
                        //this.ForumId = tmpTopic.ForumId;

                        //if (!tmpTopic.IfClosed)
                        //{
                        //    Response.Write("<script language=javascript>alert('" + Proxy[EnumText.enumForum_Topic_MessageTopicOpen] + "');window.location.href='Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1';</script>");
                        //}
                        //else
                        //{
                        TopicProcess.ReopenTopic(SiteId, UserOrOperatorId, IfOperator, TopicId);
                        Response.Redirect("~/Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1", false);
                        //}
                    }
                }
                catch (ExceptionWithCode exp)
                {
                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_ErrorHeaderReopeningTopic];
                    HandleExceptionWithCode(exp, exceptionMethod);
                }
                catch (Exception exp1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorReopeningTopic] + exp1.Message + "\");</script>");
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorReopeningTopic] + exp1.Message;
                    LogHelper.WriteExceptionLog(exp1);
                    this.IfError = true;
                }
            }
        }

        protected void lnkBtnSticky_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    //TopicWithPermissionCheck tmpTopic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, TopicId);
                    //this.ForumId = tmpTopic.ForumId;

                    //if (tmpTopic.IfSticky)
                    //{
                    //    Response.Write("<script language=javascript>alert('" + Proxy[EnumText.enumForum_Topic_MessageTopicSticky] + "');window.location.href='Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1';</script>");
                    //}
                    //else
                    //{
                    TopicProcess.SetTopicSticky(SiteId, UserOrOperatorId, IfOperator, TopicId);
                    Response.Redirect("~/Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1", false);
                    //}
                }
                catch (ExceptionWithCode exp)
                {
                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_ErrorHeaderStickingTopic];
                    HandleExceptionWithCode(exp, exceptionMethod);
                }
                catch (Exception exp1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorStickyTopic] + exp1.Message + "\");</script>");
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorStickyTopic] + exp1.Message;
                    LogHelper.WriteExceptionLog(exp1);
                    this.IfError = true;
                }
            }
        }

        protected void lnkBtnUnSticky_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    //TopicWithPermissionCheck tmpTopic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, TopicId);
                    //this.ForumId = tmpTopic.ForumId;

                    //if (!tmpTopic.IfSticky)
                    //{
                    //    Response.Write("<script language=javascript>alert('" + Proxy[EnumText.enumForum_Topic_MessageTopicUnSticky] + "');window.location.href='Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1';</script>");
                    //}
                    //else
                    //{
                    TopicProcess.SetTopicUnSticky(SiteId, UserOrOperatorId, IfOperator, TopicId);
                    Response.Redirect("~/Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1", false);
                    //}
                }
                catch (ExceptionWithCode exp)
                {
                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_ErrorHeaderUnstickingTopic];
                    HandleExceptionWithCode(exp, exceptionMethod);
                }
                catch (Exception exp1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorUnstickingTopic] + exp1.Message + "\");</script>");
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorUnstickingTopic] + exp1.Message;
                    LogHelper.WriteExceptionLog(exp1);
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

                    //TopicWithPermissionCheck tmpTopic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, TopicId);
                    int postId = 0;
                    if (!this.IfAnnoucement)
                    {
                        //this.ForumId = tmpTopic.ForumId;
                        GetLastAttahmentsInfor(-1);
                        postId = PostProcess.AddPost(SiteId, UserOrOperatorId, IfOperator, TopicId, false,true, subject, content,
                            this.LastAttachsIds.ToArray<int>(), this.LastAttachsScoresList.ToArray<int>(),
                            this.LastAttachsDescriptionsList.ToArray<string>(), ForumId);
                        //Response.Redirect("~/Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId
                        // + "&forumId=" + ForumId + "&postId=" + postId + "&goToPost=true&a=1#Post" + postId, false);
                        RefreashTopicNotAddViewNum(postId);
                    }
                    else
                    {
                        postId = PostProcess.AddAnnouncementPost(SiteId, UserOrOperatorId, TopicId, ForumId, false, subject, content);
                        //Response.Redirect("~/Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId
                        // + "&forumId=" + ForumId + "&postId=" + postId + "&goToPost=true&a=1#Post" + postId, false);
                        RefreashTopicNotAddViewNum(postId);
                    }
                }
                catch (ExceptionWithCode exp)
                {
                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_ErrorHeaderAddingPost];
                    HandleExceptionWithCode(exp, exceptionMethod);
                    lblReplyError.Text = Proxy[EnumText.enumForum_Topic_FieldError] + exceptionMethod + ": " + exp.Message;
                }
                catch (Exception exp1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorAddPost] + exp1.Message + "\");</script>");
                    lblReplyError.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorAddPost] + exp1.Message;
                    LogHelper.WriteExceptionLog(exp1);
                    this.IfError = true;
                }
            }
        }

        protected void btnSwiftReplySubmit_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    String subject = txtSubject1.Text;
                    String content = txtContent.Text;

                    //TopicWithPermissionCheck tmpTopic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, TopicId);
                    int postId = 0;
                    if (!this.IfAnnoucement)
                    {
                        //this.ForumId = tmpTopic.ForumId;
                        //GetLastAttahmentsInfor(-1);
                        postId = PostProcess.AddPost(SiteId, UserOrOperatorId, IfOperator, TopicId, false, false,subject, content,
                            null, null, null, ForumId);
                        // this.LastAttachsIds.ToArray<int>(), this.LastAttachsScoresList.ToArray<int>(),
                        //this.LastAttachsDescriptionsList.ToArray<string>(), ForumId);
                        //Response.Redirect("~/Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId
                        //    + "&forumId=" + ForumId + "&postId=" + postId + "&goToPost=true&a=1#Post" + postId, false);
                        RefreashTopicNotAddViewNum(postId);
                    }
                    else
                    {
                        postId = PostProcess.AddAnnouncementPost(SiteId, UserOrOperatorId, TopicId, ForumId, false, subject, content);
                        //Response.Redirect("~/Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId
                        // + "&forumId=" + ForumId + "&postId=" + postId + "&goToPost=true&a=1#Post" + postId, false);
                        RefreashTopicNotAddViewNum(postId);

                    }

                }
                catch (ExceptionWithCode exp)
                {
                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_ErrorHeaderAddingPost];
                    HandleExceptionWithCode(exp, exceptionMethod);
                    lblReplyError.Text = Proxy[EnumText.enumForum_Topic_FieldError] + exceptionMethod + ": " + exp.Message;
                }
                catch (Exception exp1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorAddPost] + exp1.Message + "\");</script>");
                    lblReplyError.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorAddPost] + exp1.Message;
                    LogHelper.WriteExceptionLog(exp1);
                    this.IfError = true;
                }
            }
        }

        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    String subject = txtSubject.Text;
                    String content = HTMLEditor.Text;

                    //TopicWithPermissionCheck tmpTopic = TopicProcess.GetTopicByTopicId(
                    //    SiteId, UserOrOperatorId, TopicId);
                    //this.ForumId = tmpTopic.ForumId;
                    GetLastAttahmentsInfor(-1);
                    DraftProcess.SaveDraft(SiteId, UserOrOperatorId,
                        IfOperator, TopicId, subject, content,
                        this.LastAttachsIds.ToArray<int>(), this.LastAttachsScoresList.ToArray<int>(),
                        this.LastAttachsDescriptionsList.ToArray<string>(), null);
                    Response.Redirect("~/Topic.aspx?topicId=" + TopicId
                        + "&siteId=" + SiteId + "&forumId=" + ForumId
                        + "&a=1#aReplySubject", false);
                }
                catch (ExceptionWithCode exp)
                {
                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_ErrorHeaderSavingDraft];
                    HandleExceptionWithCode(exp, exceptionMethod);
                    lblReplyError.Text = Proxy[EnumText.enumForum_Topic_FieldError] + exceptionMethod + ": " + exp.Message;
                }
                catch (Exception exp1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorSavingDraft] + exp1.Message + "\");</script>");
                    lblReplyError.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorSavingDraft] + exp1.Message;
                    LogHelper.WriteExceptionLog(exp1);
                    this.IfError = true;
                }
            }
        }

        //protected void btnSwiftSaveDraft_Click(object sender, EventArgs e)
        //{
        //    if (!this.IfError)
        //    {
        //        try
        //        {
        //            String subject = txtSubject1.Text;
        //            String content = txtContent.Text;

        //            //TopicWithPermissionCheck tmpTopic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, TopicId);
        //            //this.ForumId = tmpTopic.ForumId;
        //            GetLastAttahmentsInfor(-1);
        //            DraftProcess.SaveDraft(SiteId, UserOrOperatorId, IfOperator, TopicId, subject, content,
        //                 this.LastAttachsIds.ToArray<int>(), this.LastAttachsScoresList.ToArray<int>(),
        //                this.LastAttachsDescriptionsList.ToArray<string>(), null);
        //            Response.Redirect("~/Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1", false);
        //        }
        //        catch (ExceptionWithCode exp)
        //        {
        //            string exceptionMethod = Proxy[EnumText.enumForum_Topic_ErrorHeaderSavingDraft];
        //            HandleExceptionWithCode(exp, exceptionMethod);
        //        }
        //        catch (Exception exp1)
        //        {
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorSavingDraft] + exp1.Message + "\");</script>");
        //            lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorSavingDraft] + exp1.Message;
        //            LogHelper.WriteExceptionLog(exp1);
        //            this.IfError = true;
        //        }
        //    }
        //}

        #endregion

        private void RefreashTopicNotAddViewNum()
        {
            Response.Redirect("~/Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        private void RefreashTopicNotAddViewNum(int postId)
        {
            Response.Redirect("~/Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1" + "&postId=" + postId + "&goToPost=true#Post" + postId, false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        private bool IfTopicClosed()
        {
            if (this.IfAnnoucement)
                return false;
            bool ifTopicClosed = false;
            TopicWithPermissionCheck topic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, TopicId);
            if (topic.IfClosed)
            {
                ifTopicClosed = true;
            }
            return ifTopicClosed;
        }

        private void HandleExceptionWithCode(ExceptionWithCode exp, string exceptionMethod)
        {
            if (exp.GetErrorCode() == EnumErrorCode.enumOperatorNotLogin || exp.GetErrorCode() == EnumErrorCode.enumUserNotLogin
                || exp.GetErrorCode() == EnumErrorCode.ForumOperatingUserOrOperatorCanNotBeNull)
            {
                string currentUrl = this.Request.Url.PathAndQuery;
                currentUrl = System.Web.HttpUtility.UrlEncode(currentUrl);
                Response.Redirect("~/Login.aspx?siteId=" + SiteId + "&ReturnUrl=" + currentUrl, false);
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
                Response.End();
            }
            else if (exp.GetErrorCode() == EnumErrorCode.enumTopicIsClosed)
            {
                Response.Write("<script language=javascript>alert('" + Proxy[EnumText.enumForum_Topic_MessageTopicClosed] + "');window.location.href='Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1';</script>");
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
                Response.End();
            }
            else if (exp.GetErrorCode() == EnumErrorCode.enumSystemNotEnoughPermission)
            {
                Response.Write("<script language=javascript>alert('" + Proxy[EnumText.enumForum_Topic_MessagePermissionDenied] + "');window.location.href='Topic.aspx?topicId=" + TopicId + "&siteId=" + SiteId + "&forumId=" + ForumId + "&b=1';</script>");
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
                Response.End();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_FieldError] + exceptionMethod + ": " + exp.Message + "\");</script>");
                lblMessage.Text = Proxy[EnumText.enumForum_Topic_FieldError] + exceptionMethod + ": " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }
        }

        protected void RepeaterModerator_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Literal Literal1 = (Literal)e.Item.FindControl("Literal1");
                if (e.Item.ItemIndex < Convert.ToInt32(ViewState["ModeratorsLength"]) - 1)
                {
                    Literal1.Text = ",";
                }
            }
        }

        protected void RepeaterTopic_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void rptAttachments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                AttachmentWithPermissionCheck attachment = e.Item.DataItem as AttachmentWithPermissionCheck;
                bool IfUserPayAttach = (attachment.Score == 0) ||
                    this.UserPermissionInForum.IfAdmin ||
                    this.UserPermissionInForum.IfModerator ||
                     (this.UserOrOperatorId == attachment.UploadUserOrOperatorId) ||
                    !ForumFeature.IfEnableScore ||
                    PayHistroyProcess.ifUserPayAttachment(this.UserOrOperatorId, SiteId, this.UserOrOperatorId, attachment.Id);
                string link = "<a {0} >{1} {2}</a>";
                string Click = "";
                string Content = Server.HtmlEncode(ReplaceProhibitedWords(attachment.Name));
                string Pay = "";
                if (IfUserPayAttach)
                {
                    Click = string.Format("href='Handler/DownAttachment.aspx?AttachId={0}&siteId={2}' title='{1}' target='_blank'", attachment.Id,
                        Server.HtmlEncode(ReplaceProhibitedWords(attachment.Description)), SiteId);
                }
                else
                {
                    Click = string.Format("href=\"javascript:GetAttachId('{0}');\" title='{1}'", attachment.Id,
                        Server.HtmlEncode(ReplaceProhibitedWords(attachment.Description)));
                    Pay = string.Format(Proxy[EnumText.enumForum_Topic_Page_NeedPayAttachment], attachment.Score);
                }

                PlaceHolder phAttachmentItem = e.Item.FindControl("PHAttachmentItem") as PlaceHolder;
                phAttachmentItem.Controls.Add(new LiteralControl(string.Format(link, Click, Content, Pay)));
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Topic_ErrorLoadingAttachmentList] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        /*2.0*/

        #region Topic Base Infor Init
        private void TopicBaseInforInit(PostWithPermissionCheck post, TopicBase topic,
            RepeaterItemEventArgs e, bool ifPostAbused)
        {
            if (!post.IfTopic)
            {
                this.IfShowPollInfor = false;
                Panel panel = e.Item.FindControl("divVoteInfo") as Panel;
                panel.Visible = false;
                return;
            }
            /*Get Topic Pay History*/
            this.IfUserPayTopic = false;
            if (PayHistroyProcess.ifUserPayTopic(this.UserOrOperatorId, SiteId, this.UserOrOperatorId, topic.TopicId) ||
                (this.UserOrOperatorId == topic.PostUserOrOperatorId) ||
                this.UserPermissionInForum.IfAdmin ||
                this.UserPermissionInForum.IfModerator)
                this.IfUserPayTopic = true;
            /*Get Topic Reply History*/
            if (PostProcess.IfUserReplyTopic(SiteId, this.UserOrOperatorId, topic.TopicId, this.UserOrOperatorId) ||
               (this.UserOrOperatorId == topic.PostUserOrOperatorId) ||
               this.UserPermissionInForum.IfAdmin ||
               this.UserPermissionInForum.IfModerator)
                this.IfUserRelayTopic = true;
            /*Content*/
            this.TopicContentInit(post, topic, e);
            if (!this.IfAnnoucement)
            {
                if ((topic as TopicWithPermissionCheck).IfMoveHistory)
                {
                    e.Item.FindControl("divForumToolBar").Visible = false;
                    return;
                }
                /*Pay Score Infor*/
                this.TopicPayScoreInforInit(post, topic as TopicWithPermissionCheck, e);
                /*No Reply Infor*/
                this.TopicReplyInforInit(topic as TopicWithPermissionCheck, e);
                /*Poll Infor*/
                this.TopicPollInforInit(topic as TopicWithPermissionCheck, e);
                /*Attachment list*/
                //this.TopicAttachmentListInit(topic, e); 
            }
            PostBaseContentInforInit(post, e, ifPostAbused);
            if (!this.IfAnnoucement)
            {
                TopicWithPermissionCheck topic1 = topic as TopicWithPermissionCheck;
                if (!((!topic1.IfPayScoreRequired && !topic1.IfReplyRequired)
                       || (topic1.IfPayScoreRequired && this.IfUserPayTopic)
                       || (topic1.IfReplyRequired && this.IfUserRelayTopic)))
                {
                    e.Item.FindControl("divVoteInfo").Visible = false;
                    e.Item.FindControl("divAttachmentList").Visible = false;
                }
            }
        }

        private void TopicContentInit(PostWithPermissionCheck post, TopicBase topicOrAnnoucement, RepeaterItemEventArgs e)
        {
            PlaceHolder ph = e.Item.FindControl("PHContent") as PlaceHolder;
            string strContent = "<div class=\"content\" id=\"postContent{0}\">{1}</div>";
            if (!this.IfAnnoucement)
            {
                TopicWithPermissionCheck topic = topicOrAnnoucement as TopicWithPermissionCheck;
                if (topic.IfMoveHistory)
                {
                    /*Topic Moved Content*/
                    TopicWithPermissionCheck topicMoved = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId,
                        topic.LocateTopicId);
                    string strMovedToFromHtml = string.Format(Proxy[EnumText.enumForum_Topic_ThisTopicHaveBeenMovedToThe]
                        + " <a href='Forum.aspx?forumId={0}&siteId={1}'>{2}</a>.<br /><br />",
                        topicMoved.ForumId, SiteId, Server.HtmlEncode(this.ReplaceProhibitedWords(topicMoved.ForumName)));
                    string strMovedToTopicHtml = string.Format(Proxy[EnumText.enumForum_Topic_ToViewThisTopicPleaseClickTheUrl]
                        + "<a href='topic.aspx?forumId={0}&siteId={1}&topicId={2}'><u>{3}</u></a>.<br/><br/>",
                        topicMoved.ForumId, SiteId, topicMoved.TopicId, Server.HtmlEncode(this.ReplaceProhibitedWords(topicMoved.Subject)));
                    ph.Controls.Add(new LiteralControl(strMovedToFromHtml + strMovedToTopicHtml));
                }
                else if ((!topic.IfPayScoreRequired && !topic.IfReplyRequired)
                    || (topic.IfPayScoreRequired && this.IfUserPayTopic)
                    || (topic.IfReplyRequired && this.IfUserRelayTopic))
                {
                    ph.Controls.Add(new LiteralControl(string.Format(strContent, post.PostId, this.HtmlReplaceProhibitedWords(GetPostContent(post.Content)))));
                }
            }
            else
            {
                ph.Controls.Add(new LiteralControl(string.Format(strContent, post.PostId, this.HtmlReplaceProhibitedWords(GetPostContent(post.Content)))));
            }
        }
        private void TopicPayScoreInforInit(PostWithPermissionCheck post, TopicWithPermissionCheck topic, RepeaterItemEventArgs e)
        {
            if (topic.IfPayScoreRequired)
            {
                /*No Pay*/
                if (!this.IfUserPayTopic)
                {
                    string strHtmlMessage = "";
                    string strHtmlPayButton = "<input type='button' class='btn' value='" + Proxy[EnumText.enumForum_Topic_Page_PayScore] + "' {0}" +
                                              " onclick=\"if(confirm('" + Proxy[EnumText.enumForum_Topic_ConfirmPayScoresForThisTopic] + "')){2}\" />";
                    string strLocation = string.Format("Topic.aspx?action=paytopic&topicId={0}&siteId={1}&forumId={2}", TopicId, SiteId, ForumId);
                    string strJs = "{window.location.href='" + strLocation + "';}";
                    if (!this.IfGuest)
                    {
                        UserOrOperator user = UserProcess.GetUserOrOpertorById(SiteId, this.UserOrOperatorId);
                        if (user.Score >= topic.Score)
                        {
                            strHtmlMessage = string.Format(Proxy[EnumText.enumForum_Topic_HaveEnoughScoresViewTopic], topic.Score, user.Score);
                            strHtmlPayButton = string.Format(strHtmlPayButton, "", topic.Score, strJs);
                        }
                        else
                        {
                            strHtmlMessage = string.Format(Proxy[EnumText.enumForum_Topic_HaveNotEnoughScoresViewTopic], topic.Score, user.Score);
                            strHtmlPayButton = string.Format(strHtmlPayButton, "disabled='disabled'", topic.Score, strJs);
                        }
                    }
                    else
                    {
                        strHtmlMessage = Proxy[EnumText.enumForum_Topic_LoginAndViewTopic];
                        strHtmlPayButton = "";
                    }
                    PlaceHolder ph = e.Item.FindControl("PHUserNeverPayTopic") as PlaceHolder;
                    string html = string.Format("<div class='pay'>{0}</div>",
                        "<p><img src='App_Themes/StyleTemplate_Default/images/pay.gif' />&nbsp; <b>" + strHtmlMessage + "</b>" + strHtmlPayButton + "</p>");
                    ph.Controls.Add(new LiteralControl(html));
                    /*Quote Button*/
                    e.Item.FindControl("placeHolderQuote").Visible = false;
                }
            }
        }
        private void TopicReplyInforInit(TopicWithPermissionCheck topic, RepeaterItemEventArgs e)
        {
            /*No Reply Infor*/
            if (topic.IfReplyRequired)
            {
                /*No Reply*/
                if (!this.IfUserRelayTopic)
                {
                    string html = "<div class='pay'><p><img src='App_Themes/StyleTemplate_Default/images/pay.gif' />&nbsp;<b>" +
                                Proxy[EnumText.enumForum_Topic_ReplyAndViewTpoic] + "</b></p></div>";
                    PlaceHolder ph = e.Item.FindControl("PHUserNeverReplytopic") as PlaceHolder;
                    ph.Controls.Add(new LiteralControl(html));
                }
            }
        }
        private void TopicPollInforInit(TopicWithPermissionCheck topic, RepeaterItemEventArgs e)
        {
            /*Poll Infor*/
            if (topic.IfContainsPoll)
            {
                this.IfUserHaveVoted = PollProcess.IfUserVotePoll(UserOrOperatorId, SiteId, TopicId);

                Label lblPollDateTo = e.Item.FindControl("lblPollDateTo") as Label;
                Label lblMulitipleChoice = e.Item.FindControl("lblMulitipleChoice") as Label;
                Label lbTotalVoteNum = e.Item.FindControl("lbTotalVoteNum") as Label;
                Poll poll = PollProcess.GetPollByTopicId(this.UserOrOperatorId, SiteId, topic.TopicId);
                PollOption[] pollOptions = PollProcess.GetPollOptionsByTopicId(this.UserOrOperatorId, SiteId, topic.TopicId);
                this.IfPollMulitipleChoice = poll.IfMulitipleChoice;
                lblPollDateTo.Text = DateTimeHelper.DateFormate(poll.EndDate);
                lblMulitipleChoice.Text = poll.MaxChoices.ToString();
                this.PollMulitipleChoiceCount = poll.MaxChoices;
                /*Poll Options Result View*/
                this.AllVotesResults = 0;
                foreach (PollOption po in pollOptions)
                {
                    this.AllVotesResults += po.Votes;
                }
                this.AllVotesHistories = PollProcess.GetCountOfPollvoteHistories(SiteId, TopicId);
                this.TopicSubject = Server.HtmlEncode(ReplaceProhibitedWords(topic.Subject));
                this.rptPollsResult.DataSource = pollOptions;
                this.rptPollsResult.DataBind();

                lbTotalVoteNum.Text = this.AllVotesHistories.ToString();
                if (!poll.IfSetDeadline)
                {
                    e.Item.FindControl("PHPollDateToInfor").Visible = false;
                }
                // user Have voted this poll
                if (this.IfUserHaveVoted)
                {
                    e.Item.FindControl("btnVote").Visible = false;
                    e.Item.FindControl("btnView").Visible = false;
                }
                // poll EndDate
                if (poll.IfSetDeadline && (DateTime.UtcNow > poll.EndDate))
                {
                    e.Item.FindControl("btnVote").Visible = false;
                    this.IfPollDateExpried = true;
                }
                else
                {
                    this.IfPollDateExpried = false;
                }
                // Visit Only
                if (!IfButtonOrAreaDisplayWhenSiteIsVisitOnly())
                {
                    e.Item.FindControl("btnVote").Visible = false;
                }

                /*Poll Options List*/
                Repeater rptPollOptions = e.Item.FindControl("rptPollOptions") as Repeater;
                rptPollOptions.DataSource = pollOptions;
                rptPollOptions.DataBind();
            }
            this.IfShowPollInfor = topic.IfContainsPoll;
            e.Item.FindControl("divVoteInfo").Visible = topic.IfContainsPoll;

        }
        protected void rptPollOptions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (this.IfUserHaveVoted)
                e.Item.FindControl("divChooseOption").Visible = false;
            else
                e.Item.FindControl("pollVoteResult").Visible = false;
            if (!IfButtonOrAreaDisplayWhenSiteIsVisitOnly())
                e.Item.FindControl("divChooseOption").Visible = false;
            if (IfPollDateExpried)
                e.Item.FindControl("divChooseOption").Visible = false;
        }
        private void PostsAttachmentListInit(PostWithPermissionCheck post, RepeaterItemEventArgs e)
        {
            /*Attachment list*/
            AttachmentWithPermissionCheck[] attahments = AttachmentProcess.GetAllAttachmentsOfPost(
                this.UserOrOperatorId, SiteId, post.PostId);
            Panel panl = e.Item.FindControl("divAttachmentList") as Panel;
            if (attahments.Length == 0)
            { this.IfShowAttachmentsList = false; panl.Visible = false; }
            else { this.IfShowAttachmentsList = true; panl.Visible = true; }
            Repeater rptAttachments = e.Item.FindControl("rptAttachments") as Repeater;
            rptAttachments.DataSource = attahments;
            rptAttachments.DataBind();
        }
        private void IfTopicMovedBaseInforInit()
        {
            this.lnkBtnLoggedinReply.Visible = false;
            this.lnkBtnUnLoggedinReply.Visible = false;
            this.imgBtnFavorite.Visible = false;
            this.imgBtnUnFavorite.Visible = false;
            this.lnkBtnFeatured.Visible = false;
            this.lnkBtnUnFeatured.Visible = false;
            this.lnkBtnClose.Visible = false;
            this.lnkBtnDelete.Visible = false;
            this.lnkBtnReopen.Visible = false;
            this.lnkBtnMove.Visible = false;
            this.lnkBtnSticky.Visible = false;
            this.lnkBtnUnSticky.Visible = false;
            this.panelReplyTable.Visible = false;
            this.lnkBtnGoToAnswer.Visible = false;

            this.aspnetPager.Visible = false;
            this.aspnetPagertop.Visible = false;
        }
        private void IfTopicDeletedBaseInforInit()
        {
            IfTopicMovedBaseInforInit();
            this.hyperLnkNewTopic.Visible = false;
        }
        #endregion

        #region Topic Subscirbe and Favorite Button Init
        /*Topic Subscirbe and Favorite Button Init*/
        private void SubscirbeandFavoriteButtonInit(TopicBase topic)
        {
            ForumFeatureWithPermissionCheck feature = SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId);

            if (feature.IfEnableFavorite && !this.IfGuest)
            {
                bool ifUserFavoruiteTopic = FavoriteProcess.IfUserFavoriteTopic(
                    SiteId, UserOrOperatorId, topic.TopicId);
                this.imgBtnFavorite.Visible = !ifUserFavoruiteTopic;
                this.imgBtnUnFavorite.Visible = ifUserFavoruiteTopic;
            }
        }
        #endregion

        #region Post Base Infor Init
        private void PostBaseInforInit(PostWithPermissionCheck post, RepeaterItemEventArgs e, bool ifPostAbused)
        {
            /*User Base Infor In One Post*/
            PostUserInformationBaseInit(post, e);
            /*Post Base Content Infor Init*/
            if (!post.IfTopic)
            {
                PostBaseContentInforInit(post, e, ifPostAbused);
            }
        }
        private void PostUserInformationBaseInit(PostWithPermissionCheck post, RepeaterItemEventArgs e)
        {
            ForumFeature feature = ForumFeature;//SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId);
            UserOrOperator user = UserProcess.GetUserOrOpertorById(SiteId, post.PostUserOrOperatorId);
            if (user.IfDeleted)
            {
                (e.Item.FindControl("PHUserJoined") as HtmlGenericControl).Visible = false;
                (e.Item.FindControl("PHUserNumberOfPosts") as HtmlGenericControl).Visible = false;
            }
            /*Score*/
            if (feature.IfEnableScore && !user.IfDeleted)
            {
                string strSorceHtml = string.Format("<p>{0}{1}</p>", "<b>"
                    + Proxy[EnumText.enumForum_Topic_FiledScore] + " </b>", user.Score);
                (e.Item.FindControl("PHUserSorce") as PlaceHolder).Controls.Add(
                    new LiteralControl(strSorceHtml));
            }
            /*Reputation*/
            if (feature.IfEnableReputation && !user.IfDeleted)
            {
                UserReputationGroupWithPermissionCheck group = UserReputationGroupProcess.GetReputationGroupByUserOrOperatorId(
                    SiteId, user.Id);
                if (group == null)
                {
                    string strReputationHtml = string.Format("<p>{0}{1}</p>", "<b>" + Proxy[EnumText.enumForum_Topic_Page_Reputation] + " </b>",
                        user.Reputation);
                    (e.Item.FindControl("PHUserReputations") as PlaceHolder).Controls.Add(
                        new LiteralControl(strReputationHtml));
                }
                else
                {
                    string strReputationsHtml = "<p>{0}</p>";
                    string reputationImage = "";
                    for (int i = 0; i < group.IcoRepeat; i++)
                    {
                        reputationImage += string.Format("<img src='" + this.ImagePath + "/reputation.gif' alt='{0}' title='{0}'/>",
                            user.Reputation);
                        //Proxy[EnumText.enumForum_Topic_ToolTipReputation]);
                    }
                    (e.Item.FindControl("PHUserReputations") as PlaceHolder).Controls.Add(
                        new LiteralControl(string.Format(strReputationsHtml, reputationImage)));
                }
            }
            /*Send Message*/
            if (!this.IfGuest && feature.IfEnableMessage && !user.IfDeleted)
            {
                string js = string.Format("javascript:SetSendMessageToUserId('{0}');showWindow('divSendMessageToUser','divThickOuter');IfShowDropDownList('false');", user.Id);
                string strSendMessageHtml = string.Format("<a href=\"{0}\" title='{1}'><img src='" + this.ImagePath + "/sendmessage.gif'/></a>", js,
                    Proxy[EnumText.enumForum_Topic_ToolTipSendMessage]);
                (e.Item.FindControl("PHUserSendMessageImage") as PlaceHolder).Controls.Add(
                    new LiteralControl(strSendMessageHtml));
            }
            /*Ban User*/
            if ((this.UserPermissionInForum.IfAdmin || this.UserPermissionInForum.IfModerator) && !user.IfDeleted)
            {

                bool IfUserBanned = UserProcess.IfUserBanned(SiteId, UserOrOperatorId, ForumId, user.Id);
                if (!IfUserBanned)
                {
                    string js = string.Format("javascript:SetBanUserIframeWithUserId('{0}');showWindow('divBanOneUser','divThickOuter');IfShowDropDownList('false');", user.Id);
                    string strBanUserHtml = string.Format("<a href=\"{0}\" title='{1}'><img src='" + this.ImagePath + "/Ban.gif'/></a>", js,
                        Proxy[EnumText.enumForum_Topic_ToolTipBanUser]);
                    (e.Item.FindControl("PHBanUserImage") as PlaceHolder).Controls.Add(
                        new LiteralControl(strBanUserHtml));
                }
                else
                {
                    string url1 = string.Format("Topic.aspx?action={0}&postId={1}&topicId={2}&siteId={3}&forumId={4}&userId={5}",
                         "unban", post.PostId, TopicId, SiteId, ForumId, post.PostUserOrOperatorId);

                    string js = "javascript:if(confirm('" + Proxy[EnumText.enumForum_Topic_Page_ConfirmUnBanUser]
                                   + "')){window.location.href='" + url1 + "';}";
                    //string js = "#";//string.Format("javascript:SetBanUserIframeWithUserId('{0}');showWindow('divBanOneUser','divThickOuter');", user.Id);
                    string strUnBanUserHtml = string.Format("<a href=\"{0}\" title='{1}'><img src='" + this.ImagePath + "/lift-ban.gif'/></a>", js,
                        Proxy[EnumText.enumForum_Topic_ToolTipLiftBan]);
                    (e.Item.FindControl("PHBanUserImage") as PlaceHolder).Controls.Add(
                        new LiteralControl(strUnBanUserHtml));
                }
            }
        }

        private void PostBaseContentInforInit(PostWithPermissionCheck post, RepeaterItemEventArgs e, bool ifPostAbused)
        {

            string PostModerationRejected = "<div class='content'>" +
                                            "<div style='padding-bottom: 15px;'>" +
                                            "    " + Proxy[EnumText.enumForum_Topic_SpiltLine] + "</div>" +
                                            "<div style='font-weight: bold; font-size: 15px;'>" +
                                            "    " + Proxy[EnumText.enumForum_Topic_PostUnverified] + "</div>" +
                                            "<div style='padding-top: 15px;'>" +
                                            "    " + Proxy[EnumText.enumForum_Topic_SpiltLine] + "</div>" +
                                            "</div>";
            string PostModerationWaitingForModeration = "<div class='content'>" +
                                            "<div style='padding-bottom: 15px;'>" +
                                            "    " + Proxy[EnumText.enumForum_Topic_SpiltLine] + "</div>" +
                                            "<div style='font-weight: bold; font-size: 15px;'>" +
                                            "    " + Proxy[EnumText.enumForum_Topic_WaitingForModeration] + "</div>" +
                                            "<div style='padding-top: 15px;'>" +
                                            "    " + Proxy[EnumText.enumForum_Topic_SpiltLine] + "</div>" +
                                            "</div>";
            string PostAbusedConfirm = "<div class='content'>" +
                                       "<div style='padding-bottom: 15px;'>" +
                                       "    " + Proxy[EnumText.enumForum_Topic_SpiltLine] + "</div>" +
                                       "<div style='font-weight: bold; font-size: 15px;'>" +
                                       "    " + Proxy[EnumText.enumForum_Topic_ThisIsASpam] + "</div>" +
                                       "<div style='padding-top: 15px;'>" +
                                       "    " + Proxy[EnumText.enumForum_Topic_SpiltLine] + "</div>" +
                                       "</div>";
            //string strContent = "<div class='content'>{0}</div>";
            string strContent = "<div id=\"postContent" + post.PostId + "\">{0}</div>";
            PlaceHolder phContent = e.Item.FindControl("PHContent") as PlaceHolder;
            if (!post.IfTopic)
                phContent.Controls.Add(new LiteralControl(string.Format(strContent, HtmlReplaceProhibitedWords(GetPostContent(post.Content)))));

            Panel divForumToolBar = e.Item.FindControl("divForumToolBar") as Panel;
            PlaceHolder PHUserNeverPayTopic = e.Item.FindControl("PHUserNeverPayTopic") as PlaceHolder;
            PlaceHolder PHUserNeverReplytopic = e.Item.FindControl("PHUserNeverReplytopic") as PlaceHolder;
            Panel divVoteInfo = e.Item.FindControl("divVoteInfo") as Panel;
            Panel divAttachment = e.Item.FindControl("divAttachmentList") as Panel;
            /*Post Abuse*/
            if (ifPostAbused)
            {
                EnumPostAbuseStatus status = AbuseProcess.GetPostAbusedStuats(UserOrOperatorId, SiteId, post.PostId);
                //EnumPostAbuseStatus statusOfUser = AbuseProcess.GetPostAbusedStuatsOfUser(UserOrOperatorId, SiteId, 
                //    post.PostId,UserOrOperatorId);
                if (status == EnumPostAbuseStatus.AbusedAndApproved)
                {
                    phContent.Controls.Clear();
                    phContent.Controls.Add(new LiteralControl(PostAbusedConfirm));
                    divForumToolBar.Visible = false;
                    PHUserNeverPayTopic.Visible = false;
                    PHUserNeverReplytopic.Visible = false;
                    divVoteInfo.Visible = false;
                    divAttachment.Visible = false;
                }
                else if (status == EnumPostAbuseStatus.AbusedAndPending)
                {
                    if (this.UserPermissionInForum.IfAdmin || this.UserPermissionInForum.IfModerator)
                    {
                        string strMsg = "<span class='alertMsg' style='font-weight: bold;'>[" + Proxy[EnumText.enumForum_Topic_ThisIsAabusedPost] + "]</span>";
                        PlaceHolder PHSubjectAbusedMsg = e.Item.FindControl("PHSubjectAbusedMsg") as PlaceHolder;
                        PHSubjectAbusedMsg.Controls.Add(new LiteralControl(strMsg));
                    }
                }
            }
            /*Post Moderation*/
            if (post.ModerationStatus == EnumPostOrTopicModerationStatus.Rejected)
            {
                if (!this.UserPermissionInForum.IfAdmin && !this.UserPermissionInForum.IfModerator)
                {
                    phContent.Controls.Clear();
                    phContent.Controls.Add(new LiteralControl(PostModerationRejected));
                    divForumToolBar.Visible = false;
                    PHUserNeverPayTopic.Visible = false;
                    PHUserNeverReplytopic.Visible = false;
                    divVoteInfo.Visible = false;
                    divAttachment.Visible = false;
                }
                else
                {
                    string strMsg = "<span class='alertMsg' style='font-weight: bold;'>[" + Proxy[EnumText.enumForum_Topic_PostUnverified] + "]</span>";
                    PlaceHolder PHSubjectModerationMsg = e.Item.FindControl("PHSubjectModerationMsg") as PlaceHolder;
                    PHSubjectModerationMsg.Controls.Add(new LiteralControl(strMsg));
                    phContent.Controls.Clear();
                    phContent.Controls.Add(new LiteralControl(string.Format(strContent, HtmlReplaceProhibitedWords(GetPostContent(post.Content)))));
                }

            }
            else if (post.ModerationStatus == EnumPostOrTopicModerationStatus.WaitingForModeration)
            {
                if (!this.UserPermissionInForum.IfAdmin && !this.UserPermissionInForum.IfModerator
                    && this.UserOrOperatorId != post.PostUserOrOperatorId)
                {
                    phContent.Controls.Clear();
                    phContent.Controls.Add(new LiteralControl(PostModerationWaitingForModeration));
                    divForumToolBar.Visible = false;
                    PHUserNeverPayTopic.Visible = false;
                    PHUserNeverReplytopic.Visible = false;
                    divVoteInfo.Visible = false;
                    divAttachment.Visible = false;
                }
                else
                {
                    string strMsg = "<span class='alertMsg' style='font-weight: bold;'>[" + Proxy[EnumText.enumForum_Topic_WaitingForModeration] + "]</span>";
                    PlaceHolder PHSubjectModerationMsg = e.Item.FindControl("PHSubjectModerationMsg") as PlaceHolder;
                    PHSubjectModerationMsg.Controls.Add(new LiteralControl(strMsg));
                    phContent.Controls.Clear();
                    phContent.Controls.Add(new LiteralControl(string.Format(strContent, HtmlReplaceProhibitedWords(GetPostContent(post.Content)))));
                }
            }
        }
        #endregion

        #region Post Tool Bar Init
        private void PostToolBarInit(PostWithPermissionCheck post, TopicBase topic, RepeaterItemEventArgs e, bool ifPostAbused)
        {
            #region Delete Permanently,Restore
            if (post.IfDeleted)
            {
                foreach (Control ctrl in e.Item.FindControl("divForumToolBar").Controls)
                {
                    if (ctrl is PlaceHolder)
                        ctrl.Visible = false;
                }
                e.Item.FindControl("PHResotre").Visible = true;
                e.Item.FindControl("PHDeletePermanently").Visible = true;
                /*Delete Permanently*/
                string url1 = string.Format("Topic.aspx?action={0}&postId={4}&topicId={1}&siteId={2}&forumId={3}",
                          "deletepermanently", TopicId, SiteId, ForumId, post.PostId);

                string approveJs1 = "javascript:if(confirm('" + Proxy[EnumText.enumForum_Topic_Page_ConfirmDeletePermanentely]
                                    + "')){window.location.href='" + url1 + "';}";
                string DeleteButtonHtml = string.Format("<li class='next'><a class='edit_link' href=\"{0}\" title='{2}'><span class='edit'>{1}</span></a></li><li class='li_end'></li>", approveJs1,
                    Proxy[EnumText.enumForum_Topic_Page_Filed_DeletePaermanentely], Proxy[EnumText.enumForum_Topic_Page_Filed_DeletePaermanentely]);

                PlaceHolder DeleteButton = e.Item.FindControl("PHDeletePermanently") as PlaceHolder;
                DeleteButton.Controls.Add(new LiteralControl(DeleteButtonHtml));
                /*Restore*/
                string url2 = string.Format("Topic.aspx?action={0}&postId={4}&topicId={1}&siteId={2}&forumId={3}",
                          "restore", TopicId, SiteId, ForumId, post.PostId);

                string approveJs2 = "javascript:if(confirm('" + Proxy[EnumText.enumForum_Topic_Page_ConfirmResotre]
                                    + "')){window.location.href='" + url2 + "';}";
                string ResotreButtonHtml = string.Format("<li class='next'><a class='edit_link' href=\"{0}\" title='{2}'><span class='edit'>{1}</span></a></li><li class='li_end'></li>", approveJs2,
                    Proxy[EnumText.enumForum_Topic_Page_Filed_Restore], Proxy[EnumText.enumForum_Topic_Page_Filed_Restore]);

                PlaceHolder RestoreButton = e.Item.FindControl("PHResotre") as PlaceHolder;
                RestoreButton.Controls.Add(new LiteralControl(ResotreButtonHtml));

                return;
            }
            #endregion

            #region Close,ReOpen
            if (!this.IfAnnoucement)
            {
                TopicWithPermissionCheck topic1 = topic as TopicWithPermissionCheck;
                if (post.IfTopic && (this.UserPermissionInForum.IfAdmin || this.UserPermissionInForum.IfModerator || (post.PostUserOrOperatorId == this.UserOrOperatorId)))
                {
                    if (!topic1.IfClosed)
                    {
                        string url1 = string.Format("Topic.aspx?action={0}&topicId={1}&siteId={2}&forumId={3}",
                            "close", TopicId, SiteId, ForumId);

                        string approveJs1 = "javascript:if(confirm('" + Proxy[EnumText.enumForum_Topic_Page_ConfirmClose]
                                            + "')){window.location.href='" + url1 + "';}";
                        string CloseButtonHtml = string.Format("<li class='next'><a class='edit_link' href=\"{0}\" title='{2}'><span class='edit'>{1}</span></a></li><li class='li_end'></li>", approveJs1,
                            Proxy[EnumText.enumForum_Topic_Page_Filed_Close], Proxy[EnumText.enumForum_Topic_Page_Filed_Close]);

                        PlaceHolder CloseButton = e.Item.FindControl("PHCloseButton") as PlaceHolder;
                        CloseButton.Controls.Add(new LiteralControl(CloseButtonHtml));
                    }
                    else
                    {
                        string url1 = string.Format("Topic.aspx?action={0}&topicId={1}&siteId={2}&forumId={3}",
                            "reopen", TopicId, SiteId, ForumId);

                        string approveJs1 = "javascript:if(confirm('" + Proxy[EnumText.enumForum_Topic_Page_ConfirmReOpen]
                                            + "')){window.location.href='" + url1 + "';}";
                        string ReOpenButtonHtml = string.Format("<li class='next'><a class='edit_link' href=\"{0}\" title='{2}'><span class='edit'>{1}</span></a></li><li class='li_end'></li>", approveJs1,
                            Proxy[EnumText.enumForum_Topic_Page_Filed_ReOpen], Proxy[EnumText.enumForum_Topic_Page_Filed_ReOpen]);

                        PlaceHolder ReOpenButton = e.Item.FindControl("PHReOpenButton") as PlaceHolder;
                        ReOpenButton.Controls.Add(new LiteralControl(ReOpenButtonHtml));
                    }
                }
            }
            #endregion

            #region Abuse,Approval,Refuse
            /*Post Abuse*/
            string AbuseJs = string.Format("javascript:IfGoToLogin('{0}',{1});", IfGuest, post.PostId);
            string AbuseButtonHtml = string.Format("<li class='next'><a class='abuse_link' href=\"{0}\" title='{2}'><span class='abuse'>{1}</span></a></li><li class='li_end'></li>", AbuseJs,
                Proxy[EnumText.enumForum_Topic_Page_Filed_Abuse], Proxy[EnumText.enumForum_Topic_Page_Filed_Abuse]);
            //string AbuseButtonHtml = string.Format("<img style='cursor: pointer' src='Images/btnImages/icon_post_Abuse.gif'" +
            //                         " alt='" + Proxy[EnumText.enumForum_Topic_ToolTipAbuseThisPost] + "'onclick=\"javascript:IfGoToLogin('{0}',{1});\" />", IfGuest, post.PostId);
            PlaceHolder PHAbuseButton = e.Item.FindControl("PHAbuseButton") as PlaceHolder;
            if (ifPostAbused)
            {
                if (this.UserPermissionInForum.IfAdmin || this.UserPermissionInForum.IfModerator)
                {
                    EnumPostAbuseStatus status = AbuseProcess.GetPostAbusedStuats(UserOrOperatorId, SiteId, post.PostId);
                    if (status == EnumPostAbuseStatus.AbusedAndPending)
                    {
                        /*Approval Abuse*/
                        string url1 = "";
                        if (!this.IfAnnoucement)
                        {
                            url1 = string.Format("Topic.aspx?action={0}&postId={1}&topicId={2}&siteId={3}&forumId={4}",
                            "approvalabuse", post.PostId, TopicId, SiteId, ForumId);
                        }
                        else
                        {
                            url1 = string.Format("Topic.aspx?action={0}&postId={1}&topicId={2}&siteId={3}&forumId={4}",
                            "approvalabuse", post.PostId, TopicId, SiteId, ForumId);
                        }
                        string approveJs1 = "javascript:if(confirm('" + Proxy[EnumText.enumForum_Topic_ConfirmApproveTheAbuse]
                                            + "')){window.location.href='" + url1 + "';}";
                        string ApproveAbuseButtonHtml = string.Format("<li class='next'><a class='approval_link' href=\"{0}\" title='{2}'><span class='approval'>{1}</span></a></li><li class='li_end'></li>", approveJs1,
                            Proxy[EnumText.enumForum_Topic_Page_Filed_ApprovalAbuse], Proxy[EnumText.enumForum_Topic_Page_Filed_ApprovalAbuse]);

                        PlaceHolder PHApproveAbuseButton = e.Item.FindControl("PHApproveAbuseButton") as PlaceHolder;
                        PHApproveAbuseButton.Controls.Add(new LiteralControl(ApproveAbuseButtonHtml));
                        /*refuse Abuse*/
                        string url2 = "";
                        if (!this.IfAnnoucement)
                        {
                            url2 = string.Format("Topic.aspx?action={0}&postId={1}&topicId={2}&siteId={3}&forumId={4}",
                             "refuseabuse", post.PostId, TopicId, SiteId, ForumId);
                        }
                        else
                        {
                            url2 = string.Format("Topic.aspx?action={0}&postId={1}&topicId={2}&siteId={3}&forumId={4}",
                             "refuseabuse", post.PostId, TopicId, SiteId, ForumId);
                        }
                        string approveJs2 = "javascript:if(confirm('" + Proxy[EnumText.enumForum_Topic_ConfirmRefuseTheAbuse]
                                            + "')){window.location.href='" + url2 + "';}";
                        string RefuseAbuseButtonHtml = string.Format("<li class='next'><a class='approval_link' href=\"{0}\" title='{2}'><span class='approval'>{1}</span></a></li><li class='li_end'></li>", approveJs2,
                            Proxy[EnumText.enumForum_Topic_Page_Filed_RefuseAbuse], Proxy[EnumText.enumForum_Topic_Page_Filed_RefuseAbuse]);

                        PlaceHolder PHRefuseAbuseButton = e.Item.FindControl("PHRefuseAbuseButton") as PlaceHolder;
                        PHRefuseAbuseButton.Controls.Add(new LiteralControl(RefuseAbuseButtonHtml));
                    }
                    else
                        PHAbuseButton.Controls.Add(new LiteralControl(AbuseButtonHtml));
                }
                else
                {
                    EnumPostAbuseStatus statusOfUser = AbuseProcess.GetPostAbusedStuatsOfUser(UserOrOperatorId, SiteId, post.PostId, UserOrOperatorId);
                    if (statusOfUser == EnumPostAbuseStatus.AbusedAndPending)
                        PHAbuseButton.Visible = false;
                    else
                        PHAbuseButton.Controls.Add(new LiteralControl(AbuseButtonHtml));
                }
            }
            else
                PHAbuseButton.Controls.Add(new LiteralControl(AbuseButtonHtml));
            if (post.IfTopic && this.IfAnnoucement)
                PHAbuseButton.Visible = false;
            #endregion

            #region Approval,UnApproval
            /*Post Moderation*/
            if (post.ModerationStatus == EnumPostOrTopicModerationStatus.WaitingForModeration
                || post.ModerationStatus == EnumPostOrTopicModerationStatus.Rejected)
            {
                if (this.UserPermissionInForum.IfAdmin || this.UserPermissionInForum.IfModerator)
                {
                    string url1;
                    if (!this.IfAnnoucement)
                    {
                        url1 = string.Format("Topic.aspx?action={0}&postId={1}&topicId={2}&siteId={3}&forumId={4}",
                              "approvalmoderation", post.PostId, TopicId, SiteId, ForumId);
                    }
                    else
                    {
                        url1 = string.Format("Topic.aspx?action={0}&postId={1}&topicId={2}&siteId={3}&forumId={4}",
                              "approvalmoderation", post.PostId, TopicId, SiteId, ForumId);
                    }
                    string approveJs1 = "javascript:if(confirm('" + Proxy[EnumText.enumForum_Topic_ConfirmApprovalThisPost]
                                        + "')){window.location.href='" + url1 + "';}";
                    string ApprovalModerationButtonHtml = string.Format("<li class='next'><a class='approval_link' href=\"{0}\" title='{2}'><span class='approval'>{1}</span></a></li><li class='li_end'></li>", approveJs1,
                        Proxy[EnumText.enumForum_Topic_Page_Filed_Approval], Proxy[EnumText.enumForum_Topic_Page_Filed_Approval]);

                    string url2 = "";
                    if (!this.IfAnnoucement)
                    {
                        url2 = string.Format("Topic.aspx?action={0}&postId={1}&topicId={2}&siteId={3}&forumId={4}",
                              "unapprovalmoderation", post.PostId, TopicId, SiteId, ForumId);
                    }
                    else
                    {
                        url2 = string.Format("Topic.aspx?action={0}&postId={1}&topicId={2}&siteId={3}&forumId={4}",
                             "unapprovalmoderation", post.PostId, TopicId, SiteId, ForumId);
                    }
                    string approveJs2 = "javascript:if(confirm('" + Proxy[EnumText.enumForum_Topic_ConfirmUnapprovalThisPost]
                                        + "')){window.location.href='" + url2 + "';}";
                    string UnApprovalModerationButtonHtml = string.Format("<li class='next'><a class='approval_link' href=\"{0}\" title='{2}'><span class='approval'>{1}</span></a></li><li class='li_end'></li>", approveJs2,
                        Proxy[EnumText.enumForum_Topic_Page_Filed_UnApproval], Proxy[EnumText.enumForum_Topic_Page_Filed_UnApproval]);

                    PlaceHolder PHApprovalModerationButton = e.Item.FindControl("PHApprovalModerationButton") as PlaceHolder;
                    PlaceHolder PHUnApprovalModerationButton = e.Item.FindControl("PHUnApprovalModerationButton") as PlaceHolder;
                    PHUnApprovalModerationButton.Controls.Add(new LiteralControl(UnApprovalModerationButtonHtml));
                    PHApprovalModerationButton.Controls.Add(new LiteralControl(ApprovalModerationButtonHtml));
                    if (post.ModerationStatus == EnumPostOrTopicModerationStatus.Rejected)
                    {
                        PHUnApprovalModerationButton.Visible = false;
                    }
                }
                e.Item.FindControl("PHAbuseButton").Visible = false;
            }
            #endregion

            #region IfButtonOrAreaDisplayWhenSiteIsVisitOnly
            ButtonOrAreaDisplyWhenSiteIsVisitOnlyInToolBarInit(e);
            #endregion
        }
        #endregion

        #region AttachmentList
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                string content = this.HTMLEditor.Text;
                string subject = this.txtSubject.Text;
                try
                {
                    //this.RefreshData();
                    int attchmentOfPostCount = GetAttachmentsData().Count;
                    GetLastAttahmentsInfor(-1);
                    /*Add Attachment to DataBase*/
                    if (this.file.FileBytes.Length != 0)
                    {
                        string name = this.file.FileName;// + "_" + DateTime.UtcNow.ToString("yyyyMMddhhmmss");
                        int attachId = AttachmentProcess.AddAttachment(this.UserOrOperatorId, SiteId, ForumId, attchmentOfPostCount,
                            -1, this.UserOrOperatorId, this.file.FileBytes.Length
                            , this.file.FileBytes, false, 0, name, "", uniqueGuid, EnumAttachmentType.AttachToPost);
                        this.LastAttachsIds.Add(attachId);
                        this.LastAttachsScoresList.Add(0);
                        this.LastAttachsDescriptionsList.Add("");
                    }

                    RefreshData();
                    RefreashAttachmentsData();

                }
                catch (ExceptionWithCode exp)
                {
                    string exceptionMethod = Proxy[EnumText.enumForum_Topic_ErrorHeaderUploadingAttahment];
                    HandleExceptionWithCode(exp, exceptionMethod);
                    this.lblMessageAttachment.Text = Proxy[EnumText.enumForum_Topic_FieldError] + exceptionMethod + ": " + exp.Message; ;
                }
                catch (Exception exp1)
                {
                    lblMessageAttachment.Text = Proxy[EnumText.enumForum_Topic_ErrorUploadingAttahment] + exp1.Message;
                    LogHelper.WriteExceptionLog(exp1);

                    //this.IfError = true;
                }

                this.HTMLEditor.Text = content;
                this.txtSubject.Text = subject;
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>window.location='#divUploadTempAttachmentList';</script>");
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
        private void RefreashAttachmentsData()
        {
            int countOfAttachments = 0;
            RefreashAttachmentsData(out countOfAttachments);
        }
        private void RefreashAttachmentsData(out int countOfAttachments)
        {
           List<AttachmentWithPermissionCheck> attachments = GetAttachmentsData();
            /*Data Bind*/
            this.rptPostAttachmentsList.DataSource = attachments;
            this.rptPostAttachmentsList.DataBind();
            countOfAttachments = attachments.Count;
        }

        private List<AttachmentWithPermissionCheck> GetAttachmentsData()
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
            return attachments;
        }

        protected void rptPostAttachmentsList_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                    tbDescription.MaxLength = ForumDBFieldLength.Attachment_descriptionFieldLength;

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

                    (e.Item.FindControl("cvDownloadScore") as CompareValidator).ControlToValidate = tbScore.ID;
                    (e.Item.FindControl("cvDownloadScore") as CompareValidator).Visible = true;
                    (e.Item.FindControl("rfvDownloadScore") as RequiredFieldValidator).ControlToValidate = tbScore.ID;
                    (e.Item.FindControl("rfvDownloadScore") as RequiredFieldValidator).Visible = true;

                    if (!IfReplay_AttachmentAreaDisplay())//show attachment but no allow attach permission
                    {
                        tbScore.ReadOnly = true;
                        tbDescription.ReadOnly = true;
                        e.Item.FindControl("imgDelete").Visible = false;
                    }
                }
                catch (Exception exp)
                {
                    lblMessageAttachment.Text = Proxy[EnumText.enumForum_Topic_ErrorLoadingPostingAttachmentsList] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);

                    this.IfError = true;
                }
            }
        }

        protected void rptPostAttachmentsList_ItemCommand(object source, RepeaterCommandEventArgs e)
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
                            AttachmentProcess.DeleteAttachment(UserOrOperatorId, SiteId, attachmentId, ForumId);
                        }
                        catch (Exception exp)
                        {
                            lblMessageAttachment.Text = Proxy[EnumText.enumForum_Topic_ErrorDeletingAttachment] + exp.Message;
                            LogHelper.WriteExceptionLog(exp);
                        }
                    }
                    string content = this.HTMLEditor.Text;
                    string subject = this.txtSubject.Text;
                    RefreshData();
                    RefreashAttachmentsData();
                    this.HTMLEditor.Text = content;
                    this.txtSubject.Text = subject;
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>window.location='#divUploadTempAttachmentList';</script>");
                }
                catch (Exception exp)
                {
                    lblMessageAttachment.Text = Proxy[EnumText.enumForum_Topic_ErrorLoadingPostingAttachmentsList] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);

                    this.IfError = true;
                }
            }
        }
        #endregion

        protected void btnQuickReplyHidden_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    string currentUrl = this.Request.Url.PathAndQuery;
                    currentUrl = System.Web.HttpUtility.UrlEncode(currentUrl);
                    Response.Redirect("~/Login.aspx?siteId=" + SiteId + "&ReturnUrl=" + currentUrl, false);
                }
                catch (Exception exp)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorReply] + exp.Message + "\");</script>");
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageTopicErrorReply] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }

        private string GetPostContent(string content)
        {
            return content;
            //return content.Replace("\n", "<br/>");
        }

        /*this is for a bug: when submit reply error,post list is blank*/
        private void ReashDataWhenPostBack()
        {
            String subject1 = txtSubject1.Text;
            String content1 = txtContent.Text;

            String subject = txtSubject.Text;
            String content = HTMLEditor.Text;

            this.RefreshData();

            txtSubject1.Text = subject1;
            txtContent.Text = content1;
            txtSubject.Text = subject;
            HTMLEditor.Text = content;
        }

        protected void Promotebtn_Click(object sender, EventArgs e)
        {
            if (TopicId != 0 && UserOrOperatorId != 0)
            {
                int vote = TopicPromoted.GetPromotedVote(UserOrOperatorId, TopicId);
                if (vote < 1)
                {
                    int Resonse = TopicPromoted.InsertPromoteData(UserOrOperatorId, TopicId);
                    if (Resonse != 0)
                    {
                        Promotebtn.Text = "Promoted [" + TopicPromoted.GetPromotedVote(-1, TopicId) + "]";
                        Promotebtn.Enabled = false;
                    }
                }

                else { Promotebtn.Text = "[" + PromotedVoteValue + "]"; Promotebtn.Enabled = false; }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
      
    }
}
