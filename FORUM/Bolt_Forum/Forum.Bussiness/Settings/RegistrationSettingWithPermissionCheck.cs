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

namespace Com.Comm100.Forum.Bussiness
{
    public class RegistrationSettingWithPermissionCheck : RegistrationSetting
    {
        UserOrOperator _operatingUserOrOperator;

        public RegistrationSettingWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            UserOrOperator operatingUserOrOperator)
            :base(conn, transaction)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public override void Update(bool ifModerateNewUser, bool ifVerifyEmail, bool ifAllowNewUser, int displayNameMinLength, int displayNameMaxLength, string illegalDisplayNames, string displayNameRegularExpression, string displayNameInstruction, string greetingMessage, string agreement)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            base.Update(ifModerateNewUser, ifVerifyEmail, ifAllowNewUser, displayNameMinLength, displayNameMaxLength, illegalDisplayNames, displayNameRegularExpression, displayNameInstruction, greetingMessage, agreement);
        }
    }
}
