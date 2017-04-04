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
    public abstract class UserReputationGroupPermission : GroupPermissionBase
    {
        public UserReputationGroupPermission(SqlConnectionWithSiteId conn, SqlTransaction transaction, int groupId)
            : base(conn, transaction)
        {
            base._groupId = groupId;
            DataTable table = GroupPermissionAccess.GetUserGroupPermissionByGroupId(_groupId, _conn, transaction);

            if (table.Rows.Count <= 0)
            {
                ExceptionHelper.ThrowForumPermissionNotExistWithGroupIdException(groupId);
            }
            else
            {
                DataRow r = table.Rows[0];
                this._ifAllowViewForum = Convert.ToBoolean(r["ifAllowViewForum"]);
                this._ifAllowViewTopic = Convert.ToBoolean(r["ifAllowViewTopic"]);
                this._ifAllowPost = Convert.ToBoolean(r["ifAllowPost"]);
                this._ifAllowCustomizeAvatar = Convert.ToBoolean(r["ifallowcustomizeavatar"]);
                this._maxLengthofSignature = Convert.ToInt32(r["maxlengthofsignature"]);
                this._minIntervalForPost = Convert.ToInt32(r["minintervalforpost"]);
                this._maxLengthOfPost = Convert.ToInt32(r["maxlengthofpost"]);
                //this._ifAllowHTML = Convert.ToBoolean(r["ifallowhtml"]);
                this._ifAllowUrl = Convert.ToBoolean(r["ifallowurl"]);
                this._ifAllowUploadImage = Convert.ToBoolean(r["ifallowuploadimage"]);
                this._ifAllowUploadAttachment = Convert.ToBoolean(r["ifallowuploadattachment"]);
                this._maxCountOfAttacmentsForOnePost = Convert.ToInt32(r["maxCountOfAttacmentsForOnePost"]);
                this._maxSizeOfOneAttachment = Convert.ToInt32(r["MaxSizeOfOneAttachment"]);
                this._maxSizeOfAllAttachments = Convert.ToInt32(r["maxSizeOfAllAttachments"]);
                this._maxCountOfMessageSendOneDay = Convert.ToInt32(r["maxCountOfMessageSendOneDay"]);
                this._ifAllowSearch = Convert.ToBoolean(r["ifallowSearch"]);
                this._minIntervalForSearch = Convert.ToInt32(r["minIntervalForSearch"]);
                this._ifPostNotNeedModeration = Convert.ToBoolean(r["ifPostNotNeedModeration"]);
                //this._ifSignatureAllowHTML = Convert.ToBoolean(r["ifSignatureAllowHtml"]);
                this._ifSignatureAllowInsertImage = Convert.ToBoolean(r["ifSignatureAllowUploadImage"]);
                this._ifSignatureAllowUrl=Convert.ToBoolean(r["ifSignatureAllowUrl"]);
            }
        }
        
        public UserReputationGroupPermission(SqlConnectionWithSiteId conn, SqlTransaction transaction,int groupId,
            bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost,
            bool ifAllowCustomizeAvatar, int maxLengthofSignature,
            bool ifSignatureAllowHTML,bool ifSignatureAllowInsertImage,bool ifSignatureAllowUrl,
            int minIntervalForPost,
            int maxLengthOfPost, bool ifAllowHTML, bool ifAllowUrl, bool ifAllowUploadImage, bool ifAllowUploadAttachment,
            int maxCountOfAttacmentsForOnePost, int maxSizeOfOneAttachments, int maxSizeOfAllAttachments, int maxCountOfMessageSendOneDay,
            bool ifAllowSearch, int minIntervalForSearch, bool ifPostNotNeedModeration)
            : base(conn, transaction)
        {
            this._groupId = groupId;
            this._ifAllowCustomizeAvatar = ifAllowCustomizeAvatar;
            //this._ifAllowHTML = ifAllowHTML;
            this._ifAllowPost = ifAllowPost;
            this._ifAllowSearch = ifAllowSearch;
            this._ifAllowUploadAttachment = ifAllowUploadAttachment;
            this._ifAllowUploadImage = ifAllowUploadImage;
            this._ifAllowUrl = ifAllowUrl;
            this._ifAllowViewForum = ifAllowViewForum;
            this._ifAllowViewTopic = ifAllowViewTopic;
            this._ifPostNotNeedModeration = ifPostNotNeedModeration;
            this._maxCountOfAttacmentsForOnePost = maxCountOfAttacmentsForOnePost;
            this._maxCountOfMessageSendOneDay = maxCountOfMessageSendOneDay;
            this._maxLengthOfPost = maxLengthOfPost;
            this._maxLengthofSignature = maxLengthofSignature;
            this._maxSizeOfAllAttachments = maxSizeOfAllAttachments;
            this._maxSizeOfOneAttachment = maxSizeOfOneAttachments;
            this._minIntervalForPost = minIntervalForPost;
            this._minIntervalForSearch = minIntervalForSearch;

            //this._ifSignatureAllowHTML = ifSignatureAllowHTML;
            this._ifSignatureAllowInsertImage = ifSignatureAllowInsertImage;
            this._ifSignatureAllowUrl = ifSignatureAllowUrl;
        }

        private void CheckIfEnableReputationPermission(UserOrOperator operatingUserOrOperator)
        {
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, operatingUserOrOperator);
            if (!forumFeature.IfEnableReputation)
                ExceptionHelper.ThrowForumSettingsCloseReputationFunctio();
            if (!forumFeature.IfEnableReputationPermission)
                ExceptionHelper.ThrowForumSettingsCloseReputationPermissionFunction();
        }

        protected void Update(UserOrOperator operatingUserOrOperator,
            bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost,
            bool ifAllowCustomizeAvatar, int maxLengthofSignature,
            bool ifSignatureAllowUrl,bool ifSignatureAllowInsertImage,
            int minIntervalForPost,
            int maxLengthOfPost, bool ifAllowUrl, bool ifAllowUploadImage, bool ifAllowUploadAttachment,
            int maxCountOfAttacmentsForOnePost, int maxSizeOfOneAttachments, int maxSizeOfAllAttachments, int maxCountOfMessageSendOneDay,
            bool ifAllowSearch, int minIntervalForSearch, bool ifPostNotNeedModeration
            )
        {
            CheckIfEnableReputationPermission(operatingUserOrOperator);
            GroupPermissionAccess.UpdatePermission(_groupId, ifAllowViewForum, ifAllowViewTopic, ifAllowPost, ifAllowCustomizeAvatar,
                maxLengthofSignature,ifSignatureAllowUrl,ifSignatureAllowInsertImage, 
                minIntervalForPost, maxLengthOfPost, ifAllowUrl, ifAllowUploadImage, ifAllowUploadAttachment
                , maxCountOfAttacmentsForOnePost, maxSizeOfOneAttachments, maxSizeOfAllAttachments, maxCountOfMessageSendOneDay, ifAllowSearch
                , minIntervalForSearch, ifPostNotNeedModeration, _conn, _transaction);
        }

        protected void Delete(UserOrOperator operatingUserOrOperator)
        {
            GroupPermissionAccess.DeletePermission(_groupId, _conn, _transaction);
        }
    }
}
