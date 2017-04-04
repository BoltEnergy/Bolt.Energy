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
using Com.Comm100.Forum.Language;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Process;

namespace Com.Comm100.Forum.UI.AdminPanel.Ban
{
    public partial class BanIP : AdminBasePage
    {
        string ErrorLoad;
        string ErrorSaveBan;
        string BanTimeTypePermanent;
        string BanTimeTypeDays;
        string BanTimeTypeHours;
        string IPTypeIP;
        string IPTypeIPRange;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Ban_PageBanIPErrorLoad];
                ErrorSaveBan = Proxy[EnumText.enumForum_Ban_PageBanIPErrorSave];
                BanTimeTypePermanent = Proxy[EnumText.enumForum_Ban_TimeTypePermanent];
                BanTimeTypeDays = Proxy[EnumText.enumForum_Ban_TimeTypeDays];
                BanTimeTypeHours = Proxy[EnumText.enumForum_Ban_TimeTypeHours];
                IPTypeIP = Proxy[EnumText.enumForum_Ban_BanIPTypeIP];
                IPTypeIPRange = Proxy[EnumText.enumForum_Ban_BanIPTypeIPRange];
                this.lblTitle.Text = Proxy[EnumText.enumForum_Ban_TitleBanIP];
                this.lblSubTitle.Text = Proxy[EnumText.enumForum_Ban_SubTitleBanIP];
                this.btnSave1.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                this.btnSave2.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                this.btnCancel1.Text = Proxy[EnumText.enumForum_Public_ButtonCancel];
                this.btnCancel2.Text = Proxy[EnumText.enumForum_Public_ButtonCancel];
                this.RegularExpressionValidatorIP.ErrorMessage = Proxy[EnumText.enumForum_Ban_ErrorIpFormat];
                this.RegularExpressionValidatorStartIP.ErrorMessage = Proxy[EnumText.enumForum_Ban_ErrorStartIPFormat];
                this.RegularExpressionValidatorEndIP.ErrorMessage = Proxy[EnumText.enumForum_Ban_ErrorEndIPFormat];
                this.RequiredFieldValidatorIP.ErrorMessage = Proxy[EnumText.enumForum_Ban_ErrorIpRequire];
                this.RequiredFieldValidatorStartIP.ErrorMessage = Proxy[EnumText.enumForum_Ban_ErrorStartIpRequire];
                this.RequiredFieldValidatorEndIP.ErrorMessage = Proxy[EnumText.enumForum_Ban_ErrorEndIpRequire];
                this.CustomValidatorExpireFormat.ErrorMessage = Proxy[EnumText.enumForum_Ban_ErrorExpireInteger];
                this.CustomValidatorExpireRequired.ErrorMessage = Proxy[EnumText.enumForum_Ban_ErrorExpireTimeRequire];
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumBan);
                Master.Page.Title = Proxy[EnumText.enumForum_Ban_TitleBanIP];
                if (!IsPostBack)
                {
                    PageInit();
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        private void PageInit()
        {
            this.rlistIpMode.Items.Add(IPTypeIP);
            this.rlistIpMode.Items.Add(IPTypeIPRange);
            ddlExpire.Items.Add(new ListItem(BanTimeTypePermanent, Convert.ToInt32(EnumBanExpireType.Permanent).ToString()));
            ddlExpire.Items.Add(new ListItem(BanTimeTypeDays, Convert.ToInt32(EnumBanExpireType.Days).ToString()));
            ddlExpire.Items.Add(new ListItem(BanTimeTypeHours, Convert.ToInt32(EnumBanExpireType.Hours).ToString()));
            //this.ddlExpire.SelectedIndex = 0;
            this.ddlExpire.Attributes.Add("onchange", "SelectExprie();");
            if (ddlExpire.SelectedIndex == 0)
                txtExpire.Style["display"] = "none";
            this.rlistIpMode.SelectedIndex = 0;
            PageControlDisplay();
        }

        protected void rlistIpMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageControlDisplay();
            if (ddlExpire.SelectedIndex == 0)
                txtExpire.Style["display"] = "none";
            else
                txtExpire.Style["display"] = "";
        }
        protected void PageControlDisplay()
        {
            if (this.rlistIpMode.SelectedIndex == 0)
            {
                this.IPType.Visible = true;
                this.IPSimple.Visible = true;
                this.IPAdvanced1.Visible = false;
                this.IPAdvanced2.Visible = false;
            }
            else
            {
                this.IPType.Visible = true;
                this.IPSimple.Visible = false;
                this.IPAdvanced1.Visible = true;
                this.IPAdvanced2.Visible = true;
            }
        }

        private DateTime GetEndDate(DateTime startTime)
        {
            DateTime endDate = startTime;
            switch ((EnumBanExpireType)Convert.ToInt32(this.ddlExpire.SelectedIndex))
            {
                case EnumBanExpireType.Days://Days
                    endDate = endDate.AddDays(Convert.ToInt32(txtExpire.Text.Trim()));
                    break;
                case EnumBanExpireType.Hours://Hours
                    endDate = endDate.AddHours(Convert.ToInt32(txtExpire.Text.Trim()));
                    break;
                //case EnumBanExpireType.Days://Days
                //    endDate = endDate.AddDays(expire);
                //    break;
                //case EnumBanExpireType.Months://Months
                //    endDate = endDate.AddMonths(expire);
                //    break;
                //case EnumBanExpireType.Years://Years
                //    endDate = endDate.AddYears(expire);
                //    break;
                case EnumBanExpireType.Permanent://Permanent
                    endDate = endDate.AddYears(100);
                    break;
            }
            return endDate;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = DateTime.UtcNow;
                DateTime endDate = this.GetEndDate(startDate);
                string note = this.txtNotes.Text;
                long startIP = 0;
                long endIP = 0;
                string banUserOrOperator = string.Empty;
               
                if (this.rlistIpMode.SelectedIndex == 0)
                {
                    startIP = endIP = IpHelper.DottedIP2LongIP(this.txtIP.Text);
                }
                else
                {
                    startIP = IpHelper.DottedIP2LongIP(this.txtStartIP.Text);
                    endIP = IpHelper.DottedIP2LongIP(this.txtEndIP.Text);
                }
                BanProcess.AddBan(this.UserOrOperatorId, this.SiteId, EnumBanType.IP, startDate, endDate, note, banUserOrOperator, startIP, endIP);
                Response.Redirect(string.Format("Bans.aspx?siteId={0}", this.SiteId));
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorSaveBan + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("Bans.aspx?siteId={0}", this.SiteId));
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_RedirectError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
    }
}
