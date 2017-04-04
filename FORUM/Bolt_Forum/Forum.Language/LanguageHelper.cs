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

namespace Com.Comm100.Forum.Language
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

            Language en_us = new Language(Com.Comm100.Language.EnumLanguage.enumEnglish, maxTextId);
            LanguageList[0] = en_us;

            #region Forum

            #region Forum Public 24

            en_us.AddText(EnumText.enumForum_Public_SiteId, "Site Id");
            en_us.AddText(EnumText.enumForum_Public_DeletedUser, "a deleted user");
            en_us.AddText(EnumText.enumForum_Public_Help, "Help");
            en_us.AddText(EnumText.enumForum_Public_No, "No");
            en_us.AddText(EnumText.enumForum_Public_RequiredField, "Required Field");
            en_us.AddText(EnumText.enumForum_Public_Yes, "Yes");
            en_us.AddText(EnumText.enumForum_Public_TextAreaMaxLength, "The text that you have entered is too long ('+{0}+' characters). Please shorten it to {1} characters long.");
            en_us.AddText(EnumText.enumForum_Public_UserPanelBrowserTitle, "User Control Panel - {0}");
            en_us.AddText(EnumText.enumForum_Public_CommonBrowerTitle, "{0}");
            en_us.AddText(EnumText.enumForum_Public_TextAbout, "About Me");
            en_us.AddText(EnumText.enumForum_Public_TextStatistics, "Statistics");
            en_us.AddText(EnumText.enumForum_Public_TextCurrentLocation, "(Current Forum)");
            en_us.AddText(EnumText.enumForum_Public_AlertNoPermission, "You do not have permission to perform this operation.");
            en_us.AddText(EnumText.enumForum_Public_TextTop, "Top");
            en_us.AddText(EnumText.enumForum_Public_TextSharp, "#");
            en_us.AddText(EnumText.enumForum_Public_TextPost, "Post Reply");
            en_us.AddText(EnumText.enumForum_Public_LinkCloseSelectForumWindow, "[Close]");
            en_us.AddText(EnumText.enumForum_Public_TextLEGEND, "LEGEND");
            en_us.AddText(EnumText.enumForum_Public_TextRe, "Re:");
            en_us.AddText(EnumText.enumForum_Public_TextAt, "at");
            en_us.AddText(EnumText.enumForum_Public_Post, "Post");
            en_us.AddText(EnumText.enumForum_Public_Posts, "Posts");
            en_us.AddText(EnumText.enumForum_Public_Topic, "Topic");
            en_us.AddText(EnumText.enumForum_Public_Topics, "Topics");
            en_us.AddText(EnumText.enumForum_Public_InitializatingLanguageError, "Error initializing language :");
            en_us.AddText(EnumText.enumForum_Public_RedirectError, "Error redirecting page :");
            en_us.AddText(EnumText.enumForum_Public_ButtonCancel, "Cancel");
            en_us.AddText(EnumText.enumForum_Public_ButtonSave, "Save");
            en_us.AddText(EnumText.enumForum_Public_ButtonQuery, "Query");
            en_us.AddText(EnumText.enumForum_Public_FiledSubjectContent, "Subject/Content:");
            en_us.AddText(EnumText.enumForum_Public_ButtonOk, "OK");
            en_us.AddText(EnumText.enumForum_Public_FieldDisplayName, "Display Name");


            #endregion

            #region Forum Operator 23

            en_us.AddText(EnumText.enumForum_Operator_ConfirmAreYouSureDelete, "Are you sure you want to delete this operator?");
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
            en_us.AddText(EnumText.enumForum_Operator_ErrorEmailInvalid, "Email format is invalid.");
            en_us.AddText(EnumText.enumForum_Operator_ErrorEmailRequired, "Email is required.");
            en_us.AddText(EnumText.enumForum_Operator_ErrorEmailsMatch, "Emails don't match");
            en_us.AddText(EnumText.enumForum_Operator_ErrorFirstNameRequired, "First Name is required.");
            en_us.AddText(EnumText.enumForum_Operator_ErrorLastNameRequired, "Last Name is required.");
            en_us.AddText(EnumText.enumForum_Operator_ErrorNameRequired, "Display Name is required.");
            en_us.AddText(EnumText.enumForum_Operator_ErrorPasswordRequired, "Password is required.");
            en_us.AddText(EnumText.enumForum_Operator_ErrorPasswordsMatch, "Passwords do not match. Please retype.");
            en_us.AddText(EnumText.enumForum_Operator_ErrorRetypeEmailRequired, "Retype Email is required.");
            en_us.AddText(EnumText.enumForum_Operator_ErrorRetypePasswordRequired, "Retype Password is required.");
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
            en_us.AddText(EnumText.enumForum_Operator_HelpDisplayName, "A display name can be used for an operator to identify himself or herself. Display Name must be unique.");
            en_us.AddText(EnumText.enumForum_Operator_HelpEmail, "Email is used for an operator to log in. Email must be unique.");
            en_us.AddText(EnumText.enumForum_Operator_HelpIsActive, "An operator cannot log in when he/she is not active. An inactive operator can be re-activated by an administrator.");
            en_us.AddText(EnumText.enumForum_Operator_HelpIsAdmin, "An administrator has full permission to manage the site. A site must have at least one active administrator.");
            en_us.AddText(EnumText.enumForum_Operator_PageEditErrorLoadingPage, "Error loading  Edit an Operator page:");
            en_us.AddText(EnumText.enumForum_Operator_PageEditErrorRedirectingToOperatorsPage, "Error redirecting to Operators page:");
            en_us.AddText(EnumText.enumForum_Operator_PageEditErrorUpdatingOperator, "Error saving an operator:");
            en_us.AddText(EnumText.enumForum_Operator_PageListErrorDeletingOperator, "Error deleting an operator:");
            en_us.AddText(EnumText.enumForum_Operator_PageListErrorLoadingPage, "Error loading Operators page: ");
            en_us.AddText(EnumText.enumForum_Operator_PageListErrorRedirectingToNewOperatorPage, "Error redirecting to New Operator page:");
            en_us.AddText(EnumText.enumForum_Operator_PageNewErrorCreatingOperator, "Error adding a new operator:");
            en_us.AddText(EnumText.enumForum_Operator_PageNewErrorLoadingPage, "Error loading  New Operator page:");
            en_us.AddText(EnumText.enumForum_Operator_PageNewErrorRedirectingToOperatorsPage, "Error redirecting to Operators page:");
            en_us.AddText(EnumText.enumForum_Operator_PageResetPasswordErrorLoadingPage, "Error loading Reset Password page:");
            en_us.AddText(EnumText.enumForum_Operator_PageResetPasswordErrorRedirectingPage, "Error redirecting to Operators page:");
            en_us.AddText(EnumText.enumForum_Operator_PageResetPasswordErrorResettingPassword, "Error resetting password:");
            en_us.AddText(EnumText.enumForum_Operator_SubtitleEditPage, "Only an administrator has permission to edit an operator's information.");
            en_us.AddText(EnumText.enumForum_Operator_SubtitleListPage, "An operator is an internal user, often a customer service representative. An operator has permission to view all foreground and background info, and has full permission to manage draft. ");
            en_us.AddText(EnumText.enumForum_Operator_SubtitleNewPage, "Add a new operator. Only an administrator has permission to add a new operator.");
            en_us.AddText(EnumText.enumForum_Operator_SubtitleResetPasswordPage, "Reset an operator's password. An administrator can set a new password for an operator without knowing the operator's current password.");
            en_us.AddText(EnumText.enumForum_Operator_TitleEditPage, "Edit Operator");
            en_us.AddText(EnumText.enumForum_Operator_TitleListPage, "Operators");
            en_us.AddText(EnumText.enumForum_Operator_TitleNewPage, "New Operator");
            en_us.AddText(EnumText.enumForum_Operator_TitleResetPasswordPage, "Reset Password");


            #endregion

            #region  Admin Menu 22

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
            en_us.AddText(EnumText.enumForum_AdminMenu_ForumFeature, "Forum Feature");
            en_us.AddText(EnumText.enumForum_AdminMenu_UserGroups, "User Groups");
            en_us.AddText(EnumText.enumForum_AdminMenu_UserGroupsManagement, "General User Groups");
            en_us.AddText(EnumText.enumForum_AdminMenu_Administrators, "Forum Administrators");
            en_us.AddText(EnumText.enumForum_AdminMenu_UsersEmailVerify, "Email Verification");
            en_us.AddText(EnumText.enumForum_AdminMenu_ReputationGroups, "Reputation Groups");
            en_us.AddText(EnumText.enumForum_AdminMenu_Announcements, "Announcements");
            en_us.AddText(EnumText.enumForum_AdminMenu_PostsModeration, "Posts Moderation");
            en_us.AddText(EnumText.enumForum_AdminMenu_WaitingForModeration, "Waiting for Moderation");
            en_us.AddText(EnumText.enumForum_AdminMenu_RejectedPosts, "Rejected Posts");
            en_us.AddText(EnumText.enumForum_AdminMenu_TopicsAndPosts, "Topics & Posts");
            en_us.AddText(EnumText.enumForum_AdminMenu_TopicsManagement, "Topics Management");
            en_us.AddText(EnumText.enumForum_AdminMenu_PostsManagement, "Posts Management");
            en_us.AddText(EnumText.enumForum_AdminMenu_RecycleBin, "Recycle Bin");
            en_us.AddText(EnumText.enumForum_AdminMenu_AbuseReport, "Abuse Report");
            en_us.AddText(EnumText.enumForum_AdminMenu_BannedList, "Banned List");


            #endregion

            #region  Moderator Menu 

            en_us.AddText(EnumText.enumForum_ModeratorMenu_Categories, "Categories");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_CategoriesForums, "Categories & Forums");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_CodePlan, "Code Generation");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_Dashboard, "Dashboard");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_Drafts, "Drafts");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_ForumHome, "Forum Home");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_Forums, "Forums");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_HeaderFooterSettings, "Header & Footer Settings");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_Operators, "Operators");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_RegistrationSettings, "Registration Settings");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_Settings, "Settings");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_SiteSettings, "Site Settings");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_Styles, "Styles");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_TemplateSettings, "Template Settings");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_UsersManagement, "Users Management");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_UsersModeration, "Users Moderation");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_Users, "Users");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_RulesAndPoliciesSettings, "Policy Settings");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_ForumFeature, "Forum Feature");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_UserGroups, "User Groups");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_UserGroupsManagement, "General User Groups");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_Administrators, "Administrators");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_UsersEmailVerify, "Email Verification");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_ReputationGroups, "Reputation Groups");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_Announcements, "Announcements");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_PostsModeration, "Posts Moderation");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_WaitingForModeration, "Waiting for Moderation");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_RejectedPosts, "Rejected Posts");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_TopicsAndPosts, "Topics & Posts");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_TopicsManagement, "Topics Management");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_PostsManagement, "Posts Management");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_RecycleBin, "Recycle Bin");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_AbuseReport, "Abuse Report");
            en_us.AddText(EnumText.enumForum_ModeratorMenu_BannedList, "Banned List");


            #endregion

            #region Forum UserMenu

            en_us.AddText(EnumText.enumForum_UserMenu_Avatar, "Avatar");
            en_us.AddText(EnumText.enumForum_UserMenu_MyPosts, "My Posts");
            en_us.AddText(EnumText.enumForum_UserMenu_MyTopics, "My Topics");
            en_us.AddText(EnumText.enumForum_UserMenu_Password, "Password");
            en_us.AddText(EnumText.enumForum_UserMenu_Profile, "Profile");
            en_us.AddText(EnumText.enumForum_UserMenu_Signature, "Signature");
            en_us.AddText(EnumText.enumForum_UserMenu_UserPanel, "User Control Panel");
            en_us.AddText(EnumText.enumForum_UserMenu_MyFavorites, "My Favorites");
            en_us.AddText(EnumText.enumForum_UserMenu_MySubscribes, "My Subscribes");
            en_us.AddText(EnumText.enumForum_UserMenu_MyMessages, "My Message");
            en_us.AddText(EnumText.enumForum_UserMenu_SendMessage, "Send Message");
            en_us.AddText(EnumText.enumForum_UserMenu_InBox, "InBox");
            en_us.AddText(EnumText.enumForum_UserMenu_OutBox, "OutBox");

            #endregion

            #region User Menu& Header Footer Settings& Forum Navigation Bar 21


            en_us.AddText(EnumText.enumForum_HeaderFooter_AdvancedSearch, "Advanced Search");
            en_us.AddText(EnumText.enumForum_HeaderFooter_Copyright, "<center><a class=\"copyright_link\" href=\"http://www.comm100forum.com/\" target=\"_blank\">Forum</a> Powered by <a href=\"http://www.comm100.com/\" target=\"_blank\">Comm100</a> | Open Source & Free Hosted <a class=\"copyright_link\" href=\"http://www.comm100.com/\" target=\"_blank\">Customer Service</a> Software</center>");
            en_us.AddText(EnumText.enumForum_HeaderFooter_FooterErrorLoad, "Error loading Forum Footer:");
            en_us.AddText(EnumText.enumForum_HeaderFooter_ForumJump, "Forum Jump: ");
            en_us.AddText(EnumText.enumForum_HeaderFooter_HeaderErrorLoading, "Error loading Forum Header:");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkAdminControlPanel, "Admin Control Panel");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkBarErrorLoading, "Error loading Link Bar:");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkHome, "Home");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkLogin, "Login");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkLogout, "Logout[{0}]");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkRegister, "Register");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkSearch, "Search");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkUserControlPanel, "My Post");
            en_us.AddText(EnumText.enumForum_HeaderFooter_SearchText, "Search");
            en_us.AddText(EnumText.enumForum_HeaderFooter_SelectForum, "Select a Forum");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LogoErrorFind, "Error updating the logo: This logo does not exist.");
            en_us.AddText(EnumText.enumForum_HeaderFooter_SearchFromAll, "All");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkModeratorControlPanel, "Moderator Control Panel");
            en_us.AddText(EnumText.enumForum_HeaderFooter_LinkUnReadMessage, "Message[{0}]");


            en_us.AddText(EnumText.enumForum_NavigationBar_AddTopic, "New Topic");
            en_us.AddText(EnumText.enumForum_NavigationBar_AdminPanelLogin, "Admin Login");
            en_us.AddText(EnumText.enumForum_NavigationBar_AdvancedSearch, "Advanced Search");
            en_us.AddText(EnumText.enumForum_NavigationBar_Default, "Home");
            en_us.AddText(EnumText.enumForum_NavigationBar_EditTopicOrPost, "Edit Topic or Post");
            en_us.AddText(EnumText.enumForum_NavigationBar_EmailVerification, "Email Verification");
            en_us.AddText(EnumText.enumForum_NavigationBar_FindPassword, "Find Password");
            en_us.AddText(EnumText.enumForum_NavigationBar_Forum, "Forum");
            en_us.AddText(EnumText.enumForum_NavigationBar_Login, "Login");
            en_us.AddText(EnumText.enumForum_NavigationBar_Register, "Register");
            en_us.AddText(EnumText.enumForum_NavigationBar_ResetPassword, "Reset Password");
            en_us.AddText(EnumText.enumForum_NavigationBar_SearchResult, "Search Result");
            en_us.AddText(EnumText.enumForum_NavigationBar_SendResetPasswordEmail, "Send Reset Password Email");
            en_us.AddText(EnumText.enumForum_NavigationBar_SiteClosed, "Site Closed");
            en_us.AddText(EnumText.enumForum_NavigationBar_UserPanel, "User Control Panel");
            en_us.AddText(EnumText.enumForum_NavigationBar_IPBanned, "IP Banned");
            en_us.AddText(EnumText.enumForum_NavigationBar_UserBanned, "User Banned");
            en_us.AddText(EnumText.enumForum_NavigationBar_UserProfile, "User Profile");
            en_us.AddText(EnumText.enumForum_NacigationBar_UserPosts, "User Posts");


            #endregion

            #region Forum User Group Manangement 20

            en_us.AddText(EnumText.enumForum_UserGroups_TitleUserGroupsManagement, "General User Groups");
            en_us.AddText(EnumText.enumForum_UserGroups_SubtitleUserGroupsManagement, "User Group is a grouping of your forum users. Administrators can create a group, edit or delete an existing group, and grant permissions to a selected group. <br/><br/>Users inherit all the permissions assigned to the group they belong to. If a user is added into more than one group, he/she will inherit all the permissions from each and every user group he/she belongs to. Until being deleted from the administrator list, administrators always have full access permissions to the forum. ");
            en_us.AddText(EnumText.enumForum_UserGroups_PageUserGroupsManagementErrorCount, "Error getting count of members:");
            en_us.AddText(EnumText.enumForum_UserGroups_PageUserGroupsManagementErrorLoad, "Error loading General User Groups page:");
            en_us.AddText(EnumText.enumForum_UserGroups_PageUserGroupsManagementErrorDelete, "Error deleting a user group:");
            en_us.AddText(EnumText.enumForum_UserGroups_ColumnId, "Id");
            en_us.AddText(EnumText.enumForum_UserGroups_ColumnName, "Name");
            en_us.AddText(EnumText.enumForum_UserGroups_ColumnDescription, "Description");
            en_us.AddText(EnumText.enumForum_UserGroups_ColumnMemebers, "Members");
            en_us.AddText(EnumText.enumForum_UserGroups_ColumnPermissions, "Permissions");
            en_us.AddText(EnumText.enumForum_UserGroups_ColumnOperation, "Operation");
            en_us.AddText(EnumText.enumForum_UserGroups_FieldRequired, "*Required Field");
            en_us.AddText(EnumText.enumForum_UserGroups_ButtonNewUsergroup, "New User Group");
            en_us.AddText(EnumText.enumForum_UserGroups_ButtonSave, "Save");
            en_us.AddText(EnumText.enumForum_UserGroups_ButtonCancel, "Cancel");
            en_us.AddText(EnumText.enumForum_UserGroups_TitleNewUsergroup, "New User Group");
            en_us.AddText(EnumText.enumForum_UserGroups_SubtitleNewUsergroup, "Add a new user group. Only administrators have permission to add a new user group.");
            en_us.AddText(EnumText.enumForum_UserGroups_PageNewUserErrorLoad, "Error loading New User Group page:");
            en_us.AddText(EnumText.enumForum_UserGroups_ErrorName, "Name is required");
            en_us.AddText(EnumText.enumForum_UserGroups_ErrorSameName, "The user group with this name has already existed. Please enter another name for this user group.");
            en_us.AddText(EnumText.enumForum_UserGroups_PageNewUsergroupErrorAdd, "Error adding a new user group:");
            en_us.AddText(EnumText.enumForum_UserGroups_TitleEditUsergroup, "Edit a User Group");
            en_us.AddText(EnumText.enumForum_UserGroups_SubtitleEditUsergroup, "Here you can edit the name and description of  the selected user group.");
            en_us.AddText(EnumText.enumForum_UserGroups_PageEditUsergroupErrorLoad, "Error loading Edit a User Group page:");
            en_us.AddText(EnumText.enumForum_UserGroups_PageEditUsergroupErrorEdit, "Error editing a user group:");
            en_us.AddText(EnumText.enumForum_UserGroups_TitleMembersManagement, "User Group Members Management");
            en_us.AddText(EnumText.enumForum_UserGroups_SubtitleMembersManagement, "On this page, forum administrators can add new members to this user group, view the users' detailed info, delete the existing members in this user group as well as query for the specific users.");
            en_us.AddText(EnumText.enumForum_UserGroups_ButtonQuery, "Query");
            en_us.AddText(EnumText.enumForum_UserGroups_ButtonAdd, "Add");
            en_us.AddText(EnumText.enumForum_UserGroups_PageMembersManagementErrorLoad, "Error loading User Group Members Management page:");
            en_us.AddText(EnumText.enumForum_UserGroups_PageMembersManagementErrorDelete, "Error deleting a user from this user group:");
            en_us.AddText(EnumText.enumForum_UserGroups_PageMembersManagementErrorGet, "Error getting members:");
            en_us.AddText(EnumText.enumForum_UserGroups_PageMembersManagementErrorQuery, "Error querying members:");
            en_us.AddText(EnumText.enumForum_UserGroups_PageMembersManagementErrorAdd, "Error adding members to this user group:");
            en_us.AddText(EnumText.enumForum_UserGroups_PageMembersManagementSuccessAdd, "The selected user(s) have been added to this user group successfully.");
            en_us.AddText(EnumText.enumForum_UserGroups_FieldCurrentUserGroup, "Current User Group:");
            en_us.AddText(EnumText.enumForum_UserGroups_FieldEmailOrDisplayName, "Email/Display Name: ");
            en_us.AddText(EnumText.enumFourm_UserGroups_ConfirmDeleteMember, "Are you sure you want to delete this user from this user group?");
            en_us.AddText(EnumText.enumForum_UserGroups_ConfirmDeleteUserGroup, "Are you sure you want to delete this user group?");
            en_us.AddText(EnumText.enumForum_UserGroups_HelpPermissions, "Permissions");
            en_us.AddText(EnumText.enumForum_UserGroups_HelpDelete, "Delete");
            en_us.AddText(EnumText.enumForum_UserGroups_HelpEdit, "Edit");
            en_us.AddText(EnumText.enumForum_UserGroups_ButtonAddMembers, "Add Members");
            en_us.AddText(EnumText.enumForum_UserGroups_ColumnView, "View");
            en_us.AddText(EnumText.enumForum_UserGroups_ColumnDelete, "Delete");
            en_us.AddText(EnumText.enumForum_UserGroups_TitleAddMemgers, "Add Members");
            en_us.AddText(EnumText.enumForum_UserGroups_FieldClose, "Close");
            en_us.AddText(EnumText.enumForum_UserGroups_FieldUserType, "User Type:");
            en_us.AddText(EnumText.enumForum_UserGroups_ColumnUserType, "User Type");
            en_us.AddText(EnumText.enumForum_UserGroups_TitlePermissionSettings, "User Group Permission Settings");
            en_us.AddText(EnumText.enumForum_UserGroups_SubtitlePermissionSettings, "Permission defines a user's Can-do and Can't-do. As a forum administrator, you can set the permissions for the users in this user group.");
            en_us.AddText(EnumText.enumForum_UserGroups_PagePermissionSettingsErrorLoad, "Error loading User Group Permission Settings page:");
            en_us.AddText(EnumText.enumForum_UserGroups_PagePermissionSettingsErrorEdit, "Error editing permissions:");
            en_us.AddText(EnumText.enumForum_UserGroups_FieldPermissions, "Permissions:");

            //en_us.AddText(EnumText.enumForum_UserGroups_FieldAllowViewForum,"Allow Visit Forum:");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpAllowViewForum,"With this option checked, a user in this group can view the topic list on your forum.");
            //en_us.AddText(EnumText.enumForum_UserGroups_FieldAllowViewTopic,"Allow View Topic/Post:");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpAllowViewTopic,"With this option checked, a user in this group can view the topics and posts on your forum.");
            //en_us.AddText(EnumText.enumForum_UserGroups_FieldAllowPostTopic,"Allow Post Topic/Post:");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpAllowPostTopic,"With this option checked, a user in this group can post topics and posts on your forum.");
            //en_us.AddText(EnumText.enumForum_UserGroups_FieldAllowCustomizeAvatar,"Allow Customize Avatar:");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpAllowCustomizeAvatar,"With this option checked, a user in this group can customize his or her own avatar by either uploading an avatar from local file or choosing one from the default ones.");
            //en_us.AddText(EnumText.enumForum_UserGroups_FieldMaxSignature,"Max length of Signature:");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpMaxSignature,"Here you can set a maximum length of the signature for a user in this group. After your setting, the total length of a user's signature cannot exceed the length you've set.");
            //en_us.AddText(EnumText.enumForum_UserGroups_FieldMinIntervalPost,"Min Post Interval Time");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpMinIntervalPost,"Here you can set a minimum interval time for posting for a user in this group. After your setting, a user cannot post again within the time range you've set.");
            //en_us.AddText(EnumText.enumForum_UserGroups_FieldMaxPostLength,"Max Length of Topic/Post:");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpMaxPostLength,"Here you can set a maximum length of  the topic or post for a user in this group. After your setting, the total length of a user's topic or post cannot exceed the length you've set.");
            //en_us.AddText(EnumText.enumForum_UserGroups_FieldHtml,"Allow HTML:");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpHtml,"With this option checked, a user in this group can post a topic or post by using HTML editor.");
            //en_us.AddText(EnumText.enumForum_UserGroups_FieldURL,"Allow Insert Link:");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpURL,"With this option checked, a user in this group can insert links into his or her topic or post when posting.");
            //en_us.AddText(EnumText.enumForum_UserGroups_FieldImage,"Allow Insert Image:");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpImage,"With this option checked, a user in this group can insert images into his or her topic or post when posting.");
            //en_us.AddText(EnumText.enumForum_UserGroups_FieldAttachment,"Allow Attachment:");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpAttachment,"With this option checked, a user in this group can upload attachments into his or her topic or post when posting.");
            //en_us.AddText(EnumText.enumForum_UserGroups_FieldMaxAttachmentsOnePost,"Max Attachments in One Post:");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpMaxAttachmentsOnePost,"Here you can set the maximum number of attachments which can be attached in one post for a user in this group. After your setting, the total number of a user's attachments in one post cannot exceed the number you've set.");
            //en_us.AddText(EnumText.enumForum_UserGroups_FieldMaxSizeOfAttachment,"Max Size of each Attachment:");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpMaxSizeOfAttachment,"Here you can set a maximum size of each attachment for a user in this group. After your setting, the total size of a user's any attachment cannot exceed the size you've set.");
            //en_us.AddText(EnumText.enumForum_UserGroups_FieldMaxSizeOfAllAttachments,"Max Size of all the Attachments:");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpMaxSizeOfAllAttachments,"Here you can set a maximum size of all the attachments a user in this group can upload. After your setting, the total size of all the attachments a user can upload cannot exceed the size you've set.");
            //en_us.AddText(EnumText.enumForum_UserGroups_FieldMaxMessage,"Max Messages in One Day:");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpMaxMessage,"Here you can set a maximum number of messages a user in this group can send in one day. After your setting, the total number of the messages sent by a user in one day cannot exceed the number you've set.");
            //en_us.AddText(EnumText.enumForum_UserGroups_FieldSearch,"Allow Search:");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpSearch,"With this option checked, a user in this group can use the search function to find specific topics or posts on your forum.");
            //en_us.AddText(EnumText.enumForum_UserGroups_FieldIntervalSearch,"Min Search Interval Time:");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpIntervalSearch,"Here you can set a minimum search interval time for a user in this group. After your setting, a user cannot search again within the time range you've set.");
            //en_us.AddText(EnumText.enumForum_UserGroups_FieldPostNotModeration,"Post Moderation Required:");
            //en_us.AddText(EnumText.enumForum_UserGroups_HelpPostNotModeration,"With this option checked, the topic or post of a user in this group needs moderation before it is published.");
            en_us.AddText(EnumText.enumForum_UserGroups_ButtonAddToAdministrators, "Add to Forum Administrators");
            en_us.AddText(EnumText.enumForum_UserGroups_TitleAdministrators, "Forum Administrators");
            en_us.AddText(EnumText.enumForum_UserGroups_SubtitleAdministrators, "Forum administrators have full access permissions to configure the settings and manage the forum. The forum founder will be automatically added into the administrator list.  An administrator can select multiple users and grant them administrator permissions, view any administrators’ detailed information and delete any other administrator. <br/> <br/><b>Note</b>:  There must be at least one active forum administrator. An administrator cannot delete him or herself.");
            en_us.AddText(EnumText.enumForum_UserGroups_PageAdministratorsErrorLoad, "Error loading Forum Administrators page:");
            en_us.AddText(EnumText.enumForum_UserGroups_PageAdministratorsErrorDelete, "Error deleting a forum administrator:");
            en_us.AddText(EnumText.enumFroum_UserGroups_AdministratorsDeleteConfirm, "Are you sure you want to remove this administrator from Forum Administrators?");
            en_us.AddText(EnumText.enumForum_UserGroups_ErrorSort, "Error sorting:");
            en_us.AddText(EnumText.enumForum_UserGroups_PageAdministratorsErrorAdd, "Error adding users to Administrators:");
            en_us.AddText(EnumText.enumForum_UserGroups_PageAdministratorsSuccessAdd, "The selected user(s) have been added to Forum Administrators successfully.");
            en_us.AddText(EnumText.enumForum_UserGroups_PageAdministratorsErrorQuery, "Error querying users:");
            en_us.AddText(EnumText.enumForum_UserGroups_TitleAddAdministrators, "Add Forum Administrators");

            en_us.AddText(EnumText.enumForum_UserGroups_ErrorAddingAdministrators, "Please select at least one user.");



            #endregion

            #region Forum Site & Setting 19

            en_us.AddText(EnumText.enumForum_Settings_ButtonCopyCode, "Copy Code");
            en_us.AddText(EnumText.enumForum_Settings_ButtonSave, "Save");
            en_us.AddText(EnumText.enumForum_Settings_ErrorReason, "Close Reason is required.");
            en_us.AddText(EnumText.enumForum_Settings_ErrorSiteName, "Site Name is required.");
            en_us.AddText(EnumText.enumForum_Settings_FieldRadioSite, "Site Status:");
            en_us.AddText(EnumText.enumForum_Settings_FieldRadioSiteClose, "Closed");
            en_us.AddText(EnumText.enumForum_Settings_FieldRadioSiteOpen, "Normal");
            en_us.AddText(EnumText.enumForum_Settings_FieldRegistrationEmailVerify, "Email Address Verification Required for Registration");
            en_us.AddText(EnumText.enumForum_Settings_FieldRegistrationAllowed, "New Registration Allowed");
            en_us.AddText(EnumText.enumForum_Settings_FieldDisplayName, "Display Name");

            en_us.AddText(EnumText.enumForum_Settings_FieldRegistrationModerate, "Moderation Required for New Registered Users");
            en_us.AddText(EnumText.enumForum_Settings_FieldSiteCloseReason, "Close Reason: ");
            en_us.AddText(EnumText.enumForum_Settings_FieldSiteName, "Site Name:");
            en_us.AddText(EnumText.enumForum_Settings_FieldUserRegistrationOption, "User Registration Option:");
            en_us.AddText(EnumText.enumForum_Settings_FieldWebLink, "Forum Code:");
            en_us.AddText(EnumText.enumForum_Settings_HelpRegistrationEmailVerify, "With this option checked, a verification email will be sent to the user\'s registered email address, and the user has to click the link enclosed in this email to finish the registration.");
            en_us.AddText(EnumText.enumForum_Settings_HelpRegistrationModrate, "With this option checked, an administrator\'s approval is required to finish a user\'s registration.");
            en_us.AddText(EnumText.enumForum_Settings_HelpNotAllowedDisplayNames, "Multiple Display Names MUST be separated by a comma.");
            en_us.AddText(EnumText.enumForum_Settings_HelpProhibitedWords, "<b>Note:</b> The censored words you set here are not case sensitive, and multiple censored words should be seperated with one of the following seperators: \" \"(a space), \"<b>\\r\\n</b>\"(Enter key), \"<b>,</b>\", \"<b>;</b>\", \"<b>-</b>\", \"<b>_</b>\", \"<b>+</b>\", \"<b>/</b>\", \"<b>\\</b>\" and \"<b>|</b>\".");
            
            en_us.AddText(EnumText.enumForum_Settings_PageCodePlanErrorGetSiteSetting, "Error getting site settings:");
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
            en_us.AddText(EnumText.enumForum_Settings_SubtitleCodePlan, "Forum Code here is a URL of Comm100 Forum. You can copy the code below and paste it onto your web page so that you website will have Comm100 Forum.");
            en_us.AddText(EnumText.enumForum_Settings_SubtitleRegistrationSettings, "Registration is the process of enrolling for the forum site. On this page, you can customize the verification mode of your forum users' registration.");
            en_us.AddText(EnumText.enumForum_Settings_SubtitleSiteSettings, "Site is the whole forum site. On this page, you can turn the forum site on or off. If you close the site, your forum users cannot get access to the forum site any more, while if you open the site, you can set the forum site status as Normal or Visit Only.");
            en_us.AddText(EnumText.enumForum_Settings_TitleCodePlan, "Code Generation");
            en_us.AddText(EnumText.enumForum_Settings_TitleRegistrationSettings, "Registration Settings");
            en_us.AddText(EnumText.enumForum_Settings_TitleSiteSettings, "Site Options");
            en_us.AddText(EnumText.enumForum_Settings_FieldClosedInfo, "is currently closed.");
            en_us.AddText(EnumText.enumForum_Settings_FieldReason, "Reason: ");
            en_us.AddText(EnumText.enumForum_Settings_LinkAdminLogin, "Admin Login");
            en_us.AddText(EnumText.enumForum_Settings_UserBannedMessage, "Your account has been banned by forum Administrator at {0}.<br /><br />The ban will be lifted at {1}.");
            en_us.AddText(EnumText.enumForum_Settings_IPBannedMessage, "Your IP {0} has been banned by forum Administrator at {1}.<br /><br />The ban will be lifted at {2}.");
            en_us.AddText(EnumText.enumForum_Settings_TitleRegistrationRulesSettings, "Policy Settings");
            en_us.AddText(EnumText.enumForum_Settings_SubTitleRegistrationRuleSettings, "Policy is used to descipline your forum users' actions.");
            en_us.AddText(EnumText.enumForum_Settings_PageRegistrationRuleSettingsSuccessfullySaved, "Registration rules set successfully");
            en_us.AddText(EnumText.enumForum_Settings_PageRegistrationRuleSettingsErrorSave, "Error saving registration rules settings:");
            en_us.AddText(EnumText.enumForum_Settings_PageRegistrationRuleSettingsErrorLoad, "Error loading Registration Rules page:");
            en_us.AddText(EnumText.enumForum_Settings_TitleForumFeature, "Forum Feature");
            en_us.AddText(EnumText.enumForum_Settings_SubTitleForumFeature, "Forum Feature is a section where you can enable or disable a certain feature option for your forum site account. Seven main features are listed here for you to select or unselect: Message, Favorites, Score, Hot Topic, Reputation, Reputation Group Permission and User Group Permission. Any adjustments of the configuration in Forum Feature will lead to some changes of the interface layout at both the admin control panel and the front end of your forum accordingly.");
            en_us.AddText(EnumText.enumForum_Settings_FieldEnableMessage, "Enable Message");
            en_us.AddText(EnumText.enumForum_Settings_FieldEnableFavorite, "Enable Favorites");
            en_us.AddText(EnumText.enumForum_Settings_FieldEnableSubscribe, "Enable Subscribe");
            en_us.AddText(EnumText.enumForum_Settings_FieldEnableScore, "Enable Score");
            en_us.AddText(EnumText.enumForum_Settings_FieldEnableReputation, "Enable Reputation");
            en_us.AddText(EnumText.enumForum_Settings_FieldEnableHotTopic, "Enable Hot Topic");
            en_us.AddText(EnumText.enumForum_Settings_FieldEnableGroupPermission, "Enable User Group Permission");
            en_us.AddText(EnumText.enumForum_Settings_FieldEnableReputationPermission, "Enable Reputation Group Permission");
            en_us.AddText(EnumText.enumForum_Settings_PageForumFeatureErrorLoad, "Error loading Forum Feature page:");
            en_us.AddText(EnumText.enumForum_Settings_PageForumFeatureSuccessSave, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_Settings_PageForumFeatureErrorSave, "Error saving forum feature:");
            en_us.AddText(EnumText.enumForum_Settings_HelpEnableMessage, "Message is a communication tool which allows a user to send messages to other users or receive messages from other users. The message content can only be viewed between the sender and the recipient.");
            en_us.AddText(EnumText.enumForum_Settings_HelpEnableFavorite, "Favorites is a place to hold forum topics  that a user  is interested in. Topics can be added into or removed from Favorites.");
            en_us.AddText(EnumText.enumForum_Settings_HelpEnableSubscribe, "Subscribe is a function which allows subscribing users to receive an automated email notification when there is a new response to the subscribed topics.");
            en_us.AddText(EnumText.enumForum_Settings_HelpEnableScore, "Score system is a trading mechanism to encourage user participation. A user can earn Score by creating topics, posting answers and etc.. An author of a topic can require other users to pay Score to view his or her topic or download the attachment by setting the \"Score Needed to View\" or \"Score Needed to Download\" of a topic. How it works is that when a user wants to view a topic or download an attachment, if he or she does not have enough Score, he or she must do something (very likely, post answers/topics) to earn the required Score to view the topic or download the attachment.   If the Score feature is enabled, you can set the Score Strategy through menu \"Settings->Score Strategy\". Score Strategy defines how much Score a user can earn when a certain action is performed.");
            en_us.AddText(EnumText.enumForum_Settings_HelpEnableReputation, "Reputation is an indicator of a user's activeness on this forum. Good actions, such as posting good answers, can increase a user's reputation and bad actions, such as posting topics that are confirmed as spam by forum administrators or moderators, can hurt a user's reputation.   If the Reputation feature is enabled, you can set the Reputation Strategy through menu \"Setting->Reputation Strategy\". Reputation Strategy defines how much Reputation a user earns or loses when a certain event occurs.");
            en_us.AddText(EnumText.enumForum_Settings_HelpEnableHotTopic, "Hot Topic means a topic is popular. With this option checked, when views or replies of a topic reaches a certain number, which can be set through menu \"Settings-> Hot Topic Strategy\", the status icon of this topic will be changed to Hot.");
            en_us.AddText(EnumText.enumForum_Settings_HelpEnableGroupPermission, "User Group Permission is a group based permission management system. User Group Permission allows granular control of the permissions.  If the User Group Permission feature is not enabled, all users, except the administrators, forum moderators and guest users, have the same permissions. These permissions can be defined through menu \"Settings->User Permissions\".    If the User Group Permission feature is enabled, you can have multiple user groups and add users to different user groups. Different user groups can have different permissions. Different forums can have different user group permissions.");
            en_us.AddText(EnumText.enumForum_Settings_HelpEnableReputationPermission, "Reputation Group Permission is a dynamic permission system to ease the permission management duty of administrators and moderators. The members of a Reputation Group are the users with a Reputation that is within the Reputation range requirement of the Reputation Group. Reputation Groups can be defined through menu \"Reputation Groups\". The membership of a Reputation Group is dynamic and you cannot add user to or remove users from a Reputation Group. For example, if you have a Reputation Group called \"Expert\" with a Reputation range requirement of from 10,000 to 20,000. A user with a Reputation of 15,000 is a member of the \"Expert\" Reputation Group automatically, but another user with a Reputation of 2,000 is not a member of the \"Expert\" Reputation Group.  <br/><br/> The following is an example showing how you can use Reputation Group Permission: For example, to reduce spam, you do NOT allow  the newly registered users to create topics/posts with HTML hyper links while allow the good old ones to create topics/posts with HTML hyper links. If you need to pick which user to be added into a group with the HTML hyper link permission, that will be a lot of work. Instead, you can define, for example,  two Reputation Groups, \"Newbie\" and \"Seasoned\". The \"Newbie\" Reputation Group has a Reputation range requirement of from 0 to 1,000 and the  \"Seasoned\" Reputation Group has a Reputation range requirement of from 1,001 to 10,000. The \"Newbie\" group is not allowed to create topics/posts with HTML hyper link while the \"Season\" group is allowed. When a user is new, the user's Reputation is low so the user is in the \"Newbie\" Reputation Group so no HTML hyper link is allowed. When the user's reputation is higher than 1,000 through participation in the forum, the user is in the \"Seasoned\" Reputation Group automatically and HTML hyper link is allowed.");
            en_us.AddText(EnumText.enumForum_Settings_SubitleSettings, "Here list several settings which you can customize for your site. By clicking the links below, you can customize each setting respectively and accordingly.");
            en_us.AddText(EnumText.enumForum_Settings_HlHeaderAndFooter, "Header & Footer Settings");
            en_us.AddText(EnumText.enumForum_Settings_HlTemplate, "Template Settings");
            en_us.AddText(EnumText.enumForum_Settings_HlGuestUserPermission, "Guest User Permissions");
            en_us.AddText(EnumText.enumForum_Settings_HlUserPermission, "User Permissions");
            en_us.AddText(EnumText.enumForum_Settings_HlReputationStrategy, "Reputation Strategy");
            en_us.AddText(EnumText.enumForum_Settings_Reputations, "Reputations");
            en_us.AddText(EnumText.enumForum_Settings_HlScoreStrategy, "Score Strategy");
            en_us.AddText(EnumText.enumForum_Settings_HlProhibitedWords, "Word Censoring");
            en_us.AddText(EnumText.enumForum_Settings_HlHotTopic, "Hot Topic Strategy");
            en_us.AddText(EnumText.enumForum_Settings_HlSMTP, "SMTP Settings");
            en_us.AddText(EnumText.enumForum_Settings_PageSettingsErrorLoad, "Error loading Settings page:");
            en_us.AddText(EnumText.enumForum_Settings_ErrorPageSizeInteger, "The input Must be an integer. Please retype.");
            en_us.AddText(EnumText.enumForum_Settings_ErrorPageSizeRange, "The input Must be an integer. Please retype.");
            en_us.AddText(EnumText.enumForum_Settings_FieldMetaKeywords, "Meta Keywords:");
            en_us.AddText(EnumText.enumForum_Settings_FieldMetaDescription, "Meta Description:");
            en_us.AddText(EnumText.enumForum_Settings_FieldContactDetails, "Contact Details:");
            en_us.AddText(EnumText.enumForum_Settings_FieldPageSize, "Max Topics per Page:");
            en_us.AddText(EnumText.enumForum_Settings_FieldRadioVisitOnly, "Visit Only");
            en_us.AddText(EnumText.enumForum_Settings_ErrorMinLengthOfDisplayNameRange, "The input Must be an integer. Please retype.");
            en_us.AddText(EnumText.enumForum_Settings_ErrorMinLengthOfDisplayNameRequire, "The input Must be an integer. Please retype.");
            en_us.AddText(EnumText.enumForum_Settings_ErrorMaxLengthOfDisplayNameRange, "The input Must be an integer. Please retype.");
            en_us.AddText(EnumText.enumForum_Settings_ErrorMaxLengthOfDisplayNameRequire, "The input Must be an integer. Please retype.");
            en_us.AddText(EnumText.enumForum_Settings_ErrorDisolayNameRegularExpression, "Regular Expression format is incorrect. <br /> Please start with '^' and end with '$'.");
            en_us.AddText(EnumText.enumForum_Settings_FieldMinimumDisplayNameLength, "Min Length of Display Name:");
            en_us.AddText(EnumText.enumForum_Settings_FieldMaximumDisplayNameLength, "Max Length of Display Name:");
            en_us.AddText(EnumText.enumForum_Settings_FieldNotAllowedDisplayNames, "Not Allowed Display Names: ");
            en_us.AddText(EnumText.enumForum_Settings_FieldDisplayNameRegularExpression, "Display Name Regular Expression:");
            en_us.AddText(EnumText.enumForum_Settings_FieldDisplayNameRegularExpressionInstruction, "Display Name Regular Expression Instruction:");
            en_us.AddText(EnumText.enumForum_Settings_FieldGreetingMessage, "Greeting Message");
            en_us.AddText(EnumText.enumForum_Settings_FieldAgreement, "Agreement");
            en_us.AddText(EnumText.enumForum_Settings_PageHeaderFooterSettingSuccessSave, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_Settings_TitleGuestUserPermissions, "Guest User Permissions");
            en_us.AddText(EnumText.enumForum_Settings_SubtitleGuestUserPermissions, "Guest Users are the forum visitors who haven’t registered for your forum site account. Administrators can assign customized permissions to guest users.");
            en_us.AddText(EnumText.enumForum_Settings_PageGuestUserPermissionsErrorLoad, "Error loading Guest User Permissions page:");
            en_us.AddText(EnumText.enumForum_Settings_PageGuestUserPermissionsErrorSave, "Error saving guest user permissions:");
            en_us.AddText(EnumText.enumForum_Settings_PageGuestUserPermissionsSuccessSave, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_Settings_ErrorSearchInterval, "The input Must be an integer. Please retype.");
            en_us.AddText(EnumText.enumForum_Settings_FieldAllowViewForum, "Allow Visit Forum:");
            en_us.AddText(EnumText.enumForum_Settings_FieldAllowSearch, "Allow Search:");
            en_us.AddText(EnumText.enumForum_Settings_FieldMinIntervalTimeSearching, "Min Search Interval Time:");
            en_us.AddText(EnumText.enumForum_Settings_HelpAllowViewForum, "With this option checked, a guest user can view the topic list on your forum.");
            en_us.AddText(EnumText.enumForum_Settings_HelpAllowSearch, "With this option checked, a guest user can use the search function to find specific topics or posts on your forum.");
            en_us.AddText(EnumText.enumForum_Settings_HelpMinIntervalTimeSearching, "Here you can set a minimum search interval time for a guest user. After your setting, the guest user cannot search again within the time range you'v set.");
            en_us.AddText(EnumText.enumForum_Settings_TitleReputationStrategy, "Reputation Strategy");
            en_us.AddText(EnumText.enumForum_Settings_SubtitleReputationStrategy, "Reputation Strategy is designed to better manage users' permissions. Users with different reputations will be divided into different reputation groups. Each reputation group may have a different level of permission rights . Reputation can be gained or lost in many ways. On this page, you can set a reputation value to be added or deducted for the users' different operations. Please note if you want to deduct reputation for a certain operation, you can set the reputation value as a negative.");
            en_us.AddText(EnumText.enumForum_Settings_PageReputationStrategyErrorLoad, "Error loading Reputation Strategy page:");
            en_us.AddText(EnumText.enumForum_Settings_PageReputationStrategyErrorSave, "Error saving reputation strategy:");
            en_us.AddText(EnumText.enumForum_Settings_PageReputationStrategySuccessSave, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_Settings_ErrorOnlyDigital, "The input Must be an integer. Please retype.");
            en_us.AddText(EnumText.enumForum_Settings_ColumnReputation, "Reputation");
            en_us.AddText(EnumText.enumForum_Settings_ColumnGeneral, "General");
            en_us.AddText(EnumText.enumForum_Settings_ColumnTopic, "Topic");
            en_us.AddText(EnumText.enumForum_Settings_ColumnPost, "Post");
            en_us.AddText(EnumText.enumForum_Settings_TitleScoreStrategy, "Score Strategy");
            en_us.AddText(EnumText.enumForum_Settings_SubtitleScoreStrategy, "Score Strategy is designed to encourage  users who have registered and are active on your forum. Scores can be added in many ways and accumulated by every user. In exchange for scores, users can gain exclusive access, such as the ability to view the \"Score Needed to View\" topics or download the \"Score Needed to Download\" attachments. On this page, you can set a score value to be added for each different user operation. Please note if you want to deduct score for a certain operation, you can set the score value as a negative.");
            en_us.AddText(EnumText.enumForum_Settings_PageScoreStrategyErrorLoad, "Error loading Score Strategy page:");
            en_us.AddText(EnumText.enumForum_Settings_PageScoreStrategyErrorSave, "Error saving score strategy:");
            en_us.AddText(EnumText.enumForum_Settings_PageScoreStrategySuccessSave, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_Settings_ColumnScore, "Scores");
            en_us.AddText(EnumText.enumForum_Settings_ColumnOthers, "Other");
            en_us.AddText(EnumText.enumForum_Settings_FieldRegister, "Registration Succeeded:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreRegister, "When a user has registered for the forum successfully, the score value you set here will be added into this user's account. Please note the initial score of this user is 0.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationRegister, "When a user has registered for the forum successfully, the reputation value you set here will be added into this user's account. Please note the initial reputation of this user is 0.");
            en_us.AddText(EnumText.enumForum_Settings_FieldFirstLogin, "First Login Every Day:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreFirstLogin, "The score value you set here will be added into the user's account for his or her  first login every day. Please note no score will be added for any other login on the same day.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationFirstLogin, "The reputation value you set here will be added into the user's account for his or her first login every day. Please note no reputation will be added for any other login on the same day.");
            en_us.AddText(EnumText.enumForum_Settings_FieldModeratorAdded, "Moderator Added:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreModeratorAdded, "When a user was not a Moderator, but now has become a Moderator on your forum, the score value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationModeratorAdded, "When a user was not a Moderator, but now has become a Moderator on your forum, the reputation value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldModeratorRemoved, "Moderator Removed:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreModeratorRemoved, "When a user's Moderator status has been removed (that is to say, he or she is not a Moderator on your forum any more), the score value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationModeratorRemoved, "When a user's Moderator status has been removed (that is to say, he or she is not a Moderator on your forum any more), the reputation value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldUserAccountBanned, "User Account Banned:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreUserAccountBanned, "When a user's account has been banned, the score value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationUserAccountBanned, "When a user's account has been banned, the reputation value you set here will be added into  this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldUserAccountUnbanned, "User Account Unbanned:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreUserAccountUnbanned, "When a user's ban has been lifted, the score value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationUserAccountUnbanned, "When a user's ban has been lifted, the reputation value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldTopicPosted, "New Topic:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreTopicPosted, "When a user has posted a new topic, the score value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationTopicPosted, "When a user has posted a new topic, the reputation value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldMakeAsFeature, "Topic Marked as Featured:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreMakeAsFeature, "When a user's topic has been marked as Featured Topic, the score value will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationMakeAsFeature, "When a user's topic has been marked as Featured Topic, the reputation value will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldTopicSticky, "Topic Marked as Sticky:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreTopicSticky, "When a user's topic has been placed at the top of the topic list as a sticky, the score value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationTopicSticky, "When a user's topic has been placed at the top of the topic list as a sticky, the reputation value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldTopicDeleted, "Topic Deleted:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreTopicDeleted, "When a user's topic has been deleted, the score value you set here will be added into from this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationTopicDeleted, "When a user's topic has been deleted, the reputation value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldTopicRestored, "Topic Restored:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreTopicRestored, "When a user's deleted topic has been restored, the score value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationTopicRestored, "When a user's deleted topic has been restored, the reputation value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldTopicAddedIntoFavorites, "Topic Added into Favorites:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreTopicAddedIntoFavorites, "When a user's topic has been added into Favorites by other users, the score value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationTopicAddedIntoFavorites, "When a user's topic has been added into Favorites by other users, the reputation value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldTopicRemovedFromFavorites, "Topic Removed from Favorites:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreTopicRemovedFromFavorites, "When a user's topic has been removed from Favorites by other users, the score value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationTopicRemovedFromFavorites, "When a user's topic has been removed from Favorites by other users, the reputation value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldTopicViewed, "Topic Viewed:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreTopicViewed, "When a user's topic has been viewed by other users, the score value  you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationTopicViewed, "When a user's topic has been viewed by other users, the reputation value  you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldTopicReplied, "Topic Replied:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreTopicReplied, "When a user's topic has got a new reply, the score value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationTopicReplied, "When a user's topic has got a new reply, the reputation value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldTopicVerifiedAsSpam, "Topic Confirmed as Spam:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreTopicVerifiedAsSpam, "When a user's topic has been confirmed as Spam, the score value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationTopicVerifiedAsSpam, "When a user's topic has been confirmed as Spam, the reputation value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldVoteforaPoll, "Vote for a Topic:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreVoteforaPoll, "When a user has voted for a topic, the score value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationVoteforaPoll, "When a user has voted for a topic, the reputation value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldPollVoted, "Topic Voted:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScorePollVoted, "When a user's topic has been voted for, the score value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationPollVoted, "When a user's topic has been voted for, the reputation value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldNewPost, "New Post:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreNewPost, "When a user has posted a new post, the score value will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationNewPost, "When a user has posted a new post, the reputation value will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldPostDeleted, "Post Deleted:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScorePostDeleted, "When a user's post has been deleted, the score value will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationPostDeleted, "When a user's post has been deleted, the reputation value will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldPostRestored, "Post Restored:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScorePostRestored, "When a user's deleted post has been restored, the score value will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationPostRestored, "When a user's deleted post has been restored, the reputation value will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldPostVerifiedAsSpam, "Post Confirmed as Spam:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScorePostVerifiedAsSpam, "When a user's post has been confirmed as Spam, the score value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationPostVerifiedAsSpam, "When a user's post has been confirmed as Spam, the reputation value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldPostMarkAsAnswer, "Post Marked as Answer:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScorePostMarkAsAnswer, "When a user's post has been marked as Answer, the score value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationPostMarkAsAnswer, "When a user's post has been marked as Answer, the reputation value you set here will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldReportAbuse, "Report Abuse:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreReportAbuse, "When a user has reported abuse, the score value will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationReportAbuse, "When a user has reported abuse, the reputation value will be added into this user's account.");
            en_us.AddText(EnumText.enumForum_Settings_FieldSearch, "Search:");
            en_us.AddText(EnumText.enumForum_Settings_HelpScoreSearch, "When a user has used the search function to find the specific topics or posts on your forum, the score value will be added into this user's account. If you have set this score value as a negative, we suggest you disable \"Allow Search\" function when setting Guest User's permissions.");
            en_us.AddText(EnumText.enumForum_Settings_HelpReputationSearch, "When a user has used the search function to find the specific topics or posts on your forum, the reputation value will be added into this user's account. If you have set this reputation value as a negative, we suggest you disable \"Allow Search\" function when setting Guest User's permissions.");

            /*User Permission*/
            en_us.AddText(EnumText.enumForum_UserPermission_HelpAllowViewForum, "With this option checked, a user can view the topic list on your forum.");
            en_us.AddText(EnumText.enumForum_UserPermission_HelpAllowViewTopicOrPost, "With this option checked, a user can view the topics and posts on your forum.");
            en_us.AddText(EnumText.enumForum_UserPermission_HelpAllowPostTopicOrPost, "With this option checked, a user can post topics and posts on your forum.");
            en_us.AddText(EnumText.enumForum_UserPermission_HelpAllowCustomizeAvatar, "With this option checked, a user can customize his or her own avatar by either uploading an avatar from local file or choosing one from the default ones.");
            en_us.AddText(EnumText.enumForum_UserPermission_HelpMaxlengthofSignature, "Here you can set a maximum length of the signature for a user. After your setting, the total length of this user's signature cannot exceed the length you've set.");
            en_us.AddText(EnumText.enumForum_UserPermission_HelpMinPostIntervalTime, "Here you can set a minimum interval time for posting for a user. After your setting, this user cannot post again within the time range you've set.");
            en_us.AddText(EnumText.enumForum_UserPermission_HelpMaxLengthofTopicOrPost, "Here you can set a maximum length of  the topic or post for a user. After your setting, the total length of this user's topic or post cannot exceed the length you've set.");
            en_us.AddText(EnumText.enumForum_UserPermission_HelpAllowInsertLink, "With this option checked, a user can insert links into his or her signature.");
            en_us.AddText(EnumText.enumForum_UserPermission_HelpAllowInsertImage, "With this option checked, a user can insert images into his or her signature.");
            en_us.AddText(EnumText.enumForum_UserPermission_HelpAllowAttachment, "With this option checked, a user can upload attachments into his or her topic or post when posting.");
            en_us.AddText(EnumText.enumForum_UserPermission_HelpMaxAttachmentsinOnePost, "Here you can set the maximum number of attachments which can be attached in one post for a user. After your setting, the total number of this user's attachments in one post cannot exceed the number you've set.");
            en_us.AddText(EnumText.enumForum_UserPermission_HelpMaxSizeofeachAttachment, "Here you can set a maximum size of each attachment for a user. After your setting, the total size of this user's any attachment cannot exceed the size you've set. ");
            en_us.AddText(EnumText.enumForum_UserPermission_HelpMaxSizeofalltheAttachments, "Here you can set a maximum size of all the attachments a user can upload. After your setting, the total size of all the attachments this user can upload cannot exceed the size you've set.");
            en_us.AddText(EnumText.enumForum_UserPermission_HelpMaxMessagesinOneDay, "Here you can set a maximum number of messages a user can send in one day. After your setting, the total number of the messages sent by this user in one day cannot exceed the number you've set.");
            en_us.AddText(EnumText.enumForum_UserPermission_HelpAllowSearch, "With this option checked, a user can use the search function to find specific topics or posts on your forum.");
            en_us.AddText(EnumText.enumForum_UserPermission_HelpMinSearchIntervalTime, "Here you can set a minimum search interval time for a user. After your setting, this user cannot search again within the time range you've set.");
            en_us.AddText(EnumText.enumForum_UserPermission_HelpPostModerationRequired, "With this option checked, the topic or post of a user needs moderation before it is published.");

            #endregion

            #region Prohibited Words & Hot Topic Strategy & SMTP Setting 18

            en_us.AddText(EnumText.enumForum_ProhibitedWords_TitleProhibitedWords, "Word Censoring");
            en_us.AddText(EnumText.enumForum_ProhibitedWords_SubtitleProhibitedWords, "A certain level of appropriate, profanity-free speech is required on certain forums. Word censoring helps you avoid the rude or vulgar words. Words that match the patterns set below will be censored automatically  with the  text that you, the admin, specify. By default, the inappropriate words will be replaced with an asterisk (*).");
            en_us.AddText(EnumText.enumForum_ProhibitedWords_PageProhibitedWordsErrorLoad, "Error loading Word Censoring page:");
            en_us.AddText(EnumText.enumForum_ProhibitedWords_PageProhibitedWordsErrorSave, "Error saving word censoring:");
            en_us.AddText(EnumText.enumForum_ProhibitedWords_PageProhibitedWordsSuccessSave, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_ProhibitedWords_FieldEnable, "Enable Word Censoring:");
            en_us.AddText(EnumText.enumForum_ProhibitedWords_FieldCharacterReplace, "Replacement:");
            en_us.AddText(EnumText.enumForum_ProhibitedWords_FieldProhibitedWords, "Censored Words:");


            en_us.AddText(EnumText.enumForum_HotTopicStrategy_TitleHotTopicStrategy, "Hot Topic Strategy Settings");
            en_us.AddText(EnumText.enumForum_HotTopicStrategy_SubtitleHotTopicStrategy, "A hot topic is a topic displayed with the  hot topic icon at the end of its subject when it attracts a set number of posts or has been viewed for a set number of times.  Administrators can predefine the required number of posts and/or views for a topic to be displayed as <em>Hot</em>.");
            en_us.AddText(EnumText.enumForum_HotTopicStrategy_ErrorViewInteger, "The input Must be an integer. Please re-enter.");
            en_us.AddText(EnumText.enumForum_HotTopicStrategy_ErrorPostInteger, "The input Must be an integer. Please re-enter.");
            en_us.AddText(EnumText.enumForum_HotTopicStrategy_PageHotTopicStrategyErrorLoad, "Error loading Hot Topic Strategy Settings page:");
            en_us.AddText(EnumText.enumForum_HotTopicStrategy_PageHotTopicStrategyErrorSave, "Error saving hot topic strategy settings:");
            en_us.AddText(EnumText.enumForum_HotTopicStrategy_PageHotTopicStrategySuccessSave, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_HotTopicStrategy_FieldView, "Views:");
            en_us.AddText(EnumText.enumForum_HotTopicStrategy_FieldReplies, "Replies:");
            en_us.AddText(EnumText.enumForum_HotTopicStrategy_ErrorViewsIsRequired, "Views is required.");
            en_us.AddText(EnumText.enumForum_HotTopicStrategy_ErrorPostsIsRequired, "Posts is required.");


            en_us.AddText(EnumText.enumForum_SMTPSettings_TitleSMTPSettings, "SMTP Settings");
            en_us.AddText(EnumText.enumForum_SMTPSettings_SubtitleSMTPSettings, "Administrators can set the mail server. The setting is only applied for Open Source version, or you will use Comm100 mail server.");
            en_us.AddText(EnumText.enumForum_SMTPSettings_ErrorRequireSMTPServer, "SMTP Server is required.");
            en_us.AddText(EnumText.enumForum_SMTPSettings_ErrorRequireSMTPPort, "SMTP Port is required.");
            en_us.AddText(EnumText.enumForum_SMTPSettings_ErrorRequireUserName, "User Name is required.");
            en_us.AddText(EnumText.enumForum_SMTPSettings_ErrorRequirePassword, "Password is required.");
            en_us.AddText(EnumText.enumForum_SMTPSettings_PageSMTPSettingsErrorLoad, "Error loading SMTP Settings page:");
            en_us.AddText(EnumText.enumForum_SMTPSettings_PageSMTPSettingsErrorSave, "Error saving SMTP settings:");
            en_us.AddText(EnumText.enumForum_SMTPSettings_PageSMTPSettingsSuccessSave, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_SMTPSettings_FieldSMTPServer, "SMTP Server:");
            en_us.AddText(EnumText.enumForum_SMTPSettings_FieldSMTPPort, "SMTP Port:");
            en_us.AddText(EnumText.enumForum_SMTPSettings_FieldAuthenticationRequired, "Authentication Required:");
            en_us.AddText(EnumText.enumForum_SMTPSettings_FieldUserName, "User Name:");
            en_us.AddText(EnumText.enumForum_SMTPSettings_FieldPassword, "Password:");
            en_us.AddText(EnumText.enumForum_SMTPSettings_FieldSenderEmailAddress, "From Email Address:");
            en_us.AddText(EnumText.enumForum_SMTPSettings_FieldSenderName, "From Name:");
            en_us.AddText(EnumText.enumForum_SMTPSettings_FieldSSL, "SSL:");


            #endregion

            #region Forum Style 17

            en_us.AddText(EnumText.enumForum_Styles_ButtonCancel, "Cancel");
            en_us.AddText(EnumText.enumForum_Styles_ButtonEditLogo, "Edit logo on the Site Profile page");
            en_us.AddText(EnumText.enumForum_Styles_ButtonPrivew, "Preview");
            en_us.AddText(EnumText.enumForum_Styles_ButtonSave, "Save");
            en_us.AddText(EnumText.enumForum_Styles_ButtonUpload, "Upload");
            en_us.AddText(EnumText.enumForum_Styles_FieldAdvancedModeRadio, "Advanced Mode");
            en_us.AddText(EnumText.enumForum_Styles_FieldEasyModeRadio, "Easy Mode");
            en_us.AddText(EnumText.enumForum_Styles_FieldSelectType, "Select Type:");
            en_us.AddText(EnumText.enumForum_Styles_FieldUploadDescription, "(Upload your logo here. Your logo file should be in GIF, JPG, JPEG, PNG, or BMP format. The size of your logo file should be less than 100K)");
            en_us.AddText(EnumText.enumForum_Styles_LabelHeaderFooter, "Header & Footer");
            en_us.AddText(EnumText.enumForum_Styles_LabelLogo, "Logo");
            en_us.AddText(EnumText.enumForum_Styles_LabelPageFooter, "Home Page Footer");
            en_us.AddText(EnumText.enumForum_Styles_LabelPageHeader, "Home Page Header");
            en_us.AddText(EnumText.enumForum_Styles_NoteEditLogo, "Note: Your forum and your site share the same logo.");
            en_us.AddText(EnumText.enumForum_Styles_PageHeaderFooterSettingErrorLoad, "Error loading Header & Footer Settings page:");
            en_us.AddText(EnumText.enumForum_Styles_PageHeaderFooterSettingErrorPreview, "Error previewing:");
            en_us.AddText(EnumText.enumForum_Styles_PageHeaderFooterSettingErrorSave, "Error saving Header & Footer settings:");
            en_us.AddText(EnumText.enumForum_Styles_PageHeaderFooterSettingSuccessSave, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorLoadingPage, "Error loading Custom Logo page:");
            en_us.AddText(EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorRedirectingPage, "Error redirecting to Site Profile page:");
            en_us.AddText(EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorUploadingExceedMaxFileLength, "Error uploading a logo file: the size of your logo file should be less than 100K.");
            en_us.AddText(EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorUploadingIncorrectFormatFile, "Error uploading a logo file: your logo file should be in GIF, JPG, JPEG, PNG, or BMP format.");
            en_us.AddText(EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorUploadingNoSelectFile, "Error uploading a logo file: please select a logo file to upload.");
            en_us.AddText(EnumText.enumForum_Styles_PageTemplateSettingErrorDataBind, "Error getting templates:");
            en_us.AddText(EnumText.enumForum_Styles_PageTemplateSettingErrorGetTemplateId, "Error saving template settings:");
            en_us.AddText(EnumText.enumForum_Styles_PageTemplateSettingErrorPageLoad, "Error loading Template Settings page:");
            en_us.AddText(EnumText.enumForum_Styles_PageTemplateSettingErrorSetCurrentTemplate, "Error setting a template:");
            en_us.AddText(EnumText.enumForum_Styles_PageTemplateSettingSuccessMessage, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_Styles_SubtitleHeaderFooterSetting, "Customize your own logo and page Headers & Footers according to your needs. Easy Mode is a simple custom mode that allows you only to customize your own logo in the header by either uploading your own logo or just taking the default logo. Advanced Mode is a full custom mode that allows you to customize both Header and Footer of your forum. With this mode selected, you can write your own Header and Footer by using HTML Editor.");
            en_us.AddText(EnumText.enumForum_Styles_SubtitleTemplateSetting, "Template is a collection of settings that defines your forum page styles. You can choose one preferred template that best fits your site feel and look.");
            en_us.AddText(EnumText.enumForum_Styles_TitleHeaderFooterSetting, "Header & Footer Settings");
            en_us.AddText(EnumText.enumForum_Styles_TitleSiteCustomizeLogo, "Customize Logo");
            en_us.AddText(EnumText.enumForum_Styles_TitleTemplateSetting, "Template Settings");

            #endregion

            #region Forum Categories 16

            en_us.AddText(EnumText.enumForum_Categories_TitleList, "Categories");
            en_us.AddText(EnumText.enumForum_Categories_SubtitleList, "Categories help you organize forum discussions into different groups, making it easy for users to find the specific topics or posts. As a forum administrator, you can create a new category as well as edit and delete the existing categories according to your needs.");
            en_us.AddText(EnumText.enumForum_Categories_ButtonNew, "New Category");
            en_us.AddText(EnumText.enumForum_Categories_ColumnId, "Id");
            en_us.AddText(EnumText.enumForum_Categories_ColumnName, "Name");
            en_us.AddText(EnumText.enumForum_Categories_ColumnDescription, "Description");
            en_us.AddText(EnumText.enumForum_Categories_ColumnOrder, "Order");
            en_us.AddText(EnumText.enumForum_Categories_ColumnOperation, "Operation");
            en_us.AddText(EnumText.enumForum_Categories_ColumnForums, "Forums");
            en_us.AddText(EnumText.enumForum_Categories_ColumnStatus, "Status");
            en_us.AddText(EnumText.enumForum_Categories_HelpEdit, "Edit");
            en_us.AddText(EnumText.enumForum_Categories_HelpDelete, "Delete");
            en_us.AddText(EnumText.enumForum_Categories_HelpUp, "Move up");
            en_us.AddText(EnumText.enumForum_Categories_HelpDown, "Move down");
            en_us.AddText(EnumText.enumForum_Categories_PageListErrorDeleteCategoryWithForum, "This category is currently in use. To delete it, please delete all the forums under this category first!");
            en_us.AddText(EnumText.enumForum_Categories_PageListErrorDeleteCategory, "Error deleting a category:");
            en_us.AddText(EnumText.enumForum_Categories_PageListErrorEditCategory, "Error editing a category: ");
            en_us.AddText(EnumText.enumForum_Categories_PageListErrorOrderCategory, "Error ordering categories: ");
            en_us.AddText(EnumText.enumForum_Categories_PageListErrorGetCategory, "Error getting category list:");
            en_us.AddText(EnumText.enumForum_Categories_PageListErrorLoadPage, "Error loading Categories page:");
            en_us.AddText(EnumText.enumForum_Categories_ComfirmDelete, "Are you sure you want to delete this category?");
            en_us.AddText(EnumText.enumForum_Categories_TitleAdd, "New Category");
            en_us.AddText(EnumText.enumForum_Categories_SubtitleAdd, "Here you can create a new category.");
            en_us.AddText(EnumText.enumForum_Categories_ButtonSave, "Save");
            en_us.AddText(EnumText.enumForum_Categories_ButtonCancel, "Cancel");
            en_us.AddText(EnumText.enumForum_Categories_FieldName, "Name");
            en_us.AddText(EnumText.enumForum_Categories_FieldDescription, "Description");
            en_us.AddText(EnumText.enumForum_Categories_FieldStatus, "Status:");
            en_us.AddText(EnumText.enumForum_Categories_ErrorRequireField, "*Required Field");
            en_us.AddText(EnumText.enumForum_Categories_ErrorNameRequired, "Name is required.");
            en_us.AddText(EnumText.enumForum_Categories_PageErrorAddCreateNew, "Error adding a new category:");
            en_us.AddText(EnumText.enumForum_Categories_TitleEdit, "Edit a Category");
            en_us.AddText(EnumText.enumForum_Categories_SubtitleEdit, "Here you can edit the selected category.");
            en_us.AddText(EnumText.enumForum_Categories_PageEditErrorLoadPage, "Error loading Edit a Category page:");
            en_us.AddText(EnumText.enumForum_Categories_PageEditErrorEdit, "Error editing a category: ");
            en_us.AddText(EnumText.enumForum_Categories_StateNormal, "Normal");
            en_us.AddText(EnumText.enumForum_Categories_StateClose, "Close");
            en_us.AddText(EnumText.enumForum_Categories_EditSaveSucceeded, "Save succeeded.");


            #endregion

            #region Forum Forums 15

            en_us.AddText(EnumText.enumForum_Forums_TitleList, "Forums");
            en_us.AddText(EnumText.enumForum_Forums_TitleSubtitleList, "Forum is an online discussion site for all the participants to ask questions, share ideas. On this page, you can create a new forum as well as edit and delete the existing forums.");
            en_us.AddText(EnumText.enumForum_Forums_ButtonNew, "New Forum");
            en_us.AddText(EnumText.enumForum_Forums_ColumnId, "Id");
            en_us.AddText(EnumText.enumForum_Forums_ColumnName, "Name");
            en_us.AddText(EnumText.enumForum_Forums_ColumnDescription, "Description");
            en_us.AddText(EnumText.enumForum_Forums_ColumnStatus, "Status");
            en_us.AddText(EnumText.enumForum_Forums_ColumnAnnouncements, "Announcements");
            en_us.AddText(EnumText.enumForum_Forums_ColumnTopics, "Topics");
            en_us.AddText(EnumText.enumForum_Forums_ColumnPermissions, "Permissions");
            en_us.AddText(EnumText.enumForum_Forums_ColumnModerator, "Moderator");
            en_us.AddText(EnumText.enumForum_Forums_ColumnOrder, "Order");
            en_us.AddText(EnumText.enumForum_Forums_ColumnOperation, "Operation");
            en_us.AddText(EnumText.enumForum_Forums_HelpUp, "Move up");
            en_us.AddText(EnumText.enumForum_Forums_HelpDown, "Move down");
            en_us.AddText(EnumText.enumForum_Forums_HelpEdit, "Edit");
            en_us.AddText(EnumText.enumForum_Forums_HelpDelete, "Delete");
            en_us.AddText(EnumText.enumForum_Forums_PageListErrorDelete, "Error deleting a forum: ");
            en_us.AddText(EnumText.enumForum_Forums_PageListErrorGetting, "Error loading Forums page:");
            en_us.AddText(EnumText.enumForum_Forums_PageListErrorSortDown, "Error moving down a forum:");
            en_us.AddText(EnumText.enumForum_Forums_PageListErrorSortUp, "Error moving up a forum:");
            en_us.AddText(EnumText.enumForum_Forums_PageListErrorLoadPage, "Error loading Forums page:");
            en_us.AddText(EnumText.enumForum_Forums_PageConfirmDelete, "Are you sure you want to delete this forum?");
            en_us.AddText(EnumText.enumForum_Forums_PageListErrorConfirmDelete, "Warning; After this operation, all the topics and posts under this forum will be deleted permanently. Are you sure you want to go on?");
            en_us.AddText(EnumText.enumForum_Forums_TitleNew, "New Forum");
            en_us.AddText(EnumText.enumForum_Forums_SubtitleNew, "Here you can add a new forum. When adding a new forum, you have to choose a category for this forum and appoint at least one moderator for this new forum.");
            en_us.AddText(EnumText.enumForum_Forums_FieldName, "Name:");
            en_us.AddText(EnumText.enumForum_Forums_FieldDescription, "Description:");
            en_us.AddText(EnumText.enumForum_Forums_FieldCategory, "Category:");
            en_us.AddText(EnumText.enumForum_Forums_Moderator, "Moderator(s):");
            en_us.AddText(EnumText.enumForum_Forums_ButtonSave, "Save");
            en_us.AddText(EnumText.enumForum_Forums_ButtonCancel, "Cancel");
            en_us.AddText(EnumText.enumForum_Forums_ButtonReturn, "Return");
            en_us.AddText(EnumText.enumForum_Forums_ErrorNameRequired, "Name is required.");
            en_us.AddText(EnumText.enumForum_Forums_ErrorCategoryRequired, "Category is required.");
            en_us.AddText(EnumText.enumForum_Forums_PageNewErrorCreateNew, "Error adding a new forum:");
            en_us.AddText(EnumText.enumForum_Forums_ErrorModeratorRequired, "Moderator(s) is required.");
            en_us.AddText(EnumText.enumForum_Forums_FieldRequireField, "*Required Field");
            en_us.AddText(EnumText.enumForum_Forums_TitleEdit, "Edit a Forum");
            en_us.AddText(EnumText.enumForum_Forums_SubtitleEdit, "Here  you can edit the forum info as well as add new moderators for this forum or remove the current moderators.");
            en_us.AddText(EnumText.enumForum_Forums_FieldStatus, "Status:");
            en_us.AddText(EnumText.enumForum_Forums_HelpStatus, "<i>Normal</i> stands for that this forum is curently in normal use, while <i>Closed</i> stands for that this forum has been closed,and forum users cannot visit this forum.");
            en_us.AddText(EnumText.enumForum_Forums_PageEditErrorLoadPage, "Error loading Edit a Forum page:");
            en_us.AddText(EnumText.enumForum_Forums_PageEditErroeUpdate, "Error saving a forum:");
            en_us.AddText(EnumText.enumForum_Forums_ConfirmMoveTopic, "Are you sure you want to move this topic?");
            en_us.AddText(EnumText.enumForum_Forums_ButtonSelectedToMove, "OK");
            en_us.AddText(EnumText.enumForum_Forums_TitleSelectForum, "Move Topic To");
            en_us.AddText(EnumText.enumForum_Forums_PageSelectForumErrorLoading, "Error selecting a forum:");
            en_us.AddText(EnumText.enumForum_Forums_StatusOpen, "Open");
            en_us.AddText(EnumText.enumForum_Forums_StatusHide, "Hide");
            en_us.AddText(EnumText.enumForum_Forums_StatusLock, "Lock");
            en_us.AddText(EnumText.enumForum_Forums_StatusNormal, "Normal");
            en_us.AddText(EnumText.enumForum_Forums_StatusClose, "Closed");
            en_us.AddText(EnumText.enumForum_Forums_ModeratorPanleForumsTitle, "Forums Management");
            en_us.AddText(EnumText.enumForum_Forums_ModeratorPanleForumsSubTitle, "Forum is an online discussion site for all the participants to ask questions and share ideas. Here only display the forums which you have permission to moderate. ");
            en_us.AddText(EnumText.enumForum_Forums_ModeratorPanelForumsLoadError, "Error loading Forums Management page:");
            en_us.AddText(EnumText.enumForum_Forums_FieldCurrentForum, "Current Forum");
            en_us.AddText(EnumText.enumForum_Forums_FieldPermissions, "Permissions");
            en_us.AddText(EnumText.enumForum_Forums_ForumPermissionTabUserGroups, "User Groups");
            en_us.AddText(EnumText.enumForum_Forums_ForumPermissionTabUserReputationGroups, "Reputation Groups");
            en_us.AddText(EnumText.enumForum_Forums_ForumPermissionInherit, "Inherit Permissions from General Settings");
            en_us.AddText(EnumText.enumForum_Forums_ForumPermissionCustom, "Define My Own Permissions");
            en_us.AddText(EnumText.enumForum_Forums_forumPermissionTitleAddUserGroup, "Select User Groups");
            en_us.AddText(EnumText.enumForum_Forums_forumPermissionTitleAddReputationGroup, "Select Reputation Groups");

            en_us.AddText(EnumText.enumForum_Forums_InheritsDescriptionOfUserGroup, "With Inherit Permission from General Settings selected, the permission of the forum is defined by the permissions defined in User Groups.");
            en_us.AddText(EnumText.enumForum_Forums_InheritsDescriptionOfReputationGroup, "With Inherit Permission from General Settings selected, the permission of the forum is defined by the permissions defined in Reputation Groups.");
            en_us.AddText(EnumText.enumForum_Forums_InheritsDescriptionOfNoGroup, "With Inherit Permission from General Settings selected, the permission of the forum is defined by the permissions defined in User Permissions.");
            en_us.AddText(EnumText.enumForum_Forums_InheritsDescriptionOfBothGroup, "With Inherit Permission from General Settings selected, the permission of the forum is defined by the permissions defined in Reputation Groups and User Groups.");
            en_us.AddText(EnumText.enumForum_Forums_CommonInheritsDescription, "<br/>This option is to simply the permission management for site moderators and administrators if no particular permissions are needed for  a specific forum.");
            en_us.AddText(EnumText.enumForum_Forums_CustomDescription, "With Define My Own Permission selected, the general permission settings have no effect on the current forum. Rather, the forum has its own permission settings. You can add {0} to the permission list and define their permissions accordingly.<br/><br/>This option is for forums with particular permission needs.");
            en_us.AddText(EnumText.enumForum_Forums_AdditionalInformation, "For more information about User Group Permission and Reputation Group Permission, please refer to the interface through menu &quot;Forum Feature&quot;.");
            en_us.AddText(EnumText.enumForum_Forums_CustomDescription_UserGroup, "User Group");
            en_us.AddText(EnumText.enumForum_Forums_CustomDescription_ReputationGroup, "Reputation Group");
            en_us.AddText(EnumText.enumForum_Forums_CustomDescription_And, " and ");
            en_us.AddText(EnumText.enumForum_Forums_NoteDescription_Note, "Note: ");

            en_us.AddText(EnumText.enumForum_ForumAdd_LoadError, "Error loading New Forum page:");
            en_us.AddText(EnumText.enumForum_ForumAdd_AddModeratorError, "Error adding user(s) to Moderator(s):");
            en_us.AddText(EnumText.enumForum_ForumAdd_AddModeratorSelectUserTitle, "Add User(s) to Moderator(s)");
            en_us.AddText(EnumText.enumForum_Forum_CheckBoxNeedReplyToView, "Allow posting the \"Reply Needed to View\" topics.");
            en_us.AddText(EnumText.enumForum_Forum_CheckBoxNeedPayScoreToView, "Allow posting the \"Score Needed to View\" topics.");
            en_us.AddText(EnumText.enumForum_Forum_ButtonAddModerator, "Add Moderator");
            en_us.AddText(EnumText.enumForum_Forum_ButtonRemoveModerator, "Remove");
            en_us.AddText(EnumText.enumForum_Forum_EditSaveSucceeded, "Save succeeded.");

            #endregion

            #region Forum Topic&Post&Search&Draft 14

            en_us.AddText(EnumText.enumForum_Topic_FieldModerators, "Moderators:");
            en_us.AddText(EnumText.enumForum_Topic_PageForumErrorLoad, "Error loading Forums page:");
            en_us.AddText(EnumText.enumForum_Topic_HelpNewTopic, "New topic");
            en_us.AddText(EnumText.enumForum_Topic_PageTitleForum, "Bolt energy Forum");
            en_us.AddText(EnumText.enumForum_Topic_PageTitleDefault, "{0}");
            en_us.AddText(EnumText.enumForum_Topic_PageDefaultErrorLoad, "Error loading Home Page:");
            en_us.AddText(EnumText.enumForum_Topic_ColumnModerators, "Moderators");
            en_us.AddText(EnumText.enumForum_Topic_ColumnTopics, "Topics");
            en_us.AddText(EnumText.enumForum_Topic_FieldLockedForum, "Locked Forum");
            en_us.AddText(EnumText.enumForum_Topic_TitleDraftList, "Drafts");
            en_us.AddText(EnumText.enumForum_Topic_SubtitleDraftList, "Draft is a rough version of the comment to a topic. For a topic, there is only one draft. And only operators, including administrators have permission to write, read and revise a draft. All the operators view the same draft. Once the draft is posted, it will be deleted immediately by system.");
            en_us.AddText(EnumText.enumForum_Topic_FieldSubject, "Subject: ");
            en_us.AddText(EnumText.enumForum_Topic_ButtonQuery, "Query");
            en_us.AddText(EnumText.enumForum_Topic_ColumnTopicId, "Topic Id");
            en_us.AddText(EnumText.enumForum_Topic_ColumnTopicStarter, "Topic Author");
            en_us.AddText(EnumText.enumForum_Topic_ColumnTopicSubject, "Topic Subject");
            en_us.AddText(EnumText.enumForum_Topic_ColumnTopicStatus, "Topic Status");
            en_us.AddText(EnumText.enumForum_Topic_ColumnPostTime, "Create Time");
            en_us.AddText(EnumText.enumForum_Topic_ColumnOperation, "Operation");
            en_us.AddText(EnumText.enumForum_Topic_HelpEdit, "Edit");
            en_us.AddText(EnumText.enumForum_Topic_HelpView, "View");
            en_us.AddText(EnumText.enumForum_Topic_HelpDelete, "Delete");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmDeleteDraft, "Are you sure you want to delete this draft?");
            en_us.AddText(EnumText.enumForum_Topic_PageDraftListErrorDelete, "Error deleting a draft: ");
            en_us.AddText(EnumText.enumForum_Topic_PageDraftListErrorLoadPage, "Error loading Drafts page: ");
            en_us.AddText(EnumText.enumForum_Topic_PageDraftListErrorQuery, "Error querying drafts: ");
            en_us.AddText(EnumText.enumForum_Topic_PageDraftListErrorEdit, "Error editing a draft:");
            en_us.AddText(EnumText.enumForum_Topic_TopicStatusOpen, "Open");
            en_us.AddText(EnumText.enumForum_Topic_TopicStatusClosed, "Closed");
            en_us.AddText(EnumText.enumForum_Topic_TopicStatusMakeasanswer, "Marked as answer");
            en_us.AddText(EnumText.enumForum_Topic_TitleDraftEdit, "Edit Draft");
            en_us.AddText(EnumText.enumForum_Topic_SubtitleDraftEdit, "View the detail info of a selected topic and edit the draft of comment on this topic.");
            en_us.AddText(EnumText.enumForum_Topic_FieldPosts, "Posts");
            en_us.AddText(EnumText.enumForum_Topic_FieldDraft, "Draft");
            en_us.AddText(EnumText.enumForum_Topic_FieldContent, "Content: ");
            en_us.AddText(EnumText.enumForum_Topic_FieldAt, " at ");
            en_us.AddText(EnumText.enumForum_Topic_fieldEditInformation, "The draft was edited by {0} at {1} ");
            en_us.AddText(EnumText.enumForum_Topic_ColumnAuthor, "Posted By");
            en_us.AddText(EnumText.enumForum_Topic_ColumnMessage, "Message");
            en_us.AddText(EnumText.enumForum_Topic_ColumnPosted, "Posted: ");
            en_us.AddText(EnumText.enumForum_Topic_ColumnJoined, "Join Time:");
            en_us.AddText(EnumText.enumForum_Topic_ColumnPosts, "Posts: ");
            en_us.AddText(EnumText.enumForum_Topic_ColumnPost, "Posts");
            en_us.AddText(EnumText.enumForum_Topic_ButtonSave, "Save");
            en_us.AddText(EnumText.enumForum_Topic_ButtonPost, "Post");
            en_us.AddText(EnumText.enumForum_Topic_ButtonCancel, "Cancel");
            en_us.AddText(EnumText.enumForum_Topic_PageEditDraftErrorLoadDraft, "Error loading drafts list: ");
            en_us.AddText(EnumText.enumForum_Topic_PageEditDraftErrorLoadpage, "Error loading Edit Draft page:");
            en_us.AddText(EnumText.enumForum_Topic_PageEditDraftErrorLoadPost, "Error loading the posts: ");
            en_us.AddText(EnumText.enumForum_Topic_PageEditDraftErrorLoadTopicAndPost, "Error loading the topics and posts: ");
            en_us.AddText(EnumText.enumForum_Topic_PageEditDraftErrorUploadAttachment, "Error uploading attachment:");
            en_us.AddText(EnumText.enumForum_Topic_PageEditDraftErrorLoadAttachment, "Error loading attachment list:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorSubjectRequired, "Subject is required.");
            en_us.AddText(EnumText.enumForum_Topic_ErrorPostDraft, "Error posting a draft: ");
            en_us.AddText(EnumText.enumForum_Topic_ErrorSaveDraft, "Error saving a draft: ");
            en_us.AddText(EnumText.enumForum_Topic_FieldDownload, "Scores needed to pay for downloading:");
            en_us.AddText(EnumText.enumForum_Topic_FieldDescription, "Description:");
            en_us.AddText(EnumText.enumForum_Topic_FieldUploadattachment, "Upload attachment:");
            en_us.AddText(EnumText.enumForum_Topic_TitleMyTopics, "My Topics");
            en_us.AddText(EnumText.enumForum_Topic_HelpMyTopics, "Topic is the subject of a forum discussion. On this page, all the topics you posted are listed in chronological order from the newest to the oldest.");
            en_us.AddText(EnumText.enumForum_Topic_ColumnStatus, "Status");
            en_us.AddText(EnumText.enumForum_Topic_ColumnSubject, "Topic");
            en_us.AddText(EnumText.enumForum_Topic_ColumnDate, "Create Time");
            en_us.AddText(EnumText.enumForum_Topic_ColumnLastPost, "Activity");//Last Post
            en_us.AddText(EnumText.enumForum_Topic_ColumnReplies, "Replies");
            en_us.AddText(EnumText.enumForum_Topic_ColumnViews, "Views");
            en_us.AddText(EnumText.enumForum_Topic_ColumnPromotion, "Promotion");
            
            en_us.AddText(EnumText.enumForum_Topic_TitleMyPosts, "My Posts");
            en_us.AddText(EnumText.enumForum_Topic_HelpMyPosts, "Here are all the posts you have posted at this forum. On this page, you can set a keyword to search for the specific posts.");
            en_us.AddText(EnumText.enumForum_Topic_FieldBy, "by");
            en_us.AddText(EnumText.enumForum_Topic_StatusClosedParticipated, "Closed Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusMarkedParticipated, "Resolved Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusNormalParticipated, "Normal Topic You Participated");
            en_us.AddText(EnumText.enumForum_Topic_FieldLegend, "LEGEND");
            en_us.AddText(EnumText.enumForum_Topic_PageMyTopicsErrorLoad, "Error loading my topics:");
            en_us.AddText(EnumText.enumForum_Topic_PageMyPostsErrorLoad, "Error loading my posts:");
            en_us.AddText(EnumText.enumForum_Topic_FieldDeleteUser, "deleted user");
            en_us.AddText(EnumText.enumForum_Topic_TitleSerach, "Search Option ");
            en_us.AddText(EnumText.enumForum_Topic_TitleSearchResult, "Result:");
            en_us.AddText(EnumText.enumForum_Topic_PageTitleSearchResult, "Search Result - {0}");
            en_us.AddText(EnumText.enumForum_Topic_PageTitleSearch, "Advanced Search - {0}");
            en_us.AddText(EnumText.enumForum_Topic_HelpSerach, "Search Options help optimize your info quest by allowing multi-conditional search.");
            en_us.AddText(EnumText.enumForum_Topic_FieldKeyWords, "Subject/Content:");
            en_us.AddText(EnumText.enumForum_Topic_FieldsDisplayName, "Display Name:");
            en_us.AddText(EnumText.enumForum_Topic_FieldPostData, "Create Date:");
            en_us.AddText(EnumText.enumForum_Topic_FieldForum, "Forum(s):");
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
            en_us.AddText(EnumText.enumForum_Topic_HelpKeyWords, "Keyword helps you look for topics and posts with this word.");
            en_us.AddText(EnumText.enumForum_Topic_HelpDisplayName, "Display Name helps you look for a specific user's topics and posts.");
            en_us.AddText(EnumText.enumForum_Topic_HelpPostDate, "Post Date helps you look for topics and posts posted in a specific time span. And time format must be(mm-dd-yyyy).");
            en_us.AddText(EnumText.enumForum_Topic_HelpForum, "Forum(s) helps you look for topics and posts in a specific category or forum.");
            en_us.AddText(EnumText.enumForum_Topic_HelpTopicStatus, "Topic Status helps you look for topics and posts under a certain status.");
            en_us.AddText(EnumText.enumForum_Topic_HelpIfSticky, "If Sticky helps you look for the topics and posts put at the top of the forum.");
            en_us.AddText(EnumText.enumForum_Topic_ErrorKeyWordsRequired, "Keywords is required.");
            en_us.AddText(EnumText.enumForum_Topic_ErrorStatusRequired, "Status is required.");
            en_us.AddText(EnumText.enumForum_Topic_ErrorTimeFormat, "Time format is incorrect.");
            en_us.AddText(EnumText.enumForum_Topic_ErrorForumIsNotExist, "The Forum is not exist.");

            en_us.AddText(EnumText.enumForum_Topic_StatusClosed, "Closed Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusMarked, "Marked Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusNormal, "Normal Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusClosedUnRead, "Closed Unread Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusMarkedUnRead, "Marked Unread Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusNormalUnRead, "Normal Unread Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusClosedRead, "Closed Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusMarkedRead, "Resolved Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusNormalRead, "Normal Read Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusSticky, "Sticky Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusHotTopic, "Hot Topic");
            en_us.AddText(EnumText.enumForum_Topic_StatusFeaturedTopic, "Featured Topic");
            en_us.AddText(EnumText.enumForum_Topic_PageSearchResultErrorLoad, "Error loading Search Result page: ");
            en_us.AddText(EnumText.enumForum_Topic_Error, "Error: ");
            en_us.AddText(EnumText.enumForum_Topic_TitleAddTopic, "New Topic");
            en_us.AddText(EnumText.enumForum_Topic_BrowerTitleAddTopic, "New Topic - {0}");
            en_us.AddText(EnumText.enumForum_Topic_ButtonSubmit, "Submit");
            en_us.AddText(EnumText.enumForum_Topic_PageAddTopicErrorSubmit, "Error creating a new topic:");
            en_us.AddText(EnumText.enumForum_Topic_PageAddTopicErrorPageLoading, "Error loading New Topic page:");
            en_us.AddText(EnumText.enumForum_Topic_BrowerTitleEditTopic, "{0} - {1}");
            en_us.AddText(EnumText.enumForum_Topic_LabelEditTopic, "Edit Topic");
            en_us.AddText(EnumText.enumForum_Topic_LabelEditPost, "Edit Post");
            en_us.AddText(EnumText.enumForum_Topic_PageEditTopicOrPostErrorLoading, "Error loading Edit Topic page:");
            en_us.AddText(EnumText.enumForum_Topic_PageEditTopicOrPostErrorEdit, "Error editing a post:");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmDeletePost, "Are you sure you want to delete this post?");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmMarkAsAnswer, "Are you sure you want to mark this post as answer?");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmMarkAsAnswerInsteadOfOld, "Answer already existed. Are you sure you want to overwrite it?");
            en_us.AddText(EnumText.enumForum_Topic_LabelQuoteDeletedAuthor, "[deleted user]");
            en_us.AddText(EnumText.enumForum_Topic_FieldQuote, "Quote: ");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmDeleteTopic, "Are you sure you want to delete this topic?");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmCloseTopic, "Are you sure you want to close this topic?");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmReopenTopic, "Are you sure you want to reopen this topic?");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmStickyTopic, "Are you sure you want to make this topic sticky?");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmUnStickyTopic, "Are you sure you want to make this topic unsticky?");
            en_us.AddText(EnumText.enumForum_Topic_LinkGotoAnswer, "Go to answer");
            en_us.AddText(EnumText.enumForum_Topic_FieldPosted, "Posted: ");
            en_us.AddText(EnumText.enumForum_Topic_FieldJoined, "Join Time:");
            en_us.AddText(EnumText.enumForum_Topic_NoteClosedTopic, "Closed Topic");
            en_us.AddText(EnumText.enumForum_Topic_NoteMarkedTopic, "Marked Topic");
            en_us.AddText(EnumText.enumForum_Topic_NoteNormalTopic, "Normal Topic");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderDeletingPost, "Deleting Post");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorDeletingPost, "Error deleting a post: ");
            en_us.AddText(EnumText.enumForum_Topic_AlterPostAlreadyMarked, "This post has already been marked as answer.");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderMarkingPost, "Marking Post");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorMarkingPost, "Error marking a post: ");
            en_us.AddText(EnumText.enumForum_Topic_AlterPostAlreadyUmmarkd, "This post has already been unmarked as answer!");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderUnmarkingPost, "Unmarking Post");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorUnmarkingPost, "Error unmarking a post: ");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorLoadingPostList, "Error loading the posts list: ");
            en_us.AddText(EnumText.enumForum_Topic_BrowerTitleTopic, "{0} - {1}");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorDeletePost, "Error deleting a post: ");
            
            en_us.AddText(EnumText.enumForum_Topic_HelpUnMarkedButton, "Unmark as Answer");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmUnMarked, "Are you sure you want to unmark this post?");
            en_us.AddText(EnumText.enumForum_Topic_FieldPostMarkAsAnswer, "This post has been marked as Answer.");
            en_us.AddText(EnumText.enumForum_Topic_FieldPostEditedBy, "This post was edited by ");
            en_us.AddText(EnumText.enumForum_Topic_FieldPostEditedInfo, "This post was edited by {0} at {1}. ");
            en_us.AddText(EnumText.enumForum_Topic_TitleMoveTopic, "Move Topic To");
            en_us.AddText(EnumText.enumForum_Topic_FieldAt, "at");
            en_us.AddText(EnumText.enumForum_Topic_HelpEditPost, "Edit this post");
            en_us.AddText(EnumText.enumForum_Topic_HelpDeletePost, "Delete this post");
            en_us.AddText(EnumText.enumForum_Topic_HelpMarkButton, "Mark as Answer");
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
            en_us.AddText(EnumText.enumForum_Topic_ErrorHeaderAddingPost, "Adding Post");
            en_us.AddText(EnumText.enumForum_Topic_ErrorHeaderSavingDraft, "Saving Draft");
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
            en_us.AddText(EnumText.enumForum_Topic_FavoriteTitle, "My Favorites");
            en_us.AddText(EnumText.enumForum_Topic_FavoriteSubTitle, "Here are all the topics you have collected. On this page, you can view all the topics as well as delete them from your Favorites according to your needs.");
            en_us.AddText(EnumText.enumForum_Topic_FavoriteDeleteConfirm, "Are you sure you want to delete this topic from your Favorites?");
            en_us.AddText(EnumText.enumForum_Topic_FavoriteLoadError, "Error loading My Favorites page:");
            en_us.AddText(EnumText.enumForum_Topic_FavoriteDeleteError, "Error deleting a favorite:");
            en_us.AddText(EnumText.enumForum_Topic_SubscribesTitle, "My Subscribes");
            en_us.AddText(EnumText.enumForum_Topic_SubscribesSubTitle, "Here are all the topics you have  subscribed. After your subscription, an email will be sent  to your registered email address automatically when there is  a new response to the subscribed topics. On this page, you can view all the topics as well as delete them according to your needs.");
            en_us.AddText(EnumText.enumForum_Topic_SubscribesDeleteConfirm, "Are you sure you want to delete this topic?");
            en_us.AddText(EnumText.enumForum_Topic_SubscribesLoadError, "Error loading My Subscribes page:");
            en_us.AddText(EnumText.enumForum_Topic_SubscribesDeleteError, "Error deleting a subscribe:");
            en_us.AddText(EnumText.enumForum_Topic_SubscribeDate, "Subscribe Date");
            en_us.AddText(EnumText.enumForum_Topic_FieldCreateUser, "Author:");
            en_us.AddText(EnumText.enumForum_Topic_FieldTopicId, "Topic Id:");
            en_us.AddText(EnumText.enumForum_Topic_ColumnCreateUser, "Author");
            en_us.AddText(EnumText.enumForum_Topic_ColumnPostDate, "Create Time");
            en_us.AddText(EnumText.enumForum_Topic_ButtonSelectUser, "Select Author");
            en_us.AddText(EnumText.enumForum_Topic_HelpMove, "Move");
            en_us.AddText(EnumText.enumForum_Topic_HelpRestore, "Restore");
            en_us.AddText(EnumText.enumForum_Topic_HelpSelectUser, "Here you can only select one user whose account has not been banned.");
            en_us.AddText(EnumText.enumForum_Topic_HelpForumSelect, "Here choose a forum you wan to search in.");
            en_us.AddText(EnumText.enumForum_Topic_ForumDropDownListItemAll, "All");
            en_us.AddText(EnumText.enumForum_Topic_TopicsManagementTitle, "Topics Management");
            en_us.AddText(EnumText.enumForum_Topic_TopicsManagementSubTitleOfModeratorPanel, "Topics are the subjects of the forum discussions. All the topics posted under your forums are listed below. As a moderator, you can view, edit, delete these topics or move them to another forum as well as use the search box to query for the specific topics.");
            en_us.AddText(EnumText.enumForum_Topic_TopicsManagementSubTitleOfAdminPanel, "A topic is a collection of posts and views. All the threads in your forum are listed here. Forum administrators have permission to view, edit or delete a topic, move a topic to a more relevant board, or locate a certain topic by using the search box.");
            en_us.AddText(EnumText.enumForum_Topic_TopicsManagementLoadError, "Error loading Topics Management page:");
            en_us.AddText(EnumText.enumForum_Topic_TopicsManagementDeleteConfirm, "Are you sure you want to move this post to Recycle Bin?");
            en_us.AddText(EnumText.enumForum_Topic_TopicsManagementDeleteError, "Error deleting a topic:");
            en_us.AddText(EnumText.enumForum_Topic_TopicsManagementDefaultSortFiled, "LastPostTime");
            en_us.AddText(EnumText.enumForum_Topic_TopicsManagementDefaultSortMethod, "asc");
            en_us.AddText(EnumText.enumForum_Post_PostsManagementTitle, "Posts Management");
            en_us.AddText(EnumText.enumForum_Post_PostsManagementSubTitleOfModeratorPanel, "Posts are the comments to a topic. All the posts posted under your forums are listed below. As a moderator, you can edit or delete these posts as well as use the search box to query for the specific posts.");
            en_us.AddText(EnumText.enumForum_Post_PostsManagementSubTitleOfAdminPanel, "A post is a user submitted message contained in a topic.  Forum administrators can view, edit or delete a post, or locate a certain post by using the search box.");
            en_us.AddText(EnumText.enumForum_Post_PostsManagementLoadError, "Error loading Posts Management page:");
            en_us.AddText(EnumText.enumForum_Post_PostsManagementDeleteConfirm, "Are you sure you want to move this post to Recycle Bin?");
            en_us.AddText(EnumText.enumForum_Post_PostsManagementDeleteError, "Error deleting a post:");
            en_us.AddText(EnumText.enumForum_Topic_RecycleBinHelpKeywords, "Here the keywords query is a kind of fuzzy query. The results will show all the posts or topics that contain the keywords you have entered.");
            en_us.AddText(EnumText.enumForum_Topic_RecycleBinTitle, "Recycle Bin");
            en_us.AddText(EnumText.enumForum_Topic_RecycleBinSubTitleModeratorPanel, "Recycle Bin stores all the topics and posts which have been deleted from your forums. On this page, you can view, restore or permanently delete them. Besides, you can use the search box below to search for the specific topics or posts.");
            en_us.AddText(EnumText.enumForum_Topic_RecycleBinSubTitleAdminPanel, "All deleted topics and posts will be moved into Recycle Bin. Administrators have permission to permanently delete topics/ posts in the recycle bin, or restore deleted records before the recycle bin is erased. Restoring a deleted topic/post helps you avoid permanently losing a deleted thread/message.");
            en_us.AddText(EnumText.enumForum_Topic_RecycleBinLoadError, "Error loading Recycle Bin page:");
            en_us.AddText(EnumText.enumForum_Topic_RecycleBinRestoreConfirm, "Are you sure you want to restore this topic/post? ");
            en_us.AddText(EnumText.enumForum_Topic_RecycleBinRestoreError, "Error restoring topic/post from Recycle Bin:");
            en_us.AddText(EnumText.enumForum_Topic_RecycleBinDeleteConfirm, "Are you sure you want to delete this topic/post permanently?");
            en_us.AddText(EnumText.enumForum_Topic_RecycleBinDeleteError, "Error deleting topic/post permanently:");
            en_us.AddText(EnumText.enumForum_Post_FieldSubject, "Subject:");
            en_us.AddText(EnumText.enumForum_Post_ColumnSubject, "Subject");
            en_us.AddText(EnumText.enumForum_Post_FieldPostData, "Post Date:");
            en_us.AddText(EnumText.enumForum_Post_ColumnCreateDate, "Create Time");
            en_us.AddText(EnumText.enumForum_Post_ColumnCreateUser, "Author");
            en_us.AddText(EnumText.enumForum_Post_ColumnIsAnswer, "Is Answer");
            en_us.AddText(EnumText.enumForum_Post_HelpApprove, "Approve");
            en_us.AddText(EnumText.enumForum_Post_HelpRefuse, "Refuse");
            en_us.AddText(EnumText.enumForum_Post_HelpReApprove, "Approve");
            en_us.AddText(EnumText.enumForum_Post_HelpDelete, "Delete");
            en_us.AddText(EnumText.enumForum_Post_HelpView, "View");
            en_us.AddText(EnumText.enumForum_Post_HelpEdit, "Edit");
            en_us.AddText(EnumText.enumForum_Post_WaitingModerationTitle, "Waiting for Moderation");
            en_us.AddText(EnumText.enumForum_Post_WaitingModerationSubTitleOfModeratorPanel, "Moderation is the process that moderators approve or refuse a post before it is published. All the posts below posted under your forums are waiting for your moderation. As a moderator, you can approve or refuse a certain post according to your forum management rules and policies.");
            en_us.AddText(EnumText.enumForum_Post_WaitingModerationSubTitleOfAdminPanel, "All waiting for moderation topics and posts are listed on this page. Forum administrators have permission to approve or  refuse a waiting for moderation topic/ post. And approved topics/posts will be published immediately.");
            en_us.AddText(EnumText.enumForum_Post_WaitingModerationLoadError, "Error loading Waiting for Moderation page:");
            en_us.AddText(EnumText.enumForum_Post_WaitingModerationApproveConfirm, "Are you sure you want to approve this post?");
            en_us.AddText(EnumText.enumForum_Post_WaitingModerationRefuseConfirm, "Are you sure you want to refuse this post?");
            en_us.AddText(EnumText.enumForum_Post_WaitingModerationApproveError, "Error approving a post:");
            en_us.AddText(EnumText.enumForum_Post_WaitingModerationRefuseError, "Error refusing a post:");
            en_us.AddText(EnumText.enumForum_Post_WaitingModerationPageLoadError, "Error loading Waiting for Moderation page:");
            en_us.AddText(EnumText.enumForum_Post_WaitingModerationPagingError, "Error loading posts:");
            en_us.AddText(EnumText.enumForum_Post_WaitingModerationSortError, "Error sorting posts:");
            en_us.AddText(EnumText.enumForum_Post_WaitingModerationQueryError, "Error querying posts:");
            en_us.AddText(EnumText.enumForum_Post_WaitingModerationPageSizeError, "Error changing page size:");
            en_us.AddText(EnumText.enumForum_Post_RejectedPostsTitle, "Rejected Posts");
            en_us.AddText(EnumText.enumForum_Post_RejectedPostsSubTitleModeratorPanel, "Rejected Posts are those posts that haven't passed the moderation. On this page, you can approve the rejected posts or move them to Recycle Bin.");
            en_us.AddText(EnumText.enumForum_Post_RejectedPostsSubTitleAdminPanel, "Rejected posts are posts that have been rejected by moderators or administrators during the moderation procedure. Administrators have permission to reverse the rejection and publish rejected posts, or move them to the recycle bin.");
            en_us.AddText(EnumText.enumForum_Post_RejectedPostsLoadError, "Error loading Rejected Posts page;");
            en_us.AddText(EnumText.enumForum_Post_RejectedPostsDeleteConfirm, "Are you sure you want to move this topic to Recycle Bin?");
            en_us.AddText(EnumText.enumForum_Post_RejectedPostsDeleteError, "Error deleting a post:");
            en_us.AddText(EnumText.enumForum_Post_RejectedPostsReApproveConfirm, "Are you sure you want to approve this post?");
            en_us.AddText(EnumText.enumForum_Post_RejectedPostsReApproveError, "Error approving a post:");


            #endregion

            #region Topic 2.0 Topic & Post & Search & Froum 13

            en_us.AddText(EnumText.enumForum_AbusePost_TitleAbuse, "Report Abuse");
            en_us.AddText(EnumText.enumForum_AbusePost_LabelClose, "[Close]");
            en_us.AddText(EnumText.enumForum_AbusePost_FiledNotes, "Notes:");
            en_us.AddText(EnumText.enumForum_AbusePost_ButtonSubmit, "Submit");
            en_us.AddText(EnumText.enumForum_AbusePost_ErrorNotesIsRequired, "Notes is required.");
            en_us.AddText(EnumText.enumForum_AbusePost_ErrorAbusePost, "Error reporting abuse:");
            en_us.AddText(EnumText.enumForum_AddTopic_ConfirmDeleteThisAttachment, "Are you sure you want to delete this attachment?");
            en_us.AddText(EnumText.enumForum_AddTopic_TitleAdvancedOptions, "Advanced Options");
            en_us.AddText(EnumText.enumForum_AddTopic_TitlePostSettings, "Topic Type:");
            en_us.AddText(EnumText.enumForum_AddTopic_FiledPostNormal, "Normal");
            en_us.AddText(EnumText.enumForum_AddTopic_FiledPostNeedReplayToView, "Reply Needed to View");
            en_us.AddText(EnumText.enumForum_AddTopic_FiledPostNeedPayScoreToView, "Score Needed to View");
            en_us.AddText(EnumText.enumForum_AddTopic_FiledHowManyScoreRequired, "Score(s) are Needed.");
            en_us.AddText(EnumText.enumForum_AddTopic_ErrorPlesaeInputNumber, "The input Must be an integer. Please retype.");
            en_us.AddText(EnumText.enumForum_AddTopic_ErrorPayForTopicIsRequired, " is required.");
            en_us.AddText(EnumText.enumForum_AddTopic_TiltePollCreation, "Create Poll");
            en_us.AddText(EnumText.enumForum_AddTopic_SubTitlePollOptions, "Poll Options");
            en_us.AddText(EnumText.enumForum_AddTopic_SubTitleOtherOptions, "Other Options");
            en_us.AddText(EnumText.enumForum_AddTopic_SubTitleMulitipleChoice, "Max Choices:");
            en_us.AddText(EnumText.enumForum_AddTopic_ErrorMulitipleChoiceIsRequired, "Max Choices is required.");
            en_us.AddText(EnumText.enumForum_AddTopic_FiledPollDateTo, "Poll Date To");
            en_us.AddText(EnumText.enumForum_AddTopic_ErrorPollDateIsErrorFormat, "Poll Date format is incorrect.");
            en_us.AddText(EnumText.enumForum_AddTopic_TitleUploadAttachment, "Upload Attachment:");
            en_us.AddText(EnumText.enumForum_AddTopic_FiledDownLoadScore, "Scores needed to pay for downloading:");
            en_us.AddText(EnumText.enumForum_AddTopic_ErrorPayForDownlaodIsRequired, " is required.");
            en_us.AddText(EnumText.enumForum_AddTopic_FiledDescription, "Description:");
            en_us.AddText(EnumText.enumForum_AddTopic_ButtonUpload, "Upload");
            en_us.AddText(EnumText.enumForum_AddOrEditTopic_Page_PollDateIsRequired, "Poll Date is required.");
            en_us.AddText(EnumText.enumForum_AddOrEditTopic_Page_ToolTipDelete, "Delete");
            en_us.AddText(EnumText.enumForum_AddTopic_ErrorUploadAttachment, "Error uploading attachment:");
            en_us.AddText(EnumText.enumForum_AddTopic_ErrorLoadingAttachments, "Error loading Attachments:");
            en_us.AddText(EnumText.enumForum_AddTopic_ErrorDeleteAttachment, "Error deleting an attachment:");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_FieldKeyWord, "Keyword:");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_TitleTimeRange, "Time Range:");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_FieldAll, "All");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_Field1Day, "1 Day");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_Field7Day, "7 Days");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_Field2Weeks, "2 Weeks");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_Field1Month, "1 Month");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_Field3Months, "3 Months");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_Field6Months, "6 Months");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_Field1Years, "1 Year");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_TitleSearchWithIn, "Search within:");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_FieldPostSubjectsAndMessageText, "Post Subject and Content");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_FieldMessageTextOnly, "Post Content Only");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_FieldTopicTitlesOnly, "Topic Subject Only");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_FieldFirstPostOfTopicsOnly, "Topic Content Only");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_TitleDisplayAs, "Show Results as:");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_FieldTopics, "Topics");
            en_us.AddText(EnumText.enumForum_AdvancedSearch_FieldPosts, "Posts");
            en_us.AddText(EnumText.enumForum_BanUser_TitleBanUser, "Ban User");
            en_us.AddText(EnumText.enumForum_BanUser_TitleClose, "[Close]");
            en_us.AddText(EnumText.enumForum_BanUser_SubTitleBanUser, "Ban User:");
            en_us.AddText(EnumText.enumForum_BanUser_SubTitleExpireTime, "Expire Time:");
            en_us.AddText(EnumText.enumForum_BanUser_FiledMinuets, "Minuets");
            en_us.AddText(EnumText.enumForum_BanUser_FiledHours, "Hours");
            en_us.AddText(EnumText.enumForum_BanUser_FiledDays, "Days");
            en_us.AddText(EnumText.enumForum_BanUser_FliedMonths, "Months");
            en_us.AddText(EnumText.enumForum_BanUser_FiledYears, "Years");
            en_us.AddText(EnumText.enumForum_BanUser_FiledPermanent, "Permanent");
            en_us.AddText(EnumText.enumForum_BanUser_SubTitleNotes, "Notes:");
            //en_us.AddText(EnumText.enumForum_BanUser_SubTitleRequired, "* Required Field");
            en_us.AddText(EnumText.enumForum_BanUser_ButtonBan, "Ban");
            en_us.AddText(EnumText.enumForum_BanUser_ErrorExprieTimeIsRequired, "Expire Time is required.");
            en_us.AddText(EnumText.enumForum_BanUser_ErrorNotesIsRequired, "Notes is required.");
            en_us.AddText(EnumText.enumForum_BanUser_ErrorPleaseInputOneNumber, "The input Must be an integer. Please retype.");
            en_us.AddText(EnumText.enumForum_BanUser_PageTitleBanUser, "Ban User");
            en_us.AddText(EnumText.enumForum_BanUser_ErrorLoadingBanUserPage, "Error loading Ban User page:");
            en_us.AddText(EnumText.enumForum_BanUser_ErrorBanningUser, "Error banning user:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ConfirmDeleteTheAttachment, "Are you sure you want to delete this attachment?");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_TitleAdvancedOptions, "Advanced Options");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_TitlePostSettings, "Topic Type:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_FiledNormal, "Normal");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_FiledNeedReplyToView, "Reply Needed to View");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_FiledPayScoreToView, "Score Needed to View");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_FiledHowManyScoreRequired, "Score(s) are Needed to view this topic.");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorScoreIsRequired, "Score(s) is required.");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorPleaseInputOneNumber, "The input Must be an integer. Please retype.");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_TitlePollCreation, "Poll Creation");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_SubTitlePollOptions, "Poll Options");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_FiledOption, "Option Text");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_FiledOrder, "Order");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_FiledDelete, "Delete");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_SubTitleOtherOptions, "Other Options");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_FiledMulitipleChoice, "Max Choices:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorMulitipleChoiceIsRequired, "Max Choices is required.");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_FiledPollDateTo, "Poll Date To");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorPollDateIsErrorFormat, "Poll Date format is incorrect.");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_SubTitleUploadAttachment, "Upload Attachment:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_SubTitleDownLoad, "Scores needed to pay for downloading:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorPayForDownloadIsRequired, "Pay for downloading is required.");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_SubTitleDescription, "Description:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ButtonUpload, "Upload");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorUploadingAttachment, "Error uploading attachment:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorLoadingAttachment, "Error loading Attachments:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorDeletingAttachment, "Error deleting an attachment:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorLoadingPollOptions, "Error loading Poll Options:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorDeletingPollOption, "Error deleting poll options:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorMovingUpPollOption, "Error moving up poll option:");
            en_us.AddText(EnumText.enumForum_EditTopicOrPost_ErrorMovingDownPollOption, "Error moving down poll option:");
            en_us.AddText(EnumText.enumForum_Forum_ButtonAll, "View All Topics");
            en_us.AddText(EnumText.enumForum_Forum_ButtonFeatured, "View Featured Topics");
            en_us.AddText(EnumText.enumForum_Forum_ButtonSearch, "Search");
            en_us.AddText(EnumText.enumForum_Forum_SubTitleAnnoucements, "Annoucements");
            en_us.AddText(EnumText.enumForum_Forum_ToolTipAnnoucement, "Annoucement");
            en_us.AddText(EnumText.enumForum_Forum_ErrorShowingAllTopics, "Error Showing All Topics:");
            en_us.AddText(EnumText.enumForum_Forum_ErrorShowingFeaturedTopics, "Error Showing Featured Topics:");
            en_us.AddText(EnumText.enumForum_Forum_ErrorSearchingTopic, "Error searching topics:");
            en_us.AddText(EnumText.enumForum_Forum_ToolTipMoved, "Moved");
            en_us.AddText(EnumText.enumForum_Forum_FiledNeedScore, "{0} Scores Needed to View");
            en_us.AddText(EnumText.enumForum_Forum_ToolTipFeatured, "Featured");
            en_us.AddText(EnumText.enumForum_Forum_ToolTipHot, "Hot");
            en_us.AddText(EnumText.enumForum_Forum_ToolTipAttachments, "Attachments");
            en_us.AddText(EnumText.enumForum_Forum_ToolTipVote, "Vote");
            en_us.AddText(EnumText.enumForum_ForumIsClosed_TitleCurrentForumHasBeenClosed, " >> Current Forum Has Been Closed");
            en_us.AddText(EnumText.enumForum_ForumIsClosed_FiledCurrentForumHasBeenClosed, "Current Forum Has Been Closed");
            en_us.AddText(EnumText.enumForum_ForumIsClosed_ForumHasBeenClosed, "Forum Has Been Closed");
            en_us.AddText(EnumText.enumForum_ForumIsClosed_ForumClosedTitle, "Forum Closed");
            en_us.AddText(EnumText.enumForum_ForumIsClosed_ErrorLoadingForumIsClosedPage, "Error loading Forum Closed Page:");
            en_us.AddText(EnumText.enumForum_PayScore_TitlePayScore, "Pay Score");
            en_us.AddText(EnumText.enumForum_PayScore_SubTitleNeedPayScore, "Need Pay Score:");
            en_us.AddText(EnumText.enumForum_PayScore_SubTitleCurrentYourScore, "Your Current Scores:");
            en_us.AddText(EnumText.enumForum_PayScore_ButtonPay, "Pay");
            en_us.AddText(EnumText.enumForum_PayScore_ButtonClose, "Close");
            en_us.AddText(EnumText.enumForum_PayScore_ErrorCurrentUserHaveNotEnoughScore, "Current user do not have enough Scores.");
            en_us.AddText(EnumText.enumForum_PayScore_ErrorLoadingPayScorePage, "Error loading Pay Score page:");
            en_us.AddText(EnumText.enumForum_PayScore_ErrorPayingScore, "Error paying score:");
            en_us.AddText(EnumText.enumForum_SearchResult_FiledAuthor, "Author");
            en_us.AddText(EnumText.enumForum_SearchResult_FiledMessage, "Message");
            en_us.AddText(EnumText.enumForum_SearchResult_FiledPostSubject, "Post Subject:");
            en_us.AddText(EnumText.enumForum_SearchResult_FiledPosted, "Posted:");
            en_us.AddText(EnumText.enumForum_SearchResult_FiledForum, "Forum:");
            en_us.AddText(EnumText.enumForum_SearchResult_FiledTopic, "Topic:");
            en_us.AddText(EnumText.enumForum_SearchResult_ButtonReturn, "Return");
            en_us.AddText(EnumText.enumForum_SendMessages_SubTitleRecipeintName, "Recipient:");
            en_us.AddText(EnumText.enumForum_SendMessages_SubTitleSubject, "Subject:");
            en_us.AddText(EnumText.enumForum_SendMessages_SubTitleMessage, "Message:");
            //en_us.AddText(EnumText.enumForum_SendMessages_SubTitleRequired, "* Required Field");
            en_us.AddText(EnumText.enumForum_SendMessages_SubTitleSendMessage, "Send");
            en_us.AddText(EnumText.enumForum_SendMessages_ErrorMessageSubjectIsRequired, "Subject is required.");
            en_us.AddText(EnumText.enumForum_SendMessages_ErrorLoadingSendMessagesPage, "Error loading Send Message page:");
            en_us.AddText(EnumText.enumForum_SendMessages_ErrorSendingMessage, "Error sending message:");
            //en_us.AddText(EnumText.enumForum_UserProfile_FiledScore, "Score:");
            //en_us.AddText(EnumText.enumForum_UserProfile_FiledReputation, "Reputation:");
            en_us.AddText(EnumText.enumForum_UserProfile_TitleSendMessage, "Send Message");
            en_us.AddText(EnumText.enumForum_UserProfile_ButtonClose, "[Close]");
            en_us.AddText(EnumText.enumForum_Topic_ErrorYouShouldChooseOnePoll, "Please select at least 1 option.");
            en_us.AddText(EnumText.enumForum_Topic_ErrorMulitipleChoiceIs, "The options you can vote cannot exceed ");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipSubscribe, "Subscribe");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipUnsubscrbe, "Unsubscrbe");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipFavorite, "Add into Favorites");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipUnfavorite, "Remove from Favorites");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmFeaturedThisTopic, "Are you sure you want to mark this topic as Featured?");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmUnfeaturedThisTopic, "Are you sure you want to mark this topic as Unfeatured?");
            en_us.AddText(EnumText.enumForum_Topic_FiledPollDateTo, "Poll Date To:");
            en_us.AddText(EnumText.enumForum_Topic_FiledMulitipleChoice, "Max Choices:");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipSubmit, "Submit");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipView, "View");
            en_us.AddText(EnumText.enumForum_Topic_FiledAttachment, "Attachment");
            en_us.AddText(EnumText.enumForum_Topic_FiledUploadAttachment, "Upload Attachment:");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipDelete, "Delete");
            en_us.AddText(EnumText.enumForum_Topic_TitleDownload, "Scores needed to pay for downloading:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorPleaseInputOneNumber, "The input Must be an integer. Please retype");
            en_us.AddText(EnumText.enumForum_Topic_ErrorPayForDownloadIsRequired, "Pay for downloading is required.");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipDescription, "Description:");
            en_us.AddText(EnumText.enumForum_Topic_ButtonUpload, "Upload");
            en_us.AddText(EnumText.enumForum_Topic_SubTitleOption, "Option Text");
            en_us.AddText(EnumText.enumForum_Topic_FiledPecent, "Pecent");
            en_us.AddText(EnumText.enumForum_Topic_FiledPer, "Per");
            en_us.AddText(EnumText.enumForum_Topic_SubTitleTotalNumJoinTheVote, "Total Number of who have voted:");
            en_us.AddText(EnumText.enumForum_Topic_ButtonClose, "Close");
            en_us.AddText(EnumText.enumForum_Topic_TitleSendMessage, "Send Message");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipSendMessage, "Send Message");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipBanUser, "Ban User");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipLiftBan, "Lift Ban");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmDeleteAnnoucementPost, "This post will be deleted permanently after this operation. Are you sure you want to go on?");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmDeleteAnnoucement, "This annoucement will be deleted permanently after this operation. Are you sure you want to go on?");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderPollingTopic, "Voting for Poll");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorPollingTopic, "Error Voting for this Poll:");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderPayingTopic, "Paying for Topic:");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorPayingTopic, "Error Paying for this Topic:");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderApprovalingModeration, "Approving Post");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorApprovalingModeration, "Error Approving this Post:");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderUnApprovalingModeration, "Refusing Post");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorUnApprovalingModeration, "Error Refusing this Post:");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderRefusingAbuse, "Refusing Abuse");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorRefusingAbuse, "Error Refusing Abuse:");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderApprovalingAbuse, "Approving Abuse");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorApprovalingAbuse, "Error Approving Abuse:");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderUnBanning, "lifting Ban");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorUnBanning, "Error Lifting Ban:");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderReOpenning, "ReOpening");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorReOpenning, "Error ReOpening this Topic:");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderCloseing, "Closing");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorCloseing, "Error Closing this Topic:");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderDeleteingPermanently, "Permanently Deleting ");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorDeleteingPermanently, "Error Permanently Deleting this Topic :");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderRestoring, "Restoring");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorRestoring, "Error Restoring this Topic:");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderFavoritingTopic, "Adding Topic into Favorites");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorFavoritingTopic, "Error Adding Topic into Favorites:");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderUnFavoritingTopic, "Removing Topic from Favorites");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorUnFavoritingTopic, "Error Removing Topic from Favorites:");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderFeaturingTopic, "Marking Topic as Featutred");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorFeaturingTopic, "Error Marking Topic as Featured:");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicExpHeaderUnFeaturingTopic, "Marking Topic as Unfeatutred");
            en_us.AddText(EnumText.enumForum_Topic_PageTopicErrorUnFeaturingTopic, "Error Marking Topic as Unfeatutred:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorSubscribingTopic, "Error subscribing a topic:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorUnsubscribingTopic, "Error unsubscribing a topic:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorFavoritingTopic, "Error adding a topic into Favorites:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorUnfavoritingTopic, "Error removing a topic from Favorites:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorFeaturedingTopic, "Error marking a topic as featured:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorUnfeaturedTopic, "Error marking a topic as unfeatured:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorLoadingAttachmentList, "Error loading Attachment List:");
            en_us.AddText(EnumText.enumForum_Topic_ThisTopicHaveBeenMovedToThe, "This topic has been moved to ");
            en_us.AddText(EnumText.enumForum_Topic_ToViewThisTopicPleaseClickTheUrl, "To view this topic, please click ");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmPayScoresForThisTopic, "Are you sure you want to pay Score for this topic?");
            en_us.AddText(EnumText.enumForum_Topic_HaveEnoughScoresViewTopic, "You cannot view this topic until you have paid {0} scores. Your current Scores are {1}.");
            en_us.AddText(EnumText.enumForum_Topic_HaveNotEnoughScoresViewTopic, "You cannot view this topic until you have paid {0} scores. Your current Scores are {1}, and it is not enough to pay for this topic.");
            en_us.AddText(EnumText.enumForum_Topic_LoginAndViewTopic, "You cannot view this topic. Please log in to pay for it first.");
            en_us.AddText(EnumText.enumForum_Topic_ReplyAndViewTpoic, "You cannot view this topic until you reply.");
            en_us.AddText(EnumText.enumForum_Topic_FiledScore, "Scores:");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipReputation, "Reputation");
            en_us.AddText(EnumText.enumForum_Topic_SpiltLine, "******************************************");
            en_us.AddText(EnumText.enumForum_Topic_PostUnverified, "This post was refused");
            en_us.AddText(EnumText.enumForum_Topic_WaitingForModeration, "This post is still under review");
            en_us.AddText(EnumText.enumForum_Topic_ThisIsASpam, "This post has been confirmed as spam.");
            en_us.AddText(EnumText.enumForum_Topic_ThisIsAabusedPost, "This post has been reported abuse");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipAbuseThisPost, "Report Abuse");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmApproveTheAbuse, "Are you sure you want to approve the reported abuse?");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipApproveAbuse, "Approve Abuse");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmRefuseTheAbuse, "Are you sure you want to refuse the reported abuse?");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipRefuseAbuse, "Refuse Abuse");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmApprovalThisPost, "Are you sure you want to approve this post?");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipApprovalPost, "Approve");
            en_us.AddText(EnumText.enumForum_Topic_ConfirmUnapprovalThisPost, "Are you sure you want to refuse this post?");
            en_us.AddText(EnumText.enumForum_Topic_ToolTipUnapprovalPost, "Refuse");
            en_us.AddText(EnumText.enumForum_Topic_ErrorHeaderUploadingAttahment, "Uploading Attahment");
            en_us.AddText(EnumText.enumForum_Topic_ErrorUploadingAttahment, "Error uploading attachment:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorLoadingPostingAttachmentsList, "Error loading Posting Attachment List:");
            en_us.AddText(EnumText.enumForum_Topic_ErrorDeletingAttachment, "Error deleting an attachment:");
            en_us.AddText(EnumText.enumForum_Topic_Page_ConfirmFavoriate, "Are you sure you want to add this topic into your Favorites?");
            en_us.AddText(EnumText.enumForum_Topic_Page_ConfirmUnFavoriate, "Are you sure you want to remove this topic from your Favorites?");
            en_us.AddText(EnumText.enumForum_Topic_Page_FavoriateSuccess, "This topic has been added into your Favorites successfully!");
            en_us.AddText(EnumText.enumForum_Topic_Page_UnFavoriateSuccess, "This topic has been removed from your Favorites successfully!");
            en_us.AddText(EnumText.enumForum_Topic_Page_NeedPayAttachment, "[{0} scores are needed to download this attachment]");
            en_us.AddText(EnumText.enumForum_Topic_Page_PayScore, "Pay");
            en_us.AddText(EnumText.enumForum_Topic_Page_Reputation, "Reputation:");
            en_us.AddText(EnumText.enumForum_Topic_Page_ConfirmUnBanUser, "Are you sure you want to lift the ban of this user?");
            en_us.AddText(EnumText.enumForum_Topic_Page_ConfirmDeletePermanentely, "Are you sure you want to delete this post permanently?");
            en_us.AddText(EnumText.enumForum_Topic_Page_Filed_DeletePaermanentely, "Delete Permanently");
            en_us.AddText(EnumText.enumForum_Topic_Page_ConfirmResotre, "Are you sure you want to restore this post?");
            en_us.AddText(EnumText.enumForum_Topic_Page_Filed_Restore, "Restore");
            en_us.AddText(EnumText.enumForum_Topic_Page_ConfirmClose, "Are you sure you want to close this topic?");
            en_us.AddText(EnumText.enumForum_Topic_Page_Filed_Close, "Close");
            en_us.AddText(EnumText.enumForum_Topic_Page_ConfirmReOpen, "Are you sure you want to reopen this topic?");
            en_us.AddText(EnumText.enumForum_Topic_Page_Filed_ReOpen, "Reopen");
            en_us.AddText(EnumText.enumForum_Topic_Page_Filed_Abuse, "Report Abuse");
            en_us.AddText(EnumText.enumForum_Topic_Page_Filed_ApprovalAbuse, "Approve Abuse");
            en_us.AddText(EnumText.enumForum_Topic_Page_Filed_RefuseAbuse, "Refuse Abuse");
            en_us.AddText(EnumText.enumForum_Topic_Page_Filed_Approval, "Approve");
            en_us.AddText(EnumText.enumForum_Topic_Page_Filed_UnApproval, "Refuse");
            en_us.AddText(EnumText.enumForum_Topic_Page_Button_NewTopic, "New Topic");
            en_us.AddText(EnumText.enumForum_Topic_Page_Button_PostReply, "Post Reply");
            en_us.AddText(EnumText.enumForum_Topic_Page_Button_Favourite, "Add into Favorites");
            en_us.AddText(EnumText.enumForum_Topic_Page_Button_UnFavourite, "Remove from Favorites");
            en_us.AddText(EnumText.enumForum_Topic_Page_Button_Featured, "Mark as Featured");
            en_us.AddText(EnumText.enumForum_Topic_Page_Button_UnFeatured, "Mark as Unfeatured");
            en_us.AddText(EnumText.enumForum_Topic_Page_Button_Delete, "Delete");
            en_us.AddText(EnumText.enumForum_Topic_Page_Button_Close, "Close");
            en_us.AddText(EnumText.enumForum_Topic_Page_Button_ReOpen, "Reopen");
            en_us.AddText(EnumText.enumForum_Topic_Page_Button_Move, "Move");
            en_us.AddText(EnumText.enumForum_Topic_Page_Button_Sticky, "Sticky");
            en_us.AddText(EnumText.enumForum_Topic_Page_Button_UnSticky, "Unsticky");
            en_us.AddText(EnumText.enumForum_Topic_UnMarkedButton, "Unmark");
            en_us.AddText(EnumText.enumForum_Topic_EditPostButton, "Edit");
            en_us.AddText(EnumText.enumForum_Topic_DeletePostButton, "Delete");
            en_us.AddText(EnumText.enumForum_Topic_QuickReplyButton, "Reply");

            #endregion

            #region Ban 12

            en_us.AddText(EnumText.enumForum_Ban_ButtonBanUser, "Ban User");
            en_us.AddText(EnumText.enumForum_Ban_ButtonBanIP, "Ban IP");
            en_us.AddText(EnumText.enumForum_Ban_TitleBanList, "Banned List");
            en_us.AddText(EnumText.enumForum_Ban_SubtitleBanList, "Banned List is a list of those users and IPs which have been banned by forum administrators. On this page, you can choose to ban a user or IP as well as edit or lift the existing bans according to your needs.");
            en_us.AddText(EnumText.enumForum_Ban_PageBanListErrorLoad, "Error loading Banned List page:");
            en_us.AddText(EnumText.enumForum_Ban_PageBanListErrorQuery, "Error querying banned user/IP:");
            en_us.AddText(EnumText.enumForum_Ban_PageBanListErrorDelete, "Error lifting a ban:");
            en_us.AddText(EnumText.enumForum_Ban_PageBanListErrorSort, "Error sorting Banned List:");
            en_us.AddText(EnumText.enumForum_Ban_FieldBanUserIP, "User/IP:");
            en_us.AddText(EnumText.enumForum_Ban_ColumnBanUserIP, "User/IP");
            en_us.AddText(EnumText.enumForum_Ban_ColumnBeginTime, "Begin Time");
            en_us.AddText(EnumText.enumForum_Ban_ColumnUnbannedTime, "Expire Time");
            en_us.AddText(EnumText.enumForum_Ban_ColumnOperator, "Operator");
            en_us.AddText(EnumText.enumForum_Ban_ColumnNotes, "Notes");
            en_us.AddText(EnumText.enumForum_Ban_ColumnOperation, "Operation");
            en_us.AddText(EnumText.enumForum_Ban_HelpEdit, "Edit");
            en_us.AddText(EnumText.enumForum_Ban_HelpDelete, "Delete");
            en_us.AddText(EnumText.enumForum_Ban_ConfirmUnBan, "Are you sure you want to lift the ban?");
            en_us.AddText(EnumText.enumForum_Ban_PageBanUserErrorLoad, "Error loading Ban User page:");
            en_us.AddText(EnumText.enumForum_Ban_PageBanUserErrorSave, "Error saving ban user:");
            en_us.AddText(EnumText.enumForum_Ban_PageBanUserErrorQuery, "Error selecting user to ban:");
            en_us.AddText(EnumText.enumForum_Ban_PageBanUserErrorSelect, "Error querying user:");
            en_us.AddText(EnumText.enumForum_Ban_ButtonSelect, "Select User");
            en_us.AddText(EnumText.enumForum_Ban_TitleBanUser, "Ban User");
            en_us.AddText(EnumText.enumForum_Ban_SubtitleBanUser, "Here you can ban a user. When banning a user, you have to set a time when the ban will be expired.");
            en_us.AddText(EnumText.enumForum_Ban_ErrorIpFormat, "IP format is invalid.");
            en_us.AddText(EnumText.enumForum_Ban_ErrorStartIPFormat, "Start IP format is invalid.");
            en_us.AddText(EnumText.enumForum_Ban_ErrorEndIPFormat, "End IP format is invalid.");
            en_us.AddText(EnumText.enumForum_Ban_ErrorExpireInteger, "The input Must be an integer. Please retype.");
            en_us.AddText(EnumText.enumForum_Ban_ErrorUserRequire, "User is required.");
            en_us.AddText(EnumText.enumForum_Ban_ErrorStartIpRequire, "Start IP is required.");
            en_us.AddText(EnumText.enumForum_Ban_ErrorEndIpRequire, "End IP is required.");
            en_us.AddText(EnumText.enumForum_Ban_ErrorExpireTimeRequire, "Expire Time is required.");
            en_us.AddText(EnumText.enumForum_Ban_ErrorIpRequire, "IP is required.");
            en_us.AddText(EnumText.enumForum_Ban_TimeTypePermanent, "Permanent");
            en_us.AddText(EnumText.enumForum_Ban_TimeTypeDays, "Days");
            en_us.AddText(EnumText.enumForum_Ban_TimeTypeHours, "Hours");
            en_us.AddText(EnumText.enumForum_Ban_ColumnBanMode, "Banned Mode");
            en_us.AddText(EnumText.enumForum_Ban_FieldUser, "User:");
            en_us.AddText(EnumText.enumForum_Ban_FieldSelectType, "Select Type:");
            en_us.AddText(EnumText.enumForum_Ban_FieldIp, "IP:");
            en_us.AddText(EnumText.enumForum_Ban_FieldStartIp, "Start IP:");
            en_us.AddText(EnumText.enumForum_Ban_FieldEndIp, "End IP:");
            en_us.AddText(EnumText.enumForum_Ban_FieldExpireTime, "Expire Time:");
            en_us.AddText(EnumText.enumForum_Ban_FieldNotes, "Notes:");
            en_us.AddText(EnumText.enumForum_Ban_TitleSelectUser, "Select User");
            en_us.AddText(EnumText.enumForum_Ban_ButtonAdd, "OK");
            en_us.AddText(EnumText.enumForum_Ban_BanIPTypeIP, "IP");
            en_us.AddText(EnumText.enumForum_Ban_BanIPTypeIPRange, "IP Range");
            en_us.AddText(EnumText.enumForum_Ban_TitleBanIP, "Ban IP");
            en_us.AddText(EnumText.enumForum_Ban_SubTitleBanIP, "Here you can ban an IP or IP range. When banning an IP or IP range, you have to set a time when the ban will be expired.");
            en_us.AddText(EnumText.enumForum_Ban_PageBanIPErrorLoad, "Error loading Ban IP page:");
            en_us.AddText(EnumText.enumForum_Ban_PageBanIPErrorSave, "Error saving ban IP:");
            en_us.AddText(EnumText.enumForum_Ban_PageEditBanErrorLoad, "Error loading Edit a Ban page:");
            en_us.AddText(EnumText.enumForum_Ban_PageEditBanErrorSave, "Error saving edit a ban:");
            en_us.AddText(EnumText.enumForum_Ban_PageEditBanTitle, "Edit a ban");
            en_us.AddText(EnumText.enumForum_Ban_PageEditBanSubTitle, "Here you can edit the expire time and note for the ban.");


            #endregion

            #region Abuses 11
            en_us.AddText(EnumText.enumForum_Abuse_FieldStatus, "Status:");
            en_us.AddText(EnumText.enumForum_Abuse_StatusAll, "All");
            en_us.AddText(EnumText.enumForum_Abuse_StatusPending, "Pending");
            en_us.AddText(EnumText.enumForum_Abuse_StatusApproved, "Confirmed");
            en_us.AddText(EnumText.enumForum_Abuse_StatusRefused, "Refused");
            en_us.AddText(EnumText.enumForum_Abuse_ColumnReportTime, "Report Time");
            en_us.AddText(EnumText.enumForum_Abuse_ColumnReportUser, "Report User");
            en_us.AddText(EnumText.enumForum_Abuse_ColumnPostUser, "Post User");
            en_us.AddText(EnumText.enumForum_Abuse_ColumnNotes, "Notes");
            en_us.AddText(EnumText.enumForum_Abuse_ColumnStatus, "Status");
            en_us.AddText(EnumText.enumForum_Abuse_AbusedReportsTitle, "Abuse Report");
            en_us.AddText(EnumText.enumForum_Abuse_AbusedReportsSubTitle, "Abuse Report allows you to easily view the posts which have been reported by other users. There are three statuses for the reported posts: <i>Confirmed</i>, <i>Pending</i> and <i>Refused</i>. On this page, you can use the search box below to query for the specific reported posts.");
            en_us.AddText(EnumText.enumForum_Abuse_AbusedReportsLoadError, "Error loading Abuse Report page:");
            #endregion

            #region Annoucement 10

            en_us.AddText(EnumText.enumForum_Announcement_FieldSubject, "Subject:");
            en_us.AddText(EnumText.enumForum_Announcement_FieldForum, "Forum(s):");
            en_us.AddText(EnumText.enumForum_Announcement_FieldContent, "Content:");
            en_us.AddText(EnumText.enumForum_Announcement_FieldBeginDate, "Begin Date:");
            en_us.AddText(EnumText.enumForum_Announcement_FieldExpireDate, "Expire Date:");
            en_us.AddText(EnumText.enumForum_Announcement_ButtonNewAnnouncement, "New Announcement");
            en_us.AddText(EnumText.enumForum_Announcement_BuutonCancel, "Cancel");
            en_us.AddText(EnumText.enumForum_Announcement_BuutonQuery, "Query");
            en_us.AddText(EnumText.enumForum_Announcement_ButtonSave, "Save");
            en_us.AddText(EnumText.enumForum_Announcement_ColumnSubject, "Subject");
            en_us.AddText(EnumText.enumForum_Announcement_ColumnBeginDate, "Begin<br/>Date");
            en_us.AddText(EnumText.enumForum_Announcement_ColumnExpireDate, "Expire<br/>Date");
            en_us.AddText(EnumText.enumForum_Announcement_ColumnCreateTime, "Create Time");
            en_us.AddText(EnumText.enumForum_Announcement_ColumnCreateUser, "Author");
            en_us.AddText(EnumText.enumForum_Announcement_ColumnForum, "Forum(s)");
            en_us.AddText(EnumText.enumForum_Announcement_ColumnOperation, "Operation");
            en_us.AddText(EnumText.enumForum_Announcement_HelpDelete, "Delete");
            en_us.AddText(EnumText.enumForum_Announcement_HelpEdit, "Edit");
            en_us.AddText(EnumText.enumForum_Announcement_DropDownListForumAll, "All");
            en_us.AddText(EnumText.enumForum_Announcement_SubjectRequired, "Subject is required.");
            en_us.AddText(EnumText.enumForum_Announcement_BeginDateRequired, "Begin Date is required.");
            en_us.AddText(EnumText.enumForum_Announcement_ExpireDateRequired, "Expire Date is required.");
            en_us.AddText(EnumText.enumForum_Announcement_BeginDateFormat, "Begin Date format is incorrect.");
            en_us.AddText(EnumText.enumForum_Announcement_ExpireDateFormat, "Expire Date format is incorrect.");
            en_us.AddText(EnumText.enumForum_Announcement_BeginDateShouldEarlierThanExpireDate, "Begin Date cannot be larger than Expire Date.");
            en_us.AddText(EnumText.enumForum_Announcement_ForumRequired, "Forum(s) is required.");
            en_us.AddText(EnumText.enumForum_Announcements_Title, "Announcements");
            en_us.AddText(EnumText.enumForum_Announcements_SubTitle, "Announcements are the important information you want to publish on your forums. As a forum administrator, you can create a new announcement, view and edit the existing announcements as well as query for the specific announcements.");
            en_us.AddText(EnumText.enumForum_Announcements_ErrorLoad, "Error loading Announcements page:");
            en_us.AddText(EnumText.enumForum_Announcements_ErrorDelete, "Error deleting an announcement:");
            en_us.AddText(EnumText.enumForum_Announcements_DeleteConfirm, "Are you sure you want to delete this announcement?");
            en_us.AddText(EnumText.enumForum_AnnouncementAdd_Title, "New Announcement");
            en_us.AddText(EnumText.enumForum_AnnouncementAdd_SubTitle, "Here you can create a new announcement. Enter the Subject and Content of the announcement below, and select one or more forums on which this announcement will be published.");
            en_us.AddText(EnumText.enumForum_AnnouncementAdd_ErrorLoad, "Error loading New Announcement page:");
            en_us.AddText(EnumText.enumForum_AnnouncementAdd_ErrorAddAnnouncement, "Error adding a new announcement:");
            en_us.AddText(EnumText.enumForum_AnnouncementList_Title, "Announcements");
            en_us.AddText(EnumText.enumForum_AnnouncementList_SubTitle, "Announcements are the important information you want to publish on your forums. As a forum administrator, you can create a new announcement, view and edit the existing announcements as well as query for the specific announcements.");
            en_us.AddText(EnumText.enumForum_AnnouncementList_ErrorLoad, "Error loading Announcements page:");
            en_us.AddText(EnumText.enumForum_AnnouncementList_ErrorDelete, "Error deleting an announcement:");
            en_us.AddText(EnumText.enumForum_AnnouncementList_ConfirmDelete, "Are you sure you want to delete this announcement?");
            en_us.AddText(EnumText.enumForum_AnnouncementCreate_Title, "New Announcement");
            en_us.AddText(EnumText.enumForum_AnnouncementCreate_SubTitle, "Here you can create a new announcement. Enter the Subject and Content of the announcement below, and select one or more forums on which this announcement will be published.");
            en_us.AddText(EnumText.enumForum_AnnouncementCreate_ErrorLoad, "Error loading New Announcement page:");
            en_us.AddText(EnumText.enumForum_AnnouncementCreate_ErrorSaveAnnouncement, "Error adding a new announcement:");
            en_us.AddText(EnumText.enumForum_AnnouncementCreate_ErrorForumRequired, "Please select at least one forum to publish your announcement.");
            en_us.AddText(EnumText.enumForum_AnnouncementEdit_Title, "Edit an Announcement");
            en_us.AddText(EnumText.enumForum_AnnouncementEdit_SubTitle, "Here you can edit the selected announcement.");
            en_us.AddText(EnumText.enumForum_AnnouncementEidt_ErrorLoad, "Error loading Edit an Announcement page:");
            en_us.AddText(EnumText.enumForum_AnnouncementEdit_ErrorSaveAnnouncement, "Error saving an announcement:");
            en_us.AddText(EnumText.enumForum_AnnouncementEdit_SaveSucceeded, "Save succeeded.");

            #endregion

            #region Forum Dashboard 9

            en_us.AddText(EnumText.enumForum_Dashboard_Title, "Dashboard");
            en_us.AddText(EnumText.enumForum_Dashboard_FieldNews, "News");
            en_us.AddText(EnumText.enumForum_Dashboard_News1, "1. Comm100 Forum Version 2.0 is to hit the market in a week.");
            en_us.AddText(EnumText.enumForum_Dashboard_News2, "2. Comm100 is a professional software company providing open source and free hoste customer support and communication software for small and medium businesses. Comm100 Forum is an ASP.NET and SQL Server based open source forum.");

            #endregion

            #region Forum User 8

            en_us.AddText(EnumText.enumForum_User_TitleUserManagementList, "Users Management");
            en_us.AddText(EnumText.enumForum_User_SubtitleUserManagementList, "A user refers to a forum member who has registered from your forum site. Administrators can add a new user, edit a user’s profile, activate/deactivate a user, ban/ unban and even delete a user.<br/> <br/>When Message feature is enabled in Forum Feature, administrators can send site messages to a selected user.");
            en_us.AddText(EnumText.enumForum_User_SubtitleUserManagementListForOpenSource, "Users here include all the administrators, moderators and the external users who have registered for your forum. As an administrator, you can create a new user, edit, active or inactive, ban or unban and even delete a certain user according to your needs. In addition, you can send site messages to a certain user.");
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
            en_us.AddText(EnumText.enumForum_User_ConfirmDelete, "Are you sure you want to delete this user?");
            en_us.AddText(EnumText.enumForum_User_PageUserManagementListErrorLoad, "Error loading Users Management page: ");
            en_us.AddText(EnumText.enumForum_User_PageUserManagementListErrorGet, "Error getting users:");
            en_us.AddText(EnumText.enumForum_User_PageUserManagementListErrorQuery, "Error querying users by Display Name:");
            en_us.AddText(EnumText.enumForum_User_PageUserManagementListErrorDelete, "Error deleting a user: ");
            en_us.AddText(EnumText.enumForum_User_TitleUserView, "User Profile");
            en_us.AddText(EnumText.enumForum_User_SubtitleUserView, "Here you can view user's personal information and posts.");
            en_us.AddText(EnumText.enumForum_User_FieldAge, "Age:");
            en_us.AddText(EnumText.enumForum_User_FieldAvatar, "Avatar:");
            en_us.AddText(EnumText.enumForum_User_FieldAvatarText, "Avatar");
            en_us.AddText(EnumText.enumForum_User_FieldBasicInformation, "Basic Information");
            en_us.AddText(EnumText.enumForum_User_FieldClose, "Close");
            en_us.AddText(EnumText.enumForum_User_FieldCompany, "Organization:");
            en_us.AddText(EnumText.enumForum_User_FieldEmail, "Email:");
            en_us.AddText(EnumText.enumForum_User_FieldFaxNumber, "Fax:");
            en_us.AddText(EnumText.enumForum_User_FieldFemale, "Female");
            en_us.AddText(EnumText.enumForum_User_FieldGender, "Gender:");
            en_us.AddText(EnumText.enumForum_User_FieldHomePage, "Home Page:");
            en_us.AddText(EnumText.enumForum_User_FieldInterests, "Interest:");
            en_us.AddText(EnumText.enumForum_User_FieldItsasecret, "It's a secret");
            en_us.AddText(EnumText.enumForum_User_FieldJoined, "Join time:");
            en_us.AddText(EnumText.enumForum_User_FieldLastVisit, "Last Visit:");
            en_us.AddText(EnumText.enumForum_User_FieldMale, "Male");
            en_us.AddText(EnumText.enumForum_User_FieldOccupation, "Occupation:");
            en_us.AddText(EnumText.enumForum_User_FieldPosts, "Posts:");
            en_us.AddText(EnumText.enumForum_User_FieldPhoneNumber, "Phone:");
            en_us.AddText(EnumText.enumForum_User_FieldStatisticalInformation, "Statistical Information");
            en_us.AddText(EnumText.enumForum_User_FieldUserName, "User Name:");
            en_us.AddText(EnumText.enumForum_User_FieldVisibletoPublic, "Visible to Public");
            en_us.AddText(EnumText.enumForum_User_PageUserViewErrorLoad, "Error loading Edit Profile page:");
            en_us.AddText(EnumText.enumForum_User_TitleUserModeration, "Users Moderation");
            en_us.AddText(EnumText.enumForum_User_SubtitleUserModeration, "User Moderation is the process that forum administrators approve or refuse a user's registration request. All the users below are waiting for your moderation. You can approve or refuse their registration requests according to your forum management rules and policies.");
            en_us.AddText(EnumText.enumForum_User_ColumnEmailVerification, "Email Verification");
            en_us.AddText(EnumText.enumForum_User_ColumnModerate, "Approve");
            en_us.AddText(EnumText.enumForum_User_ColumnRefuse, "Refuse");
            en_us.AddText(EnumText.enumForum_User_HelpModerate, "Moderate");
            en_us.AddText(EnumText.enumForum_User_HelpModerateApprove, "Approve");
            en_us.AddText(EnumText.enumForum_User_HelpRefuse, "Refuse");
            en_us.AddText(EnumText.enumForum_User_ConfirmAccept, "Are you sure you want to approve this user's registration request?");
            en_us.AddText(EnumText.enumForum_User_ConfirmRefuse, "Are you sure you want to refuse this user's registration request?");
            en_us.AddText(EnumText.enumForum_User_PageModerationErrorGet, "Error getting users who are waiting for moderation:");
            en_us.AddText(EnumText.enumForum_User_PageModerationErrorLoad, "Error loading Users Moderation page: ");
            en_us.AddText(EnumText.enumForum_User_PageModerationErrorAccept, "Error approving a user's registration request:");
            en_us.AddText(EnumText.enumForum_User_PageModerationErrorModerate, "Error moderating a user;");
            en_us.AddText(EnumText.enumForum_User_PageModerationError, "Error: ");
            en_us.AddText(EnumText.enumForum_User_PageModerationErrorQuery, "Error querying users who are waiting for moderation:");
            en_us.AddText(EnumText.enumForum_User_PageModerationErrorRefuse, "Error refusing a user's registration request:");
            en_us.AddText(EnumText.enumForum_User_UserStateNeedLess, "needless");
            en_us.AddText(EnumText.enumForum_User_UserStateVerified, "verified");
            en_us.AddText(EnumText.enumForum_User_UserStateNotVerfied, "not verified");
            en_us.AddText(EnumText.enumForum_User_TitleEditProfile, "Edit Profile");
            en_us.AddText(EnumText.enumForum_User_SubtitleEditProfile, "Profile is your detailed registration info at this forum. On this page, you can edit all the info and decide whether to make it public or not.");
            en_us.AddText(EnumText.enumForum_User_FieldFirstName, "First Name:");
            en_us.AddText(EnumText.enumForum_User_FieldLastName, "Last Name:");
            en_us.AddText(EnumText.enumForum_User_ButtonSave, "Save");
            en_us.AddText(EnumText.enumForum_User_PageEditProfileErrorLoading, "Error loading Edit Profile page: ");
            en_us.AddText(EnumText.enumForum_User_PageEditProfileSuccessSave, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_User_PageEditProfileErrorSave, "Error saving the user's profile:");
            en_us.AddText(EnumText.enumForum_User_ErrorAge, "Age must be an integer in 0~100.");
            en_us.AddText(EnumText.enumForum_User_ErrorDisplayNameFormat, "Display Name format is incorrect.");
            en_us.AddText(EnumText.enumForum_User_HelpDisplayNameFormat, "Display Name is composed of the following characters: letters A-Z, a-z, and digits o-9. Here please note a display name cannot begin with a digit and it must be visible to public.");
            en_us.AddText(EnumText.enumForum_User_ErrorDisplayNameRequired, "Display Name is required.");
            en_us.AddText(EnumText.enumForum_User_ErrorEmailFormat, "Email format is incorrect.");
            en_us.AddText(EnumText.enumForum_User_ErrorEmailRequired, "Email is required.");
            en_us.AddText(EnumText.enumForum_User_ErrorOfScoreRequired, "Scores is required.");
            en_us.AddText(EnumText.enumForum_User_ErrorOfScoreRange, "Scores Must be in -2147483647~2147483647.");
            en_us.AddText(EnumText.enumForum_User_ErrorOfReputationRequired, "Reputation is required.");
            en_us.AddText(EnumText.enumForum_User_ErrorOfReputationRange, "Reputation Must be in -2147483647~2147483647.");
            en_us.AddText(EnumText.enumForum_User_TitleEditAvatar, "Edit Avatar");
            en_us.AddText(EnumText.enumForum_User_SubtitleEditAvatar, "Avatar is a picture you have used as your personal image. On this page, you can change your  avatar by customizing  your own avatar or choosing one from the default avatars.");
            en_us.AddText(EnumText.enumForum_User_LabelSystemAvatars, "Avatars:");
            en_us.AddText(EnumText.enumForum_User_LabelCurrentAvatar, "Current Avatar:");
            en_us.AddText(EnumText.enumForum_User_FieldNewAvatar, "New Avatar:");
            en_us.AddText(EnumText.enumForum_User_HelpUploadDescription, "Here you can upload your own avatar. All major image formats are supported and the maximum size allowed here is 50K. The optimal ratio of width and height is 1:1. The size of the picture you upload can not exceed 60*60, otherwise it will be resized in the proportion.");
            en_us.AddText(EnumText.enumForum_User_PageEditAvatarErrorLoading, "Error loading Edit Avatar page:: ");
            en_us.AddText(EnumText.enumForum_User_PageEditAvatarSuccessSave, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_User_PageEditAvatarErrorUpload, "Error uploading your avatar:");
            en_us.AddText(EnumText.enumForum_User_PageEditAvatarErrorSave, "Error saving edit avatar:");
            en_us.AddText(EnumText.enumForum_User_PageEditAvatarErrorDefault, "Error defaulting the default avatars:");
            en_us.AddText(EnumText.enumForum_User_FieldCustomAvatar, "Custom Avatar");
            en_us.AddText(EnumText.enumForum_User_FieldSystemAvatar, "Default Avatars");
            en_us.AddText(EnumText.enumForum_User_ButtonDefault, "Default");
            en_us.AddText(EnumText.enumForum_User_ButtonUpload, "Upload");
            en_us.AddText(EnumText.enumForum_User_FieldCurrentAvatarText, "My Avatar");
            en_us.AddText(EnumText.enumForum_User_PageEditAvatarErrorInitLanguage, "Error initializing language:");
            en_us.AddText(EnumText.enumForum_User_TitleEditSignature, "Edit Signature");
            en_us.AddText(EnumText.enumForum_User_SubtitleEditSignature, "Signature will display at the bottom of every post or topic you have posted. It indicates your own identity. On this page, you can design your own signature.");
            en_us.AddText(EnumText.enumForum_User_PageEditSignatureErrorLoading, "Error loading Signature page: ");
            en_us.AddText(EnumText.enumForum_User_PageEditSignatureSucessSave, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_User_PageEditSignatureErrorSave, "Error saving the signature:");
            en_us.AddText(EnumText.enumForum_User_TitleEditPassword, "Change Password");
            en_us.AddText(EnumText.enumForum_User_SubtitleEditPassword, "Password is a secret word or phrase you use to access the forum. On this page, you can change your password.");
            en_us.AddText(EnumText.enumForum_User_FieldCurrentPassword, "Current Password:");
            en_us.AddText(EnumText.enumForum_User_FieldNewPassword, "New Password:");
            en_us.AddText(EnumText.enumForum_User_FieldRetypePassword, "Retype Password:");
            en_us.AddText(EnumText.enumForum_User_PageEditPasswordErrorLoading, "Error loading Change Password page:");
            en_us.AddText(EnumText.enumForum_User_PageEditPasswordSuccessSave, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_User_PageEditPasswordErrorSave, "Error saving password:");
            en_us.AddText(EnumText.enumForum_User_ErrorCurrentPasswordRequired, "Current password is required.");
            en_us.AddText(EnumText.enumForum_User_ErrorNewPasswordRequired, "New Password is required.");
            en_us.AddText(EnumText.enumForum_User_ErrorRetypePasswordRequired, "Retype Password is required.");
            en_us.AddText(EnumText.enumForum_User_ErrorRetypePsswordMatch, "Passwords don't match.");
            en_us.AddText(EnumText.enumForum_User_TitleUserProfile, "User Profile");
            en_us.AddText(EnumText.enumForum_User_BroswerTitleUserProfile, "User Profile - {0}");
            en_us.AddText(EnumText.enumForum_User_PageUserProfileErrorLoading, "Error loading User Profile page: ");
            en_us.AddText(EnumText.enumForum_User_TitleResetPassword, "Reset Password");
            en_us.AddText(EnumText.enumForum_User_MessageInvalidResetPasswordLink, "The link for resetting password is invalid.");
            en_us.AddText(EnumText.enumForum_User_BrowerTitleResetPassword, "Reset Password - {0}");
            en_us.AddText(EnumText.enumForum_User_ButtonResetPassword, "Reset Password");
            en_us.AddText(EnumText.enumForum_User_AlertResetPasswordSucceed, "Reset password succeeded.");
            en_us.AddText(EnumText.enumForum_User_PageResetPasswordErrorReset, "Error reseting password:");
            en_us.AddText(EnumText.enumForum_User_HelpEdit, "Edit");
            en_us.AddText(EnumText.enumForum_User_HelpInactive, "Deactivate");
            en_us.AddText(EnumText.enumForum_User_HelpActive, "Activate");
            en_us.AddText(EnumText.enumForum_User_InactiveConfirm, "Are you sure you want to deactivate this user?");
            en_us.AddText(EnumText.enumForum_User_ActiveConfirm, "Are you sure you want to activate this user?");
            en_us.AddText(EnumText.enumForum_User_HelpBan, "Ban");
            en_us.AddText(EnumText.enumForum_User_HelpLiftBan, "Lift Ban");
            en_us.AddText(EnumText.enumForum_User_LiftBanConfirm, "Are you sure you want to lift the ban?");
            en_us.AddText(EnumText.enumForum_User_HelpSendMessage, "Send Message");
            en_us.AddText(EnumText.enumForum_User_QueryEmailOrDisplayName, "Email/Display Name:");
            en_us.AddText(EnumText.enumForum_user_ButtonNewUser, "New User");
            en_us.AddText(EnumText.enumForum_User_ButtonSendMessage, "Send Message");
            en_us.AddText(EnumText.enumForum_User_ButtonBan, "Ban");
            en_us.AddText(EnumText.enumForum_User_ErrorSubjectRequired, "Subject is required.");
            en_us.AddText(EnumText.enumForum_User_ErrorExpireTimeInteger, "Expire Time must be an integer.");
            en_us.AddText(EnumText.enumForum_User_ErrorCheckUserBan, "Error checking user ban:");
            en_us.AddText(EnumText.enumForum_User_SuccessActiveUser, "The user has been set as Active successfully.");
            en_us.AddText(EnumText.enumForum_User_SuccessInactiveUser, "The user has been set as Inactive successfully.");
            en_us.AddText(EnumText.enumForum_User_SuccessLiftBan, "The ban has been lifted successfully.");
            en_us.AddText(EnumText.enumForum_User_ErrorSendMessage, "Error sending message:");
            en_us.AddText(EnumText.enumForum_User_SuccessSendMessage, "The message has been sent successfully.");
            en_us.AddText(EnumText.enumForum_User_ErrorBanUser, "Error banning user:");
            en_us.AddText(EnumText.enumForum_User_SuccessBanUser, "This user has been banned successfully.");
            en_us.AddText(EnumText.enumForum_User_ColumnIsAdministrator, "Is Administrator");
            en_us.AddText(EnumText.enumForum_User_ColumnOperation, "Operation");
            en_us.AddText(EnumText.enumForum_User_TitleSendMessage, "Send Message");
            //en_us.AddText(EnumText.enumForum_User_FieldRecipient, "Recipient:");
            //en_us.AddText(EnumText.enumForum_User_FieldSubject, "Subject:");
            //en_us.AddText(EnumText.enumForum_User_FieldMessage, "Message:");
            en_us.AddText(EnumText.enumForum_User_TitleBanUser, "Ban User");
            en_us.AddText(EnumText.enumForum_User_FieldBanUser, "Ban User:");
            en_us.AddText(EnumText.enumForum_User_FieldExpireTime, "Expire Time:");
            en_us.AddText(EnumText.enumForum_User_FieldNote, "Note:");
            en_us.AddText(EnumText.enumForum_User_TitleNewUser, "New User");
            en_us.AddText(EnumText.enumForum_User_SubtitleNewUser, "Here you can create a new user. When adding a new user, you can select one or more user groups which the user belongs to.");
            en_us.AddText(EnumText.enumForum_User_chkAdministrator, "Administrator");
            en_us.AddText(EnumText.enumForum_User_PageNewUserErrorLoad, "Error loading New User page:");
            en_us.AddText(EnumText.enumForum_Usse_lblAddUserGroup, "There is no user group. Go to <a href=\"../UserGroups/UserGroup.aspx?siteid={0}\">User Group Management</a> to create one.");
            en_us.AddText(EnumText.enumForum_User_PageNewUserErrorAdd, "Error adding a new user:");
            en_us.AddText(EnumText.enumForum_User_FieldUserGroup, "User Group:");
            en_us.AddText(EnumText.enumForum_User_ColumnUserGroup, "User Group");
            en_us.AddText(EnumText.enumForum_User_HelpDisplayname, "Display Name is composed of the following characters: letters A-Z, a-z, and digits o-9. Here please note a display name cannot begin with a digit and it must be visible to public.");
            en_us.AddText(EnumText.enumForum_User_FieldScore, "Scores:");
            en_us.AddText(EnumText.enumForum_User_FieldReputation, "Reputation:");
            en_us.AddText(EnumText.enumForum_User_FieldLastLoginIP, "Last Login IP:");
            en_us.AddText(EnumText.enumForum_User_UserProfileLinkUserPosts, "View This User's Posts");
            en_us.AddText(EnumText.enumForum_User_TitleEdit, "Edit User");
            en_us.AddText(EnumText.enumForum_User_SubtitleEdit, "Here you can edit the selected user's information.");
            en_us.AddText(EnumText.enumForum_User_ErrorScoreRange, "Scores must be an integer.");
            en_us.AddText(EnumText.enumForum_User_ErrorScoreRequired, "Scores is required.");
            en_us.AddText(EnumText.enumForum_User_ErrorReputationRange, "Reputation must be an integer.");
            en_us.AddText(EnumText.enumForum_User_ErrorReputationRequired, "Reputation is required.");
            en_us.AddText(EnumText.enumForum_User_PageEditUserErrorLoad, "Error loading Edit User page:");
            en_us.AddText(EnumText.enumForum_User_PageEditUserSuccessEdit, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_User_PageEditUserErrorEdit, "Error updating a user:");
            en_us.AddText(EnumText.enumForum_User_FieldAdditionalInformation, "Additional Information");
            en_us.AddText(EnumText.enumForum_User_SelectUserTitle, "Select Author");
            en_us.AddText(EnumText.enumForum_User_SelectUserSearchTitle, "Search");
            en_us.AddText(EnumText.enumForum_User_SelectUserUsersTitle, "Users");
            en_us.AddText(EnumText.enumForum_User_SelectUserLoadUsersError, "Error loading Users:");
            en_us.AddText(EnumText.enumForum_User_SelectUserButtonSubmit, "Ok");
            en_us.AddText(EnumText.enumForum_User_FieldUserType, "User Type:");
            en_us.AddText(EnumText.enumForum_User_UserTypeAll, "All");
            en_us.AddText(EnumText.enumForum_User_UserTypeAdmin, "Admin");
            en_us.AddText(EnumText.enumForum_User_UserTypeOperator, "Operator");
            en_us.AddText(EnumText.enumForum_User_UserTypeRegisterUser, "Registered User");
            en_us.AddText(EnumText.enumForum_User_ColumnUserType, "User Type");
            en_us.AddText(EnumText.enumForum_User_ColumnIfDelete, "If Deleted");
            en_us.AddText(EnumText.enumForum_User_ButtonSelect, "OK");
            en_us.AddText(EnumText.enumForum_User_UsersPostsTitle, "User's Posts - {0}");
            en_us.AddText(EnumText.enumForum_User_UsersPostsFieldPosts, "Posts:");
            en_us.AddText(EnumText.enumForum_User_UsersPostsLoadError, "Error loading User's Posts page:");

            #endregion

            #region Email Verfication 7

            en_us.AddText(EnumText.enumForum_EmailVerify_TitleEmailVerify, "Email Verification");
            en_us.AddText(EnumText.enumForum_EmailVerify_SubtitleEmailVerify, "Email Verification is designed to better manage users' registration. With \"Email Address Verification Required for Registration\" checked on User Registration Settings page, a verification email will be sent to a user's registered email automatically after his registration, and this user needs to activate his account by clicking the URL in this verification email. But if this registered user is unable to receive the verification email, as a forum administrator, you can click the Resend button to send the verification email to his registered email again, or directly copy the verification URL to this user so that he can click the verification URL to activate his account as soon as possible.");
            en_us.AddText(EnumText.enumForum_EmailVerify_PageEmailVerifyErrorLoad, "Error loading Email Verification page:");
            en_us.AddText(EnumText.enumForum_EmailVerify_PageEmailVerifyErrorSend, "Error sending verification email:");
            en_us.AddText(EnumText.enumForum_EmailVerify_ColumnResendEmail, "Resend Email");
            en_us.AddText(EnumText.enumForum_EmailVerify_ColumnEmailVerify, "View Email Verification URL");
            en_us.AddText(EnumText.enumForum_EmailVerify_HelpResendEmail, "Resend Email");
            en_us.AddText(EnumText.enumForum_EmailVerify_HelpEmailVerify, "View Email Verification URL");
            en_us.AddText(EnumText.enumForum_EmailVerify_FieldEmailVerifyUrl, "Email Verification URL");
            en_us.AddText(EnumText.enumForum_EmailVerify_HelpEmailVerifyUrl, "You can run the following URL in your web browser to manually verify the registration of a user.");
            en_us.AddText(EnumText.enumForum_EmailVerify_ButtonCopyUrl, "Copy URL");
            en_us.AddText(EnumText.enumForum_EmailVerify_SendEmailSuccessInfo, "Verification Email has been sent to '{0}' successfully.");
            #endregion

            #region Reputation Groups 6
            en_us.AddText(EnumText.enumForum_Reputation_ColumnName, "Name");
            en_us.AddText(EnumText.enumForum_Reputation_ColumnDescription, "Description");
            en_us.AddText(EnumText.enumForum_Reputation_ColumnReputationRange, "Reputation Range");
            en_us.AddText(EnumText.enumForum_Reputation_ColumnRank, "Rank");
            en_us.AddText(EnumText.enumForum_Reputation_ColumnPermissions, "Permissions");
            en_us.AddText(EnumText.enumForum_Reputation_ColumnOperation, "Operation");
            en_us.AddText(EnumText.enumForum_Reputation_HelpSetPermission, "Set Permission");
            en_us.AddText(EnumText.enumForum_Reputation_HelpEdit, "Edit");
            en_us.AddText(EnumText.enumForum_Reputation_HelpDelete, "Delete");
            en_us.AddText(EnumText.enumForum_Reputation_ConfirmDeleteReputation, "Are you sure you want to delete this reputation group?");
            en_us.AddText(EnumText.enumForum_Reputation_ButtonNewReputation, "New Reputation Group");
            en_us.AddText(EnumText.enumForum_Reputation_TitleReputationGroups, "Reputation Groups Management");
            en_us.AddText(EnumText.enumForum_Reputation_SubtitleReputationGroups, "Reputation Group provides a way of rating users based on their reputation value. Administrators can create multiple reputation groups and set a specific reputation range for each group. Users are assigned to will be assigned to a group when their reputation values fit in the reputation range specify. <br/> <br/>When Reputation Group Permission feature is enabled, administrators can set permissions for a reputation group. ");
            en_us.AddText(EnumText.enumForum_Reputation_PageReputationGroupsErrorLoad, "Error loading Reputation Groups Management page:");
            en_us.AddText(EnumText.enumForum_Reputation_PageReputationGroupsErrorDelete, "Error deleting a reputation group:");
            en_us.AddText(EnumText.enumForum_Reputation_TitleNewReputationGroup, "New Reputation Group");
            en_us.AddText(EnumText.enumForum_Reputation_SubtitleNewReputationGroup, "Enter the name and description, set the reputation range and ranks below to create a new reputation group.");
            en_us.AddText(EnumText.enumForum_Reputation_ErrorNameRequire, "Name is required");
            en_us.AddText(EnumText.enumForum_Reputation_ErrorRankRequire, "Rank is required");
            en_us.AddText(EnumText.enumForum_Reputation_ErrorRangeStartRequire, "Start Range is required.");
            en_us.AddText(EnumText.enumForum_Reputation_ErrorRangeEndRequire, "End Range is required.");
            en_us.AddText(EnumText.enumForum_Reputation_ErrorRankRange, "The rank value MUST be a integer between 1~20.");
            en_us.AddText(EnumText.enumForum_Reputation_ErrorRangeStart, "Start Range MUST be an integer.");
            en_us.AddText(EnumText.enumForum_Reputation_ErrorRangeEnd, "End Range MUST be an integer.");
            en_us.AddText(EnumText.enumForum_Reputation_ErrorNewReputationExist, "This reputation group has already existed.");
            en_us.AddText(EnumText.enumForum_Reputation_PageNewReputationErrorLoad, "Error loading New Reputation Group page:");
            en_us.AddText(EnumText.enumForum_Reputation_PageNewReputationErrorAdd, "Error adding a reputation group:");
            en_us.AddText(EnumText.enumForum_Reputation_FieldName, "Name:");
            en_us.AddText(EnumText.enumForum_Reputation_FieldDescription, "Description:");
            en_us.AddText(EnumText.enumForum_Reputation_FieldReputationRange, "Reputation Range:");
            en_us.AddText(EnumText.enumForum_Reputation_FieldRank, "Rank:");
            en_us.AddText(EnumText.enumForum_Reputation_TitleEditReputationGroup, "Edit Reputation Group");
            en_us.AddText(EnumText.enumForum_Reputation_SubtitleEditReputationGroup, "Here you can edit the selected reputation group.");
            en_us.AddText(EnumText.enumForum_Reputation_PageEditReputationGroupErrorLoad, "Error loading Edit Reputation Group page:");
            en_us.AddText(EnumText.enumForum_Reputation_PageEditReputationGroupErrorEdit, "Error editing a reputation group:");
            en_us.AddText(EnumText.enumForum_Reputation_TitleReputationGroupPermission, "Reputation Group Permission Settings");
            en_us.AddText(EnumText.enumForum_Reputation_SubtitleReputationGroupPermission, "Reputation Group Permission is a dynamic permission system to ease the permission management duty of administrators and moderators. The members of a Reputation Group are the users with a Reputation that is within the Reputation range requirement of the Reputation Group. On this page, you can set permissions for the users in this reputation group.");
            en_us.AddText(EnumText.enumForum_Reputation_PageReputationGroupPermissionErrorLoad, "Error loading Reputation Group Permission Settings page:");
            en_us.AddText(EnumText.enumForum_Reputation_PageReputationGroupPermissionErrorSave, "Error saving Reputation Group Settings:");
            en_us.AddText(EnumText.enumForum_Reputation_FieldCurrentReputationGroup, "Current Reputation Group:");


            #endregion

            #region Register 5
            en_us.AddText(EnumText.enumForum_Register_TitlePreRegister, "Register");
            en_us.AddText(EnumText.enumForum_Register_SubtitlePreRegister, "Rules and Policies");
            en_us.AddText(EnumText.enumForum_Register_PageTitlePreRegister, "Register - {0}");
            en_us.AddText(EnumText.enumForum_Register_ButtonNext, "Next");
            en_us.AddText(EnumText.enumForum_Register_PagePreRegisterError, "Error: ");
            en_us.AddText(EnumText.enumForum_Register_Content1, "Welcome to our forum.<br/><br/>");
            en_us.AddText(EnumText.enumForum_Register_Content2, "&nbsp;&nbsp;&nbsp;&nbsp;By accessing our forum (hereinafter 'we', 'us', 'our'), you agree to be legally bound by the following terms. If you do not agree to be legally bound by all of the following terms then please do not access and/or use our forum. We reserve the right to update this Rules and Policies at any time without noticing you. It is your responsibility to check the latest version of the Rules and Policies.<br/><br/>");
            en_us.AddText(EnumText.enumForum_Register_Content3, "&nbsp;&nbsp;&nbsp;&nbsp;");//Our forum is powered by Comm100. For further information about Comm100, please visit:<a href='http://www.comm100.com/' target='_blank'>'http://www.comm100.com/'</a><br/><br/>
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
            en_us.AddText(EnumText.enumForum_Register_HelpImageVerificationCode, "Image Verification Code");
            en_us.AddText(EnumText.enumForum_Register_ButtonEmailVerification, "Email Verification");
            en_us.AddText(EnumText.enumForum_Register_ButtonCompleteRegister, "Complete Registration");
            en_us.AddText(EnumText.enumForum_Register_ErrorMatchPasswords, "Passwords do not match.");
            en_us.AddText(EnumText.enumForum_Register_ErrorDisplayNameRequired, "Display name is required.");
            en_us.AddText(EnumText.enumForum_Register_ErrorMatchMails, "Emails do not match.");
            en_us.AddText(EnumText.enumForum_Register_ErrorMailFormat, "Email format is invalid.");
            en_us.AddText(EnumText.enumForum_Register_ErrorEmailNotUsed, "&nbsp;This email is available.");
            en_us.AddText(EnumText.enumForum_Register_ErrorEmailUsed, "&nbsp; This email has been registered. Please try another one.");
            en_us.AddText(EnumText.enumForum_Register_ErrorEmailRequired, "Email is required.");
            en_us.AddText(EnumText.enumForum_Register_ErrorDisplayFormat, "Display Name is composed of the following characters: letters A-Z,a-z,digits 0-9,and underline(_). Please note that a display name can not begin with a digit.");
            en_us.AddText(EnumText.enumForum_Register_ErrorPasswordRequired, "Password is required.");
            en_us.AddText(EnumText.enumForum_Register_ErrorRetypeEmailRequired, "Retype Email is required.");
            en_us.AddText(EnumText.enumForum_Register_ErrorRetypePasswordRequired, "Retype Password is required.");
            en_us.AddText(EnumText.enumForum_Register_ErrorVerificationCode, "Verification Code is incorrect.");
            en_us.AddText(EnumText.enumForum_Register_ErrorVerificationCodeRequired, "Verification Code is required.");
            en_us.AddText(EnumText.enumForum_Register_PageRegisterErrorLoad, "Error loading Register page: ");
            en_us.AddText(EnumText.enumForum_Register_PageRegisterErrorRegister, "Error registering:");
            en_us.AddText(EnumText.enumForum_Register_TitlePostRegister, "Register");
            en_us.AddText(EnumText.enumForum_Register_LinkBackToLastPage, "Back to Last Page");
            en_us.AddText(EnumText.enumForum_Register_LinkUserControlPanel, "Go to User Control Panel");
            en_us.AddText(EnumText.enumForum_Register_LinkHomePage, "Go to Home Page");
            en_us.AddText(EnumText.enumForum_Register_PagePostRegisterErrorLoad, "Error loading Post Register page:");
            en_us.AddText(EnumText.enumForum_Register_ContentThanks, "Thanks for your registration.");
            en_us.AddText(EnumText.enumForum_Register_ContentEmail, "Please go to your mailbox to check the confirmation email, and finish your registration.");
            en_us.AddText(EnumText.enumForum_Register_ContentEmailAndModerated, "Please go to your mailbox to check the confirmation email, and activate your registration.");
            en_us.AddText(EnumText.enumForum_Register_StateWait, "Waiting for Moderation");
            en_us.AddText(EnumText.enumForum_Register_StateSuccess, "Registration succeeded.");
            en_us.AddText(EnumText.enumForum_Register_LinkJump, "Please click the hyper links below if this page does not redirect in 5 seconds.");

            #endregion

            #region Forum Login 4
            en_us.AddText(EnumText.enumForum_login_TitleAdminLogin, "Admin Login");
            en_us.AddText(EnumText.enumForum_login_TitleModeratorLogin, "Moderator Login");
            en_us.AddText(EnumText.enumForum_login_TitleUser, "User Login");
            en_us.AddText(EnumText.enumForum_login_FieldEmail, "Email:");
            en_us.AddText(EnumText.enumForum_login_FieldPassword, "Password:");
            en_us.AddText(EnumText.enumForum_login_FieldVerificationCode, "Verification Code:");
            en_us.AddText(EnumText.enumForum_login_FieldRememberMe, "Remember me");
            en_us.AddText(EnumText.enumForum_login_FieldForgetPassword, "Forgot your password?");
            en_us.AddText(EnumText.enumForum_login_FieldNewToForum, "New to forum?");
            en_us.AddText(EnumText.enumForum_Login_LinkUserControlPanel, "Go to User Control Panel");
            en_us.AddText(EnumText.enumForum_login_LinkClickHere, "Click here!");
            en_us.AddText(EnumText.enumForum_Login_LinkRegisterHere, "Register here!");
            en_us.AddText(EnumText.enumForum_Login_ButtonLogin, "Login");
            en_us.AddText(EnumText.enumForum_Login_ErrorVerification, "Verification Code does't match.");
            en_us.AddText(EnumText.enumForum_Login_PageLoginErrorLogin, "Error loading Login page:");
            en_us.AddText(EnumText.enumForum_Login_PageLoginErrorLoginSite, "Error logging into the site: ");
            en_us.AddText(EnumText.enumForum_Login_SessionOut, "Session time out! Please login again.");
            en_us.AddText(EnumText.enumForum_Login_LinkHomePage, "Go to Home Page");
            en_us.AddText(EnumText.enumForum_Login_LinkBackPage, "Go to Last Page");
            en_us.AddText(EnumText.enumForum_Login_WaitJump, "Please click the hyper links below if this page does not redirect in 5 seconds.");
            en_us.AddText(EnumText.enumForum_Login_StateSuccess, "Log in Successfully!");
            en_us.AddText(EnumText.enumForum_Login_EmailFormat, "Email format is incorrect.");
            en_us.AddText(EnumText.enumForum_Login_ErrorVerificationCodeRequired, "Verification Code is required.");
            en_us.AddText(EnumText.enumForum_Login_ErrorEmailRequired, "Email is required.");
            en_us.AddText(EnumText.enumForum_Login_ErrorPasswordRequired, "Password is required.");
            en_us.AddText(EnumText.enumForum_Login_PageTitle, "{0} - {1}");
            en_us.AddText(EnumText.enumForum_UserBanned_PageTitle, "User Banned");
            en_us.AddText(EnumText.enumForum_IPBanned_PageTitle, "IP Banned");
            en_us.AddText(EnumText.enumForum_Login_TitleSendResetPasswordEmail, "Send Reset Password Email.");
            en_us.AddText(EnumText.enumForum_Login_MessageSendResetPasswordEmailSuccess, "The email for resetting password has been sent successfully.");
            en_us.AddText(EnumText.enumForum_Login_BrowerTitleSendResetPasswordEmail, "Reset Password - {0}");
            en_us.AddText(EnumText.enumForum_Login_TitleFindPassword, "Find Password");
            en_us.AddText(EnumText.enumForum_Login_NoteEnterEmail, "Please enter your registered email. An email will be sent to you for resetting your password.");
            en_us.AddText(EnumText.enumForum_Login_BrowerTitleFindPassword, "Find Password - {0}");
            en_us.AddText(EnumText.enumForum_Login_PageFindPasswordErrorLoading, "Error loading Find Password page:");
            en_us.AddText(EnumText.enumForum_Login_PageFindPasswordErrorUnregisteredEmail, "This email has not been registered with our forum.");
            en_us.AddText(EnumText.enumForum_Login_PageFindPasswordErrorEmailNotVerified, "This email has not been verified or your registration request has not been moderated by forum administrator.");
            en_us.AddText(EnumText.enumForum_Login_PageFindPasswordErrorFindingPassword, "Error finding password:");
            en_us.AddText(EnumText.enumForum_Login_ButtonSend, "Send");
            en_us.AddText(EnumText.enumForum_Login_PageFindPasswordErrorEmailFormat, "Email format is invalid.");
            en_us.AddText(EnumText.enumForum_Login_PageFindPasswordErrorEmailRequired, "Email is required");
            en_us.AddText(EnumText.enumForum_Login_TitleEmailVerification, "Email Verification");
            en_us.AddText(EnumText.enumForum_Login_LinkGotoUserPanel, "Go to  User Control Panel");
            en_us.AddText(EnumText.enumForum_Login_BrowerTitleEmailVerification, "Email Verification - {0}");
            en_us.AddText(EnumText.enumForum_Login_MessageEmailVerificationSucceed, "Congratulations! Email verification succeeded.");
            en_us.AddText(EnumText.enumForum_Login_MessageEmailVerificationWait, "<br/>Thanks for verifying your email. Please wait for your registration request to be moderated first.<br/>");
            en_us.AddText(EnumText.enumForum_Login_PageEmailVerificationErrorVerification, "Sorry! Email verification failed because your registration request has been refused by forum administrator. <br/>");
            en_us.AddText(EnumText.enumForum_Login_MessageEmailVerificationAgain, "This email has been verified before.");

            #endregion

            #region Message 3
            en_us.AddText(EnumText.enumForum_SendMessage_Title, "Send Message");
            en_us.AddText(EnumText.enumForum_SendMessage_SubTitle, "Here you can send a message to other users or groups.");
            en_us.AddText(EnumText.enumForum_SendMessage_FieldSubject, "Subject:");
            en_us.AddText(EnumText.enumForum_SendMessage_FieldMessage, "Message:");
            en_us.AddText(EnumText.enumForum_SendMessage_FieldReceiver, "Recipient:");
            en_us.AddText(EnumText.enumForum_SendMessage_ButtonSelectUsers, "Select Users");
            en_us.AddText(EnumText.enumForum_SendMessage_ButtonSelectGroups, "Select Groups");
            en_us.AddText(EnumText.enumForum_SendMessage_ButtonSend, "Send");
            en_us.AddText(EnumText.enumForum_SendMessage_RequiredSubject, "Subject is required.");
            en_us.AddText(EnumText.enumForum_SendMessage_RequiredReceiver, "Recipient is required.");
            en_us.AddText(EnumText.enumForum_SendMessage_LoadError, "Error loading Send Message page:");
            en_us.AddText(EnumText.enumForum_SendMessage_SendMessageError, "Error sending message:");
            en_us.AddText(EnumText.enumForum_SendMessage_SendMessageSuccessfully, "Message has been sent successfully.");
            en_us.AddText(EnumText.enumForum_SendMessage_ReachedMaxMessagesDaily, "You are not allowed to send any other messages today because the messages you've sent today have exceeded the maximum daily messages limit.");
            en_us.AddText(EnumText.enumForum_SendMessage_SelectGroupsTitle, "Groups");
            en_us.AddText(EnumText.enumForum_SendMessage_GroupItemsAdministrators, "Administrators");
            en_us.AddText(EnumText.enumForum_SendMessage_GroupItemsModerators, "Moderators");
            en_us.AddText(EnumText.enumForum_SendMessage_GroupItemsUserGroups, "User Groups");
            en_us.AddText(EnumText.enumForum_SendMessage_GroupItemsReputationGroups, "Reputation Groups");
            en_us.AddText(EnumText.enumForum_Message_ColumnStatus, "Status");
            en_us.AddText(EnumText.enumForum_Message_ColumnReceiveTime, "Receive Time");
            en_us.AddText(EnumText.enumForum_Message_ColumnSender, "Sender");
            en_us.AddText(EnumText.enumForum_Message_ColumnSubject, "Subject");
            en_us.AddText(EnumText.enumForum_Message_ColumnOperation, "Operation");
            en_us.AddText(EnumText.enumForum_Message_ColumnSendTime, "Send Time");
            en_us.AddText(EnumText.enumForum_Message_ColumnReceiver, "Recipient");
            en_us.AddText(EnumText.enumForum_Message_HelpReply, "Reply");
            en_us.AddText(EnumText.enumForum_Message_HelpDelete, "Delete");
            en_us.AddText(EnumText.enumForum_InBox_Title, "InBox");
            en_us.AddText(EnumText.enumForum_InBox_SubTitle, "InBox stores all the messages you have received from other users. On this page, you can give a quick reply to those messages as well as delete them according to your needs.");
            en_us.AddText(EnumText.enumForum_InBox_DeleteConfirm, "Are you sure you want to delete this message?");
            en_us.AddText(EnumText.enumForum_InBox_ErrorLoad, "Error loading InBox page:");
            en_us.AddText(EnumText.enumForum_InBox_ErrorDelete, "Error deleting message:");
            en_us.AddText(EnumText.enumForum_InBox_ErrorLoadMessage, "Error loading message:");
            en_us.AddText(EnumText.enumForum_InBox_LengendReadMessage, "Read Message");
            en_us.AddText(EnumText.enumForum_InBox_LengendUnReadMessage, "Unread Message");
            en_us.AddText(EnumText.enumForum_OutBox_Title, "OutBox");
            en_us.AddText(EnumText.enumForum_OutBox_SubTitle, "OutBox stores all the messages you have sent to other users. On this page, you can view all the messages as well as delete them according to your needs.");
            en_us.AddText(EnumText.enumForum_OutBox_DeleteConfirm, "Are you sure you want to delete this message?");
            en_us.AddText(EnumText.enumForum_OutBox_ErrorLoad, "Error loading OutBox page:");
            en_us.AddText(EnumText.enumForum_OutBox_ErrorDelete, "Error deleting message:");
            en_us.AddText(EnumText.enumForum_OutBox_ErrorLoadMessage, "Error loading message:");
            #endregion

            #region Permission 2
            en_us.AddText(EnumText.enumForum_Permission_AllowViewForum, "Allow Visit Forum:");
            en_us.AddText(EnumText.enumForum_Permission_AllowViewTopicOrPost, "Allow View Topic/Post:");
            en_us.AddText(EnumText.enumForum_Permission_AllowPostTopicOrPost, "Allow Post Topic/Post:");
            en_us.AddText(EnumText.enumForum_Permission_MinInterValTimeForPosting, "Min Post Interval Time:");
            en_us.AddText(EnumText.enumForum_Permission_MaxLengthOfTopicOrPost, "Max Length of Topic/Post:");
            en_us.AddText(EnumText.enumForum_Permission_AllowHTML, "Allow HTML:");
            en_us.AddText(EnumText.enumForum_Permission_AllowLink, "Allow Insert Link:");
            en_us.AddText(EnumText.enumForum_Permission_AllowInsertImage, "Allow Insert Image:");
            en_us.AddText(EnumText.enumForum_Permission_PostModerationNotNeeded, "Post Moderation Required:");
            en_us.AddText(EnumText.enumForum_Permission_ForumToolTipAllowViewForum, "With this option checked, a user in this group can view the topic list on your forum.");
            en_us.AddText(EnumText.enumForum_Permission_ForumToolTipAllowViewTopicOrPost, "With this option checked, a user in this group can view the topics and posts on your forum.");
            en_us.AddText(EnumText.enumForum_Permission_ForumToolTipAllowPostTopicOrPost, "With this option checked, a user in this group can post topics and posts on your forum.");
            en_us.AddText(EnumText.enumForum_Permission_ForumToolTipMinInterValTimeForPosting, "Here you can set a minimum interval time for posting for a user in this group. After your setting, a user cannot post again within the time range you've set.");
            en_us.AddText(EnumText.enumForum_Permission_ForumToolTipMaxLengthOfTopicOrPost, "Here you can set a maximum length of  the topic or post for a user in this group. After your setting, the total length of a user's topic or post cannot exceed the length you've set.");
            en_us.AddText(EnumText.enumForum_Permission_ForumToolTipAllowHTML, "With this option checked, a user in this group can post a topic or post by using HTML editor.");
            en_us.AddText(EnumText.enumForum_Permission_ForumToolTipAllowLink, "With this option checked, a user in this group can insert links into his or her topic or post when posting.");
            en_us.AddText(EnumText.enumForum_Permission_ForumToolTipAllowInsertImage, "With this option checked, a user in this group can insert images into his or her topic or post when posting.");
            en_us.AddText(EnumText.enumForum_Permission_ForumToolTipPostModerationNotNeeded, "With this option checked, the topic or post of a user in this group needs moderation before it is published.");
            en_us.AddText(EnumText.enumForum_Permission_ForumDeleteUserGroupFromForumErrorMessage, "Delete User Group From Forum Error:");

            en_us.AddText(EnumText.enumForum_Permission_AllowCustomizeAvatar, "Allow Customize Avatar:");
            en_us.AddText(EnumText.enumForum_Permission_MaxSignature, "Max length of Signature:");
            en_us.AddText(EnumText.enumForum_Permission_AllowLinkSignature, "Allow Insert Link into Signature:");
            en_us.AddText(EnumText.enumForum_Permission_AllowImageSignature, "Allow Insert Image into Signature:");
            en_us.AddText(EnumText.enumForum_Permission_Attachment, "Allow Attachment:");
            en_us.AddText(EnumText.enumForum_Permission_MaxAttachmentsOnePost, "Max Attachments in One Post:");
            en_us.AddText(EnumText.enumForum_Permission_MaxSizeOfAttachment, "Max Size of each Attachment:");
            en_us.AddText(EnumText.enumForum_Permission_MaxSizeOfAllAttachments, "Max Size of all the Attachments:");
            en_us.AddText(EnumText.enumForum_Permission_MaxMessage, "Max Messages in One Day:");
            en_us.AddText(EnumText.enumForum_Permission_Search, "Allow Search:");
            en_us.AddText(EnumText.enumForum_Permission_IntervalSearch, "Min Search Interval Time:");
            en_us.AddText(EnumText.enumForum_Permission_ErrorPleaseInputPositiveInteger, "The input Must be an integer. Please retype.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipAllowViewForum, "With this option checked, a user in this group can view the topic list on your forum.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipAllowViewTopicOrPost, "With this option checked, a user in this group can view the topics and posts on your forum.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipAllowPostTopicOrPost, "With this option checked, a user in this group can post topics and posts on your forum.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipMinInterValTimeForPosting, "Here you can set a minimum interval time for posting for a user in this group. After your setting, a user cannot post again within the time range you've set.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipMaxLengthOfTopicOrPost, "Here you can set a maximum length of  the topic or post for a user in this group. After your setting, the total length of a user's topic or post cannot exceed the length you've set.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipAllowHTML, "With this option checked, a user in this group can post a topic or post by using HTML editor.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipAllowLink, "With this option checked, a user in this group can insert links into his or her topic or post when posting.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipAllowInsertImage, "With this option checked, a user in this group can insert images into his or her topic or post when posting.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipPostModerationNotNeeded, "With this option checked, the topic or post of a user in this group needs moderation before it is published.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipAllowCustomizeAvatar, "With this option checked, a user in this group can customize his or her own avatar by either uploading an avatar from local file or choosing one from the default ones.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipMaxSignature, "Here you can set a maximum length of the signature for a user in this group. After your setting, the total length of a user's signature cannot exceed the length you've set.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipAllowInsertSignatureLink, "With this option checked, a user can insert links into his or her signature.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipAllowInsertSignatureImage, "With this option checked, a user can insert images into his or her signature.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipAttachment, "With this option checked, a user in this group can upload attachments into his or her topic or post when posting.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipMaxAttachmentsOnePost, "Here you can set the maximum number of attachments which can be attached in one post for a user in this group. After your setting, the total number of a user's attachments in one post cannot exceed the number you've set.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipMaxSizeOfAttachment, "Here you can set a maximum size of each attachment for a user in this group. After your setting, the total size of a user's any attachment cannot exceed the size you've set.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipMaxSizeOfAllAttachments, "Here you can set a maximum size of all the attachments a user in this group can upload. After your setting, the total size of all the attachments a user can upload cannot exceed the size you've set.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipMaxMessage, "Here you can set a maximum number of messages a user in this group can send in one day. After your setting, the total number of the messages sent by a user in one day cannot exceed the number you've set.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipSearch, "With this option checked, a user in this group can use the search function to find specific topics or posts on your forum.");
            en_us.AddText(EnumText.enumForum_Permission_GroupToolTipIntervalSearch, "Here you can set a minimum search interval time for a user in this group. After your setting, a user cannot search again within the time range you've set.");
            en_us.AddText(EnumText.enumForum_Permission_PermissionSaveSucceeded, "Save succeeded.");
            en_us.AddText(EnumText.enumForum_Permission_ForumPermissionSettingsTitle, "Forum Permission Settings");
            en_us.AddText(EnumText.enumForum_Permission_ForumPermissionSettingsSubTitle, "Forum Permission defines a user's Can-do and Can't-do on this forum.");
            en_us.AddText(EnumText.enumForum_Permission_ForumPermissionSettingsLoadError, "Error loading Forum Permission Settings page:");
            en_us.AddText(EnumText.enumForum_Permission_ForumPermissionSettingsErrorAddReputationGroupToForum, "Error adding Reputation Group to current forum:");
            en_us.AddText(EnumText.enumForum_Permission_ForumPermissionSettingsErrorAddUserGroupToForum, "Error adding User Group to current forum:");
            en_us.AddText(EnumText.enumForum_Permission_ForumPermissionSettingsErrorRemoveReputationGroupFromForum, "Error removing Reputation from current forum:");
            en_us.AddText(EnumText.enumForum_Permission_ForumPermissionSettingsErrorRemoveUserGroupFromForum, "Error removing User Group from current forum:");
            en_us.AddText(EnumText.enumForum_Permission_ForumPermissionSettingsErrorSave, "Error saving permissions:");
            en_us.AddText(EnumText.enumForum_Permission_ForumPermissionSettingsErrorBindPermission, "Error binding permissions:");


            #endregion

            #region User Permission
            en_us.AddText(EnumText.enumForum_UserPermission_ErrorLoad, "Loading UserPermissions page error:");
            en_us.AddText(EnumText.enumForum_Permission_ErrorSave, "Save user permission error:");
            en_us.AddText(EnumText.enumForum_Permission_SuccessfullySaved, "Save Succeeded!");
            en_us.AddText(EnumText.enumForum_Permission_Title, "User Permissions");
            en_us.AddText(EnumText.enumForum_Permission_SubTitle, "Here you can set the permissions for a certain user who does not belong to any user group or reputation group.");
            en_us.AddText(EnumText.enumForum_Permission_ButtonSave, "Save");
            en_us.AddText(EnumText.enumForum_Permission_ButtonCancel, "Return");
            en_us.AddText(EnumText.enumForum_Permission_OnlyDigitalErrorMessage, "The input Must be an integer. Please retype.");
            en_us.AddText(EnumText.enumForum_Permission_InitializingLanguageError, "Initializing Language Error:");

            #endregion
            /*en_us.AddText(EnumText.enumForum_Login_Name, "User Id");
            en_us.AddText(EnumText.enumForum_Login_Password, "PassWord");
            en_us.AddText(EnumText.enumForum_Login_Ok, "Ok");
            en_us.AddText(EnumText.enumForum_Login_Title, "Login");*/
            #endregion Forum

            #region Function
            en_us.AddFunctionText(EnumFunctionLanguageCode.DateTimeHelper_DateTransferToString_Hours, "{0} Hours ago");
            en_us.AddFunctionText(EnumFunctionLanguageCode.DateTimeHelper_DateTransferToString_Hour, "1 Hour ago");
            en_us.AddFunctionText(EnumFunctionLanguageCode.DateTimeHelper_DateTransferToString_Minute, "1 Minute ago");
            en_us.AddFunctionText(EnumFunctionLanguageCode.DateTimeHelper_DateTransferToString_Minutes, "{0} Minutes ago");
            en_us.AddFunctionText(EnumFunctionLanguageCode.ASPNetPager_DateTransferToString_PageTop, "Page {0} of {1}[{2}{3}]");
            #endregion
            #endregion en-us

        }


        #region Business

        public static Com.Comm100.Language.EnumLanguage GetCurrentLanguageEnum()
        {
            return Com.Comm100.Language.LanguageHelper.GetCurrentLanguageEnum();
        }

        public static string GetLanguageName(Com.Comm100.Language.EnumLanguage language)
        {
            return Com.Comm100.Language.LanguageHelper.GetLanguageName(language);
        }

        public static string GetDisplayNameByLanguage(string firstName, string lastName, Com.Comm100.Language.EnumLanguage language)
        {
            return Com.Comm100.Language.LanguageHelper.GetDisplayNameByLanguage(firstName, lastName, language);
        }

        private string GetText(Com.Comm100.Language.EnumLanguage enumLanguage, EnumText enumText)
        {
            if (LanguageHelper.LanguageList[(int)enumLanguage] == null)
            {
                enumLanguage = Com.Comm100.Language.EnumLanguage.enumEnglish;
            }

            return LanguageHelper.LanguageList[(int)enumLanguage].GetText(enumText);
        }

        public string GetExceptionText(Com.Comm100.Language.EnumLanguage enumLanguage, EnumErrorCode enumErrorCode)
        {
            if (LanguageHelper.LanguageList[(int)enumLanguage] == null)
            {
                enumLanguage = Com.Comm100.Language.EnumLanguage.enumEnglish;
            }

            return LanguageHelper.LanguageList[(int)enumLanguage].GetExceptionText(enumErrorCode);
        }

        #endregion Business
    }
}
