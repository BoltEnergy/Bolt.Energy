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
using System.IO;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class Post
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        private int _postId;
        private int _topicId;
        private bool _ifTopic;
        private int _layer;
        private string _subject;
        private string _content;
        private int _postUserOrOperatorId;
        private string _postUserOrOperatorName;
        private bool _ifPostUserOrOperatorCustomizeAvatar;
        private string _postUserOrOperatorSystemAvatar;
        private string _postUserOrOperatorCustomizeAvatar;
        private string _postUserOrOperatorSignature;
        private bool _ifPostUserOrOperatorDeleted;
        private int _postUserOrOperatorNumberOfPosts;
        //P->p
        private DateTime _postUserOrOperatorJoinedTime;
        private DateTime _postTime;
        private bool _ifAnswer;
        private int _lastEditUserOrOperatorId;
        private string _lastEditUserOrOperatorName;
        private bool _ifLastEditUserOrOperatorDeleted;
        private DateTime _lastEditTime;
        /*-----------2.0---------*/
        private bool _ifDeleted;
        private Int16 _moderationStatus;
        private string _textContent;
        //private bool _ifFirstOfTheTopic;
        #endregion

        #region property
        public int PostId
        {
            get { return this._postId; }
        }
        public int TopicId
        {
            get { return this._topicId; }
        }
        public bool IfTopic
        {
            get { return this._ifTopic; }
           //  get { return true; }
        }
        public int Layer
        {
            get { return this._layer; }
        }
        public string Subject
        {
            get { return this._subject; }
        }
        public string Content
        {
            get { return this._content; }
        }
        public int PostUserOrOperatorId
        {
            get { return this._postUserOrOperatorId; }
        }
        public string PostUserOrOperatorName
        {
            get { return this._postUserOrOperatorName; }
        }
        public bool IfPostUserOrOperatorCustomizeAvatar
        {
            get { return this._ifPostUserOrOperatorCustomizeAvatar; }
        }
        public string PostUserOrOperatorSystemAvatar
        {
            get { return this._postUserOrOperatorSystemAvatar; }
        }
        public string PostUserOrOperatorCustomizeAvatar
        {
            get { return this._postUserOrOperatorCustomizeAvatar; }
        }
        public string PostUserOrOperatorSignature
        {
            get { return this._postUserOrOperatorSignature; }
        }
        public bool IfPostUserOrOperatorDeleted
        {
            get { return this._ifPostUserOrOperatorDeleted; }
        }
        public int PostUserOrOperatorNumberOfPosts
        {
            get { return this._postUserOrOperatorNumberOfPosts; }
        }
        public DateTime PostUserOrOperatorJoinedTime
        {
            get { return this._postUserOrOperatorJoinedTime; }
        }
        public DateTime PostTime
        {
            get { return this._postTime; }
        }
        public bool IfAnswer
        {
            get { return this._ifAnswer; }
        }
        public int LastEditUserOrOperatorId
        {
            get { return this._lastEditUserOrOperatorId; }
        }
        public string LastEditUserOrOperatorName
        {
            get { return this._lastEditUserOrOperatorName; }
        }
        public bool IfLastEditUserOrOperatorDeleted
        {
            get { return this._ifLastEditUserOrOperatorDeleted; }
        }
        public DateTime LastEditTime
        {
            get { return this._lastEditTime; }
        }
        /*-------------2.0---------------*/
        public bool IfDeleted
        {
            get { return this._ifDeleted; }
        }
        public EnumPostOrTopicModerationStatus ModerationStatus
        {
            get { return (EnumPostOrTopicModerationStatus)this._moderationStatus; }
        }
        public string TextContent
        { get { return _textContent; } }
        //public bool IfFirstOfTheTopic
        //{
        //    get { return this._ifFirstOfTheTopic; }
        //}
        #region Abstract property
        //public abstract bool IfCanEdit
        //{
        //    get;
        //}
        //public abstract bool IfCanMarkAsAnswer
        //{
        //    get;
        //}
        //public abstract bool IfCanUnMark
        //{
        //    get;
        //}
        //public abstract bool IfCanDelete
        //{
        //    get;
        //}
        //public abstract bool IfCanQuote
        //{
        //    get;
        //}
        #endregion

        #endregion

        public Post(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId)
        {
            _conn = conn;
            _transaction = transaction;

            DataTable table = new DataTable();
            table = PostAccess.GetPostByPostId(conn, transaction, postId);

            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowPostNotExistException(postId);
            }
            else
            {
                _postId = postId;
                _topicId = Convert.ToInt32(table.Rows[0]["TopicId"]);
                _ifTopic = Convert.ToBoolean(table.Rows[0]["IfTopic"]);
                _layer = Convert.ToInt32(table.Rows[0]["Layer"]);
                _subject = Convert.ToString(table.Rows[0]["Subject"]);
                _content = Convert.ToString(table.Rows[0]["Content"]);
                _postUserOrOperatorId = Convert.ToInt32(table.Rows[0]["PostUserOrOperatorId"]);
                _postUserOrOperatorName = Convert.ToString(table.Rows[0]["PostUserOrOperatorName"]);
                _postUserOrOperatorSystemAvatar = Convert.ToString(table.Rows[0]["PostUserOrOperatorSystemAvatar"]);
                _postUserOrOperatorCustomizeAvatar = Convert.ToString(table.Rows[0]["PostUserOrOperatorCustomizeAvatar"]);
                //_postUserOrOperatorNumberOfPosts = Convert.ToInt32(table.Rows[0]["PostUserOrOperatorNumberOfPosts"]);
                _postUserOrOperatorNumberOfPosts=Convert.ToInt32(PostAccess.GetCountOfNotDeletedPostsOfUserOrOperator(conn,transaction,"",_postUserOrOperatorId,new DateTime(),new DateTime()));
                _postUserOrOperatorJoinedTime = Convert.ToDateTime(table.Rows[0]["PostUserOrOperatorJoinedTime"]);
                _postTime = Convert.ToDateTime(table.Rows[0]["PostTime"]);
                _ifAnswer = Convert.ToBoolean(table.Rows[0]["IfAnswer"]);
                _lastEditUserOrOperatorId = Convert.ToInt32(table.Rows[0]["LastEditUserOrOperatorId"]);
                _lastEditUserOrOperatorName = Convert.ToString(table.Rows[0]["LastEditUserOrOperatorName"]);
                _lastEditTime = Convert.ToDateTime(table.Rows[0]["LastEditTime"]);
                _ifPostUserOrOperatorCustomizeAvatar = Convert.ToBoolean(table.Rows[0]["IfPostUserOrOperatorCustomizeAvatar"]);
                _postUserOrOperatorSignature = Convert.ToString(table.Rows[0]["Signature"]);
                _ifPostUserOrOperatorDeleted = Convert.ToBoolean(table.Rows[0]["IfPostUserOrOperatorDeleted"]);
                _ifLastEditUserOrOperatorDeleted = Convert.ToBoolean(table.Rows[0]["IfLastEditUserOrOperatorDeleted"]);
                //get Avatar's filepath
                if (this._ifPostUserOrOperatorCustomizeAvatar == true)
                    this._postUserOrOperatorCustomizeAvatar = this.GetPostOperatorOrUserCustomizeAvatar(this._postUserOrOperatorId);
                /*-----------2.0---------*/
                _ifDeleted = Convert.ToBoolean(table.Rows[0]["IfDeleted"]);
                _moderationStatus = Convert.ToInt16(table.Rows[0]["ModerationStatus"]);
                _textContent = Convert.ToString(table.Rows[0]["TextContent"]);
            }
        }

        public Post(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId, int topicId, bool ifTopic, int layer, string subject, string content,
            int postUserOrOperatorId, string postUserOrOperatorName, bool ifPostUserOrOperatorCustomizeAvatar, string postUserOrOperatorSystemAvatar,
            string postUserOrOperatorCustomizeAvatar, string postUserOrOperatorSignature, bool ifPostUserOrOperatorDeleted, int postUserOrOperatorNumberOfPosts,
            DateTime postUserOrOperatorJoinedTime, DateTime postTime, bool ifAnswer, int lastEditUserOrOperatorId, string lastEditUserOrOperatorName,
            bool ifLastEditUserOrOperatorDeleted, DateTime lastEditTime, string textContent)
        {
            this._conn = conn;
            this._transaction = transaction;

            this._topicId = topicId;
            this._subject = subject;
            this._content = content;
            this._ifAnswer = ifAnswer;
            this._ifPostUserOrOperatorCustomizeAvatar = ifPostUserOrOperatorCustomizeAvatar;
            this._ifTopic = ifTopic;
            this._lastEditTime = lastEditTime;
            this._lastEditUserOrOperatorId = lastEditUserOrOperatorId;
            this._lastEditUserOrOperatorName = lastEditUserOrOperatorName;
            this._layer = layer;
            this._postId = postId;
            this._postTime = postTime;
            this._postUserOrOperatorCustomizeAvatar = postUserOrOperatorCustomizeAvatar;
            this._postUserOrOperatorSignature = postUserOrOperatorSignature;
            this._ifPostUserOrOperatorDeleted = ifPostUserOrOperatorDeleted;
            this._postUserOrOperatorId = postUserOrOperatorId;
            this._postUserOrOperatorJoinedTime = postUserOrOperatorJoinedTime;
            this._postUserOrOperatorName = postUserOrOperatorName;
            this._postUserOrOperatorNumberOfPosts = postUserOrOperatorNumberOfPosts;
            this._postUserOrOperatorSystemAvatar = postUserOrOperatorSystemAvatar;
            this._ifLastEditUserOrOperatorDeleted = ifLastEditUserOrOperatorDeleted;
            this._textContent = textContent;

            //get Avatar's filepath
            if (this._ifPostUserOrOperatorCustomizeAvatar == true)
                this._postUserOrOperatorCustomizeAvatar = this.GetPostOperatorOrUserCustomizeAvatar(this._postUserOrOperatorId);

        }
        /*2.0*/
        public Post(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId, int topicId, bool ifTopic, int layer, string subject, string content,
          int postUserOrOperatorId, string postUserOrOperatorName, bool ifPostUserOrOperatorCustomizeAvatar, string postUserOrOperatorSystemAvatar,
          string postUserOrOperatorCustomizeAvatar, string postUserOrOperatorSignature, bool ifPostUserOrOperatorDeleted, int postUserOrOperatorNumberOfPosts,
          DateTime postUserOrOperatorJoinedTime, DateTime postTime, bool ifAnswer, int lastEditUserOrOperatorId, string lastEditUserOrOperatorName,
          bool ifLastEditUserOrOperatorDeleted, DateTime lastEditTime, string textContent, bool ifDeleted, short moderationStatus)
        {
            this._conn = conn;
            this._transaction = transaction;

            this._topicId = topicId;
            this._subject = subject;
            this._content = content;
            this._ifAnswer = ifAnswer;
            this._ifPostUserOrOperatorCustomizeAvatar = ifPostUserOrOperatorCustomizeAvatar;
            this._ifTopic = ifTopic;
            this._lastEditTime = lastEditTime;
            this._lastEditUserOrOperatorId = lastEditUserOrOperatorId;
            this._lastEditUserOrOperatorName = lastEditUserOrOperatorName;
            this._layer = layer;
            this._postId = postId;
            this._postTime = postTime;
            this._postUserOrOperatorCustomizeAvatar = postUserOrOperatorCustomizeAvatar;
            this._postUserOrOperatorSignature = postUserOrOperatorSignature;
            this._ifPostUserOrOperatorDeleted = ifPostUserOrOperatorDeleted;
            this._postUserOrOperatorId = postUserOrOperatorId;
            this._postUserOrOperatorJoinedTime = postUserOrOperatorJoinedTime;
            this._postUserOrOperatorName = postUserOrOperatorName;
            this._postUserOrOperatorNumberOfPosts = postUserOrOperatorNumberOfPosts;
            this._postUserOrOperatorSystemAvatar = postUserOrOperatorSystemAvatar;
            this._ifLastEditUserOrOperatorDeleted = ifLastEditUserOrOperatorDeleted;
            this._textContent = textContent;

            //get Avatar's filepath
            if (this._ifPostUserOrOperatorCustomizeAvatar == true)
                this._postUserOrOperatorCustomizeAvatar = this.GetPostOperatorOrUserCustomizeAvatar(this._postUserOrOperatorId);

            _ifDeleted = ifDeleted;
            _moderationStatus = moderationStatus;
        }

        private static void CheckFieldsLength(string subject, string content)
        {
            if (subject.Length == 0)
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("Subject");
            else
            {
                if (subject.Length > ForumDBFieldLength.Post_subjectFieldLength)
                    ExceptionHelper.ThrowSystemFieldLengthExceededException("Subject", ForumDBFieldLength.Post_subjectFieldLength);
            }
            //if (content.Length > ForumDBFieldLength.Post_contentFieldLength)
            //    ExceptionHelper.ThrowSystemFieldLengthExceededException("Content", ForumDBFieldLength.Post_contentFieldLength);

        }


        public static int Add(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int topicId, bool ifTopic, string subject, int postUserOrOperatorId,
            DateTime postTime, string content, UserOrOperator operatingUserOrOperator, bool ifNeedModeration)
        {
            CheckFieldsLength(subject, content);

            int postId;
            if (ifNeedModeration)
            {
                postId = PostAccess.AddPost(conn, transaction, topicId, ifTopic, subject, postUserOrOperatorId,
                    postTime, content, CommFun.StripHtml(content), (int)(EnumPostOrTopicModerationStatus.WaitingForModeration));
            }
            else
            {
                postId = PostAccess.AddPost(conn, transaction, topicId, ifTopic, subject, postUserOrOperatorId,
                    postTime, content, CommFun.StripHtml(content), (int)(EnumPostOrTopicModerationStatus.Accepted));
            }

            PostImagesWithPermissionCheck postImages = new PostImagesWithPermissionCheck(conn, transaction, postId, (int)Com.Comm100.Framework.Enum.Forum.EnumForumPostImageUseType.TopicOrPost, operatingUserOrOperator);
            int[] imageIDs = Com.Comm100.Framework.Common.CommonFunctions.GetPostContentImageIds(content);
            postImages.AttachToPost(imageIDs);

            return postId;
        }

        protected virtual void Update(int forumId, string subject, string content,
            int editUserOrOperatorId, DateTime editTime, UserOrOperator operatingOperator,
            int[] attachIds, int[] scores, string[] descriptions, int[] toDeleteAttachIds, bool ifNeedModeration)
        {
            CheckFieldsLength(subject, content);
            EnumPostOrTopicModerationStatus status;
            if (ifNeedModeration)
            {
                status = EnumPostOrTopicModerationStatus.WaitingForModeration;
            }
            else
            {
                status = EnumPostOrTopicModerationStatus.Accepted;
            }
            PostAccess.UpdatePost(this._conn, this._transaction, this._postId, subject, content, editUserOrOperatorId, editTime, status);

            TopicWithPermissionCheck topic = this.GetTopic(operatingOperator);
            if (topic.LastPostId == this._postId)
            {
                topic.UpdateLastPostInfo(editUserOrOperatorId, _postId, editTime, subject);
            }

            PostImagesWithPermissionCheck postImages = new PostImagesWithPermissionCheck(this._conn, this._transaction, this._postId, (int)Com.Comm100.Framework.Enum.Forum.EnumForumPostImageUseType.TopicOrPost, operatingOperator);
            int[] imageIDs = Com.Comm100.Framework.Common.CommonFunctions.GetPostContentImageIds(content);
            postImages.AttachToPost(imageIDs);

            /*Update Attachment*/
            for (int i = 0; i < attachIds.Length; i++)
            {
                AttachmentWithPermissionCheck attachment = new AttachmentWithPermissionCheck(_conn, _transaction,
                    operatingOperator, attachIds[i]);
                attachment.Update(forumId, _postId, scores[i], descriptions[i], EnumAttachmentType.AttachToPost);
            }

            foreach (int toDeleteAttachId in toDeleteAttachIds)
            {
                AttachmentWithPermissionCheck attachment = new AttachmentWithPermissionCheck(
                    _conn, _transaction, operatingOperator, toDeleteAttachId);
                attachment.Delete(forumId);
            }
        }

        protected virtual void UpdateAnnoucementPost(string subject, string content, int editUserOrOperatorId,
            DateTime editTime, UserOrOperator operatingOperator, bool ifNeedModeration)
        {
            CheckFieldsLength(subject, content);
            EnumPostOrTopicModerationStatus status;
            if (ifNeedModeration)
            {
                status = EnumPostOrTopicModerationStatus.WaitingForModeration;
            }
            else
            {
                status = EnumPostOrTopicModerationStatus.Accepted;
            }
            PostAccess.UpdatePost(this._conn, this._transaction, this._postId, subject, content, editUserOrOperatorId,
                editTime, status);

            AnnouncementWithPermissionCheck annoucement = new AnnouncementWithPermissionCheck(
                _conn, _transaction, operatingOperator, _topicId);
            if (annoucement.LastPostId == this._postId)
            {
                annoucement.UpdateLastPostInfo(editUserOrOperatorId, _postId, editTime, subject);
            }

            PostImagesWithPermissionCheck postImages = new PostImagesWithPermissionCheck(this._conn, this._transaction, this._postId, (int)Com.Comm100.Framework.Enum.Forum.EnumForumPostImageUseType.TopicOrPost, operatingOperator);
            int[] imageIDs = Com.Comm100.Framework.Common.CommonFunctions.GetPostContentImageIds(content);
            postImages.AttachToPost(imageIDs);

        }

        protected UserOrOperator GetUserOrOperator(UserOrOperator operatingUserOrOperator)
        {
            return UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, operatingUserOrOperator, _postUserOrOperatorId);
        }
        protected virtual void Delete(UserOrOperator operatingUserOrOperator)
        {
            //delete images
            PostImagesWithPermissionCheck postImages = new PostImagesWithPermissionCheck(this._conn, this._transaction, _postId, (int)Com.Comm100.Framework.Enum.Forum.EnumForumPostImageUseType.TopicOrPost, operatingUserOrOperator);
            postImages.Delete();
            postImages = new PostImagesWithPermissionCheck(this._conn, this._transaction, 0, (int)Com.Comm100.Framework.Enum.Forum.EnumForumPostImageUseType.TopicOrPost, operatingUserOrOperator);
            postImages.Delete();

            PostAccess.DeletePost(this._conn, this._transaction, this._postId);

            TopicWithPermissionCheck topic = this.GetTopic(operatingUserOrOperator);

            topic.DecreaseNumberOfRepliesByOne();

            if (this.IfAnswer)
            {
                topic.UnMarkedAsAnswer();
            }

            if (_postId == topic.LastPostId)
            {
                PostWithPermissionCheck lastPost = topic.GetPosts().GetLastPost();
                if (lastPost != null)
                {
                    topic.UpdateLastPostInfo(lastPost.PostUserOrOperatorId, lastPost.PostId, lastPost.PostTime, lastPost.Subject);
                }
            }
            //9.22 new add

            UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, operatingUserOrOperator, _postUserOrOperatorId);
            if (userOrOperator != null)
            {
                userOrOperator.DecreaseAutorPostsNumberByOne();
            }

            //if (_ifTopic == true)
            //{
            //    topic.Delete();
            //}

            /*2.0 stategy */
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                _conn, _transaction, null, _conn.SiteId);
            //scoreStrategySetting.UseInDeletePost(this as PostWithPermissionCheck,operatingUserOrOperator);
        }

        protected virtual void DeletePostOrTopic(UserOrOperator operatingUserOrOperator)
        {
            TopicWithPermissionCheck topic = this.GetTopic(operatingUserOrOperator);
            if (_ifTopic == true)
            {
                topic.Delete();
            }
            else
            {
                this.Delete(operatingUserOrOperator);
            }
        }

        protected virtual void DeletePostOfAnnoucement(UserOrOperator operatingUserOrOperator,int forumId)
        {
            if (!this.IfTopic)
            {
                //delete images
                PostImagesWithPermissionCheck postImages = new PostImagesWithPermissionCheck(this._conn, this._transaction, _postId, (int)Com.Comm100.Framework.Enum.Forum.EnumForumPostImageUseType.TopicOrPost, operatingUserOrOperator);
                postImages.Delete();
                postImages = new PostImagesWithPermissionCheck(this._conn, this._transaction, 0, (int)Com.Comm100.Framework.Enum.Forum.EnumForumPostImageUseType.TopicOrPost, operatingUserOrOperator);
                postImages.Delete();

                PostAccess.DeletePost(this._conn, this._transaction, this._postId);
            }
            else
            {
                ForumsOfAnnouncementWithPermissionCheck forumsOfAnnoucement = new ForumsOfAnnouncementWithPermissionCheck(_conn,
                    _transaction, operatingUserOrOperator, TopicId);
                forumsOfAnnoucement.DeleteForumsAndAnnouncementRelationWithForumId(forumId);
            }

            //AnnouncementWithPermissionCheck annoucement = new AnnouncementWithPermissionCheck(_conn, _transaction,
            //    operatingUserOrOperator, TopicId);

            //annoucement.DecreaseNumberOfRepliesByOne();

            //TopicWithPermissionCheck topic = this.GetTopic(operatingUserOrOperator);
            //if (this.IfAnswer)
            //{
            //    topic.UnMarkedAsAnswer();
            //}
            //if (_postId == topic.LastPostId)
            //{
            //    PostWithPermissionCheck lastPost = topic.GetPosts().GetLastPost();
            //    if (lastPost != null)
            //    {
            //        annoucement.UpdateLastPostInfo(lastPost.PostUserOrOperatorId, lastPost.PostId, lastPost.PostTime, lastPost.Subject);
            //    }
            //}
        }

        public virtual void UpdateForumId(int forumId)
        {
            PostAccess.UpdateForumId(this._conn, this._transaction, this._postId, forumId);
        }

        protected virtual void MarkAsAnswer(UserOrOperator operatingOperator)
        {
            TopicWithPermissionCheck topic = this.GetTopic(operatingOperator);

            if (topic.IfMarkedAsAnswer)
            {
                PostWithPermissionCheck postWithAnswer = topic.GetAnswer();
                postWithAnswer.UnMarkAsAnswer();
            }

            topic.MarkedAsAnswer();

            PostAccess.UpdatePostIfAnswerStatus(this._conn, this._transaction, this._postId, true);

            /*2.0 stategy */
            PostWithPermissionCheck post = new PostWithPermissionCheck(_conn, _transaction, _postId,
                operatingOperator);
            UserOrOperator Author = UserOrOperatorFactory.CreateNotDeletedUserOrOperator(_conn, _transaction,
                operatingOperator, post.PostUserOrOperatorId);
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                _conn, _transaction, null, _conn.SiteId);
            scoreStrategySetting.UseAfterMarkPostAsAnswer(post,Author);

            /*2.0 reputation strategy*/
            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingOperator, _conn.SiteId);
            reputationStrategySetting.UseAfterMarkPostAsAnswer(post, Author);

            ///*Send Email If User Subscibe Topic*/
            //SubscribesOfSiteWithPermissionCheck subscrbesOfSite = new SubscribesOfSiteWithPermissionCheck(
            //   _conn, _transaction, operatingOperator);
            //subscrbesOfSite.SendEmailToUsers(_topicId, _postId, EnumQueueEmailType.MakedAsAnswer, operatingOperator, DateTime.UtcNow);
        }

        protected virtual void UnMarkAsAnswer(UserOrOperator operatingOperator)
        {
            TopicWithPermissionCheck topic = this.GetTopic(operatingOperator);
            topic.UnMarkedAsAnswer();

            PostAccess.UpdatePostIfAnswerStatus(this._conn, this._transaction, this._postId, false);
        }

        protected TopicWithPermissionCheck GetTopic(UserOrOperator operatingOperator)
        {
            return new TopicWithPermissionCheck(_conn, _transaction, _topicId, operatingOperator);
        }

        private string GetPostOperatorOrUserCustomizeAvatar(int operateOrUserId)
        {
            int siteId = this._conn.SiteId;
            int id = operateOrUserId;
            string strAvatarFileName = siteId + @"/" + id + ConstantsHelper.User_Avatar_FileType;
            string strAvatarDirectory = System.Web.HttpContext.Current.Server.MapPath(@"~/" +
                                           ConstantsHelper.ForumAvatarTemporaryFolder + @"/" + siteId.ToString());
            string strAvatarFilePath = strAvatarDirectory + @"\" + id + ConstantsHelper.User_Avatar_FileType;

            /* if avatar is not exsit, load avatar from database */
            if (File.Exists(strAvatarFilePath) == false)
            {
                byte[] bAvatars = UserAccess.GetUserOrOperatorAvatar(this._conn, this._transaction, id);
                //no avatar data in database
                if (bAvatars == null)
                {
                    return null;
                }
                #region Update avatar file
                FileStream fs = null;
                try
                {
                    if (Directory.Exists(strAvatarDirectory) == false)
                        Directory.CreateDirectory(strAvatarDirectory);
                    fs = File.Create(strAvatarFilePath);
                    fs.Write(bAvatars, 0, bAvatars.Length);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
                #endregion
            }

            //for 'Opera' and 'world Window'
            Random rdNum = new Random();
            strAvatarFileName += "?" + rdNum.Next(1000000000);//default 1000000000

            return strAvatarFileName;
        }


        /*-------------------------------------2.0---------------------------------------*/
        protected AbusesOfPostWithPermissionCheck GetAbuses(UserOrOperator operatingUserOrOperator)
        {
            return new AbusesOfPostWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, _postId);
        }

        protected void LogicDelete(UserOrOperator operatingUserOrOperator)
        {
            if (IfTopic == true)
            {
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction
                    , _topicId, operatingUserOrOperator);
                topic.LogicDelete();
            }
            if (IfDeleted == true)
            {
                ExceptionHelper.ThrowPostNotExistException(_postId);
            }

            PostAccess.LogicDeletePost(_conn, _transaction, _postId);

            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                   _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                 _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            UserOrOperator Author = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, operatingUserOrOperator,
                    this._postUserOrOperatorId);

            PostWithPermissionCheck post = new PostWithPermissionCheck(_conn,_transaction,_postId,
                operatingUserOrOperator);
            /*2.0 stategy */
            scoreStrategySetting.UseAfterLogicDeletePost(post, Author);

            /*2.0 reputation strategy*/
            reputationStrategySetting.UseAfterLogicDeletePost(post, Author);

        }

        protected void Restore(UserOrOperator operatingUserOrOperator)
        {
            if (IfTopic == true)
            {
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction
                    , _topicId, operatingUserOrOperator);
                topic.Restore();
            }
            PostAccess.RestorePost(_conn, _transaction, _postId);

            PostWithPermissionCheck post = new PostWithPermissionCheck(_conn, _transaction,
                _postId, operatingUserOrOperator);

            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                 _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                 _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            UserOrOperator Author = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, operatingUserOrOperator,
                    this._postUserOrOperatorId);

            /*2.0 stategy */
            scoreStrategySetting.UseAfterRestorePost(post, Author);

            /*2.0 reputation strategy*/
            reputationStrategySetting.UseAfterRestorePost(post, Author);
        }

        protected AttachmentsOfPostWithPermissionCheck GetAttachments(UserOrOperator operatingUserOrOperator)
        {
            return null;
        }

        protected void RefuseModeration(UserOrOperator operatingUserOrOperator)
        {
            if (_ifTopic == true)
            {
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(
                    _conn, _transaction, _topicId, operatingUserOrOperator);
                topic.RefuseModeration();
            }
            PostAccess.RefuseModeration(_conn, _transaction, _postId);
        }

        protected void AcceptModeration(UserOrOperator operatingUserOrOperator)
        {
            if (_ifTopic == true)
            {
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(
                    _conn, _transaction, _topicId, operatingUserOrOperator);
                topic.AcceptModeration();
            }
            PostAccess.AcceptModeration(_conn, _transaction, _postId);
            if (!_ifTopic)
            {
                PostWithPermissionCheck post = new PostWithPermissionCheck(_conn, _transaction,
               _postId, operatingUserOrOperator);

                ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
               _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
                ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                     _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
                UserOrOperator Author = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, operatingUserOrOperator,
                        this._postUserOrOperatorId);

                /*2.0 stategy */
                scoreStrategySetting.UseAfterApproveReply(post, Author);

                /*2.0 reputation strategy*/
                reputationStrategySetting.UseAfterApproveReply(post, Author);
            }
        }

        protected bool IfAbused(UserOrOperator operatingUserOrOperator)
        {
            AbusesOfPostWithPermissionCheck abusesOfPost = GetAbuses(operatingUserOrOperator);
            return abusesOfPost.GetCountOfAbuse() > 0;
        }

        protected EnumPostAbuseStatus GetAbuseStatus(UserOrOperator operatingUserOrOperator)
        {
            EnumPostAbuseStatus status = EnumPostAbuseStatus.NotAbused;
            AbusesOfPostWithPermissionCheck abusesOfPost = GetAbuses(operatingUserOrOperator);
            if (abusesOfPost.GetCountOfAbuse() > 0)
            {
                AbuseWithPermissionCheck[] abuses = abusesOfPost.GetAllAbuses();
                foreach (AbuseWithPermissionCheck abuse in abuses)
                {
                    if (abuse.Status == EnumAbuseStatus.Pending)
                    {
                        status = EnumPostAbuseStatus.AbusedAndPending;
                        break;
                    }
                    else if (abuse.Status == EnumAbuseStatus.Approved)
                    {
                        status = EnumPostAbuseStatus.AbusedAndApproved;
                        break;
                    }
                    else if (abuse.Status == EnumAbuseStatus.Refused)
                    {
                        status = EnumPostAbuseStatus.AbusedAndRefused;
                    }
                }
            }
            return status;
        }

        protected EnumPostAbuseStatus GetAbuseStatusOfUser(UserOrOperator operatingUserOrOperator, int userId)
        {
            EnumPostAbuseStatus status = EnumPostAbuseStatus.NotAbused;
            AbusesOfPostWithPermissionCheck abusesOfPost = GetAbuses(operatingUserOrOperator);
            if (abusesOfPost.GetCountOfAbuse() > 0)
            {
                AbuseWithPermissionCheck[] abuses = abusesOfPost.GetAllAbusesOfUser(userId);
                foreach (AbuseWithPermissionCheck abuse in abuses)
                {
                    if (abuse.Status == EnumAbuseStatus.Pending)
                    {
                        status = EnumPostAbuseStatus.AbusedAndPending;
                        break;
                    }
                    else if (abuse.Status == EnumAbuseStatus.Approved)
                    {
                        status = EnumPostAbuseStatus.AbusedAndApproved;
                        break;
                    }
                    else if (abuse.Status == EnumAbuseStatus.Refused)
                    {
                        status = EnumPostAbuseStatus.AbusedAndRefused;
                    }
                }
            }
            return status;
        }

    }
}
