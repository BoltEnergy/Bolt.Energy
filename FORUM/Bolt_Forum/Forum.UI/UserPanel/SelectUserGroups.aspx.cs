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
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.UserPanel
{
    public partial class SelectUserGroups : UIBasePage
    {
        #region Language 
        protected override void InitLanguage()
        {
            try
            {
                this.btnSelect.Text = Proxy[EnumText.enumForum_Public_ButtonOk];
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        PageInit();
                    }
                }
                catch (Exception exp)
                {
                    this.IfError = true;
                    this.lblMessage.Text = exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }

            }
        }
        private void PageInit()
        {
            InitUserGroups();
            //ForumFeature forumFeature = SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId);
            //if(forumFeature.IfEnableReputation)
            InitReputations();
        }
        private void InitUserGroups()
        {
           UserGroupWithPermissionCheck[] userGroups = UserGroupProcess.GetAllUserGroups(SiteId, UserOrOperatorId);
           this.rptUsersData.DataSource = userGroups;
           this.rptUsersData.DataBind();
        }
        private void InitReputations()
        {
           UserReputationGroupWithPermissionCheck[] reputationGroup = UserReputationGroupProcess.GetAllGroups(
               SiteId, UserOrOperatorId);
           this.rptReputationData.DataSource = reputationGroup;
           this.rptReputationData.DataBind();
        }
    }
}
