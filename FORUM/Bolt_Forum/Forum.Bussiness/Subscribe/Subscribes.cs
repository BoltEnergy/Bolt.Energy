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
    public abstract class Subscribes
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        private int _userOrOperatorId;

        public int UserOrOperatorId
        {
            get { return this._userOrOperatorId; }
        }

        public Subscribes(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        {
            this._conn = conn;
            this._transaction = transaction;
            this._userOrOperatorId = userOrOperatorId;
        }

        protected SubscribeWithPermissionCheck[] GetTopicsByQueryAndPaging(out int count, int pageIndex, int pageSize, string keyword, UserOrOperator operatingUserOrOperator)
        {
            count = DataAccess.SubscribeAccess.GetCountOfSubscribes(this._conn, this._transaction, this._userOrOperatorId, keyword);
            DataTable table = DataAccess.SubscribeAccess.GetSubscribesQueryAndPaging(this._conn, this._transaction, this._userOrOperatorId, pageIndex, pageSize, keyword);
                  List<SubscribeWithPermissionCheck> Subscribes = new List<SubscribeWithPermissionCheck>();
            foreach (DataRow  dr in table.Rows )
            {
                SubscribeWithPermissionCheck tmpSubscribe = CreateSubscribeObject(dr, operatingUserOrOperator);
                Subscribes.Add(tmpSubscribe);
            }
            return Subscribes.ToArray<SubscribeWithPermissionCheck>();
        }
        protected SubscribeWithPermissionCheck CreateSubscribeObject(DataRow dr, UserOrOperator operatingUserOrOperator)
        {
            SubscribeWithPermissionCheck subscribe;
            int topicId = Convert.ToInt32(dr["TopicId"]);
            string subject = Convert.ToString(dr["Subject"]);
            DateTime postTime = Convert.ToDateTime(dr["PostTime"]);
            int postUserOrOperatorId = Convert.ToInt32(dr["PostUserOrOperatorId"]);
            string postUserOrOperatorName = Convert.ToString(dr["PostUserOrOperatorName"]);
            bool postUserOrOperatorIfDeleted = Convert.ToBoolean(dr["PostUserOrOperatorIfDeleted"]);
            int lastPostId = Convert.ToInt32(dr["LastPostId"]);
            DateTime lastPostTime = Convert.ToDateTime(dr["LastPostTime"]);
            int lastPostUserOrOperatorId = Convert.ToInt32(dr["LastPostUserOrOperatorId"]);
            string lastPostUserOrOperatorName = Convert.ToString(dr["LastPostUserOrOperatorName"]);
            bool lastPostUserOrOperatorIfDeleted = Convert.ToBoolean(dr["LastPostUserOrOperatorIfDeleted"]);
            int numberOfReplies = Convert.ToInt32(dr["NumberOfReplies"]);
            int numberOfHits = Convert.ToInt32(dr["NumberOfHits"]);
            int forumId = Convert.ToInt32(dr["ForumId"]);
            int userOrOperatorId = Convert.ToInt32(dr["UserOrOperatorId"]);
            bool ifMarkedAsAnswer = Convert.ToBoolean(dr["IfMarkedAsAnswer"]);
            bool ifClosed = Convert.ToBoolean(dr["IfClosed"]);
            string[] strTemp = Convert.ToString(dr["ParticipatorIds"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] participatorIds = new int[strTemp.Length];
            int j = 0;
            if (strTemp.Length == 0)
            {
                participatorIds = new int[] { };
            }
            else
            {
                foreach (string item in strTemp)
                {
                    participatorIds[j++] = Convert.ToInt32(item);
                }
            }
            DateTime subscribeDate = new DateTime();
            subscribeDate = Convert.ToDateTime(dr["SubscribeDate"]);
            subscribe = new SubscribeWithPermissionCheck(this._conn, this._transaction, operatingUserOrOperator, topicId, subject, postTime, postUserOrOperatorId, postUserOrOperatorName, postUserOrOperatorIfDeleted, lastPostId, lastPostTime, lastPostUserOrOperatorId, lastPostUserOrOperatorName, lastPostUserOrOperatorIfDeleted, numberOfReplies, numberOfHits, forumId, userOrOperatorId, ifMarkedAsAnswer, ifClosed, participatorIds, subscribeDate);
            return subscribe;
        }
        public virtual void Add(int topicId,UserOrOperator operatingUserOrOperator)
        {
            Subscribe.Add(_conn,_transaction,_userOrOperatorId,topicId);
            /*2.0 stategy */
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, topicId, operatingUserOrOperator);
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            //scoreStrategySetting.UseAfterAddTopicToFavorites(topic, operatingUserOrOperator);
            /*2.0 reputation strategy*/
            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            //reputationStrategySetting.UseAfterAddTopicToSubscribes(topic,operatingUserOrOperator);
        }
        protected void Delete(UserOrOperator operatingUserOrOperator, int topicId)
        {
            SubscribeWithPermissionCheck subscribe = new SubscribeWithPermissionCheck(this._conn, this._transaction, operatingUserOrOperator, topicId, this._userOrOperatorId);
            subscribe.Delete(topicId);
        }
        public bool IfUserSubscribeTopic(int userId, int topicId)
        {
            return SubscribeAccess.IfUserSubscribeTopic(_conn, _transaction, topicId, userId);
        }
    }
}
