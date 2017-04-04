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
using Com.Comm100.Framework.Common;
using System.IO;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Language;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;

namespace Com.Comm100.Forum.UI.AdminPanel.Styles
{
    public partial class SiteCustmizeLogo : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumSiteSettings);
                Master.Page.Title = Proxy[EnumText.enumForum_Styles_TitleSiteCustomizeLogo];
                btnUpload1.Text = btnUpload2.Text = Proxy[EnumText.enumForum_Styles_ButtonUpload];
                btnCancel1.Text = btnCancel2.Text = Proxy[EnumText.enumForum_Styles_ButtonCancel];

                if (!IsPostBack)
                {
                    InitFormData();
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorLoadingPage] + " " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        private void InitFormData()
        {
            SiteWithPermissionCheck tmpSite = SiteProcess.GetSiteById(CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.SiteId);

            if (tmpSite.IfCustomizeLogo)
            {
                imgLogo.Visible = true;
                imgLogo.ImageUrl = "SiteCustmizeLogoImage.aspx?" + DateTime.Now.ToString();
            }
            else
                imgLogo.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("HeaderFooterSetting.aspx", false);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {

                #region check the logo file
                if (!fileUploadLogo.HasFile)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorUploadingNoSelectFile];
                    return;
                }
                if (fileUploadLogo.PostedFile.ContentLength > 100 * 1024)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorUploadingExceedMaxFileLength];
                    return;
                }

                string strFileExtension = Path.GetExtension(fileUploadLogo.PostedFile.FileName).ToUpper();
                if ((strFileExtension != ".GIF") && (strFileExtension != ".JPG") && (strFileExtension != ".PNG") && (strFileExtension != ".JPEG")
                    && (strFileExtension != ".BMP"))
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorUploadingIncorrectFormatFile];
                    return;
                }
                #endregion

                SiteProcess.UpdateSiteLogo(CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.SiteId, true, fileUploadLogo.FileBytes);

                InitFormData();

                Response.Redirect("SiteProfile.aspx", false);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorRedirectingPage] + " " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }


    }
}
