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
    public class PostsFactory
    {
        public static PostsBase CreatePost(int forumId, int topicId, SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
        {
            PostsBase post = null;
            if (forumId <=0)
            {
                post = new PostsOfSiteWithPermissionCheck(conn, transaction, operatingUserOrOperator);      
            }
            else
            {
                post = new ForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator).GetPosts();
                //if (topicId <= 0)
                //{
                //    post = new ForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator).GetPosts();
                //}
                //else
                //{
                //    post = new TopicWithPermissionCheck(conn, transaction, topicId, operatingUserOrOperator).GetPosts();
                //}
            }
            return post;
        }
    }
}
