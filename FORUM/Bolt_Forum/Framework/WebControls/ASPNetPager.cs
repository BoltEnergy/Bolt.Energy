#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif

/*
 * Name:         ASPNetPager
 * Version:         2.0
 * Description:  GridPaging WebControl
 * Copyright:    Copyright(c) 2009 Comm100.
 *  Create:       Elei 2009-4-20 Version 1.0
 *  Modify:       Elei 2009-5-12 Version 1.0
 *  Modify:       Elei 2009-6-30 Version 2.0
 */

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Com.Comm100.Framework.Language;
using Com.Comm100.Language;
using System.Text;

[assembly: TagPrefix("Com.Comm100.Framework.WebContols", "CWC")]
namespace Com.Comm100.Framework.WebControls
{
    public enum ASPNetPagerMode
    {
        ImageButton,
        LinkButton,
    }
    [DefaultProperty("")]
    [ToolboxData("<{0}:ASPNetPager runat=server></{0}:ASPNetPager>")]
    public class ASPNetPager : Control, INamingContainer
    {
        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]

        #region Const
        private const string VSKEY_RECORDSCOUNT = "RecordsCount";
        private const string VSKEY_PAGESIZE = "PageSize";
        private const string VSKEY_PAGEINDEX = "PageIndex";
        private const string VSKEY_EXTRAINDEX = "EXTRAINDEX";
        private const string VSKEY_MODE = "Mode";
        private const string VSKEY_FIRSTBTNIMG = "FirstBtnImg";
        private const string VSKEY_PREVIOUSBTNIMG = "PreviousBtnImg";
        private const string VSKEY_NEXTBTNIMG = "NextBtnImg";
        private const string VSKEY_LASTBTNIMG = "LastBtnImg";
        //private const string VSKEY_CSSCLASS = "CSSClass";
        //private const string VSKEY_BTNSTYLE = "btnStyle";
        //private const string VSKEY_TEXTBOXSTYLE = "TextBoxStyle";
        //private const string VSKEY_DROPDOWNLISTSTYLE = "DropDownListStyle";
        #endregion

        #region Property
        private int RecordsCount
        {
            set { ViewState[VSKEY_RECORDSCOUNT] = value; }
            get { return ViewState[VSKEY_RECORDSCOUNT] == null ? 0 : (int)ViewState[VSKEY_RECORDSCOUNT]; }
        }

        public int PageSize
        {
            set { ViewState[VSKEY_PAGESIZE] = value; }
            get
            { return ViewState[VSKEY_PAGESIZE] == null ? 30 : (int)ViewState[VSKEY_PAGESIZE]; }/*efault value of ViewState[VSKEY_PAGESIZE]=20*/
        }

        public int PageIndex
        {
            set
            {
                ViewState[VSKEY_PAGEINDEX] = value;
                SaveCookie(value.ToString());
                if (Mode != ASPNetPagerMode.ImageButton)
                {
                    Controls.Clear();
                    AddLinkButtonControls();
                }
            }
            get
            {
                return GetPageIndex();
                //ViewState[VSKEY_PAGEINDEX] == null ? 0 : (int)ViewState[VSKEY_PAGEINDEX]; 
            }
        }

        public string ExtraIndex
        {
            get
            {
                return Convert.ToString(ViewState[VSKEY_EXTRAINDEX]);
            }
            set
            {
                ViewState[VSKEY_EXTRAINDEX] = value;
            }
        }

        public ASPNetPagerMode Mode
        {
            set { ViewState[VSKEY_MODE] = value; }
            get { return ViewState[VSKEY_MODE] == null ? ASPNetPagerMode.ImageButton : (ASPNetPagerMode)ViewState[VSKEY_MODE]; }
        }

        public string FirstButImage
        {
            set { ViewState[VSKEY_FIRSTBTNIMG] = value; }
            get { return ViewState[VSKEY_FIRSTBTNIMG] == null ? "~/images/btnbg_first.gif" : ViewState[VSKEY_FIRSTBTNIMG].ToString(); }
        }

