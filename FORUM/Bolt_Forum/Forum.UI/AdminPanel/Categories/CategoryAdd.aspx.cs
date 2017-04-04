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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.Categories
{
    public partial class CategoryAdd : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        //private Dictionary<string, string> Options;
        string StateNormal;
        string StateClose;
        protected override void InitLanguage()
        {
            try
            {
                StateNormal = Proxy[EnumText.enumForum_Categories_StateNormal];
                StateClose = Proxy[EnumText.enumForum_Categories_StateClose];
                Master.Page.Title = Proxy[EnumText.enumForum_Categories_TitleAdd];
                lblTitle.Text = Proxy[EnumText.enumForum_Categories_TitleAdd];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Categories_SubtitleAdd];
                btnSave1.Text = Proxy[EnumText.enumForum_Categories_ButtonSave];
                btnSave2.Text = Proxy[EnumText.enumForum_Categories_ButtonSave];
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
            
            if (!this.IsPostBack)
            {                
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumCategoriesManage);
                //this.InitOptions();
                //this.RadioButtonListStatus.Items.Add(Options["Normal"]);
                //this.RadioButtonListStatus.Items.Add(Options["Close"]);
                //this.RadioButtonListStatus.SelectedIndex = 0;

                this.txtName.MaxLength = ForumDBFieldLength.Category_nameFieldLength;
                this.txtDescription.MaxLength = ForumDBFieldLength.Category_descriptionFieldLength;

                this.txtDescription.Attributes.Add("onkeyup", "javascript:checkMaxLength(this," + ForumDBFieldLength.Category_descriptionFieldLength + string.Format(Proxy[EnumText.enumForum_Public_TextAreaMaxLength], "this.value.length", ForumDBFieldLength.Category_descriptionFieldLength)); 

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
                //txtName.Text = System.Web.HttpUtility.HtmlEncode(txtName.Text);
                //txtDescription.Text = System.Web.HttpUtility.HtmlEncode(txtDescription.Text);
                string name = this.txtName.Text.Trim();
                string description = this.txtDescription.Text.Trim();
                //this.InitOptions();
                //EnumCategoryStatus categoryStatus = this.RadioButtonListStatus.SelectedValue == Options["Normal"] ? EnumCategoryStatus.Normal : EnumCategoryStatus.Close;
                
                CategoryProcess.AddCategory(this.CurrentUserOrOperator.UserOrOperatorId,true, this.CurrentUserOrOperator.SiteId, name, description,EnumCategoryStatus.Normal);
                Response.Redirect("CategoryList.aspx?siteid=" + SiteId, false);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Categories_PageErrorAddCreateNew] + exp.Message;
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
