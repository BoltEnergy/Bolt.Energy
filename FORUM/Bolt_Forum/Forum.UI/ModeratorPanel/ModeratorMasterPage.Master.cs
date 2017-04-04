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
using Com.Comm100.Forum.Language;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;

namespace Com.Comm100.Forum.UI.ModeratorPanel
{
    public partial class ModeratorMasterPage : System.Web.UI.MasterPage
    {
        public int SiteId
        {
            get { return ((Com.Comm100.Forum.UI.UIBasePage)this.Page).SiteId; }
        }

        public LanguageProxy Proxy
        {
            get { return ((Com.Comm100.Forum.UI.UIBasePage)this.Page).Proxy; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { 
                SessionUser CurrentUserOrOperator = null;
                if (Session["CurrentUser"] != null)
                {
                    CurrentUserOrOperator = (SessionUser)Session["CurrentUser"];
                }
                else
                    Response.Redirect("~/login.aspx", false);
                Page.Title = string.Format("Moderator Control Panel - {0} - Comm100",SettingsProcess.GetSiteSettingBySiteId(SiteId,CurrentUserOrOperator.UserOrOperatorId).SiteName);

#if OPENSOURCE
#else

                this.AdminNavBar1.ApplicationTypes = SiteProcess.GetSiteAppTypesById(CurrentUserOrOperator.SiteId);
                this.AdminHeader1.ApplicationType = this.AdminNavBar1.ApplicationType = Com.Comm100.Framework.Enum.EnumApplicationType.enumForum;
#endif


                if (!IsPostBack)
                {
                    //#region Header
                    //this.hlChangePassword.Text = "Change Password";
                    //this.hlChangePassword.NavigateUrl = "~/UserPanel/UserPasswordEdit.aspx?siteId=" + SiteId;
                    //this.hlLogout.Text = "Logout";
                    //this.hlLogout.NavigateUrl = "~/login.aspx?action=logout&siteId=" + SiteId;
                    //this.lblForum.Text = Server.HtmlEncode(SettingsProcess.GetSiteSettingBySiteId(SiteId,CurrentUserOrOperator.UserOrOperatorId).SiteName);
                    //this.lblCurrentUser.Text = "Current User:" + (UserProcess.GetNotDeletedUserOrOperatorById(SiteId,CurrentUserOrOperator.UserOrOperatorId)).DisplayName;
                    //#endregion
                    #region hyperlink
                    this.hlForumHome.NavigateUrl = "~/Default.aspx?siteid=" + SiteId;
                    //this.hlAnnouncement.NavigateUrl = "~/ModeratorPanel/Announcement/Announcements.aspx?siteId=" + SiteId;
                    this.hlWaitingModeration.NavigateUrl = "~/ModeratorPanel/PostModeration/WaitingForModeration.aspx?siteId=" + SiteId;
                    this.hlRejectedPosts.NavigateUrl = "~/ModeratorPanel/PostModeration/Rejected.aspx?siteId=" + SiteId;
                    this.hlAbuse.NavigateUrl = "~/ModeratorPanel/Abuse/AbuseReport.aspx?siteId=" + SiteId;
                    this.hlTopicsManagement.NavigateUrl = "~/ModeratorPanel/TopicAndPost/Topics.aspx?siteId=" + SiteId;
                    this.hlPostsManagement.NavigateUrl = "~/ModeratorPanel/TopicAndPost/Posts.aspx?siteId=" + SiteId;
                    this.hlRecycleBin.NavigateUrl = "~/ModeratorPanel/TopicAndPost/RecycleBin.aspx?siteId=" + SiteId;
                    this.hlForumsManagement.NavigateUrl = "~/ModeratorPanel/Forum/Forums.aspx?siteId=" + SiteId;
                    #endregion

                    //int siteId = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).SiteId;
                    //int userOrOperatorId = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).UserOrOperatorId;
                
                    //if (Request.QueryString["ifAdvancedMode"] == null)
                    //{
                    //    try
                    //    {
                    //        StyleSettingWithPermissionCheck styleSetting = StyleProcess.GetStyleSettingBySiteId(siteId, userOrOperatorId);
                    //        if (styleSetting != null && styleSetting.IfAdvancedMode)
                    //        {
                    //            Literal1.Text = styleSetting.PageFooter;
                    //        }
                    //    }
                    //    catch (Exception exp)
                    //    {

                    //        throw;
                    //    }
                    //}

                } 
            }
            catch(Exception exp)
            {
                throw;
            }
        }
        public void SetMenuSyle(EnumAdminMenuType enumMasterMenu)
        {
            switch (enumMasterMenu)
            {  
                //#region Announcements
                //case EnumAdminMenuType.enumAnnouncements:
                //    itemAnnouncements.Attributes["class"] = "liSel";
                //    break;
                //#endregion    

                #region Posts Moderation
                case EnumAdminMenuType.enumWaitingForModerationPosts:
                    itemWaitingModeration.Attributes["class"] = "liSel";
                    break;
                case EnumAdminMenuType.enumRejectedPosts:
                    itemRejectedPosts.Attributes["class"] = "liSel";
                    break;
                #endregion

                #region Abuse Report
                case EnumAdminMenuType.enumAbuse:
                    itemAbuse.Attributes["class"] = "liSel";
                    break;
                #endregion

                #region Topics & Posts
                case EnumAdminMenuType.enumTopicManagement:
                    itemTopicsManagement.Attributes["class"] = "liSel";
                    break;
                case EnumAdminMenuType.enumPostManagement:
                    itemPostsManagement.Attributes["class"] = "liSel";
                    break;
                case EnumAdminMenuType.enumTopicAndPostRecycle:
                    itemRecycleBin.Attributes["class"] = "liSel";
                    break;
                #endregion
                    
                #region Forums
                case EnumAdminMenuType.enumForumManage:
                    itemForumsManagement.Attributes["class"] = "liSel";
                    break;
                #endregion

            }
        }
    }
}
