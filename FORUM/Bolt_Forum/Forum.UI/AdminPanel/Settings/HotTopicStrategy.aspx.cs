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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.Settings
{
    public partial class HotTopicStrategy : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        string ErrorLoad;
        string ErrorSave;
        string SuccessfullySaved;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_HotTopicStrategy_PageHotTopicStrategyErrorLoad];
                ErrorSave = Proxy[EnumText.enumForum_HotTopicStrategy_PageHotTopicStrategyErrorSave];
                SuccessfullySaved = Proxy[EnumText.enumForum_HotTopicStrategy_PageHotTopicStrategySuccessSave];
                lblTitle.Text = Proxy[EnumText.enumForum_HotTopicStrategy_TitleHotTopicStrategy];
                lblSubTitle.Text = Proxy[EnumText.enumForum_HotTopicStrategy_SubtitleHotTopicStrategy];
                btnSave1.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnSave2.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnReturn1.Text = Proxy[EnumText.enumForum_Forums_ButtonReturn];
                btnReturn2.Text = Proxy[EnumText.enumForum_Forums_ButtonReturn];
                this.rvViews.ErrorMessage = Proxy[EnumText.enumForum_HotTopicStrategy_ErrorViewInteger];
                this.rvPosts.ErrorMessage = Proxy[EnumText.enumForum_HotTopicStrategy_ErrorPostInteger];
                requiredFieldValidatorPosts.ErrorMessage = Proxy[EnumText.enumForum_HotTopicStrategy_ErrorPostsIsRequired];
                requiredFieldValidatorViews.ErrorMessage = Proxy[EnumText.enumForum_HotTopicStrategy_ErrorViewsIsRequired];
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumSiteSettings);
                    Master.Page.Title = Proxy[EnumText.enumForum_HotTopicStrategy_TitleHotTopicStrategy];
                    Page.Form.DefaultButton = this.btnSave1.UniqueID;
                    if (!IsPostBack)
                    { 
                        PageInit();
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

        protected void PageInit()
        {
            
            this.ddlLogical.Items.Add("And");
            this.ddlLogical.Items.Add("Or");
            GetHotTopicStrategy();
        }

        protected void GetHotTopicStrategy()
        {
            HotTopicStrategySettingWithPermissionCheck hotTopicStrategy = SettingsProcess.GetHotTopicStrategy(this.SiteId, this.UserOrOperatorId);
            this.txtViews.Text = Convert.ToString(hotTopicStrategy.ParameterGreaterThanOrEqualViews);
            this.txtPosts.Text = Convert.ToString(hotTopicStrategy.ParameterGreaterThanOrEqualPosts);
            this.ddlLogical.SelectedIndex = Convert.ToInt32(hotTopicStrategy.LogicalBetweenViewsAndrPosts);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int parameterGreaterThanOrEqualViews =Convert.ToInt32( this.txtViews.Text);
                int parameterGreaterThanOrEqualPosts = Convert.ToInt32(this.txtPosts.Text);
                EnumLogical logical = this.ddlLogical.SelectedIndex == 0 ? EnumLogical.AND : EnumLogical.OR;
                SettingsProcess.UpdateHotTopicStrategy(this.SiteId, this.UserOrOperatorId,parameterGreaterThanOrEqualViews,parameterGreaterThanOrEqualPosts,logical);
                //this.lblSuccess.Text = SuccessfullySaved;
                Response.Redirect(string.Format("Settings.aspx?siteId={0}", this.SiteId), false);
            }
            catch (Exception exp)
            {
                lblError.Text =ErrorSave+ exp.Message;
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
