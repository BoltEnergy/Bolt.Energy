
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
    public partial class EditUser : AdminBasePage
    {
        protected override void InitLanguage()
        {
            try
            {
                lblTitle.Text = Proxy[EnumText.enumForum_User_TitleEdit];
                lblSubTitle.Text = Proxy[EnumText.enumForum_User_SubtitleEdit];
                chkAdministrator.Text = Proxy[EnumText.enumForum_User_chkAdministrator];
                Master.Page.Title = Proxy[EnumText.enumForum_User_TitleEdit];
                btnSave1.Text = Proxy[EnumText.enumForum_User_ButtonSave];
                btnSave2.Text = Proxy[EnumText.enumForum_User_ButtonSave];
                btnCancel1.Text = Proxy[EnumText.enumForum_Public_ButtonCancel];
                btnCancel2.Text = Proxy[EnumText.enumForum_Public_ButtonCancel];
                ValidEmailRequired.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorEmailRequired];
                ValidEmailExpression.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorMailFormat];
                ValidDisplayNameRequired.ErrorMessage = Proxy[EnumText.enumForum_Register_ErrorDisplayNameRequired];
                VaildAgeRange.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorAge];
                ValidAgeRequired.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorAge];
                VaildScoreRange.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorScoreRange];
                ValidScoreRequired.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorScoreRequired];
                VaildReputationRange.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorReputationRange];
                ValidReputationRequired.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorReputationRequired];
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
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + ex.Message;
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
                    ViewState["Id"] = Convert.ToInt32(Request.QueryString["Id"]);
                    ViewState["pageindex"] = Convert.ToInt32(Request.QueryString["pageindex"]);
                    ViewState["pagesize"] = Convert.ToInt32(Request.QueryString["pagesize"]);

                    InitTextMaxLength();
                    //BindDisplayNameValidateExpression();
                    BindGender();
                    BindUserGroups();
                    BindUser();
                    
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_User_PageEditUserErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        #region Private Function BindUserGroups
        private void BindUserGroups()
        {
            ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(SiteId, CurrentUserOrOperator.UserOrOperatorId);
            if (forumFeature.IfEnableScore==false&&forumFeature.IfEnableReputationPermission==false)
                panelAdditionInfor.Visible = false;
            if (forumFeature.IfEnableScore == false)
                trfieldscore.Visible = false;
            if (forumFeature.IfEnableReputation == false)
                trfieldreputation.Visible = false;
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
                                 select new { Name = Server.HtmlEncode(g.Name), UserGroupId = g.UserGroupId };
                    cblGroup.DataSource = groups;
                    cblGroup.DataTextField = "Name";
                    cblGroup.DataValueField = "UserGroupId";
                    cblGroup.DataBind();
                    int userId = Convert.ToInt32(ViewState["Id"]);
                    UserGroupWithPermissionCheck[] userGroupsOfUser = UserGroupProcess.GetUserGroupsWhichContainExistUser(SiteId, CurrentUserOrOperator.UserOrOperatorId, userId);
                    foreach(ListItem item in cblGroup.Items)
                    {
                        if (IfContainGroup(userGroupsOfUser, Convert.ToInt32(item.Value)))
                            item.Selected = true;
                    }
                }
            }
            else
            {
                panelUserGroup.Visible = false;
            }
        }
        private bool IfContainGroup(UserGroupWithPermissionCheck[] userGroups, int userGroupId)
        {
            foreach (UserGroupWithPermissionCheck userGroup in userGroups)
            {
                if (userGroup.UserGroupId == userGroupId) return true;
            }
            return false;
        }
        #endregion Private Function BindUserGroups

        #region Private Function InitTextMaxLength
        private void InitTextMaxLength()
        {
            this.txtEmail.MaxLength = ForumDBFieldLength.User_emailFieldLength;
            this.txtDisplayName.MaxLength = ForumDBFieldLength.User_nameFieldLength;
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

        //Have Been Removed
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

        #region Private Function BindUser
        private void BindUser()
        {
            UserWithPermissionCheck user = UserProcess.GetNotDeletedUserById(SiteId, CurrentUserOrOperator.UserOrOperatorId, Convert.ToInt32(ViewState["Id"]));
            #region Init User Info
            txtEmail.Text = user.Email;
            chkIfShowEmail.Checked = user.IfShowEmail;
            txtDisplayName.Text = user.DisplayName;
            chkIfShowUserName.Checked = user.IfShowUserName;
            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            chkIfShowUserName.Checked = user.IfShowUserName;
            txtAge.Text = user.Age.ToString();
            chkIfShowAge.Checked = user.IfShowAge;
            ddlGender.SelectedIndex = Convert.ToInt16(user.Gender);
            chkIfShowGender.Checked = user.IfShowGender;
            txtOccupation.Text = user.Occupation;
            chkIfShowOccupation.Checked = user.IfShowOccupation;
            txtCompany.Text = user.Company;
            chkIfShowCompany.Checked = user.IfShowCompany;
            txtPhoneNumber.Text = user.PhoneNumber;
            chkIfShowPhoneNumber.Checked = user.IfShowPhoneNumber;
            txtFaxNumber.Text = user.FaxNumber;
            chkIfShowFaxNumber.Checked = user.IfShowFaxNumber;
            txtInterests.Text = user.Interests;
            chkIfShowInterests.Checked = user.IfShowInterests;
            txtHomePage.Text = user.HomePage;
            chkIfShowHomePage.Checked = user.IfShowHomePage;
            txtScore.Text = user.Score.ToString();
            txtReputation.Text = user.Reputation.ToString();
            #endregion
        }
        #endregion Private Function BindUser

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region Init Parameters
                int userId = Convert.ToInt32(ViewState["Id"]);
                string email = txtEmail.Text;
                string displayName = txtDisplayName.Text;
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                int age = Convert.ToInt32(txtAge.Text.Trim());
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
                int score = Convert.ToInt32(txtScore.Text.Trim());
                int reputation = Convert.ToInt32(txtReputation.Text.Trim());
                List<int> userGroupIds = new List<int>();
                for (int i = 0; i < cblGroup.Items.Count; i++)
                {
                    if (cblGroup.Items[i].Selected) userGroupIds.Add(Convert.ToInt32(cblGroup.Items[i].Value));
                }
                #endregion Init Parameters

                UserProcess.UpdateUser(SiteId, CurrentUserOrOperator.UserOrOperatorId, userId, email, displayName, firstName, lastName, age, gender, company,
                    occupation, phone, fax, interests, homepage, ifShowEmail, ifShowUserName, ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany, ifShowPhone,
                    ifShowFax, ifShowInterests, ifShowHomePage, userGroupIds, score, reputation);

                lblSuccess.Text = Proxy[EnumText.enumForum_User_PageEditUserSuccessEdit];
                Response.Redirect("UserList.aspx?pageindex=" + ViewState["pageindex"].ToString() + "&pagesize=" + ViewState["pagesize"].ToString() + "&SiteId=" + SiteId, false);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_User_PageEditUserErrorEdit] + exp.Message;
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
