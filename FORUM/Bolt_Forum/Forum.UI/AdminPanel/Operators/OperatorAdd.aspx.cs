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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.UI.AdminPanel;
using Com.Comm100.Language;

namespace Forum.UI.AdminPanel.Operators
{
    public partial class OperatorAdd : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {        
//#if OPENSOURCE
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumOperatorManage);
                Master.Page.Title = Proxy[EnumText.enumForum_Operator_TitleNewPage];

                txtEmail.Focus();

                if (!IsPostBack)
                {
                    imgEmail.Attributes.Add("onmouseover", "showHelp('divHelp','" + Proxy[EnumText.enumForum_Operator_HelpEmail] + "');");
                    imgDisplayName.Attributes.Add("onmouseover", "showHelp('divHelp','" + Proxy[EnumText.enumForum_Operator_HelpDisplayName] + "');");
                    imgIsAdmin.Attributes.Add("onmouseover", "showHelp('divHelp','" + Proxy[EnumText.enumForum_Operator_HelpIsAdmin] + "');");
                    imgIsActive.Attributes.Add("onmouseover", "showHelp('divHelp','" + Proxy[EnumText.enumForum_Operator_HelpIsActive] + "');");
                    btnSave1.Text = btnSave2.Text = Proxy[EnumText.enumForum_Operator_ButtonSave];
                    btnCancel1.Text = btnCancel2.Text = Proxy[EnumText.enumForum_Operator_ButtonCancel];

                    txtEmail.MaxLength = AdminDBFieldLength.Operator_emailFieldLength;
                    txtName.MaxLength = AdminDBFieldLength.Operator_nameFieldLength;
                    txtFirstName.MaxLength = AdminDBFieldLength.Operator_firstNameFieldLength;
                    txtLastName.MaxLength = AdminDBFieldLength.Operator_lastNameFieldLength;
                    txtPassword.MaxLength = AdminDBFieldLength.Operator_passwordFieldLength;
                    txtRetypePassword.MaxLength = AdminDBFieldLength.Operator_passwordFieldLength;
                    txtDescription.MaxLength = AdminDBFieldLength.Operator_descriptionFieldLength;
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Operator_PageNewErrorLoadingPage] + " " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string name = txtName.Text;
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string password = txtPassword.Text;
            string description = txtDescription.Text;
            bool ifAdmin = cbxIsAdmin.Checked;
            bool ifActive = cbxIsActive.Checked;
            try
            {
                int siteId = 0;// CurrentOperator.SiteId;
                int tmpOperator = OperatorProcess.AddOperator(CurrentOperator.UserOrOperatorId, siteId, email, name, firstName, lastName, password, description, ifAdmin, ifActive);
                
                Response.Redirect("OperatorList.aspx?pageindex=" + Request.QueryString["pageindex"].ToString() + "&pagesize=" + Request.QueryString["pagesize"].ToString(), false);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Operator_PageNewErrorCreatingOperator] + " " + exp.Message;
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
                lblMessage.Text = Proxy[EnumText.enumForum_Operator_PageNewErrorRedirectingToOperatorsPage] + " " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
//#endif
    }
}
