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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.Announcement
{
    public partial class AnnouncementList : AdminBasePage
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

        #region Language
        string ErrorLoad;
        string ErrorDelete;
        string ErrorRedirect;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_AnnouncementList_ErrorLoad];
                ErrorDelete = Proxy[EnumText.enumForum_AnnouncementList_ErrorDelete];
                ErrorRedirect = Proxy[EnumText.enumForum_Public_RedirectError];
                this.Title = Proxy[EnumText.enumForum_AnnouncementList_Title];
                this.lblTitle.Text = Proxy[EnumText.enumForum_AnnouncementList_Title];
                this.LabelDescription.Text = Proxy[EnumText.enumForum_AnnouncementList_SubTitle];
                this.btnNewAnnouncement1.Text = Proxy[EnumText.enumForum_Announcement_ButtonNewAnnouncement];
                this.btnNewAnnouncement2.Text = Proxy[EnumText.enumForum_Announcement_ButtonNewAnnouncement];
                this.btnQuery1.Text = Proxy[EnumText.enumForum_Announcement_BuutonQuery];
                this.btnQuery2.Text = Proxy[EnumText.enumForum_Announcement_BuutonQuery];
                this.btnCancel1.Text = Proxy[EnumText.enumForum_Announcement_BuutonCancel];
                this.btnCancel2.Text = Proxy[EnumText.enumForum_Announcement_BuutonCancel];
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumAnnouncements);
                if (!IsPostBack)
                {
                    /*default sort*/
                    this.SortFiled = "PostTime";
                    this.SortMethod = "asc";
                    this.SortFiledImage = imgPostTime;
                    ChangeSort();

                    this.btnCancel1.Visible = IfFromOtherPage;
                    this.btnCancel2.Visible = IfFromOtherPage;
                    this.btnNewAnnouncement1.Visible = !IfFromOtherPage;
                    this.btnNewAnnouncement2.Visible = !IfFromOtherPage;

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

        #region Control Event
        protected void rpData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    AnnoucementProcess.DeleteAnnoucement(
                        CurrentUserOrOperator.UserOrOperatorId, SiteId, Convert.ToInt32(e.CommandArgument));
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
                this.aspnetPager.PageIndex = 0;
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion

        #region Custom Method

        protected string GetForumsOfAnnoucement(int annoucementId)
        {
            string[] forumPaths;
            ForumWithPermissionCheck[] forums = ForumProcess.GetForumsofAnnoucement(
                annoucementId, SiteId, CurrentUserOrOperator.UserOrOperatorId, out forumPaths);
            string strForums = "";
            foreach (string forumPath in forumPaths)
            {
                strForums += Server.HtmlEncode(forumPath) + "<br/>";
            }
            return strForums;
        }
        protected string GetForumsOfAnnoucementTitle(int annoucementId)
        {
            string strForums = GetForumsOfAnnoucement(annoucementId);
            return strForums.Replace("<br/>", "&#13");
        }
        protected int GetForumIdOfAnnouncement(int announcementId)
        {
            int _forumId;
            string[] forumPaths;
            ForumWithPermissionCheck[] forums = ForumProcess.GetForumsofAnnoucement(
                announcementId, SiteId, CurrentUserOrOperator.UserOrOperatorId, out forumPaths);
            _forumId = forums[0].ForumId;


            return _forumId;
        }

        private void RefreshData()
        {
            /*To Show current Image*/
            SortFiledImage.Visible = true;

            int count = 0;
            AnnouncementWithPermissionCheck[] annoucements = AnnoucementProcess.GetAnnoucementsOfSiteByQueryAndPaging(
                CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.SiteId,
                this.aspnetPager.PageIndex + 1, this.aspnetPager.PageSize,
                Server.HtmlEncode(this.txtKeywords.Text),
                GetQueryStringForumId, SortFiled, SortMethod, out count);

            if (count == 0)
            {
                aspnetPager.Visible = false;
                rpAnnoucements.DataSource = null;
                rpAnnoucements.DataBind();
            }
            else
            {
                aspnetPager.CWCDataBind(this.rpAnnoucements, annoucements, count);
                aspnetPager.Visible = true;
            }
        }

        private void SetSortField(string sortFiled)
        {
            switch (sortFiled)
            {
                #region Sort Filed
                //case "BeginDate":
                //    {
                //        SortFiled = "AnnouncementStartDate";
                //        SortFiledImage = imgBeginDate;
                //        ChangeSort();
                //        break;
                //    }
                //case "ExpireDate":
                //    {
                //        SortFiled = "AnnouncementEndDate";
                //        SortFiledImage = imgExpireDate;
                //        break;
                //    }
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
        #endregion

        protected void btnNewAnnouncemengt1_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("CreateAnnouncement.aspx?SiteId=" + SiteId, false);
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = ErrorRedirect + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("../Forums/ForumList.aspx?siteId={0}", SiteId), false);
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorRedirect + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
    }
}
