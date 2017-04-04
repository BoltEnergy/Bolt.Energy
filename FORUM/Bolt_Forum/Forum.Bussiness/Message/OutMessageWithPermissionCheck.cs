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
    public class OutMessageWithPermissionCheck : OutMessage
    {
        UserOrOperator _operatingUserOrOperator;

        public OutMessageWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int id)
            : base(conn, transaction, id)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public OutMessageWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator,
            int id, string subject, string message, DateTime createDate,
            int fromUserOrOperatorId, string fromUserOrOperatorDisplayNam)
            : base(conn, transaction, id, subject, message, createDate, fromUserOrOperatorId, fromUserOrOperatorDisplayNam)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

    }
}
