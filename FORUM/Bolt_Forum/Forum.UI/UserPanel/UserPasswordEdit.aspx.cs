
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
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.UserPanel
{
    public partial class UserPasswordEdit : Com.Comm100.Forum.UI.UserPanel.UserBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IfError == true) return;
            try
            {
#if OPENSOURCE
#else
                if (Request.ServerVariables["SERVER_PORT"] == "80")
                {
                    string qstring;
                    string httpsurl;
                    qstring = Request.Url.AbsoluteUri;
                    httpsurl = qstring.Replace("http", "https");
                    Response.Redirect(httpsurl, false);
                }

                string httpsScript = "<script language=\"javascript\" type=\"text/javascript\">replaceHttps2Http();</script>";
                this.phHttps.Controls.Add(new LiteralControl(httpsScript));
#endif
                SiteSetting tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, this.UserOrOperatorId);
                Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Public_UserPanelBrowserTitle], System.Web.HttpUtility.HtmlEncode(tmpSiteSetting.SiteName.ToString()));

                if (!Page.IsPostBack)
                {
                    Page.Form.DefaultButton = this.btSave1.UniqueID;
                    ((UserMasterPage)Master).SetMenu(EnumUserMenuType.Password);
                    //maxlength
                    this.txtCurrentPassword.MaxLength = ForumDBFieldLength.User_passwordFieldLength;
                    this.txtNewPassword.MaxLength = ForumDBFieldLength.User_passwordFieldLength;
                    this.txtRetypePassword.MaxLength = ForumDBFieldLength.User_passwordFieldLength;
                }

            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_User_PageEditPasswordErrorLoading] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }
        }

        protected void btSave1_Click(object sender, EventArgs e)
        {
            if (this.IfError == true) return;
            try
            {
                int siteId = SiteId;
                int OperatorId = this.UserOrOperatorId;
                bool ifOperator = this.IfOperator;
                //encrypt
                string strEncryptCurrentPassword = Encrypt.EncryptPassword(this.txtCurrentPassword.Text);
                string strEncryptNewPassword = Encrypt.EncryptPassword(this.txtNewPassword.Text);

                UserControlProcess.ResetUserOrOperatorPassword(siteId, OperatorId, ifOperator,
                    strEncryptCurrentPassword, strEncryptNewPassword);

                this.lblSuccess.Text = Proxy[EnumText.enumForum_User_PageEditPasswordSuccessSave];
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_User_PageEditPasswordErrorSave] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;

                string script = string.Format("<script>alert(\"{0}\")</script>", this.lblError.Text);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
            }
        }
        protected override void InitLanguage()
        {
            try
            {
                this.btSave1.Text = Proxy[EnumText.enumForum_User_ButtonSave];
                
                this.ValidNewPasswordRequired.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorNewPasswordRequired];
                this.ValidRetypePasswordRequired.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorRetypePasswordRequired];
                this.vaildRetypePasswordCompare.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorRetypePsswordMatch];
            }
            catch (Exception ex)
            {
                this.lblError.Text = ex.Message;

            }
        }
    }
}
