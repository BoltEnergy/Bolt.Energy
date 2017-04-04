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
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.Ban
{
    public partial class EditBan : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        string ErrorLoad;
        string ErrorSaveBan;
        string IPTypeIP;
        string IPTypeIPRange;
        string BanTimeTypePermanent;
        string BanTimeTypeDays;
        string BanTimeTypeHours;
        protected override void InitLanguage()
        {
            try
            {
                BanTimeTypePermanent = Proxy[EnumText.enumForum_Ban_TimeTypePermanent];
                BanTimeTypeDays = Proxy[EnumText.enumForum_Ban_TimeTypeDays];
                BanTimeTypeHours = Proxy[EnumText.enumForum_Ban_TimeTypeHours];
                IPTypeIP = Proxy[EnumText.enumForum_Ban_BanIPTypeIP];
                IPTypeIPRange = Proxy[EnumText.enumForum_Ban_BanIPTypeIPRange];
                ErrorLoad = Proxy[EnumText.enumForum_Ban_PageEditBanErrorLoad];
                ErrorSaveBan = Proxy[EnumText.enumForum_Ban_PageEditBanErrorSave];
                lblTitle.Text = Proxy[EnumText.enumForum_Ban_PageEditBanTitle];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Ban_PageEditBanSubTitle];
                btnSave2.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnSave1.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnCancel1.Text = Proxy[EnumText.enumForum_Public_ButtonCancel];
                btnCancel2.Text = Proxy[EnumText.enumForum_Public_ButtonCancel];
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
                Master.Page.Title = Proxy[EnumText.enumForum_Ban_PageEditBanTitle];
                if (!IsPostBack)
                {
                    PageInit();
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad+ exp.Message;
                LogHelper.WriteExceptionLog(exp);

            }
        }

        protected void PageInit()
        {
            this.rlistIpMode.Items.Add(IPTypeIP);
            this.rlistIpMode.Items.Add(IPTypeIPRange);
            this.ddlExpire.Items.Add(new ListItem(BanTimeTypePermanent,Convert.ToInt16(EnumBanExpireType.Permanent).ToString()));
            this.ddlExpire.Items.Add(new ListItem(BanTimeTypeDays, Convert.ToInt16(EnumBanExpireType.Days).ToString()));
            this.ddlExpire.Items.Add(new ListItem(BanTimeTypeHours, Convert.ToInt16(EnumBanExpireType.Hours).ToString()));
            //this.ddlExpire.Items.Add("Days");
            //this.ddlExpire.Items.Add("Months");
            //this.ddlExpire.Items.Add("Years");
            this.ddlExpire.Attributes.Add("onchange", "SelectExprie();");
            

            this.CheckQueryString("banId");
            int banId = Convert.ToInt32(Request.QueryString["banId"]);
            BanBase ban = BanProcess.GetBanById(this.UserOrOperatorId, this.SiteId, banId);
            EnumBanType type = EnumBanType.IP;
            if (ban is BanUserOrOperatorWithPermissionCheck)
            {
                this.lblUser.Text = ban.UserOrIP;
                type = EnumBanType.User;
            }
            else
            {
                BanIPWithPermissionCheck banIP = ban as BanIPWithPermissionCheck;
                type = EnumBanType.IP;
                if (banIP.BanStartIP == banIP.BanEndIP)
                {
                    this.rlistIpMode.SelectedIndex = 0;
                    this.txtIP.Text = IpHelper.LongIP2DottedIP(banIP.BanStartIP);
                }
                else
                {
                    this.rlistIpMode.SelectedIndex = 1;
                    this.txtStartIP.Text = IpHelper.LongIP2DottedIP(banIP.BanStartIP);
                    this.txtEndIP.Text = IpHelper.LongIP2DottedIP(banIP.BanEndIP);
                }

            }
            PageControlDisplay(type);
            this.txtNotes.Text = ban.Note;
            SetExpire(ban.BanStartDate, ban.BanEndDate);
            if (ddlExpire.SelectedIndex == 0)
                txtExpire.Style["display"] = "none";
        }

        #region Private Function
        private void PageControlDisplay(EnumBanType type)
        {
            if (type == EnumBanType.User)
            {
                this.userType.Visible = true;
                this.IPType.Visible = false;
                this.IPSimple.Visible = false;
                this.IPAdvanced1.Visible = false;
                this.IPAdvanced2.Visible = false;
            }
            else
            {
                if (this.rlistIpMode.SelectedIndex == 0)
                {
                    this.userType.Visible = false;
                    this.IPType.Visible = true;
                    this.IPSimple.Visible = true;
                    this.IPAdvanced1.Visible = false;
                    this.IPAdvanced2.Visible = false;
                }
                else
                {
                    this.userType.Visible = false;
                    this.IPType.Visible = true;
                    this.IPSimple.Visible = false;
                    this.IPAdvanced1.Visible = true;
                    this.IPAdvanced2.Visible = true;
                }
            }
        }
        private void SetExpire(DateTime startTime, DateTime endTime)
        {
            //int minutes = endTime.Minute - startTime.Minute;
            int hours = endTime.Hour - startTime.Hour;
            int days = endTime.Day - startTime.Day;
            //int months = endTime.Month - startTime.Month;
            int years = endTime.Year - startTime.Year;
            if (hours != 0)
            {
                this.ddlExpire.SelectedValue = Convert.ToInt16(EnumBanExpireType.Hours).ToString();
                this.txtExpire.Text = ((TimeSpan)(endTime - startTime)).TotalHours.ToString();
            }
            else if (days != 0)
            {
                this.ddlExpire.SelectedValue = Convert.ToInt16(EnumBanExpireType.Days).ToString();
                this.txtExpire.Text = ((TimeSpan)(endTime - startTime)).TotalDays.ToString();
            }
            else
                this.ddlExpire.SelectedValue = Convert.ToInt16(EnumBanExpireType.Permanent).ToString();
        }
        private DateTime GetEndDate(DateTime startTime)
        {
            DateTime endDate = startTime;
            switch ((EnumBanExpireType)Convert.ToInt16(this.ddlExpire.SelectedValue))
            {
                case EnumBanExpireType.Days://Days
                    endDate = endDate.AddDays(Convert.ToInt32(txtExpire.Text.Trim()));
                    break;
                case EnumBanExpireType.Hours://Hours
                    endDate = endDate.AddHours(Convert.ToInt32(txtExpire.Text.Trim()));
                    break;
                case EnumBanExpireType.Permanent://Permanent
                    endDate = endDate.AddYears(100);
                    break;
            }
            return endDate;
        }
        #endregion
        #region Control Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.CheckQueryString("banId");
                int banId = Convert.ToInt32(Request.QueryString["banId"]);
                BanBase ban = BanProcess.GetBanById(this.UserOrOperatorId, this.SiteId, banId);

                EnumBanType type = EnumBanType.IP;
                DateTime startDate = ban.BanStartDate;
                DateTime endDate = GetEndDate(startDate);
                string note = this.txtNotes.Text;
                int banUserOrOperatorId = 0;
                long startIP = 0;
                long endIP = 0;

                if (ban is BanUserOrOperatorWithPermissionCheck)
                {
                    type = EnumBanType.User;
                    banUserOrOperatorId = ((BanUserOrOperatorWithPermissionCheck)ban).UserOrOperatorId;
                }
                if (rlistIpMode.SelectedIndex == 0)
                {
                    startIP = endIP = IpHelper.DottedIP2LongIP(this.txtIP.Text);
                }
                else
                {
                    startIP = IpHelper.DottedIP2LongIP(this.txtStartIP.Text);
                    endIP = IpHelper.DottedIP2LongIP(this.txtEndIP.Text);
                }
                BanProcess.UpdateBan(this.UserOrOperatorId, this.SiteId, banId, type, startDate, endDate, note, banUserOrOperatorId, startIP, endIP);
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
        protected void rlistIpMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageControlDisplay(EnumBanType.IP);
        }
        #endregion
    }
}
