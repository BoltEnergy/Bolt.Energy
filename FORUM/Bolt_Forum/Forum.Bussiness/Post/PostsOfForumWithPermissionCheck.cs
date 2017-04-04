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
    public class PostsOfForumWithPermissionCheck : PostsOfForum
    {
        UserOrOperator _operatingUserOrOperator;

        public PostsOfForumWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int forumId)
            : base(conn, transaction, forumId)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public PostWithPermissionCheck[] GetNotDeletedPostsByQueryAndPaging(
           string keywords,  string name, DateTime startDate,
            DateTime endDate, int pageIndex, int pageSize, string orderFiled, string orderMethod, out int CountOfPosts)
        {
            return base.GetNotDeletedPostsByQueryAndPaging(_operatingUserOrOperator, keywords, 
                name, startDate, endDate, pageIndex, pageSize, orderFiled, orderMethod, out CountOfPosts);
        }
        
    }
}
