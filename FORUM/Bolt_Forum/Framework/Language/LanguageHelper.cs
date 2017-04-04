#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Language;

namespace Com.Comm100.Language
{
    public class LanguageHelper
    {
        #region property
        public static Language[] LanguageList = new Language[14];
        #endregion

        public static void LoadLanguage()
        {

            int maxTextId = (int)EnumText.enumComm100System_Ended + 1;

            /*English*/
            #region en-us

            Language en_us = new Language(EnumLanguage.enumEnglish, maxTextId);
            LanguageList[0] = en_us;

            #region Public
            en_us.AddText(EnumText.enumPublic_RequiredField, "Required Field");
            en_us.AddText(EnumText.enumPublic_ChangePassword, "Change Password");
            en_us.AddText(EnumText.enumPublic_Logout, "Logout");
            en_us.AddText(EnumText.enumPublic_CurrentOperator, "Current Operator:");
            en_us.AddText(EnumText.enumPublic_CurrentUser, "Current User:");
            en_us.AddText(EnumText.enumPublic_SiteId, "Site Id:");
            en_us.AddText(EnumText.enumPublic_PartnerId, "Partner Id:");
            en_us.AddText(EnumText.enumPublic_HelpAlt, "help");
            en_us.AddText(EnumText.enumPublic_GridPager_PageSize, "Page Size:");
            en_us.AddText(EnumText.enumPublic_GridPager_CurrentPageItems, "Current Page Items:");
            en_us.AddText(EnumText.enumPublic_GridPager_TotalItems, "Total Items:");
            en_us.AddText(EnumText.enumPublic_Copyright, "Copyright &copy;2010 <a href=\"http://www.comm100.com\" target=\"_blank\">Comm100</a>");
            en_us.AddText(EnumText.enumPublic_Comm100TagLine, "Open Source & Free Hosted Customer Service Software");
            en_us.AddText(EnumText.enumPublic_AdminPanelCopyright, "Copyright &copy;2010 <a href=\"http://www.comm100.com\" target=\"_blank\">Comm100</a>");
            en_us.AddText(EnumText.enumPublic_VistorPageCopyright, "Powered by <a href=\"http://www.comm100.com\" target=\"_blank\">Comm100</a>");
            en_us.AddText(EnumText.enumPublic_PoweredbyComm100, "Powered by Comm100");
            en_us.AddText(EnumText.enumPublic_Logo, "Logo");

            
            en_us.AddText(EnumText.enumPublic_Forum, "Forum");
            en_us.AddText(EnumText.enumPublic_Newsletter, "Newsletter");
            en_us.AddText(EnumText.enumPublic_KnowledgeBase, "Knowledge Base");
            en_us.AddText(EnumText.enumPublic_EmailTicket, "Email Ticket");
            en_us.AddText(EnumText.enumPublic_Partner, "Partner");

            en_us.AddText(EnumText.enumPublic_Ticket, "Ticket");
            en_us.AddText(EnumText.enumPublic_EmailCaseManagement, "Email Case Management");
            en_us.AddText(EnumText.enumPublic_Survey, "Survey");
            en_us.AddText(EnumText.enumPublic_Account, "Account");
            en_us.AddText(EnumText.enumPublic_DateFormat, "MM-dd-yyyy");
            en_us.AddText(EnumText.enumPublic_DateFormatWithHour, "MM-dd-yyyy HH:mm:ss");
            #endregion

            #region Forum

            #region Forum Public

            en_us.AddText(EnumText.enumForum_Public_DeletedUser, "deleted user");
            en_us.AddText(EnumText.enumForum_Public_Help, "Help");
            en_us.AddText(EnumText.enumForum_Public_No, "No");
            en_us.AddText(EnumText.enumForum_Public_RequiredField, "Required Field");
            en_us.AddText(EnumText.enumForum_Public_Yes, "yes");
            en_us.AddText(EnumText.enumForum_Public_TextAreaMaxLength, ",'The text that you have entered is too long ('+{0}+' characters). Please shorten it to {1} characters long.');");
            en_us.AddText(EnumText.enumForum_Public_UserPanelBrowserTitle, "User Control Panel - {0}");
            en_us.AddText(EnumText.enumForum_Public_CommonBrowerTitle, "{0}");
            en_us.AddText(EnumText.enumForum_Public_TextAbout, "About");
            en_us.AddText(EnumText.enumForum_Public_TextStatistics, "Statistics");
            en_us.AddText(EnumText.enumForum_Public_TextCurrentLocation, "(current location)");
            en_us.AddText(EnumText.enumForum_Public_AlertNoPermission, "You do not have the permission!");
            en_us.AddText(EnumText.enumForum_Public_TextTop, "Top");
            en_us.AddText(EnumText.enumForum_Public_TextSharp, "#");
            en_us.AddText(EnumText.enumForum_Public_TextPost, "Post");
            en_us.AddText(EnumText.enumForum_Public_LinkCloseSelectForumWindow, "[ Close ]");
            en_us.AddText(EnumText.enumForum_Public_TextLEGEND, "LEGEND");
            en_us.AddText(EnumText.enumForum_Public_TextRe, "Re:");
            en_us.AddText(EnumText.enumForum_Public_TextAt, "At");
            en_us.AddText(EnumText.enumForum_Public_Post, "Post");
            en_us.AddText(EnumText.enumForum_Public_Posts, "Posts");
            en_us.AddText(EnumText.enumForum_Public_Topic, "Topic");
            en_us.AddText(EnumText.enumForum_Public_Topics, "Topics");



            #endregion

            #region Forum Operator

            en_us.AddText(EnumText.enumForum_Operator_ConfirmAreYouSureDelete, "Are you sure to delete this operator?");
            en_us.AddText(EnumText.enumForum_Operator_ButtonCancel, "Cancel");
            en_us.AddText(EnumText.enumForum_Operator_ButtonNew, "New Operator");
            en_us.AddText(EnumText.enumForum_Operator_ButtonSave, "Save");
            en_us.AddText(EnumText.enumForum_Operator_ColumnDelete, "Delete");
            en_us.AddText(EnumText.enumForum_Operator_ColumnDescription, "Description");
            en_us.AddText(EnumText.enumForum_Operator_ColumnDisplayName, "Display Name");
            en_us.AddText(EnumText.enumForum_Operator_ColumnEdit, "Edit");
            en_us.AddText(EnumText.enumForum_Operator_ColumnEmail, "Email");
            en_us.AddText(EnumText.enumForum_Operator_ColumnId, "Id");
            en_us.AddText(EnumText.enumForum_Operator_ColumnIsActive, "Is Active");
            en_us.AddText(EnumText.enumForum_Operator_ColumnIsAdmin, "Is Admin");
            en_us.AddText(EnumText.enumForum_Operator_ColumnResetPassword, "Reset Password");
            en_us.AddText(EnumText.enumForum_Operator_ErrorEmailInvalid, "Email format is invalid");
            en_us.AddText(EnumText.enumForum_Operator_ErrorEmailRequired, "Email is required");
            en_us.AddText(EnumText.enumForum_Operator_ErrorEmailsMatch, "Emails don't match");
            en_us.AddText(EnumText.enumForum_Operator_ErrorFirstNameRequired, "First name is required");
            en_us.AddText(EnumText.enumForum_Operator_ErrorLastNameRequired, "Last name is required");
            en_us.AddText(EnumText.enumForum_Operator_ErrorNameRequired, "Display name is required");
            en_us.AddText(EnumText.enumForum_Operator_ErrorPasswordRequired, "Password is required");
            en_us.AddText(EnumText.enumForum_Operator_ErrorPasswordsMatch, "Passwords don't match");
            en_us.AddText(EnumText.enumForum_Operator_ErrorRetypeEmailRequired, "Retype email is required");
            en_us.AddText(EnumText.enumForum_Operator_ErrorRetypePasswordRequired, "Retpye password is required");
            en_us.AddText(EnumText.enumForum_Operator_FieldDescription, "Description:");
            en_us.AddText(EnumText.enumForum_Operator_FieldDisplayName, "Display Name:");
            en_us.AddText(EnumText.enumForum_Operator_FieldEmail, "Email:");
            en_us.AddText(EnumText.enumForum_Operator_FieldFirstName, "First Name:");
            en_us.AddText(EnumText.enumForum_Operator_FieldIsActive, "Is Active");
            en_us.AddText(EnumText.enumForum_Operator_FieldIsAdministrator, "Is Administrator");
            en_us.AddText(EnumText.enumForum_Operator_FieldLastName, "Last Name:");
            en_us.AddText(EnumText.enumForum_Operator_FieldNewPassword, "New Password:");
            en_us.AddText(EnumText.enumForum_Operator_FieldPassword, "Password:");
            en_us.AddText(EnumText.enumForum_Operator_FieldRetypeEmail, "Retype Email:");
            en_us.AddText(EnumText.enumForum_Operator_FieldRetypePassword, "Retype Password:");
            en_us.AddText(EnumText.enumForum_Operator_HelpDisplayName, "A display name can be used for an operator to identify himself or herself. Display names must be unique.");
            en_us.AddText(EnumText.enumForum_Operator_HelpEmail, "Email is used for an operator to log in. Email address must be unique.");
            en_us.AddText(EnumText.enumForum_Operator_HelpIsActive, "An operator cannot log in when he/she is not active. An inactive operator can be re-activated by an administrator.");
            en_us.AddText(EnumText.enumForum_Operator_HelpIsAdmin, "An administrator has full permission to manage your site. A site must have at least one active administrator.");
            en_us.AddText(EnumText.enumForum_Operator_PageEditErrorLoadingPage, "Error loading  edit operator page:");
            en_us.AddText(EnumText.enumForum_Operator_PageEditErrorRedirectingToOperatorsPage, "Error redirecting to Operators page:");
            en_us.AddText(EnumText.enumForum_Operator_PageEditErrorUpdatingOperator, "Error updating an operator:");
            en_us.AddText(EnumText.enumForum_Operator_PageListErrorDeletingOperator, "Error deleting an operator:");
            en_us.AddText(EnumText.enumForum_Operator_PageListErrorLoadingPage, "Error loading Operators page: ");
            en_us.AddText(EnumText.enumForum_Operator_PageListErrorRedirectingToNewOperatorPage, "Error redirecting to New Operator page:");
            en_us.AddText(EnumText.enumForum_Operator_PageNewErrorCreatingOperator, "Error creating a new operator:");
            en_us.AddText(EnumText.enumForum_Operator_PageNewErrorLoadingPage, "Error loading  New Operator page:");
            en_us.AddText(EnumText.enumForum_Operator_PageNewErrorRedirectingToOperatorsPage, "Error redirecting to Operators page:");
            en_us.AddText(EnumText.enumForum_Operator_PageResetPasswordErrorLoadingPage, "Error loading Reset Password page:");
            en_us.AddText(EnumText.enumForum_Operator_PageResetPasswordErrorRedirectingPage, "Error redirecting to Operators page:");
            en_us.AddText(EnumText.enumForum_Operator_PageResetPasswordErrorResettingPassword, "Error resetting password");
            en_us.AddText(EnumText.enumForum_Operator_SubtitleEditPage, "Only an administrator has the permission to edit an operator's information.");
            en_us.AddText(EnumText.enumForum_Operator_SubtitleListPage, "An operator is an internal user, often a customer service representative. An operator has permission to view all foreground and background info, and has full permission to manage draft. ");
            en_us.AddText(EnumText.enumForum_Operator_SubtitleNewPage, "Add a new operator. Only an administrator has the permission to add a new operator.");
            en_us.AddText(EnumText.enumForum_Operator_SubtitleResetPasswordPage, "Reset an operator's password. An administrator can set a new password for an operator without knowing the operator's current password.");
            en_us.AddText(EnumText.enumForum_Operator_TitleEditPage, "Edit Operator");
            en_us.AddText(EnumText.enumForum_Operator_TitleListPage, "Operators");
            en_us.AddText(EnumText.enumForum_Operator_TitleNewPage, "New Operator");
            en_us.AddText(EnumText.enumForum_Operator_TitleResetPasswordPage, "Reset Password");

            #endregion

            #region Forum AdminMenu

            en_us.AddText(EnumText.enumForum_AdminMenu_Categories, "Categories");
            en_us.AddText(EnumText.enumForum_AdminMenu_CategoriesForums, "Categories & Forums");
            en_us.AddText(EnumText.enumForum_AdminMenu_CodePlan, "Code Generation");
            en_us.AddText(EnumText.enumForum_AdminMenu_Dashboard, "Dashboard");
            en_us.AddText(EnumText.enumForum_AdminMenu_Drafts, "Drafts");
            en_us.AddText(EnumText.enumForum_AdminMenu_ForumHome, "Forum Home");
            en_us.AddText(EnumText.enumForum_AdminMenu_Forums, "Forums");
            en_us.AddText(EnumText.enumForum_AdminMenu_HeaderFooterSettings, "Header & Footer Settings");
            en_us.AddText(EnumText.enumForum_AdminMenu_Operators, "Operators");
            en_us.AddText(EnumText.enumForum_AdminMenu_RegistrationSettings, "Registration Settings");
            en_us.AddText(EnumText.enumForum_AdminMenu_Settings, "Settings");
            en_us.AddText(EnumText.enumForum_AdminMenu_SiteSettings, "Site Settings");
            en_us.AddText(EnumText.enumForum_AdminMenu_Styles, "Styles");
            en_us.AddText(EnumText.enumForum_AdminMenu_TemplateSettings, "Template Settings");
            en_us.AddText(EnumText.enumForum_AdminMenu_UsersManagement, "Users Management");
            en_us.AddText(EnumText.enumForum_AdminMenu_UsersModeration, "Users Moderation");
            en_us.AddText(EnumText.enumForum_AdminMenu_Users, "Users");
            en_us.AddText(EnumText.enumForum_AdminMenu_RulesAndPoliciesSettings, "Policy Settings");
            #endregion

            #region Forum UserMenu

            en_us.AddText(EnumText.enumForum_UserMenu_Avatar, "Avatar");
            en_us.AddText(EnumText.enumForum_UserMenu_MyPosts, "My Posts");
            en_us.AddText(EnumText.enumForum_UserMenu_MyTopics, "My Topics");
            en_us.AddText(EnumText.enumForum_UserMenu_Password, "Password");
            en_us.AddText(EnumText.enumForum_UserMenu_Profile, "Profile");
            en_us.AddText(EnumText.enumForum_UserMenu_Signature, "Signature");
            en_us.AddText(EnumText.enumForum_UserMenu_UserPanel, "User Panel");

            #endregion

            #region Forum Header & Footer & Other

            en_us.AddText(EnumText.enumForum_HeaderFooter_AdvancedSearch, "Advanced Search");
            en_us.AddText(EnumText.enumForum_HeaderFooter_Copyright, "{0} Powered by <a href=\"http://www.comm100.com\" target=\"_blank\">Comm100</a> - Open Source & Free Hosted Customer Service Software");
            en_us.AddText(EnumText.enumForum_HeaderFooter_FooterErrorLoad, "Error loading the forum footer: ");
            en_us.AddText(EnumText.enumForum_HeaderFooter_ForumJump, "Forum Jump: ");
            en_us.AddText(EnumText.enumForum_HeaderFooter_HeaderErrorLoading, "Error loading the forum header: ");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkAdminControlPanel, "Admin Control Panel");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkBarErrorLoading, "Error loading the link bar: ");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkHome, "Home");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkLogin, "Login");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkLogout, "Logout[{0}]");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkRegister, "Register");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkSearch, "Search");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkUserControlPanel, "My Post");
            en_us.AddText(EnumText.enumForum_HeaderFooter_SearchText, "Search...");
            en_us.AddText(EnumText.enumForum_HeaderFooter_SelectForum, "Select a forum");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LogoErrorFind, "Error updating the logo:this logo does not exist.");
            en_us.AddText(EnumText.enumForum_HeaderFooter_SearchFromAll, "All");

            #endregion

            #region Forum NavigationBar

            en_us.AddText(EnumText.enumForum_NavigationBar_AddTopic, "New Topic");
            en_us.AddText(EnumText.enumForum_NavigationBar_AdminPanelLogin, "Admin Login");
            en_us.AddText(EnumText.enumForum_NavigationBar_AdvancedSearch, "Advanced Search");
            en_us.AddText(EnumText.enumForum_NavigationBar_Default, "Home");
            en_us.AddText(EnumText.enumForum_NavigationBar_EditTopicOrPost, "EditTopicOrPost");
            en_us.AddText(EnumText.enumForum_NavigationBar_EmailVerification, "Email Verification");
            en_us.AddText(EnumText.enumForum_NavigationBar_FindPassword, "Find Password");
            en_us.AddText(EnumText.enumForum_NavigationBar_Forum, "Forum");
            en_us.AddText(EnumText.enumForum_NavigationBar_Login, "Login");
            en_us.AddText(EnumText.enumForum_NavigationBar_Register, "Register");
            en_us.AddText(EnumText.enumForum_NavigationBar_ResetPassword, "Reset Password");
            en_us.AddText(EnumText.enumForum_NavigationBar_SearchResult, "Search Result");
            en_us.AddText(EnumText.enumForum_NavigationBar_SendResetPasswordEmail, "Send Reset Password Email");
            en_us.AddText(EnumText.enumForum_NavigationBar_SiteClosed, "Site Closed");
            en_us.AddText(EnumText.enumForum_NavigationBar_UserPanel, "User Panel");
            en_us.AddText(EnumText.enumForum_NavigationBar_UserProfile, "User Profile");

            #endregion

            #region Forum Site & Settings

            en_us.AddText(EnumText.enumForum_Settings_ButtonCopyCode, "Copy Code");
            en_us.AddText(EnumText.enumForum_Settings_ButtonSave, "Save");
            en_us.AddText(EnumText.enumForum_Settings_ErrorReason, "close reason is required");
            en_us.AddText(EnumText.enumForum_Settings_ErrorSiteName, "site name is required");
            en_us.AddText(EnumText.enumForum_Settings_FieldRadioSite, "Open/Close Site:");
            en_us.AddText(EnumText.enumForum_Settings_FieldRadioSiteClose, "Close");
            en_us.AddText(EnumText.enumForum_Settings_FieldRadioSiteOpen, "Open");
            en_us.AddText(EnumText.enumForum_Settings_FieldRegistrationEmailVerify, "Email Address Verification Required for Registration");
            en_us.AddText(EnumText.enumForum_Settings_FieldRegistrationModerate, "Moderation Required for New Registered Users");
            en_us.AddText(EnumText.enumForum_Settings_FieldSiteCloseReason, "Close Reason: ");
            en_us.AddText(EnumText.enumForum_Settings_FieldSiteName, "Site Name:");
            en_us.AddText(EnumText.enumForum_Settings_FieldUserRegistrationOption, "User Registration Option:");
            en_us.AddText(EnumText.enumForum_Settings_FieldWebLink, "Forum Code:");
            en_us.AddText(EnumText.enumForum_Settings_HelpRegistrationEmailVerify, "With this option selected, a verification Email will be sent to the user\'s mailbox. The user can click the link enclosed in this mail to finish the registration.");
            en_us.AddText(EnumText.enumForum_Settings_HelpRegistrationModrate, "With this option selected, an administrator\'s approval is required to finish a user\'s registration.");
            en_us.AddText(EnumText.enumForum_Settings_PageCodePlanErrorGetSiteSetting, "Error getting Site Settings: ");
            en_us.AddText(EnumText.enumForum_Settings_PageCodePlanErrorLoad, "Error loading Site Settings page: ");
            en_us.AddText(EnumText.enumForum_Settings_PageRegistrationSettingsErrorGetRegistrationSetting, "Error getting registration settings:");
            en_us.AddText(EnumText.enumForum_Settings_PageRegistrationSettingsErrorLoad, "Error loading Registration Settings page: ");
            en_us.AddText(EnumText.enumForum_Settings_PageRegistrationSettingsErrorSave, "Error saving registration settings: ");
            en_us.AddText(EnumText.enumForum_Settings_PageRegistrationSettingsSuccessfullySaved, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_Settings_PageSiteSettingsErrorCloseSite, "Error closing the site:");
            en_us.AddText(EnumText.enumForum_Settings_PageSiteSettingsGuestsCannotVisit, "Guests are not allowed to visit the forum.");
            en_us.AddText(EnumText.enumForum_Settings_PageSiteSettingsErrorGetSiteSetting, " Error getting site settings: ");
            en_us.AddText(EnumText.enumForum_Settings_PageSiteSettingsErrorLoad, "Error loading Site Settings page: ");
            en_us.AddText(EnumText.enumForum_Settings_PageSiteSettingsErrorOpenSite, "Error opening the site: ");
            en_us.AddText(EnumText.enumForum_Settings_PageSiteSettingsSuccessSiteClose, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_Settings_PageSiteSettingsSuccessSiteOpen, "Site opened successfully.");
            en_us.AddText(EnumText.enumForum_Settings_SubtitleCodePlan, "Forum code is a URL of Comm100 Forum. You can copy the forum code below and paste it into your web page so your web site  can have Comm100 Forum.");
            en_us.AddText(EnumText.enumForum_Settings_SubtitleRegistrationSettings, "Registration is the process of enrolling for the forum site. In this page, you can customize the verification mode of your forum users' registration.");
            en_us.AddText(EnumText.enumForum_Settings_SubtitleSiteSettings, "Site is the whole forum site. In this page, you can turn the forum site on or off. If  you close the site, your forum users cannot get access to the forum site anymore.");
            en_us.AddText(EnumText.enumForum_Settings_TitleCodePlan, "Code Generation");
            en_us.AddText(EnumText.enumForum_Settings_TitleRegistrationSettings, "Registration Settings");
            en_us.AddText(EnumText.enumForum_Settings_TitleSiteSettings, "Site Options");

            en_us.AddText(EnumText.enumForum_Settings_FieldClosedInfo, "is currently closed.");
            en_us.AddText(EnumText.enumForum_Settings_FieldReason, "Reason: ");
            en_us.AddText(EnumText.enumForum_Settings_LinkAdminLogin, "Admin Login");

            en_us.AddText(EnumText.enumForum_Settings_TitleRegistrationRulesSettings, "Policy Settings");
            en_us.AddText(EnumText.enumForum_Settings_SubTitleRegistrationRuleSettings, "Policy is used to descipline your forum users' actions.");
            en_us.AddText(EnumText.enumForum_Settings_PageRegistrationRuleSettingsSuccessfullySaved, "Registration rules set successfully");
            en_us.AddText(EnumText.enumForum_Settings_PageRegistrationRuleSettingsErrorSave, "Error updating registration rules settings:");
            en_us.AddText(EnumText.enumForum_Settings_PageRegistrationRuleSettingsErrorLoad, "Error loading Registration Rules page:");



            #endregion

            #region Forum Styles
            en_us.AddText(EnumText.enumForum_Styles_ButtonCancel, "Cancel");
            en_us.AddText(EnumText.enumForum_Styles_ButtonEditLogo, "Edit logo in the Site Profile page");
            en_us.AddText(EnumText.enumForum_Styles_ButtonPrivew, "Preview");
            en_us.AddText(EnumText.enumForum_Styles_ButtonSave, "Save");
            en_us.AddText(EnumText.enumForum_Styles_ButtonUpload, "Upload");
            en_us.AddText(EnumText.enumForum_Styles_FieldAdvancedModeRadio, "Advanced Mode");
            en_us.AddText(EnumText.enumForum_Styles_FieldEasyModeRadio, "Easy Mode");
            en_us.AddText(EnumText.enumForum_Styles_FieldSelectType, "Select Type:");
            en_us.AddText(EnumText.enumForum_Styles_FieldUploadDescription, "(Upload your logo here. Your logo file should be in GIF, JPG, JPEG, PNG, or BMP format. The size of your logo file should be less than 100K)");
            en_us.AddText(EnumText.enumForum_Styles_LabelHeaderFooter, "Header&Footer");
            en_us.AddText(EnumText.enumForum_Styles_LabelLogo, "Logo");
            en_us.AddText(EnumText.enumForum_Styles_LabelPageFooter, "Home Page Footer");
            en_us.AddText(EnumText.enumForum_Styles_LabelPageHeader, "Home Page Header");
            en_us.AddText(EnumText.enumForum_Styles_NoteEditLogo, "Note: Your forum and your site share a common logo.");
            en_us.AddText(EnumText.enumForum_Styles_PageHeaderFooterSettingErrorLoad, "Error loading Header & Footer Settings page:");
            en_us.AddText(EnumText.enumForum_Styles_PageHeaderFooterSettingErrorPreview, "Error previewing:");
            en_us.AddText(EnumText.enumForum_Styles_PageHeaderFooterSettingErrorSave, "Error updating header & footer settings: ");
            en_us.AddText(EnumText.enumForum_Styles_PageHeaderFooterSettingSuccessSave, "Header and Footer settings updated successfully");
            en_us.AddText(EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorLoadingPage, "Error loading Custom Logo page:");
            en_us.AddText(EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorRedirectingPage, "Error redirecting to Site Profile page:");
            en_us.AddText(EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorUploadingExceedMaxFileLength, "Error uploading logo file: the size of your logo file should be less than 100K.");
            en_us.AddText(EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorUploadingIncorrectFormatFile, "Error uploading logo file: your logo file should be in GIF, JPG, JPEG, PNG, or BMP format.");
            en_us.AddText(EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorUploadingNoSelectFile, "Error uploading logo file: please select a logo file to upload.");
            en_us.AddText(EnumText.enumForum_Styles_PageTemplateSettingErrorDataBind, "Error getting templates:");
            en_us.AddText(EnumText.enumForum_Styles_PageTemplateSettingErrorGetTemplateId, "Error saving template settings:");
            en_us.AddText(EnumText.enumForum_Styles_PageTemplateSettingErrorPageLoad, "Error loading Template Settings page ");
            en_us.AddText(EnumText.enumForum_Styles_PageTemplateSettingErrorSetCurrentTemplate, "Error setting a template:");
            en_us.AddText(EnumText.enumForum_Styles_PageTemplateSettingSuccessMessage, "Template set successfully");
            en_us.AddText(EnumText.enumForum_Styles_SubtitleHeaderFooterSetting, "Customize your own logo, and page headers and footers according to your need.Easy Mode is a simple custom mode that allows you only to customize your own logo in the header. You can upload your own logo. Or you can just take the default logo.Advanced Mode is a full custom mode that allows you to customize your forum header and footer. And you can write your own header and footer using HTML.");
            en_us.AddText(EnumText.enumForum_Styles_SubtitleTemplateSetting, "Template is a collection of settings that defines your forum page styles . You can choose one preferred template that best fits your site feel and look.");
            en_us.AddText(EnumText.enumForum_Styles_TitleHeaderFooterSetting, "Header & Footer Settings");
            en_us.AddText(EnumText.enumForum_Styles_TitleSiteCustomizeLogo, "Customize Logo");
            en_us.AddText(EnumText.enumForum_Styles_TitleTemplateSetting, "Template Settings");

            #endregion

            #region Forum Categories

            en_us.AddText(EnumText.enumForum_Categories_TitleList, "Categories");
            en_us.AddText(EnumText.enumForum_Categories_SubtitleList, "Category helps organize forum discussions into different groups, making it easy for users to find specific topics.");
            en_us.AddText(EnumText.enumForum_Categories_ButtonNew, "New Category");
            en_us.AddText(EnumText.enumForum_Categories_ColumnId, "Id");
            en_us.AddText(EnumText.enumForum_Categories_ColumnName, "Name");
            en_us.AddText(EnumText.enumForum_Categories_ColumnDescription, "Description");
            en_us.AddText(EnumText.enumForum_Categories_ColumnOrder, "Order");
            en_us.AddText(EnumText.enumForum_Categories_ColumnOperation, "Operation");
            en_us.AddText(EnumText.enumForum_Categories_HelpEdit, "Edit");
            en_us.AddText(EnumText.enumForum_Categories_HelpDelete, "Delete");
            en_us.AddText(EnumText.enumForum_Categories_HelpUp, "Move up");
            en_us.AddText(EnumText.enumForum_Categories_HelpDown, "Move down");
            en_us.AddText(EnumText.enumForum_Categories_PageListErrorDeleteCategoryWithForum, "Please delete the forums belong to the category first!");
            en_us.AddText(EnumText.enumForum_Categories_PageListErrorDeleteCategory, "Error deleting a category:");
            en_us.AddText(EnumText.enumForum_Categories_PageListErrorEditCategory, "Error editing a category: ");
            en_us.AddText(EnumText.enumForum_Categories_PageListErrorOrderCategory, "Error ordering categories: ");
            en_us.AddText(EnumText.enumForum_Categories_PageListErrorGetCategory, "Error getting categories: ");
            en_us.AddText(EnumText.enumForum_Categories_PageListErrorLoadPage, "Error refreshing Categories page:");
            en_us.AddText(EnumText.enumForum_Categories_ComfirmDelete, "Are you sure to delete this category?");

            en_us.AddText(EnumText.enumForum_Categories_TitleAdd, "New Category");
            en_us.AddText(EnumText.enumForum_Categories_SubtitleAdd, "Add a new category.");
            en_us.AddText(EnumText.enumForum_Categories_ButtonSave, "Save");
            en_us.AddText(EnumText.enumForum_Categories_ButtonCancel, "Cancel");
            en_us.AddText(EnumText.enumForum_Categories_FieldName, "Name");
            en_us.AddText(EnumText.enumForum_Categories_FieldDescription, "Description");
            en_us.AddText(EnumText.enumForum_Categories_ErrorRequireField, "*Required Field");
            en_us.AddText(EnumText.enumForum_Categories_ErrorNameRequired, "Name is Required");
            en_us.AddText(EnumText.enumForum_Categories_PageErrorAddCreateNew, "Error creating a new category: ");

            en_us.AddText(EnumText.enumForum_Categories_TitleEdit, "Edit Category");
            en_us.AddText(EnumText.enumForum_Categories_SubtitleEdit, "Edit the selected category.");
            en_us.AddText(EnumText.enumForum_Categories_PageEditErrorLoadPage, "Error loading Edit Category page: ");
            en_us.AddText(EnumText.enumForum_Categories_PageEditErrorEdit, "Error editing a category: ");
            //en_us.AddText(EnumText. "");

            #endregion

            #region Forum Forums

            en_us.AddText(EnumText.enumForum_Forums_TitleList, "Forums");
            en_us.AddText(EnumText.enumForum_Forums_TitleSubtitleList, "Forum is the online discussion area for forum participants to ask questions or share ideas.");
            en_us.AddText(EnumText.enumForum_Forums_ButtonNew, "New Forum");
            en_us.AddText(EnumText.enumForum_Forums_ColumnId, "Id");
            en_us.AddText(EnumText.enumForum_Forums_ColumnName, "Name");
            en_us.AddText(EnumText.enumForum_Forums_ColumnDescription, "Description");
            en_us.AddText(EnumText.enumForum_Forums_ColumnStatus, "Status");
            en_us.AddText(EnumText.enumForum_Forums_ColumnModerator, "Moderator");
            en_us.AddText(EnumText.enumForum_Forums_ColumnOrder, "Order");
            en_us.AddText(EnumText.enumForum_Forums_ColumnOperation, "Operation");
            en_us.AddText(EnumText.enumForum_Forums_HelpUp, "Move up");
            en_us.AddText(EnumText.enumForum_Forums_HelpDown, "Move down");
            en_us.AddText(EnumText.enumForum_Forums_HelpEdit, "Edit");
            en_us.AddText(EnumText.enumForum_Forums_HelpDelete, "Delete");
            en_us.AddText(EnumText.enumForum_Forums_PageListErrorDelete, "Error deleting a forum: ");
            en_us.AddText(EnumText.enumForum_Forums_PageListErrorGetting, "Error loading Forums page:");
            en_us.AddText(EnumText.enumForum_Forums_PageListErrorSortDown, "Error sorting down a forum: ");
            en_us.AddText(EnumText.enumForum_Forums_PageListErrorSortUp, "Error sorting up a forum: ");
            en_us.AddText(EnumText.enumForum_Forums_PageListErrorLoadPage, "Error loading Forums page:");
            en_us.AddText(EnumText.enumForum_Forums_PageListErrorConfirmDelete, "Warning: All of the data will be deleted permanently.Are you sure to delete this forum?");

            en_us.AddText(EnumText.enumForum_Forums_TitleNew, "New Forum");
            en_us.AddText(EnumText.enumForum_Forums_SubtitleNew, "Add a new forum. when adding a new forum, you need to choose a category and appoint a moderator for this new forum.");
            en_us.AddText(EnumText.enumForum_Forums_FieldName, "Name:");
            en_us.AddText(EnumText.enumForum_Forums_FieldDescription, "Description:");
            en_us.AddText(EnumText.enumForum_Forums_FieldCategory, "Category:");
            en_us.AddText(EnumText.enumForum_Forums_Moderator, "Moderator:");
            en_us.AddText(EnumText.enumForum_Forums_ButtonSave, "Save");
            en_us.AddText(EnumText.enumForum_Forums_ButtonCancel, "Cancel");
            en_us.AddText(EnumText.enumForum_Forums_ErrorNameRequired, "Name is Required");
            en_us.AddText(EnumText.enumForum_Forums_ErrorCategoryRequired, "Category is required");
            en_us.AddText(EnumText.enumForum_Forums_PageNewErrorCreateNew, "Error creating a new forum: ");
            en_us.AddText(EnumText.enumForum_Forums_ErrorModeratorRequired, "Moderator is required");
            en_us.AddText(EnumText.enumForum_Forums_FieldRequireField, "*Required Field");

            en_us.AddText(EnumText.enumForum_Forums_TitleEdit, "Edit Forum");
            en_us.AddText(EnumText.enumForum_Forums_SubtitleEdit, "Edit the selected forum.");
            en_us.AddText(EnumText.enumForum_Forums_FieldStatus, "Status:");
            en_us.AddText(EnumText.enumForum_Forums_PageEditErrorLoadPage, "Error loading Edit Forum page: ");
            en_us.AddText(EnumText.enumForum_Forums_PageEditErroeUpdate, "Error updating a forum: ");

            en_us.AddText(EnumText.enumForum_Forums_ConfirmMoveTopic, "Are you sure to move this topic?");
            en_us.AddText(EnumText.enumForum_Forums_ButtonSelectedToMove, "Select");
            en_us.AddText(EnumText.enumForum_Forums_PageSelectForumErrorLoading, "Error selecting a forum: ");

            en_us.AddText(EnumText.enumForum_Forums_StatusOpen, "Open");
            en_us.AddText(EnumText.enumForum_Forums_StatusHide, "Hide");
            en_us.AddText(EnumText.enumForum_Forums_StatusLock, "Lock");

            #endregion

            #region Forum Topics & Posts & Search & Drafts
            en_us.AddText(EnumText.enumForum_Topic_FieldModerators, "Moderators:");
            en_us.AddText(EnumText.enumForum_Topic_PageForumErrorLoad, "Error loading Forums page:");
            en_us.AddText(EnumText.enumForum_Topic_HelpNewTopic, "New topic");
            en_us.AddText(EnumText.enumForum_Topic_PageTitleForum, "{0} - {1}");

            en_us.AddText(EnumText.enumForum_Topic_PageTitleDefault, "{0}");
            en_us.AddText(EnumText.enumForum_Topic_PageDefaultErrorLoad, "Error loading the Home page: ");
            en_us.AddText(EnumText.enumForum_Topic_ColumnModerators, "Moderators");
            en_us.AddText(EnumText.enumForum_Topic_ColumnTopics, "Topics");

            en_us.AddText(EnumText.enumForum_Topic_FieldLockedForum, "Locked Forum");

            en_us.AddText(EnumText.enumForum_Topic_TitleDraftList, "Drafts");
            en_us.AddText(EnumText.enumForum_Topic_SubtitleDraftList, "Draft is a rough version of your comment on a topic. For every topic,there is only one draft. Only operators, including administrators and moderators, have permission to write, read and revise a draft. All the operators view the same draft. Once the draft is posted, it will be deleted immediately by system.");
            en_us.AddText(EnumText.enumForum_Topic_FieldSubject, "Subject: ");
            en_us.AddText(EnumText.enumForum_Topic_ButtonQuery, "Query");
            en_us.AddText(EnumText.enumForum_Topic_ColumnTopicId, "Topic Id");
            en_us.AddText(EnumText.enumForum_Topic_ColumnTopicStarter, "Topic Starter");
            en_us.AddText(EnumText.enumForum_Topic_ColumnTopicSubject, "Topic Subject");
            en_us.AddText(EnumText.enumForum_Topic_ColumnTopicStatus, "Topic Status");
            en_us.AddText(EnumText.enumForum_Topic_ColumnPostTime, "Post Time");
            en_us.AddText(EnumText.enumForum_Topic_ColumnOperation, "Operation");
            en_us.AddText(EnumText.enumForum_Topic_HelpEdit, "Edit");
            en_us.AddText(EnumText.enumForum_Topic_HelpDelete, "Delete");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmDeleteDraft, "Are you sure to delete this draft?");
            en_us.AddText(EnumText.enumForum_Topic_PageDraftListErrorDelete, "Error deleting a draft: ");
            en_us.AddText(EnumText.enumForum_Topic_PageDraftListErrorLoadPage, "Error loading Drafts page: ");
            en_us.AddText(EnumText.enumForum_Topic_PageDraftListErrorQuery, "Error querying drafts: ");
            en_us.AddText(EnumText.enumForum_Topic_TopicStatusOpen, "Open");
            en_us.AddText(EnumText.enumForum_Topic_TopicStatusClosed, "Closed");
            en_us.AddText(EnumText.enumForum_Topic_TopicStatusMakeasanswer, "Marked as answer");

            en_us.AddText(EnumText.enumForum_Topic_TitleDraftEdit, "Edit Draft");
            en_us.AddText(EnumText.enumForum_Topic_SubtitleDraftEdit, "View the detail info of a selected topic and edit the draft of comment on this topic.");
            en_us.AddText(EnumText.enumForum_Topic_FieldPosts, "Posts");
            en_us.AddText(EnumText.enumForum_Topic_FieldDraft, "Draft");
            en_us.AddText(EnumText.enumForum_Topic_FieldContent, "Content: ");
            //en_us.AddText(EnumText.enumForum_Topic_FieldAt, " at ");
            en_us.AddText(EnumText.enumForum_Topic_fieldEditInformation, "The draft was edited by {0} at {1} ");
            en_us.AddText(EnumText.enumForum_Topic_ColumnAuthor, "Posted By");//Author
            en_us.AddText(EnumText.enumForum_Topic_ColumnMessage, "Message");
            en_us.AddText(EnumText.enumForum_Topic_ColumnPosted, "Posted: ");
            en_us.AddText(EnumText.enumForum_Topic_ColumnJoined, "Joined: ");
            en_us.AddText(EnumText.enumForum_Topic_ColumnPosts, "Posts: ");
            en_us.AddText(EnumText.enumForum_Topic_ColumnPost, "Posts");
            en_us.AddText(EnumText.enumForum_Topic_ButtonSave, "Save");
            en_us.AddText(EnumText.enumForum_Topic_ButtonPost, "Post");
            en_us.AddText(EnumText.enumForum_Topic_ButtonCancel, "Cancel");
            en_us.AddText(EnumText.enumForum_Topic_PageEditDraftErrorLoadDraft, "Error loading drafts list: ");
            en_us.AddText(EnumText.enumForum_Topic_PageEditDraftErrorLoadpage, "Error loading Edit Draft page:");
            en_us.AddText(EnumText.enumForum_Topic_PageEditDraftErrorLoadPost, "Error loading the posts: ");
            en_us.AddText(EnumText.enumForum_Topic_PageEditDraftErrorLoadTopicAndPost, "Error loading the topics and posts: ");
            en_us.AddText(EnumText.enumForum_Topic_ErrorSubjectRequired, "Subject can't be empty!");
            en_us.AddText(EnumText.enumForum_Topic_ErrorPostDraft, "Error posting a draft: ");
            en_us.AddText(EnumText.enumForum_Topic_ErrorSaveDraft, "Error saving a draft: ");

            en_us.AddText(EnumText.enumForum_Topic_TitleMyTopics, "My Topics");
            en_us.AddText(EnumText.enumForum_Topic_HelpMyTopics, "  Topic is the subject of a forum discussion. In this page,all the topics you start are listed in chronological order from the newest to the oldest.");
            en_us.AddText(EnumText.enumForum_Topic_ColumnStatus, "Status");
            en_us.AddText(EnumText.enumForum_Topic_ColumnSubject, "Topic");//subject
            en_us.AddText(EnumText.enumForum_Topic_ColumnDate, "Date");
            en_us.AddText(EnumText.enumForum_Topic_ColumnLastPost, "Activity");//Last Post
            en_us.AddText(EnumText.enumForum_Topic_ColumnReplies, "Replies");
            en_us.AddText(EnumText.enumForum_Topic_ColumnViews, "Views");
           
            en_us.AddText(EnumText.enumForum_Topic_TitleMyPosts, "My Posts");
            en_us.AddText(EnumText.enumForum_Topic_HelpMyPosts, "Post is the comments on topics. In this page, all the posts you make are listed in chronological order from the newest to the oldest.");
            en_us.AddText(EnumText.enumForum_Topic_FieldBy, "by");
            en_us.AddText(EnumText.enumForum_Topic_StatusClosedParticipated, "Closed Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusMarkedParticipated, "Resolved Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusNormalParticipated, "Normal Topic You Participated");
            en_us.AddText(EnumText.enumForum_Topic_FieldLegend, "LEGEND");
            en_us.AddText(EnumText.enumForum_Topic_PageMyTopicsErrorLoad, "Error loading your topics: ");
            en_us.AddText(EnumText.enumForum_Topic_PageMyPostsErrorLoad, "Error loading your posts:");
            en_us.AddText(EnumText.enumForum_Topic_FieldDeleteUser, "deleted user");

            en_us.AddText(EnumText.enumForum_Topic_TitleSerach, "Search Option ");
            en_us.AddText(EnumText.enumForum_Topic_TitleSearchResult, "Result:");
            en_us.AddText(EnumText.enumForum_Topic_PageTitleSearchResult, "Search Result - {0} - Comm100");
            en_us.AddText(EnumText.enumForum_Topic_PageTitleSearch, "AdvancedSearch - {0} - Comm100");
            en_us.AddText(EnumText.enumForum_Topic_HelpSerach, "Search Options help optimize your info quest by allowing multi-conditional search.");
            en_us.AddText(EnumText.enumForum_Topic_FieldKeyWords, "Keywords:");
            en_us.AddText(EnumText.enumForum_Topic_FieldsDisplayName, "Display Name:");
            en_us.AddText(EnumText.enumForum_Topic_FieldPostData, "Post Date:");
            en_us.AddText(EnumText.enumForum_Topic_FieldForum, "Forum:");
            en_us.AddText(EnumText.enumForum_Topic_FieldTopicStatus, "Topic Status:");
            en_us.AddText(EnumText.enumForum_Topic_FieldIfSticky, "If Sticky:");
            en_us.AddText(EnumText.enumForum_Topic_FieldFrom, "from:");
            en_us.AddText(EnumText.enumForum_Topic_FieldTo, "to:");
            en_us.AddText(EnumText.enumForum_Topic_FieldOpen, "Open");
            en_us.AddText(EnumText.enumForum_Topic_FieldClose, "Close");
            en_us.AddText(EnumText.enumForum_Topic_FieldMarked, "Marked As Answer");
            en_us.AddText(EnumText.enumForum_Topic_FieldSticky, "Sticky");
            en_us.AddText(EnumText.enumForum_Topic_ButtonReturn, "Return");
            en_us.AddText(EnumText.enumForum_Topic_ButtonSearch, "Search");
            en_us.AddText(EnumText.enumForum_Topic_ButtonReset, "Reset");
            en_us.AddText(EnumText.enumForum_Topic_ColumnForum, "Forum");
            en_us.AddText(EnumText.enumForum_Topic_HelpKeyWords, "Keywords helps you to look for topics and posts with these words in their titles");
            en_us.AddText(EnumText.enumForum_Topic_HelpDisplayName, "Display Name helps you to look for a specific user's topics and posts.");
            en_us.AddText(EnumText.enumForum_Topic_HelpPostDate, "Post Date helps you to look for topics and posts posted in a specific time span.And time format must be(mm-dd-yyyy)");
            en_us.AddText(EnumText.enumForum_Topic_HelpForum, "Category and Forum helps you to look for topics and posts in a specific category or forum.");
            en_us.AddText(EnumText.enumForum_Topic_HelpTopicStatus, "Topic Status helps you to look for topics and posts in certain status.");
            en_us.AddText(EnumText.enumForum_Topic_HelpIfSticky, "If Sticky helps you to look for the topics and posts put at the top of the forum.");
            en_us.AddText(EnumText.enumForum_Topic_ErrorKeyWordsRequired, "keywords  is required!");
            en_us.AddText(EnumText.enumForum_Topic_ErrorStatusRequired, "No status was checked!");
            en_us.AddText(EnumText.enumForum_Topic_ErrorTimeFormat, "Invalid date or the date out of range!");
            en_us.AddText(EnumText.enumForum_Topic_StatusClosedUnRead, "Closed Unread Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusMarkedUnRead, "Marked Unread Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusNormalUnRead, "Normal Unread Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusClosedRead, "Closed Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusMarkedRead, "Resolved Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusNormalRead, "Normal Read Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusSticky, "Sticky Topic");
            en_us.AddText(EnumText.enumForum_Topic_PageSearchResultErrorLoad, "Error loading Search Result page: ");
            en_us.AddText(EnumText.enumForum_Topic_Error, "Error: ");

            en_us.AddText(EnumText.enumForum_Topic_TitleAddTopic, "Post New Topic");
            en_us.AddText(EnumText.enumForum_Topic_BrowerTitleAddTopic, "New Topic - {0}");
            en_us.AddText(EnumText.enumForum_Topic_ButtonSubmit, "Submit");
            en_us.AddText(EnumText.enumForum_Topic_PageAddTopicErrorSubmit, "Error creating a new topic:");
            en_us.AddText(EnumText.enumForum_Topic_PageAddTopicErrorPageLoading, "Error loading post new topic page:");

            en_us.AddText(EnumText.enumForum_Topic_BrowerTitleEditTopic, "{0} - {1}");
            en_us.AddText(EnumText.enumForum_Topic_LabelEditTopic, "Edit Topic");
            en_us.AddText(EnumText.enumForum_Topic_LabelEditPost, "Edit Post");
            en_us.AddText(EnumText.enumForum_Topic_PageEditTopicOrPostErrorLoading, "Error loading Editing Post page:");
            en_us.AddText(EnumText.enumForum_Topic_PageEditTopicOrPostErrorEdit, "Error editing a post: ");

            en_us.AddText(EnumText.enumForum_Topic_ConfirmDeletePost, "Are you sure to delete this post?");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmMarkAsAnswer, "Are you sure to mark this post as answer?");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmMarkAsAnswerInsteadOfOld, "Answer exists, Are you sure to overwrite it?");
            en_us.AddText(EnumText.enumForum_Topic_LabelQuoteDeletedAuthor, "[deleted user]");
            en_us.AddText(EnumText.enumForum_Topic_FieldQuote, "Quote: ");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmDeleteTopic, "Are you sure to delete this topic?");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmCloseTopic, "Are you sure to close this topic?");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmReopenTopic, "Are you sure to reopen this topic?");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmStickyTopic, "Are you sure to make this topic sticky?");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmUnStickyTopic, "Are you sure to make this topic unsticky?");
            en_us.AddText(EnumText.enumForum_Topic_LinkGotoAnswer, "Go to answer");
            en_us.AddText(EnumText.enumForum_Topic_FieldPosted, "Posted: ");
            en_us.AddText(EnumText.enumForum_Topic_FieldJoined, "Joined:");
            en_us.AddText(EnumText.enumForum_Topic_NoteClosedTopic, "Closed Topic");
            en_us.AddText(EnumText.enumForum_Topic_NoteMarkedTopic, "Marked Topic");
            en_us.AddText(EnumText.enumForum_Topic_NoteNormalTopic, "Normal Topic");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderDeletingPost, "Deleting Post");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorDeletingPost, "Error deleting a post: ");
            en_us.AddText(EnumText.enumForum_Topic_AlterPostAlreadyMarked, "This post has already been marked as answer!");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderMarkingPost, "Marking Post");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorMarkingPost, "Error marking a post: ");
            en_us.AddText(EnumText.enumForum_Topic_AlterPostAlreadyUmmarkd, "This post has already been unmarked as answer!");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderUnmarkingPost, "Unmarking Post");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorUnmarkingPost, "Error unmarking a post: ");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorLoadingPostList, "Error loading the posts list: ");
            en_us.AddText(EnumText.enumForum_Topic_BrowerTitleTopic, "{0} - {1} - Comm100");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorDeletePost, "Error deleting a post: ");
            en_us.AddText(EnumText.enumForum_Topic_FieldPostMarkedAsAnswer, "This post is marked as the answer.");
            en_us.AddText(EnumText.enumForum_Topic_HelpUnMarkedButton, "Unmark as answer");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmUnMarked, "Are you sure to unmark this post?");
            en_us.AddText(EnumText.enumForum_Topic_FieldPostMarkAsAnswer, "This post is marked as the answer.");
            en_us.AddText(EnumText.enumForum_Topic_FieldPostEditedBy, "This post was edited by ");
            en_us.AddText(EnumText.enumForum_Topic_FieldPostEditedInfo, "This post was edited by {0} at {1}. ");

            en_us.AddText(EnumText.enumForum_Topic_TitleMoveTopic, "Move Topic");
            en_us.AddText(EnumText.enumForum_Topic_FieldAt, "at");
            en_us.AddText(EnumText.enumForum_Topic_HelpEditPost, "Edit this post");
            en_us.AddText(EnumText.enumForum_Topic_HelpDeletePost, "Delete this post");
            en_us.AddText(EnumText.enumForum_Topic_HelpMarkButton, "Mark as answer");
            en_us.AddText(EnumText.enumForum_Topic_HelpQuoteButton, "Quote");
            en_us.AddText(EnumText.enumForum_Topic_HelpQuickReplyButton, "Quick reply");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorGoToAnswer, "Error getting the answer: ");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorReply, "Error replying: ");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorDeleteTopic, "Error deleting a topic: ");
            en_us.AddText(EnumText.enumForum_Topic_MessageTopicClosed, "This topic has already been closed!");
            en_us.AddText(EnumText.enumForum_Topic_TitleQuickReply, "Quick Reply");
            en_us.AddText(EnumText.enumForum_Topic_ErrorHeaderClosingTopic, "Closing Topic");
            en_us.AddText(EnumText.enumForum_Topic_ErrorClosingTopic, "Error closing a topic: ");
            en_us.AddText(EnumText.enumForum_Topic_MessageTopicOpen, "This topic has already been reopened!");
            en_us.AddText(EnumText.enumForum_Topic_ErrorHeaderReopeningTopic, "Reopening Topic");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorReopeningTopic, "Error reopening a topic: ");
            en_us.AddText(EnumText.enumForum_Topic_MessageTopicSticky, "This topic has already been sticky!");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorStickyTopic, "Error sticking a topic: ");
            en_us.AddText(EnumText.enumForum_Topic_ErrorHeaderStickingTopic, "Sticking Topic");
            en_us.AddText(EnumText.enumForum_Topic_MessageTopicUnSticky, "This topic has already been unsticky!");
            en_us.AddText(EnumText.enumForum_Topic_ErrorHeaderUnstickingTopic, "Unsticking Topic");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorUnstickingTopic, "Error unsticking a topic: ");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorAddPost, "Error creating a post: ");
            en_us.AddText(EnumText.enumForum_Topic_ErrorHeaderAddingPost, "adding post");
            en_us.AddText(EnumText.enumForum_Topic_ErrorHeaderSavingDraft, "saving draft");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorSavingDraft, "Error saving a draft: ");
            en_us.AddText(EnumText.enumForum_Topic_HelpPostReply, "Post reply");
            en_us.AddText(EnumText.enumForum_Topic_MessagePermissionDenied, "You do not have the permission!");
            en_us.AddText(EnumText.enumForum_Topic_FieldError, "Error ");
            en_us.AddText(EnumText.enumForum_Topic_TooltipDeleteTopic, "Delete this topic");
            en_us.AddText(EnumText.enumForum_Topic_TooltipCloseTopic, "Close this topic");
            en_us.AddText(EnumText.enumForum_Topic_TooltipReopenTopic, "Reopen this topic");
            en_us.AddText(EnumText.enumForum_Topic_TooltipMoveTopic, "Move this topic to...");
            en_us.AddText(EnumText.enumForum_Topic_TooltipStickyTopic, "Sticky this topic");
            en_us.AddText(EnumText.enumForum_Topic_TooltipUnstickyTopic, "Unsticky this topic");
            en_us.AddText(EnumText.enumForum_Topic_TooltipGotoAnswer, "Go to answer");
            en_us.AddText(EnumText.enumForum_Topic_FieldDraftEditBy, "The draft was edited by ");
            en_us.AddText(EnumText.enumForum_Topic_FieldDraftEditInfo, "The draft was edited by {0} at {1}.");
            en_us.AddText(EnumText.enumForum_Topic_ButtonSaveDraft, "Save Draft");
            #endregion

            #region Forum Dashboard

            en_us.AddText(EnumText.enumForum_Dashboard_Title, "Dashboard");
            en_us.AddText(EnumText.enumForum_Dashboard_FieldNews, "News");
            en_us.AddText(EnumText.enumForum_Dashboard_News1, "1. Comm100 Forum is to hit the market in a week.");
            en_us.AddText(EnumText.enumForum_Dashboard_News2, "2. Comm100 is a professional software company providing open source and free hoste customer support and communication software for small and medium businesses. Comm100 Forum is an ASP.NET and SQL Server based open source forum.");

            #endregion

            #region Forum User
            en_us.AddText(EnumText.enumForum_User_TitleUserManagementList, "Users Management");
            en_us.AddText(EnumText.enumForum_User_SubtitleUserManagementList, "User in Comm100 Forum is an external user who has registered for your forum. Users do not include administrators and operators.");
            en_us.AddText(EnumText.enumForum_User_FieldDisplayName, "Display Name:");
            en_us.AddText(EnumText.enumForum_User_ButtonQuery, "Query");
            en_us.AddText(EnumText.enumForum_User_ColumnId, "Id");
            en_us.AddText(EnumText.enumForum_User_ColumnEmail, "Email");
            en_us.AddText(EnumText.enumForum_User_ColumnDisplayName, "Display Name");
            en_us.AddText(EnumText.enumForum_User_ColumnJoinedTime, "Join Time");
            en_us.AddText(EnumText.enumForum_User_ColumnLastJoinedTime, "Last Login Time");
            en_us.AddText(EnumText.enumForum_User_ColumnView, "View");
            en_us.AddText(EnumText.enumForum_User_ColumnDelete, "Delete");
            en_us.AddText(EnumText.enumForum_User_HelpView, "View");
            en_us.AddText(EnumText.enumForum_User_HelpDelete, "Delete");
            en_us.AddText(EnumText.enumForum_User_ConfirmDelete, "Are you sure to delete the user ?");
            en_us.AddText(EnumText.enumForum_User_PageUserManagementListErrorLoad, "Error loading Users Management page: ");
            en_us.AddText(EnumText.enumForum_User_PageUserManagementListErrorGet, "Error getting undeleted users: ");
            en_us.AddText(EnumText.enumForum_User_PageUserManagementListErrorQuery, "Error querying undeleted users by Display Name: ");
            en_us.AddText(EnumText.enumForum_User_PageUserManagementListErrorDelete, "Error deleting a user: ");

            en_us.AddText(EnumText.enumForum_User_TitleUserView, "User Profile");
            en_us.AddText(EnumText.enumForum_User_SubtitleUserView, "Here you can view user's personal information and posts.");
            en_us.AddText(EnumText.enumForum_User_FieldAge, "Age:");
            en_us.AddText(EnumText.enumForum_User_FieldAvatar, "Avatar:");
            en_us.AddText(EnumText.enumForum_User_FieldAvatarText, "Avatar");
            en_us.AddText(EnumText.enumForum_User_FieldBasicInformation, "Basic Information");
            en_us.AddText(EnumText.enumForum_User_FieldClose, "Close");
            en_us.AddText(EnumText.enumForum_User_FieldCompany, "Company:");
            en_us.AddText(EnumText.enumForum_User_FieldEmail, "Email:");
            en_us.AddText(EnumText.enumForum_User_FieldFaxNumber, "Fax Number:");
            en_us.AddText(EnumText.enumForum_User_FieldFemale, "Female");
            en_us.AddText(EnumText.enumForum_User_FieldGender, "Gender:");
            en_us.AddText(EnumText.enumForum_User_FieldHomePage, "Home Page:");
            en_us.AddText(EnumText.enumForum_User_FieldInterests, "Interests:");
            en_us.AddText(EnumText.enumForum_User_FieldItsasecret, "It's a secret");
            en_us.AddText(EnumText.enumForum_User_FieldJoined, "Joined time:");
            en_us.AddText(EnumText.enumForum_User_FieldLastVisit, "Last Visit:");
            en_us.AddText(EnumText.enumForum_User_FieldMale, "Male");
            en_us.AddText(EnumText.enumForum_User_FieldOccupation, "Occupation:");
            en_us.AddText(EnumText.enumForum_User_FieldPosts, "Posts:");
            en_us.AddText(EnumText.enumForum_User_FieldPhoneNumber, "Phone Number:");
            en_us.AddText(EnumText.enumForum_User_FieldStatisticalInformation, "Statistical Information");
            en_us.AddText(EnumText.enumForum_User_FieldUserName, "User Name:");
            en_us.AddText(EnumText.enumForum_User_FieldVisibletoPublic, "Visible to Public");
            en_us.AddText(EnumText.enumForum_User_PageUserViewErrorLoad, "Error loading Edit Profile page:");

            en_us.AddText(EnumText.enumForum_User_TitleUserModeration, "Users Moderation");
            en_us.AddText(EnumText.enumForum_User_SubtitleUserModeration, "Moderation is the process that administrators approve or refuse a user's registration request. In this page, all the users awaiting moderation are listed. You can view their info and approve or refuse a registration according to your forum management rules.");
            en_us.AddText(EnumText.enumForum_User_ColumnEmailVerification, "Email Verification");
            en_us.AddText(EnumText.enumForum_User_ColumnModerate, "Approve");
            en_us.AddText(EnumText.enumForum_User_ColumnRefuse, "Refuse");
            en_us.AddText(EnumText.enumForum_User_HelpModerate, "Moderate");
            en_us.AddText(EnumText.enumForum_User_HelpRefuse, "Refuse");
            en_us.AddText(EnumText.enumForum_User_ConfirmAccept, "Are you sure to approve the registration ?");
            en_us.AddText(EnumText.enumForum_User_ConfirmRefuse, "Are you sure to refuse the registration ?");
            en_us.AddText(EnumText.enumForum_User_PageModerationErrorGet, "Error getting users waiting for moderation ");
            en_us.AddText(EnumText.enumForum_User_PageModerationErrorLoad, "Error loading Users Moderation page: ");
            en_us.AddText(EnumText.enumForum_User_PageModerationErrorAccept, "Error accepting a user's registration:");
            en_us.AddText(EnumText.enumForum_User_PageModerationErrorModerate, "Error moderating a user: ");
            en_us.AddText(EnumText.enumForum_User_PageModerationError, "Error: ");
            en_us.AddText(EnumText.enumForum_User_PageModerationErrorQuery, "Error querying users waiting for moderation:");
            en_us.AddText(EnumText.enumForum_User_PageModerationErrorRefuse, "Error refusing a  user's registration: ");
            en_us.AddText(EnumText.enumForum_User_UserStateNeedLess, "needless");
            en_us.AddText(EnumText.enumForum_User_UserStateVerified, "verified");
            en_us.AddText(EnumText.enumForum_User_UserStateNotVerfied, "not verified");

            en_us.AddText(EnumText.enumForum_User_TitleEditProfile, "Edit Profile");
            en_us.AddText(EnumText.enumForum_User_SubtitleEditProfile, "Profile is the detail info you input when registering the forum. In this page, you can edit all these info and decide whether to make it public.");
            en_us.AddText(EnumText.enumForum_User_FieldFirstName, "First Name:");
            en_us.AddText(EnumText.enumForum_User_FieldLastName, "Last Name:");
            en_us.AddText(EnumText.enumForum_User_ButtonSave, "Save");
            en_us.AddText(EnumText.enumForum_User_PageEditProfileErrorLoading, "Error loading Edit Profile page: ");
            en_us.AddText(EnumText.enumForum_User_PageEditProfileSuccessSave, "Profile updated successfully");
            en_us.AddText(EnumText.enumForum_User_PageEditProfileErrorSave, "Error saving the user's profile:");
            en_us.AddText(EnumText.enumForum_User_ErrorAge, "Age is in 0~100.");
            en_us.AddText(EnumText.enumForum_User_ErrorDisplayNameFormat, "Display Name format is error.");
            en_us.AddText(EnumText.enumForum_User_HelpDisplayNameFormat, "Dispaly Names are composed of the following characters: letters A-Z, a-z, _, digits 0-9; but a display name can not begin with a digit.");
            en_us.AddText(EnumText.enumForum_User_ErrorDisplayNameRequired, "Display Name is required.");
            en_us.AddText(EnumText.enumForum_User_ErrorEmailFormat, "Email format is error.");
            en_us.AddText(EnumText.enumForum_User_ErrorEmailRequired, "Email is required.");

            en_us.AddText(EnumText.enumForum_User_TitleEditAvatar, "Edit Avatar");
            en_us.AddText(EnumText.enumForum_User_SubtitleEditAvatar, "Avatar is a picture you use as your personal image. In this page, you can view your current avatar and upload your own avatar or choose one from default avatars.");
            en_us.AddText(EnumText.enumForum_User_LabelSystemAvatars, "Avatars:");
            en_us.AddText(EnumText.enumForum_User_LabelCurrentAvatar, "Current Avatar:");
            en_us.AddText(EnumText.enumForum_User_FieldNewAvatar, "New Avatar:");
            en_us.AddText(EnumText.enumForum_User_HelpUploadDescription, "Upload your avatar here.All major image formats are supported.The maximum size allowed is 50K.The optimal ratio of width and height is1:1.The size of the picture can not exceed 60*60.The picture exceeding 60*60 will be resized in the same proportion.");
            en_us.AddText(EnumText.enumForum_User_PageEditAvatarErrorLoading, "Error loading Edit Avatar page:: ");
            en_us.AddText(EnumText.enumForum_User_PageEditAvatarSuccessSave, "Avatar Updated Successfully");
            en_us.AddText(EnumText.enumForum_User_PageEditAvatarErrorUpload, "Error uploading your avatar:");
            en_us.AddText(EnumText.enumForum_User_PageEditAvatarErrorSave, "Error saving edit avatar:");
            en_us.AddText(EnumText.enumForum_User_PageEditAvatarErrorDefault, "Error uploading the default avatar:");
            en_us.AddText(EnumText.enumForum_User_FieldCustomAvatar, "Custom Avatars");
            en_us.AddText(EnumText.enumForum_User_FieldSystemAvatar, "Default Avatars");
            en_us.AddText(EnumText.enumForum_User_ButtonDefault, "Default");
            en_us.AddText(EnumText.enumForum_User_ButtonUpload, "Upload");
            en_us.AddText(EnumText.enumForum_User_FieldCurrentAvatarText, "My Avatar");
            en_us.AddText(EnumText.enumForum_User_PageEditAvatarErrorInitLanguage, "Error:");

            en_us.AddText(EnumText.enumForum_User_TitleEditSignature, "Edit Signature");
            en_us.AddText(EnumText.enumForum_User_SubtitleEditSignature, "Signature displays at the bottom of every topic or post you write. It is a mark used to indicate your identity. In this page, you can design your own personal signature.");
            en_us.AddText(EnumText.enumForum_User_PageEditSignatureErrorLoading, "Error loading Signature page: ");
            en_us.AddText(EnumText.enumForum_User_PageEditSignatureSucessSave, "Signature updated successfully.");
            en_us.AddText(EnumText.enumForum_User_PageEditSignatureErrorSave, "Error saving the signature:");

            en_us.AddText(EnumText.enumForum_User_TitleEditPassword, "Change Password");
            en_us.AddText(EnumText.enumForum_User_SubtitleEditPassword, "Password is a secret word or phrase you use to access the forum. In this page, you can change your password.");
            en_us.AddText(EnumText.enumForum_User_FieldCurrentPassword, "Current Password:");
            en_us.AddText(EnumText.enumForum_User_FieldNewPassword, "New Password:");
            en_us.AddText(EnumText.enumForum_User_FieldRetypePassword, "Retype Password:");
            en_us.AddText(EnumText.enumForum_User_PageEditPasswordErrorLoading, "Error loading Change Password page:");
            en_us.AddText(EnumText.enumForum_User_PageEditPasswordSuccessSave, "Password updated successfully");
            en_us.AddText(EnumText.enumForum_User_PageEditPasswordErrorSave, "Error saving password:");
            en_us.AddText(EnumText.enumForum_User_ErrorCurrentPasswordRequired, "Current password is required.");
            en_us.AddText(EnumText.enumForum_User_ErrorNewPasswordRequired, "New password is required.");
            en_us.AddText(EnumText.enumForum_User_ErrorRetypePasswordRequired, "Retype password is required.");
            en_us.AddText(EnumText.enumForum_User_ErrorRetypePsswordMatch, "Passwords don't match");

            en_us.AddText(EnumText.enumForum_User_TitleUserProfile, "User Profile");
            en_us.AddText(EnumText.enumForum_User_BroswerTitleUserProfile, "User's Profile - {0} - Comm100");
            en_us.AddText(EnumText.enumForum_User_PageUserProfileErrorLoading, "Error loading  User Profile page: ");

            en_us.AddText(EnumText.enumForum_User_TitleResetPassword, "Reset Password");
            en_us.AddText(EnumText.enumForum_User_MessageInvalidResetPasswordLink, "The link for resetting password is invalid.");
            en_us.AddText(EnumText.enumForum_User_BrowerTitleResetPassword, "Reset Password - {0} - Comm100");
            en_us.AddText(EnumText.enumForum_User_ButtonResetPassword, "Reset Password");
            en_us.AddText(EnumText.enumForum_User_AlertResetPasswordSucceed, "Reset password succeed.");
            en_us.AddText(EnumText.enumForum_User_PageResetPasswordErrorReset, "Error reseting password:");

            en_us.AddText(EnumText.enumForum_User_QueryEmailOrDisplayName, "Email/Display Name:");
            #endregion

            #region Forum Register

            en_us.AddText(EnumText.enumForum_Register_TitlePreRegister, "Register");
            en_us.AddText(EnumText.enumForum_Register_SubtitlePreRegister, "Rules and Policies");
            en_us.AddText(EnumText.enumForum_Register_PageTitlePreRegister, "Register - {0} - Comm100");
            en_us.AddText(EnumText.enumForum_Register_ButtonNext, "Next");
            en_us.AddText(EnumText.enumForum_Register_PagePreRegisterError, "Error: ");
            en_us.AddText(EnumText.enumForum_Register_Content1, "Welcome to our forum.<br/><br/>");
            en_us.AddText(EnumText.enumForum_Register_Content2, "&nbsp;&nbsp;&nbsp;&nbsp;By accessing our forum (hereinafter 'we', 'us', 'our'), you agree to be legally bound by the following terms. If you do not agree to be legally bound by all of the following terms then please do not access and/or use our forum. We reserve the right to update this Rules and Policies at any time without notice to you. It is your responsibility to check the latest version of this Rules and Policies.<br/><br/>");
            en_us.AddText(EnumText.enumForum_Register_Content3, "&nbsp;&nbsp;&nbsp;&nbsp;Our forums are powered by Comm100 Forum. For further information about Comm100 Forum, please visit:<a href='http://www.comm100.com' target='_blank'>'http://www.comm100.com/'</a>.<br/><br/>");
            en_us.AddText(EnumText.enumForum_Register_Content4, "&nbsp;&nbsp;&nbsp;&nbsp;You agree not to post any abusive, obscene, vulgar, slanderous, hateful, threatening, sexually-orientated or any other material that may violate any laws in your country and the country we are located. You agree not to violate any code of conduct or other guidelines which may be applicable for our forums, including, not limited to, those ones posted by forum administrators and moderators. We reserve the right to immediately and permanently ban your account without cause. The IP addresses of all posts are recorded to aid in enforcing these conditions. You agree that we have the right to remove, edit, move or close any topic at any time should we see fit.<br/><br/>");
            en_us.AddText(EnumText.enumForum_Register_Content5, "&nbsp;&nbsp;&nbsp;&nbsp;IN NO EVENT SHALL WE, COMM100 NETWORK CORPORATION AND/OR ITS  RESPECTIVE SUPPLIERS BE LIABLE FOR ANY SPECIAL, INDIRECT OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTIONG WITH THE USE OR PREFORMANCE OF SOFTWARE, DOCUMENTS, PROVISION OF OR FAILURE TO PROVIDE SERVICES, OR INFORMATION AVAILABLE FROM THE SERVICES.");
            en_us.AddText(EnumText.enumForum_Register_ContentEnd, "I have read, and agree to abide by the Rules and Policies.");

            en_us.AddText(EnumText.enumForum_Register_TitleRegister, "Registration Option");
            en_us.AddText(EnumText.enumForum_Register_FieldEmail, "Email:");
            en_us.AddText(EnumText.enumForum_Register_FieldRetypeEmail, "Retype Email:");
            en_us.AddText(EnumText.enumForum_Register_FieldDisplayName, "Display Name:");
            en_us.AddText(EnumText.enumForum_Register_FieldPassword, "Password:");
            en_us.AddText(EnumText.enumForum_Register_FieldRetypePassword, "Retype Password:");
            en_us.AddText(EnumText.enumForum_Register_FieldVerificationCode, "Verification Code:");
            en_us.AddText(EnumText.enumForum_Register_HelpImageVerificationCode, "imgVerificationCode");
            en_us.AddText(EnumText.enumForum_Register_ButtonEmailVerification, "Email Verification");
            en_us.AddText(EnumText.enumForum_Register_ButtonCompleteRegister, "Complete Registration");
            en_us.AddText(EnumText.enumForum_Register_ErrorMatchPasswords, "The two passwords you entered don't match.");
            en_us.AddText(EnumText.enumForum_Register_ErrorDisplayNameRequired, "Display name is required.");
            en_us.AddText(EnumText.enumForum_Register_ErrorMatchMails, "The emails entered don't match.");
            en_us.AddText(EnumText.enumForum_Register_ErrorMailFormat, "Email format is invalid.");
            en_us.AddText(EnumText.enumForum_Register_ErrorEmailNotUsed, " &nbsp;has not been used. You can use it to register.");
            en_us.AddText(EnumText.enumForum_Register_ErrorEmailUsed, "&nbsp; has been used. You cannot use it to register again.");
            en_us.AddText(EnumText.enumForum_Register_ErrorEmailRequired, "Email is required.");
            en_us.AddText(EnumText.enumForum_Register_ErrorDisplayFormat, "Display Name is composed of the following characters.letters A-Z,a-z,digits 0-9,and underline(_)  but a display name can not begin with a digit.");
            en_us.AddText(EnumText.enumForum_Register_ErrorPasswordRequired, "Password is required.");
            en_us.AddText(EnumText.enumForum_Register_ErrorRetypeEmailRequired, "Retype Email is required.");
            en_us.AddText(EnumText.enumForum_Register_ErrorRetypePasswordRequired, "Retype password is required.");
            en_us.AddText(EnumText.enumForum_Register_ErrorVerificationCode, "Verification Code is wrong.");
            en_us.AddText(EnumText.enumForum_Register_ErrorVerificationCodeRequired, "verification is required.");
            en_us.AddText(EnumText.enumForum_Register_PageRegisterErrorLoad, "Error loading Register page: ");
            en_us.AddText(EnumText.enumForum_Register_PageRegisterErrorRegister, "Error registering a new user: ");

            en_us.AddText(EnumText.enumForum_Register_TitlePostRegister, "Register");
            en_us.AddText(EnumText.enumForum_Register_LinkBackToLastPage, "Back to Last Page");
            en_us.AddText(EnumText.enumForum_Register_LinkUserControlPanel, "Enter User Control Panel");
            en_us.AddText(EnumText.enumForum_Register_LinkHomePage, "Go to Home Page");
            en_us.AddText(EnumText.enumForum_Register_PagePostRegisterErrorLoad, "Error loading Post Register page:");
            en_us.AddText(EnumText.enumForum_Register_ContentThanks, "Thank you.");
            en_us.AddText(EnumText.enumForum_Register_ContentEmail, "Please enter your Email address to receive the confirmation mail, and finish your registration.");
            en_us.AddText(EnumText.enumForum_Register_ContentEmailAndModerated, "Please go to your mailbox to receive the confirmation mail, and activate your registration. ");
            en_us.AddText(EnumText.enumForum_Register_StateWait, "Waiting for moderation.");
            en_us.AddText(EnumText.enumForum_Register_StateSuccess, "Registration Succeeds.");
            en_us.AddText(EnumText.enumForum_Register_LinkJump, "Please click the hyper links below if this page does not redirect in 5 seconds.");
            #endregion

            #region Forum Login
            en_us.AddText(EnumText.enumForum_login_TitleAdminLogin, "Admin Login");
            en_us.AddText(EnumText.enumForum_login_TitleUser, "User Login");
            en_us.AddText(EnumText.enumForum_login_FieldEmail, "Email:");
            en_us.AddText(EnumText.enumForum_login_FieldPassword, "Password:");
            en_us.AddText(EnumText.enumForum_login_FieldVerificationCode, "Verification Code:");
            en_us.AddText(EnumText.enumForum_login_FieldRememberMe, "Remember me");
            en_us.AddText(EnumText.enumForum_login_FieldForgetPassword, "Forgot password?");
            en_us.AddText(EnumText.enumForum_login_FieldNewToForum, "New to forum?");
            en_us.AddText(EnumText.enumForum_Login_LinkUserControlPanel, "Go to User Control Panel");
            en_us.AddText(EnumText.enumForum_login_LinkClickHere, "Click here!");
            en_us.AddText(EnumText.enumForum_Login_LinkRegisterHere, "Register here!");
            en_us.AddText(EnumText.enumForum_Login_ButtonLogin, "Login");
            en_us.AddText(EnumText.enumForum_Login_ErrorVerification, "Verification Code does't match.");
            en_us.AddText(EnumText.enumForum_Login_PageLoginErrorLogin, "Error loading the Login page: ");
            en_us.AddText(EnumText.enumForum_Login_PageLoginErrorLoginSite, "Error logging in to the site: ");
            en_us.AddText(EnumText.enumForum_Login_SessionOut, "Session time out! Please login again.");
            en_us.AddText(EnumText.enumForum_Login_LinkHomePage, "Go to Home Page");
            en_us.AddText(EnumText.enumForum_Login_LinkBackPage, "Go to Last Page");
            en_us.AddText(EnumText.enumForum_Login_WaitJump, "Please click the hyper links below if this page does not redirect in 5 seconds.");
            en_us.AddText(EnumText.enumForum_Login_StateSuccess, "Log in Successfully!");
            en_us.AddText(EnumText.enumForum_Login_EmailFormat, "Email is error.");
            en_us.AddText(EnumText.enumForum_Login_ErrorVerificationCodeRequired, "Verification Code is required.");
            en_us.AddText(EnumText.enumForum_Login_ErrorEmailRequired, "Email is required.");
            en_us.AddText(EnumText.enumForum_Login_ErrorPasswordRequired, "Password is required.");
            en_us.AddText(EnumText.enumForum_Login_PageTitle, "{0} - {1} - Comm100");

            en_us.AddText(EnumText.enumForum_Login_TitleSendResetPasswordEmail, "Send Reset Password Email.");
            en_us.AddText(EnumText.enumForum_Login_MessageSendResetPasswordEmailSuccess, "The email for resetting the password has been sent.");
            en_us.AddText(EnumText.enumForum_Login_BrowerTitleSendResetPasswordEmail, "Reset Password - {0} - Comm100");
            en_us.AddText(EnumText.enumForum_Login_TitleFindPassword, "Find Password");
            en_us.AddText(EnumText.enumForum_Login_NoteEnterEmail, "Please enter the email you used to register the account. We will send an email to you for resetting the password.");
            en_us.AddText(EnumText.enumForum_Login_BrowerTitleFindPassword, "Find Password - {0} - Comm100");
            en_us.AddText(EnumText.enumForum_Login_PageFindPasswordErrorLoading, "Error loading Find Password page:");
            en_us.AddText(EnumText.enumForum_Login_PageFindPasswordErrorUnregisteredEmail, "Unregistered Email");
            en_us.AddText(EnumText.enumForum_Login_PageFindPasswordErrorEmailNotVerified, "The email has not be verified or the account has not been moderated by operator.");
            en_us.AddText(EnumText.enumForum_Login_PageFindPasswordErrorFindingPassword, "Error finding the password:");
            en_us.AddText(EnumText.enumForum_Login_ButtonSend, "Send");
            en_us.AddText(EnumText.enumForum_Login_PageFindPasswordErrorEmailFormat, "Email format is invalid");
            en_us.AddText(EnumText.enumForum_Login_PageFindPasswordErrorEmailRequired, "Email is required");

            en_us.AddText(EnumText.enumForum_Login_TitleEmailVerification, "Email Verification");
            en_us.AddText(EnumText.enumForum_Login_LinkGotoUserPanel, "Go to  User Control Panel");
            en_us.AddText(EnumText.enumForum_Login_BrowerTitleEmailVerification, "Email Verification - {0} - Comm100");
            en_us.AddText(EnumText.enumForum_Login_MessageEmailVerificationSucceed, "Congratulation. Email Verification succeed.");
            en_us.AddText(EnumText.enumForum_Login_MessageEmailVerificationWait, "<br/>Please wait to be moderated.<br/>");
            en_us.AddText(EnumText.enumForum_Login_PageEmailVerificationErrorVerification, "Sorry. Email Verification failed because your registration has been refused by forum administrator.<br/>");
            en_us.AddText(EnumText.enumForum_Login_MessageEmailVerificationAgain, "You have verified the email before.<br/>");


            #endregion

            #region Email Content
            en_us.AddText(EnumText.enumForum_Email_VerifyEmailSubject, "Email Verification from {0}");
            en_us.AddText(EnumText.enumForum_Email_VerifyEmailContentHiName, "Hi {0},");
            en_us.AddText(EnumText.enumForum_Email_VerifyEmailContentThank, "Thank you very much for signing up for {0}.");
            en_us.AddText(EnumText.enumForum_Email_VerifyEmailContentFollow, "Your {0} information is as follows:");
            en_us.AddText(EnumText.enumForum_Email_VerifyEmailContentInfoEmail, "Email: {0} ");
            en_us.AddText(EnumText.enumForum_Email_VerifyEmailContentInfoName, "Display Name: {0} ");
            en_us.AddText(EnumText.enumForum_Email_VerifyEmailContentInfoPassword, "Password: {0} ");
            en_us.AddText(EnumText.enumForum_Email_VerifyEmailContentVerifyPageDescription, "Please click the following hyper link to verify your email address and finish your registration.");
            en_us.AddText(EnumText.enumForum_Email_VerifyEmailContentVerifyPageHyperLink, "Click here to verify your email address.");
            en_us.AddText(EnumText.enumForum_Email_VerifyEmailContentLoginUrlDescription, "The login URL is:");
            en_us.AddText(EnumText.enumForum_Email_VerifyEmailContentLoginUrlHyperLink, "Login to the forum.");
            en_us.AddText(EnumText.enumForum_Email_VerifyEmailContentSincerely, "Sincerely yours,");

            en_us.AddText(EnumText.enumForum_Email_ResetPasswordEmailSubject, "Reset Password Email From {0}");
            en_us.AddText(EnumText.enumForum_Email_ResetPasswordEmailContentHiName, "Hi {0},");
            en_us.AddText(EnumText.enumForum_Email_ResetPasswordEmailContetnEmailDescription, "This is an email to you for resetting your account password from the {0}.");
            en_us.AddText(EnumText.enumForum_Email_ResetPasswordEmailContentResetPasswordPageDescription, "Please click the following hyper link to reset your password.");
            en_us.AddText(EnumText.enumFOrum_Email_ResetPasswordEmailContentResetPasswordPageHyperLink, "Go to reset your password.");
            en_us.AddText(EnumText.enumForum_Email_ResetPasswordEmailContentHomePageUrlDescription, "The homepage URL is:");
            en_us.AddText(EnumText.enumForum_Email_ResetPasswordEmailContentHomePageUrlHyperLink, "Go to the forum homepage");
            en_us.AddText(EnumText.enumForum_Email_ResetPasswordEmailContentSincerely, "Sincerely yours,");

            en_us.AddText(EnumText.enumForum_Email_ModeratorSuccessEmailSubject, "User Moderate Success From {0}");
            en_us.AddText(EnumText.enumForum_Email_ModeratorSuccessEmailContentHiName, "Hi {0},");
            en_us.AddText(EnumText.enumForum_Email_ModeratorSuccessEmailContentEmailDescription, "This is an email for you to tell  that your registration to the forum has been moderated successfully from the {0}.");
            en_us.AddText(EnumText.enumForum_Email_ModeratorSuccessEmailContentLoginUrlDescription, "Please click the following hyper link to login to the forum.");
            en_us.AddText(EnumText.enumForum_Email_ModeratorSuccessEmailContentLoginUrlHyperLink, "Click here to login to the forum.");
            en_us.AddText(EnumText.enumForum_Email_ModeratorSuccessEmailContentSincerely, "Sincerely yours,");

            en_us.AddText(EnumText.enumForum_Email_ModeratorFailedEmailSubject, "User Moderate Failed from {0}");
            en_us.AddText(EnumText.enumForum_Email_ModeratorFailedEmailContentSorryName, "Sorry {0},");
            en_us.AddText(EnumText.enumForum_Email_ModeratorFailedEmailContentEmailDescription, "This is an email to tell you that your registration to the forum has been refused from the {0}.");
            en_us.AddText(EnumText.enumForum_Email_ModeratorFailedEmailContentHomePageDescription, "Please click the following hyper link to see the homepage of the forum.");
            en_us.AddText(EnumText.enumForum_Email_ModeratorFailedEmailContentHomePageHyperLink, "Click here to go to the homepage.");
            en_us.AddText(EnumText.enumForum_Email_ModeratorFailedEmailContentSincerely, "Sincerely yours,");
            #endregion

            /*en_us.AddText(EnumText.enumForum_Login_Name, "User Id");
            en_us.AddText(EnumText.enumForum_Login_Password, "PassWord");
            en_us.AddText(EnumText.enumForum_Login_Ok, "Ok");
            en_us.AddText(EnumText.enumForum_Login_Title, "Login");*/

            #region Forum2.0 Topics & Posts 7 Search & Fourm
            /*Abuse Post Page*/
            en_us.AddText(EnumText.enumForum_AbusePost_TitleAbuse, "Abuse");
            en_us.AddText(EnumText.enumForum_AbusePost_LabelClose, "[Close]");
            en_us.AddText(EnumText.enumForum_AbusePost_FiledNotes, "Notes");
            en_us.AddText(EnumText.enumForum_AbusePost_ButtonSubmit, "Submit");
            en_us.AddText(EnumText.enumForum_AbusePost_ErrorNotesIsRequired, "Notes is required.");
            en_us.AddText(EnumText.enumForum_AbusePost_ErrorAbusePost, "Error Abusing a Post:");

            /*Add Topic Page*/
            en_us.AddText(EnumText.enumForum_AddTopic_TitleAdvancedOptions, "Advanced Options");
            en_us.AddText(EnumText.enumForum_AddTopic_TitlePostSettings, "Post Settings:");
            en_us.AddText(EnumText.enumForum_AddTopic_FiledPostNormal, "Normal");
            en_us.AddText(EnumText.enumForum_AddTopic_FiledPostNeedReplayToView, "Need Replay to view");
            en_us.AddText(EnumText.enumForum_AddTopic_FiledPostNeedPayScoreToView, "Need Pay Score to view");
            en_us.AddText(EnumText.enumForum_AddTopic_FiledHowManyScoreRequired, "How Many Score Required");
            en_us.AddText(EnumText.enumForum_AddTopic_ErrorPlesaeInputNumber, "please input one number.");
            en_us.AddText(EnumText.enumForum_AddTopic_ErrorPayForTopicIsRequired, "pay for topic is required.");
            en_us.AddText(EnumText.enumForum_AddTopic_TiltePollCreation, "Poll Creation");
            en_us.AddText(EnumText.enumForum_AddTopic_SubTitlePollOptions, "Poll Options");
            en_us.AddText(EnumText.enumForum_AddTopic_SubTitleOtherOptions, "Other Options");
            en_us.AddText(EnumText.enumForum_AddTopic_ErrorMulitipleChoiceIsRequired, "Mulitiple Choice is required.");
            en_us.AddText(EnumText.enumForum_AddTopic_FiledPollDateTo, "Poll Date To");
            en_us.AddText(EnumText.enumForum_AddTopic_ErrorPollDateIsErrorFormat, "Poll Date is error format.");
            en_us.AddText(EnumText.enumForum_AddTopic_TitleUploadAttachment, "Upload Attachment");
            en_us.AddText(EnumText.enumForum_AddTopic_FiledDownLoadScore, "DownLoad");
            en_us.AddText(EnumText.enumForum_AddTopic_ErrorPayForDownlaodIsRequired, "pay for download is required.");
            en_us.AddText(EnumText.enumForum_AddTopic_FiledDescription, "Description");
            en_us.AddText(EnumText.enumForum_AddTopic_ButtonUpload, "Upload");
            //code
            en_us.AddText(EnumText.enumForum_AddTopic_ErrorUploadAttachment, "Error Uploading Attachment:");
            en_us.AddText(EnumText.enumForum_AddTopic_ErrorLoadingAttachments, "Error Loading Attachments:");
            en_us.AddText(EnumText.enumForum_AddTopic_ErrorDeleteAttachment, "Error Deleting Attachment:");
            /*Advanced Search Page*/
            en_us.AddText(EnumText.enumForum_AdvancedSearch_TitleTimeRange, "Time Range");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_Field1Day, "1 Day");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_Field7Day, "7 Days");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_Field2Weeks, "2 Weeks");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_Field1Month, "1 Month");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_Field3Months, "3 Months");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_Field6Months, "6 Months");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_Field1Years, "1 Year");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_TitleSearchWithIn, "Search Within");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_FieldPostSubjectsAndMessageText, "Post Subjects And Message Text");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_FieldMessageTextOnly, "Message Text Only");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_FieldTopicTitlesOnly, "Topic Titles Only");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_FieldFirstPostOfTopicsOnly, "First Post Of Topics Only");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_TitleDisplayAs, "Display As");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_FieldTopics, "Topics");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_FieldPosts, "Posts");
            /*Ban User Page*/
            en_us.AddText(EnumText.enumForum_BanUser_TitleBanUser, "Ban User");
            en_us.AddText(EnumText.enumForum_BanUser_TitleClose, "[Close]");
            en_us.AddText(EnumText.enumForum_BanUser_SubTitleBanUser, "Ban User");
            en_us.AddText(EnumText.enumForum_BanUser_SubTitleExpireTime, "Expire Time");
            en_us.AddText(EnumText.enumForum_BanUser_FiledMinuets, "Minuets");
            en_us.AddText(EnumText.enumForum_BanUser_FiledHours, "Hours");
            en_us.AddText(EnumText.enumForum_BanUser_FiledDays, "Days");
            en_us.AddText(EnumText.enumForum_BanUser_FliedMonths, "Months");
            en_us.AddText(EnumText.enumForum_BanUser_FiledYears, "Years");
            en_us.AddText(EnumText.enumForum_BanUser_FiledPermanent, "Permanent");
            en_us.AddText(EnumText.enumForum_BanUser_SubTitleNotes, "Notes");
            en_us.AddText(EnumText.enumForum_BanUser_SubTitleRequired, "* Is Required");
            en_us.AddText(EnumText.enumForum_BanUser_ButtonBan, "Ban");
            en_us.AddText(EnumText.enumForum_BanUser_ErrorExprieTimeIsRequired, "Exprie Time Is Required");
            en_us.AddText(EnumText.enumForum_BanUser_ErrorNotesIsRequired, "Notes Is Required");
            en_us.AddText(EnumText.enumForum_BanUser_ErrorPleaseInputOneNumber, "Please Input One Number");
            //code
            en_us.AddText(EnumText.enumForum_BanUser_PageTitleBanUser, "Ban User");
            en_us.AddText(EnumText.enumForum_BanUser_ErrorLoadingBanUserPage, "Error Loading Ban User Page:");
            en_us.AddText(EnumText.enumForum_BanUser_ErrorBanningUser, "Error Banning User:");
            /*Edit Topic Or Post Page*/
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_TitleAdvancedOptions, "Advanced Options");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_TitlePostSettings, "Post Settings");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_FiledNormal, "Normal");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_FiledPayScoreToView, "Pay Score To View");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_FiledHowManyScoreRequired, "How Many Score Required");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorScoreIsRequired, "Score Is Required");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorPleaseInputOneNumber, "Please Input One Number");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_TitlePollCreation, "Poll Creation");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_SubTitlePollOptions, "Poll Options");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_FiledOption, "Option");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_FiledOrder, "Order");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_FiledDelete, "Delete");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_SubTitleOtherOptions, "Other Options");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_FiledMulitipleChoice, "Mulitiple Choice");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorMulitipleChoiceIsRequired, "Mulitiple Choice Is Required");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_FiledPollDateTo, "Poll Date To");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorPollDateIsErrorFormat, "Poll Date Is Error Format");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_SubTitleUploadAttachment, "Upload Attachment:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_SubTitleDownLoad, "Download:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorPayForDownloadIsRequired, "Pay For Download Is Required");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_SubTitleDescription, "Description:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ButtonUpload, "Upload");
            //code
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorUploadingAttachment, "Error Uploading Attachment:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorLoadingAttachment, "Error Loading Attachment:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorDeletingAttachment, "Error Deleting Attachment:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorLoadingPollOptions, "Error Loading Poll Options:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorDeletingPollOption, "Error Deleting Poll Option:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorMovingUpPollOption, "Error Moving Up Poll Option:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorMovingDownPollOption, "Error Moving Down Poll Option:");
            /*Forum Page*/
            en_us.AddText(EnumText.enumForum_Forum_ButtonAll, "All");
            en_us.AddText(EnumText.enumForum_Forum_ButtonFeatured, "Featured");
            en_us.AddText(EnumText.enumForum_Forum_ButtonSearch, "Search");
            en_us.AddText(EnumText.enumForum_Forum_SubTitleAnnoucements, "Annoucements");
            en_us.AddText(EnumText.enumForum_Forum_ToolTipAnnoucement, "Annoucement");
            //code
            en_us.AddText(EnumText.enumForum_Forum_ErrorShowingAllTopics, "Error Showing All Topics:");
            en_us.AddText(EnumText.enumForum_Forum_ErrorShowingFeaturedTopics, "Error Showing Featured Topics:");
            en_us.AddText(EnumText.enumForum_Forum_ErrorSearchingTopic, "Error Searching Topic:");
            en_us.AddText(EnumText.enumForum_Forum_ToolTipMoved, "Moved");
            en_us.AddText(EnumText.enumForum_Forum_FiledNeedScore, "Need Score");
            en_us.AddText(EnumText.enumForum_Forum_ToolTipFeatured, "Featured");
            en_us.AddText(EnumText.enumForum_Forum_ToolTipHot, "Hot");
            en_us.AddText(EnumText.enumForum_Forum_ToolTipAttachments, "Attachments");
            en_us.AddText(EnumText.enumForum_Forum_ToolTipVote, "Vote");
            /*ForumIsClosed Page*/
            en_us.AddText(EnumText.enumForum_ForumIsClosed_TitleCurrentForumHasBeenClosed, " >> Current Forum Has Been Closed");
            en_us.AddText(EnumText.enumForum_ForumIsClosed_FiledCurrentForumHasBeenClosed, "Current Forum Has Been Closed");
            //code
            en_us.AddText(EnumText.enumForum_ForumIsClosed_ErrorLoadingForumIsClosedPage, "Error Loading Forum Is Closed Page:");
            /*Pay Score Page*/
            en_us.AddText(EnumText.enumForum_PayScore_TitlePayScore, "Pay Score");
            en_us.AddText(EnumText.enumForum_PayScore_SubTitleNeedPayScore, "Need Pay Score:");
            en_us.AddText(EnumText.enumForum_PayScore_SubTitleCurrentYourScore, "Current Your Score:");
            en_us.AddText(EnumText.enumForum_PayScore_ButtonPay, "Pay");
            en_us.AddText(EnumText.enumForum_PayScore_ButtonClose, "Close");
            en_us.AddText(EnumText.enumForum_PayScore_ErrorCurrentUserHaveNotEnoughScore, "Current User Have Not Enough Score");
            //code
            en_us.AddText(EnumText.enumForum_PayScore_ErrorLoadingPayScorePage, "Error Loading Pay Score Page:");
            en_us.AddText(EnumText.enumForum_PayScore_ErrorPayingScore, "Error Paying Score:");
            /*Search Result Page*/
            en_us.AddText(EnumText.enumForum_SearchResult_FiledAuthor, "Author");
            en_us.AddText(EnumText.enumForum_SearchResult_FiledMessage, "Message");
            en_us.AddText(EnumText.enumForum_SearchResult_FiledPostSubject, "Post Subject:");
            en_us.AddText(EnumText.enumForum_SearchResult_FiledPosted, "Posted:");
            /*Send Messages Page*/
            en_us.AddText(EnumText.enumForum_SendMessages_SubTitleRecipeintName, "Recipeint's Name");
            en_us.AddText(EnumText.enumForum_SendMessages_SubTitleSubject, "Subject");
            en_us.AddText(EnumText.enumForum_SendMessages_SubTitleMessage, "Message");
            en_us.AddText(EnumText.enumForum_SendMessages_SubTitleRequired, " * Is Required");
            en_us.AddText(EnumText.enumForum_SendMessages_SubTitleSendMessage, "Send Message");
            en_us.AddText(EnumText.enumForum_SendMessages_ErrorMessageSubjectIsRequired, "Message Subject Is Required");
            //code
            en_us.AddText(EnumText.enumForum_SendMessages_ErrorLoadingSendMessagesPage, "Error Loading Send Messages Page:");
            en_us.AddText(EnumText.enumForum_SendMessages_ErrorSendingMessage, "Error Sending Message:");
            /*User Profile Page*/
            en_us.AddText(EnumText.enumForum_UserProfile_FiledScore, "Score:");
            en_us.AddText(EnumText.enumForum_UserProfile_FiledReputation, "Reputation:");
            en_us.AddText(EnumText.enumForum_UserProfile_TitleSendMessage, "Send Message");
            en_us.AddText(EnumText.enumForum_UserProfile_ButtonClose, "[Close]");
            /*Topic Page*/
            en_us.AddText(EnumText.enumForum_Topic_ErrorYouShouldChooseOnePoll, "You Should Choose One Poll");
            en_us.AddText(EnumText.enumForum_Topic_ErrorMulitipleChoiceIs, "Mulitiple Choice Is");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipSubscribe, "Subscribe");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipUnsubscrbe, "Unsubscrbe");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipFavorite, "Favorite");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipUnfavorite, "Unfavorite");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmFeaturedThisTopic, "Are you sure to Featured this Topic?");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmUnfeaturedThisTopic, "Are you sure to Unfeatured this Topic?");
            en_us.AddText(EnumText.enumForum_Topic_FiledPollDateTo, "Poll Date To");
            en_us.AddText(EnumText.enumForum_Topic_FiledMulitipleChoice, "Mulitiple Choice");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipSubmit, "Submit");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipView, "View");
            en_us.AddText(EnumText.enumForum_Topic_FiledAttachment, "Attachment");
            en_us.AddText(EnumText.enumForum_Topic_FiledUploadAttachment, "Upload Attachment:");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipDelete, "Delete");
            en_us.AddText(EnumText.enumForum_Topic_TitleDownload, "Download");
            en_us.AddText(EnumText.enumForum_Topic_ErrorPleaseInputOneNumber, "Please Input One Number");
            en_us.AddText(EnumText.enumForum_Topic_ErrorPayForDownloadIsRequired, "Pay For Download Is Required");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipDescription, "Description");
            en_us.AddText(EnumText.enumForum_Topic_ButtonUpload, "Upload");
            en_us.AddText(EnumText.enumForum_Topic_SubTitleOption, "Option");
            en_us.AddText(EnumText.enumForum_Topic_FiledPecent, "Pecent");
            en_us.AddText(EnumText.enumForum_Topic_FiledPer, "Per");
            en_us.AddText(EnumText.enumForum_Topic_SubTitleTotalNumJoinTheVote, "Total Num Join The Vote");
            en_us.AddText(EnumText.enumForum_Topic_ButtonClose, "[Close]");
            en_us.AddText(EnumText.enumForum_Topic_TitleSendMessage, "Send Message");
            //code
            en_us.AddText(EnumText.enumForum_Topic_ErrorSubscribingTopic, "Error Subscribing Topic:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorUnsubscribingTopic, "Error Unsubscribing Topic:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorFavoritingTopic, "Error Favoriting Topic:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorUnfavoritingTopic, "Error Unfavoriting Topic:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorFeaturedingTopic, "Error Featureding Topic:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorUnfeaturedTopic, "Error Unfeatured Topic:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorLoadingAttachmentList, "Error Loading Attachment List:");
            en_us.AddText(EnumText.enumForum_Topic_ThisTopicHaveBeenMovedToThe, "This Topic Have Been Moved To The");
            en_us.AddText(EnumText.enumForum_Topic_ToViewThisTopicPleaseClickTheUrl, "To View This Topic Please Click The Url");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmPayScoresForThisTopic, "are you sure to Pay Scores for this Topic?");
            en_us.AddText(EnumText.enumForum_Topic_HaveEnoughScoresViewTopic, "you can view topic until you pay for this topic ,you have {0} scores now.");
            en_us.AddText(EnumText.enumForum_Topic_HaveNotEnoughScoresViewTopic, "you can view topic until you pay for this topic, you noly have {0} scores now.");
            en_us.AddText(EnumText.enumForum_Topic_LoginAndViewTopic, "you can view topic until you Logined and pay it.");
            en_us.AddText(EnumText.enumForum_Topic_ReplyAndViewTpoic, "you can view topic until you Reply this tpoic.");
            en_us.AddText(EnumText.enumForum_Topic_FiledScore, "Score");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipReputation, "Reputation");
            en_us.AddText(EnumText.enumForum_Topic_SpiltLine, "*********************************");
            en_us.AddText(EnumText.enumForum_Topic_PostUnverified, "Post Unverified");
            en_us.AddText(EnumText.enumForum_Topic_WaitingForModeration, "Waiting For Moderation");
            en_us.AddText(EnumText.enumForum_Topic_ThisIsASpam, "This Is A Spam");
            en_us.AddText(EnumText.enumForum_Topic_ThisIsAabusedPost, "This Is Aabused Post");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipAbuseThisPost, "Abuse This Post");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmApproveTheAbuse, "are you sure to Approve this abused post?");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipApproveAbuse, "Approve Abuse");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmRefuseTheAbuse, "are you sure to Refuse this abused post?");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipRefuseAbuse, "Refuse Abuse");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmApprovalThisPost, "are you sure to Approval this moderated post?");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipApprovalPost, "Approval Post");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmUnapprovalThisPost, "are you sure to Unapproval this moderated post?");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipUnapprovalPost, "Unapproval Post");
            en_us.AddText(EnumText.enumForum_Topic_ErrorUploadingAttahment, "Error Uploading Attahment:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorLoadingPostingAttachmentsList, "Error Loading Posting Attachments List:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorDeletingAttachment, "Error Deleting Attachment:");
            #endregion

            #endregion Forum                        

            #region Exception
            #region Public
            en_us.AddExceptionText(EnumErrorCode.enumPublicDateFormatWrong, "Date format is wrong.");
            en_us.AddExceptionText(EnumErrorCode.enumPublicTimeFormatWrong, "Time format is wrong.");
            en_us.AddExceptionText(EnumErrorCode.enumPublicDateTimeFormatWrong, "Date or time format is wrong.");
            en_us.AddExceptionText(EnumErrorCode.enumDateFromEarlierThanToDate, "The 'To' time can not be earlier than the 'From' time.");
            en_us.AddExceptionText(EnumErrorCode.PublicDateToLaterThanNow, "The 'To' time can not be later than 'now' ");
            en_us.AddExceptionText(EnumErrorCode.enumPublicInputStringIsNotAnInteger, "{0} is not an integer.");
            #endregion

            #region System
            en_us.AddExceptionText(EnumErrorCode.enumSystemNoError, "Successful.");
            en_us.AddExceptionText(EnumErrorCode.enumSystemException, "System Error.");
            en_us.AddExceptionText(EnumErrorCode.enumSystemFieldLengthExceeded, "The length of {0} cannot be greater than {1} characters.");
            en_us.AddExceptionText(EnumErrorCode.enumSystemFieldCanNotBeNull, "The Field {0} can not be null.");
            en_us.AddExceptionText(EnumErrorCode.enumSystemFieldNotEqual, "The Field of {0} should be equal to {1} characters.");
            en_us.AddExceptionText(EnumErrorCode.enumSystemNotEnoughPermission, "Operator {0} does not have the permission.");
            en_us.AddExceptionText(EnumErrorCode.enumSystemSessionTimeOut, "Time is out for the session with name {0}.");
            en_us.AddExceptionText(EnumErrorCode.enumSystemQuerystringNull, "Querystring with key {0} is null.");
            en_us.AddExceptionText(EnumErrorCode.enumSystemRegisterFaild, "Registration failed.");
            en_us.AddExceptionText(EnumErrorCode.enumSystemProductTypeNotCompatible, "Product type is not compatible.");
            en_us.AddExceptionText(EnumErrorCode.enumSystemProductVersionNotCompatible, "Product version is not compatible.");
            en_us.AddExceptionText(EnumErrorCode.enumSystemDatabaseVersionNotCompatible, "Database version is not compatible.");
            en_us.AddExceptionText(EnumErrorCode.enumSystemAttachmentsLengthExceeded, "The maximum attachments size is 50 MB.");
            #endregion

            #region Admin
            //have translation
            en_us.AddExceptionText(EnumErrorCode.enumSiteNotExist, "Site does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumStyleTemplateNotExist, "Style template with Id '{0}' does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumSiteNotExistWithEmailAndPassword, "Email or password is wrong.");
            en_us.AddExceptionText(EnumErrorCode.enumSiteEmailCanNotBeDuplicated, "Email has already been used, please try another one.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorNotLogin, "Operator has not logged in.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorEmailNotUnique, "Email of Operator is not unique.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorNameNotUnique, "Name of Operator is not unique.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorNotExist, "Operator with Id '{0}' does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorDeleted, "Operator with Id '{0}' has been deleted.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorNoAdmin, "There should be at least one active administrator.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorNotExistWithSiteIdAndEmailAndPWD, "Site Id, Email or password is wrong.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorNotExistWithSiteIdAndEmail, "Site Id or Email is wrong.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorNotExistWithOperatorIdAndSiteId, "Operator \"{0}\" dose not exist in site with Id '{1}'.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorNotExistWithEmail, "Operator with Email '{0}' does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorHasBeenDeletedWithId, "Operator with Id '{0}' has been deleted.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorHasBeenDeletedWithEmail, "Operator with Email '{0}' has been deleted.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorNotActiveWithEmail, "Operator with Email '{0}' is not active.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorNotActiveWithId, "Operator with Id '{0}' is not active.");
            //en_us.AddExceptionText(EnumErrorCode.enumOperatorNotExistWithSiteIdAndEmail, "Operator with Site Id '{0}' and Email '{1}' does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorCannotDeleteYourself, "You can not delete yourself.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorCannotDisableYourself, "You can not disable yourself.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorOldPasswordIsWrong, "The current password is incorrect.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorEmailNotUnique, "Email of Operator is not unique.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorDisplayNameNotUnique, "Display Name of Operator is not unique.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorRestePasswordTimeExpired, "Time expired, please submit the reset password page again.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorForgetPasswordTagIsIncorrect, "The action is wrong, please submit the reset password page again.");
            en_us.AddExceptionText(EnumErrorCode.enumOperatorAlreadyResetPasswordForForgettingPassword, "You already reset password, please submit the reset password page again.");

            #endregion

            #region Forum Helper 1
            en_us.AddExceptionText(EnumErrorCode.enumCategoryNotExist, "Category with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumCategoryIsUsing, "Category with Id {0} is currently in use. To delete this category, please delete all the Forums under this category first!");
            en_us.AddExceptionText(EnumErrorCode.enumDraftNotExist, "Draft with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enmuDraftNotExistInTopic, "Topic with Id {0} does not have a draft.");
            en_us.AddExceptionText(EnumErrorCode.enumTopicNotExist, "Topic with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumTopicIsClosed, "Topic with Id {0} has been closed.");
            en_us.AddExceptionText(EnumErrorCode.enumTopicHasBeenMoved, "Topic with Id {0} has been moved.");
            en_us.AddExceptionText(EnumErrorCode.enumAnnouncementNotExsit, "Annoucement with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumAnswerNotExit, "Topic with Id {0} does not have an answer.");
            en_us.AddExceptionText(EnumErrorCode.enumForumNotExist, "Forum with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumIsHidden, "Forum with Id {0} has been hidden.");
            en_us.AddExceptionText(EnumErrorCode.enumForumIsLocked, "Forum with Id {0} has been locked.");
            en_us.AddExceptionText(EnumErrorCode.enumForumPostNeedingPayTopicNotAllow, "This forum does not allow posting the \"Score Needed to View\" topic.");
            en_us.AddExceptionText(EnumErrorCode.enumForumPostNeedingReplayTopicNotAllow, "This forum does not allow posting the \"Reply Needed to View\" topic.");
            en_us.AddExceptionText(EnumErrorCode.enumAvatarFileNotExists, "File with Path '{0}' does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumAvatarFileSizeIsTooLarge, "The size of the file with Name '{0}' is too large.");
            en_us.AddExceptionText(EnumErrorCode.enumAvatarFormatError, "The format of the file with Name'{0}' is incorrect.");
            en_us.AddExceptionText(EnumErrorCode.enumUserEmailNotUnique, "Email is not unique.");
            en_us.AddExceptionText(EnumErrorCode.enumUserDisplayNameNotUnique, "Display Name is not unique.");
            en_us.AddExceptionText(EnumErrorCode.enumForumMessageReceiverIsRequired, "Message Receiver is required.");
            en_us.AddExceptionText(EnumErrorCode.enumUserPasswordError, "The current password is incorrect. Please re-enter your password.");
            en_us.AddExceptionText(EnumErrorCode.enumUserNotExistWithSiteIdAndEmail, "User with Site Id {0} and Email '{1}' does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumUserNotExistWithSiteIdAndEmailAndPWD, "Site Id, Email or password is incorrect.");
            en_us.AddExceptionText(EnumErrorCode.enumUserNotEmailVerificated, "Your email has not been verified.");
            en_us.AddExceptionText(EnumErrorCode.enumUserNotModerated, "Your email has not got through moderation.");
            en_us.AddExceptionText(EnumErrorCode.enumUserRefused, "Your email has been refused.");
            en_us.AddExceptionText(EnumErrorCode.enumUserIdNotExist, "User with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumUserNotExistWithEmail, "User with Email '{0}' does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumUserHasBeenDeletedWithId, "User with Id {0} has been deleted.");
            en_us.AddExceptionText(EnumErrorCode.enumUserNotActiveWithEmail, "User with Email '{0}' is inactive.");
            en_us.AddExceptionText(EnumErrorCode.enumUserNotActiveWithId, "User with Id {0} is inactive.");
            en_us.AddExceptionText(EnumErrorCode.enumUserNotLogin, "User has not logged in.");
            en_us.AddExceptionText(EnumErrorCode.ForumOperatingUserOrOperatorCanNotBeNull, "User has not logged in.");
            en_us.AddExceptionText(EnumErrorCode.enumUserEmailFormatWrong, "Email format is incorrect.");
            en_us.AddExceptionText(EnumErrorCode.enumUserDisplayNameFormatWrong, "Display Name format is incorrect.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserOrOperatorScoreIsNotEnough, "Your score is insufficient to perform this operation.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserOrOperatorHavePaid, "You have already paid scores for this topic or attachment.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserOrOperatorHaveNotPaid, "You have not paid scores for this topic or attachment.");
            en_us.AddExceptionText(EnumErrorCode.enumUserIllegalDispalyName, "Display Name '{0}' is illegal.");
            en_us.AddExceptionText(EnumErrorCode.enumUserDisplayNameLength, "The length of Display Name should between {0} ~ {1} characters.");
            en_us.AddExceptionText(EnumErrorCode.enumUserDisplayNameFormatErrorAndShowInstruction, "Display Name format error: ({0}).");
            en_us.AddExceptionText(EnumErrorCode.enumForumRegisterFailed, "Register failed.");
            en_us.AddExceptionText(EnumErrorCode.enumForumEmailVerificationGuidTagWrong, "Email verification GUID tag is incorrect.");
            en_us.AddExceptionText(EnumErrorCode.enumForumRegisterEmailRepeated, "This email has already been registered.");
            en_us.AddExceptionText(EnumErrorCode.enumForumRegisterNameRepeated, "This display name has already been registered.");
            en_us.AddExceptionText(EnumErrorCode.enumForumRegisterEmailOrNameRepeated, "This email or display name has already been registered.");
            en_us.AddExceptionText(EnumErrorCode.enumForumRegisterSendVerificationEmailFailed, "Send registration verification email failed.");
            en_us.AddExceptionText(EnumErrorCode.enumPostNotExist, "Post with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumPostNotInRecycleBin, "This post has been restored or permanently deleted .");
            en_us.AddExceptionText(EnumErrorCode.enumForumPostNotWaitingForModeration, "This post has got through moderation or has been rejected.");
            en_us.AddExceptionText(EnumErrorCode.enumForumPostNotWaingForModerationOrRejected, "This post has got through moderation or has been rejected.");
            en_us.AddExceptionText(EnumErrorCode.enumForumPostModerationStautsIsRejected, "You cannot edit this post because this post has been rejected by forum administrator or moderator.");
            en_us.AddExceptionText(EnumErrorCode.enumForumPostAbuseStautsIsAbusedAndApproved, "You cannot edit this post because this post has been confirned as spam");

            en_us.AddExceptionText(EnumErrorCode.enumForumTopicNotInRecycleBin, "This topic has been either restored or permanently deleted. You cannot perform this operation.");
            en_us.AddExceptionText(EnumErrorCode.enumTopicFirstPostNotExist, "First Post of the Topic with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumTopicLastPostNotExist, "Last Post of the Topic with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserNotExistWithSiteIdAndEmailAndPWD, "Your email or password is incorrect.");
            en_us.AddExceptionText(EnumErrorCode.enumPostImageNotExist, "Image with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumPostImageFileNotExists, "File with Path '{0}' does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumPostImageFileSizeIsTooLarge, "The size of the file with Name '{0}' is too large.");
            en_us.AddExceptionText(EnumErrorCode.enumPostImageFormatError, "The format of the file with Name'{0}' is incorrect.");
            en_us.AddExceptionText(EnumErrorCode.enumForumAbuseNotExist, "Abuse with Id {0} dose not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumPostIsAbuseing, "The post you reported is still waiting for moderation.");
            en_us.AddExceptionText(EnumErrorCode.enumForumPostNotAbused, "The reported post has been verified as a spam or has been refused.");
            en_us.AddExceptionText(EnumErrorCode.enumForumPollNotExist, "Poll does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumPollHaveVoted, "You have already voted this poll.");
            en_us.AddExceptionText(EnumErrorCode.enumForumPollHaveExpired, "You are not allowed to vote this poll because the poll has been expired.");
            en_us.AddExceptionText(EnumErrorCode.enumForumPostNoPermission, "You are not allowed to visit this forum.");
            en_us.AddExceptionText(EnumErrorCode.enumForumPollOptionCountLessThanMaxChoice, "Max Choices MUST NOT exceed the total number of poll options.");
            en_us.AddExceptionText(EnumErrorCode.enumForumAttachmentNotExist, "Attachment with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumFavoriteNotExist, "Favorite with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumFavoriteIsExist, "This topic has already been added into Favorites.");
            en_us.AddExceptionText(EnumErrorCode.enumForumSubscribeNotExistInTopic, "You never subscribe this topic.");
            en_us.AddExceptionText(EnumErrorCode.enumForumInMessageNotExist, "Message with Id {0} in InBox does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumOutMessageNotExist, "Message with Id {0} in OutBox does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserOrOperatorNotExistWithId, "User or Operator with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserOrOperatorNotExistWithEmail, "User or Operator with Email '{0}' does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserOrOperatorNotExistWithName, "User or Operator with Display Name '{0}' does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumOperatingUserOrOperatorCanNotBeNull, "Operating user or operator cannot be null.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserOrOperatorNotActiveWithId, "User or Operator with Id {0} is inactive.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserOrOperatorNotActiveWithName, "User or Operator with Display Name '{0}' is inactive.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserOrOperatorNotActiveWithEmail, "User or Operator with Email '{0}' is inactive.");
            en_us.AddExceptionText(EnumErrorCode.enumForumBanNotExist, "Banned User or IP with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumBanCannotAddWithUserId, "User with Id {0} has already been banned.");
            en_us.AddExceptionText(EnumErrorCode.enumForumBanStartIPCannotLargerThanEndIP, "Start IP cannot be larger than End IP.");
            en_us.AddExceptionText(EnumErrorCode.enumForumBanedUserNotExist, "Banned User with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumBanedIPNotExist, "Banned IP with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserGroupNotExistWithId, "User Group with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumReputationGroupNotExistWithId, "Reputation Group with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumAllForumUserGroupCannotBeDeleted, "User Group with Name \"All Registered Users\" does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumPermissionNotExistWithGroupId, "Group Permission with Group Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumPermissionNotExistWithGroupIdAndForumId, "Group Permission with Group Id {0} and Forum Id {1} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumGetPermissionError, "Error getting permission.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserGroupOfForumHaveExistedWithGroupIdAndForumId, "User Group with Group Id {0} and Forum Id {1} has already existed.");
            en_us.AddExceptionText(EnumErrorCode.enumForumReputationGroupOfForumHaveExistedWithGroupIdAndForumId, "Reputation Group with Group Id {0} and Forum Id {1} has already existed.");
            en_us.AddExceptionText(EnumErrorCode.enumForumReputationGroupInvalidReputationRange, "The reputation range is invalid.");
            en_us.AddExceptionText(EnumErrorCode.enumForumReputationGroupRepititiveRange, "Reputation Group with this reputation range has already existed.");
            en_us.AddExceptionText(EnumErrorCode.enumForumGroupIsNotReputationGroupWithId, "Group with Id {0} is not a reputation group.");
            en_us.AddExceptionText(EnumErrorCode.enumForumSettingsCloseMessageFunction, "Message function is disabled.");
            en_us.AddExceptionText(EnumErrorCode.enumForumSettingsCloseFavoriteFunction, "Favorites function is disabled.");
            en_us.AddExceptionText(EnumErrorCode.enumForumSettingsCloseSubscribeFunction, "Subscribe function is disabled.");
            en_us.AddExceptionText(EnumErrorCode.enumForumSettingsCloseHotTopicFunction, "Hot Topic function is disabled.");
            en_us.AddExceptionText(EnumErrorCode.enumForumSettingsCloseGroupPermissionFunction, "User Group Permission function is disabled.");
            en_us.AddExceptionText(EnumErrorCode.enumForumSettingsCloseReputationPermissionFunction, "Reputation Group Permission function is disabled.");
            en_us.AddExceptionText(EnumErrorCode.enumForumSettingsCloseScoreFunction, "Score function is disabled.");
            en_us.AddExceptionText(EnumErrorCode.enumForumSettingsCloseReputationFunction, "Reputation function is disabled.");
            en_us.AddExceptionText(EnumErrorCode.enumForumSettingsCloseGroupPermissionAndReputationGroup, "User Group Permission function and Reputation Group Permission function are disabled.");
            en_us.AddExceptionText(EnumErrorCode.enumForumFeatureDisableReputationPermission, "Reputation Group Permission will be available only after you have checked 'Enable Reputation'.");
            en_us.AddExceptionText(EnumErrorCode.enumForumFeatureNotExist, "Forum Feature does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumGuestUserPermissionSettingNotExist, "Forum Guest User Permission does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumHotTopicStrategySettingNotExist, "Forum Hot Topic Strategy does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumProhibitedWordsSettingNotExist, "Forum Prohibited Words does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumRegistrationSettingNotExist, "Forum Registration Setting does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumReputationStrategySettingNotExist, "Forum Reputation Strategy does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumScoreStrategySettingNotExist, "Forum Score Strategy does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumSiteSettingNotExist, "Forum Site Setting does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumStyleSettingNotExist, "Forum Style Setting does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserPermissionSettingNotExist, "Forum User Permission Settings does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumRegistrationSettingDisplayNameMinLengthLargerThanMaxLength, "Min length of Display Name must be less than Max length of Display Name.");
            en_us.AddExceptionText(EnumErrorCode.enumForumSMTPSettingsNotExist, "Forum SMTP Settings does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumSMTPAuthenticationRequiredUserNameAndPassword, "With Forum SMTP Authentication Required checked, User Name and Password are required.");
            en_us.AddExceptionText(EnumErrorCode.enumForumSiteIsVisitOnly, "You are not allowed to post because this forum site is only for visit. ");
            en_us.AddExceptionText(EnumErrorCode.enumForumAdministratorNotExistWithId, "Administrator with Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserCannotDeleteHimSelf, "You can't delete yourself.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserOnlyOneAdministrator, "Forum only has one administrator.");
            en_us.AddExceptionText(EnumErrorCode.enumForumMemberOfGroupNotExistWithUserIdAndGroupId, "Member with User Id {0} and Group Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumModeratorOfForumNotExistWithUserIdAndForumId, "Moderator with User Id {0} and Forum Id {0} does not exist.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUnSubscribeSingleTopicEmailMisMatch, "Unsubscribe failed because you did not subscribe this topic.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserHaveNoPermissionToVisit, "You do not have permission to view this page or perform this operation.");
            en_us.AddExceptionText(EnumErrorCode.enumForumOnlyAdministratorsHavePermission, "Only forum administrators have the permission.");
            en_us.AddExceptionText(EnumErrorCode.enumForumOnlyModeratorsHavePermission, "Only moderators have the permission.");
            en_us.AddExceptionText(EnumErrorCode.enumForumOnlyModeratorsOrAdminstratorsHavePermission, "Only moderators or forum administrators have the permission.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUseOrOperatorHaveBeenBanned, "User or Operator with Display Name '{0}' has been banned.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUseOrOperatorHaveBeenModerated, "User or Operator with Display Name '{0}' has got through moderation.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionViewForum, "You are not allowed to visit the forum.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionAllowViewTopicOrPost, "You are not allowed to view the topics or posts.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionAllowPostTopicOrPost, "You are not allowed to post the topic or post.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionAllowCustomizeAvatar, "You are not allowed to customize your avatar.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionMaxLengthofSignature, "The total length of your signature cannot exceed {0}.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionMinIntervalTimeforPosting, "You are not allowed to post again within {0} s.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionMaxLengthofTopicOrPost, "The total length of the topic or post you can post cannot exceed {0}.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionAllowHTML, "You are not allowed to post by using HTML.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionAllowURL, "You are not allowed to insert links into the topics or posts when posting.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionAllowInsertImage, "You are not allowed to insert images into the topics or posts when posting.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionAllowAttachment, "You are not allowed to upload attachments.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionMaxAttachmentsinOnePost, "The number of the attachments you can upload in one post cannot exceed {0}.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionMaxSizeoftheAttachment, "The size of each attachment you can upload cannot exceed {0} K.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionMaxSizeofalltheAttachments, "The size of all the attachments you can upload cannot exceed {0} K.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionMaxMessagesSentinOneDay, "The number of all the messages you can send in one day cannot exceed {0}");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionAllowSearch, "You are not allowed to use search function to search for any topics or posts.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionMinIntervalTimeforSearching, "You are not allowed to search again within {0} s.");
            en_us.AddExceptionText(EnumErrorCode.enumForumUserWithoutPermissionPostModerationNotRequired, "You are not allowed to post \"Post Moderation Not Required\" topic or post.");

            #endregion

            #endregion Exception

            #region Function
            en_us.AddFunctionText(EnumFunctionLanguageCode.DateTimeHelper_DateTransferToString_Hours, "{0} Hours ago");
            en_us.AddFunctionText(EnumFunctionLanguageCode.DateTimeHelper_DateTransferToString_Hour, "1 Hour ago");
            en_us.AddFunctionText(EnumFunctionLanguageCode.DateTimeHelper_DateTransferToString_Minute, "1 Minute ago");
            en_us.AddFunctionText(EnumFunctionLanguageCode.DateTimeHelper_DateTransferToString_Minutes, "{0} Minutes ago");
            en_us.AddFunctionText(EnumFunctionLanguageCode.ASPNetPager_DateTransferToString_PageTop, "");//Page {0} of {1}[{2}{3}]
            #endregion
            #endregion en-us

        }


        #region Business

        public static EnumLanguage GetCurrentLanguageEnum()
        {
            return EnumLanguage.enumEnglish;
        }

        public static string GetLanguageName(EnumLanguage language)
        {
            string languageName = "en-US";
            return languageName;
        }

        public static string GetDisplayNameByLanguage(string firstName, string lastName, EnumLanguage language)
        {
            string displayName =  string.Format("{0} {1}", firstName, lastName);

            switch (language)
            {
                case EnumLanguage.enumEnglish:
                    displayName = string.Format("{0} {1}", firstName, lastName);
                    break;
                case EnumLanguage.enumSimplifiedChinese:
                    displayName = string.Format("{0}{1}", lastName, firstName);
                    break;
                case EnumLanguage.enumSpanish:
                    displayName = string.Format("{0} {1}", firstName, lastName);
                    break;
                case EnumLanguage.enumJapaness:
                    displayName = string.Format("{0}{1}", lastName, firstName);
                    break;
                case EnumLanguage.enumFrench:
                    displayName = string.Format("{0} {1}", firstName, lastName);
                    break;
                case EnumLanguage.enumGerman:
                    displayName = string.Format("{0}{1}", firstName, lastName);
                    break;
                default:
                    displayName = string.Format("{0} {1}", firstName, lastName);
                    break;
            }

            return displayName;
        }

        private string GetText(EnumLanguage enumLanguage, EnumText enumText)
        {
            if (LanguageHelper.LanguageList[(int)enumLanguage] == null)
            {
                enumLanguage = EnumLanguage.enumEnglish;
            }

            return LanguageHelper.LanguageList[(int)enumLanguage].GetText(enumText);
        }

        public string GetExceptionText(EnumLanguage enumLanguage, EnumErrorCode enumErrorCode)
        {
            if (LanguageHelper.LanguageList[(int)enumLanguage] == null)
            {
                enumLanguage = EnumLanguage.enumEnglish;
            }

            return LanguageHelper.LanguageList[(int)enumLanguage].GetExceptionText(enumErrorCode);
        }



        #endregion Business
    }
}
