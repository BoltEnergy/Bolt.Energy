
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
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using System.IO;
using Com.Comm100.Forum.Language;

namespace Forum.UI
{
    public partial class User_Profile : Com.Comm100.Forum.UI.UIBasePage
    {
        public int UserId { get { CheckQueryString("userId"); return Convert.ToInt32(Request.QueryString["userId"]); } }
        protected readonly string _AvatarsSystemFilePath = ConstantsHelper.ForumSystemAvatarFolder;
        protected readonly string _AvatarsFileTempPath = ConstantsHelper.ForumAvatarTemporaryFolder;
        private Dictionary<string, string> Genders;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IfError == true) return;
            try
            {
                SiteSetting tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, this.UserOrOperatorId);
                Master.Page.Title = string.Format(Proxy[EnumText.enumForum_User_BroswerTitleUserProfile], Server.HtmlEncode(tmpSiteSetting.SiteName.ToString()));

                CheckQueryString("userId");
                int id = Convert.ToInt32(Request.QueryString["userId"]);
                SessionUser sessionUser = this.CurrentUserOrOperator;
                int siteId = SiteId;

                this.InitGenders();

                this.imgPicture.AlternateText = Proxy[EnumText.enumForum_User_FieldAvatarText];

                UserOrOperator user = UserProcess.GetNotDeletedUserOrOperatorById(siteId, id);
                //if (!user.IfCustomizeAvatar)
                //{
                //    this.imgPicture.ImageUrl = _AvatarsSystemFilePath + user.;
                //    //{? "images/SystemAvatar/" + Eval("PostUserOrOperatorSystemAvatar") 
                //    //    : "Temp/Avatar/" + Eval("PostUserOrOperatorCustomizeAvatar")) + "'";
                //}
                //else
                //{
 
                //}
                this.imgPicture.ImageUrl = user.Avatar;//this._AvatarsFileTempPath +  @"/" + 
                #region Data Filled
                this.lblDisplayName.Text = System.Web.HttpUtility.HtmlEncode(this.ReplaceProhibitedWords(user.DisplayName));
                if (user.IfShowUserName == true || IfShowInfor())
                    this.lblUserName.Text = System.Web.HttpUtility.HtmlEncode(this.ReplaceProhibitedWords(user.FirstName))
                        + " " + System.Web.HttpUtility.HtmlEncode(this.ReplaceProhibitedWords(user.LastName));
                else
                    this.trUserName.Visible = false;
                if (user.IfShowAge == true || IfShowInfor())
                    this.lblAge.Text = user.Age.ToString();
                else
                    this.trAge.Visible = false;
                if (user.IfShowGender == true || IfShowInfor())
                {
                    if (user.Gender == Com.Comm100.Framework.Enum.EnumGender.Female)
                        this.lblGender.Text = this.Genders["Female"];
                    else if (user.Gender == Com.Comm100.Framework.Enum.EnumGender.Male)
                        this.lblGender.Text = this.Genders["Male"];
                    else
                        this.lblGender.Text = this.Genders["Itsasecret"];
                }
                else
                    this.trGender.Visible = false;
                if (user.IfShowOccupation == true || IfShowInfor())
                    this.lblOccupation.Text = System.Web.HttpUtility.HtmlEncode(this.ReplaceProhibitedWords(user.Occupation));
                else
                    this.trOccupation.Visible = false;
                if (user.IfShowCompany == true || IfShowInfor())
                    this.lblCompany.Text = System.Web.HttpUtility.HtmlEncode(this.ReplaceProhibitedWords(user.Company));
                else
                    this.trCompany.Visible = false;
                if (user.IfShowPhoneNumber == true || IfShowInfor())
                    this.lblPhoneNumber.Text = System.Web.HttpUtility.HtmlEncode(this.ReplaceProhibitedWords(user.PhoneNumber));
                else
                    this.trPhoneNumber.Visible = false;
                if (user.IfShowFaxNumber == true || IfShowInfor())
                    this.lblFaxNumber.Text = System.Web.HttpUtility.HtmlEncode(this.ReplaceProhibitedWords(user.FaxNumber));
                else
                    this.trFaxNumber.Visible = false;
                if (user.IfShowEmail == true || IfShowInfor())
                    this.lblEmail.Text = System.Web.HttpUtility.HtmlEncode(this.ReplaceProhibitedWords(user.Email));
                else
                    this.trEmail.Visible = false;
                if (user.IfShowInterests == true || IfShowInfor())
                    this.lblInterests.Text = System.Web.HttpUtility.HtmlEncode(this.ReplaceProhibitedWords(user.Interests));
                else
                    this.trInterests.Visible = false;
                if (user.IfShowHomePage == true || IfShowInfor())
                    this.lblHomePage.Text = System.Web.HttpUtility.HtmlEncode(this.ReplaceProhibitedWords(user.HomePage));
                else
                    this.trHomePage.Visible = false;

