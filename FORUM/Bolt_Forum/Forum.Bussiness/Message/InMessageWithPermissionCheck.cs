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
    public class InMessageWithPermissionCheck : InMessage
    {
        UserOrOperator _operatingUserOrOperator;

        public InMessageWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int id)
            : base(conn, transaction, id)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public InMessageWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operaingUserOrOperator,
            int id, string subject, string message, DateTime createDate, int fromUserOrOperatorId,
            string fromUserOrOperatorDisplayName, int toUserOrOperatorId, string toUserOrOperatorDisplayName, bool ifView)
            : base(conn, transaction, id, subject, message, createDate, fromUserOrOperatorId, fromUserOrOperatorDisplayName, toUserOrOperatorId, toUserOrOperatorDisplayName, ifView)
        {
            _operatingUserOrOperator = operaingUserOrOperator;
        }
        public override void Delete()
        {
            CommFun.CheckUserPanelCommonPermission(_operatingUserOrOperator);
            base.Delete();
        }
        public override void UpdateIfView()
        {
            CommFun.CheckUserPanelCommonPermission(_operatingUserOrOperator);
            base.UpdateIfView();
        }


    }
}
