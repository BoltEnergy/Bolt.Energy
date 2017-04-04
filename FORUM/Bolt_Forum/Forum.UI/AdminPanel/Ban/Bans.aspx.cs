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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using System.Text.RegularExpressions;
using Com.Comm100.Forum.Language;
namespace Com.Comm100.Forum.UI.AdminPanel.Ban
{
    public partial class Bans : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {
        #region Const
        private const string VSKEY_OrderField = "orderField";
        private const string VSKEY_OrderDirection = "orderDirection";
        private const string VSKEY_Keywords = "keywords";
        private const string IMGPATH_OrderUp = "~/images/sort_up.gif";
        private const string IMGPATH_OrderDown = "~/images/sort_down.gif";
        #endregion

        string ErrorLoad;
        string ErrorSort;
        string ErrorQuery;
        string ErrorDelete;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Ban_PageBanListErrorLoad];
                ErrorSort = Proxy[EnumText.enumForum_Ban_PageBanListErrorSort];
                ErrorQuery = Proxy[EnumText.enumForum_Ban_PageBanListErrorQuery];
                ErrorDelete = Proxy[EnumText.enumForum_Ban_PageBanListErrorDelete];
                lblTitle.Text = Proxy[EnumText.enumForum_Ban_TitleBanList];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Ban_SubtitleBanList];
                this.btnQuery1.Text = Proxy[EnumText.enumForum_Public_ButtonQuery];
                this.btnQuery2.Text = Proxy[EnumText.enumForum_Public_ButtonQuery];
                this.btnBanUser1.Text = Proxy[EnumText.enumForum_Ban_ButtonBanUser];
                this.btnBanUser2.Text = Proxy[EnumText.enumForum_Ban_ButtonBanUser];
                this.btnBanIP1.Text = Proxy[EnumText.enumForum_Ban_ButtonBanIP];
                this.btnBanIP2.Text = Proxy[EnumText.enumForum_Ban_ButtonBanIP];
               
            }
            catch (Exception exp)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumBan);
                Master.Page.Title = "Banned List";
                if (!IsPostBack)
                {
                    ViewState[VSKEY_OrderField] = "StartDate";
                    ViewState[VSKEY_OrderDirection] = "desc";
                    this.imgBeginTime.ImageUrl = IMGPATH_OrderDown;
                    this.imgBeginTime.Visible = true;
                    ViewState[VSKEY_Keywords] = "";
                    RefreshData();
                }
            }
            catch (Exception exp)
            {
                this.lblError.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }

        }
        protected void RefreshData()
        {
            int recordsCount=0;
            BanBase[] bans;
            string keywords = ViewState[VSKEY_Keywords] as string;
            string orderField=ViewState[VSKEY_OrderField] as string;
            string orderDirection = ViewState[VSKEY_OrderDirection] as string;
            EnumBanType type = EnumBanType.User;
            long ip = 0;
            string name = string.Empty;
            if (keywords != "")
            {
                name = keywords.Trim();
                Regex regularExpressions = new Regex(@"^(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])\.(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])\.(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])\.(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])$");
                Match matchIP = regularExpressions.Match(keywords);
                if (matchIP.Success)
                {
                    type=EnumBanType.IP;
                    ip = IpHelper.DottedIP2LongIP(keywords);
                }
                else
                {
                    type=EnumBanType.User;
                    ip = 0;
                }
            }
            recordsCount = BanProcess.GetCountOfBansByQuery(this.UserOrOperatorId, this.SiteId,type,ip,name);
            bans = BanProcess.GetBansByQueryAndPaging(this.UserOrOperatorId, this.SiteId, this.aspnetPager.PageIndex + 1, this.aspnetPager.PageSize,type,ip,name,orderField,orderDirection);
            if (recordsCount == 0)
            {
                aspnetPager.Visible = false;
                repeaterBans.DataSource = null;
                repeaterBans.DataBind();
            }
            else
            {
                aspnetPager.CWCDataBind(this.repeaterBans, bans, recordsCount);
                aspnetPager.Visible = true;
            }
        }

        #region control event
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.aspnetPager.PageIndex = 0;
                ViewState[VSKEY_Keywords] = this.txtKeywords.Text;
                RefreshData();
            }
            catch (Exception exp)
            {
                lblError.Text = ErrorQuery + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void btnBanUser_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("BanUser.aspx?siteId=" + this.SiteId,false);
            }
            catch (Exception ex)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_Public_RedirectError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        } 
        
        protected void btnBanIP_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("BanIP.aspx?siteId=" + this.SiteId, false);
            }
            catch (Exception ex)
            {
                this.lblError.Text = Proxy[EnumText.enumForum_Public_RedirectError];
                LogHelper.WriteExceptionLog(ex);
            }

        }
        protected void lbtnSort_click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                this.imgBeginTime.Visible = false;
                this.imgUnbannedTime.Visible = false;
                this.imgOperator.Visible = false;
                switch (lbtn.CommandName)
                {
                    case "BeginTime":
                        {
                            this.imgBeginTime.Visible = true;
                            ViewState[VSKEY_OrderField] = "StartDate";
                            if (ViewState[VSKEY_OrderDirection].ToString().Equals("desc"))
                            {
                                ViewState[VSKEY_OrderDirection] = "asc";
                                this.imgBeginTime.ImageUrl = IMGPATH_OrderUp;
                            }
                            else
                            {
                                ViewState[VSKEY_OrderDirection] = "desc";
                                this.imgBeginTime.ImageUrl = IMGPATH_OrderDown;
                            }
                            break;
                        }
                    case "UnbannedTime":
                        {
                            this.imgUnbannedTime.Visible = true;
                            ViewState[VSKEY_OrderField] = "EndDate";
                            if (ViewState[VSKEY_OrderDirection].ToString().Equals("desc"))
                            {
                                ViewState[VSKEY_OrderDirection] = "asc";
                                this.imgUnbannedTime.ImageUrl = IMGPATH_OrderUp;
                            }
                            else
                            {
                                ViewState[VSKEY_OrderDirection] = "desc";
                                this.imgUnbannedTime.ImageUrl = IMGPATH_OrderDown;
                            }
                            break;
                        }
                    case "Operator":
                        {
                            this.imgOperator.Visible = true;
                            ViewState[VSKEY_OrderField] = "OperatedUserOrOperatorId";
                            if (ViewState[VSKEY_OrderDirection].ToString().Equals("desc"))
                            {
                                ViewState[VSKEY_OrderDirection] = "asc";

                                this.imgOperator.ImageUrl = IMGPATH_OrderUp;
                            }
                            else
                            {
                                ViewState[VSKEY_OrderDirection] = "desc";
                                this.imgOperator.ImageUrl = IMGPATH_OrderUp;
                            }
                            break;
                        }
                }
                RefreshData();
            }
            catch (Exception exp)
            {
                this.lblError.Text = ErrorSort+ exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void repeaterBans_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    int banId = Convert.ToInt32(e.CommandArgument);
                    try
                    {
                        BanProcess.DeleteBan(this.UserOrOperatorId, this.SiteId, banId);
                    }
                    catch (Exception exp)
                    {
                        this.lblError.Text = ErrorDelete + exp.Message;
                        LogHelper.WriteExceptionLog(exp);
                    }
                }
                else if (e.CommandName == "Edit")
                {
                    int banId = Convert.ToInt32(e.CommandArgument);
                    try
                    {
                        Response.Redirect(string.Format("EditBan.aspx?banId={0}&siteId={1}", banId, SiteId));
                    }
                    catch (Exception exp)
                    {
                        this.lblError.Text = "Redirect page Error:" + exp.Message;
                        LogHelper.WriteExceptionLog(exp);
                        this.IfError = true;
                    }
                }
                this.RefreshData();
            }
            catch (Exception exp)
            {
                this.lblError.Text = exp.Message;
            }
        }
        #region pager event
        protected void aspnetPager_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs e)
        {
            try
            {
                RefreshData();
            }
            catch (Exception exp)
            {
                this.lblError.Text = exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void aspnetPager_ChangePageSize(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.ChangePageSizeEventArgs e)
        {
            try
            {
                RefreshData();
            }
            catch (Exception exp)
            {
                this.lblError.Text = exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion
        #endregion

        protected string GetOperator(int operatedUserOrOperatorId)
        {
            return Server.HtmlEncode(UserProcess.GetUserOrOpertorById(this.SiteId, operatedUserOrOperatorId).DisplayName);
        }

       
    }
}
