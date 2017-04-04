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
    public abstract class UserReputationGroup : UserReputationGroupBase
    {
        public UserReputationGroup(SqlConnectionWithSiteId conn, SqlTransaction transaction, int groupId)
            : base(conn, transaction)
        {
            this._groupId = groupId;

            DataTable table = GroupAccess.GetGroupById(groupId, conn, transaction);

            if (table.Rows.Count > 0)
            {
                DataRow r = table.Rows[0];

                if (r["type"].Equals("0"))
                    ExceptionHelper.ThrowForumGroupIsNotReputationGroupWithId(groupId);

                this._description = r["description"].ToString();
                this._name = r["name"].ToString();
                this._limitedBegin = Convert.ToInt32(r["limitedbegin"]);
                this._limitedExpire = Convert.ToInt32(r["limitedexpire"]);
                this._icoRepeat = Convert.ToInt32(r["icoRepeat"]);

            }
            else
                ExceptionHelper.ThrowForumReputationGroupNotExistWithId(groupId);
        }

        public UserReputationGroup(SqlConnectionWithSiteId conn, SqlTransaction transaction, int groupId, string name, string description, int limitedBegin, int limitedExpire, int icoRepeat)
            : base(conn, transaction)
        {
            this._groupId = groupId;
            this._icoRepeat = icoRepeat;
            this._limitedBegin = limitedBegin;
            this._limitedExpire = limitedExpire;
            this._name = name;
            this._description = description;
        }

        #region Public Static Function CheckFieldslLength
        public static void CheckFieldsLength(string name, string description, int limitedBegin, int limitedExpire)
        { 
            //required
            if (name.Length <= 0)
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("name");

            //length
            if (name.Length > ForumDBFieldLength.Group_nameFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("name",
                    ForumDBFieldLength.Group_nameFieldLength);

            if (description.Length > ForumDBFieldLength.Group_descriptionFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("description",
                    ForumDBFieldLength.Group_descriptionFieldLength);

            if (limitedBegin >= limitedExpire)
                ExceptionHelper.ThrowForumReputationGroupInvalidReputationRangeException();

        }
        #endregion Public Static Function CheckFieldslLength

        //#region Public Static Function CheckReputationRangeLimitedBegin
        //public static void CheckReputationRangeLimitedBegin(SqlConnectionWithSiteId conn, SqlTransaction transaction, int limitedBegin)
        //{
        //    if (GroupAccess.GetCountOfReputationGroupsByLimitedBegin(conn, transaction, limitedBegin) > 0)
        //        ExceptionHelper.ThrowForumReputationGroupRepititiveRangeException();
        //}
        //#endregion Public Static Function CheckReputationRangeLimitedBegin

        //#region Public Static Function CheckReputationRangeLimitedExpire
        //public static void CheckReputationRangeLimitedExpire(SqlConnectionWithSiteId conn, SqlTransaction transaction, int limitedExpire)
        //{
        //    if (GroupAccess.GetCountOfReputationGroupsByLimitedExpire(conn, transaction, limitedExpire) > 0)
        //        ExceptionHelper.ThrowForumReputationGroupRepititiveRangeException();
        //}
        //#endregion Public Static Function CheckReputationRangeLimitedExpire

        #region Public Static Function CheckReputationRangeBeginAndExpire
        public static void CheckReputationRangeBeginAndExpire(SqlConnectionWithSiteId conn, SqlTransaction transaction, int limitedBegin, int limitedExpire)
        {
            if (GroupAccess.GetCountOfReputationGroupsByLimitedBeginAndLimitedExpire(conn, transaction, limitedBegin, limitedExpire) > 0)
                ExceptionHelper.ThrowForumReputationGroupRepititiveRangeException();
        }

        public static void CheckReputationRangeBeginAndExpireOfEdit(SqlConnectionWithSiteId conn, SqlTransaction transaction, int limitedBegin, int limitedExpire,int groupId)
        {
            if (GroupAccess.GetCountOfReputationGroupsByLimitedBeginAndLimitedExpireOfEdit(conn, transaction, limitedBegin, limitedExpire,groupId) > 0)
                ExceptionHelper.ThrowForumReputationGroupRepititiveRangeException();
        }
        #endregion Public Static Function CheckReputationRangeBeginAndExpire

        #region Private Function CheckIfEnableReputation
        private void CheckIfEnableReputation()
        {
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, null);
            if (!forumFeature.IfEnableReputation)
                ExceptionHelper.ThrowForumSettingsCloseReputationFunctio();
            //if (!forumFeature.IfEnableReputationPermission)
            //    ExceptionHelper.ThrowForumSettingsCloseGroupPermissionFunction();
        }
        #endregion Private Function CheckIfEnableReputation

        public static int Add(string name, string description, int limitedBegin, int limitedExpire, int icoRepeat,SqlConnectionWithSiteId conn,SqlTransaction transaction)
        {
            CheckFieldsLength(name, description, limitedBegin, limitedExpire);
            //CheckReputationRangeLimitedBegin(conn, transaction, limitedBegin);
            //sCheckReputationRangeLimitedExpire(conn, transaction, limitedExpire);
            CheckReputationRangeBeginAndExpire(conn, transaction, limitedBegin, limitedExpire);
            int identity = GroupAccess.AddReputationGroup(name, description, false, limitedBegin, limitedExpire, icoRepeat, conn, transaction);
            
            UserPermissionSetting ups = new UserPermissionSettingWithPermissionCheck(conn, transaction, null, conn.SiteId);  

            GroupPermissionAccess.AddPermission(identity, ups.IfAllowViewForum, ups.IfAllowViewTopic, ups.IfAllowPost,
                ups.IfAllowCustomizeAvatar, ups.MaxLengthofSignature,
                ups.IfSignatureAllowUrl,ups.IfSignatureAllowInsertImage,
                ups.MinIntervalForPost, ups.MaxLengthOfPost, 
                ups.IfAllowUrl, ups.IfAllowUploadImage, 
                ups.IfAllowUploadAttachment, ups.MaxCountOfAttacmentsForOnePost,ups.MaxSizeOfOneAttachment, ups.MaxSizeOfAllAttachments, 
                ups.MaxCountOfMessageSendOneDay, ups.IfAllowSearch,
                ups.MinIntervalForSearch, ups.IfPostNotNeedModeration, conn, transaction);

            return identity;
        }

        public virtual void Update(string name, string description, int limitedBegin, int limitedExpire, int icoRepeat)
        {
            CheckIfEnableReputation();
            CheckFieldsLength(name, description, LimitedBegin, limitedExpire);
            //if (_limitedBegin != limitedBegin) CheckReputationRangeLimitedBegin(_conn, _transaction, limitedBegin);
            //if (_limitedExpire != limitedExpire) CheckReputationRangeLimitedExpire(_conn, _transaction, limitedExpire);
            CheckReputationRangeBeginAndExpireOfEdit(_conn, _transaction, limitedBegin, limitedExpire,GroupId);
            GroupAccess.UpdateReputationGroup(GroupId, name, description, limitedBegin, limitedExpire, icoRepeat, _conn, _transaction);
        }

        protected void Delete(UserOrOperator operatingUserOrOperator)
        {
            CheckIfEnableReputation();
            GroupAccess.DeleteGroup(_groupId, _conn, _transaction);
            GroupOfForumAccess.DeleteGroupForumRalation(_groupId, _conn, _transaction);
            GetPermission(operatingUserOrOperator).Delete();
        }

        protected UserReputationGroupPermissionWithPermissionCheck GetPermission(UserOrOperator operatingUserOrOperator)
        {
            return new UserReputationGroupPermissionWithPermissionCheck(_conn,_transaction,operatingUserOrOperator,_groupId);
        }

        protected UserReputationGroupOfForumWithPermissionCheck MakeThisInForum(int forumId, UserOrOperator operatingUserOrOperator)
        {
            return null;
        }
    }
}
