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
using System.Web.UI.MobileControls;
using System.Collections;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.Language;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.UI.Common;

namespace Com.Comm100.Forum.UI.UserControl
{
    public partial class ForumFooter : BaseUserControl
    {
        #region Permission Check
        protected GuestUserPermissionSettingWithPermissionCheck TheGeustUserPermission
        { get; set; }

        private bool IfAllowViewForum(int forumId)
        {
            if ((this.Page as UIBasePage).IfGuest)
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

                //UserForumPermissionItem item = (this.Page as UIBasePage).UserForumPermissionList(forumId);
                //if ((this.Page as UIBasePage).IfAdmin() || item.IfModerator)
                //    return true;
                //else
                //    return item.IfAllowViewForum;
                return true;
            }
        }
        #endregion
        private string copyright;
        private string selectForum;
        private string errLoad;
        protected void InitLanguage()
        {
            try
            {
                copyright = Proxy[EnumText.enumForum_HeaderFooter_Copyright];
                selectForum = Proxy[EnumText.enumForum_HeaderFooter_SelectForum];
                errLoad = Proxy[EnumText.enumForum_HeaderFooter_FooterErrorLoad];
            }
            catch (Exception exp)
            {
                SetError(exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitLanguage();
            if (!IsPostBack)
            {
                lblMessage.Visible = false;
                lblMessage.Text = errLoad;

                int siteId = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).SiteId;
                int userOrOperatorId = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).UserOrOperatorId;
                bool ifOperator = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).IfOperator;


                //string siteName = "";
                try
                {
                    //SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(siteId, userOrOperatorId);
                    //siteName = siteSetting.SiteName;
                }
                catch (Exception exp)
                {
                    SetError(exp);
                }
                //literalCopyright.Text = string.Format(copyright, System.Web.HttpUtility.HtmlEncode(siteName));
                literalCopyright.Text = copyright;

                if (Request.QueryString["ifAdvancedMode"] == null)
                {
                    try
                    {
                        StyleSettingWithPermissionCheck styleSetting = StyleProcess.GetStyleSettingBySiteId(siteId, userOrOperatorId);
                        if (styleSetting != null && styleSetting.IfAdvancedMode)
                        {
                            Literal1.Text = styleSetting.PageFooter;
                        }
                    }
                    catch (Exception exp)
                    {
                        SetError(exp);
                    }
                }

                this.RefreshData();
                if (Request.QueryString["forumId"] != null)
                {
                    ddlForumJump.SelectedValue = "forumId=" + Request.QueryString["forumId"];
                }

                this.ddlForumJump.Attributes.Add("onchange", "javascript:jumpForum();");
            }
            else
            {
                if (this.hdnForumJump.Value.Length > 0)
                {
                    this.hdnForumJump.Value = "";
                    this.JumpForum();
                }
            }
            achTerms.HRef = WebUtility.GetAppSetting("Terms");
            achPrivacy.HRef = WebUtility.GetAppSetting("Privacy");

        }

        private void SetError(Exception exp)
        {
            lblMessage.Visible = true;
            lblMessage.Text += exp.Message + "<br />";
            LogHelper.WriteExceptionLog(exp);
            ((Com.Comm100.Forum.UI.UIBasePage)this.Page).IfError = true;
        }

        private void RefreshData()
        {
            try
            {
                ListItem li = new ListItem();
                li.Text = selectForum;
                li.Value = "-1";
                li.Attributes.Add("style", "color:gray");
                ddlForumJump.Items.Add(li);

                li = new ListItem();
                li.Text = "------------------";
                li.Value = "-2";
                li.Attributes.Add("style", "color:gray");
                ddlForumJump.Items.Add(li);

                int userOrOperatorId = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).UserOrOperatorId;
                bool ifOperator = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).IfOperator;
                int siteId = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).SiteId;
                this.TheGeustUserPermission = SettingsProcess.GetGuestUserPermission(
                           siteId, userOrOperatorId);
                CategoryWithPermissionCheck[] categoryArray = CategoryProcess.GetAllCategories(userOrOperatorId, ifOperator, siteId);
                foreach (CategoryWithPermissionCheck category in categoryArray)
                {
                    //ForumWithPermissionCheck[] forumArray = ForumProcess.GetNotHiddenForumsByCategoryID(0, siteId, category.CategoryId);

                    ForumWithPermissionCheck[] tmpForumArray = ForumProcess.GetForumsByCategoryID(0, siteId, category.CategoryId);
                    
                    //bool ifModeratorOfCategory;
                    //ForumWithPermissionCheck[] forums = ForumsCanVisitWhenCategoryClosed(category,
                    //                HideWithNOPermissionForum(forumArray), out ifModeratorOfCategory);
                    //if (!ifModeratorOfCategory)
                    //{
                    //    //continue;

                    //}

                    li = new ListItem();
                    li.Text = category.Name;
                    li.Value = "categoryId=" + category.CategoryId;
                    li.Attributes.Add("style", "color:gray");
                    ddlForumJump.Items.Add(li);

                    foreach (ForumWithPermissionCheck forum in tmpForumArray)
                    {
                        /* Jason 2.0 forum lock/close and admin or moderator*/
                        if (IfForumIsLockOrClosed(forum) && !(this.Page as UIBasePage).IfModerator(forum.ForumId) && !(this.Page as UIBasePage).IfAdmin())
                        {
                            li = new ListItem();
                            li.Text = HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;" + forum.Name);
                            li.Value = "-2";
                            li.Attributes.Add("style", "color:gray");
                            ddlForumJump.Items.Add(li);
                        }
                        else
                        {
                            ddlForumJump.Items.Add(new ListItem(HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;" + forum.Name), "forumId=" + forum.ForumId));
                        }
                        
                    }
                }
            }
            catch (Exception exp)
            {
                SetError(exp);
            }
        }

        private void JumpForum()
        {
            if (ddlForumJump.SelectedIndex != -1)
            {
                if (ddlForumJump.SelectedValue[0] == 'f')
                {
                    Response.Redirect(
                        string.Format(
                        "~/default.aspx?{0}&siteid={1}", ddlForumJump.SelectedValue, (this.Page as UIBasePage).SiteId)
                        , false);
                }
            }
        }

        /*Gavin 2.0*/
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
            ForumWithPermissionCheck[] forums, out bool ifModeratorOfCategory)
        {
            ifModeratorOfCategory = true;
            if (catetory.Status != EnumCategoryStatus.Close)
                return forums;
            if ((this.Page as UIBasePage).IfAdmin())
                return forums;
            ifModeratorOfCategory = false;
            List<ForumWithPermissionCheck> forumsToShow = new List<ForumWithPermissionCheck>();
            foreach (var forum in forums)
            {
                if ((this.Page as UIBasePage).IfModerator(forum.ForumId))
                {
                    ifModeratorOfCategory = true;
                    forumsToShow.Add(forum);
                }
            }
            return forumsToShow.ToArray();
        }

        private string GetForumRewriteUrl(string url, string forumName, int forumId, int siteId)
        {
            int lastIndex = url.LastIndexOf('/');


            url = url.Substring(0, lastIndex + 1);
            url = url + Com.Comm100.Framework.Common.CommonFunctions.URLReplace(forumName.Trim()) + "_f" + forumId + ".aspx?siteId=" + siteId;

            return url;
        }
    }
}