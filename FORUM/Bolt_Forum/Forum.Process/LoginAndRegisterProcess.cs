#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Framework.Enum.Forum;

using System.Web.Configuration;

namespace Com.Comm100.Forum.Process
{
    public class LoginAndRegisterProcess
    {
        public static int UserRegister(int siteId, string email, string displayName,
            string password, string userIp, bool ifModearteUser, bool ifVerifyEmail)
        {

            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            email = email.Trim();
            displayName = displayName.Trim();

            try
            {
                password = Encrypt.EncryptPassword(password);
                long ip = IpHelper.DottedIP2LongIP(userIp);

                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                transaction = conn.SqlConn.BeginTransaction();

                UsersWithPermissionCheck users = new UsersWithPermissionCheck(conn, transaction, null);
                int userId = users.Register(siteId, email, displayName, password, ip, ifModearteUser, ifVerifyEmail);
                transaction.Commit();
                return userId;
            }
            catch (System.Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        //credentials check
        public static SessionUser UserOrOperatorLogin(int siteId, string email, string password, string loginIp, double timezoneOffset,bool ifAdmin,bool ifModerator, out UserPermissionCache userPermissionList)
        {
            SqlConnectionWithSiteId conn = null;
            email = email.Trim();
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UsersOrOperatorsOfSite usersOrOperatos = new UsersOrOperatorsOfSite(conn,null);
                UserOrOperator userOrOperator = usersOrOperatos.Login(email, password, loginIp, ifAdmin,ifModerator,out userPermissionList);

                SessionUser sessionUser = null;
                sessionUser = new SessionUser(userOrOperator.Id, siteId, CommFun.IfOperator(userOrOperator), timezoneOffset, EnumApplicationType.enumForum, userOrOperator.DisplayName);
                
                return sessionUser;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static SessionUser UserOrOperatorLoginDirect(int siteId, string email, string password, string loginIp, double timezoneOffset, bool ifAdmin, bool ifModerator, out UserPermissionCache userPermissionList)
        {
            SqlConnectionWithSiteId conn = null;
            email = email.Trim();
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UsersOrOperatorsOfSite usersOrOperatos = new UsersOrOperatorsOfSite(conn, null);
                UserOrOperator userOrOperator = usersOrOperatos.Login(email, password, loginIp, ifAdmin, ifModerator, out userPermissionList, false);

                SessionUser sessionUser = null;
                sessionUser = new SessionUser(userOrOperator.Id, siteId, CommFun.IfOperator(userOrOperator), timezoneOffset, EnumApplicationType.enumForum, userOrOperator.DisplayName);

                return sessionUser;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void SendVerificationEmail(int siteId, string email, string orignPassword)
        {
            SqlConnectionWithSiteId conn = null;

            email = email.Trim();

            SiteSetting siteSetting = SettingsProcess.GetSiteSettingBySiteId(siteId, 0);
            string siteName = siteSetting.SiteName.ToString();

            int userId = 0;
            string toEmail = email;
            string toName = "";
            //string toPassword = "";
            string emailVerificationTag = "";
            #region Init from Config
            string fromEmail = WebConfigurationManager.AppSettings["smtpFromAddress"];
            string fromName = "COMM100 - Forum";
            string smtpServer = WebConfigurationManager.AppSettings["smtpServer"];
            string fromUserName = WebConfigurationManager.AppSettings["smtpUserName"];
            string password = WebConfigurationManager.AppSettings["smtpPassword"];
            Boolean smtpEnableSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["smtpEnableSsl"]);
            Int32 smtpPort = Convert.ToInt32(WebConfigurationManager.AppSettings["smtpPort"]);
            #endregion
            string emailSubject = "Email Verification from " + siteName;
            string emailContent = "";
            string siteLocal = System.Web.HttpContext.Current.Request.Url.Authority.ToString();
            string sitePath;
            string path = System.Web.HttpContext.Current.Request.ApplicationPath;
            if (path == "/")
                sitePath = "";
            else
                sitePath = path;
            string siteHost = System.Web.HttpContext.Current.Request.Url.DnsSafeHost;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator user = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, email);//new UserOrOperator(conn, null, email);

                if (CommFun.IfUser(user))
                {
                    UserWithPermissionCheck userAccount = user as UserWithPermissionCheck;
                    userId = userAccount.Id;
                    toName = userAccount.DisplayName;
                    //toPassword = userAccount.Password;
                    emailVerificationTag = userAccount.EmailVerificationGUIDTag;
                }
                else if(CommFun.IfOperator(user))
                {
                    OperatorWithPermissionCheck operatorAccount = user as OperatorWithPermissionCheck;
                    userId = operatorAccount.Id;
                    toName = operatorAccount.DisplayName;

                    // toPassword = operatorAccount.Password;
                    
                    emailVerificationTag = operatorAccount.ForgetPasswordGUIDTag;
                }

                #region Define the email content.
                emailContent += string.Format("<table width=\"100%\"><tr><td ><b>Hi {0}:</b></td></tr><tr><td colspan=\"2\"></td></tr>", toName);

                emailContent += string.Format("<tr><td >Thank you very much for signing up for {0} </td></tr><tr><td colspan=\"2\"></td></tr>", siteName);
                emailContent += string.Format("<tr><td >Your {0} information is as follows:</td></tr>", siteName);

                emailContent += string.Format("<tr><td ><b>Email: {0}</b></td></tr>", toEmail);
                emailContent += string.Format("<tr><td ><b>Display Name: {0}</b></td></tr>", toName);
                emailContent += string.Format("<tr><td ><b>Password: {0}</b></td></tr>", orignPassword);

                emailContent += string.Format("<tr><td ><br/></td></tr>");
                emailContent += string.Format("<tr><td >Please click the following hyper link to verify your email address and finish your registration.</td></tr>");

                ///
                ///The url here should be changed.
                ///

                emailContent += string.Format("<tr><td ><b><a href=\"http://{0}/EmailVerification.aspx", siteLocal + sitePath);

                emailContent += string.Format("?userId={0}", userId);
                emailContent += string.Format("&email={0}", toEmail);
                emailContent += string.Format("&siteId={0}", siteId);
                emailContent += string.Format("&emailVerificationGuidTag={0}\" target=\"_blank\">Click here to verify your email address.</a></b></td></tr>", emailVerificationTag);
                emailContent += string.Format("<tr><td ><br/></td></tr>");

                emailContent += string.Format("<tr><td >The login URL is:</td></tr>");
                emailContent += string.Format("<tr><td ><b><a href=\"http://{0}/login.aspx", siteLocal + sitePath);

               
                emailContent += string.Format("?siteId={0}", siteId);
                emailContent += string.Format("\" target=\"_blank\">Login to the forum.</a></b></td></tr>");
                emailContent += string.Format("<tr><td ><br/></td></tr>");
                //emailContent += string.Format("<tr><td ><a href=\"http://{0}/login.aspx\" target=\"_blank\">Login to the forum.</a></td></tr><tr><td colspan=\"2\"></td></tr>", siteLocal + sitePath);
                //9.27 update
                //emailContent += string.Format("<tr><td >We are committed to providing highly reliable and secure live chat service for your business. If you need any help or have any questions, please do not hesitate to <a href=\"http://www.comm100.com/company/contactus.aspx\" target=\"_blank\">contact us</a>.</td></tr><tr><td colspan=\"2\"></td></tr>");

                emailContent += string.Format("<tr><td >Sincerely yours,</td></tr><tr><td colspan=\"2\"></td></tr>");
                emailContent += string.Format("<tr><td >{0}</td></tr>", siteName);                         
                //emailContent += string.Format("<tr><td ><a href=\"http://www.comm100.com/\" target=\"_blank\">Comm100</a> - Open Source & Free Hosted Customer Support Software</td></tr>");
                #endregion
                
            }
            catch (System.Exception)
            {
               
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }

            try
            {
                Mail.SendEmail(fromEmail, fromName, toEmail, toName, emailSubject, emailContent, true, smtpServer, fromUserName, password, smtpEnableSsl, smtpPort);
            }
            catch (System.Exception)
            {
                ExceptionHelper.ThrowRegisterSendVerificationEmailFailed();
            }
        }

        public static void SendResetPasswordEmail(int siteId, string email)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            /* Set UserOrOperatorId:0 IfOperator:false */
            SiteSetting siteSetting = SettingsProcess.GetSiteSettingBySiteId(siteId, 0);
            string siteName = siteSetting.SiteName.ToString();

            email = email.Trim();

            #region Init from Config
            string fromEmail = WebConfigurationManager.AppSettings["smtpFromAddress"];
            string fromName = "COMM100 - Forum";
            string toEmail = "";
            string toName = "";
            string smtpServer = WebConfigurationManager.AppSettings["smtpServer"];
            string fromUserName = WebConfigurationManager.AppSettings["smtpUserName"];
            string password = WebConfigurationManager.AppSettings["smtpPassword"];
            Boolean smtpEnableSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["smtpEnableSsl"]);
            Int32 smtpPort = Convert.ToInt32(WebConfigurationManager.AppSettings["smtpPort"]);
            string emailSubject = "Reset Password Email From " + siteName;
            string emailContent = "";
            string siteLocal = System.Web.HttpContext.Current.Request.Url.Authority.ToString();
            string sitePath;
            string path = System.Web.HttpContext.Current.Request.ApplicationPath;
            if (path == "/")
                sitePath = "";
            else
                sitePath = path;
            string siteHost = System.Web.HttpContext.Current.Request.Url.DnsSafeHost;
            #endregion

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                string forgetPasswordGuidTag = "";

