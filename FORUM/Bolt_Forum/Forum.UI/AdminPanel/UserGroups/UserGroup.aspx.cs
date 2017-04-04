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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.UserGroups
{
    public partial class UserGroup : AdminBasePage
    {
        protected override void InitLanguage()
        {
            try
            {
                lblTitle.Text = Proxy[EnumText.enumForum_UserGroups_TitleUserGroupsManagement];
                lblSubTitle.Text = Proxy[EnumText.enumForum_UserGroups_SubtitleUserGroupsManagement];
                //lblId.Text = Proxy[EnumText.enumForum_UserGroups_ColumnId];
                lblName.Text = Proxy[EnumText.enumForum_UserGroups_ColumnName];
                lblDescription.Text = Proxy[EnumText.enumForum_UserGroups_ColumnDescription];
                lblMemebers.Text = Proxy[EnumText.enumForum_UserGroups_ColumnMemebers];
                lblPermissions.Text = Proxy[EnumText.enumForum_UserGroups_ColumnPermissions];
                lblOperation.Text = Proxy[EnumText.enumForum_UserGroups_ColumnOperation];
                btnNewUsergroup0.Text = Proxy[EnumText.enumForum_UserGroups_ButtonNewUsergroup];
                btnNewUsergroup1.Text = Proxy[EnumText.enumForum_UserGroups_ButtonNewUsergroup];
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }       
        }
        
        protected int GetCountOfMembers(int userGroupId)
        {
            int count = 0;
            try
            {
                count = UserGroupProcess.GetCountOfMembersByUserGroupId(SiteId, CurrentUserOrOperator.UserOrOperatorId, userGroupId);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_PageUserGroupsManagementErrorCount] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
            return count;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumUserGroups);
                Master.Page.Title = Proxy[EnumText.enumForum_UserGroups_TitleUserGroupsManagement];
                //this.lblMessage.Visible = false;
                CheckGroupPermissionFunction();
                if (!IsPostBack)
                {
                    if (Request.QueryString["action"] != null)
                    {
                        CheckQueryString("groupId");
                        string action = Request.QueryString["action"].ToLower();
                        if (action == "delete")
                        {
                            ViewState["Id"] = Request.QueryString["groupId"];
                            DeleteGroup();
                        }
                    }
                    this.Bind();
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_PageUserGroupsManagementErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void CheckGroupPermissionFunction()
        {
            ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(this.SiteId, this.UserOrOperatorId);
            if (!forumFeature.IfEnableGroupPermission)
                ExceptionHelper.ThrowForumSettingsCloseGroupPermissionFunction();
        }
        void Bind()
        {
            this.rpUserGroup.DataSource = UserGroupProcess.GetAllUserGroups(SiteId, CurrentUserOrOperator.UserOrOperatorId);
            this.rpUserGroup.DataBind();
        }

        private void DeleteGroup()
        {
            try
            {
                int id = Convert.ToInt32(ViewState["Id"]);
                UserGroupProcess.Delete(SiteId, CurrentUserOrOperator.UserOrOperatorId, id);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_PageUserGroupsManagementErrorDelete] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnNewUsergroup1_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("UsergroupAdd.aspx?siteId={0}", SiteId), false);
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_RedirectError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
    }
}
