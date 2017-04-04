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

namespace Com.Comm100.Forum.UI.ModeratorPanel.Announcement
{
    public partial class AddAnnouncement : Com.Comm100.Forum.UI.ModeratorPanel.ModeratorBasePage
    {
        string ErrorLoad;
        string ErrorAddNewAnnouncement;
        string ErrorRedirect;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_AnnouncementAdd_ErrorLoad];
                ErrorAddNewAnnouncement = Proxy[EnumText.enumForum_AnnouncementAdd_ErrorAddAnnouncement];
                ErrorRedirect = Proxy[EnumText.enumForum_Public_RedirectError];
                this.lblTitle.Text = Proxy[EnumText.enumForum_AnnouncementAdd_Title];
                this.lblSubTitle.Text = Proxy[EnumText.enumForum_AnnouncementAdd_SubTitle];
                this.btnSave1.Text = Proxy[EnumText.enumForum_Announcement_ButtonSave];
                this.btnSave2.Text = Proxy[EnumText.enumForum_Announcement_ButtonSave];
                this.btnCancel1.Text = Proxy[EnumText.enumForum_Announcement_BuutonCancel];
                this.btnCancel2.Text = Proxy[EnumText.enumForum_Announcement_BuutonCancel];
                this.rfvSubject.ErrorMessage = Proxy[EnumText.enumForum_Announcement_SubjectRequired];
                this.revBeginDate.ErrorMessage = Proxy[EnumText.enumForum_Announcement_BeginDateFormat];
                this.rfvBeginDate.ErrorMessage = Proxy[EnumText.enumForum_Announcement_BeginDateRequired];
                this.revExpireDate.ErrorMessage = Proxy[EnumText.enumForum_Announcement_ExpireDateFormat];
                this.rfvExpireDate.ErrorMessage = Proxy[EnumText.enumForum_Announcement_ExpireDateRequired];
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError];
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((ModeratorMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumAnnouncements);
                //this.Title = "New Annoucement";
                if (!IsPostBack)
                {
                    /*Init Forums Control*/
                    InitLanguage();
                    PageInit();
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
            this.txtStartTime.Text = DateTimeHelper.DateFormateWithoutHours(DateTime.Now);
            this.ddlForum.DataTextField = "Name";
            this.ddlForum.DataValueField = "ForumId";
            this.ddlForum.DataSource=ForumProcess.GetForumsOfModerator(this.SiteId,this.UserOrOperatorId);
            this.ddlForum.DataBind();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string subject=this.txtSubject.Text;
                string content=this.htmlEditorContent.Text;
                DateTime startDate = Convert.ToDateTime(this.txtStartTime.Text.Trim());
                DateTime expireDate = Convert.ToDateTime(this.txtExpireTime.Text.Trim());
                if (startDate > expireDate)
                    throw new Exception(Proxy[EnumText.enumForum_Announcement_BeginDateShouldEarlierThanExpireDate]);
                int forumId = Convert.ToInt32(this.ddlForum.SelectedValue);
                AnnoucementProcess.AddAnnoucement(this.UserOrOperatorId, this.SiteId, subject, this.UserOrOperatorId, content, new int[] { forumId });
                Response.Redirect("Announcements.aspx?siteId=" + SiteId, false);
            }
            catch(Exception exp)
            {
                this.lblMessage.Text =ErrorAddNewAnnouncement +exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("Announcements.aspx?siteId={0}", SiteId));
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorRedirect + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
    }
}
