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
    public abstract class Moderator : UserOrOperator
    {
        private int _forumId;
        public int ForumId
        {
            get { return _forumId; }
        }

        public Moderator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, int forumId)
            : base(conn, transaction)
        {
            DataTable dt = ModeratorAccess.GetModeratorByUserOrOperatorIdAndForumId(conn, transaction, forumId, userOrOperatorId);
            if (dt.Rows.Count == 0)
            {
                ExceptionHelper.ThrowForumModeratorOfForumNotExistWithUserIdAndForumId(userOrOperatorId, forumId);
            }
            else
            {
                #region UserInformation Init
                this._forumId = forumId;
                this._id = userOrOperatorId;
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
                //this._ip = ip;
                #endregion
                //get Avatar's filepath
                GetAvatar();
            }
        }

        public Moderator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int id, string email, string name, string password,
                string description, bool ifAdmin, bool ifDeleted, bool ifActive, string forgetPasswordGUIDTag, DateTime forgetPasswordTagTime,
                string emailVerificationGUIDTag, Int16 moderateStatus, Int16 emailVerificationStatus, int numberOfPosts, DateTime joinedTime,
                Int64 joinedIP, DateTime lastLoginTime, Int64 lastLoginIP, bool ifShowEmail, string firstName, string lastName, bool ifShowUserName,
                Int16 age, bool ifShowAge, Int16 gender, bool ifShowGender, string occupation, bool ifShowOccupation, string company,
                bool ifShowCompany, string phoneNumber, bool ifShowPhoneNumber, string faxNumber, bool ifShowFaxNumber, string interests,
                bool ifShowInterests, string homePage, bool ifShowHomePage, string signature, bool ifCustomizeAvatar, string systemAvatar, byte[] customizeAvatar
                ,bool ifForumAdmin,int score,int reputation
            )
            : base(conn,transaction)
        {
            #region User Information Init
            _forumId = forumId;
            _id = id;
            _ifActive=ifActive;
            _ifDeleted=ifDeleted;
            _email=email;
            _name=name;
            _password=password;
            _forgetPasswordGUIDTag=forgetPasswordGUIDTag;
            _forgetPasswordTagTime=forgetPasswordTagTime;

            _lastLoginTime=lastLoginTime;
            _lastLoginIP=lastLoginIP;
            _joinedTime=joinedTime;
            _joinedIP=joinedIP;

            _numberOfPosts=numberOfPosts;
            _ifShowEmail=ifShowEmail;
            _firstName=firstName;
            _lastName=lastName;
            _ifShowUserName=ifShowUserName;
            _age=age;
            _ifShowAge=ifShowAge;
            _gender=gender;
            _ifShowGender=ifShowGender;
            _occupation=occupation;
            _ifShowOccupation=ifShowOccupation;
            _company=company;
            _ifShowCompany=ifShowCompany;
            _phoneNumber=phoneNumber;
            _ifShowPhoneNumber=ifShowPhoneNumber;
            _faxNumber=faxNumber;
            _ifShowFaxNumber=ifShowFaxNumber;
            _interests=interests;
            _ifShowInterests=ifShowInterests;
            _homePage=homePage;
            _ifShowHomePage=ifShowHomePage;
            _signature=signature;
            _ifCustomizeAvatar=ifCustomizeAvatar;
            _customizeAvatar=customizeAvatar;
            _systemAvatar=systemAvatar;
            _avatar=Avatar;
            /*-------------------2.0-------------------*/
            _ifForumAdmin = ifForumAdmin;
            _score = score;
            _reputation = reputation;
            #endregion
        }

        public static void Delete(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, int userOrOperatorId)
        {
            ModeratorAccess.Delete(conn, transaction, forumId, userOrOperatorId);
        }

        /*----------------------------2.0------------------------------------*/

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
