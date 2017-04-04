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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.UserGroups
{
    public partial class Administrators : AdminBasePage
    {
        string UserTypeAll;
        string UserTypeAdmin;
        string UserTypeOperator;
        string UserTypeRegisterUser;
        protected override void InitLanguage()
        {
            try
            {
                UserTypeAll = Proxy[EnumText.enumForum_User_UserTypeAll];
                UserTypeAdmin = Proxy[EnumText.enumForum_User_UserTypeAdmin];
                UserTypeOperator = Proxy[EnumText.enumForum_User_UserTypeOperator];
                UserTypeRegisterUser = Proxy[EnumText.enumForum_User_UserTypeRegisterUser];
                lblTitle.Text = Proxy[EnumText.enumForum_UserGroups_TitleAdministrators];
                lblSubTitle.Text = Proxy[EnumText.enumForum_UserGroups_SubtitleAdministrators];
                btnQuery1.Text = Proxy[EnumText.enumForum_UserGroups_ButtonQuery];
                btnQuery2.Text = Proxy[EnumText.enumForum_UserGroups_ButtonQuery];
                btnSelectTop.Text = Proxy[EnumText.enumForum_UserGroups_ButtonAddToAdministrators];
                btnSelectButtom.Text = Proxy[EnumText.enumForum_UserGroups_ButtonAddToAdministrators];
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumAdministrators);
                Master.Page.Title = Proxy[EnumText.enumForum_UserGroups_TitleAdministrators];
                InitImageAlign();
                if (!IsPostBack)
                {                  
                    if (Request.QueryString["action"] != null)
                    {
                        CheckQueryString("id");
                        string action = Request.QueryString["action"].ToLower();
                        if (action == "delete")
                        {
                            ViewState["Id"] = Request.QueryString["id"].ToString();
                            DeleteAdministrator();
                        }
                    }

                    ViewState["SortOrder"] = "LastLoginTime";
                    ViewState["Direct"] = "desc";
                    ViewState["EmailOrDisplayNameKeyWord"] = "";
                    ViewState["UserSortOrder"] = "Name";
                    ViewState["UserDirect"] = "asc";
#if OPENSOURCE
#else
                    InitDropDownList();
#endif

                    BindAdministrators();
                    BindUsersAndOperators();
                }
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_PageAdministratorsErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        #region Private Function InitImageAlign
        private void InitImageAlign()
        {
            //imgId.Attributes.Add("align", "absmiddle");
            imgEmail.Attributes.Add("align", "absmiddle");
            imgName.Attributes.Add("align", "absmiddle");
            imgJoinedTime.Attributes.Add("align", "absmiddle");
            imgLastLoginTime.Attributes.Add("align", "absmiddle");
            imgUserEmail.Attributes.Add("align", "absmiddle");
            imgUserDisplayName.Attributes.Add("align", "absmiddle");
            imgUserUserType.Attributes.Add("align", "absmiddle");
            imgUserJoinedTime.Attributes.Add("align", "absmiddle");
        }
        #endregion

        #region Private Function InitDropDownList
        void InitDropDownList()
        {
            trUserType.Visible = true;
            ddlUserType.Items.Add(new ListItem(UserTypeAll,"0"));
            ddlUserType.Items.Add(new ListItem(UserTypeAdmin,"1"));
            ddlUserType.Items.Add(new ListItem(UserTypeOperator,"2"));
            ddlUserType.Items.Add(new ListItem(UserTypeRegisterUser,"3"));
        }
        #endregion Private Function InitDropDownList

        #region Private Function DeleteAdministrator
        private void DeleteAdministrator()
        {
            try
            {
                int id = Convert.ToInt32(ViewState["Id"]);
                AdministratorProcess.DeleteUserOrOpratorFromAdministrators(SiteId, CurrentUserOrOperator.UserOrOperatorId, id);
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_PageAdministratorsErrorDelete] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }

        }
        #endregion Private Function DeleteAdministrator

        #region Sort
        protected void Sort(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                string commadName = lbtn.CommandName;
                imgEmail.Visible = false;
                imgName.Visible = false;
                //imgId.Visible = false;
                imgJoinedTime.Visible = false;
                imgLastLoginTime.Visible = false;
                switch (commadName)
                {
                    //case "Id":
                    //    imgId.Visible = true;
                    //    ViewState["SortOrder"] = "Id";
                    //    if (ViewState["Direct"].ToString().Equals("desc"))
                    //    {
                    //        ViewState["Direct"] = "asc";
                    //        imgId.ImageUrl = "~/images/sort_up.gif";
                    //    }
                    //    else
                    //    {
                    //        ViewState["Direct"] = "desc";
                    //        imgId.ImageUrl = "~/images/sort_down.gif";
                    //    }
                    //    break;
                    case "Email":
                        imgEmail.Visible = true;
                        ViewState["SortOrder"] = "Email";
                        if (ViewState["Direct"].ToString().Equals("desc"))
                        {
                            ViewState["Direct"] = "asc";
                            imgEmail.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState["Direct"] = "desc";
                            imgEmail.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                    case "Name":
                        imgName.Visible = true;
                        ViewState["SortOrder"] = "Name";
                        if (ViewState["Direct"].ToString().Equals("desc"))
                        {
                            ViewState["Direct"] = "asc";
                            imgName.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState["Direct"] = "desc";
                            imgName.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                    case "JoinedTime":
                        imgJoinedTime.Visible = true;
                        ViewState["SortOrder"] = "JoinedTime";
                        if (ViewState["Direct"].ToString().Equals("desc"))
                        {
                            ViewState["Direct"] = "asc";
                            imgJoinedTime.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState["Direct"] = "desc";
                            imgJoinedTime.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                    case "LastLoginTime":
                        imgLastLoginTime.Visible = true;
                        ViewState["SortOrder"] = "LastLoginTime";
                        if (ViewState["Direct"].ToString().Equals("desc"))
                        {
                            ViewState["Direct"] = "asc";
                            imgLastLoginTime.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState["Direct"] = "desc";
                            imgLastLoginTime.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                }
                BindAdministrators();
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_ErrorSort] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion Sort

        #region Select User Sort
        protected void UserSort(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                string commadName = lbtn.CommandName;
                imgUserEmail.Visible = false;
                imgUserDisplayName.Visible = false;
                imgUserUserType.Visible = false;
                imgUserJoinedTime.Visible = false;
                switch (commadName)
                {
                    case "UserEmail":
                        imgUserEmail.Visible = true;
                        ViewState["UserSortOrder"] = "Email";
                        if (ViewState["UserDirect"].ToString().Equals("desc"))
                        {
                            ViewState["UserDirect"] = "asc";
                            imgUserEmail.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState["UserDirect"] = "desc";
                            imgUserEmail.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                    case "UserDisplayName":
                        imgUserDisplayName.Visible = true;
                        ViewState["UserSortOrder"] = "Name";
                        if (ViewState["UserDirect"].ToString().Equals("desc"))
                        {
                            ViewState["UserDirect"] = "asc";
                            imgUserDisplayName.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState["UserDirect"] = "desc";
                            imgUserDisplayName.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                    case "UserJoinedTime":
                        imgUserJoinedTime.Visible = true;
                        ViewState["UserSortOrder"] = "JoinedTime";
                        if (ViewState["UserDirect"].ToString().Equals("desc"))
                        {
                            ViewState["UserDirect"] = "asc";
                            imgUserJoinedTime.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState["UserDirect"] = "desc";
                            imgUserJoinedTime.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                    case "UserUserType":
                        imgUserUserType.Visible = true;
                        ViewState["UserSortOrder"] = "UserType";
                        if (ViewState["UserDirect"].ToString().Equals("desc"))
                        {
                            ViewState["UserDirect"] = "asc";
                            imgUserUserType.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState["UserDirect"] = "desc";
                            imgUserUserType.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                }
                BindUsersAndOperators();
                ScriptManager.RegisterStartupScript(divScript, typeof(string), "showUser", "javascript:showWindow('divThickInner', 'divThickOuter');", true);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_ErrorSort] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion Sort

        #region BindAdministrators
        protected void BindAdministrators()
        {
            string sortField = ViewState["SortOrder"].ToString();
            string sortDirect = ViewState["Direct"].ToString();

            rpAdministrators.DataSource = AdministratorProcess.GetAllAdministrators(this.SiteId, sortField, sortDirect, this.CurrentUserOrOperator.UserOrOperatorId);
            rpAdministrators.DataBind();
        }
        #endregion

        #region Private Function BindUsersAndOperators
        void BindUsersAndOperators()
        {
            string emailOrDisplayNameKeyword = ViewState["EmailOrDisplayNameKeyWord"].ToString();
#if OPENSOURCE
            bool ifGetAll = true;
#else
            bool ifGetAll = false;
#endif
            bool IfGetAdmin = false;
            EnumUserType userType = EnumUserType.Operator;
            if (ddlUserType.SelectedValue == "0") ifGetAll = true;
            else if (ddlUserType.SelectedValue == "1") IfGetAdmin = true;
            else if (ddlUserType.SelectedValue == "2") userType = EnumUserType.Operator;
            else if (ddlUserType.SelectedValue == "3") userType = EnumUserType.User;

            string sortField = ViewState["UserSortOrder"].ToString();
            string sortDirection = ViewState["UserDirect"].ToString();

            int recordsCount;
            UserOrOperator[] userOrOperators = AdministratorProcess.GetNotDeletedAndNotBannedUsersOrOperatorsWhichisNotAdministratorByQueryAndPaging(SiteId, CurrentUserOrOperator.UserOrOperatorId, aspnetPager.PageIndex + 1, aspnetPager.PageSize, emailOrDisplayNameKeyword, userType, ifGetAll, IfGetAdmin, out recordsCount, sortField, sortDirection);
            
            if (recordsCount == 0)
            {
                aspnetPager.Visible = false;
                rpUser.DataSource = null;
                rpUser.DataBind();
            }
            else
            {
                aspnetPager.CWCDataBind(rpUser, userOrOperators, recordsCount);
                aspnetPager.Visible = true;
            }
        }
        #endregion Private Function BindUsersAndOperators

        #region Protected Function GetItemType
        protected string GetItemType(UserOrOperator userOrOperator)
        {
            if (!CommFun.IfOperator(userOrOperator)) 
                return UserTypeRegisterUser;
            else
            {
                OperatorWithPermissionCheck operatorTmp = userOrOperator as OperatorWithPermissionCheck;
                if (operatorTmp.IfAdmin) return UserTypeAdmin;
                else return UserTypeOperator;
            }
        }
        #endregion Protected Function GetItemType

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                foreach (RepeaterItem r in rpUser.Items)
                {
                    System.Web.UI.HtmlControls.HtmlInputCheckBox chbUser = r.FindControl("chbSelSingle") as HtmlInputCheckBox;

                    if (chbUser.Checked)
                    {
                        count++;
                        int id = int.Parse(chbUser.Value);
                        AdministratorProcess.AddUserOrOperatorToAdministrator(SiteId, CurrentUserOrOperator.UserOrOperatorId, id);

                    }
                }
                if (count == 0)
                    throw new Exception(Proxy[EnumText.enumForum_UserGroups_ErrorAddingAdministrators]);
                else
                {
                    BindAdministrators();
                    BindUsersAndOperators();
                    lblSuccess.Text = Proxy[EnumText.enumForum_UserGroups_PageAdministratorsSuccessAdd];
                }
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_PageAdministratorsErrorAdd] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.aspnetPager.PageIndex = 0;
                ViewState["EmailOrDisplayNameKeyWord"] = txtKeyWord.Text;
                this.BindUsersAndOperators();
                ScriptManager.RegisterStartupScript(divScript, typeof(string), "showUser", "javascript:showWindow('divThickInner', 'divThickOuter');", true);
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_PageAdministratorsErrorQuery] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        

        protected void aspnetPager_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
        {
            try
            {
                BindUsersAndOperators();
                ScriptManager.RegisterStartupScript(divScript, typeof(string), "showUser", "javascript:showWindow('divThickInner', 'divThickOuter');", true);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_User_PageUserManagementListErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void aspnetPager_ChangePageSize(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.ChangePageSizeEventArgs ce)
        {
            try
            {
                BindUsersAndOperators();
                ScriptManager.RegisterStartupScript(divScript, typeof(string), "showUser", "javascript:showWindow('divThickInner', 'divThickOuter');", true);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_User_PageUserManagementListErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        
    }
}
