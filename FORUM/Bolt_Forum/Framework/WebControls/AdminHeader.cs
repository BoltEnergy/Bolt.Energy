#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif

/*
 * Name:         AdminHeader
 * Version:         1.0
 * Description:  Admin Page Header WebControl
 * Copyright:    Copyright(c) 2009 Comm100.
 *  Create:       Elei 2009-7-1 Version 1.0
 */
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Framework.Database;
using System.Web.Configuration;
using System.Web;
using Com.Comm100.Framework.Language;
using Com.Comm100.Language;

[assembly: TagPrefix("Com.Comm100.Framework.WebContols", "CWC")]
namespace Com.Comm100.Framework.WebControls
{
    [DefaultProperty("")]
    [ToolboxData("<{0}:AdminHeader runat=server></{0}:AdminHeader>")]
    public class AdminHeader : Control, INamingContainer
    {
        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]

        private readonly string ChangePasswordUrl = string.Format("{0}/AdminPanel/ChangePassword.aspx",System.Web.Configuration.WebConfigurationManager.AppSettings["AdminUrl"]);
        private readonly string LoginUrl = "~/Login.aspx";

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            string targetUrl = GetApplicationLoginUrl();

            //HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Remove("CurrentUser");

            if (HttpContext.Current.Request.Cookies["IfLogined"] != null)
                HttpContext.Current.Response.Cookies["IfLogined"].Expires = DateTime.Now.AddDays(-1);
            if (HttpContext.Current.Request.Cookies["Password"] != null)
                HttpContext.Current.Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

