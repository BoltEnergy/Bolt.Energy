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
    public abstract class InMessagesOfReciever : MessagesBase
    {
        private int _userOrOperatorId;

        public int UserOrOperatorId
        {
            get { return this._userOrOperatorId; }
        }

        public InMessagesOfReciever(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
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
            if (subject.Length > ForumDBFieldLength.InMessage_subjectFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("subject",
                    ForumDBFieldLength.InMessage_subjectFieldLength);
            }
            if (message.Length > ForumDBFieldLength.InMessage_messageFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("message",
                    ForumDBFieldLength.InMessage_messageFieldLength);
            }
        }

        public virtual int Add(string subject, string message, DateTime createDate, int fromUserOrOperatorId)
        {
            CheckFieldLength(subject, message);
            return InMessageAccess.AddInMessage(_conn, _transaction, subject, message, createDate, fromUserOrOperatorId, _userOrOperatorId);
        }
        public virtual void UpdateIfView(UserOrOperator operatingOperator, int inMessageId)
        {
            InMessageWithPermissionCheck InMessage = new InMessageWithPermissionCheck(_conn, _transaction, operatingOperator, inMessageId);
            InMessage.UpdateIfView();
        }
        public virtual void Delete(UserOrOperator operatingOperator, int inMessageId)
        {
            InMessageWithPermissionCheck InMessage = new InMessageWithPermissionCheck(_conn,_transaction, operatingOperator,inMessageId);
            InMessage.Delete();
        }
        protected InMessageWithPermissionCheck[] GetMessagesByPaging(out int count,int pageIdex, int pageSize, UserOrOperator operatingUserOrOperator)
        {
            count = InMessageAccess.GetCountOfInmessges(_conn, _transaction, _userOrOperatorId);
            DataTable table = InMessageAccess.GetInMessagesByPaging(_conn, _transaction,_userOrOperatorId, pageIdex, pageSize);
            List<InMessageWithPermissionCheck> InMessages = new List<InMessageWithPermissionCheck>();
            foreach (DataRow  dr in table.Rows)
            {
                InMessageWithPermissionCheck tmpInMessage = CreateInMessageObject(dr, operatingUserOrOperator);
                InMessages.Add(tmpInMessage);
            }
            return InMessages.ToArray<InMessageWithPermissionCheck>();          
        }

        private InMessageWithPermissionCheck CreateInMessageObject(DataRow dr, UserOrOperator operatingUserOrOperator)
        {
            int messageId = Convert.ToInt32(dr["Id"]);
            string subject = Convert.ToString(dr["Subject"]);
            string message = Convert.ToString(dr["Message"]);
            DateTime createDate = Convert.ToDateTime(dr["CreateDate"]);
            int fromUserOrOperatorId = Convert.ToInt32(dr["FromUserId"]);
            string fromUserOrOperatorDisplayName = Convert.ToString(dr["FromUserName"]);         
            int toUserOrOperatorId = Convert.ToInt32(dr["ToUserId"]);
            string toUserOrOperatorDisplayName = Convert.ToString(dr["ToUserName"]);
            bool ifView = Convert.ToBoolean(dr["IfView"]);
            InMessageWithPermissionCheck inMessage = new InMessageWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, messageId, subject, message, createDate, fromUserOrOperatorId, fromUserOrOperatorDisplayName, toUserOrOperatorId, toUserOrOperatorDisplayName, ifView);
            return inMessage;
        }

        public int GetCountOfUnReadInMessages()
        {
            return InMessageAccess.GetCountOfUnReadInMessages(_conn,_transaction,UserOrOperatorId);
        }
        
    }
}
