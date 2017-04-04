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
    public abstract class UserGroupOfForum : UserGroupBase
    {
        private int _forumId;

        public int ForumId
        {
            get { return this._forumId; }
        }

        public UserGroupOfForum(SqlConnectionWithSiteId conn, SqlTransaction transaction, int groupId, int forumId)
            : base(conn, transaction)
        {
            _forumId = forumId;
            DataTable table = GroupAccess.GetGroupById(groupId, conn, transaction);
            if (table.Rows.Count > 0)
            {
                this._groupId = groupId;
                DataRow dr = table.Rows[0];
                this._name = Convert.ToString(dr["Name"]);
                this._description = Convert.ToString(dr["Description"]);
            }
            else
            {
                ExceptionHelper.ThrowForumUserGroupOfForumNotExistWithGroupIdAndForumId(groupId, forumId);
            }
        }

        public UserGroupOfForum(SqlConnectionWithSiteId conn, SqlTransaction transaction, int groupId, int forumId, string name, string description)
            :base(conn, transaction)
        {
            this._groupId = groupId;
            this._forumId = forumId;
            this._name = name;
            this._description = description;
        }

        public virtual void Delete(UserOrOperator operatingUserOrOperator)
        {
            GroupOfForumAccess.DeleteGroupFromForumRalation(_groupId, _forumId, _conn, _transaction);
            //UserGroupPermissionForForumWithPermissionCheck u = new UserGroupPermissionForForumWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, _groupId, _forumId);
            //u.Delete();
        }

        public UserGroupPermissionForForumWithPermissionCheck GetPermission(UserOrOperator operaingUserOrOperator)
        {
            return new UserGroupPermissionForForumWithPermissionCheck(_conn, _transaction, operaingUserOrOperator, _groupId, _forumId);
        }
    }
}
