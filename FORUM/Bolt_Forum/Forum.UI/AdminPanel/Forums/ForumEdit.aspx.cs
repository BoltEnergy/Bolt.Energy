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
using System.Reflection;
using Com.Comm100.Forum.Language;
using Com.Comm100.Framework.Enum;
using System.Web.UI.HtmlControls;

namespace Forum.UI.AdminPanel.Forums
{
    public partial class ForumEdit : AdminBasePage
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
        string SaveSucceeded;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Forums_PageEditErrorLoadPage];
                ErrorSave = Proxy[EnumText.enumForum_Forums_PageEditErroeUpdate];
                ErrorRedirect = Proxy[EnumText.enumForum_Public_RedirectError];
                UserTypeAll = Proxy[EnumText.enumForum_User_UserTypeAll];
                UserTypeRegisterUser = Proxy[EnumText.enumForum_User_UserTypeRegisterUser];
                UserTypeAdmin = Proxy[EnumText.enumForum_User_UserTypeAdmin];
                UserTypeOperator = Proxy[EnumText.enumForum_User_UserTypeOperator];
                ErrorAddUserToModerator = Proxy[EnumText.enumForum_ForumAdd_AddModeratorError];
                ErrorLoadUsers = Proxy[EnumText.enumForum_User_SelectUserLoadUsersError];
                ModeratorRequired = Proxy[EnumText.enumForum_Forums_ErrorModeratorRequired]; ;
                SaveSucceeded = Proxy[EnumText.enumForum_Forum_EditSaveSucceeded];
                Master.Page.Title = Proxy[EnumText.enumForum_Forums_TitleEdit];
                lblTitle.Text = Proxy[EnumText.enumForum_Forums_TitleEdit];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Forums_SubtitleEdit];
                btnSave1.Text = Proxy[EnumText.enumForum_Forums_ButtonSave];
                btnSave2.Text = Proxy[EnumText.enumForum_Forums_ButtonSave];
                btnCancel1.Text = Proxy[EnumText.enumForum_Forums_ButtonCancel];
                btnCancel2.Text = Proxy[EnumText.enumForum_Forums_ButtonCancel];
                btnSelectTop.Text = Proxy[EnumText.enumForum_User_SelectUserButtonSubmit];
                btnSelectButtom.Text = Proxy[EnumText.enumForum_User_SelectUserButtonSubmit];
                btnQueryUser1.Text = Proxy[EnumText.enumForum_Public_ButtonQuery];
                btnQueryUser2.Text = Proxy[EnumText.enumForum_Public_ButtonQuery];
                this.lblSelectUserTitle.Text = Proxy[EnumText.enumForum_ForumAdd_AddModeratorSelectUserTitle];
                this.btnAddModerator.Text = Proxy[EnumText.enumForum_Forum_ButtonAddModerator];
                this.btnRemoveModerator.Text = Proxy[EnumText.enumForum_Forum_ButtonRemoveModerator];
                this.checkBoxReply.Text = Proxy[EnumText.enumForum_Forum_CheckBoxNeedReplyToView];
                this.checkBoxPay.Text = Proxy[EnumText.enumForum_Forum_CheckBoxNeedPayScoreToView];
                this.rfvName.ErrorMessage = Proxy[EnumText.enumForum_Forums_ErrorNameRequired];
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }
        #endregion Language

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

                    int forumId = Convert.ToInt32(Request.QueryString["forumId"]);
                    ViewState["ForumId"] = forumId;

                    InitFormData();
#if OPENSOURCE
#else
                    InitDropDownList();
