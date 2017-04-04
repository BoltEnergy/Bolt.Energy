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
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Database;

namespace Com.Comm100.Forum.Process
{
    public class UserReputationGroupProcess
    {
        public static UserReputationGroupWithPermissionCheck[] GetAllGroups(int siteId, int operatingOperatorId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);

                DbHelper.OpenConn(conn);

                UserOrOperator user = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingOperatorId);

                UserReputationGroupsWithPermissionCheck groups = new UserReputationGroupsWithPermissionCheck(conn, null, user);

                return groups.GetAllGroups(user);
            }
            catch
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void AddReputationGroupToForum(int groupId, int forumId, int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();
                UserOrOperator user = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UserReputationGroupsOfForumWithPermissionCheck userReputationGroupsOfForum = new UserReputationGroupsOfForumWithPermissionCheck(conn, transaction, user, forumId);
                userReputationGroupsOfForum.Add(groupId);
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

        public static int Add(string name, string description, int limitedBegin, int limitedExpire, int icoRepeat, int siteId, int operatingOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                name = name.Trim();
                description = description.Trim();

                conn = DbHelper.GetSqlConnection(siteId);

                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingOperatorId);

                transaction = conn.SqlConn.BeginTransaction();
                UserReputationGroupsWithPermissionCheck reputationGroups = new UserReputationGroupsWithPermissionCheck(conn, transaction, operatingUserOrOperator);

                int identity = reputationGroups.Add(name, description, limitedBegin, limitedExpire, icoRepeat);

                transaction.Commit();

                return identity;
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

        public static void Update(int groupId, string name, string description, int limitedBegin, int limitedExpire, int icoRepeat, int siteId, int operatingOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                name = name.Trim();
                description = description.Trim();
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingOperatorId);
                UserReputationGroupWithPermissionCheck group = new UserReputationGroupWithPermissionCheck(conn, transaction, operatingUserOrOperator, groupId);

                group.Update(name, description, limitedBegin, limitedExpire, icoRepeat);

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

        public static void Delete(int groupId, int siteId, int operatingOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingOperatorId);
                UserReputationGroupsWithPermissionCheck groups = new UserReputationGroupsWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                groups.Delete(groupId);

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
        public static void Delete(int groupId, int forumId, int siteId, int operatingOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingOperatorId);
                UserReputationGroupsOfForumWithPermissionCheck userReputationGroupsOfForum = new UserReputationGroupsOfForumWithPermissionCheck(conn, transaction, userOrOperator, forumId);
                userReputationGroupsOfForum.Delete(groupId);
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

        public static UserReputationGroupWithPermissionCheck GetGroupById(int groupId, int siteId, int operatingOperatorId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);

                DbHelper.OpenConn(conn);

                UserOrOperator user = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingOperatorId);
                return new UserReputationGroupWithPermissionCheck(conn, null, user, groupId);
            }
            catch
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static UserReputationGroupWithPermissionCheck GetReputationGroupByUserOrOperatorId(int siteId, int operatingOperatorId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator user = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingOperatorId);
                UserReputationGroupsWithPermissionCheck groups = new UserReputationGroupsWithPermissionCheck(conn, null, user);
                return groups.GetUserReputationGroupOfUserOrOperator(operatingOperatorId);
            }
            catch
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static UserReputationGroupPermissionWithPermissionCheck GetPermissionByGroupId(int siteId, int operatingUserOrOperatorId, int groupId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UserReputationGroupWithPermissionCheck group = new UserReputationGroupWithPermissionCheck(conn, transaction, operatingUserOrOperator, groupId);
                return group.GetPermission();
            }
            catch
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void UpdateGroupPermission(
            int groupId, int siteId, bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost,
            bool ifAllowCustomizeAvatar, int maxLengthofSignature, 
            bool ifSignatureAllowUrl,bool ifSignatureAllowInserImage,
            int minIntervalForPost,
            int maxLengthOfPost,bool ifAllowUrl, bool ifAllowUploadImage, bool ifAllowUploadAttachment,
            int maxCountOfAttacmentsForOnePost, int maxSizeOfOneAttachments, int maxSizeOfAllAttachments, int maxCountOfMessageSendOneDay,
            bool ifAllowSearch, int minIntervalForSearch, bool ifPostNotNeedModeration, int operatingOperatorId)
        {
            SqlConnectionWithSiteId conn = null;

            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingOperatorId);
                UserReputationGroupWithPermissionCheck userGroup = new UserReputationGroupWithPermissionCheck(conn, transaction, operatingUserOrOperator, groupId);

                UserReputationGroupPermissionWithPermissionCheck groupPermission = userGroup.GetPermission();

                groupPermission.Update(ifAllowViewForum, ifAllowViewTopic, ifAllowPost, ifAllowCustomizeAvatar,
                maxLengthofSignature,ifSignatureAllowUrl,ifSignatureAllowInserImage,
                minIntervalForPost, maxLengthOfPost, ifAllowUrl, ifAllowUploadImage, ifAllowUploadAttachment,
                maxCountOfAttacmentsForOnePost, maxSizeOfOneAttachments, maxSizeOfAllAttachments, maxCountOfMessageSendOneDay, ifAllowSearch,
                minIntervalForSearch, ifPostNotNeedModeration);

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

        public static UserReputationGroupWithPermissionCheck[] GetGroupsNotInForum(int forumId, int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);

                DbHelper.OpenConn(conn);

                UserOrOperator user = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                UserReputationGroupsWithPermissionCheck group = new UserReputationGroupsWithPermissionCheck(conn, null, user);

                return group.GetGroupsNotInForum(forumId, user);
            }
            catch
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static UserReputationGroupWithPermissionCheck[] GetReputationGroupsByForumId(int forumId, int siteId, int operatingOperatorId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);

                DbHelper.OpenConn(conn);

                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingOperatorId);

                UserReputationGroupsWithPermissionCheck urgs = new UserReputationGroupsWithPermissionCheck(conn, null, userOrOperator);

                return urgs.GetGroupsByForumId(forumId, userOrOperator);

            }
            catch
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static UserReputationGroupPermissionForForumWithPermissionCheck GetUserReputationGroupPermissionForForum(int siteId, int operatingUserOrOperatorId, int groupId, int forumId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UserReputationGroupOfForumWithPermissionCheck userReputationGroupOfForum = new UserReputationGroupOfForumWithPermissionCheck(conn, transaction, operatingUserOrOperator, groupId, forumId);
                return userReputationGroupOfForum.GetPermission();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
    }
}
