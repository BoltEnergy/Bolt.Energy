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
    public class HotTopicStrategySettingWithPermissionCheck : HotTopicStrategySetting
    {
        UserOrOperator _operatingUserOrOperator;

        public HotTopicStrategySettingWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public void Update(int parameterGreaterThanOrEqualViews, int parameterGreaterThanOrEqualPosts, EnumLogical logicalBetweenViewsAndPosts)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            base.Update(_operatingUserOrOperator,parameterGreaterThanOrEqualViews, parameterGreaterThanOrEqualPosts, logicalBetweenViewsAndPosts);
        }
    }
}
