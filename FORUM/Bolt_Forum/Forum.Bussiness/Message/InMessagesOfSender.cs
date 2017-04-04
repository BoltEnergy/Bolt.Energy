#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2009                                                                               //
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
    public abstract class InMessagesOfSender : MessagesBase
    {
        private int _userOrOperatorId;

        public int UserOrOperatorId
        {
            get { return this._userOrOperatorId; }
        }

        public InMessagesOfSender(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
            :base(conn, transaction)
        {
 
        }

        public int GetCountOfMessagesByTimeUnit(DateTime startDate, DateTime endDate)
        {
            return 0;
        }
    }
}
