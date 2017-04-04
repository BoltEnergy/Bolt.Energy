
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
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.Language;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;

namespace Com.Comm100.Forum.UI.AdminPanel
{
    public partial class AdminMasterPage : System.Web.UI.MasterPage
    {
        public int SiteId
        {
            get { return ((Com.Comm100.Forum.UI.UIBasePage)this.Page).SiteId; }
        }

        public LanguageProxy Proxy
        {
            get { return ((Com.Comm100.Forum.UI.UIBasePage)this.Page).Proxy; }
        }

        private string _jsFilePath = "../../";
        public string JsFilePath
        {
            get { return this._jsFilePath; }
            set { this._jsFilePath = value ;}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                #region Nav Bar
                SessionUser CurrentUserOrOperator = null;
                if (Session["CurrentUser"] != null)
                    CurrentUserOrOperator = (SessionUser)Session["CurrentUser"];
                else
                    Response.Redirect("~/login.aspx", false);

#if OPENSOURCE
#else

                this.AdminNavBar1.ApplicationTypes = SiteProcess.GetSiteAppTypesById(CurrentUserOrOperator.SiteId);
                this.AdminHeader1.ApplicationType = this.AdminNavBar1.ApplicationType = EnumApplicationType.enumForum;
#endif                 
                #endregion

                

                if (!IsPostBack)
                {
                    #region hyperlink
                    this.hlDashboard.NavigateUrl = "~/AdminPanel/Dashboard.aspx?siteid=" + SiteId;
                    this.hlCodePlan.NavigateUrl = "~/AdminPanel/Settings/CodePlan.aspx?siteid=" + SiteId;
                    this.hlForumHome.NavigateUrl = "~/Default.aspx?siteid=" + SiteId;
                    this.hlForumFeature.NavigateUrl = "~/AdminPanel/Settings/ForumFeature.aspx?siteid=" + SiteId;
                    this.hlUserGroupsManagement.NavigateUrl = "~/AdminPanel/UserGroups/UserGroup.aspx?siteid=" + SiteId;
                    this.hlAdministrators.NavigateUrl = "~/AdminPanel/UserGroups/Administrators.aspx?siteid=" + SiteId;
                    this.hlAdministratorsSingle.NavigateUrl = "~/AdminPanel/UserGroups/Administrators.aspx?siteid=" + SiteId;
                    this.hlReputationGroups.NavigateUrl = "~/AdminPanel/ReputationGroup/ReputationGroups.aspx?siteid=" + SiteId ;
                    this.hlAnnouncements.NavigateUrl = "~/AdminPanel/Announcement/AnnouncementList.aspx?siteid=" + SiteId;
                    this.hlWaitingForModeration.NavigateUrl = "~/AdminPanel/PostModeration/WaitingForModeration.aspx?siteid=" + SiteId;
                    this.hlRejected.NavigateUrl = "~/AdminPanel/PostModeration/Rejected.aspx?siteid=" + SiteId;
                    this.hlTopics.NavigateUrl = "~/AdminPanel/TopicAndPost/Topics.aspx?siteid=" + SiteId;
                    this.hlPosts.NavigateUrl = "~/AdminPanel/TopicAndPost/Posts.aspx?siteid=" + SiteId;
                    this.hlRecycleBin.NavigateUrl = "~/AdminPanel/TopicAndPost/RcycleBin.aspx?siteid=" + SiteId;
                    this.hlAbuse.NavigateUrl = "~/AdminPanel/Abuse/AbuseReport.aspx?siteid=" + SiteId;
                    this.hlBan.NavigateUrl = "~/AdminPanel/Ban/Bans.aspx?siteid=" + SiteId;
                    this.hlSettings.NavigateUrl = "~/AdminPanel/Settings/Settings.aspx?siteid=" + SiteId;

                    this.hlUserManager.NavigateUrl = "~/AdminPanel/Users/UserList.aspx?siteid=" + SiteId;
                    this.hlModeration.NavigateUrl = "~/AdminPanel/Users/RatifyUser.aspx?siteid=" + SiteId;
                    this.hlEmailVerify.NavigateUrl = "~/AdminPanel/Users/EmailVerify.aspx?siteid=" + SiteId;
                    this.hlCategories.NavigateUrl = "~/AdminPanel/Categories/CategoryList.aspx?siteid=" + SiteId;
                    this.hlForums.NavigateUrl = "~/AdminPanel/Forums/ForumList.aspx?siteid=" + SiteId;
                    this.hlDrafts.NavigateUrl = "~/AdminPanel/Drafts/DraftList.aspx?siteid=" + SiteId;
                    
#if OPENSOURCE
                    this.itemCodePlan.Visible = false;
                    this.itemDraftManage.Visible = false;
#endif
                    #endregion hyperlink

                    #region Images
                    imgDashboard.Attributes.Add("align", "absmiddle");
                    imgCodePlan.Attributes.Add("align", "absmiddle");
                    imgForumHome.Attributes.Add("align", "absmiddle");
                    imgForumFeature.Attributes.Add("align", "absmiddle");
                    imgUserGroups.Attributes.Add("align", "absmiddle");
                    imgAdministrators.Attributes.Add("align", "absmiddle");
                    imgUsers.Attributes.Add("align", "absmiddle");
                    imgReputationGroups.Attributes.Add("align", "absmiddle");
                    imgCategoriesAndForums.Attributes.Add("align", "absmiddle");
                    imgAnnouncements.Attributes.Add("align", "absmiddle");
                    imgPostsModeration.Attributes.Add("align", "absmiddle");
                    imgTopicsAndPosts.Attributes.Add("align", "absmiddle");
                    imgDraft.Attributes.Add("align", "absmiddle");
                    imgAbuse.Attributes.Add("align", "absmiddle");
                    imgBan.Attributes.Add("align", "absmiddle");
                    imgSettings.Attributes.Add("align", "absmiddle");
                    #endregion
                    
                    SetMenuVisible();
                }
            }
            catch (Exception)
            {
            }
        }

