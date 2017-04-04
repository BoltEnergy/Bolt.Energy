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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Language;
using System.Configuration;

namespace Com.Comm100.Forum.UI.Common
{
    public class WebUtility
    {

        public static int GetVisitingForumId(int siteId, int userOrOperatorId, bool ifOperator)
        {
            int currentForumId = 0;

            string[] requireGetForumIdArray = new string[]
                    {
                        "/default.aspx",
                        "/topic.aspx",
                        "/addtopic.aspx",
                        "/selectforum.aspx",
                        "/abusepost.aspx",
                        "/edittopicorpost.aspx"
                    };
            string[] requireGetTopicIdArray = new string[]
                        {
                            "/edittopicorpost.aspx"
                        };

            bool found = false;
            foreach (string match in requireGetForumIdArray)
            {
                if (System.Web.HttpContext.Current.Request.Path.ToLower().Contains(match))
                {
                    found = true;
                    break;
                }
            }
            if (found)
            {

                currentForumId = int.Parse(WebUtility.GetAppSetting("forumId"));

            }
            else
            {
                foreach (string match in requireGetTopicIdArray)
                {
                    if (System.Web.HttpContext.Current.Request.Path.ToLower().Contains(match))
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    CheckQueryString("topicId");
                    int topicId = int.Parse(System.Web.HttpContext.Current.Request.QueryString["topicId"]);
                    currentForumId = TopicProcess.GetTopicByTopicId(siteId, userOrOperatorId, topicId).ForumId;
                }
            }
            return currentForumId;
        }

