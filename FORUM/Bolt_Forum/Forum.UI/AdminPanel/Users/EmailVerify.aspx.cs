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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.AdminPanel.Users
{
    public partial class EmailVerify : AdminBasePage
    {
        #region Const
        private const string VSKEY_OrderField="orderField";
        private const string VSKEY_OrdeerDirection = "orderDirection";
        private const string IMGPATH_OerderUp = "~/images/sort_up.gif";
        private const string IMGPATH_OrderDown = "~/images/sort_down.gif";
        #endregion 
        string ErrorLoad;
        string ErrorSendVerifyEmail;
        string SendEmailSuccessInfo;
        protected override void InitLanguage()
        {
            try
            {
                SendEmailSuccessInfo = Proxy[EnumText.enumForum_EmailVerify_SendEmailSuccessInfo];//"Send Verification Email to {0} successfully!";
                this.lblTitle.Text = Proxy[EnumText.enumForum_EmailVerify_TitleEmailVerify];
                this.lblSubTitle.Text = Proxy[EnumText.enumForum_EmailVerify_SubtitleEmailVerify];
                ErrorLoad = Proxy[EnumText.enumForum_EmailVerify_PageEmailVerifyErrorLoad];
                ErrorSendVerifyEmail = Proxy[EnumText.enumForum_EmailVerify_PageEmailVerifyErrorSend];
                this.btnQuery1.Text = Proxy[EnumText.enumForum_User_ButtonQuery];
                this.btnQuery2.Text = Proxy[EnumText.enumForum_User_ButtonQuery];
                
            }
            catch(Exception exp)
            {
                this.lblMessage.Text=Proxy[EnumText.enumForum_Public_InitializatingLanguageError]+exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((AdminMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumEmailVerify);
                Master.Page.Title = Proxy[EnumText.enumForum_EmailVerify_TitleEmailVerify];
                if (!IsPostBack)
                {
                    this.Page.SetFocus(txtQuery);
                    Page.Form.DefaultButton = btnQuery1.UniqueID;
                    ViewState[VSKEY_OrderField] = "JoinedTime";
                    ViewState[VSKEY_OrdeerDirection] = "desc";
                    this.imgJoinedTime.Visible = true;
                    imgJoinedTime.ImageUrl = IMGPATH_OrderDown;
                    RefreshData();
                }
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void RefreshData()
        {
            string emailOrDisplayNameKeyword = this.txtQuery.Text;
            string orderField = ViewState[VSKEY_OrderField] as string;
            string orderDiction = ViewState[VSKEY_OrdeerDirection] as string;
            int count = 0;
            UserWithPermissionCheck[] users = UserProcess.GetUsersWhichNotEmailVerify(SiteId, UserOrOperatorId, emailOrDisplayNameKeyword,
                this.aspnetPager.PageIndex + 1, this.aspnetPager.PageSize, orderField, orderDiction, out count);
            if (count == 0)
            {
                this.rptEmailVerifyUsers.DataSource = null;
                this.rptEmailVerifyUsers.DataBind();
                this.aspnetPager.Visible = false;
            }
            else
            {
                aspnetPager.CWCDataBind(rptEmailVerifyUsers, users, count);
                this.aspnetPager.Visible = true;
            }

        }
        protected void rptEmailVerifyUsers_ItemCommand(object sender, CommandEventArgs e)
        {
            try
            {
                if(e.CommandName=="ResendEmail")
                {
                    try
                    {
                        int userId=Convert.ToInt32(e.CommandArgument);
                        UserOrOperator user=UserProcess.GetUserOrOpertorById(SiteId,userId);
                        LoginAndRegisterProcess.SendVerificationEmail(SiteId,user.Email,user.Password);
                        this.lblSuccess.Text = string.Format(SendEmailSuccessInfo,user.Email);
                    }
                    catch(Exception exp)
                    {
                        this.lblMessage.Text=ErrorSendVerifyEmail+exp.Message;
                        LogHelper.WriteExceptionLog(exp);
                    }
                }
                else if(e.CommandName=="EmailVerify")
                {
                    try
                    {
                        int userId=Convert.ToInt32(e.CommandArgument);
                        UserOrOperator user=UserProcess.GetUserOrOpertorById(SiteId,userId);
                        string url = string.Empty;
                        string siteLocal = System.Web.HttpContext.Current.Request.Url.Authority.ToString();
                        string sitePath;
                        string path = System.Web.HttpContext.Current.Request.ApplicationPath;
                        if (path == "/")
                            sitePath = "";
                        else
                            sitePath = path;
                        string siteHost = System.Web.HttpContext.Current.Request.Url.DnsSafeHost;
                        url = string.Format("http://{0}/EmailVerification.aspx", siteLocal + sitePath);
                        url += string.Format("?userId={0}", userId);
                        url += string.Format("&email={0}", user.Email);
                        url += string.Format("&siteId={0}", SiteId);
                        url += string.Format("&emailVerificationGuidTag={0}", user.ForgetPasswordGUIDTag);
                        this.txtEmailVerifyUrl.Text = url;
                        this.txtEmailVerifyUrl.ReadOnly = true;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "showWindow", "showWindow('divThickInner','divThickOuter');window.onload = init;", true);
                    }
                    catch(Exception exp)
                    {
                        this.lblMessage.Text = exp.Message;
                        LogHelper.WriteExceptionLog(exp);
                    }
                }
            }
            catch (Exception exp)
            {
                this.lblMessage.Text=exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }

        }

        protected void rptEmailVerifyUsers_sorting(object sender,EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;
            imgEmail.Visible = false;
            imgName.Visible = false;
            //imgId.Visible = false;
            imgJoinedTime.Visible = false;
            switch (lbtn.CommandName)
            {
                //case "Id":
                //    {
                //        imgId.Visible = true;
                //        ViewState[VSKEY_OrderField] = "Id";

                //        if (ViewState[VSKEY_OrdeerDirection].ToString().Equals("desc"))
                //        {
                //            ViewState[VSKEY_OrdeerDirection] = "asc";
                //            imgId.Visible = true;
                //            imgId.ImageUrl =IMGPATH_OerderUp;
                //        }
                //        else
                //        {
                //            ViewState[VSKEY_OrdeerDirection] = "desc";
                //            imgId.ImageUrl = IMGPATH_OrderDown;
                //        }
                //        break;
                //    }

                case "Email":
                    {
                        imgEmail.Visible = true;
                        ViewState[VSKEY_OrderField] = "Email";
                        if (ViewState[VSKEY_OrdeerDirection].ToString().Equals("desc"))
                        {
                            ViewState[VSKEY_OrdeerDirection] = "asc";

                            imgEmail.ImageUrl = IMGPATH_OerderUp;
                        }
                        else
                        {
                            ViewState[VSKEY_OrdeerDirection] = "desc";
                            imgEmail.ImageUrl = IMGPATH_OrderDown;
                        }
                        break;
                    }
                case "Name":
                    {
                        imgName.Visible = true;
                        ViewState[VSKEY_OrderField] = "Name";
                        if (ViewState[VSKEY_OrdeerDirection].ToString().Equals("desc"))
                        {
                            ViewState[VSKEY_OrdeerDirection] = "asc";

                            imgName.ImageUrl = IMGPATH_OerderUp;
                        }
                        else
                        {
                            ViewState[VSKEY_OrdeerDirection] = "desc";
                            imgName.ImageUrl = IMGPATH_OrderDown;
                        }
                        break;
                    }

                case "JoinedTime":
                    {
                        imgJoinedTime.Visible = true;
                        ViewState[VSKEY_OrderField] = "JoinedTime";
                        if (ViewState[VSKEY_OrdeerDirection].ToString().Equals("desc"))
                        {
                            ViewState[VSKEY_OrdeerDirection] = "asc";

                            imgJoinedTime.ImageUrl = IMGPATH_OerderUp;
                        }
                        else
                        {
                            ViewState[VSKEY_OrdeerDirection] = "desc";
                            imgJoinedTime.ImageUrl = IMGPATH_OrderDown;
                        }
                        break;
                    }
            }
            RefreshData();
        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.aspnetPager.PageIndex = 0;
                RefreshData();
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = exp.Message;
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
                lblMessage.Text =ErrorLoad+ exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        
    }
}
