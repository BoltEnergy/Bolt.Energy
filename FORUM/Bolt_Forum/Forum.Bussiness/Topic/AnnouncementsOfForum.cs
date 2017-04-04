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
    public abstract class AnnouncementsOfForum : AnnouncementsBase
    {
        private int _forumId;
        public int ForumId
        {
            get { return this._forumId; }
        }

        public AnnouncementsOfForum(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
            : base(conn, transaction)
        {
            _forumId = forumId;
        }

        protected AnnouncementWithPermissionCheck[] GetAllAnnoucementsOfForum()
        {
            List<AnnouncementWithPermissionCheck> annoucements = new List<AnnouncementWithPermissionCheck>();
            DataTable dt = AnnoucementAccess.GetAllAnnoucementsOfForum(_conn, _transaction, _forumId);
            foreach (DataRow dr in dt.Rows)
            {
                #region Data Init
                int AnnoucementId = Convert.ToInt32(dr["id"]);
                string AnnoucementSubject = Convert.ToString(dr["Subject"]);
                //DateTime beginDate = Convert.ToDateTime(dr["AnnouncementStartDate"]);
                //DateTime expireDate = Convert.ToDateTime(dr["AnnouncementEndDate"]);
                DateTime createTime = Convert.ToDateTime(dr["PostTime"]);
                int createUserId = Convert.ToInt32(dr["PostUserOrOperatorId"]); ;
                string createUser = Convert.ToString(dr["PostUserOrOperatorName"]);
                bool ifCreateUserIfDeleted = Convert.ToBoolean(dr["PostUserOrOperatorIfDeleted"]);
                int lastPostId = Convert.ToInt32(dr["LastPostId"]);
                string lastPostUserName = Convert.ToString(dr["LastPostUserOrOperatorName"]);
                int lastPostUserId = Convert.ToInt32(dr["LastPostUserOrOperatorId"]);
                DateTime lastPostTime = Convert.ToDateTime(dr["LastPostTime"]);
                bool ifLastPostUserIfDeleted = Convert.ToBoolean(dr["LastPostUserOrOperatorIfDeleted"]);
                //int NumOfReplies = Convert.ToInt32(dr["NumberOfReplies"]);//
                int NumOfReplies = PostAccess.GetCountOfNotDeletedPostsByTopicId(_conn, _transaction, Convert.ToInt32(dr["TopicId"]));
                int NumOfHits = Convert.ToInt32(dr["NumberOfHits"]);
                #endregion

                AnnouncementWithPermissionCheck annoucement =
                    new AnnouncementWithPermissionCheck(
                        _conn, _transaction,
                        AnnoucementId, AnnoucementSubject,
                        createTime, createUserId, createUser,
                        ifCreateUserIfDeleted,lastPostId,
                        lastPostTime,lastPostUserId,
                        lastPostUserName,ifLastPostUserIfDeleted,
                        NumOfReplies,NumOfHits,"");
                annoucements.Add(annoucement);
            }
            return annoucements.ToArray<AnnouncementWithPermissionCheck>();
        }

        public override AnnouncementWithPermissionCheck[] GetAnnouncementsByQueryAndPaging(UserOrOperator operatingUserOrOperator,
          int pageIndex, int pageSize, string subject, string orderField, string orderMethod, out int count)
        {
            count = AnnoucementAccess.GetCountOfAnnoucementsOfSiteByQueryAndPaging(_conn, _transaction, subject, _forumId);
            DataTable dt = AnnoucementAccess.GetAnnoucementsOfSiteByQueryAndPaging(_conn, _transaction,
                subject,_forumId, pageIndex, pageSize, orderField, orderMethod);
            List<AnnouncementWithPermissionCheck> annoucements = new List<AnnouncementWithPermissionCheck>();
            foreach (DataRow dr in dt.Rows)
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
                        AnnoucementId, AnnoucementSubject, 
                        createTime, createUserId, createUser,
                        false, -1, new DateTime(), -1, "", false, -1, -1, "");//won't use value
                annoucements.Add(annoucement);

            }
            return annoucements.ToArray();
        }

        //when delete announcement from forum
        public virtual void DeleteAnnouncement(int id,UserOrOperator operatingUserOrOperator)
        {
            AnnoucementAccess.DeleteForumsAndAnnouncementRelation(_conn, _transaction, _forumId, id);
            if(!AnnoucementAccess.CheckAnnouncementRelation(_conn,_transaction,id))
            {
                AnnoucementAccess.DeleteAnnoucement(_conn, _transaction, id);
                PostsOfTopicWithPermissionCheck posts = new PostsOfTopicWithPermissionCheck(
                _conn, _transaction, id, operatingUserOrOperator);
                posts.DeleteAllPostsOfAnnoucement();
            }
            //throw new NotImplementedException();
        }

        //when delete forum.
        public void DeleteAllAnnouncement(UserOrOperator operatingUserOrOperator)
        {
            AnnouncementWithPermissionCheck[] announcements=GetAllAnnoucementsOfForum();
            for (int i = 0; i < announcements.Length; i++)
            {
                int topicId = announcements[i].TopicId;
                DeleteAnnouncement(topicId, operatingUserOrOperator);
            }
        }
    }
}
