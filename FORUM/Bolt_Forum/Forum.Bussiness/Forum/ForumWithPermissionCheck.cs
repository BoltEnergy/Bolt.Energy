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
    public class ForumWithPermissionCheck : Forum
    {
        UserOrOperator _operatingUserOrOperator;

        public ForumWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction, forumId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public ForumWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, UserOrOperator operatingUserOrOperator, int categoryId, int orderId, string name,
            EnumForumStatus forumStatus, int numberOfTopics, int numberOfPosts, string description, int lastPostId,int lastPostTopicId, string lastPostSubject, int lastPostCreatedUserOrOperatorId,
            string lastPostCreateUserOrOperatorName, bool lastPostCreatedUserOrOperatorIfDeleted, DateTime lastPostPostTime, bool ifAllowPostNeedingReplayTopic, bool ifAllowPostNeedingPayTopic)
            : base(conn, transaction, forumId, categoryId, orderId, name, forumStatus, numberOfTopics, numberOfPosts, description, lastPostId,lastPostTopicId, lastPostSubject, lastPostCreatedUserOrOperatorId, lastPostCreateUserOrOperatorName,lastPostCreatedUserOrOperatorIfDeleted,
            lastPostPostTime, ifAllowPostNeedingReplayTopic, ifAllowPostNeedingPayTopic)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public void Update(string name, string description, int oldCategoryId, int newCategoryId, EnumForumStatus forumStatus, int[] moderatorIds, bool ifAllowPostNeedingReplayTopic, bool ifAllowPostNeedingPayTopic)
        {
            CheckUpdatePermission();
            base.Update(name, description, oldCategoryId, newCategoryId, forumStatus, moderatorIds, _operatingUserOrOperator, ifAllowPostNeedingReplayTopic, ifAllowPostNeedingPayTopic);            
        }

        private void CheckUpdatePermission()
        {
            if (CommFun.IfOperator(_operatingUserOrOperator))
            {
                if (!((OperatorWithPermissionCheck)_operatingUserOrOperator).IfAdmin)
                    ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        private void CheckDeletePermission()
        {
            if (CommFun.IfOperator(_operatingUserOrOperator))
            {
                if (!((OperatorWithPermissionCheck)_operatingUserOrOperator).IfAdmin)
                    ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            }
        }

        public override void IncreaseNumberOfTopicsByOne()
        {
            base.IncreaseNumberOfTopicsByOne();
        }

        public override void IncreaseNumberOfPostsByOne()
        {
            base.IncreaseNumberOfPostsByOne();
        }

        public override void IncreaseNumberOfPosts(int numberOfPosts)
        {
            base.IncreaseNumberOfPosts(numberOfPosts);
        }

        public override void DecreaseNumberOfTopicsByOne()
        {
            base.DecreaseNumberOfTopicsByOne();
        }

        public override void DecreaseNumberOfPostsByOne()
        {
            base.DecreaseNumberOfPostsByOne();
        }

        public override void DecreaseNumberOfPosts(int numberOfPosts)
        {
            base.DecreaseNumberOfPosts(numberOfPosts);
        }

        public override void UpdateLastCreateInfo(int lastCreateUserOrOperatorId, DateTime lastCreateTime, int lastPostId, int lastPostTopicId, string lastPostSubject)
        {
            base.UpdateLastCreateInfo(lastCreateUserOrOperatorId, lastCreateTime, lastPostId,lastPostTopicId, lastPostSubject);
        }

        public PostWithPermissionCheck GetLastPost()
        {
            return base.GetLastPost(this._operatingUserOrOperator);
        }

        public override void Sort(EnumSortMoveDirection sortDirection)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            base.Sort(sortDirection);
        }

        /*--------------------------2.0----------------------------*/

        public UserGroupsOfForumWithPermissionCheck GetUserGroups()
        {
            return base.GetUserGroups(_operatingUserOrOperator);
        }

        public UserReputationGroupsOfForumWithPermissionCheck GetUserReputationGroups()
        {
            return base.GetUserReputationGroups(_operatingUserOrOperator);
        }

        public PostsOfForumWithPermissionCheck GetPosts()
        {
            return base.GetPosts(this._operatingUserOrOperator);
        }

        public CategoryWithPermissionCheck GetCategory()
        {
            return base.GetCategory(_operatingUserOrOperator);
        }

        public TopicsOfForumWithPermissionCheck GetTopics()
        {
            return base.GetTopics(_operatingUserOrOperator);
        }

        public AnnouncementsOfForumWithPermissionCheck GetAnnouncements()
        {
            return base.GetAnnouncements(_operatingUserOrOperator);
        }
        
    }
}
