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
using Com.Comm100.Forum.UI.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI
{
    public partial class PayScore : FrameBasePage
    {
        #region Page Property
        protected int TopicId { get; set; }
        protected int ForumId { get; set; }
        protected int AttachId { get; set; }
        protected int UserScore { get; set; }
        protected int AttachScore { get; set; }
        //protected int SiteId { get; set; }
        #endregion
        protected override void InitLanguage()
        {
            base.InitLanguage();
            btnPay.Text = Proxy[EnumText.enumForum_PayScore_ButtonPay];
            btnClose.Text = Proxy[EnumText.enumForum_PayScore_ButtonClose];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.IfGuest == true)
                {
                    Response.Write("<script language='javascript' type='text/javascript'>window.parent.location='login.aspx';</script>");
                    Response.End();
                }
                else
                {
                    InitProperty();
                }
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_PayScore_ErrorLoadingPayScorePage] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        private void InitProperty()
        {
            CheckQueryString("topicId");
            this.TopicId = Convert.ToInt32(Request.QueryString["topicId"]);
            CheckQueryString("forumId");
            this.ForumId = Convert.ToInt32(Request.QueryString["forumId"]);
            CheckQueryString("attachId");
            this.AttachId = Convert.ToInt32(Request.QueryString["attachId"]);
            AttachmentWithPermissionCheck attachment = AttachmentProcess.GetAttachmentById(this.UserOrOperatorId,this.SiteId, this.AttachId);
            this.AttachScore = attachment.Score;
            UserOrOperator user = UserProcess.GetUserOrOpertorById(this.SiteId, this.UserOrOperatorId);
            this.UserScore = user.Score;
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                int attachId = Convert.ToInt32(Request.QueryString["attachId"]);
                if (attachId != -1)
                {
                    Attachment attachment = AttachmentProcess.GetAttachmentById(this.UserOrOperatorId, SiteId,
                        attachId);
                    PayHistroyProcess.AddAttachmentPayHistroy(this.UserOrOperatorId, SiteId,
                        attachId, this.UserOrOperatorId, attachment.Score, DateTime.UtcNow);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "",
                        "<script language='javascript' type='text/javascript'>ShowPostContent();</script>");
                }
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_PayScore_ErrorPayingScore] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

    }
}
