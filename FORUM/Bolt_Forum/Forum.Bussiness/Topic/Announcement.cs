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
using Com.Comm100.Framework.FieldLength;
using System.Web;
using Com.Comm100.Framework.ASPNETState;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class Announcement : TopicBase
    {
        #region private fields
        //private DateTime _beginDate;
        //private DateTime _expireDate;
        private string _content;
        #endregion

        #region properties
        //public DateTime BeginDate
        //{
        //    get { return this._beginDate; }
        //}
        //public DateTime ExpireDate
        //{
        //    get { return this._expireDate; }
        //}
        public string Content
        {
            get { return _content; }
        }
        #endregion

        public Announcement(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id)
            : base(conn, transaction)
        {
            DataTable table = new DataTable();
            table = TopicAccess.GetTopicByTopicId(_conn, _transaction, id);
            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowTopicNotExistException(id);
            }
            else
            {
                _topicId = id;
                _subject = Convert.ToString(table.Rows[0]["Subject"]);
                _postUserOrOperatorId = table.Rows[0]["PostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[0]["PostUserOrOperatorId"]);
                _postTime = Convert.ToDateTime(table.Rows[0]["PostTime"]);
                //_beginDate = Convert.ToDateTime(table.Rows[0]["AnnouncementStartDate"]);
                //_expireDate = Convert.ToDateTime(table.Rows[0]["AnnouncementEndDate"]);
                _postUserOrOperatorName = Convert.ToString(table.Rows[0]["PostUserOrOperatorName"]);
                _postUserOrOperatorIfDeleted = Convert.ToBoolean(table.Rows[0]["PostUserOrOperatorIfDeleted"]);
                _lastPostId = Convert.ToInt32(table.Rows[0]["LastPostId"]);
                _lastPostTime = Convert.ToDateTime(table.Rows[0]["LastPostTime"]);
                _lastPostUserOrOperatorId = Convert.ToInt32(table.Rows[0]["LastPostUserOrOperatorId"]); 
                _lastPostUserOrOperatorName =Convert.ToString(table.Rows[0]["LastPostUserOrOperatorName"]);
                _lastPostUserOrOperatorIfDeleted = Convert.ToBoolean(table.Rows[0]["LastPostUserOrOperatorIfDeleted"]
                    is System.DBNull ? 0 : table.Rows[0]["LastPostUserOrOperatorIfDeleted"]); 
                _numberOfReplies = Convert.ToInt32(table.Rows[0]["NumberOfReplies"]);
                _numberOfHits = Convert.ToInt32(table.Rows[0]["NumberOfHits"]);

                PostsOfTopicWithPermissionCheck posts = GetPosts(null);
                PostWithPermissionCheck post = posts.GetFirstPost();
                _content = post.Content;
            }
        }

        public Announcement(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int id,string subject,DateTime postTime,
            int postUserOrOperatorId,string postUserOrOperatorName,bool postUserOrOperatorIfDeleted,
            int lastPostId,DateTime lastPostTime,int lastPostUserOrOperatorId,
            string lastPostUserOrOperatorName,bool lastPostUserOrOperatorIfDeleted,
            int numberOfReplies,int numberOfHits,string content
            ):base(id,subject,postTime,postUserOrOperatorId,postUserOrOperatorName,
            postUserOrOperatorIfDeleted,lastPostId,lastPostTime,lastPostUserOrOperatorId,
            lastPostUserOrOperatorName,lastPostUserOrOperatorIfDeleted,numberOfReplies,
            numberOfHits) 
        {
            _conn = conn;
            _transaction = transaction;
            
            _content = content;
        }

        public static int Add(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            string subject, UserOrOperator operatingUserOrOperator, DateTime PostTime, 
             string content, int[] ForumIds)
        {
            CheckFieldsLength(subject, content);
            //add Annoucement
            int AnnoucementId = AnnoucementAccess.AddAnnoucement(conn, transaction,
                subject, operatingUserOrOperator.Id, PostTime);
            //add relation
            for (int i = 0; i < ForumIds.Length; i++)
            {
               AnnoucementAccess.AddForumsAndAnnoucementRelateion(conn, transaction, AnnoucementId, ForumIds[i]);
            }
            //add content
            PostsOfTopicWithPermissionCheck posts = new PostsOfTopicWithPermissionCheck(conn, transaction, AnnoucementId, operatingUserOrOperator);
            posts.AddAnnouncementPost(true, subject, PostTime, content);
            return AnnoucementId;
        }
       
        public virtual void IncreaseNumberOfHitsByOne()
        {
            TopicAccess.UpdateTopicNumberOfHits(_conn, _transaction, _topicId, _numberOfHits + 1);
        }
        protected virtual void IncreaseNumberOfRepliesByOne(bool ifTopic, UserOrOperator operatingOperator)
        {
            if (!ifTopic)
            {
                TopicAccess.UpdateTopicNumberOfReplies(this._conn, this._transaction, this._topicId, this._numberOfReplies + 1);
            }
        }
        protected virtual void DecreaseNumberOfRepliesByOne(UserOrOperator operatingOperator)
        {
            TopicAccess.UpdateTopicNumberOfReplies(_conn, _transaction, _topicId, _numberOfReplies - 1);
        }
        protected virtual void UpdateLastPostInfo(int lastPostUserOrOperatorId, int lastPostId, DateTime lastPostTime, string subject, UserOrOperator operatingOperator)
        {
            TopicAccess.UpdateTopicLastPostInfo(this._conn, this._transaction, this._topicId, lastPostUserOrOperatorId, lastPostId, lastPostTime);
        }
        public virtual void UpdateParticipatorIds(int newParticipatorId)
        {
            //int length = 0;
            //if (_participatorIds != null)
            //{
            //    length = _participatorIds.Length;
            //}
            //int[] newParticipatorIds = new int[length + 1];
            //for (int i = 0; i < length; i++)
            //{
            //    newParticipatorIds[i] = _participatorIds[i];
            //}
            //newParticipatorIds[length] = newParticipatorId;

            //TopicAccess.UpdateTopicParticipatorIds(_conn, _transaction, _topicId, newParticipatorIds);
        }
        public bool IfBelongToSite()
        {
            return false;
        }

        protected ForumsOfAnnouncementWithPermissionCheck GetForums(UserOrOperator operatingUserOrOperator)
        {
            ForumsOfAnnouncementWithPermissionCheck forumsOfAnnoucement = new ForumsOfAnnouncementWithPermissionCheck(_conn, _transaction,
                operatingUserOrOperator, _topicId);
            return forumsOfAnnoucement;
        }

        protected void Delete(UserOrOperator operatingUserOrOperator)
        {
            //delete posts
            PostsOfTopicWithPermissionCheck posts = new PostsOfTopicWithPermissionCheck(
                _conn, _transaction, _topicId, operatingUserOrOperator);
            posts.DeleteAllPostsOfAnnoucement();
            //delete annoucement
            AnnoucementAccess.DeleteAnnoucement(_conn, _transaction, _topicId);
            //delete relation
            ForumsOfAnnouncementWithPermissionCheck forumsOfAnnoucement = new ForumsOfAnnouncementWithPermissionCheck(
                _conn, _transaction, operatingUserOrOperator, _topicId);

            foreach (var forum in forumsOfAnnoucement.GetAllForums())
            {
                FavoriteAccess.DeleteFavoriteByAnnoucementId(_conn, _transaction, this.TopicId, forum.ForumId);
            }
            
            forumsOfAnnoucement.DeleteForumsAndAnnoucementRelation();
        }

        protected void Update(string subject, UserOrOperator operatingUserOrOperator,
            DateTime PostTime,  string content, int[] ForumIds)
        {
            CheckFieldsLength(subject, content);
            //update annoucement
            AnnoucementAccess.UpdateAnnoucement(_conn, _transaction, _topicId,
                subject, PostUserOrOperatorId, PostTime);
            //update annoucement content
            PostsOfTopicWithPermissionCheck posts = new PostsOfTopicWithPermissionCheck(
                _conn, _transaction,_topicId, operatingUserOrOperator);
            PostWithPermissionCheck post = posts.GetFirstPost();
            post.UpdateAnnoucementPost(subject, content, operatingUserOrOperator.Id,PostTime,ForumIds[0]);

            //update all relation
            ForumsOfAnnouncementWithPermissionCheck forumsOfAnnoucement = this.GetForums(operatingUserOrOperator);

            ForumWithPermissionCheck[] forums = forumsOfAnnoucement.GetAllForums();

            var annoucementWithFavoriteRelationToRemove = from forum in forums
                                                          where !ForumIds.Contains(forum.ForumId)
                                                          select forum.ForumId;

            foreach (var forumid in annoucementWithFavoriteRelationToRemove)
            {
                FavoriteAccess.DeleteFavoriteByAnnoucementId(_conn, _transaction, this.TopicId, forumid);
            }

            forumsOfAnnoucement.DeleteForumsAndAnnoucementRelation();

            foreach (int forumid in ForumIds)
            {
                forumsOfAnnoucement.AddForumsAndAnnoucementRelation(forumid);
            }

            
        }
    }
}