                UserOrOperator user = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, email);
               
                if (CommFun.IfUser(user))
                {
                    UserWithPermissionCheck userAccount = user as UserWithPermissionCheck;

                    forgetPasswordGuidTag = System.Guid.NewGuid().ToString();
                    forgetPasswordGuidTag = forgetPasswordGuidTag.Replace("-", "");
                    userAccount.UpdateEmailVerificationGUIDTag(forgetPasswordGuidTag);
                    toName = userAccount.DisplayName;
                }
                else
                {
                    OperatorWithPermissionCheck operatorAccount = user as OperatorWithPermissionCheck;
                    forgetPasswordGuidTag = System.Guid.NewGuid().ToString();
                    forgetPasswordGuidTag = forgetPasswordGuidTag.Replace("-", "");
                    DateTime forgetPasswordDateTime = DateTime.UtcNow;
                    //operatorAccount.SetForgotPasswordTagInfo(forgetPasswordGuidTag, forgetPasswordDateTime);
                    operatorAccount.UpdateEmailVerificationGUIDTag(forgetPasswordGuidTag);
                    toName = operatorAccount.DisplayName;
                }

                toEmail = email;
                transaction.Commit();

                #region Define the email content.
                emailContent += string.Format("<table width=\"100%\"><tr><td ><b>Hi {0}:</b></td></tr><tr><td colspan=\"2\"></td></tr>", toName.ToString());

