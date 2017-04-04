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
    public class SubscribesWithPermissionCheck : Subscribes
    {
        UserOrOperator _operatingUserOrOperator;

        public SubscribesWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int userOrOperatorId)
            :base(conn, transaction, userOrOperatorId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public void Add(int topicId)
        {
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            base.Add(topicId,_operatingUserOrOperator);
        }
        public void Delete(int topicId)
        {
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            base.Delete(_operatingUserOrOperator, topicId);
        }
        public SubscribeWithPermissionCheck[] GetTopicsByQueryAndPaging(out int count, int pageIndex, int pageSize, string keyword)
        {
            return base.GetTopicsByQueryAndPaging(out count, pageIndex, pageSize, keyword, _operatingUserOrOperator);
        }
    }
}
