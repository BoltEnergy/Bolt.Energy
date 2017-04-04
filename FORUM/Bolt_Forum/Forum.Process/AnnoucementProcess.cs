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
using Com.Comm100.Framework.Database;
using System.Data.SqlClient;
using Com.Comm100.Forum.Bussiness;

namespace Com.Comm100.Forum.Process
{
    public class AnnoucementProcess
    {
        public static void AddAnnoucement(int operatingUserOrOperatorId, int siteId,
            string subject, int postUserOrOperatorId,  string content, int[] ForumIds)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            DateTime dtNow = DateTime.UtcNow;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();


                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                //Add Annoucement
                AnnouncementsOfSiteWithPermissionCheck annoucementOfSite = new AnnouncementsOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                int AnnoucementId = annoucementOfSite.Add(subject, dtNow, content, ForumIds);
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

        public static void UpdateAnnoucement(int operatingUserOrOperatorId, bool ifOperator, int siteId, int AnnoucementId,
            string subject, int postUserOrOperatorId,  string content, int[] ForumIds)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            DateTime dtNow = DateTime.UtcNow;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                AnnouncementWithPermissionCheck annoucement = new AnnouncementWithPermissionCheck(conn, transaction,
                    operatingUserOrOperator, AnnoucementId);
                annoucement.Update(subject, operatingUserOrOperator, dtNow, content, ForumIds);
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

        public static AnnouncementWithPermissionCheck GetAnnoucement(int operatingUserOrOperatorId, int siteId,
           int AnnoucementId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                AnnouncementWithPermissionCheck annoucement = new AnnouncementWithPermissionCheck(conn, transaction,
                    operatingUserOrOperator, AnnoucementId);
                return annoucement;
            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static AnnouncementWithPermissionCheck[] GetAnnoucementsOfSiteByQueryAndPaging(
            int operatingUserOrOperatorId, int siteId,
            int pageIndex, int pageSize, string subject, int forumId, string orderField, string orderMethod, out int count)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                //AnnouncementsOfSiteWithPermissionCheck annoucementofSite = new AnnouncementsOfSiteWithPermissionCheck(conn, transaction,
                //    operatingUserOrOperator);
                AnnouncementsBase annoucement = AnnoucementsFactory.CreateAnnoucement(
                    forumId, conn, transaction, operatingUserOrOperator);
                AnnouncementWithPermissionCheck[] annoucements = annoucement.GetAnnouncementsByQueryAndPaging(
                    operatingUserOrOperator,
                    pageIndex, pageSize, subject, 
                    orderField, orderMethod, out  count);
                return annoucements;
            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void DeleteAnnoucement(int operatingUserOrOperatorId, int siteId,
            int AnnoucementId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                AnnouncementWithPermissionCheck annoucement = new AnnouncementWithPermissionCheck(conn, transaction,
                    operatingUserOrOperator, AnnoucementId);
                annoucement.Delete();
            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static AnnouncementWithPermissionCheck[] GetAllAnnoucementsOfForum(int operatingUserOrOperatorId, int siteId,
            int forumId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                AnnouncementsOfForumWithPermissionCheck annoucementOfForum = new AnnouncementsOfForumWithPermissionCheck(
                    conn, transaction, operatingUserOrOperator, forumId);
                return annoucementOfForum.GetAllAnnoucementsOfForum();
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

        public static AnnouncementWithPermissionCheck[] GetAnnoucementsOfForumByQueryAndPaging(
            int operatingUserOrOperatorId, int siteId,
            int pageIndex, int pageSize, string subject, int forumId, string orderField, string orderMethod,out int count)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                AnnouncementsBase annoucement = AnnoucementsFactory.CreateAnnoucement(
                    forumId, conn, transaction, operatingUserOrOperator);
                AnnouncementWithPermissionCheck[] annoucements = annoucement.GetAnnouncementsByQueryAndPaging(
                    operatingUserOrOperator,
                    pageIndex, pageSize, subject,
                    orderField, orderMethod,out count);
                return annoucements;
            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void DeleteAnnouncementAndForumRelation(int siteId, int operatingUserOrOperatorId, int forumId, int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                AnnouncementsOfForumWithPermissionCheck announcementOfForum = new AnnouncementsOfForumWithPermissionCheck(conn, transaction, operatingUserOrOperator, forumId);
                announcementOfForum.DeleteAnnouncement(topicId, operatingUserOrOperator);
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

        #region 
        public static AnnouncementWithPermissionCheck[] GetAnnouncementsByModeratorWithQueryAndPaging(int siteId,int operatingUserOrOperatorId,int pageIndex,int pageSize,string subject,string orderField,string orderDirection)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                AnnouncementsOfSiteWithPermissionCheck announcementOfSite = new AnnouncementsOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return announcementOfSite.GetAnnouncementsByModeratorWithQueryAndPaging(operatingUserOrOperator, pageIndex, pageSize, subject, orderField, orderDirection);
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

        public static int GetCountOfAnnouncementsByModerator(int siteId, int operatingUserOrOperatorId, string subject)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                AnnouncementsOfSiteWithPermissionCheck announcementOfSite = new AnnouncementsOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return announcementOfSite.GetCountOfAnnouncementByModeratorWithQuery(operatingUserOrOperator, subject);
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

        public static void DeleteAnnouncementByModeratorWithAllForum(int siteId, int operatingUserOrOperatorId, int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                AnnouncementsOfSiteWithPermissionCheck announcementOfSite = new AnnouncementsOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                announcementOfSite.DeleteAnnouncementByModerator(topicId);
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

        public static void DeleteAnnouncementByModeratorOrAdmin(int siteId, int operatingUserOrOperatorId,int forumId, int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                AnnouncementsOfSiteWithPermissionCheck announcementOfSite = new AnnouncementsOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                announcementOfSite.DeleteAnnoucementByModeratorOrAdmin(forumId,topicId);
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
        #endregion

        public static ModeratorWithPermissionCheck[] GetModeratorsOfAnnoucement(int annoucementId, int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = operatingUserOrOperatorId == 0 ? null : UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                ModeratorsOfAnnoucement moderatorsOfAnnoucement = new ModeratorsOfAnnoucement(conn, transaction, annoucementId);
                return moderatorsOfAnnoucement.GetAllModreators(operatingUserOrOperator);
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

         public static bool IfModeratorOfAnnoucement(int annoucementId, int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = operatingUserOrOperatorId == 0 ? null : UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                ModeratorsOfAnnoucement moderatorsOfAnnoucement = new ModeratorsOfAnnoucement(conn, transaction, annoucementId);
                return moderatorsOfAnnoucement.IfModerator(operatingUserOrOperator);
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

    }
}