        public void SetMenuVisible()
        {
            SessionUser CurrentUserOrOperator = null;
            if (Session["CurrentUser"] != null)
                CurrentUserOrOperator = (SessionUser)Session["CurrentUser"];
            else
                Response.Redirect("~/login.aspx", false);
            #region Menu Visible Settings
            ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(SiteId, CurrentUserOrOperator.UserOrOperatorId);
            #region User Group Menus
            if (forumFeature.IfEnableGroupPermission)
            {
                itemUserGroups.Visible = true;
                itemUserGroupsManagement.Visible = true;
                itemAdministrators.Visible = true;
                itemAdministratorsSingle.Visible = false;
            }
            else
            {
                
                itemUserGroups.Visible = false;
                itemUserGroupsManagement.Visible = false;
                itemAdministrators.Visible = false;
                itemAdministratorsSingle.Visible = true;
            }
            #endregion

            #region Reputation Group Menus
            if (forumFeature.IfEnableReputation)
            {
                itemReputationGroups.Visible = true;
            }
            else
            {
                itemReputationGroups.Visible = false;
            }
            #endregion

            #endregion
        }

        public void SetMenuSyle(EnumAdminMenuType enumMasterMenu)
        {
            switch (enumMasterMenu)
            {
                #region Dashboard
                case EnumAdminMenuType.enumForumDashboard:
                    itemDashboard.Attributes["class"] = "liSel";
                    break;
                #endregion

                #region Code Plan
                case EnumAdminMenuType.enumCodePlan:
                    itemCodePlan.Attributes["class"] = "liSel";
                    break;
                #endregion

                #region Forum Feature
                case EnumAdminMenuType.enumForumFeature:
                    itemForumFeature.Attributes["class"] = "liSel";
                    break;
                #endregion

                #region User Groups
                case EnumAdminMenuType.enumUserGroups:
                    itemUserGroupsManagement.Attributes["class"] = "liSel";
                    break;
                case EnumAdminMenuType.enumAdministrators:
                    itemAdministrators.Attributes["class"] = "liSel";
                    itemAdministratorsSingle.Attributes["class"] = "liSel";
                    break;
                #endregion

                #region Users
                case EnumAdminMenuType.enumUserManage:
                    itemUserManage.Attributes["class"] = "liSel";
                    break;
                case EnumAdminMenuType.enumUserModeration:
                    itemUserModeration.Attributes["class"] = "liSel";
                    break;
                case EnumAdminMenuType.enumEmailVerify:
                    itemEmailVerify.Attributes["class"] = "liSel";
                    break;
                #endregion

                #region Reputation Groups
                case EnumAdminMenuType.enumReputationGroups:
                    itemReputationGroups.Attributes["class"] = "liSel";
                    break;
                #endregion

                #region Announcements
                case EnumAdminMenuType.enumAnnouncements:
                    itemAnnouncements.Attributes["class"] = "liSel";
                    break;
                #endregion

                #region Categories & Forums
                case EnumAdminMenuType.enumCategoriesManage:
                    itemCategories.Attributes["class"] = "liSel";
                    break;
                case EnumAdminMenuType.enumForumManage:
                    itemForums.Attributes["class"] = "liSel";
                    break;
                #endregion

                #region Posts Moderation
                case EnumAdminMenuType.enumWaitingForModerationPosts:
                    itemWaitingForModeration.Attributes["class"] = "liSel";
                    break;
                case EnumAdminMenuType.enumRejectedPosts:
                    itemRejected.Attributes["class"] = "liSel";
                    break;
                #endregion

                #region Topics & Posts
                case EnumAdminMenuType.enumTopicManagement:
                    itemTopics.Attributes["class"] = "liSel";
                    break;
                case EnumAdminMenuType.enumPostManagement:
                    itemPosts.Attributes["class"] = "liSel";
                    break;
                case EnumAdminMenuType.enumTopicAndPostRecycle:
                    itemRecycleBin.Attributes["class"] = "liSel";
                    break;
                #endregion

                #region Draft
                case EnumAdminMenuType.enumDraftManage:
                    itemDraftManage.Attributes["class"] = "liSel";
                    break;
                #endregion

                #region Abuse Report
                case EnumAdminMenuType.enumAbuse:
                    itemAbuse.Attributes["class"] = "liSel";
                    break;
                #endregion

                #region Banned List
                case EnumAdminMenuType.enumBan:
                    itemBan.Attributes["class"] = "liSel";
                    break;
                #endregion

                #region Site Settings
                case EnumAdminMenuType.enumSiteSettings:
                    itemSettings.Attributes["class"] = "liSel";
                    break;
                #endregion

            }
        }
    }
}
