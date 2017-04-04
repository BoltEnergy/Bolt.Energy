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
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public class TopicsOfSiteWithPermissionCheck : TopicsOfSite
    {
        UserOrOperator _operatingUserOrOperator;

        public TopicsOfSiteWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public override TopicWithPermissionCheck[] GetNotDeletedTopicsByQueryAndPaging(
            UserOrOperator operatingUserOrOperator, string keywords, string name, 
            DateTime startDate, DateTime endDate, int pageIndex, int pageSize, 
            string orderField, string orderMethod,out int countOfTopics)
        {
            return base.GetNotDeletedTopicsByQueryAndPaging(_operatingUserOrOperator, keywords,
                name, startDate, endDate, pageIndex, pageSize,
                orderField, orderMethod, out countOfTopics);
        }
    }
}
