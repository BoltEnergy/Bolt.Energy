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
using Com.Comm100.Forum.UI.Common;
using Com.Comm100.Framework.Common;
using Com.Comm100.Language;
using Com.Comm100.Framework.Language;

namespace Com.Comm100.Forum.UI
{
    public partial class UnsubscribeSingleTopic : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IfError == true) return;
            try
            {
                CheckQueryString("UserOrOperatorid");
                CheckQueryString("UserOrOperatorEmail");
                CheckQueryString("Topicid");
                int userOrOperatorId = Convert.ToInt32(Request.QueryString["UserOrOperatorid"]);
                string userOrOperatorEmail = Convert.ToString(Request.QueryString["UserOrOperatorEmail"]);
                int topicId = Convert.ToInt32(Request.QueryString["Topicid"]);
                SubscribeProcess.UnsubscribeSingleTopic(SiteId, userOrOperatorId, userOrOperatorEmail, topicId);
                lblSuccess.Text = "Unsubscribe topic succeeded.";
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = "Unsubscribe Topic Error:" + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }
        }
    }
}
