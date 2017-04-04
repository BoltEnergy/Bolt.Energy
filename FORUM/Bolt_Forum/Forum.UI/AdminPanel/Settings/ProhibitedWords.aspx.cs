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
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.Settings
{
    public partial class ProhibitedWords : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        string ErrorLoad;
        string ErrorSave;
        string SuccessfullySaved;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_ProhibitedWords_PageProhibitedWordsErrorLoad];
                ErrorSave = Proxy[EnumText.enumForum_ProhibitedWords_PageProhibitedWordsErrorSave];
                SuccessfullySaved = Proxy[EnumText.enumForum_ProhibitedWords_PageProhibitedWordsSuccessSave];
                lblTitle.Text = Proxy[EnumText.enumForum_ProhibitedWords_TitleProhibitedWords];
                lblSubTitle.Text = Proxy[EnumText.enumForum_ProhibitedWords_SubtitleProhibitedWords];
                btnSave1.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnSave2.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnReturn1.Text = Proxy[EnumText.enumForum_Forums_ButtonReturn];
                btnReturn2.Text = Proxy[EnumText.enumForum_Forums_ButtonReturn];
                
            }
            catch (Exception exp)
            {
                this.lblError.Visible = true;
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
                    Master.Page.Title = Proxy[EnumText.enumForum_ProhibitedWords_TitleProhibitedWords];
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
            GetProhibitedWords();
        }

        protected void GetProhibitedWords()
        {
            ProhibitedWordsSettingWithPermissionCheck prohibitedWords = SettingsProcess.GetProhibitedWords(this.SiteId, this.UserOrOperatorId);
            this.chbEnable.Checked=prohibitedWords.IfEnabledProhibitedWords;
            this.txtReplaceWords.Text=prohibitedWords.CharacterToReplaceProhibitedWord;
            //string[] prohibitedWordsArray=prohibitedWords.ProhibitedWords;
            //string prohibitedWordsString=string.Empty;
            //for(int i=0;i<prohibitedWordsArray.Length;i++)
            //{
            //    prohibitedWordsString=prohibitedWordsString+prohibitedWordsArray[i]+",";
            //}
            this.txtProhibitedWords.Text = prohibitedWords.OriginalProhibitedWords; //prohibitedWordsString.TrimEnd(',');
            CheckEnable();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool ifEnabledProhibitedWords = this.chbEnable.Checked;
                string characterToReplaceProhibitedWord = this.txtReplaceWords.Text;
                string prohibitedWords = this.txtProhibitedWords.Text;
                SettingsProcess.UpdateProhibitedWords(this.SiteId, this.UserOrOperatorId,ifEnabledProhibitedWords,characterToReplaceProhibitedWord,prohibitedWords);
                //lblSuccess.Text = SuccessfullySaved;
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

        protected void chbEnable_CheckedChanged(object sender, EventArgs e)
        {
            CheckEnable();
            
        }
        private void CheckEnable()
        {
            if (this.chbEnable.Checked)
            {
                this.txtReplaceWords.Enabled = true;
                this.txtProhibitedWords.Enabled = true;
            }
            else
            {
                this.txtProhibitedWords.Enabled = false;
                this.txtReplaceWords.Enabled = false;
            }
        }
    }
}
