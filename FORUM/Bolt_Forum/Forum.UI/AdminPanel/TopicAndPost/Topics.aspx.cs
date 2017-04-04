#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Web.UI.WebControls;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.TopicAndPost
{
    public partial class Topics : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
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
            //Image img = SortFiledImage;
            if (SortMethod.Equals("desc"))
            {
                SortMethod = "asc";
                SortFiledImage.ImageUrl = "~/images/sort_up.gif";
            }
            else
            {
                SortMethod = "desc";
                SortFiledImage.ImageUrl = "~/images/sort_down.gif";
            }
            SortFiledImage.Visible = true;
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

        #region Language
        string ErrorLoad;
        string ErrorDelete;
        string ItemAll;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Topic_TopicsManagementLoadError];
                ErrorDelete = Proxy[EnumText.enumForum_Topic_TopicsManagementDeleteError];
                ItemAll = Proxy[EnumText.enumForum_Topic_ForumDropDownListItemAll];
                this.btnQuery1.Text = Proxy[EnumText.enumForum_Topic_ButtonQuery];
                this.btnQuery2.Text = Proxy[EnumText.enumForum_Topic_ButtonQuery];
                this.btnSelectUser.Text = Proxy[EnumText.enumForum_Topic_ButtonSelectUser];
                this.lblTitle.Text = Proxy[EnumText.enumForum_Topic_TopicsManagementTitle];
                this.lblSubTitle.Text = Proxy[EnumText.enumForum_Topic_TopicsManagementSubTitleOfAdminPanel];
                this.revStartDate.ErrorMessage = Proxy[EnumText.enumForum_Topic_ErrorTimeFormat] + "<br />";
                this.revEndDate.ErrorMessage = Proxy[EnumText.enumForum_Topic_ErrorTimeFormat];
            }
            catch (Exception ex)
            {;
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }
        #endregion 
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumTopicManagement);
                this.Title = Proxy[EnumText.enumForum_Topic_TopicsManagementTitle];
                if (!IsPostBack)
                {
                    InitForumsControl();

                    InitForumSelected();

                    /*default sort*/
                    this.SortFiled = "LastPostTime";
                    this.SortMethod = "desc";
                    this.SortFiledImage = imgLastPost;
                    SortFiledImage.ImageUrl = "~/images/sort_down.gif";
                    SortFiledImage.Visible = true;
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
                        TopicProcess.LogicDeleteTopic(
                            CurrentUserOrOperator.UserOrOperatorId, SiteId, Convert.ToInt32(e.CommandArgument));
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
                this.lblMessage.Text = ErrorLoad+exp.Message;
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
                lblMessage.Text = ErrorLoad +exp.Message;
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
                if (this.txtEndTime.Text.Trim()!="")
                    dtEnd = DateTimeHelper.LocalDateStringToUtc(this.txtEndTime.Text.Trim() + " 23:59:59");

                if (DateTime.Compare(dtStart, dtEnd) > 0)
                {
                    ExceptionHelper.ThrowPublicDateFromEarlierThanToDateException();
                }
                else
                {
                    RefreshData();
                }


            } 
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad+ exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion

        #region Custom Methods
        public string GetForumPathOfPost(int topicId)
        {
            return Server.HtmlEncode(TopicProcess.GetTopicPath(
                CurrentUserOrOperator.UserOrOperatorId,
                SiteId, topicId));

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

        public void InitForumSelected()
        {
            int slecectForumId = this.GetQueryStringForumId;
            bool ifFindItem = false;
            foreach (ListItem item in this.ddlForums.Items)
            {
                if (item.Value == slecectForumId.ToString())
                {
                    item.Selected = true;
                    this.lbSelectedForum.Text = item.Text;
                    ifFindItem = true;
                    break;
                }
            }
            /*select default*/
            if (ifFindItem == false)
            {
                throw new Exception(Proxy[EnumText.enumForum_Topic_ErrorTimeFormat]);
                //this.ddlForums.SelectedIndex = 0;
                //this.lbSelectedForum.Text = this.ddlForums.Items[0].Text;
            }
        }

        public string ShowTopicState(bool ifclosed, bool ifMarkedAsAnswer)
        {
            //if (ifclosed)
            //    return "<img src='../../Images/status/participate_close.gif'/>";
            //else if (ifMarkedAsAnswer)
            //    return "<img src='../../Images/status/participate_mark.gif'/>";
            //else
            //    return "<img src='../../Images/status/participate_normal.gif'/>";
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
            int count = 0; int forumId = -1;
            DateTime dtStart = new DateTime();
            DateTime dtEnd = new DateTime();
            //int postId = Convert.ToInt32(hdUserId.Value);
            string name = txtUser.Value;
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

            TopicWithPermissionCheck[] topics = TopicProcess.GetNotDeletedTopicsByQueryAndPagingInTopicsOfSite(
                    CurrentUserOrOperator.UserOrOperatorId, SiteId,
                    this.txtKeywords.Text, name, forumId, dtStart, dtEnd,
                    this.aspnetPager.PageIndex + 1, this.aspnetPager.PageSize,
                    SortFiled, SortMethod, out count);

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
                        SortFiledImage= this.imgViews;
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
            PostWithPermissionCheck post = PostProcess.GetFirstPostOfTopic(SiteId,topicId,UserOrOperatorId);
            return post.PostId.ToString();
        }
        #endregion
    }
}
