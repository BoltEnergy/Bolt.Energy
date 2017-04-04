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
using Com.Comm100.Forum.Language;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.UI.Common;
using Com.Comm100.Framework.FieldLength;

namespace Com.Comm100.Forum.UI
{
    public partial class SendMessages : FrameBasePage
    {
        public int ToUserId
        {
            get{return Convert.ToInt32(ViewState["ToUserId"]);}
            set{ViewState["ToUserId"] = value;}
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                this.txtSubject.MaxLength = ForumDBFieldLength.OutMessage_subjectFieldLength;
                this.txtMessage.MaxLength = ForumDBFieldLength.OutMessage_messageFieldLength;
                this.btnSubmit.Text = Proxy[EnumText.enumForum_SendMessages_SubTitleSendMessage];
                if (!IsPostBack)
                {
                    if (this.IfGuest == true)
                    {
                        Response.Write("<script language='javascript' type='text/javascript'>window.parent.location='login.aspx';</script>");
                    }
                    PageInit();
                }
            }
            catch (System.Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_SendMessages_ErrorLoadingSendMessagesPage] + exp.Message;
                LogHelper.WriteExceptionLog(exp);

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                MessageProcess.SendMessage(SiteId, UserOrOperatorId,
                    ToUserId, this.txtSubject.Text, this.txtMessage.Text);

                this.txtSubject.Text = "";
                this.txtMessage.Text = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "",
                    "<script language='javascript' type='text/javascript'>window.parent.location.reload();closeWindow();</script>");
                    
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_SendMessages_ErrorSendingMessage] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        private void PageInit()
        {
            CheckQueryString("userId");
            this.ToUserId = Convert.ToInt32(Request.QueryString["userId"]);
            UserOrOperator user = UserProcess.GetUserOrOpertorById(SiteId, ToUserId);
            this.lbUserName.Text = user.DisplayName;
        }
    }
}
