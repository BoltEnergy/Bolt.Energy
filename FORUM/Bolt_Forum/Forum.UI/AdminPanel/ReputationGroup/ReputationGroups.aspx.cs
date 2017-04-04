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
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.ReputationGroup
{
    public partial class ReputationGroups : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        protected string IfPermissionDisplay{
            get
            {
                string display = string .Empty;
                ForumFeature forumFeature=SettingsProcess.GetForumFeature(SiteId,UserOrOperatorId);
                if (!forumFeature.IfEnableReputationPermission)
                    display = "display:none;";
                return display;
            }
        }
        protected override void InitLanguage()
        {
            try
            {
                btnNewReputation1.Text = Proxy[EnumText.enumForum_Reputation_ButtonNewReputation];
                btnNewReputation2.Text = Proxy[EnumText.enumForum_Reputation_ButtonNewReputation];
                lblTitle.Text = Proxy[EnumText.enumForum_Reputation_TitleReputationGroups];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Reputation_SubtitleReputationGroups];
                
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
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumReputationGroups);
                Master.Page.Title = Proxy[EnumText.enumForum_Reputation_TitleReputationGroups];
                CheckReputationFunction();
                if (!IsPostBack)
                {
                    bind();
                }
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Reputation_PageReputationGroupsErrorLoad] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }
        protected void CheckReputationFunction()
        {
            ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(this.SiteId, this.UserOrOperatorId);
            if (!forumFeature.IfEnableReputation)
                ExceptionHelper.ThrowForumSettingsCloseReputationFunctio();
        }
        void bind()
        {
            this.rpGroups.DataSource = UserReputationGroupProcess.GetAllGroups(SiteId, CurrentUserOrOperator.UserOrOperatorId);
            this.rpGroups.DataBind();
        }

        protected void btnNewReputation_OnClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/adminpanel/ReputationGroup/AddReputationGroup.aspx?SiteId=" + SiteId, false);
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_RedirectError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }

        protected void ibtnDelete_OnClick(object sender, EventArgs e)
        {
            try
            {
                int groupid = int.Parse((sender as ImageButton).CommandArgument);
                UserReputationGroupProcess.Delete(groupid, SiteId, CurrentUserOrOperator.UserOrOperatorId);
                this.bind();
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Reputation_PageReputationGroupsErrorDelete] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }
        protected void rpGroups_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                switch (e.Item.ItemType)
                {
                    case ListItemType.Item:
                    case ListItemType.AlternatingItem:
                        {
                            Label lblImages = e.Item.FindControl("lblImages") as Label;

                            int count = int.Parse(lblImages.Text);

                            lblImages.Text = string.Empty;

                            for (int i = 0; i < count; i++)
                            {
                                lblImages.Text += @"<img src='../../Images/user reputation.GIF' />";
                            }

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Reputation_PageReputationGroupsErrorLoad] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }
    }
}
