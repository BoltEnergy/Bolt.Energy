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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Database;

namespace Com.Comm100.Forum.Bussiness
{
    public class ForumsOfSite : ForumBase
    {
        public ForumsOfSite(SqlConnectionWithSiteId conn, SqlTransaction transaction)
            : base(conn, transaction)
        { }

        protected ForumWithPermissionCheck[] GetAllNotClosedForums(UserOrOperator operatingUserOrOperator)
        {
            return null;
        }

        protected ForumWithPermissionCheck[] GetAllForums(
            out string[] forumPaths, out int[] forumIds, 
            UserOrOperator operatingUserOrOperator)
        {
            List<ForumWithPermissionCheck> Allforums = new List<ForumWithPermissionCheck>();
            List<string> lforumPaths = new List<string>();
            List<int> lforumIds = new List<int>();
            Dictionary<int, string> aforums = new Dictionary<int, string>();
            CategoriesWithPermissionCheck categories = new CategoriesWithPermissionCheck(_conn, _transaction, operatingUserOrOperator);
            CategoryWithPermissionCheck[] Allcategories = categories.GetAllCategories();
            foreach (CategoryWithPermissionCheck category in Allcategories)
            {
                ForumsOfCategoryWithPermissionCheck forums = new ForumsOfCategoryWithPermissionCheck(_conn, _transaction, category.CategoryId, operatingUserOrOperator);
                ForumWithPermissionCheck[] tempForums = forums.GetAllForums();
                foreach (ForumWithPermissionCheck forum in tempForums)
                {
                    //lforumPaths.Add(category.Name + "/" + forum.Name);
                    //lforumIds.Add(forum.ForumId);
                    aforums.Add(forum.ForumId, category.Name + "/" + forum.Name);

                }
                Allforums.AddRange(tempForums);
            }
            var o = from forum in aforums
                    orderby forum.Value
                    select forum;
            forumIds = (from forum in o
                       select forum.Key).ToArray<int>();
            forumPaths = (from forum in o
                          select forum.Value).ToArray<string>();
            
            return Allforums.ToArray<ForumWithPermissionCheck>();
        }
    }
}
