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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.ModeratorPanel.PostModeration
{
    public partial class WaitingForModeration : Com.Comm100.Forum.UI.ModeratorPanel.ModeratorBasePage
    {
        #region Page Property
        public string SortField
        {
            set { ViewState["SortFiled"] = value; }
            get { return ViewState["SortFiled"].ToString(); }
        }
        public string SortMethod
        {
            set { ViewState["SortMethod"] = value; }
            get { return ViewState["SortMethod"].ToString(); }
        }
        public Image SortFiledImage
        {
            set { ViewState["SortFiledImage"] = value.ID; }
            get
            {
                return this.tbHeader.FindControl(
                    ViewState["SortFiledImage"].ToString()) as Image;
            }
        }
        public void ChangeSort()
        {
            Image img = SortFiledImage;
            if (SortMethod.Equals("desc"))
            {
                SortMethod = "asc";
                img.ImageUrl = "~/images/sort_up.gif";
            }
            else
            {
                SortMethod = "desc";
                img.ImageUrl = "~/images/sort_down.gif";
            }
            img.Visible = true;
        }

        public void CurrentSort()
        {
            if (SortMethod.Equals("asc"))
            {
                SortFiledImage.ImageUrl = "~/images/sort_up.gif";
            }
            else
            {
                SortFiledImage.ImageUrl = "~/images/sort_down.gif";
            }
            //SortFiledImage.Visible = true;
        }
        #endregion

        string ErrorLoad;
        string ErrorAccept;
        string ErrorRefuse;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Post_WaitingModerationLoadError];
                ErrorAccept = Proxy[EnumText.enumForum_Post_WaitingModerationApproveError];
                ErrorRefuse = Proxy[EnumText.enumForum_Post_WaitingModerationRefuseError];
                this.lblTitle.Text = Proxy[EnumText.enumForum_Post_WaitingModerationTitle];
                this.lblSubTitle.Text = Proxy[EnumText.enumForum_Post_WaitingModerationSubTitleOfModeratorPanel];
                this.btnQuery1.Text = Proxy[EnumText.enumForum_Topic_ButtonQuery];
                this.btnQuery2.Text = Proxy[EnumText.enumForum_Topic_ButtonQuery];
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + exp.Message;
                this.lblMessage.Visible = true;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        private List<int> _forumIdsOfModerator;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((ModeratorMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumWaitingForModerationPosts);
                //this.Title = "Waiting for Moderation";
                if (!IsPostBack)
                {
                    //default sort
                    this.SortField = "PostTime";
                    this.SortMethod = "asc";
                    this.SortFiledImage = imgCreateDate;
                    SortFiledImage.ImageUrl = "~/images/sort_up.gif";
                    SortFiledImage.Visible = true;

                    RefreshData();
                }
                CurrentSort();
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        #region Custom Methods
        public string GetForumPathOfPost(int topicId)
        {
            string AllPaths = "";
            bool ifAnnoucement;
            TopicBase topicbase = TopicProcess.CreateTopic(CurrentUserOrOperator.UserOrOperatorId,
                SiteId, topicId, out ifAnnoucement);
            if (ifAnnoucement)
            {
                AnnouncementWithPermissionCheck annoucement = topicbase as AnnouncementWithPermissionCheck;

                string[] fourmPaths;
                ForumWithPermissionCheck[] forumsOfAnnoucement = ForumProcess.GetForumsofAnnoucement(
                    topicId, SiteId, CurrentUserOrOperator.UserOrOperatorId, out fourmPaths);
                foreach (string forumPath in fourmPaths)
                {
                    AllPaths += forumPath + "<br/>";
                }
            }
            else
            {
                return TopicProcess.GetTopicPath(
                    CurrentUserOrOperator.UserOrOperatorId,
                    SiteId, topicId);
            }
            return AllPaths;
        }

        public string GetForumId(int topicId)
        {
            TopicWithPermissionCheck topic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, topicId);
            return topic.ForumId.ToString();
        }

        private void RefreshData()
        {
            /*To Show Current Image*/
            SortFiledImage.Visible = true;

            int count = 0;
            string queryCondition = this.txtKeywords.Text;
            PostWithPermissionCheck[] posts = PostProcess.GetNotDeletedModerationPostsByModeratorWithQueryAndPaging(
                this.SiteId, this.UserOrOperatorId, queryCondition, this.aspnetPager.PageIndex + 1, this.aspnetPager.PageSize,
                SortField, SortMethod, EnumPostOrTopicModerationStatus.WaitingForModeration);
            count = PostProcess.GetCountOfNoDeletedModerationPostsByModeratorWithQuery(this.SiteId, this.UserOrOperatorId, queryCondition, EnumPostOrTopicModerationStatus.WaitingForModeration);

            if (count == 0)
            {
                aspnetPager.Visible = false;
                rpData.DataSource = null;
                rpData.DataBind();
            }
            else
            {
                aspnetPager.CWCDataBind(this.rpData, posts, count);
                aspnetPager.Visible = true;
            }
        }

        private void SetSortField(string sortFiled)
        {
            switch (sortFiled)
            {
                #region Sort Filed
                case "Create Date":
                    {
                        this.SortFiledImage = this.imgCreateDate;
                        SortField = "PostTime";
                        ChangeSort();
                        break;
                    }
                case "Create User":
                    {
                        this.SortFiledImage = this.imgCreateUser;
                        SortField = "Name";
                        ChangeSort();
                        break;
                    }
                #endregion
            }
        }
        #endregion

        #region Control Event
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.aspnetPager.PageIndex = 0;
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void rpData_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            try
            {
                int forumId; int postId;
                GetForumIdAndPostId(out forumId, out postId, Convert.ToString(e.CommandArgument));
                //int postId = Convert.ToInt32(e.CommandArgument);
                switch (e.CommandName)
                {
                    case "Accept":
                        try
                        {
                            PostProcess.AcceptPostModerationByAdminOrModerator(
                             SiteId, CurrentUserOrOperator.UserOrOperatorId, forumId, postId);
                            //PostProcess.AcceptPostModeration(this.SiteId, this.UserOrOperatorId, postId);
                        }
                        catch (Exception exp)
                        {
                            this.lblMessage.Text = ErrorAccept + exp.Message;
                            LogHelper.WriteExceptionLog(exp);
                        }
                        break;
                    case "Refuse":
                        try
                        {
                            PostProcess.RefusePostModerationByAdminOrModerator(
                             SiteId, CurrentUserOrOperator.UserOrOperatorId, forumId, postId);
                            //PostProcess.RefusePostModeration(this.SiteId, this.UserOrOperatorId, postId);
                        }
                        catch (Exception exp)
                        {
                            this.lblMessage.Text = ErrorRefuse + exp.Message;
                            LogHelper.WriteExceptionLog(exp);
                        }
                        break;
                }
                RefreshData();
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void btnSort_click(object sender, CommandEventArgs e)
        {
            try
            {
                SetSortField(e.CommandName.ToString());
                this.aspnetPager.PageIndex = 0;
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #region Pager Event
        protected void aspnetPager_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
        {
            try
            {
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad + exp.Message;
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
                lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion

        protected void rpData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                int forumId;
                PostWithPermissionCheck post = e.Item.DataItem as PostWithPermissionCheck;
                string path = GetForumPathOfPost(post.TopicId, out forumId);

                System.Web.UI.HtmlControls.HtmlAnchor lkSubject = e.Item.FindControl("lkSubject")
                    as System.Web.UI.HtmlControls.HtmlAnchor;
                lkSubject.HRef = "../../Topic.aspx?topicId=" + post.TopicId + "&forumId=" + forumId +
                    "&type=moderation&siteId=" + SiteId + "&PostId=" + post.PostId + "&GotoPost=true#Post" + post.PostId;
                PlaceHolder phPath = e.Item.FindControl("phForumPathOfPost") as PlaceHolder;
                phPath.Controls.Add(new LiteralControl(path));

                (e.Item.FindControl("btnAccept") as ImageButton).CommandArgument = post.PostId + "#" + forumId;
                (e.Item.FindControl("btnRefuse") as ImageButton).CommandArgument = post.PostId + "#" + forumId;
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Post_WaitingModerationQueryError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }

        }

        public string GetForumPathOfPost(int topicId, out int forumId)
        {
            string AllPaths = ""; forumId = 0;
            bool ifAnnoucement;
            TopicBase topicbase = TopicProcess.CreateTopic(CurrentUserOrOperator.UserOrOperatorId,
                SiteId, topicId, out ifAnnoucement);
            if (ifAnnoucement)
            {
                AnnouncementWithPermissionCheck annoucement = topicbase as AnnouncementWithPermissionCheck;

                string[] fourmPaths;
                ForumWithPermissionCheck[] forumsOfAnnoucement = ForumProcess.GetForumsofAnnoucement(
                    topicId, SiteId, CurrentUserOrOperator.UserOrOperatorId, out fourmPaths);
                foreach (string forumPath in fourmPaths)
                {
                    AllPaths += Server.HtmlEncode(forumPath) + "<br/>";
                }

                if (forumsOfAnnoucement.Length > 0)
                {
                    forumId = GetForumId(forumsOfAnnoucement);
                }
            }
            else
            {
                forumId = (topicbase as TopicWithPermissionCheck).ForumId;
                return Server.HtmlEncode(TopicProcess.GetTopicPath(
                    CurrentUserOrOperator.UserOrOperatorId,
                    SiteId, topicId));
            }
            return AllPaths;
        }

        private void GetForumIdAndPostId(out int forumId, out int postId, string cmdArg)
        {
            string[] ids = cmdArg.Split('#');
            postId = Convert.ToInt32(ids[0]);
            forumId = Convert.ToInt32(ids[1]);
        }

        private int GetForumId(ForumWithPermissionCheck[] forumsOfAnnoucement)
        {
            int forumId = (from forumOfAnnoucement in forumsOfAnnoucement
                           where _forumIdsOfModerator.Contains(forumOfAnnoucement.ForumId)
                           select forumOfAnnoucement.ForumId).FirstOrDefault();
            return forumId;
        }

        private void InitForumIdsOfModerator()
        {
            ForumWithPermissionCheck[] forumsOfModerator = ForumProcess.GetForumsOfModerator(
                SiteId, UserOrOperatorId);
            _forumIdsOfModerator = (from forum in forumsOfModerator
                                    select forum.ForumId).ToList();
        }

        #endregion
    }
}
