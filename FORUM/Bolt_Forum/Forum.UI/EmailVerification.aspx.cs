
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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI
{
    public partial class EmailVerification : Com.Comm100.Forum.UI.UIBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IfError)
            {
                try
                {
                    SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                    Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Login_BrowerTitleEmailVerification], System.Web.HttpUtility.HtmlEncode(siteSetting.SiteName));
                }
                catch (Exception exp)
                {
                    lblMessage.Text = exp.Message;
                    IfError = true;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
            ///
            ///If the operator has not moderate the user, how to tell the user to wait for moderating
            ///
            if (!this.IsPostBack)
            {
                this.phAutoSubmit.Controls.Add(new LiteralControl("<input type='submit' id='btnSubmit'  /><script language='javascript' type='text/javascript'>document.getElementById('btnSubmit').click();</script>"));
            }
            else
            {
                this.phAutoSubmit.Controls.Clear();
                if (!IfError)
                {
                    try
                    {
#if OPENSOURCE
#else
                        CheckQueryString("siteId");
#endif
                        CheckQueryString("userId");
                        CheckQueryString("email");
                        CheckQueryString("emailVerificationGuidTag");

                        int siteId = this.SiteId; //Convert.ToInt32(Request.QueryString["siteId"]);
                        int userId = Convert.ToInt32(Request.QueryString["userId"]);
                        string email = Request.QueryString["email"];
                        string emailVerificationGuidTag = Request.QueryString["emailVerificationGuidTag"];

                        #region if verified before
                        if (LoginAndRegisterProcess.IfEmailVerified(email, SiteId) == 2)
                        {
                            lblAfterEmailVerification.Text = Proxy[EnumText.enumForum_Login_MessageEmailVerificationAgain];
                            lblAfterEmailVerification.Visible = true;

                            if (LoginAndRegisterProcess.IfModerated(userId, SiteId) != 1 &&
                                LoginAndRegisterProcess.IfModerated(userId, SiteId) != 3)
                            {
                                double timezoneOffset = Convert.ToDouble(CommonFunctions.ReadCookies("TimezoneOffset"));
                                SessionUser user = new SessionUser(userId, SiteId, false, timezoneOffset, EnumApplicationType.enumForum);

                               SiteSession.CurrentUser = user;
                                LoginAndRegisterProcess.UpdateLastLoginTimeToCurrentTime(user.SiteId, user.UserOrOperatorId, user.IfOperator);

                                linkToUserControl.NavigateUrl = "~/UserPanel/UserProfileEdit.aspx?siteid=" + siteId.ToString();
                                //string applicationPath = Request.ApplicationPath.ToString();
                                //if (applicationPath == "/")
                                //{
                                //    linkToUserControl.NavigateUrl = "/UserPanel/UserProfileEdit.aspx?siteid=" + siteId.ToString();
                                //}
                                //else
                                //{
                                //    linkToUserControl.NavigateUrl = applicationPath + "/UserPanel/UserProfileEdit.aspx?siteid=" + siteId.ToString();
                                //}

                                linkToUserControl.Visible = true;
                            }
                            else
                            {
                                lblAfterEmailVerification.Text = lblAfterEmailVerification.Text + "<br/>Please wait to be moderated.";
                            }

                            return;
                        }
                        #endregion

                        LoginAndRegisterProcess.SetUserEmailVerificationStatusPassing(siteId, email, emailVerificationGuidTag);
                        
                        lblAfterEmailVerification.Text = Proxy[EnumText.enumForum_Login_MessageEmailVerificationSucceed];
                        lblAfterEmailVerification.Visible = true;

                        if (LoginAndRegisterProcess.IfModerated(userId, SiteId) != 1 &&
                            LoginAndRegisterProcess.IfModerated(userId, SiteId) != 3)
                        {
                            linkToUserControl.NavigateUrl = string.Format("UserPanel/UserProfileEdit.aspx?UserId={0}&siteid={1}", userId, siteId);
                            linkToUserControl.Visible = true;

                            double timezoneOffset = Convert.ToDouble(CommonFunctions.ReadCookies("TimezoneOffset"));
                            SessionUser user = new SessionUser(userId, SiteId, false, timezoneOffset, EnumApplicationType.enumForum);

                           SiteSession.CurrentUser = user;
                            LoginAndRegisterProcess.UpdateLastLoginTimeToCurrentTime(user.SiteId, user.UserOrOperatorId, user.IfOperator);
                        }
                        else
                        {
                            //Please wait to be moderated.
                            
                            lblAfterEmailVerification.Text = lblAfterEmailVerification.Text + Proxy[EnumText.enumForum_Login_MessageEmailVerificationWait];
                        }
                    }
                    catch (Exception exp)
                    {
                        //Sorry. Email Verification failed                  
                        lblAfterEmailVerification.Text = Proxy[EnumText.enumForum_Login_PageEmailVerificationErrorVerification];
                        lblAfterEmailVerification.Visible = true;

                        //lblMessage.Text = exp.Message;
                        IfError = true;
                        LogHelper.WriteExceptionLog(exp);
                    }
                }
            }
        }
    }
}
