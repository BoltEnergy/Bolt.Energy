#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif

using System;
using System.Collections.Generic;
using System.Web;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.UI.Common;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
//using Forum.UI;
using System.Linq;
using Forum.UI;

namespace Com.Comm100.Forum.UI.Common
{
    public class ForumBasePage : UIBasePage
    {
        /*User Permission In Page*/
        private UserPermissionInFourm _userPermissionInForum;
        public UserPermissionInFourm UserPermissionInForum
        {
            get { return _userPermissionInForum; }
        }


        private ForumFeature _forumFeature;
        public ForumFeature ForumFeature { get { return _forumFeature; } }

        public string StyleIfScoreEnabled { get { 
            if (!_forumFeature.IfEnableScore) { return "display:none;"; }
            else { return ""; }; } }

        protected override void OnPreLoad(EventArgs e)
        {
            try
            {
                base.OnPreLoad(e);

                InitUserPermissionInPage();
                CheckIfSiteIsVisitOnlyOnLoad();
                /*Check QueryString TopicId,ForumId,PostId is Right / check topic if moved*/
                PostWithPermissionCheck post; TopicWithPermissionCheck topic;
                AnnouncementWithPermissionCheck annoucement; ForumWithPermissionCheck forum;
                bool ifTopicIsMoved; int MovedToicId;
                CheckTopicIdOrForumIdOrPostIdQueryString(out post, out topic, out annoucement, out forum, out ifTopicIsMoved, out MovedToicId);
                if (ifTopicIsMoved)
                {
                    string url = "topic.aspx?topicId=" + MovedToicId + "&forumId=" + forum.ForumId + "&siteId=" + SiteId;
                    Response.Redirect(url, false);
                    Response.End();
                }
                if (forum != null)
                    CheckForumIsClosed(forum);
            }
            catch (Exception exp)
            {
                string js = "alert('Error: " + exp.Message.Replace("'", "\\'") + "');"
                               + "window.location.href ='" + this.UrlWithAuthorityAndApplicationPath + "default.aspx?siteid=" + this.SiteId + "'";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "BasePageErrorWriter", js, true);
                this.IfError = true;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitForumFeatured();
        }

        protected void InitUserPermissionInPage()
        {
            if (IfGuest)
            {
                GuestUserPermissionSettingWithPermissionCheck guestUser = SettingsProcess.GetGuestUserPermission(SiteId, UserOrOperatorId);
                _userPermissionInForum = new UserPermissionInFourm(_visitingForumId, IfAdmin(), IfModerator(),
                    guestUser.IfAllowGuestUserViewForum,
                    true, false, false, 0, int.MaxValue, 0, false, false, false, 0, 0, 0, 0,
                    guestUser.IfAllowGuestUserSearch, guestUser.GuestUserSearchInterval, false);
            }

            else if (IfAdmin() || IfModerator())
            {
                _userPermissionInForum = new UserPermissionInFourm(_visitingForumId, IfAdmin(), IfModerator(),
                    true, true, true, true, int.MaxValue, 0, int.MaxValue, true, true, true, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue,
                    true, 0, true);
            }
            else
            {
                _userPermissionInForum = new UserPermissionInFourm(_visitingForumId, IfAdmin(), IfModerator(),
                    this.UserForumPermissionList(_visitingForumId).IfAllowViewForum,
                    this.UserForumPermissionList(_visitingForumId).IfAllowViewTopic,
                    this.UserForumPermissionList(_visitingForumId).IfAllowPost,
                    this.UserPermissionCache.IfAllowCustomizeAvatar,
                    this.UserPermissionCache.MaxLengthofSignature,
                    this.UserForumPermissionList(_visitingForumId).MinIntervalForPost,
                    this.UserForumPermissionList(_visitingForumId).MaxLengthOfPost,
                    //this.UserForumPermissionList(_visitingForumId).IfAllowHTML,
                    this.UserForumPermissionList(_visitingForumId).IfAllowUrl,
                    this.UserForumPermissionList(_visitingForumId).IfAllowUploadImage,
                    this.UserPermissionCache.IfAllowUploadAttachment,
                    this.UserPermissionCache.MaxCountOfAttacmentsForOnePost,
                    this.UserPermissionCache.MaxSizeOfOneAttachment,
                    this.UserPermissionCache.MaxSizeOfAllAttachments,
                    this.UserPermissionCache.MaxCountOfMessageSendOneDay,
                    this.UserPermissionCache.IfAllowSearch,
                    this.UserPermissionCache.MinIntervalForSearch,
                    this.UserForumPermissionList(_visitingForumId).IfPostNotNeedModeration);
            }
        }

        protected void CheckIfSiteIsVisitOnlyOnLoad()
        {
            if (IfCheckSiteIsVisitOnlyOnLoad() && IfSiteOnlyVisit)
            {
                ExceptionHelper.ThrowForumSiteIsVisitOnlyException();
            }
        }

