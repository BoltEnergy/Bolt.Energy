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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.Announcement
{
    public partial class CreateAnnouncement : AdminBasePage
    {
        string ErrorLoad;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_AnnouncementCreate_ErrorLoad];
                this.lblTitle.Text = Proxy[EnumText.enumForum_AnnouncementCreate_Title];
                this.lblSubTitle.Text = Proxy[EnumText.enumForum_AnnouncementCreate_SubTitle];
                this.btnSave1.Text = Proxy[EnumText.enumForum_Announcement_ButtonSave];
                this.btnSave2.Text = Proxy[EnumText.enumForum_Announcement_ButtonSave];
                this.btnCancel1.Text = Proxy[EnumText.enumForum_Announcement_BuutonCancel];
                this.btnCancel2.Text = Proxy[EnumText.enumForum_Announcement_BuutonCancel];
                this.rfvSubject.ErrorMessage = Proxy[EnumText.enumForum_Announcement_SubjectRequired];
                //this.rfvBeginDate.ErrorMessage = Proxy[EnumText.enumForum_Announcement_BeginDateRequired];
                //this.revBeginDate.ErrorMessage = Proxy[EnumText.enumForum_Announcement_BeginDateFormat];
                //this.rfvExpireDate.ErrorMessage = Proxy[EnumText.enumForum_Announcement_ExpireDateRequired];
                //this.revExpireDate.ErrorMessage = Proxy[EnumText.enumForum_Announcement_ExpireDateFormat];
                //this.CompareValidatorBeginAndExpireTime.ErrorMessage = Proxy[EnumText.enumForum_Announcement_BeginDateShouldEarlierThanExpireDate];
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumAnnouncements);
                this.Title = Proxy[EnumText.enumForum_AnnouncementCreate_Title];
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
                this.lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        #region Control Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //DateTime dtStart = DateTime.Parse(this.txtStartTime.Text.Trim());
                //DateTime dtEnd = DateTime.Parse(this.txtExpireTime.Text.Trim());
                if (this.ddlForum.SelectedIndex < 0)
                    throw new Exception(Proxy[EnumText.enumForum_AnnouncementCreate_ErrorForumRequired]);
                AnnoucementProcess.AddAnnoucement(CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.SiteId,
                     this.tbSubject.Text, CurrentUserOrOperator.UserOrOperatorId,
                     htmlEditor.Text, GetSelectedForumId());
                Response.Redirect("AnnouncementList.aspx?siteId=" + SiteId, false);
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_AnnouncementCreate_ErrorSaveAnnouncement] + exp.Message;
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

        protected void btnCancel1_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("AnnouncementList.aspx?siteid=" + SiteId.ToString(), false);
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_RedirectError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }
    }
}
