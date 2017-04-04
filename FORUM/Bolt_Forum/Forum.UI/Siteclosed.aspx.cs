
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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI
{
    public partial class Siteclosed : Com.Comm100.Forum.UI.UIBasePage
    {
        public override bool IfValidateForumClosed
        {
             get
             {
                 return false;
             }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IfError) return;
            try
            {
                SiteSettingWithPermissionCheck tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Public_CommonBrowerTitle], System.Web.HttpUtility.HtmlEncode(tmpSiteSetting.SiteName));

                CheckQueryString("ReturnUrl");
                ViewState["ReturnUrl"] = Request.QueryString["ReturnUrl"];

                SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                if (siteSetting.SiteStatus == Com.Comm100.Framework.Enum.Forum.EnumSiteStatus.Close)
                {
                    this.lblSiteName.Text = System.Web.HttpUtility.HtmlEncode(siteSetting.SiteName);
                    this.lblCloseReason.Text = siteSetting.CloseReason;//System.Web.HttpUtility.HtmlEncode(siteSetting.CloseReason);             
                }
                else
                {
                    Response.Redirect(ViewState["ReturnUrl"].ToString(),false);
                }
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }
        }
    }
}
