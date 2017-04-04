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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.WebControls;
using Com.Comm100.Forum.Language;
using Com.Comm100.Forum.UI.Common;
using System.Web.UI.HtmlControls;

namespace Com.Comm100.Forum.UI
{
    public partial class mytopics : SearchBasePage
    {
        public string[] Colours = { "blue", "red", "pink", "orange", "green", "bronze" };
        #region Permission Check
        private bool IfHasAllowViewTopicOrPostPermission
        {
            get;
            set;
        }
        private bool CheckIfAllowViewForumPermission(int forumId)
        {
            if (this.IfGuest)
                return true;
            UserForumPermissionItem item = this.UserForumPermissionList(forumId);
            if (this.IfAdmin() || this.IfModerator())
                return true;
            else
                return item.IfAllowViewForum;
        }
        private bool CheckIfAllowSearchPermission(
            GuestUserPermissionSettingWithPermissionCheck guestUser)
        {
            if (this.IfGuest)
                return guestUser.IfAllowGuestUserSearch;
            else if (this.IfAdmin() || this.IfModerator())
                return true;
            else
                return this.UserPermissionCache.IfAllowSearch;
        }
        private bool CheckSearchInMinIntervalTimeforSearching(
            GuestUserPermissionSettingWithPermissionCheck guestUser)
        {
            if (this.IfGuest)
            {
                if (TimeSpan.FromSeconds(guestUser.GuestUserSearchInterval) < (DateTime.Now - this.LastSearchtime()))
                    return true;
                else
                    return false;
            }
            else if (this.IfModerator() || this.IfAdmin())
                return true;
            else
            {
                if (TimeSpan.FromSeconds(this.UserPermissionCache.MinIntervalForSearch) < (DateTime.Now - this.LastSearchtime()))
                    return true;
                else
                    return false;
            }

        }
        private bool CheckIfCanSearchPermission()
        {
            GuestUserPermissionSettingWithPermissionCheck guestUser = SettingsProcess.GetGuestUserPermission(
                SiteId, UserOrOperatorId);
            if (!CheckIfAllowSearchPermission(guestUser) ||
                !CheckSearchInMinIntervalTimeforSearching(guestUser))
                return false;
            else
                return true;
        }

        private bool CheckIfCanNewTopicPermission()
        {
            if (this.IfAdmin() || this.IfModerator())
            {
                return true;
            }
            else if (this.IfGuest)
            {
                if (this.IfSiteOnlyVisit)
                    return false;
                else
                    return true;
            }
            else
            {
                if (this.IfSiteOnlyVisit)
                    return false;
                else
                    return this.UserForumPermissionList(Convert.ToInt32(ViewState["forumId"])).IfAllowPost;
            }
        }


        #endregion

        #region Page Property
        protected bool IfFeatured
        {
            set
            {//done by techtier
                ViewState["IfFeaturedTopics"] = false;
                //value;
            }
            get { return Convert.ToBoolean(ViewState["IfFeaturedTopics"]); }
        }
        protected int ForumId
        { get { return Convert.ToInt32(ViewState["forumId"]); } }

        protected int repPageSize
        { get { return Convert.ToInt32(WebUtility.GetAppSetting("PageSize")); } }


        #endregion
        public static int PromotedVoteValue;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override void InitLanguage()
        {
            base.InitLanguage();
            this.btnAll.Text = Proxy[EnumText.enumForum_Forum_ButtonAll];
            this.btnFeatured.Text = Proxy[EnumText.enumForum_Forum_ButtonFeatured];
            this.btnSearch.Text = Proxy[EnumText.enumForum_Forum_ButtonSearch];
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IfError)
            {
                if (SiteSession.CurrentUser != null)
                {
                    try
                    {
                        ViewState["forumId"] = WebUtility.GetAppSetting(Constants.WK_ForumId);


                        //this.IfHasAllowViewTopicOrPostPermission = CheckIfAllowViewForumPermission(ForumId);
                        SiteSettingWithPermissionCheck tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                        Com.Comm100.Forum.Bussiness.ForumWithPermissionCheck tmpForum = ForumProcess.GetForumByForumId(SiteId, UserOrOperatorId, Convert.ToInt32(ViewState["forumId"]));
                        Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Topic_PageTitleForum], System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(tmpForum.Name)), System.Web.HttpUtility.HtmlEncode(tmpSiteSetting.SiteName));
                        this.lblForumName2.Text = System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(tmpForum.Name));
                        this.aspnetPagerTop.ItemName = Proxy[EnumText.enumForum_Public_Topic];
                        this.aspnetPagerTop.ItemsName = Proxy[EnumText.enumForum_Public_Topics];
                        this.aspnetPager.ItemName = Proxy[EnumText.enumForum_Public_Topic];
                        this.aspnetPager.ItemsName = Proxy[EnumText.enumForum_Public_Topics];

