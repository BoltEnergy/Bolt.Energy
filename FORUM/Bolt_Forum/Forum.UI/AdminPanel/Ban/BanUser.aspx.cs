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
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Language;
using Com.Comm100.Framework.Enum;
using System.Web.UI.HtmlControls;


namespace Com.Comm100.Forum.UI.AdminPanel.Ban
{
    public partial class BanUser : Com.Comm100.Forum.UI.AdminPanel.AdminBasePage
    {

        #region Initialize Language
        string ErrorLoad;
        string ErrorSaveBan;
        string ErrorSelectUser;
        string ErrorQueryUser;
        string UserTypeRegisterUser;
        string UserTypeOperator;
        string UserTypeAdmin;//System Administrator
        string UserTypeAll;
        string BanTimeTypePermanent;
        string BanTimeTypeDays;
        string BanTimeTypeHours;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Ban_PageBanUserErrorLoad];
                ErrorSaveBan = Proxy[EnumText.enumForum_Ban_PageBanUserErrorSave];
                ErrorSelectUser = Proxy[EnumText.enumForum_Ban_PageBanUserErrorSelect];
                ErrorQueryUser = Proxy[EnumText.enumForum_Ban_PageBanUserErrorQuery];

                UserTypeRegisterUser = Proxy[EnumText.enumForum_User_UserTypeRegisterUser];//Proxy[EnumText.enumForum_Ban_UserTypeRegisterUser];
                UserTypeOperator = Proxy[EnumText.enumForum_User_UserTypeOperator]; //Proxy[EnumText.enumForum_Ban_UserTypeOperator];
                UserTypeAdmin = Proxy[EnumText.enumForum_User_UserTypeAdmin];//Proxy[EnumText.enumForum_Ban_UserTypeAdmin];
                UserTypeAll = Proxy[EnumText.enumForum_User_UserTypeAll];

                BanTimeTypePermanent = Proxy[EnumText.enumForum_Ban_TimeTypePermanent];
                BanTimeTypeDays = Proxy[EnumText.enumForum_Ban_TimeTypeDays];
                BanTimeTypeHours = Proxy[EnumText.enumForum_Ban_TimeTypeHours];

                lblTitle.Text = Proxy[EnumText.enumForum_Ban_TitleBanUser];
                lblSubTitle.Text = Proxy[EnumText.enumForum_Ban_SubtitleBanUser];
                btnQueryUser1.Text = Proxy[EnumText.enumForum_Public_ButtonQuery];
                btnQueryUser2.Text = Proxy[EnumText.enumForum_Public_ButtonQuery];
                btnSelectButtom.Text = Proxy[EnumText.enumForum_Ban_ButtonAdd];
                btnSelectTop.Text = Proxy[EnumText.enumForum_Ban_ButtonAdd];
                
