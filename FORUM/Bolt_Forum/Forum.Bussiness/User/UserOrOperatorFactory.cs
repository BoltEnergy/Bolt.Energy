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
using System.IO;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Common;
using System.Web;

namespace Com.Comm100.Forum.Bussiness
{
    public class UserOrOperatorFactory
    {
        public static UserOrOperator CreateUserOrOperator(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            UserOrOperator operatingUserOrOperator, int userOrOperatorId)
        {
            if (userOrOperatorId <= 0) return null;
            UserOrOperator userOrOperator = null;
            DataTable table = UserAccess.GetUserOrOperatorById(conn, transaction, userOrOperatorId);
            userOrOperator = CreateUserOrOperatorBase(conn, transaction, operatingUserOrOperator, userOrOperatorId, table);
            
            return userOrOperator;
        }

        public static UserOrOperator CreateNotDeletedUserOrOperator(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            UserOrOperator operatingUserOrOperator, int userOrOperatorId)
        {
            try
            {
                if (userOrOperatorId <= 0) return null;
                UserOrOperator userOrOperator = null;
                DataTable table = UserAccess.GetUserOrOperatorById(conn, transaction, userOrOperatorId);
                userOrOperator = CreateUserOrOperatorBase(conn, transaction, operatingUserOrOperator, userOrOperatorId, table);
                if (userOrOperator.IfDeleted)
                    ExceptionHelper.ThrowForumUserOrOperatorNotExistWithIdException(userOrOperatorId);
                return userOrOperator;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static UserOrOperator CreateUserOrOperatorBase(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            UserOrOperator operatingUserOrOperator, int userOrOperatorId, DataTable table)
        {
            UserOrOperator userOrOperator = null;
            if (table.Rows.Count <= 0)
            {
                ExceptionHelper.ThrowForumUserOrOperatorNotExistWithIdException(userOrOperatorId);
            }
            string ipAddr = HttpContext.Current.Request.ServerVariables["Remote_Addr"].ToString();
            Int64 ip = IpHelper.DottedIP2LongIP(ipAddr);
            if (((EnumUserType)Convert.ToInt16(table.Rows[0]["UserType"])) == EnumUserType.User)
            {
                #region Init User Infor
                int id = userOrOperatorId;
                bool ifActive = Convert.ToBoolean(table.Rows[0]["IfActive"]);
                bool ifDeleted = Convert.ToBoolean(table.Rows[0]["IfDeleted"]);
                string email = Convert.ToString(table.Rows[0]["Email"]);
                string name = Convert.ToString(table.Rows[0]["Name"]);
                string password = Convert.ToString(table.Rows[0]["Password"]);
                string forgetPasswordGUIDTag = Convert.ToString(table.Rows[0]["ForgetPasswordGUIDTag"]);
                DateTime forgetPasswordTagTime = Convert.ToDateTime(table.Rows[0]["ForgetPasswordTagTime"]);
                DateTime lastLoginTime = Convert.ToDateTime(table.Rows[0]["LastLoginTime"]);
                Int64 lastLoginIP = Convert.ToInt64(table.Rows[0]["LastLoginIP"]);
                DateTime joinedTime = Convert.ToDateTime(table.Rows[0]["JoinedTime"]);
                Int64 joinedIP = Convert.ToInt64(table.Rows[0]["JoinedIP"]);
                int numberOfPosts = table.Rows[0]["Posts"] is DBNull ? 0 : Convert.ToInt32(table.Rows[0]["Posts"]);
                bool ifShowEmail = Convert.ToBoolean(table.Rows[0]["IfShowEmail"]);
                string firstName = Convert.ToString(table.Rows[0]["FirstName"]);
                string lastName = Convert.ToString(table.Rows[0]["LastName"]);
                bool ifShowUserName = Convert.ToBoolean(table.Rows[0]["IfShowUserName"]);
                Int16 age = Convert.ToInt16(table.Rows[0]["Age"]);
                bool ifShowAge = Convert.ToBoolean(table.Rows[0]["IfShowAge"]);
                Int16 gender = Convert.ToInt16(table.Rows[0]["Gender"]);
                bool ifShowGender = Convert.ToBoolean(table.Rows[0]["IfShowGender"]);
                string occupation = Convert.ToString(table.Rows[0]["Occupation"]);
                bool ifShowOccupation = Convert.ToBoolean(table.Rows[0]["IfShowOccupation"]);
                string company = Convert.ToString(table.Rows[0]["Company"]);
                bool ifShowCompany = Convert.ToBoolean(table.Rows[0]["IfShowCompany"]);
                string phoneNumber = Convert.ToString(table.Rows[0]["PhoneNumber"]);
                bool ifShowPhoneNumber = Convert.ToBoolean(table.Rows[0]["IfShowPhoneNumber"]);
                string faxNumber = Convert.ToString(table.Rows[0]["FaxNumber"]);
                bool ifShowFaxNumber = Convert.ToBoolean(table.Rows[0]["IfShowFaxNumber"]);
                string interests = Convert.ToString(table.Rows[0]["Interests"]);
                bool ifShowInterests = Convert.ToBoolean(table.Rows[0]["IfShowInterests"]);
                string homePage = Convert.ToString(table.Rows[0]["HomePage"]);
                bool ifShowHomePage = Convert.ToBoolean(table.Rows[0]["IfShowHomePage"]);
                string signature = Convert.ToString(table.Rows[0]["Signature"]);
                bool ifCustomizeAvatar = Convert.ToBoolean(table.Rows[0]["IfCustomizeAvatar"]);
                byte[] customizeAvatar = null;
                if (!Convert.IsDBNull(table.Rows[0]["CustomizeAvatar"])) customizeAvatar = (byte[])table.Rows[0]["CustomizeAvatar"];
                string systemAvatar = Convert.ToString(table.Rows[0]["SystemAvatar"]);
                bool ifForumAdmin = Convert.ToBoolean(table.Rows[0]["IfForumAdmin"]);
                int score = Convert.ToInt32(table.Rows[0]["ForumScore"]);
                int reputation = Convert.ToInt32(table.Rows[0]["ForumReputation"]);
                //it's same to fogetpasswordguidtag
                string emailVerificationGUIDTag = Convert.ToString(table.Rows[0]["ForgetPasswordGUIDTag"]);
                bool ifVerified = Convert.ToBoolean(table.Rows[0]["IfVerified"]);
                Int16 moderateStatus = Convert.ToInt16(table.Rows[0]["ModerateStatus"]);
                Int16 emailVerificationStatus = Convert.ToInt16(table.Rows[0]["EmailVerificationStatus"]);
                #endregion

                userOrOperator = new UserWithPermissionCheck(conn, transaction, userOrOperatorId, operatingUserOrOperator, ifActive, ifDeleted, email, name, password,
                    forgetPasswordGUIDTag, forgetPasswordTagTime, lastLoginTime, lastLoginIP, joinedTime, joinedIP, numberOfPosts, ifShowEmail, firstName, lastName,
                    ifShowUserName, age, ifShowAge, gender, ifShowGender, occupation, ifShowOccupation, company, ifShowCompany, phoneNumber, ifShowPhoneNumber,
                    faxNumber, ifShowFaxNumber, interests, ifShowInterests, homePage, ifShowHomePage, signature, ifCustomizeAvatar, customizeAvatar, systemAvatar,
                    ifForumAdmin, score, reputation, emailVerificationGUIDTag, ifVerified, moderateStatus, emailVerificationStatus, ip);
            }
            else if (((EnumUserType)Convert.ToInt16(table.Rows[0]["UserType"])) == EnumUserType.Operator)
            {
                #region Init User Infor
                int id = userOrOperatorId;
                bool ifActive = Convert.ToBoolean(table.Rows[0]["IfActive"]);
                bool ifDeleted = Convert.ToBoolean(table.Rows[0]["IfDeleted"]);
                string email = Convert.ToString(table.Rows[0]["Email"]);
                string name = Convert.ToString(table.Rows[0]["Name"]);
                string password = Convert.ToString(table.Rows[0]["Password"]);
                string forgetPasswordGUIDTag = Convert.ToString(table.Rows[0]["ForgetPasswordGUIDTag"]);
                DateTime forgetPasswordTagTime = Convert.ToDateTime(table.Rows[0]["ForgetPasswordTagTime"]);
                DateTime lastLoginTime = Convert.ToDateTime(table.Rows[0]["LastLoginTime"]);
                Int64 lastLoginIP = Convert.ToInt64(table.Rows[0]["LastLoginIP"]);
                DateTime joinedTime = Convert.ToDateTime(table.Rows[0]["JoinedTime"]);
                Int64 joinedIP = Convert.ToInt64(table.Rows[0]["JoinedIP"]);
                int numberOfPosts = table.Rows[0]["Posts"] is DBNull ? 0 : Convert.ToInt32(table.Rows[0]["Posts"]);
                bool ifShowEmail = Convert.ToBoolean(table.Rows[0]["IfShowEmail"]);
                string firstName = Convert.ToString(table.Rows[0]["FirstName"]);
                string lastName = Convert.ToString(table.Rows[0]["LastName"]);
                bool ifShowUserName = Convert.ToBoolean(table.Rows[0]["IfShowUserName"]);
                Int16 age = Convert.ToInt16(table.Rows[0]["Age"]);
                bool ifShowAge = Convert.ToBoolean(table.Rows[0]["IfShowAge"]);
                Int16 gender = Convert.ToInt16(table.Rows[0]["Gender"]);
                bool ifShowGender = Convert.ToBoolean(table.Rows[0]["IfShowGender"]);
                string occupation = Convert.ToString(table.Rows[0]["Occupation"]);
                bool ifShowOccupation = Convert.ToBoolean(table.Rows[0]["IfShowOccupation"]);
                string company = Convert.ToString(table.Rows[0]["Company"]);
                bool ifShowCompany = Convert.ToBoolean(table.Rows[0]["IfShowCompany"]);
                string phoneNumber = Convert.ToString(table.Rows[0]["PhoneNumber"]);
                bool ifShowPhoneNumber = Convert.ToBoolean(table.Rows[0]["IfShowPhoneNumber"]);
                string faxNumber = Convert.ToString(table.Rows[0]["FaxNumber"]);
                bool ifShowFaxNumber = Convert.ToBoolean(table.Rows[0]["IfShowFaxNumber"]);
                string interests = Convert.ToString(table.Rows[0]["Interests"]);
                bool ifShowInterests = Convert.ToBoolean(table.Rows[0]["IfShowInterests"]);
                string homePage = Convert.ToString(table.Rows[0]["HomePage"]);
                bool ifShowHomePage = Convert.ToBoolean(table.Rows[0]["IfShowHomePage"]);
                string signature = Convert.ToString(table.Rows[0]["Signature"]);
                bool ifCustomizeAvatar = Convert.ToBoolean(table.Rows[0]["IfCustomizeAvatar"]);
                byte[] customizeAvatar = null;
                if (!Convert.IsDBNull(table.Rows[0]["CustomizeAvatar"])) customizeAvatar = (byte[])table.Rows[0]["CustomizeAvatar"];
                string systemAvatar = Convert.ToString(table.Rows[0]["SystemAvatar"]);
                bool ifForumAdmin = Convert.ToBoolean(table.Rows[0]["IfForumAdmin"]);
                int score = Convert.ToInt32(table.Rows[0]["ForumScore"]);
                int reputation = Convert.ToInt32(table.Rows[0]["ForumReputation"]);
                //it's same to fogetpasswordguidtag
                bool ifAdmin = Convert.ToBoolean(table.Rows[0]["IfAdmin"]);
                string description = Convert.ToString(table.Rows[0]["description"]);
                #endregion

                userOrOperator = new OperatorWithPermissionCheck(conn, transaction, userOrOperatorId, operatingUserOrOperator, ifActive, ifDeleted, email,
                    name, password, forgetPasswordGUIDTag, forgetPasswordTagTime, lastLoginTime, lastLoginIP, joinedTime, joinedIP, numberOfPosts,
                    ifShowEmail, firstName, lastName, ifShowUserName, age, ifShowAge, gender, ifShowGender, occupation, ifShowOccupation, company,
                    ifShowCompany, phoneNumber, ifShowPhoneNumber, faxNumber, ifShowFaxNumber, interests, ifShowInterests, homePage, ifShowHomePage,
                    signature, ifCustomizeAvatar, customizeAvatar, systemAvatar, ifForumAdmin, score, reputation, ifAdmin, description, ip);
            }
            return userOrOperator;
        }
        //credential check
        public static UserOrOperator CreateUserOrOperator(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, string email)
        {
            UserOrOperator userOrOperator = null;
            //credential check by email id bases
            DataTable table = UserAccess.GetNotDeletedUserOrOperatorByEmail(conn, transaction, email);
            if (table.Rows.Count <= 0)
            {
                ExceptionHelper.ThrowForumUserOrOperatorNotExistWithEmailException(email);
            }
            string ipAddr = HttpContext.Current.Request.ServerVariables["Remote_Addr"].ToString();
            Int64 ip = IpHelper.DottedIP2LongIP(ipAddr);
            if (((EnumUserType)Convert.ToInt16(table.Rows[0]["UserType"])) == EnumUserType.User)
            {
                #region Init User Infor
                int id = Convert.ToInt32(table.Rows[0]["Id"]);
                bool ifActive = Convert.ToBoolean(table.Rows[0]["IfActive"]);
                bool ifDeleted = Convert.ToBoolean(table.Rows[0]["IfDeleted"]);
                string name = Convert.ToString(table.Rows[0]["Name"]);
                string password = Convert.ToString(table.Rows[0]["Password"]);
                string forgetPasswordGUIDTag = Convert.ToString(table.Rows[0]["ForgetPasswordGUIDTag"]);
                DateTime forgetPasswordTagTime = Convert.ToDateTime(table.Rows[0]["ForgetPasswordTagTime"]);
                DateTime lastLoginTime = Convert.ToDateTime(table.Rows[0]["LastLoginTime"]);
                Int64 lastLoginIP = Convert.ToInt64(table.Rows[0]["LastLoginIP"]);
                DateTime joinedTime = Convert.ToDateTime(table.Rows[0]["JoinedTime"]);
                Int64 joinedIP = Convert.ToInt64(table.Rows[0]["JoinedIP"]);
                int numberOfPosts = table.Rows[0]["Posts"] is DBNull ? 0 : Convert.ToInt32(table.Rows[0]["Posts"]);
                bool ifShowEmail = Convert.ToBoolean(table.Rows[0]["IfShowEmail"]);
                string firstName = Convert.ToString(table.Rows[0]["FirstName"]);
                string lastName = Convert.ToString(table.Rows[0]["LastName"]);
                bool ifShowUserName = Convert.ToBoolean(table.Rows[0]["IfShowUserName"]);
                Int16 age = Convert.ToInt16(table.Rows[0]["Age"]);
                bool ifShowAge = Convert.ToBoolean(table.Rows[0]["IfShowAge"]);
                Int16 gender = Convert.ToInt16(table.Rows[0]["Gender"]);
                bool ifShowGender = Convert.ToBoolean(table.Rows[0]["IfShowGender"]);
                string occupation = Convert.ToString(table.Rows[0]["Occupation"]);
                bool ifShowOccupation = Convert.ToBoolean(table.Rows[0]["IfShowOccupation"]);
                string company = Convert.ToString(table.Rows[0]["Company"]);
                bool ifShowCompany = Convert.ToBoolean(table.Rows[0]["IfShowCompany"]);
                string phoneNumber = Convert.ToString(table.Rows[0]["PhoneNumber"]);
                bool ifShowPhoneNumber = Convert.ToBoolean(table.Rows[0]["IfShowPhoneNumber"]);
                string faxNumber = Convert.ToString(table.Rows[0]["FaxNumber"]);
                bool ifShowFaxNumber = Convert.ToBoolean(table.Rows[0]["IfShowFaxNumber"]);
                string interests = Convert.ToString(table.Rows[0]["Interests"]);
                bool ifShowInterests = Convert.ToBoolean(table.Rows[0]["IfShowInterests"]);
                string homePage = Convert.ToString(table.Rows[0]["HomePage"]);
                bool ifShowHomePage = Convert.ToBoolean(table.Rows[0]["IfShowHomePage"]);
                string signature = Convert.ToString(table.Rows[0]["Signature"]);
                bool ifCustomizeAvatar = Convert.ToBoolean(table.Rows[0]["IfCustomizeAvatar"]);
                byte[] customizeAvatar = null;
                if (!Convert.IsDBNull(table.Rows[0]["CustomizeAvatar"])) customizeAvatar = (byte[])table.Rows[0]["CustomizeAvatar"];
                string systemAvatar = Convert.ToString(table.Rows[0]["SystemAvatar"]);
                bool ifForumAdmin = Convert.ToBoolean(table.Rows[0]["IfForumAdmin"]);
                int score = Convert.ToInt32(table.Rows[0]["ForumScore"]);
                int reputation = Convert.ToInt32(table.Rows[0]["ForumReputation"]);
                //it's same to fogetpasswordguidtag
                string emailVerificationGUIDTag = Convert.ToString(table.Rows[0]["ForgetPasswordGUIDTag"]);
                bool ifVerified = Convert.ToBoolean(table.Rows[0]["IfVerified"]);
                Int16 moderateStatus = Convert.ToInt16(table.Rows[0]["ModerateStatus"]);
                Int16 emailVerificationStatus = Convert.ToInt16(table.Rows[0]["EmailVerificationStatus"]);
                #endregion

                userOrOperator = new UserWithPermissionCheck(conn, transaction, id, operatingUserOrOperator, ifActive, ifDeleted, email, name, password,
                    forgetPasswordGUIDTag, forgetPasswordTagTime, lastLoginTime, lastLoginIP, joinedTime, joinedIP, numberOfPosts, ifShowEmail, firstName, lastName,
                    ifShowUserName, age, ifShowAge, gender, ifShowGender, occupation, ifShowOccupation, company, ifShowCompany, phoneNumber, ifShowPhoneNumber,
                    faxNumber, ifShowFaxNumber, interests, ifShowInterests, homePage, ifShowHomePage, signature, ifCustomizeAvatar, customizeAvatar, systemAvatar,
                    ifForumAdmin, score, reputation, emailVerificationGUIDTag, ifVerified, moderateStatus, emailVerificationStatus, ip);
            }
            else if (((EnumUserType)Convert.ToInt16(table.Rows[0]["UserType"])) == EnumUserType.Operator)
            {
                #region Init User Infor
                int id = Convert.ToInt32(table.Rows[0]["Id"]);
                bool ifActive = Convert.ToBoolean(table.Rows[0]["IfActive"]);
                bool ifDeleted = Convert.ToBoolean(table.Rows[0]["IfDeleted"]);
                string name = Convert.ToString(table.Rows[0]["Name"]);
                string password = Convert.ToString(table.Rows[0]["Password"]);
                string forgetPasswordGUIDTag = Convert.ToString(table.Rows[0]["ForgetPasswordGUIDTag"]);
                DateTime forgetPasswordTagTime = Convert.ToDateTime(table.Rows[0]["ForgetPasswordTagTime"]);
                DateTime lastLoginTime = Convert.ToDateTime(table.Rows[0]["LastLoginTime"]);
                Int64 lastLoginIP = Convert.ToInt64(table.Rows[0]["LastLoginIP"]);
                DateTime joinedTime = Convert.ToDateTime(table.Rows[0]["JoinedTime"]);
                Int64 joinedIP = Convert.ToInt64(table.Rows[0]["JoinedIP"]);
                int numberOfPosts = table.Rows[0]["Posts"] is DBNull ? 0 : Convert.ToInt32(table.Rows[0]["Posts"]);
                bool ifShowEmail = Convert.ToBoolean(table.Rows[0]["IfShowEmail"]);
                string firstName = Convert.ToString(table.Rows[0]["FirstName"]);
                string lastName = Convert.ToString(table.Rows[0]["LastName"]);
                bool ifShowUserName = Convert.ToBoolean(table.Rows[0]["IfShowUserName"]);
                Int16 age = Convert.ToInt16(table.Rows[0]["Age"]);
                bool ifShowAge = Convert.ToBoolean(table.Rows[0]["IfShowAge"]);
                Int16 gender = Convert.ToInt16(table.Rows[0]["Gender"]);
                bool ifShowGender = Convert.ToBoolean(table.Rows[0]["IfShowGender"]);
                string occupation = Convert.ToString(table.Rows[0]["Occupation"]);
                bool ifShowOccupation = Convert.ToBoolean(table.Rows[0]["IfShowOccupation"]);
                string company = Convert.ToString(table.Rows[0]["Company"]);
                bool ifShowCompany = Convert.ToBoolean(table.Rows[0]["IfShowCompany"]);
                string phoneNumber = Convert.ToString(table.Rows[0]["PhoneNumber"]);
                bool ifShowPhoneNumber = Convert.ToBoolean(table.Rows[0]["IfShowPhoneNumber"]);
                string faxNumber = Convert.ToString(table.Rows[0]["FaxNumber"]);
                bool ifShowFaxNumber = Convert.ToBoolean(table.Rows[0]["IfShowFaxNumber"]);
                string interests = Convert.ToString(table.Rows[0]["Interests"]);
                bool ifShowInterests = Convert.ToBoolean(table.Rows[0]["IfShowInterests"]);
                string homePage = Convert.ToString(table.Rows[0]["HomePage"]);
                bool ifShowHomePage = Convert.ToBoolean(table.Rows[0]["IfShowHomePage"]);
                string signature = Convert.ToString(table.Rows[0]["Signature"]);
                bool ifCustomizeAvatar = Convert.ToBoolean(table.Rows[0]["IfCustomizeAvatar"]);
                byte[] customizeAvatar = null;
                if (!Convert.IsDBNull(table.Rows[0]["CustomizeAvatar"])) customizeAvatar = (byte[])table.Rows[0]["CustomizeAvatar"];
                string systemAvatar = Convert.ToString(table.Rows[0]["SystemAvatar"]);
                bool ifForumAdmin = Convert.ToBoolean(table.Rows[0]["IfForumAdmin"]);
                int score = Convert.ToInt32(table.Rows[0]["ForumScore"]);
                int reputation = Convert.ToInt32(table.Rows[0]["ForumReputation"]);
                //it's same to fogetpasswordguidtag
                bool ifAdmin = Convert.ToBoolean(table.Rows[0]["IfAdmin"]);
                string description = Convert.ToString(table.Rows[0]["description"]);
                #endregion

                userOrOperator = new OperatorWithPermissionCheck(conn, transaction, id, operatingUserOrOperator, ifActive, ifDeleted, email,
                    name, password, forgetPasswordGUIDTag, forgetPasswordTagTime, lastLoginTime, lastLoginIP, joinedTime, joinedIP, numberOfPosts,
                    ifShowEmail, firstName, lastName, ifShowUserName, age, ifShowAge, gender, ifShowGender, occupation, ifShowOccupation, company,
                    ifShowCompany, phoneNumber, ifShowPhoneNumber, faxNumber, ifShowFaxNumber, interests, ifShowInterests, homePage, ifShowHomePage,
                    signature, ifCustomizeAvatar, customizeAvatar, systemAvatar, ifForumAdmin, score, reputation, ifAdmin, description, ip);
            }

            return userOrOperator;
        }

        public static UserOrOperator CreateUserOrOperatorByName(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, string name)
        {
            UserOrOperator userOrOperator = null;
            DataTable table = UserAccess.GetNotDeletedUserOrOperatorByName(conn, transaction, name);
            if (table.Rows.Count <= 0)
            {
                ExceptionHelper.ThrowForumUserOrOperatorNotExistWithNameException(name);
            }
            string ipAddr = HttpContext.Current.Request.ServerVariables["Remote_Addr"].ToString();
            Int64 ip = IpHelper.DottedIP2LongIP(ipAddr);
            if (((EnumUserType)Convert.ToInt16(table.Rows[0]["UserType"])) == EnumUserType.User)
            {
                #region Init User Infor
                int id = Convert.ToInt32(table.Rows[0]["Id"]);
                bool ifActive = Convert.ToBoolean(table.Rows[0]["IfActive"]);
                bool ifDeleted = Convert.ToBoolean(table.Rows[0]["IfDeleted"]);
                string email = Convert.ToString(table.Rows[0]["Email"]);
                //string name = Convert.ToString(table.Rows[0]["Name"]);
                string password = Convert.ToString(table.Rows[0]["Password"]);
                string forgetPasswordGUIDTag = Convert.ToString(table.Rows[0]["ForgetPasswordGUIDTag"]);
                DateTime forgetPasswordTagTime = Convert.ToDateTime(table.Rows[0]["ForgetPasswordTagTime"]);
                DateTime lastLoginTime = Convert.ToDateTime(table.Rows[0]["LastLoginTime"]);
                Int64 lastLoginIP = Convert.ToInt64(table.Rows[0]["LastLoginIP"]);
                DateTime joinedTime = Convert.ToDateTime(table.Rows[0]["JoinedTime"]);
                Int64 joinedIP = Convert.ToInt64(table.Rows[0]["JoinedIP"]);
                int numberOfPosts = table.Rows[0]["Posts"] is DBNull ? 0 : Convert.ToInt32(table.Rows[0]["Posts"]);
                bool ifShowEmail = Convert.ToBoolean(table.Rows[0]["IfShowEmail"]);
                string firstName = Convert.ToString(table.Rows[0]["FirstName"]);
                string lastName = Convert.ToString(table.Rows[0]["LastName"]);
                bool ifShowUserName = Convert.ToBoolean(table.Rows[0]["IfShowUserName"]);
                Int16 age = Convert.ToInt16(table.Rows[0]["Age"]);
                bool ifShowAge = Convert.ToBoolean(table.Rows[0]["IfShowAge"]);
                Int16 gender = Convert.ToInt16(table.Rows[0]["Gender"]);
                bool ifShowGender = Convert.ToBoolean(table.Rows[0]["IfShowGender"]);
                string occupation = Convert.ToString(table.Rows[0]["Occupation"]);
                bool ifShowOccupation = Convert.ToBoolean(table.Rows[0]["IfShowOccupation"]);
                string company = Convert.ToString(table.Rows[0]["Company"]);
                bool ifShowCompany = Convert.ToBoolean(table.Rows[0]["IfShowCompany"]);
                string phoneNumber = Convert.ToString(table.Rows[0]["PhoneNumber"]);
                bool ifShowPhoneNumber = Convert.ToBoolean(table.Rows[0]["IfShowPhoneNumber"]);
                string faxNumber = Convert.ToString(table.Rows[0]["FaxNumber"]);
                bool ifShowFaxNumber = Convert.ToBoolean(table.Rows[0]["IfShowFaxNumber"]);
                string interests = Convert.ToString(table.Rows[0]["Interests"]);
                bool ifShowInterests = Convert.ToBoolean(table.Rows[0]["IfShowInterests"]);
                string homePage = Convert.ToString(table.Rows[0]["HomePage"]);
                bool ifShowHomePage = Convert.ToBoolean(table.Rows[0]["IfShowHomePage"]);
                string signature = Convert.ToString(table.Rows[0]["Signature"]);
                bool ifCustomizeAvatar = Convert.ToBoolean(table.Rows[0]["IfCustomizeAvatar"]);
                byte[] customizeAvatar = null;
                if (!Convert.IsDBNull(table.Rows[0]["CustomizeAvatar"])) customizeAvatar = (byte[])table.Rows[0]["CustomizeAvatar"];
                string systemAvatar = Convert.ToString(table.Rows[0]["SystemAvatar"]);
                bool ifForumAdmin = Convert.ToBoolean(table.Rows[0]["IfForumAdmin"]);
                int score = Convert.ToInt32(table.Rows[0]["ForumScore"]);
                int reputation = Convert.ToInt32(table.Rows[0]["ForumReputation"]);
                //it's same to fogetpasswordguidtag
                string emailVerificationGUIDTag = Convert.ToString(table.Rows[0]["ForgetPasswordGUIDTag"]);
                bool ifVerified = Convert.ToBoolean(table.Rows[0]["IfVerified"]);
                Int16 moderateStatus = Convert.ToInt16(table.Rows[0]["ModerateStatus"]);
                Int16 emailVerificationStatus = Convert.ToInt16(table.Rows[0]["EmailVerificationStatus"]);
                #endregion

                userOrOperator = new UserWithPermissionCheck(conn, transaction, id, operatingUserOrOperator, ifActive, ifDeleted, email, name, password,
                    forgetPasswordGUIDTag, forgetPasswordTagTime, lastLoginTime, lastLoginIP, joinedTime, joinedIP, numberOfPosts, ifShowEmail, firstName, lastName,
                    ifShowUserName, age, ifShowAge, gender, ifShowGender, occupation, ifShowOccupation, company, ifShowCompany, phoneNumber, ifShowPhoneNumber,
                    faxNumber, ifShowFaxNumber, interests, ifShowInterests, homePage, ifShowHomePage, signature, ifCustomizeAvatar, customizeAvatar, systemAvatar,
                    ifForumAdmin, score, reputation, emailVerificationGUIDTag, ifVerified, moderateStatus, emailVerificationStatus, ip);
            }
            else if (((EnumUserType)Convert.ToInt16(table.Rows[0]["UserType"])) == EnumUserType.Operator)
            {
                #region Init User Infor
                int id = Convert.ToInt32(table.Rows[0]["Id"]);
                bool ifActive = Convert.ToBoolean(table.Rows[0]["IfActive"]);
                bool ifDeleted = Convert.ToBoolean(table.Rows[0]["IfDeleted"]);
                string email = Convert.ToString(table.Rows[0]["Email"]);
                //string name = Convert.ToString(table.Rows[0]["Name"]);
                string password = Convert.ToString(table.Rows[0]["Password"]);
                string forgetPasswordGUIDTag = Convert.ToString(table.Rows[0]["ForgetPasswordGUIDTag"]);
                DateTime forgetPasswordTagTime = Convert.ToDateTime(table.Rows[0]["ForgetPasswordTagTime"]);
                DateTime lastLoginTime = Convert.ToDateTime(table.Rows[0]["LastLoginTime"]);
                Int64 lastLoginIP = Convert.ToInt64(table.Rows[0]["LastLoginIP"]);
                DateTime joinedTime = Convert.ToDateTime(table.Rows[0]["JoinedTime"]);
                Int64 joinedIP = Convert.ToInt64(table.Rows[0]["JoinedIP"]);
                int numberOfPosts = table.Rows[0]["Posts"] is DBNull ? 0 : Convert.ToInt32(table.Rows[0]["Posts"]);
                bool ifShowEmail = Convert.ToBoolean(table.Rows[0]["IfShowEmail"]);
                string firstName = Convert.ToString(table.Rows[0]["FirstName"]);
                string lastName = Convert.ToString(table.Rows[0]["LastName"]);
                bool ifShowUserName = Convert.ToBoolean(table.Rows[0]["IfShowUserName"]);
                Int16 age = Convert.ToInt16(table.Rows[0]["Age"]);
                bool ifShowAge = Convert.ToBoolean(table.Rows[0]["IfShowAge"]);
                Int16 gender = Convert.ToInt16(table.Rows[0]["Gender"]);
                bool ifShowGender = Convert.ToBoolean(table.Rows[0]["IfShowGender"]);
                string occupation = Convert.ToString(table.Rows[0]["Occupation"]);
                bool ifShowOccupation = Convert.ToBoolean(table.Rows[0]["IfShowOccupation"]);
                string company = Convert.ToString(table.Rows[0]["Company"]);
                bool ifShowCompany = Convert.ToBoolean(table.Rows[0]["IfShowCompany"]);
                string phoneNumber = Convert.ToString(table.Rows[0]["PhoneNumber"]);
                bool ifShowPhoneNumber = Convert.ToBoolean(table.Rows[0]["IfShowPhoneNumber"]);
                string faxNumber = Convert.ToString(table.Rows[0]["FaxNumber"]);
                bool ifShowFaxNumber = Convert.ToBoolean(table.Rows[0]["IfShowFaxNumber"]);
                string interests = Convert.ToString(table.Rows[0]["Interests"]);
                bool ifShowInterests = Convert.ToBoolean(table.Rows[0]["IfShowInterests"]);
                string homePage = Convert.ToString(table.Rows[0]["HomePage"]);
                bool ifShowHomePage = Convert.ToBoolean(table.Rows[0]["IfShowHomePage"]);
                string signature = Convert.ToString(table.Rows[0]["Signature"]);
                bool ifCustomizeAvatar = Convert.ToBoolean(table.Rows[0]["IfCustomizeAvatar"]);
                byte[] customizeAvatar = null;
                if (!Convert.IsDBNull(table.Rows[0]["CustomizeAvatar"])) customizeAvatar = (byte[])table.Rows[0]["CustomizeAvatar"];
                string systemAvatar = Convert.ToString(table.Rows[0]["SystemAvatar"]);
                bool ifForumAdmin = Convert.ToBoolean(table.Rows[0]["IfForumAdmin"]);
                int score = Convert.ToInt32(table.Rows[0]["ForumScore"]);
                int reputation = Convert.ToInt32(table.Rows[0]["ForumReputation"]);
                //it's same to fogetpasswordguidtag
                bool ifAdmin = Convert.ToBoolean(table.Rows[0]["IfAdmin"]);
                string description = Convert.ToString(table.Rows[0]["description"]);
                #endregion

                userOrOperator = new OperatorWithPermissionCheck(conn, transaction, id, operatingUserOrOperator, ifActive, ifDeleted, email,
                    name, password, forgetPasswordGUIDTag, forgetPasswordTagTime, lastLoginTime, lastLoginIP, joinedTime, joinedIP, numberOfPosts,
                    ifShowEmail, firstName, lastName, ifShowUserName, age, ifShowAge, gender, ifShowGender, occupation, ifShowOccupation, company,
                    ifShowCompany, phoneNumber, ifShowPhoneNumber, faxNumber, ifShowFaxNumber, interests, ifShowInterests, homePage, ifShowHomePage,
                    signature, ifCustomizeAvatar, customizeAvatar, systemAvatar, ifForumAdmin, score, reputation, ifAdmin, description, ip);
            }

            return userOrOperator;
        }
    }
}
