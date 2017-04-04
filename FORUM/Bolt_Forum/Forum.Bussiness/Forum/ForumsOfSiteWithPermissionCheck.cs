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
    public class ForumsOfSiteWithPermissionCheck : ForumsOfSite
    {
        UserOrOperator _operatingUserOrOperator;

        public ForumsOfSiteWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public ForumWithPermissionCheck[] GetAllNotClosedForums()
        {
            return base.GetAllNotClosedForums(_operatingUserOrOperator);
        }
        public ForumWithPermissionCheck[] GetAllForums(out string[] forumPaths, out int[] forumIds)
        {

            return base.GetAllForums(out forumPaths,out forumIds, _operatingUserOrOperator);
        }
        public ForumWithPermissionCheck[] GetAllForums()
        {
            string[] forumPaths;
            int[] forumIds;
            return base.GetAllForums(out forumPaths, out forumIds, _operatingUserOrOperator);
        }
    }
}
