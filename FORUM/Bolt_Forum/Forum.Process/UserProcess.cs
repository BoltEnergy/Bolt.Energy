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
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Common;
using System.Web;
using System.Web.Configuration;


namespace Com.Comm100.Forum.Process
{
    public class UserProcess
    {
        public static void DeleteUser(int siteId, int operatingUserOrOperatorId, int userId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UserWithPermissionCheck user = new UserWithPermissionCheck(conn, transaction, userId, 0 ,operatingUserOrOperator);
                user.Delete();

                transaction.Commit();
            }
            catch (Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally 
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void ApproveUserRegistration(int siteId, int operatingUserOrOperatorId, int userId)
        {
            SqlConnectionWithSiteId siteConn = null;
            SqlTransaction siteTransaction = null;
            SqlConnection generalConn = null;
            try
            {
                siteConn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(siteConn);
                generalConn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(generalConn);

                siteTransaction = siteConn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(siteConn, siteTransaction, null, operatingUserOrOperatorId);
                UserWithPermissionCheck tmpUser = new UserWithPermissionCheck(siteConn, siteTransaction, userId, 0, operatingUserOrOperator);
                tmpUser.ApproveRegistration();
                #region Send Email
                SiteSettingWithPermissionCheck siteSetting = new SiteSettingWithPermissionCheck(siteConn, siteTransaction, generalConn, null, operatingUserOrOperator);
                string siteName = siteSetting.SiteName.ToString();

                string email = tmpUser.Email;
                string name = tmpUser.DisplayName;

                #region Init from Config
                string fromEmail = WebConfigurationManager.AppSettings["smtpFromAddress"];
                string smtpServer = WebConfigurationManager.AppSettings["smtpServer"];
                string fromUserName = WebConfigurationManager.AppSettings["smtpUserName"];
                string password = WebConfigurationManager.AppSettings["smtpPassword"];
                Boolean smtpEnableSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["smtpEnableSsl"]);
                Int32 smtpPort = Convert.ToInt32(WebConfigurationManager.AppSettings["smtpPort"]);

                string fromName = "COMM100 - Forum";
                string toEmail = email;
                string toName = name;
                string emailSubject = "User Moderate Success From " + siteName;
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

                #region Define the email content.
                emailContent += string.Format("<table width=\"100%\"><tr><td ><b>Hi {0}:</b></td></tr><tr><td colspan=\"2\"></td></tr>", toName.ToString());

                emailContent += string.Format("<tr><td >This is an email for you to tell  that your registration to the forum has been moderated successfully from the {0}.</td></tr><tr><td colspan=\"2\"></td></tr>", siteName);

                emailContent += string.Format("<tr><td ><br/></td></tr>");
                emailContent += string.Format("<tr><td >Please click the following hyper link to login to the forum.</td></tr>");

                emailContent += string.Format("<tr><td ><b><a href=\"http://{0}/Login.aspx", siteLocal + sitePath);

                emailContent += string.Format("?email={0}", toEmail.ToString());
                emailContent += string.Format("&siteId={0}", siteId);
                emailContent += string.Format("&\" target=\"_blank\">Click here to login to the forum.</a></b></td></tr>");
                emailContent += string.Format("<tr><td ><br/></td></tr>");

                emailContent += string.Format("<tr><td >Sincerely yours,</td></tr><tr><td colspan=\"2\"></td></tr>");
                emailContent += string.Format("<tr><td >{0}</td></tr>", siteName);
                #endregion

                Mail.SendEmail(fromEmail, fromName, toEmail, toName, emailSubject, emailContent, true, smtpServer, fromUserName, password, smtpEnableSsl, smtpPort);

                #endregion Send Email
                siteTransaction.Commit();
            }
            catch
            {
                DbHelper.RollbackTransaction(siteTransaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(siteConn);
                DbHelper.CloseConn(generalConn);
            }
        }

        public static void RefuseUserRegistration(int siteId, int operatingUserOrOperatorId, int userId)
        {
            SqlConnectionWithSiteId siteConn = null;
            SqlTransaction siteTransaction = null;
            SqlConnection generalConn = null;

            try
            {
                siteConn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(siteConn);
                generalConn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(generalConn);

                siteTransaction = siteConn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(siteConn, siteTransaction, null, operatingUserOrOperatorId);
                UserWithPermissionCheck tmpUser = new UserWithPermissionCheck(siteConn, siteTransaction, userId, 0, operatingUserOrOperator);
                tmpUser.RefuseRegistration();
                #region Send Email

                SiteSettingWithPermissionCheck siteSetting = new SiteSettingWithPermissionCheck(siteConn, siteTransaction, generalConn, null, operatingUserOrOperator);
                string siteName = siteSetting.SiteName.ToString();

                string email = tmpUser.Email;
                string name = tmpUser.DisplayName;

                #region Init from Config
                string fromEmail = WebConfigurationManager.AppSettings["smtpFromAddress"];
                string smtpServer = WebConfigurationManager.AppSettings["smtpServer"];
                string fromUserName = WebConfigurationManager.AppSettings["smtpUserName"];
                string password = WebConfigurationManager.AppSettings["smtpPassword"];
                Boolean smtpEnableSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["smtpEnableSsl"]);
                Int32 smtpPort = Convert.ToInt32(WebConfigurationManager.AppSettings["smtpPort"]);

                string fromName = "COMM100 - Forum";
                string toEmail = email;
                string toName = name;
                string emailSubject = "User Moderate Faied from " + siteName;
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


                #region Define the email content.
                emailContent += string.Format("<table width=\"100%\"><tr><td ><b>Sorry {0}:</b></td></tr><tr><td colspan=\"2\"></td></tr>", toName.ToString());

                emailContent += string.Format("<tr><td >This is an email to tell you that your registration to the forum has been refused from the {0}</td></tr><tr><td colspan=\"2\"></td></tr>", siteName);

                emailContent += string.Format("<tr><td ><br/></td></tr>");
                emailContent += string.Format("<tr><td >Please click the following hyper link to see the homepage of the forum .</td></tr>");

                emailContent += string.Format("<tr><td ><b><a href=\"http://{0}/Default.aspx", siteLocal + sitePath);


                emailContent += string.Format("?siteId={0}", siteId);
                emailContent += string.Format("\" target=\"_blank\">Click here to go to the homepage.</a></b></td></tr>");
                emailContent += string.Format("<tr><td ><br/></td></tr>");


                emailContent += string.Format("<tr><td >Sincerely yours,</td></tr><tr><td colspan=\"2\"></td></tr>");
                emailContent += string.Format("<tr><td >{0}</td></tr>", siteName);
                #endregion

                Mail.SendEmail(fromEmail, fromName, toEmail, toName, emailSubject, emailContent, true, smtpServer, fromUserName, password, smtpEnableSsl, smtpPort);

                #endregion Send Email

                siteTransaction.Commit();
            }
            catch
            {
                DbHelper.RollbackTransaction(siteTransaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(siteConn);
                DbHelper.CloseConn(generalConn);
            }
        }
        
        //Not Delete?
        public static UserOrOperator GetNotDeletedUserOrOperatorById(int siteId, int userOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingOperator = UserOrOperatorFactory.CreateNotDeletedUserOrOperator(conn, null, null, userOrOperatorId);
                return operatingOperator;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static UserOrOperator GetUserOrOpertorById(int siteId,  int userOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, userOrOperatorId);
                return userOrOperator;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static UserPermissionCache GetUserPermissionCacheById(int siteId, int userOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, userOrOperatorId);
                UserPermissionCache userPermission;
                userOrOperator.GetUserPermissionCache(out userPermission);
                return userPermission;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static UserWithPermissionCheck GetNotDeletedUserById(int siteId, int operatingUserOrOperatorId, int userId)
        {
            SqlConnectionWithSiteId conn = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                UsersWithPermissionCheck users = new UsersWithPermissionCheck(conn, null, operatingUserOrOperator);
                return users.GetNotDeletedUserById(userId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static UserWithPermissionCheck[] GetAllUserNotDelete(int siteId,int userOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);

                DbHelper.OpenConn(conn);

                UserOrOperator user = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, userOrOperatorId);

                UsersWithPermissionCheck up = new UsersWithPermissionCheck(conn, null, user);

                return up.GetAllUserNotDelete(user);
            }
            catch
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static UserWithPermissionCheck[] GetNotDeletedUsersByQueryAndPaging(int siteId, int operatingUserOrOperatorId, int pageIndex,
            int pageSize, string orderField, string EmailOrdisplayNameKeyWord, out int count)
        {
            SqlConnectionWithSiteId conn = null;
            EmailOrdisplayNameKeyWord = EmailOrdisplayNameKeyWord.Trim();

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                UsersWithPermissionCheck users = new UsersWithPermissionCheck(conn, null, operatingUserOrOperator);
                return users.GetNotDeletedUsersByQueryAndPaging(pageIndex, pageSize, orderField, EmailOrdisplayNameKeyWord, out count);

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

        public static UserWithPermissionCheck[] GetNotModeratedUsersByPaging(int siteId, int operatingUserOrOperatorId, int pageIndex,
            int pageSize, string orderField, string displayNameKeyWord)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            displayNameKeyWord = displayNameKeyWord.Trim();
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                UsersWithPermissionCheck usersWithPermissiongCheck =
                    new UsersWithPermissionCheck(conn, transaction, operatingUserOrOperator);

                UserWithPermissionCheck[] notModeratedUsers =
                    usersWithPermissiongCheck.GetNotModeratedUsersByPaging(pageIndex, pageSize, orderField,
                    displayNameKeyWord, operatingUserOrOperator);

                return notModeratedUsers;
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

        public static int GetCountOfNotModeratedUsersBySearch(int siteId, int operatingUserOrOperatorId, string emailOrDisplayNameKeyWord)
        {
            SqlConnectionWithSiteId conn = null;

            emailOrDisplayNameKeyWord = emailOrDisplayNameKeyWord.Trim();
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                UsersWithPermissionCheck usersWithPermissiongCheck =
                    new UsersWithPermissionCheck(conn, null, operatingUserOrOperator);

                return usersWithPermissiongCheck.GetCountOfNotModeratedUsersBySearch(emailOrDisplayNameKeyWord);
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

        public static UserWithPermissionCheck[] GetUsersWhichNotEmailVerify(int siteId, int operatingUserOrOperatorId, string emailOrDispalyNameKeyword, int pageIndex, int pageSize, string orderField, string orderDirection,out int count)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UsersWithPermissionCheck users = new UsersWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return users.GetUsersNotEmailVerfyByQueryAndPaging(pageIndex, pageSize, orderField, orderDirection, emailOrDispalyNameKeyword, operatingUserOrOperator, out count);
            }
            catch
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static bool IfUserOrOperatorBanById(int siteId, int userOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, userOrOperatorId);
                return userOrOperator.IfBanById();
            }
            catch (Exception)
            {
                throw;
            }
            finally 
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void SetUserActive(int siteId, int operatingUserOrOperatorId, int userId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                string ipAddr = HttpContext.Current.Request.ServerVariables["Remote_Addr"].ToString();
                Int64 ip = IpHelper.DottedIP2LongIP(ipAddr);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                UserWithPermissionCheck user = new UserWithPermissionCheck(conn, null, userId, ip, operatingUserOrOperator);
                user.SetActive();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void SetUserInActive(int siteId, int operatingUserOrOperatorId, int userId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                string ipAddr = HttpContext.Current.Request.ServerVariables["Remote_Addr"].ToString();
                Int64 ip = IpHelper.DottedIP2LongIP(ipAddr);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                UserWithPermissionCheck user = new UserWithPermissionCheck(conn, null, userId, ip, operatingUserOrOperator);
                user.SetInactive();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void UpdateUser(int siteId, int operatingUserOrOperatorId, int userId, 
            string email, string displayName, string firstName, string lastName, int age,
            EnumGender gender, string company, string occupation, string phone, string fax, string interests,
            string homepage, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender,
            bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests,
            bool ifShowHomePage, List<int> userGroupIds, int score, int reputation)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            #region Trim
            email = email.Trim();
            displayName = displayName.Trim();
            firstName = firstName.Trim();
            lastName = lastName.Trim();
            company = company.Trim();
            occupation = occupation.Trim();
            phone = phone.Trim();
            fax = fax.Trim();
            interests = interests.Trim();
            homepage = homepage.Trim();
            #endregion Trim
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction(IsolationLevel.Serializable);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UserWithPermissionCheck user = new UserWithPermissionCheck(conn, transaction, userId, 0, operatingUserOrOperator);
                user.Update(email, displayName, firstName, lastName, age, gender, company, occupation, phone, fax, interests, homepage, ifShowEmail, ifShowUserName,
                    ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany, ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage, userGroupIds,score, reputation);
                transaction.Commit();
            }
            catch (Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void AddUser(int siteId, int operatingUserOrOperatorId,
            string email, string displayName, string password, string firstName, string lastName, int age,
            EnumGender gender, string company, string occupation, string phone, string fax, string interests,
            string homepage, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender,
            bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests,
            bool ifShowHomePage, List<int> userGroupIds)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            #region Trim
            email = email.Trim();
            displayName = displayName.Trim();
            password = Encrypt.EncryptPassword(password.Trim());
            firstName = firstName.Trim();
            lastName = lastName.Trim();
            company = company.Trim();
            occupation = occupation.Trim();
            phone = phone.Trim();
            fax = fax.Trim();
            interests = interests.Trim();
            homepage = homepage.Trim();
            #endregion Trim
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction(IsolationLevel.Serializable);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UsersWithPermissionCheck users = new UsersWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                users.Add(email, displayName, password, firstName, lastName, age, gender, company, occupation, phone, fax, interests, homepage, ifShowEmail, ifShowUserName, ifShowAge,
                    ifShowGender, ifShowOccupation, ifShowCompany, ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage, userGroupIds);

                transaction.Commit();
            }
            catch (Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }

        }

        public static UserOrOperator[] GetNotDeletedAndNotBannedUserOrOperatorByQueryAndPaging(int siteId, int operatingUserOrOperatorId, int pageIndex, int pageSize, string emailOrDisplayNameKeyword, EnumUserType userType, bool ifGetAll, bool ifGetAdmin, out int recordsCount, string orderField, string orderDirection)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                emailOrDisplayNameKeyword = emailOrDisplayNameKeyword.Trim();
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UsersOrOperatorsOfSite userOrOperatorsOfSite = new UsersOrOperatorsOfSite(conn, transaction);
                return userOrOperatorsOfSite.GetNotDeletedAndNotBannedUserOrOperatorsByQueryAndPaging(pageIndex, pageSize, emailOrDisplayNameKeyword, userType, ifGetAll, ifGetAdmin, out recordsCount, operatingUserOrOperator, orderField, orderDirection);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }

        }

