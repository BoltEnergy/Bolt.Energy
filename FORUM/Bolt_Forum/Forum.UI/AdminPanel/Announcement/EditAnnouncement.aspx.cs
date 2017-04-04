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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.Announcement
{
    public partial class EditAnnouncement : AdminBasePage
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

        string ErrorLoad;
        string ErrorRedirect;
        string ErrorSave;
        string ForumRequired;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_AnnouncementEidt_ErrorLoad];
                ErrorSave = Proxy[EnumText.enumForum_AnnouncementEdit_ErrorSaveAnnouncement];
                ErrorRedirect = Proxy[EnumText.enumForum_Public_RedirectError];
                Master.Page.Title = Proxy[EnumText.enumForum_AnnouncementEdit_Title];
                ForumRequired = Proxy[EnumText.enumForum_Announcement_ForumRequired];
                this.lblTitle.Text = Proxy[EnumText.enumForum_AnnouncementEdit_Title];
                this.lblSubTitle.Text = Proxy[EnumText.enumForum_AnnouncementEdit_SubTitle];
                this.btnSave1.Text = Proxy[EnumText.enumForum_Announcement_ButtonSave];
                this.btnSave2.Text = Proxy[EnumText.enumForum_Announcement_ButtonSave];
                this.btnCancel1.Text = Proxy[EnumText.enumForum_Announcement_BuutonCancel];
                this.btnCancel2.Text = Proxy[EnumText.enumForum_Announcement_BuutonCancel];
                //this.rfvBeginDate.ErrorMessage = Proxy[EnumText.enumForum_Announcement_BeginDateRequired];
                //this.revBeginDate.ErrorMessage = Proxy[EnumText.enumForum_Announcement_BeginDateFormat];
                //this.rfvExpireDate.ErrorMessage = Proxy[EnumText.enumForum_Announcement_ExpireDateRequired];
                //this.revExpireDate.ErrorMessage = Proxy[EnumText.enumForum_Announcement_ExpireDateFormat];
                //this.CompareValidatorBeginAndExpireTime.ErrorMessage = Proxy[EnumText.enumForum_Announcement_BeginDateShouldEarlierThanExpireDate];
                this.cvForum.ErrorMessage = ForumRequired;
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumAnnouncements);
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
                this.lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        #region Control Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //DateTime dtStart = DateTime.ParseExact(this.txtStartTime.Text.Trim(), "MM-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //DateTime dtEnd = DateTime.ParseExact(this.txtExpireTime.Text.Trim(), "MM-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (this.listBoxForum.SelectedIndex < 0)
                    throw new Exception(Proxy[EnumText.enumForum_AnnouncementCreate_ErrorForumRequired]);//test
                AnnoucementProcess.UpdateAnnoucement(
                    CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.IfOperator,
                    CurrentUserOrOperator.SiteId, AnnoucementId,
                    this.tbSubject.Text, CurrentUserOrOperator.UserOrOperatorId,
                     htmlEditor.Text, GetSelectedForumIds());
                Response.Redirect("AnnouncementList.aspx?siteId=" + SiteId, false);
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorSave + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }

        }
        protected void btnCancel1_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("AnnouncementList.aspx?siteid=" + SiteId.ToString(), false);
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = ErrorRedirect + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }
        #endregion

        #region Custom Method

        protected void InitAnnoucement(AnnouncementWithPermissionCheck Annoucement)
        {
            //Init Base
            this.tbSubject.Text = Server.HtmlDecode(Annoucement.Subject);
            //this.txtStartTime.Text = Annoucement.BeginDate.ToString("MM-dd-yyyy");
            //this.txtExpireTime.Text = Annoucement.ExpireDate.ToString("MM-dd-yyyy");
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
            foreach (ListItem item in this.listBoxForum.Items)
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
                this.listBoxForum.Items.Add(new ListItem(forumPaths[i], forumIds[i].ToString()));
            }
        }

        protected void SetForumOfAnnoucementChoose(int[] forumIdsOfAnnoucement)
        {
            for (int i = 0; i < forumIdsOfAnnoucement.Length; i++)
            {
                foreach (ListItem item in this.listBoxForum.Items)
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
