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

namespace Com.Comm100.Framework.Enum.Forum
{
    public enum EnumAdminMenuType
    {
        enumForumDashboard = 0,
        enumForumHomePage = 1,
        enumUserManage = 2,
        enumUserModeration= 3,
        enumCategoriesManage = 4,
        enumForumManage = 5,
        enumDraftManage = 6,
        enumStyleSettings = 7,
        enumSiteSettings = 8,
        enumRegistrationSettings = 9,
        enumStyleTemplate = 10,
        enumCodePlan = 11,
        enumOperatorManage = 12,//Open Source
        enumRulesAndPoliciesSettings=13,

        /*--------------------2.0 Begin----------------------*/
        enumForumFeature = 14,
        enumUserGroups = 15,
        enumAdministrators = 16,
        enumReputationGroups = 17,
        enumAnnouncements = 18,
        enumWaitingForModerationPosts = 19,
        enumRejectedPosts = 20,
        enumTopicManagement = 21,
        enumPostManagement = 22,
        enumTopicAndPostRecycle = 23,
        enumAbuse = 24,
        enumBan = 25,
        enumEmailVerify=26,
        /*--------------------2.0 End----------------------*/
    }
}
