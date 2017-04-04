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
    public partial class MySubscribes :UserBasePage
    {
        string ErrorLoad;
        string ErrorDelete;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad=Proxy[EnumText.enumForum_Topic_SubscribesLoadError];
                ErrorDelete=Proxy[EnumText.enumForum_Topic_SubscribesDeleteError];
                this.btnQuery.Text = Proxy[EnumText.enumForum_Topic_ButtonQuery];
            }
            catch (Exception exp)
            {
                this.lblMessage.Visible=true;
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
                    CheckSubscribePermission();
                    if (!IsPostBack)
                    {
                        ((UserMasterPage)Master).SetMenu(EnumUserMenuType.MySubscribes);
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
            string keyword = QueryWord.Text.Trim().ToString();
            SubscribeWithPermissionCheck[] MySubscribes = SubscribeProcess.GetSubscribeByQueryAndPaging(this.SiteId, this.UserOrOperatorId,out recordCount, aspnetPager.PageIndex+1, aspnetPager.PageSize, keyword);
            if (MySubscribes.Length <= 0)
            {
                aspnetPager.PageIndex = 0;
                MySubscribes = SubscribeProcess.GetSubscribeByQueryAndPaging(this.SiteId, this.UserOrOperatorId, out recordCount, aspnetPager.PageIndex + 1, aspnetPager.PageSize, keyword);
             }
            aspnetPager.CWCDataBind(RepeaterMySubscribes, MySubscribes, recordCount);
        }
        protected void aspnetPager_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
        {
            if (!IfError)
            {
                try
                {
                    this.aspnetPager.PageIndex = 0;
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
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        
        protected void RepeaterMySubscribes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    try
                    {
                        SubscribeProcess.DeleteSubscribe(this.SiteId, this.UserOrOperatorId, Convert.ToInt32(e.CommandArgument));
                    }
                    catch (Exception exp)
                    {
                        this.lblMessage.Visible = true;
                        this.lblMessage.Text = ErrorDelete + exp.Message;
                        LogHelper.WriteExceptionLog(exp);
                    }
                }
                RefreshData();

            }
            catch (Exception exp)
            {
                lblMessage.Visible = true;
                lblMessage.Text = exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        public bool TopicIfRead(int topicId)
        {
            bool ifRead = false;
            string strReadTopicId = "";
            strReadTopicId = Framework.Common.CommonFunctions.ReadCookies("ReadTopicId");

            string[] readTopicIds = strReadTopicId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < readTopicIds.Length; i++)
            {
                if (readTopicIds[i] == Convert.ToString(topicId))
                {
                    ifRead = true;
                    break;
                }
            }

            return ifRead;
        }
        public string ImageStutas(int topicId, bool ifClosed, bool ifMarkedAsAnswer, bool ifParticpant)
        {
            string img = CommonFunctions.StatusImage(TopicIfRead(topicId),
                 ifClosed,
                 ifMarkedAsAnswer,
                 ifParticpant);
            return img.Replace("Images/Status/", "../Images/Status/");

        }
        public string ForumPath(int topicId)
        {
            return TopicProcess.GetTopicPath(
                    this.UserOrOperatorId,
                    SiteId, topicId);
        }
        private void CheckSubscribePermission()
        {
            ForumFeatureWithPermissionCheck forumfeature = SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId);
            if (!forumfeature.IfEnableSubscribe)
            {
                ExceptionHelper.ThrowForumSettingsCloseSubscribeFunction();
            }
        }

    }
}
