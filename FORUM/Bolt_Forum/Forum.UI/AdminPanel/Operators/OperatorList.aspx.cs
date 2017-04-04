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
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Comm100.Admin.Process;
using Com.Comm100.Admin.Bussiness;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.HelpDocument;
using Com.Comm100.Forum.UI.AdminPanel;
using Com.Comm100.Language;
using System.Web.Configuration;

namespace Forum.UI.AdminPanel.Operators
{
    public partial class OperatorList : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
//#if OPENSOURCE
        protected void Page_Load(object sender, EventArgs e)
        {     
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumOperatorManage);
                Master.Page.Title = Proxy[EnumText.enumForum_Operator_TitleListPage];

                if (!IsPostBack)
                {
                    btnNewOperator.Text = Proxy[EnumText.enumForum_Operator_ButtonNew];

                    string action = Request.QueryString["action"];
                    if (action == "delete")
                    {
                        try
                        {
                            CheckQueryString("id");
                            int delId = Convert.ToInt32(Request.QueryString["id"]);

                            OperatorProcess.DeleteOperator(CurrentOperator.UserOrOperatorId, CurrentOperator.SiteId, delId);

                            List<string> sessionIdList = new List<string>();
                            Cache.Insert(string.Format(ConstantsHelper.CacheKey_InactivedOrDeletedUserId, SiteId, delId), sessionIdList, null, DateTime.Now.AddMinutes(Session.Timeout), TimeSpan.Zero);


                        }
                        catch (Exception exp)
                        {
                            lblMessage.Text = Proxy[EnumText.enumForum_Operator_PageListErrorDeletingOperator] + " " + exp.Message;
                            LogHelper.WriteExceptionLog(exp);

                        }
                    }

                    ViewState["SortOrder"] = "Name";
                    ViewState["Direct"] = "ASC";

                    if (Request.QueryString["pageindex"] != null && Request.QueryString["pagesize"] != null)
                    {
                        aspnetPager.PageIndex = Convert.ToInt32(Request.QueryString["pageindex"]);
                        aspnetPager.PageSize = Convert.ToInt32(Request.QueryString["pagesize"]);
                    }

                    RefreshData();
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Operator_PageListErrorLoadingPage] + " " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void aspnetPager_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
        {
            try
            {
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Operator_PageListErrorLoadingPage] + " " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void aspnetPager_ChangePageSize(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.ChangePageSizeEventArgs ce)
        {
            try
            {
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Operator_PageListErrorLoadingPage] + " " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void RefreshData()
        {
            int recordsCount = OperatorProcess.GetCountOfNotDeletedOperator(CurrentOperator.UserOrOperatorId, CurrentOperator.SiteId);

            string sortOrder = (string)ViewState["SortOrder"] + " " + (string)ViewState["Direct"];
            OperatorWithPermissionCheck[] tmpOperators = OperatorProcess.GetNotDeletedOperatorsByPaging(CurrentOperator.UserOrOperatorId, CurrentOperator.SiteId, aspnetPager.PageIndex + 1, aspnetPager.PageSize, sortOrder);
            
            aspnetPager.CWCDataBind(gdvOperatorList, tmpOperators, recordsCount);
        }

        protected void gdvOperatorList_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                ViewState["SortOrder"] = e.SortExpression;
                if (ViewState["Direct"].ToString() == "DESC")
                    ViewState["Direct"] = "ASC";
                else
                    ViewState["Direct"] = "DESC";

                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Operator_PageListErrorLoadingPage] + " " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void gdvOperatorList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onmousedown", "highLightRow(this);");
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[0].Controls[0].Controls.Add(new LiteralControl(Proxy[EnumText.enumForum_Operator_ColumnId]));
                    e.Row.Cells[1].Controls[0].Controls.Add(new LiteralControl(Proxy[EnumText.enumForum_Operator_ColumnEmail]));
                    e.Row.Cells[2].Controls[0].Controls.Add(new LiteralControl(Proxy[EnumText.enumForum_Operator_ColumnDisplayName]));
                    e.Row.Cells[3].Controls.Add(new LiteralControl(Proxy[EnumText.enumForum_Operator_ColumnDescription]));
                    e.Row.Cells[4].Controls.Add(new LiteralControl(Proxy[EnumText.enumForum_Operator_ColumnIsAdmin]));
                    e.Row.Cells[5].Controls.Add(new LiteralControl(Proxy[EnumText.enumForum_Operator_ColumnIsActive]));
                    e.Row.Cells[6].Controls.Add(new LiteralControl(Proxy[EnumText.enumForum_Operator_ColumnResetPassword]));
                    e.Row.Cells[7].Controls.Add(new LiteralControl(Proxy[EnumText.enumForum_Operator_ColumnEdit]));
                    e.Row.Cells[8].Controls.Add(new LiteralControl(Proxy[EnumText.enumForum_Operator_ColumnDelete]));

                    if (ViewState["SortOrder"].ToString() == "Id")
                    {
                        if (ViewState["Direct"].ToString() == "ASC")
                            e.Row.Cells[0].Controls[0].Controls.Add(new LiteralControl("<img src=\"../../images/sort_up.gif\" align=\"absmiddle\">"));
                        else if (ViewState["Direct"].ToString() == "DESC")
                            e.Row.Cells[0].Controls[0].Controls.Add(new LiteralControl("<img src=\"../../images/sort_down.gif\" align=\"absmiddle\">"));
                    }
                    if (ViewState["SortOrder"].ToString() == "Email")
                    {
                        if (ViewState["Direct"].ToString() == "ASC")
                            e.Row.Cells[1].Controls[0].Controls.Add(new LiteralControl("<img src=\"../../images/sort_up.gif\" align=\"absmiddle\">"));
                        else if (ViewState["Direct"].ToString() == "DESC")
                            e.Row.Cells[1].Controls[0].Controls.Add(new LiteralControl("<img src=\"../../images/sort_down.gif\" align=\"absmiddle\">"));
                    }                    
                    
                    if (ViewState["SortOrder"].ToString() == "Name")
                    {
                        if (ViewState["Direct"].ToString() == "ASC")
                            e.Row.Cells[2].Controls[0].Controls.Add(new LiteralControl("<img src=\"../../images/sort_up.gif\" align=\"absmiddle\">"));
                        else if (ViewState["Direct"].ToString() == "DESC")
                            e.Row.Cells[2].Controls[0].Controls.Add(new LiteralControl("<img src=\"../../images/sort_down.gif\" align=\"absmiddle\">"));
                    }

                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Operator_PageListErrorLoadingPage] + " " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnNewOperator_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = aspnetPager.PageIndex;
                int pageSize = aspnetPager.PageSize;
                Response.Redirect("OperatorAdd.aspx?pageindex=" + pageIndex.ToString() + "&pagesize=" + pageSize.ToString(),false);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Operator_PageListErrorRedirectingToNewOperatorPage] + " " + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }


//#endif


    }
}
