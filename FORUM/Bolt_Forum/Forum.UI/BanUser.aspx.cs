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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Language;
using Com.Comm100.Forum.UI.Common;
using Com.Comm100.Framework.FieldLength;

namespace Com.Comm100.Forum.UI
{
    public enum EnumExprieTime
    {
        Permanent,
        //Months,
        Days,
        Hours,
    }
    public partial class BanUser : FrameBasePage
    {
        public int ToUserId
        {
            get { return Convert.ToInt32(ViewState["ToUserId"]); }
            set { ViewState["ToUserId"] = value; }
        }

        //protected override void InitLanguage()
        //{
        //    base.InitLanguage();
        //    this.btnSubmit.Text = Proxy[EnumText.enumForum_BanUser_ButtonBan];

        //    this.ddlExpire.Items.Add(Proxy[EnumText.enumForum_BanUser_FiledPermanent]);
        //    this.ddlExpire.Items.Add(Proxy[EnumText.enumForum_BanUser_FliedMonths]);
        //   // this.ddlExpire.Items.Add(Proxy[EnumText.enumForum_BanUser_FiledMinuets]);
        //   // this.ddlExpire.Items.Add(Proxy[EnumText.enumForum_BanUser_FiledHours]);
        //    this.ddlExpire.Items.Add(Proxy[EnumText.enumForum_BanUser_FiledDays]);

        //    //this.ddlExpire.Items.Add(Proxy[EnumText.enumForum_BanUser_FiledYears]);
 
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Title = Proxy[EnumText.enumForum_BanUser_PageTitleBanUser];
                if (!IsPostBack)
                {
                    PageInit();
                }
            }
            catch (System.Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_BanUser_ErrorLoadingBanUserPage] + exp.Message;
                LogHelper.WriteExceptionLog(exp);

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageProcess.SendMessage(SiteId, UserOrOperatorId,
                //    ToUserId, this.txtSubject.Text, this.txtMessage.Value);
                int forumId = Convert.ToInt32(Request.QueryString["forumId"]);
                int exprieTimeNum = Convert.ToInt32(this.txtExpire.Text.Trim());
                EnumExprieTime exprieTime = (EnumExprieTime)(this.ddlExpire.SelectedIndex);
                DateTime startDate;DateTime endDate;
                GetDate(out startDate,out endDate,exprieTimeNum,exprieTime);
                BanProcess.AddBanInUI(SiteId, UserOrOperatorId,forumId, startDate, endDate, this.taNotes.Text, ToUserId);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "",
                    "<script language='javascript' type='text/javascript'>window.parent.location.reload();closeWindow();</script>");

            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_BanUser_ErrorBanningUser] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        private void PageInit()
        {
            this.btnSubmit.Text = Proxy[EnumText.enumForum_BanUser_ButtonBan];
            this.taNotes.MaxLength = ForumDBFieldLength.Ban_noteFieldLength;

            this.ddlExpire.Items.Add(Proxy[EnumText.enumForum_BanUser_FiledPermanent]);
            this.ddlExpire.Items.Add(Proxy[EnumText.enumForum_BanUser_FiledDays]);
            this.ddlExpire.Items.Add(Proxy[EnumText.enumForum_BanUser_FiledHours]);


            CheckQueryString("userId");
            
            this.ToUserId = Convert.ToInt32(Request.QueryString["userId"]);
            UserOrOperator user = UserProcess.GetUserOrOpertorById(SiteId, ToUserId);
            this.lbBanUser.Text = user.DisplayName;
            this.ddlExpire.Attributes.Add("onchange", "SelectExprie();");
        }
        private void GetDate(out DateTime startDate, out DateTime endDate,int exprieTimeNum,EnumExprieTime exprieTime)
        {
            startDate = DateTime.UtcNow;
            endDate = new DateTime();
            switch(exprieTime)
            {
                //case EnumExprieTime.Minutes:
                //    endDate = startDate.AddMinutes(exprieTimeNum);
                //    break;
                case EnumExprieTime.Hours:
                    endDate = startDate.AddHours(exprieTimeNum);
                    break;
                case EnumExprieTime.Days:
                    endDate = startDate.AddDays(exprieTimeNum);
                    break;
                //case EnumExprieTime.Months:
                //    endDate = startDate.AddMonths(exprieTimeNum);
                //    break;
                //case EnumExprieTime.years:
                //    endDate = startDate.AddYears(exprieTimeNum);
                //    break;
                case EnumExprieTime.Permanent:
                    endDate = startDate.AddYears(100);
                    break;
            }
        }
    }
}