        public string NextButImage
        {
            set { ViewState[VSKEY_NEXTBTNIMG] = value; }
            get { return ViewState[VSKEY_NEXTBTNIMG] == null ? "~/images/btnbg_next.gif" : ViewState[VSKEY_NEXTBTNIMG].ToString(); }
        }

        public string PreviousButImage
        {
            set { ViewState[VSKEY_PREVIOUSBTNIMG] = value; }
            get { return ViewState[VSKEY_PREVIOUSBTNIMG] == null ? "~/images/btnbg_previous.gif" : ViewState[VSKEY_PREVIOUSBTNIMG].ToString(); }
        }

        public string LastButImage
        {
            set { ViewState[VSKEY_LASTBTNIMG] = value; }
            get { return ViewState[VSKEY_LASTBTNIMG] == null ? "~/images/btnbg_last.gif" : ViewState[VSKEY_LASTBTNIMG].ToString(); }
        }

        //public string CSSClass
        //{
        //    set { ViewState[VSKEY_CSSCLASS] = value; }
        //    get { return ViewState[VSKEY_CSSCLASS] == null ? "ASPNetPager" : ViewState[VSKEY_CSSCLASS].ToString(); }
        //}

        //public string BtnStyle
        //{
        //    set { ViewState[VSKEY_BTNSTYLE] = value; }
        //    get { return ViewState[VSKEY_BTNSTYLE] == null ? "BtnStyle" : ViewState[VSKEY_BTNSTYLE].ToString(); }
        //}

        //public string TextBoxStyle
        //{
        //    set { ViewState[VSKEY_TEXTBOXSTYLE] = value; }
        //    get { return ViewState[VSKEY_TEXTBOXSTYLE] == null ? "TextBoxStyle" : ViewState[VSKEY_TEXTBOXSTYLE].ToString(); }
        //}

        //public string DropDownListStyle
        //{
        //    set { ViewState[VSKEY_DROPDOWNLISTSTYLE] = value; }
        //    get { return ViewState[VSKEY_DROPDOWNLISTSTYLE] == null ? "DropDownListStyle" : ViewState[VSKEY_DROPDOWNLISTSTYLE].ToString(); }
        //}

        public string ItemsName
        {
            get
            {
                if (ViewState["ItemsName"] == null)
                    ViewState["ItemsName"] = "records";
                return ViewState["ItemsName"].ToString();
            }
            set
            {
                ViewState["ItemsName"] = value;
            }
        }
        public string ItemName
        {
            get
            {
                if (ViewState["ItemName"] == null)
                    ViewState["ItemName"] = "record";
                return ViewState["ItemName"].ToString();
            }
            set
            {
                ViewState["ItemName"] = value;
            }
        }
        #endregion

        #region ChildControls
        private ImageButton btnFirst = new ImageButton();
        private ImageButton btnPrevious = new ImageButton();
        private ImageButton btnNext = new ImageButton();
        private ImageButton btnLast = new ImageButton();
        private Label lblTotalPage = new Label();
        private Label lblPageRows = new Label();
        private Label lblRecordsCount = new Label();
        private Label lblPageSizeText = new Label();
        private Label lblCurrPageRowsText = new Label();
        private Label lblTotalRowsText = new Label();
        private DropDownList ddlPageSize = new DropDownList();
        private TextBox txtPageIndex = new TextBox();
        private Button btnGoto = new Button();
        #endregion

        #region Method
        public void CWCDataBind(GridView bindControl, object dataSource, int recordsCount)
        {
            RecordsCount = recordsCount;
            bindControl.DataSource = dataSource;
            bindControl.DataBind();
            if (this.Mode == ASPNetPagerMode.ImageButton)
            {
                SetButtonsEnable();
                SetLabelText();
            }
        }

        public void CWCDataBind(DataList bindControl, object dataSource, int recordsCount)
        {
            RecordsCount = recordsCount;
            bindControl.DataSource = dataSource;
            bindControl.DataBind();
            if (this.Mode == ASPNetPagerMode.ImageButton)
            {
                SetButtonsEnable();
                SetLabelText();
            }
        }

