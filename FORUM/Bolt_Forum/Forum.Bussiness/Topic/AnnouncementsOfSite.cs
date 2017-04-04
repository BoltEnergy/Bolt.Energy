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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class AnnouncementsOfSite : AnnouncementsBase
    {
        public AnnouncementsOfSite(SqlConnectionWithSiteId conn, SqlTransaction transaction)
            : base(conn, transaction)
        { }

        public override AnnouncementWithPermissionCheck[] GetAnnouncementsByQueryAndPaging(UserOrOperator operatingUserOrOperator,
            int pageIndex, int pageSize, string subject, string orderField, string orderMethod, out int count)
        {
            count = AnnoucementAccess.GetCountOfAnnoucementsOfSiteByQueryAndPaging(_conn, _transaction, subject, -1);
            DataTable dt = AnnoucementAccess.GetAnnoucementsOfSiteByQueryAndPaging(_conn, _transaction,
                subject, -1, pageIndex, pageSize, orderField, orderMethod);
            List<AnnouncementWithPermissionCheck> annoucements = new List<AnnouncementWithPermissionCheck>();
            foreach (DataRow dr in dt.Rows)
            {
                int AnnoucementId = Convert.ToInt32(dr["TopicId"]);
                string AnnoucementSubject = Convert.ToString(dr["Subject"]);
                //DateTime beginDate = Convert.ToDateTime(dr["AnnouncementStartDate"]);
                //DateTime expireDate = Convert.ToDateTime(dr["AnnouncementEndDate"]);
                DateTime createTime = Convert.ToDateTime(dr["PostTime"]);
                int createUserId = Convert.ToInt32(dr["PostUserOrOperatorId"]); ;
                string createUser = Convert.ToString(dr["Name"]);

                AnnouncementWithPermissionCheck annoucement =
                    new AnnouncementWithPermissionCheck(
                        _conn, _transaction,
                        AnnoucementId, AnnoucementSubject, 
                         createTime, createUserId, createUser,
                        false, -1, new DateTime(), -1, "", false, -1, -1, "");//won't use value
                annoucements.Add(annoucement);

            }
            return annoucements.ToArray();
        }

        protected void Delete(int id, UserOrOperator operatingUserOrOperator)
        {
            AnnouncementWithPermissionCheck annoucement = new AnnouncementWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, id);
            annoucement.Delete();

        }

        protected int Add(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            string subject, UserOrOperator operatingOperator, DateTime PostTime,  string content, int[] ForumIds)
        {
            return Announcement.Add(conn, transaction,
                subject, operatingOperator, PostTime,  content, ForumIds);

        }

        #region Moderator
        public AnnouncementWithPermissionCheck[] GetAnnouncementsByModeratorWithQueryAndPaging(UserOrOperator operatingUserOrOperator,
            int pageIndex, int pageSize, string subject, string orderField, string orderDirection)
        {
            DataTable table = new DataTable();
            table = AnnoucementAccess.GetAnnouncementsByModeratorWithQueryAndPaging(_conn, _transaction, operatingUserOrOperator.Id, subject, pageIndex, pageSize, orderField, orderDirection);
            List<AnnouncementWithPermissionCheck> annoucements = new List<AnnouncementWithPermissionCheck>();
            foreach (DataRow dr in table.Rows)
            {
                int AnnoucementId = Convert.ToInt32(dr["id"]);
                string AnnoucementSubject = Convert.ToString(dr["Subject"]);
                DateTime beginDate = Convert.ToDateTime(dr["AnnouncementStartDate"]);
                DateTime expireDate = Convert.ToDateTime(dr["AnnouncementEndDate"]);
                DateTime createTime = Convert.ToDateTime(dr["PostTime"]);
                int createUserId = Convert.ToInt32(dr["PostUserOrOperatorId"]); ;
                string createUser = Convert.ToString(dr["Name"]);

                AnnouncementWithPermissionCheck annoucement =
                    new AnnouncementWithPermissionCheck(
                        _conn, _transaction,
                        AnnoucementId, AnnoucementSubject, createTime, createUserId, createUser,
                        false, -1, new DateTime(), -1, "", false, -1, -1, "");//won't use value
                annoucements.Add(annoucement);

            }
            return annoucements.ToArray();
        }

        public int GetCountOfAnnouncementByModeratorWithQuery(UserOrOperator operatingUserOrOperator, string subject)
        {
            return AnnoucementAccess.GetCountOfAnnouncementsByModeratorWithQuery(_conn, _transaction, operatingUserOrOperator.Id, subject);
        }

        public void DeleteAnnouncementByModerator(UserOrOperator operatingUserOrOperator, int topicId)
        {
            AnnoucementAccess.DeleteForumsAndAnnouncementRelationByModeratorWithAnnouncement(_conn, _transaction,
                topicId, operatingUserOrOperator.Id);
            if (!AnnoucementAccess.CheckAnnouncementRelation(_conn, _transaction, topicId))
            {
                AnnoucementAccess.DeleteAnnoucement(_conn, _transaction, topicId);
                PostsOfTopicWithPermissionCheck posts = new PostsOfTopicWithPermissionCheck(
                _conn, _transaction, topicId, operatingUserOrOperator);
                posts.DeleteAllPostsOfAnnoucement();
            }
        }

        public void DeleteAnnoucementByModeratorOrAdmin(UserOrOperator operatingUserOrOperator,int forumId, int topicId)
        {
            AnnoucementAccess.DeleteForumsAndAnnouncementRelation(_conn, _transaction, forumId, topicId);
            if (!AnnoucementAccess.CheckAnnouncementRelation(_conn, _transaction, topicId))
            {
                AnnoucementAccess.DeleteAnnoucement(_conn, _transaction, topicId);
                PostsOfTopicWithPermissionCheck posts = new PostsOfTopicWithPermissionCheck(
                _conn, _transaction, topicId, operatingUserOrOperator);
                posts.DeleteAllPostsOfAnnoucement();
            }

        }

        private bool IfAdmin(UserOrOperator operatingUserOrOperator)
        {
            //if (CommFun.IfOperator(operatingUserOrOperator))
            //    if ((operatingUserOrOperator as OperatorWithPermissionCheck).IfAdmin)
            //        return true;
            //if (operatingUserOrOperator.IfForumAdmin)
            //    return true;
            //return false;
            return CommFun.IfAdminInUI(operatingUserOrOperator);
        }
        #endregion
    }
}
