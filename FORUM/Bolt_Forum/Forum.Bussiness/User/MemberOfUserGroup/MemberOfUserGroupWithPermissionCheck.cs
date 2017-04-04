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
    public class MemberOfUserGroupWithPermissionCheck : MemberOfUserGroup
    {
        UserOrOperator _operatingUserOrOperator;

        public MemberOfUserGroupWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int userOrOperatorId, int userGroupId)
            : base(conn, transaction, userOrOperatorId, userGroupId)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public MemberOfUserGroupWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator,
            int userOrOperatorId, int userGroupId, bool ifActive, bool ifDeleted,
            string email, string name, string password, string forgetPasswordGUIDTag, DateTime forgetPasswordTagTime, DateTime lastLoginTime, Int64 lastLoginIP,
            DateTime joinedTime, Int64 joinedIP, int numberOfPosts, bool ifShowEmail, string firstName, string lastName, bool ifShowUserName, Int16 age, bool ifShowAge,
            Int16 gender, bool ifShowGender, string occupation, bool ifShowOccupation, string company, bool ifShowCompany, string phoneNumber, bool ifShowPhoneNumber,
            string faxNumber, bool ifShowFaxNumber, string interests, bool ifShowInterests, string homePage, bool ifShowHomePage, string signature,
            bool ifCustomizeAvatar, byte[] customizeAvatar, string systemAvatar, bool ifForumAdmin, int score, int reputation)
            : base(conn, transaction, userOrOperatorId, userGroupId, ifActive, ifDeleted, email, name, password, forgetPasswordGUIDTag, forgetPasswordTagTime,
            lastLoginTime, lastLoginIP, joinedTime, joinedIP, numberOfPosts, ifShowEmail, firstName, lastName, ifShowUserName, age, ifShowAge, gender, ifShowGender,
            occupation, ifShowOccupation, company, ifShowCompany, phoneNumber, ifShowPhoneNumber, faxNumber, ifShowFaxNumber, interests, ifShowInterests,
            homePage, ifShowHomePage, signature, ifCustomizeAvatar, customizeAvatar, systemAvatar, ifForumAdmin, score, reputation)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
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

        private void CheckPermission()
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
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

        public override void Delete()
        {
            CheckPermission();
            base.Delete();
        }
    }
}
