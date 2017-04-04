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
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Language;
namespace Com.Comm100.Forum.UI.UserPanel
{
    public partial class MySendMessage : UserBasePage
    {
        string ErrorLoad;
        string ErrorSendMessage;
        string SuccessfullySendMessage;
        public string pageSiteId;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_SendMessage_LoadError];
                ErrorSendMessage = Proxy[EnumText.enumForum_SendMessage_SendMessageError];
                SuccessfullySendMessage = Proxy[EnumText.enumForum_SendMessage_SendMessageSuccessfully];
                this.btnSend.Text = Proxy[EnumText.enumForum_SendMessage_ButtonSend];
                this.btnSelectUsers.Text = Proxy[EnumText.enumForum_SendMessage_ButtonSelectUsers];
                this.btnSelectGroups.Text = Proxy[EnumText.enumForum_SendMessage_ButtonSelectGroups];
                this.ValidSubjectRequired.ErrorMessage = Proxy[EnumText.enumForum_SendMessage_RequiredSubject];
                this.lblSelectUserOrGroupIsNuLL.Text = Proxy[EnumText.enumForum_SendMessage_RequiredReceiver];
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + exp.Message;
            }
        }

        public int InMessageId 
        {
            get 
            {
                if (Request.QueryString["inMessageId"] != null)
                    return Convert.ToInt32(Request.QueryString["inMessageId"]); 
                else
                    return -1;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    SiteSetting tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, this.UserOrOperatorId);
                    Master.Page.Title = String.Format(Proxy[EnumText.enumForum_Public_UserPanelBrowserTitle], System.Web.HttpUtility.HtmlEncode(tmpSiteSetting.SiteName.ToString()));
                    CheckMessagePermission();
                    pageSiteId = this.SiteId.ToString();
                    if (!IsPostBack)
                    {
                        ((UserMasterPage)Master).SetMenu(EnumUserMenuType.SendMessage);            
                    }
                    PageInit();
                }
                catch (Exception exp)
                {
                    this.IfError = true;
                    this.lblError.Text = ErrorLoad + exp.Message; //have problem about language!
                    LogHelper.WriteExceptionLog(exp);
                }
            }


        }
        private void PageInit()
        {
            if (IfAdmin())
            {
                this.btnSelectGroups.Visible = true;
                this.tbGroups.Visible = true;
            }
            if (InMessageId != -1 && !IsPostBack)
            {
                InMessageWithPermissionCheck inMessage = MessageProcess.GetInMessageById(SiteId, UserOrOperatorId, InMessageId);
                this.Subject.Text = "Re:" + inMessage.Subject;
                this.tbUsers.Value = inMessage.FromUserOrOperatorDisplayName +";";
                this.hdUserIds.Value = inMessage.FromUserOrOperatorId + ";";
            }
        }
        protected void btSend_Click(object sender, EventArgs e)
        {
            try 
            {  
                int recieveId;
                string subject = Subject.Text.Trim();
                string message = Message.Text.Trim() ;
                this.tbUsers.Value = this.tbUsers.Value;
                this.tbGroups.Text = this.tbGroups.Text;

                if (IfAdmin())
                {
                    #region Get Send Users
                    List<int> userIds = new List<int>();
                    if (this.hdUserIds.Value != "")
                    {
                        foreach (string str in this.hdUserIds.Value.Trim(';').Split(';'))
                            userIds.Add(Convert.ToInt32(str));
                    }
                    List<int> reputationIds = new List<int>();
                    if (this.hdReputationGroups.Value != "")
                    {
                        foreach (string str in this.hdReputationGroups.Value.Trim(';').Split(';'))
                            reputationIds.Add(Convert.ToInt32(str));
                    }                        
                    List<int> userGroupIds = new List<int>();
                    if (this.hdUserGroups.Value != "")
                    {

                        foreach (string str in this.hdUserGroups.Value.Trim(';').Split(';'))
                            userGroupIds.Add(Convert.ToInt32(str));
                    }
                    bool ifAdminGroup = false;bool ifModeratorGroup = false;
                    if (hdIfAdminGroup.Value.ToLower() == "true")
                        ifAdminGroup = true;
                    if (hdIfModeratorGroup.Value.ToLower() == "true")
                        ifModeratorGroup = true;
                    #endregion
                   
                    if (userIds.Count == 0 && reputationIds.Count == 0 && userGroupIds.Count == 0
                        && !ifAdminGroup && !ifModeratorGroup)
                    {
                        
                        lblSelectUserOrGroupIsNuLL.Visible = true;
                        return;
                    }
                        MessageProcess.SendMessage(SiteId, UserOrOperatorId, subject, message, UserOrOperatorId, userIds,
                            reputationIds,userGroupIds,ifAdminGroup,ifModeratorGroup);
                }
                else
                {
                    //if (hdUserGroups.Value == ""||hdUserIds.Value.Contains(";"))
                    //{
                    //    ExceptionHelper.ThrowForumOnlyAdministratorsHavePermissionException();
                    //}
                    if (hdUserIds.Value == null || hdUserIds.Value == "")
                    {
                        lblSelectUserOrGroupIsNuLL.Visible = true;
                        return;
                        //ExceptionHelper.ThrowMessageReceiverIsRequiredException();
                    }

                    recieveId = Convert.ToInt32(hdUserIds.Value.Trim(';'));                  
                    MessageProcess.SendMessage(this.SiteId, this.UserOrOperatorId, recieveId, subject, message);
                }
                
                this.lblSuccess.Text = SuccessfullySendMessage;
                string url = "MyOutBox.aspx?siteId=" + SiteId;
                //string script = string.Format(@"window.location = '{0}'", url);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", script,true);
                Response.Redirect(url,false);
            }
            catch(Exception exp)
            {
                lblError.Text =ErrorSendMessage+ exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }


        }
        private void CheckMessagePermission()
        {
            ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId);
            if (!forumFeature.IfEnableMessage)
            {
                ExceptionHelper.ThrowForumSettingsCloseMessageFunction();
            }
        }
        
        public bool IfUser()
        {
            if (!this.IfAdmin())
                return true;
            else
                return false;
        }
    }
}
