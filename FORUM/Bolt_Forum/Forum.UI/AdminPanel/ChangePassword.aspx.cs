﻿
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

namespace Com.Comm100.Forum.UI.AdminPanel
{
    public partial class ChangePassword : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((AdminMasterPage)this.Master).JsFilePath = "../";

            Response.Redirect("~/UserPanel/UserPasswordEdit.aspx?siteid=" + this.SiteId, false);
        }
    }
}
