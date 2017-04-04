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
using System.Drawing;
using System.Drawing.Imaging;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.UI.Common;



namespace Com.Comm100.Forum.UI.HTMLEditor
{
    public partial class ImageBrowser : System.Web.UI.Page
    {
        private SessionUser GetCurrentUserOrOperator(int siteId)
        {
            SessionUser currentUser = null;
            if (Session["CurrentUser"] != null)
            {
                currentUser =SiteSession.CurrentUser as SessionUser;
                if (currentUser.SiteId == siteId)
                {
                    string key = string.Format(ConstantsHelper.CacheKey_InactivedOrDeletedUserId, siteId, currentUser.UserOrOperatorId);
                    if (Cache[key] != null)
                    {
                        List<string> sessionIdList = Cache[key] as List<string>;
                        if (!sessionIdList.Contains(Session.SessionID))
                        {
                            Session.Remove("CurrentUser");
                            Session.Remove("UserPermissionList");
                            Session.Abandon();
                            currentUser = null;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "UserOrOperatorIsDeletedOrInactivedScript", string.Format("<script>alert('{0}')</script>", "Error: Current user is inactive or deleted."));

                            sessionIdList.Add(Session.SessionID);
                        }
                    }
                }
                else
                {
                    Session.Remove("CurrentUser");
                    Session.Remove("UserPermissionList");
                    Session.Abandon();
                }
            }
            return currentUser;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

            try
            {             
#if OPENSOURCE
                int siteId = 0;
#else
                int siteId = Convert.ToInt32(this.hdnSiteId.Value);
                if (siteId == 0) throw new Exception("Querystring with name 'siteId' is null");
#endif            
                
                //Get Custom Language
                string languageName = WebUtility.GetAppSetting(Constants.WK_Defaultlang);// Com.Comm100.Forum.Language.LanguageHelper.GetLanguageName((Com.Comm100.Language.EnumLanguage)Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["EnumLanguage"]));
                //Set to cultureInfo
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(languageName);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(languageName);

                SessionUser currentUserOrOperator = this.GetCurrentUserOrOperator(siteId);
                int userOrOperatorId = currentUserOrOperator == null ? 0 : currentUserOrOperator.UserOrOperatorId;
                bool ifOperator = currentUserOrOperator == null ? false : currentUserOrOperator.IfOperator;

                #region Check Image File
                if (this.fileImage.HasFile == false)
                {
                    ExceptionHelper.ThrowPostImageFileNotExistException(fileImage.FileName);
                }
                if (this.fileImage.FileBytes.Length > ConstantsHelper.Post_Image_MaxSize * 1024)
                {
                    ExceptionHelper.ThrowPostImageFileSizeIsTooLargeException(this.fileImage.FileName);
                }
                string strFileExtension = System.IO.Path.GetExtension(this.fileImage.FileName).ToUpperInvariant();
                if ((strFileExtension != ".GIF") && (strFileExtension != ".JPG") && (strFileExtension != ".PNG") && (strFileExtension != ".JPEG")
                   && (strFileExtension != ".BMP") && (strFileExtension != ".TIFF") && (strFileExtension != ".RAW") && (strFileExtension != ".IMG"))
                {
                    ExceptionHelper.ThrowPostImageFormatErrorException(this.fileImage.FileName);
                }
                #endregion

                string name = this.fileImage.FileName;
                string type = this.fileImage.PostedFile.ContentType;
                byte[] bAvatars = this.fileImage.FileBytes;

                int imageId = PostImageProcess.AddPostImage(siteId, userOrOperatorId, ifOperator,0,(int)Com.Comm100.Framework.Enum.Forum.EnumForumPostImageUseType.Default,name,type,bAvatars);

                string appPath = Request.ApplicationPath;
                if (appPath.Length > 1)
                {
                    appPath += "/";
                }
                this.txtFileName.Text = "http://" + Request.Url.Authority + appPath + "JS/HTMLEditor/Image.aspx?id=" + imageId + "&siteid=" + siteId;
                this.phScript.Controls.Add(new LiteralControl("<script language='javascript' type='text/javascript'>cancelUpload(false);</script>"));
            }
            catch (Exception exp)
            {
                string script = string.Format("<script>alert(\"{0}\")</script>", exp.Message.Replace("\r"," ").Replace("\n"," "));
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorScript", script);
            }

        }
    }
}
