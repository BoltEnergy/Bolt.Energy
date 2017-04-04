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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public class Poll
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        private int _topicId;
        private bool _ifMulitipleChoice;
        private int _maxChoices;
        private bool _ifSetDeadline;
        private DateTime _startDate;
        private DateTime _endDate;
        #endregion

        #region properties
        public int TopicId
        {
            get { return this._topicId; }
        }
        public bool IfMulitipleChoice
        {
            get { return this._ifMulitipleChoice; }
        }
        public int MaxChoices
        {
            get { return this._maxChoices; }
        }
        public bool IfSetDeadline
        {
            get { return this._ifSetDeadline; }
        }
        public DateTime StartDate
        {
            get { return this._startDate; }
        }
        public DateTime EndDate
        {
            get { return this._endDate; }
        }
        #endregion

        public Poll(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {
            _conn = conn;
            _transaction = transaction;
            _topicId = topicId;

            DataTable dt = PollAccess.GetPollByTopicId(conn, transaction, topicId);
            if (dt.Rows.Count == 0)
                ExceptionHelper.ThrowForumPollNotExistException();
            foreach (DataRow dr in dt.Rows)
            {
                _ifMulitipleChoice = Convert.ToBoolean(dr["IfMulitipleChoice"]);
                _maxChoices = Convert.ToInt32(dr["MaxChoices"]);
                _ifSetDeadline = Convert.ToBoolean(dr["IfSetDeadline"]);
                _startDate = Convert.ToDateTime(dr["StartDate"]);
                _endDate = Convert.ToDateTime(dr["EndDate"]);
            }
        }

        public void Update(bool ifMultipleChoice, int maxChoices, bool ifSetDeadline,
                DateTime startDate, DateTime endDate, PollOptionStruct[] options)
        {
            //update poll
            PollAccess.UpdatePoll(_conn, _transaction, _topicId, ifMultipleChoice,
                maxChoices, ifSetDeadline, endDate);
            //get want to delete option id
            PollOptions ItsOptions = this.GetOptions();
            List<int> toDeleteIds = new List<int>();
            foreach (PollOption option in ItsOptions.GetAllOptions())
            {
                bool IfDelete = true;
                foreach (PollOptionStruct Toption in options)
                {
                    if (Toption.Id == option.Id)
                    {
                        IfDelete = false; break;
                    }
                }
                if (IfDelete)
                    toDeleteIds.Add(option.Id);
            }
            //delete option
            foreach (int toDeleteId in toDeleteIds)
            {
                PollOption option = new PollOption(_conn, _transaction, toDeleteId);
                option.Delete();
            }
            //update option
            foreach (PollOptionStruct Toption in options)
            {
                PollOption option=new PollOption(_conn,_transaction,Toption.Id);
                option.Update(Toption.OptionText,Toption.OrderNum);
            }
        }

        public void Update(bool ifMultipleChoice, int maxChoices, bool ifSetDeadline,
                 DateTime endDate)
        {
            //update poll
            PollAccess.UpdatePoll(_conn, _transaction, _topicId, ifMultipleChoice,
                maxChoices, ifSetDeadline, endDate);
        }

        public void Delete()
        {
            //delete poll
            PollAccess.DeletePoll(_conn, _transaction, _topicId);
            //delete pollOptions
            PollOptions pollOptions = new PollOptions(_conn, _transaction, _topicId);
            foreach (PollOption option in pollOptions.GetAllOptions())
                option.Delete();
        }

        public PollOptions GetOptions()
        {
            return new PollOptions(_conn, _transaction, _topicId);
        }

        public void VotePollOption(int forumId, int topicId, int[] optionIds, UserOrOperator operatingUserOrOperator)
        {
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, forumId, operatingUserOrOperator);
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, topicId, operatingUserOrOperator);
            if (topic.IfDeleted)
                ExceptionHelper.ThrowTopicNotExistException(topic.TopicId);
            if(!CommFun.IfAdminInUI(operatingUserOrOperator) && !CommFun.IfModeratorInUI(_conn,_transaction,forumId,operatingUserOrOperator)
                && CommFun.IfForumIsClosedInForumPage(forum))
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(operatingUserOrOperator.DisplayName);

            CommFun.CheckCommonPermissionInUI(operatingUserOrOperator);

            if (this.IfSetDeadline && this.EndDate < DateTime.UtcNow)
            {
                ExceptionHelper.ThrowForumPollHaveExpireException();
            }
            PollVoteHistories history = new PollVoteHistories(_conn, _transaction, topicId);
            if (history.IfExist(operatingUserOrOperator.Id))
            {
                ExceptionHelper.ThrowForumPollHaveVotedException();
            }

            foreach (int optionId in optionIds)
            {
                new PollOption(_conn, _transaction, optionId).Vote(_topicId, operatingUserOrOperator);
            }

            history.Add(operatingUserOrOperator.Id);

            /*2.0 stategy */
            //TopicWithPermissionCheck topic = new TopicWithPermissionCheck(
            //    _conn, _transaction, _topicId, operatingUserOrOperator);
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            scoreStrategySetting.UseAfterVote(topic, operatingUserOrOperator);
            /*2.0 reputation strategy*/
            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            reputationStrategySetting.UseAfterVote(topic, operatingUserOrOperator);
        }
    }
}