                emailContent += string.Format("<tr><td >This is an email to you for resetting your account password from the {0}.</td></tr><tr><td colspan=\"2\"></td></tr>", siteName);

                emailContent += string.Format("<tr><td ><br/></td></tr>");
                emailContent += string.Format("<tr><td >Please click the following hyper link to reset your password.</td></tr>");

                ///
                ///The url here should be changed.
                ///

                emailContent += string.Format("<tr><td ><b><a href=\"http://{0}/ResetPassword.aspx", siteLocal + sitePath);

                emailContent += string.Format("?email={0}", toEmail.ToString());
                emailContent += string.Format("&siteId={0}", siteId);
                emailContent += string.Format("&forgetPasswordGuidTag={0}\" target=\"_blank\">Go to reset your password.</a></b></td></tr>", forgetPasswordGuidTag);
                emailContent += string.Format("<tr><td ><br/></td></tr>");

                emailContent += string.Format("<tr><td >The homepage URL is:</td></tr>");         
                emailContent += string.Format("<tr><td ><b><a href=\"http://{0}/Default.aspx", siteLocal + sitePath);              
                emailContent += string.Format("?siteId={0}", siteId);
                emailContent += string.Format("\" target=\"_blank\">Go to the forum homepage</a></b></td></tr>");
                emailContent += string.Format("<tr><td ><br/></td></tr>");
                //emailContent += string.Format("<tr><td ><a href=\"http://{0}/login.aspx\" target=\"_blank\">Login to the forum.</a></td></tr><tr><td colspan=\"2\"></td></tr>",siteLocal+sitePath);

                //emailContent += string.Format("<tr><td >We are committed to providing highly reliable and secure live chat service for your business. If you need any help or have any questions, please do not hesitate to <a href=\"http://www.comm100.com/company/contactus.aspx\" target=\"_blank\">contact us</a>.</td></tr><tr><td colspan=\"2\"></td></tr>");

