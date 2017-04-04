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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.WebControls;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Forum.Process;

namespace Com.Comm100.Forum.UI.AdminPanel.TopicAndPost
{
    public partial class SelectUser : AdminBasePage
    {
        #region Const
        private const string VSKEY_OrderField = "OrderField";
        private const string VSKEY_OrderDirection = "OrderDirection";
        #endregion 

        #region Language
        string ErrorLoad;
        string UserTypeAll;
        string UserTypeAdmin;
        string UserTypeOperator;
        string UserTypeRegisterUser;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_User_SelectUserLoadUsersError];
                UserTypeAll = Proxy[EnumText.enumForum_User_UserTypeAll];
                UserTypeAdmin = Proxy[EnumText.enumForum_User_UserTypeAdmin];
                UserTypeOperator = Proxy[EnumText.enumForum_User_UserTypeOperator];
                UserTypeRegisterUser = Proxy[EnumText.enumForum_User_UserTypeRegisterUser];
                this.lblTitle.Text = Proxy[EnumText.enumForum_User_SelectUserTitle];
                this.btnQuery1.Text = Proxy[EnumText.enumForum_Public_ButtonQuery];
                this.btnQuery2.Text = Proxy[EnumText.enumForum_Public_ButtonQuery];
                this.btnSelect1.Text = Proxy[EnumText.enumForum_User_SelectUserButtonSubmit];
                this.btnSelect2.Text = Proxy[EnumText.enumForum_User_SelectUserButtonSubmit];

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
                if (!IsPostBack)
                {
                    ViewState[VSKEY_OrderField] = "JoinedTime";
                    ViewState[VSKEY_OrderDirection] = "desc";
                    this.imgJoinedTime.Visible = true;
                    this.imgJoinedTime.ImageUrl = "~/images/sort_down.gif";

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
                lblMessage.Text = ErrorLoad+ exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void InitUserTypeDropDownList(){
            this.ddlUserType.Items.Add(new ListItem(UserTypeAll,"0"));
            this.ddlUserType.Items.Add(new ListItem(UserTypeAdmin,"1"));
            this.ddlUserType.Items.Add(new ListItem(UserTypeOperator,"2"));
            this.ddlUserType.Items.Add(new ListItem(UserTypeRegisterUser,"3"));
            this.ddlUserType.SelectedValue = UserTypeAll;
        }

        protected void aspnetPager_Paging(object sender, ASPNetPager.PagingEventArgs pe)
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

        protected void aspnetPager_ChangePageSize(object sender, ASPNetPager.ChangePageSizeEventArgs ce)
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

        protected void btnSort_click(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;
            imgUserType.Visible = false;
            imgJoinedTime.Visible = false;
            switch (lbtn.ID)
            {
                case "lbtnUserType":
                    {
                        imgUserType.Visible = true;
                        ViewState[VSKEY_OrderField] = "UserType";
                        if (ViewState[VSKEY_OrderDirection].ToString().Equals("desc"))
                        {
                            ViewState[VSKEY_OrderDirection] = "asc";
                            imgUserType.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState[VSKEY_OrderDirection] = "desc";
                            imgUserType.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                    }

                case "lbtnJoinedTime":
                    {
                        imgJoinedTime.Visible = true;
                        ViewState[VSKEY_OrderField] = "JoinedTime";
                        if (ViewState[VSKEY_OrderDirection].ToString().Equals("desc"))
                        {
                            ViewState[VSKEY_OrderDirection] = "asc";

                            imgJoinedTime.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState[VSKEY_OrderDirection] = "desc";
                            imgJoinedTime.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                    }
            }
            RefreshData();
        }

        private void RefreshData()
        {
            string emailOrDisplayNameKeyword = this.tbDisplayName.Value;
            string orderField = ViewState["OrderField"] as string;
            string orderDirection = ViewState["OrderDirection"] as string;
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

            int recordsCount;
            UserOrOperator[] userOrOperators = UserProcess.GetUserOrOperatorByQueryAndPaging(SiteId, UserOrOperatorId,
                    this.aspnetPager1.PageIndex + 1, this.aspnetPager1.PageSize, emailOrDisplayNameKeyword, userType, ifGetAll,
                    IfGetAdmin, orderField, orderDirection, out recordsCount);
                    
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
                    this.aspnetPager1.PageIndex = 0;
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
    }
}
