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
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI
{
    public partial class SendResetPasswordEmail : Com.Comm100.Forum.UI.UIBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IfError)
            {
                try
                {
                    SiteSetting siteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                    Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Login_BrowerTitleSendResetPasswordEmail], System.Web.HttpUtility.HtmlEncode(siteSetting.SiteName));
                }
                catch (Exception exp)
                {
                    lblMessage.Text = exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    IfError = true;
                }
            }
        }
    }
}
