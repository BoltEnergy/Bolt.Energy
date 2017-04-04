
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
    public partial class UserAvatarEdit : UserBasePage
    {
        #region For Page to get values
        protected string IfShowSystem
        {
            get
            {
                if (_ifShowSystem == false)
                    return "display:none";
                else
                    return "display:";
            }
        }
        protected string IfShowCustom
        {
            get
            {
                if (_ifShowSystem == true)
                    return "display:none";
                else
                    return "display:";
            }
        }
        protected int AvatorIndex
        {
            get { return _SystemAvatorChoosedIndex;}
        }
        #endregion

        protected bool _ifShowSystem;
        protected int _SystemAvatorChoosedIndex = 4;//default 4;

        protected readonly string _AvatarsSystemFilePath = @"../" + ConstantsHelper.ForumSystemAvatarFolder;
        protected readonly string _AvatarsFileTempPath = @"../" + ConstantsHelper.ForumAvatarTemporaryFolder;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IfError == true) return;
            try
            {
                SiteSetting tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, this.UserOrOperatorId);
                Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Public_UserPanelBrowserTitle], Server.HtmlEncode(tmpSiteSetting.SiteName.ToString()));

                if (!Page.IsPostBack)
                {


                    ((UserMasterPage)Master).SetMenu(EnumUserMenuType.Avatar);

                    //maxlength
                    //this.ImgPicture.Width = ConstantsHelper.User_Avatar_Width;
                    //this.ImgPicture.Height = ConstantsHelper.User_Avatar_Height;
              
                    this.PageInit();
                }

                this.rblMode.Items[0].Attributes.Add("onclick", "javascript:changePanel(0);");
                this.rblMode.Items[1].Attributes.Add("onclick", "javascript:changePanel(1);");
                this.FileUpload1.Attributes.Add("onkeypress", "javascript:return false;");
            }
            catch(Exception exp) 
            {
                this.lblError.Text = Proxy[EnumText.enumForum_User_PageEditAvatarErrorLoading] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }
        }
        
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (this.IfError == true) return;
            #region Upload Avatar File
            try
            {
                int siteId = SiteId;
                int OperatorId = this.UserOrOperatorId;
                bool ifOperator = this.IfOperator;

                #region Check Avatar File
                if (FileUpload1.HasFile == false)
                {
                    ExceptionHelper.ThrowAvatarFileNotExistsException(FileUpload1.FileName);
                }
                if(FileUpload1.FileBytes.Length > ConstantsHelper.User_Avatar_MaxSize * 1024)
                {
                    ExceptionHelper.ThrowAvatarFileSizeIsTooLargeException(FileUpload1.FileName);
                }
                string strFileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToUpperInvariant();
                if ((strFileExtension != ".GIF") && (strFileExtension != ".JPG") && (strFileExtension != ".PNG") && (strFileExtension != ".JPEG")
                   && (strFileExtension != ".BMP") && (strFileExtension != ".TIFF") && (strFileExtension != ".RAW") && (strFileExtension != ".IMG"))
                {
                    ExceptionHelper.ThrowAvatarFormatErrorException(FileUpload1.FileName);
                }
                #endregion

                byte[] bScaleAvatars = null;
                #region Avatar Scale
                byte[] bAvatars = this.FileUpload1.FileBytes;
                System.IO.MemoryStream ms = new System.IO.MemoryStream(bAvatars);
                System.Drawing.Image imAvatar = System.Drawing.Image.FromStream(ms);

                System.Drawing.Size AvatarScaleSize = this.ScaleSize(imAvatar.Size);
                System.Drawing.Bitmap imAvatarScale = new System.Drawing.Bitmap(
                                                          imAvatar,
                                                          AvatarScaleSize.Width,
                                                          AvatarScaleSize.Height);
                System.IO.MemoryStream msScaleAvatar = new System.IO.MemoryStream();
                System.Drawing.Imaging.ImageFormat avatarFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                
                imAvatarScale.Save(msScaleAvatar,System.Drawing.Imaging.ImageFormat.Png);//save to 'Png'

                bScaleAvatars = msScaleAvatar.GetBuffer();

                //dispose
                imAvatarScale.Dispose();
                imAvatar.Dispose();
                ms.Dispose();
                msScaleAvatar.Dispose();
                #endregion 

                UserControlProcess.UpdateUserOrOperatorAvatar(siteId, OperatorId,ifOperator,
                    bScaleAvatars);

                this.lblSuccess.Text = Proxy[EnumText.enumForum_User_PageEditAvatarSuccessSave];
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_User_PageEditAvatarErrorUpload] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;

                string script = string.Format("<script>alert(\"{0}\")</script>", this.lblError.Text);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
            }
            #endregion
            #region PageInit
            //whatever upload Success or failure, PageInit() will be done;
            try
            {
                this.PageInit();

                this._ifShowSystem = false;
                this.rblMode.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_User_PageEditAvatarErrorUpload] + exp.Message;
                LogHelper.WriteExceptionLog(exp);

                this.IfError = true;
                string script = string.Format("<script>alert(\"{0}\")</script>", this.lblError.Text);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
            }
            #endregion
        }

        protected void btnSave1_Click(object sender, EventArgs e)
        {
            if (this.IfError == true) return;
            try
            {
                string strChoosedRbtn = Request.Form["rbtnTemp"];
                if (string.IsNullOrEmpty(strChoosedRbtn) == false)
                    this.SaveAvatar(strChoosedRbtn);

            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_User_PageEditAvatarErrorSave] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;

                string script = string.Format("<script>alert(\"{0}\")</script>",this.lblError.Text);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
            }
        }

        private void PageInit()
        {
            int siteId = SiteId;
            int OperatorId = this.UserOrOperatorId;
            bool ifOperator = this.IfOperator;

            UserOrOperator user = UserProcess.GetNotDeletedUserOrOperatorById(siteId, OperatorId);
            #region load System Avatars from file
            string strDirectory = System.Web.HttpContext.Current.Server.MapPath(this._AvatarsSystemFilePath);
            string[] strFiles = System.IO.Directory.GetFiles(strDirectory);
            List<string> strFileNames = new List<string>();
            for (int i = 0; i < strFiles.Length; i++)
            {
                string strFileExtension = System.IO.Path.GetExtension(strFiles[i]).ToLowerInvariant();
                string strFileName = System.IO.Path.GetFileName(strFiles[i]);

                if (strFileExtension == ".gif")//"gif" should be defined in System.
                {
                    string strLocalPath = this._AvatarsSystemFilePath + @"/" + strFileName;
                    strFileNames.Add(strLocalPath);
                    //Choose System Avatar's Index
                    if (user.IfCustomizeAvatar == false)
                    {
                        if (System.IO.Path.GetFileName(user.Avatar) == strFileName)
                            this._SystemAvatorChoosedIndex = strFileNames.Count - 1;
                    }
                }
            }
            #endregion
            #region show Custom Avatar
            if (user.IfCustomizeAvatar == true)
            {
                this.ImgPicture.ImageUrl = this._AvatarsFileTempPath + @"/" + this.SiteId.ToString() + @"/" +
                                            System.IO.Path.GetFileName(user.Avatar);
                this._ifShowSystem = false;
                this.rblMode.SelectedIndex = 0;
            }
            else
            {
                this.ImgPicture.ImageUrl = this._AvatarsSystemFilePath + @"/" +
                                            System.IO.Path.GetFileName(user.Avatar);
                this._ifShowSystem = true;
                this.rblMode.SelectedIndex = 1;
            }

            this.dlstViewAvators.DataSource = strFileNames;
            this.dlstViewAvators.DataBind();
            #endregion

            if (!this.UserPermissionInSignature.IfAllowCustomizeAvatar)
            {
                btnUpload.Visible = false;
                this.NewAvatar.Visible = false;
            }
        }

        protected void btnDefault_Click(object sender, EventArgs e)
        {
            if (this.IfError == true) return;
            try
            {
                this.SaveAvatar(ConstantsHelper.User_Avatar_Default);
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_User_PageEditAvatarErrorDefault] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;

                string script = string.Format("<script>alert(\"{0}\")</script>", this.lblError.Text);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
            }
        }
        private void SaveAvatar(string strAvatarPath)
        {
            int siteId = SiteId;
            int OperatorId = this.UserOrOperatorId;
            bool ifOperator = this.IfOperator;

            UserControlProcess.UpdateUserOrOperatorAvatarAsSystemProvided(siteId, OperatorId, ifOperator,
                strAvatarPath);
            this.PageInit();
            this.lblSuccess.Text = Proxy[EnumText.enumForum_User_PageEditAvatarSuccessSave];
        }
        
        protected override void InitLanguage()
        {
            try
            {
                this.rblMode.Items[0].Text = Proxy[EnumText.enumForum_User_FieldCustomAvatar];
                this.rblMode.Items[1].Text = Proxy[EnumText.enumForum_User_FieldSystemAvatar];
                this.btnDefault.Text = Proxy[EnumText.enumForum_User_ButtonDefault];
                this.btnSave1.Text = Proxy[EnumText.enumForum_User_ButtonSave];
                this.btnUpload.Text = Proxy[EnumText.enumForum_User_ButtonUpload];
                this.ImgPicture.AlternateText = Proxy[EnumText.enumForum_User_FieldCurrentAvatarText];
            }
            catch (Exception ex)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_User_PageEditAvatarErrorInitLanguage] + ex.Message;

            }
        }
        private System.Drawing.Size ScaleSize(System.Drawing.Size ImageSize)
        {
            int nDefalutWidth = ConstantsHelper.User_Avatar_Width;
            int nDefalutHeight = ConstantsHelper.User_Avatar_Height;

            //do not scale
            if (ImageSize.Width <= nDefalutWidth && ImageSize.Height <= nDefalutHeight)
                return ImageSize;

            float fImageWidth = (float)ImageSize.Width;
            float fImageHeight = (float)ImageSize.Height;

            //if image's width or height is too large to show.    
            float fWidthScale = fImageWidth / nDefalutWidth; //scale
            float fHeightScale = fImageHeight / nDefalutHeight; //scale

            if (fWidthScale > fHeightScale)
            {
                return new System.Drawing.Size(nDefalutWidth,(int)(fImageHeight * nDefalutWidth/fImageWidth));
            }
            else
            {
                return new System.Drawing.Size((int)(fImageWidth * nDefalutHeight / fImageHeight),nDefalutHeight);
            }
        }
    }
}
