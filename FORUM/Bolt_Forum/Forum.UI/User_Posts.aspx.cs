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
using Com.Comm100.Forum.Language;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.UI
{
    public partial class User_Posts : UIBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                Master.Page.Title = string.Format(Proxy[EnumText.enumForum_User_UsersPostsTitle], System.Web.HttpUtility.HtmlEncode(siteSetting.SiteName));
                //this.imgReturn.Src = this.GetButtonIMGDir() + "button_return.gif";
                CheckQueryString("userId");
                ViewState["UserId"] = Convert.ToInt32(Request.QueryString["userId"]);
                CheckAdminRole();
                if (!IsPostBack)
                {
                    aspnetPager.PageIndex = 0;
                    this.RefreshData();
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_User_UsersPostsLoadError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void CheckAdminRole()
        {
            if (!IfAdmin())
                ExceptionHelper.ThrowForumOnlyAdministratorsHavePermissionException();
        }
        private void RefreshData()
        {

            int userId = Convert.ToInt32(ViewState["UserId"]);
            int searchResultCount;
            PostWithPermissionCheck[] posts = UserControlProcess.GetMyPostsByPaging(this.SiteId, userId,
                aspnetPager.PageIndex + 1, aspnetPager.PageSize, "",
                new DateTime(), new DateTime(), out searchResultCount);
            aspnetPager.CWCDataBind(rtpData, posts, searchResultCount);
            this.lblResultCount.Text = searchResultCount.ToString();
            //this.btnReturnBack.Visible = false;
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
                int userId=Convert.ToInt32(ViewState["UserId"]);
                Response.Redirect(string.Format("User_Profile.aspx?userId={0}&siteId={1}", userId, SiteId));
            }
            catch( Exception  exp)
            {
                this.lblMessage.Text=Proxy[EnumText.enumForum_Public_RedirectError]+exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
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
                    string code = string.Format("Forum: <a class='forum_link' href='default.aspx?siteId={0}&forumId={1}'>{2}</a>",
                                                SiteId, topic.ForumId, Server.HtmlEncode(ReplaceProhibitedWords(topic.ForumName)))
                                                + "&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;" +
                                  string.Format("Topic: <a class='topic_link' href='Topic.aspx?siteId={0}&forumId={3}&topicId={1}'>{2}</a>",
                                                SiteId, topic.TopicId, Server.HtmlEncode(ReplaceProhibitedWords(topic.Subject)), topic.ForumId);
                    phcode.Controls.Add(new LiteralControl(code));
                }
            }
            catch (Exception exp)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", string.Format("<script>alert(\"{0}\");</script>", exp.Message));
                LogHelper.WriteExceptionLog(exp);
            }
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
                //throw exp; 1.0 code
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
                //throw exp; 1.0 code 
            }
        }
        protected int GetForumId(int topicId)
        {
            TopicWithPermissionCheck topic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, topicId);
            return topic.ForumId;
        }
    }
}
