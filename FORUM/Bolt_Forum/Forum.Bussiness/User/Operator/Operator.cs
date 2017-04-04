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
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.FieldLength;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class Operator : UserOrOperator
    {
        #region private fields
        private bool _ifAdmin;
        private string _description;
        #endregion

        #region properties
        public bool IfAdmin
        {
            get { return this._ifAdmin; }
        }
        public string Description
        {
            get { return this._description; }
        }
        #endregion

        public Operator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id, Int64 ip)
            : base(conn, transaction)
        {
            DataTable dt = UserAccess.GetOperatorById(_conn, _transaction, id);

            if (dt.Rows.Count == 0)
            {
                ExceptionHelper.ThrowOperatorNotExistException(id);
            }
            else
            {
                #region operator information init
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

                this._ifAdmin = Convert.ToBoolean(dt.Rows[0]["IfAdmin"]);
                this._description = Convert.ToString(dt.Rows[0]["Description"]);
                #endregion
                GetAvatar();
            }
        }

        public Operator(SqlConnectionWithSiteId conn, SqlTransaction transaction, string email, Int64 ip)
            : base(conn, transaction)
        {
            DataTable dt = UserAccess.GetOperatorByEmail(_conn, _transaction, email);

            if (dt.Rows.Count == 0)
            {
                ExceptionHelper.ThrowOperatorNotExistWithEmailException(email);
            }
            else
            {
                #region operator information init
                this._id = Convert.ToInt32(dt.Rows[0]["Id"]);
                this._ifActive = Convert.ToBoolean(dt.Rows[0]["IfActive"]);
                this._ifDeleted = Convert.ToBoolean(dt.Rows[0]["IfDeleted"]);
                this._email = email;
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

                this._ifAdmin = Convert.ToBoolean(dt.Rows[0]["IfAdmin"]);
                this._description = Convert.ToString(dt.Rows[0]["Description"]);
                #endregion
                GetAvatar();
            }
        }

        public Operator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id, bool ifActive, bool ifDeleted, string email, string name, string password, 
                string forgetPasswordGUIDTag, DateTime forgetPasswordTagTime, DateTime lastLoginTime, Int64 lastLoginIP, DateTime joinedTime, Int64 joinedIP, 
                int numberOfPosts, bool ifShowEmail, string firstName, string lastName, bool ifShowUserName, Int16 age, bool ifShowAge, Int16 gender, 
                bool ifShowGender, string occupation, bool ifShowOccupation, string company, bool ifShowCompany, string phoneNumber, bool ifShowPhoneNumber, 
                string faxNumber, bool ifShowFaxNumber, string interests, bool ifShowInterests, string homePage, bool ifShowHomePage, string signature, 
                bool ifCustomizeAvatar, byte[] customizeAvatar, string systemAvatar, bool ifForumAdmin, int score, int reputation, bool ifAdmin, string description, Int64 ip)
            :base(conn, transaction)
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
            this._ifAdmin = ifAdmin;
            this._description = description;
            this._ip = ip;
            #endregion
            GetAvatar();
        }

        public override void UpdateProfile(string email, string displayName, string firstName
            , string lastName, int age, EnumGender gender, string company, string occupation
            , string phone, string fax, string interests, string homepage,string score
            ,string reputation, bool ifShowEmail, bool ifShowUserName, bool ifShowAge
            , bool ifShowGender, bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone
            , bool ifShowFax, bool ifShowInterests, bool ifShowHomePage)
        {
            if (IfAdmin)
            {
                base.UpdateProfile(email, displayName, firstName, lastName, age, gender
                    , company, occupation, phone, fax, interests, homepage, score, reputation
                    , ifShowEmail, ifShowUserName, ifShowAge, ifShowGender, ifShowOccupation
                    , ifShowCompany, ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage);
                //UserAccess.UpdateOperatorProfile(_conn, _transaction, _id, firstName, lastName, age,
                //                                      gender, company, occupation, phone, fax, interests, homepage,score,reputation, ifShowEmail,
                //                                      ifShowUserName, ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany,
                //                                      ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage);
            }
            else
            {
                score = _score.ToString();
                reputation = _reputation.ToString();
                base.UpdateProfile(email, displayName, firstName, lastName, age, gender
                  , company, occupation, phone, fax, interests, homepage, score, reputation
                  , ifShowEmail, ifShowUserName, ifShowAge, ifShowGender, ifShowOccupation
                  , ifShowCompany, ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage);
            }
       }

        public override abstract PostsOfUserOrOperatorWithPermissionCheck GetPosts();

        public override abstract FavoritesWithPermissionCheck GetFavorites();

        public override abstract SubscribesWithPermissionCheck GetSubscribes();

        public override abstract BansOfIPWithPermissionCheck GetBansOfIP();

        public override abstract BansOfUserOrOperatorWithPermissionCheck GetBansOfUserOrOperator();

        public override abstract InMessagesOfRecieverWithPermissionCheck GetInMessages();

        public override abstract OutMessagesOfSenderWithPermissionCheck GetOutMessages();

        public virtual void UpdateEmailVerificationGUIDTag(string emailVerificationGUIDTag)
        {
            UserAccess.UpdateUserOrOperatorEmailVerificationGUIDTag(_conn, _transaction, _id, emailVerificationGUIDTag);
        }

        public override abstract bool IfBanById();

        public override abstract void LiftBan();

        public virtual void SendMessage(List<int> userGroupIds, List<int> reputationGroupIds, List<int> receiverIds,
              bool ifAdminGroup, bool ifModeratorGroup,
              string subject, string message, DateTime sendDate, UserOrOperator operatingUserOrOperator)
        {
            List<int> GroupIds = new List<int>();
            List<int> ReputationGroupIds = new List<int>();
            bool ifAllRegisterUser = false;
            if (userGroupIds != null)
                foreach (int groupId in userGroupIds)
                {
                    if (GroupAccess.IfAllRegisterUserGroup(_conn, _transaction, groupId) == true)
                    {
                        ifAllRegisterUser = true;
                    }
                    else
                    {
                        GroupIds.Add(groupId);
                    }
                }
            if (reputationGroupIds != null)
                ReputationGroupIds.AddRange(reputationGroupIds);
           
            OutMessagesOfSenderWithPermissionCheck outMessags = new OutMessagesOfSenderWithPermissionCheck(_conn, _transaction, this, _id);
            int outMessageId = outMessags.Add(subject, message, sendDate, userGroupIds, reputationGroupIds, receiverIds);
            InMessageAccess.AddInMessage(_conn, _transaction, subject, message, sendDate, _id, receiverIds.ToArray<int>(),
                GroupIds.ToArray<int>(), ReputationGroupIds.ToArray<int>(), ifAdminGroup, ifModeratorGroup, ifAllRegisterUser);
        }
    }
}
