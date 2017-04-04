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
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.UI.AdminPanel;
using Com.Comm100.Framework.Enum.Forum;
using System.Web.Configuration;
using Com.Comm100.Forum.Language;

namespace Forum.UI.AdminPanel.Settings
{
    public partial class CodePlan : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        private string ErrorGetSiteSetting;
        private string ErrorLoad;
        protected override void InitLanguage()
        {
            try
            {
                Master.Page.Title = Proxy[EnumText.enumForum_Settings_TitleCodePlan];
                lblTitle.Text = Proxy[EnumText.enumForum_Settings_TitleCodePlan];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Settings_SubtitleCodePlan];
                ErrorGetSiteSetting = Proxy[EnumText.enumForum_Settings_PageCodePlanErrorGetSiteSetting];
                ErrorLoad = Proxy[EnumText.enumForum_Settings_PageCodePlanErrorLoad]; 
            }
            catch (Exception ex)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + ex.Message;

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumCodePlan);                        
                        getSiteSetting();
                    }
                }
                catch (Exception exp)
                {
                    this.lblError.Text = Proxy[EnumText.enumForum_Settings_PageCodePlanErrorLoad] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }

        protected void getSiteSetting()
        {
            try
            {
                SiteSetting tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, this.UserOrOperatorId);
                txtWebSiteUrl.Text = "<a href=\"" + this.UrlWithAuthorityAndApplicationPath + "Default.aspx?siteid=" + this.SiteId + "\">" + tmpSiteSetting.SiteName + "</a>";
                
            }
            catch (Exception exp)
            {
                lblError.Text = Proxy[EnumText.enumForum_Settings_PageCodePlanErrorGetSiteSetting] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

    }
}
