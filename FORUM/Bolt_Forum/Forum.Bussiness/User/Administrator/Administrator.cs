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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class Administrator : UserOrOperator
    {
        private Int16 _userType;
        public EnumUserType UseType
        {
            get { return (EnumUserType)this._userType; }
        }

        public Administrator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId,
            bool ifActive, bool ifDelete, string email, string name, string password,
            string forgetPasswordGuidTag, DateTime forgetPasswordTagTime, DateTime joinedTime,
            Int64 joinedIP, DateTime lastLoginTime, Int64 lastLoginIP,
            int numberOfPosts, bool ifShowEmail, string firstName, string lastName,
            bool ifShowUserName, Int16 age, bool ifShowAge, short gender,
            bool ifShowGender, string occupation, bool ifShowOccupation, string company,
            bool ifShowCompany, string phoneNumber, bool ifShowPhoneNumber,
            string faxNumber, bool ifShowFaxNumber, string interests,
            bool ifShowInterests, string homePage, bool ifShowHomePage, string signature, 
            bool ifCustomizeAvatar, byte[] customizeAvatar, string systemAvatar,
            bool ifForumAdmin, int score, int reputation,Int16 userType)
            : base(conn, transaction)
        {
            #region Init fileds
            base._id = userOrOperatorId;
            base._ifActive = ifActive;
            base._ifDeleted = ifDelete;
            base._email = email;
            base._name = name;
            base._password = password;
            base._forgetPasswordGUIDTag = forgetPasswordGuidTag;
            base._forgetPasswordTagTime = forgetPasswordTagTime;
            base._joinedIP = joinedIP;
            base._joinedTime = joinedTime;
            base._lastLoginIP = lastLoginIP;
            base._lastLoginTime = lastLoginTime;
            base._numberOfPosts = numberOfPosts;
            base._ifShowEmail = ifShowEmail;
            base._firstName = firstName;
            base._lastName = lastName;
            base._ifShowUserName = ifShowUserName;
            base._age = age;
            base._ifShowAge = ifShowAge;
            base._gender = gender;
            base._ifShowGender = ifShowGender;
            base._occupation = occupation;
            base._ifShowOccupation = ifShowOccupation;
            base._company = company;
            base._ifShowCompany = ifShowCompany;
            base._phoneNumber = phoneNumber;
            base._ifShowPhoneNumber = ifShowPhoneNumber;
            base._faxNumber = faxNumber;
            base._ifShowFaxNumber = ifShowFaxNumber;
            base._interests = interests;
            base._ifShowInterests = ifShowInterests;
            base._homePage = homePage;
            base._ifShowHomePage = ifShowHomePage;
            base._signature = signature;
            base. _ifCustomizeAvatar = ifCustomizeAvatar;
            base._customizeAvatar = customizeAvatar;
            base._systemAvatar = systemAvatar;
            base._ifForumAdmin = ifForumAdmin;
            base._score = score;
            base._reputation = reputation;
            this._userType = userType;
            #endregion
        }

        public Administrator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
            : base(conn, transaction)
        {
            DataTable dt = UserAccess.GetAdministratorById(conn, transaction, userOrOperatorId);
            if (dt.Rows.Count == 0)
            {
                ExceptionHelper.ThrowForumAdministratorNotExistWithId(userOrOperatorId);
            }
            else
            {
                #region UserInformation Init
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
                this._userType = Convert.ToInt16(dt.Rows[0]["UserType"]);
                #endregion
                //get Avatar's filepath
                GetAvatar();
            }
        }

        

        //private void CheckDelete()
        //{
        //    if (UserAccess.GetCountOfNotDeletedUsersOrOperatorsWhichisAdministrator(_conn, _transaction) <= 1) ;            
        //        //ExceptionHelper.ThrowUserOnlyOneAdministartor();  
        //}

        public void Delete()
        {         
            UserAccess.DeleteAdministrator(this._id, _conn, _transaction);
        }

        public virtual void SendMessage(List<int> userGroupIds, List<int> reputationGroupIds, List<int> receiverIds,
            bool ifAdminGroup, bool ifModeratorGroup,
            string subject, string message, DateTime sendDate, UserOrOperator operatingUserOrOperator)
        {
            List<int> GroupIds = new List<int>();
            List<int> ReputationGroupIds = new List<int>();
            bool ifAllRegisterUser = false;
            if(userGroupIds != null)
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
            if(reputationGroupIds != null)
                ReputationGroupIds.AddRange(reputationGroupIds);
            
            OutMessagesOfSenderWithPermissionCheck outMessags = new OutMessagesOfSenderWithPermissionCheck(_conn, _transaction, this, _id);
            int outMessageId = outMessags.Add(subject, message, sendDate, userGroupIds, reputationGroupIds, receiverIds);
            InMessageAccess.AddInMessage(_conn, _transaction, subject, message, sendDate, _id, receiverIds.ToArray<int>(),
                GroupIds.ToArray<int>(), ReputationGroupIds.ToArray<int>(), ifAdminGroup, ifModeratorGroup, ifAllRegisterUser);
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
