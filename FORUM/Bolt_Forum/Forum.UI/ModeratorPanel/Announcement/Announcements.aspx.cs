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

namespace Com.Comm100.Forum.UI.ModeratorPanel.Announcement
{
    public partial class Announcements : Com.Comm100.Forum.UI.ModeratorPanel.ModeratorBasePage
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
                if (Request.QueryString["forumId"] != null)
                {
                    try
                    {
                        forumId = Convert.ToInt32(Request.QueryString["forumId"]);
                    }
                    catch
                    { }
                }
                return forumId;
            }
        }
        #endregion

        string ErrorLoad;
        string ErrorDelete;
        string ErrorRedirect;
        string ForumAll;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Announcements_ErrorLoad];
                ErrorDelete = Proxy[EnumText.enumForum_Announcements_ErrorDelete];
                ErrorRedirect = Proxy[EnumText.enumForum_Public_RedirectError];
                ForumAll = Proxy[EnumText.enumForum_Announcement_DropDownListForumAll];
                this.lblTitle.Text = Proxy[EnumText.enumForum_Announcements_Title];
                this.lblSubTitle.Text = Proxy[EnumText.enumForum_Announcements_SubTitle];
                this.btnQuery1.Text = Proxy[EnumText.enumForum_Announcement_BuutonQuery];
                this.btnQuery2.Text = Proxy[EnumText.enumForum_Announcement_BuutonQuery];
                this.btnNewAnnouncement1.Text = Proxy[EnumText.enumForum_Announcement_ButtonNewAnnouncement];
                this.btnNewAnnouncement2.Text = Proxy[EnumText.enumForum_Announcement_ButtonNewAnnouncement];
                this.btnCancel1.Text = Proxy[EnumText.enumForum_Announcement_BuutonCancel];
                this.btnCancel2.Text = Proxy[EnumText.enumForum_Announcement_BuutonCancel];
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + exp.Message;
                this.lblMessage.Visible = true;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((ModeratorMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumAnnouncements); 
                if (!IsPostBack)
                {
                    this.SortFiled = "AnnouncementStartDate";
                    this.SortMethod = "desc";
                    ViewState["Keywords"] = "";
                    ViewState["Forum"] = IfFromOtherPage ? GetQueryStringForumId.ToString() : "0";
                    this.SortFiledImage = imgBeginDate;
                    SortFiledImage.ImageUrl = "~/images/sort_down.gif";
                    SortFiledImage.Visible = true;
                    PageInit();
                    RefreshData();
                }
            }
            catch (Exception exp)
            {
                this.lblMessage.Text =ErrorLoad+ exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        private void PageInit()
        {
            this.btnCancel1.Visible = IfFromOtherPage;
            this.btnCancel2.Visible = IfFromOtherPage;
            this.btnNewAnnouncement1.Visible = !IfFromOtherPage;
            this.btnNewAnnouncement2.Visible = !IfFromOtherPage;
            this.ddlForum.Items.Add(new ListItem(ForumAll, "0"));
            ForumWithPermissionCheck[] forums = ForumProcess.GetForumsOfModerator(this.SiteId, this.UserOrOperatorId);
            for (int i = 0; i < forums.Length; i++)
            {
                this.ddlForum.Items.Add(new ListItem(forums[i].Name, forums[i].ForumId.ToString()));
            }
            if (IfFromOtherPage)
            {
                this.ddlForum.SelectedValue = GetQueryStringForumId.ToString();
                this.ddlForum.Enabled = false;
            }
        }
        
        private void RefreshData()
        {
            int forumId=Convert.ToInt32(ViewState["Forum"]);
            int pageSize=this.aspnetPager.PageSize;
            int pageIndex=this.aspnetPager.PageIndex+1;
            string subject = ViewState["Keywords"].ToString();
            string orderField=SortFiled;
            string orderDirection=SortMethod;
            int count=0;
            AnnouncementWithPermissionCheck[] announcements;
            if (forumId!=0)
            {
                announcements = AnnoucementProcess.GetAnnoucementsOfForumByQueryAndPaging(this.UserOrOperatorId, this.SiteId, pageIndex, pageSize, subject, forumId, orderField, orderDirection, out count);
            }
            else
            {
                count = AnnoucementProcess.GetCountOfAnnouncementsByModerator(this.SiteId, this.UserOrOperatorId, subject);
                announcements = AnnoucementProcess.GetAnnouncementsByModeratorWithQueryAndPaging(this.SiteId,this.UserOrOperatorId,pageIndex,pageSize,subject,orderField,orderDirection);
            }
            if (count == 0)
            {
                aspnetPager.Visible = false;
                rpAnnoucements.DataSource = null;
                rpAnnoucements.DataBind();
            }
            else
            {
                aspnetPager.CWCDataBind(this.rpAnnoucements, announcements, count);
                aspnetPager.Visible = true;
            }

        }
        
        #region Controls Event
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["Keywords"] = this.txtKeywords.Text;
                ViewState["Forum"] = this.ddlForum.SelectedValue;
                this.aspnetPager.PageIndex = 0;
                RefreshData();
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void btnNewAnnouncement_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("AddAnnouncement.aspx?siteId={0}", this.SiteId));
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorRedirect + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("../Forum/Forums.aspx?siteId={0}", this.SiteId));
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorRedirect + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void btnSort_click(object sender, CommandEventArgs e)
        {
            try
            {
                SetSortField(e.CommandName);
                this.aspnetPager.PageIndex = 0;
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text =ErrorLoad+ exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void rpData_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    try
                    {
                        int topicId = Convert.ToInt32(e.CommandArgument);
                        int forumId = Convert.ToInt32(ViewState["Forum"]);
                        if (forumId == 0)
                        {
                            AnnoucementProcess.DeleteAnnouncementByModeratorWithAllForum(this.SiteId, this.UserOrOperatorId, topicId);
                        }
                        else
                        {
                            AnnoucementProcess.DeleteAnnouncementAndForumRelation(this.SiteId, this.UserOrOperatorId, forumId, topicId);
                        }
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
                this.lblMessage.Text =ErrorLoad+ exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #region pager event
        protected void aspnetPager_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs e)
        {
            try
            {
                RefreshData();
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = exp.Message;
            }
        }
        protected void aspnetPager_ChangePageSize(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.ChangePageSizeEventArgs e)
        {
            try
            {
                RefreshData();
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = exp.Message;
            }
        }
        #endregion
        #endregion
        
        private void SetSortField(string sortFiled)
        {
            switch (sortFiled)
            {
                #region Sort Filed
                case "BeginDate":
                    {
                        SortFiled = "AnnouncementStartDate";
                        SortFiledImage = imgBeginDate;
                        ChangeSort();
                        break;
                    }
                case "ExpireDate":
                    {
                        SortFiled = "AnnouncementEndDate";
                        SortFiledImage = imgExpireDate;
                        ChangeSort();
                        break;
                    }
                case "PostTime":
                    {
                        SortFiled = "PostTime";
                        SortFiledImage = imgPostTime;
                        ChangeSort();
                        break;
                    }
                case "CreateUser":
                    {
                        SortFiled = "CreateUser";
                        SortFiledImage = imgCreateUser;
                        ChangeSort();
                        break;
                    }
                #endregion
            }
        }

        protected string GetForumsOfAnnoucement(int topicId)
        {
            string[] forumPaths;
            ForumWithPermissionCheck[] forums = ForumProcess.GetForumsofAnnoucement(
                topicId, SiteId, this.UserOrOperatorId, out forumPaths);
            string strForums = "";
            foreach (string forumPath in forumPaths)
            {
                strForums += forumPath + "<br/>";
            }
            return strForums;
        }

        protected int GetForumIdOfAnnouncement(int announcementId)
        {
            int forumId;
            string[] forumPaths;
            ForumWithPermissionCheck[] forums = ForumProcess.GetForumsofAnnoucement(
                announcementId, SiteId, CurrentUserOrOperator.UserOrOperatorId, out forumPaths);
            forumId = forums[0].ForumId;


            return forumId;
        }
    }
}
