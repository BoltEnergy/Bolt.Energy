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
    public partial class MyInbox : UserBasePage
    {
        string ErrorLoad;
        string ErrorDelete;
        string ErrorLoadMessage;
        private ProhibitedWordsSettingWithPermissionCheck _prohibitedWords;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_InBox_ErrorLoad];
                ErrorDelete = Proxy[EnumText.enumForum_InBox_ErrorDelete];
                ErrorLoadMessage = Proxy[EnumText.enumForum_InBox_ErrorLoadMessage];
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + exp.Message;
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
                        ((UserMasterPage)Master).SetMenu(EnumUserMenuType.InBox);
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
            InMessageWithPermissionCheck[] myInMessages = MessageProcess.GetInMessagesByIdAndPaging(this.SiteId, this.UserOrOperatorId, out recordCount, aspnetPager.PageIndex + 1, aspnetPager.PageSize);
            if (myInMessages.Length <= 0)
            {
                aspnetPager.PageIndex = 0;
                myInMessages = MessageProcess.GetInMessagesByIdAndPaging(this.SiteId, this.UserOrOperatorId, out recordCount, aspnetPager.PageIndex + 1, aspnetPager.PageSize);
            }  
            aspnetPager.CWCDataBind(RepeaterInMessages, myInMessages, recordCount);
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

        protected void RepeaterInMessages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    try
                    {
                        MessageProcess.DeleteInMessage(this.SiteId, this.UserOrOperatorId, Convert.ToInt32(e.CommandArgument));
                    }
                    catch (Exception exp)
                    {
                        this.lblMessage.Visible = true;
                        this.lblMessage.Text = ErrorDelete + exp.Message;
                        LogHelper.WriteExceptionLog(exp);
                    }
                }
                else if (e.CommandName == "Message")
                {
                    try
                    {
                        string[] str = e.CommandArgument.ToString().Split(';');
                        string InMessageId = str[0].ToString();
                        hdMessageIds.Value += InMessageId + ";";
                        bool ifView = Convert.ToBoolean(str[1]);
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>showDetail();</script>");
                        if (!ifView)
                        {
                            MessageProcess.UpdateIfView(this.SiteId, this.UserOrOperatorId, Convert.ToInt32(InMessageId));
                        }
                    }
                    catch (Exception exp)
                    {
                        this.lblMessage.Visible = true;
                        this.lblMessage.Text = ErrorLoadMessage + exp.Message;
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
        protected string Status(bool ifView)
        {
            string strImg = "";
            if (ifView)
                strImg = "<img src=\"../"+this.ImagePath+"/status/read_normal.gif\" title='" + Proxy[EnumText.enumForum_InBox_LengendReadMessage] + "'/>";//have some problems about lanaguage
            else
                strImg = "<img src=\"../"+this.ImagePath+"/status/noread_normal.gif\" title='" + Proxy[EnumText.enumForum_InBox_LengendUnReadMessage] + "'/>";
            return strImg;        
        }
        private void CheckMessagePermission()
        {
            ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId);
            if (!forumFeature.IfEnableMessage)
            {
                ExceptionHelper.ThrowForumSettingsCloseMessageFunction();
            }
        }

        public string ReplaceProhibitedWords(string content)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(content);
            _prohibitedWords = SettingsProcess.GetProhibitedWords(SiteId, UserOrOperatorId);
            if (_prohibitedWords.IfEnabledProhibitedWords)
            {
                foreach (var str in _prohibitedWords.ProhibitedWords)
                {
                    sb.Replace(str, _prohibitedWords.CharacterToReplaceProhibitedWord);
                }
            }
            return sb.ToString();
        }
    }
}
