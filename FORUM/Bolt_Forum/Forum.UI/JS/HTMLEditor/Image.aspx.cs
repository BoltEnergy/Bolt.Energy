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
using Com.Comm100.Forum.UI.Common;

namespace Com.Comm100.Forum.UI.HTMLEditor
{
    public partial class Image : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
#if OPENSOURCE
#else
                WebUtility.CheckQueryString("siteid");
#endif
                WebUtility.CheckQueryString("id");
                
#if OPENSOURCE
                int siteId = 0;
#else
                int siteId = Convert.ToInt32(Request.QueryString["siteid"]);
#endif
                int id = Convert.ToInt32(Request.QueryString["id"]);

                PostImageWithPermissionCheck postImage = PostImageProcess.GetPostImageById(siteId,0,false,id);

                if (postImage != null && postImage.File.Length > 0)
                {
                    Response.ClearContent();
                    Response.ContentType = postImage.Type;
                    Response.BinaryWrite(postImage.File);
                }
            }
            catch
            {
            }

        }
    }
}
