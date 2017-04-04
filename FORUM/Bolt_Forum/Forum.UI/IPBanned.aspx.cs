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
    public partial class IPBanned : Com.Comm100.Forum.UI.UIBasePage
    {

        public override bool IfValidateIPBanned
        {
            get
            {
                return false;
            }
        }
        public virtual bool IfValidateUserBanned { get { return false; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IfError) return;
            try
            {
                SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);

                Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Login_PageTitle], Proxy[EnumText.enumForum_IPBanned_PageTitle], System.Web.HttpUtility.HtmlEncode(siteSetting.SiteName));
                string userIp = Request.ServerVariables["remote_addr"];
                long ip = IpHelper.DottedIP2LongIP(userIp);
                BanBase ban = BanProcess.GetBanByIP(this.UserOrOperatorId, this.SiteId, ip);
                if (ban == null)
                {
                    string lnkHome = "Default.aspx?siteid=" + this.SiteId;
                    Response.Redirect(lnkHome, false);
                }
                else
                    lblIPBannedMessage.Text = string.Format(Proxy[EnumText.enumForum_Settings_IPBannedMessage], userIp, DateTimeHelper.DateFormateAsMMDDYYYYHHmmss(DateTimeHelper.UTCToLocal(ban.BanStartDate)), DateTimeHelper.DateFormateAsMMDDYYYYHHmmss(DateTimeHelper.UTCToLocal(ban.BanEndDate)));
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
