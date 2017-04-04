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
    public class ForumsOfCategoryWithPermissionCheck : ForumsOfCategory
    {
        UserOrOperator _operatingUserOrOperator;

        public ForumsOfCategoryWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int categoryId, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction, categoryId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public int Add(string name, string description, int[] moderatorIds,bool ifAllowPostNeedingReplayTopic,bool ifAllowPostNeedingPayTopic)
        {
            CommFun.CheckAdminPanelCommonPermission(this._operatingUserOrOperator);
            return base.Add(name, description, moderatorIds, _operatingUserOrOperator, ifAllowPostNeedingReplayTopic, ifAllowPostNeedingPayTopic);
        }

        public void Delete(int forumId)
        {
            CommFun.CheckAdminPanelCommonPermission(this._operatingUserOrOperator);
            base.Delete(forumId, _operatingUserOrOperator);
        }
        
        public ForumWithPermissionCheck[] GetAllNotHiddenForums()
        {
            return base.GetAllNotHiddenForums(this._operatingUserOrOperator);
        }

        public ForumWithPermissionCheck[] GetAllForums()
        {
            return base.GetAllForums(this._operatingUserOrOperator);
        }
        public CategoryWithPermissionCheck GetCategory()
        {
            return base.GetCategory(_operatingUserOrOperator);
        }


    }
}
