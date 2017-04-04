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
using Com.Comm100.Framework.HelpDocument;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.UI.Common;
using System.IO;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.Styles
{
    public partial class HeaderFooterSetting : AdminBasePage
    {
        #region get form page
        protected string GetEditLogoUrl
        {
            get { return UpadteCustmizeLogoURL; }
        }
        protected string BtnEditLogoText
        {
            get { return btnEditLogotext; }
        }
        #endregion
        public string ControlPrefix
        {
            get;
            set;
        }
        private string UpadteCustmizeLogoURL;
        private string btnEditLogotext;

        string SaveSuccessfully;
        protected override void InitLanguage()
        {
            try
            {
                this.lblTitle.Text = Proxy[EnumText.enumForum_Styles_TitleHeaderFooterSetting];
                this.lblSubTitle.Text = Proxy[EnumText.enumForum_Styles_SubtitleHeaderFooterSetting];
                this.rlistMode.Items[0].Text = Proxy[EnumText.enumForum_Styles_FieldEasyModeRadio];
                this.rlistMode.Items[1].Text = Proxy[EnumText.enumForum_Styles_FieldAdvancedModeRadio];
                this.btnPreview1.Text = Proxy[EnumText.enumForum_Styles_ButtonPrivew];
                this.btnPreview2.Text = Proxy[EnumText.enumForum_Styles_ButtonPrivew];
                this.btnSave1.Text = Proxy[EnumText.enumForum_Styles_ButtonSave];
                this.btnSave2.Text = Proxy[EnumText.enumForum_Styles_ButtonSave];
                this.btnReturn1.Text = Proxy[EnumText.enumForum_Forums_ButtonReturn];
                this.btnReturn2.Text = Proxy[EnumText.enumForum_Forums_ButtonReturn];
                this.btnEditLogotext = Proxy[EnumText.enumForum_Styles_ButtonEditLogo];
                SaveSuccessfully = Proxy[EnumText.enumForum_Settings_PageHeaderFooterSettingSuccessSave];

            }
            catch (Exception exp)
            {
                this.lblMessage.Text = exp.Message;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Master.Page.Title = Proxy[EnumText.enumForum_Styles_TitleHeaderFooterSetting];
                lblTitle.Text = Proxy[EnumText.enumForum_Styles_TitleHeaderFooterSetting];

                lblSubTitle.Text = Proxy[EnumText.enumForum_Styles_SubtitleHeaderFooterSetting];

                UpadteCustmizeLogoURL = ForumConfig.GetInstance().AdminUrl
                                                        + "/AdminPanel/SiteProfile.aspx?siteid=" + SiteId;
#if OPENSOURCE
                this.phUploadButton.Visible = false;
                this.phUploadLogo.Visible = true;
                this.hdnIfPreview.Value = "0";
#else
                this.phUploadButton.Visible = true;
                this.phUploadLogo.Visible = false;
                
#endif

                if (!IsPostBack)
                {
                    ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumSiteSettings);

                    //maxlength
                    txtPageHeader.MaxLength = ForumDBFieldLength.Styles_pageHeaderFieldLength;


                    InitFormData();
                }
                else
                {
                    StyleSettingWithPermissionCheck tmpStyleSetting = StyleProcess.GetStyleSettingBySiteId(CurrentUserOrOperator.SiteId,
                    CurrentUserOrOperator.UserOrOperatorId);

                    this.InitLogoImage(tmpStyleSetting);
                }
                this.ControlPrefix = this.btnPreview1.ClientID.Replace("_" + this.btnPreview1.ID, "");

            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Styles_PageHeaderFooterSettingErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        private void InitFormData()
        {
            StyleSettingWithPermissionCheck tmpStyleSetting = StyleProcess.GetStyleSettingBySiteId(CurrentUserOrOperator.SiteId,
                CurrentUserOrOperator.UserOrOperatorId);

            ChangeShowMode(tmpStyleSetting.IfAdvancedMode);

            this.InitLogoImage(tmpStyleSetting);

            //load Header,Footer
            txtPageHeader.Text = tmpStyleSetting.PageHeader;
            txtPageFooter.Value = tmpStyleSetting.PageFooter;

        }

        private void ChangeShowMode(bool IfAdvancedMode)
        {
            if (IfAdvancedMode == false)
            {
                rlistMode.Items[0].Selected = true;
                divEasyMode.Visible = true;
                divAdvancedMode.Visible = false;
            }
            else
            {
                rlistMode.Items[1].Selected = true;
                divEasyMode.Visible = false;
                divAdvancedMode.Visible = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool ifAdvancedMode = rlistMode.Items[1].Selected;
                string pageHeader = "";
                string pageFooter = "";

                if (ifAdvancedMode)
                {
                    pageHeader = txtPageHeader.Text;
                    pageFooter = txtPageFooter.Value;
                    StyleProcess.UpdateHeaderAndFooter(CurrentUserOrOperator.SiteId, CurrentUserOrOperator.UserOrOperatorId,
                                                    ifAdvancedMode, pageHeader, pageFooter);
                    //lblSuccess.Text = Proxy[EnumText.enumForum_Styles_PageHeaderFooterSettingSuccessSave];
                    //lblSuccess.Text = SaveSuccessfully;

                }
                else
                {

                    StyleProcess.UpdateHeaderAndFooter(CurrentUserOrOperator.SiteId, CurrentUserOrOperator.UserOrOperatorId,
                                                    ifAdvancedMode, pageHeader, pageFooter);
                    //lblSuccess.Text = Proxy[EnumText.enumForum_Styles_PageHeaderFooterSettingSuccessSave];
                    //lblSuccess.Text = SaveSuccessfully;



                }
                Response.Redirect("~/AdminPanel/Settings/Settings.aspx?SiteId=" + SiteId, false);

            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Styles_PageHeaderFooterSettingErrorSave] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {

                if (rlistMode.Items[1].Selected == true)
                {


                }
                else
                {

#if OPENSOURCE



                    if (fileUploadLogo.HasFile)
                    {
                        UploadImageToTemp();
                        imgLogo.Src = "~/" + ConstantsHelper.ForumLogoTemporaryFolder + "/" + this.SiteId + Path.GetExtension(fileUploadLogo.PostedFile.FileName).ToUpper() + "?number=" + new Random().Next(1000000000);
                        Response.Write("<script language='javascript'>window.open('../../default.aspx?ifAdvancedMode=false&ifUpload=true&siteId=0');</script>");
                    }
                    else if (!fileUploadLogo.HasFile)
                    {
                        if (hdnIfPreview.Value == "1")
                        {
                            FileInfo lastFile = null;
                            DirectoryInfo di = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/" + ConstantsHelper.ForumLogoTemporaryFolder));
                            FileInfo[] fis = di.GetFiles();
                            if (fis.Length > 0)
                            {
                                DateTime lastModifyTime = DateTime.Parse("1900-01-01");

                                foreach (FileInfo fi in fis)
                                {
                                    if (fi.CreationTime > lastModifyTime)
                                    {
                                        lastFile = fi;
                                        lastModifyTime = fi.CreationTime;
                                    }
                                }
                            }
                            if (lastFile != null)
                            {
                                this.imgLogo.Src = "~/" + ConstantsHelper.ForumLogoTemporaryFolder + "/" + lastFile.Name + "?Number=" + new Random().Next(1000000000);
                                Response.Write("<script language='javascript'>window.open('../../default.aspx?ifAdvancedMode=false&ifUpload=false&siteId=0&hdnIfPreview=true');</script>");
                            }
                        }
                        else
                        {
                            this.imgLogo.Src = "Logo.aspx?siteid=" + SiteId;
                            Response.Write("<script language='javascript'>window.open('../../default.aspx?ifAdvancedMode=false&ifUpload=false&siteId=0&hdnIfPreview=false');</script>");
                        }
                    }
                    else
                    {
                        this.imgLogo.Src = this.GetButtonIMGDir() + "DefaultLogo.gif";
                    }
                    imgLogo.Visible = true;
                    this.hdnIfPreview.Value = "1";

#else
                    StyleSettingWithPermissionCheck tmpStyleSetting = StyleProcess.GetStyleSettingBySiteId(CurrentUserOrOperator.SiteId,
                              CurrentUserOrOperator.UserOrOperatorId);
                    
                    this.InitLogoImage(tmpStyleSetting);
                    Response.Write("<script language='javascript'>window.open('../../default.aspx?ifAdvancedMode=false&ifUpload=true&siteId="+this.SiteId+"');</script>");
#endif
                    //Response.Write("<script language='javascript'>window.open('../../default.aspx?ifAdvancedMode=false&ifUpload=true&siteId=0');</script>");

                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Styles_PageHeaderFooterSettingErrorPreview] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void rlistMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rlistMode.SelectedIndex == 0)
            {
                divEasyMode.Visible = true;
                divAdvancedMode.Visible = false;
            }
            else
            {
                divEasyMode.Visible = false;
                divAdvancedMode.Visible = true;
            }
        }
        private void InitLogoImage(StyleSettingWithPermissionCheck tmpStyleSetting)
        {
            if (tmpStyleSetting.IfCustomizeLogo == false)
            {
                imgLogo.Visible = true;
                //imgLogo.Src = "~/" + Com.Comm100.Framework.Common.ConstantsHelper.Default_Logo_Path;
                this.imgLogo.Src = "~/" + this.GetButtonIMGDir() + "DefaultLogo.gif";
            }
            else if (tmpStyleSetting.IfCustomizeLogo && tmpStyleSetting.CustomizeLogo != null)
            {
                imgLogo.Visible = true;
                imgLogo.Src = "CustomizeLogo.aspx?siteId=" + this.SiteId.ToString();
                //imgLogo.Src = "~/TempLogo/"+ this.SiteId + Path.(tmpStyleSetting.CustomizeLogo.ToString()).ToUpper(); //"Logo.aspx?siteid=" + SiteId;
            }
            else
            {
                imgLogo.Visible = true;
            }
        }
        private void UploadImageToTemp()
        {
            this.hdnIfUpload.Value = "1";
            if (fileUploadLogo.HasFile)
            {
                #region check the logo file

                if (fileUploadLogo.PostedFile.ContentLength > 100 * 1024)
                {
                    throw new Exception(Proxy[EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorUploadingExceedMaxFileLength]);
                    //lblMessage.Text = Proxy[EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorUploadingExceedMaxFileLength];
                    //return;
                }

                string strFileExtension = Path.GetExtension(fileUploadLogo.PostedFile.FileName).ToUpper();
                if ((strFileExtension != ".GIF") && (strFileExtension != ".JPG") && (strFileExtension != ".PNG") && (strFileExtension != ".JPEG")
                    && (strFileExtension != ".BMP"))
                {
                    throw new Exception(Proxy[EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorUploadingIncorrectFormatFile]);
                    //lblMessage.Text = Proxy[EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorUploadingIncorrectFormatFile];
                    //return;
                }
                #endregion
            }
            string localDirectory = System.Web.HttpContext.Current.Server.MapPath("~/" + ConstantsHelper.ForumLogoTemporaryFolder);
            if (Directory.Exists(localDirectory) == false)
            {
                Directory.CreateDirectory(localDirectory);
            }


            DirectoryInfo di = new DirectoryInfo(localDirectory);
            FileInfo[] fis = di.GetFiles();
            if (fis.Length > 0)
            {
                DateTime lastModifyTime = DateTime.Parse("1900-01-01");
                FileInfo lastFile = null;
                foreach (FileInfo fi in fis)
                {
                    if (fi.CreationTime > lastModifyTime)
                    {
                        lastFile = fi;
                        fi.Delete();
                    }
                }
            }
            this.fileUploadLogo.SaveAs(System.Web.HttpContext.Current.Server.MapPath("~/" + ConstantsHelper.ForumLogoTemporaryFolder + "/" + this.SiteId + Path.GetExtension(fileUploadLogo.PostedFile.FileName).ToUpper()));

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/AdminPanel/Settings/Settings.aspx?SiteId=" + SiteId, false);
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_RedirectError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileUploadLogo.HasFile)
                {
                    if (this.hdnIfPreview.Value == "0")
                    {
                        UploadImageToTemp();

                    }
                    else if (this.hdnIfPreview.Value == "1")
                    {
                        UploadImageToTemp();
                    }
                    SiteProcess.UpdateSiteLogo(CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.SiteId, true, fileUploadLogo.FileBytes);
                    //lblSuccess.Text = Proxy[EnumText.enumForum_Styles_PageHeaderFooterSettingSuccessSave];
                    //StyleSettingWithPermissionCheck tmpStyleSetting = StyleProcess.GetStyleSettingBySiteId(CurrentUserOrOperator.SiteId,
                    //     CurrentUserOrOperator.UserOrOperatorId);
                    imgLogo.Visible = true;
                    imgLogo.Src = "CustomizeLogo.aspx?siteId=" + this.SiteId.ToString();
                    //imgLogo.Visible = true;
                    ////imgLogo.Src = "~/" + ConstantsHelper.ForumLogoTemporaryFolder + "/" + this.SiteId + Path.GetExtension(fileUploadLogo.PostedFile.FileName).ToUpper() + "?number=" + new Random().Next(1000000000);
                    //imgLogo.Src = "Logo.aspx?siteid=" + SiteId;
                }
                else if (this.hdnIfUpload.Value == "1")
                {
                    FileInfo lastFile = null;
                    DirectoryInfo di = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/" + ConstantsHelper.ForumLogoTemporaryFolder));
                    FileInfo[] fis = di.GetFiles();
                    if (fis.Length > 0)
                    {
                        DateTime lastModifyTime = DateTime.Parse("1900-01-01");

                        foreach (FileInfo fi in fis)
                        {
                            if (fi.CreationTime > lastModifyTime)
                            {
                                lastFile = fi;
                                lastModifyTime = fi.CreationTime;
                            }
                        }
                    }
                    if (lastFile != null)
                    {

                        this.imgLogo.Src = "~/" + ConstantsHelper.ForumLogoTemporaryFolder + "/" + lastFile.Name + "?Number=" + new Random().Next(1000000000);
                        FileStream fs = lastFile.OpenRead();
                        byte[] bs = new byte[lastFile.Length];
                        int len = fs.Read(bs, 0, bs.Length);
                        fs.Close();
                        SiteProcess.UpdateSiteLogo(CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.SiteId, true, bs);

                       // lblSuccess.Text = Proxy[EnumText.enumForum_Styles_PageHeaderFooterSettingSuccessSave];
                        //StyleSettingWithPermissionCheck tmpStyleSetting = StyleProcess.GetStyleSettingBySiteId(CurrentUserOrOperator.SiteId,
                        // CurrentUserOrOperator.UserOrOperatorId);


                    }

                    else
                    {
                        lblMessage.Text =Proxy [EnumText .enumForum_HeaderFooter_LogoErrorFind ] ;
                    }


                    imgLogo.Visible = true;
                    hdnIfPreview.Value = "0";

                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Styles_PageSiteCustomizeLogoErrorLoadingPage] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        
    }
}
