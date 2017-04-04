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
    public class UserReputationGroupOfForumWithPermissionCheck : UserReputationGroupOfForum
    {
        UserOrOperator _operatingUserOrOperator;

        public UserReputationGroupOfForumWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int groupId, int forumId)
            : base(conn, transaction, groupId, forumId)
        {
            _operatingUserOrOperator = operatingUserOrOperator; 
        }

        public UserReputationGroupOfForumWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int groupId, int forumId, string name, string description, int limitedBegin, int limitedExpire, int icoRepeat)
            : base(conn, transaction, groupId, forumId, name, description, limitedBegin, limitedExpire, icoRepeat)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }
        public void Delete()
        {
            CommFun.CheckAdminPanelOrModeratorPanelCommomPermission(_operatingUserOrOperator, ForumId);
            base.Delete(_operatingUserOrOperator);
        }

        public UserReputationGroupPermissionForForumWithPermissionCheck GetPermission()
        {
            return base.GetPermission(_operatingUserOrOperator);
        }
    }
}
