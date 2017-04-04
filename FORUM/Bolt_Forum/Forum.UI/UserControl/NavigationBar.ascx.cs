
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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Language;
using Com.Comm100.Forum.UI.Common;

namespace Com.Comm100.Forum.UI.UserControl
{

    public partial class NavigationBar : BaseUserControl
    {

        private void Page_Load(object sender, EventArgs e)
        {
            SiteMap.SiteMapResolve +=
              new SiteMapResolveEventHandler(this.ExpandForumPaths);
            InitProhibitedWords();
        }
        protected ProhibitedWordsSettingWithPermissionCheck _prohibitedWords;
        public string ReplaceProhibitedWords(string content)
        {
            return _prohibitedWords.ReplaceProhibitedWords(content);
        }
        private void InitProhibitedWords()
        {
            int siteId = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).SiteId;
            int userId = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).UserOrOperatorId;
            _prohibitedWords = SettingsProcess.GetProhibitedWords(siteId, userId);
        }
        private SiteMapNode ExpandForumPaths(Object sender, SiteMapResolveEventArgs e)
        {
            if (SiteMap.CurrentNode == null) return null;
            SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
            SiteMapNode tempNode = currentNode;
            try
            {
                string resourceKey = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.ToLower();
                resourceKey = resourceKey.Substring(2, resourceKey.Length - 7).Replace("/", "_");

                EnumText languageKey = EnumText.enumForum_NavigationBar_Default;
                switch (resourceKey)
                {
                    case "addtopic":
                        languageKey = EnumText.enumForum_NavigationBar_AddTopic;
                        break;
                    case "adminpanel_login":
                        languageKey = EnumText.enumForum_NavigationBar_AdminPanelLogin;
                        break;
                    case "advancedsearch":
                        languageKey = EnumText.enumForum_NavigationBar_AdvancedSearch;
                        break;
                    case "default":
                        languageKey = EnumText.enumForum_NavigationBar_Forum;//EnumText.enumForum_NavigationBar_Default;
                        break;
                    case "edittopicorpost":
                        languageKey = EnumText.enumForum_NavigationBar_EditTopicOrPost;
                        break;
                    case "emailverification":
                        languageKey = EnumText.enumForum_NavigationBar_EmailVerification;
                        break;
                    case "findpassword":
                        languageKey = EnumText.enumForum_NavigationBar_FindPassword;
                        break;
                    case "forum":
                        languageKey = EnumText.enumForum_NavigationBar_Forum;
                        break;
                    case "login":
                        languageKey = EnumText.enumForum_NavigationBar_Login;
                        break;
                    case "post_register":
                    case "pre_register":
                    case "register":
                        languageKey = EnumText.enumForum_NavigationBar_Register;
                        break;
                    case "resetpassword":
                        languageKey = EnumText.enumForum_NavigationBar_ResetPassword;
                        break;
                    case "searchresult":
                        languageKey = EnumText.enumForum_NavigationBar_SearchResult;
                        break;
                    case "sendresetpasswordemail":
                        languageKey = EnumText.enumForum_NavigationBar_SendResetPasswordEmail;
                        break;
                    case "siteclosed":
                        languageKey = EnumText.enumForum_NavigationBar_SiteClosed;
                        break;
                    case "user_profile":
                        languageKey = EnumText.enumForum_NavigationBar_UserProfile;
                        break;
                    case "user_posts":
                        languageKey = EnumText.enumForum_NacigationBar_UserPosts;
                        break;
                    case "userpanel_useravataredit":
                    case "userpanel_usermyposts":
                    case "userpanel_userpasswordedit":
                    case "userpanel_userprofileedit":
                    case "userpanel_usersignatureedit":
                    case "userpanel_usertopicslist":
                        languageKey = EnumText.enumForum_NavigationBar_UserPanel;
                        break;
                    case "ipbanned":
                        languageKey = EnumText.enumForum_NavigationBar_IPBanned;
                        break;
                    case "userbanned":
                        languageKey = EnumText.enumForum_NavigationBar_UserBanned;
                        break;
                    default:
                        break;
                }

                int siteId = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).SiteId;
                int userId = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).UserOrOperatorId;
                bool ifOperator = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).IfOperator;

                SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(siteId, userId);

                if (tempNode.Key.EndsWith("default.aspx"))
                {
                    tempNode.Title = siteSetting.SiteName;
                    tempNode.Url = tempNode.Url + "?siteid=" + siteId;
                    return currentNode;
                }
                #region forum
                if (tempNode.Key.EndsWith("default.aspx"))
                {
                    int forumId = Convert.ToInt32(HttpContext.Current.Request.QueryString["forumId"]);
                    string forumName = "";
                    try
                    {
                        ForumWithPermissionCheck forum = ForumProcess.GetForumByForumId(siteId, userId, forumId);
                        forumName = forum.Name;
                    }
                    catch { }

                    tempNode.Title = forumName;
                    tempNode.Url = forumName == "" ? "" : GetForumRewriteUrl(tempNode.Url + "?forumId=" + +forumId + "&siteid=" + siteId,forumName,forumId,siteId);

                }
                #endregion
                #region addTopic
                else if (tempNode.Key.EndsWith("addtopic.aspx"))
                {
                    int forumId = Convert.ToInt32(HttpContext.Current.Request.QueryString["forumId"]);
                    string forumName = "";
                    try
                    {
                        ForumWithPermissionCheck forum = ForumProcess.GetForumByForumId(siteId, userId, forumId);
                        forumName = forum.Name;
                    }
                    catch { }

                    tempNode.Title = Proxy[languageKey];

                    tempNode = tempNode.ParentNode;
                    tempNode.Title = forumName;
                    tempNode.Url = forumName == "" ? "" : tempNode.Url + "?forumId=" + forumId + "&siteid=" + siteId;
                }
                #endregion
                #region editTopicOrPost
                else if (tempNode.Key.EndsWith("edittopicorpost.aspx"))
                {
                    int postId = Convert.ToInt32(HttpContext.Current.Request.QueryString["postId"]);
                    int forumId = Convert.ToInt32(HttpContext.Current.Request.QueryString["forumId"]);
                    string postSubject = "";
                    string forumName = "";
                    try
                    {
                        PostWithPermissionCheck post = PostProcess.GetPostByPostId(siteId, userId, ifOperator, postId);
                        postSubject = ReplaceProhibitedWords(post.Subject);
                    }
                    catch { }
                    try
                    {
                        ForumWithPermissionCheck forum = ForumProcess.GetForumByForumId(siteId, userId, forumId);
                        forumName = forum.Name;
                    }
                    catch { }

                    tempNode.Title = postSubject;

                    tempNode = tempNode.ParentNode;
                    tempNode.Title = forumName;
                    tempNode.Url = forumName == "" ? "" : tempNode.Url + "?forumId=" + forumId + "&siteid=" + siteId;
                }
                #endregion
                #region topic
                else if (tempNode.Key.EndsWith("topic.aspx"))
                {
                    int topicId = Convert.ToInt32(HttpContext.Current.Request.QueryString["topicId"]);
                    int forumId = Convert.ToInt32(HttpContext.Current.Request.QueryString["forumId"]);
                    string topicSubject = "";
                    string forumName = "";
                    try
                    {
                        TopicWithPermissionCheck topic = TopicProcess.GetTopicByTopicId(siteId, userId, topicId);
                        topicSubject = ReplaceProhibitedWords(topic.Subject);
                    }
                    catch { }
                    try
                    {
                        ForumWithPermissionCheck forum = ForumProcess.GetForumByForumId(siteId, userId, forumId);
                        forumName = forum.Name;
                    }
                    catch { }

                    tempNode.Title = topicSubject;
                    tempNode.Url = topicSubject == "" ? "" : GetTopicRewriteUrl( tempNode.Url + "?topicId=" + topicId + "&siteid=" + siteId,topicSubject,topicId,forumId,siteId);

                    tempNode = tempNode.ParentNode;

                    tempNode.Title = forumName;
                    tempNode.Url = forumName == "" ? "" : GetForumRewriteUrl(tempNode.Url + "?forumId=" + forumId + "&siteid=" + siteId,forumName,forumId,siteId);
  
                }
                #endregion
                else
                {
                    tempNode.Title = Proxy[languageKey];
                }
                if (null != (tempNode = tempNode.ParentNode))
                {
                    tempNode.Title = siteSetting.SiteName;
                    /*updated on 3/3/3017 by techtier for redirecting to myform to topics.aspx instead of default.aspx*/
                    tempNode.Url = "/default.aspx";//tempNode.Url + "?siteid=" + siteId;
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = "Error: " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                ((Com.Comm100.Forum.UI.UIBasePage)this.Page).IfError = true;
            }
            return currentNode;
        }

        private string GetForumRewriteUrl(string url, string forumName, int forumId, int siteId)
        {
            int lastIndex = url.LastIndexOf('/');
            if (lastIndex > -1)
            {
                url = url.Substring(0, lastIndex +1);
                url = url + Com.Comm100.Framework.Common.CommonFunctions.URLReplace(forumName.Trim()) + "_f" + forumId + ".aspx?siteId=" + siteId;
            }
            return url;
        }

        private string GetTopicRewriteUrl(string url, string subject, int topicId, int forumId, int siteId)
        {
            int lastIndex = url.LastIndexOf('/');
            if (lastIndex > -1)
            {
                url = url.Substring(0, lastIndex + 1);
                url = url + Com.Comm100.Framework.Common.CommonFunctions.URLReplace(subject.Trim()) + "_t" + topicId + ".aspx?siteId=" + siteId + "&forumId=" + forumId;
            }
            return url;
        }
    }
}