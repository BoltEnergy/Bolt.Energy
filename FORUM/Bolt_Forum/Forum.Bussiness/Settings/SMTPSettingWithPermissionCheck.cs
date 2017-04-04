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
    public class SMTPSettingWithPermissionCheck : SMTPSetting
    {
        UserOrOperator _operatingUserOrOperator;

        public SMTPSettingWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int siteId)
            : base(conn, transaction, siteId)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public override void Update(string smtpServer, int smtpPort, bool ifAuthentication, string smtpUserName, string smtpPassword, string fromEmailAddress, string fromName, bool ifSSL)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            base.Update(smtpServer, smtpPort, ifAuthentication, smtpUserName, smtpPassword, fromEmailAddress, fromName, ifSSL);
        }
    }
}
