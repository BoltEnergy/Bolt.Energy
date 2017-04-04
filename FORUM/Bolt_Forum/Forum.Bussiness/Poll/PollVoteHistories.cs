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
    public class PollVoteHistories
    {
        private SqlConnectionWithSiteId _conn;
        private SqlTransaction _transaction;

        private int _topicId;
        public int TopicId
        {
            get { return this._topicId; }
        }

        public PollVoteHistories(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
        {
            _conn = conn;
            _transaction = transaction;
            _topicId = topicId;
        }

        public void Add(int userOrOperatorId)
        {
            PollAccess.AddPollVoteHistory(_conn, _transaction, _topicId, userOrOperatorId);
        }

        public bool IfExist(int userOrOperatorId)
        {
            if (PollAccess.GetCountOfPollVoteHistoryByUser(_conn, _transaction, _topicId, userOrOperatorId) > 0)
                return true;
            else
                return false;
        }

        public int GetCountOfPollVoteHistories()
        {
           return PollAccess.GetCountOfPollvoteHistory(_conn, _transaction, _topicId);
        }
    }
}
