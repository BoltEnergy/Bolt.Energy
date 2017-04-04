#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
/*
 * Name:         AdminFooter
 * Version:         1.0
 * Description:  Admin Page Footer WebControl
 * Copyright:    Copyright(c) 2009 Comm100.
 *  Create:       Elei 2009-7-1 Version 1.0
 */

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Com.Comm100.Framework.Language;
using Com.Comm100.Language;

[assembly: TagPrefix("Com.Comm100.Framework.WebContols", "CWC")]
namespace Com.Comm100.Framework.WebControls
{
    [DefaultProperty("")]
    [ToolboxData("<{0}:AdminFooter runat=server></{0}:AdminFooter>")]
    public class AdminFooter : Control, INamingContainer
    {
        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            LanguageProxy languageProxy = new LanguageProxy();
            string textCopyright = languageProxy[EnumText.enumPublic_Copyright];
            Controls.Add(new LiteralControl("<div class=\"divFooter\"><div>"+textCopyright+"</div></div>"));  //Copyright ©2009 <a href=\"http://www.comm100.com\" target=\"_blank\">Comm100</a>
        }

        protected override void Render(HtmlTextWriter output)
        {
            base.Render(output);
        }
    }
}
