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
using Com.Comm100.Forum.Bussiness;
using Forum.UI;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.UI.Common
{
    public class FrameBasePage:ForumBasePage
    {
        protected override void OnPreLoad(EventArgs e)
        {
            try
            {
                InitUserPermissionInPage();
                /*Check QueryString TopicId,ForumId,PostId is Right / check topic if moved*/
                PostWithPermissionCheck post; TopicWithPermissionCheck topic;
                AnnouncementWithPermissionCheck annoucement; ForumWithPermissionCheck forum;
                bool ifTopicIsMoved; int MovedToicId;
                CheckTopicIdOrForumIdOrPostIdQueryString(out post, out topic, out annoucement, out forum, out ifTopicIsMoved, out MovedToicId);

                //base.OnPreLoad(e);
                if (this.IfGuest)
                {
                    CurrentParentWindowRedirect("Login.aspx?siteid=" + this.SiteId);
                }
                if (this.IfCheckSiteIsVisitOnlyOnLoad() && this.IfSiteOnlyVisit)
                {
                    ExceptionHelper.ThrowForumSiteIsVisitOnlyException();
                }
                if (this.IfValidateForumClosed && this.IfSiteClosed)
                {
                    CurrentParentWindowRedirect("ForumIsClosed.aspx?siteId=" + SiteId);
                }
                if (base.IfIPBanned())
                {
                    CurrentParentWindowRedirect("IPBanned.aspx?siteId=" + SiteId);
                }
                if (!this.IfGuest && base.IfUserBanned(-1))
                {
                    CurrentParentWindowRedirect("UserBanned.aspx?siteId=" + SiteId);
                }
                if (forum != null && base.IfForumIsClosed(forum))
                {
                    CurrentParentWindowRedirect("Siteclosed.aspx?siteId=" + SiteId);
                }
                if (topic != null && ifTopicIsMoved)
                {
                    CurrentParentWindowRedirect("Topic.aspx?topicId=" + topic.TopicId + "&siteId=" + SiteId + "&forumId=" + forum.ForumId + "&b=1");
                }
                if (true)//can't visit through url 
                {
                    Response.Write("<script type=\"text/javascript\" language=\"javascript\">if (parent.location.href == window.location.href) window.location.href='Default.aspx?siteId=" + SiteId + "';</script>");
                }

                this.InitLanguage();
            }
            catch (Exception exp)
            {
                Response.Write(string.Format("<script type=\"text/javascript\" language=\"javascript\">window.parent.location.reload();</script>", exp.Message));
            }
        }

        protected override bool IfCheckSiteIsVisitOnlyOnLoad()
        {
            if (this.UserPermissionInForum.IfAdmin || this.UserPermissionInForum.IfModerator)
                return false;

            if (this is AbusePost /*|| this is SelectForum*/ || this is PayScore)
                //|| this is PayScore || this is SendMessages || this is BanUser || this is SelectForum
                return true;
            else
                return false;
        }

        private void CurrentParentWindowRedirect(string url)
        {
            Response.Write(string.Format("<script type=\"text/javascript\" language=\"javascript\">window.parent.location='{0}';</script>", url));
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}
