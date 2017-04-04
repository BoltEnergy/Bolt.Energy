#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2009                                                                               //
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
    public abstract class Posts
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        public Posts(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            this._conn = conn;
            this._transaction = transaction;
        }

        protected PostWithPermissionCheck CreatePostObject(DataRow dr, IUserOrOperator operatingUserOrOperator)
        {
            PostWithPermissionCheck post = null;
            if (dr != null)
            {
                int postId = Convert.ToInt32(dr["Id"]);
                int topicId = Convert.ToInt32(dr["TopicId"]);
                bool ifTopic = Convert.ToBoolean(dr["IfTopic"]);
                int layer = Convert.ToInt32(dr["Layer"]);
                string subject = Convert.ToString(dr["Subject"]);
                string content = Convert.ToString(dr["Content"]);
                int postUserOrOperatorId = Convert.ToInt32(dr["PostUserOrOperatorId"]);
                bool ifPostUserOrOperatorCustomizeAvatar = Convert.ToBoolean(dr["IfPostUserOrOperatorCustomizeAvatar"]);
                string postUserOrOperatorName = Convert.ToString(dr["PostUserOroperatorName"]);
                string postUserOrOperatorSystemAvatar = Convert.ToString(dr["PostUserOrOperatorSystemAvatar"]);
                string postUserOrOperatorCustomizeAvatar = "";// Convert.ToString(table.Rows[i]["PostUserOrOperatorCustomizeAvatar"]);
                int postUserOrOperatorNumberOfPosts = Convert.ToInt32(dr["PostUserOrOperatorNumberOfPosts"]);
                DateTime postUserOrOperatorJoinedTime = Convert.ToDateTime(dr["PostUserOrOperatorJoinedTime"]);
                DateTime postTime = Convert.ToDateTime(dr["PostTime"]);
                bool ifAnswer = Convert.ToBoolean(dr["IfAnswer"]);
                int lastEditUserOrOperatorId = Convert.ToInt32(dr["LastEditUserOrOperatorId"]);
                string lastEditUserOrOperatorName = Convert.ToString(dr["LastEditUserOrOperatorName"]);
                DateTime lastEditTime = Convert.ToDateTime(dr["LastEditTime"]);
                String postUserOrOperatorSignature = Convert.ToString(dr["Signature"]);
                bool ifPostUserOrOperatorDeleted = Convert.ToBoolean(dr["IfDeleted"]);
                bool ifLastEditUserOrOperatorDeleted = Convert.ToBoolean(dr["ifLastEditUserOrOperatorDeleted"]);

                post = new PostWithPermissionCheck(_conn, _transaction, postId, operatingUserOrOperator, topicId, ifTopic, layer, subject, content,
                    postUserOrOperatorId, postUserOrOperatorName, ifPostUserOrOperatorCustomizeAvatar, postUserOrOperatorSystemAvatar,
                    postUserOrOperatorCustomizeAvatar, postUserOrOperatorSignature, ifPostUserOrOperatorDeleted, postUserOrOperatorNumberOfPosts, postUserOrOperatorJoinedTime, postTime, ifAnswer,
                    lastEditUserOrOperatorId, lastEditUserOrOperatorName, ifLastEditUserOrOperatorDeleted, lastEditTime);
            }
            return post;
        }

        protected abstract  PostWithPermissionCheck[] GetNotDeletedPostsByQueryAndPaging(IUserOrOperator operatingUserOrOperator, string keywords, int posterId, DateTime startDate, DateTime endDate, int pageIndex, int pageSize, out int CountOfPosts);

        public abstract PostWithPermissionCheck[] GetNotDeletedPostsByQueryAndPaging(string keywords, int posterId, DateTime startDate, DateTime endDate, int pageIndex, int pageSize, out int CountOfPosts);
    }
}
