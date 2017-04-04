
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
using Com.Comm100.Forum.Language;
namespace Forum.UI
{
    public partial class Pre_Register : Com.Comm100.Forum.UI.UIBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string urlReferrer = "";

            if (!IfError)
            {
                try
                {
                    SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                    string siteName = System.Web.HttpUtility.HtmlEncode(siteSetting.SiteName);
                    btnToRegister.Text = Proxy[EnumText.enumForum_Register_ButtonNext];
                    Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Register_PageTitlePreRegister], siteName);
                    //string content = "";
                    RegistrationSettingWithPermissionCheck register = SettingsProcess.GetRegistrationSettingBySiteId(
                        this.UserOrOperatorId,this.SiteId);                    
                    lblContent.Text = register.Agreement;

                    if (!IsPostBack)
                    {
                        if (Request.UrlReferrer != null)
                        {
                            urlReferrer = Request.UrlReferrer.OriginalString;
                            //Session["UrlReferrer"] = urlReferrer;
                            ViewState["UrlReferrer"] = urlReferrer;
                        }
                        //lblContent.Text = content;
                    }
                }
                catch (Exception exp)
                {

                    lblMessage.Text = Proxy[EnumText.enumForum_Register_PagePreRegisterError] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);

                    IfError = true;
                }
            }
        }

        protected void btnToRegister_Click(object sender, EventArgs e)
        {
            //string applicationPath = Request.ApplicationPath;

            string urlReferrer = "";
            if (ViewState["UrlReferrer"] != null)
            {
                urlReferrer = ViewState["UrlReferrer"].ToString();
            }

            //Response.Redirect(string.Format("~/Register.aspx?UrlReferrer={0}&siteid={1}", Server.UrlEncode(urlReferrer), SiteId), false);
            Server.Transfer(string.Format("~/Register.aspx?UrlReferrer={0}&siteid={1}", Server.UrlEncode(urlReferrer), SiteId), true);

            //if (applicationPath == "/")
            //{
            //    Response.Redirect(string.Format("/Register.aspx?UrlReferrer={0}&siteid={1}", Server.UrlEncode(urlReferrer), SiteId), false);
            //}
            //else
            //{
            //    Response.Redirect(applicationPath + string.Format("/Register.aspx?UrlReferrer={0}&siteid={1}", Server.UrlEncode(urlReferrer), SiteId), false);
            //}
        }
    }
}
