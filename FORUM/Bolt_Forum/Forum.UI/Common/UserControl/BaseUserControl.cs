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
using Com.Comm100.Forum.Language;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;

namespace Com.Comm100.Forum.UI.UserControl
{
    public class BaseUserControl : System.Web.UI.UserControl
    {
        public LanguageProxy Proxy
        {
            get { return ((Com.Comm100.Forum.UI.UIBasePage)this.Page).Proxy; }
        }
        
    }
}
