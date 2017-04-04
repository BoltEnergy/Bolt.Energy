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
    public class InMessagesOfRecieverWithPermissionCheck : InMessagesOfReciever
    {
        UserOrOperator _operatingUserOrOperator;

        public InMessagesOfRecieverWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int userOrOperatorId)
            : base(conn, transaction, userOrOperatorId)
        {
            _operatingUserOrOperator = operatingUserOrOperator;        
        }

        public override void UpdateIfView(UserOrOperator operatingOperator, int inMessageId)
        {
            base.UpdateIfView(operatingOperator, inMessageId);
        }
        public override void Delete(UserOrOperator operatingOperator, int inMessageId)
        {
            base.Delete(operatingOperator, inMessageId);           
        }
        public InMessageWithPermissionCheck[] GetMessagesByPaging(out int count, int pageIndex, int pageSize)
        {
            return base.GetMessagesByPaging(out count, pageIndex, pageSize, _operatingUserOrOperator);
        }

    }
}
