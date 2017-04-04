
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
    public partial class UserMyPosts : Com.Comm100.Forum.UI.UserPanel.UserBasePage
    {
        //protected override void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);
        //    IfValidateSite = true;
        //}

        protected override void InitLanguage()
        {
            try
            {
                this.btnQuery.Text = Proxy[EnumText.enumForum_Topic_ButtonQuery];
            }
            catch (Exception exp)
            {
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
                    if (!IsPostBack)
                    {
                        ((UserMasterPage)Master).SetMenu(EnumUserMenuType.MyPosts);
                        PostTimeInit();
                        aspnetPager.PageIndex = 0;
                        this.RefreshData();
                    }
                }
                catch (Exception exp)
                {
                    IfError = true;
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageMyPostsErrorLoad] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
        }
        private void PostTimeInit()
        {
            this.ddlPostTime.Items.Add("All");
            this.ddlPostTime.Items.Add("1 day");
            this.ddlPostTime.Items.Add("7 days");
            this.ddlPostTime.Items.Add("2 weeks");
            this.ddlPostTime.Items.Add("1 month");
            this.ddlPostTime.Items.Add("3 months");
            this.ddlPostTime.Items.Add("6 months");
            this.ddlPostTime.Items.Add("1 year");
        }
        
        private DateTime GetTodayUtcTime()
        {
            //return DateTime.UtcNow.Date.AddHours(-12);
            return DateTimeHelper.LocalToUTC(DateTime.Now.AddDays(-1));
        }

        private void RefreshData()
        {
            //int recordsCount = UserControlProcess.GetCountOfPosts(this.SiteId, this.UserOrOperatorId,this.IfOperator);
            int recordsCount;
            DateTime startDate; DateTime endDate;
            GetStartDateAndEndDate(out startDate, out endDate, this.ddlPostTime.SelectedIndex);
            PostWithPermissionCheck[] myPosts = UserControlProcess.GetMyPostsByPaging(this.SiteId, this.UserOrOperatorId,
                aspnetPager.PageIndex + 1, aspnetPager.PageSize, this.tbKeywords.Value,
                startDate, endDate, out recordsCount);
            aspnetPager.CWCDataBind(RepeaterMyPosts, myPosts, recordsCount);
        }

        private void GetStartDateAndEndDate(out DateTime startDate, out DateTime endDate, int timeRange)
        {
            startDate = new DateTime(); endDate = new DateTime();
            //choose date time
            switch (timeRange)
            {
                case 0: //ALL
                    break;
                case 1: //1 day
                    endDate = GetTodayUtcTime().AddDays(1);
                    startDate = GetTodayUtcTime();
                    break;
                case 2: //7 days
                    endDate = GetTodayUtcTime().AddDays(1);
                    startDate = endDate.AddDays(-7);
                    break;
                case 3: //2 weeks
                    endDate = GetTodayUtcTime().AddDays(1);
                    startDate = endDate.AddDays(-14);
                    break;
                case 4: //1 months
                    endDate = GetTodayUtcTime().AddDays(1);
                    startDate = endDate.AddMonths(-1);
                    break;
                case 5: //3 months
                    endDate = GetTodayUtcTime().AddDays(1);
                    startDate = endDate.AddMonths(-3);
                    break;
                case 6: //6 months
                    endDate = GetTodayUtcTime().AddDays(1);
                    startDate = endDate.AddMonths(-6);
                    break;
                case 7: //1 year
                    endDate = GetTodayUtcTime().AddDays(1);
                    startDate = endDate.AddYears(-1);
                    break;
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
                    IfError = true;
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageMyPostsErrorLoad] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
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
                    IfError = true;
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageMyPostsErrorLoad] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
        }

        protected String Status(bool ifClosed, bool ifMarkAsAnswer)
        {
            String srcImage = "";
            if (ifClosed)
                srcImage = "<img src=\"../Images/status/participate_close.gif\" alt='" + Proxy[EnumText.enumForum_Topic_StatusClosedParticipated] + "' />";
            else if (ifMarkAsAnswer)
                srcImage = "<img src=\"../Images/status/participate_mark.gif\" alt='" + Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated] + "' />";
            else
                srcImage = "<img src=\"../Images/status/participate_normal.gif\" alt='" + Proxy[EnumText.enumForum_Topic_StatusNormalParticipated] + "' />";
            return srcImage;
        }

        protected void RepeaterMyPosts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            /*Post Subject Link*/
            PostWithPermissionCheck post = e.Item.DataItem as PostWithPermissionCheck;
            TopicWithPermissionCheck topic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, post.TopicId);
            ForumWithPermissionCheck forum = ForumProcess.GetForumByForumId(SiteId, UserOrOperatorId, topic.ForumId);
            HyperLink link = e.Item.FindControl("linkPost") as HyperLink;
            link.NavigateUrl = string.Format("../Topic.aspx?forumId={0}&topicId={3}&siteId={2}&postId={1}&GotoPost=true#Post{1}",
                forum.ForumId, post.PostId, SiteId,topic.TopicId);
            link.Text = StringHelper.GetMarkedLengthOfString(Server.HtmlEncode(ReplaceProhibitedWords(post.Subject)), 60);
            link.ToolTip = GetTooltipString(ReplaceProhibitedWords(post.Subject));
            /*Forum Path*/
            CategoryWithPermissionCheck category = CategoryProcess.GetCategoryById(UserOrOperatorId,IfOperator,SiteId,forum.CategoryId);
            string forumPath = Server.HtmlEncode(ReplaceProhibitedWords(category.Name)) + "/" + Server.HtmlEncode(ReplaceProhibitedWords(forum.Name));
            (e.Item.FindControl("lbFroumPath") as Label).Text = forumPath;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (!IfError)
            {
                try
                {
                    aspnetPager.PageIndex = 0;
                    RefreshData();
                    aspnetPager.PageIndex = 0;
                }
                catch (Exception exp)
                {
                    IfError = true;
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageMyPostsErrorLoad] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
        }
    }
}
