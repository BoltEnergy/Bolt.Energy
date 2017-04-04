#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;

namespace Com.Comm100.Forum.Process
{
    public class AdministratorProcess
    {
        public static void AddUserOrOperatorToAdministrator(int siteId, int operatingUserOrOperatorId, int userOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);

                AdministratorsWithPermissionCheck admins = new AdministratorsWithPermissionCheck(conn, transaction, operatingUserOrOperator);

                admins.Add(userOrOperatorId);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void DeleteUserOrOpratorFromAdministrators(int siteId, int operatingUserOrOperatorId, int userOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);

                AdministratorsWithPermissionCheck administrators = new AdministratorsWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                administrators.Delete(userOrOperatorId);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }

        }

        public static AdministratorWithPermissionCheck[] GetAllAdministrators(int siteId, string orderField, string order,int userOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);

                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, userOrOperatorId);

                AdministratorsWithPermissionCheck administrators = new AdministratorsWithPermissionCheck(conn, null, operatingUserOrOperator);

                return administrators.GetAllAdministrators(orderField, order);
            }
            catch
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static UserOrOperator[] GetNotDeletedAndNotBannedUsersOrOperatorsWhichisNotAdministratorByQueryAndPaging(int siteId, int operatingUserOrOperatorId, 
            int pageIndex, int pageSize, string emailOrDisplayNameKeyword, EnumUserType userType, bool ifGetAll, bool ifGetAdmin, out int recordsCount, string orderField, string orderDirection)
        {
            SqlConnectionWithSiteId conn = null;
            try
            {
                emailOrDisplayNameKeyword = emailOrDisplayNameKeyword.Trim();
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                UsersOrOperatorsOfSite usersOrOperators = new UsersOrOperatorsOfSite(conn, null);
                return usersOrOperators.GetNotDeletedAndNotBannedUsersOrOperatorsWhichisNotAdministratorByQueryAndPaging(pageIndex, pageSize, 
                    emailOrDisplayNameKeyword, userType, ifGetAll, ifGetAdmin, out recordsCount, operatingUserOrOperator, orderField, orderDirection);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
    }
}
