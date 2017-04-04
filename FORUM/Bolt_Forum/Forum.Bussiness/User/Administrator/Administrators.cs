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
    public abstract class Administrators
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        public Administrators(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            this._conn = conn;
            this._transaction = transaction;
        }

        public virtual void Add(int userOrOperatorId)
        {
            UserAccess.AddAdministrator( _conn, _transaction, userOrOperatorId);
        }

        protected void Delete(int userOrOperatorId, UserOrOperator operatingUserOrOperator)
        {
            if (userOrOperatorId == operatingUserOrOperator.Id)
                ExceptionHelper.ThrowForumUserCannotDeleteHimselfException();
            if (UserAccess.GetCountOfNotDeletedUsersOrOperatorsWhichisAdministrator(_conn, _transaction) <= 1)
                ExceptionHelper.ThrowUserOnlyOneAdministartor();
            AdministratorWithPermissionCheck admin = new AdministratorWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, userOrOperatorId);
            admin.Delete();
        }

        protected AdministratorWithPermissionCheck[] GetAllAdministrators(string orderField, string order, UserOrOperator operatingUserOrOperator)
        {
            DataTable table = UserAccess.GetAllAdministrators(orderField, order, _conn);

            AdministratorWithPermissionCheck[] administrators = new AdministratorWithPermissionCheck[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow r = table.Rows[i];

                administrators[i] = CreateAdministratorObject(table.Rows[i], operatingUserOrOperator);
            }

            return administrators;
        }

        protected AdministratorWithPermissionCheck[] GetAllAdministrators(UserOrOperator operatingUserOrOperator)
        {
            DataTable dt = UserAccess.GetAllAdministrators(_conn);
            List<AdministratorWithPermissionCheck> administrators = new List<AdministratorWithPermissionCheck>();
            foreach (DataRow dr in dt.Rows)
            {
                administrators.Add(CreateAdministratorObject(dr,operatingUserOrOperator));
            }
            return administrators.ToArray<AdministratorWithPermissionCheck>();
        }

        #region Private Functiion CreateAdministratorObject
        private AdministratorWithPermissionCheck CreateAdministratorObject(DataRow dr, UserOrOperator operatingUserOrOperator)
        {
            int id = Convert.ToInt32(dr["Id"]);
            bool ifActive = Convert.ToBoolean(dr["IfActive"]);
            bool ifDeleted = Convert.ToBoolean(dr["IfDeleted"]);
            string email = Convert.ToString(dr["Email"]);
            string name = Convert.ToString(dr["Name"]);
            string password = Convert.ToString(dr["Password"]).Trim();
            string forgetPasswordGUIDTag = Convert.ToString(dr["ForgetPasswordGUIDTag"]);
            DateTime forgetPasswordTagTime = Convert.ToDateTime(dr["ForgetPasswordTagTime"]);
            DateTime lastLoginTime = Convert.ToDateTime(dr["LastLoginTime"]);
            long lastLoginIP = Convert.ToInt64(dr["LastLoginIP"]);
            DateTime joinedTime = Convert.ToDateTime(dr["JoinedTime"]);
            long joinedIP = Convert.ToInt64(dr["JoinedIP"]);
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
            Int16 userType = Convert.ToInt16(dr["UserType"]);

            AdministratorWithPermissionCheck administrator = new AdministratorWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, id, ifActive, ifDeleted, email, name,
                password, forgetPasswordGUIDTag, forgetPasswordTagTime, joinedTime, joinedIP, lastLoginTime, lastLoginIP, numberOfPosts, ifShowEmail, firstName, lastName,
                ifShowUserName, age, ifShowAge, gender, ifShowGender, occupation, ifShowOccupation, company, ifShowCompany, phoneNumber, ifShowPhoneNumber, faxNumber,
                ifShowFaxNumber, interests, ifShowInterests, homePage, ifShowHomePage, signature, ifCustomizeAvatar, customizeAvatar, systemAvatar, ifForumAdmin, score, reputation,
                userType);
            return administrator;
        }
        #endregion Private Functiion CreateAdministratorObject

    }
}
