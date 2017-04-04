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
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum;

namespace Com.Comm100.Forum.Bussiness
{
    public class UserWithPermissionCheck : User
    {
        UserOrOperator _operatingUserOrOperator;

        public UserWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id, Int64 ip, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction, id, ip)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public UserWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, string email, Int64 ip, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction, email, ip)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public UserWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id, UserOrOperator operatingUserOrOperator,
                bool ifActive, bool ifDeleted, string email, string name, string password,
                string forgetPasswordGUIDTag, DateTime forgetPasswordTagTime, DateTime lastLoginTime, Int64 lastLoginIP, DateTime joinedTime, Int64 joinedIP,
                int numberOfPosts, bool ifShowEmail, string firstName, string lastName, bool ifShowUserName, Int16 age, bool ifShowAge, Int16 gender,
                bool ifShowGender, string occupation, bool ifShowOccupation, string company, bool ifShowCompany, string phoneNumber, bool ifShowPhoneNumber,
                string faxNumber, bool ifShowFaxNumber, string interests, bool ifShowInterests, string homePage, bool ifShowHomePage, string signature,
                bool ifCustomizeAvatar, byte[] customizeAvatar, string systemAvatar, bool ifForumAdmin, int score, int reputation, 
            string emailVerificationGUIDTag, bool ifVerified, Int16 moderateStatus, Int16 emailVerificationStatus, Int64 ip)
            : base(conn, transaction, id, ifActive, ifDeleted, email, name, password, forgetPasswordGUIDTag, forgetPasswordTagTime, lastLoginTime,
            lastLoginIP, joinedTime, joinedIP, numberOfPosts, ifShowEmail, firstName, lastName, ifShowUserName, age, ifShowAge, gender, ifShowGender,
            occupation, ifShowOccupation, company, ifShowCompany, phoneNumber, ifShowPhoneNumber, faxNumber, ifShowFaxNumber, interests,
            ifShowInterests, homePage, ifShowHomePage, signature, ifCustomizeAvatar, customizeAvatar, systemAvatar, ifForumAdmin, score, reputation,
            emailVerificationGUIDTag, ifVerified, moderateStatus, emailVerificationStatus, ip)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public override void Delete()
        {
            CheckAdminPermission();
            base.Delete();
        }

        public override void IncreaseNumberPostsByOne()
        {
            base.IncreaseNumberPostsByOne();
        }
        //9.22 new add
        public override void DecreaseAutorPostsNumberByOne()
        {
            base.DecreaseAutorPostsNumberByOne();
        }


        public override void SetEmailVerificationPassing(string emailVerificationGUIDTag)
        {
            base.SetEmailVerificationPassing(emailVerificationGUIDTag);
        }

        public override void UpdateEmailVerificationGUIDTag(string emailVerificationGUIDTag)
        {
            base.UpdateEmailVerificationGUIDTag(emailVerificationGUIDTag);
        }

        public override void UpdateLastLoginTimeToCurrentTime()
        {
            base.UpdateLastLoginTimeToCurrentTime();
        }

        #region user moderator
        public override void UpdateModerateStatus(Com.Comm100.Framework.Enum.Forum.EnumUserModerateStatus moderateStatus)
        {
            CheckAdminPermission();
            base.UpdateModerateStatus(moderateStatus);
        }

        public override void ApproveRegistration()
        {
            CheckAdminPermission();
            base.ApproveRegistration();
        }

        public override void RefuseRegistration()
        {
            CheckAdminPermission();
            base.RefuseRegistration();
        }
        #endregion user moderator

        public override void UpdateProfile(string email, string displayName, string firstName, string lastName, int age, Com.Comm100.Framework.Enum.EnumGender gender, string company, string occupation, string phone, string fax, string interests, string homepage,string score,string reputation, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender, bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests, bool ifShowHomePage)
        {

            base.UpdateProfile(email, displayName, firstName, lastName, age, gender, company, occupation, phone, fax, interests, homepage,score,reputation, ifShowEmail, ifShowUserName, ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany, ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage);
        }

        public override void UpdateSignature(string signature)
        {
            CommFun.CheckCommonPermissionInUI(this);
            CommFun.CommonEditSignaturePermissionWithImageOrLinkCheck(ref signature, this);
           
            CommFun.UserPermissionCache().CheckMaxLengthofSignature(this,signature.Replace("<p>", "").Replace("</p>", "").Replace("<!-- alert(66666); // -->", "").Length);
            base.UpdateSignature(signature);
        }

        public override void UpdateAvatar(byte[] avatar)
        {
            CommFun.UserPermissionCache().CheckIfAllowCustomizeAvatarPermission(_operatingUserOrOperator);
            base.UpdateAvatar(avatar);
        }

        private void CheckUpdatePermission(int score, int reputation)
        {
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, this);

            CheckAdminPermission();
            if (!forumFeature.IfEnableScore)
            {
                if (this.Score != score)
                    ExceptionHelper.ThrowForumSettingsCloseScoreFunctio();
            }
            if (!forumFeature.IfEnableReputation)
            {
                if (this.Reputation != reputation)
                    ExceptionHelper.ThrowForumSettingsCloseReputationFunctio();
            }
        }

        public void Update(string email, string displayName, string firstName, string lastName, int age,
            EnumGender gender, string company, string occupation, string phone, string fax, string interests,
            string homepage, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender,
            bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests,
            bool ifShowHomePage, List<int> userGroupIds, int score, int reputation)
        {
            CheckUpdatePermission(score, reputation);
            base.Update(email, displayName, firstName, lastName, age, gender, company, occupation, phone, fax, interests, homepage, ifShowEmail, ifShowUserName,
                ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany, ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage, userGroupIds, score, reputation, _operatingUserOrOperator);
        }

        public override void Update(string email, string displayName, string firstName, string lastName, int age, EnumGender gender, string company, string occupation, string phone, string fax, string interests, string homepage, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender, bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests, bool ifShowHomePage, int score, int reputation)
        {
            CheckUpdatePermission(score, reputation);
            base.Update(email, displayName, firstName, lastName, age, gender, company, occupation, phone, fax, interests, homepage, ifShowEmail, ifShowUserName, ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany, ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage, score, reputation);
        }

        /*---------------------------------2.0------------------------------------*/

        public override UserGroupsOfUserOrOperatorWithPermissionCheck GetUserGroups()
        {
            return base.GetUserGroups(_operatingUserOrOperator);
        }

        public override UserReputationGroupWithPermissionCheck GetUserReputationGroup()
        {
            return base.GetUserReputationGroup(_operatingUserOrOperator);
        }

        public override PostsOfUserOrOperatorWithPermissionCheck GetPosts()
        {
            return base.GetPosts(_operatingUserOrOperator);
        }

        public override FavoritesWithPermissionCheck GetFavorites()
        {
            return base.GetFavorites(_operatingUserOrOperator);
        }

        public override SubscribesWithPermissionCheck GetSubscribes()
        {
            return base.GetSubscribes(_operatingUserOrOperator);
        }

        public override BansOfIPWithPermissionCheck GetBansOfIP()
        {
            return base.GetBansOfIP(_operatingUserOrOperator);
        }

        public override BansOfUserOrOperatorWithPermissionCheck GetBansOfUserOrOperator()
        {
            return base.GetBansOfUserOrOperator(_operatingUserOrOperator);
        }

        public override InMessagesOfRecieverWithPermissionCheck GetInMessages()
        {
            return base.GetInMessages(_operatingUserOrOperator);
        }

        public override OutMessagesOfSenderWithPermissionCheck GetOutMessages()
        {
            return base.GetOutMessages(_operatingUserOrOperator);
        }

        public override bool IfBanById()
        {
            return base.IfBanById(_operatingUserOrOperator);
        }

        public override void LiftBan()
        {
            CheckAdminPermission();
            base.LiftBan(_operatingUserOrOperator);
        }

        public override void SetActive()
        {
            CheckAdminPermission();
            base.SetActive();
        }

        public override void SetInactive()
        {
            CheckAdminPermission();
            base.SetInactive();
        }

        private void CheckAdminPermission()
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
        }
    }
}
