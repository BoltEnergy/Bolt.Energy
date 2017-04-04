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
    public abstract class Search
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;


        #region private fields
        private string _topicSubject;
        private string _postUserOrOperatorName;
        private DateTime _startDate;
        private DateTime _endDate;
        private int[] _forumIds;
        private bool _ifClosed;
        private bool _ifMarkedAsAnswer;
        private bool _ifSticky;
        private String _status;

        /*2.0*/
        private bool _ifAllFroums;
        private bool _ifCategory;
        private bool _ifForum;
        private int _searchId;
        //private DateTime _BeginDate;
        //private DateTime _EndDate;
        private int _searchMethod;
        private string _keywords;
        #endregion

        #region property
        public string TopicSubject
        {
            set { this._topicSubject = value; }
            get { return this._topicSubject; }
        }
        public string PostUserOrOperatorName
        {
            set { this._postUserOrOperatorName = value; }
            get { return this._postUserOrOperatorName; }
        }
        public DateTime StartDate
        {
            set { this._startDate = value; }
            get { return this._startDate; }
        }
        public DateTime EndDate
        {
            set { this._endDate = value; }
            get { return this._endDate; }
        }
        public int[] ForumIds
        {
            set { this._forumIds = value; }
            get { return this._forumIds; }
        }
        public bool IfClosed
        {
            set { this._ifClosed = value; }
            get { return this._ifClosed; }
        }
        public bool IfMarkedAsAnswer
        {
            set { this._ifMarkedAsAnswer = value; }
            get { return this._ifMarkedAsAnswer; }
        }
        public bool IfSticky
        {
            set { this._ifSticky = value; }
            get { return this._ifSticky; }
        }
        #endregion

        public Search(SqlConnectionWithSiteId conn, SqlTransaction transaction, string subject1, string displayName,
             DateTime startTime, DateTime endTime, string status, bool ifSticky, bool ifAnswered)
        {
            this._conn = conn;
            this._transaction = transaction;
            this._topicSubject = subject1;
            this._postUserOrOperatorName = displayName;
            this._startDate = startTime;
            this._endDate = endTime;
            this._ifSticky = ifSticky;
            this._ifMarkedAsAnswer = ifAnswered;
            this._status = status;

        }
        /*2.0*/
        public Search(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            string keywords, bool ifAllForums, bool ifCategory
            , bool ifForum, int id, DateTime BeginDate, DateTime EndDate, int searchMethod)
        {
            _conn = conn;
            _transaction = transaction;
            _keywords = keywords;
            _ifAllFroums = ifAllForums;
            _ifCategory = ifCategory;
            _ifForum = ifForum;
            _searchId = id;
            _startDate = BeginDate;
            _endDate = EndDate;
            _searchMethod = searchMethod;
        }

        public Search(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            _conn = conn;
            _transaction = transaction;
        }


        public virtual TopicWithPermissionCheck[] GetTopicsByPagingAndSearchOptionsWithForumId(out int searchResultCount, int pageIndex, int pageSize, int forumId, UserOrOperator operatingOperator)
        {
            DataTable table = new DataTable();
            string subject = "%" + _topicSubject + "%";
            if (_ifMarkedAsAnswer)
            {
                if ("open".Equals(_status))
                {
                    this._ifClosed = false;
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInForumAndAnsweredWithOneStatus(_conn, _transaction, forumId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInForumAndAnsweredWithOneStatus(_conn, _transaction, forumId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);

                }
                else if ("close".Equals(_status))
                {
                    this._ifClosed = true;
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInForumAndAnsweredWithOneStatus(_conn, _transaction, forumId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInForumAndAnsweredWithOneStatus(_conn, _transaction, forumId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);

                }
                else if ("both".Equals(_status))
                {
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInForumAndAnsweredWithBothStatus(_conn, _transaction, forumId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInForumAndAnsweredWithBothStatus(_conn, _transaction, forumId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                }
                else
                {
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInForumAndAnsweredWithoutStatus(_conn, _transaction, forumId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInForumAndAnsweredWithoutStatus(_conn, _transaction, forumId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                }
            }
            else
            {
                if ("open".Equals(_status))
                {
                    this._ifClosed = false;
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInForumAndNotAnsweredWithOneStatus(_conn, _transaction, forumId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInForumAndNotAnsweredWithOneStatus(_conn, _transaction, forumId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);

                }
                else if ("close".Equals(_status))
                {
                    this._ifClosed = true;
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInForumAndNotAnsweredWithOneStatus(_conn, _transaction, forumId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInForumAndNotAnsweredWithOneStatus(_conn, _transaction, forumId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);

                }
                else
                {
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInForumAndNotAnsweredWithBothStatus(_conn, _transaction, forumId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInForumAndNotAnsweredWithBothStatus(_conn, _transaction, forumId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                }


            }
            TopicWithPermissionCheck[] topics = new TopicWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {


                topics[i] = new TopicWithPermissionCheck(_conn, _transaction, Convert.ToInt32(table.Rows[i]["Id"]), operatingOperator,
                    Convert.ToInt32(table.Rows[i]["ForumId"]), Convert.ToString(table.Rows[i]["ForumName"]), Convert.ToString(table.Rows[i]["Subject"]),
                    table.Rows[i]["PostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[i]["PostUserOrOperatorId"]),
                    table.Rows[i]["PostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[i]["PostUserOrOperatorName"]),
                    table.Rows[i]["PostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[i]["PostUserOrOperatorIfDeleted"]),
                    Convert.ToDateTime(table.Rows[i]["PostTime"]),
                    Convert.ToInt32(table.Rows[i]["LastPostId"]),
                    Convert.ToDateTime(table.Rows[i]["LastPostTime"]),
                    table.Rows[i]["LastPostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[i]["LastPostUserOrOperatorId"]),
                    table.Rows[i]["LastPostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[i]["LastPostUserOrOperatorName"]),
                    table.Rows[i]["LastPostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[i]["LastPostUserOrOperatorIfDeleted"]),
                    Convert.ToInt32(table.Rows[i]["NumberOfReplies"]), Convert.ToInt32(table.Rows[i]["NumberOfHits"]),
                    Convert.ToBoolean(table.Rows[i]["IfClosed"]), Convert.ToBoolean(table.Rows[i]["IfMarkedAsAnswer"]),
                    Convert.ToBoolean(table.Rows[i]["IfSticky"]), StringHelper.GetIntArrayFromString(table.Rows[i]["ParticipatorIds"].ToString(), ','));
            }
            return topics;

        }


        public virtual TopicWithPermissionCheck[] GetTopicsByPagingAndSearchOptionsWithCategoryId(out int searchResultCount, int pageIndex, int pageSize, int categoryId, UserOrOperator operatingOperator)
        {
            DataTable table = new DataTable();
            string subject = "%" + _topicSubject + "%";
            if (_ifMarkedAsAnswer)
            {
                if ("open".Equals(_status))
                {
                    this._ifClosed = false;
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInCategoryAndAnsweredWithOneStatus(_conn, _transaction, categoryId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInCategoryAndAnsweredWithOneStatus(_conn, _transaction, categoryId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);

                }
                else if ("close".Equals(_status))
                {
                    this._ifClosed = true;
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInCategoryAndAnsweredWithOneStatus(_conn, _transaction, categoryId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInCategoryAndAnsweredWithOneStatus(_conn, _transaction, categoryId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);

                }
                else if ("both".Equals(_status))
                {
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInCategoryAndAnsweredWithBothStatus(_conn, _transaction, categoryId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInCategoryAndAnsweredWithBothStatus(_conn, _transaction, categoryId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                }
                else
                {
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInCategoryAndAnsweredWithoutStatus(_conn, _transaction, categoryId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInCategoryAndAnsweredWithoutStatus(_conn, _transaction, categoryId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                }
            }
            else
            {
                if ("open".Equals(_status))
                {
                    this._ifClosed = false;
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInCategoryAndNotAnsweredWithOneStatus(_conn, _transaction, categoryId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInCategoryAndNotAnsweredWithOneStatus(_conn, _transaction, categoryId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);

                }
                else if ("close".Equals(_status))
                {
                    this._ifClosed = true;
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInCategoryAndNotAnsweredWithOneStatus(_conn, _transaction, categoryId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInCategoryAndNotAnsweredWithOneStatus(_conn, _transaction, categoryId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);

                }
                else
                {
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInCategoryAndNotAnsweredWithBothStatus(_conn, _transaction, categoryId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInCategoryAndNotAnsweredWithBothStatus(_conn, _transaction, categoryId, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                }


            }

            TopicWithPermissionCheck[] topics = new TopicWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {


                topics[i] = new TopicWithPermissionCheck(_conn, _transaction, Convert.ToInt32(table.Rows[i]["Id"]), operatingOperator,
                    Convert.ToInt32(table.Rows[i]["ForumId"]), Convert.ToString(table.Rows[i]["ForumName"]), Convert.ToString(table.Rows[i]["Subject"]),
                    table.Rows[i]["PostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[i]["PostUserOrOperatorId"]),
                    table.Rows[i]["PostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[i]["PostUserOrOperatorName"]),
                    table.Rows[i]["PostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[i]["PostUserOrOperatorIfDeleted"]),
                    Convert.ToDateTime(table.Rows[i]["PostTime"]),
                    Convert.ToInt32(table.Rows[i]["LastPostId"]),
                    Convert.ToDateTime(table.Rows[i]["LastPostTime"]),
                    table.Rows[i]["LastPostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[i]["LastPostUserOrOperatorId"]),
                    table.Rows[i]["LastPostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[i]["LastPostUserOrOperatorName"]),
                    table.Rows[i]["LastPostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[i]["LastPostUserOrOperatorIfDeleted"]),
                    Convert.ToInt32(table.Rows[i]["NumberOfReplies"]), Convert.ToInt32(table.Rows[i]["NumberOfHits"]),
                    Convert.ToBoolean(table.Rows[i]["IfClosed"]), Convert.ToBoolean(table.Rows[i]["IfMarkedAsAnswer"]),
                    Convert.ToBoolean(table.Rows[i]["IfSticky"]), StringHelper.GetIntArrayFromString(table.Rows[i]["ParticipatorIds"].ToString(), ','));
            }
            return topics;

        }


        public virtual TopicWithPermissionCheck[] GetTopicsByPagingAndSearchOptionsWithoutId(out int searchResultCount, int pageIndex, int pageSize, UserOrOperator operatingOperator)
        {
            DataTable table = new DataTable();
            string subject = "%" + _topicSubject + "%";
            if (_ifMarkedAsAnswer)
            {
                if ("open".Equals(_status))
                {
                    this._ifClosed = false;
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInAllAndAnsweredWithOneStatus(_conn, _transaction, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInAllAndAnsweredWithOneStatus(_conn, _transaction, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);

                }
                else if ("close".Equals(_status))
                {
                    this._ifClosed = true;
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInAllAndAnsweredWithOneStatus(_conn, _transaction, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInAllAndAnsweredWithOneStatus(_conn, _transaction, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);

                }
                else if ("both".Equals(_status))
                {
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInAllAndAnsweredWithBothStatus(_conn, _transaction, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInAllAndAnsweredWithBothStatus(_conn, _transaction, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                }
                else
                {
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInAllAndAnsweredWithoutStatus(_conn, _transaction, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInAllAndAnsweredWithoutStatus(_conn, _transaction, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                }
            }
            else
            {
                if ("open".Equals(_status))
                {
                    this._ifClosed = false;
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInAllAndNotAnsweredWithOneStatus(_conn, _transaction, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInAllAndNotAnsweredWithOneStatus(_conn, _transaction, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);

                }
                else if ("close".Equals(_status))
                {
                    this._ifClosed = true;
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInAllAndNotAnsweredWithOneStatus(_conn, _transaction, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInAllAndNotAnsweredWithOneStatus(_conn, _transaction, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, Convert.ToBoolean(_ifClosed), _ifSticky);

                }
                else
                {
                    searchResultCount = TopicAccess.GetCountOfTopicsBySearchInAllAndNotAnsweredWithBothStatus(_conn, _transaction, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                    table = TopicAccess.GetTopicsBySearchInAllAndNotAnsweredWithBothStatus(_conn, _transaction, pageIndex, pageSize, subject, _postUserOrOperatorName, _startDate, _endDate, _ifSticky);
                }


            }

            TopicWithPermissionCheck[] topics = new TopicWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {


                topics[i] = new TopicWithPermissionCheck(_conn, _transaction, Convert.ToInt32(table.Rows[i]["Id"]), operatingOperator, Convert.ToInt32(table.Rows[i]["ForumId"]),
                    Convert.ToString(table.Rows[i]["ForumName"]), Convert.ToString(table.Rows[i]["Subject"]),
                    table.Rows[i]["PostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[i]["PostUserOrOperatorId"]),
                    table.Rows[i]["PostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[i]["PostUserOrOperatorName"]),
                    table.Rows[i]["PostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[i]["PostUserOrOperatorIfDeleted"]),
                    Convert.ToDateTime(table.Rows[i]["PostTime"]),
                    Convert.ToInt32(table.Rows[i]["LastPostId"]),
                    Convert.ToDateTime(table.Rows[i]["LastPostTime"]),
                    table.Rows[i]["LastPostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[i]["LastPostUserOrOperatorId"]),
                    table.Rows[i]["LastPostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[i]["LastPostUserOrOperatorName"]),
                    table.Rows[i]["LastPostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[i]["LastPostUserOrOperatorIfDeleted"]),
                    Convert.ToInt32(table.Rows[i]["NumberOfReplies"]), Convert.ToInt32(table.Rows[i]["NumberOfHits"]),
                    Convert.ToBoolean(table.Rows[i]["IfClosed"]), Convert.ToBoolean(table.Rows[i]["IfMarkedAsAnswer"]),
                    Convert.ToBoolean(table.Rows[i]["IfSticky"]), StringHelper.GetIntArrayFromString(table.Rows[i]["ParticipatorIds"].ToString(), ','));
            }
            return topics;

        }

        /*2.0*/
        public virtual TopicWithPermissionCheck[] GetTopicsByPagingAndSearchOptions(
            out int searchResultCount, int pageIndex, int pageSize, UserOrOperator operatingOperator)
        {
            List<int> forumIdsWithNoPermission = GetForumWithNoPermission(operatingOperator);
            searchResultCount = TopicAccess.GetCountOfTopicsByAdvancedSearch(
                _conn, _transaction, pageIndex, pageSize, _keywords, _ifAllFroums, _ifCategory, _ifForum, _searchId,
                _startDate, _endDate, _searchMethod, forumIdsWithNoPermission);
            DataTable table = TopicAccess.GetTopicsByAdvancedSearch(
               _conn, _transaction, pageIndex, pageSize, _keywords, _ifAllFroums, _ifCategory, _ifForum, _searchId,
                _startDate, _endDate, _searchMethod, forumIdsWithNoPermission);


            List<TopicWithPermissionCheck> topics = new List<TopicWithPermissionCheck>();
            //TopicWithPermissionCheck[] topics = new TopicWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                #region New one Topic
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(
                _conn, _transaction,
                Convert.ToInt32(table.Rows[i]["Id"]),
                operatingOperator, Convert.ToInt32(table.Rows[i]["ForumId"]),
                Convert.ToString(table.Rows[i]["ForumName"]),
                Convert.ToString(table.Rows[i]["Subject"]),
                table.Rows[i]["PostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[i]["PostUserOrOperatorId"]),
                table.Rows[i]["PostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[i]["PostUserOrOperatorName"]),
                table.Rows[i]["PostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[i]["PostUserOrOperatorIfDeleted"]),
                Convert.ToDateTime(table.Rows[i]["PostTime"]),
                Convert.ToInt32(table.Rows[i]["LastPostId"]),
                Convert.ToDateTime(table.Rows[i]["LastPostTime"]),
                table.Rows[i]["LastPostUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[i]["LastPostUserOrOperatorId"]),
                table.Rows[i]["LastPostUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[i]["LastPostUserOrOperatorName"]),
                table.Rows[i]["LastPostUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[i]["LastPostUserOrOperatorIfDeleted"]),
                PostAccess.GetCountOfNotDeletedPostsByTopicId(_conn, _transaction, Convert.ToInt32(table.Rows[i]["Id"])), 
                Convert.ToInt32(table.Rows[i]["NumberOfHits"]),
                Convert.ToBoolean(table.Rows[i]["IfClosed"]), Convert.ToBoolean(table.Rows[i]["IfMarkedAsAnswer"]),
                Convert.ToBoolean(table.Rows[i]["IfSticky"]), StringHelper.GetIntArrayFromString(table.Rows[i]["ParticipatorIds"].ToString(), ',')

                );
                #endregion
                topics.Add(topic);
            }
            return topics.ToArray<TopicWithPermissionCheck>();

        }

        public virtual PostWithPermissionCheck[] GetPostsByPagingAndSearchOptions(
           out int searchResultCount, int pageIndex, int pageSize, UserOrOperator operatingOperator)
        {
             List<int> forumIdsWithNoPermission = GetForumWithNoPermission(operatingOperator);
            searchResultCount = PostAccess.GetCountOfPostsByAdvancedSearch(
                _conn, _transaction, pageIndex, pageSize, _keywords, _ifAllFroums, _ifCategory, _ifForum, _searchId,
                _startDate, _endDate, _searchMethod, forumIdsWithNoPermission);
            DataTable table = PostAccess.GetPostsByAdvancedSearch(
               _conn, _transaction, pageIndex, pageSize, _keywords, _ifAllFroums, _ifCategory, _ifForum, _searchId,
                _startDate, _endDate, _searchMethod, forumIdsWithNoPermission);


            List<PostWithPermissionCheck> posts = new List<PostWithPermissionCheck>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                #region New one Topic
                PostWithPermissionCheck post = new PostWithPermissionCheck(
                _conn, _transaction,
                Convert.ToInt32(table.Rows[i]["Id"]),
                operatingOperator,
                Convert.ToInt32(table.Rows[i]["TopicId"]),
                Convert.ToBoolean(table.Rows[i]["IfTopic"]),
                0,//Convert.ToInt32(table.Rows[i]["Layer"]),
                Convert.ToString(table.Rows[i]["Subject"]),
                Convert.ToString(table.Rows[i]["Content"]),
                Convert.ToInt32(table.Rows[i]["PostUserOrOperatorId"]),
               Convert.ToString(table.Rows[i]["PostUserOrOperatorName"]),
                false, //Convert.ToString(table.Rows[i]["PostUserOrOperatorSystemAvatar"]),
                "",//Convert.ToString(table.Rows[i]["PostUserOrOperatorCustomizeAvatar"]),
                "",
                "",//Signature
                Convert.ToBoolean(table.Rows[i]["PostUserOrOperatorIfDeleted"]),
                0,//Convert.ToInt32(table.Rows[i]["PostUserOrOperatorNumberOfPosts"]),
                Convert.ToDateTime(table.Rows[i]["PostUserOrOperatorJoinedTime"]),
                Convert.ToDateTime(table.Rows[i]["PostTime"]),
                Convert.ToBoolean(table.Rows[i]["IfAnswer"]),
                1,// Convert.ToInt32(table.Rows[i]["LastEditUserOrOperatorId"]),
                "",//Convert.ToString(table.Rows[i]["LastEditUserOrOperatorName"]),
                false,//Convert.ToBoolean(table.Rows[i]["IfLastEditUserOrOperatorDeleted"]),
                new DateTime(),//Convert.ToDateTime(table.Rows[i]["LastEditTime"]),
                Convert.ToString(table.Rows[i]["TextContent"])
                );
                //PostWithPermissionCheck post = new PostWithPermissionCheck(
                //    _conn, _transaction, Convert.ToInt32(table.Rows[i]["Id"]), operatingOperator);
                #endregion
                posts.Add(post);
            }
            return posts.ToArray<PostWithPermissionCheck>();

        }

        protected void SearchResult(UserOrOperator operatingUserOrOperator)
        {
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(_conn, _transaction,
                operatingUserOrOperator, _conn.SiteId);
            scoreStrategySetting.UseAfterSearch(operatingUserOrOperator);

            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(_conn, _transaction,
                operatingUserOrOperator, _conn.SiteId);
            reputationStrategySetting.UseAfterSearch(operatingUserOrOperator);
        }


        private List<int> GetForumWithNoPermission(UserOrOperator operatingUserOrOperator)
        {
            List<int> forumIds;
            ForumsOfSiteWithPermissionCheck forumsOfSite = new ForumsOfSiteWithPermissionCheck(_conn, _transaction, operatingUserOrOperator);
            List<ForumWithPermissionCheck> AllforumsOfSite = new List<ForumWithPermissionCheck>(forumsOfSite.GetAllForums());

            if (CommFun.IfGuest())
            {
                var forumIdsWithClosed = from c in AllforumsOfSite
                                         where c.Status != EnumForumStatus.Open
                                         select c.ForumId;
                forumIds = forumIdsWithClosed.ToList<int>();
            }
            else if (CommFun.IfAdminInUI(operatingUserOrOperator))
            {
                var forumIdsWithClosed = from c in AllforumsOfSite
                                         where c.Status != EnumForumStatus.Open
                                         select c.ForumId;
                forumIds = forumIdsWithClosed.ToList<int>();
            }
            else if (CommFun.IfModeratorOfSiteInUI(operatingUserOrOperator))
            {

                ForumsOfModeratorWithPermissionCheck forumsOfModerator = new ForumsOfModeratorWithPermissionCheck(_conn, _transaction,
                    operatingUserOrOperator, operatingUserOrOperator.Id);
                List<int> forumIdsHavePermission = (from o in forumsOfModerator.GetAllForums()
                                                    //from b in GetForumIdsWithHavePermissionInUserPermissionCache()
                                                    select o.ForumId).ToList();
                forumIdsHavePermission.AddRange(GetForumIdsWithHavePermissionInUserPermissionCache());
                var forumIdsWithClosed = //from o in forumIdsHavePermission.ToList()
                                         from c in AllforumsOfSite
                                         where !forumIdsHavePermission.Contains(c.ForumId) || (c.Status != EnumForumStatus.Open)
                                         select c.ForumId;
                forumIds = forumIdsWithClosed.ToList<int>();
            }
            else // Common Logined User
            {
                List<int> forumIdsHavePermission = GetForumIdsWithHavePermissionInUserPermissionCache();
                var forumIdsWithClosed = //from o in forumIdsHavePermission
                                           from c in AllforumsOfSite
                                           where !forumIdsHavePermission.Contains(c.ForumId) || (c.Status != EnumForumStatus.Open)
                                           select c.ForumId;
                forumIds = forumIdsWithClosed.ToList<int>();
            }

            //forumIds.Distinct();
            return forumIds;
        }

        private List<int> GetForumIdsWithHavePermissionInUserPermissionCache()
        {
            List<int> forumIds = new List<int>();
            foreach (var forumId in CommFun.UserPermissionCache().UserForumPermissionsList.Keys)
            {
                UserForumPermissionItem item = CommFun.UserPermissionCache().UserForumPermissionsList[forumId] as UserForumPermissionItem;
                if (item.IfAllowViewForum && item.IfAllowViewTopic)
                    forumIds.Add(Convert.ToInt32(forumId));
            }
            return forumIds;
        }
    }
}
