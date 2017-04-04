#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif

/*
 * Name:         AdminNavBar
 * Version:         1.0
 * Description:  Navigation Bar WebControl
 * Copyright:    Copyright(c) 2009 Comm100.
 *  Create:       Elei 2009-7-1 Version 1.0
 */

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Framework.Language;
using Com.Comm100.Language;

[assembly: TagPrefix("Com.Comm100.Framework.WebContols", "CWC")]
namespace Com.Comm100.Framework.WebControls
{
    [DefaultProperty("")]
    [ToolboxData("<{0}:AdminNavBar runat=server></{0}:AdminNavBar>")]
    public class AdminNavBar : Control, INamingContainer
    {
        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]

        private readonly string liveChatUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["LiveChatUrl"] + "/AdminPanel/Dashboard.aspx";
        private readonly string forumUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["ForumUrl"] + "/AdminPanel/Dashboard.aspx";
        private readonly string adminUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["AdminUrl"] + "/AdminPanel/Dashboard.aspx";
        private readonly string newsletterUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["NewsletterUrl"] + "/AdminConsole/Dashboard.aspx";
        private readonly string knowledgeBaseUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["KnowledgeBaseUrl"] + "/AdminPanel/Dashboard.aspx";
        private readonly string emailTicketUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailTicketUrl"] + "/AdminPanel/Dashboard.aspx";
        private readonly string partnerPortalUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["PartnerPortalUrl"] + "/AdminPanel/PartnerSignUpInPortal.aspx";

        private EnumApplicationType _applicationType = EnumApplicationType.enumAdmin;
        public EnumApplicationType ApplicationType
        {
            set { _applicationType = value; }
        }
        private int _applicationTypes = 0;
        public int ApplicationTypes
        {
            set { _applicationTypes = value; }
        }
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            SessionUser currentUser = (SessionUser)System.Web.HttpContext.Current.Session["CurrentUser"];

            if (currentUser != null)
            {
                if (currentUser.SiteId > 0) 
                {
                    if (currentUser.IfOperator)
                    {
                        LanguageProxy languageProxy = new LanguageProxy();
                        string textAdmin = languageProxy[EnumText.enumPublic_Admin];
                        string textLiveChat = languageProxy[EnumText.enumPublic_LiveChat];
                        string textForum = languageProxy[EnumText.enumPublic_Forum];
                        string textNewsletter = languageProxy[EnumText.enumPublic_Newsletter];
                        string textKnowledgeBase = languageProxy[EnumText.enumPublic_KnowledgeBase];
                        string textEmailTicket = languageProxy[EnumText.enumPublic_EmailTicket];
                        string textPartner = languageProxy[EnumText.enumPublic_Partner];

                        Controls.Add(new LiteralControl("<div id=\"divTab\">"));
                        Controls.Add(new LiteralControl("<ul style='margin:0 0;padding:0 0'>"));
                        string path = System.Web.HttpContext.Current.Request.ApplicationPath == "/" ? "" : System.Web.HttpContext.Current.Request.ApplicationPath;

                        Controls.Add(new LiteralControl("<li><img src=\"" + path + "/images/before-tag.gif\" style=\"vertical-align:bottom;\"/></li>"));
                        if ((_applicationTypes & Convert.ToInt32(EnumApplicationType.enumLiveChat)) == Convert.ToInt32(EnumApplicationType.enumLiveChat))
                            Controls.Add(new LiteralControl("<li><a href=\"" + liveChatUrl + "\" class=\"" + (_applicationType == EnumApplicationType.enumLiveChat ? "aSel" : "aNSel") + "\"><span class=\"" + (_applicationType == EnumApplicationType.enumLiveChat ? "spanSel" : "spanNSel") + "\">" + textLiveChat + "</span></a></li>"));
                        if ((_applicationTypes & Convert.ToInt32(EnumApplicationType.enumEmailTicket)) == Convert.ToInt32(EnumApplicationType.enumEmailTicket))
                            Controls.Add(new LiteralControl("<li><a href=\"" + emailTicketUrl + "?siteId=" + currentUser.SiteId + "\" class=\"" + (_applicationType == EnumApplicationType.enumEmailTicket ? "aSel" : "aNSel") + "\"><span class=\"" + (_applicationType == EnumApplicationType.enumEmailTicket ? "spanSel" : "spanNSel") + "\">"+textEmailTicket+"</span></a></li>"));
                        if ((_applicationTypes & Convert.ToInt32(EnumApplicationType.enumForum)) == Convert.ToInt32(EnumApplicationType.enumForum))
                            Controls.Add(new LiteralControl("<li><a href=\"" + forumUrl + "?siteId=" + currentUser.SiteId + "\" class=\"" + (_applicationType == EnumApplicationType.enumForum ? "aSel" : "aNSel") + "\"><span class=\"" + (_applicationType == EnumApplicationType.enumForum ? "spanSel" : "spanNSel") + "\">" + textForum + "</span></a></li>"));
                        if ((_applicationTypes & Convert.ToInt32(EnumApplicationType.enumNewsletter)) == Convert.ToInt32(EnumApplicationType.enumNewsletter))
                            Controls.Add(new LiteralControl("<li><a href=\"" + newsletterUrl + "\" class=\"" + (_applicationType == EnumApplicationType.enumNewsletter ? "aSel" : "aNSel") + "\"><span class=\"" + (_applicationType == EnumApplicationType.enumNewsletter ? "spanSel" : "spanNSel") + "\">" + textNewsletter + "</span></a></li>"));
                        if ((_applicationTypes & Convert.ToInt32(EnumApplicationType.enumKnowledgeBase)) == Convert.ToInt32(EnumApplicationType.enumKnowledgeBase))
                            Controls.Add(new LiteralControl("<li><a href=\"" + knowledgeBaseUrl + "?siteId=" + currentUser.SiteId + "\" class=\"" + (_applicationType == EnumApplicationType.enumKnowledgeBase ? "aSel" : "aNSel") + "\"><span class=\"" + (_applicationType == EnumApplicationType.enumKnowledgeBase ? "spanSel" : "spanNSel") + "\">" + textKnowledgeBase + "</span></a></li>"));

                        Controls.Add(new LiteralControl("<li><a href=\"" + adminUrl + "\" class=\"" + (_applicationType == EnumApplicationType.enumAdmin ? "aSel" : "aNSel") + "\"><span class=\"" + (_applicationType == EnumApplicationType.enumAdmin ? "spanSel" : "spanNSel") + "\">" + textAdmin + "</span></a></li>"));
                        Controls.Add(new LiteralControl("<li>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<img src=\"" + path + "/images/before-tag.gif\" style=\"vertical-align:bottom;\"/></li>"));
                        Controls.Add(new LiteralControl("<li><a href=\"" + partnerPortalUrl + "\" class=\"" + (_applicationType == EnumApplicationType.enumPartner ? "aSel" : "aNSel") + "\"><span class=\"" + (_applicationType == EnumApplicationType.enumPartner ? "spanSel" : "spanNSel") + "\">" + textPartner + "</span></a></li>"));
                        Controls.Add(new LiteralControl("</ul>"));
                        Controls.Add(new LiteralControl("</div>"));
                    }
                    else// if forum administrator hide links
                    {
                        Controls.Add(new LiteralControl("<div id=\"divTab\"></div>"));
                    }
                }
                Controls.Add(new LiteralControl("<div id=\"divTabLine\"></div>"));
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            base.Render(output);
        }

    }
}