            HttpContext.Current.Response.Redirect(targetUrl);
        }

        protected string GetApplicationLoginUrl()
        {
            SessionUser currentUser = (SessionUser)HttpContext.Current.Session["CurrentUser"];

            if (currentUser != null)
            {
                int siteId = currentUser.SiteId;

                switch (_applicationType)
                {
                    case EnumApplicationType.enumLiveChat:
                        return LoginUrl;
                    case EnumApplicationType.enumForum:
                        return string.Format("{0}?siteid={1}", LoginUrl, siteId.ToString());
                    case EnumApplicationType.enumNewsletter:
                        return LoginUrl;
                    case EnumApplicationType.enumKnowledgeBase:
                        return string.Format("{0}?siteId={1}", LoginUrl, siteId.ToString());
                    default:
                        return LoginUrl;
                }
            }
            else
                return LoginUrl;
        }
        private EnumApplicationType _applicationType = EnumApplicationType.enumAdmin;
        public EnumApplicationType ApplicationType
        {
            set { _applicationType = value; }
        }
        private string GetOperatorName(SessionUser currentUser)
        {
            SqlConnectionWithSiteId conn = null;
            try
            {
                conn = DbHelper.GetSqlConnection(currentUser.SiteId);
                DbHelper.OpenConn(conn);

                string strSQL;
                if (currentUser.SiteId == 0)    
                {
                    strSQL = "select Name from t_User0 where id=@id";
                }
                else
                {
                    strSQL = "select Name from t_User" + currentUser.SiteId.ToString() + " where id=@id";
                }

                SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn);
                cmd.Parameters.AddWithValue("@id", currentUser.UserOrOperatorId);

                return Convert.ToString(cmd.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            SessionUser currentUser = (SessionUser)HttpContext.Current.Session["CurrentUser"];

            if (currentUser != null)
            {
                LanguageProxy languageProxy = new LanguageProxy();
                string textChangePassword = languageProxy[EnumText.enumPublic_ChangePassword];
                string textLogout = languageProxy[EnumText.enumPublic_Logout];
                string textComm100TagLine = languageProxy[EnumText.enumPublic_Comm100TagLine];
                string textSiteId = "";
                if (currentUser.CurrentApplicationType == EnumApplicationType.enumPartner)
                    textSiteId = languageProxy[EnumText.enumPublic_PartnerId];
                else
                    textSiteId = languageProxy[EnumText.enumPublic_SiteId];
                string textCurrentOperator = languageProxy[EnumText.enumPublic_CurrentUser];

                Controls.Add(new LiteralControl("<div>"));
                Controls.Add(new LiteralControl("<div style='float:right;padding-right:5px'>"));
                Controls.Add(new LiteralControl("<ul style=\"margin: 0 0;padding:0 0;font-size: 1.1em;display: inline;width:320px;_width:310px;text-align:right;\">"));
                //Controls.Add(new LiteralControl("<ul style=\"float: right;clear: both;margin-right: 10px;font-size: 1.1em;display: inline;width:320px;_width:310px;text-align:right;\">"));
                Controls.Add(new LiteralControl("<li style=\"text-align:right;\">"));
                if (currentUser.SiteId == 0)
                {
                    string appPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                    if (appPath != "/")
                    {
                        appPath += "/";
                    }
                    Controls.Add(new LiteralControl("<a href=\"" + appPath + "UserPanel/UserPasswordEdit.aspx?siteId=0\">" + textChangePassword + "</a>"));

                }
                else
                {
                    string url = "";
                    if (currentUser.IfOperator)
                        url = ChangePasswordUrl;
                    else
                    {
                        string appPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                        if (appPath != "/")
                        {
                            appPath += "/";
                        }
                        url = appPath + "UserPanel/UserPasswordEdit.aspx";
                    }
                    Controls.Add(new LiteralControl("<a href=\"" + url + "?siteid=" + currentUser.SiteId + "\">" + textChangePassword + "</a>"));
                }
                Controls.Add(new LiteralControl(" | "));
                LinkButton lbtnLogout = new LinkButton();
                lbtnLogout.CausesValidation = false;
                lbtnLogout.Text = textLogout;
                lbtnLogout.Click += new EventHandler(lbtnLogout_Click);
                Controls.Add(lbtnLogout);
                Controls.Add(new LiteralControl("</li>"));
                Controls.Add(new LiteralControl("<li style='_width:310px;overflow:hidden'>"));
                if (currentUser.SiteId > 0)    
                {
                    Controls.Add(new LiteralControl("<span> " + textSiteId + " </span>"));
                    Controls.Add(new LiteralControl("<span>" + currentUser.SiteId.ToString() + "</span> | "));
                }
                Controls.Add(new LiteralControl("<span> "+textCurrentOperator+" </span>"));
                Controls.Add(new LiteralControl("<span>" + System.Web.HttpUtility.HtmlEncode(GetOperatorName(currentUser)) + "</span>"));
                Controls.Add(new LiteralControl("</li>"));
                Controls.Add(new LiteralControl("</ul>"));
                Controls.Add(new LiteralControl("</div>"));
                Controls.Add(new LiteralControl("<div style='float:left;padding-left:10px'>"));
                Controls.Add(new LiteralControl("<ul style=\"margin:0 0;padding:0 0\">"));
                string path = HttpContext.Current.Request.ApplicationPath == "/" ? "" : HttpContext.Current.Request.ApplicationPath;
                Controls.Add(new LiteralControl("<li style=\"white-space:nowrap\"><a href="
                    + System.Web.Configuration.WebConfigurationManager.AppSettings["WebSiteUrl"]
                    +" target=\"_blank\"><img src=\"" + path + "/images/comm100.gif\" alt=\"Comm100\"  /></a><span style=\"font-size: 1.3em;font-weight: bold;vertical-align:bottom\" >&nbsp;&nbsp;"+textComm100TagLine+"</span></li>"));
                Controls.Add(new LiteralControl("</ul>"));
                Controls.Add(new LiteralControl("</div>"));
                Controls.Add(new LiteralControl("<div style='clear:both;height:1px;overflow:hidden;font-size:1px'></div>"));
                Controls.Add(new LiteralControl("</div>"));
            }
            else
                HttpContext.Current.Response.Redirect(LoginUrl);
        }

        protected override void Render(HtmlTextWriter output)
        {
            base.Render(output);
        }
    }
}
