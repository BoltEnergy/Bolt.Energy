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
    public abstract class MembersOfUserGroup
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        private int _userGroupId;
        public int UserGroupId
        {
            get { return this._userGroupId; }
        }

        public MembersOfUserGroup(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userGroupId)
        {
            this._conn = conn;
            this._transaction = transaction;
            this._userGroupId = userGroupId;
        }

        protected void Delete(int userOrOperatorId, UserOrOperator operatingUserOrOperator)
        {
            MemberOfUserGroupWithPermissionCheck member = new MemberOfUserGroupWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, userOrOperatorId, _userGroupId);
            member.Delete();
        }

        public void DeleteAll()
        {
            MemberOfUserGroupAccess.DeleteMembersByUserGroupId(_conn, _transaction, _userGroupId);
        }

        public virtual void Add(int userOrOperatorId)
        {
            UsersOrOperatorsOfSite usersOrOperators = new UsersOrOperatorsOfSite(_conn, _transaction);
            if (!usersOrOperators.IfUserOrOperatorExist(userOrOperatorId)) 
                ExceptionHelper.ThrowUserIdNotExist(userOrOperatorId);

            UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, null, userOrOperatorId);
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, userOrOperator);
            if(!forumFeature.IfEnableGroupPermission)
                ExceptionHelper.ThrowForumSettingsCloseGroupPermissionFunction();
            MemberOfUserGroupAccess.AddMember(_conn, _transaction, userOrOperatorId, _userGroupId);
        }

        public int GetCount()
        {
            return MemberOfUserGroupAccess.GetCountGroupMember(_conn, _transaction, _userGroupId);
        }

        protected MemberOfUserGroupWithPermissionCheck[] GetMembersByQueryAndPaging(int pageIndex, int pageSize, string emailOrDisplayNameKeyword, string orderField, out int recordCount, UserOrOperator operatingUserOrOperator)
        {
            DataTable table = MemberOfUserGroupAccess.GetMembersByQueryAndPaging(_conn, _transaction, _userGroupId, emailOrDisplayNameKeyword, pageIndex, pageSize, orderField);
            MemberOfUserGroupWithPermissionCheck[] members = new MemberOfUserGroupWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                members[i] = CreateMemberObject(table.Rows[i], operatingUserOrOperator);
            }
            recordCount = MemberOfUserGroupAccess.GetCountOfMembersByQuery(_conn, _transaction, _userGroupId, emailOrDisplayNameKeyword);
            return members;
        }

        private MemberOfUserGroupWithPermissionCheck CreateMemberObject(DataRow dr, UserOrOperator operatingUserOrOperator)
        {
            #region Init User Infor
            int id = Convert.ToInt32(dr["Id"]);
            int groupId = Convert.ToInt32(dr["GroupId"]);
            bool ifActive = Convert.ToBoolean(dr["IfActive"]);
            bool ifDeleted = Convert.ToBoolean(dr["IfDeleted"]);
            string email = Convert.ToString(dr["Email"]);
            string name = Convert.ToString(dr["Name"]);
            string password = Convert.ToString(dr["Password"]);
            string forgetPasswordGUIDTag = Convert.ToString(dr["ForgetPasswordGUIDTag"]);
            DateTime forgetPasswordTagTime = Convert.ToDateTime(dr["ForgetPasswordTagTime"]);
            DateTime lastLoginTime = Convert.ToDateTime(dr["LastLoginTime"]);
            Int64 lastLoginIP = Convert.ToInt64(dr["LastLoginIP"]);
            DateTime joinedTime = Convert.ToDateTime(dr["JoinedTime"]);
            Int64 joinedIP = Convert.ToInt64(dr["JoinedIP"]);
            int numberOfPosts = dr["Posts"] is DBNull ? 0 : Convert.ToInt32(dr["Posts"]);
            bool ifShowEmail = Convert.ToBoolean(dr["IfShowEmail"]);
            string firstName = Convert.ToString(dr["FirstName"]);
            string lastName = Convert.ToString(dr["LastName"]);
            bool ifShowUserName = Convert.ToBoolean(dr["IfShowUserName"]);
            Int16 age = Convert.ToInt16(dr["Age"]);
            bool ifShowAge = Convert.ToBoolean(dr["IfShowAge"]);
            Int16 gender = Convert.ToInt16(dr["Gender"]);
            bool ifShowGender = Convert.ToBoolean(dr["IfShowGender"]);
            string occupation = Convert.ToString(dr["Occupation"]);
            bool ifShowOccupation = Convert.ToBoolean(dr["IfShowOccupation"]);
            string company = Convert.ToString(dr["Company"]);
            bool ifShowCompany = Convert.ToBoolean(dr["IfShowCompany"]);
            string phoneNumber = Convert.ToString(dr["PhoneNumber"]);
            bool ifShowPhoneNumber = Convert.ToBoolean(dr["IfShowPhoneNumber"]);
            string faxNumber = Convert.ToString(dr["FaxNumber"]);
            bool ifShowFaxNumber = Convert.ToBoolean(dr["IfShowFaxNumber"]);
            string interests = Convert.ToString(dr["Interests"]);
            bool ifShowInterests = Convert.ToBoolean(dr["IfShowInterests"]);
            string homePage = Convert.ToString(dr["HomePage"]);
            bool ifShowHomePage = Convert.ToBoolean(dr["IfShowHomePage"]);
            string signature = Convert.ToString(dr["Signature"]);
            bool ifCustomizeAvatar = Convert.ToBoolean(dr["IfCustomizeAvatar"]);
            byte[] customizeAvatar = null;
            if (!Convert.IsDBNull(dr["CustomizeAvatar"])) customizeAvatar = (byte[])dr["CustomizeAvatar"];
            string systemAvatar = Convert.ToString(dr["SystemAvatar"]);
            bool ifForumAdmin = Convert.ToBoolean(dr["IfForumAdmin"]);
            int score = Convert.ToInt32(dr["ForumScore"]);
            int reputation = Convert.ToInt32(dr["ForumReputation"]);
            #endregion
            return new MemberOfUserGroupWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, id, groupId, ifActive, ifDeleted, email, name, password,
                forgetPasswordGUIDTag, forgetPasswordTagTime, lastLoginTime, lastLoginIP, joinedTime, joinedIP, numberOfPosts, ifShowEmail, firstName, lastName,
                ifShowUserName, age, ifShowAge, gender, ifShowGender, occupation, ifShowOccupation, company, ifShowCompany, phoneNumber, ifShowPhoneNumber,
                faxNumber, ifShowFaxNumber, interests, ifShowInterests, homePage, ifShowHomePage, signature, ifCustomizeAvatar, customizeAvatar, systemAvatar,
                ifForumAdmin, score, reputation);
        }
    }
}
