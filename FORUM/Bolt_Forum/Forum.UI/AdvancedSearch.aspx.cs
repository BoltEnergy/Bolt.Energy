
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
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Language;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Com.Comm100.Forum.UI.Common;

namespace Com.Comm100.Forum.UI
{
    public partial class AdvancedSearch : SearchBasePage
    {
        public string LanguageName
        {
            get
            {
                return LanguageHelper.GetLanguageName(Proxy._language);
            }
        }
        public int SearchInIndex
        {
            get;
            set;
        }

        protected override void InitLanguage()
        {
            base.InitLanguage();

            requiredFieldSubject.Text = Proxy[EnumText.enumForum_Topic_ErrorKeyWordsRequired];
            //txtSubject.Attributes.Add("oncontextmenu", "return isMaxlength(this," + ForumDBFieldLength.Category_nameFieldLength + ");");
            //txtSubject.Attributes.Add("oncontextmenu", "return isMaxlength(this," + ForumDBFieldLength.Category_nameFieldLength.ToString() + ");");

            if (!this.IsPostBack)
            {
                this.dlTimeRange.Items.Clear();
                this.dlTimeRange.Items.Add(Proxy[EnumText.enumForum_AdvancedSearch_FieldAll]);
                this.dlTimeRange.Items.Add(Proxy[EnumText.enumForum_AdvancedSearch_Field1Day]);
                this.dlTimeRange.Items.Add(Proxy[EnumText.enumForum_AdvancedSearch_Field7Day]);
                this.dlTimeRange.Items.Add(Proxy[EnumText.enumForum_AdvancedSearch_Field2Weeks]);
                this.dlTimeRange.Items.Add(Proxy[EnumText.enumForum_AdvancedSearch_Field1Month]);
                this.dlTimeRange.Items.Add(Proxy[EnumText.enumForum_AdvancedSearch_Field3Months]);
                this.dlTimeRange.Items.Add(Proxy[EnumText.enumForum_AdvancedSearch_Field6Months]);
                this.dlTimeRange.Items.Add(Proxy[EnumText.enumForum_AdvancedSearch_Field1Years]);

                this.rdSearchIn.Items.Clear();
                this.rdSearchIn.Items.Add(Proxy[EnumText.enumForum_AdvancedSearch_FieldPostSubjectsAndMessageText]);
                this.rdSearchIn.Items.Add(Proxy[EnumText.enumForum_AdvancedSearch_FieldMessageTextOnly]);
                this.rdSearchIn.Items.Add(Proxy[EnumText.enumForum_AdvancedSearch_FieldTopicTitlesOnly]);
                this.rdSearchIn.Items.Add(Proxy[EnumText.enumForum_AdvancedSearch_FieldFirstPostOfTopicsOnly]);

                this.rdDisplay.Items.Clear();
                this.rdDisplay.Items.Add(Proxy[EnumText.enumForum_AdvancedSearch_FieldPosts]);
                this.rdDisplay.Items.Add(Proxy[EnumText.enumForum_AdvancedSearch_FieldTopics]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    SiteSettingWithPermissionCheck siteSetting = SettingsProcess.GetSiteSettingBySiteId(SiteId, UserOrOperatorId);
                    Master.Page.Title = string.Format(Proxy[EnumText.enumForum_Topic_PageTitleSearch], System.Web.HttpUtility.HtmlEncode(siteSetting.SiteName));
                    if (!IsPostBack)
                    {
                        txtSubject.MaxLength = ForumDBFieldLength.Topic_subjectFieldLength;

                        this.btnSearch.Text = Proxy[EnumText.enumForum_Topic_ButtonSearch];
                        this.btnReset.Text = Proxy[EnumText.enumForum_Topic_ButtonReset];
                       
                        this.dlTimeRange.SelectedIndex = 0;
                       
                        this.rdSearchIn.SelectedIndex = 0;
                       
                        this.rdDisplay.SelectedIndex = 0;

                        string url = Request.Url.AbsoluteUri;
                        if (url.Contains("query="))
                        {
                            this.Bind();
                            SetValues();
                        }
                        else
                        {
                            this.Bind();
                            this.listboxForum.SelectedIndex = 0;
                        }
                    }

                    this.Form.DefaultButton = this.btnSearch.UniqueID;
                }
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_Error] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    this.IfError = true;
                }
            }
        }
        protected void Bind()
        {
            try
            {
                List<CategoryAndForum> categoryAndForum = new List<CategoryAndForum>();
                categoryAndForum = this.GetCategoryAndForum(UserOrOperatorId, IfOperator, SiteId);

                this.listboxForum.DataSource = categoryAndForum;
                listboxForum.DataTextField = "Name";
                listboxForum.DataValueField = "Type_Id";
                listboxForum.DataBind();
            }
            catch (Exception exp)
            {
                LogHelper.WriteExceptionLog(exp);
                throw exp;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    string keywords = this.txtSubject.Text;//Request.QueryString["key"].ToString();
                    string location = this.listboxForum.SelectedValue;//Request.QueryString["id"].ToString();
                    int timeRange = this.dlTimeRange.SelectedIndex;//Convert.ToInt32(Request.QueryString["time"]);
                    int searchIn = this.rdSearchIn.SelectedIndex;//Convert.ToInt32(Request.QueryString["in"].ToString());
                    int ifPost = this.rdDisplay.SelectedIndex;//Convert.ToInt32(Request.QueryString["ifpost"]);

                    SearchProcess.Search(SiteId, UserOrOperatorId);

                    string query = MyEncode("key=" + System.Web.HttpUtility.UrlEncode(keywords)
                                        + "&time=" + System.Web.HttpUtility.UrlEncode(timeRange.ToString())
                                        + "&in=" + System.Web.HttpUtility.UrlEncode(searchIn.ToString())
                                        + "&id=" + System.Web.HttpUtility.UrlEncode(location)
                                        + "&ifpost=" + System.Web.HttpUtility.UrlEncode(ifPost.ToString())
                                        + "&siteid=" + SiteId);

                    string url = "~/SearchResult.aspx?siteId=" + this.SiteId + "&query=" + query;

                    Response.Redirect(url
                        , false);
                }
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_Error] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                    string script = string.Format("<script language='javascript' type='text/javascript'>alert(\"{0}\");</script>", exp.Message);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", script);
                    this.IfError = true;
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            //this.lblRequired.Text = "";
            //this.txtSubject.Text = "";

            //this.requiredFieldSubject.Attributes.Add("onclick", "javascript:this.style.display='none';");

            //this.Bind();
            //this.listboxForum.SelectedIndex = 0;

            Response.Redirect("AdvancedSearch.aspx?siteId=" +SiteId ,false);
        }


        private List<CategoryAndForum> GetCategoryAndForum(int operatingOperatorId, bool ifOperator, int siteId)
        {
            List<CategoryAndForum> categoryAndForum1 = new List<CategoryAndForum>();
            categoryAndForum1.Add(new CategoryAndForum("a_", Proxy[EnumText.enumForum_HeaderFooter_SearchFromAll]));
            CategoryWithPermissionCheck[] categorys = CategoryProcess.GetAllCategories(operatingOperatorId, ifOperator, siteId);
            foreach (CategoryWithPermissionCheck category in categorys)
            {
                categoryAndForum1.Add(new CategoryAndForum("c_" + category.CategoryId, Server.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + category.Name)));
                Com.Comm100.Forum.Bussiness.ForumWithPermissionCheck[] forums = ForumProcess.GetForumsByCategoryID(operatingOperatorId, siteId, category.CategoryId);
                foreach (ForumWithPermissionCheck forum in forums)
                {
                    categoryAndForum1.Add(new CategoryAndForum("f_" + forum.ForumId, Server.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + forum.Name)));
                }
            }
            return categoryAndForum1;
        }

        protected void listboxForum_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void SetValues()
        {
            string query = MyDecode(Request.QueryString["query"].ToString());
                
            //CheckQueryString("key");
            string keywords = GetQueryString(query, "key");
            //CheckQueryString("id");
            string location = GetQueryString(query,"id");
            //CheckQueryString("time");
            int timeRange = Convert.ToInt32(GetQueryString(query,"time"));
            //CheckQueryString("in");
            int searchIn =  Convert.ToInt32(GetQueryString(query,"in"));
            //CheckQueryString("ifpost");
            short ifPost = Convert.ToInt16(GetQueryString(query,"ifpost"));

            this.txtSubject.Text = keywords;
            this.dlTimeRange.SelectedIndex = timeRange;
            this.SearchInIndex = searchIn;
            this.rdSearchIn.SelectedIndex = searchIn;
            this.rdDisplay.SelectedIndex = ifPost;
            this.listboxForum.SelectedValue = location;
        }

       
    }
}
