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
using System.IO;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Common;
using System.Collections;
using System.Reflection;

namespace Com.Comm100.Forum.Bussiness
{
    public class UsersOrOperatorsOfSite : UsersOrOperatorsBase
    {
        public UsersOrOperatorsOfSite(SqlConnectionWithSiteId conn, SqlTransaction transaction)
            : base(conn, transaction)
        {
        }

        public UserOrOperator Login(string email, string password, string loginIp, bool ifAdmin, bool ifModerator, out UserPermissionCache userPermissionList, Boolean matchPwd = true)
        {
            password = Encrypt.EncryptPassword(password.Trim());
            long ip = IpHelper.DottedIP2LongIP(loginIp);

            UserOrOperator userOrOperator = null;
            //checking for authentication 
            userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(_conn, null, null, email);

            if (!matchPwd)
                 password = userOrOperator.Password.Trim() ;

            #region Check User Or Operator
            if (userOrOperator.IfDeleted || userOrOperator.Password.Trim() != password)
            {
                ExceptionHelper.ThrowForumUserNotExistWithSiteIdAndEmailAndPWDException();
            }
            else if (!userOrOperator.IfActive)
            {
                ExceptionHelper.ThrowForumUserOrOperatorNotActiveWithEmailException(email);
            }
            #endregion

            if (CommFun.IfUser(userOrOperator))
            {
                UserWithPermissionCheck user = userOrOperator as UserWithPermissionCheck;
                #region Check User
                if (user.EmailVerificationStatus == EnumUserEmailVerificationStatus.enumNotVerified)
                {
                    ExceptionHelper.ThrowUserNotEmailVerificatedException();
                }
                else if (user.ModerateStatus == EnumUserModerateStatus.enumNotModerated)
                {
                    ExceptionHelper.ThrowUserNotModeratedException();
                }
                else if (user.ModerateStatus == EnumUserModerateStatus.enumRefused)
                {
                    ExceptionHelper.ThrowUserNotModeratedException();
                }
                else
                {
                    if (ifAdmin)
                    {
                        if (!user.IfForumAdmin)
                            ExceptionHelper.ThrowForumOnlyAdministratorsHavePermissionException();
                    }
                }
                //else if (!user.IfForumAdmin)
                //{
                //    ExceptionHelper.ThrowForumOnlyAdministratorsHavePermissionException();
                //}
                #endregion
            }
            #region check ifModerator
            if (ifModerator)
                if (!userOrOperator.IfModerator())
                {
                    ExceptionHelper.ThrowForumOnlyModeratorsHavePermissionException();
                    //ExceptionHelper.ThrowForumOnlyModeratorsOrAdminstratorsHavePermissionException();
                }
            #endregion
            userOrOperator.UpdateLastLoginIp(ip);
            userOrOperator.UpdateLastLoginTimeToCurrentTime();

            userOrOperator.GetUserPermissionCache(out userPermissionList);

            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
               _conn, _transaction, userOrOperator, _conn.SiteId);
            scoreStrategySetting.UseAfterLogin(userOrOperator);
            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                _conn, _transaction, userOrOperator, _conn.SiteId);
            reputationStrategySetting.UseAfterLogin(userOrOperator);
            return userOrOperator;
        }

        public bool IfUserOrOperatorExist(int userOrOperatorId)
        {
            return UserAccess.GetCountOfNotDeletedUserOrOperatorById(_conn, _transaction, userOrOperatorId) > 0;
        }

        public UserOrOperator[] GetNotDeletedAndNotBannedUsersOrOperatorsWhichisNotAdministratorByQueryAndPaging(int pageIndex, int pageSize, string emailOrDisplayNameKeyword, EnumUserType userType, bool ifGetAll, bool ifGetAdmin, out int recordsCount, UserOrOperator operatingUserOrOperator, string orderField, string orderDirection)
        {
            StringBuilder userTypeCondition = new StringBuilder("");
            #region User Type Condition
            if (ifGetAll)
            {
                userTypeCondition.Append(string.Format(" and UserType<>{0}", Convert.ToInt16(EnumUserType.Contact)));
            }
            else
            {
                if (ifGetAdmin)
                {
                    userTypeCondition.Append(string.Format(" and UserType={0} and IfAdmin=1", Convert.ToInt16(EnumUserType.Operator)));
                }
                else
                {
                    userTypeCondition.Append(string.Format(" and UserType={0} and IfAdmin=0", Convert.ToInt16(userType)));
                }
            }
            #endregion User Type Condition

            string orderCondition = orderField + " " + orderDirection;
            DataTable table = UserAccess.GetNotDeletedAndNotBannedUsersOrOperatorsWhichisNotAdministratorByQueryAndPaging(_conn, _transaction, pageIndex, pageSize, emailOrDisplayNameKeyword, userTypeCondition.ToString(), orderCondition);

            UserOrOperator[] userOrOperators = new UserOrOperator[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                userOrOperators[i] = CreateUserOrOperatorObject(table.Rows[i], operatingUserOrOperator);
            }

            recordsCount = UserAccess.GetCountOfNotDeletedAndNotBannedUsersOrOperatorsWhichisNotAdministratorByQuery(_conn, _transaction, emailOrDisplayNameKeyword, userTypeCondition.ToString());

            return userOrOperators;
        }

        public UserOrOperator[] GetNotDeletedAndNotBannedUsersOrOperatorsWhichisNotInUserGroupByQueryAndPaging(int userGroupId, int pageIndex, int pageSize, string emailOrDisplayNameKeyword, EnumUserType userType, bool ifGetAll, bool ifGetAdmin, out int recordsCount, UserOrOperator operatingUserOrOperator, string orderField, string orderDirection)
        {
            StringBuilder userTypeCondition = new StringBuilder("");
            #region User Type Condition
            if (ifGetAll)
            {
                userTypeCondition.Append(string.Format(" and UserType<>{0}", Convert.ToInt16(EnumUserType.Contact)));
            }
            else
            {
                if (ifGetAdmin)
                {
                    userTypeCondition.Append(string.Format(" and UserType={0} and IfAdmin=1", Convert.ToInt16(EnumUserType.Operator)));
                }
                else
                {
                    userTypeCondition.Append(string.Format(" and UserType={0} and IfAdmin=0", Convert.ToInt16(userType)));
                }
            }
            #endregion User Type Condition
            string orderCondition = orderField + " " + orderDirection;
            DataTable table = UserAccess.GetNotDeletedAndNotBannedUsersOrOperatorsWhichisNotInUserGroupByQueryAndPaging(_conn, _transaction, userGroupId, pageIndex, pageSize, emailOrDisplayNameKeyword, userTypeCondition.ToString(), orderCondition);

            UserOrOperator[] userOrOperators = new UserOrOperator[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                userOrOperators[i] = CreateUserOrOperatorObject(table.Rows[i], operatingUserOrOperator);
            }

            recordsCount = UserAccess.GetCountOfNotDeletedAndNotBannedUsersOrOperatorsWhichisNotInUserGroupByQuery(_conn, _transaction, userGroupId, emailOrDisplayNameKeyword, userTypeCondition.ToString());
            return userOrOperators;
        }

        public UserOrOperator[] GetNotDeletedAndNotBannedUserOrOperatorsByQueryAndPaging(int pageIndex, int pageSize, string emailOrDisplayNameKeyword, EnumUserType userType, bool ifGetAll, bool ifGetAdmin, out int recordsCount, UserOrOperator operatingUserOrOperator, string orderField, string orderDirection)
        {
            StringBuilder userTypeCondition = new StringBuilder("");
            #region User Type Condition
            if (ifGetAll)
            {
                userTypeCondition.Append(string.Format(" and UserType<>{0}", Convert.ToInt16(EnumUserType.Contact)));
            }
            else
            {
                if (ifGetAdmin)
                {
                    userTypeCondition.Append(string.Format(" and UserType={0} and IfAdmin=1", Convert.ToInt16(EnumUserType.Operator)));
                }
                else
                {
                    userTypeCondition.Append(string.Format(" and UserType={0} and IfAdmin=0", Convert.ToInt16(userType)));
                }
            }
            #endregion User Type Condition
            string orderCondition = orderField + " " + orderDirection;
            DataTable table = UserAccess.GetNotDeletedAndNotBannedUserOrOperatorsByQueryAndPaging(_conn, _transaction, pageIndex, pageSize, emailOrDisplayNameKeyword, userTypeCondition.ToString(), orderCondition);

            UserOrOperator[] userOrOperators = new UserOrOperator[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                userOrOperators[i] = CreateUserOrOperatorObject(table.Rows[i], operatingUserOrOperator);
            }

            recordsCount = UserAccess.GetCountOfNotDeletedAndNotBannedUserOrOperatorsByQuery(_conn, _transaction, emailOrDisplayNameKeyword, userTypeCondition.ToString());
            return userOrOperators;
        }

        public UserOrOperator[] GetNotDeletedAndNotBannedUserOrOperatorsByQueryNameAndPaging(int pageIndex, int pageSize, string displayNameKeyword, EnumUserType userType, bool ifGetAll, bool ifGetAdmin, out int recordsCount, UserOrOperator operatingUserOrOperator, string orderField, string orderDirection)
        {
            StringBuilder userTypeCondition = new StringBuilder("");
            #region User Type Condition
            if (ifGetAll)
            {
                userTypeCondition.Append(string.Format(" and UserType<>{0}", Convert.ToInt16(EnumUserType.Contact)));
            }
            else
            {
                if (ifGetAdmin)
                {
                    userTypeCondition.Append(string.Format(" and UserType={0} and IfAdmin=1", Convert.ToInt16(EnumUserType.Operator)));
                }
                else
                {
                    userTypeCondition.Append(string.Format(" and UserType={0} and IfAdmin=0", Convert.ToInt16(userType)));
                }
            }
            #endregion User Type Condition
            string orderCondition = orderField + " " + orderDirection;
            DataTable table = UserAccess.GetNotDeletedAndNotBannedUserOrOperatorsByQueryNameAndPaging(_conn, _transaction, pageIndex, pageSize, displayNameKeyword, userTypeCondition.ToString(), orderCondition);

            UserOrOperator[] userOrOperators = new UserOrOperator[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                userOrOperators[i] = CreateUserOrOperatorObject(table.Rows[i], operatingUserOrOperator);
            }

            recordsCount = UserAccess.GetCountOfNotDeletedAndNotBannedUserOrOperatorsByQueryName(_conn, _transaction, displayNameKeyword, userTypeCondition.ToString());
            return userOrOperators;
        }

        public UserOrOperator[] GetUserOrOperatorByQueryAndPaging(int pageIndex, int pageSize, string emailOrDisplayNameKeyword, EnumUserType userType, bool ifGetAll, bool ifGetAdmin, string orderField, string orderDirection, out int recordsCount, UserOrOperator operatingUserOrOperator)
        {
            StringBuilder userTypeCondition = new StringBuilder("");
            #region User Type Condition
            if (ifGetAll)
            {
                userTypeCondition.Append(string.Format(" and UserType<>{0}", Convert.ToInt16(EnumUserType.Contact)));
            }
            else
            {
                if (ifGetAdmin)
                {
                    userTypeCondition.Append(string.Format(" and UserType={0} and IfAdmin=1", Convert.ToInt16(EnumUserType.Operator)));
                }
                else if ((Int16)userType == 1)
                {
                    userTypeCondition.Append(string.Format(" and UserType={0} and IfAdmin=0", Convert.ToInt16(userType)));
                }
                else
                {
                    userTypeCondition.Append(string.Format(" and UserType={0}", Convert.ToInt16(userType)));
                }

            }
            #endregion User Type Condition
            string orderCondition = orderField + " " + orderDirection;
            DataTable table = UserAccess.GetUserOrOperatorsByQueryAndPaging(_conn, _transaction, pageIndex, pageSize, emailOrDisplayNameKeyword, userTypeCondition.ToString(), orderCondition);
            UserOrOperator[] userOrOperators = new UserOrOperator[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                userOrOperators[i] = CreateUserOrOperatorObject(table.Rows[i], operatingUserOrOperator);
            }
            recordsCount = UserAccess.GetCountOfUserOrOperatorsByQuery(_conn, _transaction, emailOrDisplayNameKeyword, userTypeCondition.ToString());
            return userOrOperators;
        }

    }
}
