using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.UI.AdminPanel.Annoucement
{
    public partial class EditAnnoucement : AdminBasePage
    {
        #region Page Property
        public int AnnoucementId
        {
            get
            {
                CheckQueryString("Id");
                return Convert.ToInt32(Request.QueryString["Id"]);
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumAnnouncements);
            this.Title = "Edit Annoucement";
            try
            {
                if (!IsPostBack)
                {
                    AnnouncementWithPermissionCheck Annoucement = 
                        AnnoucementProcess.GetAnnoucement(
                        CurrentUserOrOperator.UserOrOperatorId,
                        CurrentUserOrOperator.SiteId, AnnoucementId);

                    InitAnnoucement(Annoucement);
                }
               
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        #region Control Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dtStart = DateTime.Parse(this.txtStartTime.Text.Trim());
                DateTime dtEnd = DateTime.Parse(this.txtExpireTime.Text.Trim());
                if (this.ddlForum.SelectedIndex < 0)
                    throw new Exception("选中一个Forum");//test
                AnnoucementProcess.UpdateAnnoucement(
                    CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.IfOperator,
                    CurrentUserOrOperator.SiteId, AnnoucementId,
                    this.tbSubject.Text, CurrentUserOrOperator.UserOrOperatorId,
                    dtStart, dtEnd, htmlEditor.Text, GetSelectedForumIds());
                Response.Redirect("AnnoucementList.aspx?siteId=" + SiteId, false);
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }

        }
        #endregion

        #region Custom Method

        protected void InitAnnoucement(AnnouncementWithPermissionCheck Annoucement)
        {
            //Init Base
            this.tbSubject.Text = Annoucement.Subject;
            this.txtStartTime.Text = Annoucement.BeginDate.ToString("MM-dd-yyyy");
            this.txtExpireTime.Text = Annoucement.ExpireDate.ToString("MM-dd-yyyy");
            this.htmlEditor.Text = Annoucement.Content;
            //Init Forum
            int[] forumIdsOfAnnoucement = GetForumsOfAnnoucement();
            int[] AllForumIdsOfSite;
            string[] AllForumPaths;
            GetAllForumsOfSite(out AllForumPaths, out AllForumIdsOfSite);
            SetAllCategoryAndFourmsOfSite(AllForumPaths, AllForumIdsOfSite);
            SetForumOfAnnoucementChoose(forumIdsOfAnnoucement);
        }

        protected int[] GetSelectedForumIds()
        {
            List<int> ForumIds = new List<int>();
            foreach (ListItem item in this.ddlForum.Items)
            {
                if (item.Selected == true)
                {
                    int forumId = int.Parse(item.Value);
                    //the code here to check the forum is exsit
                    ForumWithPermissionCheck forum = ForumProcess.GetForumByForumId(
                        SiteId, CurrentUserOrOperator.UserOrOperatorId, forumId);
                    ForumIds.Add(forumId);
                }
            }
            return ForumIds.ToArray<int>();
        }

        protected int[] GetForumsOfAnnoucement()
        {
            List<int> forumIds = new List<int>();
            string[] forumPaths;
            ForumWithPermissionCheck[] forums = ForumProcess.GetForumsofAnnoucement(
                AnnoucementId, SiteId, CurrentUserOrOperator.UserOrOperatorId,out forumPaths);
            foreach (ForumWithPermissionCheck forum in forums)
            {
                forumIds.Add(forum.ForumId);
            }
            return forumIds.ToArray<int>();
        }

        protected void GetAllForumsOfSite(out string[] forumPaths, out int[] forumIds)
        {
            ForumProcess.GetForumsOfSite(SiteId,CurrentUserOrOperator.UserOrOperatorId,out forumPaths,out forumIds);
        }

        protected void SetAllCategoryAndFourmsOfSite(string[] forumPaths, int[] forumIds)
        {
            for (int i = 0; i < forumPaths.Length; i++)
            {
                this.ddlForum.Items.Add(new ListItem(forumPaths[i], forumIds[i].ToString()));
            }
        }

        protected void SetForumOfAnnoucementChoose(int[] forumIdsOfAnnoucement)
        {
            for (int i = 0; i < forumIdsOfAnnoucement.Length; i++)
            {
                foreach (ListItem item in this.ddlForum.Items)
                {
                    if (item.Value.ToString() == forumIdsOfAnnoucement[i].ToString())
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
        }
        #endregion
    }
}
