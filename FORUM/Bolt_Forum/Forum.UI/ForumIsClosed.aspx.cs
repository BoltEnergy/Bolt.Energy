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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Language;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.UI
{
    public partial class ForumIsClosed : UIBasePage
    {
        public string SiteName
        { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SiteSettingWithPermissionCheck siteSettings = SettingsProcess.GetSiteSettingBySiteId(
                    this.SiteId, this.UserOrOperatorId);
                this.SiteName = siteSettings.SiteName;
                Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Topic_PageTitleForum], Proxy[EnumText.enumForum_ForumIsClosed_ForumClosedTitle], System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(siteSettings.SiteName)));
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_ForumIsClosed_ErrorLoadingForumIsClosedPage] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
    }
}
