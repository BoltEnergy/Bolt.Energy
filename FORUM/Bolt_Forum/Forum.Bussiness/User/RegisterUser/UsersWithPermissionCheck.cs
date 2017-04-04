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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Enum;

namespace Com.Comm100.Forum.Bussiness
{
    public class UsersWithPermissionCheck : Users
    {
        UserOrOperator _operatingUserOrOperator;

        public UsersWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public override UserWithPermissionCheck[] GetAllUserNotDelete(UserOrOperator operatingUserOrOperator)
        {
            return base.GetAllUserNotDelete(operatingUserOrOperator);
        }

        public  UserWithPermissionCheck[] GetNotDeletedUsersByQueryAndPaging(int pageIndex, int pageSize, string strOrder, string EmailOrdisplayNameKeyWord, out int count)
        {
            return base.GetNotDeletedUsersByQueryAndPaging(pageIndex, pageSize, strOrder, EmailOrdisplayNameKeyWord, _operatingUserOrOperator, out count);
        }

        public override void Delete(int userId)
        {
            base.Delete(userId);
        }

        public override int GetCountOfNotDeletedUsersByDisplayName(string displayName)
        {
            return base.GetCountOfNotDeletedUsersByDisplayName(displayName);
        }

        public override int GetCountOfNotDeletedUsersByEmail(string email)
        {
            return base.GetCountOfNotDeletedUsersByEmail(email);
        }

        public override int GetCountOfNotModeratedUsersBySearch(string displayNameKeyWord)
        {
            return base.GetCountOfNotModeratedUsersBySearch(displayNameKeyWord);
        }

        public override UserWithPermissionCheck[] GetNotModeratedUsersByPaging(int pageIndex, int pageSize, string strOrder, string displayNameKeyWord, UserOrOperator operatingUserOrOperator)
        {
            return base.GetNotModeratedUsersByPaging(pageIndex, pageSize, strOrder, displayNameKeyWord, operatingUserOrOperator);
        }

        public override UserWithPermissionCheck[] GetUsersNotEmailVerfyByQueryAndPaging(int pageIndex, int pageSize, string orderField, string orderDirection, string emailOrDispalyNameKeyword, UserOrOperator operatingUserOrOperator, out int count)
        {
            return base.GetUsersNotEmailVerfyByQueryAndPaging(pageIndex, pageSize, orderField, orderDirection, emailOrDispalyNameKeyword, operatingUserOrOperator, out count);
        }

        public UserWithPermissionCheck GetNotDeletedUserById(int userId)
        {
            return base.GetNotDeletedUserById(userId, _operatingUserOrOperator);
        }
        //This function used in admin control panel.
        public int Add(string email, string displayName, string password, string firstName, string lastName, int age,
            EnumGender gender, string company, string occupation, string phone, string fax, string interests,
            string homepage, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender,
            bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests,
            bool ifShowHomePage, List<int> userGroupIds)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            return base.Add(email, displayName, password, firstName, lastName, age, gender, company, occupation, phone, fax, interests, homepage, ifShowEmail, ifShowUserName,
                ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany, ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage, userGroupIds, _operatingUserOrOperator);
        }

        public override int Add(string email, string displayName, string password, string firstName, string lastName, int age, 
            EnumGender gender, string company, string occupation, string phone, string fax, string interests, 
            string homepage, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender, 
            bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests, 
            bool ifShowHomePage)
        {
            //CheckAddPermission();
            return base.Add(email, displayName, password, firstName, lastName, age, gender, company, occupation, phone, fax, interests, homepage, ifShowEmail, ifShowUserName, ifShowAge, ifShowGender, ifShowOccupation, ifShowCompany, ifShowPhone, ifShowFax, ifShowInterests, ifShowHomePage);
        }
    }
}
