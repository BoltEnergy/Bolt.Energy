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
using Com.Comm100.Forum.UI.AdminPanel;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.HelpDocument;
using Com.Comm100.Forum.Language;
using Com.Comm100.Framework.Enum;
using System.Web.UI.HtmlControls;

namespace Forum.UI.AdminPanel.Forums
{
    public partial class ForumAdd : AdminBasePage
    {
        public List<int> SelectedUserIds
        {
            get
            {
                if (ViewState["SelectedUserIds"] != null)
                    return ViewState["SelectedUserIds"] as List<int>;
                else
                    return null;
            }
            set
            {
                ViewState["SelectedUserIds"] = value;
            }
        }

        #region Language
        string ErrorLoad;
        string ErrorSave;
        string ErrorRedirect;
        string UserTypeAll;
        string UserTypeRegisterUser;
        string UserTypeAdmin;
        string UserTypeOperator;
        string ErrorAddUserToModerator;
        string ErrorLoadUsers;
        string ModeratorRequired;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_ForumAdd_LoadError];
                ErrorSave = Proxy[EnumText.enumForum_Forums_PageNewErrorCreateNew];
                ErrorRedirect = Proxy[EnumText.enumForum_Public_RedirectError];
                UserTypeAll = Proxy[EnumText.enumForum_User_UserTypeAll];
                UserTypeRegisterUser = Proxy[EnumText.enumForum_User_UserTypeRegisterUser];
                UserTypeAdmin = Proxy[EnumText.enumForum_User_UserTypeAdmin];
                UserTypeOperator = Proxy[EnumText.enumForum_User_UserTypeOperator];
                ErrorAddUserToModerator = Proxy[EnumText.enumForum_ForumAdd_AddModeratorError];
                ErrorLoadUsers = Proxy[EnumText.enumForum_User_SelectUserLoadUsersError];
                ModeratorRequired = Proxy[EnumText.enumForum_Forums_ErrorModeratorRequired];
                Master.Page.Title = Proxy[EnumText.enumForum_Forums_TitleNew];
                lblTitle.Text = Proxy[EnumText.enumForum_Forums_TitleNew];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Forums_SubtitleNew];
                btnSave1.Text = Proxy[EnumText.enumForum_Forums_ButtonSave];
                btnSave2.Text = Proxy[EnumText.enumForum_Forums_ButtonSave];
                btnCancel1.Text = Proxy[EnumText.enumForum_Forums_ButtonCancel];
                btnCancel2.Text = Proxy[EnumText.enumForum_Forums_ButtonCancel];
                btnSelectTop.Text = Proxy[EnumText.enumForum_User_SelectUserButtonSubmit];
                btnSelectButtom.Text = Proxy[EnumText.enumForum_User_SelectUserButtonSubmit];
                btnQueryUser1.Text = Proxy[EnumText.enumForum_Public_ButtonQuery];
                btnQueryUser2.Text = Proxy[EnumText.enumForum_Public_ButtonQuery];
                btnAddModerator.Text = Proxy[EnumText.enumForum_Forum_ButtonAddModerator];
                btnRemoveModerator.Text = Proxy[EnumText.enumForum_Forum_ButtonRemoveModerator];
                checkBoxReply.Text = Proxy[EnumText.enumForum_Forum_CheckBoxNeedReplyToView];
                checkBoxPay.Text = Proxy[EnumText.enumForum_Forum_CheckBoxNeedPayScoreToView];
                this.cCategory.ErrorMessage=Proxy[EnumText.enumForum_Forums_ErrorCategoryRequired];
                this.rfvName.ErrorMessage = Proxy[EnumText.enumForum_Forums_ErrorNameRequired];

                this.lblSelectUserTitle.Text = Proxy[EnumText.enumForum_ForumAdd_AddModeratorSelectUserTitle];
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError]+ ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }
        #endregion 

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumForumManage);

                txtName.Focus();

                if (!IsPostBack)
                {
                    this.SelectedUserIds = new List<int>();
                    txtName.MaxLength = ForumDBFieldLength.Forum_nameFieldLength;
                    txtDescription.MaxLength = ForumDBFieldLength.Forum_descriptionFieldLength;

                    this.txtDescription.Attributes.Add("onkeyup", "javascript:checkMaxLength(this," + ForumDBFieldLength.Category_descriptionFieldLength + string.Format(Proxy[EnumText.enumForum_Public_TextAreaMaxLength], "this.value.length", ForumDBFieldLength.Category_descriptionFieldLength));

                    InitFormData();
                    InitDropDownList();
                    ViewState["UserOrOperatorKeyWord"] = "";
                    ViewState["UserSortOrder"] = "Name";
                    ViewState["UserDirect"] = "asc";
                    BindUser();
                }
                else
                {
                    List<int> selectUserIds = this.SelectedUserIds;
                    foreach (RepeaterItem r in rpUser.Items)
                    {
                        HtmlInputCheckBox chbUser = r.FindControl("checkBoxUser") as HtmlInputCheckBox;
                        int userId = int.Parse(chbUser.Value);
                        if (chbUser.Checked)
                        {
                            if(!selectUserIds.Contains(userId))
                                selectUserIds.Add(userId);
                        }
                        else
                            selectUserIds.Remove(userId);
                    }
                    this.SelectedUserIds = selectUserIds;
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);

            }
        }
        protected void InitFormData()
        {
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataSource = CategoryProcess.GetAllCategories(CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.IfOperator, CurrentUserOrOperator.SiteId);
            ddlCategory.DataBind();
        }

        private int GetSelectedCategoryId()
        {

            CategoryWithPermissionCheck category = CategoryProcess.GetCategoryById(UserOrOperatorId, IfOperator, SiteId, Convert.ToInt32(ddlCategory.SelectedValue));
            return category.CategoryId;

        }

        private int[] GetModeratorIds()
        {
            int[] moderators = new int[this.listboxModerator.Items.Count];
            for (int i = 0; i < listboxModerator.Items.Count; i++)
            {
                moderators[i] = Convert.ToInt32(listboxModerator.Items[i].Value);
            }
            return moderators;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string description = txtDescription.Text;
            bool ifAllowPostNeedingReplayTopic = this.checkBoxReply.Checked;
            bool ifAllowPostNeedingPayTopic = this.checkBoxPay.Checked;

            try
            {
                if (listboxModerator.Items.Count == 0)
                    lblModeratorReuired.Text = ModeratorRequired;
                else
                {

                    int categoryId = GetSelectedCategoryId();
                    int[] moderatorIds = GetModeratorIds();

                    int forumId = ForumProcess.AddForum(CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.SiteId, name, description, categoryId, moderatorIds, ifAllowPostNeedingReplayTopic, ifAllowPostNeedingPayTopic);

                    Response.Redirect("ForumList.aspx?siteid=" + SiteId, false);
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorSave + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("ForumList.aspx?siteid=" + SiteId, false);
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorRedirect + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnRemoveModerator_Click(object sender, EventArgs e)
        {
            int[] index = this.listboxModerator.GetSelectedIndices();
            ListItem[] items = new ListItem[index.Length];
            for (int i = 0; i < index.Length; i++)
                items[i] = listboxModerator.Items[index[i]];
            for (int i = 0; i < index.Length; i++)
                this.listboxModerator.Items.Remove(items[i]);
        }

        #region Select User
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
        void InitDropDownList()
        {
            trUserType.Visible = true;
            ddlUserType.Items.Add(UserTypeAll);
            ddlUserType.Items.Add(UserTypeAdmin);
            ddlUserType.Items.Add(UserTypeOperator);
            ddlUserType.Items.Add(UserTypeRegisterUser);
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                //int userGroupId = Convert.ToInt32(ViewState["groupId"]);
                //foreach (RepeaterItem r in rpUser.Items)
                //{
                //    HtmlInputCheckBox chbUser = r.FindControl("checkBoxUser") as HtmlInputCheckBox;

                //    if (chbUser.Checked)
                //    {
                //        int id = int.Parse(chbUser.Value);
                //        UserOrOperator user=UserProcess.GetUserOrOpertorById(this.SiteId, id);
                //        ListItem itemUser=new ListItem(user.DisplayName,id.ToString());
                //        bool exist=false;
                //        for (int i = 0; i < listboxModerator.Items.Count; i++)
                //        {
                //            if (itemUser.Value == listboxModerator.Items[i].Value)
                //            {
                //                exist = true;
                //                break;
                //            }
                //        } 
                //        if(exist==false)
                //            this.listboxModerator.Items.Add(itemUser);
                //    }
                //}
                int userGroupId = Convert.ToInt32(ViewState["groupId"]);
                foreach (int userId in this.SelectedUserIds)
                {
                    int id = userId;
                    UserOrOperator user = UserProcess.GetUserOrOpertorById(this.SiteId, id);
                    ListItem itemUser = new ListItem(user.DisplayName, id.ToString());
                    bool exist = false;
                    for (int i = 0; i < listboxModerator.Items.Count; i++)
                    {
                        if (itemUser.Value == listboxModerator.Items[i].Value)
                        {
                            exist = true;
                            break;
                        }
                    }
                    if (exist == false)
                        this.listboxModerator.Items.Add(itemUser);
                }
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorAddUserToModerator + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void btnQueryUser_Click(object sender, EventArgs e)
        {
            try
            {
                this.aspnetPager.PageIndex = 0;
                ViewState["UserOrOperatorKeyWord"] = txtKeyWord.Text;
                this.BindUser();
                ScriptManager.RegisterStartupScript(divScript, typeof(string), "showUser", "javascript:showWindow('divThickInner', 'divThickOuter');", true);
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorLoadUsers + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void aspnetPager_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
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
        protected void aspnetPager_ChangePageSize(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.ChangePageSizeEventArgs ce)
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
        private void BindUser()
        {
            string emailOrDisplayNameKeyword = ViewState["UserOrOperatorKeyWord"].ToString();
            bool ifGetAll = false;
            bool IfGetAdmin = false;
            EnumUserType userType = EnumUserType.Operator;
            if (ddlUserType.SelectedIndex == 0) ifGetAll = true;
            else if (ddlUserType.SelectedIndex == 1) IfGetAdmin = true;
            else if (ddlUserType.SelectedIndex == 2) userType = EnumUserType.Operator;
            else if (ddlUserType.SelectedIndex == 3) userType = EnumUserType.User;

            int recordsCount;
            string sortField = ViewState["UserSortOrder"].ToString();
            string sortDirection = ViewState["UserDirect"].ToString();
            UserOrOperator[] userOrOperators = UserProcess.GetNotDeletedAndNotBannedUserOrOperatorByQueryAndPaging(this.SiteId, this.UserOrOperatorId,
                this.aspnetPager.PageIndex + 1, this.aspnetPager.PageSize, emailOrDisplayNameKeyword, userType, ifGetAll, IfGetAdmin, out recordsCount, sortField, sortDirection);

            if (recordsCount == 0)
            {
                rpUser.DataSource = null;
                rpUser.DataBind();
                aspnetPager.Visible = false;
            }
            else
            {
                aspnetPager.Visible = true;
                aspnetPager.CWCDataBind(rpUser, userOrOperators, recordsCount);

            }
        }
        protected void rpUser_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            UserOrOperator user = e.Item.DataItem as UserOrOperator;
            HtmlInputCheckBox chbUser = e.Item.FindControl("checkBoxUser") as HtmlInputCheckBox;
            if (this.SelectedUserIds.Contains(user.Id))
                chbUser.Checked = true;
            else
                chbUser.Checked = false;
        }
        #endregion

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
    }
}
