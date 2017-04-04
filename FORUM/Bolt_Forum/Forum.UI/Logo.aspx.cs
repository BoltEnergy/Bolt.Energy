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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;

namespace Com.Comm100.Forum.UI
{
    public partial class Logo : Com.Comm100.Forum.UI.UIBasePage
    {
        public override bool IfValidateForumClosed
        {
            get
            {
                return false;
            }
        }
        public override bool IfValidateIPBanned
        {
            get
            {
                return false;
            }
        }
        public override bool IfValidateUserBanned
        {
            get
            {
                return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            StyleSettingWithPermissionCheck styleSetting = StyleProcess.GetStyleSettingBySiteId(this.SiteId, this.UserOrOperatorId);

            if ((styleSetting.CustomizeLogo != null) && (styleSetting.CustomizeLogo.Length > 0))
            {
                Response.ClearContent();
                Response.ContentType = "image/gif";
                Response.BinaryWrite(styleSetting.CustomizeLogo);
            }
        }
    }
}
