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
    public abstract class OutMessage : MessageBase
    {
        public OutMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id)
            : base(conn, transaction)
        {
            DataTable table = new DataTable();
            table = OutMessageAccess.GetOutMessageById(conn, transaction,id);
            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowForumOutMessageNotExistException(id);
            }
            else 
            {
                _id = id;
                _subject = Convert.ToString(table.Rows[0]["Subject"]);
                _message = Convert.ToString(table.Rows[0]["Message"]);
                _createDate = Convert.ToDateTime(table.Rows[0]["CreateDate"]);
                _fromUserOrOperatorId = Convert.ToInt32(table.Rows[0]["FromUserId"]);
                _fromUserOrOperatorDisplayName = Convert.ToString(table.Rows[0]["FromUserName"]);            
            }

        }
        public OutMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id, string subject, string message, DateTime createDate,
            int fromUserOrOperatorId, string fromUserOrOperatorDisplayName)           
            : base(conn, transaction)
        {      
            this._id = id;
            this._subject = subject;
            this._message = message;
            this._createDate = createDate;
            this._fromUserOrOperatorId = fromUserOrOperatorId;
            this._fromUserOrOperatorDisplayName = fromUserOrOperatorDisplayName;           
        }

        public override void Delete()
        {
            OutMessageAccess.DeleteOutMessage(_conn, _transaction , _id);
        }

        public UsersOrOperatorsOfOutMessage GetRecieveUsersAndOperators()
        {
            return new UsersOrOperatorsOfOutMessage(_conn, _transaction, _id);
        }

        public UserGroupsOfOutMessage GetUserGroups()
        {
            return new UserGroupsOfOutMessage(_conn, _transaction, _id);
        }

        public UserReputationGroupsOfOutMessage GetUserReputationGroups()
        {
            return new UserReputationGroupsOfOutMessage(_conn, _transaction, _id);
        }
    }
}
