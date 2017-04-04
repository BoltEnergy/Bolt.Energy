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
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using System.Collections;
using Com.Comm100.Forum.UI.AdminPanel;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.Language;

namespace Forum.UI.AdminPanel.Drafts
{
    public partial class DraftList : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        string ErrorLoad;
        string ErrorDelete;
        string ErrorEdit;
        string ErrorQuery;
        string HelpView;
        string HelpDelete;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Topic_PageDraftListErrorLoadPage];
                ErrorDelete = Proxy[EnumText.enumForum_Topic_PageDraftListErrorDelete];
                ErrorEdit = Proxy[EnumText.enumForum_Topic_PageDraftListErrorEdit];
                ErrorQuery = Proxy[EnumText.enumForum_Topic_PageDraftListErrorQuery];
                HelpView = Proxy[EnumText.enumForum_Topic_HelpView];
                HelpDelete = Proxy[EnumText.enumForum_Topic_HelpDelete];
                this.lblTitle.Text = Proxy[EnumText.enumForum_Topic_TitleDraftList];
                this.lblSubTitle.Text = Proxy[EnumText.enumForum_Topic_SubtitleDraftList];
                btnQuery1.Text = Proxy[EnumText.enumForum_Topic_ButtonQuery];
                btnQuery2.Text = Proxy[EnumText.enumForum_Topic_ButtonQuery];

            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + exp.Message;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.SetFocus(tbSubject);
                this.Page.Form.DefaultButton = btnQuery1.UniqueID;
                if (!IsPostBack)
                {
                    ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumDraftManage);
                    Master.Page.Title = Proxy[EnumText.enumForum_Topic_TitleDraftList];    
                    tbSubject.MaxLength = ForumDBFieldLength.Draft_subjectFieldLength;
                    ViewState["subject"] = "";
                    if (Request.QueryString["pageindex"] != null && Request.QueryString["pagesize"] != null)
                    {
                        aspnetPager.PageIndex = Convert.ToInt32(Request.QueryString["pageindex"]);
                        aspnetPager.PageSize = Convert.ToInt32(Request.QueryString["pagesize"]);
                    }
                    RefreshData();
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        private void RefreshData()
        {
            string subject = ViewState["subject"].ToString();
            int recordsCount = DraftProcess.GetCountOfDrafs(SiteId, UserOrOperatorId, IfOperator, subject);
            if (recordsCount == 0)
            {
                repeaterDrafts.DataSource = null;
                repeaterDrafts.DataBind();
                aspnetPager.Visible = false;
            }
            else
            {
                TopicWithPermissionCheck[] topics = TopicProcess.GetTopicsWhichExistDraftByPaging(SiteId,
                UserOrOperatorId, IfOperator, aspnetPager.PageIndex + 1, aspnetPager.PageSize, subject, "");

                aspnetPager.Visible = true;
                aspnetPager.CWCDataBind(repeaterDrafts, topics, recordsCount);
            }

        }

        private void BindDraftList()
        {
            repeaterDrafts.DataSource = TopicProcess.GetTopicsWhichExistDraftByPaging(SiteId,
            UserOrOperatorId, IfOperator, 1, 10, "", "");
            repeaterDrafts.DataBind();
        }


        protected void RepeaterDrafts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "delete")
            {
                try
                {
                    string forumIdAndtopicId=e.CommandArgument.ToString();
                    int topicId = Convert.ToInt32(forumIdAndtopicId.Substring(forumIdAndtopicId.IndexOf('#') + 1, forumIdAndtopicId.Length - forumIdAndtopicId.IndexOf('#')-1));

                    DraftProcess.DeleteDraftByTopicId(SiteId, UserOrOperatorId, IfOperator, topicId);
                    this.RefreshData();
                }

                catch (Exception exp)
                {
                    lblMessage.Text = ErrorDelete + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
            else if (e.CommandName == "view")
            {
                try
                {
                    string forumIdAndtopicId = e.CommandArgument.ToString();
                    int topicId = Convert.ToInt32(forumIdAndtopicId.Substring(forumIdAndtopicId.IndexOf('#') + 1, forumIdAndtopicId.Length - forumIdAndtopicId.IndexOf('#') - 1));
                    int forumId = Convert.ToInt32(forumIdAndtopicId.Substring(0, forumIdAndtopicId.IndexOf('#')));
                    //Response.Write(string.Format("<script language='javascript'>window.open('../../Topic.aspx?topicId={0}&Siteid={1}&forumId={2}');</script>", topicId, SiteId,forumId));
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onclick", string.Format("window.open('../../Topic.aspx?topicId={0}&Siteid={1}&forumId={2}#divpostreplay');", topicId, SiteId, forumId), true);     
                }
                catch (Exception exp)
                {
                    lblMessage.Text =ErrorEdit+ exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
        }

        protected void aspnetPager_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
        {
            try
            {
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void aspnetPager_ChangePageSize(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.ChangePageSizeEventArgs ce)
        {
            try
            {
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.aspnetPager.PageIndex = 0;
                ViewState["subject"] = tbSubject.Text;
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorQuery + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void repeaterDrafts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                TopicWithPermissionCheck topic = e.Item.DataItem as TopicWithPermissionCheck;
                ImageButton imgbtnView = e.Item.FindControl("imgbtnView") as ImageButton;
                imgbtnView.ToolTip = HelpView;

                imgbtnView.CommandArgument = topic.ForumId +  "#" + topic.TopicId;
                ImageButton imgbtnDelete = e.Item.FindControl("imgbtnDelete") as ImageButton;
                imgbtnDelete.ToolTip = HelpDelete;
                imgbtnDelete.CommandArgument = topic.ForumId + "#" + topic.TopicId;
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

    }
}
