
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
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Language;
using Com.Comm100.Forum.UI.Common;
//using Com.Comm100.Forum.UI;
namespace Com.Comm100.Forum.UI.UserControl
{
    public partial class LinkBar : BaseUserControl
    {
        #region Check Permission
        private bool IfGuest { get { return (this.Page as UIBasePage).IfGuest; } }
        private bool IfAdmin { get { return (this.Page as UIBasePage).IfAdmin(); } }
        private UserPermissionCache UserPermissionCache { get { return (this.Page as UIBasePage).UserPermissionCache; } }
        private int UserOrOperatorId { get { return (this.Page as UIBasePage).UserOrOperatorId; } }

        private bool CheckIfAllowSearchPermission(
            GuestUserPermissionSettingWithPermissionCheck guestUser)
        {
            if (this.IfGuest)
                return guestUser.IfAllowGuestUserSearch;
            else if (this.IfAdmin)
                return true;
            else
                return this.UserPermissionCache.IfAllowSearch;
        }
       
        private bool CheckIfCanSearchPermission()
        {
            GuestUserPermissionSettingWithPermissionCheck guestUser = SettingsProcess.GetGuestUserPermission(
                SiteId, UserOrOperatorId);
            if (!CheckIfAllowSearchPermission(guestUser))
                return false;
            else
                return true;
        }
        #endregion

        public int SiteId { get { return ((UIBasePage)this.Page).SiteId; } }
        public string ImagePath
        {
            get {
                return ((UIBasePage)this.Page).ImagePath;
            }
        }
        protected ProhibitedWordsSettingWithPermissionCheck _prohibitedWords;
        public string ReplaceProhibitedWords(string content)
        {
            return _prohibitedWords.ReplaceProhibitedWords(content);
        }
        private string logout;
        private string errLoad;
        private string unReadMessage;
        protected void InitLanguage()
        {
            try
            {
                unReadMessage = Proxy[EnumText.enumForum_HeaderFooter_LinkUnReadMessage];
                lnkSearch.Text = Proxy[EnumText.enumForum_HeaderFooter_LinkSearch];
                lnkHome.Text = Proxy[EnumText.enumForum_HeaderFooter_LinkHome];
                lnkRegister.Text = Proxy[EnumText.enumForum_HeaderFooter_LinkRegister];
                lnkAdminControlPanel.Text = Proxy[EnumText.enumForum_HeaderFooter_LinkAdminControlPanel];
                lnkModeratorControlPanel.Text = Proxy[EnumText.enumForum_HeaderFooter_LinkModeratorControlPanel];
                lnkUserControlPanel.Text = Proxy[EnumText.enumForum_HeaderFooter_LinkUserControlPanel];
                logout = Proxy[EnumText.enumForum_HeaderFooter_LinkLogout];
                lnkLogin.Text = Proxy[EnumText.enumForum_HeaderFooter_LinkLogin];
                errLoad = Proxy[EnumText.enumForum_HeaderFooter_LinkBarErrorLoading];
            }
            catch (Exception exp)
            {
                lblMessage.Text = errLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                ((Com.Comm100.Forum.UI.UIBasePage)this.Page).IfError = true;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPreRender(EventArgs e)
        {
            try
            {
                InitLanguage();
                InitProhibitedWords();
                SetImageUrl();
                #region hyperlink
                this.lnkSearch.NavigateUrl = "~/AdvancedSearch.aspx?siteId=" + SiteId.ToString();/*this.lnkSearch.NavigateUrl link tab is hidden in pages*/
                //Done by techtier for redirecting the user to forum page after clicked on Home tab in master page// 
                //this.lnkHome.NavigateUrl = "~/Default.aspx?siteId=" + SiteId.ToString(); 
                this.lnkHome.NavigateUrl = "~/default.aspx&siteId=" + SiteId.ToString();
                //---------------------------------********************************-------------------------//
                this.lnkAdminControlPanel.NavigateUrl = "~/AdminPanel/Dashboard.aspx?siteId=" + SiteId.ToString();/*~/AdminPanel/Dashboard.aspx?siteId= link tab is hidden in pages*/
                this.lnkLogin.NavigateUrl = "~/Login.aspx?action=login&siteId=" + SiteId.ToString();
                this.lnkLogout.NavigateUrl = "~/Login.aspx?action=logout&siteId=" + SiteId.ToString();
                this.lnkRegister.NavigateUrl = "~/Register.aspx?siteId=" + SiteId.ToString();
                this.lnkUserControlPanel.NavigateUrl = "~/UserPanel/UserMyPosts.aspx?siteId=" + SiteId.ToString();//changed by techtier 
                this.lnkModeratorControlPanel.NavigateUrl = "~/ModeratorPanel/PostModeration/WaitingForModeration.aspx?siteId=" + SiteId.ToString();/*~/ModeratorPanel/PostModeration/WaitingForModeration.aspx?siteId= link tab is hidden in pages*/
                #endregion hyperlink

                int siteId = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).SiteId;
                int userOrOperatorId = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).UserOrOperatorId;
                bool ifOperator = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).IfOperator;
                bool ifModerator = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).IfModeratorInSite();
                SessionUser currentUserOrOperator = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).CurrentUserOrOperator;
                if (currentUserOrOperator != null)
                {
                    lnkLogin.Visible = false; imgLogin.Visible = false;
                    lnkLogout.Visible = true; imgLogout.Visible = true;

                    UserOrOperator userOrOperator = UserProcess.GetNotDeletedUserOrOperatorById(siteId, userOrOperatorId);
                    lnkLogout.Text = string.Format(logout, System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(userOrOperator.DisplayName)));
                    lnkRegister.Visible = false; imgRegister.Visible = false;
                    lnkUserControlPanel.Visible = true; imgUserControlPanel.Visible = true;
                    if (ifOperator || userOrOperator.IfForumAdmin)
                    {
                        //Updated on 2/3/2017 by techtier for hide admin access tab
                        //lnkAdminControlPanel.Visible = true;/*Old link*/
                        //imgAdminControlPanel.Visible = true;/*Old link*/
                        lnkAdminControlPanel.Visible = false;
                        imgAdminControlPanel.Visible = false;
                    }
                    else
                    {
                        lnkAdminControlPanel.Visible = false;
                        imgAdminControlPanel.Visible = false;
                    }
                    //Updated on 2/3/2017 by techtier for hide admin access tab
                    //lnkModeratorControlPanel.Visible = ifModerator;/*Old link*/
                    //imgModeratorControlPanel.Visible = ifModerator;/*Old link*/
                    lnkModeratorControlPanel.Visible = false;
                    imgModeratorControlPanel.Visible = false;


