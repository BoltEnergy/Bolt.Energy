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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class OutMessagesOfSender : MessagesBase
    {
        private int _userOrOperatorId;

        public int UserOrOperatorId
        {
            get { return this._userOrOperatorId; }
        }

        public OutMessagesOfSender(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
            : base(conn, transaction)
        {
            this._userOrOperatorId = userOrOperatorId;
        }

        private void CheckFieldLength(string subject, string message)
        {
            //required
            if (subject.Length == 0)
            {
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("subject");
            }
            if (subject.Length > ForumDBFieldLength.OutMessage_subjectFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("subject",
                    ForumDBFieldLength.OutMessage_subjectFieldLength);
            }
            if (message.Length > ForumDBFieldLength.OutMessage_messageFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("message",
                    ForumDBFieldLength.OutMessage_messageFieldLength);
            }
        }

        private int Add(string subject, string message, DateTime createDate)
        {
            CheckFieldLength(subject, message);
            return OutMessageAccess.AddOutMessage(_conn, _transaction, subject, message, createDate, this._userOrOperatorId);
        }

        public virtual int Add(string subject, string message, DateTime createDate, int receiverId)
        {
            int outMessageId = Add(subject, message, createDate);
            UsersOrOperatorsOfOutMessage usersOrOperatorsOfOutMessage = new UsersOrOperatorsOfOutMessage(_conn, _transaction, outMessageId);
            usersOrOperatorsOfOutMessage.Add(receiverId);
            return outMessageId;
        }   

        public virtual int Add(string subject, string message, DateTime createDate, List<int> userGroups, List<int> reputationGroups, List<int> receiverIds)
        {
            int outMessageId = Add(subject, message, createDate);
            UserGroupsOfOutMessage userGroupsOfOutMessage = new UserGroupsOfOutMessage(_conn, _transaction, outMessageId);
            userGroupsOfOutMessage.Add(userGroups);
            UserReputationGroupsOfOutMessage userReputationGroupsOfOutMessage = new UserReputationGroupsOfOutMessage(_conn, _transaction, outMessageId);
            userReputationGroupsOfOutMessage.Add(reputationGroups);
            UsersOrOperatorsOfOutMessage usersOrOperatorsOfOutMessage = new UsersOrOperatorsOfOutMessage(_conn, _transaction, outMessageId);
            usersOrOperatorsOfOutMessage.Add(receiverIds);
            return outMessageId;
        }
      
        protected OutMessageWithPermissionCheck[] GetMessagesByPaging(out int count, int pageIndex, int pageSize, UserOrOperator operatingUserOrOperator)
        {
            count = OutMessageAccess.GetCountOfOutMessages(_conn, _transaction, _userOrOperatorId);
            DataTable table = OutMessageAccess.GetOutMessagesByPaging(_conn, _transaction, _userOrOperatorId, pageIndex, pageSize);
            List<OutMessageWithPermissionCheck> outMessages = new List<OutMessageWithPermissionCheck>();
            foreach (DataRow  dr in table.Rows)
            {
                OutMessageWithPermissionCheck tmpOutMessage = CreateOutMessageObject(dr, operatingUserOrOperator);
                outMessages.Add(tmpOutMessage);
            }
            return outMessages.ToArray<OutMessageWithPermissionCheck>();
        }

        private OutMessageWithPermissionCheck CreateOutMessageObject(DataRow dr, UserOrOperator operatingUserOrOperator)
        {
            int outMessageId = Convert.ToInt32(dr["Id"]);
            string subject = Convert.ToString(dr["Subject"]);
            string message = Convert.ToString(dr["Message"]);
            DateTime createDate = Convert.ToDateTime(dr["CreateDate"]);
            int fromUserOrOperatorId = Convert.ToInt32(dr["FromUserId"]);
            string fromUserOrOperatorDisplayName = Convert.ToString(dr["FromUserName"]);
            OutMessageWithPermissionCheck outMessage = new OutMessageWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, outMessageId, subject, message, createDate, fromUserOrOperatorId, fromUserOrOperatorDisplayName);
            return outMessage;
        }

        protected void Delete(int outMessageId, UserOrOperator operatingUserOrOprator)
        {
            OutMessageWithPermissionCheck outMessage = new OutMessageWithPermissionCheck(_conn, _transaction, operatingUserOrOprator, outMessageId);
            outMessage.Delete();
        }
                
    }
}