#endif

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
                            if (!selectUserIds.Contains(userId))
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
                lblMessage.Text = ErrorLoad+ exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void InitFormData()
        {
            int forumId = (int)ViewState["ForumId"];
            Com.Comm100.Forum.Bussiness.ForumWithPermissionCheck tmpForum = ForumProcess.GetForumByForumId(this.SiteId, this.UserOrOperatorId, forumId);
            ViewState["OldCategoryId"] = tmpForum.CategoryId;

            txtName.Text = tmpForum.Name;
            txtDescription.Text = tmpForum.Description;
            this.radioButtonListStatus.SelectedIndex = tmpForum.Status == EnumForumStatus.Open ? 0 : 1;

            ddlCategory.DataTextField = "Name";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataSource = CategoryProcess.GetAllCategories(this.UserOrOperatorId, this.IfOperator, this.SiteId);
            ddlCategory.DataBind();
            ddlCategory.SelectedValue = tmpForum.CategoryId.ToString();

            this.radioButtonListStatus.Items.Add(new ListItem(Proxy[EnumText.enumForum_Forums_StatusNormal], ((int)EnumForumStatus.Open).ToString()));//Forum status Normal/Close
            this.radioButtonListStatus.Items.Add(new ListItem(Proxy[EnumText.enumForum_Forums_StatusClose], ((int)EnumForumStatus.Lock).ToString()));
            this.radioButtonListStatus.SelectedValue = tmpForum.Status == EnumForumStatus.Open ? ((int)EnumForumStatus.Open).ToString() : ((int)EnumForumStatus.Lock).ToString();



            listboxModerator.DataTextField = "DisplayName";
            listboxModerator.DataValueField = "Id";
            listboxModerator.DataSource = ForumProcess.GetModeratorsByForumId(forumId, this.SiteId, this.UserOrOperatorId, this.IfOperator);//OperatorProcess.GetAllNotDeletedOperators(CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.SiteId);
            listboxModerator.DataBind();

            this.checkBoxReply.Checked = tmpForum.IfAllowPostNeedingReplayTopic;
            this.checkBoxPay.Checked = tmpForum.IfAllowPostNeedingPayTopic;

            //Moderator[] tmpModerators = ForumProcess.GetModeratorsByForumId(tmpForum.ForumId, CurrentUserOrOperator.SiteId, CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.IfOperator);
            //for (int i = 0; i < tmpModerators.Length; i++)
            //{
            //    for (int j = 0; j < listboxModerator.Items.Count; j++)
            //    {
            //        if (tmpModerators[i].Id == Convert.ToInt32(listboxModerator.Items[j].Value))
            //            listboxModerator.Items[j].Selected = true;
            //    }
            //}
        }
        private int GetSelectedCategoryId()
        {
            CategoryWithPermissionCheck category = CategoryProcess.GetCategoryById(UserOrOperatorId, IfOperator, SiteId, Convert.ToInt32(ddlCategory.SelectedValue));
            return category.CategoryId;
        }

        private int[] GetModeratorIds()
        {
            int[] moderatorIds = new int[listboxModerator.Items.Count];

            for (int i = 0; i < listboxModerator.Items.Count; i++)
            {
                moderatorIds[i] = Convert.ToInt32(listboxModerator.Items[i].Value);
            }

            return moderatorIds;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string description = txtDescription.Text;

            try
            {
                if (listboxModerator.Items.Count == 0)
                    lblModeratorReuired.Text = Proxy[EnumText.enumForum_Forums_ErrorModeratorRequired];
                else
                {
                    int forumId = (int)ViewState["ForumId"];
                    int oldCategoryId = (int)ViewState["OldCategoryId"];
                    int newCategoryId = GetSelectedCategoryId();
                    int[] moderatorIds = GetModeratorIds();
                    bool ifAllowPostNeedingReplayTopic = this.checkBoxReply.Checked;
                    bool ifAllowPostNeedingPayTopic = this.checkBoxPay.Checked;
                    EnumForumStatus enumForumStatus = (EnumForumStatus)Convert.ToInt16(radioButtonListStatus.SelectedValue);
                    ForumProcess.UpdateForum(CurrentUserOrOperator.UserOrOperatorId, CurrentUserOrOperator.SiteId, forumId, name, description, oldCategoryId, newCategoryId, enumForumStatus, moderatorIds, ifAllowPostNeedingReplayTopic, ifAllowPostNeedingPayTopic);
                    this.lblSuccess.Text = SaveSucceeded;
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
                lblMessage.Text = Proxy[EnumText.enumForum_Forums_PageEditErroeUpdate] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
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

        void InitDropDownList()
        {
            trUserType.Visible = true;
            ddlUserType.Items.Add(new ListItem( UserTypeAll,"0"));
            ddlUserType.Items.Add(new ListItem(UserTypeAdmin,"1"));
            ddlUserType.Items.Add(new ListItem(UserTypeOperator,"2"));
            ddlUserType.Items.Add(new ListItem(UserTypeRegisterUser,"3"));
            //ddlCategory.SelectedIndex = 0;
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
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
                    {
                        this.listboxModerator.Items.Add(itemUser);
                        itemUser.Selected = true;
                    }
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
                this.aspnetPager1.PageIndex = 0;
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

        protected void aspnetPager1_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
        {
            try
            {
                BindUser();
                ScriptManager.RegisterStartupScript(divScript, typeof(string), "showUser", "javascript:showWindow('divThickInner', 'divThickOuter');", true);
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoadUsers + exp.Message;
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
                lblMessage.Text = ErrorLoadUsers + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        private void BindUser()
        {
            string emailOrDisplayNameKeyword = ViewState["UserOrOperatorKeyWord"].ToString();
            //int userGroupId = Convert.ToInt32(ViewState["groupId"]);
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
            UserOrOperator[] userOrOperators = UserProcess.GetNotDeletedAndNotBannedUserOrOperatorByQueryAndPaging(this.SiteId, this.UserOrOperatorId,
                this.aspnetPager1.PageIndex + 1, this.aspnetPager1.PageSize, emailOrDisplayNameKeyword, userType, ifGetAll, IfGetAdmin, out recordsCount, sortField, sortDirection);

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

        protected void btnRemoveModerator_Click(object sender, EventArgs e)
        {
            int[] index = this.listboxModerator.GetSelectedIndices();
            ListItem[] items = new ListItem[index.Length];
            for (int i = 0; i < index.Length; i++)
                items[i] = listboxModerator.Items[index[i]];
            for (int i = 0; i < index.Length; i++)
                this.listboxModerator.Items.Remove(items[i]);
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

    }
}
