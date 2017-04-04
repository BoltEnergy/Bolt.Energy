
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
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Language;

namespace Forum.UI
{
    public partial class Post_Register : Com.Comm100.Forum.UI.UIBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["step"] != null && Request.QueryString["step"] == "3")
            {
                if (!IfError)
                {
                    try
                    {
                        linkBack.Text = Proxy[EnumText.enumForum_Register_LinkBackToLastPage];
                        linkHomePage.Text = Proxy[EnumText.enumForum_Register_LinkHomePage];
                        linkUserPanel.Text = Proxy[EnumText.enumForum_Register_LinkUserControlPanel];
                        linkHomePage.NavigateUrl = "~/default.aspx";
                        //linkHomePage.NavigateUrl = "~/Default.aspx?siteid=" + this.SiteId;
                        
                        SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                        Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Register_PageTitlePreRegister], System.Web.HttpUtility.HtmlEncode(siteSetting.SiteName));
                    }
                    catch (Exception exp)
                    {
                        lblMessage.Text = Proxy[EnumText.enumForum_Register_PagePostRegisterErrorLoad] + exp.Message;
                        IfError = true;
                        LogHelper.WriteExceptionLog(exp);
                    }
                }

                if (!IfError)
                {
                    try
                    {
                        if (!IsPostBack)
                        {
                            //if (Request.UrlReferrer == null)
                            //{
                            //    Response.Redirect("Pre_Register.aspx?siteid=" + SiteId, false);
                            //}

                            string urlReferrer = "~/default.aspx";


                            if (Request.QueryString["UrlReferrer"] != null)
                            {
                                urlReferrer = Request.QueryString["UrlReferrer"];
                                string tempUrlReferrer = urlReferrer.ToLower();
                                if (!Com.Comm100.Forum.UI.Common.WebUtility.CanReturnPreUrl(tempUrlReferrer))
                                {
                                    tempUrlReferrer = this.UrlWithAuthorityAndApplicationPath + "Default.aspx?siteid=" + this.SiteId;
                                }

                                linkBack.NavigateUrl = "default.aspx";//tempUrlReferrer;
                            }
                            else
                            {
                                linkBack.NavigateUrl = "Default.aspx?siteid=" + SiteId;
                            }

                            string ifModerateNewUser = Server.UrlDecode(Request.QueryString["IfModerateNewUser"]);
                            string ifVerifyEmail = Server.UrlDecode(Request.QueryString["IfVerifyEmail"]);

                            bool ifModerate = false;
                            bool ifEmail = false;

                            if (ifModerateNewUser == "1")
                            {
                                ifModerate = true;
                            }
                            else
                            {
                                ifModerate = false;
                            }

                            if (ifVerifyEmail == "1")
                            {
                                ifEmail = true;
                            }
                            else
                            {
                                ifEmail = false;
                            }

                            if (ifModerate == false && ifEmail == false)
                            {
                                LabelSuccess.Text = GetGeetingMessage();//Proxy[EnumText.enumForum_Register_StateSuccess];
                                lblAfterRegister.Text = Proxy[EnumText.enumForum_Register_ContentThanks];

                                linkUserPanel.NavigateUrl = "~/UserPanel/UserProfileEdit.aspx?siteid=" + SiteId;


                                linkUserPanel.Visible = true;
                                lblWait.Text = Proxy[EnumText.enumForum_Register_LinkJump];
                                string script = string.Format(@"<script>setTimeout(""window.location = '{0}'"",5000)</script>", linkBack.NavigateUrl);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
                            }
                            else if (ifModerate == false && ifEmail == true)
                            {

                                lblAfterRegister.Text = Proxy[EnumText.enumForum_Register_ContentEmail];
                            }
                            else if (ifModerate == true && ifEmail == false)
                            {
                                LabelSuccess.Text = Proxy[EnumText.enumForum_Register_StateWait];
                                lblAfterRegister.Text = "";
                            }
                            else
                            {
                                LabelSuccess.Text = Proxy[EnumText.enumForum_Register_StateWait];
                                lblAfterRegister.Text = Proxy[EnumText.enumForum_Register_ContentEmailAndModerated];
                            }

                        }
                    }
                    catch (Exception exp)
                    {
                        lblMessage.Text = exp.Message;
                        IfError = true;
                        LogHelper.WriteExceptionLog(exp);
                    }
                }
            }
            else
            {
                Response.Redirect("~/Pre_Register.aspx?siteid=" + SiteId);
            }
        }

        /*Gavin 2.0*/
        private string GetGeetingMessage()
        {
            RegistrationSettingWithPermissionCheck registrationSetting = SettingsProcess.GetRegistrationSettingBySiteId(
                UserOrOperatorId, SiteId);
            return registrationSetting.GreetingMessage;
        }
    }
}
