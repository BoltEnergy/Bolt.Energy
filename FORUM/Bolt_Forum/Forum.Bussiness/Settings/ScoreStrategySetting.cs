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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class ScoreStrategySetting
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        private bool _ifEnableScore;

        #region private fields
        private int _siteId;
        private int _registration;
        private int _firstLoginEveryDay;
        private int _addModerator;
        private int _removeModerator;
        private int _ban;
        private int _unban;
        private int _newTopic;
        private int _topicMarkedAsFeature;
        private int _topicMarkedAsSticky;
        private int _topicDeleted;
        private int _topicRestored;
        private int _topicAddedIntoFavorites;
        private int _topicRemovedFromFavorites;
        private int _topicViewed;
        private int _topicReplied;
        private int _topicVerifiedAsSpam;
        private int _vote;
        private int _pollVoted;
        private int _newPost;
        private int _postDeleted;
        private int _postRestored;
        private int _postVerifiedAsSpam;
        private int _postMarkedAsAnswer;
        private int _reportAbuse;
        private int _search;
        #endregion

        #region properties
        public int SiteId
        {
            get { return _siteId; }
        }
        public int Registration
        {
            get { return _registration; }
        }
        public int FirstLoginEveryDay
        {
            get { return _firstLoginEveryDay; }
        }
        public int AddModerator
        {
            get { return _addModerator; }
        }
        public int RemoveModerator
        {
            get { return _removeModerator; }
        }
        public int Ban
        {
            get { return _ban; }
        }
        public int Unban
        {
            get { return _unban; }
        }
        public int NewTopic
        {
            get { return _newTopic; }
        }
        public int TopicMarkedAsFeature
        {
            get { return _topicMarkedAsFeature; }
        }
        public int TopicMarkedAsSticky
        {
            get { return _topicMarkedAsSticky; }
        }
        public int TopicDeleted
        {
            get { return _topicDeleted; }
        }
        public int TopicRestored
        {
            get { return _topicRestored; }
        }
        public int TopicAddedIntoFavorites
        {
            get { return _topicAddedIntoFavorites; }
        }
        public int TopicRemovedFromFavorites
        {
            get { return _topicRemovedFromFavorites; }
        }
        public int TopicViewed
        {
            get { return _topicViewed; }
        }
        public int TopicReplied
        {
            get { return _topicReplied; }
        }
        public int TopicVerifiedAsSpam
        {
            get { return _topicVerifiedAsSpam; }
        }
        public int Vote
        {
            get { return _vote; }
        }
        public int PollVoted
        {
            get { return _pollVoted; }
        }
        public int NewPost
        {
            get { return _newPost; }
        }
        public int PostDeleted
        {
            get { return _postDeleted; }
        }
        public int PostRestored
        {
            get { return _postRestored; }
        }
        public int PostVerifiedAsSpam
        {
            get { return _postVerifiedAsSpam; }
        }
        public int PostMarkedAsAnswer
        {
            get { return _postMarkedAsAnswer; }
        }
        public int ReportAbuse
        {
            get { return _reportAbuse; ; }
        }
        public int Search
        {
            get { return _search; }
        }
        #endregion

        public ScoreStrategySetting(SqlConnectionWithSiteId conn, SqlTransaction transaction, int siteId)
        {
            _conn = conn;
            _transaction = transaction;
            DataTable table = ConfigAccess.GetScoreStrategy(conn,transaction);
            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowForumScoreStrategySettingNotExist();
            }
            else
            {
                _siteId = siteId;
                _ifEnableScore = Convert.ToBoolean(table.Rows[0]["IfEnableScore"]);
                _registration = Convert.ToInt32(table.Rows[0]["ScoreForRegistration"]);
                _firstLoginEveryDay = Convert.ToInt32(table.Rows[0]["ScoreForFirstLogin"]);
                _addModerator = Convert.ToInt32(table.Rows[0]["ScoreForAddModerator"]);
                _removeModerator = Convert.ToInt32(table.Rows[0]["ScoreForRemoveModerator"]);
                _ban = Convert.ToInt32(table.Rows[0]["ScroeForBan"]);
                _unban = Convert.ToInt32(table.Rows[0]["ScoreForUnban"]);
                _newTopic = Convert.ToInt32(table.Rows[0]["ScoreForNewTopic"]);
                _topicMarkedAsFeature = Convert.ToInt32(table.Rows[0]["ScoreForTopicMarkedAsFeature"]);
                _topicMarkedAsSticky = Convert.ToInt32(table.Rows[0]["ScoreForTopicMarkedAsSticky"]);
                _topicDeleted = Convert.ToInt32(table.Rows[0]["ScoreForTopicDeleted"]);
                _topicRestored = Convert.ToInt32(table.Rows[0]["ScoreForTopicRestored"]);
                _topicAddedIntoFavorites = Convert.ToInt32(table.Rows[0]["ScoreForTopicAddedIntoFavorites"]);
                _topicRemovedFromFavorites = Convert.ToInt32(table.Rows[0]["ScoreForTopicRemovedFromFavorites"]);
                _topicViewed = Convert.ToInt32(table.Rows[0]["ScoreForTopicViewed"]);
                _topicReplied = Convert.ToInt32(table.Rows[0]["ScoreForTopicReplied"]);
                _topicVerifiedAsSpam = Convert.ToInt32(table.Rows[0]["ScoreForTopicVerifiedAsSpam"]);
                _vote = Convert.ToInt32(table.Rows[0]["ScoreForVote"]);
                _pollVoted = Convert.ToInt32(table.Rows[0]["ScoreForPollVoted"]);
                _newPost = Convert.ToInt32(table.Rows[0]["ScoreForNewPost"]);
                _postDeleted = Convert.ToInt32(table.Rows[0]["ScoreForPostDeleted"]);
                _postRestored = Convert.ToInt32(table.Rows[0]["ScoreForPostRestored"]);
                _postVerifiedAsSpam = Convert.ToInt32(table.Rows[0]["ScoreForPostVerifiedAsSpam"]);
                _postMarkedAsAnswer = Convert.ToInt32(table.Rows[0]["ScoreForPostMarkedAsAnswer"]);
                _reportAbuse = Convert.ToInt32(table.Rows[0]["ScoreForReportAbuse"]);
                _search = Convert.ToInt32(table.Rows[0]["ScoreForSearch"]);
            }
           

        }
        private void CheckIfEnableScore()
        {
            if (!_ifEnableScore)
                ExceptionHelper.ThrowForumSettingsCloseScoreFunctio();
        }

        #region Public Function Update
        public virtual void Update(int registration, 
                                int firstLoginEveryDay,
                                int addModerator,
                                int removeModerator,
                                int ban,
                                int unban,
                                int newTopic,
                                int topicMarkedAsFeature,
                                int topicMarkedAsSticky,
                                int topicDeleted,
                                int topicRestored,
                                int topicAddedIntoFavorites,
                                int topicRemovedFromFavorites,
                                int topicViewed,
                                int topicReplied,
                                int topicVerifiedAsSpam,
                                int vote,
                                int pollVoted,
                                int newPost,
                                int postDeleted,
                                int postRestored,
                                int postVerifiedAsSpam,
                                int postMarkedAsAnswer,
                                int reportAbuse,
                                int search)
        {
            CheckIfEnableScore();
            ConfigAccess.UpdateScoreStrategy(_conn, _transaction, registration, firstLoginEveryDay, addModerator, removeModerator,
                ban, unban, newTopic, topicMarkedAsFeature, topicMarkedAsSticky, topicDeleted, topicRestored, topicAddedIntoFavorites,
                topicRemovedFromFavorites, topicViewed, topicReplied, topicVerifiedAsSpam, vote, pollVoted, newPost, postDeleted,
                postRestored, postVerifiedAsSpam, postMarkedAsAnswer, reportAbuse, search);
        }
        #endregion Public Function Update

        #region Public Function UseAfterRegistration
        public void UseAfterRegistration(UserWithPermissionCheck registerUser)
        {
            if (_ifEnableScore)
            {
                if (registerUser.IfDeleted) return;
                if (registerUser.ModerateStatus != EnumUserModerateStatus.enumDoNotNeed) return;
                if (registerUser.EmailVerificationStatus != EnumUserEmailVerificationStatus.enumDoNotNeed) return;
                registerUser.IncreaseScore(_registration);
            }
        }
        #endregion Public Function UseAfterRegistration

        #region Public Function UseAfterModerateRegisterUser
        public void UseAfterModerateRegisterUser(UserWithPermissionCheck registerUser)
        {
            if (_ifEnableScore)
            {
                if (registerUser.IfDeleted) return;
                if (registerUser.ModerateStatus != EnumUserModerateStatus.enumModerated) return;
                if (registerUser.EmailVerificationStatus == EnumUserEmailVerificationStatus.enumNotVerified) return;
                registerUser.IncreaseScore(_registration);
            }
        }
        #endregion Public Function UseAfterModerateRegisterUser

        #region Public Function UserAfterEmailVerification
        public void UserAfterEmailVerification(UserWithPermissionCheck registerUser)
        {
            if (_ifEnableScore)
            {
                if (registerUser.IfDeleted) return;
                if (!(registerUser.ModerateStatus == EnumUserModerateStatus.enumDoNotNeed || registerUser.ModerateStatus == EnumUserModerateStatus.enumModerated)) return;
                if (registerUser.EmailVerificationStatus != EnumUserEmailVerificationStatus.enumVerified) return;
                registerUser.IncreaseScore(_registration);
            }
        }
        #endregion Public Function UserAfterEmailVerification

        #region Public Function UseAfterLogin
        public void UseAfterLogin(UserOrOperator loginedUserOrOperator)
        {
            if (_ifEnableScore)
            {
                if (loginedUserOrOperator.IfDeleted) return;

                DateTime loginedTime = loginedUserOrOperator.LastLoginTime;
                DateTime now = DateTime.UtcNow;
                //TimeSpan loginTimeSpan = now.Subtract(loginedTime);
                int increasedValue = 0;

                //increase value for login
                if (now.Day - loginedTime.Day > 0)
                {
                    increasedValue += _firstLoginEveryDay;
                    loginedUserOrOperator.IncreaseScore(increasedValue);
                }                
            }
        }
        #endregion Public Function UseAfterLogin

        #region Public Function UserBeforeAddModerator
        public void UserBeforeAddModerator(UserOrOperator userOrOperator)
        {
            if (_ifEnableScore)
            {
                if (userOrOperator.IfDeleted) return;
                if (userOrOperator.IfModerator()) return;
                userOrOperator.IncreaseScore(_addModerator);
            }
        }
        #endregion Public Function UserBeforeAddModerator

        #region Public Function UserAfterRemoveModerator
        public void UserAfterRemoveModerator(UserOrOperator userOrOperator)
        {
            if (_ifEnableScore)
            {
                if (userOrOperator.IfDeleted) return;
                if (userOrOperator.IfModerator()) return;
                userOrOperator.IncreaseScore(_removeModerator);
            }
        }
        #endregion Public Function UserAfterRemoveModerator

        #region Public Function UseAfterBanUserOrOperator
        public void UseAfterBanUserOrOperator(UserOrOperator userOrOperator)
        {
            if (_ifEnableScore)
            {
                if (userOrOperator.IfDeleted) return;
                userOrOperator.IncreaseScore(_ban);
            }
        }
        #endregion Public Function UseAfterBanUserOrOperator

        #region Public Function UserAfterUnbanUserOrOperator
        public void UserAfterUnbanUserOrOperator(UserOrOperator userOrOperator)
        {
            if (_ifEnableScore)
            {
                if (userOrOperator.IfDeleted) return;
                userOrOperator.IncreaseScore(_unban);
            }
        }
        #endregion UserAfterUnbanUserOrOperator

        #region Public Function UseAfterPostTopic
        public void UseAfterPostTopic(TopicWithPermissionCheck postedTopic, UserOrOperator author)
        {
            if (_ifEnableScore)
            {
                if (postedTopic.ModerationStatus != EnumPostOrTopicModerationStatus.Accepted) return;
                if (author.IfDeleted) return;
                author.IncreaseScore(_newTopic);
            }
        }
        #endregion Public Function UseAfterPostTopic

        #region Public Function UseAfterApproveTopic
        public void UseAfterApproveTopic(TopicWithPermissionCheck postedTopic, UserOrOperator author)
        {
            UseAfterPostTopic(postedTopic, author);
        }
        #endregion Public Function UseAfterApproveTopic

        #region Public Function UseAfterMarkedTopicAsFeature
        public void UseAfterMarkedTopicAsFeature(TopicWithPermissionCheck topic, UserOrOperator author)
        {
            if (_ifEnableScore)
            {
                if (!topic.IfFeatured) return;
                if (author.IfDeleted) return;
                author.IncreaseScore(_topicMarkedAsFeature);
            }
        }
        #endregion Public Function UseAfterMarkedTopicAsFeature

        #region Public Function UseAfterMarkedTopicAsSticky
        public void UseAfterMarkedTopicAsSticky(TopicWithPermissionCheck topic, UserOrOperator author)
        {
            if (_ifEnableScore)
            {
                if (!topic.IfSticky) return;
                if (author.IfDeleted) return;
                author.IncreaseScore(_topicMarkedAsSticky);
            }
        }
        #endregion Public Function UseAfterMarkedTopicAsSticky

        #region Public Function UseAfterAddTopicToFavorites
        public void UseAfterAddTopicToFavorites(UserOrOperator topicAuthor)
        {
            if (_ifEnableScore)
            {
                if (topicAuthor.IfDeleted) return;
                topicAuthor.IncreaseScore(_topicAddedIntoFavorites);
            }
        }
        #endregion Public Function UseAfterAddTopicToFavorites

        #region Public Function UseAfterRemoveTopicFromFavorites
        public void UseAfterRemoveTopicFromFavorites(UserOrOperator topicAuthor)
        {
            if (_ifEnableScore)
            {
                if (topicAuthor.IfDeleted) return;
                topicAuthor.IncreaseScore(_topicRemovedFromFavorites);
            }
        }
        #endregion Public Function UseAfterRemoveTopicFromFavorites

        #region Public Function UseAfterHitTopic
        public void UseAfterHitTopic(UserOrOperator topicAuthor)
        {
            if (_ifEnableScore)
            {
                if (topicAuthor.IfDeleted) return;
                topicAuthor.IncreaseScore(_topicViewed);
            }
        }
        #endregion Public Function UseAfterHitTopic

        #region Public Function UseAfterReplyTopic
        public void UseAfterPostReply(PostWithPermissionCheck postedReply, UserOrOperator replyingUserOrOperator)
        {
            if (_ifEnableScore)
            {
                if (postedReply.ModerationStatus != EnumPostOrTopicModerationStatus.Accepted) return;
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, postedReply.TopicId, replyingUserOrOperator);
                replyingUserOrOperator.IncreaseScore(_newPost);
                if (topic.PostUserOrOperatorIfDeleted) return;
                UserOrOperator topicAuthor = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, replyingUserOrOperator, topic.PostUserOrOperatorId);
                topicAuthor.IncreaseScore(_topicReplied);
            }
        }
        #endregion Public Function UseAfterReplyTopic

        #region Public Function UseAfterApproveReply
        public void UseAfterApproveReply(PostWithPermissionCheck postedReply, UserOrOperator replyingUserOrOperator)
        {
            UseAfterPostReply(postedReply, replyingUserOrOperator);
        }
        #endregion Public Function UseAfterApproveReply

        #region Public Function UseAfterApproveAbuseReport
        public void UseAfterApproveAbuseReport(PostWithPermissionCheck AbusedPost, UserOrOperator postAuthor)
        {
            if (_ifEnableScore)
            {
                if (AbusedPost.IfPostUserOrOperatorDeleted) return;
                if (AbusedPost.IfTopic) postAuthor.IncreaseScore(_topicVerifiedAsSpam);
                else postAuthor.IncreaseScore(_postVerifiedAsSpam);
            }
        }
        #endregion Public Function UseAfterApproveAbuseReport

        #region Public Function UseAfterVote
        public void UseAfterVote(TopicWithPermissionCheck topic, UserOrOperator votingUserOrOperator)
        {
            if (_ifEnableScore)
            {
                votingUserOrOperator.IncreaseScore(_vote);
                if (topic.PostUserOrOperatorIfDeleted) return;
                UserOrOperator topicAuthor = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, votingUserOrOperator, topic.PostUserOrOperatorId);
                topicAuthor.IncreaseScore(_pollVoted);
            }
        }
        #endregion Public Function UseAfterVote

        #region Public Function UseAfterLogicDeletePost
        public void UseAfterLogicDeletePost(PostWithPermissionCheck deletedPost, UserOrOperator author)
        {
            if (_ifEnableScore)
            {
                if (!deletedPost.IfDeleted) return;
                if (author.IfDeleted) return;
                if (deletedPost.IfTopic) author.IncreaseScore(_topicDeleted);
                else author.IncreaseScore(_postDeleted);
            }
        }
        #endregion Public Function UseAfterLogicDeletePost

        #region Public Function UseAfterRestorePost
        public void UseAfterRestorePost(PostWithPermissionCheck post, UserOrOperator author)
        {
            if (_ifEnableScore)
            {
                if (post.IfDeleted) return;
                if (author.IfDeleted) return;
                if (post.IfTopic) author.IncreaseScore(_topicRestored);
                else author.IncreaseScore(_postRestored);
            }
        }
        #endregion Public Function UseAfterRestorePost

        #region Public Function UseAfterMarkPostAsAnswer
        public void UseAfterMarkPostAsAnswer(PostWithPermissionCheck post, UserOrOperator author)
        {
            if (_ifEnableScore)
            {
                if (post.IfPostUserOrOperatorDeleted) return;
                author.IncreaseScore(_postMarkedAsAnswer);
            }
        }
        #endregion Public Function UseAfterMarkPostAsAnswer

        #region Public Function UseAfterReportAbuse
        public void UseAfterReportAbuse(UserOrOperator ReportedUserOrOperator)
        {
            if (_ifEnableScore)
            {
                ReportedUserOrOperator.IncreaseScore(_reportAbuse);
            }
        }
        #endregion Public Function UseAfterReportAbuse

        #region Public Function UseAfterSearch
        public void UseAfterSearch(UserOrOperator searchingUserOrOperator)
        {
            if (_ifEnableScore)
            {
                searchingUserOrOperator.IncreaseScore(_search);
            }
        }
        #endregion Public Function UseAfterSearch
    }
}
