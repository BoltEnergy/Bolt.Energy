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
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;
using System.Drawing;
using System.IO;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Framework.Common;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class User : UserOrOperator, IGroupsOfUserOrOperator 
    {
        #region private fields
        private string _emailVerificationGUIDTag;
        private bool _ifVerified;
        private Int16 _moderateStatus;
        private Int16 _emailVerificationStatus;
        
        #endregion

        #region properties
        public string EmailVerificationGUIDTag
        {
            get { return this._emailVerificationGUIDTag; }
        }
        public bool IfVerified
        {
            get { return this._ifVerified; }
        }
        public EnumUserModerateStatus ModerateStatus
        {
            get { return (EnumUserModerateStatus)this._moderateStatus; }
        }
        public EnumUserEmailVerificationStatus EmailVerificationStatus
        {
            get { return (EnumUserEmailVerificationStatus)this._emailVerificationStatus; }
        }        
        #endregion

        public User(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id, Int64 ip)
            : base(conn, transaction)
        {
            DataTable dt = UserAccess.GetUserById(conn, transaction, id);
            if (dt.Rows.Count == 0)
            {
                ExceptionHelper.ThrowUserIdNotExist(id);
            }
            else
            {
                #region UserInformation Init
                this._id = id;
                this._ifActive = Convert.ToBoolean(dt.Rows[0]["IfActive"]);
                this._ifDeleted = Convert.ToBoolean(dt.Rows[0]["IfDeleted"]);
                this._email = Convert.ToString(dt.Rows[0]["Email"]);
                this._name = Convert.ToString(dt.Rows[0]["Name"]);
                this._password = Convert.ToString(dt.Rows[0]["Password"]).Trim();
                this._forgetPasswordGUIDTag = Convert.ToString(dt.Rows[0]["ForgetPasswordGUIDTag"]);
                this._forgetPasswordTagTime = Convert.ToDateTime(dt.Rows[0]["ForgetPasswordTagTime"]);
                this._lastLoginTime = Convert.ToDateTime(dt.Rows[0]["LastLoginTime"]);
                this._lastLoginIP = Convert.ToInt64(dt.Rows[0]["LastLoginIP"]);
                this._joinedTime = Convert.ToDateTime(dt.Rows[0]["JoinedTime"]);
                this._joinedIP = Convert.ToInt64(dt.Rows[0]["JoinedIP"]);
                this._numberOfPosts = dt.Rows[0]["Posts"] is DBNull ? 0 :Convert.ToInt32(dt.Rows[0]["Posts"]);
                this._ifShowEmail = Convert.ToBoolean(dt.Rows[0]["IfShowEmail"]);
                this._firstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                this._lastName = Convert.ToString(dt.Rows[0]["LastName"]);
                this._ifShowUserName = Convert.ToBoolean(dt.Rows[0]["IfShowUserName"]);
                this._age = Convert.ToInt16(dt.Rows[0]["Age"]);
                this._ifShowAge = Convert.ToBoolean(dt.Rows[0]["IfShowAge"]);
                this._gender = Convert.ToInt16(dt.Rows[0]["Gender"]);
                this._ifShowGender = Convert.ToBoolean(dt.Rows[0]["IfShowGender"]);
                this._occupation = Convert.ToString(dt.Rows[0]["Occupation"]);
                this._ifShowOccupation = Convert.ToBoolean(dt.Rows[0]["IfShowOccupation"]);
                this._company = Convert.ToString(dt.Rows[0]["Company"]);
                this._ifShowCompany = Convert.ToBoolean(dt.Rows[0]["IfShowCompany"]);
                this._phoneNumber = Convert.ToString(dt.Rows[0]["PhoneNumber"]);
                this._ifShowPhoneNumber = Convert.ToBoolean(dt.Rows[0]["IfShowPhoneNumber"]);
                this._faxNumber = Convert.ToString(dt.Rows[0]["FaxNumber"]);
                this._ifShowFaxNumber = Convert.ToBoolean(dt.Rows[0]["IfShowFaxNumber"]);
                this._interests = Convert.ToString(dt.Rows[0]["Interests"]);
                this._ifShowInterests = Convert.ToBoolean(dt.Rows[0]["IfShowInterests"]);
                this._homePage = Convert.ToString(dt.Rows[0]["HomePage"]);
                this._ifShowHomePage = Convert.ToBoolean(dt.Rows[0]["IfShowHomePage"]);
                this._signature = Convert.ToString(dt.Rows[0]["Signature"]);
                this._ifCustomizeAvatar = Convert.ToBoolean(dt.Rows[0]["IfCustomizeAvatar"]);
                if (!Convert.IsDBNull(dt.Rows[0]["CustomizeAvatar"])) this._customizeAvatar = (byte[])dt.Rows[0]["CustomizeAvatar"];
                this._systemAvatar = Convert.ToString(dt.Rows[0]["SystemAvatar"]);
                this._ifForumAdmin = Convert.ToBoolean(dt.Rows[0]["IfForumAdmin"]);
                this._score = Convert.ToInt32(dt.Rows[0]["ForumScore"]);
                this._reputation = Convert.ToInt32(dt.Rows[0]["ForumReputation"]);
                this._ip = ip;
                //it's same to fogetpasswordguidtag
                this._emailVerificationGUIDTag = Convert.ToString(dt.Rows[0]["ForgetPasswordGUIDTag"]);
                this._ifVerified = Convert.ToBoolean(dt.Rows[0]["IfVerified"]);
                this._moderateStatus = Convert.ToInt16(dt.Rows[0]["ModerateStatus"]);
                this._emailVerificationStatus = Convert.ToInt16(dt.Rows[0]["EmailVerificationStatus"]);
                #endregion
                //get Avatar's filepath
                GetAvatar();
            }
        }
        public User(SqlConnectionWithSiteId conn, SqlTransaction transaction, string email, Int64 ip)
            :base(conn, transaction)
		{
            this._email = email;
            DataTable dt = UserAccess.GetUserByEmail(conn, transaction, email);
            if (dt.Rows.Count == 0)
            {
                ExceptionHelper.ThrowUserNotExistWithEmailException(email);
            }
            else
            {
                #region UserInformation Init
                this._id = Convert.ToInt32(dt.Rows[0]["Id"]);
                this._ifActive = Convert.ToBoolean(dt.Rows[0]["IfActive"]);
                this._ifDeleted = Convert.ToBoolean(dt.Rows[0]["IfDeleted"]);
                this._email = Convert.ToString(dt.Rows[0]["Email"]);
                this._name = Convert.ToString(dt.Rows[0]["Name"]);
                this._password = Convert.ToString(dt.Rows[0]["Password"]).Trim();
                this._forgetPasswordGUIDTag = Convert.ToString(dt.Rows[0]["ForgetPasswordGUIDTag"]);
                this._forgetPasswordTagTime = Convert.ToDateTime(dt.Rows[0]["ForgetPasswordTagTime"]);
                this._lastLoginTime = Convert.ToDateTime(dt.Rows[0]["LastLoginTime"]);
                this._lastLoginIP = Convert.ToInt64(dt.Rows[0]["LastLoginIP"]);
                this._joinedTime = Convert.ToDateTime(dt.Rows[0]["JoinedTime"]);
                this._joinedIP = Convert.ToInt64(dt.Rows[0]["JoinedIP"]);
                this._numberOfPosts = dt.Rows[0]["Posts"] is DBNull ? 0 : Convert.ToInt32(dt.Rows[0]["Posts"]);
                this._ifShowEmail = Convert.ToBoolean(dt.Rows[0]["IfShowEmail"]);
                this._firstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                this._lastName = Convert.ToString(dt.Rows[0]["LastName"]);
                this._ifShowUserName = Convert.ToBoolean(dt.Rows[0]["IfShowUserName"]);
                this._age = Convert.ToInt16(dt.Rows[0]["Age"]);
                this._ifShowAge = Convert.ToBoolean(dt.Rows[0]["IfShowAge"]);
                this._gender = Convert.ToInt16(dt.Rows[0]["Gender"]);
                this._ifShowGender = Convert.ToBoolean(dt.Rows[0]["IfShowGender"]);
                this._occupation = Convert.ToString(dt.Rows[0]["Occupation"]);
                this._ifShowOccupation = Convert.ToBoolean(dt.Rows[0]["IfShowOccupation"]);
                this._company = Convert.ToString(dt.Rows[0]["Company"]);
                this._ifShowCompany = Convert.ToBoolean(dt.Rows[0]["IfShowCompany"]);
                this._phoneNumber = Convert.ToString(dt.Rows[0]["PhoneNumber"]);
                this._ifShowPhoneNumber = Convert.ToBoolean(dt.Rows[0]["IfShowPhoneNumber"]);
                this._faxNumber = Convert.ToString(dt.Rows[0]["FaxNumber"]);
                this._ifShowFaxNumber = Convert.ToBoolean(dt.Rows[0]["IfShowFaxNumber"]);
                this._interests = Convert.ToString(dt.Rows[0]["Interests"]);
                this._ifShowInterests = Convert.ToBoolean(dt.Rows[0]["IfShowInterests"]);
                this._homePage = Convert.ToString(dt.Rows[0]["HomePage"]);
                this._ifShowHomePage = Convert.ToBoolean(dt.Rows[0]["IfShowHomePage"]);
                this._signature = Convert.ToString(dt.Rows[0]["Signature"]);
                this._ifCustomizeAvatar = Convert.ToBoolean(dt.Rows[0]["IfCustomizeAvatar"]);
                if (!Convert.IsDBNull(dt.Rows[0]["CustomizeAvatar"])) this._customizeAvatar = (byte[])dt.Rows[0]["CustomizeAvatar"];
                this._systemAvatar = Convert.ToString(dt.Rows[0]["SystemAvatar"]);
                this._ifForumAdmin = Convert.ToBoolean(dt.Rows[0]["IfForumAdmin"]);
                this._score = Convert.ToInt32(dt.Rows[0]["ForumScore"]);
                this._reputation = Convert.ToInt32(dt.Rows[0]["ForumReputation"]);
                this._ip = ip;
                //it's same to fogetpasswordguidtag
                this._emailVerificationGUIDTag = Convert.ToString(dt.Rows[0]["ForgetPasswordGUIDTag"]);
                this._ifVerified = Convert.ToBoolean(dt.Rows[0]["IfVerified"]);
                this._moderateStatus = Convert.ToInt16(dt.Rows[0]["ModerateStatus"]);
                this._emailVerificationStatus = Convert.ToInt16(dt.Rows[0]["EmailVerificationStatus"]);
                #endregion
                //get Avatar's filepath
                GetAvatar();
            }
		}
        public User(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id, bool ifActive, bool ifDeleted, string email, string name, string password, 
                string forgetPasswordGUIDTag, DateTime forgetPasswordTagTime, DateTime lastLoginTime, Int64 lastLoginIP, DateTime joinedTime, Int64 joinedIP, 
                int numberOfPosts, bool ifShowEmail, string firstName, string lastName, bool ifShowUserName, Int16 age, bool ifShowAge, Int16 gender, 
                bool ifShowGender, string occupation, bool ifShowOccupation, string company, bool ifShowCompany, string phoneNumber, bool ifShowPhoneNumber, 
                string faxNumber, bool ifShowFaxNumber, string interests, bool ifShowInterests, string homePage, bool ifShowHomePage, string signature, 
                bool ifCustomizeAvatar, byte[] customizeAvatar, string systemAvatar, bool ifForumAdmin, int score, int reputation, 
                string emailVerificationGUIDTag, bool ifVerified, Int16 moderateStatus, Int16 emailVerificationStatus, Int64 ip) 
            : base(conn, transaction)
        {
            #region UserInformation Init
                this._id = id;
                this._ifActive = ifActive;
                this._ifDeleted = ifDeleted;
                this._email = email;
                this._name = name;
                this._password = password;
                this._forgetPasswordGUIDTag = forgetPasswordGUIDTag;
                this._forgetPasswordTagTime = forgetPasswordTagTime;
                this._lastLoginTime = lastLoginTime;
                this._lastLoginIP = lastLoginIP;
                this._joinedTime = joinedTime;
                this._joinedIP = joinedIP;
                this._numberOfPosts = numberOfPosts;
                this._ifShowEmail = ifShowEmail;
                this._firstName = firstName;
                this._lastName = lastName;
                this._ifShowUserName = ifShowUserName;
                this._age = age;
                this._ifShowAge = ifShowAge;
                this._gender = gender;
                this._ifShowGender = ifShowGender;
                this._occupation = occupation;
                this._ifShowOccupation = ifShowOccupation;
                this._company = company;
                this._ifShowCompany = ifShowCompany;
                this._phoneNumber = phoneNumber;
                this._ifShowPhoneNumber = ifShowPhoneNumber;
                this._faxNumber = faxNumber;
                this._ifShowFaxNumber = ifShowFaxNumber;
                this._interests = interests;
                this._ifShowInterests = ifShowInterests;
                this._homePage = homePage;
                this._ifShowHomePage = ifShowHomePage;
                this._signature = signature;
                this._ifCustomizeAvatar = ifCustomizeAvatar;
                this._customizeAvatar = customizeAvatar;
                this._systemAvatar = systemAvatar;
                this._ifForumAdmin = ifForumAdmin;
                this._score = score;
                this._reputation = reputation;
                //it's same to fogetpasswordguidtag
                this._emailVerificationGUIDTag = emailVerificationGUIDTag;
                this._ifVerified = ifVerified;
                this._moderateStatus = moderateStatus;
                this._emailVerificationStatus = emailVerificationStatus;
                this._ip = ip;
                #endregion
            GetAvatar();
        }

        #region Private Function MakeSureUserIsNotDeleted
        private void MakeSureUserIsNotDeleted()
        {
            if (this._ifDeleted) ExceptionHelper.ThrowUserIdNotExist(_id);
        }
        #endregion Private Function MakeSureUserIsNotDeleted

        public static int Add(SqlConnectionWithSiteId conn, SqlTransaction transaction, string email, string displayName, string password, long ip, int siteId, bool ifModearteUser, bool ifVerifyEmail)
        {
            #region
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
            if (password.Length == 0)
            {
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("password");
            }
            //maxlength
            if (email.Length > ForumDBFieldLength.User_emailFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("email",
                    ForumDBFieldLength.User_emailFieldLength);
            }
            //if (displayName.Length > ForumDBFieldLength.User_nameFieldLength)
            //{
            //    ExceptionHelper.ThrowSystemFieldLengthExceededException("displayName",
            //        ForumDBFieldLength.User_nameFieldLength);
            //}
            if (password.Length > ForumDBFieldLength.User_passwordFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("password",
                    ForumDBFieldLength.User_passwordFieldLength);
            }
            #endregion
            Int16 moderateStatus =ifModearteUser ? (short)1 : (short)0;
            Int16 emailVerification = ifVerifyEmail ? (short)1 : (short)0;
            string strDefalutAvatarFilePath = ConstantsHelper.User_Avatar_Default;
            int userId = UserAccess.AddUser(conn, transaction, email, password, displayName, ip, moderateStatus, emailVerification, strDefalutAvatarFilePath);

            return userId;
        }


        public static int Add(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            string email, string displayName, string password, string firstName, string lastName, int age,
            EnumGender gender, string company, string occupation, string phone, string fax, string interests,
            string homepage, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender,
            bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests,
            bool ifShowHomePage)
        {
            string strDefalutAvatarFilePath = ConstantsHelper.User_Avatar_Default;
            return UserAccess.AddUser(conn, transaction, email, displayName, password, firstName, lastName, age, gender, company, occupation,
                phone, fax, interests, homepage, ifShowEmail, ifShowUserName, ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany, ifShowPhone, ifShowFax, ifShowInterests,
                ifShowHomePage,strDefalutAvatarFilePath);
        }


        public virtual void Delete()
        {
            if (_ifDeleted)
                ExceptionHelper.ThrowForumUserOrOperatorNotExistWithIdException(_id);
            else
                UserAccess.DeleteUser(_conn, _transaction, _id);
        }

        public virtual void UpdateEmailVerificationGUIDTag(string emailVerificationGUIDTag)
        {
            UserAccess.UpdateUserOrOperatorEmailVerificationGUIDTag(_conn, _transaction, _id, emailVerificationGUIDTag);
        }

        public virtual void SetEmailVerificationPassing(string emailVerificationGUIDTag)
        {
            if (emailVerificationGUIDTag == _emailVerificationGUIDTag)
            {
                UserAccess.SetUserEmailVerificationPassing(_conn, _transaction, _id);
            }
            else
            {
                ExceptionHelper.ThrowEmailVerificationGuidTagWrong();
            }
            if (_moderateStatus == Convert.ToInt16(EnumUserModerateStatus.enumDoNotNeed) || _moderateStatus == Convert.ToInt16(EnumUserModerateStatus.enumModerated))
                SetVerified();

        }
        public virtual void UpdateModerateStatus(EnumUserModerateStatus moderateStatus)
        {
            UserAccess.UpdateUserModerateStatus(_conn, _transaction, _id, moderateStatus);
        }

        private void SetVerified()
        {
            UserAccess.SetUserVerified(_conn, _transaction, _id);
        }

        public virtual void ApproveRegistration()
        {
            if (_ifDeleted)
                ExceptionHelper.ThrowForumUserOrOperatorNotExistWithIdException(_id);
            if (_moderateStatus == Convert.ToInt16(EnumUserModerateStatus.enumModerated) || _moderateStatus == Convert.ToInt16(EnumUserModerateStatus.enumRefused))
                ExceptionHelper.ThrowForumUseOrOperatorHaveBeenModerated(_name);
            UpdateModerateStatus(EnumUserModerateStatus.enumModerated);
            if (_emailVerificationStatus == Convert.ToInt16(EnumUserEmailVerificationStatus.enumDoNotNeed) || _emailVerificationStatus == Convert.ToInt16(EnumUserEmailVerificationStatus.enumVerified))
            {
                SetVerified();
            }
        }

        public virtual void RefuseRegistration()
        {
            if (_ifDeleted)
                ExceptionHelper.ThrowForumUserOrOperatorNotExistWithIdException(_id);
            if (_moderateStatus == Convert.ToInt16(EnumUserModerateStatus.enumModerated) || _moderateStatus == Convert.ToInt16(EnumUserModerateStatus.enumRefused))
                ExceptionHelper.ThrowForumUseOrOperatorHaveBeenModerated(_name);
            UpdateModerateStatus(EnumUserModerateStatus.enumRefused);
            Delete();
        }

        ///
        ///Add without permission
        ///
        public void ResetPassword(string newPassword)
        {
            #region Field Check
            if (newPassword.Length == 0)
            {
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("oldPassword");
            }
            if (newPassword.Length > ForumDBFieldLength.User_passwordFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("newPassword",
                                                        ForumDBFieldLength.User_passwordFieldLength);
            }
            #endregion
            UserAccess.UpdateUserOrOperatorPassword(_conn, _transaction, _id, newPassword);
        }

        public override void UpdateProfile(string email, string displayName, string firstName
            , string lastName, int age, EnumGender gender, string company, string occupation
            , string phone, string fax, string interests, string homepage,string score
            ,string reputation, bool ifShowEmail, bool ifShowUserName, bool ifShowAge
            , bool ifShowGender, bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone
            , bool ifShowFax, bool ifShowInterests, bool ifShowHomePage)
        {
            if (IfForumAdmin)
                base.UpdateProfile(email, displayName, firstName, lastName, age, gender, company
                    , occupation, phone, fax, interests, homepage, score, reputation, ifShowEmail
                    , ifShowUserName, ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany
                    , ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage);
            else
            {
                //DataTable table = UserAccess.GetUserOrOperatorById(_conn,_transaction,_id);
                //score = table.Rows[0]["ForumScore"].ToString();
                //reputation = table.Rows[0]["ForumReputation"].ToString();
                score = _score.ToString();
                reputation = _reputation.ToString();
                base.UpdateProfile(email, displayName, firstName, lastName, age, gender, company
                    , occupation, phone, fax, interests, homepage, score, reputation, ifShowEmail
                    , ifShowUserName, ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany
                    , ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage);
            }

               
            //UserAccess.UpdateUseProfile(_conn, _transaction, _id, email, displayName, firstName, lastName, age,
             //                           gender, company, occupation, phone, fax, interests, homepage,score,reputation, ifShowEmail,
               //                         ifShowUserName, ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany,
                //                        ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage);
        }

      


        public virtual void Update(string email, string displayName, string firstName, string lastName, int age,
            EnumGender gender, string company, string occupation, string phone, string fax, string interests,
            string homepage, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender,
            bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests,
            bool ifShowHomePage, int score, int reputation)
        {
            MakeSureUserIsNotDeleted();
            CheckFieldsLength(email, displayName, firstName, lastName, company, occupation, phone, fax, interests, homepage);
            UserAccess.UpdateUser(_conn, _transaction, this._id, email, displayName, firstName, lastName, age, gender, company, occupation,
                phone, fax, interests, homepage, ifShowEmail, ifShowUserName, ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany,
                ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage, score, reputation);
        }

        protected void Update(string email, string displayName, string firstName, string lastName, int age,
            EnumGender gender, string company, string occupation, string phone, string fax, string interests,
            string homepage, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender,
            bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests,
            bool ifShowHomePage, List<int> userGroupIds, int score, int reputation, UserOrOperator operatingUserOrOperator)
        {
            Update(email, displayName, firstName, lastName, age, gender, company, occupation, phone, fax, interests, homepage,
                ifShowEmail, ifShowUserName, ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany, ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage,
                score, reputation);
            UserGroupsOfUserOrOperatorWithPermissionCheck userGroupsTmp = GetUserGroups();
            UserGroupWithPermissionCheck[] userGroups = userGroupsTmp.GetAllUserGroups();
            MembersOfUserGroupWithPermissionCheck members = null;
            foreach (UserGroupWithPermissionCheck userGroup in userGroups)
            {
                
                if (userGroup.IfAllForumUsersGroup)
                    continue;
                members = userGroup.GetMembers();
                members.Delete(_id);
            }
            if (userGroupIds.Count > 0)
            {
                CheckIfCanAddToUserGroup(operatingUserOrOperator);
                UserGroupWithPermissionCheck userGroup = null;
                MembersOfUserGroupWithPermissionCheck membersTmp = null;
                for (int i = 0; i < userGroupIds.Count; i++)
                {
                    userGroup = new UserGroupWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, userGroupIds[i]);
                    membersTmp = userGroup.GetMembers();
                    membersTmp.Add(_id);
                }
            }
        }

        private void CheckIfCanAddToUserGroup(UserOrOperator operatingUserOrOperator)
        {
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, operatingUserOrOperator);
            if (!forumFeature.IfEnableGroupPermission)
                ExceptionHelper.ThrowForumSettingsCloseGroupPermissionFunction();
                //throw Exception
        }


        protected UserGroupsOfUserOrOperatorWithPermissionCheck GetUserGroups(UserOrOperator operatingUserOrOperator)
        {
            return new UserGroupsOfUserOrOperatorWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, _id);
        }

        protected UserReputationGroupWithPermissionCheck GetUserReputationGroup(UserOrOperator operatingUserOrOperator)
        {
            return null;
        }

        public abstract UserGroupsOfUserOrOperatorWithPermissionCheck GetUserGroups();

        public abstract UserReputationGroupWithPermissionCheck GetUserReputationGroup();

        /*---------------------------2.0-------------------------------*/

        public override abstract PostsOfUserOrOperatorWithPermissionCheck GetPosts();

        public override abstract FavoritesWithPermissionCheck GetFavorites();

        public override abstract SubscribesWithPermissionCheck GetSubscribes();

        public override abstract BansOfIPWithPermissionCheck GetBansOfIP();

        public override abstract BansOfUserOrOperatorWithPermissionCheck GetBansOfUserOrOperator();

        public override abstract InMessagesOfRecieverWithPermissionCheck GetInMessages();

        public override abstract OutMessagesOfSenderWithPermissionCheck GetOutMessages();

        public override abstract bool IfBanById();

        public override abstract void LiftBan();
    }
}
