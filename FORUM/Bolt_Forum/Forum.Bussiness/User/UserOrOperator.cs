#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using System.Drawing;
using System.IO;
using Com.Comm100.Framework.FieldLength;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class UserOrOperator
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        protected int _id;
        protected bool _ifActive;
        protected bool _ifDeleted;
        protected string _email;
        protected string _name;
        protected string _password;
        protected string _forgetPasswordGUIDTag;
        protected DateTime _forgetPasswordTagTime;

        protected DateTime _lastLoginTime;
        protected Int64 _lastLoginIP;
        protected DateTime _joinedTime;
        protected Int64 _joinedIP;

        protected int _numberOfPosts;
        protected bool _ifShowEmail;
        protected string _firstName;
        protected string _lastName;
        protected bool _ifShowUserName;
        protected Int16 _age;
        protected bool _ifShowAge;
        protected Int16 _gender;
        protected bool _ifShowGender;
        protected string _occupation;
        protected bool _ifShowOccupation;
        protected string _company;
        protected bool _ifShowCompany;
        protected string _phoneNumber;
        protected bool _ifShowPhoneNumber;
        protected string _faxNumber;
        protected bool _ifShowFaxNumber;
        protected string _interests;
        protected bool _ifShowInterests;
        protected string _homePage;
        protected bool _ifShowHomePage;
        protected string _signature;
        protected bool _ifCustomizeAvatar;
        protected byte[] _customizeAvatar;
        protected string _systemAvatar;
        protected string _avatar;
        /*-------------------2.0-------------------*/
        protected bool _ifForumAdmin;
        protected int _score;
        protected int _reputation;
        protected Int64 _ip;
        #endregion

        #region properties
        public int Id
        {
            get { return this._id; }
        }
        public bool IfActive
        {
            get { return this._ifActive; }
        }
        public bool IfDeleted
        {
            get { return this._ifDeleted; }
        }
        public string Email
        {
            get { return this._email; }
        }
        public string DisplayName
        {
            get { return this._name; }
        }
        public string Password
        {
            get { return this._password; }
        }
        public string ForgetPasswordGUIDTag
        {
            get { return this._forgetPasswordGUIDTag; }
        }
        public DateTime ForgetPasswordTagTime
        {
            get { return this._forgetPasswordTagTime; }
        }
        public DateTime JoinedTime
        {
            get { return this._joinedTime; }
        }
        public Int64 JoinedIP
        {
            get { return this._joinedIP; }
        }
        public DateTime LastLoginTime
        {
            get { return this._lastLoginTime; }
        }
        public Int64 LastLoginIP
        {
            get { return this._lastLoginIP; }
        }
        public int NumberOfPosts
        {
            get { return this._numberOfPosts; }
        }
        public bool IfShowEmail
        {
            get { return this._ifShowEmail; }
        }
        public string FirstName
        {
            get { return this._firstName; }
        }
        public string LastName
        {
            get { return this._lastName; }
        }
        public bool IfShowUserName
        {
            get { return this._ifShowUserName; }
        }
        public Int16 Age
        {
            get { return this._age; }
        }
        public bool IfShowAge
        {
            get { return this._ifShowAge; }
        }
        public EnumGender Gender
        {
            get { return (EnumGender)this._gender; }
        }
        public bool IfShowGender
        {
            get { return this._ifShowGender; }
        }
        public string Occupation
        {
            get { return this._occupation; }
        }
        public bool IfShowOccupation
        {
            get { return this._ifShowOccupation; }
        }
        public string Company
        {
            get { return this._company; }
        }
        public bool IfShowCompany
        {
            get { return this._ifShowCompany; }
        }
        public string PhoneNumber
        {
            get { return this._phoneNumber; }
        }
        public bool IfShowPhoneNumber
        {
            get { return this._ifShowPhoneNumber; }
        }
        public string FaxNumber
        {
            get { return this._faxNumber; }
        }
        public bool IfShowFaxNumber
        {
            get { return this._ifShowFaxNumber; }
        }
        public string Interests
        {
            get { return this._interests; }
        }
        public bool IfShowInterests
        {
            get { return this._ifShowInterests; }
        }
        public string HomePage
        {
            get { return this._homePage; }
        }
        public bool IfShowHomePage
        {
            get { return this._ifShowHomePage; }
        }
        public string Signature
        {
            get { return this._signature; }
        }
        public string Avatar
        {
            get { return this._avatar; }
        }
        public bool IfCustomizeAvatar
        { get { return this._ifCustomizeAvatar; } }

        /*-----------------------2.0-----------------------*/
        public bool IfForumAdmin
        {
            get { return this._ifForumAdmin; }
        }
        public int Score
        {
            get { return this._score; }
        }
        public int Reputation
        {
            get { return this._reputation; }
        }
        public Int64 IP
        {
            get { return this._ip; }
        }
        #endregion

        public UserOrOperator(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            this._conn = conn;
            this._transaction = transaction;
        }

        #region Protected Function GetAvater
        protected void GetAvatar()
        {
            if (this._ifCustomizeAvatar)
                this._avatar = ConstantsHelper.ForumAvatarTemporaryFolder + "/" + this.GetCustomizeAvatar();
            else
                this._avatar = "images/SystemAvatar/" + this._systemAvatar;//System.Configuration.ConfigurationSettings.AppSettings["ForumUrl"].ToString() +
        }
        #endregion

        #region Private Function GetCustomizeAvatar
        private string GetCustomizeAvatar()
        {
            int siteId = this._conn.SiteId;
            int id = this._id;
            string strAvatarFileName = siteId + @"/" + id + ConstantsHelper.User_Avatar_FileType;
            string strAvatarDirectory = System.Web.HttpContext.Current.Server.MapPath(@"~/" +
                                           ConstantsHelper.ForumAvatarTemporaryFolder + @"/" + siteId.ToString());
            string strAvatarFilePath = strAvatarDirectory + @"\" + id + ConstantsHelper.User_Avatar_FileType;
            string strAvatarFilePathTemp = strAvatarFilePath + "Temp";

            /* if avatar is not exsit, load avatar from database */
            if (File.Exists(strAvatarFilePath) == false)
            {
                byte[] bAvatars = UserAccess.GetUserOrOperatorAvatar(this._conn, this._transaction, id);
                //no avatar data in database
                if (bAvatars == null)
                {
                    return null;
                }
                #region Update avatar file
                FileStream fs = null;
                try
                {
                    if (Directory.Exists(strAvatarDirectory) == false)
                        Directory.CreateDirectory(strAvatarDirectory);
                    fs = File.Create(strAvatarFilePath);
                    fs.Write(bAvatars, 0, bAvatars.Length);
                    //fs = File.Create(strAvatarFilePathTemp);
                    //fs.Write(bAvatars,0
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
                #endregion
            }
            //for 'Opera' and 'world Window'
            Random rdNum = new Random();
            strAvatarFileName += "?" + rdNum.Next(1000000000);//default 1000000000

            return strAvatarFileName;
        }
        #endregion

        #region Protected Function CheckFieldFormat
        protected static bool CheckFieldFormat(string format, string field)
        {
            Regex regularExpressions = new Regex(format, RegexOptions.IgnoreCase);
            Match match = regularExpressions.Match(field, 0, field.Length);
            return match.Success;
        }
        #endregion

        #region Protected Function CheckFieldsLength
        protected void CheckFieldsLength(string email, string displayName, string firstName, string lastName,
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
            if (this._name.ToLower() != displayName.ToLower())
            {
                if (UserAccess.GetCountOfNotDeletedUsersByDisplayName(this._conn, this._transaction, displayName) != 0)
                {
                    ExceptionHelper.ThrowUserDisplayNameNotUniqueException();//user
                }
            }
            //Max length
            if (this._email != email)
            {
                if (UserAccess.GetCountOfNotDeletedUsersByEmail(this._conn, this._transaction, email) != 0)
                {
                    ExceptionHelper.ThrowUserEmailNotUniqueException();//user
                }
            }
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

        protected void CheckMaxLengthofSignature(string signature)
        {
            signature = signature.Replace("<p>", "").Replace("</p>", "").Replace("<!-- alert(66666); // -->", "");
            if (signature.Length > ForumDBFieldLength.User_signatureFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("sginature",
                    ForumDBFieldLength.User_signatureFieldLength);
            }
        }


        #endregion Protected Function CheckFieldsLength

        #region Public Function ResetPassword
        public void ResetPassword(string oldPassword, string newPassword)
        {
            #region Field Check
            //check operator password 
            if (oldPassword != this._password)
            {
                ExceptionHelper.ThrowOperatorOldPasswordIsWrongException();//operator
            }
            if (oldPassword.Length == 0)
            {
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("newPassword");
            }
            if (newPassword.Length == 0)
            {
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("oldPassword");
            }
            if (oldPassword.Length > ForumDBFieldLength.User_passwordFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("oldPassword",
                                                        ForumDBFieldLength.User_passwordFieldLength);
            }
            if (newPassword.Length > ForumDBFieldLength.User_passwordFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("newPassword",
                                                        ForumDBFieldLength.User_passwordFieldLength);
            }
            #endregion
            CommFun.CheckCommonPermissionInUI(this);
            UserAccess.UpdateUserOrOperatorPassword(_conn, _transaction, _id, newPassword);
        }
        #endregion

        #region Public Function UpdateProfile
        public virtual void UpdateProfile(string email, string displayName, string firstName, string lastName, int age,
            EnumGender gender, string company, string occupation, string phone, string fax, string interests,
            string homepage, string score, string reputation, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender,
            bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests,
            bool ifShowHomePage)
        {
            CommFun.CheckCommonPermissionInUI(this);
            CheckUpdatePermission(Convert.ToInt32(score), Convert.ToInt32(reputation));
            CheckFieldsLength(email, displayName, firstName, lastName, company, occupation, phone, fax, interests, homepage);
            UserAccess.UpdateUseProfile(_conn, _transaction, _id, email, displayName, firstName, lastName, age,
                                        gender, company, occupation, phone, fax, interests, homepage, score, reputation, ifShowEmail,
                                        ifShowUserName, ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany,
                                        ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage);

        }
        #endregion

        private void CheckUpdatePermission(int score, int reputation)
        {
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, this);
            if (CheckIfForumAdmin())
            {
                if (!forumFeature.IfEnableScore)
                {
                    if(this._score != score)
                        ExceptionHelper.ThrowForumSettingsCloseScoreFunctio();
                }
                if (!forumFeature.IfEnableReputation)
                {
                    if(this._reputation != reputation)
                        ExceptionHelper.ThrowForumSettingsCloseReputationFunctio();
                }
            }
        }

        private Boolean CheckIfForumAdmin()
        {
            Boolean ifForumAdminOrSiteAdmin = false;
            //if (this.IfForumAdmin || (this as OperatorWithPermissionCheck).IfAdmin)
            if (this.IfForumAdmin)
                ifForumAdminOrSiteAdmin = true;
            if (this is OperatorWithPermissionCheck)
            {
                if ((this as OperatorWithPermissionCheck).IfAdmin == true)
                    ifForumAdminOrSiteAdmin = true;
            }            

            return ifForumAdminOrSiteAdmin;
        }

        #region Public Function UpdateSignature
        public virtual void UpdateSignature(string signature)
        {
            //#region Field Check
            //if (signature.Length > ForumDBFieldLength.User_signatureFieldLength)
            //{
            //    ExceptionHelper.ThrowSystemFieldLengthExceededException("sginature",
            //        ForumDBFieldLength.User_signatureFieldLength);
            //}
            //#endregion
            //CommFun.UserPermissionCache().CheckMaxLengthofSignature(_operatingUserOrOperator, signature.Length);
            //CheckMaxLengthofSignature(signature);
            CommFun.CheckCommonPermissionInUI(this);
            UserAccess.UpdateUserOrOperatorSignature(_conn, _transaction, _id, signature);
        }
        #endregion

        #region Public Function UpdateAvatar
        public virtual void UpdateAvatar(byte[] avatar)
        {
            CommFun.CheckCommonPermissionInUI(this);
            UserAccess.UpdateUserOrOperatorAvatar(_conn, _transaction, _id,
                true, this._systemAvatar, avatar);
        }
        #endregion

        #region Public Function UpdateAvatarAsSystemProvided
        public void UpdateAvatarAsSystemProvided(string avatar)
        {
            CommFun.CheckCommonPermissionInUI(this);
            UserAccess.UpdateUserOrOperatorAvatar(_conn, _transaction, _id,
                false, avatar, new byte[] { });
        }
        #endregion

        #region Public Function UpdateLastLoginTimeToCurrentTime
        public virtual void UpdateLastLoginTimeToCurrentTime()
        {
            UserAccess.UpdateUserOrOperatorLastLoginTimeToCurrentTime(_conn, _transaction, _id);
        }
        #endregion

        #region Public Function UpdateLastLoginIp
        public virtual void UpdateLastLoginIp(long ip)
        {
            UserAccess.UpdateUserOrOperatorLastLoginIp(_conn, _transaction, _id, ip);
        }
        #endregion

        #region Public Function IncreaseNumberPostsByOne
        public virtual void IncreaseNumberPostsByOne()
        {
            UserAccess.IncreaseUserOrOperatorNumberOfPostsByOne(this._conn, this._transaction, this._id);
        }
        #endregion

        //9.22 new add
        #region Public Function DecreaseAutorPostsNumberByOne
        public virtual void DecreaseAutorPostsNumberByOne()
        {
            UserAccess.DecreaseAutorPostsNumberByOne(this._conn, this._transaction, this._id);
        }
        #endregion

        /*--------------------------2.0----------------------------*/
        #region UserPermissionCache
        public void GetUserPermissionCache(out UserPermissionCache userPermissionList)
        {
            /*2.0 get user permission list*/
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction,
                this);
            UserPermissionCache userPermission = GetUserBasePermission(forumFeature, this);
            Hashtable forumPermissionTable;
            CreateUserPermissionList(out forumPermissionTable, userPermission, this, forumFeature);
            userPermission.UserForumPermissionsList = forumPermissionTable;
            userPermissionList = userPermission;
        }

        #region Forum Permission
        private void CreateUserPermissionList(out Hashtable forumPermissionTable, UserPermissionCache userPermission,
            UserOrOperator operatingUserOrOperator, ForumFeatureWithPermissionCheck forumFeature)
        {
            forumPermissionTable = new Hashtable();
            //get all forums in site
            ForumsOfSiteWithPermissionCheck forumsOfSite = new ForumsOfSiteWithPermissionCheck(_conn, _transaction,
                operatingUserOrOperator);
            string[] forumPath; int[] forumIds;
            forumsOfSite.GetAllForums(out forumPath, out forumIds);
            //foreach all forums by id
            foreach (var id in forumIds)
            {
                UserForumPermissionItem forumPermissionitem = CreateOneForumPermissionItem(
                    id, userPermission, operatingUserOrOperator, forumFeature);
                forumPermissionTable.Add(id, forumPermissionitem);
            }
        }

        private UserForumPermissionItem CreateOneForumPermissionItem(int forumId, UserPermissionCache userPermission,
            UserOrOperator operatingUserOrOperator, ForumFeatureWithPermissionCheck forumFeature)
        {
            //get ForumPermission
            ForumPermissionManager forumPermission = new ForumPermissionManager(_conn, _transaction, forumId);
            //If InheritPermission, use user permission
            if (forumPermission.IfInheritPermission)
            {
                return new UserForumPermissionItem(operatingUserOrOperator.Id, forumId, IfModerator(operatingUserOrOperator, forumId),
                    userPermission.IfAllowViewForum, userPermission.IfAllowViewTopic, userPermission.IfAllowPost,
                    userPermission.MinIntervalForPost, userPermission.MaxLengthOfPost, userPermission.IfPostNotNeedModeration,
                    userPermission.IfAllowUrl, userPermission.IfAllowUploadImage);
            }
            else
            {
                //get (reputation groups and user groups) of Forum and (reputation groups and user groups) of user
                UserGroupBase[] usergroups; UserReputationGroupBase[] userReputationgroups;
                GetRepuationAndUserGroupsOfForumInUser(forumId, operatingUserOrOperator,
                    out usergroups, out userReputationgroups, forumFeature);
                //get every permission item in (reputation groups and user groups)
                return GetUserForumPermissionItemIfNotHeritPermission(operatingUserOrOperator, forumId,
                    usergroups, userReputationgroups, forumFeature, userPermission);
            }
        }

        private void GetRepuationAndUserGroupsOfForumInUser(int forumId, UserOrOperator operatingUserOrOperator,
            out UserGroupBase[] usergroups, out UserReputationGroupBase[] reputationgroups,
            ForumFeatureWithPermissionCheck forumFeature)
        {
            if (forumFeature.IfEnableGroupPermission)
            {
                //get user groups of user
                UserGroupsOfUserOrOperatorWithPermissionCheck alluserGroupsOfUser =
                    new UserGroupsOfUserOrOperatorWithPermissionCheck(
                    _conn, _transaction, operatingUserOrOperator, operatingUserOrOperator.Id);
                UserGroupWithPermissionCheck[] userGroupsOfUser = alluserGroupsOfUser.GetAllUserGroups();
                //get user groups of forum
                UserGroupsOfForumWithPermissionCheck alluserGroupsOfForum =
                    new UserGroupsOfForumWithPermissionCheck(_conn, _transaction, operatingUserOrOperator,
                        forumId);
                UserGroupOfForumWithPermissionCheck[] userGroupsOfForum = alluserGroupsOfForum.GetAllUserGroups();
                //get user groups in user's usergroups and forum's usergroups
                if (userGroupsOfUser == null || userGroupsOfForum == null)
                    usergroups = null;
                else
                {
                    var userGroups = from a in userGroupsOfUser
                                     from b in userGroupsOfForum
                                     where a.UserGroupId == b.UserGroupId
                                     select a;
                    usergroups = userGroups.ToArray();
                }
            }
            else
                usergroups = null;
            if (forumFeature.IfEnableReputationPermission)
            {
                // get reputation groups of user
                UserReputationGroupsWithPermissionCheck alluserReputationGroups = new UserReputationGroupsWithPermissionCheck(
                    _conn, _transaction, operatingUserOrOperator);
                UserReputationGroupWithPermissionCheck reputationGroupOfuser = alluserReputationGroups.GetUserReputationGroupOfUserOrOperator(
                    operatingUserOrOperator.Id);
                UserReputationGroupWithPermissionCheck[] userReputationGroups = new UserReputationGroupWithPermissionCheck[]{
                    reputationGroupOfuser};
                // get reputation groups of froum
                UserReputationGroupsOfForumWithPermissionCheck alluserReputationOfForum =
                    new UserReputationGroupsOfForumWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, forumId);
                UserReputationGroupOfForumWithPermissionCheck[] userReputationOfForum = alluserReputationOfForum.GetAllGroups();
                //get reputation groups in user's and forum's repuation
                if (reputationGroupOfuser == null ||userReputationGroups == null || userReputationOfForum == null)
                    reputationgroups = null;
                else
                {
                    var reputationGroups = from a in userReputationGroups
                                           from b in userReputationOfForum
                                           where a.GroupId == b.GroupId
                                           select a;
                    reputationgroups = reputationGroups.ToArray();
                }
            }
            else
                reputationgroups = null;
        }

        private UserForumPermissionItem GetUserForumPermissionItemIfNotHeritPermission(
            UserOrOperator operatingUserOrOperator, int forumId,
            UserGroupBase[] usergroups, UserReputationGroupBase[] userReputationgroups,
            ForumFeatureWithPermissionCheck forumFeature, UserPermissionCache userPermission)
        {
            //get user groups permission list
            List<UserGroupPermissionForForumWithPermissionCheck> userGroupPermissions =
                new List<UserGroupPermissionForForumWithPermissionCheck>();
            //get reputation groups permission list
            List<UserReputationGroupPermissionForForumWithPermissionCheck> reputationPermissions =
                new List<UserReputationGroupPermissionForForumWithPermissionCheck>();

            if (forumFeature.IfEnableGroupPermission)
            {
                foreach (var usergroup in usergroups)
                {
                    UserGroupPermissionForForumWithPermissionCheck usergroupPermission =
                        new UserGroupPermissionForForumWithPermissionCheck(
                        _conn, _transaction, operatingUserOrOperator, usergroup.UserGroupId, forumId);
                    userGroupPermissions.Add(usergroupPermission);
                }
            }
            if (forumFeature.IfEnableReputationPermission)
            {
                if (userReputationgroups != null)
                {
                    foreach (var userReputationgroup in userReputationgroups)
                    {
                        UserReputationGroupPermissionForForumWithPermissionCheck reputationPermission =
                            new UserReputationGroupPermissionForForumWithPermissionCheck(_conn, _transaction, operatingUserOrOperator,
                                userReputationgroup.GroupId, forumId);
                        reputationPermissions.Add(reputationPermission);
                    }
                }
            }

            if (!forumFeature.IfEnableReputationPermission && !forumFeature.IfEnableGroupPermission)
            {
                return new UserForumPermissionItem(operatingUserOrOperator.Id,
                    forumId, IfModerator(operatingUserOrOperator, forumId), userPermission.IfAllowViewForum,
                    userPermission.IfAllowViewTopic, userPermission.IfAllowPost,
                    userPermission.MinIntervalForPost, userPermission.MaxLengthOfPost, userPermission.IfPostNotNeedModeration,
                    userPermission.IfAllowUrl, userPermission.IfAllowUploadImage);
            }

            int userOrOperatorId = operatingUserOrOperator.Id;
            bool ifAllowViewForum = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, "IfAllowViewForum");
            bool ifAllowViewTopic = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, "IfAllowViewTopic");
            bool ifAllowPost = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, "IfAllowPost");
            int minIntervalForPost = GetOneItemPermissionMaxOrMin(userGroupPermissions, reputationPermissions, true, "MinIntervalForPost");
            int maxLengthOfPost = GetOneItemPermissionMaxOrMin(userGroupPermissions, reputationPermissions, false, "MaxLengthOfPost"); ;
            bool ifPostNotNeedModeration = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, "IfPostNotNeedModeration");
            //bool ifAllowHTML = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, "IfAllowHTML");
            bool ifAllowUrl = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, "IfAllowUrl");
            bool ifAllowUploadImage = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, "IfAllowUploadImage");
            return new UserForumPermissionItem(userOrOperatorId, forumId, IfModerator(operatingUserOrOperator, forumId),
                ifAllowViewForum, ifAllowViewTopic, ifAllowPost, minIntervalForPost,
                maxLengthOfPost, ifPostNotNeedModeration, ifAllowUrl, ifAllowUploadImage);
        }

        private bool GetOneItemPermissionIfAllow(
            IList usergroups,
            IList userReputationgroups,
            string methodName)
        {
            if (usergroups != null && usergroups.Count > 0)
            {
                foreach (var usergroup in usergroups)
                {
                    PropertyInfo info = usergroup.GetType().GetProperty(methodName);
                    bool ifAllow = (bool)info.GetValue(usergroup, null);
                    if (ifAllow == true)
                        return true;
                }
            }
            if (userReputationgroups != null && userReputationgroups.Count > 0)
            {
                foreach (var reputationgroup in userReputationgroups)
                {
                    PropertyInfo info = reputationgroup.GetType().GetProperty(methodName);
                    bool ifAllow = (bool)info.GetValue(reputationgroup, null);
                    if (ifAllow == true)
                        return true;
                }
            }
            return false;
        }

        private bool GetOneItemPermissionIfAllow(IList usergroups,
            IList userReputationgroups,UserPermissionSettingWithPermissionCheck userPermissionSetting,
            string methodName)
        {
            bool ifAllow = GetOneItemPermissionIfAllow(usergroups, userReputationgroups, methodName);
            if (ifAllow == true)
                return true;

            if (userPermissionSetting != null)
            {
                PropertyInfo info = userPermissionSetting.GetType().GetProperty(methodName);
                bool ifAllow2 = (bool)info.GetValue(userPermissionSetting, null);
                if (ifAllow2 == true)
                    return true;
            }

            return false;
        }

        private int GetOneItemPermissionMaxOrMin(
            IList usergroups,
            IList userReputationgroups, bool ifMin,
            string methodName)
        {
            int maxOrmin = 0; bool iffirst = true;
            if (ifMin)
                maxOrmin = int.MaxValue;
            if (usergroups != null && usergroups.Count > 0)
            {
                foreach (var usergroup in usergroups)
                {
                    PropertyInfo info = usergroup.GetType().GetProperty(methodName);
                    int temp = (int)info.GetValue(usergroup, null);
                    if (iffirst) { maxOrmin = temp; iffirst = false; }

                    if (temp < maxOrmin && ifMin)
                        maxOrmin = temp;
                    else if (temp > maxOrmin && !ifMin)
                        maxOrmin = temp;
                }
            }

            if (userReputationgroups != null && userReputationgroups.Count > 0)
            {
                foreach (var reputationgroup in userReputationgroups)
                {
                    PropertyInfo info = reputationgroup.GetType().GetProperty(methodName);
                    int temp = (int)info.GetValue(reputationgroup, null);
                    if (iffirst) { maxOrmin = temp; iffirst = false; }
                    if (temp < maxOrmin && ifMin)
                        maxOrmin = temp;
                    else if (temp > maxOrmin && !ifMin)
                        maxOrmin = temp;
                }
            }
            return maxOrmin;
        }

        private int GetOneItemPermissionMaxOrMin(
            IList usergroups,
            IList userReputationgroups,
            UserPermissionSettingWithPermissionCheck userPermissionSetting,
            bool ifMin,
            string methodName)
        {
            int maxOrmin = 0; //bool iffirst = true;
            if (ifMin)
                maxOrmin = int.MaxValue;
            maxOrmin = GetOneItemPermissionMaxOrMin(usergroups, userReputationgroups, ifMin, methodName);

            if (userPermissionSetting != null)
            {
                PropertyInfo info = userPermissionSetting.GetType().GetProperty(methodName);
                int temp = (int)info.GetValue(userPermissionSetting, null);
                //if (iffirst) { maxOrmin = temp; iffirst = false; }
                if (temp < maxOrmin && ifMin)
                    maxOrmin = temp;
                else if (temp > maxOrmin && !ifMin)
                    maxOrmin = temp;
            }

            return maxOrmin;
        }
        private bool IfModerator(UserOrOperator userOrOperator, int forumId)
        {
            bool ifModerator = false;
            ModeratorsWithPermisisonCheck moderators = new ModeratorsWithPermisisonCheck(_conn, _transaction, forumId, userOrOperator);
            Moderator[] moderator = moderators.GetAllModerators();
            for (int i = 0; i < moderator.Length; i++)
            {
                if (userOrOperator.Id != 0)
                {
                    if (moderator[i].Id == userOrOperator.Id)
                    {
                        ifModerator = true;
                        break;
                    }
                }
            }
            return ifModerator;
        }
        #endregion
        #region User Base Permission
        private UserPermissionCache GetUserBasePermission(
            ForumFeatureWithPermissionCheck forumFeature,
            UserOrOperator operatingUserOrOperator)
        {
            //get user groups permission list
            List<UserGroupPermissionWithPermissionCheck> userGroupPermissions =
                new List<UserGroupPermissionWithPermissionCheck>();
            //get reputation groups permission list
            List<UserReputationGroupPermissionWithPermissionCheck> reputationPermissions =
                new List<UserReputationGroupPermissionWithPermissionCheck>();

            UserPermissionSettingWithPermissionCheck userPermission = null;

            /*If Enable User Group,Use User Group Permission*/
            if (forumFeature.IfEnableGroupPermission)
            {
                //get user groups of user
                UserGroupsOfUserOrOperatorWithPermissionCheck alluserGroupsOfUser =
                    new UserGroupsOfUserOrOperatorWithPermissionCheck(
                    _conn, _transaction, operatingUserOrOperator, operatingUserOrOperator.Id);
                UserGroupWithPermissionCheck[] userGroupsOfUser = alluserGroupsOfUser.GetAllUserGroups();

                foreach (var usergroup in userGroupsOfUser)
                {
                    UserGroupPermissionWithPermissionCheck usergroupPermission = new UserGroupPermissionWithPermissionCheck(
                        _conn, _transaction, operatingUserOrOperator, usergroup.UserGroupId);
                    userGroupPermissions.Add(usergroupPermission);
                }
            }
            /*If Enable Reputation Group, Use User Reputation Group Permission*/
            if (forumFeature.IfEnableReputationPermission)
            {
                // get reputation groups of user
                UserReputationGroupsWithPermissionCheck alluserReputationGroups = new UserReputationGroupsWithPermissionCheck(
                    _conn, _transaction, operatingUserOrOperator);
                UserReputationGroupWithPermissionCheck[] userReputationGroups = new UserReputationGroupWithPermissionCheck[]{ 
                    alluserReputationGroups.GetUserReputationGroupOfUserOrOperator(_id)};

                foreach (var userReputationgroup in userReputationGroups)
                {
                    if (userReputationgroup != null)
                    {
                        UserReputationGroupPermissionWithPermissionCheck reputationPermission =
                            new UserReputationGroupPermissionWithPermissionCheck(_conn, _transaction, operatingUserOrOperator,
                                userReputationgroup.GroupId);
                        reputationPermissions.Add(reputationPermission);
                    }
                }
            }
            /*If not, Use User Permission Setting's Permission*/
            if (!forumFeature.IfEnableGroupPermission)
            {
                userPermission = new UserPermissionSettingWithPermissionCheck(
                   _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
                //return new UserPermissionCache(operatingUserOrOperator.Id, null,
                //    operatingUserOrOperator.IfForumAdmin,
                //    userPermission.IfAllowCustomizeAvatar, userPermission.MaxLengthofSignature,
                //    userPermission.IfAllowUploadAttachment, userPermission.MaxCountOfAttacmentsForOnePost,
                //    userPermission.MaxSizeOfOneAttachment, userPermission.MaxSizeOfAllAttachments,
                //    userPermission.MaxCountOfMessageSendOneDay, userPermission.IfAllowSearch,
                //    userPermission.MinIntervalForSearch, userPermission.IfAllowViewForum,
                //    userPermission.IfAllowViewTopic, userPermission.IfAllowPost, userPermission.MinIntervalForPost,
                //    userPermission.MaxLengthOfPost, userPermission.IfPostNotNeedModeration, 
                //    userPermission.IfAllowUrl, userPermission.IfAllowUploadImage, userPermission.IfSignatureAllowInsertImage
                //    ,userPermission.IfSignatureAllowUrl);
            }

            int userOrOperatorId = operatingUserOrOperator.Id;
            /*base permission*/
            //bool ifAdministrator = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, "IfAdministrator");
            //bool ifModerator = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, "IfModerator");
            bool ifAllowCustomizeAvatar = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, userPermission,
                                    "IfAllowCustomizeAvatar");
            int maxLengthofSignature = GetOneItemPermissionMaxOrMin(userGroupPermissions, reputationPermissions,userPermission, false, "MaxLengthofSignature");
            bool ifAllowUploadAttachment = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, userPermission, "IfAllowUploadAttachment");
            int maxCountOfAttacmentsForOnePost = GetOneItemPermissionMaxOrMin(userGroupPermissions, reputationPermissions,userPermission, false,
                                    "MaxCountOfAttacmentsForOnePost");
            int maxSizeOfOneAttachment = GetOneItemPermissionMaxOrMin(userGroupPermissions, reputationPermissions, userPermission, false,
                                    "MaxSizeOfOneAttachment");
            int maxSizeOfAllAttachments = GetOneItemPermissionMaxOrMin(userGroupPermissions, reputationPermissions, userPermission, false,
                                    "MaxSizeOfAllAttachments");
            int maxCountOfMessageSendOneDay = GetOneItemPermissionMaxOrMin(userGroupPermissions, reputationPermissions, userPermission,false,
                                    "MaxCountOfMessageSendOneDay");
            bool ifAllowSearch = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, userPermission, "IfAllowSearch");
            int minIntervalForSearch = GetOneItemPermissionMaxOrMin(userGroupPermissions, reputationPermissions, userPermission,true, "MinIntervalForSearch");

            /*from permission*/
            bool ifAllowViewForum = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, userPermission,"IfAllowViewForum");
            bool ifAllowViewTopic = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, userPermission,"IfAllowViewTopic");
            bool ifAllowPost = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, userPermission,"IfAllowPost");
            int minIntervalForPost = GetOneItemPermissionMaxOrMin(userGroupPermissions, reputationPermissions,userPermission, true, "MinIntervalForPost");
            int maxLengthOfPost = GetOneItemPermissionMaxOrMin(userGroupPermissions, reputationPermissions,userPermission, false, "MaxLengthOfPost");
            bool ifPostNotNeedModeration = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, userPermission,"IfPostNotNeedModeration");
            //bool ifAllowHTML = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, "IfAllowHTML");
            bool ifAllowUrl = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, userPermission,"IfAllowUrl");
            bool ifAllowUploadImage = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, userPermission,"IfAllowUploadImage");
            bool IfSignatureAllowInsertImage = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, userPermission,"IfSignatureAllowInsertImage");
            //bool IfSignatureAllowHTML = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, "IfSignatureAllowHTML");
            bool IfSignatureAllowUrl = GetOneItemPermissionIfAllow(userGroupPermissions, reputationPermissions, userPermission,"IfSignatureAllowUrl");


            return new UserPermissionCache(operatingUserOrOperator.Id, null,
                    operatingUserOrOperator.IfForumAdmin,
                    ifAllowCustomizeAvatar, maxLengthofSignature,
                    ifAllowUploadAttachment, maxCountOfAttacmentsForOnePost,
                    maxSizeOfOneAttachment, maxSizeOfAllAttachments,
                    maxCountOfMessageSendOneDay, ifAllowSearch,
                    minIntervalForSearch, ifAllowViewForum,
                    ifAllowViewTopic, ifAllowPost, minIntervalForPost,
                    maxLengthOfPost, ifPostNotNeedModeration, 
                    ifAllowUrl, ifAllowUploadImage,
                    IfSignatureAllowInsertImage,
                    IfSignatureAllowUrl);
        }
        #endregion
        #endregion

        public void IncreaseScore(int increasedValue)
        {
            if ((long)this._score + increasedValue >= int.MaxValue)
                increasedValue = int.MaxValue - this._score;
            UserAccess.UpdateUserOrOperatorScore(_conn, _transaction, _id, increasedValue);
        }

        public void DecreaseScore(int decreasedValue)
        {
            UserAccess.UpdateUserOrOperatorScore(_conn, _transaction, _id, -decreasedValue);
        }

        public void IncreaseReputation(int increasedValue)
        {
            if ((long)this._reputation + increasedValue >= int.MaxValue)
                increasedValue = int.MaxValue - this._reputation;
            UserAccess.UpdateUserOrOperatorReputation(_conn, _transaction, _id, increasedValue);
        }

        public void DecreaseReputation(int decreasedValue)
        {
            UserAccess.UpdateUserOrOperatorReputation(_conn, _transaction, _id, -decreasedValue);
        }

        protected PostsOfUserOrOperatorWithPermissionCheck GetPosts(UserOrOperator operatingUserOrOperator)
        {
            return new PostsOfUserOrOperatorWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, _id);
        }

        public abstract PostsOfUserOrOperatorWithPermissionCheck GetPosts();

        protected FavoritesWithPermissionCheck GetFavorites(UserOrOperator operatingUserOrOperator)
        {
            return new FavoritesWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, _id);
        }

        public abstract FavoritesWithPermissionCheck GetFavorites();

        protected SubscribesWithPermissionCheck GetSubscribes(UserOrOperator operatingUserOrOperator)
        {
            return new SubscribesWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, _id);
        }

        public abstract SubscribesWithPermissionCheck GetSubscribes();

        protected BansOfUserOrOperatorWithPermissionCheck GetBansOfUserOrOperator(UserOrOperator operatingUserOrOperator)
        {
            return new BansOfUserOrOperatorWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, _id);
        }

        public abstract BansOfUserOrOperatorWithPermissionCheck GetBansOfUserOrOperator();

        protected BansOfIPWithPermissionCheck GetBansOfIP(UserOrOperator operatingUserOrOperator)
        {
            return null;
        }

        public abstract BansOfIPWithPermissionCheck GetBansOfIP();

        protected InMessagesOfRecieverWithPermissionCheck GetInMessages(UserOrOperator operatingUserOrOperator)
        {
            return new InMessagesOfRecieverWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, _id);
        }

        public abstract InMessagesOfRecieverWithPermissionCheck GetInMessages();

        protected OutMessagesOfSenderWithPermissionCheck GetOutMessages(UserOrOperator operatingUserOrOperator)
        {
            return new OutMessagesOfSenderWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, _id);
        }

        public abstract OutMessagesOfSenderWithPermissionCheck GetOutMessages();

        protected bool IfBanById(UserOrOperator operatingUserOrOperator)
        {
            BansOfUserOrOperatorWithPermissionCheck tempBans = this.GetBansOfUserOrOperator(operatingUserOrOperator);
            BanUserOrOperatorWithPermissionCheck[] bans = tempBans.GetAllNotDeletedBans();
            DateTime now = DateTime.UtcNow;
            foreach (BanUserOrOperatorWithPermissionCheck ban in bans)
            {
                if (ban.BanStartDate <= now && now <= ban.BanEndDate)
                    return true;
            }
            return false;
        }

        public abstract bool IfBanById();

        protected void LiftBan(UserOrOperator operatingUserOrOperator)
        {
            UsersOrOperatorsOfSite usersOrOperators = new UsersOrOperatorsOfSite(_conn, _transaction);
            if (!usersOrOperators.IfUserOrOperatorExist(_id))
                ExceptionHelper.ThrowUserIdNotExist(_id);
            BansOfUserOrOperatorWithPermissionCheck tempBans = this.GetBansOfUserOrOperator(operatingUserOrOperator);
            tempBans.DeleteAll();
        }

        public void LiftBan(int forumId, UserOrOperator operatingUserOrOperator)
        {
            BansOfUserOrOperatorWithPermissionCheck tempBans = this.GetBansOfUserOrOperator(operatingUserOrOperator);
            tempBans.DeleteAll(forumId, operatingUserOrOperator);
        }

        public abstract void LiftBan();

        public virtual void SetActive()
        {
            UsersOrOperatorsOfSite usersOrOperators = new UsersOrOperatorsOfSite(_conn, _transaction);
            if (!usersOrOperators.IfUserOrOperatorExist(_id))
                ExceptionHelper.ThrowUserIdNotExist(_id);
            UserAccess.SetUserOrOperatorActiveStatus(_conn, _transaction, _id, true);
        }

        public virtual void SetInactive()
        {
            UsersOrOperatorsOfSite usersOrOperators = new UsersOrOperatorsOfSite(_conn, _transaction);
            if (!usersOrOperators.IfUserOrOperatorExist(_id))
                ExceptionHelper.ThrowUserIdNotExist(_id);
            UserAccess.SetUserOrOperatorActiveStatus(_conn, _transaction, _id, false);
        }

        public void SendMessage(int receiverId, string subject, string message, DateTime createDate)
        {
            
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, this);
            if (!forumFeature.IfEnableMessage)
                ExceptionHelper.ThrowForumSettingsCloseMessageFunction();
            //if (this.IfForumAdmin == false)
            //{
                
            //}



            UserOrOperator recciever = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, this, receiverId);
            if (recciever == null)
                ExceptionHelper.ThrowForumUserOrOperatorNotExistWithIdException(receiverId);
            if (recciever.IfDeleted)
                ExceptionHelper.ThrowForumUserOrOperatorNotExistWithNameException(recciever.DisplayName);
            
            OutMessagesOfSenderWithPermissionCheck outMessags = new OutMessagesOfSenderWithPermissionCheck(_conn, _transaction, this, _id);            
            int outMessageId = outMessags.Add(subject, message, createDate, receiverId); //insert out box,outMessageId
           
            InMessagesOfRecieverWithPermissionCheck inMessages = recciever.GetInMessages();
            inMessages.Add(subject, message, createDate, this._id);//insert in box
        }

        public bool IfModerator()
        {
            return ModeratorAccess.GetCountOfModeratorsByUserOrOperatorId(_conn, _transaction, _id) > 0;
        }

        public bool IfModerator(int forumId)
        {
            if (forumId <= 0)
                return IfModerator();
            else
                return ModeratorAccess.GetCountOfModeratorsByUserOrOperatorIdAnForumId(_conn, _transaction, _id, forumId) > 0;
        }

        public void UnsubscribeSingleTopic(string email, int topicId)
        {
            if (this._email != email)
                ExceptionHelper.ThrowForumUnSubscribeSingleTopicEmailMisMatch();
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, this);
            if (!forumFeature.IfEnableMessage)
                ExceptionHelper.ThrowForumSettingsCloseMessageFunction();
            SubscribesWithPermissionCheck subscribes = new SubscribesWithPermissionCheck(_conn, _transaction, this, _id);
            subscribes.Delete(topicId);
        }
       
    }
}
