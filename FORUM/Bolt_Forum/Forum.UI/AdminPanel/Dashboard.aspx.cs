
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
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel
{
    public partial class Dashboard : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((AdminMasterPage)this.Master).JsFilePath = "../";
            Master.Page.Title = Proxy[EnumText.enumForum_Dashboard_Title];

            ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumForumDashboard);

            try
            {
#if OPENSOURCE
                panelContent.Visible = false;

#else
                panelIframe.Visible = false;
                string news = Com.Comm100.Framework.Common.WebServiceHelper.GetComm100News(Com.Comm100.Framework.Enum.EnumApplicationType.enumForum);
                this.phNews.Controls.Add(new LiteralControl(news.ToString()));
#endif
            }
            catch
            {

            }
        }

        
    }
}
