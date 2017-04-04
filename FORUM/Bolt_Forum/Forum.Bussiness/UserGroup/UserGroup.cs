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
    public abstract class UserGroup : UserGroupBase
    {
        private bool _ifAllForumUsersGroup;
        public bool IfAllForumUsersGroup
        {
            get { return this._ifAllForumUsersGroup; }
        }

        public UserGroup(SqlConnectionWithSiteId conn, SqlTransaction transaction, int groupId)
            : base(conn, transaction)
        {
            base._groupId = groupId;

            DataTable table = GroupAccess.GetGroupById(UserGroupId, conn, _transaction);

            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowForumUserGroupNotExistWithId(groupId);
            }
            else
            {
                DataRow r = table.Rows[0];
                this._groupId = Convert.ToInt32(r["Id"]);
                this._name = r["name"].ToString();
                this._description = r["description"].ToString();
                this._ifAllForumUsersGroup = Convert.ToBoolean(r["IfAllForumUsersGroup"]);
            }
        }

        public UserGroup(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id, string name, string description, bool ifAllForumUsersGroup)
            : base(conn, transaction)
        {
            this._groupId = id;
            this._name = name;
            this._description = description;
            this._ifAllForumUsersGroup = ifAllForumUsersGroup;
        }

        private static void CheckFieldsLength(string name, string description)
        {
            if (name.Length <= 0)
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("name");

            if (name.Length > ForumDBFieldLength.Group_nameFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("name",
                    ForumDBFieldLength.Group_nameFieldLength);

            if (description.Length > ForumDBFieldLength.Group_descriptionFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("description",
                    ForumDBFieldLength.Group_descriptionFieldLength);
        }

        private void CheckIfEnableGroupPermission(UserOrOperator operatingUserOrOperator)
        {
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, operatingUserOrOperator);
            if (!forumFeature.IfEnableGroupPermission)
                ExceptionHelper.ThrowForumSettingsCloseGroupPermissionFunction();
        }

        public static int Add(string name, string description, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            CheckFieldsLength(name, description);
            int identity = GroupAccess.AddUserGroup(name, description, conn, transaction);

            UserPermissionSetting ups = new UserPermissionSettingWithPermissionCheck(conn, transaction, null, conn.SiteId);
            GroupPermissionAccess.AddPermission(identity, ups.IfAllowViewForum, ups.IfAllowViewTopic, ups.IfAllowPost,
                ups.IfAllowCustomizeAvatar, ups.MaxLengthofSignature, ups.IfSignatureAllowUrl,
                ups.IfSignatureAllowInsertImage,
                ups.MinIntervalForPost, ups.MaxLengthOfPost,
                ups.IfAllowUrl, ups.IfAllowUploadImage, 
                ups.IfAllowUploadAttachment, ups.MaxCountOfAttacmentsForOnePost,ups.MaxSizeOfOneAttachment, ups.MaxSizeOfAllAttachments, 
                ups.MaxCountOfMessageSendOneDay, ups.IfAllowSearch,
                ups.MinIntervalForSearch, ups.IfPostNotNeedModeration, conn, transaction);

            return identity;
        }

        protected void Update(string name, string description,UserOrOperator operatingUserOrOperator)
        {
            CheckFieldsLength(name, description);
            CheckIfEnableGroupPermission(operatingUserOrOperator);
            GroupAccess.UpdateUserGroup(_groupId, name, description, _conn, _transaction);
        }

        public virtual void Delete(UserOrOperator operatingUserOrOperator)
        {
            if (this._ifAllForumUsersGroup)
                ExceptionHelper.ThrowForumAllForumUserGroupCannotBeDeleted();
            CheckIfEnableGroupPermission(operatingUserOrOperator);
            GroupAccess.DeleteGroup(this._groupId, this._conn, this._transaction);
            GroupOfForumAccess.DeleteGroupForumRalation(_groupId, _conn, _transaction);
            UserGroupPermissionWithPermissionCheck permission = GetPermission(operatingUserOrOperator);
            permission.Delete();
            MembersOfUserGroupWithPermissionCheck members = GetMembers(operatingUserOrOperator);
            members.DeleteAll();
        }

        protected MembersOfUserGroupWithPermissionCheck GetMembers(UserOrOperator operatingUserOrOperator)
        {
            return new MembersOfUserGroupWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, _groupId);
        }

        protected UserGroupPermissionWithPermissionCheck GetPermission(UserOrOperator operatingUserOrOperator)
        {
            return new UserGroupPermissionWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, _groupId);
        }

        protected UserGroupOfForumWithPermissionCheck MakeThisInForum(int forumId, UserOrOperator operatingUserOrOperator)
        {
            return null;
        }
    }
}