                this.lblJoined.Text = DateTimeHelper.DateFormate(user.JoinedTime);
                this.lblLastVisit.Text = user.LastLoginTime > OriginalComparisonDate ? DateTimeHelper.DateFormate(user.LastLoginTime) : "";
                this.lblPosts.Text = user.NumberOfPosts.ToString();
                this.lblScore.Text = user.Score.ToString();
                //this.lblRepuation.Text = user.Reputation.ToString();
                /*****Reputation 2.0*****/
                //ForumFeature feature = SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId);
                ForumFeature sitesetting = SettingsProcess.GetForumFeature(siteId, this.UserOrOperatorId);
                if (sitesetting.IfEnableReputation && !user.IfDeleted)
                {
                    UserReputationGroupWithPermissionCheck group = UserReputationGroupProcess.GetReputationGroupByUserOrOperatorId(
                        SiteId, user.Id);
                    if (group == null)
                    {
                        string strReputationHtml = string.Format("<p>{0}</p>", user.Reputation);
                        //(this.FindControl("PHUserReputations") as PlaceHolder).Controls.Add(
                        //    new LiteralControl(strReputationHtml));
                        PHUserReputations.Controls.Add(new LiteralControl(strReputationHtml));
                    }
                    else
                    {
                        string strReputationsHtml = "<p>{0}</p>";
                        string reputationImage = "";
                        for (int i = 0; i < group.IcoRepeat; i++)
                        {
                            reputationImage += string.Format("<img src='" + this.ImagePath + "/reputation.gif' alt='{0}' title='{0}'/>",
                                user.Reputation);
                            //Proxy[EnumText.enumForum_Topic_ToolTipReputation]);
                        }
                        //(this.FindControl("PHUserReputations") as PlaceHolder).Controls.Add(
                        //    new LiteralControl(string.Format(strReputationsHtml, reputationImage)));
                        PHUserReputations.Controls.Add(new LiteralControl(string.Format(strReputationsHtml, reputationImage)));
                    }
                }


                #endregion

                /*2.0*/
                /*Ban User Link Image*/
                
                if (!this.IfGuest && sitesetting.IfEnableMessage && !user.IfDeleted)
                {
                    linkSendUser.Visible = true;
                    linkSendUser.HRef = "javascript:SetSendMessageToUserId('" + user.Id + "');showWindow('divSendMessageToUser','divThickOuter');";
                    linkSendUser.Title = Proxy[EnumText.enumForum_Topic_ToolTipSendMessage];
                }
                if (!sitesetting.IfEnableScore)
                {
                    lblScore.Visible = false;
                    trScore.Visible = false;
                }
                if (!sitesetting.IfEnableReputation)
                {
                    trReputation.Visible = false;
                }
                if (IfAdmin())
                {
                    lbtnUserPosts.Visible = true;
                    lbtnUserPosts.ForeColor = System.Drawing.Color.Blue;
                    lbtnUserPosts.PostBackUrl = string.Format("User_Posts.aspx?userId={0}&siteId={1}", user.Id, siteId);
                }
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_User_PageUserProfileErrorLoading] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
                this.IfError = true;
            }
        }
        private void InitGenders()
        {
            this.Genders = new Dictionary<string, string>();
            this.Genders.Add("Female", Proxy[EnumText.enumForum_User_FieldFemale]);
            this.Genders.Add("Male", Proxy[EnumText.enumForum_User_FieldMale]);
            this.Genders.Add("Itsasecret", Proxy[EnumText.enumForum_User_FieldItsasecret]);
        }

        private bool IfShowInfor()
        {
            return IfAdmin();
        }
    }
}