        protected virtual bool IfCheckSiteIsVisitOnlyOnLoad()
        {
            if (this.UserPermissionInForum.IfAdmin || this.UserPermissionInForum.IfModerator)
                return false;
            if (this is AddTopic || this is EditTopic) // EditTopicOrPage.cs
            {
                return true;
            }
            else
                return false;
        }

        protected void CheckTopicIdOrForumIdOrPostIdQueryString(out PostWithPermissionCheck post,
           out TopicWithPermissionCheck topic, out AnnouncementWithPermissionCheck annoucement,
           out ForumWithPermissionCheck forum, out bool ifTopicIsMoved, out int MovedTopicId)
        {
            post = null; topic = null; annoucement = null; forum = null; ifTopicIsMoved = false; MovedTopicId = -1;
            string postId = "postId"; string topicId = "topicId"; string forumId = "forumId";
            int npostId = -1; int ntopicId = -1; int nforumId = -1;
            //get id
            if (!CheckQueryStringIsNUll(postId))
            {
                npostId = Convert.ToInt32(Request.QueryString[postId]);
            }
            if (!CheckQueryStringIsNUll(topicId))
            {
                ntopicId = Convert.ToInt32(Request.QueryString[topicId]);
            }
            /*
            if (!CheckQueryStringIsNUll(forumId))
            {
                nforumId = Convert.ToInt32(Request.QueryString[forumId]);
            }
             */

            nforumId = Convert.ToInt32(WebUtility.GetAppSetting(Constants.WK_ForumId));

            //check 
            if (npostId != -1 && !(this.GetType().Name == "topic_aspx"))
            {
                post = PostProcess.GetPostByPostId(SiteId, UserOrOperatorId, IfOperator, npostId);
                if (post.IfDeleted)
                    ExceptionHelper.ThrowPostNotExistException(npostId);
                if (post.TopicId != ntopicId)
                    ExceptionHelper.ThrowPostNotExistException(npostId);
            }
            if (ntopicId != -1)
            {
                //annoucement
                bool ifAnnoucement;
                TopicBase topicbase = TopicProcess.CreateTopic(UserOrOperatorId, SiteId, ntopicId, out ifAnnoucement);
                if (!ifAnnoucement)
                {
                    topic = topicbase as TopicWithPermissionCheck;
                    if (topic.IfDeleted && !(IfAdmin()|| IfModerator()))
                        ExceptionHelper.ThrowTopicNotExistException(ntopicId);
                    if (topic.ForumId != nforumId)
                    {
                        //check Topic moved
                        TopicWithPermissionCheck topic1 = TopicProcess.GetLastMovedTopicInForum(SiteId, UserOrOperatorId, ntopicId, nforumId);
                        if (topic1 != null)
                        {
                            ifTopicIsMoved = true;
                            MovedTopicId = topic1.TopicId;
                        }
                        else
                        {
                            ExceptionHelper.ThrowTopicNotExistException(ntopicId);
                        }
                    }
                }
                //topic
                else
                {
                    annoucement = topicbase as AnnouncementWithPermissionCheck;
                    ForumWithPermissionCheck[] forumsOfAnnoucement = ForumProcess.GetForumsofAnnoucement(annoucement.TopicId, SiteId, UserOrOperatorId);
                    var result = (from forum1 in forumsOfAnnoucement
                                  where forum1.ForumId == nforumId
                                  select forum1).FirstOrDefault();
                    if (result == null)
                    {
                        ExceptionHelper.ThrowAnnouncementNotExsitException(annoucement.TopicId);
                    }
                }
            }
            if (nforumId != -1)
            {
                forum = ForumProcess.GetForumByForumId(SiteId, UserOrOperatorId, nforumId);
            }
        }

        private bool CheckQueryStringIsNUll(string key)
        {
            if (Request.QueryString[key] == null || Request.QueryString[key] == "")
                return true;
            else
                return false;
        }

        protected void CheckForumIsClosed(ForumWithPermissionCheck forum)
        {
            /*Check If Forum Is Closed*/
            if (IfForumIsClosed(forum))
            {
                string applicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                if (applicationPath != "/")
                {
                    applicationPath = applicationPath + "/";
                }
                Response.Redirect("~/ForumIsClosed.aspx?siteId=" + SiteId);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        protected virtual bool IfForumIsClosed(ForumWithPermissionCheck forum)
        {
            if (!_userPermissionInForum.IfAdmin && !_userPermissionInForum.IfModerator
                && !(forum.Status == EnumForumStatus.Open))
                return true;
            else
                return false;
        }


        private void InitForumFeatured()
        {
            _forumFeature = SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId);
        }

     

    }
}