        public static UserOrOperator[] GetNotDeletedAndNotBannedUserOrOperatorByQueryNameAndPaging(int siteId, int operatingUserOrOperatorId, int pageIndex, int pageSize, string displayNameKeyword, EnumUserType userType, bool ifGetAll, bool ifGetAdmin, out int recordsCount, string orderField, string orderDirection)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                displayNameKeyword = displayNameKeyword.Trim();
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UsersOrOperatorsOfSite userOrOperatorsOfSite = new UsersOrOperatorsOfSite(conn, transaction);
                return userOrOperatorsOfSite.GetNotDeletedAndNotBannedUserOrOperatorsByQueryNameAndPaging(pageIndex, pageSize, displayNameKeyword, userType, ifGetAll, ifGetAdmin, out recordsCount, operatingUserOrOperator, orderField, orderDirection);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }

        }

        public static UserOrOperator[] GetUserOrOperatorByQueryAndPaging(int siteId, int operatingUserOrOperatorId, int pageIndex, int pageSize, string emailOrDisplayNameKeyword, EnumUserType userType, bool ifGetAll, bool ifGetAdmin,string orderField,string orderDirection, out int recordesCount)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                emailOrDisplayNameKeyword = emailOrDisplayNameKeyword.Trim();
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UsersOrOperatorsOfSite userOrOperatorsOfSite = new UsersOrOperatorsOfSite(conn, transaction);
                return userOrOperatorsOfSite.GetUserOrOperatorByQueryAndPaging(pageIndex, pageSize, emailOrDisplayNameKeyword, userType, ifGetAll, ifGetAdmin, orderField, orderDirection, out recordesCount, operatingUserOrOperator);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static bool IfModerator(int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                if (operatingUserOrOperator is UserWithPermissionCheck)
                    return ((UserWithPermissionCheck)operatingUserOrOperator).IfModerator();
                else
                    return ((OperatorWithPermissionCheck)operatingUserOrOperator).IfModerator();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static bool CheckIfBannedInUI(int siteId, int operatingUserOrOperatorId, int forumId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                return CommFun.IfBannedInUI(operatingUserOrOperator, forumId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static bool IfUserBanned(int siteId, int operatingUserOrOperatorId, int forumId,int userId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UserOrOperator userBanned = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, operatingUserOrOperator, userId);
                return userBanned.IfBanById();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

    }
}