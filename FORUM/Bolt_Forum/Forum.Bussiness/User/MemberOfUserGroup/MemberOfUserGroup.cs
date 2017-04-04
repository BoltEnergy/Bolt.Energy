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
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class MemberOfUserGroup : UserOrOperator
    {
        private int _userGroupId;
        public int UserGroupId
        {
            get { return this._userGroupId; }
        }

        public MemberOfUserGroup(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, int userGroupId)
            :base(conn, transaction)
        {
            DataTable dt = MemberOfUserGroupAccess.GetNotDeletedMemberByUserOrOperatorIdAndUserGroupId(conn, transaction, userOrOperatorId, userGroupId); 
            if (dt.Rows.Count == 0)
            {
                ExceptionHelper.ThrowForumMemberOfGroupNotExistWithUserIdAndGroupId(userOrOperatorId, userGroupId);
            }
            else
            {
                #region Member Information Init
                this._id = userOrOperatorId;
                this._userGroupId = userGroupId;
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
                #endregion
                //get Avatar's filepath
                GetAvatar();
            }
        }

        public MemberOfUserGroup(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, int userGroupId, bool ifActive, bool ifDeleted,
            string email, string name, string password, string forgetPasswordGUIDTag, DateTime forgetPasswordTagTime, DateTime lastLoginTime, Int64 lastLoginIP,
            DateTime joinedTime, Int64 joinedIP, int numberOfPosts, bool ifShowEmail, string firstName, string lastName, bool ifShowUserName, Int16 age, bool ifShowAge,
            Int16 gender, bool ifShowGender, string occupation, bool ifShowOccupation, string company, bool ifShowCompany, string phoneNumber, bool ifShowPhoneNumber,
            string faxNumber, bool ifShowFaxNumber, string interests, bool ifShowInterests, string homePage, bool ifShowHomePage, string signature,
            bool ifCustomizeAvatar, byte[] customizeAvatar, string systemAvatar, bool ifForumAdmin, int score, int reputation)
            : base(conn, transaction)
        {
            #region Member Information Init
            this._id = userOrOperatorId;
            this._userGroupId = userGroupId;
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
            #endregion Member Information Init
        }

        public virtual void Delete()
        {
            if (this._ifDeleted)
                ExceptionHelper.ThrowForumUserOrOperatorNotExistWithNameException(_name);
            MemberOfUserGroupAccess.DeleteMember(_conn, _transaction, _id, _userGroupId);
        }

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
