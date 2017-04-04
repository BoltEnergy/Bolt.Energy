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

namespace Com.Comm100.Forum.Bussiness
{
    public class ModeratorWithPermissionCheck : Moderator
    {
        UserOrOperator _operatingUserOrOperator;

        public ModeratorWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int userOrOperatorId, int forumId)
            : base(conn, transaction, userOrOperatorId, forumId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public ModeratorWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator,
                int forumId, int id, string email, string name, string password,
                string description, bool ifAdmin, bool ifDeleted, bool ifActive, string forgetPasswordGUIDTag, DateTime forgetPasswordTagTime,
                string emailVerificationGUIDTag, Int16 moderateStatus, Int16 emailVerificationStatus, int numberOfPosts, DateTime joinedTime,
                Int64 joinedIP, DateTime lastLoginTime, Int64 lastLoginIP, bool ifShowEmail, string firstName, string lastName, bool ifShowUserName,
                Int16 age, bool ifShowAge, Int16 gender, bool ifShowGender, string occupation, bool ifShowOccupation, string company,
                bool ifShowCompany, string phoneNumber, bool ifShowPhoneNumber, string faxNumber, bool ifShowFaxNumber, string interests,
                bool ifShowInterests, string homePage, bool ifShowHomePage, string signature, bool ifCustomizeAvatar, string systemAvatar, byte[] customizeAvatar
                ,bool ifForumAdmin, int score, int reputation
            )
            : base(conn, transaction, forumId, id, email, name, password, description, ifAdmin, ifDeleted, ifActive, forgetPasswordGUIDTag, forgetPasswordTagTime, emailVerificationGUIDTag,
            moderateStatus, emailVerificationStatus, numberOfPosts, joinedTime, joinedIP, lastLoginTime, lastLoginIP, ifShowEmail, firstName, lastName, ifShowUserName, age,
            ifShowAge, gender, ifShowGender, occupation, ifShowOccupation, company, ifShowCompany, phoneNumber, ifShowPhoneNumber, faxNumber, ifShowFaxNumber,
            interests, ifShowInterests, homePage, ifShowHomePage, signature, ifCustomizeAvatar, systemAvatar, customizeAvatar
            ,ifForumAdmin,score,reputation
            )
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public override void UpdateSignature(string signature)
        {
            CommFun.CommonEditSignaturePermissionWithImageOrLinkCheck(ref signature, _operatingUserOrOperator);
            CommFun.UserPermissionCache().CheckMaxLengthofSignature(_operatingUserOrOperator, signature.Length);
            base.UpdateSignature(signature);
        }

        public override void UpdateAvatar(byte[] avatar)
        {
            CommFun.UserPermissionCache().CheckIfAllowCustomizeAvatarPermission(_operatingUserOrOperator);
            base.UpdateAvatar(avatar);
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
            CheckPermission();
            base.LiftBan(_operatingUserOrOperator);
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
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
        }
    }
}
