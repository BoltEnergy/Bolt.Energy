
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
using Com.Comm100.Forum.UI.AdminPanel;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.WebControls;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.HelpDocument;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Language;
using Com.Comm100.Framework.FieldLength;

namespace Com.Comm100.Forum.UI.AdminPanel.Users
{
    public partial class AddUser : AdminBasePage
    {
        protected override void InitLanguage()
        {

            try
            {
                lblTitle.Text = Proxy[EnumText.enumForum_User_TitleNewUser];
                lblSubTitle.Text = Proxy[EnumText.enumForum_User_SubtitleNewUser];
                chkAdministrator.Text = Proxy[EnumText.enumForum_User_chkAdministrator];
                chkIfShowDisplayName.Checked = true;
                Master.Page.Title = Proxy[EnumText.enumForum_User_TitleNewUser];
                btnSave1.Text = Proxy[EnumText.enumForum_User_ButtonSave];
                btnSave2.Text = Proxy[EnumText.enumForum_User_ButtonSave];
                btnCancel1.Text = Proxy[EnumText.enumForum_Public_ButtonCancel];
                btnCancel2.Text = Proxy[EnumText.enumForum_Public_ButtonCancel];
                ValidEmailRequired.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorEmailRequired];
                ValidEmailExpression.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorMailFormat];
                ValidDisplayNameRequired.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorDisplayNameRequired];
                RequiredTxtPassword.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorPasswordRequired];
                RequiredTxtConfirmPassword.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorRetypePasswordRequired];
                ComparePassword.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorMatchPasswords];
                VaildAgeRange.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorAge];
                //ValidAgeRequired.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorAge];
                string strVisible = Proxy[EnumText.enumForum_User_FieldVisibletoPublic];
                this.chkIfShowAge.Text = strVisible;
                this.chkIfShowGender.Text = strVisible;
                this.chkIfShowCompany.Text = strVisible;
                this.chkIfShowDisplayName.Text = strVisible;
                this.chkIfShowEmail.Text = strVisible;
                this.chkIfShowFaxNumber.Text = strVisible;
                this.chkIfShowHomePage.Text = strVisible;
                this.chkIfShowInterests.Text = strVisible;
                this.chkIfShowOccupation.Text = strVisible;
                this.chkIfShowPhoneNumber.Text = strVisible;
                this.chkIfShowUserName.Text = strVisible;
             
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError]+ ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumUserManage);
                this.Page.SetFocus(txtEmail);
                if (!IsPostBack)
                {
                    ViewState["pageindex"] = Convert.ToInt32(Request.QueryString["pageindex"]);
                    ViewState["pagesize"] = Convert.ToInt32(Request.QueryString["pagesize"]);
                    InitTextMaxLength();
                    BindGender();
                    BindUserGroups();

                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_User_PageNewUserErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        #region Private Function BindUserGroups
        private void BindUserGroups()
        {
            ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(SiteId, CurrentUserOrOperator.UserOrOperatorId);
            if (forumFeature.IfEnableGroupPermission)
            {
#if OPENSOURCE
#else
                chkAdministrator.Visible = false;
#endif
                UserGroupWithPermissionCheck[] userGroups = UserGroupProcess.GetUserGroupsExceptAllForumUser(SiteId, CurrentUserOrOperator.UserOrOperatorId);
                if (userGroups == null || userGroups.Length == 0)
                {
                    lblAddUserGroup.Visible = true;
                    lblAddUserGroup.Text = String.Format(Proxy[EnumText.enumForum_Usse_lblAddUserGroup], SiteId.ToString());
                }
                else
                {
                    var groups = from g in userGroups
                                 select new {Name = Server.HtmlEncode(g.Name),UserGroupId =g.UserGroupId};
                    cblGroup.DataSource = groups;
                    cblGroup.DataTextField = "Name";
                    cblGroup.DataValueField = "UserGroupId";
                    cblGroup.DataBind();
                }
            }
            else
            {
                panelUserGroup.Visible = false;
            }
        }
        #endregion Private Function BindUserGroups

        #region Private Function InitTextMaxLength
        private void InitTextMaxLength()
        {
            this.txtEmail.MaxLength = ForumDBFieldLength.User_emailFieldLength;
            this.txtDisplayName.MaxLength = ForumDBFieldLength.User_nameFieldLength;
            this.txtPassword.MaxLength = ForumDBFieldLength.User_passwordFieldLength;
            this.txtConfirmPassword.MaxLength = ForumDBFieldLength.User_passwordFieldLength;
            this.txtFirstName.MaxLength = ForumDBFieldLength.User_firstNameFieldLength;
            this.txtLastName.MaxLength = ForumDBFieldLength.User_lastNameFieldLength;
            this.txtAge.MaxLength = 3;
            this.txtOccupation.MaxLength = ForumDBFieldLength.User_occupationFieldLength;
            this.txtCompany.MaxLength = ForumDBFieldLength.User_companyFieldLength;
            this.txtPhoneNumber.MaxLength = ForumDBFieldLength.User_phoneNumberFieldLength;
            this.txtFaxNumber.MaxLength = ForumDBFieldLength.User_faxNumberFieldLength;
            this.txtInterests.MaxLength = ForumDBFieldLength.User_interestsFieldLength;
            this.txtHomePage.MaxLength = ForumDBFieldLength.User_homePageFieldLength;
        }
        #endregion Private Function InitTextMaxLength

        #region Private Function BindGender
        private void BindGender()
        {
            ddlGender.Items.Add(new ListItem(Proxy[EnumText.enumForum_User_FieldMale], Convert.ToInt16(EnumGender.Male).ToString()));
            ddlGender.Items.Add(new ListItem(Proxy[EnumText.enumForum_User_FieldFemale], Convert.ToInt16(EnumGender.Female).ToString()));
            ddlGender.Items.Add(new ListItem(Proxy[EnumText.enumForum_User_FieldItsasecret], Convert.ToInt16(EnumGender.ItsaSecret).ToString()));
        }
        #endregion Private Function BindGender

        //#region Private Function BindDisplayNameValidateExpression
        //private void BindDisplayNameValidateExpression()
        //{
        //    RegistrationSettingWithPermissionCheck registrationSetting = SettingsProcess.GetRegistrationSettingBySiteId(CurrentUserOrOperator.UserOrOperatorId, SiteId);
        //    if (registrationSetting.DisplayNameRegularExpression != "")
        //    {
        //        ValidDisplayNameExpression.ValidationExpression = registrationSetting.DisplayNameRegularExpression;
        //        ValidDisplayNameExpression.ErrorMessage = registrationSetting.DisplayNameInstruction;
        //    }
        //    else
        //    {
        //        ValidDisplayNameExpression.Visible = false;
        //    }
        //}
        //#endregion Private Function BindDisplayNameValidateExpression

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
                {
                    ComparePassword.IsValid = false;
                    throw new Exception(ComparePassword.ErrorMessage);
                }
                #region Init Parameters
                int userId = Convert.ToInt32(ViewState["Id"]);
                string email = txtEmail.Text;
                string displayName = txtDisplayName.Text;
                string password = txtPassword.Text;
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                int age;
                if (txtAge.Text.Trim() != "")
                    age = Convert.ToInt32(txtAge.Text.Trim());
                else
                    age = 0;
                EnumGender gender = (EnumGender)Convert.ToInt32(ddlGender.SelectedValue);
                string company = txtCompany.Text;
                string occupation = txtOccupation.Text;
                string phone = txtPhoneNumber.Text;
                string fax = txtFaxNumber.Text;
                string interests = txtInterests.Text;
                string homepage = txtHomePage.Text;
                bool ifShowEmail = chkIfShowEmail.Checked;
                bool ifShowUserName = chkIfShowUserName.Checked;
                bool ifShowAge = chkIfShowAge.Checked;
                bool ifShowGender = chkIfShowGender.Checked;
                bool ifShowOccupation = chkIfShowOccupation.Checked;
                bool ifShowCompany = chkIfShowCompany.Checked;
                bool ifShowPhone = chkIfShowPhoneNumber.Checked;
                bool ifShowFax = chkIfShowFaxNumber.Checked;
                bool ifShowInterests = chkIfShowInterests.Checked;
                bool ifShowHomePage = chkIfShowHomePage.Checked;
                List<int> userGroupIds = new List<int>();
                for (int i = 0; i < cblGroup.Items.Count; i++)
                {
                    if (cblGroup.Items[i].Selected) userGroupIds.Add(Convert.ToInt32(cblGroup.Items[i].Value));
                }
                #endregion Init Parameters

                UserProcess.AddUser(SiteId, CurrentUserOrOperator.UserOrOperatorId, email, displayName, password, firstName, lastName, age, gender,
                    company, occupation, phone, fax, interests, homepage, ifShowEmail, ifShowUserName, ifShowAge, ifShowGender, ifShowOccupation,
                    ifShowCompany, ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage, userGroupIds);

                Response.Redirect("UserList.aspx?pageindex=" + ViewState["pageindex"].ToString() + "&pagesize=" + ViewState["pagesize"].ToString() + "&SiteId=" + SiteId, false);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_User_PageNewUserErrorAdd] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("UserList.aspx?pageindex=" + ViewState["pageindex"].ToString() + "&pagesize=" + ViewState["pagesize"].ToString() + "&SiteId=" + SiteId, false);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Public_RedirectError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
    }
}