        public void CWCDataBind(Repeater bindControl, object dataSource, int recordsCount)
        {
            RecordsCount = recordsCount;
            bindControl.DataSource = dataSource;
            bindControl.DataBind();
            if (this.Mode == ASPNetPagerMode.ImageButton)
            {
                SetButtonsEnable();
                SetLabelText();
            }
        }

        private bool IsNumber(string strValue)
        {
            if (string.IsNullOrEmpty(strValue)) return false;
            Char[] cArray = strValue.ToCharArray();
            foreach (Char c in cArray)
            {
                if (!Char.IsNumber(c)) return false;
            }
            return true;
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            if (RecordsCount == 0) return;
            switch (((ImageButton)sender).CommandArgument)
            {
                case "First":
                    PageIndex = 0;
                    break;
                case "Previous":
                    PageIndex--;
                    break;
                case "Next":
                    PageIndex++;
                    break;
                case "Last":
                    int pageCount = RecordsCount / PageSize;
                    pageCount += RecordsCount % PageSize == 0 ? 0 : 1;
                    PageIndex = pageCount - 1;
                    break;
            }
            if (Mode == ASPNetPagerMode.ImageButton)
            {
                SetButtonsEnable();
                SetLabelText();
            }
            else
            {
                Controls.Clear();
                AddLinkButtonControls();
            }
            OnPaging(new PagingEventArgs());
        }

        protected void lbtn_Click(object sender, EventArgs e)
        {
            if (RecordsCount == 0) return;
            PageIndex = Convert.ToInt32(((LinkButton)sender).CommandArgument) - 1;
            Controls.Clear();
            AddLinkButtonControls();
            OnPaging(new PagingEventArgs());
        }

