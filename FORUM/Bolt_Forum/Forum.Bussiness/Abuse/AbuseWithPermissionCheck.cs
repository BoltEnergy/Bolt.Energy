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
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.Bussiness
{
    public class AbuseWithPermissionCheck : Abuse
    {
        UserOrOperator _operatingUserOrOperator;

        public AbuseWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, 
            UserOrOperator operatingUserOrOperator, int id)
            :base(conn, transaction, id)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public AbuseWithPermissionCheck(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            UserOrOperator operatingUserOrOperator,
            int id, int postId, string postSubject, int postUserOrOperatortId,
            string postUserOrOperatorName, int abuseUserOrOperatorId, string abuseUserOrOperatorName
            , EnumAbuseStatus status, DateTime date, string note)
            : base(conn, transaction, id, postId,postSubject,postUserOrOperatortId,
            postUserOrOperatorName,abuseUserOrOperatorId,abuseUserOrOperatorName,
            status,date,note)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        private void CheckApprovePermission()
        { }

        public override void Approve()
        {
            CheckApprovePermission();
            base.Approve();
        }

        private void CheckRefusePermission()
        { }

        public override void Refuse()
        {
            CheckRefusePermission();
            base.Refuse();
        }
    }
}
