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
using Com.Comm100.Forum.Language;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.ASPNETState;

namespace Com.Comm100.Forum.UI
{
    public partial class UserPermissionView : UIBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Login();
                UserPermissionCache userPermissionList = SiteSession.UserPermissionList as UserPermissionCache;
                if (userPermissionList == null)
                {
                    this.lblError.Text = "user have no permission list";
                    return;
                }
                List<UserForumPermissionItem> items = new List<UserForumPermissionItem>();
                foreach (var o in userPermissionList.UserForumPermissionsList.Values)
                {
                    items.Add(o as UserForumPermissionItem);
                }
                this.rptData.DataSource = items;
                this.rptData.DataBind();

                //this.lb_ifAdministrator .Text= userPermissionList.IfAdministrator.ToString();
                this.lb_ifAllowCustomizeAvatar.Text = userPermissionList.IfAllowCustomizeAvatar.ToString();
                this.lb_ifAllowSearch.Text = userPermissionList.IfAllowSearch.ToString();
                this.lb_ifAllowUploadAttachment.Text = userPermissionList.IfAllowUploadAttachment.ToString();
                //this.lb_ifModerator.Text = userPermissionList.IfModerator.ToString();
                this.lb_maxCountOfAttacmentsForOnePost.Text = userPermissionList.MaxCountOfAttacmentsForOnePost.ToString();
                this.lb_maxCountOfMessageSendOneDay.Text = userPermissionList.MaxCountOfMessageSendOneDay.ToString();
                this.lb_maxLengthofSignature.Text = userPermissionList.MaxLengthofSignature.ToString();
                this.lb_maxSizeOfAllAttachments.Text = userPermissionList.MaxSizeOfAllAttachments.ToString();
                this.lb_maxSizeOfOneAttachment.Text = userPermissionList.MaxSizeOfOneAttachment.ToString();
                this.lb_minIntervalForSearch.Text = userPermissionList.MinIntervalForSearch.ToString();
                //this.lb_ifAllowHTMLSignature.Text = userPermissionList.IfSignatureAllowHTML.ToString();
                this.lb_ifAllowLinkSignature.Text = userPermissionList.IfSignatureAllowUrl.ToString();
                this.lb_ifAllowImageSignature.Text = userPermissionList.IfSignatureAllowInsertImage.ToString();
                this.lbViewForum.Text = userPermissionList.IfAllowViewForum.ToString();
                this.lbViewTopicOrPost.Text = userPermissionList.IfAllowViewTopic.ToString();
                this.lbAllowPostTopicOrPost.Text = userPermissionList.IfAllowPost.ToString();
                this.lbMinIntervalTimeForPosting.Text = userPermissionList.MinIntervalForPost.ToString();
                this.lbMaxLengthOfTopicOrPost.Text = userPermissionList.MaxLengthOfPost.ToString();
                this.lbAllowURL.Text = userPermissionList.IfAllowUrl.ToString();
                this.lbAllowInsertImage.Text = userPermissionList.IfAllowUploadImage.ToString();
                this.lbPostModerationNotRequired.Text = userPermissionList.IfPostNotNeedModeration.ToString();

                if (this.CurrentUserOrOperator != null)
                {
                    UserOrOperator userOrOperator = UserProcess.GetUserOrOpertorById(this.SiteId, this.UserOrOperatorId);

                    txtUserName.Value = userOrOperator.Email;
                    txtPassword.Value = "1";

                }
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_User_PageUserProfileErrorLoading] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }
        }
        private void Login()
        {
            string loginIp = Request.ServerVariables["remote_addr"];
            string strTimezoneOffset = CommonFunctions.ReadCookies("TimezoneOffset");
            double timezoneOffset = strTimezoneOffset.Length == 0 ? 0 : Convert.ToDouble(strTimezoneOffset);

            UserPermissionCache userPermissionList;
            SessionUser sessionUser = LoginAndRegisterProcess.UserOrOperatorLogin(
                this.SiteId, txtUserName.Value, txtPassword.Value, loginIp, timezoneOffset, false, false, out userPermissionList);
           SiteSession.CurrentUser = sessionUser;
            SiteSession.UserPermissionList = userPermissionList;

            this.CurrentUserOrOperator = sessionUser;
        }

        public string GetForumName(int forumId)
        {
            ForumWithPermissionCheck forum = ForumProcess.GetForumByForumId(this.SiteId, this.UserOrOperatorId, forumId);
            string name = "";
            if (forum.Name.Length > 20)
                name = forum.Name.Substring(0, 20) + "...";
            else
                name = forum.Name;
            return Server.HtmlEncode(name);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Login();
        }
    }
}
