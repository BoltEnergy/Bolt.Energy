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
using Com.Comm100.Forum.Bussiness;

namespace Com.Comm100.Forum.Process
{
    public class PollProcess
    {
        public static Poll GetPollByTopicId(
           int operatingUserOrOperatorId, int siteId,
           int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                //UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                Poll poll = new Poll(conn, transaction, topicId);
                return poll;
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

        public static PollOption[] GetPollOptionsByTopicId(
           int operatingUserOrOperatorId, int siteId,
           int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                //UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                PollOptions options = new PollOptions(conn, transaction, topicId);
                return options.GetAllOptions();
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

        public static void MoveUpOrDownPollOptionsById(
            int operatingUserOrOperatorId, int siteId,
           int id,int topicId,bool ifMoveUp)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                //UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                PollOptions options = new PollOptions(conn, transaction, topicId);
                options.MoveUpOrMoveDownPollOption(id,ifMoveUp);
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

        public static void DeletePollOption(
            int operatingUserOrOperatorId, int siteId,
           int id, int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                //UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                PollOptions options = new PollOptions(conn, transaction, topicId);
                options.DeletePollOption(id);
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

        public static void VotePollOption(
           int operatingUserOrOperatorId, int siteId, int forumId,
          int[] ids, int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                Poll poll = new Poll(conn, transaction, topicId);
                poll.VotePollOption(forumId,topicId,ids, operatingUserOrOperator);
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

        public static bool IfUserVotePoll(int operatingUserOrOperatorId, int siteId,int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                PollVoteHistories history = new PollVoteHistories(conn, transaction, topicId);
                return history.IfExist(operatingUserOrOperatorId);
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


        public static int GetCountOfPollvoteHistories(int siteId, int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                PollVoteHistories history = new PollVoteHistories(conn, transaction, topicId);
                return history.GetCountOfPollVoteHistories();
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
    }
}
