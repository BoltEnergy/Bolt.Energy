﻿#if OPENSOURCE
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
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Language;
namespace Com.Comm100.Forum.UI.AdminPanel.Settings
{
    public partial class ScoreStrategy : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        string ErrorLoad;
        string ErrorSave;
        string SuccessfullySaved;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Settings_PageScoreStrategyErrorLoad];
                ErrorSave = Proxy[EnumText.enumForum_Settings_PageScoreStrategyErrorSave];
                SuccessfullySaved = Proxy[EnumText.enumForum_Settings_PageScoreStrategySuccessSave];
                lblTitle.Text = Proxy[EnumText.enumForum_Settings_TitleScoreStrategy];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Settings_SubtitleScoreStrategy];
                btnSave1.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnSave2.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnCancel1.Text = Proxy[EnumText.enumForum_Forums_ButtonReturn];
                btnCancel2.Text = Proxy[EnumText.enumForum_Forums_ButtonReturn];

                #region Validator ErrorMessage
                string OnlyDigitalErrorMessage = "<br />"+Proxy[EnumText.enumForum_Settings_ErrorOnlyDigital];
                this.rvI_AddedIntoFacorites.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_Ban.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_Featured.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_Login.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_ModeratorAdded.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_ModeratorRemoved.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_NewPost.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_NewTopic.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_PollVoted.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_PostDeleted.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_PostMarkedAsAnswer.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_PostRestored.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_PostVerifiedAsSpam.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_Register.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_RemovedFromFavorites.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_ReportAbuse.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_Search.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_Sticky.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_TopicDeleted.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_TopicReplied.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_TopicRestored.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_TopicVerifiedAsSpam.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_TopicViewed.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_Unban.ErrorMessage = OnlyDigitalErrorMessage;
                this.rvI_VoteForaPoll.ErrorMessage = OnlyDigitalErrorMessage;
                #endregion
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumSiteSettings);
                    Master.Page.Title = Proxy[EnumText.enumForum_Settings_TitleScoreStrategy];
                    Page.Form.DefaultButton = this.btnSave1.UniqueID;
                    if (!IsPostBack)
                    {
                        GetScoreStrategy();
                    }
                }
                catch (Exception exp)
                {
                    this.lblError.Text = ErrorLoad + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }

        protected void GetScoreStrategy()
        {
            ScoreStrategySettingWithPermissionCheck scoreStrategy = SettingsProcess.GetScoreStrategy(this.SiteId, this.UserOrOperatorId);
            #region General
            txtI_Register.Text = scoreStrategy.Registration.ToString();
            txtI_Login.Text = scoreStrategy.FirstLoginEveryDay.ToString();
            txtI_ModeratorAdded.Text = scoreStrategy.AddModerator.ToString();
            txtI_ModeratorRemoved.Text = scoreStrategy.RemoveModerator.ToString();
            txtI_Ban.Text = scoreStrategy.Ban.ToString();
            txtI_Unban.Text = scoreStrategy.Unban.ToString();
            #endregion
            #region Topic
            txtI_NewTopic.Text = scoreStrategy.NewTopic.ToString();
            txtI_Featured.Text = scoreStrategy.TopicMarkedAsFeature.ToString();
            txtI_Sticky.Text = scoreStrategy.TopicMarkedAsSticky.ToString();
            txtI_TopicDeleted.Text = scoreStrategy.TopicDeleted.ToString();
            txtI_TopicRestored.Text = scoreStrategy.TopicRestored.ToString();
            txtI_AddedIntoFavorites.Text = scoreStrategy.TopicAddedIntoFavorites.ToString();
            txtI_RemovedFromFavorites.Text = scoreStrategy.TopicRemovedFromFavorites.ToString();
            txtI_TopicViewed.Text = scoreStrategy.TopicViewed.ToString();
            txtI_TopicReplied.Text = scoreStrategy.TopicReplied.ToString();
            txtI_TopicVerifiedAsSpam.Text = scoreStrategy.TopicVerifiedAsSpam.ToString();
            txtI_VoteForaPoll.Text = scoreStrategy.Vote.ToString();
            txtI_PollVoted.Text = scoreStrategy.PollVoted.ToString();
            #endregion
            #region Post
            txtI_NewPost.Text = scoreStrategy.NewPost.ToString();
            txtI_PostDeleted.Text = scoreStrategy.PostDeleted.ToString();
            txtI_PostRestored.Text = scoreStrategy.PostRestored.ToString();
            txtI_PostVerifiedAsSpam.Text = scoreStrategy.PostVerifiedAsSpam.ToString();
            txtI_PostMarkedAsAnswer.Text = scoreStrategy.PostMarkedAsAnswer.ToString();
            #endregion
            #region Others
            txtI_ReportAbuse.Text = scoreStrategy.ReportAbuse.ToString();
            txtI_Search.Text = scoreStrategy.Search.ToString();
            #endregion
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region General
                int registration = Convert.ToInt32(txtI_Register.Text);
                int firstLoginEveryDay = Convert.ToInt32(txtI_Login.Text);
                int addModerator = Convert.ToInt32(txtI_ModeratorAdded.Text);
                int removeModerator = Convert.ToInt32(txtI_ModeratorRemoved.Text);
                int ban = Convert.ToInt32(txtI_Ban.Text);
                int unban = Convert.ToInt32(txtI_Unban.Text);
                #endregion
                #region Topic
                int newTopic = Convert.ToInt32(txtI_NewTopic.Text);
                int topicMarkedAsFeature = Convert.ToInt32(txtI_Featured.Text);
                int topicMarkedAdSticky = Convert.ToInt32(txtI_Sticky.Text);
                int topicDeleted = Convert.ToInt32(txtI_TopicDeleted.Text);
                int topicRestored = Convert.ToInt32(txtI_TopicRestored.Text);
                int topicAddIntoFavorites = Convert.ToInt32(txtI_AddedIntoFavorites.Text);
                int topicRemoveFromFavorites = Convert.ToInt32(txtI_RemovedFromFavorites.Text);
                int topicViewed = Convert.ToInt32(txtI_TopicViewed.Text);
                int topicReplied = Convert.ToInt32(txtI_TopicReplied.Text);
                int topicVerifiedAsSpam = Convert.ToInt32(txtI_TopicVerifiedAsSpam.Text);
                int vote = Convert.ToInt32(txtI_VoteForaPoll.Text);
                int pollVoted = Convert.ToInt32(txtI_PollVoted.Text);
                #endregion
                #region Post
                int newPost = Convert.ToInt32(txtI_NewPost.Text);
                int postDeleted = Convert.ToInt32(txtI_PostDeleted.Text);
                int postRestored = Convert.ToInt32(txtI_PostRestored.Text);
                int postVerifiedAsSpam = Convert.ToInt32(txtI_PostVerifiedAsSpam.Text);
                int postMarkedAsAnswer = Convert.ToInt32(txtI_PostMarkedAsAnswer.Text);
                #endregion
                #region Others
                int reportAbuse = Convert.ToInt32(txtI_ReportAbuse.Text);
                int search = Convert.ToInt32(txtI_Search.Text);
                #endregion
                SettingsProcess.UpdateScoreStrategy(SiteId, CurrentUserOrOperator.UserOrOperatorId, registration, firstLoginEveryDay, addModerator,
                    removeModerator, ban, unban, newTopic, topicMarkedAsFeature, topicMarkedAdSticky, topicDeleted, topicRestored, topicAddIntoFavorites,
                    topicRemoveFromFavorites, topicViewed, topicReplied, topicVerifiedAsSpam, vote, pollVoted, newPost, postDeleted, postRestored,
                    postVerifiedAsSpam, postMarkedAsAnswer, reportAbuse, search);
                //lblSuccess.Text = SuccessfullySaved;
                Response.Redirect(string.Format("Settings.aspx?siteId={0}", this.SiteId), false);
            }
            catch (Exception exp)
            {
                lblError.Text = ErrorSave+ exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("Settings.aspx?siteId={0}", this.SiteId), false);
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_Public_RedirectError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }
        }
    }
}
