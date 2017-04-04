#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2009                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using System.Web.UI.WebControls;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.Settings
{
    public partial class RulesPoliciesSettings :Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        Master.Page.Title = Proxy[EnumText.enumForum_Settings_TitleRegistrationRulesSettings];
                        ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumRulesAndPoliciesSettings);
                        SiteSetting tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, this.UserOrOperatorId, this.IfOperator);
                        this.txtRulesContent.Text = tmpSiteSetting.ForumUserAgreement;
                      
                        //getRegistrationRulesSetting();
                        btnSaveRegistrationRulesSetting1.Text = Proxy[EnumText.enumForum_Settings_ButtonSave];
                        btnSaveRegistrationRulesSetting2.Text = Proxy[EnumText.enumForum_Settings_ButtonSave];
                    }
                }
                catch (Exception exp)
                {
                    this.lblError.Text = Proxy [EnumText.enumForum_Settings_PageRegistrationRuleSettingsErrorLoad ] + exp.Message;
                }
            }
        }

        protected void btnSaveRegistrationRulesSetting1_Click(object sender, EventArgs e)
        {
            try
            {
                SettingsProcess.SaveRules(this.SiteId,this.UserOrOperatorId,this.txtRulesContent.Text);
                this.lblSuccess.Text = Proxy [EnumText .enumForum_Settings_PageRegistrationRuleSettingsSuccessfullySaved];
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy [EnumText.enumForum_Settings_PageRegistrationRuleSettingsErrorSave] + exp.Message;
            }
        }
    }
}
