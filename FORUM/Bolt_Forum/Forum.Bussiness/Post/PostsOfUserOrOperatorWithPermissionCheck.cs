﻿#if OPENSOURCE
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
    public class PostsOfUserOrOperatorWithPermissionCheck : PostsOfUserOrOperator
    {
        UserOrOperator _operatingUserOrOperator;

        public PostsOfUserOrOperatorWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int userOrOperatorId)
            : base(conn, transaction, userOrOperatorId)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public PostWithPermissionCheck[] GetPostsByQueryAndPaging(int pageIndex, int pageSize, string keyword, DateTime startDate, DateTime endDate, out int count)
        {
            return base.GetPostsByQueryAndPaging(pageIndex, pageSize, keyword, startDate, endDate, out count,  _operatingUserOrOperator);
        }

    }
}
