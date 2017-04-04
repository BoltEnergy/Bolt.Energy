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

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class PostsOfForum : PostsBase
    {

        private int _forumId;

        public PostsOfForum(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
            : base(conn, transaction)
        {
            this._forumId = forumId;
        }

        public PostsOfTopicWithPermissionCheck CreatePostsOfTopic(int topicId, UserOrOperator operatingUserOrOperator)
        {
            return new PostsOfTopicWithPermissionCheck(_conn, _transaction, topicId, operatingUserOrOperator);
        }

        public override PostWithPermissionCheck[] GetNotDeletedPostsByQueryAndPaging(
            UserOrOperator operatingUserOrOperator, string keywords, string name, DateTime startDate,
            DateTime endDate, int pageIndex, int pageSize, string orderFiled, string orderMethod, out int CountOfPosts)
        {
            CountOfPosts = PostAccess.GetCountOfNotDeletedPostsByQueryAndPagingOfSite(
               _conn, _transaction, keywords, name, startDate, endDate,_forumId);
            DataTable dt = PostAccess.GetNotDeletedPostsByQueryAndPagingOfSite(_conn, _transaction,
                keywords, name, startDate, endDate,
                pageIndex, pageSize, orderFiled, orderMethod, _forumId);
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
                    false, new DateTime(),"");
                posts.Add(post);
            }
            return posts.ToArray<PostWithPermissionCheck>();
        }

        protected PostWithPermissionCheck[] GetNotDeletedModerationPostsByQueryAndPaging(
            UserOrOperator operatingUserOrOperator, string keywords, int posterId, DateTime startDate,
            DateTime endDate, int pageIndex, int pageSize, out int CountOfPosts)
        {
            throw new NotImplementedException();
        }

        protected PostWithPermissionCheck[] GetNotDeletedRejectedPostsByQueryAndPaging(
            UserOrOperator operatingUserOrOperator, string keywords, int posterId, DateTime startDate, 
            DateTime endDate, int pageIndex, int pageSize, out int CountOfPosts)
        {
            throw new NotImplementedException();
        }

        //public abstract override PostWithPermissionCheck[] GetNotDeletedPostsByQueryAndPaging(string keywords, int posterId, DateTime startDate, DateTime endDate, int pageIndex, int pageSize, out int CountOfPosts);


    }
}
