
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
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.UserPanel
{
    public partial class UserSignatureEdit : UserBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IfError == true) return;
            try
            {
                SiteSetting tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, this.UserOrOperatorId);
                Master.Page.Title = String.Format(Proxy[EnumText.enumForum_Public_UserPanelBrowserTitle], System.Web.HttpUtility.HtmlEncode(tmpSiteSetting.SiteName.ToString()));

                if (!IsPostBack)
                {
                    ((UserMasterPage)Master).SetMenu(EnumUserMenuType.Signature);

                    int siteId = SiteId;
                    int OperatorId = this.UserOrOperatorId;
                    bool ifOperator = this.IfOperator;
                    //maxlength
                    this.HTMLEditor1.MaxLength = ForumDBFieldLength.User_signatureFieldLength;

                    UserOrOperator user = UserProcess.GetNotDeletedUserOrOperatorById(siteId, OperatorId);
                    
                    this.HTMLEditor1.Text = user.Signature;
                    
                }
                HtmlControlInit();
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_User_PageEditSignatureErrorLoading] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }
        }

        private void HtmlControlInit()
        {
            //if (true)//this.UserPermissionInSignature.IfSignatureAllowHTML)
            //{
                HTMLEditor1.Mode = "bandbyidorname";
                HTMLEditor1.IfAllowInsertImage = this.UserPermissionInSignature.IfSignatureAllowInsertImage;
                HTMLEditor1.IfAllowInsertLink = this.UserPermissionInSignature.IfSignatureAllowUrl;
            //}
            //else
            //    HTMLEditor1.Mode = "text";
            
        }

        protected void btnSave1_Click(object sender, EventArgs e)
        {
            if (this.IfError == true) return;
            try
            {
                int siteId = SiteId;
                int OperatorId = this.UserOrOperatorId;
                bool ifOperator = this.IfOperator;
                
                UserControlProcess.UpdateUserOrOperatorSignature(siteId, OperatorId, ifOperator,
                    this.HTMLEditor1.Text);
                this.lblSuccess.Text = Proxy[EnumText.enumForum_User_PageEditSignatureSucessSave];
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_User_PageEditSignatureErrorSave] + exp.Message;
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
                this.btnSave1.Text = Proxy[EnumText.enumForum_User_ButtonSave];
            }
            catch (Exception ex)
            {
                this.lblError.Text = ex.Message;

            }
        }
    }
}
