#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using Com.Comm100.Framework.Common;
using System.Data.SqlClient;
using Com.Comm100.Framework.Database;
using System;

namespace Com.Comm100.Forum.Bussiness
{
    public class OperatorWithPermissionCheck : Operator
    {
        protected UserOrOperator _operatingOperator;

        public OperatorWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id, UserOrOperator operatingOperator, Int64 ip)
            : base(conn, transaction, id, ip)
        {
            _operatingOperator = operatingOperator;
        }

        public OperatorWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, string email, UserOrOperator operatingOperator, Int64 ip)
            : base(conn, transaction, email, ip)
        {
            _operatingOperator = operatingOperator;
        }

        public OperatorWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id, UserOrOperator operatingOperator,
                bool ifActive, bool ifDeleted, string email, string name, string password,
                string forgetPasswordGUIDTag, DateTime forgetPasswordTagTime, DateTime lastLoginTime, Int64 lastLoginIP, DateTime joinedTime, Int64 joinedIP,
                int numberOfPosts, bool ifShowEmail, string firstName, string lastName, bool ifShowUserName, Int16 age, bool ifShowAge, Int16 gender,
                bool ifShowGender, string occupation, bool ifShowOccupation, string company, bool ifShowCompany, string phoneNumber, bool ifShowPhoneNumber,
                string faxNumber, bool ifShowFaxNumber, string interests, bool ifShowInterests, string homePage, bool ifShowHomePage, string signature,
                bool ifCustomizeAvatar, byte[] customizeAvatar, string systemAvatar, bool ifForumAdmin, int score, int reputation, bool ifAdmin, string description, Int64 ip)
            : base(conn, transaction, id, ifActive, ifDeleted, email, name, password, forgetPasswordGUIDTag, forgetPasswordTagTime, lastLoginTime, lastLoginIP,
            joinedTime, joinedIP, numberOfPosts, ifShowEmail, firstName, lastName, ifShowUserName, age, ifShowAge, gender, ifShowGender, occupation,
            ifShowOccupation, company, ifShowCompany, phoneNumber, ifShowPhoneNumber, faxNumber, ifShowFaxNumber, interests, ifShowInterests,
            homePage, ifShowHomePage, signature, ifCustomizeAvatar, customizeAvatar, systemAvatar, ifForumAdmin, score, reputation,
            ifAdmin, description, ip)
        {
            _operatingOperator = operatingOperator;
        }

        public override void UpdateSignature(string signature)
        {
            CommFun.CommonEditSignaturePermissionWithImageOrLinkCheck(ref signature,this);
            CommFun.UserPermissionCache().CheckMaxLengthofSignature(this, signature.Length);
            base.UpdateSignature(signature);
        }


        public override void UpdateProfile(string email, string displayName, string firstName, string lastName, int age, Com.Comm100.Framework.Enum.EnumGender gender, string company, string occupation, string phone, string fax, string interests, string homepage, string score, string reputation, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender, bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests, bool ifShowHomePage)
        {
            //CommFun.UserPermissionCache().
            base.UpdateProfile(email, displayName, firstName, lastName, age, gender, company, occupation, phone, fax, interests, homepage, score, reputation, ifShowEmail, ifShowUserName, ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany, ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage);
        }


        public override void UpdateAvatar(byte[] avatar)
        {
            CommFun.UserPermissionCache().CheckIfAllowCustomizeAvatarPermission(_operatingOperator);
            base.UpdateAvatar(avatar);
        }

        public override PostsOfUserOrOperatorWithPermissionCheck GetPosts()
        {
            return base.GetPosts(_operatingOperator);
        }

        public override FavoritesWithPermissionCheck GetFavorites()
        {
            return base.GetFavorites(_operatingOperator);
        }

        public override SubscribesWithPermissionCheck GetSubscribes()
        {
            return base.GetSubscribes(_operatingOperator);
        }

        public override BansOfIPWithPermissionCheck GetBansOfIP()
        {
            return base.GetBansOfIP(_operatingOperator);
        }

        public override BansOfUserOrOperatorWithPermissionCheck GetBansOfUserOrOperator()
        {
            return base.GetBansOfUserOrOperator(_operatingOperator);
        }

        public override void SendMessage(System.Collections.Generic.List<int> userGroupIds, System.Collections.Generic.List<int> reputationGroupIds, System.Collections.Generic.List<int> receiverIds, bool ifAdminGroup, bool ifModeratorGroup, string subject, string message, DateTime sendDate, UserOrOperator operatingUserOrOperator)
        {
            if (operatingUserOrOperator.IfDeleted)
            {
                ExceptionHelper.ThrowUserHasBeenDeletedWithIdException(_operatingOperator.Id);
            }
            else if (!operatingUserOrOperator.IfActive)
            {
                ExceptionHelper.ThrowUserNotActiveWithIdException(_operatingOperator.Id);
            }
            else
            {
                ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, operatingUserOrOperator);
                if (forumFeature.IfEnableMessage)
                {
                    base.SendMessage(userGroupIds, reputationGroupIds, receiverIds, ifAdminGroup, ifModeratorGroup, subject, message, sendDate, operatingUserOrOperator);

                }
                else
                    ExceptionHelper.ThrowForumSettingsCloseMessageFunction();
            }
        }

        public override InMessagesOfRecieverWithPermissionCheck GetInMessages()
        {
            return base.GetInMessages(_operatingOperator);
        }

        public override OutMessagesOfSenderWithPermissionCheck GetOutMessages()
        {
            return base.GetOutMessages(_operatingOperator);
        }

        public override bool IfBanById()
        {
            return base.IfBanById(_operatingOperator);
        }

        public override void LiftBan()
        {
            CheckPermission();
            base.LiftBan(_operatingOperator);
        }

        public override void SetActive()
        {
            CheckPermission();
            base.SetActive();
        }

        public override void SetInactive()
        {
            CheckPermission();
            base.SetInactive();
        }
        private void CheckPermission()
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingOperator);
        }

        public override void UpdateEmailVerificationGUIDTag(string emailVerificationGUIDTag)
        {
            base.UpdateEmailVerificationGUIDTag(emailVerificationGUIDTag);
        }
    }
}
