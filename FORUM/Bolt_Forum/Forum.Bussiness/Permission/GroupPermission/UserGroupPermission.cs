#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class UserGroupPermission : GroupPermissionBase
    {        
        public UserGroupPermission(SqlConnectionWithSiteId conn, SqlTransaction transaction, int groupId)
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
                this._ifAllowViewForum= Convert.ToBoolean(r["ifAllowViewForum"]);
                this._ifAllowViewTopic=Convert.ToBoolean(r["ifAllowViewTopic"]);
                this._ifAllowPost=Convert.ToBoolean(r["ifAllowPost"]);
                this._ifAllowCustomizeAvatar=Convert.ToBoolean(r["ifallowcustomizeavatar"]);
                this._maxLengthofSignature=Convert.ToInt32(r["maxlengthofsignature"]);

                /*-----Signature Content Permission.Added in 5.13 by Allon-----*/
                //this._ifSignatureAllowHTML = Convert.ToBoolean(r["IfSignatureAllowHTML"]);
                this._ifSignatureAllowUrl = Convert.ToBoolean(r["IfSignatureAllowUrl"]);
                this._ifSignatureAllowInsertImage = Convert.ToBoolean(r["IfSignatureAllowUploadImage"]);
                /*-----------*/
                this._minIntervalForPost=Convert.ToInt32(r["minintervalforpost"]);
                this._maxLengthOfPost=Convert.ToInt32(r["maxlengthofpost"]);
                //this._ifAllowHTML=Convert.ToBoolean(r["ifallowhtml"]);
                this._ifAllowUrl=Convert.ToBoolean(r["ifallowurl"]);
                this._ifAllowUploadImage=Convert.ToBoolean(r["ifallowuploadimage"]);
                this._ifAllowUploadAttachment=Convert.ToBoolean(r["ifallowuploadattachment"]);
                this._maxCountOfAttacmentsForOnePost=Convert.ToInt32(r["maxCountOfAttacmentsForOnePost"]);
                this._maxSizeOfOneAttachment=Convert.ToInt32(r["MaxSizeOfOneAttachment"]);
                this._maxSizeOfAllAttachments=Convert.ToInt32(r["maxSizeOfAllAttachments"]);
                this._maxCountOfMessageSendOneDay=Convert.ToInt32(r["maxCountOfMessageSendOneDay"]);
                this._ifAllowSearch=Convert.ToBoolean(r["ifallowSearch"]);
                this._minIntervalForSearch=Convert.ToInt32(r["minIntervalForSearch"]);
                this._ifPostNotNeedModeration=Convert.ToBoolean(r["ifPostNotNeedModeration"]);          
            }
        }

        public UserGroupPermission(int groupId,
            bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost,
            bool ifAllowCustomizeAvatar, int maxLengthofSignature,bool ifSignatureAllowHTML,bool ifSignatureAllowUrl,bool ifSignatureAllowInserImage,
            int minIntervalForPost,int maxLengthOfPost, 
            bool ifAllowHTML, bool ifAllowUrl, bool ifAllowUploadImage, 
            bool ifAllowUploadAttachment,int maxCountOfAttacmentsForOnePost, int maxSizeOfOneAttachments, int maxSizeOfAllAttachments, 
            int maxCountOfMessageSendOneDay,bool ifAllowSearch, int minIntervalForSearch, bool ifPostNotNeedModeration, 
            SqlConnectionWithSiteId conn, SqlTransaction transaction)
            : base(conn, transaction)
        {
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

            /*--------Signature content permission added 5.13 by Allon.--------*/
            //this._ifSignatureAllowHTML = ifSignatureAllowHTML;
            this._ifSignatureAllowUrl = ifSignatureAllowUrl;
            this._ifSignatureAllowInsertImage = ifSignatureAllowInserImage;
        }

        private void CheckIfEnableGroupPermission(UserOrOperator operatingUserOrOperator)
        {
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction, operatingUserOrOperator);
            if (!forumFeature.IfEnableGroupPermission)
                ExceptionHelper.ThrowForumSettingsCloseGroupPermissionFunction();
        }

        protected void Update(UserOrOperator operatingUserOrOperator,
            bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost,
            bool ifAllowCustomizeAvatar, int maxLengthofSignature,bool ifSignatureAllowUrl,bool ifSignatureAllowInsertImage,
            int minIntervalForPost,int maxLengthOfPost, bool ifAllowUrl, bool ifAllowUploadImage, 
            bool ifAllowUploadAttachment,int maxCountOfAttacmentsForOnePost, int maxSizeOfOneAttachments, int maxSizeOfAllAttachments, 
            int maxCountOfMessageSendOneDay,bool ifAllowSearch, int minIntervalForSearch, bool ifPostNotNeedModeration)
        {
            CheckIfEnableGroupPermission(operatingUserOrOperator);
            GroupPermissionAccess.UpdatePermission(_groupId, ifAllowViewForum, ifAllowViewTopic, ifAllowPost, ifAllowCustomizeAvatar,
                    maxLengthofSignature,ifSignatureAllowUrl,ifSignatureAllowInsertImage,
                    minIntervalForPost, maxLengthOfPost, ifAllowUrl, ifAllowUploadImage, ifAllowUploadAttachment
                    , maxCountOfAttacmentsForOnePost, maxSizeOfOneAttachments, maxSizeOfAllAttachments, maxCountOfMessageSendOneDay, ifAllowSearch
                    , minIntervalForSearch, ifPostNotNeedModeration, _conn, _transaction);
        }

        protected void Delete(UserOrOperator  operatingUserOrOperator)
        {
            CheckIfEnableGroupPermission(operatingUserOrOperator);
            GroupPermissionAccess.DeletePermission(_groupId, _conn, _transaction);
        }
    }
}
