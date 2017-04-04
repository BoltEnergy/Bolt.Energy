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
    public abstract class Topic : TopicBase
    {
        #region private fields

        private int _forumId;
        private string _forumName;
        private bool _ifMarkedAsAnswer;
        private bool _ifSticky;
        private bool _ifHasDraft;
        private bool _ifClosed;
        private int[] _participatorIds;
        /*----------------2.0--------------------*/
        private bool _ifHot;
        private bool _ifDeleted;
        private Int16 _moderationStatus;
        private bool _ifPayScoreRequired;
        private int _score;

        private bool _ifMoveHistory;
        private int _locateTopicId;
        private int _locateForumId;
        private DateTime _moveDate;
        private int _moveUserOrOperatorId;
        private bool _ifFeatured;
        private bool _ifContainsPoll;
        private bool _ifReplyRequired;
        private int _totalPromotion;
        #endregion

        #region property

        public int ForumId
        {
            get { return this._forumId; }
        }
        public string ForumName
        {
            get { return this._forumName; }
        }

        public bool IfMarkedAsAnswer
        {
            get { return this._ifMarkedAsAnswer; }
        }
        public bool IfSticky
        {
            get { return this._ifSticky; }
        }
        public bool IfClosed
        {
            get { return this._ifClosed; }
        }
        public int[] ParticipatorIds
        {
            get { return this._participatorIds; }
        }
        public abstract bool IfParticipant
        {
            get;
        }
        public bool IfHasDraft
        {
            get
            {
                return this._ifHasDraft;
            }
        }
        /*----------------2.0--------------------*/
        public bool IfHot
        {
            get { return this._ifHot; }
        }
        public bool IfDeleted
        {
            get { return this._ifDeleted; }
        }
        public EnumPostOrTopicModerationStatus ModerationStatus
        {
            get { return (EnumPostOrTopicModerationStatus)this._moderationStatus; }
        }
        public bool IfPayScoreRequired
        {
            get { return this._ifPayScoreRequired; }
        }
        public int Score
        {
            get { return this._score; }
        }

        public bool IfMoveHistory
        {
            get { return this._ifMoveHistory; }
        }
        public int LocateTopicId
        {
            get { return this._locateTopicId; }
        }
        public int LocateForumId
        {
            get { return this._locateForumId; }
        }
        public DateTime MoveDate
        {
            get { return this._moveDate; }
        }
        public int MoveUserOrOperatorId
        {
            get { return this._moveUserOrOperatorId; }
        }
        public bool IfFeatured
        { get { return this._ifFeatured; } }
        public bool IfContainsPoll
        { get { return this._ifContainsPoll; } }
        public bool IfReplyRequired
        { get { return this._ifReplyRequired; } }
        public int TotalPromotion
        {
            get { return this._totalPromotion; }
        }
        #endregion

        public Topic(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId)
            : base(conn, transaction)
        {
            this._conn = conn;
            this._transaction = transaction;

            DataTable table = new DataTable();
            table = TopicAccess.GetTopicByTopicId(_conn, _transaction, topicId);
            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowTopicNotExistException(topicId);
            }
            else
            {
                _topicId = topicId;
                _forumId = Convert.ToInt32(table.Rows[0]["ForumId"]);
                _forumName = Convert.ToString(table.Rows[0]["ForumName"]);
                _subject = Convert.ToString(table.Rows[0]["Subject"]);
                _postUserOrOperatorId = table.Rows[0]["PostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[0]["PostUserOrOperatorId"]);
                _postUserOrOperatorName = table.Rows[0]["PostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[0]["PostUserOrOperatorName"]);
                _postUserOrOperatorIfDeleted = table.Rows[0]["PostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[0]["PostUserOrOperatorIfDeleted"]);
                _postTime = Convert.ToDateTime(table.Rows[0]["PostTime"]);
                _lastPostId = Convert.ToInt32(table.Rows[0]["LastPostId"]);
                _lastPostTime = Convert.ToDateTime(table.Rows[0]["LastPostTime"]);
                _lastPostUserOrOperatorId = table.Rows[0]["LastPostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[0]["LastPostUserOrOperatorId"]);
                _lastPostUserOrOperatorName = table.Rows[0]["LastPostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[0]["LastPostUserOrOperatorName"]);
                _lastPostUserOrOperatorIfDeleted = table.Rows[0]["LastPostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[0]["LastPostUserOrOperatorIfDeleted"]);
                _numberOfReplies = PostAccess.GetCountOfNotDeletedPostsByTopicId(_conn,_transaction,topicId);
                _numberOfHits = Convert.ToInt32(table.Rows[0]["NumberOfHits"]);
                _ifClosed = Convert.ToBoolean(table.Rows[0]["IfClosed"]);
                _ifMarkedAsAnswer = Convert.ToBoolean(table.Rows[0]["IfMarkedAsAnswer"]);
                _ifSticky = Convert.ToBoolean(table.Rows[0]["IfSticky"]);

                _participatorIds = StringHelper.GetIntArrayFromString(table.Rows[0]["ParticipatorIds"].ToString(), ',');
                _ifHasDraft = IfTopicHasDraft();

                /*2.0*/
                _ifFeatured = Convert.ToBoolean(table.Rows[0]["IfFeatured"]);
                _ifMoveHistory = Convert.ToBoolean(table.Rows[0]["IfMoveHistory"]);
                _ifContainsPoll = Convert.ToBoolean(table.Rows[0]["IfContainsPoll"]);
                _ifReplyRequired = Convert.ToBoolean(table.Rows[0]["IfReplyRequired"]);
                _ifPayScoreRequired = Convert.ToBoolean(table.Rows[0]["IfPayScoreRequired"]);
                _score = Convert.ToInt32(table.Rows[0]["Score"]);
                _locateTopicId = Convert.ToInt32(table.Rows[0]["LocateTopicId"]);
                _ifDeleted = Convert.ToBoolean(table.Rows[0]["TopicIfDeleted"]);
                _moderationStatus = Convert.ToInt16(table.Rows[0]["ModerationStatus"]);
                _totalPromotion = Convert.ToInt32(table.Rows[0]["TotalPromotion"]);
            }


        }


        public Topic(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, int forumId, string forumName, string subject, int postUserOrOperatorId, string postUserOrOperatorName, bool postUserOrOperatorIfDeleted,
                DateTime postTime, int lastPostId, DateTime lastPostTime, int lastPostUserOrOperatorId, string lastPostUserOrOperatorName, bool lastPostUserOrOperatorIfDeleted, int numberOfReplies, int numberOfHits,
                bool ifClosed, bool ifMarkedAsAnswer, bool ifSticky, int[] participatorIds)
            : base(conn, transaction)
        {
            this._conn = conn;
            this._transaction = transaction;

            this._topicId = topicId;
            this._forumId = forumId;
            this._forumName = forumName;
            this._subject = subject;
            this._postUserOrOperatorId = postUserOrOperatorId;
            this._postUserOrOperatorName = postUserOrOperatorName;
            this._postUserOrOperatorIfDeleted = postUserOrOperatorIfDeleted;
            this._postTime = postTime;
            this._lastPostId = lastPostId;
            this._lastPostTime = lastPostTime;
            this._lastPostUserOrOperatorId = lastPostUserOrOperatorId;
            this._lastPostUserOrOperatorName = lastPostUserOrOperatorName;
            this._lastPostUserOrOperatorIfDeleted = lastPostUserOrOperatorIfDeleted;
            this._numberOfReplies = numberOfReplies;
            this._numberOfHits = numberOfHits;
            this._ifClosed = ifClosed;
            this._ifMarkedAsAnswer = ifMarkedAsAnswer;
            this._ifSticky = ifSticky;
            this._participatorIds = participatorIds;
            this._ifHasDraft = IfTopicHasDraft();
            
        }
        /*2.0*/
        public Topic(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, int forumId, string forumName, string subject, int postUserOrOperatorId, string postUserOrOperatorName, bool postUserOrOperatorIfDeleted,
              DateTime postTime, int lastPostId, DateTime lastPostTime, int lastPostUserOrOperatorId, string lastPostUserOrOperatorName, bool lastPostUserOrOperatorIfDeleted, int numberOfReplies, int numberOfHits,
              bool ifClosed, bool ifMarkedAsAnswer, bool ifSticky, int[] participatorIds, bool ifDeleted, short moderationStatus,
              bool ifPayScoreRequired, int score, bool ifMoveHistory, int locateTopicId, int locateForumId, DateTime moveDate,
              int moveUserOrOperatorId, bool ifFeatured, bool ifContainsPoll, bool ifReplyRequired,int totalPromotion
            )
            : base(conn, transaction)
        {
            this._conn = conn;
            this._transaction = transaction;

            this._topicId = topicId;
            this._forumId = forumId;
            this._forumName = forumName;
            this._subject = subject;
            this._postUserOrOperatorId = postUserOrOperatorId;
            this._postUserOrOperatorName = postUserOrOperatorName;
            this._postUserOrOperatorIfDeleted = postUserOrOperatorIfDeleted;
            this._postTime = postTime;
            this._lastPostId = lastPostId;
            this._lastPostTime = lastPostTime;
            this._lastPostUserOrOperatorId = lastPostUserOrOperatorId;
            this._lastPostUserOrOperatorName = lastPostUserOrOperatorName;
            this._lastPostUserOrOperatorIfDeleted = lastPostUserOrOperatorIfDeleted;
            this._numberOfReplies = numberOfReplies;
            this._numberOfHits = numberOfHits;
            this._ifClosed = ifClosed;
            this._ifMarkedAsAnswer = ifMarkedAsAnswer;
            this._ifSticky = ifSticky;
            this._participatorIds = participatorIds;
            this._ifHasDraft = IfTopicHasDraft();

            /*2.0*/
            //_ifHot = ifHot;
            _ifDeleted = ifDeleted;
            _moderationStatus = moderationStatus;
            _ifPayScoreRequired = ifPayScoreRequired;
            _score = score;

            _ifMoveHistory = ifMoveHistory;
            _locateTopicId = locateTopicId;
            _locateForumId = locateForumId;
            _moveDate = moveDate;
            _moveUserOrOperatorId = moveUserOrOperatorId;
            _ifFeatured = ifFeatured;
            _ifContainsPoll = ifContainsPoll;
            _ifReplyRequired = ifReplyRequired;
            _totalPromotion = totalPromotion;
        }
        public static int Add(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator,
            int forumId, string subject, int postUserOrOperatorId, DateTime postTime,
            string content, int score, bool ifReplyRequired, bool ifPayScoreRequired,
            bool ifContainsPoll, bool ifMultipleChoice, int maxChoices, bool ifSetDeadline,
            DateTime startDate, DateTime endDate, string[] options,
            int[] attachIds, int[] scores, string[] descriptions,bool ifTopicModeration)
        {
            CheckFieldsLength(subject, content);
            // if option is null, default not contain poll
            if (options == null)
                ifContainsPoll = false;

            if (options != null && options.Length < maxChoices)
            {
                ExceptionHelper.ThrowForumPollOptionCountLessThanMaxChoiceException();
            }
            int topicId = TopicAccess.AddTopic(conn, transaction, forumId, subject,
                postUserOrOperatorId, postTime, content, score, ifContainsPoll, ifReplyRequired, ifPayScoreRequired, ifTopicModeration);
            /*add first Post*/
            PostsOfTopicWithPermissionCheck posts = new PostsOfTopicWithPermissionCheck(conn, transaction, topicId, operatingUserOrOperator);
            int postId = posts.Add(true,false, subject, postTime, content, attachIds, scores, descriptions, forumId);
            /*add Poll*/
            List<PollOptionStruct> pollOptions = new List<PollOptionStruct>();
            if (options != null)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    pollOptions.Add(new PollOptionStruct(topicId, options[i], i));
                }
            }
            Polls polls = new Polls(conn, transaction);
            polls.Add(topicId, ifMultipleChoice, maxChoices, ifSetDeadline, startDate,
                endDate, pollOptions.ToArray<PollOptionStruct>());
            return topicId;
        }
        protected void Update(string subject, string content, UserOrOperator operatingOperator, 
            bool ifSetDeadline,DateTime endDate,
            int[] attachIds, int[] scores, string[] descriptions,
            int forumId,
            int[] toDeleteAttachIds
            )
        {
            CheckFieldsLength(subject, content);
            TopicAccess.UpdateTopic(this._conn, this._transaction, this._topicId, subject, content,
                this.Score, this.IfReplyRequired, this.IfPayScoreRequired, this.IfContainsPoll);
            /*Poll*/
            Poll poll = new Poll(_conn, _transaction, _topicId);
            poll.Update(poll.IfMulitipleChoice, poll.MaxChoices, ifSetDeadline, endDate);
            /*Update Content*/
            DateTime postTime = DateTime.UtcNow;
            PostsOfTopicWithPermissionCheck posts = this.GetPosts(operatingOperator);
            PostWithPermissionCheck post = posts.GetFirstPost();
            post.Update(subject, content, postTime, attachIds, scores, descriptions, forumId,toDeleteAttachIds);
        }

        public virtual void SaveDraft(string subject, string content,
            UserOrOperator operatingOperator, DateTime createTime,
            int[] attachIds, int[] scores, string[] descriptions, int[] toDeleteAttachIds)
        {
            int draftId;
            if (this._ifHasDraft)
            {
                DraftWithPermissionCheck draft = this.GetDraft(operatingOperator);
                draftId = draft.DraftId;
                draft.Update(subject, content, createTime);
            }
            else
            {
                draftId = DraftWithPermissionCheck.Add(_conn, _transaction, _topicId, subject, content, operatingOperator.Id, createTime);
            }
            /*Update Attachment*/
            for (int i = 0; i < attachIds.Length; i++)
            {
                AttachmentWithPermissionCheck attachment = new AttachmentWithPermissionCheck(_conn, _transaction,
                    operatingOperator, attachIds[i]);
                attachment.Update(ForumId,draftId, scores[i], descriptions[i], EnumAttachmentType.AttachToDraft);
            }
            //delete Attachment
            if (toDeleteAttachIds != null)
            {
                for (int i = 0; i < toDeleteAttachIds.Length; i++)
                {
                    AttachmentWithPermissionCheck attachment = new AttachmentWithPermissionCheck(_conn, _transaction,
                       operatingOperator, toDeleteAttachIds[i]);
                    attachment.Delete(ForumId);
                }
            }
        }

        public virtual DraftWithPermissionCheck AddDraft(string subject, string content, UserOrOperator operatingOperator, DateTime createTime)
        {
            int draftId = DraftWithPermissionCheck.Add(_conn, _transaction, _topicId, subject, content, operatingOperator.Id, createTime);
            return new DraftWithPermissionCheck(_conn, _transaction, draftId, operatingOperator);
        }

        public virtual void DeleteDraft(UserOrOperator operatingOperator)
        {
            if (_ifHasDraft)
            {
                DraftWithPermissionCheck draft = this.GetDraft(operatingOperator);
                draft.Delete();
            }
        }

        public virtual int PostDraft(SqlConnectionWithSiteId conn, SqlTransaction transaction, bool ifTopic,
            string subject, int postUserOrOperatorId, DateTime postTime, string content)
        {
            DataTable table = DraftAccess.GetDraftByTopicId(_conn, _transaction, _topicId);
            if (table.Rows.Count > 0)
            {
                DraftAccess.DeleteDraftByTopicId(_conn, _transaction, _topicId);
            }
            CheckFieldsLength(subject, content);
            return PostAccess.AddPost(conn, transaction, _topicId, ifTopic, subject, postUserOrOperatorId,
                postTime, content, CommFun.StripHtml(content), (int)(EnumPostOrTopicModerationStatus.Accepted));
        }

        protected virtual ForumWithPermissionCheck GetForum(UserOrOperator operatingOperator)
        {
            return new ForumWithPermissionCheck(_conn, _transaction, _forumId, operatingOperator);
        }

        protected virtual DraftWithPermissionCheck GetDraft(UserOrOperator operatingOperator)
        {
            return new DraftWithPermissionCheck(_conn, _transaction, _topicId, 0, operatingOperator);
        }

        public virtual PostWithPermissionCheck GetAnswer(UserOrOperator operatingOperator)
        {
            PostsOfTopicWithPermissionCheck posts = new PostsOfTopicWithPermissionCheck(this._conn, this._transaction, this._topicId, operatingOperator);
            return posts.GetAnswer();
        }

        protected virtual void Delete(UserOrOperator operatingOperator)
        {
            ForumWithPermissionCheck forum = this.GetForum(operatingOperator);

            forum.DecreaseNumberOfTopicsByOne();

            this.DeleteDraft(operatingOperator);

            PostsOfTopicWithPermissionCheck posts = new PostsOfTopicWithPermissionCheck(_conn, _transaction, _topicId, operatingOperator);
            posts.DeleteAllPosts();

            TopicAccess.DeleteTopic(_conn, _transaction, _topicId);

            FavoriteAccess.DeleteFavoriteByTopicId(_conn, _transaction, _topicId);

            if (this.TopicId == forum.LastPostTopicId)
            {
                PostWithPermissionCheck post = forum.GetLastPost();

                if (post == null)
                {
                    forum.UpdateLastCreateInfo(0, Convert.ToDateTime("1900-01-01"), 0, 0, string.Empty);
                }
                else
                {
                    forum.UpdateLastCreateInfo(post.PostUserOrOperatorId, post.PostTime, post.PostId, post.TopicId, post.Subject);
                }
            }

            // moved topic Of Topic deleted
             TopicWithPermissionCheck[] movedTopics = GetMovedTopicOfTopic(operatingOperator);
             foreach (var topic in movedTopics)
             {
                 topic.DeleteWithNoPermissionCheck();
             }
            /*2.0 stategy */
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                _conn, _transaction, null, _conn.SiteId);
            //scoreStrategySetting.UseAfterDeleteTopic(this as TopicWithPermissionCheck,operatingOperator);
        }

        public virtual void Read()
        {
            string readTopicIds = "";
            readTopicIds = Framework.Common.CommonFunctions.ReadCookies("ReadTopicId");
            if (readTopicIds.Length != 0)
            {
                int[] readTopicIdsIntArray = StringHelper.GetIntArrayFromString(readTopicIds, ',');
                for (int i = 0; i < readTopicIdsIntArray.Length; i++)
                {
                    if (readTopicIdsIntArray[i] == this.TopicId)
                        return;
                }
                readTopicIds = readTopicIds + "," + Convert.ToString(TopicId);
            }
            else
            {
                readTopicIds = Convert.ToString(TopicId);
            }
            Framework.Common.CommonFunctions.WriteCookies("ReadTopicId", readTopicIds);

        }

        public virtual void Featured(UserOrOperator operatingUserOrOperator)
        {
            TopicAccess.UpdateTopicFeatureStatus(_conn, _transaction, _topicId, true);
            /*2.0 stategy */
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn,_transaction,_topicId,operatingUserOrOperator);
            UserOrOperator Author = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction,
                operatingUserOrOperator, topic.PostUserOrOperatorId);
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            scoreStrategySetting.UseAfterMarkedTopicAsFeature(topic, Author);
            /*2.0 reputation strategy*/
            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            reputationStrategySetting.UseAfterMarkedTopicAsFeature(topic, Author);
        }

        public virtual void UnFeatured()
        {
            TopicAccess.UpdateTopicFeatureStatus(_conn, _transaction, _topicId, false);
        }

        public virtual void Close()
        {
            TopicAccess.UpdateTopicStatus(_conn, _transaction, _topicId, true, _ifMarkedAsAnswer);
        }

        public virtual void Reopen()
        {
            TopicAccess.UpdateTopicStatus(_conn, _transaction, _topicId, false, _ifMarkedAsAnswer);
        }

        protected virtual void Move(int forumId, UserOrOperator operatingOperator)
        {
            ForumWithPermissionCheck oldForum = new ForumWithPermissionCheck(_conn, _transaction, _forumId, operatingOperator);
            int numberOfPosts = _numberOfReplies + 1;
            oldForum.DecreaseNumberOfTopicsByOne();
            oldForum.DecreaseNumberOfPosts(numberOfPosts);

            ForumWithPermissionCheck newForum = new ForumWithPermissionCheck(_conn, _transaction, forumId, operatingOperator);
            newForum.IncreaseNumberOfTopicsByOne();
            newForum.IncreaseNumberOfPosts(numberOfPosts);

            PostsOfTopicWithPermissionCheck posts = new PostsOfTopicWithPermissionCheck(_conn, _transaction, _topicId, operatingOperator);
            PostWithPermissionCheck post = posts.GetLastPost(operatingOperator);

            posts.MoveAllPosts(forumId);
            //move topic to other forum
            TopicAccess.UpdateTopicForumIdAndForumName(
                _conn, _transaction, _topicId, forumId, newForum.Name);
            //update current forum topic's information
            int topicId = TopicAccess.AddTopicWithMoved(
                _conn, _transaction, _forumId, _subject,
                _postUserOrOperatorId, _postTime, "", _score, _ifContainsPoll,
                _ifReplyRequired, _ifPayScoreRequired,
                _topicId, _postUserOrOperatorId, DateTime.UtcNow);
            //add first blank post
            PostsOfTopicWithPermissionCheck postsOfTopic = new PostsOfTopicWithPermissionCheck(
                _conn, _transaction, topicId, operatingOperator);
            int postId = postsOfTopic.AddPostWithMoved(true, _subject, _postUserOrOperatorId, _postTime, "");

            if (newForum.LastPostId < _lastPostId)
            {
                newForum.UpdateLastCreateInfo(post.PostUserOrOperatorId, post.PostTime, post.PostId, post.TopicId, post.Subject);
            }

            if (_topicId == oldForum.LastPostTopicId)
            {
                post = oldForum.GetLastPost();

                if (post == null)
                {
                    oldForum.UpdateLastCreateInfo(0, Convert.ToDateTime("1900-01-01"), 0, 0, string.Empty);
                }
                else
                {
                    oldForum.UpdateLastCreateInfo(post.PostUserOrOperatorId, post.PostTime, post.PostId, post.TopicId, post.Subject);
                }
            }
        }

        public virtual void SetSticky(UserOrOperator operatingUserOrOperator)
        {
            TopicAccess.UpdateTopicStickyStatus(_conn, _transaction, _topicId, true);
            /*2.0 stategy */
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, _topicId,
                operatingUserOrOperator);
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            UserOrOperator author = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, operatingUserOrOperator, PostUserOrOperatorId);
            scoreStrategySetting.UseAfterMarkedTopicAsSticky(topic, author);
            /*2.0 reputation strategy*/
            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            reputationStrategySetting.UseAfterMarkedTopicAsSticky(topic, operatingUserOrOperator);
        }

        public virtual void UnSticky()
        {
            TopicAccess.UpdateTopicStickyStatus(_conn, _transaction, _topicId, false);
        }

        public virtual void MarkedAsAnswer()
        {
            TopicAccess.UpdateTopicStatus(_conn, _transaction, _topicId, _ifClosed, true);
        }

        public virtual void UnMarkedAsAnswer()
        {
            TopicAccess.UpdateTopicStatus(_conn, _transaction, _topicId, _ifClosed, false);
        }

        public virtual void IncreaseNumberOfHitsByOne(UserOrOperator operatingUserOrOperator)
        {
            TopicAccess.UpdateTopicNumberOfHits(_conn, _transaction, _topicId, _numberOfHits + 1);
            /*2.0 stategy */
            if (this.IfMoveHistory == false)
            {
                UserOrOperator Author = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction,
                    operatingUserOrOperator, this.PostUserOrOperatorId);
                ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                    _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
                scoreStrategySetting.UseAfterHitTopic(Author);
                /*2.0 reputation strategy*/
                ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                    _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
                reputationStrategySetting.UseAfterHitTopic(Author);
            }
        }

        protected virtual void IncreaseNumberOfRepliesByOne(bool ifTopic, UserOrOperator operatingOperator)
        {
            if (!ifTopic)
            {
                TopicAccess.UpdateTopicNumberOfReplies(this._conn, this._transaction, this._topicId, this._numberOfReplies + 1);
            }

            ForumWithPermissionCheck forum = this.GetForum(operatingOperator);
            forum.IncreaseNumberOfPostsByOne();
        }

        protected virtual void DecreaseNumberOfRepliesByOne(UserOrOperator operatingOperator)
        {
            TopicAccess.UpdateTopicNumberOfReplies(_conn, _transaction, _topicId, _numberOfReplies - 1);

            ForumWithPermissionCheck forum = this.GetForum(operatingOperator);
            forum.DecreaseNumberOfPostsByOne();
        }

        protected virtual void UpdateLastPostInfo(int lastPostUserOrOperatorId, int lastPostId, DateTime lastPostTime, string subject, UserOrOperator operatingOperator)
        {
            TopicAccess.UpdateTopicLastPostInfo(this._conn, this._transaction, this._topicId, lastPostUserOrOperatorId, lastPostId, lastPostTime);

            ForumWithPermissionCheck forum = this.GetForum(operatingOperator);

            if (forum.LastPostId <= lastPostId)
            {
                PostWithPermissionCheck lastPost = forum.GetLastPost(operatingOperator);
                forum.UpdateLastCreateInfo(lastPost.PostUserOrOperatorId, lastPost.PostTime, lastPost.PostId, lastPost.TopicId, lastPost.Subject);
            }
        }

        public virtual void UpdateParticipatorIds(int newParticipatorId)
        {
            int length = 0;
            if (_participatorIds != null)
            {
                length = _participatorIds.Length;
            }
            int[] newParticipatorIds = new int[length + 1];
            for (int i = 0; i < length; i++)
            {
                newParticipatorIds[i] = _participatorIds[i];
            }
            newParticipatorIds[length] = newParticipatorId;

            TopicAccess.UpdateTopicParticipatorIds(_conn, _transaction, _topicId, newParticipatorIds);
        }



        private bool IfTopicHasDraft()
        {
            return DraftAccess.GetDraftByTopicId(_conn, _transaction, TopicId).Rows.Count == 0 ? false : true;
        }

        /*---------------------2.0------------------------*/
        public virtual void LogicDelete(UserOrOperator operatingUserOrOperator)
        {
            //delete topic
            if (IfDeleted == true)
            {
                ExceptionHelper.ThrowTopicNotExistException(_topicId);
            }
            TopicAccess.LogicDelete(_conn, _transaction, _topicId);

            //moved topics Of topic logicDelete
            TopicWithPermissionCheck[] movedTopics = GetMovedTopicOfTopic(operatingUserOrOperator);
            foreach (var movedTopic in movedTopics)
            {
                movedTopic.LogicDeleteTopicAndFirstPost();
            }
        }

        public virtual void LogicDeleteTopicAndFirstPost(UserOrOperator operatingUserOrOperator)
        {
            //delete topic's first post and itemself
            PostsOfTopicWithPermissionCheck postsOfTopic = new PostsOfTopicWithPermissionCheck(_conn, _transaction,
                _topicId, operatingUserOrOperator);
            PostWithPermissionCheck post = postsOfTopic.GetFirstPost();
            post.LogicDelete();
        }

        public virtual void Restore(UserOrOperator operatingUserOrOperator)
        {
            TopicAccess.Restore(_conn, _transaction, _topicId);

            //moved topics Of topic logicDelete
            TopicWithPermissionCheck[] movedTopics = GetMovedTopicOfTopic(operatingUserOrOperator);
            foreach (var movedTopic in movedTopics)
            {
                PostsOfTopicWithPermissionCheck postsOfTopic = new PostsOfTopicWithPermissionCheck(
                    _conn, _transaction,movedTopic.TopicId, operatingUserOrOperator);
                PostWithPermissionCheck post = postsOfTopic.GetFirstPost();
                post.Restore();
            }
        }

        public void RefuseModeration()
        {
            TopicAccess.RefuseModeration(_conn, _transaction, _topicId);
        }

        protected void AcceptModeration(UserOrOperator operatingUserOrOperator)
        {
            TopicAccess.AcceptModeration(_conn, _transaction, _topicId);

            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, _topicId,
                operatingUserOrOperator);

            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
           _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                 _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            UserOrOperator Author = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, operatingUserOrOperator,
                    this._postUserOrOperatorId);

            /*2.0 stategy */
            scoreStrategySetting.UseAfterApproveTopic(topic, Author);

            /*2.0 reputation strategy*/
            reputationStrategySetting.UseAfterApproveTopic(topic, Author);
        }

        public bool IfFavorite(UserOrOperator operatingUserOrOperator,int userOrOperatorId)
        {
            FavoritesWithPermissionCheck favorites = new FavoritesWithPermissionCheck(_conn, _transaction, 
                operatingUserOrOperator, operatingUserOrOperator.Id);
            return favorites.IfUserFavoriteTopic(operatingUserOrOperator, _topicId);
        }

        protected int GetCountOfFavoriteTimes(UserOrOperator operatingUserOrOperator)
        {
            FavoritesOfSiteWithPermissionCheck favorites = new FavoritesOfSiteWithPermissionCheck(_conn, _transaction, operatingUserOrOperator);
            return favorites.GetCountByTopicId(_topicId);
        }

        public bool IfSubscribe(UserOrOperator operatingUserOrOperator, int userOrOperatorId)
        {
            SubscribesWithPermissionCheck subscribes = new SubscribesWithPermissionCheck(_conn, _transaction,
                operatingUserOrOperator, userOrOperatorId);
            return subscribes.IfUserSubscribeTopic(userOrOperatorId, _topicId);
        }

        protected int GetCountOfSubscribeTimes(UserOrOperator operatingUserOrOperator)
        {
            SubscribesOfSiteWithPermissionCheck subscribes = new SubscribesOfSiteWithPermissionCheck(_conn, _transaction, operatingUserOrOperator);
            return subscribes.GetCountByTopicId(_topicId);
        }

        public Poll GetPoll()
        {
            return new Poll(_conn, _transaction, _topicId);
        }

        public TopicWithPermissionCheck[] GetMovedTopicOfTopic(UserOrOperator operatingUserOrOperator)
        {
            //TopicAccess.DeleteMovedTopic(_conn, _transaction, _topicId);
            DataTable dtMovedTopics = TopicAccess.GetMovedTopicsOfTopic(_conn, _transaction, _topicId);

            List<TopicWithPermissionCheck> MovedTopics = new List<TopicWithPermissionCheck>();
            for (int i = 0; i < dtMovedTopics.Rows.Count; i++)
            {
                TopicWithPermissionCheck topic = CreateTopicObject(dtMovedTopics.Rows[i], operatingUserOrOperator);
                MovedTopics.Add(topic);
            }
            return MovedTopics.ToArray();
        }

        protected TopicWithPermissionCheck CreateTopicObject(DataRow dr, UserOrOperator operatingUserOrOperator)
        {
            TopicWithPermissionCheck topic = null;

            if (dr != null)
            {

                topic = new TopicWithPermissionCheck(_conn, _transaction, Convert.ToInt32(dr["Id"]), operatingUserOrOperator, Convert.ToInt32(dr["ForumId"]),
                       Convert.ToString(dr["ForumName"]), Convert.ToString(dr["Subject"]),
                       dr["PostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(dr["PostUserOrOperatorId"]),
                       dr["PostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(dr["PostUserOrOperatorName"]),
                       dr["PostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(dr["PostUserOrOperatorIfDeleted"]),
                       Convert.ToDateTime(dr["PostTime"]),
                       Convert.ToInt32(dr["LastPostId"]),
                       Convert.ToDateTime(dr["LastPostTime"]),
                       dr["LastPostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(dr["LastPostUserOrOperatorId"]),
                       dr["LastPostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(dr["LastPostUserOrOperatorName"]),
                       dr["LastPostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(dr["LastPostUserOrOperatorIfDeleted"]),
                       PostAccess.GetCountOfNotDeletedPostsByTopicId(_conn, _transaction, Convert.ToInt32(dr["Id"])), Convert.ToInt32(dr["NumberOfHits"]),
                       Convert.ToBoolean(dr["IfClosed"]), Convert.ToBoolean(dr["IfMarkedAsAnswer"]),
                       Convert.ToBoolean(dr["IfSticky"]), StringHelper.GetIntArrayFromString(dr["ParticipatorIds"].ToString(),','));

            }
            return topic;
        }
    }
}
