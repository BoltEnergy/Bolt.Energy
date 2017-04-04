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
using Com.Comm100.Forum.Language;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.UI.ModeratorPanel.TopicAndPost
{
    public partial class Posts : Com.Comm100.Forum.UI.ModeratorPanel.ModeratorBasePage
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
        string ItemAll;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Post_PostsManagementLoadError];
                ErrorDelete = Proxy[EnumText.enumForum_Post_PostsManagementDeleteError];
                ItemAll = Proxy[EnumText.enumForum_Topic_ForumDropDownListItemAll];
                this.lblTitle.Text = Proxy[EnumText.enumForum_Post_PostsManagementTitle];
                this.lblSunTitle.Text = Proxy[EnumText.enumForum_Post_PostsManagementSubTitleOfModeratorPanel];
                this.btnQuery1.Text = Proxy[EnumText.enumForum_Topic_ButtonQuery];
                this.btnQuery2.Text = Proxy[EnumText.enumForum_Topic_ButtonQuery];
                this.revBeginDate.ErrorMessage = Proxy[EnumText.enumForum_Topic_ErrorTimeFormat]+"<br/>";
                this.revEndDate.ErrorMessage = Proxy[EnumText.enumForum_Topic_ErrorTimeFormat];
            }
            catch(Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError];
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((ModeratorMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumPostManagement);
                //this.Title = "Posts Management";
                if (!IsPostBack)
                {
                    InitFormsControl();

                    /*default sort*/
                    this.SortFiled = "PostTime";
                    this.SortMethod = "desc";
                    this.SortFiledImage = imgPostDate;
                    SortFiledImage.ImageUrl = "~/images/sort_down.gif";
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
                this.lblMessage.Text = ErrorLoad + exp.Message;
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
                lblMessage.Text =ErrorLoad + exp.Message;
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
        private void RefreshData()
        {
            /*To Show Current Image*/
            SortFiledImage.Visible = true;
            /*Check Fileds*/
            int count = 0; int forumId = -1; //int topicId = -1;//int posterId=-1;
            string queryConditions=this.txtKeywords.Text;
            DateTime dtStart = new DateTime();
            DateTime dtEnd = new DateTime();
           // if (this.txtTopicId.Text.Trim() != "")
            //    topicId = Convert.ToInt32(this.txtTopicId.Text.Trim());
            //if(this.txtUser.Value.Trim() != "")
            //    posterId = Convert.ToInt32(this.hdUserId.Value);
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

            count = PostProcess.GetCountOfNotDeletedPostsByModeratorWithQuery(this.SiteId, this.UserOrOperatorId, queryConditions, name, dtStart, dtEnd, forumId);
            PostWithPermissionCheck[] posts = PostProcess.GetNotDeltedPostsByModeratorWithQueryAndPaging(
                this.SiteId, this.UserOrOperatorId, queryConditions,name, dtStart, dtEnd, forumId,
                this.aspnetPager.PageIndex+1, this.aspnetPager.PageSize, SortFiled, SortMethod);

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

        protected void InitFormsControl()
        {
            this.txtEndTime.Text =DateTimeHelper.DateFormateWithoutHours(DateTime.Now);
            this.ddlForums.Items.Clear();
            this.ddlForums.Items.Add(new ListItem(ItemAll, "-1"));

            ForumWithPermissionCheck[] forums = ForumProcess.GetForumsOfModerator(this.SiteId, this.UserOrOperatorId);
            for (int i = 0; i < forums.Length; i++)
            {
                this.ddlForums.Items.Add(new ListItem(forums[i].Name, forums[i].ForumId.ToString()));
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
        public string GetIfAnswer(bool ifAnswer)
        {
            if (!ifAnswer)
                return Proxy[EnumText.enumForum_Public_No].ToString();
            else
                return Proxy[EnumText.enumForum_Public_Yes].ToString();
        }
        #endregion
    }
}
