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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;
using System.Text.RegularExpressions;
using Com.Comm100.Framework.Enum;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class Users
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        public Users(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            _conn = conn;
            _transaction = transaction;
        }

        #region Protected Function CheckFieldFormat
        protected static bool CheckFieldFormat(string format, string field)
        {
            Regex regularExpressions = new Regex(format, RegexOptions.IgnoreCase);
            Match match = regularExpressions.Match(field, 0, field.Length);
            return match.Success;
        }
        #endregion

        #region Protected Function CheckFieldLength
        protected void CheckFieldLength(string email, string displayName, string firstName, string lastName,
            string company, string occupation, string phone, string fax, string interests, string homepage)
        {
            //format
            if (CheckFieldFormat(Com.Comm100.Framework.Common.ConstantsHelper.Email_Format, email) == false)
            {
                ExceptionHelper.ThrowUserEmailFormatWrongException();
            }
            //required
            if (email.Length == 0)
            {
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("email");
            }
            if (displayName.Length == 0)
            {
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("displayName");
            }
            //Field is only one

            if (UserAccess.GetCountOfNotDeletedUsersByDisplayName(this._conn, this._transaction, displayName) != 0)
            {
                ExceptionHelper.ThrowUserDisplayNameNotUniqueException();//user
            }

            if (UserAccess.GetCountOfNotDeletedUsersByEmail(this._conn, this._transaction, email) != 0)
            {
                ExceptionHelper.ThrowUserEmailNotUniqueException();//user
            }
            //Max length
            if (email.Length > ForumDBFieldLength.User_emailFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("email",
                    ForumDBFieldLength.User_emailFieldLength);
            }
            if (displayName.Length > ForumDBFieldLength.User_nameFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("displayName",
                    ForumDBFieldLength.User_nameFieldLength);
            }
            if (firstName.Length > ForumDBFieldLength.User_firstNameFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("firstName",
                    ForumDBFieldLength.User_firstNameFieldLength);
            }
            if (lastName.Length > ForumDBFieldLength.User_lastNameFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("lastName",
                    ForumDBFieldLength.User_lastNameFieldLength);
            }
            if (company.Length > ForumDBFieldLength.User_companyFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("company",
                    ForumDBFieldLength.User_companyFieldLength);
            }
            if (occupation.Length > ForumDBFieldLength.User_occupationFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("occupation",
                    ForumDBFieldLength.User_occupationFieldLength);
            }
            if (phone.Length > ForumDBFieldLength.User_phoneNumberFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("phoneNumber",
                    ForumDBFieldLength.User_phoneNumberFieldLength);
            }
            if (fax.Length > ForumDBFieldLength.User_faxNumberFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("faxNumber",
                    ForumDBFieldLength.User_faxNumberFieldLength);
            }
            if (interests.Length > ForumDBFieldLength.User_interestsFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("interests",
                    ForumDBFieldLength.User_interestsFieldLength);
            }
            if (homepage.Length > ForumDBFieldLength.User_homePageFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("homepage",
                    ForumDBFieldLength.User_homePageFieldLength);
            }
        }
        #endregion Protected Function CheckFieldLength

        public virtual UserWithPermissionCheck[] GetAllUserNotDelete(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = UserAccess.GetAllUserNotDelete(_conn, _transaction);

            UserWithPermissionCheck[] user = new UserWithPermissionCheck[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                user[i] = CreateUserObject(table.Rows[i], operatingUserOrOperator);
            }

            return user;
        }
        protected UserWithPermissionCheck[] GetNotDeletedUsersByQueryAndPaging(int pageIndex, int pageSize, string strOrder, string EmailOrdisplayNameKeyWord, UserOrOperator operatingUserOrOperator, out int count)
        {
            count = UserAccess.GetCountOfNotDeletedUsersByQuery(_conn, _transaction, EmailOrdisplayNameKeyWord);
            DataTable table = new DataTable();
            table = UserAccess.GetNotDeletedUsersByQueryAndPaging(_conn, _transaction, pageIndex, pageSize, strOrder, EmailOrdisplayNameKeyWord);
            UserWithPermissionCheck[] user = new UserWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                user[i] = CreateUserObject(table.Rows[i], operatingUserOrOperator);
            }
            return user;
        }

        #region Not Moderated
        public virtual UserWithPermissionCheck[] GetNotModeratedUsersByPaging(int pageIndex, int pageSize, string strOrder, string displayNameKeyWord, UserOrOperator operatingUserOrOperator)
        {
            DataTable table = new DataTable();
            table = UserAccess.GetNotModeratedUsersByPaging(_conn, _transaction, pageIndex, pageSize, strOrder, displayNameKeyWord);
            UserWithPermissionCheck[] userNotModerated = new UserWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                userNotModerated[i] = CreateUserObject(table.Rows[i], operatingUserOrOperator);
            }
            return userNotModerated;
        }

        public virtual int GetCountOfNotModeratedUsersBySearch(string displayNameKeyWord)
        {
            return UserAccess.GetCountOfNotModeratedUsersBySearch(_conn, _transaction, displayNameKeyWord);
        }
        #endregion Not Moderated

        #region Email Not Verify
        public virtual UserWithPermissionCheck[] GetUsersNotEmailVerfyByQueryAndPaging(int pageIndex, int pageSize, string orderField, string orderDirection, string emailOrDispalyNameKeyword, UserOrOperator operatingUserOrOperator, out int count)
        {
            DataTable table = new DataTable();
            string orderCondition = orderField + " " + orderDirection;
            table = UserAccess.GetUsersWhichNeedEmailVerifyByQueryAndPaging(_conn, _transaction, emailOrDispalyNameKeyword, orderCondition, pageIndex, pageSize);
            UserWithPermissionCheck[] usersNotEmailVerfy = new UserWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                usersNotEmailVerfy[i] = CreateUserObject(table.Rows[i], operatingUserOrOperator);
            }
            count = UserAccess.GetCountOfUsersWhichNeedEmailVerify(_conn, _transaction, emailOrDispalyNameKeyword);
            return usersNotEmailVerfy;
        }
        #endregion Email Not Verify

        public UserWithPermissionCheck[] GetNotDeletedUserByEmail(string email, UserOrOperator operatingUserOrOperator)
        {
            DataTable table = UserAccess.GetNotDeletedUserByEmail(_conn, _transaction, email);
            UserWithPermissionCheck[] user = new UserWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                user[i] = CreateUserObject(table.Rows[i], operatingUserOrOperator);
            }
            return user;
        }

        private UserWithPermissionCheck CreateUserObject(DataRow dr, UserOrOperator operatingUserOrOperator)
        {
            #region Init User Infor
            int id = Convert.ToInt32(dr["Id"]);
            bool ifActive = Convert.ToBoolean(dr["IfActive"]);
            bool ifDeleted = Convert.ToBoolean(dr["IfDeleted"]);
            string email = Convert.ToString(dr["Email"]);
            string name = Convert.ToString(dr["Name"]);
            string password = Convert.ToString(dr["Password"]);
            string forgetPasswordGUIDTag = Convert.ToString(dr["ForgetPasswordGUIDTag"]);
            DateTime forgetPasswordTagTime = Convert.ToDateTime(dr["ForgetPasswordTagTime"]);
            DateTime lastLoginTime = Convert.ToDateTime(dr["LastLoginTime"]);
            Int64 lastLoginIP = Convert.ToInt64(dr["LastLoginIP"]);
            DateTime joinedTime = Convert.ToDateTime(dr["JoinedTime"]);
            Int64 joinedIP = Convert.ToInt64(dr["JoinedIP"]);
            int numberOfPosts = dr["Posts"] is DBNull ? 0 : Convert.ToInt32(dr["Posts"]);
            bool ifShowEmail = Convert.ToBoolean(dr["IfShowEmail"]);
            string firstName = Convert.ToString(dr["FirstName"]);
            string lastName = Convert.ToString(dr["LastName"]);
            bool ifShowUserName = Convert.ToBoolean(dr["IfShowUserName"]);
            Int16 age = Convert.ToInt16(dr["Age"]);
            bool ifShowAge = Convert.ToBoolean(dr["IfShowAge"]);
            Int16 gender = Convert.ToInt16(dr["Gender"]);
            bool ifShowGender = Convert.ToBoolean(dr["IfShowGender"]);
            string occupation = Convert.ToString(dr["Occupation"]);
            bool ifShowOccupation = Convert.ToBoolean(dr["IfShowOccupation"]);
            string company = Convert.ToString(dr["Company"]);
            bool ifShowCompany = Convert.ToBoolean(dr["IfShowCompany"]);
            string phoneNumber = Convert.ToString(dr["PhoneNumber"]);
            bool ifShowPhoneNumber = Convert.ToBoolean(dr["IfShowPhoneNumber"]);
            string faxNumber = Convert.ToString(dr["FaxNumber"]);
            bool ifShowFaxNumber = Convert.ToBoolean(dr["IfShowFaxNumber"]);
            string interests = Convert.ToString(dr["Interests"]);
            bool ifShowInterests = Convert.ToBoolean(dr["IfShowInterests"]);
            string homePage = Convert.ToString(dr["HomePage"]);
            bool ifShowHomePage = Convert.ToBoolean(dr["IfShowHomePage"]);
            string signature = Convert.ToString(dr["Signature"]);
            bool ifCustomizeAvatar = Convert.ToBoolean(dr["IfCustomizeAvatar"]);
            byte[] customizeAvatar = null;
            if (!Convert.IsDBNull(dr["CustomizeAvatar"])) customizeAvatar = (byte[])dr["CustomizeAvatar"];
            string systemAvatar = Convert.ToString(dr["SystemAvatar"]);
            bool ifForumAdmin = Convert.ToBoolean(dr["IfForumAdmin"]);
            int score = Convert.ToInt32(dr["ForumScore"]);
            int reputation = Convert.ToInt32(dr["ForumReputation"]);
            //it's same to fogetpasswordguidtag
            string emailVerificationGUIDTag = Convert.ToString(dr["ForgetPasswordGUIDTag"]);
            bool ifVerified = Convert.ToBoolean(dr["IfVerified"]);
            Int16 moderateStatus = Convert.ToInt16(dr["ModerateStatus"]);
            Int16 emailVerificationStatus = Convert.ToInt16(dr["EmailVerificationStatus"]);
            #endregion
            Int64 ip = 0;

            UserWithPermissionCheck user = new UserWithPermissionCheck(_conn, _transaction, id, operatingUserOrOperator, ifActive, ifDeleted, email, name, password,
                forgetPasswordGUIDTag, forgetPasswordTagTime, lastLoginTime, lastLoginIP, joinedTime, joinedIP, numberOfPosts, ifShowEmail, firstName, lastName,
                ifShowUserName, age, ifShowAge, gender, ifShowGender, occupation, ifShowOccupation, company, ifShowCompany, phoneNumber, ifShowPhoneNumber,
                faxNumber, ifShowFaxNumber, interests, ifShowInterests, homePage, ifShowHomePage, signature, ifCustomizeAvatar, customizeAvatar, systemAvatar,
                ifForumAdmin, score, reputation, emailVerificationGUIDTag, ifVerified, moderateStatus, emailVerificationStatus, ip);

            return user;
        }

        public int Add(string email, string displayName, string password, long ip, bool ifModearteUser, bool ifVerifyEmail)
        {
            return User.Add(_conn, _transaction, email, displayName, password, ip, _conn.SiteId, ifModearteUser, ifVerifyEmail);
        }


        public virtual int Add(string email, string displayName, string password, string firstName, string lastName, int age,
            EnumGender gender, string company, string occupation, string phone, string fax, string interests,
            string homepage, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender,
            bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests,
            bool ifShowHomePage)
        {
            CheckFieldLength(email, displayName, firstName, lastName, company, occupation, phone, fax, interests, homepage);
            string strDefalutAvatarFilePath = ConstantsHelper.User_Avatar_Default;
            return UserAccess.AddUser(_conn, _transaction, email, displayName, password, firstName, lastName, age, gender, company, occupation,
                phone, fax, interests, homepage, ifShowEmail, ifShowUserName, ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany, ifShowPhone, ifShowFax,
                ifShowInterests, ifShowHomePage, strDefalutAvatarFilePath);
        }


        #region Protected Function Add

        protected int Add(string email, string displayName, string password, string firstName, string lastName, int age,
            EnumGender gender, string company, string occupation, string phone, string fax, string interests,
            string homepage, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender,
            bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests,
            bool ifShowHomePage, List<int> userGroupIds, UserOrOperator operatingUserOrOperator)
        {
            int userId = Add(email, displayName, password, firstName, lastName, age, gender, company, occupation, phone, fax, interests, homepage, ifShowEmail, ifShowUserName,
                ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany, ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage);
            if (userGroupIds.Count > 0)
            {
                CheckIfCanAddToUserGroup(operatingUserOrOperator);
                UserGroupWithPermissionCheck userGroup = null;
                MembersOfUserGroupWithPermissionCheck membersTmp = null;
                for (int i = 0; i < userGroupIds.Count; i++)
                {
                    userGroup = new UserGroupWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, userGroupIds[i]);
                    membersTmp = userGroup.GetMembers();
                    membersTmp.Add(userId);
                }
            }
            return userId;
        }


        private void CheckIfCanAddToUserGroup(UserOrOperator operatingUserOrOperator)
        {
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, operatingUserOrOperator);
            if (!forumFeature.IfEnableGroupPermission)
                ExceptionHelper.ThrowForumSettingsCloseGroupPermissionFunction();
        }

        #endregion Protected Function Add

        private void CheckDisplayName(string displayName, RegistrationSetting registerSetting)
        {
            if (displayName.Length > registerSetting.DisplayNameMaxLength || displayName.Length < registerSetting.DisplayNameMinLength)
            {
                ExceptionHelper.ThrowUserDisplayNameLength(registerSetting.DisplayNameMinLength, registerSetting.DisplayNameMaxLength);
            }
            /**/
            if (!string.IsNullOrEmpty(registerSetting.DisplayNameRegularExpression))
            {
                Regex reg = new Regex(registerSetting.DisplayNameRegularExpression);
                if (!reg.IsMatch(displayName))
                {
                    ExceptionHelper.ThrowUserDisplayNameFormatErrorAndShowInstruction(registerSetting.DisplayNameInstruction);
                }
            }
            /**/
            if (registerSetting.IllegalDisplayNames != null)
            {
                foreach (string str in registerSetting.IllegalDisplayNames)
                {
                    if (str == displayName)
                    {
                        ExceptionHelper.ThrowUserIllegalDispalyName(displayName);
                    }
                }
            }
        }

        public int Register(int siteId, string email, string displayName,
            string password, long ip, bool ifModearteUser, bool ifVerifyEmail)
        {
            int userId = 0;

            UserWithPermissionCheck user = null;
            RegistrationSetting registerSetting = new RegistrationSettingWithPermissionCheck(_conn, _transaction, null);
            CheckDisplayName(displayName, registerSetting);

            /*
              Need to check the email and display name through operators and users repectively
              Need Operators.cs and OperatorsWithPermissionCheck.cs
              Need GetCountOfNotDeleteOperatorsByEmail
            */
            if (this.GetCountOfNotDeletedUsersByEmail(email) != 0)
                ExceptionHelper.ThrowRegisterEmailRepeatedException();
            if (this.GetCountOfNotDeletedUsersByDisplayName(displayName) != 0)
                ExceptionHelper.ThrowRegisterNameRepeatedException();

            userId = this.Add(email, displayName, password, ip, ifModearteUser, ifVerifyEmail);
            user = new UserWithPermissionCheck(_conn, _transaction, userId, 0, null);

            if (ifVerifyEmail)
            {
                string EmailVerificationGuid = System.Guid.NewGuid().ToString();
                EmailVerificationGuid = EmailVerificationGuid.Replace("-", "");
                user.UpdateEmailVerificationGUIDTag(EmailVerificationGuid);
            }

            /*2.0 strategy */
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                _conn, _transaction, user, siteId);
            scoreStrategySetting.UseAfterRegistration(user);

            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                _conn, _transaction, user, _conn.SiteId);
            reputationStrategySetting.UseAfterRegistration(user);

            return userId;
        }

        public virtual int GetCountOfNotDeletedUsersByDisplayName(string displayName)
        {
            int count = UserAccess.GetCountOfNotDeletedUsersByDisplayName(this._conn, this._transaction, displayName);
            return count;
        }

        public virtual int GetCountOfNotDeletedUsersByEmail(string email)
        {
            return UserAccess.GetCountOfNotDeletedUsersByEmail(this._conn, this._transaction, email);
        }

        public virtual void Delete(int userId)
        {

        }

        protected UserWithPermissionCheck GetNotDeletedUserById(int userId, UserOrOperator operatingUserOrOperator)
        {
            UserWithPermissionCheck user = new UserWithPermissionCheck(_conn, _transaction, userId, 0, operatingUserOrOperator);
            if (user.IfDeleted) ExceptionHelper.ThrowUserIdNotExist(userId);
            return user;
        }
    }
}
