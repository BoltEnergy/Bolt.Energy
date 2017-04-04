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
using Com.Comm100.Forum.Language;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.WebControls;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;

namespace Com.Comm100.Forum.UI.UserPanel
{
    public partial class SelectUsers : UIBasePage
    {

        public bool ifSingle { get;set; }

        #region Language;
        string ErrorLoad;
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
                ErrorLoad = Proxy[EnumText.enumForum_User_SelectUserLoadUsersError];
                this.btnQuery.Text = Proxy[EnumText.enumForum_Public_ButtonQuery];
                this.btnSelect.Text = Proxy[EnumText.enumForum_Public_ButtonOk];
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError];
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IfError)
            {
                try
                {
                    CheckQueryString("ifSingle");
                    this.ifSingle = Convert.ToBoolean(Request.QueryString["ifSingle"]);
                    if (!IsPostBack)
                    {
                        ViewState["UserSortOrder"] = "Name";
                        ViewState["UserDirect"] = "asc";
#if OPENSOURCE
#else
                        InitUserTypeDropDownList();
#endif
                        RefreshData();
                    }
                }
                catch (Exception exp)
                {
                    IfError = true;
                    lblMessage.Text = ErrorLoad + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
        }
        private void InitUserTypeDropDownList()
        {
            this.ddlUserType.Items.Add(new ListItem(UserTypeAll,"0"));
            this.ddlUserType.Items.Add(new ListItem(UserTypeAdmin,"1"));
            this.ddlUserType.Items.Add(new ListItem(UserTypeOperator,"2"));
            this.ddlUserType.Items.Add(new ListItem(UserTypeRegisterUser, "3"));
            this.ddlUserType.SelectedIndex = 0;
        }

        protected void aspnetPager_Paging(object sender, ASPNetPager.PagingEventArgs pe)
        {
            if (!IfError)
            {
                try
                {
                    RefreshData();
                }
                catch (Exception exp)
                {
                    IfError = true;
                    lblMessage.Text = ErrorLoad + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
        }

        protected void aspnetPager_ChangePageSize(object sender, ASPNetPager.ChangePageSizeEventArgs ce)
        {
            if (!IfError)
            {
                try
                {
                    RefreshData();
                }
                catch (Exception exp)
                {
                    IfError = true;
                    lblMessage.Text = ErrorLoad + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
        }

        private void RefreshData()
        {
            string displayNameKeyword = this.tbDisplayName.Value;
#if OPENSOURCE
            bool ifGetAll = true;
#else
            bool ifGetAll = false;
#endif
            bool IfGetAdmin = false;
            EnumUserType userType = EnumUserType.Operator;
            if (ddlUserType.SelectedIndex == 0) ifGetAll = true;
            else if (ddlUserType.SelectedIndex == 1) IfGetAdmin = true;
            else if (ddlUserType.SelectedIndex == 2) userType = EnumUserType.Operator;
            else if (ddlUserType.SelectedIndex == 3) userType = EnumUserType.User;

            int recordsCount;
            string sortField = ViewState["UserSortOrder"].ToString();
            string sortDirection = ViewState["UserDirect"].ToString();
            UserOrOperator[] userOrOperators = UserProcess.GetNotDeletedAndNotBannedUserOrOperatorByQueryNameAndPaging(this.SiteId, this.UserOrOperatorId,
                this.aspnetPager1.PageIndex + 1, this.aspnetPager1.PageSize, displayNameKeyword, userType, ifGetAll, IfGetAdmin, out recordsCount, sortField, sortDirection);

            if (recordsCount == 0)
            {
                aspnetPager1.Visible = false;
                rptUsers.DataSource = null;
                rptUsers.DataBind();
            }
            else
            {
                aspnetPager1.CWCDataBind(rptUsers, userOrOperators, recordsCount);
                aspnetPager1.Visible = true;
            }
        }

        protected string GetItemType(UserOrOperator userOrOperator)
        {
            if (!CommFun.IfOperator(userOrOperator)) return UserTypeRegisterUser;
            else
            {
                OperatorWithPermissionCheck operatorTmp = userOrOperator as OperatorWithPermissionCheck;
                if (operatorTmp.IfAdmin) return UserTypeAdmin;
                else return UserTypeOperator;
            }
        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (!IfError)
            {
                try
                {
                    RefreshData();
                }
                catch (Exception exp)
                {
                    IfError = true;
                    lblMessage.Text = ErrorLoad + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
        }

        public string GetInputHtml(int userId,string name)
        {
            if (ifSingle)
                return string.Format("<input type='radio' name='singleUser' onclick=\"CheckSigleUser('{0}','{1}');\" />", userId, name);
            else
                return string.Format("<input type='checkbox' id='u_{0}' onclick=\"CheckUser('{0}');\"/>",userId);
            
        }

        #region Select User Sort
        protected void UserSort(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                string commadName = lbtn.CommandName;
                imgUserDisplayName.Visible = false;
                imgUserUserType.Visible = false;
                imgUserJoinedTime.Visible = false;
                switch (commadName)
                {                    
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
                RefreshData();
                //ScriptManager.RegisterStartupScript(divScript, typeof(string), "showUser", "javascript:showWindow('divThickInner', 'divThickOuter');", true);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_ErrorSort] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion Sort
    }
}
