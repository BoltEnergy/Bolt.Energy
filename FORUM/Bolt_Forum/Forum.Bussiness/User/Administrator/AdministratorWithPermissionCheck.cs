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
    public class AdministratorWithPermissionCheck : Administrator
    {
        UserOrOperator _operatingUserOrOperator;

        public AdministratorWithPermissionCheck(
            SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator,
            int userOrOperatorId,
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
            bool ifForumAdmin, int score, int reputation, Int16 userType)
            : base(conn, transaction, userOrOperatorId, ifActive, ifDelete, email, name, password, forgetPasswordGuidTag, forgetPasswordTagTime, joinedTime,
            joinedIP, lastLoginTime, lastLoginIP, numberOfPosts, ifShowEmail, firstName, lastName, ifShowUserName, age, ifShowAge, gender, ifShowGender, occupation,
            ifShowOccupation, company, ifShowCompany, phoneNumber, ifShowPhoneNumber, faxNumber, ifShowFaxNumber, interests, ifShowInterests, homePage,
            ifShowHomePage, signature, ifCustomizeAvatar, customizeAvatar, systemAvatar,ifForumAdmin, score, reputation, userType)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }
            

        public AdministratorWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, 
            UserOrOperator operatingUserOrOperator, int userOrOperatorId)
            : base(conn, transaction, userOrOperatorId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }


        public override void UpdateAvatar(byte[] avatar)
        {
            base.UpdateAvatar(avatar);
        }

        public override void UpdateSignature(string signature)
        {
           //CommFun.CommonEditSignaturePermissionWithImageOrLinkCheck(signature,
            base.UpdateSignature(signature);
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
        public override void SendMessage(List<int> userGroupIds, List<int> reputationGroupIds, List<int> receiverIds,
            bool ifAdminGroup, bool ifModeratorGroup,
            string subject, string message, DateTime sendDate, UserOrOperator operatingUserOrOperator)
        {
            if (_operatingUserOrOperator.IfDeleted)
            {
                ExceptionHelper.ThrowUserHasBeenDeletedWithIdException(_operatingUserOrOperator.Id);
            }
            else if (!_operatingUserOrOperator.IfActive)
            {
                ExceptionHelper.ThrowUserNotActiveWithIdException(_operatingUserOrOperator.Id);
            }
            else
            {
                ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, _operatingUserOrOperator);
                if (forumFeature.IfEnableMessage)
                {
                    base.SendMessage(userGroupIds, reputationGroupIds, receiverIds, ifAdminGroup, ifModeratorGroup, subject, message, sendDate, operatingUserOrOperator);
                }
                else
                    ExceptionHelper.ThrowForumSettingsCloseMessageFunction();
            }
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
