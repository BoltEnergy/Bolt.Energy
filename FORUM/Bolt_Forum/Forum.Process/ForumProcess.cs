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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;


namespace Com.Comm100.Forum.Process
{
    public class ForumProcess
    {
        public static Com.Comm100.Forum.Bussiness.ForumWithPermissionCheck[] GetNotHiddenForumsByCategoryID(int operatingUserOrOperatorId, int siteId, int categoryId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingOperator = null;
                if (operatingUserOrOperatorId > 0)
                    operatingOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                ForumsOfCategoryWithPermissionCheck forums = new ForumsOfCategoryWithPermissionCheck(conn, transaction, categoryId, operatingOperator);
                return forums.GetAllNotHiddenForums();
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static Com.Comm100.Forum.Bussiness.ForumWithPermissionCheck[] GetForumsByCategoryID(int operatingUserOrOperatorId, int siteId, int categoryId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                ForumsOfCategoryWithPermissionCheck forums = new ForumsOfCategoryWithPermissionCheck(conn, transaction, categoryId, operatingUserOrOperator);
                return forums.GetAllForums();
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static Com.Comm100.Forum.Bussiness.ForumWithPermissionCheck GetForumByForumId(int siteId, int operatingUserOrOperatorId, int ForumId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                transaction = conn.SqlConn.BeginTransaction();

                Com.Comm100.Forum.Bussiness.ForumWithPermissionCheck forum = new Com.Comm100.Forum.Bussiness.ForumWithPermissionCheck(conn, transaction, ForumId, operatingUserOrOperator);

                transaction.Commit();

                return forum;
            }
            catch (System.Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static int AddForum(int operatingUserOrOperatorId, int siteId, string name, string description, int categoryId, int[] moderatorIds, bool ifAllowPostNeedingReplayTopic, bool ifAllowPostNeedingPayTopic)
        {
            name = name.Trim();
            description = description.Trim();

            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                transaction = conn.SqlConn.BeginTransaction();

                ForumsOfCategoryWithPermissionCheck forums = new ForumsOfCategoryWithPermissionCheck(conn, transaction, categoryId, operatingUserOrOperator);

                int forumId = forums.Add(name, description, moderatorIds, ifAllowPostNeedingReplayTopic, ifAllowPostNeedingPayTopic);

                transaction.Commit();

                return forumId;
            }
            catch (Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void UpdateForum(int operatingUserOrOperatorId, int siteId, int forumId, string name, string description, int oldCategoryId, int newCategoryId, EnumForumStatus forumStatus, int[] moderatorIds, bool ifAllowPostNeedingReplayTopic, bool ifAllowPostNeedingPayTopic)
        {
            name = name.Trim();
            description = description.Trim();

            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                transaction = conn.SqlConn.BeginTransaction();

                ForumWithPermissionCheck forum = new ForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);

                forum.Update(name, description, oldCategoryId, newCategoryId, forumStatus, moderatorIds, ifAllowPostNeedingReplayTopic, ifAllowPostNeedingPayTopic);

                transaction.Commit();
            }
            catch (Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void DeleteForum(int operatingUserOrOperatorId, int siteId, int forumId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                ForumWithPermissionCheck forum = new ForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);
                ForumsOfCategoryWithPermissionCheck forums = new ForumsOfCategoryWithPermissionCheck(conn, transaction, forum.CategoryId, operatingUserOrOperator);

                forums.Delete(forumId);

                transaction.Commit();
            }
            catch (Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void SortForums(int operatingUserOrOperatorId, int siteId, int forumId, EnumSortMoveDirection sortDirection)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                transaction = conn.SqlConn.BeginTransaction();

                ForumWithPermissionCheck forum = new ForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);

                forum.Sort(sortDirection);
                transaction.Commit();
            }
            catch (System.Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static ModeratorWithPermissionCheck[] GetModeratorsByForumId(int forumId, int siteId, int operatingUserOrOperatorId, bool ifOperator)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = operatingUserOrOperatorId == 0 ? null : UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);

                ModeratorsWithPermisisonCheck moderators =
                    new ModeratorsWithPermisisonCheck(conn, transaction, forumId, operatingUserOrOperator);

                return moderators.GetAllModerators();
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static ForumWithPermissionCheck[] GetForumsofAnnoucement(
            int AnnoucementId, int siteId, int operatingUserOrOperatorId, out string[] forumPath)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                ForumsOfAnnouncementWithPermissionCheck forumsOfAnnoucement = new ForumsOfAnnouncementWithPermissionCheck(conn, transaction,
                    operatingUserOrOperator, AnnoucementId);
                return forumsOfAnnoucement.GetAllForums(out forumPath);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static ForumWithPermissionCheck[] GetForumsofAnnoucement(
         int AnnoucementId, int siteId, int operatingUserOrOperatorId)
        {
            string[] forumPath;

            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                ForumsOfAnnouncementWithPermissionCheck forumsOfAnnoucement = new ForumsOfAnnouncementWithPermissionCheck(conn, transaction,
                    operatingUserOrOperator, AnnoucementId);
                return forumsOfAnnoucement.GetAllForums(out forumPath);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static ForumWithPermissionCheck[] GetForumsOfSite(int siteId, int operatingUserOrOperatorId, out string[] forumPaths, out int[] forumIds)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                ForumsOfSiteWithPermissionCheck forumsOfSite = new ForumsOfSiteWithPermissionCheck(conn, transaction,
                    operatingUserOrOperator);
                return forumsOfSite.GetAllForums(out forumPaths, out forumIds);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static ForumWithPermissionCheck[] GetForumsOfModerator(int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                ForumsOfModeratorWithPermissionCheck forumsOfModerator = new ForumsOfModeratorWithPermissionCheck(conn, transaction, operatingUserOrOperator, operatingUserOrOperatorId);
                return forumsOfModerator.GetAllForums();
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static EnumForumStatus GetForumStatus(int forumId, int siteId, int operatingUserOrOperatorId, bool ifOperator)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                ForumWithPermissionCheck forum = new ForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);

                return forum.Status;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static bool IfInheritPermission(int siteId, int operatingUserOrOperatorId, int forumId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                ForumWithPermissionCheck forum = new ForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);
                return forum.GetPermissionManager().IfInheritPermission;
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

        public static void UpdateIfInheritPermission(int siteId, int operatingUserOrOperatorId, int forumId, bool ifInheritPermission)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                ForumWithPermissionCheck forum = new ForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);
                forum.UpdateIfInheritPermission(ifInheritPermission);
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