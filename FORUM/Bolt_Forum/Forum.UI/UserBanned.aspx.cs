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
using Com.Comm100.Forum.Language;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.UI
{
    public partial class UserBanned : Com.Comm100.Forum.UI.UIBasePage
    {
        public override bool IfValidateUserBanned
        {
            get
            {
                return false;
            }
        }

        public override bool IfValidateIPBanned
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
                SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);

                Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Login_PageTitle], Proxy[EnumText.enumForum_UserBanned_PageTitle], System.Web.HttpUtility.HtmlEncode(siteSetting.SiteName));
                if (this.UserOrOperatorId == 0)
                {
                    string lnkHome = "Default.aspx?siteid=" + this.SiteId;
                    Response.Redirect(lnkHome, false);
                }
                else
                {
                    BanBase ban = BanProcess.GetBanByUserOrOperatorId(this.UserOrOperatorId, this.SiteId);
                    if (ban == null)
                    {
                        string lnkHome = "Default.aspx?siteid=" + this.SiteId;
                        Response.Redirect(lnkHome, false);
                    }
                    else
                        lblUserBannedMessage.Text = string.Format(Proxy[EnumText.enumForum_Settings_UserBannedMessage], DateTimeHelper.DateFormateAsMMDDYYYYHHmmss(DateTimeHelper.UTCToLocal(ban.BanStartDate)), DateTimeHelper.DateFormateAsMMDDYYYYHHmmss(DateTimeHelper.UTCToLocal(ban.BanEndDate)));
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
