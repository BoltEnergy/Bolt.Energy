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
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public class UserReputationGroupWithPermissionCheck : UserReputationGroup
    {
        UserOrOperator _operatingUserOrOperator;

        public UserReputationGroupWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int id)
            : base(conn, transaction, id)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public UserReputationGroupWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int id, string name, string description, int limitedBegin, int limitedExpire, int icoRepeat)
            : base(conn, transaction, id, name, description, limitedBegin, limitedExpire, icoRepeat)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public override void Update(string name, string description, int limitedBegin, int limitedExpire, int icoRepeat)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            base.Update(name, description, limitedBegin, limitedExpire, icoRepeat);
        }

        public void Delete()
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            base.Delete(_operatingUserOrOperator);
        }

        public UserReputationGroupPermissionWithPermissionCheck GetPermission()
        {
            return base.GetPermission(_operatingUserOrOperator);
        }

        public UserReputationGroupOfForumWithPermissionCheck MakeThisInForum(int forumId)
        {
            return base.MakeThisInForum(forumId, _operatingUserOrOperator);
        }
    }
}
