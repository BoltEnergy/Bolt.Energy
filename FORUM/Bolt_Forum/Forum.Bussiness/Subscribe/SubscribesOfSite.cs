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
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class SubscribesOfSite
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        public SubscribesOfSite(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            _conn = conn;
            _transaction = transaction;
        }

        public int GetCountByTopicId(int topicId)
        {
            return SubscribeAccess.GetCountOfSubscribesByTopicId(_conn, _transaction, topicId);
        }

        public UserOrOperator[] GetUserOrOperatorsByTopicId(int topicId,UserOrOperator operatoringUserOrOperator)
        {
            List<UserOrOperator> users = new List<UserOrOperator>();
            DataTable dt = SubscribeAccess.GetUserOrOperatorsByTopicId(_conn, _transaction, topicId);
            foreach (DataRow dr in dt.Rows)
            {
                UserOrOperator user = UserOrOperatorFactory.CreateUserOrOperator(_conn,
                    _transaction, operatoringUserOrOperator,Convert.ToInt32(dr["UserId"]));
                users.Add(user);
            }
            return users.ToArray<UserOrOperator>();
        }

        public void SendEmailToUsers(int topicId,int postId,EnumQueueEmailType emailType,UserOrOperator operatingUserOrOperator,DateTime createTime)
        {
            UserOrOperator[] users = this.GetUserOrOperatorsByTopicId(
                topicId, operatingUserOrOperator);
            QueueEmails emailsQueue = new QueueEmails(_conn.SqlConn, _transaction);

            foreach (UserOrOperator user in users)
            {
                emailsQueue.Add(_conn.SiteId, topicId, postId, emailType, createTime);
            }
        }
    }
}
