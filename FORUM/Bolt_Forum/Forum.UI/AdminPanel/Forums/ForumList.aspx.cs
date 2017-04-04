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
using System.Web.UI.HtmlControls;
using Com.Comm100.Forum.UI.AdminPanel;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.HelpDocument;
using System.Data;
using Com.Comm100.Forum.Language;

namespace Forum.UI.AdminPanel.Forums
{
    public partial class ForumList : AdminBasePage
    {
        string _ifDisplay = null;
        public string IfDisplay
        {
            get
            {
                if (_ifDisplay == null)
                {
                    ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId);
                    if (!forumFeature.IfEnableReputationPermission && !forumFeature.IfEnableGroupPermission)
                        _ifDisplay = "display:none;";
                    else
                        _ifDisplay = "";
                }
                return _ifDisplay;
            }
        }

        #region Language
        string ErrorLoad;
        string ErrorPermissions;
        //string ErrorAnnouncements;
        //string ErrorTopics;
        string ErrorDelete;
        string ErrorEdit;
        string ErrorDown;
        string ErrorUp;
        string ErrorRedirect;
        string ForumStatusNormal;
        string ForumStatusClose;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Forums_PageListErrorLoadPage];
                ErrorPermissions = Proxy[EnumText.enumForum_Public_RedirectError];
                //ErrorAnnouncements = "Error Redirecting Forum Announcements Page:";
                //ErrorTopics = "Error Redirecting Forum Topics";
                ErrorDelete=Proxy[EnumText.enumForum_Forums_PageListErrorDelete];
                ErrorEdit = Proxy[EnumText.enumForum_Public_RedirectError];
                ErrorUp = Proxy[EnumText.enumForum_Forums_PageListErrorSortUp];
                ErrorDown=Proxy[EnumText.enumForum_Forums_PageListErrorSortDown];
                ErrorRedirect = Proxy[EnumText.enumForum_Public_InitializatingLanguageError];
                ForumStatusNormal = Proxy[EnumText.enumForum_Forums_StatusNormal];
                ForumStatusClose = Proxy[EnumText.enumForum_Forums_StatusClose];
                Master.Page.Title = Proxy[EnumText.enumForum_Forums_TitleList];
                lblTitle.Text = Proxy[EnumText.enumForum_Forums_TitleList];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Forums_TitleSubtitleList];
                btnNewForum1.Text = Proxy[EnumText.enumForum_Forums_ButtonNew];
                btnNewForum2.Text = Proxy[EnumText.enumForum_Forums_ButtonNew];
                btnCancel.Text = Proxy[EnumText.enumForum_Forums_ButtonCancel];
                btnCancel1.Text = Proxy[EnumText.enumForum_Forums_ButtonCancel];
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
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumForumManage);
                
                if (!IsPostBack)
                {
                    string action = Request.QueryString["action"];
                    RefreshData();
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Forums_PageListErrorLoadPage] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void RefreshData()
        {
            //int categoryId =Convert.ToInt32( Request.QueryString["categoryId"]);
            //if (categoryId != 0)
            //{
            //    this.btnCancel1.Visible = true;
            //    this.btnCancel.Visible = true;
                
            //    rptCategory.DataSource =new CategoryWithPermissionCheck[]{ CategoryProcess.GetCategoryById(CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.IfOperator, CurrentUserOrOperator.SiteId, categoryId)};
            //    rptCategory.DataBind();
            //}
            //else
            //{
                this.btnCancel.Visible = false;
                this.btnCancel1.Visible = false;
                rptCategory.DataSource = CategoryProcess.GetAllCategories(CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.IfOperator, CurrentUserOrOperator.SiteId);
                rptCategory.DataBind();
            //}
        }
        protected void rptCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
                {
                    Repeater rpt = (Repeater)e.Item.FindControl("rptForum");
                    
                    if (rpt!= null)
                    {
                        Category tmpCategory = (Category)e.Item.DataItem;

                        Com.Comm100.Forum.Bussiness.ForumWithPermissionCheck[] tempForums = ForumProcess.GetForumsByCategoryID(CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.SiteId, tmpCategory.CategoryId);

                        ViewState["ForumsLeghth"] = tempForums.Length;

                        rpt.DataSource = tempForums;
                        rpt.DataBind();
                    }
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Forums_PageListErrorLoadPage] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
               
            }
        }
        protected void rptForum_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            try
            {
                int forumId=Convert.ToInt32(e.CommandArgument);
                #region Permission
                if (e.CommandName == "Permission")
                {
                    try
                    {
                        ForumWithPermissionCheck forum = ForumProcess.GetForumByForumId(SiteId, UserOrOperatorId, forumId);
                        Response.Redirect(string.Format("ForumPermission.aspx?forumId={0}&siteId={1}", forumId, SiteId));
                    }
                    catch (Exception exp)
                    {
                        lblMessage.Text = ErrorPermissions + exp.Message;
                        LogHelper.WriteExceptionLog(exp);
                    }
                }
                #endregion Permission
                //#region Announcement
                //else if (e.CommandName == "Announcement")
                //{
                //    try
                //    {
                //        ForumWithPermissionCheck forum = ForumProcess.GetForumByForumId(SiteId, UserOrOperatorId, forumId);
                //        Response.Redirect(string.Format("../Announcement/AnnouncementList.aspx?forumId={0}&siteId={1}", forumId, SiteId));
                //    }
                //    catch (Exception exp)
                //    {
                //        lblMessage.Text = ErrorAnnouncements + exp;
                //        LogHelper.WriteExceptionLog(exp);
                //    }
                //}
                //#endregion Announcement
                //#region Topics
                //else if (e.CommandName == "Topics")
                //{
                //    try
                //    {
                //        ForumWithPermissionCheck forum = ForumProcess.GetForumByForumId(SiteId, UserOrOperatorId, forumId);
                //        Response.Redirect(string.Format("../TopicAndPost/Topics.aspx?forumId={0}&siteId={1}", forumId, SiteId));
                //    }
                //    catch (Exception exp)
                //    {
                //        lblMessage.Text = ErrorTopics + exp.Message;
                //        LogHelper.WriteExceptionLog(exp);
                //    }
                //}
                //#endregion Topics
                #region Delete
                else if (e.CommandName == "Delete")
                {
                    try
                    {
                        ForumWithPermissionCheck forum = ForumProcess.GetForumByForumId(SiteId, UserOrOperatorId, forumId);
                        ForumProcess.DeleteForum(CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.SiteId, forumId);
                    }
                    catch (Exception exp)
                    {
                        lblMessage.Text = ErrorDelete + exp.Message;
                        LogHelper.WriteExceptionLog(exp);

                    }
                }
                #endregion Delete
                #region Edit
                else if (e.CommandName == "Edit")
                {
                    try
                    {
                        ForumWithPermissionCheck forum = ForumProcess.GetForumByForumId(SiteId, UserOrOperatorId, forumId);
                        Response.Redirect(string.Format("ForumEdit.aspx?forumId={0}&siteId={1}", forumId, SiteId));
                    }
                    catch (Exception exp)
                    {
                        lblMessage.Text = ErrorEdit + exp.Message;
                        LogHelper.WriteExceptionLog(exp);
                    }

                }
                #endregion Edit
                #region Up
                else if (e.CommandName == "Up")
                {
                    try
                    {
                        ForumWithPermissionCheck forum = ForumProcess.GetForumByForumId(SiteId, UserOrOperatorId, forumId);
                        Com.Comm100.Forum.Bussiness.ForumWithPermissionCheck tmpForum = ForumProcess.GetForumByForumId(CurrentUserOrOperator.SiteId, CurrentUserOrOperator.UserOrOperatorId, forumId);

                        if (tmpForum.OrderId > 0)
                            ForumProcess.SortForums(CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.SiteId, forumId, EnumSortMoveDirection.Up);
                    }
                    catch (Exception exp)
                    {
                        lblMessage.Text = ErrorUp + exp.Message;
                        LogHelper.WriteExceptionLog(exp);
                    }
                }
                #endregion Up
                #region Down
                else if (e.CommandName == "Down")
                {
                    try
                    {
                        Com.Comm100.Forum.Bussiness.ForumWithPermissionCheck tmpForum = ForumProcess.GetForumByForumId(CurrentUserOrOperator.SiteId, CurrentUserOrOperator.UserOrOperatorId, forumId);
                        Com.Comm100.Forum.Bussiness.ForumWithPermissionCheck[] tmpForms = ForumProcess.GetForumsByCategoryID(CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.SiteId, tmpForum.CategoryId);

                        if (tmpForum.OrderId < tmpForms.Length - 1)
                            ForumProcess.SortForums(CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.SiteId, forumId, EnumSortMoveDirection.Down);
                    }
                    catch (Exception exp)
                    {
                        lblMessage.Text = ErrorDown + exp.Message;
                        LogHelper.WriteExceptionLog(exp);
                    }
                }
                #endregion
                this.RefreshData();
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void rptForum_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try 
            {
                if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
                {
                    Repeater rpt = (Repeater)e.Item.FindControl("rptModerator");
                    if (rpt != null)
                    {
                        Com.Comm100.Forum.Bussiness.Forum tmpForum = (Com.Comm100.Forum.Bussiness.Forum) e.Item.DataItem;
                            
                        if (((int)ViewState["ForumsLeghth"] == 1) || e.Item.ItemIndex == 0)
                        {
                            ImageButton imgbtnSortUp = (ImageButton)e.Item.FindControl("imgbtnSortUp");

                            imgbtnSortUp.ImageUrl = "~/images/sort_up_disable.gif";
                            imgbtnSortUp.ToolTip = "";
                            imgbtnSortUp.Style.Add("visibility", "hidden");     
                        }
                        if (((int)ViewState["ForumsLeghth"] == 1) || e.Item.ItemIndex == ((int)ViewState["ForumsLeghth"] - 1))
                        {
                            ImageButton imgbtnSortDown = (ImageButton)e.Item.FindControl("imgbtnSortDown");

                            imgbtnSortDown.ImageUrl = "~/images/sort_down_disable.gif";
                            imgbtnSortDown.ToolTip = "";
                            imgbtnSortDown.Style.Add("visibility", "hidden");
                        }
                        Moderator[] tempModerators = ForumProcess.GetModeratorsByForumId(tmpForum.ForumId,CurrentUserOrOperator.SiteId,CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.IfOperator);
                        ViewState["ModeratorsLength"] = tempModerators.Length;
                        rpt.DataSource = tempModerators;
                        rpt.DataBind();
                    }
                }

            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Forums_PageListErrorGetting] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            
            }
        }

        protected void rptModerator_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Moderator moderator = (Moderator)e.Item.DataItem;
                Label lblDisplayNames = (Label)e.Item.FindControl("lblDisplayName");
                if (e.Item.ItemIndex < Convert.ToInt32(ViewState["ModeratorsLength"]) - 1)
                {
                    lblDisplayNames.Text = System.Web.HttpUtility.HtmlEncode(moderator.DisplayName) + ",";
                }
                else
                {
                    lblDisplayNames.Text = System.Web.HttpUtility.HtmlEncode(moderator.DisplayName);
                }
            }
        }
        protected void btnNewForum_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("ForumAdd.aspx?siteid=" + SiteId, false);
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorRedirect + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("../Categories/CategoryList.aspx?siteId={0}", SiteId));
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorRedirect + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected string GetForumStatusName(Com.Comm100.Framework.Enum.Forum.EnumForumStatus statusEnum)
        {
            string statusStr = string.Empty;
            if (statusEnum == Com.Comm100.Framework.Enum.Forum.EnumForumStatus.Open)   //update statu in framework
            {
                statusStr = ForumStatusNormal;
            }
            else
                statusStr = ForumStatusClose;
            return statusStr;
        }
    }

}
