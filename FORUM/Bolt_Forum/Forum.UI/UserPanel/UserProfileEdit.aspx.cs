
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
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Framework.FieldLength;
using System.Web.Util;
using Com.Comm100.Forum.UI.UserControl;
using System.Collections;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.UserPanel
{
    public partial class UserProfileEdit : UserBasePage
    {
        private Dictionary<string, string> Genders;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IfError == true) return;
            try
            {
                SiteSetting tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, this.UserOrOperatorId);
                Master.Page.Title = String.Format(Proxy[EnumText.enumForum_Public_UserPanelBrowserTitle], System.Web.HttpUtility.HtmlEncode(tmpSiteSetting.SiteName.ToString()));
                this.InitGenders();

                if (!IsPostBack)
                {
                    ((UserMasterPage)Master).SetMenu(EnumUserMenuType.Profile);
                    this.PageInit();
                }
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_User_PageEditProfileErrorLoading] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }
        }

        protected void btnSave1_Click(object sender, EventArgs e)
        {
            if (this.IfError == true) return;
            try
            {
                int siteId = SiteId;
                int OperatorId = this.UserOrOperatorId;
                bool ifOperator = this.IfOperator;

                Comm100.Framework.Enum.EnumGender Gender;
                string strGender = this.ddlGender.SelectedItem.Text;
                #region Gender selected
                if (strGender == this.Genders["Female"])
                {
                    Gender = Com.Comm100.Framework.Enum.EnumGender.Female;
                }
                else if (strGender == this.Genders["Male"])
                {
                    Gender = Com.Comm100.Framework.Enum.EnumGender.Male;
                }
                else
                {
                    Gender = Com.Comm100.Framework.Enum.EnumGender.ItsaSecret;
                }
                #endregion
                int nAge;
                if (this.txtAge.Text.Trim() != "")
                    nAge = int.Parse(this.txtAge.Text.Trim());
                else
                    nAge = 0;//default is 0;

                UserControlProcess.UpdateUserOrOperatorProfile(siteId, OperatorId, ifOperator,
                                        this.txtEmail.Text, this.txtDisplayName.Text,
                                        this.txtFirstName.Text, this.txtLastName.Text,
                                        nAge, Gender, this.txtCompany.Text, this.txtOccupation.Text,
                                        this.txtPhoneNumber.Text, this.txtFaxNumber.Text,
                                        this.txtInterests.Text, this.txtHomePage.Text,
                                        this.txtScore.Text,this.txtReputation.Text,
                                        this.chkIfShowEmail.Checked, this.chkIfShowUserName.Checked,
                                        this.chkIfShowAge.Checked, this.chkIfShowGender.Checked,
                                        this.chkIfShowOccupation.Checked, this.chkIfShowCompany.Checked,
                                        this.chkIfShowPhoneNumber.Checked, this.chkIfShowFaxNumber.Checked,
                                        this.chkIfShowInterests.Checked, this.chkIfShowHomePage.Checked);

                this.lblSuccess.Text = Proxy[EnumText.enumForum_User_PageEditProfileSuccessSave];

                this.PageInit();
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_User_PageEditProfileErrorSave] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;

                string script = string.Format("<script>alert(\"{0}\")</script>", this.lblError.Text);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
            }
        }

        private void PageInit()
        {
            #region TextBoxs's maxLength
            this.txtEmail.MaxLength = ForumDBFieldLength.User_emailFieldLength;
            this.txtDisplayName.MaxLength = ForumDBFieldLength.User_nameFieldLength;
            this.txtFirstName.MaxLength = ForumDBFieldLength.User_firstNameFieldLength;
            this.txtLastName.MaxLength = ForumDBFieldLength.User_lastNameFieldLength;
            this.txtAge.MaxLength = 3; //here 3 should defined in public
            this.txtOccupation.MaxLength = ForumDBFieldLength.User_occupationFieldLength;
            this.txtCompany.MaxLength = ForumDBFieldLength.User_companyFieldLength;
            this.txtPhoneNumber.MaxLength = ForumDBFieldLength.User_phoneNumberFieldLength;
            this.txtFaxNumber.MaxLength = ForumDBFieldLength.User_faxNumberFieldLength;
            this.txtInterests.MaxLength = ForumDBFieldLength.User_interestsFieldLength;
            this.txtHomePage.MaxLength = ForumDBFieldLength.User_homePageFieldLength;
            #endregion
            int siteId = SiteId;
            int OperatorId = this.UserOrOperatorId;
            bool ifOperator = this.IfOperator;
            bool ifAdmin = this.IfAdmin();
            UserOrOperator user = UserProcess.GetNotDeletedUserOrOperatorById(siteId, OperatorId);
            /* operator can't modify DiplayName and Email */
       
            if (ifAdmin == true)
            {
                txtScore.Enabled=true;
                txtReputation.Enabled = true;
            }

           

            #region score&reputation display 
            if (IfScoreEnable() == false)
            {
                this.Score.Visible = false;
            }
            else
            {
                this.Score.Visible = true;
            }
            if (ifReputationEnable() == false)
            {
                this.Reputation.Visible = false;
            }
            else
            {
                this.Reputation.Visible = true;
            }
            #endregion
            this.txtDisplayName.MaxLength = ForumDBFieldLength.User_nameFIeldLengthInDatabase;
            txtDisplayName.Enabled = false;
            txtEmail.Enabled = false;

            //forbid client script
            this.ValidEmailExpression.EnableClientScript = false;
            this.ValidEmailExpression.Visible = false;


            #region Data Filled
            txtDisplayName.Text = user.DisplayName;
            txtEmail.Text = user.Email;
            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            txtAge.Text = user.Age.ToString();
            /* load Gender*/
            ddlGender.Items.Clear();
            ddlGender.Items.Add(this.Genders["Female"]);
            ddlGender.Items.Add(this.Genders["Male"]);
            ddlGender.Items.Add(this.Genders["Itsasecret"]);

            if (user.Gender == Com.Comm100.Framework.Enum.EnumGender.Female)
                ddlGender.SelectedValue = this.Genders["Female"];
            else if (user.Gender == Com.Comm100.Framework.Enum.EnumGender.Male)
                ddlGender.SelectedValue = this.Genders["Male"];
            else
                ddlGender.SelectedValue = this.Genders["Itsasecret"];
            txtOccupation.Text = user.Occupation;
            txtCompany.Text = user.Company;
            txtPhoneNumber.Text = user.PhoneNumber;
            txtFaxNumber.Text = user.FaxNumber;
            txtInterests.Text = user.Interests;
            txtHomePage.Text = user.HomePage;
            txtScore.Text = user.Score.ToString();
            txtReputation.Text = user.Reputation.ToString();

            chkIfShowEmail.Checked = user.IfShowEmail;
            chkIfShowUserName.Checked = user.IfShowUserName;
            chkIfShowAge.Checked = user.IfShowAge;
            chkIfShowGender.Checked = user.IfShowGender;
            chkIfShowOccupation.Checked = user.IfShowOccupation;
            chkIfShowCompany.Checked = user.IfShowCompany;
            chkIfShowPhoneNumber.Checked = user.IfShowPhoneNumber;
            chkIfShowFaxNumber.Checked = user.IfShowFaxNumber;
            chkIfShowInterests.Checked = user.IfShowInterests;
            chkIfShowHomePage.Checked = user.IfShowHomePage;
            
            #endregion
        }
        protected override void InitLanguage()
        {
            try
            {
                #region Text Load
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
                this.btnSave1.Text = Proxy[EnumText.enumForum_User_ButtonSave];

                this.VaildAgeRange.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorAge];
                this.ValidDisplayNameRequired.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorDisplayNameRequired];
                this.ValidEmailExpression.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorEmailFormat];
                this.ValidEmailRequired.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorEmailRequired];
                this.ValidScoreRequired.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorOfScoreRequired];
                this.ValidScoreRange.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorOfScoreRange];
                this.ValidReputationRequired.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorOfReputationRequired];
                this.ValidReputationRange.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorOfReputationRange];
                #endregion 
            }
            catch (Exception ex)
            {
                this.lblError.Text = ex.Message;

            }
        }
        private void InitGenders()
        {
            this.Genders = new Dictionary<string, string>();
            this.Genders.Add("Female", Proxy[EnumText.enumForum_User_FieldFemale]);
            this.Genders.Add("Male", Proxy[EnumText.enumForum_User_FieldMale]);
            this.Genders.Add("Itsasecret", Proxy[EnumText.enumForum_User_FieldItsasecret]);
        }
        private bool IfScoreEnable()
        {
            bool ifOpenScoreFeature;
            ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId);
            ifOpenScoreFeature = forumFeature.IfEnableScore;
            return ifOpenScoreFeature;
        }
        private bool ifReputationEnable()
        {
            bool ifOpenReputationFeature;
            ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId);
            ifOpenReputationFeature = forumFeature.IfEnableReputation;
            return ifOpenReputationFeature;
        }
    }
}
