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
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using System.Collections;
using Com.Comm100.Framework.Enum.Forum;
using System.Web.UI.HtmlControls;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.ModeratorPanel.Forum
{
    public partial class ForumPermission : Com.Comm100.Forum.UI.ModeratorPanel.ModeratorBasePage
    {
        #region property ForumFeature
        ForumFeatureWithPermissionCheck _forumFeature = null;
        ForumFeatureWithPermissionCheck ForumFeature
        {
            get
            {
                if (_forumFeature == null)
                    _forumFeature = SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId);
                return _forumFeature;
            }
        }
        #endregion
        #region property ForumId
        int ForumId
        {
            get
            {
                if (Request.QueryString["forumId"] != null)
                    return int.Parse(Request.QueryString["forumid"]);
                else
                {
                    Response.Redirect(string.Format("Forums.aspx?siteId=", this.SiteId));
                    return 0;
                }
            }
        }
        #endregion

        #region Language
        string ErrorLoad;
        string SuccessfullySaved;
        string ErrorAddReputationGroupToForum;
        string ErrorAddUserGroupToForum;
        string ErrorRemoveReputationGroupFromForum;
        string ErrorRemoveUserGroupFromForum;
        string ErrorSavePermission;
        string ErrorBindPermission;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Permission_ForumPermissionSettingsLoadError];
                SuccessfullySaved = Proxy[EnumText.enumForum_Permission_PermissionSaveSucceeded];
                ErrorAddReputationGroupToForum=Proxy[EnumText.enumForum_Permission_ForumPermissionSettingsErrorAddReputationGroupToForum];
                ErrorAddUserGroupToForum=Proxy[EnumText.enumForum_Permission_ForumPermissionSettingsErrorAddUserGroupToForum];
                ErrorRemoveReputationGroupFromForum=Proxy[EnumText.enumForum_Permission_ForumPermissionSettingsErrorRemoveReputationGroupFromForum];
                ErrorRemoveUserGroupFromForum=Proxy[EnumText.enumForum_Permission_ForumPermissionSettingsErrorRemoveUserGroupFromForum];
                ErrorSavePermission = Proxy[EnumText.enumForum_Permission_ForumPermissionSettingsErrorSave];
                ErrorBindPermission = Proxy[EnumText.enumForum_Permission_ForumPermissionSettingsErrorBindPermission];

                lblTitle.Text = Proxy[EnumText.enumForum_Permission_ForumPermissionSettingsTitle];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Permission_ForumPermissionSettingsSubTitle];
                btnSaveUserGroup1.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnSaveUserGroup2.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                //btnAddGroup1.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnAddGroup2.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                //btnAddGroup3.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnAddGroup4.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                #region permission item tooltip
                
                lblViewForum.Text = Proxy[EnumText.enumForum_Permission_ForumToolTipAllowViewForum];
                lblViewTopic.Text = Proxy[EnumText.enumForum_Permission_ForumToolTipAllowViewTopicOrPost];
                lblPostNotModeration.Text = Proxy[EnumText.enumForum_Permission_ForumToolTipPostModerationNotNeeded];
                lblAllowURL.Text = Proxy[EnumText.enumForum_Permission_ForumToolTipAllowLink];
                lblAllowImage.Text = Proxy[EnumText.enumForum_Permission_ForumToolTipAllowInsertImage];
                lblPostTopicOrPost.Text = Proxy[EnumText.enumForum_Permission_ForumToolTipAllowPostTopicOrPost];
                lblMinIntervalPost.Text = Proxy[EnumText.enumForum_Permission_ForumToolTipMinInterValTimeForPosting];
                lblMaxPostLength.Text = Proxy[EnumText.enumForum_Permission_ForumToolTipMaxLengthOfTopicOrPost]; 

                #endregion 
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + exp.Message;
                this.lblMessage.Visible = true;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion

        public string VisibleUserGroups
        {
            get
            {
                ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId);
                if (forumFeature.IfEnableGroupPermission) return "";
                else return "display:none;";
            }
        }
        public string VisibleReputationGroups
        {
            get
            {
                ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId);
                if (forumFeature.IfEnableReputationPermission) return "";
                else return "display:none;";
            }
        }

        public bool ifUserGroupEnabled
        {
            get
            {
                if (VisibleUserGroups == "")
                    return true;
                else
                    return false;
            }
        }

        public bool ifReputationEnabled
        {
            get
            {
                if (VisibleReputationGroups == "")
                    return true;
                else
                    return false;
            }
        }
        #region Const
        private const string VSKEY_UserGroupsInForum = "UserGroupsInForum";
        private const string VSKEY_ReputationGroupsInForum = "ReputationGroupsInForum";
        private const string VSKEY_Permissions = "Permissions";
        #endregion 

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((ModeratorMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumForumManage);
                CheckPermissionEnable();
                this.lblMessage.Visible = false;
                this.lblSuccess.Visible = false;
                if (!IsPostBack)
                {
                    txtMaxPostLength.MaxLength = Int32.MaxValue.ToString().Length;
                    txtMinIntervalPost.MaxLength = Int32.MaxValue.ToString().Length;
                    PageInit();
                }
            }
            catch (Exception exp)
            {
                this.lblMessage.Visible = true;
                this.lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void PageInit()
        {
            if (ForumFeature.IfEnableReputationPermission)
            {
                this.ReputationGroupsBind();
                this.NotAddForumReputationGroupBind();
            }
            if (ForumFeature.IfEnableGroupPermission)
            {
                this.UserGroupsBind();
                this.NotAddForumUserGroupBind();
            }
            ForumWithPermissionCheck forum = ForumProcess.GetForumByForumId(SiteId, CurrentUserOrOperator.UserOrOperatorId, ForumId);
            this.lblForumName.Text =Server.HtmlEncode(forum.Name);

            #region Init Inherit or Custom
            if (ForumProcess.IfInheritPermission(this.SiteId, this.UserOrOperatorId, ForumId))
            {
                this.hdIfInherit.Value = "1";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Inherit", "doCustom(false);", true);
            }
            else
            {
                this.hdIfInherit.Value = "0";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Custom", "doCustom(true);", true);
            }
            #endregion Init Inherit or Custom
        }

        #region Show Group With JS
        void ShowGroup(EnumUserGroupType type)
        {
            string userGroupStyle = @"changeTabStyle(1);";
            string reputationGroupStyle = @"changeTabStyle(2);";
            string userGroupShow = @"highLightDiv('divUserGroup');";
            string reputationGroupshow = @"highLightDiv('divReputationGroup');";
            //string userGroupShowFirst = @"setFirstGroup('divUserGroup');";
            //string reputationGroupFirst = @"setFirstGroup('divReputationGroup')";
            if (type == EnumUserGroupType.UserGroup)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowStyle1", userGroupStyle, true);
                //if (ifFirst)
                //{
                //    if (rpUserGroup.Items.Count == 0)
                //    {
                //        this.divGroupPermission.Visible = false;
                //    }
                //    else
                //    {
                //        this.divGroupPermission.Visible = true;
                //        Page.ClientScript.RegisterStartupScript(this.GetType(), "userGroupShowFirst", userGroupShowFirst, true);
                //    }
                //}
                //else
                //{
                    this.divGroupPermission.Visible = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "userGroupShow", userGroupShow, true);
                //}
            }
            else if (type == EnumUserGroupType.UserReputationGroup)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowStyle2", reputationGroupStyle, true);
                //if (ifFirst)
                //{
                //    if (rpReputationGroups.Items.Count == 0)
                //    {
                //        this.divGroupPermission.Visible = false;
                //    }
                //    else
                //    {
                //        this.divGroupPermission.Visible = true;
                //        Page.ClientScript.RegisterStartupScript(this.GetType(), "reputationGroupShowFirst", reputationGroupFirst, true);
                //    }
                //}
                //else
                //{
                    this.divGroupPermission.Visible = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "reputationGroupShow", reputationGroupshow, true);
                //}

            }
        }
        #endregion

        #region Group Bind
        #region User Groups In Forum
        void UserGroupsBind()
        {
            try{
                if (ViewState[VSKEY_UserGroupsInForum] == null)
                {
                    UserGroupWithPermissionCheck[] groupsInForum = UserGroupProcess.GetUserGroupsByForumId(ForumId, SiteId, UserOrOperatorId);
                    List<int> userGroupsInForum = new List<int>();
                    for (int i = 0; i < groupsInForum.Length; i++)
                    {
                        int groupId = groupsInForum[i].UserGroupId;
                        if (!userGroupsInForum.Contains(groupId))
                            userGroupsInForum.Add(groupsInForum[i].UserGroupId);
                    }
                    ViewState[VSKEY_UserGroupsInForum] = userGroupsInForum;

                    #region Add Permission In Permssions
                    List<GroupPermission> permissions = this.GetPermissionList();
                    for (int i = 0; i < userGroupsInForum.Count; i++)
                    {
                        GroupPermission permission = new GroupPermission();
                        UserGroupPermissionForForumWithPermissionCheck userGroupPermission = UserGroupProcess.GetUserGroupPermissionForForum(SiteId, UserOrOperatorId, groupsInForum[i].UserGroupId, ForumId);
                        permission.GroupId = userGroupPermission.GroupId;
                        //permission.IfAllowHtml = userGroupPermission.IfAllowHTML;
                        permission.IfAllowInsertImage = userGroupPermission.IfAllowUploadImage;
                        permission.IfAllowPost = userGroupPermission.IfAllowPost;
                        permission.IfAllowUrl = userGroupPermission.IfAllowUrl;
                        permission.IfAllowViewForum = userGroupPermission.IfAllowViewForum;
                        permission.IfAllowViewTopic = userGroupPermission.IfAllowViewTopic;
                        permission.IfPostModerationNotRequired = !userGroupPermission.IfPostNotNeedModeration;
                        permission.MaxLengthOfTopicPost = userGroupPermission.MaxLengthOfPost;
                        permission.MinIntervalTimeForPosting = userGroupPermission.MinIntervalForPost;
                        permissions.Add(permission);
                    }
                    ViewState[VSKEY_Permissions] = permissions;
                    #endregion

                    this.rpUserGroup.DataSource = groupsInForum;
                    this.rpUserGroup.DataBind();
                    if (groupsInForum.Count() > 0)
                    {
                        hdGroupId.Value = groupsInForum[0].UserGroupId.ToString();
                        this.PermissionBind(groupsInForum[0].UserGroupId, EnumUserGroupType.UserGroup);
                    }
                    else
                    {
                        ShowGroup(EnumUserGroupType.UserGroup);
                        this.divGroupPermission.Visible = false;
                    }
                }
                else
                {
                    List<int> userGroupsInForum = ViewState[VSKEY_UserGroupsInForum] as List<int>;
                    UserGroupWithPermissionCheck[] groups = UserGroupProcess.GetAllUserGroups(this.SiteId, this.UserOrOperatorId);
                    UserGroupWithPermissionCheck[] groupsInForum = new UserGroupWithPermissionCheck[userGroupsInForum.Count];
                    int j = 0;
                    for (int i = 0; i < groups.Length; i++)
                    {
                        if (userGroupsInForum.Contains(groups[i].UserGroupId))
                        {
                            groupsInForum[j] = groups[i];
                            j++;
                        }
                    }
                    this.rpUserGroup.DataSource = groupsInForum;
                    this.rpUserGroup.DataBind();
                    if (groupsInForum.Count() > 0)
                    {
                        if (hdGroupId.Value != "")
                        {
                            int groupId = Convert.ToInt32(hdGroupId.Value);
                            if (userGroupsInForum.Contains(groupId))
                                this.PermissionBind(groupId, EnumUserGroupType.UserGroup);
                            else
                            {
                                hdGroupId.Value = groupsInForum[0].UserGroupId.ToString();
                                this.PermissionBind(groupsInForum[0].UserGroupId, EnumUserGroupType.UserGroup);
                            }
                        }
                        else
                        {
                            hdGroupId.Value = groupsInForum[0].UserGroupId.ToString();
                            this.PermissionBind(groupsInForum[0].UserGroupId, EnumUserGroupType.UserGroup);
                        }
                    }
                    else
                    {
                        ShowGroup(EnumUserGroupType.UserGroup);
                        this.divGroupPermission.Visible = false;
                    }

                }

            }
            catch (Exception ex)
            {
                this.lblMessage.Text = ex.Message;
                this.lblMessage.Visible = true;
            }
        }
        #endregion

        #region Reputation Groups In Forum
        void ReputationGroupsBind()
        {
            if (ViewState[VSKEY_ReputationGroupsInForum] == null)
            {
                UserReputationGroupWithPermissionCheck[] groupsInForum = UserReputationGroupProcess.GetReputationGroupsByForumId(ForumId, SiteId,
                    CurrentUserOrOperator.UserOrOperatorId);
                List<int> reputationGroupsInForum = new List<int>();
                for (int i = 0; i < groupsInForum.Count(); i++)
                {
                    int groupId = groupsInForum[i].GroupId;
                    if (!reputationGroupsInForum.Contains(groupId))
                        reputationGroupsInForum.Add(groupsInForum[i].GroupId);
                }
                ViewState[VSKEY_ReputationGroupsInForum] = reputationGroupsInForum;

                #region Add Permission In Permissions
                List<GroupPermission> permissions = this.GetPermissionList();
                for (int i = 0; i < reputationGroupsInForum.Count; i++)
                {
                    GroupPermission permission = new GroupPermission();
                    UserReputationGroupPermissionForForumWithPermissionCheck userReputationGroupPermission = UserReputationGroupProcess.GetUserReputationGroupPermissionForForum(SiteId, UserOrOperatorId, groupsInForum[i].GroupId, ForumId);
                    permission.GroupId = userReputationGroupPermission.GroupId;
                    //permission.IfAllowHtml = userReputationGroupPermission.IfAllowHTML;
                    permission.IfAllowInsertImage = userReputationGroupPermission.IfAllowUploadImage;
                    permission.IfAllowPost = userReputationGroupPermission.IfAllowPost;
                    permission.IfAllowUrl = userReputationGroupPermission.IfAllowUrl;
                    permission.IfAllowViewForum = userReputationGroupPermission.IfAllowViewForum;
                    permission.IfAllowViewTopic = userReputationGroupPermission.IfAllowViewTopic;
                    permission.IfPostModerationNotRequired = !userReputationGroupPermission.IfPostNotNeedModeration;
                    permission.MaxLengthOfTopicPost = userReputationGroupPermission.MaxLengthOfPost;
                    permission.MinIntervalTimeForPosting = userReputationGroupPermission.MinIntervalForPost;
                    permissions.Add(permission);
                }
                ViewState[VSKEY_Permissions] = permissions;
                #endregion

                this.rpReputationGroups.DataSource = groupsInForum;
                this.rpReputationGroups.DataBind();
                if (groupsInForum.Count() > 0)
                {
                    hdGroupId.Value = groupsInForum[0].GroupId.ToString();

                    this.PermissionBind(groupsInForum[0].GroupId, EnumUserGroupType.UserReputationGroup);
                }
                else
                {
                    ShowGroup(EnumUserGroupType.UserReputationGroup);
                    this.divGroupPermission.Visible = false;
                }
            }
            else
            {
                List<int> reputationGroupsInForum = ViewState[VSKEY_ReputationGroupsInForum] as List<int>;
                UserReputationGroupWithPermissionCheck[] groups = UserReputationGroupProcess.GetAllGroups(this.SiteId, this.UserOrOperatorId);
                UserReputationGroupWithPermissionCheck[] groupsInForum = new UserReputationGroupWithPermissionCheck[reputationGroupsInForum.Count];
                int j = 0;
                for (int i = 0; i < groups.Length; i++)
                {
                    if (reputationGroupsInForum.Contains(groups[i].GroupId))
                    {
                        groupsInForum[j] = groups[i];
                        j++;
                    }
                }
                this.rpReputationGroups.DataSource = groupsInForum;
                this.rpReputationGroups.DataBind();
                if (groupsInForum.Count() > 0)
                {
                    if (hdGroupId.Value != "")
                    {
                        int groupId = Convert.ToInt32(hdGroupId.Value);
                        if (reputationGroupsInForum.Contains(groupId))
                            this.PermissionBind(groupId, EnumUserGroupType.UserReputationGroup);
                        else
                        {
                            hdGroupId.Value = groupsInForum[0].GroupId.ToString();
                            this.PermissionBind(groupsInForum[0].GroupId, EnumUserGroupType.UserReputationGroup);
                        }
                    }
                    else
                    {
                        hdGroupId.Value = groupsInForum[0].GroupId.ToString();
                        this.PermissionBind(groupsInForum[0].GroupId, EnumUserGroupType.UserReputationGroup);
                    }
                }
                else
                {
                    ShowGroup(EnumUserGroupType.UserReputationGroup);
                    this.divGroupPermission.Visible = false;
                }
            }
        }
        #endregion

        #region User Group Not Added In Forum
        protected void NotAddForumUserGroupBind()
        {
            UserGroupWithPermissionCheck[] groups = UserGroupProcess.GetAllUserGroups(SiteId, UserOrOperatorId);
            List<int > userGroupsInForum=ViewState[VSKEY_UserGroupsInForum] as List<int>;
            UserGroupWithPermissionCheck[] userGroupNotInForum=new UserGroupWithPermissionCheck[groups.Length-userGroupsInForum.Count];
            int j=0;
            for (int i = 0; i < groups.Length; i++)
            {
                if (!userGroupsInForum.Contains(groups[i].UserGroupId))
                {
                    userGroupNotInForum[j] = groups[i];
                    j++;
                }
            }
            this.rpUserGroupNotInForum.DataSource = userGroupNotInForum;
            this.rpUserGroupNotInForum.DataBind();
        }
        #endregion

        #region Reputation Group Not Added In Forum
        protected void NotAddForumReputationGroupBind()
        {
            UserReputationGroupWithPermissionCheck[] groups=UserReputationGroupProcess.GetAllGroups(SiteId,UserOrOperatorId);
            List<int> reputationGroupsInForum=ViewState[VSKEY_ReputationGroupsInForum] as List<int>;
            UserReputationGroupWithPermissionCheck[] reputationGroupsNotInForum = new UserReputationGroupWithPermissionCheck[groups.Length - reputationGroupsInForum.Count];
            int j = 0;
            for (int i = 0; i < groups.Length;i++ )
            {
                if (!reputationGroupsInForum.Contains(groups[i].GroupId))
                {
                    reputationGroupsNotInForum[j] = groups[i];
                    j++;
                }
            }
            this.rpReputationGroupsNotInForum.DataSource = reputationGroupsNotInForum;
            this.rpReputationGroupsNotInForum.DataBind();
        }
        #endregion 
        #endregion 

        #region Event
        #region Select Group
        protected void lbtnName_Click(object sender, EventArgs e)
        {
            try
            {
                CheckGorupPermissionEnable();
                int groupId = int.Parse((sender as LinkButton).CommandArgument);
                if(hdGroupId.Value!="")
                    AddToCache(Convert.ToInt32(hdGroupId.Value));
                hdGroupId.Value = groupId.ToString();
                this.PermissionBind(groupId,EnumUserGroupType.UserGroup);
                this.NotAddForumUserGroupBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text =ErrorBindPermission + ex.Message;
                lblMessage.Visible = true;
            }
        }
        protected void lbtnReputationGroupName_Click(object sender, EventArgs e)
        {
            try
            {
                CheckReputationPermissionEnble();
                int groupId = int.Parse((sender as LinkButton).CommandArgument);
                if (hdGroupId.Value != "")
                    AddToCache(Convert.ToInt32(hdGroupId.Value));
                hdGroupId.Value = groupId.ToString();
                this.PermissionBind(groupId,EnumUserGroupType.UserReputationGroup);
                this.NotAddForumReputationGroupBind();

            }
            catch (Exception ex)
            {
                lblMessage.Text =ErrorBindPermission + ex.Message;
                lblMessage.Visible = true;
            }
        }
        #endregion

        #region Delete Group
        protected void ibtnGroupDel_Click(object sender, EventArgs e)
        {
            try
            {
                CheckGorupPermissionEnable();
                int groupId = int.Parse((sender as ImageButton).CommandArgument);
                List<int> userGroupsInForum = ViewState[VSKEY_UserGroupsInForum] as List<int>;
                userGroupsInForum.Remove(groupId);
                ViewState[VSKEY_UserGroupsInForum] = userGroupsInForum;
                if(hdGroupId.Value!="")
                {
                    AddToCache(Convert.ToInt32(hdGroupId.Value));
                    if(Convert.ToInt32(hdGroupId.Value)==groupId)
                        hdGroupId.Value="";
                }
                this.UserGroupsBind();
                this.NotAddForumUserGroupBind();
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = ErrorRemoveUserGroupFromForum + ex.Message;
                this.lblMessage.Visible = true;
                LogHelper.WriteExceptionLog(ex);
            }
        }
        protected void ibtnReputatioinGroupDel_Click(object sender, EventArgs e)
        {
            try
            {
                CheckReputationPermissionEnble();
                int groupId = int.Parse((sender as ImageButton).CommandArgument);
                List<int> reputationGroupInForum = ViewState[VSKEY_ReputationGroupsInForum] as List<int>;
                reputationGroupInForum.Remove(groupId);
                ViewState[VSKEY_ReputationGroupsInForum] = reputationGroupInForum;
                if (hdGroupId.Value != "")
                {
                    AddToCache(Convert.ToInt32(hdGroupId.Value));
                    if (Convert.ToInt32(hdGroupId.Value) == groupId)
                        hdGroupId.Value = "";
                }
                this.ReputationGroupsBind();
                this.NotAddForumReputationGroupBind();
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = ErrorRemoveReputationGroupFromForum + ex.Message;
                this.lblMessage.Visible = true;
                LogHelper.WriteExceptionLog(ex);
            }
        }
        #endregion

        #region Add Group
        protected void btnAddUserGroup_Click(object sender, EventArgs e)
        {
            try
            {
                CheckGorupPermissionEnable();
                if (hdGroupId.Value != "")
                    AddToCache(Convert.ToInt32(hdGroupId.Value));
                #region Add Group In Forum Group List & Add Permission In Permissions
                List<int> userGroupsInForum = ViewState[VSKEY_UserGroupsInForum] as List<int>;
                List<GroupPermission> permissions = this.GetPermissionList();
                for (int i = 0; i < this.rpUserGroupNotInForum.Items.Count; i++)
                {
                    RepeaterItem r = rpUserGroupNotInForum.Items[i];
                    CheckBox chbGroup = r.FindControl("chbGroup") as CheckBox;
                    Label lblGroupId = r.FindControl("lblGroupId") as Label;
                    int groupId = int.Parse(lblGroupId.Text);
                    if (chbGroup.Checked == true)
                    {
                        //Add group in list
                        if(!userGroupsInForum.Contains(groupId))
                            userGroupsInForum.Add(groupId);
                        //Add permission when permissions doesn't exist
                        var lists = from GroupPermission g in permissions
                                    where g.GroupId == groupId
                                    select g;
                        if(lists.Count()<=0)
                        {
                            UserGroupPermissionWithPermissionCheck userGroupPermission = UserGroupProcess.GetGroupPermissionByGroupId(groupId, SiteId, UserOrOperatorId);
                            GroupPermission permission=new GroupPermission();
                            permission.GroupId = userGroupPermission.GroupId;
                            //permission.IfAllowHtml = userGroupPermission.IfAllowHTML;
                            permission.IfAllowInsertImage = userGroupPermission.IfAllowUploadImage;
                            permission.IfAllowPost = userGroupPermission.IfAllowPost;
                            permission.IfAllowUrl = userGroupPermission.IfAllowUrl;
                            permission.IfAllowViewForum = userGroupPermission.IfAllowViewForum;
                            permission.IfAllowViewTopic = userGroupPermission.IfAllowViewTopic;
                            permission.IfPostModerationNotRequired = !userGroupPermission.IfPostNotNeedModeration;
                            permission.MaxLengthOfTopicPost = userGroupPermission.MaxLengthOfPost;
                            permission.MinIntervalTimeForPosting = userGroupPermission.MinIntervalForPost;
                            permissions.Add(permission);
                        }
                        hdGroupId.Value = groupId.ToString();
                    }
                }
                ViewState[VSKEY_UserGroupsInForum] = userGroupsInForum;
                ViewState[VSKEY_Permissions] = permissions;
                #endregion 
                this.UserGroupsBind();
                this.NotAddForumUserGroupBind();
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorAddUserGroupToForum + exp.Message;
                this.lblMessage.Visible = true;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void btnAddReputationGroup_Click(object sender, EventArgs e)
        {
            try
            {
                CheckReputationPermissionEnble();
                if (hdGroupId.Value != "")
                    AddToCache(Convert.ToInt32(hdGroupId.Value));
                #region Add Group In Forum Group List & Add Permission In Permissions
                List<int> reputationGroupsInForum = ViewState[VSKEY_ReputationGroupsInForum] as List<int>;
                List<GroupPermission> permissions = this.GetPermissionList();
                for (int i = 0; i < this.rpReputationGroupsNotInForum.Items.Count; i++)
                {
                    RepeaterItem r = rpReputationGroupsNotInForum.Items[i];
                    CheckBox chbGroup = r.FindControl("chbGroup") as CheckBox;
                    Label lblGroupId = r.FindControl("lblGroupId") as Label;
                    int groupId = int.Parse(lblGroupId.Text);
                    if (chbGroup.Checked == true)
                    {
                        if(!reputationGroupsInForum.Contains(groupId))
                            reputationGroupsInForum.Add(groupId);
                        var lists = from GroupPermission g in permissions
                                    where g.GroupId == groupId
                                    select g;
                        if (lists.Count() <= 0)
                        {
                            UserReputationGroupPermissionWithPermissionCheck userReputationGroupPermission = UserReputationGroupProcess.GetPermissionByGroupId(SiteId,UserOrOperatorId,groupId);
                            GroupPermission permission = new GroupPermission();
                            permission.GroupId = userReputationGroupPermission.GroupId;
                            //permission.IfAllowHtml = userReputationGroupPermission.IfAllowHTML;
                            permission.IfAllowInsertImage = userReputationGroupPermission.IfAllowUploadImage;
                            permission.IfAllowPost = userReputationGroupPermission.IfAllowPost;
                            permission.IfAllowUrl = userReputationGroupPermission.IfAllowUrl;
                            permission.IfAllowViewForum = userReputationGroupPermission.IfAllowViewForum;
                            permission.IfAllowViewTopic = userReputationGroupPermission.IfAllowViewTopic;
                            permission.IfPostModerationNotRequired = !userReputationGroupPermission.IfPostNotNeedModeration;
                            permission.MaxLengthOfTopicPost = userReputationGroupPermission.MaxLengthOfPost;
                            permission.MinIntervalTimeForPosting = userReputationGroupPermission.MinIntervalForPost;
                            permissions.Add(permission);
                        }
                        hdGroupId.Value = groupId.ToString();
                    }
                }
                ViewState[VSKEY_ReputationGroupsInForum] = reputationGroupsInForum;
                ViewState[VSKEY_Permissions] = permissions;
                #endregion
                this.ReputationGroupsBind();
                this.NotAddForumReputationGroupBind();
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorAddReputationGroupToForum + exp.Message;
                this.lblMessage.Visible = true;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion

        #region Save
        protected void btnUserGroupSave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckPermissionEnable();
                bool ifInherit = Convert.ToInt32(this.hdIfInherit.Value) == 1 ? true : false;
                ForumProcess.UpdateIfInheritPermission(this.SiteId, this.UserOrOperatorId, ForumId, ifInherit);
                if (ifInherit)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Inherit", "doCustom(false);", true);
                else
                {
                    if (this.divGroupPermission.Visible == true)
                        AddToCache(int.Parse((sender as Button).CommandArgument));
                    
                    List<int> userGroupIdsInForum = new List<int>();
                    List<GroupPermission> userGroupPermissions = new List<GroupPermission>();
                    if (ForumFeature.IfEnableGroupPermission)
                    {
                        userGroupIdsInForum = ViewState[VSKEY_UserGroupsInForum] as List<int>;
                        if (userGroupIdsInForum == null)
                        {
                            userGroupIdsInForum = new List<int>();
                           // ExceptionHelper.ThrowForumSettingsCloseReputationPermissionFunction();
                        }
                        else
                        {
                            for (int i = 0; i < userGroupIdsInForum.Count; i++)
                                userGroupPermissions.Add(GetPermission(userGroupIdsInForum[i]));
                        }
                    }
                   

                    List<int> reputationGorupIdsInForum =new List<int>();
                    List<GroupPermission> reputationGroupPermissions = new List<GroupPermission>();
                    if (ForumFeature.IfEnableReputationPermission)
                    {
                        reputationGorupIdsInForum = ViewState[VSKEY_ReputationGroupsInForum] as List<int>;
                        if (reputationGorupIdsInForum == null)
                        {
                            reputationGorupIdsInForum = new List<int>();
                            //ExceptionHelper.ThrowForumSettingsCloseGroupPermissionFunction();
                            
                        }
                        else
                        {
                            for (int i = 0; i < reputationGorupIdsInForum.Count; i++)
                                reputationGroupPermissions.Add(GetPermission(reputationGorupIdsInForum[i]));
                        }
                    }
                  
                    GroupPermissioniProcess.UpdateGroupsPermissionInForum(SiteId, UserOrOperatorId, ForumId, userGroupIdsInForum, userGroupPermissions, reputationGorupIdsInForum, reputationGroupPermissions);
                    if (this.hdGroupType.Value == "1")
                        UserGroupsBind();
                        //ShowGroup(EnumUserGroupType.UserGroup);
                    else
                        ReputationGroupsBind();
                        //ShowGroup(EnumUserGroupType.UserReputationGroup);
                }
                this.lblSuccess.Text = SuccessfullySaved;
                this.lblSuccess.Visible = true;
                this.lblMessage.Visible = false;
            }
            catch (Exception ex)
            {
                this.lblMessage.Text =ErrorSavePermission + ex.Message;
                this.lblMessage.Visible = true;
                this.lblSuccess.Visible = false;
                LogHelper.WriteExceptionLog(ex);
            }
        }
        #endregion
        #endregion

        #region Bind Permission With GroupId
        void PermissionBind(int groupId,EnumUserGroupType type)
        {
            GroupPermission p = this.GetPermission(groupId);
            this.chbImage.Checked = p.IfAllowInsertImage;
            this.chbPost.Checked = p.IfAllowPost;
            this.chbPostNotModeration.Checked = !p.IfPostModerationNotRequired;
            this.chbURL.Checked = p.IfAllowUrl;
            this.chbViewForum.Checked = p.IfAllowViewForum;
            this.chbViewTopic.Checked = p.IfAllowViewTopic;
            this.txtMaxPostLength.Text = p.MaxLengthOfTopicPost.ToString();
            this.txtMinIntervalPost.Text = p.MinIntervalTimeForPosting.ToString();
            this.btnSaveUserGroup1.CommandArgument = this.btnSaveUserGroup2.CommandArgument = groupId.ToString();
            if (type == EnumUserGroupType.UserGroup)
                hdHighLightDIV.Value = "divUserGroupItem" + groupId;
            else
                hdHighLightDIV.Value = "divReputationGroupItem" + groupId;
            ShowGroup(type);
        
        }
        #endregion

        GroupPermission GetPermission(int groupId)
        {
            IList permissions = ViewState[VSKEY_Permissions] as IList;

            var lists = from GroupPermission r in permissions
                        where r.GroupId == groupId
                        select r;
            if (lists.Count() != 1)     //Just Test
                ExceptionHelper.ThrowForumGetPermissionError();
            return lists.First() as GroupPermission;
        }

        List<GroupPermission> GetPermissionList()
        {
            if (ViewState[VSKEY_Permissions] == null)
            {
                return new List<GroupPermission>();
            }

            return ViewState[VSKEY_Permissions] as List<GroupPermission>;
        }

        void AddToCache(int groupId)
        {
            List<GroupPermission> permissions = this.GetPermissionList();
            var lists = from GroupPermission r in permissions
                        where r.GroupId == groupId
                        select r;
            GroupPermission p = lists.First() as GroupPermission;
            permissions.Remove(p);
            p.IfAllowPost = chbPost.Checked;
            p.IfAllowInsertImage = chbImage.Checked;
            p.IfAllowUrl = chbURL.Checked;
            p.IfAllowViewForum = chbViewForum.Checked;
            p.IfAllowViewTopic = chbViewTopic.Checked;
            p.IfPostModerationNotRequired = !chbPostNotModeration.Checked;
            p.MaxLengthOfTopicPost = int.Parse(txtMaxPostLength.Text);
            p.MinIntervalTimeForPosting = int.Parse(txtMinIntervalPost.Text);
            p.GroupId = groupId;
            permissions.Add(p);
            ViewState[VSKEY_Permissions] = permissions;
        }
        protected void lkbUserGroups_Click(object sender, EventArgs e)
        {
            UserGroupsBind();
        }

        protected void lkbReputationGroups_Click(object sender, EventArgs e)
        {
            ReputationGroupsBind();
        }

        protected string GetCustomDescribtion()
        {
            string describtion = Proxy[EnumText.enumForum_Forums_CustomDescription];
            string StrenableGroup = "";
            if(_forumFeature.IfEnableGroupPermission)
                StrenableGroup += Proxy[EnumText.enumForum_Forums_CustomDescription_UserGroup] + Proxy[EnumText.enumForum_Forums_CustomDescription_And];
            if(_forumFeature.IfEnableReputationPermission)
                StrenableGroup += Proxy[EnumText.enumForum_Forums_CustomDescription_ReputationGroup] + Proxy[EnumText.enumForum_Forums_CustomDescription_And];
            if(StrenableGroup != "")
                StrenableGroup = StrenableGroup.Substring(0,StrenableGroup.Length - " And ".Length);
            return string.Format(describtion, StrenableGroup);
        }

        #region Check Permission Enable
        private void CheckPermissionEnable()
        {
            if (!ForumFeature.IfEnableReputationPermission && !ForumFeature.IfEnableGroupPermission)
                ExceptionHelper.ThrowForumSettingsCloseGroupsPermissionAndReputationGroup();
        }

        private void CheckGorupPermissionEnable()
        {
            if (!ForumFeature.IfEnableGroupPermission)
                ExceptionHelper.ThrowForumSettingsCloseGroupPermissionFunction();
        }
        private void CheckReputationPermissionEnble()
        {
            if (!ForumFeature.IfEnableReputationPermission)
                ExceptionHelper.ThrowForumSettingsCloseReputationPermissionFunction();
        }
        #endregion Check Permission Enables
    }
}
