
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
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.ASPNETState;
using System.Text;
using Com.Comm100.Forum.Language;
using Com.Comm100.Forum.UI.Common;

namespace Com.Comm100.Forum.UI
{
    public partial class SearchResult : SearchBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Topic_PageTitleSearchResult], System.Web.HttpUtility.HtmlEncode(siteSetting.SiteName));

                if (!IsPostBack)
                {
                    aspnetPager.PageIndex = 0;
                    this.RefreshData();
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageSearchResultErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void RefreshData()
        {
            //if (Request.QueryString["userId"] != null) //search one user's posts
            //{
            //    UserPostsRefreashData(Convert.ToInt32(Request.QueryString["userId"]));
            //}
            //else
            //{
            #region getQueryStrings
            string query = MyDecode(Request.QueryString["query"].ToString()).ToLower();

            string keywords = GetQueryString(query, "key");

            string location = GetQueryString(query, "id");

            int timeRange = Convert.ToInt32(GetQueryString(query, "time"));

            int searchIn = Convert.ToInt32(GetQueryString(query, "in"));

            short ifPost = Convert.ToInt16(GetQueryString(query, "ifpost"));

            #endregion

            if (ifPost != 0)
                TopicsRefreashData(location, timeRange, keywords, searchIn);
            else
                PostsRefreashData(location, timeRange, keywords, searchIn);

            Session["LastSearchTime"] = DateTime.UtcNow;
            //}
        }
        protected void aspnetPager_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
        {
            try
            {
                RefreshData();
            }
            catch (Exception exp)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", string.Format("<script>alert(\"{0}\");</script>", exp.Message));
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void aspnetPager_ChangePageSize(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.ChangePageSizeEventArgs ce)
        {
            try
            {
                RefreshData();
            }
            catch (Exception exp)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", string.Format("<script>alert(\"{0}\");</script>", exp.Message));
                LogHelper.WriteExceptionLog(exp);
            }
        }

        private string GetDisplayName(string displayName)
        {
            string returnDisplayName;
            if (displayName != null && !"".Equals(displayName.Trim()))
            {
                returnDisplayName = CommonFunctions.SqlReplace(displayName);
            }
            else
            {
                returnDisplayName = "%%";
            }
            return returnDisplayName;
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

        protected void btnReturnBack_Click(object sender, EventArgs e)
        {
            try
            {
                string query = MyDecode(Request.QueryString["query"].ToString()).ToLower();
                string forumId = "";
                if (query.ToLower().Contains("forumid="))
                {
                    forumId = GetQueryString(query, "forumid");
                }
                if (string.IsNullOrEmpty(forumId))
                {
                    string url = Request.Url.AbsoluteUri;
                    int length = url.Length;
                    int index = url.IndexOf("?");
                    string paramater = url.Substring(index);
                    Response.Redirect("~/AdvancedSearch.aspx" + paramater);
                }
                else
                {
                    Response.Redirect("~/default.aspx?forumId=" + forumId + "&siteId=" + SiteId, false);
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageSearchResultErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }

        }

        /*2.0*/
        private void GetCategroyIdOrForumIdOrAllForums(out bool ifAllForums, out bool ifCategory, out bool ifForum,
            out int id, string location)
        {
            id = -1; ifAllForums = false; ifCategory = false; ifForum = false;
            if (location.Contains("c_"))
            { id = Convert.ToInt32(location.Replace("c_", "").ToString()); ifCategory = true; }
            else if (location.Contains("f_"))
            { id = Convert.ToInt32(location.Replace("f_", "").ToString()); ifForum = true; }
            else if (location.Contains("a_"))
            { id = -1; ifAllForums = true; }
            else { }

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

        private DateTime GetTodayUtcTime()
        {
            //return DateTime.UtcNow.Date;
            return DateTimeHelper.LocalToUTC(DateTime.Now.AddDays(-1));
        }

        private void PostsRefreashData(string location, int timeRange, string keywords, int searchIn)
        {
            int searchResultCount;
            DateTime endTime; DateTime startTime;
            bool ifAllForums; bool ifForum; bool ifCategory; int id;
            GetCategroyIdOrForumIdOrAllForums(out ifAllForums, out ifCategory, out ifForum, out id, location);
            GetStartDateAndEndDate(out startTime, out endTime, timeRange);

            PostWithPermissionCheck[] posts = PostProcess.GetPostsByAdvancedSearchAndPaging(
                out searchResultCount, SiteId,
                this.UserOrOperatorId, aspnetPager.PageIndex + 1, aspnetPager.PageSize,
                keywords, startTime, endTime, ifAllForums,
                ifCategory, ifForum, id, searchIn);
            aspnetPager.CWCDataBind(rtpData, posts, searchResultCount);
            this.lblResultCount.Text = searchResultCount.ToString();
        }
        private void TopicsRefreashData(string location, int timeRange, string keywords, int searchIn)
        {
            int searchResultCount;
            DateTime endTime; DateTime startTime;
            bool ifAllForums; bool ifForum; bool ifCategory; int id;
            GetCategroyIdOrForumIdOrAllForums(out ifAllForums, out ifCategory, out ifForum, out id, location);
            GetStartDateAndEndDate(out startTime, out endTime, timeRange);

            TopicWithPermissionCheck[] topics = TopicProcess.GetTopicsByAdvancedSearchAndPaging(
                out searchResultCount, SiteId,
                this.UserOrOperatorId, aspnetPager.PageIndex + 1, aspnetPager.PageSize,
                keywords, startTime, endTime, ifAllForums,
                ifCategory, ifForum, id, searchIn);
            aspnetPager.CWCDataBind(RepeaterSearchRustlt, topics, searchResultCount);
            this.lblResultCount.Text = searchResultCount.ToString();


        }

        protected void rtpData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
                {
                    PostWithPermissionCheck post = e.Item.DataItem as PostWithPermissionCheck;
                    TopicWithPermissionCheck topic = TopicProcess.GetTopicByTopicId(
                        this.SiteId, this.UserOrOperatorId, post.TopicId);

                    PlaceHolder phcode = e.Item.FindControl("phaddCode") as PlaceHolder;
                    string code = string.Format("<b>{3}</b>&nbsp;<a class='forum_link' target=\"_blank\" href='default.aspx?siteId={0}&forumId={1}' title='{4}'>{2}</a>",
                                                SiteId, topic.ForumId, System.Web.HttpUtility.HtmlEncode(StringHelper.GetMarkedLengthOfString(ReplaceProhibitedWords(topic.ForumName), 30)), Proxy[EnumText.enumForum_SearchResult_FiledForum], GetTooltipString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(topic.ForumName)).ToString()))
                                                + "&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;" +
                                  string.Format("<b>{4}</b>&nbsp;<a class='topic_link' target=\"_blank\" href='Topic.aspx?siteId={0}&forumId={3}&topicId={1}' title='{5}'>{2}</a>",
                                                SiteId, topic.TopicId, System.Web.HttpUtility.HtmlEncode(StringHelper.GetMarkedLengthOfString(ReplaceProhibitedWords(topic.Subject), 30)), topic.ForumId, Proxy[EnumText.enumForum_SearchResult_FiledTopic], GetTooltipString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(topic.Subject)).ToString()));
                    phcode.Controls.Add(new LiteralControl(code));
                }
            }
            catch (Exception exp)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", string.Format("<script>alert(\"{0}\");</script>", exp.Message));
                LogHelper.WriteExceptionLog(exp);
            
            }
        }

        protected int GetForumId(int topicId)
        {
            TopicWithPermissionCheck topic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, topicId);
            return topic.ForumId;
        }

        protected string GetPostContent(string content)
        {
            if (content.Length <= 200)
                return HtmlReplaceProhibitedWords(content);
            else
                return HtmlReplaceProhibitedWords(content).Substring(0, 200) + "....";
        }

      
    }
}
