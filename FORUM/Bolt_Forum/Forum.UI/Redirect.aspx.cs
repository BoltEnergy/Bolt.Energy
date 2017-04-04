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

namespace Com.Comm100.Forum.UI
{
    public partial class Redirect : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["information"] != null && Request.QueryString["dest"] != null)
            {
                string information = HttpUtility.UrlDecode(Request.QueryString["information"]);
                string dest = HttpUtility.UrlDecode(Request.QueryString["dest"]);
                string script = "<script language='javascript' type='text/javascript'>alert('" + information + "');window.location='"+dest+"';</script>";
                this.phScript.Controls.Add(new LiteralControl(script));

            }
        }
    }
}
