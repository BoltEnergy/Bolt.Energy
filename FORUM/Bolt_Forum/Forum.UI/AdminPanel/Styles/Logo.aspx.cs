
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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.UI.AdminPanel;
using System.IO;

namespace Com.Comm100.Forum.UI.AdminPanel.Styles
{
    public partial class Logo : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.BufferOutput = true;
            Response.Expires = 0;
            Response.ExpiresAbsolute = DateTime.UtcNow.AddMinutes(-1);
            Response.CacheControl = "no-cache";

            StyleSettingWithPermissionCheck tmpStyleSetting = StyleProcess.GetStyleSettingBySiteId(this.SiteId, this.UserOrOperatorId);

            if ((tmpStyleSetting.CustomizeLogo != null) && (tmpStyleSetting.CustomizeLogo.Length >0))
            {
                Response.ClearContent();
                Response.ContentType = "image/gif";
                Response.BinaryWrite(tmpStyleSetting.CustomizeLogo);
            }
        }
    }
}
