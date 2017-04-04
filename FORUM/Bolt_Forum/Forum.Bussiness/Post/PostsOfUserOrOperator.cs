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

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class PostsOfUserOrOperator
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;
        private int _userOrOperatorId;
        public int UserOrOperatorId
        {
            get { return this._userOrOperatorId; }
        }

        public PostsOfUserOrOperator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        {
            _conn = conn;
            _transaction = transaction;
            _userOrOperatorId = userOrOperatorId;
        }

        protected PostWithPermissionCheck[] GetPostsByQueryAndPaging(int pageIndex, int pageSize, string keyword,
            DateTime startDate, DateTime endDate, out int count, UserOrOperator operatingUserOrOperator)
        {
            count = PostAccess.GetCountOfNotDeletedPostsOfUserOrOperator(_conn, _transaction, keyword, _userOrOperatorId,
                startDate, endDate);
            List<PostWithPermissionCheck> posts = new List<PostWithPermissionCheck>();
            DataTable dt = PostAccess.GetNotDeletedPostsOfUserOrOperator(_conn, _transaction,
                keyword, _userOrOperatorId, startDate, endDate, pageIndex, pageSize);
            foreach (DataRow dr in dt.Rows)
            {
                int topicId = Convert.ToInt32(dr["TopicId"]);
                string subject = Convert.ToString(dr["Subject"]);
                string content = Convert.ToString(dr["Content"]);
                string postUserOrOperatorName = Convert.ToString(dr["PostUserOrOperatorName"]);
                bool ifTopic = Convert.ToBoolean(dr["IfTopic"]);
                int postId = Convert.ToInt32(dr["Id"]);
                DateTime postTime = Convert.ToDateTime(dr["PostTime"]);
                PostWithPermissionCheck post = new PostWithPermissionCheck(_conn, _transaction, postId, operatingUserOrOperator,
                    topicId, ifTopic, -1, subject, content, -1, postUserOrOperatorName,
                    false, "", "", "", false, 0, new DateTime(), postTime, false, -1,
                    "", false, new DateTime(), "");
                posts.Add(post);
            }
            return posts.ToArray<PostWithPermissionCheck>();
        }

        public DateTime GetLastPostTime(UserOrOperator operatingUserOrOperator)
        {
            return PostAccess.GetLastPostTime(_conn, _transaction, operatingUserOrOperator.Id);
        }

    }
}
