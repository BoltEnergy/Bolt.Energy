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
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Language;

namespace Forum.UI
{
    public partial class SelectForum1 : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        public override bool IfValidateForumClosed
        {
            get
            {
                return false;
            }
        }
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    //can not visit this page through URL
                    Response.Write("<script language=javascript>if (parent.location.href == window.location.href) window.location.href='Default.aspx?siteId=" + SiteId + "';</script>");

                    CheckQueryString("topicId");
                    CheckQueryString("forumId");
                    ViewState["topicId"] = Convert.ToInt32(Request.QueryString["topicId"]);
                    ViewState["forumId"] = Convert.ToInt32(Request.QueryString["forumId"]);

                    btnMove1.Text = Proxy[EnumText.enumForum_Forums_ButtonSelectedToMove];
                    btnMove.Text = Proxy[EnumText.enumForum_Forums_ButtonSelectedToMove];
                    lblTitle.Text = Proxy[EnumText.enumForum_Forums_TitleSelectForum];
                    if (!this.IsPostBack)
                    {
                        CategoryWithPermissionCheck[] categorys = CategoryProcess.GetAllCategories(UserOrOperatorId, IfOperator, SiteId);
                        repeaterCategory1.DataSource = categorys;
                        repeaterCategory1.DataBind();
                    }
                }
                catch (System.Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_Forums_PageSelectForumErrorLoading] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }

        protected void repeaterCategory1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CategoryWithPermissionCheck category = (CategoryWithPermissionCheck)e.Item.DataItem;
                Com.Comm100.Forum.Bussiness.ForumWithPermissionCheck[] tmpForumArray = ForumProcess.GetNotHiddenForumsByCategoryID(UserOrOperatorId, SiteId, category.CategoryId);
                Repeater repeaterForum = (Repeater)e.Item.FindControl("repeaterForum");
                repeaterForum.DataSource = tmpForumArray;
                repeaterForum.DataBind();
            }
        }

        protected void repeaterForum_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ForumWithPermissionCheck forum = (ForumWithPermissionCheck)e.Item.DataItem;

                string forumName = "";

                if (forum.ForumId == Convert.ToInt32(ViewState["forumId"]))
                {
                    forumName = "<tr><td class=\"trStyle1\" width=\"500px\"><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + System.Web.HttpUtility.HtmlEncode(forum.Name) + "&nbsp;&nbsp;<b>" + Proxy[EnumText.enumForum_Public_TextCurrentLocation] + "</b></p></td></tr>";
                }
                else
                {
                    forumName = "<tr><td id='" + forum.ForumId + "' onclick='forumchoosed(" + forum.ForumId + ");' class=\"trStyle2\" width=\"500px\"><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + System.Web.HttpUtility.HtmlEncode(forum.Name) + "</p></td></tr>";
                }

                PlaceHolder unMarkHolder = e.Item.FindControl("forumName") as PlaceHolder;
                unMarkHolder.Controls.Add(new LiteralControl(forumName));

            }
        }

        protected void btnMove_Click(object sender, EventArgs e)
        {
            int forumId = Convert.ToInt32(this.chooseForum.Value);

            if (!this.IfError)
            {
                string key = "MoveExceptionScript";
                try
                {
                    //if (this.IfSiteClosed)
                    //{
                    //    Page.ClientScript.RegisterStartupScript(this.GetType(), key, "<script language=javascript>window.parent.location.reload();closeWindow();</script>");
                    //}
                    //else
                    //{
                        TopicProcess.MoveTopic(SiteId, UserOrOperatorId, IfOperator, Convert.ToInt32(ViewState["topicId"]), forumId);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), key, "<script language=javascript>window.parent.location.reload();window.parent.open('Topic.aspx?topicId=" + Convert.ToInt32(ViewState["topicId"]) + "&siteId=" + SiteId + "&forumId=" + forumId + "');closeWindow();</script>");
                        
                    //}
                }
                catch (ExceptionWithCode expwc)
                {
                    if (expwc.GetErrorCode() == EnumErrorCode.enumOperatorNotLogin)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), key, "<script language=javascript>window.parent.location.href='Login.aspx?siteId=" + SiteId + "';closeWindow();</script>");
                        LogHelper.WriteExceptionLog(expwc);
                        this.IfError = true;
                    }
                    else if (expwc.GetErrorCode() == EnumErrorCode.enumSystemNotEnoughPermission)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), key, "<script language=javascript>alert('" + Proxy[EnumText.enumForum_Public_AlertNoPermission] + "');window.parent.location.href='Topic.aspx?topicId=" + Convert.ToInt32(ViewState["topicId"]) + "&siteId=" + SiteId + "&forumId=" + Convert.ToInt32(ViewState["forumId"]) + "';closeWindow();</script>");
                        LogHelper.WriteExceptionLog(expwc);
                        this.IfError = true;
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), key, "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Forums_PageSelectForumErrorLoading] + expwc.Message + "\");window.parent.location.href='Topic.aspx?topicId=" + Convert.ToInt32(ViewState["topicId"]) + "&siteId=" + SiteId + "&forumId=" + Convert.ToInt32(ViewState["forumId"]) + "';closeWindow();</script>");
                        LogHelper.WriteExceptionLog(expwc);
                        this.IfError = true;
                    }
                }
                catch (Exception exp)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), key, "<script language=javascript>alert(\"" + Proxy[EnumText.enumForum_Forums_PageSelectForumErrorLoading] + exp.Message + "\");window.parent.location.href='Topic.aspx?topicId=" + Convert.ToInt32(ViewState["topicId"]) + "&siteId=" + SiteId + "&forumId=" + Convert.ToInt32(ViewState["forumId"]) + "';closeWindow();</script>");
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }
    }
}
