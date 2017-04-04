
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
using Com.Comm100.Forum.UI.AdminPanel;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.WebControls;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.HelpDocument;
using Com.Comm100.Forum.Bussiness;
using System.Data;
using Com.Comm100.Forum.Language;
using Com.Comm100.Framework.FieldLength;
namespace Forum.UI.AdminPanel.Users
{
    public partial class UserList : AdminBasePage
    {

        public enum EnumExprieTime
        {
            Permanent,
            //Months,
            Days,
            Hours,
        }
        #region Const
        string VSKEY_OrderField="OrderField";
        string VSKEY_OrderDirection="OrderDirection";
        string VSKEY_EmailOrDisplayNameKeyWord="EmailOrDisplayNameKeyWord";
        #endregion

        protected bool IfUserBanById(int userId)
        {
            bool ifBan = false;
            try
            {
                ifBan = UserProcess.IfUserOrOperatorBanById(SiteId, userId);
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_User_ErrorCheckUserBan] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
            return ifBan;
        }

        protected string IfDisplay
        {
            get {
#if OPENSOURCE
                return "";
#else
                return "display:none";
#endif
            }
        }

        protected bool IfEnableMessage
        {
            get
            {
                bool ifEnableMessage = false;
                try
                {
                    ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(SiteId, CurrentUserOrOperator.UserOrOperatorId);
                    ifEnableMessage = forumFeature.IfEnableMessage;
                }
                catch (Exception exp)
                {
                    lblMessage.Text = Proxy[EnumText.enumForum_User_ErrorCheckUserBan] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }
                return ifEnableMessage;
            }
        }

        string BanTimeTypePermanent;
        string BanTimeTypeDays;
        string BanTimeTypeHours;
        protected override void InitLanguage()
        {
            try
            {
                BanTimeTypePermanent = Proxy[EnumText.enumForum_Ban_TimeTypePermanent];
                BanTimeTypeDays = Proxy[EnumText.enumForum_Ban_TimeTypeDays];
                BanTimeTypeHours = Proxy[EnumText.enumForum_Ban_TimeTypeHours];
                lblTitle.Text = Proxy[EnumText.enumForum_User_TitleUserManagementList];

                lblSubTitle.Text = Proxy[EnumText.enumForum_User_SubtitleUserManagementList];
                btnNewUser1.Text = Proxy[EnumText.enumForum_user_ButtonNewUser];
                btnNewUser2.Text = Proxy[EnumText.enumForum_user_ButtonNewUser];
                btnSend.Text = Proxy[EnumText.enumForum_User_ButtonSendMessage];
                btnBan.Text = Proxy[EnumText.enumForum_User_ButtonBan];
                Master.Page.Title = Proxy[EnumText.enumForum_User_TitleUserManagementList];
                this.btnQueryButton1.Text = Proxy[EnumText.enumForum_User_ButtonQuery];
                this.btnQueryButton2.Text = Proxy[EnumText.enumForum_User_ButtonQuery];
                this.requiredSubject.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorSubjectRequired];
                this.requiredExpireTime.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorExpireTimeInteger];
                this.VaildExpireTime.ErrorMessage = Proxy[EnumText.enumForum_User_ErrorExpireTimeInteger];
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + ex.Message;
                LogHelper.WriteExceptionLog(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumUserManage);
                this.Page.SetFocus(txtQuery);
                Page.Form.DefaultButton = btnQueryButton1.UniqueID;

