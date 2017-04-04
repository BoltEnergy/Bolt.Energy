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
using Com.Comm100.Framework.FieldLength;

namespace Com.Comm100.Forum.Bussiness
{
    public class PollOption
    {
        private SqlConnectionWithSiteId _conn;
        private SqlTransaction _transaction;

        #region private fields
        private int _id;
        private int _topicId;
        private string _optionText;
        private int _orderNum;
        private int _votes;
        #endregion

        #region properties
        public int Id
        {
            get { return this._id; }
        }
        public int TopicId
        {
            get { return this._topicId; }
        }
        public string OptionText
        {
            get { return this._optionText; }
        }
        public int OrderNum { get { return this._orderNum; } }
        public int Votes
        {
            get { return this._votes; }
        }
        #endregion

        public PollOption(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id)
        {
            _conn = conn;
            _transaction = transaction;
            _id = id;

            DataTable dt = PollAccess.GetPollOptionById(_conn, _transaction, _id);
            foreach (DataRow dr in dt.Rows)
            {
                _topicId = Convert.ToInt32(dr["PollId"]);
                _optionText = Convert.ToString(dr["OptionText"]);
                _votes = Convert.ToInt32(dr["Votes"]);
                _orderNum = Convert.ToInt32(dr["OrderNum"]);
            }
        }

        public PollOption(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id,
            int topicId,string optionText,int votes,int orderNum)
        {
            _conn = conn;
            _transaction = transaction;
            _id = id;

            _topicId = topicId;
            _optionText = optionText;
            _votes = votes;
            _orderNum = orderNum;
        }

        private static void CheckFieldsLength(string optionText)
        {
            if (optionText.Trim().Length > ForumDBFieldLength.PollOption_optionTextFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("OptionText", ForumDBFieldLength.PollOption_optionTextFieldLength);
            if (optionText.Trim() == "")
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("OptionText");
        }

        public static void Add(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int pollId, string optionText, int orderNum)
        {
            CheckFieldsLength(optionText);
            PollAccess.AddPollOption(conn, transaction, pollId, optionText, orderNum);
        }

        public void Update(string optionText, int orderNum)
        {
            CheckFieldsLength(optionText);
            PollAccess.UpdatePollOption(_conn, _transaction,_id,optionText, orderNum);
        }

        public void Delete()
        {
            PollAccess.DeletePollOption(_conn, _transaction, _id);
        }

        public void Vote(int topicId,UserOrOperator operatingUserOrOperator)
        {
            PollAccess.VotePollOption(_conn, _transaction, _id, topicId);
        }
    }
}
