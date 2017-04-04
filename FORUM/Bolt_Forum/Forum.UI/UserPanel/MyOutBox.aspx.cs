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
    public partial class MyOutBox :UserBasePage 
    {

        string ErrorLoad;
        string ErrorDelete;
        string ErrorLoadMessage;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad=Proxy[EnumText.enumForum_OutBox_ErrorLoad];
                ErrorDelete = Proxy[EnumText.enumForum_OutBox_ErrorDelete];
                ErrorLoadMessage = Proxy[EnumText.enumForum_OutBox_ErrorLoadMessage];
            }
            catch (Exception exp)
            {
                this.lblMessage.Visible = true;
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
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
                    if (!IsPostBack)
                    {
                        ((UserMasterPage)Master).SetMenu(EnumUserMenuType.OutBox);
                        aspnetPager.PageIndex = 0;
                        this.RefreshData();
                    }

                }
                catch (Exception exp)
                {
                    this.IfError = true;
                    this.lblMessage.Text = ErrorLoad + exp.Message; //have problem about language!
                    LogHelper.WriteExceptionLog(exp);
                }
            }

        }
        protected void RefreshData()
        {
            int recordCount;
            OutMessageWithPermissionCheck[] myOutMessages = MessageProcess.GetOutMessagesByIdAndPaging(this.SiteId, this.UserOrOperatorId, out recordCount, aspnetPager.PageIndex + 1, aspnetPager.PageSize);
            if (myOutMessages.Length <= 0)
            {
                aspnetPager.PageIndex = 0;
                myOutMessages = MessageProcess.GetOutMessagesByIdAndPaging(this.SiteId, this.UserOrOperatorId, out recordCount, aspnetPager.PageIndex + 1, aspnetPager.PageSize);
            }
            aspnetPager.CWCDataBind(RepeaterOutMessages, myOutMessages, recordCount);
        }
        protected void aspnetPager_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
        {
            if (!IfError)
            {
                try
                {
                    RefreshData();
                }
                catch (Exception exp)
                {
                    IfError = true;
                    lblMessage.Text = ErrorLoad + exp.Message;//have some questions!
                    LogHelper.WriteExceptionLog(exp);
                }
            }
        }
        protected void RepeaterOutMessages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Subject")
                {
                    try
                    {
                        string str = e.CommandArgument.ToString();
                        hdMessageIds.Value += str + ";";
                    }
                    catch (Exception exp)
                    {
                        this.lblMessage.Text = ErrorLoadMessage + exp.Message;
                        this.lblMessage.Visible = true;
                        LogHelper.WriteExceptionLog(exp);
                    }
                }
                if (e.CommandName == "Delete")
                {
                    try
                    {
                        MessageProcess.DeleteOutMessage(this.SiteId, this.UserOrOperatorId, Convert.ToInt32(e.CommandArgument));
                    }
                    catch (Exception exp)
                    {
                        this.lblMessage.Text = ErrorDelete + exp.Message;
                        this.lblMessage.Visible = true;
                        LogHelper.WriteExceptionLog(exp);
                    }
                }
                RefreshData();

            }
            catch (Exception exp)
            {
                lblMessage.Text =ErrorLoad+ exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        public string ReceiveUsers(int outMessageId)
        {
            string strUsersAndGroups = "";
            UserOrOperator[] userOrOperator = MessageProcess.GetRecieversOfOutMessage(this.SiteId, this.UserOrOperatorId, outMessageId);
            if (userOrOperator.Length == 0)
            {
            }
            else
            {
                strUsersAndGroups = "<span onclick='changeReceiveUserMore(" + outMessageId + ")' style='cursor: pointer;'>User...</span><span id='moreReceiveUser" + outMessageId + "' style='display: none;'><br/>";
                for (int i = 0; i < userOrOperator.Length; i++)
                {
                    strUsersAndGroups += "&nbsp;&nbsp;<a class='user_link' href='../User_Profile.aspx?siteId=" + SiteId + "&userId=" + userOrOperator[i].Id + "' target=\"_blank\">" + Server.HtmlEncode(ReplaceProhibitedWords(userOrOperator[i].DisplayName)) + " </a><br/>";
                }
                strUsersAndGroups += "</span>";
            }
            return strUsersAndGroups;
        }
        public string ReceiveGroups(int outMessageId)
        {
            string strUsersAndGroups = "";
            UserGroupWithPermissionCheck[] userGroups = MessageProcess.GetUserGroupsOfOutMessage(this.SiteId, this.UserOrOperatorId, outMessageId);
            if (userGroups.Length == 0)
            {
            }
            else
            {
                strUsersAndGroups = "<span onclick='changeUserGroupMore(" + outMessageId + ")' style='cursor: pointer;'>UserGroup...</span><span id='moreUserGroup" + outMessageId + "' style='display: none;'><br/>";
                for (int i = 0; i < userGroups.Length; i++)
                {
                    strUsersAndGroups += "&nbsp;&nbsp;<a class='user_link' href='../AdminPanel/UserGroups/MemberList.aspx?siteId=" + SiteId + "&groupId=" + userGroups[i].UserGroupId + "' target=\"_blank\" >" + Server.HtmlEncode(ReplaceProhibitedWords(userGroups[i].Name)) + " </a><br/>";
                }
                strUsersAndGroups += "</span>";
            }
            return strUsersAndGroups;           
        }

        public string ReceiveReputationGroups(int outMessageId)
        {
            string strUsersAndGroups = "";
            UserReputationGroupWithPermissionCheck[] userReputationGroups = MessageProcess.GetUserReputationGroupsOfOutMessage(this.SiteId, this.UserOrOperatorId, outMessageId);
            if (userReputationGroups.Length == 0)
            {
            }
            else
            {
                strUsersAndGroups = "<span onclick='changeUserReputationGroupMore(" + outMessageId + ")' style='cursor: pointer;'>Reputation Group...</span><span id='moreUserReputationGroup" + outMessageId + "' style='display: none;'><br/>";
                if (userReputationGroups.Length > 0)
                    for (int i = 0; i < userReputationGroups.Length; i++)
                    {
                        strUsersAndGroups += "&nbsp;&nbsp;" + Server.HtmlEncode(ReplaceProhibitedWords(userReputationGroups[i].Name)) + " <br/>";
                    }
                strUsersAndGroups += "</span>";
            }
            return strUsersAndGroups;
        }

        private void CheckMessagePermission()
        {
            ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId);
            if (!forumFeature.IfEnableMessage)
            {
                ExceptionHelper.ThrowForumSettingsCloseMessageFunction();
            }
        }
    }
}
