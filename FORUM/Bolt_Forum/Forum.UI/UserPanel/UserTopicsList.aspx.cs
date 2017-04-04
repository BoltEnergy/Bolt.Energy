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
    public partial class UserTopicsEdit : Com.Comm100.Forum.UI.UserPanel.UserBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    SiteSetting SiteSetting = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, this.UserOrOperatorId);

                    Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Public_UserPanelBrowserTitle], System.Web.HttpUtility.HtmlEncode(SiteSetting.SiteName));
                    ((UserMasterPage)Master).SetMenu(EnumUserMenuType.MyTopics);
                    if (!IsPostBack)
                    {
                        aspnetPager.PageIndex = 0;
                        this.RefreshData();
                    }
                }
                catch (Exception exp)
                {
                    this.IfError = true;
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageMyTopicsErrorLoad] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
        }

        private void RefreshData()
        {
            if (!IfError)
            {
                try
                {
                    int recordsCount = 0;//UserControlProcess.GetCountOfMyTopics(this.SiteId, this.UserOrOperatorId, this.IfOperator);

                    TopicWithPermissionCheck[] getMyTopics = UserControlProcess.GetMyTopicsByPaging(this.SiteId, this.UserOrOperatorId, this.IfOperator, aspnetPager.PageIndex + 1, aspnetPager.PageSize);

                    aspnetPager.CWCDataBind(RepeaterForum, getMyTopics, recordsCount);
                }
                catch (System.Exception exp)
                {
                    this.IfError = true;

                    this.lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageMyTopicsErrorLoad] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
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
                    this.IfError = true;
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageMyTopicsErrorLoad] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    string script = string.Format("<script>alert({0})</script>", exp.Message);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
                }
            }
        }

        protected void aspnetPager_ChangePageSize(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.ChangePageSizeEventArgs ce)
        {
            if (!IfError)
            {
                try
                {
                    RefreshData();
                }
                catch (Exception exp)
                {
                    this.IfError = true;
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageMyTopicsErrorLoad] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    string script = string.Format("<script>alert({0})</script>", exp.Message);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
                }
            }
        }

        protected string StatusImage(bool ifClose, bool ifMarkedAsAnswer)
        {
            string statusImage = "";
            if ((!ifClose) && (!ifMarkedAsAnswer))
            {
                statusImage = "<img src=\"../Images/status/participate_normal.gif\" alt='" + Proxy[EnumText.enumForum_Topic_StatusNormalParticipated] + "' />";
            }
            else if ((!ifClose) && (ifMarkedAsAnswer))
            {
                statusImage = "<img src=\"../Images/status/participate_mark.gif\" alt='" + Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated] + "' />";
            }
            else if ((ifClose))
            {
                statusImage = "<img src=\"../Images/status/participate_close.gif\" alt='" + Proxy[EnumText.enumForum_Topic_StatusClosedParticipated] + "' />";
            }

            return statusImage;
        }
    }
}
