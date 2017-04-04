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
    public class UserGroupsWithPermissionCheck : UserGroups
    {
        UserOrOperator _operatingUserOrOperator;

        public UserGroupsWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public override int GetCountOfUserGroupsByName(string name)
        {
            return base.GetCountOfUserGroupsByName(name);
        }
        public int Add(string name, string description)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            return base.Add(name, description, _operatingUserOrOperator);
        }

        public void Delete(int groupId)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            base.Delete(groupId, _operatingUserOrOperator);
        }

        public UserGroupWithPermissionCheck GetGroupById(int groupId)
        {
            return base.GetGroupById(groupId, _operatingUserOrOperator);
        }
    }
}
