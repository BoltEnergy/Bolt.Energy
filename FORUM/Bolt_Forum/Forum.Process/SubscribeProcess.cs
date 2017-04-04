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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
namespace Com.Comm100.Forum.Process
{
    public class SubscribeProcess
    {
        public static SubscribeWithPermissionCheck[] GetSubscribeByQueryAndPaging(int siteId, int operatingUserOrOperatorId, out int count, int pageIndex, int pageSize, string keyword)
        {
            SqlConnectionWithSiteId conn = null;       
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                SubscribesWithPermissionCheck subscribes = operatingUserOrOperator.GetSubscribes();
                return subscribes.GetTopicsByQueryAndPaging(out count, pageIndex, pageSize, keyword);
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
        public static void DeleteSubscribe(int siteId, int operatingUserOrOperatorId, int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                SubscribesWithPermissionCheck subscribes = new SubscribesWithPermissionCheck(conn, transaction, operatingUserOrOperator, operatingUserOrOperatorId);
                subscribes.Delete(topicId);
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

        public static void AddSubscribe(int siteId, int operatingUserOrOperatorId,int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                SubscribesWithPermissionCheck subscribes = new SubscribesWithPermissionCheck(conn, transaction, operatingUserOrOperator, operatingUserOrOperatorId);
                subscribes.Add(topicId);
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

        public static bool IfUserSubscribeTopic(int siteId, int operatingUserOrOperatorId, int topicId,int userId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn, transaction, topicId, operatingUserOrOperator);
                return topic.IfSubscribe(operatingUserOrOperator,operatingUserOrOperatorId);
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

        public static void UnsubscribeSingleTopic(int siteId, int userOrOperatorId, string email, int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();
                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, userOrOperatorId);
                userOrOperator.UnsubscribeSingleTopic(email, topicId);
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
    }
}
