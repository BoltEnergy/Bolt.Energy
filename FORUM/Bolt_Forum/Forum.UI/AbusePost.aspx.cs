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
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Language;
using Com.Comm100.Forum.UI.Common;

namespace Com.Comm100.Forum.UI
{
    public partial class AbusePost : FrameBasePage
    {
        public int PostId 
        { 
            get 
            { 
                CheckQueryString("postId");
                return Convert.ToInt32( Request.QueryString["postId"]); 
            } 
        }
        public int TopicId
        {
            get
            { 
                CheckQueryString("topicId");
                return Convert.ToInt32( Request.QueryString["topicId"]);
            }
        }
        public int ForumId
        {
            get
            {
                return Convert.ToInt32(ForumConfig.GetInstance().ForumId);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnAbuseReason.Text = Proxy[EnumText.enumForum_AbusePost_ButtonSubmit];
        }

        /*Abuse Reason*/
        protected void btnAbuseReasonSubmit_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    int postId = PostId;

                    AbuseProcess.AddAbuseOfPost(this.UserOrOperatorId, SiteId,ForumId, postId,
                        this.UserOrOperatorId, this.taAbuseReason.Text);
                    //RefreshData();
                    this.taAbuseReason.Text = "";//clear data
                    string js = string.Format("window.parent.location = 'topic.aspx?siteId={0}&topicId={1}&forumId={2}&postId={3}&goToPost=true&a=1#Post{3}';"
                        ,SiteId,TopicId,ForumId,postId);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "",
                   "<script language='javascript' type='text/javascript'>closeWindow();" + js + "</script>");
                }
                //catch (ExceptionWithCode)
                //{
                //    string exceptionMethod = Proxy[EnumText.enumForum_Topic_ErrorHeaderAddingPost];
                //    HandleExceptionWithCode(exp, exceptionMethod);
                //}
                catch (Exception exp1)
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Topic_PageTopicErrorAddPost] + exp1.Message + "\");</script>");
                    lblMessage.Text = Proxy[EnumText.enumForum_AbusePost_ErrorAbusePost] + exp1.Message;
                    LogHelper.WriteExceptionLog(exp1);
                    this.IfError = true;
                }
            }
        }
    }
}
