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
    public abstract class ForumsOfAnnouncement : ForumsBase
    {
        private int _topicId;

        public int TopicId
        {
            get { return this._topicId; }
        }

        public ForumsOfAnnouncement(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
            : base(conn, transaction)
        {
            _topicId = topicId;
        }

        protected ForumWithPermissionCheck[] GetAllForums(UserOrOperator operatingUserOrOperator
            , out string[] forumPaths)
        {

            DataTable dt = ForumAccess.GetAllForumsOfAnnoucement(_conn, _transaction, TopicId);
            List<ForumWithPermissionCheck> forums = new List<ForumWithPermissionCheck>();
            List<string> lforumPaths = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                //int ForumId = Convert.ToInt32(dr["CategoryOrForumId"]);
                ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, Convert.ToInt32(dr["Id"]), operatingUserOrOperator,
                    Convert.ToInt32(dr["CategoryId"]), Convert.ToInt32(dr["OrderId"]),
                    Convert.ToString(dr["Name"]), (EnumForumStatus)Convert.ToInt16((dr["Status"])),
                    Convert.ToInt32(dr["NumberOfTopics"]), Convert.ToInt32(dr["NumberOfPosts"]),
                    Convert.ToString(dr["Description"]), Convert.ToInt32(dr["LastPostId"]),
                    Convert.ToInt32(dr["LastPostTopicId"]),
                    Convert.ToString(dr["LastPostSubject"]),
                    //dr["LastPostCreatedUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(dr["LastPostCreatedUserOrOperatorId"]),
                    //dr["LastPostCreatedUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(dr["LastPostCreatedUserOrOperatorName"]),
                    //dr["LastPostCreatedUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(dr["LastPostCreatedUserOrOperatorIfDeleted"]),
                    //Convert.ToDateTime(dr["LastPostPostTime"]),
                    0,"",false,new DateTime(),
                    Convert.ToBoolean(dr["IfAllowPostNeedingReplayTopic"]),
                    Convert.ToBoolean(dr["IfAllowPostNeedingPayTopic"]));
                CategoryWithPermissionCheck category = forum.GetCategory();
                lforumPaths.Add(category.Name + "/" + forum.Name);
                forums.Add(forum);
            }
            forumPaths = lforumPaths.ToArray<string>();
            return forums.ToArray<ForumWithPermissionCheck>();
        }

        protected void DeleteForumsAndAnnoucementRelation()
        {
            AnnoucementAccess.DeleteAllForumsAndAnnoucementRelationWithAnnouncement(_conn, _transaction, TopicId);
        }

        protected void AddForumsAndAnnoucementRelation(int forumId)
        {
            AnnoucementAccess.AddForumsAndAnnoucementRelateion(_conn, _transaction, TopicId, forumId);
        }

        protected void DeleteForumsAndAnnoucementRelation(int moderatorId)
        {
            AnnoucementAccess.DeleteForumsAndAnnouncementRelationByModeratorWithAnnouncement(_conn, _transaction, TopicId, moderatorId);
        }

        protected void DeleteForumsAndAnnoucementRelationWithForumId(int forumId)
        {
            AnnoucementAccess.DeleteForumsAndAnnouncementRelation(_conn, _transaction, forumId, TopicId);
        }
    }
}
