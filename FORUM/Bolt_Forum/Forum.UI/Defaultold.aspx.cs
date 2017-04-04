
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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Language;
namespace Com.Comm100.Forum.UI
{
    public partial class Defaultold : Com.Comm100.Forum.UI.UIBasePage
    {
        #region Permission Check
        protected GuestUserPermissionSettingWithPermissionCheck TheGeustUserPermission
        { get; set; }

        private bool IfAllowViewForum(int forumId)
        {
            if (this.IfGuest)
            {
                if (!TheGeustUserPermission.IfAllowGuestUserViewForum)
                {
                    return false;
                }
                else
                    return true;
            }
            else
            {
                return true;
                //UserForumPermissionItem item = this.UserForumPermissionList(forumId);
                //if (this.IfAdmin() || item.IfModerator)
                //    return true;
                //else
                //    return item.IfAllowViewForum;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IfError)
            {
                try
                {
                    Server.Transfer("~/topics.aspx");

                    SiteSettingWithPermissionCheck tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                    Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Topic_PageTitleDefault], System.Web.HttpUtility.HtmlEncode(tmpSiteSetting.SiteName));

                    this.TheGeustUserPermission = SettingsProcess.GetGuestUserPermission(
                            SiteId, UserOrOperatorId);
                    if (!IsPostBack)
                    {
                        this.RefreshData();
                    }
                }
                catch (System.Exception exp)
                {
                    LogHelper.WriteExceptionLog(exp);
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageDefaultErrorLoad] + exp.Message;
                    IfError = true;
                }
            }
        }


        protected void RefreshData()
        {
            CategoryWithPermissionCheck[] tmpCategoryArray =CategoryProcess.GetAllCategories(UserOrOperatorId, IfOperator, SiteId);

            this.repeaterCategory.DataSource = tmpCategoryArray;
            this.repeaterCategory.DataBind();
        }

        protected void repeaterCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (!IfError)
            {
                try
                {
                    switch (e.Item.ItemType)
                    {
                        case ListItemType.AlternatingItem:
                        case ListItemType.Item:
                            {
                                //Label lblCategoryId = (Label)e.Item.FindControl("lblCategoryId");

                                //CategoryWithPermissionCheck category = e.Item.DataItem as CategoryWithPermissionCheck;
                                //Com.Comm100.Forum.Bussiness.ForumWithPermissionCheck[] tmpForumArray = ForumProcess.GetNotHiddenForumsByCategoryID(
                                //    UserOrOperatorId, SiteId, Convert.ToInt32(lblCategoryId.Text));
                                ///*Gavin 2.0 default sort by orderId*/
                                ////Com.Comm100.Forum.Bussiness.ForumWithPermissionCheck[] tmpForumArray = ForumProcess.GetForumsByCategoryID(
                                ////      UserOrOperatorId, SiteId, Convert.ToInt32(lblCategoryId.Text));
                                //bool ifModeratorOfCategory;
                                //ForumWithPermissionCheck[] forums = ForumsCanVisitWhenCategoryClosed(category,
                                //    HideWithNOPermissionForum(tmpForumArray),out ifModeratorOfCategory);  

                                //if (ifModeratorOfCategory)
                                //{
                                //    Repeater repeaterForum = (Repeater)e.Item.FindControl("repeaterForum");
                                //    repeaterForum.DataSource = forums;
                                //    repeaterForum.DataBind();
                                //}
                                //else
                                //{
                                //    e.Item.Visible = false;
                                //}
                                //break;



                                /*Jason 2.0 remove category closed and show all forums*/
                                Label lblCategoryId = (Label)e.Item.FindControl("lblCategoryId");

                                CategoryWithPermissionCheck category = e.Item.DataItem as CategoryWithPermissionCheck;
                                Com.Comm100.Forum.Bussiness.ForumWithPermissionCheck[] tmpForumArray = ForumProcess.GetForumsByCategoryID(
                                    UserOrOperatorId, SiteId, Convert.ToInt32(lblCategoryId.Text));
                                Repeater repeaterForum = (Repeater)e.Item.FindControl("repeaterForum");
                                repeaterForum.DataSource = tmpForumArray;
                                repeaterForum.DataBind();
                                break;
                            }
                    }
                }
                catch (System.Exception exp)
                {
                    LogHelper.WriteExceptionLog(exp);
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageDefaultErrorLoad] + exp.Message;
                    IfError = true;
                }
            }
        }

        protected void repeaterForum_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (!IfError)
            {
                try
                {
                    switch (e.Item.ItemType)
                    {
                        case ListItemType.Item:
                        case ListItemType.AlternatingItem:
                            {
                                Com.Comm100.Forum.Bussiness.ForumWithPermissionCheck forum = (ForumWithPermissionCheck)e.Item.DataItem;
                                Label lblForumName = (Label)e.Item.FindControl("lblForumName");
                                /* Gavin 2.0 lock or hide state*/
                                /*Jason show tooltip of forum name*/
                                //lblForumName.Text = string.Format("<span title='<%#GetTooltipString(Eval(\"{0}\").ToString().Replace(\"'\",\" \"))%>'>",forum.Name);

                                if (IfForumIsLockOrClosed(forum) && !IfModerator(forum.ForumId) && !IfAdmin())
                                {
                                    lblForumName.Text = Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(forum.Name), 60);
                                    lblForumName.Text += "<img src=\""+ImagePath+"/Status/lock.gif\" alt='" + Proxy[EnumText.enumForum_Topic_FieldLockedForum] + "' title='" + Proxy[EnumText.enumForum_Topic_FieldLockedForum] + "' />";
                                }
                                else if (IfForumIsLockOrClosed(forum))
                                {
                                    lblForumName.Text = "<a class='forum_link' href='" + Com.Comm100.Forum.UI.Common.WebUtility.GetForumUrlRewritePath(Com.Comm100.Framework.Common.CommonFunctions.URLReplace(forum.Name), forum.ForumId, SiteId) + "'>" + Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(forum.Name)), 60) + "</a>";
                                    lblForumName.Text += "<img src=\""+ImagePath+ "/Status/lock.gif\" alt='" + Proxy[EnumText.enumForum_Topic_FieldLockedForum] + "' title='" + Proxy[EnumText.enumForum_Topic_FieldLockedForum] + "'/>";
                                }
                                else
                                {
                                    //----------------done by surinder for redirecting to forum page after clicked on default foru in default page------------// 
                                    //lblForumName.Text = "<a class='forum_link' href='" + Com.Comm100.Forum.UI.Common.WebUtility.GetForumUrlRewritePath(Com.Comm100.Framework.Common.CommonFunctions.URLReplace(forum.Name), forum.ForumId, SiteId) + "'>" + Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(forum.Name)), 60) + "</a>";
                                    //lblForumName.Text = "<a class='forum_link' href='topics.aspx'></a>";
                                    lblForumName.Text = "<a class='forum_link' href='topics.aspx'>" + Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(forum.Name)), 60) + "</a>";
                                }
                                //lblForumName.Text += "</span>"; 
                                
                                Label lblLastPostSubject = (Label)e.Item.FindControl("lblLastPostSubject");

                                lblLastPostSubject.ToolTip = replaceStringForTootip(forum.LastPostSubject);
                                //HyperLink lnkLastPost = e.Item.FindControl("lnkLastPost") as HyperLink;

                                if (forum.NumberOfPosts == 0)
                                {
                                    lblLastPostSubject.Text = "";
                                }
                                /* Gavin 2.0 lock or hide state*/
                                else if (IfForumIsLockOrClosed(forum) && !IfModerator(forum.ForumId) && !IfAdmin())
                                {
                                    lblLastPostSubject.Text = Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(forum.LastPostSubject.ToString()), 40) + "<br/>";
                                }
                                else
                                {
                                    string topicUrl = Com.Comm100.Forum.UI.Common.WebUtility.GetTopicUrlRewritePath(forum.LastPostSubject.Trim(), forum.LastPostTopicId, SiteId, forum.ForumId);
                                    lblLastPostSubject.Text = "<a class='topic_link' title=" + replaceStringForTootip(forum.LastPostSubject) + " href='" + topicUrl + "&postId=-1&goToPost=true&a=1#bottom'>" 
                                    //lblLastPostSubject.Text = "<a class='topic_link' title='" + System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(forum.LastPostSubject.Replace("'", "''"))) + "' href='" + topicUrl + "&postId=-1&goToPost=true&a=1#bottom'>" 
                                        //+ Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(forum.LastPostSubject.ToString())), 40) 
                                        + "<img src='"+ImagePath+ "/lastpost.gif' />"
                                        + "</a>";
                                }

                                Label lblTr = (Label)e.Item.FindControl("lblTr");
                                lblTr.Text = " <tr class=\"tr" + ForumTrSty(e.Item.ItemIndex) + "\" onmousemove=\"changeCSS(this,'trOnMouseOverStyle');\" onmouseout=\"changeCSS(this,'tr" + ForumTrSty(e.Item.ItemIndex) + "');\">";
                                Label lblForumId = (Label)e.Item.FindControl("lblForumId");
                                Moderator[] tmpModeratorArray = ForumProcess.GetModeratorsByForumId(Convert.ToInt32(lblForumId.Text), SiteId, UserOrOperatorId, IfOperator);
                                ViewState["ModeratorsLength"] = tmpModeratorArray.Length;
                                Repeater repeaterModerators = (Repeater)e.Item.FindControl("repeaterModerators");
                                repeaterModerators.DataSource = tmpModeratorArray;
                                repeaterModerators.DataBind();
                                break;
                            }
                    }
                }
                catch (System.Exception exp)
                {
                    LogHelper.WriteExceptionLog(exp);
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageDefaultErrorLoad] + exp.Message;
                    IfError = true;
                }
            }
        }


        protected string replaceStringForTootip(string inputString)
        {
            if(inputString.Contains("'"))
                inputString = "\"" + System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(inputString)) + "\"";
            else
                inputString = "'" + System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(inputString)) + "'";
            return inputString;
        }

        protected void repeaterModerators_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (!IfError)
            {
                try
                {
                    if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
                    {
                        Moderator moderator = (Moderator)e.Item.DataItem;
                        Label lblDisplayNames = (Label)e.Item.FindControl("lblDisplayName");
                        if (e.Item.ItemIndex < Convert.ToInt32(ViewState["ModeratorsLength"]) - 1)
                        {
                            lblDisplayNames.Text = Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(moderator.DisplayName)), 30) + ", ";
                            //lblSep.Text = ", ";
                        }
                        else
                        {
                            lblDisplayNames.Text = Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(moderator.DisplayName)), 30);
                        }
                    }
                }
                catch (System.Exception exp)
                {
                    LogHelper.WriteExceptionLog(exp);
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageDefaultErrorLoad] + exp.Message;
                    IfError = true;
                }
            }
        }

        private string ForumTrSty(int index)
        {
            return (index % 2 == 0 ? "Even" : "Odd");
        }

        /* Gavin 2.0 Method*/
        private bool IfForumIsLockOrClosed(ForumWithPermissionCheck forum)
        {
            if (forum.Status == EnumForumStatus.Lock)
                return true;
            else
                return false;
        }
        private ForumWithPermissionCheck[] HideWithNOPermissionForum(ForumWithPermissionCheck[] forums)
        {
            List<ForumWithPermissionCheck> forumsToShow = new List<ForumWithPermissionCheck>();
            foreach (var forum in forums)
            {
                if (IfAllowViewForum(forum.ForumId))
                {
                    forumsToShow.Add(forum);
                }
            }
            return forumsToShow.ToArray();
        }

        private ForumWithPermissionCheck[] ForumsCanVisitWhenCategoryClosed(CategoryWithPermissionCheck catetory,
            ForumWithPermissionCheck[] forums,out bool ifModeratorOfCategory)
        {
            ifModeratorOfCategory = true;
            if (catetory.Status != EnumCategoryStatus.Close)
                return forums;
            if (IfAdmin())
                return forums;
            ifModeratorOfCategory = false;
            List<ForumWithPermissionCheck> forumsToShow = new List<ForumWithPermissionCheck>();
            foreach (var forum in forums)
            {
                if (IfModerator(forum.ForumId))
                {
                    ifModeratorOfCategory = true;
                    forumsToShow.Add(forum);
                }
            }
            return forumsToShow.ToArray();
        }
    }
}