        private void AddLinkButtonControls()
        {
            Controls.Add(new LiteralControl("<div class=\"ASPNetPager\">"));

            int pageCount = RecordsCount / PageSize;
            pageCount += RecordsCount % PageSize == 0 ? 0 : 1;

            /* add pager info*/
            int showPageIndex = PageIndex;
            if (showPageIndex <= 0) showPageIndex = 1;
            else if (showPageIndex >= pageCount) showPageIndex = pageCount;
            else showPageIndex += 1;

            FunctionLanguageHelper pageTop = new FunctionLanguageHelper();
            

            
            /*StringBuilder pagerInfo = new StringBuilder();
            pagerInfo.Append("<span class=\"PagerCurrent\">&nbsp;&nbsp;");
            pagerInfo.Append("<span class=\"PagerCurrent\">Page&nbsp;&nbsp;");
            pagerInfo.Append(showPageIndex);
            pagerInfo.Append("&nbsp;&nbsp;of&nbsp;&nbsp;");
            pagerInfo.Append(pageCount);
            pagerInfo.Append("</span>&nbsp;&nbsp;<span class=\"PagerCount\">[&nbsp;");
            pagerInfo.Append(this.RecordsCount);
            pagerInfo.Append("&nbsp;");*/

            string itemName = this.ItemsName;
            if (this.RecordsCount == 1)
            {
                //pagerInfo.Append(this.ItemName);
                itemName = this.ItemName;
            }

            string pagerInfo = string.Format(pageTop.GetText(Com.Comm100.Framework.Language.EnumFunctionLanguageCode.ASPNetPager_DateTransferToString_PageTop, new object[] { showPageIndex, pageCount, this.RecordsCount, itemName }));
            
            string strpager = string.Format(pagerInfo);
            //pagerInfo.Append("&nbsp;]</span>");
            Controls.Add(new LiteralControl(strpager));
            //Controls.Add(new LiteralControl(pagerInfo.ToString()));
            /*end*/

            if (pageCount > 1)
            {
                Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

                int startNum, endNum;
                if (PageIndex <= 5)
                {
                    startNum = 1;
                    endNum = pageCount >= 10 ? 10 : pageCount;
                }
                else if (PageIndex <= pageCount - 5)
                {
                    startNum = PageIndex - 4;
                    endNum = PageIndex + 5;
                    //startNum = pageCount >= 10 ? pageCount - 9 : 1;
                    //endNum = pageCount;
                }
                else
                {
                    startNum = PageIndex - 4;
                    endNum = pageCount >= PageIndex + 5 ? PageIndex + 5 : pageCount;
                }

                if (PageIndex > 5)
                {
                    btnFirst.ID = "btnFirst";
                    btnFirst.EnableViewState = false;
                    btnFirst.CommandArgument = "First";
                    btnFirst.ImageUrl = this.FirstButImage;
                    btnFirst.CssClass = "BtnStyle";
                    btnFirst.Click += new ImageClickEventHandler(btn_Click);
                    btnFirst.CausesValidation = false;
                    Controls.Add(btnFirst);

                    Controls.Add(new LiteralControl("&nbsp;"));
                    btnPrevious.ID = "btnPrevious";
                    btnPrevious.EnableViewState = false;
                    btnPrevious.ImageUrl = this.PreviousButImage;
                    btnPrevious.CssClass = "BtnStyle";
                    btnPrevious.CommandArgument = "Previous";
                    btnPrevious.Click += new ImageClickEventHandler(btn_Click);
                    btnPrevious.CausesValidation = false;
                    Controls.Add(btnPrevious);
                }

                for (int i = startNum; i <= endNum; i++)
                {
                    if (i == PageIndex + 1)
                    {
                        Controls.Add(new LiteralControl("&nbsp;"));
                        Controls.Add(new LiteralControl("<span class=\"aSel\">" + i.ToString() + "</span>"));
                    }
                    else
                    {
                        LinkButton linkButton = new LinkButton();
                        linkButton.Text = i.ToString();
                        linkButton.CssClass = "aNSel";
                        linkButton.CommandArgument = i.ToString();
                        linkButton.EnableViewState = false;
                        linkButton.Click += new EventHandler(lbtn_Click);
                        linkButton.CausesValidation = false;
                        Controls.Add(new LiteralControl("&nbsp;"));
                        Controls.Add(linkButton);
                    }
                }

                if (endNum < pageCount)
                {
                    Controls.Add(new LiteralControl("&nbsp;"));
                    btnNext.ID = "btnNext";
                    btnNext.EnableViewState = false;
                    btnNext.ImageUrl = this.NextButImage;
                    btnNext.CssClass = "BtnStyle";
                    btnNext.CommandArgument = "Next";
                    btnNext.Click += new ImageClickEventHandler(btn_Click);
                    btnNext.CausesValidation = false;
                    Controls.Add(btnNext);

                    Controls.Add(new LiteralControl("&nbsp;"));
                    btnLast.ID = "btnLast";
                    btnLast.EnableViewState = false;
                    btnLast.ImageUrl = this.LastButImage;
                    btnLast.CssClass = "BtnStyle";
                    btnLast.CommandArgument = "Last";
                    btnLast.Click += new ImageClickEventHandler(btn_Click);
                    btnLast.CausesValidation = false;
                    Controls.Add(btnLast);
                }
            }

            Controls.Add(new LiteralControl("</div>"));
        }