                        InitPageSize();/*impementation of paging*/
                        InitIfShowNewTopicButton();
                        InitIfShowSearchBar();

                        lnkNewTopic1.HRef = "~/AddTopic.aspx?forumId=" + Convert.ToInt32(ViewState["forumId"]) + "&pageType=Forum&siteId=" + SiteId;
                        lnkNewTopic2.HRef = "~/AddTopic.aspx?forumId=" + Convert.ToInt32(ViewState["forumId"]) + "&pageType=Forum&siteId=" + SiteId;
                        //lnkNewTopic1.ToolTip = Proxy[EnumText.enumForum_Topic_HelpNewTopic];
                        //lnkNewTopic2.ToolTip = Proxy[EnumText.enumForum_Topic_HelpNewTopic];
                        //this.lnkNewTopic1.ImageUrl = this.GetButtonIMGDir() + "button_topic_new.gif";
                        //this.lnkNewTopic2.ImageUrl = this.GetButtonIMGDir() + "button_topic_new.gif";

                        Button btn = Master.FindControl("BtnSearch") as Button;
                        btn.Click += new EventHandler(btnSearch_Click);



                        if (!IsPostBack)
                        {

                            MyPostData();
                            AnnoucementRefreashData();

                            ImageButtonLastPost.Visible = true;
                            ImageButtonReplies.Visible = true;
                            ImageButtonNumberofHits.Visible = true;
                        }


                    }
                    catch (Exception exp)
                    {
                        LogHelper.WriteExceptionLog(exp);
                        lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageForumErrorLoad] + exp.Message;
                        IfError = true;
                    }
                }
                else
                    Response.Redirect(WebUtility.GetAppSetting("BaseURL"));
            }

        }

        /* create by techtier on 3/1/217 for manual sorting*/
        protected void SortData(string sortKeyword)
        {

            setPageIndex();
            Moderator[] tmpModeratorArray = ForumProcess.GetModeratorsByForumId(Convert.ToInt32(ViewState["forumId"]), SiteId, UserOrOperatorId, IfOperator);
            ViewState["ModeratorsLength"] = tmpModeratorArray.Length;
            /*Gavin 2.0*/
            int recordsCount = 0;
            TopicWithPermissionCheck[] tmpTopicArray = null;
            //SiteSettingWithPermissionCheck tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, this.UserOrOperatorId);
            //if(tmpSiteSetting.PageSize.ToString() !="")
            //    aspnetPager.PageSize = tmpSiteSetting.PageSize;    
            if (CheckIfAllowViewForumPermission(Convert.ToInt32(ViewState["forumId"])))
            {
                if (IfFeatured == false)
                {
                    //recordsCount = TopicProcess.GetCountOfTopicsByForumId(SiteId, UserOrOperatorId,
                    //    IfOperator, Convert.ToInt32(ViewState["forumId"]));
                    if (this.IfAdmin() || this.IfModerator())
                        tmpTopicArray = TopicProcess.GetTopicsByForumIdAndPagingWithoutWaitingForModerationByManualSort(SiteId, UserOrOperatorId,
                        Convert.ToInt32(ViewState["forumId"]), aspnetPager.PageIndex + 1, aspnetPager.PageSize, sortKeyword,
                        out recordsCount);
                    /*updated on 3/1/2017 by techtier admin sort function*/

                        //tmpTopicArray = TopicProcess.GetTopicsByForumIdAndPaging(SiteId, UserOrOperatorId,
                    //Convert.ToInt32(ViewState["forumId"]), aspnetPager.PageIndex + 1, aspnetPager.PageSize,
                    //out recordsCount);
                    else
                        tmpTopicArray = TopicProcess.GetTopicsByForumIdAndPagingWithoutWaitingForModerationByManualSort(SiteId, UserOrOperatorId,
                        Convert.ToInt32(ViewState["forumId"]), aspnetPager.PageIndex + 1, aspnetPager.PageSize, sortKeyword,
                        out recordsCount);
                }
                else
                {
                    //recordsCount = TopicProcess.GetCountOfTopicsByForumId(SiteId, UserOrOperatorId,
                    //    IfOperator, Convert.ToInt32(ViewState["forumId"]));
                    if (this.IfAdmin() || this.IfModerator())
                        tmpTopicArray = TopicProcess.GetFeaturedTopicsByForumIdAndPaging(SiteId, UserOrOperatorId,
                        Convert.ToInt32(ViewState["forumId"]), aspnetPager.PageIndex + 1, aspnetPager.PageSize,
                        out recordsCount);
                    else
                        tmpTopicArray = TopicProcess.GetFeaturedTopicsByForumIdAndPagingWithoutWaitingForModeration(SiteId, UserOrOperatorId,
                        Convert.ToInt32(ViewState["forumId"]), aspnetPager.PageIndex + 1, aspnetPager.PageSize,
                        out recordsCount);
                }
            }
            else
                ExceptionHelper.ThrowForumUserHaveNoPermissionToVisit();
            if (tmpTopicArray == null || tmpTopicArray.Length == 0 || (repPageSize >= recordsCount))
            {
                aspnetPagerTop.Visible = false;
                aspnetPager.Visible = false;
            }

            aspnetPager.CWCDataBind(RepeaterForum, tmpTopicArray, recordsCount);
            this.aspnetPagerTop.CWCDataBind(RepeaterForum, tmpTopicArray, recordsCount);
        }

        protected void SearchData(string searchKeyword)
        {
            setPageIndex();
            Moderator[] tmpModeratorArray = ForumProcess.GetModeratorsByForumId(Convert.ToInt32(ViewState["forumId"]), SiteId, UserOrOperatorId, IfOperator);
            ViewState["ModeratorsLength"] = tmpModeratorArray.Length;

            /*Gavin 2.0*/
            int recordsCount1 = 0;
            TopicWithPermissionCheck[] tmpTopicArray = null;

            if (CheckIfAllowViewForumPermission(Convert.ToInt32(ViewState["forumId"])))
            {
                if (IfFeatured == false)
                {
                    //recordsCount = TopicProcess.GetCountOfTopicsByForumId(SiteId, UserOrOperatorId,
                    //    IfOperator, Convert.ToInt32(ViewState["forumId"]));
                    if (this.IfAdmin() || this.IfModerator())
                        tmpTopicArray = TopicProcess.GetTopicsByForumIdAndPagingWithoutWaitingForModerationByManualSearch(SiteId, UserOrOperatorId,
                        Convert.ToInt32(ViewState["forumId"]), aspnetPager.PageIndex + 1, aspnetPager.PageSize, searchKeyword,
                        out recordsCount1);
                    /*updated on 3/1/2017 by techtier admin sort function*/

                        //tmpTopicArray = TopicProcess.GetTopicsByForumIdAndPaging(SiteId, UserOrOperatorId,
                    //Convert.ToInt32(ViewState["forumId"]), aspnetPager.PageIndex + 1, aspnetPager.PageSize,
                    //out recordsCount);
                    else
                        tmpTopicArray = TopicProcess.GetTopicsByForumIdAndPagingWithoutWaitingForModerationByManualSearch(SiteId, UserOrOperatorId,
                        Convert.ToInt32(ViewState["forumId"]), aspnetPager.PageIndex + 1, aspnetPager.PageSize, searchKeyword,
                        out recordsCount1);
                }
                else
                {
                    //recordsCount = TopicProcess.GetCountOfTopicsByForumId(SiteId, UserOrOperatorId,
                    //    IfOperator, Convert.ToInt32(ViewState["forumId"]));
                    if (this.IfAdmin() || this.IfModerator())
                        tmpTopicArray = TopicProcess.GetFeaturedTopicsByForumIdAndPaging(SiteId, UserOrOperatorId,
                        Convert.ToInt32(ViewState["forumId"]), aspnetPager.PageIndex + 1, aspnetPager.PageSize,
                        out recordsCount1);
                    else
                        tmpTopicArray = TopicProcess.GetFeaturedTopicsByForumIdAndPagingWithoutWaitingForModeration(SiteId, UserOrOperatorId,
                        Convert.ToInt32(ViewState["forumId"]), aspnetPager.PageIndex + 1, aspnetPager.PageSize,
                        out recordsCount1);
                }
            }
            else
                ExceptionHelper.ThrowForumUserHaveNoPermissionToVisit();
            if (tmpTopicArray == null || tmpTopicArray.Length == 0 || (repPageSize >= recordsCount1))
            {
                aspnetPagerTop.Visible = false;
                aspnetPager.Visible = false;
            }

            aspnetPager.CWCDataBind(RepeaterForum, tmpTopicArray, recordsCount1);
            this.aspnetPagerTop.CWCDataBind(RepeaterForum, tmpTopicArray, recordsCount1);
        }



        protected void RefreshData()
        {
            setPageIndex();
            Moderator[] tmpModeratorArray = ForumProcess.GetModeratorsByForumId(Convert.ToInt32(ViewState["forumId"]), SiteId, UserOrOperatorId, IfOperator);
            ViewState["ModeratorsLength"] = tmpModeratorArray.Length;

            /*Gavin 2.0*/
            int recordsCount = 0;
            TopicWithPermissionCheck[] tmpTopicArray = null;
            //SiteSettingWithPermissionCheck tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, this.UserOrOperatorId);
            //if(tmpSiteSetting.PageSize.ToString() !="")
            //    aspnetPager.PageSize = tmpSiteSetting.PageSize;    
            if (CheckIfAllowViewForumPermission(Convert.ToInt32(ViewState["forumId"])))
            {
                if (IfFeatured == false)
                {
                    //recordsCount = TopicProcess.GetCountOfTopicsByForumId(SiteId, UserOrOperatorId,
                    //    IfOperator, Convert.ToInt32(ViewState["forumId"]));
                    if (this.IfAdmin() || this.IfModerator())
                        tmpTopicArray = TopicProcess.GetTopicsByForumIdAndPaging(SiteId, UserOrOperatorId,
                        Convert.ToInt32(ViewState["forumId"]), aspnetPager.PageIndex + 1, aspnetPager.PageSize,
                        out recordsCount);
                    else
                        tmpTopicArray = TopicProcess.GetTopicsByForumIdAndPagingWithoutWaitingForModeration(SiteId, UserOrOperatorId,
                        Convert.ToInt32(ViewState["forumId"]), aspnetPager.PageIndex + 1, aspnetPager.PageSize,
                        out recordsCount);
                }
                else
                {
                    //recordsCount = TopicProcess.GetCountOfTopicsByForumId(SiteId, UserOrOperatorId,
                    //    IfOperator, Convert.ToInt32(ViewState["forumId"]));
                    if (this.IfAdmin() || this.IfModerator())
                        tmpTopicArray = TopicProcess.GetFeaturedTopicsByForumIdAndPaging(SiteId, UserOrOperatorId,
                        Convert.ToInt32(ViewState["forumId"]), aspnetPager.PageIndex + 1, aspnetPager.PageSize,
                        out recordsCount);
                    else
                        tmpTopicArray = TopicProcess.GetFeaturedTopicsByForumIdAndPagingWithoutWaitingForModeration(SiteId, UserOrOperatorId,
                        Convert.ToInt32(ViewState["forumId"]), aspnetPager.PageIndex + 1, aspnetPager.PageSize,
                        out recordsCount);
                }
            }
            else
                ExceptionHelper.ThrowForumUserHaveNoPermissionToVisit();
            if (tmpTopicArray == null || tmpTopicArray.Length == 0)
            {
                aspnetPagerTop.Visible = false;
                aspnetPager.Visible = false;
            }

            aspnetPager.CWCDataBind(RepeaterForum, tmpTopicArray, recordsCount);
            this.aspnetPagerTop.CWCDataBind(RepeaterForum, tmpTopicArray, recordsCount);
        }

        protected void aspnetPager_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
        {
            if (!IfError)
            {
                try
                {
                    ASPNetPager o = sender as ASPNetPager;

                    currentAspNetPage.Value = o.ID;

                    RefreshData();
                    AnnoucementRefreashData();
                }
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageForumErrorLoad] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    string script = string.Format("<script>alert(\"" + Proxy[EnumText.enumForum_Topic_PageForumErrorLoad] + "{0}\")</script>", exp.Message);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
                    IfError = true;
                }
            }

        }

        private void setPageIndex()
        {
            if (currentAspNetPage.Value.Equals("aspnetPager"))
            {
                this.aspnetPagerTop.PageIndex = aspnetPager.PageIndex;
            }
            else
            {
                aspnetPager.PageIndex = aspnetPagerTop.PageIndex;
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

        #region not in use for now

        protected void RepeaterForum_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                TopicWithPermissionCheck data = (TopicWithPermissionCheck)e.Item.DataItem;
                HtmlControl achDelete = (HtmlControl)e.Item.FindControl("achDelete");
                LinkButton lbdelete = (LinkButton)e.Item.FindControl("lbdelete");
                if (lbdelete != null)
                    lbdelete.CommandArgument = Convert.ToString(data.TopicId);

                if (achDelete != null)
                    achDelete.Attributes.Add("topicid", data.TopicId.ToString());
            }
        }

        protected void RepeaterForum_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int topicId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Delete" && topicId > 0 && SiteSession.CurrentUser != null)
            {
                ForumTopicDAL objTop = new ForumTopicDAL();
                int op = objTop.DeleteMyPost(SiteSession.CurrentUser.UserOrOperatorId, topicId);
                MyPostData();
                lblMessage.Text = "Topic deleted successfully.";
            }
        } 
        #endregion

        protected void RepeaterModerator_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Literal Literal1 = (Literal)e.Item.FindControl("Literal1");
                if (e.Item.ItemIndex < Convert.ToInt32(ViewState["ModeratorsLength"]) - 1)
                {
                    Literal1.Text = ",";
                }
            }
        }

        /******** 2.0 code ************/
        /*Gavin 2.0*/
        protected void btnAll_Click(object sender, EventArgs e)
        {
            if (!IfError)
            {
                try
                {
                    //reset pager index
                    this.aspnetPager.PageIndex = 0;
                    this.aspnetPagerTop.PageIndex = 0;
                    this.IfFeatured = false;
                    RefreshData();
                    AnnoucementRefreashData();
                }
                catch (Exception exp)
                {
                    LogHelper.WriteExceptionLog(exp);
                    lblMessage.Text = Proxy[EnumText.enumForum_Forum_ErrorShowingAllTopics] + exp.Message;
                    IfError = true;
                }
            }
        }

        protected void btnFeatured_Click(object sender, EventArgs e)
        {
            if (!IfError)
            {
                try
                {
                    //reset pager index
                    this.aspnetPager.PageIndex = 0;
                    this.aspnetPagerTop.PageIndex = 0;
                    this.IfFeatured = true;
                    RefreshData();
                    AnnoucementRefreashData();
                }
                catch (Exception exp)
                {
                    LogHelper.WriteExceptionLog(exp);
                    lblMessage.Text = Proxy[EnumText.enumForum_Forum_ErrorShowingFeaturedTopics] + exp.Message;
                    IfError = true;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!IfError)
            {
                try
                {
                    HtmlInputText search = (HtmlInputText)Master.FindControl("tbSearch");
                    if (!string.IsNullOrEmpty(search.Value))
                    {
                        SearchData(search.Value);
                        ImageButtonLastPost.Visible = false;
                        ImageButtonReplies.Visible = false;
                        ImageButtonNumberofHits.Visible = false;
                        AnnoucementRefreashData();
                    }
                    else
                    {
                        this.RefreshData();
                        AnnoucementRefreashData();
                        //lblMessage.Text = "Pease enter the search keyword";
                    }

                }
                catch (Exception exp)
                {
                    LogHelper.WriteExceptionLog(exp);
                    lblMessage.Text = Proxy[EnumText.enumForum_Forum_ErrorSearchingTopic] + exp.Message;
                    IfError = true;
                }
            }
        }

        private void AnnoucementRefreashData()
        {
            /*
            AnnouncementWithPermissionCheck[] annoucements = AnnoucementProcess.GetAllAnnoucementsOfForum(
                 UserOrOperatorId, SiteId, Convert.ToInt32(ViewState["forumId"]));
            if (annoucements == null || annoucements.Length == 0)
                trAnnouncementTitle.Style["display"] = "none";
            this.rptAnnoucement.DataSource = annoucements;
            this.rptAnnoucement.DataBind();
             * */
        }

        private void InitPageSize()
        {
            aspnetPager.Visible = true;
            aspnetPagerTop.Visible = true;
            SiteSettingWithPermissionCheck siteSettings = SettingsProcess.GetSiteSettingBySiteId(
                this.SiteId, this.UserOrOperatorId);
            int pageSize = siteSettings.PageSize;
            aspnetPager.PageSize = pageSize;
            aspnetPagerTop.PageSize = pageSize;
        }

        private void InitIfShowSearchBar()
        {
            if (!CheckIfCanSearchPermission())
                this.divSearchBar.Visible = false;
        }

        private void InitIfShowNewTopicButton()
        {
            if (!CheckIfCanNewTopicPermission())
            {
                this.lnkNewTopic1.Visible = false;
                this.lnkNewTopic2.Visible = false;
            }
            else
            {
                this.lnkNewTopic1.Visible = true;
                this.lnkNewTopic2.Visible = true;
            }
        }

        public string InitTopicStatus(bool ifTopicMoved, bool IfTopicRead, bool ifclosed, bool ifMarkedasAnswer, bool ifParticipant)
        {
            if (ifTopicMoved)
                return string.Format("<img src='" + this.ImagePath + "/status/move.gif' alt='{0}' title='{0}'/>", Proxy[EnumText.enumForum_Forum_ToolTipMoved]);
            else
                return WebUtility.StatusImage(
                    this.ImagePath, IfTopicRead, ifclosed, ifMarkedasAnswer, ifParticipant);
        }

        public string InitTopicState(TopicWithPermissionCheck topic)
        {
            if (topic.IfMoveHistory)
                return "";

            ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(
                SiteId, this.UserOrOperatorId);
            string imgs = "";
            /*Score Image*/
            if (topic.IfPayScoreRequired && SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId).IfEnableScore)
                imgs += string.Format("&nbsp;&nbsp;<span style='color: #009999'>[" + Proxy[EnumText.enumForum_Forum_FiledNeedScore] + "]</span>", topic.Score);
            /*Feature Image*/
            if (topic.IfFeatured)
                imgs += string.Format("&nbsp;&nbsp;<img src='{1}/featured.gif' alt='{0}' title='{0}'/>", Proxy[EnumText.enumForum_Forum_ToolTipFeatured], this.ImagePath);
            /*Hot Image*/
            HotTopicStrategySettingWithPermissionCheck hotTopic = SettingsProcess.GetHotTopicStrategy(
                SiteId, this.UserOrOperatorId);

            if (((hotTopic.LogicalBetweenViewsAndrPosts == Com.Comm100.Framework.Enum.EnumLogical.AND/*And*/
                && (topic.NumberOfReplies >= hotTopic.ParameterGreaterThanOrEqualPosts
                && topic.NumberOfHits >= hotTopic.ParameterGreaterThanOrEqualViews)) ||
                (hotTopic.LogicalBetweenViewsAndrPosts == Com.Comm100.Framework.Enum.EnumLogical.OR /*OR*/
                && (topic.NumberOfReplies >= hotTopic.ParameterGreaterThanOrEqualPosts
                || topic.NumberOfHits >= hotTopic.ParameterGreaterThanOrEqualViews)))
                && forumFeature.IfEnableHotTopic)/*Enable Hot Topic*/
            {
                imgs += string.Format("&nbsp;&nbsp;<img src='{1}/hot.gif' alt='{0}' title='{0}'/>", Proxy[EnumText.enumForum_Forum_ToolTipHot], this.ImagePath);
            }
            /*Attachment Image*/
            bool ifHasAttach = AttachmentProcess.IfHasAttachmentByTopicId(this.UserOrOperatorId, SiteId, topic.TopicId);
            if (ifHasAttach)
                imgs += string.Format("&nbsp;&nbsp;<img src='{1}/attachment.gif' alt='{0}' title='{0}'/>", Proxy[EnumText.enumForum_Forum_ToolTipAttachments], this.ImagePath);
            /*Poll Image*/
            if (topic.IfContainsPoll)
                imgs += string.Format("&nbsp;&nbsp;<img src='{1}/vote.gif' alt='{0}' title='{0}'/>", Proxy[EnumText.enumForum_Forum_ToolTipVote], this.ImagePath);

            PromotedVoteValue = TopicPromoted.GetPromotedVote(-1, topic.TopicId);
            return imgs;
        }

        protected void ddlForumJump_TextChanged(object sender, EventArgs e)
        {
            if (!IfError)
            {
                try
                {
                    //reset pager index
                    this.aspnetPager.PageIndex = 0;
                    this.aspnetPagerTop.PageIndex = 0;
                    this.IfFeatured = false;
                    if (ddlForumJump.SelectedValue == "NumberOfHits")
                    {
                        SortData(ddlForumJump.SelectedValue.ToString());
                        AnnoucementRefreashData();
                    }
                    else if (ddlForumJump.SelectedValue == "LastPostTime")
                    {
                        SortData(ddlForumJump.SelectedValue.ToString());
                        AnnoucementRefreashData();
                    }
                    else
                    {
                        SortData("LastPostTime");
                        AnnoucementRefreashData();
                    }

                }
                catch (Exception exp)
                {
                    LogHelper.WriteExceptionLog(exp);
                    lblMessage.Text = Proxy[EnumText.enumForum_Forum_ErrorShowingAllTopics] + exp.Message;
                    IfError = true;
                }
            }
        }

        protected void MyPostData()
        {
            if (UserOrOperatorId > 0 && SiteSession.CurrentUser != null)
            {
                setPageIndex();
                Moderator[] tmpModeratorArray = ForumProcess.GetModeratorsByForumId(Convert.ToInt32(ViewState["forumId"]), SiteId, UserOrOperatorId, IfOperator);
                ViewState["ModeratorsLength"] = tmpModeratorArray.Length;
                /*Gavin 2.0*/
                int recordsCount = 0;
                TopicWithPermissionCheck[] tmpTopicArray = null;

                if (CheckIfAllowViewForumPermission(Convert.ToInt32(ViewState["forumId"])))
                {

                    tmpTopicArray = TopicProcess.GetTopicsByForumIdAndPagingWithoutWaitingForModerationByManualMyPost(SiteId, UserOrOperatorId,
                    Convert.ToInt32(ViewState["forumId"]), aspnetPager.PageIndex + 1, aspnetPager.PageSize,
                    out recordsCount);

                }
                else
                    ExceptionHelper.ThrowForumUserHaveNoPermissionToVisit();
                if (tmpTopicArray == null || tmpTopicArray.Length == 0 || (repPageSize >= recordsCount))
                {
                    aspnetPagerTop.Visible = false;
                    aspnetPager.Visible = false;
                }

                aspnetPager.CWCDataBind(RepeaterForum, tmpTopicArray, recordsCount);
                this.aspnetPagerTop.CWCDataBind(RepeaterForum, tmpTopicArray, recordsCount);
            }
            else
            {
                Response.Redirect("default.aspx");
            }
        }

        protected void ImageButtonEvent_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton clickedButton = sender as ImageButton;

            string ImageSrcUp = "~/Images/up-short-icon.png";
            string ImageSrcDown = "~/Images/down-short-icon.png";

            if (clickedButton.ID == ImageButtonLastPost.ID)
            {
                if (ImageButtonLastPost.ImageUrl == ImageSrcUp)
                {
                    SortData("LastPostTime Desc");
                    ImageButtonLastPost.ImageUrl = ImageSrcDown;

                }
                else
                {
                    SortData("LastPostTime Asc");
                    ImageButtonLastPost.ImageUrl = ImageSrcUp;
                }
            }
            if (clickedButton.ID == ImageButtonReplies.ID)
            {
                if (ImageButtonReplies.ImageUrl == ImageSrcUp)
                {
                    SortData("NumberOfReplies Desc");
                    ImageButtonReplies.ImageUrl = ImageSrcDown;

                }
                else
                {
                    SortData("NumberOfReplies Asc");
                    ImageButtonReplies.ImageUrl = ImageSrcUp;
                }
            }
            if (clickedButton.ID == ImageButtonNumberofHits.ID)
            {
                if (ImageButtonNumberofHits.ImageUrl == ImageSrcUp)
                {
                    SortData("NumberOfHits Desc");
                    ImageButtonNumberofHits.ImageUrl = ImageSrcDown;

                }
                else
                {
                    SortData("NumberOfHits Asc");
                    ImageButtonNumberofHits.ImageUrl = ImageSrcUp;
                }
            }
            AnnoucementRefreashData();
        }

    }
}
