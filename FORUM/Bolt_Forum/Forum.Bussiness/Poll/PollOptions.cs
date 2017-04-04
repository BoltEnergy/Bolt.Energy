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

namespace Com.Comm100.Forum.Bussiness
{
    public class PollOptions
    {
        private SqlConnectionWithSiteId _conn;
        private SqlTransaction _transaction;

        private int _topicId;
        public int TopicId
        {
            get { return this._topicId; }
        }

        public PollOptions(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int topicId)
        {
            _conn = conn;
            _transaction = transaction;
            _topicId = topicId;
        }

        public PollOption[] GetAllOptions()
        {
            List<PollOption> polls = new List<PollOption>();
            DataTable dt = PollAccess.GetPollOptionsByTopicId(_conn, _transaction, _topicId);
            foreach (DataRow dr in dt.Rows)
            {
                PollOption poll = new PollOption(_conn, _transaction,
                    Convert.ToInt32(dr["Id"]),
                    Convert.ToInt32(dr["PollId"]),
                    Convert.ToString(dr["OptionText"]),
                    Convert.ToInt32(dr["Votes"]),
                    Convert.ToInt32(dr["OrderNum"]));
                polls.Add(poll);
            }
            return polls.ToArray<PollOption>();
        }

        public void MoveUpOrMoveDownPollOption(int pollOptionId, bool ifMoveUp)
        {
            PollOption[] pollOptions = this.GetAllOptions();
            PollOption toMoveOption; int toMoveIdOrderNum = -1;
            PollOption beMovedOption; int beMovedIdOrderNum = -1;
            for (int i = 0; i < pollOptions.Length; i++)
            {
                if (pollOptions[i].Id == pollOptionId)
                {
                    int MovePosition = -1;
                    if (ifMoveUp)
                        MovePosition = i-1;
                    else
                        MovePosition = i+1;

                    toMoveOption = pollOptions[i];
                    toMoveIdOrderNum = MovePosition;
                    beMovedOption = pollOptions[MovePosition];
                    beMovedIdOrderNum = i;

                    /*move to move*/
                    toMoveOption.Update(toMoveOption.OptionText, toMoveIdOrderNum);
                    /*move to be moved*/
                    beMovedOption.Update(beMovedOption.OptionText, beMovedIdOrderNum);
                    break;
                }
            }
        }
        public void DeletePollOption(int pollOptionId)
        {
            PollOption[] pollOptions = this.GetAllOptions();
            bool ifUpdatePostion = false;
            for (int i = 0; i < pollOptions.Length; i++)
            {
                if (pollOptions[i].Id == pollOptionId)
                {
                    pollOptions[i].Delete();
                    ifUpdatePostion = true;
                }
                else if (ifUpdatePostion == true)
                {
                    pollOptions[i].Update(pollOptions[i].OptionText, i - 1);
                }
            }
        }

        public int GetCount()
        {
            return PollAccess.GetCountOfOptionsByTopicId(_conn, _transaction, _topicId);
        }

    }
}