        public static bool IfValidateForumHidden()
        {
            string[] requireValidateForumHidenArray = new string[]
                    {
                        "/default.aspx",
                        "/topic.aspx",
                        "/addtopic.aspx",
                        "/edittopicorpost.aspx"
                    };
            bool found = false;
            foreach (string match in requireValidateForumHidenArray)
            {
                if (System.Web.HttpContext.Current.Request.Path.ToLower().Contains(match))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        public static bool IfValidateForumLock()
        {
            string[] requireValidateForumLockArray = new string[]
                    {
                        "/default.aspx",
                        "/topic.aspx",
                        "/addtopic.aspx",
                        "/edittopicorpost.aspx"
                    };
            bool found = false;
            foreach (string match in requireValidateForumLockArray)
            {
                if (System.Web.HttpContext.Current.Request.Path.ToLower().Contains(match))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }
        public static bool IfValidateCategoryClose()
        {
            string[] requireValidateCategoryCloseArray = new string[]
                    {
                        "/default.aspx",
                        "/topic.aspx",
                        "/addtopic.aspx",
                        "/edittopicorpost.aspx",
                        "/selectforum.aspx",
                        "/abusepost.aspx"
                    };
            bool found = false;
            foreach (string match in requireValidateCategoryCloseArray)
            {
                if (System.Web.HttpContext.Current.Request.Path.ToLower().Contains(match))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }
        /*Gavin 2.0*/
        public static bool IfValidateGuestAllowViewForum()
        {
            string[] UnRequireValidateForumToViewArray = new string[]
                    {
                        "/findpassword.aspx",
                        "/register.aspx",
                        "/pre_register.aspx",
                        "/post_register.aspx",
                        "/resetpassword.aspx",
                        "/redirect.aspx",
                        "/sendResetpassword.aspx",
                        "/login.aspx"
                    };
            bool found = false;
            foreach (string match in UnRequireValidateForumToViewArray)
            {
                if (System.Web.HttpContext.Current.Request.Path.ToLower().Contains(match))
                {
                    found = true;
                    break;
                }
            }
            return !found;
        }
        public static EnumForumStatus GetVisitingForumStatus(int visitingForumId, int siteId)
        {
            return ForumProcess.GetForumStatus(visitingForumId, siteId, 0, false);
        }

        public static EnumSiteStatus GetSiteStatus(int siteId, out SiteSettingWithPermissionCheck siteSetting)
        {
            siteSetting = SettingsProcess.GetSiteSettingBySiteId(siteId, 0);
            return siteSetting.SiteStatus;
        }
        /*Gavin 2.0*/
        public static GuestUserPermissionSettingWithPermissionCheck GetGuestUserPermissions(int siteId,
            //out int GuestUserSearchInterval,out bool ifAllowGuestUserSearch,
            out bool ifAllowGuestUserViewForum)
        {
            GuestUserPermissionSettingWithPermissionCheck guest = SettingsProcess.GetGuestUserPermission(
                siteId, 0);
            //GuestUserSearchInterval = guest.GuestUserSearchInterval;
            //ifAllowGuestUserSearch = guest.IfAllowGuestUserSearch;
            ifAllowGuestUserViewForum = guest.IfAllowGuestUserViewForum;
            return guest;
        }
        public static void CheckQueryString(string queryStringName)
        {
            if (System.Web.HttpContext.Current.Request.QueryString[queryStringName] == null)
            {
                ExceptionHelper.ThrowSystemQuerystringNullException(queryStringName);
            }
        }

        public static string GetTooltipString(string src)
        {
            return src.Replace("'", "&#39;").Replace("\"", "&#34;").Replace("\\r", "&#13;").Replace("\\n", "&#10;");
        }

        public static bool CanReturnPreUrl(string preUrl)
        {
            preUrl = preUrl.ToLower();
            string[] cannotReturnPreUrlArray = new string[]
                    {
                        "resetpassword",
                        "post_register",
                        "pre_register",
                        "register",
                        "findpassword",
                        "login",
                        "emailverification",
                        "clearsession",
                        "selectforum",
                        "logo",
                        "seteclosed",
                        "sendresetpasswordemail",
                        "imageverificationcode",
                        "redirect"
                    };
            bool found = false;
            foreach (string match in cannotReturnPreUrlArray)
            {
                if (preUrl.Contains(match))
                {
                    found = true;
                    break;
                }
            }
            return !found;
        }

        public static string GetTopicUrlRewritePath(string subject, int topicId)
        {
            return Com.Comm100.Framework.Common.CommonFunctions.URLReplace(subject.Trim()) + "_t" + topicId + ".aspx";
        }

        public static string GetTopicUrlRewritePath(string subject, int topicId, int siteId, int forumId)
        {
            //#if OPENSOURCE
            //            return "topic.aspx?topicId=" + topicId + "&siteId=" + siteId + "&forumId=" + forumId;
            //#else
            return Com.Comm100.Framework.Common.CommonFunctions.URLReplace(subject.Trim()) + "_t" + topicId + ".aspx?siteId=" + siteId + "&forumId=" + forumId;
            //#endif
        }



        public static string GetForumUrlRewritePath(string forumName, int forumId, int siteId)
        {
            return Com.Comm100.Framework.Common.CommonFunctions.URLReplace(forumName.Trim()) + "_f" + forumId + ".aspx?siteId=" + siteId;
        }

        public static string StatusImage(string path, bool ifRead, bool ifClose, bool ifMarkedAsAnswer, bool ifParticipant)
        {
            #region language
            LanguageProxy Proxy = new LanguageProxy();
            Com.Comm100.Language.EnumLanguage languagetype = Proxy._language;
            #endregion
            string strTmp = "";
            if ((ifRead) && (!ifClose) && (!ifMarkedAsAnswer) && (ifParticipant))
            {
                strTmp = string.Format("<img src=\"{1}/Status/participate_normal.gif\" alt='{0}' title='{0}' >", Proxy[EnumText.enumForum_Topic_StatusNormalParticipated], path);//Normal Topic You Participated  
            }
            else if ((ifRead) && (ifClose) && (!ifMarkedAsAnswer) && (ifParticipant))
            {
                strTmp = string.Format("<img src=\"{1}/Status/participate_close.gif\" alt='{0}' title='{0}' >", Proxy[EnumText.enumForum_Topic_StatusClosedParticipated], path);//Closed Topic You Participated 
            }
            else if ((ifRead) && (!ifClose) && (ifMarkedAsAnswer) && (ifParticipant))
            {
                strTmp = string.Format("<img src=\"{1}/Status/participate_mark.gif\" alt='{0}' title='{0}' >", Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated], path);//Marked Topic You Participated
            }
            else if ((!ifRead) && (!ifClose) && (!ifMarkedAsAnswer) && (ifParticipant))
            {
                strTmp = string.Format("<img src=\"{1}/Status/participate_normal.gif\" alt='{0}' title='{0}' >", Proxy[EnumText.enumForum_Topic_StatusNormalParticipated], path);//Normal Topic You Participated 
            }
            else if (!ifRead && (ifClose) && (!ifMarkedAsAnswer) && (ifParticipant))
            {
                strTmp = string.Format("<img src=\"{1}/Status/participate_close.gif\" alt='{0}' title='{0}' >", Proxy[EnumText.enumForum_Topic_StatusClosedParticipated], path);//Closed Topic You Participated 
            }
            else if ((!ifRead) && (!ifClose) && (ifMarkedAsAnswer) && (ifParticipant))
            {
                strTmp = string.Format("<img src=\"{1}/Status/participate_mark.gif\" alt='{0}' title='{0}' >", Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated], path);//Marked Topic You Participated 
            }
            else if ((ifRead) && (!ifClose) && (!ifMarkedAsAnswer) && (!ifParticipant))
            {
                strTmp = string.Format("<img src=\"{1}/Status/read_normal.gif\" alt='{0}' title='{0}' >", Proxy[EnumText.enumForum_Topic_StatusNormalRead], path);//Normal Read Topic
            }
            else if ((ifRead) && (ifClose) && (!ifMarkedAsAnswer) && (!ifParticipant))
            {
                strTmp = string.Format("<img src=\"{1}/Status/read_close.gif\" alt='{0}' title='{0}' >", Proxy[EnumText.enumForum_Topic_StatusClosedRead], path);//Closed Read Topic
            }
            else if ((ifRead) && (!ifClose) && (ifMarkedAsAnswer) && (!ifParticipant))
            {
                strTmp = string.Format("<img src=\"{1}/Status/read_mark.gif\" alt='{0}' title='{0}' >", Proxy[EnumText.enumForum_Topic_StatusMarkedRead], path);//Marked Read Topic 
            }
            else if ((!ifRead) && (!ifClose) && (!ifMarkedAsAnswer) && (!ifParticipant))
            {
                strTmp = string.Format("<img src=\"{1}/Status/noread_normal.gif\" alt='{0}' title='{0}' >", Proxy[EnumText.enumForum_Topic_StatusNormalUnRead], path);//Normal Unread Topic
            }
            else if ((!ifRead) && (ifClose) && (!ifMarkedAsAnswer) && (!ifParticipant))
            {
                strTmp = string.Format("<img src=\"{1}/Status/noread_close.gif\" alt='{0}' title='{0}' >", Proxy[EnumText.enumForum_Topic_StatusClosedUnRead], path);//Closed Unread Topic
            }
            else if ((!ifRead) && (!ifClose) && (ifMarkedAsAnswer) && (!ifParticipant))
            {
                strTmp = string.Format("<img src=\"{1}/Status/noread_mark.gif\" alt='{0}' title='{0}' >", Proxy[EnumText.enumForum_Topic_StatusMarkedUnRead], path);//Marked Unread Topic 
            }
            else if ((!ifRead) && (ifClose) && (ifMarkedAsAnswer) && (ifParticipant))
            {
                strTmp = string.Format("<img src=\"{1}/Status/participate_close.gif\" alt='{0}' title='{0}' >", Proxy[EnumText.enumForum_Topic_StatusClosedParticipated], path);//Closed Topic You Participated 
            }
            else if ((ifRead) && (ifClose) && (ifMarkedAsAnswer) && (ifParticipant))
            {
                strTmp = string.Format("<img src=\"{1}/Status/participate_close.gif\" alt='{0}' title='{0}' >", Proxy[EnumText.enumForum_Topic_StatusClosedParticipated], path);//Closed Topic You Participated 
            }
            else if ((ifRead) && (ifClose) && (ifMarkedAsAnswer) && (!ifParticipant))
            {
                strTmp = string.Format("<img src=\"{1}/Status/read_close.gif\" alt='{0}' title='{0}' >", Proxy[EnumText.enumForum_Topic_StatusClosedRead], path);//Closed Read Topic 
            }
            else if ((!ifRead) && (ifClose) && (ifMarkedAsAnswer) && (!ifParticipant))
            {
                strTmp = string.Format("<img src=\"{1}/Status/noread_close.gif\" alt='{0}' title='{0}' >", Proxy[EnumText.enumForum_Topic_StatusClosedUnRead], path);//Closed Unread Topic
            }
            return strTmp;
        }

        public static string GetAppSetting(string keyName)
        {
            return (ConfigurationManager.AppSettings[keyName] != null ? Convert.ToString(ConfigurationManager.AppSettings[keyName]) : "");
        }

        public static string GetInitialClass(string username)
        {
            if (string.IsNullOrEmpty(username))
                return "green";

            string[] Colours = { "blue", "red", "pink", "orange", "green", "bronze" };

            string[] blue = { "a", "b", "c", "d", "e" };
            string[] red = { "f", "g", "h", "i" };
            string[] pink = { "j", "k", "l", "m", "n" };

            string[] orange = { "o", "p", "q", "r", "s" };
            string[] green = { "t", "u", "v", "w" };
            string[] bronze = { "x", "y", "z" };


            string csscolor = "blue";

            if (blue.ToList().Any(item => username.ToLower().StartsWith(item)))
                csscolor = "blue";
            else if (red.ToList().Any(item => username.ToLower().StartsWith(item)))
                csscolor = "red";
            else if (pink.ToList().Any(item => username.ToLower().StartsWith(item)))
                csscolor = "pink";
            else if (orange.ToList().Any(item => username.ToLower().StartsWith(item)))
                csscolor = "orange";
            else if (green.ToList().Any(item => username.ToLower().StartsWith(item)))
                csscolor = "green";
            else if (bronze.ToList().Any(item => username.ToLower().StartsWith(item)))
                csscolor = "bronze";


            return csscolor;
        }

    }
}
