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
    public class PostsOfSiteWithPermissionCheck : PostsOfSite
    {
        private UserOrOperator _operatingUserOrOperator;

        public PostsOfSiteWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public PostWithPermissionCheck[] GetNotDeletedPostsByQueryAndPaging(
            string keywords, string name, DateTime startDate,
            DateTime endDate, int pageIndex, int pageSize, string orderFiled, string orderMethod, out int CountOfPosts)
        {
            return base.GetNotDeletedPostsByQueryAndPaging(_operatingUserOrOperator, keywords, 
               name, startDate, endDate, pageIndex, pageSize, orderFiled, orderMethod, out CountOfPosts);
        }

        public PostWithPermissionCheck[] GetDeletedPostsByQueryAndPaging(
            string keywords, string name, DateTime startDate, DateTime endDate,
            int pageIndex, int pageSize, string orderFiled, string orderMethod, out int CountOfPosts)
        {
            return base.GetDeletedPostsByQueryAndPaging(_operatingUserOrOperator,
                keywords, name, startDate, endDate,
                pageIndex, pageSize, orderFiled, orderMethod, out CountOfPosts);
        }

        public PostWithPermissionCheck[] GetNotDeletedRejectedPostsOfSiteByQueryAndPaging(
           int pageIndex, int pageSize,
           string queryConditions, string orderField, string orderMethod, out int CountOfPosts)
        {
            return base.GetNotDeletedRejectedPostsOfSiteByQueryAndPaging(_operatingUserOrOperator, pageIndex, pageSize,
                queryConditions, orderField, orderMethod, out CountOfPosts);
        }

        public PostWithPermissionCheck[] GetNotDeletedModerationPostsOfSiteByQueryAndPaging(
          int pageIndex, int pageSize,
           string queryConditions, string orderField, string orderMethod, out int CountOfPosts)
        {
            return base.GetNotDeletedModerationPostsOfSiteByQueryAndPaging(_operatingUserOrOperator, pageIndex, pageSize,
                queryConditions, orderField, orderMethod, out CountOfPosts);
        }

        #region Moderator Panel
        public PostWithPermissionCheck[] GetNotDeletedModerationPostsByModeratorWithQueryAndPaging(int moderatorId, string queryConditions, int pageIndex, int pageSize, string orderField, string orderDirection, EnumPostOrTopicModerationStatus status)
        {
            return base.GetNotDeletedModerationPostsByModeratorWithQueryAndPaging(_operatingUserOrOperator, moderatorId, queryConditions, pageIndex, pageSize, orderField, orderDirection, status);
        }

        public int GetCountOfNoDeletedModerationPostsByModeratorWithQuery(int moderatorId, string queryConditions, EnumPostOrTopicModerationStatus status)
        {
            return base.GetCountOfNotDeletedModerationPostsByModeratorWithQuery(_operatingUserOrOperator, moderatorId, queryConditions, status);
        }
        public PostWithPermissionCheck[] GetNotDeletedPostsByModeratorWithQueryAndPaging(int moderatorId, string queryConditions,string name,DateTime startDate,DateTime endDate,int forumId, int pageIndex, int pageSize,string orderField,string orderDirection)
        {
            return base.GetNotDeletePostsByModeratorWithQueryAndPaging(_operatingUserOrOperator, moderatorId, queryConditions, name, startDate, endDate, forumId, pageIndex, pageSize, orderField, orderDirection);
        }
        public int GetCountOfNotDeletePostsByModeratorWithQuery(int moderatorId,string queryConditions,string name,DateTime startDate,DateTime endDate,int forumId)
        {
            return base.GetCountOfNotDeletePostsByModeratorWitheQuery(moderatorId,queryConditions,name,startDate,endDate,forumId);
        }
        public PostWithPermissionCheck[] GetDeletedPostsByModeratorWithQueryAndPaging(int moderatorId,string keywords,string name,DateTime startDate,DateTime endDate,int pageIndex,int pageSize,string orderField,string orderDirection)
        {
            return base.GetDeletedPostsByModeratorWithQueryAndPaging(_operatingUserOrOperator,moderatorId,keywords,name,startDate,endDate,pageIndex,pageSize,orderField,orderDirection);
        }
        public int GetCountOfDeletedPostsByModeratorWithQuery(int moderatorId, string keywords, string name, DateTime startDate, DateTime endDate)
        {
            return base.GetCountOfDeletedPostsByModeratorWithQuery(moderatorId, keywords, name, startDate, endDate);
        }
        #endregion Moderator Panel
    }
}
