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
    public abstract class UserPermissionSetting : IViewForumPermission, IViewTopicPermission, IAttachmentPermission, IHTMLPermission, IAvatarPermission, IImagePermission, IMessagePermission, IPostPermission, ISearchPermission, ISignaturePermission
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        private int _siteId;
        private bool _ifAllowViewForum;
        private bool _ifAllowViewTopic;
        private bool _ifAllowPost;
        private int _minIntervalForPost; //unit: second
        private int _maxLengthOfPost;
        private bool _ifPostNotNeedModeration;
        private bool _ifAllowCustomizeAvatar;
        private int _maxLengthofSignature;
        /*------Signature content permission added 5.13 Allon-------*/
        //private bool _ifSignatureAllowHTML;
        private bool _ifSignatureAllowUrl;
        private bool _ifSignatureAllowInsertImage;
        /*-------------*/
        //private bool _ifAllowHTML;
        private bool _ifAllowUrl;
        private bool _ifAllowUploadImage;
        private bool _ifAllowUploadAttachment;
        private int _maxCountOfAttacmentsForOnePost;
        private int _maxSizeOfOneAttachment;//unit:byte
        private int _maxSizeOfAllAttachments;//unit:byte
        private int _maxCountOfMessageSendOneDay;
        private bool _ifAllowSearch;
        private int _minIntervalForSearch;
        #endregion

        #region properties
        public int SiteId
        {
            get { return this._siteId; }
        }
        public bool IfAllowViewForum
        {
            get { return this._ifAllowViewForum; }
        }
        public bool IfAllowViewTopic
        {
            get { return this._ifAllowViewTopic; }
        }
        public bool IfAllowPost
        {
            get { return this._ifAllowPost; }
        }
        public bool IfAllowCustomizeAvatar
        {
            get { return this._ifAllowCustomizeAvatar; }
        }
        public int MaxLengthofSignature
        {
            get { return this._maxLengthofSignature; }
        }
        /*------Signature content permission added 5.13 Allon-------*/
        //public bool IfSignatureAllowHTML
        //{
        //    get { return this._ifSignatureAllowHTML; }
        //}
        public bool IfSignatureAllowUrl
        {
            get { return this._ifSignatureAllowUrl; }
        }
        public bool IfSignatureAllowInsertImage
        {
            get { return this._ifSignatureAllowInsertImage; }
        }
        /*-------------*/
        public int MinIntervalForPost
        {
            get { return this._minIntervalForPost; }
        }
        public int MaxLengthOfPost
        {
            get { return this._maxLengthOfPost; }
        }
        //public bool IfAllowHTML
        //{
        //    get { return this._ifAllowHTML; }
        //}
        public bool IfAllowUrl
        {
            get { return this._ifAllowUrl; }
        }
        public bool IfAllowUploadImage
        {
            get { return this._ifAllowUploadImage; }
        }
        public bool IfAllowUploadAttachment
        {
            get { return this._ifAllowUploadAttachment; }
        }
        public int MaxCountOfAttacmentsForOnePost
        {
            get { return this._maxCountOfAttacmentsForOnePost; }
        }
        public int MaxSizeOfOneAttachment
        {
            get { return this._maxSizeOfOneAttachment; }
        }
        public int MaxSizeOfAllAttachments
        {
            get { return this._maxSizeOfAllAttachments; }
        }
        public int MaxCountOfMessageSendOneDay
        {
            get { return this._maxCountOfMessageSendOneDay; }
        }
        public bool IfAllowSearch
        {
            get { return this._ifAllowSearch; }
        }
        public int MinIntervalForSearch
        {
            get { return this._minIntervalForSearch; }
        }
        public bool IfPostNotNeedModeration
        {
            get { return this._ifPostNotNeedModeration; }
        }
        #endregion

        public UserPermissionSetting(SqlConnectionWithSiteId conn, SqlTransaction transaction, int siteId)
        {
            _conn = conn;
            _transaction = transaction;
            
            DataTable table = GroupPermissionAccess.GetUserPermission(conn, transaction);
            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowForumUserPermissionSettingNotExist();
            }
            else
            {
                _siteId = siteId;
                _ifAllowViewForum=Convert.ToBoolean(table.Rows[0]["IfAllowViewForum"]);
                _ifAllowViewTopic=Convert.ToBoolean(table.Rows[0]["IfAllowViewTopic"]);
                _ifAllowPost=Convert.ToBoolean(table.Rows[0]["IfAllowPost"]);
                _ifAllowCustomizeAvatar=Convert.ToBoolean(table.Rows[0]["IfAllowCustomizeAvatar"]);
                _maxLengthofSignature=Convert.ToInt32(table.Rows[0]["MaxLengthofSignature"]);
                _minIntervalForPost=Convert.ToInt32(table.Rows[0]["MinIntervalForPost"]);
                _maxLengthOfPost=Convert.ToInt32(table.Rows[0]["MaxLengthOfPost"]);
                //_ifAllowHTML=Convert.ToBoolean(table.Rows[0]["IfAllowHTML"]);
                _ifAllowUrl=Convert.ToBoolean(table.Rows[0]["IfAllowUrl"]);
                _ifAllowUploadAttachment=Convert.ToBoolean(table.Rows[0]["IfAllowUploadAttachment"]);
                _ifAllowUploadImage=Convert.ToBoolean(table.Rows[0]["IfAllowUploadImage"]);
                _maxCountOfAttacmentsForOnePost=Convert.ToInt32(table.Rows[0]["MaxCountOfAttacmentsForOnePost"]);
                _maxSizeOfOneAttachment=Convert.ToInt32(table.Rows[0]["MaxSizeOfOneAttachment"]);
                _maxSizeOfAllAttachments=Convert.ToInt32(table.Rows[0]["MaxSizeOfAllAttachments"]);
                _maxCountOfMessageSendOneDay=Convert.ToInt32(table.Rows[0]["MaxCountOfMessageSendOneDay"]);
                _ifAllowSearch=Convert.ToBoolean(table.Rows[0]["IfAllowSearch"]);
                _minIntervalForSearch=Convert.ToInt32(table.Rows[0]["MinIntervalForSearch"]);
                _ifPostNotNeedModeration=Convert.ToBoolean(table.Rows[0]["IfPostNotNeedModeration"]);
                //_ifSignatureAllowHTML = Convert.ToBoolean(table.Rows[0]["IfSignatureAllowHTML"]);
                _ifSignatureAllowInsertImage = Convert.ToBoolean(table.Rows[0]["IfSignatureAllowUploadImage"]);
                _ifSignatureAllowUrl = Convert.ToBoolean(table.Rows[0]["IfSignatureAllowUrl"]);
            }

        }

        public virtual void Update(bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost, int minIntervalForPost, int maxLengthOfPost,
            bool ifPostNotNeedModeration, bool ifAllowCustomizeAvatar, int maxLengthofSignature, bool ifSignatureAllowUrl, bool ifSignatureAllowUploadImage, 
            bool ifAllowUrl,bool ifAllowUploadImage, bool ifAllowUploadAttachment, int maxCountOfAttacmentsForOnePost, int maxSizeOfOneAttachment,
            int maxSizeOfAllAttachments, int maxCountOfMessageSendOneDay, bool ifAllowSearch, int minIntervalForSearch)
        {
            GroupPermissionAccess.UpdateUserPermission(_conn, _transaction, ifAllowViewForum, 
                ifAllowViewTopic, ifAllowPost, ifAllowCustomizeAvatar, maxLengthofSignature,
                ifSignatureAllowUrl, ifSignatureAllowUploadImage, minIntervalForPost,
                maxLengthOfPost, ifAllowUrl, ifAllowUploadImage, ifAllowUploadAttachment, 
                maxCountOfAttacmentsForOnePost, maxSizeOfOneAttachment, maxSizeOfAllAttachments,
                maxCountOfMessageSendOneDay, ifAllowSearch, minIntervalForSearch, 
                ifPostNotNeedModeration);
        }

    }
}
