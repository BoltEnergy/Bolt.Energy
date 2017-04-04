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

namespace Com.Comm100.Forum.UI.AdminPanel.Annoucement
{
    public partial class AnnoucementList : AdminBasePage
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

        public string ShowStyle1 {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumAnnouncements);
                this.Title = "Announcements";
                if (!IsPostBack)
                {
                    /*default sort*/
                    this.SortFiled = "AnnouncementStartDate";
                    this.SortMethod = "desc";
                    this.SortFiledImage = imgBeginDate;
                    ChangeSort();

                    RefreshData();
                }
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = exp.Message;
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
                    RefreshData();
                }
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
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = exp.Message;
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
                strForums += forumPath + "<br/>";
            }
            return strForums;
        }

        private void RefreshData()
        {
            /*To Show current Image*/
            SortFiledImage.Visible = true;

            int count = 0;
            AnnouncementWithPermissionCheck[] annoucements = AnnoucementProcess.GetAnnoucementsOfSiteByQueryAndPaging(
                CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.SiteId,
                this.aspnetPager.PageIndex + 1, this.aspnetPager.PageSize, this.txtKeywords.Text, 
                GetQueryStringForumId,SortFiled, SortMethod, out count);

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
        #endregion
    }
}
