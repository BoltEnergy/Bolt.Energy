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
    public class PayHistroyProcess
    {
        public static void AddAttachmentPayHistroy(int operatingUserOrOperatorId, int siteId
             , int itemId, int userId, int score, DateTime date)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                    conn, transaction, null, operatingUserOrOperatorId);

                new PayHistoriesOfAttachment(conn, transaction, itemId).Add(operatingUserOrOperator,userId, score, date);
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

        public static void AddTopicPayHistroy(int operatingUserOrOperatorId, int siteId
            , int itemId, int userId, int score, DateTime date)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                    conn, transaction, null, operatingUserOrOperatorId);

                PayHistoriesOfTopic history = new PayHistoriesOfTopic(conn, transaction, itemId);
                    history.Add(operatingUserOrOperator,userId, score, date);
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

        public static bool ifUserPayAttachment(int operatingUserOrOperatorId, int siteId
            ,int userId,int attachId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                    conn, transaction, null, operatingUserOrOperatorId);

                return new PayHistoriesOfAttachment(conn, transaction, attachId).IfPaid(userId);
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

        public static bool ifUserPayTopic(int operatingUserOrOperatorId, int siteId
            , int userId,int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                    conn, transaction, null, operatingUserOrOperatorId);

                return new PayHistoriesOfTopic(conn, transaction, topicId).IfPaid(userId);
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
