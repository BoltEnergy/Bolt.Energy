
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
using System.IO;
using System.Web.UI;
using Com.Comm100.Framework.FieldLength;
using System.Web.UI.WebControls;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.UserControl
{
    public partial class ForumHeader : BaseUserControl
    {
        protected int SiteId
        {
            get
            {
                return (this.Page as UIBasePage).SiteId;
            }
        }


        private string search;
        public string advancedSearch;
        private string errLoad;
        protected void InitLanguage()
        {
            try
            {
                search = Proxy[EnumText.enumForum_HeaderFooter_SearchText];
                errLoad = Proxy[EnumText.enumForum_HeaderFooter_HeaderErrorLoading];
            }
            catch (Exception exp)
            {
                SetError(exp);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                //this.imgLogo.ImageUrl = "~/" + Com.Comm100.Framework.Common.ConstantsHelper.Default_Logo_Path;
                this.imgLogo.ImageUrl = "~/" + ((Com.Comm100.Forum.UI.UIBasePage)this.Page).GetButtonIMGDir() + "DefaultLogo.gif";

                InitLanguage();

                lblMessage.Visible = false;
                lblMessage.Text = errLoad;

                #region hyperlink
                string defaultPathUrl = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).UrlWithAuthorityAndApplicationPath + "Default.aspx?siteid=" + SiteId;
                this.imgLogo.Attributes.Add("onclick", "javascript:window.location.href='" + defaultPathUrl + "';");
                this.imgLogo.Attributes.Add("style", "cursor:pointer");
                #endregion hyperlink

                divDefaultHeader.Visible = true;

                int siteId = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).SiteId;
                int userOrOperatorId = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).UserOrOperatorId;
                bool ifOperator = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).IfOperator;
                SessionUser currentUserOrOperator = ((Com.Comm100.Forum.UI.UIBasePage)this.Page).CurrentUserOrOperator;

                if (Request.QueryString["ifAdvancedMode"] == null)
                {
                    try
                    {
                        StyleSetting styleSetting = StyleProcess.GetStyleSettingBySiteId(siteId, userOrOperatorId);
                        if (!styleSetting.IfAdvancedMode && styleSetting.IfCustomizeLogo)
                        {
                            
                            imgLogo.ImageUrl = "~/Logo.aspx?siteid=" + SiteId;
                        }
                        else if (styleSetting.IfAdvancedMode)
                        {
                            divDefaultHeader.Visible = false;
                            Literal1.Text = styleSetting.PageHeader;
                            this.Logo.Style["display"] = "none";

                        }
                    }
                    catch (Exception exp)
                    {
                        SetError(exp);
                    }
                }
                else
                {
                    if (Request.QueryString["ifAdvancedMode"].ToLower() == "false")
                    {
                        try
                        {
                            StyleSetting styleSetting = StyleProcess.GetStyleSettingBySiteId(siteId, userOrOperatorId);
                            //load Logo
                            if (styleSetting.IfCustomizeLogo)
                            {
#if OPENSOURCE
                                if (Request.QueryString["ifUpload"].ToLower() == "true")
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
                                        this.imgLogo.ImageUrl = "~/" + ConstantsHelper.ForumLogoTemporaryFolder + "/" + lastFile.Name + "?Number=" + new Random().Next(1000000000);
                                    }
                                }

                                else if (Request.QueryString["ifUpload"].ToLower() == "false")
                                {
                                    if (Request.QueryString["hdnIfPreview"].ToLower() == "true")
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
                                            this.imgLogo.ImageUrl = "~/" + ConstantsHelper.ForumLogoTemporaryFolder + "/" + lastFile.Name + "?Number=" + new Random().Next(1000000000);
                                        }
                                    }
                                    else
                                    {
                                        this.imgLogo.ImageUrl = "~/Logo.aspx?siteid=" + SiteId
                                                        + "&Number=" + new Random().Next(1000000000);
                                    }
                                }
#else
                                imgLogo.ImageUrl = "~/Logo.aspx?siteid=" + SiteId
                                                    + "&Number=" + new Random().Next(1000000000);//'Number' is for 'Opera';
#endif
                            }
                        }
                        catch (Exception exp)
                        {
                            SetError(exp);
                        }
                    }
                    else if (Request.QueryString["ifAdvancedMode"].ToLower() == "true")
                    {
                        this.Logo.Style["display"] = "none";
                        divDefaultHeader.Visible = false;
                    }
                }

               

            }
        }

        private void SetError(Exception exp)
        {
            lblMessage.Visible = true;
            lblMessage.Text += exp.Message + "<br />";
            LogHelper.WriteExceptionLog(exp);
            ((Com.Comm100.Forum.UI.UIBasePage)this.Page).IfError = true;
        }
    }
}