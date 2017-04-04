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
    public class ForumFeatureWithPermissionCheck : ForumFeature
    {
        UserOrOperator _operatingUserOrOperator;

        public ForumFeatureWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
            :base(conn, transaction)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        private void CheckPermission(bool ifEnableReputation, bool ifEnableReputationPermission)
        {
            if (!ifEnableReputation && ifEnableReputationPermission)
            {
                ExceptionHelper.ThrowForumFeatureDisableReputationPermission();
            }

            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
        }

        public void Update(bool ifEnableMessage, bool ifEnableFavorite, bool ifEnableSubscribe, bool ifEnableScore, bool ifEnableReputation, bool ifEnableHotTopic, bool ifEnableGroupPermission, bool ifEnableReputationPermission)
        {
            CheckPermission(ifEnableReputation, ifEnableReputationPermission);
            base.Update(ifEnableMessage, ifEnableFavorite, ifEnableSubscribe, ifEnableScore, ifEnableReputation, ifEnableHotTopic, ifEnableGroupPermission, ifEnableReputationPermission);
        }
      
    }
}
