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
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.UI.Handler
{
    public partial class DownAttachment : UIBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    CheckQueryString("AttachId");
                    int attchmentId = Convert.ToInt32(Request.QueryString["AttachId"]);
                   
                    AttachmentWithPermissionCheck attachment;
                    byte[] buff = AttachmentProcess.GetDownloadFileOfAttachment(UserOrOperatorId, SiteId, attchmentId,out attachment);
                    this.DownLoadFile(attachment.Name, buff, buff.Length);
                    
                }
                catch (Exception exp)
                {
                    Response.Write("Download Attachment Error:" + exp.Message);
                    LogHelper.WriteExceptionLog(exp);
                }
            }
            catch (Exception exp)
            {
                LogHelper.WriteExceptionLog(exp);
            }
        }

        private void DownLoadFile(string FileName, byte[] buff, long Length)
        {
            Response.Buffer = false;
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Connection", "Keep-Alive");
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            Response.AddHeader("Content-Length", Length.ToString());
            Response.Buffer = true;
            Response.Expires = 0;
            Response.BinaryWrite(buff);
            //Response.Output.Write(buff);
            Response.Flush();
            Response.End();
        }
    }
}
