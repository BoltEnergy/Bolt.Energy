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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.UserGroups
{
    public partial class UserGroupAdd : AdminBasePage
    {
        protected override void InitLanguage()
        {
            try
            {
                lblTitle.Text = Proxy[EnumText.enumForum_UserGroups_TitleNewUsergroup];
                lblSubTitle.Text = Proxy[EnumText.enumForum_UserGroups_SubtitleNewUsergroup];
                lblName.Text = Proxy[EnumText.enumForum_UserGroups_ColumnName];
                lblDescript.Text = Proxy[EnumText.enumForum_UserGroups_ColumnDescription];
                lblRequired.Text = Proxy[EnumText.enumForum_UserGroups_FieldRequired];
                btnSave1.Text = Proxy[EnumText.enumForum_UserGroups_ButtonSave];
                btnSave2.Text = Proxy[EnumText.enumForum_UserGroups_ButtonSave];
                btnCancel1.Text = Proxy[EnumText.enumForum_UserGroups_ButtonCancel];
                btnCancel2.Text = Proxy[EnumText.enumForum_UserGroups_ButtonCancel];
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //this.lblMessage.Visible = false;
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumUserGroups);
                Master.Page.Title = Proxy[EnumText.enumForum_UserGroups_TitleNewUsergroup];
                CheckGroupPermissionFunction();
                if (!IsPostBack)
                {
                    requiredName.ErrorMessage = Proxy[EnumText.enumForum_UserGroups_ErrorName];

                    txtName.MaxLength = ForumDBFieldLength.Group_nameFieldLength;
                    txtDescription.MaxLength = ForumDBFieldLength.Group_descriptionFieldLength;
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_PageNewUserErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void CheckGroupPermissionFunction()
        {
            ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(this.SiteId, this.UserOrOperatorId);
            if (!forumFeature.IfEnableGroupPermission)
                ExceptionHelper.ThrowForumSettingsCloseGroupPermissionFunction();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool ExistUserGroupName = false;
            try
            {
                ExistUserGroupName = UserGroupProcess.IfExistUserGroupName(txtName.Text, SiteId);
                if (!ExistUserGroupName)
                {
                    UserGroupProcess.AddGroup(txtName.Text, txtDescription.Text, SiteId, CurrentUserOrOperator.UserOrOperatorId);
                    Response.Redirect(string.Format("usergroup.aspx?siteId={0}", SiteId), false);
                }
                else
                {
                    txtName.Attributes.Add("onchange", "javascript:cleanRequireAlert();");
                    RequireAlert.Text = Proxy[EnumText.enumForum_UserGroups_ErrorSameName]; 
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_PageNewUsergroupErrorAdd] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("usergroup.aspx?siteId={0}", SiteId), false);
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_RedirectError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
    }
}
