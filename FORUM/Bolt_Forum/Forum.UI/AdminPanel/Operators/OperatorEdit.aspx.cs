#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2009                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using Com.Comm100.Admin.Bussiness;
using Com.Comm100.Admin.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Framework.HelpDocument;
using Com.Comm100.Forum.UI.AdminPanel;
using System.Web.Configuration;
using Com.Comm100.Language;

namespace Forum.UI.AdminPanel.Operators
{
    public partial class OperatorEdit : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
#if OPENSOURCE
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumOperatorManage);
                Master.Page.Title = Proxy[EnumText.enumForum_Operator_TitleEditPage];                
                
                CheckQueryString("id");
                CheckQueryString("pageindex");
                CheckQueryString("pagesize");

                txtEmail.Focus();

                if (!IsPostBack)
                {
                    lblSubTitle.Text = AdminHelpDocument.helpMessageOperatorEdit;
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
                    txtDescription.MaxLength = AdminDBFieldLength.Operator_descriptionFieldLength;
                
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    ViewState["id"] = id;

                    OperatorWithPermissionCheck tmpOperator = OperatorProcess.GetOperatorById(CurrentOperator.UserOrOperatorId, CurrentOperator.SiteId, id);

                    txtEmail.Text = tmpOperator.Email;
                    txtRetypeEmail.Text = tmpOperator.Email;
                    txtName.Text = tmpOperator.DisplayName;
                    txtFirstName.Text = tmpOperator.FirstName;
                    txtLastName.Text = tmpOperator.LastName;
                    txtDescription.Text = tmpOperator.Description;
                    cbxIsAdmin.Checked = tmpOperator.IfAdmin;
                    cbxIsActive.Checked = tmpOperator.IfActive;
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Operator_PageEditErrorLoadingPage] + " " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
#endif
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

#if OPENSOURCE
            try
            {
                int id = Convert.ToInt32(ViewState["id"]);
                string email = txtEmail.Text;
                string name = txtName.Text;
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                string description = txtDescription.Text;
                bool ifAdmin = cbxIsAdmin.Checked;
                bool ifActive = cbxIsActive.Checked;

                OperatorProcess.UpdateOperator(CurrentOperator.UserOrOperatorId, CurrentOperator.SiteId, id, email, name, firstName, lastName, description, ifAdmin,ifActive);

                #region Disable 用户时同时清除此用户的Session

                string cacheKey = string.Format(ConstantsHelper.CacheKey_InactivedOrDeletedUserId, SiteId, id);
                if (ifActive)
                {
                    if (Cache[cacheKey] != null)
                    {
                        Cache.Remove(cacheKey);
                    }
                }
                else
                {
                    List<string> sessionIdList = new List<string>();
                    Cache.Insert(cacheKey, sessionIdList, null, DateTime.Now.AddMinutes(Session.Timeout), TimeSpan.Zero);
                }

                #endregion

                Response.Redirect("OperatorList.aspx?pageindex=" + Request.QueryString["pageindex"].ToString() + "&pagesize=" + Request.QueryString["pagesize"].ToString(), false);

            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Operator_PageEditErrorUpdatingOperator] + " " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
#endif
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

#if OPENSOURCE
            try
            {
                Response.Redirect("OperatorList.aspx?pageindex=" + Request.QueryString["pageindex"].ToString() + "&pagesize=" + Request.QueryString["pagesize"].ToString(),false);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Operator_PageEditErrorRedirectingToOperatorsPage] + " " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }

#endif
        }
    }
}