        protected void btnGoto_Click(object sender, EventArgs e)
        {
            int pageCount = RecordsCount / PageSize;
            pageCount += RecordsCount % PageSize == 0 ? 0 : 1;
            if (!IsNumber(txtPageIndex.Text))
            {
                txtPageIndex.Text = Convert.ToString(PageIndex + 1);
            }
            else if (Convert.ToInt32(txtPageIndex.Text) >= 1 && Convert.ToInt32(txtPageIndex.Text) <= pageCount)
            {
                PageIndex = Convert.ToInt32(txtPageIndex.Text) - 1;
            }
            else if (Convert.ToInt32(txtPageIndex.Text) < 1)
            {
                PageIndex = 0;
            }
            else if (Convert.ToInt32(txtPageIndex.Text) > pageCount)
            {
                PageIndex = pageCount - 1;
            }
            else
            {
                txtPageIndex.Text = Convert.ToString(PageIndex + 1);
            }
            SetButtonsEnable();
            SetLabelText();
            OnPaging(new PagingEventArgs());
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RecordsCount == 0) return;
            PageIndex = 0;
            PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            SetButtonsEnable();
            SetLabelText();
            OnChangePageSize(new ChangePageSizeEventArgs());
        }

        private void SetButtonsEnable()
        {
            if (RecordsCount <= 0)/////////////////3
            {
                btnFirst.Visible = false;
                btnPrevious.Visible = false;
                btnNext.Visible = false;
                btnLast.Visible = false;
                ddlPageSize.Visible = false;
                return;
            }
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            if (PageIndex <= 0)
            {
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
            }
            int pageCount = RecordsCount / PageSize;
            pageCount += RecordsCount % PageSize == 0 ? 0 : 1;
            if (PageIndex >= pageCount - 1)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
        }

        private void SetLabelText()
        {
            if (RecordsCount <= 0)//////////2=
            {
                lblTotalPage.Visible = false;
                lblPageRows.Visible = false;
                lblRecordsCount.Visible = false;
                lblPageSizeText.Visible = false;
                lblCurrPageRowsText.Visible = false;
                lblTotalRowsText.Visible = false;
                txtPageIndex.Visible = false;
                return;
            }
            //set lblPageIndex.Text
            int pageCount = RecordsCount / PageSize;
            pageCount += RecordsCount % PageSize == 0 ? 0 : 1;
            lblTotalPage.Text = "/" + pageCount.ToString();

            //set lblPageRows.Text
            lblPageRows.Text = "";
            if (RecordsCount <= PageSize) //only one page and recordscount <= pagesize
            {
                lblPageRows.Text += RecordsCount.ToString();
            }
            else if (PageIndex >= 0 && PageIndex < pageCount - 1 && RecordsCount > PageSize) //more then one page and current page is not the last one
            {
                lblPageRows.Text += PageSize.ToString();
            }
            else if (PageIndex == pageCount - 1) //last page
            {
                lblPageRows.Text += Convert.ToString((RecordsCount % PageSize == 0 ? PageSize : RecordsCount % PageSize));
            }
            else if (PageIndex == pageCount)
            {
                lblPageRows.Text += "0";
            }

            //set txtPageIndex.Text
            txtPageIndex.Text = Convert.ToString(PageIndex + 1);

            txtPageIndex.Width = 12;
            if (Convert.ToString(this.PageIndex + 1).Length > 1)
                txtPageIndex.Width = 12 + (Convert.ToString(this.PageIndex + 1).Length - 1) * 5;

            //set lblRecordsCount.Text
            lblRecordsCount.Text = RecordsCount.ToString();
        }
        #endregion

        #region OverrideMethod
        protected override void CreateChildControls()
        {
            if (RecordsCount <= 0)//////////=1
                return;
            if (Mode == ASPNetPagerMode.ImageButton)
            {
                LanguageProxy languageProxy = new LanguageProxy();
                string textPageSize = languageProxy[EnumText.enumPublic_GridPager_PageSize];
                string textCurrentPageItems = languageProxy[EnumText.enumPublic_GridPager_CurrentPageItems];
                string textTotalItems = languageProxy[EnumText.enumPublic_GridPager_TotalItems];
 
                SetButtonsEnable();
                SetLabelText();
                base.CreateChildControls();

                Controls.Add(new LiteralControl("<div class=\"ASPNetPager\">"));

                btnGoto.Width = 0;
                btnGoto.ID = "btnGoto";
                btnGoto.Style.Add(HtmlTextWriterStyle.Display, "none");
                btnGoto.EnableViewState = false;
                btnGoto.Click += new EventHandler(btnGoto_Click);
                btnGoto.CausesValidation = false;
                Controls.Add(btnGoto);

                btnFirst.ID = "btnFirst";
                btnFirst.EnableViewState = false;
                btnFirst.CommandArgument = "First";
                btnFirst.ImageUrl = this.FirstButImage;
                btnFirst.CssClass = "BtnStyle";
                btnFirst.Click += new ImageClickEventHandler(btn_Click);
                btnFirst.CausesValidation = false;
                Controls.Add(btnFirst);

                Controls.Add(new LiteralControl("&nbsp;"));
                btnPrevious.ID = "btnPrevious";
                btnPrevious.EnableViewState = false;
                btnPrevious.ImageUrl = this.PreviousButImage;
                btnPrevious.CssClass = "BtnStyle";
                btnPrevious.CommandArgument = "Previous";
                btnPrevious.Click += new ImageClickEventHandler(btn_Click);
                btnPrevious.CausesValidation = false;
                Controls.Add(btnPrevious);

                Controls.Add(new LiteralControl("&nbsp;"));
                txtPageIndex.CssClass = "TextBoxStyle";
                txtPageIndex.MaxLength = 9;
                
                
                //txtPageIndex.Attributes.Add("onkeydown", "if (event.keyCode==13) {document.getElementById('" + btnGoto.ClientID + "').click(); return false;}");
                txtPageIndex.Attributes.Add("onkeydown", "if (event.keyCode==13 || event.which == 13) { window.focus();this.parentNode.childNodes[0].click();event.keyCode = 0; event.returnValue=false;event.cancelBubble=true;return false;}");
                //txtPageIndex.Attributes.Add("onkeypress", "document.getElementById('" + btnGoto.ClientID + "').focus();");
                Controls.Add(txtPageIndex);
                lblTotalPage.EnableViewState = false;
                Controls.Add(lblTotalPage);

                Controls.Add(new LiteralControl("&nbsp;"));
                btnNext.ID = "btnNext";
                btnNext.EnableViewState = false;
                btnNext.ImageUrl = this.NextButImage;
                btnNext.CssClass = "BtnStyle";
                btnNext.CommandArgument = "Next";
                btnNext.Click += new ImageClickEventHandler(btn_Click);
                btnNext.CausesValidation = false;
                Controls.Add(btnNext);

                Controls.Add(new LiteralControl("&nbsp;"));
                btnLast.ID = "btnLast";
                btnLast.EnableViewState = false;
                btnLast.ImageUrl = this.LastButImage;
                btnLast.CssClass = "BtnStyle";
                btnLast.CommandArgument = "Last";
                btnLast.Click += new ImageClickEventHandler(btn_Click);
                btnLast.CausesValidation = false;
                Controls.Add(btnLast);

                Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;"));
                lblPageSizeText.Text = textPageSize;//"Page Size:";
                lblPageSizeText.EnableViewState = false;
                Controls.Add(lblPageSizeText);
                Controls.Add(new LiteralControl("&nbsp;"));
                ddlPageSize.Items.Add(new ListItem("10", "10"));
                ddlPageSize.Items.Add(new ListItem("30", "30"));
                ddlPageSize.Items.Add(new ListItem("50", "50"));
                ddlPageSize.Items.Add(new ListItem("100", "100"));
                ddlPageSize.Items.Add(new ListItem("500", "500"));
                //ddlPageSize.Items.Add(new ListItem("1000", "1000"));
                ddlPageSize.AutoPostBack = true;
                ddlPageSize.Text = PageSize.ToString();
                ddlPageSize.CssClass = "DropDownListStyle";
                ddlPageSize.SelectedIndexChanged += new EventHandler(ddlPageSize_SelectedIndexChanged);
                Controls.Add(ddlPageSize);


                Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;"));
                lblCurrPageRowsText.Text = textCurrentPageItems;//"Current Page Items:";
                lblCurrPageRowsText.EnableViewState = false;
                Controls.Add(lblCurrPageRowsText);
                Controls.Add(new LiteralControl("&nbsp;"));
                lblPageRows.EnableViewState = false;
                Controls.Add(lblPageRows);

                Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;"));
                lblTotalRowsText.Text = textTotalItems;//"Total Items:";
                lblTotalRowsText.EnableViewState = false;
                Controls.Add(lblTotalRowsText);
                Controls.Add(new LiteralControl("&nbsp;"));
                lblRecordsCount.EnableViewState = false;
                Controls.Add(lblRecordsCount);

                Controls.Add(new LiteralControl("</div>"));
            }
            else
            {
                Controls.Clear();
                base.CreateChildControls();
                AddLinkButtonControls();
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            base.Render(output);
        }
        #endregion

        #region Event
        public class PagingEventArgs : System.EventArgs
        { }

        public delegate void PagingEventHandler(object sender, PagingEventArgs pe);

        public event PagingEventHandler Paging;

        protected void OnPaging(PagingEventArgs pe)
        {
            if (Paging != null)
            {
                Paging(this, pe);
            }
        }

        public class ChangePageSizeEventArgs : System.EventArgs
        { }

        public delegate void ChangePageSizeEventHandler(object sender, ChangePageSizeEventArgs ce);

        public event ChangePageSizeEventHandler ChangePageSize;

        protected void OnChangePageSize(ChangePageSizeEventArgs ce)
        {
            if (ChangePageSize != null)
            {
                ChangePageSize(this, ce);
            }
        }


        protected void ASPNetPager_Paging(object sender, EventArgs e)
        {
            OnPaging(new PagingEventArgs());
        }
        #endregion


        private void SaveCookie(string value)
        {
            string key = "";
            string index = Index(out key);

            if (System.Web.HttpContext.Current.Request.Cookies["Pager"] != null)
            {
                string pagerMatter = System.Web.HttpContext.Current.Request.Cookies["Pager"].Value;
                string[] pagerArray = pagerMatter.Replace("++", "+").Trim('+').Split('+');
                pagerMatter = "";
                bool exist = false;
                for (int i = 0; i < pagerArray.Length; i++)
                {
                    if (pagerArray[i].Contains(key))
                    {
                        pagerArray[i] = key + index + value;
                        exist = true;
                    }
                    pagerMatter = pagerMatter + pagerArray[i] + "+";
                }
                if (exist == false)
                {
                    pagerMatter = pagerMatter + key + index + value + "+";
                }
                System.Web.HttpContext.Current.Response.Cookies["Pager"].Value = pagerMatter;
            }
            else
            {
                System.Web.HttpCookie cookiePageIndex = new System.Web.HttpCookie("Pager", key + index + value + "+");
                System.Web.HttpContext.Current.Response.Cookies.Add(cookiePageIndex);
            }

        }

        private int GetPageIndex()
        {
            int pageIndex = 0;

            string key = "";
            string index = Index(out key);
            if (ViewState[VSKEY_PAGEINDEX] == null)
            {
                if (System.Web.HttpContext.Current.Request.Cookies["Pager"] == null)
                    pageIndex = 0;
                else
                {
                    string pagerMatter = System.Web.HttpContext.Current.Request.Cookies["Pager"].Value;
                    string[] pagerArray = pagerMatter.Split('+');
                    bool exist = false;
                    for (int i = 0; i < pagerArray.Length; i++)
                    {
                        if (pagerArray[i].Contains(key + index))
                        {
                            pageIndex = Convert.ToInt32(pagerArray[i].Remove(0, (key + index).Length));
                            exist = true;
                        }
                    }
                    if (exist == false)
                    {
                        pageIndex = 0;
                    }
                }

            }
            else
                pageIndex = (int)ViewState[VSKEY_PAGEINDEX];
            return pageIndex;
        }

        private string Index(out string key)
        {
            string url = System.Web.HttpContext.Current.Request.Url.ToString();
            System.Text.RegularExpressions.Regex regularExpressions = new
                System.Text.RegularExpressions.Regex(@"/(.*\.aspx)(.*)",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Match match = regularExpressions.Match(url);
            key = match.Groups[1].Value;

            string index = "";

            if (ExtraIndex != null && ExtraIndex != "")
            {
                index = ExtraIndex;
            }
            else
            {
                string property = match.Groups[2].Value.Trim().TrimStart('?');
                if (property.Contains("&"))
                {
                    string[] splitProperty = property.Split('&');
                    splitProperty = BubbleSort(splitProperty);
                    property = "";
                    for (int i = 0; i < splitProperty.Length; i++)
                        property = property + splitProperty[i];
                }
                index = property.Replace("=", "");
            }

            return index + PageSize;
        }

        private string[] BubbleSort(string[] str)
        {

            for (int i = 0; i < str.Length; i++)
            {
                string tempString = "";
                for (int j = str.Length - 1; j > i; j--)
                {
                    if (str[j].CompareTo(str[j - 1]) < 0)
                    {
                        tempString = str[j];
                        str[j] = str[j - 1];
                        str[j - 1] = tempString;
                    }
                }
            }
            return str;
        }

    }
}