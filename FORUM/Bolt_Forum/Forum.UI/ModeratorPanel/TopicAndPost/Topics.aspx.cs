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
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.ModeratorPanel.TopicAndPost
{
    public partial class Topics : Com.Comm100.Forum.UI.ModeratorPanel.ModeratorBasePage
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

        #region Custom Property
        public bool IfFromOtherPage
        {
            get
            {
                if (GetQueryStringForumId != -1)
                    return true;
                else
                    return false;
            }
        }
        public int GetQueryStringForumId
        {
            get
            {
                int forumId = -1;
                if (Request.QueryString["forumid"] != null)
                {
                    try
                    {
                        forumId = Convert.ToInt32(Request.QueryString["forumid"]);
                    }
                    catch
                    { }
                }
                return forumId;
            }
        }

        public string ShowStyle1
        {
            get
            {
                if (IfFromOtherPage) return "display:none;";
                else return "";
            }
        }
        public string ShowStyle2
        {
            get
            {
                if (!IfFromOtherPage) return "display:none;";
                else return "";
            }
        }
        #endregion

        string ErrorLoad;
        string ErrorDelete;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Topic_TopicsManagementLoadError];
                ErrorDelete = Proxy[EnumText.enumForum_Topic_TopicsManagementDeleteError];
                this.lblTitle.Text = Proxy[EnumText.enumForum_Topic_TopicsManagementTitle];
                this.lblSubTitle.Text = Proxy[EnumText.enumForum_Topic_TopicsManagementSubTitleOfModeratorPanel];
                this.btnQuery1.Text = Proxy[EnumText.enumForum_Topic_ButtonQuery];
                this.btnQuery2.Text = Proxy[EnumText.enumForum_Topic_ButtonQuery];
                this.revBeginDate.ErrorMessage = Proxy[EnumText.enumForum_Topic_ErrorTimeFormat]+"<br/>";
                this.revEndDate.ErrorMessage = Proxy[EnumText.enumForum_Topic_ErrorTimeFormat];
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError]+exp.Message;;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((ModeratorMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumTopicManagement);
                //this.Title = "Topics Management";
                if (!IsPostBack)
                {
                    InitFormControl();

                    InitForumSelected();

                    /*default sort*/
                    this.SortFiled = "LastPostTime";
                    this.SortMethod = "desc";
                    this.SortFiledImage = imgLastPost;
                    SortFiledImage.ImageUrl = "~/images/sort_down.gif";
                    SortFiledImage.Visible = true;

                    RefreshData();
                }
                CurrentSort();
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = exp.Message;
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
                    TopicProcess.LogicDeleteTopic(
                        CurrentUserOrOperator.UserOrOperatorId, SiteId, Convert.ToInt32(e.CommandArgument));
                }
                RefreshData();
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = exp.Message;
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
                lblMessage.Text = exp.Message;
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
                lblMessage.Text = exp.Message;
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
                lblMessage.Text = exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnQuery1_Click(object sender, EventArgs e)
        {
            try
            {
                this.aspnetPager.PageIndex = 0;
               
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
                lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion

        #region Custom Methods
        public string GetForumPathOfPost(int topicId)
        {
            return TopicProcess.GetTopicPath(
                CurrentUserOrOperator.UserOrOperatorId,
                SiteId, topicId);

        }

        protected void InitFormControl()
        {
            this.txtEndTime.Text = DateTimeHelper.DateFormateWithoutHours(DateTime.Now);
            this.ddlForums.Items.Clear();
            this.ddlForums.Items.Add(new ListItem("All", "-1"));

            ForumWithPermissionCheck[] forums = ForumProcess.GetForumsOfModerator(this.SiteId,this.UserOrOperatorId);
            for (int i = 0; i < forums.Length; i++)
            {
                this.ddlForums.Items.Add(new ListItem(
                    forums[i].Name, forums[i].ForumId.ToString()));
            }
        }

        public void InitForumSelected()
        {
            int slecectForumId = this.GetQueryStringForumId;
            bool ifFindItem = false;
            foreach (ListItem item in this.ddlForums.Items)
            {
                if (item.Value == slecectForumId.ToString())
                {
                    this.ddlForums.SelectedValue = item.Value;
                    if(IfFromOtherPage)
                        this.ddlForums.Enabled = false;
                    ifFindItem = true;
                    break;
                }
            }
            if (ifFindItem == false)
            {
                ExceptionHelper.ThrowForumNotExistException(slecectForumId);
            }
        }

        public string ShowTopicState(bool ifclosed, bool ifMarkedAsAnswer)
        {
            if (ifclosed)
                return "<span title=\"" + Proxy[EnumText.enumForum_Topic_StatusClosed] + "\"><img src='../../Images/status/close.gif'/></span>";
            else if (ifMarkedAsAnswer)
                return "<span title=\"" + Proxy[EnumText.enumForum_Topic_StatusMarked] + "\"><img src='../../Images/status/mark.gif'/></span>";
            else
                return "<span title=\"" + Proxy[EnumText.enumForum_Topic_StatusNormal] + "\"><img src='../../Images/status/normal.gif'/></span>";
        
        }

        private void RefreshData()
        {
            /*To show Current Image*/
            SortFiledImage.Visible = true;
            /*Query Fileds*/
            int count = 0;
            int forumId = -1;
            //int posterId = -1;
            string keywords=this.txtKeywords.Text;
            DateTime dtStart = new DateTime();
            DateTime dtEnd = new DateTime();
            //if(this.txtUser.Value!="")
            //    posterId = Convert.ToInt32(hdUserId.Value);
            string name = this.txtUser.Value;
            if (this.txtStartTime.Text.Trim() != "")
            {
                dtStart = Convert.ToDateTime(this.txtStartTime.Text.Trim());
                dtStart = Com.Comm100.Framework.Common.DateTimeHelper.LocalToUTC(dtStart);

            }
            if (this.txtEndTime.Text.Trim() != "")
            {
                dtEnd = Convert.ToDateTime(this.txtEndTime.Text.Trim());
                dtEnd = dtEnd.AddHours(23);
                dtEnd = dtEnd.AddMinutes(59);
                dtEnd = dtEnd.AddSeconds(59);
                dtEnd = Com.Comm100.Framework.Common.DateTimeHelper.LocalToUTC(dtEnd);
            }
            if (this.ddlForums.SelectedValue != "")
                forumId = Convert.ToInt32(this.ddlForums.SelectedValue);

            count = TopicProcess.GetCountOfNotDeletedTopicsByModeratorWithQuery(this.SiteId, this.UserOrOperatorId, name, keywords, dtStart, dtEnd,forumId);
            TopicWithPermissionCheck[] topics = TopicProcess.GetNotDeletedTopicsByModeratorWithQueryAndPaging(this.SiteId, this.UserOrOperatorId,
                name, keywords, dtStart, dtEnd,forumId, this.aspnetPager.PageIndex + 1, this.aspnetPager.PageSize, SortFiled, SortMethod);
            
            if (count == 0)
            {
                aspnetPager.Visible = false;
                rptData.DataSource = null;
                rptData.DataBind();
            }
            else
            {
                aspnetPager.CWCDataBind(this.rptData, topics, count);
                aspnetPager.Visible = true;
            }
        }

        private void SetSortField(string sortFiled)
        {
            switch (sortFiled)
            {
                #region Sort Filed
                case "Topic Starter":
                    {
                        SortFiledImage = this.imgTopicStarter;
                        SortFiled = "Name";
                        ChangeSort();
                        break;
                    }
                case "Post Date":
                    {
                        SortFiledImage = this.imgPostDate;
                        SortFiled = "PostTime";
                        ChangeSort();
                        break;
                    }
                case "Last Post":
                    {
                        SortFiledImage = this.imgLastPost;
                        SortFiled = "LastPostTime";
                        ChangeSort();
                        break;
                    }
                case "Replies":
                    {
                        SortFiledImage = this.imgReplies;
                        SortFiled = "NumberOfReplies";
                        ChangeSort();
                        break;
                    }
                case "Views":
                    {
                        SortFiledImage = this.imgViews;
                        SortFiled = "NumberOfHits";
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

        public string GetFirstPostId(int topicId)
        {
            PostWithPermissionCheck post = PostProcess.GetFirstPostOfTopic(SiteId, topicId, UserOrOperatorId);
            return post.PostId.ToString();
        }
        #endregion
    }
}
