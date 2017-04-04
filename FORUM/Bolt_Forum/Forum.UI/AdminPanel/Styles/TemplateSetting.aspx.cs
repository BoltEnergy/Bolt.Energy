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
using System.Web.UI.HtmlControls;
using System.Drawing.Imaging;
using System.IO;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.UI.AdminPanel;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Language;

namespace Forum.UI.AdminPanel.Styles
{
    public partial class StyleSettings : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Master.Page.Title = Proxy[EnumText.enumForum_Styles_TitleTemplateSetting];
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumSiteSettings);
                this.lblError.Text = null;
                if (!IsPostBack)
                {
                    RepeaterBind();
                    this.setCurrentTemplate();
                }
            }
            catch (Exception exp)
            {
                lblError.Text = Proxy[EnumText.enumForum_Styles_PageTemplateSettingErrorPageLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);

            }
        }

        public void RepeaterBind()
        {
            try
            {
                repeater.DataSource = StyleProcess.GetAllStyleTemplates(this.UserOrOperatorId, this.SiteId);
                repeater.DataBind();
            }
            catch (Exception exception)
            {
                lblError.Text = Proxy[EnumText.enumForum_Styles_PageTemplateSettingErrorDataBind] + exception.Message;
                LogHelper.WriteExceptionLog(exception);
            }
        }

        protected override void InitLanguage()
        {
            base.InitLanguage();
            this.btnSave.Text = Proxy[EnumText.enumForum_Styles_ButtonSave];
            this.btnSave2.Text = Proxy[EnumText.enumForum_Styles_ButtonSave];
            this.btnReturn1.Text = Proxy[EnumText.enumForum_Forums_ButtonReturn];
            this.btnReturn2.Text = Proxy[EnumText.enumForum_Forums_ButtonReturn];
        }

        protected void setCurrentTemplate()
        {
            try
            {
                StyleTemplateWithPermissionCheck styleTemplate =
                            StyleProcess.GetStyleTemplateBySiteId(
                            this.UserOrOperatorId,
                            this.SiteId);
                Page.ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "template",
                    string.Format("<script>setSystemTemplate({0})</script>", styleTemplate.Id)
                    );
                hdCurrentId.Value = styleTemplate.Id.ToString();
            }
            catch (Exception exception)
            {
                lblError.Text = Proxy[EnumText.enumForum_Styles_PageTemplateSettingErrorSetCurrentTemplate] + exception.Message;
                LogHelper.WriteExceptionLog(exception);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.lblError.Text = null;
            try
            {
                int templateId = Convert.ToInt32(this.hdCurrentId.Value);
                StyleProcess.UpdateTemplate(this.SiteId, templateId, this.UserOrOperatorId);
                this.setCurrentTemplate();
                Response.Redirect("~/AdminPanel/Settings/Settings.aspx?SiteId=" + SiteId,false);
                //this.lblSuccess.Text = Proxy[EnumText.enumForum_Styles_PageTemplateSettingSuccessMessage];
            }
            catch (Exception exception)
            {
                lblError.Text = Proxy[EnumText.enumForum_Styles_PageTemplateSettingErrorGetTemplateId] + exception.Message;
                LogHelper.WriteExceptionLog(exception);
            }
            RepeaterBind();
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            this.lblError.Text = null;
            try
            {
                if (this.hdCurrentId.Value != null)
                {
                    string redirect = string.Format("~/default.aspx?TemplateID={0}&siteid={1}", this.hdCurrentId.Value, SiteId);

                    Response.Redirect(redirect, false);
                }
                else
                    this.setCurrentTemplate();
            }
            catch (Exception exception)
            {
                lblError.Text = Proxy[EnumText.enumForum_Styles_PageTemplateSettingErrorGetTemplateId] + exception.Message;
                LogHelper.WriteExceptionLog(exception);
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/AdminPanel/Settings/Settings.aspx?SiteId=" + SiteId);
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
