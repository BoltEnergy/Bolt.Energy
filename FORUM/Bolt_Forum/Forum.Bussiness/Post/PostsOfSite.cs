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
    public abstract class PostsOfSite : PostsBase
    {
        public PostsOfSite(SqlConnectionWithSiteId conn, SqlTransaction transaction)
            : base(conn, transaction)
        { }

        public PostsOfForumWithPermissionCheck CreatePostsOfForum(int forumId, UserOrOperator operatingUserOrOperator)
        {
            return new PostsOfForumWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, forumId);
        }

        public override PostWithPermissionCheck[] GetNotDeletedPostsByQueryAndPaging(
           UserOrOperator operatingUserOrOperator, string keywords,  string name, DateTime startDate,
            DateTime endDate, int pageIndex, int pageSize, string orderFiled, string orderMethod, out int CountOfPosts)
        {
            CountOfPosts = PostAccess.GetCountOfAllNotDeletedPostsByQueryAndPagingOfSite(
                _conn,_transaction,keywords,name,startDate,endDate);
            DataTable dt = PostAccess.GetAllNotDeletedPostsByQueryAndPagingOfSite(_conn, _transaction, 
                keywords, name, startDate, endDate,
                pageIndex, pageSize, orderFiled, orderMethod);
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
                    _conn, _transaction,postId,operatingUserOrOperator,PosttopicId,
                    false,-1,subject,"",postUserOrOperatorId,postUserOrOperatorName,
                    false,"","","",false,-1,new DateTime(),postTime,ifAnswer,-1,"",
                    false,new DateTime(),"");
                posts.Add(post);
            }
            return posts.ToArray<PostWithPermissionCheck>();
        }

        //public abstract override PostWithPermissionCheck[] GetNotDeletedPostsByQueryAndPaging(string keywords, int posterId, DateTime startDate, DateTime endDate, int pageIndex, int pageSize, out int CountOfPosts);

        protected PostWithPermissionCheck[] GetDeletedPostsByQueryAndPaging(
            UserOrOperator operatingUserOrOperator, 
            string keywords, string name, DateTime startDate, DateTime endDate,
            int pageIndex, int pageSize, string orderFiled,string orderMethod, out int CountOfPosts)
        {
            CountOfPosts = PostAccess.GetCountOfDeletedPostsByQueryAndPaging(
                _conn,_transaction,keywords,name,startDate,endDate);
            DataTable dt = PostAccess.GetDeletedPostsByQueryAndPaging(
                _conn, _transaction, keywords, name, startDate, endDate,
                pageIndex, pageSize, orderFiled, orderMethod);
            List<PostWithPermissionCheck> posts = new List<PostWithPermissionCheck>();
            foreach(DataRow dr in dt.Rows)
            {
                #region Init Data
                int postId = Convert.ToInt32(dr["Id"]);
                int PosttopicId = Convert.ToInt32(dr["TopicId"]);
                string subject = Convert.ToString(dr["Subject"]);
                //bool ifAnswer = Convert.ToBoolean(dr["IfAnswer"]);
                DateTime postTime = Convert.ToDateTime(dr["PostTime"]);
                int postUserOrOperatorId = Convert.ToInt32(dr["PostUserOrOperatorId"]);
                string postUserOrOperatorName = Convert.ToString(dr["PostUserOrOperatorName"]);
                #endregion 

                PostWithPermissionCheck post = new PostWithPermissionCheck(
                    _conn, _transaction, postId, operatingUserOrOperator, PosttopicId,
                    false, -1, subject, "", postUserOrOperatorId, postUserOrOperatorName,
                    false, "", "", "", false, -1, new DateTime(), postTime, false, -1, "",
                    false, new DateTime(),"");
                posts.Add(post);
            }
            return posts.ToArray<PostWithPermissionCheck>();
        }

        protected PostWithPermissionCheck[] GetNotDeletedModerationPostsOfSiteByQueryAndPaging(
          UserOrOperator operatingUserOrOperator, int pageIndex, int pageSize,
           string queryConditions, string orderField, string orderMethod, out int CountOfPosts)
        {
            CountOfPosts = PostAccess.GetCountOfNotDeletedModerationPostsOfSiteByQueryAndPaging(
                _conn,_transaction,queryConditions);
            DataTable dt = PostAccess.GetNotDeletedModerationPostsOfSiteByQueryAndPaging(
                _conn,_transaction,pageIndex,pageSize,queryConditions,orderField,orderMethod);
            List<PostWithPermissionCheck> posts = new List<PostWithPermissionCheck>();
            foreach (DataRow dr in dt.Rows)
            {
                #region Init Data
                int postId = Convert.ToInt32(dr["Id"]);
                int PosttopicId = Convert.ToInt32(dr["TopicId"]);
                string subject = Convert.ToString(dr["Subject"]);
                //bool ifAnswer = Convert.ToBoolean(dr["IfAnswer"]);
                DateTime postTime = Convert.ToDateTime(dr["PostTime"]);
                int postUserOrOperatorId = Convert.ToInt32(dr["PostUserOrOperatorId"]);
                string postUserOrOperatorName = Convert.ToString(dr["PostUserOrOperatorName"]);
                bool ifPostUserOrOperatorNameDeleted = Convert.ToBoolean(dr["IfDeleted"]);
                #endregion

                PostWithPermissionCheck post = new PostWithPermissionCheck(
                    _conn, _transaction, postId, operatingUserOrOperator, PosttopicId,
                    false, -1, subject, "", postUserOrOperatorId, postUserOrOperatorName,
                    false, "", "", "", ifPostUserOrOperatorNameDeleted, -1, new DateTime(), postTime, false, -1, "",
                    false, new DateTime(),"");
               posts.Add(post);
            }
            return posts.ToArray<PostWithPermissionCheck>();
        }

        protected PostWithPermissionCheck[] GetNotDeletedRejectedPostsOfSiteByQueryAndPaging(
          UserOrOperator operatingUserOrOperator, int pageIndex, int pageSize,
           string queryConditions, string orderField, string orderMethod, out int CountOfPosts)
        {
            CountOfPosts = PostAccess.GetCountOfNotDeletedRejectedPostsOfSiteByQueryAndPaging(
                _conn,_transaction,queryConditions);
            DataTable dt = PostAccess.GetNotDeletedRejectedPostsOfSiteByQueryAndPaging(
                _conn, _transaction, pageIndex, pageSize, queryConditions, orderField, orderMethod);
            List<PostWithPermissionCheck> posts = new List<PostWithPermissionCheck>();
            foreach (DataRow dr in dt.Rows)
            {
                #region Init Data
                int postId = Convert.ToInt32(dr["Id"]);
                int PosttopicId = Convert.ToInt32(dr["TopicId"]);
                string subject = Convert.ToString(dr["Subject"]);
                //bool ifAnswer = Convert.ToBoolean(dr["IfAnswer"]);
                DateTime postTime = Convert.ToDateTime(dr["PostTime"]);
                int postUserOrOperatorId = Convert.ToInt32(dr["PostUserOrOperatorId"]);
                string postUserOrOperatorName = Convert.ToString(dr["PostUserOrOperatorName"]);
                #endregion

                PostWithPermissionCheck post = new PostWithPermissionCheck(
                    _conn, _transaction, postId, operatingUserOrOperator, PosttopicId,
                    false, -1, subject, "", postUserOrOperatorId, postUserOrOperatorName,
                    false, "", "", "", false, -1, new DateTime(), postTime, false, -1, "",
                    false, new DateTime(),"");
                posts.Add(post);
            }
            return posts.ToArray<PostWithPermissionCheck>();
        }

        #region Moderator panel
        protected PostWithPermissionCheck[] GetNotDeletedModerationPostsByModeratorWithQueryAndPaging(
            UserOrOperator operatingUserOrOperator,int moderatorId,string queryConditions,int pageIndex,int pageSize,
            string orderField,string orderDirection,EnumPostOrTopicModerationStatus status)
        {
            DataTable dt = PostAccess.GetNotDeletedModerationPostsByModeratorWithQueryAndPaging(
                 _conn, _transaction, moderatorId,queryConditions, pageIndex, pageSize, orderField, orderDirection, status);
            List<PostWithPermissionCheck> posts = new List<PostWithPermissionCheck>();
            foreach (DataRow dr in dt.Rows)
            {
                #region Init Data
                int postId = Convert.ToInt32(dr["Id"]);
                int PosttopicId = Convert.ToInt32(dr["TopicId"]);
                string subject = Convert.ToString(dr["Subject"]);
                //bool ifAnswer = Convert.ToBoolean(dr["IfAnswer"]);
                DateTime postTime = Convert.ToDateTime(dr["PostTime"]);
                int postUserOrOperatorId = Convert.ToInt32(dr["PostUserOrOperatorId"]);
                string postUserOrOperatorName = Convert.ToString(dr["Name"]);
                #endregion

                PostWithPermissionCheck post = new PostWithPermissionCheck(
                    _conn, _transaction, postId, operatingUserOrOperator, PosttopicId,
                    false, -1, subject, "", postUserOrOperatorId, postUserOrOperatorName,
                    false, "", "", "", false, -1, new DateTime(), postTime, false, -1, "",
                    false, new DateTime(), "");
                posts.Add(post);
            }
            return posts.ToArray<PostWithPermissionCheck>();
        }

        protected int GetCountOfNotDeletedModerationPostsByModeratorWithQuery(UserOrOperator operatingUserOrOperator, int moderatorId, string queryConditions, EnumPostOrTopicModerationStatus status)
        {
            return PostAccess.GetCountOfNotDeletedModerationPostsByModeratorWithQuery(_conn, _transaction, moderatorId, queryConditions, status);
        }

        protected PostWithPermissionCheck[] GetNotDeletePostsByModeratorWithQueryAndPaging(UserOrOperator operatingUserOrOperator,
            int moderatorId,string keywords,string name,DateTime startDate,DateTime endDate,int forumId,
            int pageIndex,int pageSize,string orderField,string orderDirection)
        {
            DataTable dt ;
            if (forumId >= 0)
                dt= PostAccess.GetNotDeletedPostsByModeratorWithQueryAndPaging(_conn, _transaction, 
                moderatorId, keywords,  name, startDate, endDate, forumId, pageIndex, pageSize, orderField, orderDirection);
            else
                dt = PostAccess.GetAllNotDeletedPostsByModeratorWithQueryAndPaging(_conn, _transaction,
                moderatorId, keywords, name, startDate, endDate, pageIndex, pageSize, orderField, orderDirection);
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
                    false, "", "", "", false, -1, new DateTime(), postTime,ifAnswer, -1, "",
                    false, new DateTime(), "");
                posts.Add(post);
            }
            return posts.ToArray<PostWithPermissionCheck>();
        }

        protected int GetCountOfNotDeletePostsByModeratorWitheQuery(int moderatorId
            , string keywords, string name, DateTime startDate, DateTime endDate, int forumId)
        {
            if (forumId >= 0)
                return PostAccess.GetCountOfNotDeletedPostsByModeratorWithQuery(_conn, _transaction
                    , moderatorId, keywords, name, startDate, endDate, forumId);
            else
                return PostAccess.GetCountOfAllNotDeletedPostsByModeratorWithQuery(_conn, _transaction
                    , moderatorId, keywords, name, startDate, endDate);
        }
        
        protected PostWithPermissionCheck[] GetDeletedPostsByModeratorWithQueryAndPaging(UserOrOperator operatingUserOrOperator,
            int moderatorId,string keywords,string name, DateTime startDate,DateTime endDate,int pageIndex,int pageSize,string orderField,string orderDirection)
        {
            DataTable dt = PostAccess.GetDeletedPostsByModeratorWithQueryAndPaging(_conn, _transaction, moderatorId, startDate, endDate, keywords, name, pageIndex, pageSize, orderField, orderDirection);
            List<PostWithPermissionCheck> posts = new List<PostWithPermissionCheck>();
            foreach (DataRow dr in dt.Rows)
            {
                #region Init Data
                int postId = Convert.ToInt32(dr["Id"]);
                int PosttopicId = Convert.ToInt32(dr["TopicId"]);
                string subject = Convert.ToString(dr["Subject"]);
                //bool ifAnswer = Convert.ToBoolean(dr["IfAnswer"]);
                DateTime postTime = Convert.ToDateTime(dr["PostTime"]);
                int postUserOrOperatorId = Convert.ToInt32(dr["PostUserOrOperatorId"]);
                string postUserOrOperatorName = Convert.ToString(dr["Name"]);
                #endregion

                PostWithPermissionCheck post = new PostWithPermissionCheck(
                    _conn, _transaction, postId, operatingUserOrOperator, PosttopicId,
                    false, -1, subject, "", postUserOrOperatorId, postUserOrOperatorName,
                    false, "", "", "", false, -1, new DateTime(), postTime, false, -1, "",
                    false, new DateTime(), "");
                posts.Add(post);
            }
            return posts.ToArray<PostWithPermissionCheck>();
        }
        protected int GetCountOfDeletedPostsByModeratorWithQuery(int moderatorId
            , string keywords, string name, DateTime startDate, DateTime endDate)
        {
            return PostAccess.GetCountOfDeletedPostsByModeratorWithQuery(_conn, _transaction
                , moderatorId, startDate, endDate, keywords, name);
        }

        #endregion
    }
}
