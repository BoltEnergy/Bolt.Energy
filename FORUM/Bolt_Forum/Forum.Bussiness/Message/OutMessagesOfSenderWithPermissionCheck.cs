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
    public class OutMessagesOfSenderWithPermissionCheck : OutMessagesOfSender
    {
        UserOrOperator _operatingUserOrOperator;

        public OutMessagesOfSenderWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int userOrOperatorId)
            : base(conn, transaction, userOrOperatorId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }
        public OutMessageWithPermissionCheck[] GetMessagesByPaging(out int count, int pageIndex, int pageSize)
        {
            return base.GetMessagesByPaging(out count, pageIndex, pageSize, _operatingUserOrOperator);
        }

        public void Delete(int outMessageId)
        {
            CommFun.CheckUserPanelCommonPermission(_operatingUserOrOperator);
            base.Delete(outMessageId, _operatingUserOrOperator);
        }
        public override int Add(string subject, string message, DateTime createTime, int receiverId)
        {
            CommFun.CheckUserPanelCommonPermission(_operatingUserOrOperator);
            SentMessagesOfUserOrOperatorWithPermissionCheck sentMessages = new SentMessagesOfUserOrOperatorWithPermissionCheck(
                _conn,_transaction,_operatingUserOrOperator,UserOrOperatorId);
            
            int maxMessagesSentInOneDay = sentMessages.GetCountOfMessagesByTimeUnit(DateTime.Now.Date.ToUniversalTime());
            CommFun.UserPermissionCache().CheckMaxMessagesSentinOneDayPermission(maxMessagesSentInOneDay ,
                _operatingUserOrOperator);
            return base.Add(subject, message, createTime, receiverId);
        }

        public override int Add(string subject, string message, DateTime createDate, List<int> userGroups,
            List<int> reputationGroups, List<int> receiverIds)
        {
            CommFun.CheckUserPanelCommonPermission(_operatingUserOrOperator);
            return base.Add(subject, message, createDate, userGroups, reputationGroups, receiverIds);
        }

       
    }
}
