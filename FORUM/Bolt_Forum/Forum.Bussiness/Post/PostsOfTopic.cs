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
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class PostsOfTopic : PostsBase
    {
        private int _topicId;
        public int TopicId
        {
            get { return this._topicId; }
        }

        public PostsOfTopic(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
            : base(conn, transaction)
        {
            _conn = conn;
            _transaction = transaction;
            _topicId = topicId;
        }
        private bool IfAdmin(UserOrOperator operatingUserOrOperator)
        {
            if (operatingUserOrOperator.IfForumAdmin)
                return true;
            if (operatingUserOrOperator is OperatorWithPermissionCheck)
            {
                if ((operatingUserOrOperator as OperatorWithPermissionCheck).IfAdmin == true)
                    return true;
            }
            return false;
        }
        public virtual PostWithPermissionCheck[] GetPostsByPaging(
            int pageIdex, int pageSize, UserOrOperator operatingUserOrOperator,out int postsCount, int forumId)
        {
            bool IfShowDeleted = false;
            if (CommFun.IfAdminInUI(operatingUserOrOperator) || CommFun.IfModeratorInUI(_conn, _transaction, forumId, operatingUserOrOperator))
            {
                IfShowDeleted = true;
            }
            postsCount = PostAccess.GetCountOfPostByTopicId(_conn, _transaction, _topicId, true, true,IfShowDeleted);
            DataTable table = PostAccess.GetPostsByTopicIdAndPaging(_conn, _transaction, _topicId, 
                pageIdex, pageSize,true,true,IfShowDeleted);
            PostWithPermissionCheck[] posts = new PostWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                posts[i] = CreatePostObject(table.Rows[i], operatingUserOrOperator);
            }
            /*Topic Deleted*/
            var firstPost = (from post in posts
                            where post.IfTopic == true
                            select post).FirstOrDefault();
            if (firstPost != null && firstPost.IfDeleted)
            {
                postsCount = 1;
                return new PostWithPermissionCheck[] { firstPost };
            }
            else
                return posts;
        }

        public virtual PostWithPermissionCheck[] GetPostsByPaging(
            int pageIdex, int pageSize, UserOrOperator operatingUserOrOperator)
        {
            DataTable table = PostAccess.GetPostsByTopicIdAndPaging(_conn, _transaction, _topicId,
                pageIdex, pageSize,true,true,false);
            PostWithPermissionCheck[] posts = new PostWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                posts[i] = CreatePostObject(table.Rows[i], operatingUserOrOperator);
            }
            return posts;
        }

        public virtual int GetCountOfPostsByPaging(
           UserOrOperator operatingUserOrOperator,int forumId)
        {
            ////bool IfShowAbusingPost = false;
            ////bool IfShowWatingModerationPost = false;
            //if (!CommFun.IfGuest() && (IfAdmin(operatingUserOrOperator) || CommFun.UserPermissionCache().IfModerator(forumId)))
            //{
            //    //IfShowAbusingPost = true;
            //    //IfShowWatingModerationPost = true;
            //}
            if (CommFun.IfAdminInUI(operatingUserOrOperator) || CommFun.IfModeratorInUI(_conn, _transaction, forumId, operatingUserOrOperator))
                return PostAccess.GetCountOfPostByTopicId(_conn, _transaction, _topicId, true, true, true);
            else
                return PostAccess.GetCountOfPostByTopicId(_conn, _transaction, _topicId, true, true, false);
        }

        public virtual int GetCountOfPostsByPaging()
        {
            return PostAccess.GetCountOfPostByTopicId(_conn, _transaction, _topicId, true, true,false);
        }

        public virtual PostWithPermissionCheck GetAnswer(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = new DataTable();
            table = PostAccess.GetAnswerByTopicId(this._conn, this._transaction, this._topicId);
            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowAnswerNotExistException(this._topicId);
            }
            else
            {
                PostWithPermissionCheck answer = new PostWithPermissionCheck(
                    this._conn, this._transaction, Convert.ToInt32(table.Rows[0]["Id"]), operatingUserOrOperator,
                    this._topicId, Convert.ToBoolean(table.Rows[0]["IfTopic"]), Convert.ToInt32(table.Rows[0]["Layer"]),
                    Convert.ToString(table.Rows[0]["Subject"]), Convert.ToString(table.Rows[0]["Content"]), Convert.ToInt32(table.Rows[0]["PostUserOrOperatorId"]),
                    Convert.ToString(table.Rows[0]["postUserOrOperatorName"]), Convert.ToBoolean(table.Rows[0]["IfCustomizeAvatar"]),
                    Convert.ToString(table.Rows[0]["SystemAvatar"]), Convert.ToString(table.Rows[0]["CustomizeAvatar"]), Convert.ToString(table.Rows[0]["Signature"]), Convert.ToBoolean(table.Rows[0]["IfDeleted"]),
                    Convert.ToInt32(table.Rows[0]["Posts"]), Convert.ToDateTime(table.Rows[0]["JoinedTime"]), Convert.ToDateTime(table.Rows[0]["PostTime"]), true,
                    Convert.ToInt32(table.Rows[0]["LastEditUserOrOperatorId"]), Convert.ToString(table.Rows[0]["lastEditUserOrOperatorName"]),
                    Convert.ToBoolean(table.Rows[0]["IfLastEditUserOrOperatorDeleted"]),
                    Convert.ToDateTime(table.Rows[0]["LastEditTime"]),
                    "",
                    Convert.ToBoolean(table.Rows[0]["IfDeleted"]),
                    Convert.ToInt16(table.Rows[0]["ModerationStatus"]));

                return answer;
            }
            return null;
        }

        protected virtual int Add(int forumId,bool ifTopic,bool ifReplaceDraft, string subject,
            UserOrOperator operatingUserOrOperator, DateTime postTime, string content,
            int[] attachIds, int[] scores, string[] descriptions, bool ifNeedModeration)
        {
            TopicWithPermissionCheck topic = this.GetTopic(operatingUserOrOperator);

            int postId = 0;
            if (ifReplaceDraft && topic.IfHasDraft && operatingUserOrOperator is OperatorWithPermissionCheck)
            {
                postId = topic.PostDraft(false, subject, postTime, content);
            }
            else
            {
                postId = Post.Add(this._conn, this._transaction, this._topicId, ifTopic,
                    subject, operatingUserOrOperator.Id, postTime, content, operatingUserOrOperator, ifNeedModeration);
            }

            /*Update Attachment*/
            if (attachIds != null && scores != null && descriptions != null)
            {
                for (int i = 0; i < attachIds.Length; i++)
                {
                    AttachmentWithPermissionCheck attachment = new AttachmentWithPermissionCheck(_conn, _transaction,
                        operatingUserOrOperator, attachIds[i]);
                    attachment.Update(forumId, postId, scores[i], descriptions[i], EnumAttachmentType.AttachToPost);
                }
            }

            topic.IncreaseNumberOfRepliesByOne(ifTopic);
            topic.UpdateLastPostInfo(operatingUserOrOperator.Id, postId, postTime, subject);
            topic.UpdateParticipatorIds(operatingUserOrOperator.Id);

            if (operatingUserOrOperator != null)
            {
                operatingUserOrOperator.IncreaseNumberPostsByOne();
            }
            else
            {
                ExceptionHelper.ThrowOperatorNotLoginException();
            }
            if (!ifTopic)
            {
                /*2.0 stategy */
                PostWithPermissionCheck post = new PostWithPermissionCheck(_conn, _transaction, postId,
                    operatingUserOrOperator);
                //UserOrOperator User = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, null, topic.PostUserOrOperatorId);
                ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                    _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
                scoreStrategySetting.UseAfterPostReply(post, operatingUserOrOperator);

                /*2.0 reputation strategy*/
                ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                    _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
                reputationStrategySetting.UseAfterPostReply(post, operatingUserOrOperator);

                ///*Send Email If User Subscibe Topic*/
                //SubscribesOfSiteWithPermissionCheck subscrbesOfSite = new SubscribesOfSiteWithPermissionCheck(
                //    _conn, _transaction, operatingUserOrOperator);
                //subscrbesOfSite.SendEmailToUsers(_topicId,postId,EnumQueueEmailType.NewReply,operatingUserOrOperator,postTime);
            }
            return postId;
        }

        protected virtual int AddPostWithMoved(bool ifTopic, string subject,
            UserOrOperator operatingUserOrOperator, int postUserOrOperatorId,DateTime postTime, string content)
        {
            int postId = Post.Add(this._conn, this._transaction, this._topicId, ifTopic,
                   subject, postUserOrOperatorId, postTime, content, operatingUserOrOperator, false);
            return postId;
        }

        protected virtual int AddAnnoucementPost(
            bool ifTopic, string subject, UserOrOperator operatingUserOrOperator, DateTime postTime, string content,
            bool ifNeedModeration)
        {
            int postId = Post.Add(this._conn, this._transaction, this._topicId,
                ifTopic, subject, operatingUserOrOperator.Id, postTime, content, operatingUserOrOperator,
                ifNeedModeration);

            AnnouncementWithPermissionCheck annoucement = new AnnouncementWithPermissionCheck(
                _conn, _transaction, operatingUserOrOperator, _topicId);

            annoucement.IncreaseNumberOfRepliesByOne(ifTopic);
            annoucement.UpdateLastPostInfo(operatingUserOrOperator.Id, postId, postTime, subject);
            //annoucement.UpdateParticipatorIds(operatingUserOrOperator.Id);

            //if (operatingUserOrOperator != null)
            //{
            //    operatingUserOrOperator.IncreaseNumberPostsByOne();
            //}
            //else
            //{
            //    ExceptionHelper.ThrowOperatorNotLoginException();
            //}

            return postId;
        }

        public virtual PostWithPermissionCheck GetFirstPost(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = new DataTable();
            table = PostAccess.GetFirstPostByTopicId(this._conn, this._transaction, this._topicId);
            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowFirstPostNotExistException(this._topicId);
            }
            else
            {
                PostWithPermissionCheck post = CreatePostObject(table.Rows[0], operatingUserOrOperator);

                return post;
            }
            return null;
        }

        public virtual PostWithPermissionCheck GetLastPost(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = new DataTable();
            table = PostAccess.GetLastPostByTopicId(this._conn, this._transaction, this._topicId);
            if (table.Rows.Count == 0)
            {
                return null;
                //ExceptionHelper.ThrowLastPostNotExistException(this._topicId);
            }
            else
            {
                PostWithPermissionCheck post = CreatePostObject(table.Rows[0], operatingUserOrOperator);

                return post;
            }
        }

        public virtual PostWithPermissionCheck[] GetAllPosts(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = PostAccess.GetPostsByTopicId(_conn, _transaction, _topicId);
            PostWithPermissionCheck[] posts = new PostWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                posts[i] = CreatePostObject(table.Rows[i], operatingUserOrOperator);
            }
            return posts;
        }

        public virtual PostWithPermissionCheck[] GetNotDeletedPosts(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = PostAccess.GetNotDeletedPostsByTopicId(_conn, _transaction, _topicId);
            PostWithPermissionCheck[] posts = new PostWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                posts[i] = CreatePostObject(table.Rows[i], operatingUserOrOperator);
            }
            return posts;
        }

        public void Delete(int postId, UserOrOperator operatingOperator)
        {
            PostWithPermissionCheck post = new PostWithPermissionCheck(_conn, _transaction, postId, operatingOperator);
            post.Delete();
        }

        public void DeleteOfAnnoucement(int forumId,int postId, UserOrOperator operatingOperator)
        {
            PostWithPermissionCheck post = new PostWithPermissionCheck(_conn, _transaction, postId, operatingOperator);
            post.DeletePostOfAnnoucement(forumId);
        }

        public virtual void DeleteAllPosts(UserOrOperator operatingOperator)
        {
            PostWithPermissionCheck[] postArray = this.GetAllPosts(operatingOperator);
            foreach (PostWithPermissionCheck post in postArray)
            {
                this.Delete(post.PostId, operatingOperator);
            }
        }

        public virtual void DeleteAllPostsOfAnnoucement(UserOrOperator operatingOperator)
        {
            PostWithPermissionCheck[] postArray = this.GetAllPosts(operatingOperator);
            foreach (PostWithPermissionCheck post in postArray)
            {
                //delete images
                PostImagesWithPermissionCheck postImages = new PostImagesWithPermissionCheck(
                    this._conn, this._transaction, post.PostId, (int)Com.Comm100.Framework.Enum.Forum.EnumForumPostImageUseType.TopicOrPost, operatingOperator);
                postImages.Delete();
                postImages = new PostImagesWithPermissionCheck(
                    this._conn, this._transaction, 0, (int)Com.Comm100.Framework.Enum.Forum.EnumForumPostImageUseType.TopicOrPost, operatingOperator);
                postImages.Delete();

                PostAccess.DeletePost(this._conn, this._transaction, post.PostId);
            }

        }

        public virtual void MoveAllPosts(int forumId, UserOrOperator operatingOperator)
        {
            PostWithPermissionCheck[] postArray = this.GetAllPosts(operatingOperator);
            foreach (PostWithPermissionCheck post in postArray)
            {
                post.UpdateForumId(forumId);
            }
        }

        public virtual TopicWithPermissionCheck GetTopic(UserOrOperator operatingOperator)
        {
            return new TopicWithPermissionCheck(_conn, _transaction, _topicId, operatingOperator);
        }

        public virtual int GetPostIndex(int postId)
        {
            return PostAccess.GetPostIndexInTopic(_conn, _transaction, postId, _topicId);
        }

        /*-----------------------2.0--------------------------*/
        public override PostWithPermissionCheck[] GetNotDeletedPostsByQueryAndPaging(
            UserOrOperator operatingUserOrOperator, string keywords, string name, DateTime startDate,
            DateTime endDate, int pageIndex, int pageSize, string orderFiled, string orderMethod, out int CountOfPosts)
        {
            CountOfPosts = PostAccess.GetCountOfNotDeletedPostsByQueryAndPagingOfSite(
               _conn, _transaction, keywords, name, startDate, endDate, -1);
            DataTable dt = PostAccess.GetNotDeletedPostsByQueryAndPagingOfSite(_conn, _transaction,
                keywords, name, startDate, endDate,
                pageIndex, pageSize, orderFiled, orderMethod, -1);
            List<PostWithPermissionCheck> posts = new List<PostWithPermissionCheck>();
            foreach (DataRow dr in dt.Rows)
            {
                #region Init Data
                int postId = Convert.ToInt32(dr["Id"]);
                int PosttopicId = Convert.ToInt32(dr["TopicId"]);
                string subject = Convert.ToString(dr["Subject"]);
                bool ifAnswer = Convert.ToBoolean(dr["IfAnswer"]);
                DateTime postTime = Convert.ToDateTime(dr["PostTime"]);
                int postUserOrOperatorId = Convert.ToInt32(dr["PostUserOrOperatorId"]);
                string postUserOrOperatorName = Convert.ToString(dr["Name"]);
                #endregion

                PostWithPermissionCheck post = new PostWithPermissionCheck(
                    _conn, _transaction, postId, operatingUserOrOperator, PosttopicId,
                    false, -1, subject, "", postUserOrOperatorId, postUserOrOperatorName,
                    false, "", "", "", false, -1, new DateTime(), postTime, ifAnswer, -1, "",
                    false, new DateTime(), "");
                posts.Add(post);
            }
            return posts.ToArray<PostWithPermissionCheck>();
        }

        public bool IfUserReplyTopic(int userId)
        {
            return PostAccess.IfUserReplyTopic(_conn, _transaction, TopicId, userId);
        }

        //public abstract override PostWithPermissionCheck[] GetNotDeletedPostsByQueryAndPaging(string keywords, int posterId, DateTime startDate, DateTime endDate, int pageIndex, int pageSize, out int CountOfPosts);

    }
}
