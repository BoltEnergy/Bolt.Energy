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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Database;
using System.Data.SqlClient;
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.Process
{
    public class AbuseProcess
    {
        public static AbuseWithPermissionCheck[] GetAbusesByQueryAndPagingOfSite(int operatingUserOrOperatorId, int siteId,
           string keyword, EnumAbuseStatus Status, bool ifAllStatus, int pageIndex, int pageSize,
            string orderField, string orderMethod,out int count)

        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                    conn, transaction, null, operatingUserOrOperatorId);
                AbusesOfSiteWithPermissionCheck abuseOfSite = new AbusesOfSiteWithPermissionCheck(
                    conn, transaction, operatingUserOrOperator);
                AbuseWithPermissionCheck[] abuses = abuseOfSite.GetAbusesByQueryAndPaging(keyword, Status,
                    ifAllStatus,pageIndex, pageSize, orderField, orderMethod,out count);
                return abuses;
            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static AbuseWithPermissionCheck[] GetAbusesByModeratorWithQueryAndPaging(int operatingUserOrOperatorId, int siteId,
            string keyword, EnumAbuseStatus status, bool ifAllStatus, int pageIndex, int pageSize, string orderField, string orderMethod)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                    conn, transaction, null, operatingUserOrOperatorId);
                AbusesOfSiteWithPermissionCheck abuseOfSite = new AbusesOfSiteWithPermissionCheck(
                    conn, transaction, operatingUserOrOperator);
                AbuseWithPermissionCheck[] abuses = abuseOfSite.GetAbusesByModeratorWithQueryAndPaging(operatingUserOrOperatorId,keyword, status,
                    ifAllStatus, pageIndex, pageSize, orderField, orderMethod);
                return abuses;
            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static int GetCountOfAbusesByModeratorWithQuery(int operatingUserOrOperatorId, int siteId,
            string keyword, EnumAbuseStatus status, bool ifAllStatus)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                AbusesOfSiteWithPermissionCheck abusesOfSite = new AbusesOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return abusesOfSite.GetCountOfAbusesByModeratorWithQuery(operatingUserOrOperatorId, keyword, status, ifAllStatus);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static int AddAbuseOfPost(int operatingUserOrOperatorId, int siteId,int forumId,
           int postId,int abuseUserOrOperatorId, string note)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            DateTime abuseDate = DateTime.UtcNow;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                    conn, transaction, null, operatingUserOrOperatorId);
                AbusesOfPostWithPermissionCheck abusesOfPost = new AbusesOfPostWithPermissionCheck(
                    conn, transaction, operatingUserOrOperator, postId);
                return abusesOfPost.Add(forumId,abuseUserOrOperatorId, note, abuseDate);
            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static bool IfPostAbused(int operatingUserOrOperatorId, int siteId,
          int postId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                    conn, transaction, null, operatingUserOrOperatorId);
                PostWithPermissionCheck post = new PostWithPermissionCheck(conn, transaction, postId, operatingUserOrOperator);
                return post.IfAbused();
            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static EnumPostAbuseStatus GetPostAbusedStuats(int operatingUserOrOperatorId, int siteId,
          int postId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                    conn, transaction, null, operatingUserOrOperatorId);
                PostWithPermissionCheck post = new PostWithPermissionCheck(conn, transaction, postId, operatingUserOrOperator);
                return post.GetAbuseStatus();
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static EnumPostAbuseStatus GetPostAbusedStuatsOfUser(int operatingUserOrOperatorId, int siteId,
         int postId,int userId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                    conn, transaction, null, operatingUserOrOperatorId);
                PostWithPermissionCheck post = new PostWithPermissionCheck(conn, transaction, postId, operatingUserOrOperator);
                return post.GetAbuseStatusOfUser(userId);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void ApproveAbsesOfPost(int operatingUserOrOperatorId, int siteId,int forumId,
         int postId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                    conn, transaction, null, operatingUserOrOperatorId);
                AbusesOfPostWithPermissionCheck abusesOfPost = new AbusesOfPostWithPermissionCheck(
                    conn, transaction, operatingUserOrOperator, postId);
                abusesOfPost.ApproveAubsesOfPost(forumId);
            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void RefuseAbusesOfPost(int operatingUserOrOperatorId, int siteId,int forumId,
        int postId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                    conn, transaction, null, operatingUserOrOperatorId);
                AbusesOfPostWithPermissionCheck abusesOfPost = new AbusesOfPostWithPermissionCheck(
                    conn, transaction, operatingUserOrOperator, postId);
                abusesOfPost.RefuseAbusesOfPost(forumId);
            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
    }
}
