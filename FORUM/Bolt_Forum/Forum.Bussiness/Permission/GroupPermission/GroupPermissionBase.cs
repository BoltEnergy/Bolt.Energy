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
    public abstract class GroupPermissionBase : IViewForumPermission, IViewTopicPermission, IAttachmentPermission, IHTMLPermission, IAvatarPermission, IImagePermission, IMessagePermission, IPostPermission, ISearchPermission, ISignaturePermission
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        protected int _groupId;
        protected bool _ifAllowViewForum;
        protected bool _ifAllowViewTopic;
        protected bool _ifAllowPost;
        protected int _minIntervalForPost; //unit: second
        protected int _maxLengthOfPost;
        protected bool _ifPostNotNeedModeration;
        protected bool _ifAllowCustomizeAvatar;
        protected int _maxLengthofSignature;
        /*--------Signature content permission added 5.13 by Allon.--------*/
        //protected bool _ifSignatureAllowHTML;
        protected bool _ifSignatureAllowUrl;
        protected bool _ifSignatureAllowInsertImage;
        /*-------------*/
        //protected bool _ifAllowHTML;
        protected bool _ifAllowUrl;
        protected bool _ifAllowUploadImage;
        protected bool _ifAllowUploadAttachment;
        protected int _maxCountOfAttacmentsForOnePost;
        protected int _maxSizeOfOneAttachment;//unit:Kbyte
        protected int _maxSizeOfAllAttachments;//unit:Kbyte
        protected int _maxCountOfMessageSendOneDay;
        protected bool _ifAllowSearch;
        protected int _minIntervalForSearch;
        #endregion

        #region properties
        public int GroupId
        {
            get { return this._groupId; }
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
        /*--------Signature content permission added 5.13 by Allon.--------*/
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
        /*----------------*/
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

        public GroupPermissionBase(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            this._conn = conn;
            this._transaction = transaction;
        }
    }
}
