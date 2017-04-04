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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Language;
using System.Web.UI.HtmlControls;
using Com.Comm100.Framework.Enum;

namespace Com.Comm100.Forum.UI.AdminPanel.UserGroups
{
    public partial class MemberList : AdminBasePage
    {


        public int UserGroupId
        {
            get { return Convert.ToInt32(ViewState["groupId"]); }
        }

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
                lblTitle.Text = Proxy[EnumText.enumForum_UserGroups_TitleMembersManagement];
                lblSubTitle.Text = Proxy[EnumText.enumForum_UserGroups_SubtitleMembersManagement];
                btnQuery1.Text = Proxy[EnumText.enumForum_UserGroups_ButtonQuery];
                btnQuery2.Text = Proxy[EnumText.enumForum_UserGroups_ButtonQuery];
                btnQueryUser1.Text = Proxy[EnumText.enumForum_UserGroups_ButtonQuery];
                btnQueryUser2.Text = Proxy[EnumText.enumForum_UserGroups_ButtonQuery];              
                btnSelectTop.Text = Proxy[EnumText.enumForum_UserGroups_ButtonAdd];
                btnSelectButtom.Text = Proxy[EnumText.enumForum_UserGroups_ButtonAdd];
                btnCancel.Text = Proxy[EnumText.enumForum_UserGroups_ButtonCancel];
                btnCancel1.Text = Proxy[EnumText.enumForum_UserGroups_ButtonCancel];
                lblCurrent.Text = Proxy[EnumText.enumForum_UserGroups_FieldCurrentUserGroup];
                lblEmailOrDisplayName.Text = Proxy[EnumText.enumForum_UserGroups_FieldEmailOrDisplayName];
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
                CheckQueryString("groupId");
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumUserGroups);
                Master.Page.Title = Proxy[EnumText.enumForum_UserGroups_TitleMembersManagement];
                CheckGroupPermissionFunction();
                if (!IsPostBack)
                {
                    ViewState["groupId"] = Request.QueryString["groupId"];

                    InitImageAlign();

                    if (Request.QueryString["action"] != null)
                    {
                        CheckQueryString("id");
                        string action = Request.QueryString["action"].ToLower();
                        if (action == "delete")
                        {
                            ViewState["Id"] = Request.QueryString["Id"];
                            DeleteMember();
                        }
                    }

                    InitUserGroupInfo();

                    ViewState["SortOrder"] = "LastLoginTime";
                    ViewState["Direct"] = "desc";
                    ViewState["EmailOrDisplayNameKeyWord"] = "";
                    ViewState["UserOrOperatorKeyWord"] = "";
                    ViewState["UserSortOrder"] = "Name";
                    ViewState["UserDirect"] = "asc";

                    if (Request.QueryString["pageindex"] != null && Request.QueryString["pagesize"] != null)
                    {
                        aspnetPager.PageIndex = Convert.ToInt32(Request.QueryString["pageindex"]);
                        aspnetPager.PageSize = Convert.ToInt32(Request.QueryString["pagesize"]);
                    }

#if OPENSOURCE
#else
                    InitDropDownList();
#endif
                    this.RefreshData();
                    this.BindUser();
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_PageMembersManagementErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void CheckGroupPermissionFunction()
        {
            ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(this.SiteId, this.UserOrOperatorId);
            if (!forumFeature.IfEnableGroupPermission)
                ExceptionHelper.ThrowForumSettingsCloseGroupPermissionFunction();
        }

        #region Private Function InitImageAlign
        private void InitImageAlign()
        {
            //imgId.Attributes.Add("align", "absmiddle");
            imgEmail.Attributes.Add("align", "absmiddle");
            imgName.Attributes.Add("align", "absmiddle");
            imgJoinedTime.Attributes.Add("align", "absmiddle");
            imgLastLoginTime.Attributes.Add("align", "absmiddle");
        }
        #endregion

        #region Private Function DeleteMember
        private void DeleteMember()
        {
            try
            {
                int userGroupId = Convert.ToInt32(ViewState["groupId"]);
                int userOrOperatorId = Convert.ToInt32(ViewState["Id"]);
                UserGroupProcess.DeleteUserOrOperatorFromUserGroup(SiteId, CurrentUserOrOperator.UserOrOperatorId, userGroupId, userOrOperatorId);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_PageMembersManagementErrorDelete] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion Private Function DeleteMember

        #region Private Function InitUserGroupInfo
        private void InitUserGroupInfo()
        {
            int id = Convert.ToInt32(ViewState["groupId"]);
            UserGroupWithPermissionCheck userGroup = UserGroupProcess.GetGroupById(SiteId, CurrentUserOrOperator.UserOrOperatorId, id);
            lblCurrentUserGroup.Text = System.Web.HttpUtility.HtmlEncode(userGroup.Name);
        }
        #endregion Private Function InitUserGroupInfo

        #region Sort
        protected void Sort(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;
            imgEmail.Visible = false;
            imgName.Visible = false;
            //imgId.Visible = false;
            imgJoinedTime.Visible = false;
            imgLastLoginTime.Visible = false;
            switch (lbtn.ID)
            {
                /*
                case "lbtnId":
                    {
                        imgId.Visible = true;
                        ViewState["SortOrder"] = "Id";

                        if (ViewState["Direct"].ToString().Equals("desc"))
                        {
                            ViewState["Direct"] = "asc";
                            imgId.Visible = true;
                            imgId.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState["Direct"] = "desc";
                            imgId.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                    }
                * */

                case "lbtnEmail":
                    {
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
                    }

                case "lbtnDisplayName":
                    {
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
                    }

                case "lbtnJoinedTime":
                    {
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
                    }
                case "lbtnLastLoginTime":
                    {
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
            }
            RefreshData();
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
                BindUser();
                ScriptManager.RegisterStartupScript(divScript, typeof(string), "showUser", "javascript:showWindow('divThickInner', 'divThickOuter');", true);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_ErrorSort] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion Sort

        #region RefreshData
        private void RefreshData()
        {
            try
            {
                int recordsCount = 0;
                int groupId = Convert.ToInt32(ViewState["groupId"]);
                string emailOrdisplayNameKeyWord = ViewState["EmailOrDisplayNameKeyWord"].ToString();
                string orderField = (string)ViewState["SortOrder"] + " " + (string)ViewState["Direct"];
                MemberOfUserGroupWithPermissionCheck[] members = UserGroupProcess.GetMembersOfUserGroupByQueryAndPaging(SiteId, CurrentUserOrOperator.UserOrOperatorId, groupId, this.aspnetPager.PageIndex + 1, this.aspnetPager.PageSize, emailOrdisplayNameKeyWord, orderField, out recordsCount);
                if (recordsCount == 0)
                {
                    aspnetPager.Visible = false;
                    rpMembers.DataSource = null;
                    rpMembers.DataBind();
                }
                else
                {
                    aspnetPager.CWCDataBind(rpMembers, members, recordsCount);
                    aspnetPager.Visible = true;
                }
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_PageMembersManagementErrorGet] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion RefreshData

        #region Paging
        protected void aspnetPager_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
        {
            try
            {
                RefreshData();
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
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_User_PageUserManagementListErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion Paging

        #region Query
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.aspnetPager.PageIndex = 0;
                ViewState["EmailOrDisplayNameKeyWord"] = txtQuery.Text;
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_PageMembersManagementErrorQuery] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion Query

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("UserGroup.aspx?siteid=" + SiteId, false);
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_RedirectError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }


        #region Protected Function GetItemType
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
        #endregion Protected Function GetItemType

        #region Private Function InitDropDownList
        void InitDropDownList()
        {
            trUserType.Visible = true;
            ddlUserType.Items.Add(UserTypeAll);
            ddlUserType.Items.Add(UserTypeAdmin);
            ddlUserType.Items.Add(UserTypeOperator);
            ddlUserType.Items.Add(UserTypeRegisterUser);
        }
        #endregion Private Function InitDropDownList

        #region Private Function BindUser
        public void BindUser()
        {
            string emailOrDisplayNameKeyword = ViewState["UserOrOperatorKeyWord"].ToString();
            int userGroupId = Convert.ToInt32(ViewState["groupId"]);
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

            string sortField = ViewState["UserSortOrder"].ToString();
            string sortDirection = ViewState["UserDirect"].ToString();
            int recordsCount;
            UserOrOperator[] userOrOperators = UserGroupProcess.GetNotDeletedAndNotBannedUsersOrOperatorsWhichisNotInUserGroupByQueryAndPaging(SiteId, CurrentUserOrOperator.UserOrOperatorId, userGroupId, aspnetPager1.PageIndex + 1, aspnetPager1.PageSize, emailOrDisplayNameKeyword, userType, ifGetAll, IfGetAdmin, out recordsCount, sortField, sortDirection);

            if (recordsCount == 0)
            {
                aspnetPager1.Visible = false;
                rpUser.DataSource = null;
                rpUser.DataBind();
            }
            else
            {
                aspnetPager1.CWCDataBind(rpUser, userOrOperators, recordsCount);
                aspnetPager1.Visible = true;
            }
        }
        #endregion Private Function BindUser

        #region Private Function Query User
        protected void btnQueryUser_Click(object sender, EventArgs e)
        {
            try
            {
                this.aspnetPager1.PageIndex = 0;
                ViewState["UserOrOperatorKeyWord"] = txtKeyWord.Text;
                this.BindUser();
                ScriptManager.RegisterStartupScript(divScript, typeof(string), "showUser", "javascript:showWindow('divThickInner', 'divThickOuter');", true);
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_PageMembersManagementErrorQuery] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion Private Function Query User

        #region Paging
        protected void aspnetPager1_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
        {
            try
            {
                BindUser();
                ScriptManager.RegisterStartupScript(divScript, typeof(string), "showUser", "javascript:showWindow('divThickInner', 'divThickOuter');", true);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_User_PageUserManagementListErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void aspnetPager1_ChangePageSize(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.ChangePageSizeEventArgs ce)
        {
            try
            {
                BindUser();
                ScriptManager.RegisterStartupScript(divScript, typeof(string), "showUser", "javascript:showWindow('divThickInner', 'divThickOuter');", true);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_User_PageUserManagementListErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion Paging

        #region Add Members
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                int userGroupId = Convert.ToInt32(ViewState["groupId"]);
                foreach (RepeaterItem r in rpUser.Items)
                {
                    System.Web.UI.HtmlControls.HtmlInputCheckBox chbUser = r.FindControl("chbSelSingle") as HtmlInputCheckBox;

                    if (chbUser.Checked)
                    {
                        int id = int.Parse(chbUser.Value);
                        UserGroupProcess.AddUserOrOperatorToUserGroup(SiteId, CurrentUserOrOperator.UserOrOperatorId, userGroupId, id);
                    }
                }
                this.RefreshData();
                this.BindUser();
                lblSuccess.Text = Proxy[EnumText.enumForum_UserGroups_PageMembersManagementSuccessAdd];
                Response.Redirect("memberlist.aspx?siteid=" + SiteId.ToString() + "&groupId=" + userGroupId.ToString(), false);
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_PageMembersManagementErrorAdd] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion Add Members

        
    }
}
