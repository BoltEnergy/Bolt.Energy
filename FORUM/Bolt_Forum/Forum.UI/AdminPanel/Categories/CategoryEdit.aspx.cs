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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.Categories
{
    public partial class CategoryEdit : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        //private Dictionary<string, string> Options;

        #region Language
        string SaveSucceeded;
        string StateNormal;
        string StateClose;
        protected override void InitLanguage()
        {
            try
            {
                SaveSucceeded = Proxy[EnumText.enumForum_Categories_EditSaveSucceeded];
                StateNormal = Proxy[EnumText.enumForum_Categories_StateNormal];
                StateClose = Proxy[EnumText.enumForum_Categories_StateClose];
                Master.Page.Title = Proxy[EnumText.enumForum_Categories_TitleEdit];
                lblTitle.Text = Proxy[EnumText.enumForum_Categories_TitleEdit];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Categories_SubtitleEdit];
                btnSave1.Text = Proxy[EnumText.enumForum_Categories_ButtonSave];
                btnSave2.Text = Proxy[EnumText.enumForum_Categories_ButtonSave];
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }
        #endregion 
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CheckQueryString("id");
                
                if (!this.IsPostBack)
                {
                    ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumCategoriesManage);
                    this.txtName.MaxLength = ForumDBFieldLength.Category_nameFieldLength;
                    this.txtDescription.MaxLength = ForumDBFieldLength.Category_descriptionFieldLength;

                    int categoryId = Convert.ToInt32(Request.QueryString["id"]);
                    CategoryWithPermissionCheck category = CategoryProcess.GetCategoryById(this.UserOrOperatorId , this.IfOperator, this.SiteId, categoryId);
                    this.txtName.Text = category.Name;
                    this.txtDescription.Text = category.Description;
                    //this.InitOptions();
                    //this.RadioButtonListStatus.Items.Add(Options["Normal"]);
                    //this.RadioButtonListStatus.Items.Add(Options["Close"]);
                    //if (category.Status == EnumCategoryStatus.Normal)
                    //{
                    //    this.RadioButtonListStatus.SelectedIndex = 0;
                    //}
                    //else
                    //    this.RadioButtonListStatus.SelectedIndex = 1;
                    ViewState["id"] = categoryId;

                    this.txtDescription.Attributes.Add("onkeyup", "javascript:checkMaxLength(this," + ForumDBFieldLength.Category_descriptionFieldLength + string.Format(Proxy[EnumText.enumForum_Public_TextAreaMaxLength], "this.value.length", ForumDBFieldLength.Category_descriptionFieldLength));
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Categories_PageEditErrorLoadPage] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnSave2_Click(object sender, EventArgs e)
        {
            this.SaveCategory();
        }

        protected void btnSave1_Click(object sender, EventArgs e)
        {
            this.SaveCategory();
        }

        private void SaveCategory()
        {
            try
            {
                int id = Convert.ToInt32(ViewState["id"]);
                string name = this.txtName.Text.Trim();
                string description = this.txtDescription.Text.Trim();
                //this.InitOptions();
                //EnumCategoryStatus categoryStatus = this.RadioButtonListStatus.SelectedValue == Options["Normal"] ? EnumCategoryStatus.Normal : EnumCategoryStatus.Close;
                CategoryProcess.UpdateCategory(this.UserOrOperatorId, this.IfOperator, this.SiteId,id, name, description,EnumCategoryStatus.Normal);
                this.lblSuccess.Text = SaveSucceeded;
                Response.Redirect("CategoryList.aspx?siteid=" + SiteId, false);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Categories_PageEditErrorEdit] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        //private void InitOptions()
        //{
        //    this.Options = new Dictionary<string, string>();
        //    this.Options.Add("Normal", StateNormal);
        //    this.Options.Add("Close", StateClose);
        //}
    }
}
