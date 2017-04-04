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
    public abstract class PostsBase
    {  
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        public PostsBase(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            _conn = conn;
            _transaction = transaction;
        }

        protected PostWithPermissionCheck CreatePostObject(DataRow dr, UserOrOperator operatingUserOrOperator)
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
                bool ifPostUserOrOperatorCustomizeAvatar = false;
                /*for 1.0 topic.aspx page query posts*/
                if (dr.Table.Columns.Contains("IfCustomizeAvatar"))
                    ifPostUserOrOperatorCustomizeAvatar = Convert.ToBoolean(dr["IfCustomizeAvatar"]);
                else if (dr.Table.Columns.Contains("IfPostUserOrOperatorCustomizeAvatar"))
                    ifPostUserOrOperatorCustomizeAvatar = Convert.ToBoolean(dr["IfPostUserOrOperatorCustomizeAvatar"]);
                string postUserOrOperatorName = Convert.ToString(dr["PostUserOroperatorName"]);
                string postUserOrOperatorSystemAvatar="";
                /*for 1.0 topic.aspx page query posts*/
                if (dr.Table.Columns.Contains("PostUserOrOperatorSystemAvatar"))
                    postUserOrOperatorSystemAvatar = Convert.ToString(dr["PostUserOrOperatorSystemAvatar"]);
                string postUserOrOperatorCustomizeAvatar="";
                if (dr.Table.Columns.Contains("PostUserOrOperatorCustomizeAvatar"))
                     postUserOrOperatorCustomizeAvatar = Convert.ToString(dr["PostUserOrOperatorCustomizeAvatar"]);
                int postUserOrOperatorNumberOfPosts = 0;
                //if(dr.Table.Columns.Contains("PostUserOrOperatorNumberOfPosts"))
                //    postUserOrOperatorNumberOfPosts = Convert.ToInt32(dr["PostUserOrOperatorNumberOfPosts"]);//PostUserOrOperatorNumberOf
                //else if (dr.Table.Columns.Contains("Posts"))
                //    postUserOrOperatorNumberOfPosts = Convert.ToInt32(dr["Posts"]);
                postUserOrOperatorNumberOfPosts=Convert.ToInt32(PostAccess.GetCountOfNotDeletedPostsOfUserOrOperator(_conn,_transaction,"",postUserOrOperatorId,new DateTime(),new DateTime()));
                DateTime postUserOrOperatorJoinedTime = new DateTime();
                if(dr.Table.Columns.Contains("PostUserOrOperatorJoinedTime"))
                    postUserOrOperatorJoinedTime = Convert.ToDateTime(dr["PostUserOrOperatorJoinedTime"]);
                DateTime postTime = Convert.ToDateTime(dr["PostTime"]);
                bool ifAnswer = Convert.ToBoolean(dr["IfAnswer"]);
                int lastEditUserOrOperatorId = Convert.ToInt32(dr["LastEditUserOrOperatorId"]);
                string lastEditUserOrOperatorName = Convert.ToString(dr["LastEditUserOrOperatorName"]);
                DateTime lastEditTime = Convert.ToDateTime(dr["LastEditTime"]);
                String postUserOrOperatorSignature = Convert.ToString(dr["Signature"]);
                bool ifPostUserOrOperatorDeleted = Convert.ToBoolean(dr["IfPostUserOrOperatorDeleted"]);
                bool ifLastEditUserOrOperatorDeleted = Convert.ToBoolean(dr["ifLastEditUserOrOperatorDeleted"]);
                /*2.0*/
                bool ifDeleted = Convert.ToBoolean(dr["IfDeleted"]);
                short moderationStatus = Convert.ToInt16(dr["ModerationStatus"]);
                string textContent = Convert.ToString(dr["TextContent"]);

                post = new PostWithPermissionCheck(_conn, _transaction, postId, operatingUserOrOperator, topicId, ifTopic, layer, subject, content,
                    postUserOrOperatorId, postUserOrOperatorName, ifPostUserOrOperatorCustomizeAvatar, postUserOrOperatorSystemAvatar,
                    postUserOrOperatorCustomizeAvatar, postUserOrOperatorSignature, ifPostUserOrOperatorDeleted, postUserOrOperatorNumberOfPosts, postUserOrOperatorJoinedTime, postTime, ifAnswer,
                    lastEditUserOrOperatorId, lastEditUserOrOperatorName, ifLastEditUserOrOperatorDeleted, lastEditTime, textContent, ifDeleted, moderationStatus);
            }
            return post;
        }

        public abstract  PostWithPermissionCheck[] GetNotDeletedPostsByQueryAndPaging(
            UserOrOperator operatingUserOrOperator, string keywords,string name, DateTime startDate, 
            DateTime endDate, int pageIndex, int pageSize, string orderFiled,string orderMethod,out int CountOfPosts);

       // public abstract PostWithPermissionCheck[] GetNotDeletedPostsByQueryAndPaging(string keywords, int posterId, DateTime startDate, DateTime endDate, int pageIndex, int pageSize, out int CountOfPosts);
    }
}