                InitImageAlign();
                if (!IsPostBack)
                {
                    InitTextBoxMaxLength();
                    BindddlExpireTime();

                    if(ViewState[VSKEY_OrderField]==null)
                        ViewState[VSKEY_OrderField] = "LastLoginTime";
                    if(ViewState[VSKEY_OrderDirection]==null)
                        ViewState[VSKEY_OrderDirection] = "desc";
                    if(ViewState[VSKEY_EmailOrDisplayNameKeyWord]==null)
                        ViewState[VSKEY_EmailOrDisplayNameKeyWord] = "";

                    if (Request.QueryString["action"] != null)
                    {
                        CheckQueryString("id");
                        string action = Request.QueryString["action"].ToLower();
                        if (action == "delete")
                        {
                            DeleteUser();
                        }
                        else if (action == "active")
                        {
                            Active();
                        }
                        else if (action == "inactive")
                        {
                            Inactive();
                        }
                        else if (action == "liftban")
                        {
                            LiftBan();
                        }
                    }
                    if (Request.QueryString["pageindex"] != null && Request.QueryString["pagesize"] != null)
                    {
                        aspnetPager.PageIndex = Convert.ToInt32(Request.QueryString["pageindex"]);
                        aspnetPager.PageSize = Convert.ToInt32(Request.QueryString["pagesize"]);
                    }
                    //if (ddlExpireTime.SelectedIndex == 0)
                    //{
                    //    requiredExpireTime.Enabled = false;
                    //    VaildExpireTime.Enabled = false;
                    //   // txtExpireTime.Enabled = false;
                    //}
                    this.ddlExpireTime.Attributes.Add("onchange", "SelectExprie();");


                    RefreshData();
                }
            }
            catch (Exception exp)
            {

                lblMessage.Text = Proxy[EnumText.enumForum_User_PageUserManagementListErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }


        #region Private Function InitImageAlign
        private void InitImageAlign()
        {
            //imgId.Attributes.Add("align", "absmiddle");
            imgEmail.Attributes.Add("align", "absmiddle");
            imgName.Attributes.Add("align", "absmiddle");
            imgJoinedTime.Attributes.Add("align", "absmiddle");
            imgLastLoginTime.Attributes.Add("align", "absmiddle");
        }
        #endregion

        #region Private Function InitTextBoxMaxLength
        private void InitTextBoxMaxLength()
        {
            txtSubject.MaxLength = ForumDBFieldLength.Message_subjectFieldLength;
        }
        #endregion Private Function InitTextBoxMaxLength

        #region Private Function BindddlExpireTime
        private void BindddlExpireTime()
        {
            //ddlExpireTime.Items.Add(new ListItem("Minutes", Convert.ToInt32(EnumBanExpireType.Minutes).ToString()));
            //ddlExpireTime.Items.Add(new ListItem("Hours", Convert.ToInt32(EnumBanExpireType.Hours).ToString()));
            //ddlExpireTime.Items.Add(new ListItem("Days", Convert.ToInt32(EnumBanExpireType.Days).ToString()));
            //ddlExpireTime.Items.Add(new ListItem("Months", Convert.ToInt32(EnumBanExpireType.Months).ToString()));
            //ddlExpireTime.Items.Add(new ListItem("Years", Convert.ToInt32(EnumBanExpireType.Years).ToString()));
            //ddlExpireTime.Items.Add(new ListItem("Permanent", Convert.ToInt32(EnumBanExpireType.Permanent).ToString()));
            ddlExpireTime.Items.Add(new ListItem(BanTimeTypePermanent, Convert.ToInt32(EnumBanExpireType.Permanent).ToString()));
            ddlExpireTime.Items.Add(new ListItem(BanTimeTypeDays, Convert.ToInt32(EnumBanExpireType.Days).ToString()));
            ddlExpireTime.Items.Add(new ListItem(BanTimeTypeHours, Convert.ToInt32(EnumBanExpireType.Hours).ToString()));
        }
        #endregion Private Function BindddlExpireTime

        #region Private Function DeleteUser
        private void DeleteUser()
        {
            int userId = Convert.ToInt32(Request.QueryString["id"]);
            UserProcess.DeleteUser(this.SiteId, this.UserOrOperatorId, userId);
            //List<string> sessionIdList = new List<string>();
            //Cache.Insert(string.Format(ConstantsHelper.CacheKey_InactivedOrDeletedUserId, SiteId, userId), sessionIdList, null, DateTime.Now.AddMinutes(Session.Timeout), TimeSpan.Zero);
        }
        #endregion Private Function DeleteUser

        #region Private Function Active
        private void Active()
        {
            int userId = Convert.ToInt32(Request.QueryString["id"]);
            UserProcess.SetUserActive(SiteId, UserOrOperatorId, userId);
            lblSuccess.Text = Proxy[EnumText.enumForum_User_SuccessActiveUser];
        }
        #endregion Private Function Active

        #region Private Function Inactive
        private void Inactive()
        {
            int userId = Convert.ToInt32(Request.QueryString["id"]);
            UserProcess.SetUserInActive(SiteId, UserOrOperatorId, userId);
            lblSuccess.Text = Proxy[EnumText.enumForum_User_SuccessInactiveUser];
        }
        #endregion Private Function Inactive

        #region Private Function LiftBan
        private void LiftBan()
        {
            int userId = Convert.ToInt32(Request.QueryString["id"]);
            BanProcess.LiftUserOrOperatorBan(SiteId, CurrentUserOrOperator.UserOrOperatorId, userId);
            lblSuccess.Text = Proxy[EnumText.enumForum_User_SuccessLiftBan];
        }
        #endregion

        protected void RefreshData()
        {
            try
            {
                int recordsCount = 0;
                string EmailOrdisplayNameKeyWord = ViewState[VSKEY_EmailOrDisplayNameKeyWord].ToString();
                string orderField = (string)ViewState[VSKEY_OrderField] + " " + (string)ViewState[VSKEY_OrderDirection];
                Com.Comm100.Forum.Bussiness.UserWithPermissionCheck[] users = UserProcess.GetNotDeletedUsersByQueryAndPaging(this.SiteId, this.UserOrOperatorId, this.aspnetPager.PageIndex + 1, this.aspnetPager.PageSize, orderField, EmailOrdisplayNameKeyWord, out recordsCount);
                if (recordsCount == 0)
                {
                    aspnetPager.Visible = false;
                    gdvUserList.DataSource = null;
                    gdvUserList.DataBind();
                }
                else
                {
                    aspnetPager.CWCDataBind(gdvUserList, users, recordsCount);
                    aspnetPager.Visible = true;
                }
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_User_PageUserManagementListErrorGet] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
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
                lblMessage.Text = Proxy[EnumText.enumForum_User_PageUserManagementListErrorLoad] + exp.Message;
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
                lblMessage.Text = Proxy[EnumText.enumForum_User_PageUserManagementListErrorLoad] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void btnQueryButton_Click(object sender, EventArgs e)
        {

            try
            {
                this.aspnetPager.PageIndex = 0;
                ViewState[VSKEY_EmailOrDisplayNameKeyWord] = txtQuery.Text;
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_User_PageUserManagementListErrorQuery] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        #region Sort
        protected void btnSort_click(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;
            imgEmail.Visible = false;
            imgName.Visible = false;
            //imgId.Visible = false;
            imgJoinedTime.Visible = false;
            imgLastLoginTime.Visible = false;
            switch (lbtn.ID)
            {
                /* 
                 * remove
                 * 
                case "lbtnId":
                    {
                        imgId.Visible = true;
                        ViewState["SortOrder"] = "Id";

                        if (ViewState["Direct"].ToString().Equals("desc"))
                        {
                            ViewState["Direct"] = "asc";
                            imgId.Visible = true;
                            imgId.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState["Direct"] = "desc";
                            imgId.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                    }
                */
                case "lbtnEmail":
                    {
                        imgEmail.Visible = true;
                        ViewState[VSKEY_OrderField] = "Email";
                        if (ViewState[VSKEY_OrderDirection].ToString().Equals("desc"))
                        {
                            ViewState[VSKEY_OrderDirection] = "asc";

                            imgEmail.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState[VSKEY_OrderDirection] = "desc";
                            imgEmail.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                    }



                case "lbtnDisplayName":
                    {
                        imgName.Visible = true;
                        ViewState[VSKEY_OrderField] = "Name";
                        if (ViewState[VSKEY_OrderDirection].ToString().Equals("desc"))
                        {
                            ViewState[VSKEY_OrderDirection] = "asc";

                            imgName.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState[VSKEY_OrderDirection] = "desc";
                            imgName.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                    }

                case "lbtnJoinedTime":
                    {
                        imgJoinedTime.Visible = true;
                        ViewState[VSKEY_OrderField] = "JoinedTime";
                        if (ViewState[VSKEY_OrderDirection].ToString().Equals("desc"))
                        {
                            ViewState[VSKEY_OrderDirection] = "asc";

                            imgJoinedTime.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState[VSKEY_OrderDirection] = "desc";
                            imgJoinedTime.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                    }
                case "lbtnLastLoginTime":
                    {
                        imgLastLoginTime.Visible = true;
                        ViewState[VSKEY_OrderField] = "LastLoginTime";
                        if (ViewState[VSKEY_OrderDirection].ToString().Equals("desc"))
                        {
                            ViewState[VSKEY_OrderDirection] = "asc";

                            imgLastLoginTime.ImageUrl = "~/images/sort_up.gif";
                        }
                        else
                        {
                            ViewState[VSKEY_OrderDirection] = "desc";
                            imgLastLoginTime.ImageUrl = "~/images/sort_down.gif";
                        }
                        break;
                    }
            }
            RefreshData();
        }
        #endregion Sort

        protected void btnNewUser_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("AddUser.aspx?pageindex=" + aspnetPager.PageIndex + "&pagesize=" + aspnetPager.PageSize + "&siteid=" + SiteId);
            }
            catch (Exception exp)
            {
                lblMessage.Text = lblMessage.Text = Proxy[EnumText.enumForum_Public_RedirectError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                int recipientId = Convert.ToInt32(hidRecipientId.Value);
                MessageProcess.SendMessage(SiteId, CurrentUserOrOperator.UserOrOperatorId, recipientId, txtSubject.Text, txtMessage.Text);
                lblSuccess.Text = Proxy[EnumText.enumForum_User_SuccessSendMessage];
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_User_ErrorSendMessage] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        #region Private Function GetEndDate
        private DateTime GetEndDate(DateTime startTime)
        {
            DateTime endDate = startTime;
            int expire;
            expire = Convert.ToInt32(txtExpireTime.Text.Trim());

            switch ((EnumExprieTime)Convert.ToInt32(this.ddlExpireTime.SelectedIndex))
            {
                //case EnumBanExpireType.Minutes://Minutes
                //    endDate = endDate.AddMinutes(expire);
                //    break;
                case EnumExprieTime.Hours://Hours
                    endDate = endDate.AddHours(expire);
                    break;
                case EnumExprieTime.Days://Days
                    endDate = endDate.AddDays(expire);
                    break;
                //case EnumBanExpireType.Months://Months
                //    endDate = endDate.AddMonths(expire);
                //    break;
                //case EnumBanExpireType.Years://Years
                //    endDate = endDate.AddYears(expire);
                //    break;
                case EnumExprieTime.Permanent://Permanent
                    endDate = endDate.AddYears(100);
                    break;
            }
            return endDate;
        }
        #endregion Private Function GetEndDate

        protected void btnBan_Click(object sender, EventArgs e)
        {
            try
            {
               
                int banUserId = Convert.ToInt32(hidBanUserId.Value);
                DateTime startDate = DateTime.UtcNow;
                DateTime endDate = this.GetEndDate(startDate);
                BanProcess.AddBan(SiteId, CurrentUserOrOperator.UserOrOperatorId, startDate, endDate, txtNote.Text, banUserId);
                txtExpireTime.Text = "";
                txtNote.Text = "";
                RefreshData();
                lblSuccess.Text = Proxy[EnumText.enumForum_User_SuccessBanUser];
                
            }
            catch (Exception exp)
            {
                lblMessage.Text = Proxy[EnumText.enumForum_User_ErrorBanUser] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
    }
}
