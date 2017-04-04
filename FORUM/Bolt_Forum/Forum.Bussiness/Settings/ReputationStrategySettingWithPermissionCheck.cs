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
    public class ReputationStrategySettingWithPermissionCheck : ReputationStrategySetting
    {
        UserOrOperator _operatingUserOrOperator;

        public ReputationStrategySettingWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operaingUserOrOperator, int siteId)
            : base(conn, transaction)
        {
            _operatingUserOrOperator = operaingUserOrOperator;
        }
        public override void Update(int registration, int firstLoginEveryDay, int addModerator, int removeModerator, int ban, int unban, int newTopic, int topicMarkedAsFeature, int topicMarkedAsSticky, int topicDeleted, int topicRestored, int topicAddedIntoFavorites, int topicRemovedFromFavorites, int topicViewed, int topicReplied, int topicVerifiedAsSpam, int vote, int pollVoted, int newPost, int postDeleted, int postRestored, int postVerifiedAsSpam, int postMarkedAsAnswer, int reportAbuse, int search)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            base.Update(registration, firstLoginEveryDay, addModerator, removeModerator, ban, unban, newTopic, topicMarkedAsFeature, topicMarkedAsSticky, topicDeleted, topicRestored, topicAddedIntoFavorites, topicRemovedFromFavorites, topicViewed, topicReplied, topicVerifiedAsSpam, vote, pollVoted, newPost, postDeleted, postRestored, postVerifiedAsSpam, postMarkedAsAnswer, reportAbuse, search);
        }
    }
}