                emailContent += string.Format("<tr><td >Sincerely yours,</td></tr><tr><td colspan=\"2\"></td></tr>");
                //emailContent += string.Format("<tr><td >Comm100 Forum support team</td></tr>");
                emailContent += string.Format("<tr><td >{0}</td></tr>", siteName);
                //emailContent += string.Format("<tr><td ><a href=\"http://www.comm100.com/\" target=\"_blank\">Comm100</a> - Open Source & Free Hosted Customer Support Software</td></tr>");
                #endregion
            }
            catch (System.Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }

            try
            {
                Mail.SendEmail(fromEmail, fromName, toEmail, toName, emailSubject, emailContent, true, smtpServer, fromUserName, password, smtpEnableSsl, smtpPort);
            }
            catch (System.Exception)
            {
                ExceptionHelper.ThrowRegisterSendVerificationEmailFailed();
            }
        }

        public static void SetUserEmailVerificationStatusPassing(int siteId, string email, string verifyEmailGUIDTag)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transcation = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                transcation = conn.SqlConn.BeginTransaction();

                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transcation,null,email);
                if (!(userOrOperator is UserWithPermissionCheck))
                {
                    ExceptionHelper.ThrowUserNotExistWithEmailException(email);
                }
                UserWithPermissionCheck user = userOrOperator as UserWithPermissionCheck;

                user.SetEmailVerificationPassing(verifyEmailGUIDTag);

                /*
                  Update the email verification GUID tag after email verification
                  To prevent the second time verification
                */
                string EmailVerificationGuid = System.Guid.NewGuid().ToString();
                EmailVerificationGuid = EmailVerificationGuid.Replace("-", "");
                user.UpdateEmailVerificationGUIDTag(EmailVerificationGuid);

                transcation.Commit();
            }
            catch (System.Exception)
            {
                DbHelper.RollbackTransaction(transcation);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        ///
        ///Add without permission
        ///

        public static bool IfExistEmail(string email, int siteId)
        {
            SqlConnectionWithSiteId conn = null;

            email = email.Trim();

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UsersWithPermissionCheck users = new UsersWithPermissionCheck(conn, null, null);

                int usersCount = users.GetCountOfNotDeletedUsersByEmail(email);
                if (usersCount == 0)
                    return false;
                else
                    return true;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static int IfEmailVerified(string email, int siteId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                //UserWithPermissionCheck user = new UserWithPermissionCheck(conn, null, email, 0,  null);
                UserOrOperator useroroperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, email);

                if (useroroperator is User)
                    return (int)((UserWithPermissionCheck)useroroperator).EmailVerificationStatus;
                else
                    return 0;
                //return (int)user.EmailVerificationStatus;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static int IfModerated(int userId, int siteId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserWithPermissionCheck user = new UserWithPermissionCheck(conn, null, userId,0, null);//(conn, null, userId);
                return (int)user.ModerateStatus;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static bool IfVerified(int siteId, string email)
        {
            SqlConnectionWithSiteId conn = null;
            email = email.Trim();

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator user = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, email);
                if (CommFun.IfUser(user))
                { 
                    UserWithPermissionCheck userAccount = user as UserWithPermissionCheck;
                    if (userAccount.EmailVerificationStatus != EnumUserEmailVerificationStatus.enumNotVerified &&
                        userAccount.ModerateStatus != EnumUserModerateStatus.enumNotModerated &&
                        userAccount.ModerateStatus != EnumUserModerateStatus.enumRefused)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return true;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static bool IfForgetPasswordGuidTagMatch(int siteId, string email, string forgetPasswordGuidTag)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator user = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, email);

                if (CommFun.IfUser(user))
                {

                    if (((UserWithPermissionCheck)user).ForgetPasswordGUIDTag == forgetPasswordGuidTag)
                        return true;
                    else
                        return false;

                }
                else
                {
                    if (((OperatorWithPermissionCheck)user).ForgetPasswordGUIDTag == forgetPasswordGuidTag)
                        return true;
                    else
                        return false;
                }

            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void UpdateLastLoginTimeToCurrentTime(int siteId, int userOrOperatorId, bool ifOperator)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, userOrOperatorId);
                userOrOperator.UpdateLastLoginTimeToCurrentTime();

            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void ResetPasswordForFindPassword(int siteId, string email, string password)
        {
            password = Encrypt.EncryptPassword(password);

            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator user = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, email);

                if (CommFun.IfUser(user))
                {
                    UserWithPermissionCheck userAccount = user as UserWithPermissionCheck;
                    userAccount.ResetPassword(password);
                    //string newForgetPasswordGuidTag = System.Guid.NewGuid().ToString();
                    //newForgetPasswordGuidTag = newForgetPasswordGuidTag.Replace("-", "");
                    //userAccount.UpdateEmailVerificationGUIDTag(newForgetPasswordGuidTag);
                }
                else if(CommFun.IfOperator(user))
                {
                    OperatorWithPermissionCheck operatorAccount = user as OperatorWithPermissionCheck;
                    operatorAccount.ResetPassword(operatorAccount.Password, password);
                    //string newForgetPasswordGuidTag = System.Guid.NewGuid().ToString();
                    //newForgetPasswordGuidTag = newForgetPasswordGuidTag.Replace("-", "");
                    //DateTime forgetPasswordDateTime = DateTime.UtcNow;
                    //operatorAccount.SetForgotPasswordTagInfo(newForgetPasswordGuidTag, forgetPasswordDateTime);
                }

                transaction.Commit();
            }
            catch (System.Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
    }
}
