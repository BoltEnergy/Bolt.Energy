using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.UI.AdminPanel.Annoucement
{
    public partial class CreateAnnoucement : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumAnnouncements);
                this.Title = "New Annoucement";
                if (!IsPostBack)
                {
                    /*Init Forums Control*/
                    int[] AllForumIdsOfSite;
                    string[] AllForumPaths;
                    GetAllForumsOfSite(out AllForumPaths, out AllForumIdsOfSite);
                    SetAllCategoryAndFourmsOfSite(AllForumPaths, AllForumIdsOfSite);
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
                AnnoucementProcess.AddAnnoucement(CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.SiteId,
                    this.tbSubject.Text, CurrentUserOrOperator.UserOrOperatorId,
                    dtStart, dtEnd, htmlEditor.Text, GetSelectedForumId());
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
        protected void GetAllForumsOfSite(out string[] forumPaths, out int[] forumIds)
        {
            ForumProcess.GetForumsOfSite(SiteId, CurrentUserOrOperator.UserOrOperatorId, out forumPaths, out forumIds);
        }
        protected void SetAllCategoryAndFourmsOfSite(string[] forumPaths, int[] forumIds)
        {
            for (int i = 0; i < forumPaths.Length; i++)
            {
                this.ddlForum.Items.Add(new ListItem(forumPaths[i], forumIds[i].ToString()));
            }
        }
        protected int[] GetSelectedForumId()
        {
            List<int> ForumIds = new List<int>();
            foreach (ListItem item in this.ddlForum.Items)
            {
                if (item.Selected == true)
                {
                    int fourmId = int.Parse(item.Value);
                    //the code here to check the forum is exsit
                    ForumWithPermissionCheck forum = ForumProcess.GetForumByForumId(
                        SiteId, CurrentUserOrOperator.UserOrOperatorId, fourmId);
                    ForumIds.Add(fourmId);
                }
            }
            return ForumIds.ToArray<int>();
        }
        #endregion
    }
}