                    SetMessageLink();
                    //UIBasePage ub = new UIBasePage();
                    //ub.CheckIfUserOrOperatorBanned(0);
                   
                  
                }
                else
                {
                    lnkLogin.Visible = true; imgLogin.Visible = true;
                    lnkLogout.Visible = false; imgLogout.Visible = false;
                    //lnkRegister.Visible = true; imgRegister.Visible = true;
                    lnkUserControlPanel.Visible = false; imgUserControlPanel.Visible = false;
                    lnkAdminControlPanel.Visible = false; imgAdminControlPanel.Visible = false;
                    lnkModeratorControlPanel.Visible = false; imgModeratorControlPanel.Visible = false;
                }

                /*2.0*/
                RegistrationSettingWithPermissionCheck registrationSetting = SettingsProcess.GetRegistrationSettingBySiteId(
                    userOrOperatorId, SiteId);
                if (registrationSetting.IfAllowNewUser == false)
                {
                    this.lnkRegister.Visible = false;
                    this.imgRegister.Visible = false;
                }

                if (!CheckIfCanSearchPermission())
                {
                    this.lnkSearch.Visible = false;
                    this.imgSearch.Visible = false;
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = errLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                ((Com.Comm100.Forum.UI.UIBasePage)this.Page).IfError = true;
            }
        }

        /*2.0*/
        private void SetMessageLink()
        {
            if (SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId).IfEnableMessage == false)
            {
                this.imgMessages.Visible = false;
                this.lnkMessages.Visible = false;
            }
            else
            {
                int countOfMessage = MessageProcess.GetCountOfUnReadInMessages(SiteId, UserOrOperatorId);
                if (countOfMessage == 0)
                {
                    this.imgMessages.Visible = false;
                    this.lnkMessages.Visible = false;
                }
                else
                {
                    this.imgMessages.Visible = true;
                    this.lnkMessages.Visible = true;
                    string text = string.Format(unReadMessage, countOfMessage);
                    this.lnkMessages.Text = text;
                    this.lnkMessages.NavigateUrl = "~/UserPanel/MyInBox.aspx?siteId=" + SiteId;
                }
            }
        }

        private void InitProhibitedWords()
        {
            _prohibitedWords = SettingsProcess.GetProhibitedWords(SiteId, UserOrOperatorId);
        }

        private void SetImageUrl()
        {
            imgHome.Src = InitImageUrl() + "/nav_bar/forum home.gif";
            imgSearch.Src = InitImageUrl() + "/nav_bar/search.gif";
            imgRegister.Src = InitImageUrl() + "/nav_bar/register.gif";
            imgAdminControlPanel.Src = InitImageUrl() + "/nav_bar/admin_control_panel.gif";
            imgModeratorControlPanel.Src = InitImageUrl() + "/nav_bar/moderator_control_panel.gif";
            imgUserControlPanel.Src = InitImageUrl() + "/nav_bar/user_control_panel.gif";
            imgMessages.Src =InitImageUrl() + "/nav_bar/sendmessage.gif";
            imgLogout.Src = InitImageUrl() + "/nav_bar/logout.gif";
            imgLogin.Src = InitImageUrl() + "/nav_bar/login.gif";
        }

        private string  InitImageUrl()
        {
            return "~/" + ImagePath;
        }
    }

}