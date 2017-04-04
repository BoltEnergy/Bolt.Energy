#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif

using System;
using System.Data;
using System.Web.UI.WebControls;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.UI.AdminPanel;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.WebControls;
using Com.Comm100.Framework.HelpDocument;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.Language;

namespace Forum.UI.AdminPanel.Users
{
    public partial class RatifyUser : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        #region resource
        private string ErrorAccept;
        private string ErrorRefuse;
        private string ErrorGetNotModeratedUser;
        private string ErrorLoad;
        private string ErrorQuery;
        private string ErrorModerteUser;
        private string ErrorMessage;
        protected override void InitLanguage()
        {
            try
            {
                lblTitle.Text = Proxy[EnumText.enumForum_User_TitleUserModeration];
                lblSubTitle.Text = Proxy[EnumText.enumForum_User_SubtitleUserModeration];
                btnQuery1.Text = Proxy[EnumText.enumForum_User_ButtonQuery];
                btnQuery2.Text = Proxy[EnumText.enumForum_User_ButtonQuery];
                Master.Page.Title = Proxy[EnumText.enumForum_User_TitleUserModeration];
                ErrorAccept = Proxy[EnumText.enumForum_User_PageModerationErrorAccept];
                ErrorRefuse = Proxy[EnumText.enumForum_User_PageModerationErrorRefuse];
                ErrorGetNotModeratedUser = Proxy[EnumText.enumForum_User_PageModerationErrorGet];
                ErrorLoad = Proxy[EnumText.enumForum_User_PageModerationErrorLoad];
                ErrorQuery = Proxy[EnumText.enumForum_User_PageModerationErrorQuery];
                ErrorModerteUser = Proxy[EnumText.enumForum_User_PageModerationErrorModerate];
                ErrorMessage = Proxy[EnumText.enumForum_User_PageModerationError];
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
            if (!this.IfError)
            {
                try
                {
                    Master.Page.Title = Proxy[EnumText.enumForum_User_TitleUserModeration];
                    ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumUserModeration);
                    this.Page.SetFocus(txtQuery);
                    Page.Form.DefaultButton = btnQuery1.UniqueID;
                    InitImageAlign();
                    if (!IsPostBack)
                    {
                        imgJoinedTime.Visible = true;
                        imgJoinedTime.ImageUrl = "~/images/sort_down.gif";                     
                        if (Request.QueryString["action"] != null)
                        {
                            CheckQueryString("id");
                            string action = Request.QueryString["action"];
                            ViewState["id"] = Request.QueryString["id"];

                            if (action == "Moderate")
                            {
                                Moderate();
                            }
                            else if (action == "Refuse")
                            {
                                Refuse();
                            }
                        }
                        ViewState["SortOrder"] = "JoinedTime";
                        ViewState["Direct"] = "desc";
                        ViewState["emailOrDisplayNameKeyWord"] = "";
                        if (Request.QueryString["pageindex"] != null && Request.QueryString["pagesize"] != null)
                        {
                            aspnetPager.PageIndex = Convert.ToInt32(Request.QueryString["pageindex"]);
                            aspnetPager.PageSize = Convert.ToInt32(Request.QueryString["pagesize"]);
                        }
                        this.RefreshData();
                    }
                }
                catch (Exception exp)
                {
                    
                    lblMessage.Text = ErrorLoad + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }

        #region Private Function Moderate
        private void Moderate()
        {
            try
            {
                int userId = Convert.ToInt32(ViewState["id"]);
                UserProcess.ApproveUserRegistration(SiteId, CurrentUserOrOperator.UserOrOperatorId, userId);
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorAccept + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion Private Function Moderate

        #region Private Function Refuse
        private void Refuse()
        {
            try
            {
                int userId = Convert.ToInt32(ViewState["id"]);
                UserProcess.RefuseUserRegistration(SiteId, CurrentUserOrOperator.UserOrOperatorId, userId);
            }
            catch (Exception exp)
            {

                lblMessage.Text = ErrorRefuse + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion Private Function Refuse

        #region Private Function InitImageAlign
        private void InitImageAlign()
        {
            //imgId.Attributes.Add("align", "absmiddle");
            imgEmail.Attributes.Add("align", "absmiddle");
            imgName.Attributes.Add("align", "absmiddle");
            imgJoinedTime.Attributes.Add("align", "absmiddle");
        }
        #endregion

        protected void RefreshData()
        {
            if (!this.IfError)
            {
                try
                {
                    string emailOrDisplayNameKeyword = ViewState["emailOrDisplayNameKeyWord"].ToString();
                    string orderField = (string)ViewState["SortOrder"] + " " + (string)ViewState["Direct"];
                    int recordsCount = UserProcess.GetCountOfNotModeratedUsersBySearch(this.SiteId, this.UserOrOperatorId, emailOrDisplayNameKeyword);
                    
                    if (recordsCount == 0)
                    {
                        repeaterModeration.DataSource = null;
                        repeaterModeration.DataBind();
                        aspnetPager.Visible = false;
                    }
                    else
                    {
                        Com.Comm100.Forum.Bussiness.User[] users = UserProcess.GetNotModeratedUsersByPaging(this.SiteId,
                            this.UserOrOperatorId, this.aspnetPager.PageIndex + 1, this.aspnetPager.PageSize, orderField, emailOrDisplayNameKeyword);
                        aspnetPager.CWCDataBind(repeaterModeration, users, recordsCount);
                        aspnetPager.Visible = true;
                    }
                    
                }
                catch (Exception exp)
                {
                    
                    lblMessage.Text = ErrorGetNotModeratedUser + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    this.aspnetPager.PageIndex = 0;                    
                    //ViewState["SortOrder"] = "JoinedTime";
                    //ViewState["Direct"] = "desc";

                    //imgEmail.Visible = false;
                    ////imgId.Visible = false;
                    //imgJoinedTime.Visible = false;
                    //imgName.Visible = false;

                    //imgJoinedTime.ImageUrl = "~/images/sort_down.gif";
                    //imgJoinedTime.Visible = true;
                    ViewState["emailOrDisplayNameKeyWord"] = txtQuery.Text;
                    RefreshData();
                }
                catch (Exception exp)
                {
                    
                    lblMessage.Text = ErrorQuery + exp.Message;
                    LogHelper.WriteExceptionLog(exp);

                    this.IfError = true;
                }
            }
        }
      
        protected void aspnetPager_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
        {
            if (!this.IfError)
            {
                try
                {
                    RefreshData();
                }
                catch (Exception exp)
                { 
                    lblMessage.Text = ErrorModerteUser + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }

        protected void aspnetPager_ChangePageSize(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.ChangePageSizeEventArgs ce)
        {
            if (!this.IfError)
            {
                try
                {
                    RefreshData();
                }
                catch (Exception exp)
                {
                    
                    lblMessage.Text = ErrorModerteUser + exp.Message;
                    LogHelper.WriteExceptionLog(exp);

                    this.IfError = true;
                }
            }
        }

        protected void repeaterModeration_sorting(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    LinkButton lbtnSender = sender as LinkButton;
                    string urlImgSort_up = "~/images/sort_up.gif";
                    string urlImgSort_down = "~/images/sort_down.gif";
                    imgEmail.Visible = false;
                    //imgId.Visible = false;
                    imgJoinedTime.Visible = false;
                    imgName.Visible = false;

                    #region switch
                    switch (lbtnSender.ID)
                    {
                        //case "lbtnUserId":
                        //    {
                        //        imgId.Visible = true;
                        //        ViewState["SortOrder"] = "ID";

                        //        if (ViewState["Direct"].ToString().Equals("desc"))
                        //        {
                        //            ViewState["Direct"] = "asc";

                        //            imgId.ImageUrl = urlImgSort_up;
                        //        }
                        //        else
                        //        {
                        //            ViewState["Direct"] = "desc";
                        //            imgId.ImageUrl = urlImgSort_down;
                        //        }
                        //        break;
                        //    }

                        case "lbtnUserEmail":
                            {
                                imgEmail.Visible = true;
                                ViewState["SortOrder"] = "Email";
                                if (ViewState["Direct"].ToString().Equals("desc"))
                                {
                                    ViewState["Direct"] = "asc";

                                    imgEmail.ImageUrl = urlImgSort_up;
                                }
                                else
                                {
                                    ViewState["Direct"] = "desc";
                                    imgEmail.ImageUrl = urlImgSort_down;
                                }
                                break;
                            }

                        case "lbtnUserDisplayName":
                            {
                                imgName.Visible = true;
                                ViewState["SortOrder"] = "Name";
                                if (ViewState["Direct"].ToString().Equals("desc"))
                                {
                                    ViewState["Direct"] = "asc";

                                    imgName.ImageUrl = urlImgSort_up;
                                }
                                else
                                {
                                    ViewState["Direct"] = "desc";
                                    imgName.ImageUrl = urlImgSort_down;
                                }
                                break;
                            }

                        case "lbtnJoinedTime":
                            {
                                imgJoinedTime.Visible = true;

                                if (ViewState["Direct"].ToString().Equals("desc"))
                                {
                                    ViewState["Direct"] = "asc";

                                    imgJoinedTime.ImageUrl = urlImgSort_up;
                                }
                                else
                                {
                                    ViewState["Direct"] = "desc";
                                    imgJoinedTime.ImageUrl = urlImgSort_down;
                                }
                                ViewState["SortOrder"] = "JoinedTime";
                                break;
                            }
                    }
                    #endregion
                    RefreshData();
                }
                catch (Exception exp)
                {
                    lblMessage.Text = ErrorMessage + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }

    }
}
