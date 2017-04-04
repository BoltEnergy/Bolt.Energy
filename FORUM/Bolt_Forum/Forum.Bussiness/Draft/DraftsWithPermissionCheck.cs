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
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.Bussiness
{
    public class DraftsWithPermissionCheck : Drafts
    {
        UserOrOperator _operatinUserOrOperator;

        public DraftsWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction)
        {
            this._operatinUserOrOperator = operatingUserOrOperator;
        }

        public TopicWithPermissionCheck[] GetTopicsWhichExistDraftByPaging(int pageindex, int pageSize, string subjectKeyWord, string strOrder)
        {
            return base.GetTopicsWhichExistDraftByPaging(pageindex, pageSize, subjectKeyWord, strOrder, _operatinUserOrOperator);
        }

        public override int GetCountOfDrafts(string keyWords)
        {
            return base.GetCountOfDrafts(keyWords);
        }
    }
}
