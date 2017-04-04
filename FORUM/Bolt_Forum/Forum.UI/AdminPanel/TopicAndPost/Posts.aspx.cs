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

namespace Com.Comm100.Forum.UI.AdminPanel.TopicAndPost
{
    public partial class Posts : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        #region Page Property
        public string SortFiled
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

        #region Language
        string ErrorLoad;
        string ErrorDelete;
        protected override void InitLanguage()
        {
            try
            {
                this.lblTitle.Text = Proxy[EnumText.enumForum_Post_PostsManagementTitle];
                this.lblSubTitle.Text = Proxy[EnumText.enumForum_Post_PostsManagementSubTitleOfAdminPanel];
                ErrorLoad=Proxy[EnumText.enumForum_Post_PostsManagementLoadError];
                ErrorDelete = Proxy[EnumText.enumForum_Post_PostsManagementDeleteError];
                this.Title = Proxy[EnumText.enumForum_Post_PostsManagementTitle];
                this.btnQuery1.Text = Proxy[EnumText.enumForum_Topic_ButtonQuery];
                this.btnQuery2.Text = Proxy[EnumText.enumForum_Topic_ButtonQuery];
                this.btnSelectUser.Text = Proxy[EnumText.enumForum_Topic_ButtonSelectUser];
                this.revBeginDate.ErrorMessage = Proxy[EnumText.enumForum_Topic_ErrorTimeFormat]+"<br />";
                this.revEndDate.ErrorMessage = Proxy[EnumText.enumForum_Topic_ErrorTimeFormat];

            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }

        }
        #endregion Language
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumPostManagement);
                if (!IsPostBack)
                {
                    InitForumsControl();

                    /*default sort*/
                    this.SortFiled = "PostTime";
                    this.SortMethod = "desc";
                    this.SortFiledImage = imgPostDate;
                    //ChangeSort();

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

        #region Controls Event
        protected void rpData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    try
                    {
                        PostProcess.LogicDeletePost(
                           SiteId, CurrentUserOrOperator.UserOrOperatorId, Convert.ToInt32(e.CommandArgument));
                    }
                    catch (Exception exp)
                    {
                        this.lblMessage.Text = ErrorDelete + exp.Message;
                        LogHelper.WriteExceptionLog(exp);
                    }
                }
                RefreshData();
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorDelete + exp.Message;
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

        protected void btnSort_click(object sender, CommandEventArgs e)
        {
            try
            {
                SetSortField(e.CommandName.ToString());
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnQuery1_Click(object sender, EventArgs e)
        {
            try
            {
                this.aspnetPager.PageIndex = 0;//default form first page
                
                DateTime dtStart = new DateTime();
                DateTime dtEnd = new DateTime();
                if (this.txtStartTime.Text.Trim()!="")
                    dtStart = DateTimeHelper.LocalDateStringToUtc(this.txtStartTime.Text.Trim() + " 00:00:00");
                if (this.txtEndTime.Text.Trim() != "")
                    dtEnd = DateTimeHelper.LocalDateStringToUtc(this.txtEndTime.Text.Trim() + " 23:59:59");

                if (DateTime.Compare(dtStart, dtEnd) > 0)
                {
                    ExceptionHelper.ThrowPublicDateFromEarlierThanToDateException();
                }
                else
                    RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad +exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion

        #region Custom Methods
        private void RefreshData()
        {
            /*To Show Current Image*/
            SortFiledImage.Visible = true;
            /*Check Fileds*/
            int count = 0; int forumId = -1; int topicId = -1;
            DateTime dtStart = new DateTime();
            DateTime dtEnd = new DateTime();
            //if (this.txtTopicId.Text.Trim() != "")
            //    topicId = Convert.ToInt32(this.txtTopicId.Text.Trim());
            //int posterId=-1;
            //if (txtUser.Value== "")
            //    this.hdUserId.Value = "";
            //else
            //    posterId = Convert.ToInt32(this.hdUserId.Value);
            string name = txtUser.Value;
            if (this.txtStartTime.Text.Trim() != "")
            {
                dtStart = Convert.ToDateTime(this.txtStartTime.Text.Trim());
                
                dtStart = Com.Comm100.Framework.Common.DateTimeHelper.LocalToUTC(dtStart);
            }
            if (this.txtEndTime.Text.Trim() != "")
            {
                dtEnd = Convert.ToDateTime(this.txtEndTime.Text.Trim());
                dtEnd=dtEnd.AddHours(23);
                dtEnd=dtEnd.AddMinutes(59);
                dtEnd=dtEnd.AddSeconds(59);
                dtEnd = Com.Comm100.Framework.Common.DateTimeHelper.LocalToUTC(dtEnd);
                //dtEnd = dtEnd.AddHours(16);
            }
            if (this.ddlForums.SelectedValue != "")
                forumId = Convert.ToInt32(this.ddlForums.SelectedValue);

            PostWithPermissionCheck[] posts = PostProcess.GetNotDeletedPostsByQueryAndPaging(
                SiteId, CurrentUserOrOperator.UserOrOperatorId,
                this.txtKeywords.Text, topicId, name, dtStart, dtEnd,
                this.aspnetPager.PageIndex + 1, this.aspnetPager.PageSize,
                SortFiled, SortMethod, out count, forumId);

            if (count == 0)
            {
                aspnetPager.Visible = false;
                rptData.DataSource = null;
                rptData.DataBind();
            }
            else
            {
                aspnetPager.CWCDataBind(this.rptData, posts, count);
                aspnetPager.Visible = true;
            }
        }

        protected void InitForumsControl()
        {
            this.ddlForums.Items.Clear();
            this.ddlForums.Items.Add(new ListItem("All", "-1"));

            string[] forumPaths; int[] forumIds;
            ForumWithPermissionCheck[] forums = ForumProcess.GetForumsOfSite(
                SiteId, CurrentUserOrOperator.UserOrOperatorId, out forumPaths, out forumIds);
            for (int i = 0; i < forumPaths.Length; i++)
            {
                this.ddlForums.Items.Add(new ListItem(forumPaths[i], forumIds[i].ToString()));
            }
        }

        private void SetSortField(string sortFiled)
        {
            switch (sortFiled)
            {
                #region Sort Filed
                case "Post Date":
                    {
                       SortFiledImage = this.imgPostDate;
                        SortFiled = "PostTime";
                        ChangeSort();
                        break;
                    }
                case "Post User":
                    {
                        SortFiledImage = this.imgPostUser;
                        SortFiled = "PostUser";
                        ChangeSort();
                        break;
                    }
                case "Is Answer":
                    {
                        SortFiledImage = this.imgIsAnswer;
                        SortFiled = "IfAnswer";
                        ChangeSort();
                        break;
                    }
                #endregion
            }
        }

        public string GetForumId(int topicId)
        {
            TopicWithPermissionCheck topic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, topicId);
            return topic.ForumId.ToString();
        }

        public string GetQueryString(int postId)
        {
            PostWithPermissionCheck post = PostProcess.GetPostByPostId(SiteId, UserOrOperatorId, false, postId);
            //TopicWithPermissionCheck topic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, post.TopicId);
            bool ifAnnouncement;
            TopicBase topicOrAnnouncement = TopicProcess.CreateTopic(UserOrOperatorId, SiteId, post.TopicId,
                out ifAnnouncement);
            if (ifAnnouncement)
            {
                ForumWithPermissionCheck[] forumsOfAnnouncement =
                    ForumProcess.GetForumsofAnnoucement(topicOrAnnouncement.TopicId, SiteId, UserOrOperatorId);
                return "postId=" + postId + "&topicId=" + post.TopicId + "&ForumId=" +
                          forumsOfAnnouncement[0].ForumId;
            }
            else
            {
                return "postId=" + postId + "&topicId=" + post.TopicId + "&ForumId=" +
                    (topicOrAnnouncement as TopicWithPermissionCheck).ForumId;
            }
            
        }

        public string GetIfAnswer(bool ifAnswer)
        {
            if (!ifAnswer)
                return Proxy[EnumText.enumForum_Public_No];
            else
                return Proxy[EnumText.enumForum_Public_Yes];
        }
        #endregion

    }


}
