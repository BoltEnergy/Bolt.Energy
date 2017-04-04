
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
using Com.Comm100.Forum.Language;

namespace Forum.UI.AdminPanel.Users
{
    public partial class UserView : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        protected readonly string _AvatarsSystemFilePath = @"../../" + ConstantsHelper.ForumSystemAvatarFolder;
        protected readonly string _AvatarsFileTempPath = @"../../" + ConstantsHelper.ForumAvatarTemporaryFolder;
        private Dictionary<string, string> Genders;
        protected override void InitLanguage()
        {
            try
            {
                lblTitle.Text = Proxy[EnumText.enumForum_User_TitleUserView];
                lblSubTitle.Text = Proxy[EnumText.enumForum_User_SubtitleUserView];                
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
                this.Title = Proxy[EnumText.enumForum_User_TitleUserView];

                CheckQueryString("id");
                this.InitGenders();
                this.imgPicture.AlternateText = Proxy[EnumText.enumForum_User_FieldAvatarText];
                Bind();
                

                
                
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_User_PageUserViewErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        private void Bind()
        {
            int siteId = SiteId;
            int userId = Convert.ToInt32(Request.QueryString["id"]);
            UserOrOperator user = UserProcess.GetNotDeletedUserOrOperatorById(SiteId, userId);
            this.imgPicture.ImageUrl = @"../../" + user.Avatar;
            this.lblDisplayName.Text = System.Web.HttpUtility.HtmlEncode(user.DisplayName);
            //if (user.IfShowUserName == true)
                this.lblUserName.Text = System.Web.HttpUtility.HtmlEncode(user.FirstName) 
                                        + " " + System.Web.HttpUtility.HtmlEncode(user.LastName);
            //else
                //this.trUserName.Visible = false;
            //if (user.IfShowAge == true)
                this.lblAge.Text = user.Age.ToString();
            //else
                //this.trAge.Visible = false;
            //if (user.IfShowGender == true)
            //{
                if (user.Gender == Com.Comm100.Framework.Enum.EnumGender.Female)
                    this.lblGender.Text = this.Genders["Female"];
                else if (user.Gender == Com.Comm100.Framework.Enum.EnumGender.Male)
                    this.lblGender.Text = this.Genders["Male"];
                else
                    this.lblGender.Text = this.Genders["Itsasecret"];
            //}
            //else
                //this.trGender.Visible = false;
            //if (user.IfShowOccupation == true)
                this.lblOccupation.Text = System.Web.HttpUtility.HtmlEncode(user.Occupation);
            //else
                //this.trOccupation.Visible = false;
            //if (user.IfShowCompany == true)
                this.lblCompany.Text = System.Web.HttpUtility.HtmlEncode(user.Company);
            //else
                //this.trCompany.Visible = false;
            //if(user.IfShowPhoneNumber == true)
                this.lblPhone.Text = System.Web.HttpUtility.HtmlEncode(user.PhoneNumber);
            //else 
                //this.trPhone.Visible = false;
            //if (user.IfShowFaxNumber == true)
                this.lblFax.Text = System.Web.HttpUtility.HtmlEncode(user.FaxNumber);
            //else
                //this.trFax.Visible = false;
            //if(user.IfShowEmail == true)
                this.lblEmail.Text = System.Web.HttpUtility.HtmlEncode(user.Email);
            //else 
                //this.trEmail.Visible = false;
            //if(user.IfShowInterests == true)
                this.lblInterests.Text = System.Web.HttpUtility.HtmlEncode(user.Interests);
            //else 
                //this.trInterests.Visible = false;
            //if (user.IfShowHomePage == true)
                this.lblHomePage.Text = System.Web.HttpUtility.HtmlEncode(user.HomePage);
            //else
                //this.trHomePage.Visible = false;

                this.lblJoined.Text = DateTimeHelper.DateTransferToString(user.JoinedTime);
            this.lblLastVisit.Text = user.LastLoginTime > OriginalComparisonDate ? DateTimeHelper.DateTransferToString(user.LastLoginTime) : "";
            this.lblLastLoginIP.Text = IpHelper.LongIP2DottedIP(user.LastLoginIP);
            this.lblPosts.Text = user.NumberOfPosts.ToString();
            this.lblScore.Text = user.Score.ToString();
            //this.lblReputation.Text = user.Reputation.ToString();
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
                        reputationImage += string.Format("<img src='../../Images/user reputation.GIF' alt='{0}' title='{0}'/>",
                            user.Reputation);
                        //Proxy[EnumText.enumForum_Topic_ToolTipReputation]);
                    }
                    //(this.FindControl("PHUserReputations") as PlaceHolder).Controls.Add(
                    //    new LiteralControl(string.Format(strReputationsHtml, reputationImage)));
                    PHUserReputations.Controls.Add(new LiteralControl(string.Format(strReputationsHtml, reputationImage)));
                }
            }
        }
        private void InitGenders()
        {
            this.Genders = new Dictionary<string, string>();
            this.Genders.Add("Female", Proxy[EnumText.enumForum_User_FieldFemale]);
            this.Genders.Add("Male", Proxy[EnumText.enumForum_User_FieldMale]);
            this.Genders.Add("Itsasecret", Proxy[EnumText.enumForum_User_FieldItsasecret]);
        }
    }
}
