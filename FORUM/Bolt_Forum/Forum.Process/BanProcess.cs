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
using Com.Comm100.Framework.Database;
using System.Data.SqlClient;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Bussiness;

namespace Com.Comm100.Forum.Process
{
    public class BanProcess
    {
        public static BanBase[] GetBansByQueryAndPaging(int operatingUserOrOperatorId, int siteId, int pageIndex, int pageSize, EnumBanType type, long ip, string name, string orderField, string orderDirection)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                BansOfSiteWithPermissionCheck bansOfSite = new BansOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return bansOfSite.GetBansByQueryAndPaging(operatingUserOrOperator, pageIndex, pageSize, type, ip, name, orderField, orderDirection);
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

        public static int GetCountOfBansByQuery(int operatingUserOrOperatorId, int siteId, EnumBanType type, long ip, string name)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                BansOfSiteWithPermissionCheck bansOfSite = new BansOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return bansOfSite.GetCountOfBansByQuery(type, ip, name);
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

        public static int GetCountOfBanByIP(int operatingUserOrOperatorId, int siteId, EnumBanType type, long ip)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                BansOfSiteWithPermissionCheck bansOfSite = new BansOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return bansOfSite.GetCountOfBanByIP(type, ip);
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


        public static void DeleteBan(int operatingUserOrOperatorId, int siteId,int banId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                BanBase ban = BanFactory.GetBanById(conn, transaction, banId, operatingUserOrOperator);
                ban.Delete();
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

        public static void AddBan(int operatingUserOrOperatorId, int siteId, EnumBanType type,DateTime startDate,DateTime endDate,string note, string banUserOrOperatorName,long startIP,long endIP)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                BansOfSiteWithPermissionCheck bansOfSite = new BansOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                int banId;
                if (type == EnumBanType.User)
                {
                    UserOrOperator banUserOrOperator = UserOrOperatorFactory.CreateUserOrOperatorByName(conn, transaction, null, banUserOrOperatorName);
                   
                    banId = bansOfSite.Add(startDate, endDate, note, operatingUserOrOperatorId, banUserOrOperator.Id);
                }
                else if (type == EnumBanType.IP)
                {
                    banId = bansOfSite.Add(startDate, endDate, note, operatingUserOrOperatorId, startIP, endIP);
                }
               
                transaction.Commit();
            }
            catch (Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void AddBan(int siteId, int operatingUserOrOperatorId, DateTime startDate, DateTime endDate, string note, int banUserOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction  = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                BansOfSiteWithPermissionCheck bansOfSite = new BansOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                bansOfSite.Add(startDate, endDate, note, operatingUserOrOperatorId, banUserOperatorId);

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

        public static void AddBanInUI(int siteId, int operatingUserOrOperatorId,int forumId, DateTime startDate, DateTime endDate, string note, int banUserOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                BansOfSiteWithPermissionCheck bansOfSite = new BansOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                bansOfSite.AddBanUserInUI(forumId,startDate, endDate, note, operatingUserOrOperatorId, banUserOperatorId);

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

        public static BanBase GetBanById(int operatingUserOrOperatorId, int siteId, int banId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                return BanFactory.GetBanById(conn, transaction, banId, operatingUserOrOperator);
                
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

        public static BanBase GetBanByUserOrOperatorId(int operatingUserOrOperatorId, int siteId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                return BanFactory.GetBanByUserOrOperatorId(conn, transaction, operatingUserOrOperator);

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

        public static BanBase GetBanByIP(int operatingUserOrOperatorId, int siteId, long ip)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                return BanFactory.GetBanByIP(conn, transaction, operatingUserOrOperator, ip);

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

        public static void UpdateBan(int operatingUserOrOperatorId, int siteId, int banId,EnumBanType type,DateTime startDate,DateTime endDate,string note,int banUserOrOperatorId,long startIP,long endIP)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                BanBase ban=BanFactory.GetBanById(conn,transaction,banId,operatingUserOrOperator);
                if (type == EnumBanType.User)
                {
                    ban.Update<int>(new int[] { banUserOrOperatorId }, startDate, endDate, note, operatingUserOrOperatorId);
                }
                else if (type == EnumBanType.IP)
                {
                    ban.Update<long>(new long[] { startIP, endIP }, startDate, endDate, note, operatingUserOrOperatorId);
                }
                
                transaction.Commit();
            }
            catch (Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }

        }

        public static void LiftUserOrOperatorBan(int siteId, int operatingUserOrOperatorId, int userOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transactiion = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transactiion = conn.SqlConn.BeginTransaction();
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transactiion, null, operatingUserOrOperatorId);
                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transactiion, operatingUserOrOperator, userOrOperatorId);
                userOrOperator.LiftBan();
                transactiion.Commit();
            }
            catch (Exception)
            {
                DbHelper.RollbackTransaction(transactiion);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void LiftUserOrOperatorBan(int siteId, int operatingUserOrOperatorId, int forumId,int userOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transactiion = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transactiion = conn.SqlConn.BeginTransaction();
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transactiion, null, operatingUserOrOperatorId);
                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transactiion, operatingUserOrOperator, userOrOperatorId);
                userOrOperator.LiftBan(forumId, operatingUserOrOperator);
                transactiion.Commit();
            }
            catch (Exception)
            {
                DbHelper.RollbackTransaction(transactiion);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
    }
}
    
