using Com.Comm100.Forum.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevOne.Security.Cryptography.BCrypt;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;


namespace Com.Comm100.Forum.UI
{
    public static class Utility
    {
        public static string Encryptpassword(string password)
        {
            string hashedPassword = DevOne.Security.Cryptography.BCrypt.BCryptHelper.HashPassword(password, DevOne.Security.Cryptography.BCrypt.BCryptHelper.GenerateSalt(12));
            return hashedPassword;
        }

        public static bool CheckPassword(string enteredPassword, string hashedPassword)
        {
            bool pwdHash = DevOne.Security.Cryptography.BCrypt.BCryptHelper.CheckPassword(enteredPassword, hashedPassword);
            return pwdHash;
        }

        public static void AuthorizeDirectlogIn()
        {
            if (HttpContext.Current.Request.QueryString["action"] != null && HttpContext.Current.Request.QueryString["userid"] != null && HttpContext.Current.Request.QueryString["action"] == "directlogin")
            {
                if (HttpContext.Current.Request.UrlReferrer != null && HttpContext.Current.Request.UrlReferrer.ToString().ToLower().Contains(WebUtility.GetAppSetting("BoltURL")))
                {
                    string useremail = Convert.ToString(HttpContext.Current.Request.QueryString["userid"]);
                    HttpContext.Current.Response.Redirect("directlogin.aspx?action=directlogin&userid=" + useremail);
                }
                else
                    HttpContext.Current.Response.Redirect(WebUtility.GetAppSetting("BaseURL"));
            }
            else
                HttpContext.Current.Response.Redirect(WebUtility.GetAppSetting("BaseURL"));
        }

        //public static Int32 AddUpdateUserInfoInDB(LoginUserBE user)
        //{
        //    UserDAL objuser = new UserDAL();
        //    int userid = objuser.AddUpdateUserInfo(user);
        //    if (userid == 0)
        //    {
        //        string userIp = user.IpAddress;
        //        if (userIp == "")
        //        {
        //            try
        //            {
        //                userIp = HttpContext.Current.Request.ServerVariables[Constants.Remote_address];
        //            }
        //            catch (Exception ex)
        //            {

        //            }
        //        }

        //        RegistrationSettingWithPermissionCheck registrationSetting = SettingsProcess.GetRegistrationSettingBySiteId(UserOrOperatorId, SiteId);

        //        bool moderateStatus = registrationSetting.IfModerateNewUser;
        //        bool verifyEmail = registrationSetting.IfVerifyEmail;

        //        userid = LoginAndRegisterProcess.UserRegister(0, user.Email, user.FirstName + " " + user.LastName, WebUtility.GetAppSetting(Constants.WK_UserPassword), userIp, moderateStatus, verifyEmail);
        //    }

        //    return userid;
        //}
        
    }
}