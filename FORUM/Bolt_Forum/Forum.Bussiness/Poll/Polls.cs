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
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.Bussiness
{
    public class Polls
    {
        private SqlConnectionWithSiteId _conn;
        private SqlTransaction _transaction;

        public Polls(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            _conn = conn;
            _transaction = transaction;
        }
        
        public void Add(int topicId, bool ifMultipleChoice, int maxChoices, bool ifSetDeadline, 
            DateTime startDate, DateTime endDate, PollOptionStruct[] options)
        { 
            //add poll
            
            PollAccess.AddPoll(_conn, _transaction, 
                topicId, ifMultipleChoice, maxChoices, ifSetDeadline,
                startDate, endDate);
            //add pollOptions
            
            foreach (PollOptionStruct option in options)
                PollOption.Add(_conn, _transaction, topicId,
                    option.OptionText, option.OrderNum);
        }

        public void Delete(int topicId)
        {
            Poll poll = new Poll(_conn, _transaction, topicId);
            poll.Delete();
        }
    }
}