                btnSave2.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnSave1.Text = Proxy[EnumText.enumForum_Public_ButtonSave];
                btnCancel1.Text = Proxy[EnumText.enumForum_Public_ButtonCancel];
                btnCancel2.Text = Proxy[EnumText.enumForum_Public_ButtonCancel];
                btnSelectUser.Text = Proxy[EnumText.enumForum_Ban_ButtonSelect];
                //this.RegularExpressionValidatorIP.ErrorMessage = Proxy[EnumText.enumForum_Ban_ErrorIpFormat];
                //this.RegularExpressionValidatorStartIP.ErrorMessage = Proxy[EnumText.enumForum_Ban_ErrorStartIPFormat];
                //this.RegularExpressionValidatorEndIP.ErrorMessage = Proxy[EnumText.enumForum_Ban_ErrorEndIPFormat];
                this.CustomValidatorExpireFormat.ErrorMessage = Proxy[EnumText.enumForum_Ban_ErrorExpireInteger];
                this.RequiredFieldValidatorUser.ErrorMessage =Proxy[EnumText.enumForum_Ban_ErrorUserRequire];
                //this.RequiredFieldValidatorIP.ErrorMessage = Proxy[EnumText.enumForum_Ban_ErrorIpRequire];
                //this.RequiredFieldValidatorStartIP.ErrorMessage = Proxy[EnumText.enumForum_Ban_ErrorStartIpRequire];
                //this.RequiredFieldValidatorEndIP.ErrorMessage = Proxy[EnumText.enumForum_Ban_ErrorEndIpRequire];
                this.CustomValidatorExpireRequired.ErrorMessage = Proxy[EnumText.enumForum_Ban_ErrorExpireTimeRequire];
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion Initialize Language
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumBan);
                Master.Page.Title = Proxy[EnumText.enumForum_Ban_TitleBanUser]; 
                if (!IsPostBack)
                {
                    PageInit();
                }
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad+ exp.Message;
                LogHelper.WriteExceptionLog(exp);

            }
        }

        protected void PageInit()
        {
            //this.rlistType.Items.Add("Banned User");
            //this.rlistType.Items.Add("Banned IP");
            //this.rlistIpMode.Items.Add("IP");
            //this.rlistIpMode.Items.Add("IP Range");
            ddlExpire.Items.Add(new ListItem(BanTimeTypePermanent, Convert.ToInt32(EnumBanExpireType.Permanent).ToString()));
            ddlExpire.Items.Add(new ListItem(BanTimeTypeDays, Convert.ToInt32(EnumBanExpireType.Days).ToString()));
            ddlExpire.Items.Add(new ListItem(BanTimeTypeHours, Convert.ToInt32(EnumBanExpireType.Hours).ToString()));
            //ddlExpire.Items.Add(new ListItem("Days", Convert.ToInt32(EnumBanExpireType.Days).ToString()));
            //ddlExpire.Items.Add(new ListItem("Months", Convert.ToInt32(EnumBanExpireType.Months).ToString()));
            //ddlExpire.Items.Add(new ListItem("Years", Convert.ToInt32(EnumBanExpireType.Years).ToString()));
            
            //this.rlistType.SelectedIndex = Convert.ToInt32(EnumBanType.User);
            //this.rlistIpMode.SelectedIndex = 0;
            //this.ddlExpire.SelectedIndex = 0;
            this.ddlExpire.Attributes.Add("onchange", "SelectExprie();");
            if(ddlExpire.SelectedIndex ==0)
                txtExpire.Style["display"] = "none";
            //PageControlDisplay();
#if OPENSOURCE
#else
            InitDropDownList();
#endif
            ViewState["UserOrOperatorKeyWord"] = "";
            ViewState["UserSortOrder"] = "Name";
            ViewState["UserDirect"] = "asc";
            BindUser();
        }

        #region Control Event
        //protected void rlistType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    PageControlDisplay();
        //}
        //protected void rlistIpMode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    PageControlDisplay();
        //}
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //EnumBanType type = this.rlistType.SelectedIndex == Convert.ToInt32(EnumBanType.User) ? EnumBanType.User : EnumBanType.IP;
                DateTime startDate = DateTime.UtcNow;
                DateTime endDate = this.GetEndDate(startDate);
                string note = this.txtNotes.Text;
                long startIP = 0;
                long endIP = 0;
                string banUserOrOperator = string.Empty;
                //if (this.rlistType.SelectedIndex == Convert.ToInt32(EnumBanType.User))
                //{
                    banUserOrOperator = this.txtUser.Text;
                //}
                //else
                //{
                //    if (this.rlistIpMode.SelectedIndex == 0)
                //    {
                //        startIP = endIP = IpHelper.DottedIP2LongIP(this.txtIP.Text);
                //    }
                //    else
                //    {
                //        startIP = IpHelper.DottedIP2LongIP(this.txtStartIP.Text);
                //        endIP = IpHelper.DottedIP2LongIP(this.txtEndIP.Text);
                //    }
                //}
                BanProcess.AddBan(this.UserOrOperatorId, this.SiteId, EnumBanType.User, startDate, endDate, note, banUserOrOperator, startIP, endIP);
                Response.Redirect(string.Format("Bans.aspx?siteId={0}", this.SiteId));
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorSaveBan + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("Bans.aspx?siteId={0}", this.SiteId));
            }
            catch(Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_RedirectError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion
        
        #region Private Function
        private DateTime GetEndDate(DateTime startTime)
        {
            DateTime endDate = startTime;
            switch ((EnumBanExpireType)Convert.ToInt32(this.ddlExpire.SelectedIndex))
            {
                case EnumBanExpireType.Days://Days
                    endDate = endDate.AddDays(Convert.ToInt32(txtExpire.Text.Trim()));
                    break;
                case EnumBanExpireType.Hours://Hours
                    endDate = endDate.AddHours(Convert.ToInt32(txtExpire.Text.Trim()));
                    break;
                //case EnumBanExpireType.Days://Days
                //    endDate = endDate.AddDays(expire);
                //    break;
                //case EnumBanExpireType.Months://Months
                //    endDate = endDate.AddMonths(expire);
                //    break;
                //case EnumBanExpireType.Years://Years
                //    endDate = endDate.AddYears(expire);
                //    break;
                case EnumBanExpireType.Permanent://Permanent
                    endDate = endDate.AddYears(100);
                    break;
            }
            return endDate;
        }
        //private void PageControlDisplay()
        //{
        //    if (this.rlistType.SelectedIndex == Convert.ToInt32(EnumBanType.User))
        //    {
        //        this.userType.Visible = true;
        //        this.IPType.Visible = false;
        //        this.IPSimple.Visible = false;
        //        this.IPAdvanced1.Visible = false;
        //        this.IPAdvanced2.Visible = false;
        //    }
        //    else
        //    {
        //        if (this.rlistIpMode.SelectedIndex == 0)
        //        {
        //            this.userType.Visible = false;
        //            this.IPType.Visible = true;
        //            this.IPSimple.Visible = true;
        //            this.IPAdvanced1.Visible = false;
        //            this.IPAdvanced2.Visible = false;
        //        }
        //        else
        //        {
        //            this.userType.Visible = false;
        //            this.IPType.Visible = true;
        //            this.IPSimple.Visible = false;
        //            this.IPAdvanced1.Visible = true;
        //            this.IPAdvanced2.Visible = true;
        //        }
        //    }
        //}
        #endregion

        protected string GetItemType(UserOrOperator userOrOperator)
        {
            if (CommFun.IfOperator(userOrOperator)) 
            {
                OperatorWithPermissionCheck operatorTmp = userOrOperator as OperatorWithPermissionCheck;
                if (operatorTmp.IfAdmin) return UserTypeAdmin;
                else return UserTypeOperator;
            }
            else return UserTypeRegisterUser;
        }
        void InitDropDownList()
        {
            trUserType.Visible = true;
            ddlUserType.Items.Add(UserTypeAll);
            ddlUserType.Items.Add(UserTypeAdmin);
            ddlUserType.Items.Add(UserTypeOperator);
            ddlUserType.Items.Add(UserTypeRegisterUser);
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtUser.Text = this.hdUserName.Value;
                if (ddlExpire.SelectedIndex == 0)
                    txtExpire.Style["display"] = "none";
                else
                    txtExpire.Style["display"] = "";
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorSelectUser + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void btnQueryUser_Click(object sender, EventArgs e)
        {
            try
            {
                this.aspnetPager1.PageIndex = 0;
                ViewState["UserOrOperatorKeyWord"] = txtKeyWord.Text;
                this.BindUser();
                ScriptManager.RegisterStartupScript(divScript, typeof(string), "showUser", "javascript:showWindow('divThickInner', 'divThickOuter');", true);
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorQueryUser + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #region pager event
        protected void aspnetPager1_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
        {
            try
            {
                BindUser();
                ScriptManager.RegisterStartupScript(divScript, typeof(string), "showUser", "javascript:showWindow('divThickInner', 'divThickOuter');", true);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_User_PageUserManagementListErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void aspnetPager1_ChangePageSize(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.ChangePageSizeEventArgs ce)
        {
            try
            {
                BindUser();
                ScriptManager.RegisterStartupScript(divScript, typeof(string), "showUser", "javascript:showWindow('divThickInner', 'divThickOuter');", true);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_User_PageUserManagementListErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion pager event
        private void BindUser()
        {
            string emailOrDisplayNameKeyword = ViewState["UserOrOperatorKeyWord"].ToString();
            //int userGroupId = Convert.ToInt32(ViewState["groupId"]);
#if OPENSOURCE
            bool ifGetAll = true;
#else
            bool ifGetAll = false;
#endif
            bool IfGetAdmin = false;
            EnumUserType userType = EnumUserType.Operator;
            if (ddlUserType.SelectedIndex == 0) ifGetAll = true;
            else if (ddlUserType.SelectedIndex == 1) IfGetAdmin = true;
            else if (ddlUserType.SelectedIndex == 2) userType = EnumUserType.Operator;
            else if (ddlUserType.SelectedIndex == 3) userType = EnumUserType.User;

            int recordsCount;
            string sortField = ViewState["UserSortOrder"].ToString();
            string sortDirection = ViewState["UserDirect"].ToString();

            UserOrOperator[] userOrOperators = UserProcess.GetNotDeletedAndNotBannedUserOrOperatorByQueryAndPaging(
                SiteId,UserOrOperatorId,this.aspnetPager1.PageIndex+1,this.aspnetPager1.PageSize,emailOrDisplayNameKeyword,userType,ifGetAll,IfGetAdmin,out recordsCount, sortField, sortDirection);

            if (recordsCount == 0)
            {
                aspnetPager1.Visible = false;
                rpUser.DataSource = null;
                rpUser.DataBind();
            }
            else
            {
                aspnetPager1.CWCDataBind(rpUser, userOrOperators, recordsCount);
                aspnetPager1.Visible = true;
            }
        }

        #region Select User Sort
        protected void UserSort(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                string commadName = lbtn.CommandName;
                imgUserEmail.Visible = false;
                imgUserDisplayName.Visible = false;
                imgUserUserType.Visible = false;
                imgUserJoinedTime.Visible = false;
                switch (commadName)
                {
                    case "UserEmail":
                        imgUserEmail.Visible = true;
                        ViewState["UserSortOrder"] = "Email";
                        if (ViewState["UserDirect"].ToString().Equals("desc"))
                        {
                            ViewState["UserDirect"] = "asc";
                            imgUserEmail.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState["UserDirect"] = "desc";
                            imgUserEmail.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                    case "UserDisplayName":
                        imgUserDisplayName.Visible = true;
                        ViewState["UserSortOrder"] = "Name";
                        if (ViewState["UserDirect"].ToString().Equals("desc"))
                        {
                            ViewState["UserDirect"] = "asc";
                            imgUserDisplayName.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState["UserDirect"] = "desc";
                            imgUserDisplayName.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                    case "UserJoinedTime":
                        imgUserJoinedTime.Visible = true;
                        ViewState["UserSortOrder"] = "JoinedTime";
                        if (ViewState["UserDirect"].ToString().Equals("desc"))
                        {
                            ViewState["UserDirect"] = "asc";
                            imgUserJoinedTime.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState["UserDirect"] = "desc";
                            imgUserJoinedTime.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                    case "UserUserType":
                        imgUserUserType.Visible = true;
                        ViewState["UserSortOrder"] = "UserType";
                        if (ViewState["UserDirect"].ToString().Equals("desc"))
                        {
                            ViewState["UserDirect"] = "asc";
                            imgUserUserType.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState["UserDirect"] = "desc";
                            imgUserUserType.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                }
                BindUser();
                ScriptManager.RegisterStartupScript(divScript, typeof(string), "showUser", "javascript:showWindow('divThickInner', 'divThickOuter');", true);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_UserGroups_ErrorSort] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion Sort
    }
}
