#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.Bussiness;

namespace Com.Comm100.Forum.Process
{
    public class GroupPermissioniProcess
    {
        /*------No Reference----------
        public static void UpdateUserGroupPermission(
            int groupId,
            bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost, int minIntervalForPost, int maxLengthOfPost,
            bool ifPostNotNeedModeration, bool ifAllowCustomizeAvatar, int maxLengthofSignature, bool ifAllowHTML, bool ifAllowUrl,
            bool ifAllowUploadImage, bool ifAllowUploadAttachment, int maxCountOfAttacmentsForOnePost, int maxSizeOfOneAttachment,
            int maxSizeOfAllAttachments, int maxCountOfMessageSendOneDay, bool ifAllowSearch, int minIntervalForSearch, int siteId,
            int userOrOperatorId
            )
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);

                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, userOrOperatorId);

                UserGroupPermissionWithPermissionCheck ugp = new UserGroupPermissionWithPermissionCheck(conn, transaction, userOrOperator, groupId);

                ugp.Update(
                    ifAllowViewForum, ifAllowViewTopic, ifAllowPost, ifAllowCustomizeAvatar,
                maxLengthofSignature, minIntervalForPost, maxLengthOfPost, ifAllowHTML, ifAllowUrl, ifAllowUploadImage, ifAllowUploadAttachment
                , maxCountOfAttacmentsForOnePost, maxSizeOfOneAttachment, maxSizeOfAllAttachments, maxCountOfMessageSendOneDay, ifAllowSearch
                , minIntervalForSearch, ifPostNotNeedModeration);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
         */

        /*------No Reference----------
        public static void UpdateUserGroupPermissionForForum(int siteId, int operatingUserOrOperatorId, int groupId, int forumId,
            bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost, int minIntervalForPost, int maxLengthOfPost,
            bool ifPostNotNeedModeration, bool ifAllowHTML, bool ifAllowUrl, bool ifAllowUploadImage)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                transaction = conn.SqlConn.BeginTransaction();
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UserGroupPermissionForForumWithPermissionCheck userGroupPermissionForForum = new UserGroupPermissionForForumWithPermissionCheck(conn, transaction, operatingUserOrOperator, groupId, forumId);
                userGroupPermissionForForum.Update(ifAllowViewForum, ifAllowViewTopic, ifAllowPost, minIntervalForPost, maxLengthOfPost, ifPostNotNeedModeration, ifAllowHTML, ifAllowUrl, ifAllowUploadImage);
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void UpdateReputationGroupPermissionForForum(int siteId, int operatingUserOrOperatorId, int groupId, int forumId,
            bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost, int minIntervalForPost, int maxLengthOfPost,
            bool ifPostNotNeedModeration, bool ifAllowHTML, bool ifAllowUrl, bool ifAllowUploadImage)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UserReputationGroupPermissionForForumWithPermissionCheck userReputationGroupPermissionForForum = new UserReputationGroupPermissionForForumWithPermissionCheck(conn, transaction, operatingUserOrOperator, groupId, forumId);
                userReputationGroupPermissionForForum.Update(ifAllowViewForum, ifAllowViewTopic, ifAllowPost, minIntervalForPost, maxLengthOfPost, ifPostNotNeedModeration, ifAllowHTML, ifAllowUrl, ifAllowUploadImage);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }

        }
        */

        public static void UpdateGroupsPermissionInForum(int siteId, int operatingUserOrOperatorId,int forumId, 
            List<int> userGroupIdsInForum,List<GroupPermission> userGroupPermissions,
            List<int> reputationGroupIdsInForum,List<GroupPermission> reputationGroupPermissions)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UserGroupsOfForumWithPermissionCheck userGroupsOfForum = new UserGroupsOfForumWithPermissionCheck(conn, transaction, operatingUserOrOperator, forumId);
                userGroupsOfForum.UpdateUserGroups(userGroupIdsInForum, userGroupPermissions);
                UserReputationGroupsOfForumWithPermissionCheck userReputationGroupsOfForum = new UserReputationGroupsOfForumWithPermissionCheck(conn, transaction, operatingUserOrOperator, forumId);
                userReputationGroupsOfForum.UpdateReputationGroups(reputationGroupIdsInForum, reputationGroupPermissions);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
    }
}
