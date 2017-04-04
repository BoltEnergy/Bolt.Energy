

#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2009                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using Com.Comm100.Admin.Bussiness;
using Com.Comm100.Admin.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Framework.Enum.Admin;
using Com.Comm100.Framework.HelpDocument;
using Com.Comm100.Forum.UI.AdminPanel;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Language;

namespace Forum.UI.AdminPanel.Operators
{
    public partial class OperatorResetPassword : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumOperatorManage);
                Master.Page.Title = Proxy[EnumText.enumForum_Operator_TitleResetPasswordPage];
                btnSave1.Text = btnSave2.Text = Proxy[EnumText.enumForum_Operator_ButtonSave];
                btnCancel1.Text = btnCancel2.Text = Proxy[EnumText.enumForum_Operator_ButtonCancel];


                CheckQueryString("Id");
                CheckQueryString("pageindex");
                CheckQueryString("pagesize");

                if (!IsPostBack)
                {
                    ViewState["Id"] = Request.QueryString["Id"].ToString();

                    lblSubTitle.Text = AdminHelpDocument.helpMessageOperatorResetPWD;
                    txtPassword.MaxLength = AdminDBFieldLength.Operator_passwordFieldLength;
                    txtRetypePassword.MaxLength = AdminDBFieldLength.Operator_passwordFieldLength;
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Operator_PageResetPasswordErrorLoadingPage] + " " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(ViewState["Id"]);

                OperatorProcess.ResetPasswordByAdmin(CurrentOperator.UserOrOperatorId, CurrentOperator.SiteId, id, txtPassword.Text);

                Response.Redirect("OperatorList.aspx?pageindex=" + Request.QueryString["pageindex"].ToString() + "&pagesize=" + Request.QueryString["pagesize"].ToString(), false);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Operator_PageResetPasswordErrorResettingPassword] + " " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("OperatorList.aspx?pageindex=" + Request.QueryString["pageindex"].ToString() + "&pagesize=" + Request.QueryString["pagesize"].ToString(), false);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Operator_PageResetPasswordErrorRedirectingPage] + " " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
    }
}
