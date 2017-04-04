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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.ModeratorPanel.Forum
{
    public partial class Forums : Com.Comm100.Forum.UI.ModeratorPanel.ModeratorBasePage
    {

        #region property IfDisplay
        string _ifDisplay=null;
        protected string IfDisplay
        {
            get
            {
                if(_ifDisplay == null)
                {
                    ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId);
                    if (!forumFeature.IfEnableGroupPermission && !forumFeature.IfEnableReputationPermission)
                        _ifDisplay = "display:none";
                    else
                        _ifDisplay = "";
                }
                return _ifDisplay;
            }
        }
        #endregion

        #region Language
        string ErrorLoad;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Forums_ModeratorPanelForumsLoadError];
                this.lblTitle.Text = Proxy[EnumText.enumForum_Forums_ModeratorPanleForumsTitle];
                this.lblSubTitle.Text = Proxy[EnumText.enumForum_Forums_ModeratorPanleForumsSubTitle];
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError];
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion 
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((ModeratorMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumForumManage);
                //Master.Page.Title = "Forum Management";
                if (!IsPostBack)
                {
                    RefreshData();
                }
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void RefreshData()
        {
            try
            {
                this.rpForums.DataSource = ForumProcess.GetForumsOfModerator(this.SiteId, this.UserOrOperatorId);
                this.rpForums.DataBind();
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void rpData_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            
        }
        public string GetForumStatusName(EnumForumStatus status)
        {
            if (status == EnumForumStatus.Open)
                return Proxy[EnumText.enumForum_Forums_StatusNormal];
            else
                return Proxy[EnumText.enumForum_Forums_StatusClose];
        }

        protected void rpForums_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                string url = "";
                int forumId = Convert.ToInt32(e.CommandArgument);
                switch (e.CommandName)
                {
                    case "Announcements":
                        url=string.Format("../Announcement/Announcements.aspx?siteId={0}&forumId={1}", this.SiteId, forumId);
                        break;
                    case "Topics":
                        url=string.Format("../TopicAndPost/Topics.aspx?siteId={0}&forumId={1}", this.SiteId, forumId);
                        break;
                    case "Permissions":
                        url=string.Format("ForumPermission.aspx?siteId={0}&forumId={1}", this.SiteId, forumId);
                        break;
                }
                Response.Redirect(url);
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
    }
}
