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
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using System.Web.UI.HtmlControls;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.Categories
{
    public partial class CategoryList : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        #region Language
        string StateNormal;
        string StateClose;
        protected override void InitLanguage()
        {
            try
            {
                StateNormal = Proxy[EnumText.enumForum_Categories_StateNormal];
                StateClose = Proxy[EnumText.enumForum_Categories_StateClose];
                Master.Page.Title = Proxy[EnumText.enumForum_Categories_TitleList];
                lblTitle.Text = Proxy[EnumText.enumForum_Categories_TitleList];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Categories_SubtitleList];
                btnNewCategory.Text = Proxy[EnumText.enumForum_Categories_ButtonNew];
                this.lblDescription.Text = Proxy[EnumText.enumForum_Categories_ColumnDescription];
                //this.lblForums.Text = Proxy[EnumText.enumForum_Categories_ColumnForums];
                //this.lblId.Text = Proxy[EnumText.enumForum_Categories_ColumnId];
                lblName.Text = Proxy[EnumText.enumForum_Categories_ColumnName];
                lblOperation.Text = Proxy[EnumText.enumForum_Categories_ColumnOperation];
                lblOrder.Text = Proxy[EnumText.enumForum_Categories_ColumnOrder];
                //lblStatus.Text=Proxy[EnumText.enumForum_Categories_ColumnStatus];
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
            this.Form.DefaultButton = btnNewCategory.UniqueID;
            if (!this.IsPostBack)
            {                
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumCategoriesManage);
                this.RefreshData();
            }
        }

        protected void RefreshData()
        {
            try
            {
                CategoryWithPermissionCheck[]  categories= CategoryProcess.GetAllCategories(this.CurrentUserOrOperator.UserOrOperatorId, this.CurrentUserOrOperator.IfOperator, this.SiteId);

                this.repeaterCategories.DataSource = categories;
                this.repeaterCategories.DataBind();
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Categories_PageListErrorLoadPage] + exp.Message;
                
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void repeaterCategories_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Forums")
            {
                 int categoryId = Convert.ToInt32(e.CommandArgument);
                 Response.Redirect(string.Format("../Forums/ForumList.aspx?categoryId={0}&siteId={1}", categoryId, SiteId), false);                
            }
             else if (e.CommandName == "Delete")
            {
                try
                {
                    int categoryId = Convert.ToInt32(e.CommandArgument);
                    CategoryProcess.DeleteCategory(this.CurrentUserOrOperator.UserOrOperatorId, this.CurrentUserOrOperator.IfOperator, this.SiteId, categoryId);
                    
                }
                catch (Exception exp)
                {
                    this.lblMessage.Text = Proxy[EnumText.enumForum_Categories_PageListErrorDeleteCategory] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
            else if (e.CommandName == "Edit")
            {
                try
                {
                    int categoryId = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect(string.Format("CategoryEdit.aspx?id={0}&siteid={1}", categoryId, SiteId), false);
                }
                catch (Exception exp)
                {
                    this.lblMessage.Text = Proxy[EnumText.enumForum_Categories_PageListErrorEditCategory] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
            else if (e.CommandName == "Up")
            {
                try
                {
                    int categoryId = Convert.ToInt32(e.CommandArgument);
                    CategoryProcess.SortCategories(this.UserOrOperatorId, this.IfOperator, this.SiteId, categoryId, EnumSortMoveDirection.Up);
                }
                catch (Exception exp)
                {
                    this.lblMessage.Text = Proxy[EnumText.enumForum_Categories_PageListErrorOrderCategory] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
            else
            {
                try
                {
                    int categoryId = Convert.ToInt32(e.CommandArgument);
                    CategoryProcess.SortCategories(this.UserOrOperatorId, this.IfOperator, this.SiteId, categoryId, EnumSortMoveDirection.Down);
                }
                catch (Exception exp)
                {
                    this.lblMessage.Text = Proxy[EnumText.enumForum_Categories_PageListErrorOrderCategory] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
            this.RefreshData();
        }

        protected void repeaterCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                ViewState["CategoriesLength"] = ((Category[])this.repeaterCategories.DataSource).Length;

                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                {
                    if (((Category[])this.repeaterCategories.DataSource).Length == 1)
                    {
                        ImageButton imgbtnUp = e.Item.FindControl("imgbtnUp") as ImageButton;
                        imgbtnUp.Style.Add("visibility", "hidden");
                        ImageButton imgbtnDown = e.Item.FindControl("imgbtnDown") as ImageButton;
                        imgbtnDown.Style.Add("visibility", "hidden");


                    }
                    else
                    {
                        if ((e.Item.ItemIndex == 0))
                        {
                            ImageButton imgbtnUp = e.Item.FindControl("imgbtnUp") as ImageButton;
                            imgbtnUp.Style.Add("visibility", "hidden");
                            //imgbtnUp.Visible = false;
                            /*ImageButton imgbtnUp = (ImageButton)e.Item.FindControl("imgbtnUp");                        
                            imgbtnUp.ImageUrl = "../../images/sort_up_disable.gif";                        
                            imgbtnUp.OnClientClick = "javascript:return false;";
                            imgbtnUp.Enabled = false;
                            imgbtnUp.ToolTip = "";*/


                        }
                        else if ((e.Item.ItemIndex == ((Category[])this.repeaterCategories.DataSource).Length - 1))//||((Category[])this.repeaterCategories.DataSource).Length==1)
                        {
                            ImageButton imgbtnDown = e.Item.FindControl("imgbtnDown") as ImageButton;
                            imgbtnDown.Style.Add("visibility", "hidden");

                        }
                    }

                    ImageButton imgbtnDelete = e.Item.FindControl("imgbtnDelete") as ImageButton;
                    imgbtnDelete.Attributes.Add("onclick", "javascript:return confirm('" + Proxy[EnumText.enumForum_Categories_ComfirmDelete] + "');");
                }
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Categories_PageListErrorGetCategory] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }



        }

        protected void btnNewCategory_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("CategoryAdd.aspx?siteid=" + SiteId, false);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Public_RedirectError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        public string GetState(EnumCategoryStatus state)
        {
            string stateString=string.Empty;
            stateString=state == EnumCategoryStatus.Normal ? StateNormal : StateClose;
            return stateString;
        }
    }
}
