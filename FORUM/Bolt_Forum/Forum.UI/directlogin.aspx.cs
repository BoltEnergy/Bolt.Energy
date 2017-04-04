using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.UI.Common;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Framework.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Linq;
using System.Web.UI;



namespace Com.Comm100.Forum.UI
{

    public partial class directlogin : Com.Comm100.Forum.UI.UIBasePage
    {

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (SiteSession.DirectLoginUser != null)
            {
                DirectUserlogin();
            }
            else
                Response.Redirect(WebUtility.GetAppSetting(Constants.WK_BaseURL));
        }

        private void DirectUserlogin()
        {
            try
            {
                string useremail = SiteSession.DirectLoginUser.Email;
                string loginIp = SiteSession.DirectLoginUser.IpAddress;

                if (loginIp == "")
                {
                    try
                    {
                        loginIp = Request.ServerVariables[Constants.Remote_address];
                    }
                    catch { }
                }


                string strTimezoneOffset = CommonFunctions.ReadCookies("TimezoneOffset");
                double timezoneOffset = strTimezoneOffset.Length == 0 ? 0 : Convert.ToDouble(strTimezoneOffset);

                AddUpdateUserInfoInDB(SiteSession.DirectLoginUser);

                UserPermissionCache userPermissionList;
                SessionUser sessionUser = LoginAndRegisterProcess.UserOrOperatorLoginDirect(
                    this.SiteId, useremail, "", loginIp, timezoneOffset, false, false, out userPermissionList);

                if (string.IsNullOrEmpty(sessionUser.UserName))
                    sessionUser.UserName = useremail;

                SiteSession.CurrentUser = sessionUser;
                SiteSession.UserPermissionList = userPermissionList;
                this.CurrentUserOrOperator = sessionUser;
                try
                {
                    this.CheckIfUserOrOperatorBanned(0);
                }
                catch { }

                string url = "";
                if (Request.QueryString["ReturnUrl"] != null)
                {
                    //Response.Redirect(Request.QueryString["ReturnUrl"], false);
                    url = Request.QueryString["ReturnUrl"];
                }
                else
                {
                    //Response.Redirect(ViewState["UrlReferrer"].ToString(), false);
                    url = Convert.ToString(ViewState["UrlReferrer"]);
                }
                url = url.ToLower();
                if (!Com.Comm100.Forum.UI.Common.WebUtility.CanReturnPreUrl(url))
                {
                    url = this.UrlWithAuthorityAndApplicationPath + "Default.aspx?siteid=" + this.SiteId;
                }

                //SiteSession.Remove(Constants.SS_DirectLoginUser);
                if (string.IsNullOrEmpty(url))
                    Response.Redirect(WebUtility.GetAppSetting(Constants.WK_BaseURL));
                else
                    Response.Redirect(url, false);
            }
            catch (Exception)
            {
                Response.Redirect(WebUtility.GetAppSetting(Constants.WK_BaseURL));
            }
        }

        private Int32 AddUpdateUserInfoInDB(LoginUserBE user)
        {
            UserDAL objuser = new UserDAL();
            int userid = 0;

            if (!objuser.IfUserExist(user))
            {
                string userIp = user.IpAddress;
                if (userIp == "")
                {
                    try
                    {
                        userIp = Request.ServerVariables[Constants.Remote_address];
                    }
                    catch (Exception ex)
                    {

                    }
                }

                bool moderateStatus = false;
                bool verifyEmail = false;

                userid = LoginAndRegisterProcess.UserRegister(0, user.Email, user.FirstName + " " + user.LastName, WebUtility.GetAppSetting(Constants.WK_UserPassword), userIp, moderateStatus, verifyEmail);
            }

            userid = objuser.AddUpdateUserInfo(user);

            return userid;
        }

    }




}