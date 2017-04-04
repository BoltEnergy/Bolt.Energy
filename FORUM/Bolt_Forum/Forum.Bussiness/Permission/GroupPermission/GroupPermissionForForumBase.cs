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
    public abstract class GroupPermissionForForumBase : IViewForumPermission, IViewTopicPermission, IHTMLPermission, IImagePermission, IPostPermission
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        protected int _groupId;
        protected int _forumId;
        protected bool _ifAllowViewForum;
        protected bool _ifAllowViewTopic;
        protected bool _ifAllowPost;
        protected int _minIntervalForPost; //unit: second
        protected int _maxLengthOfPost;
        protected bool _ifPostNotNeedModeration;
        //protected bool _ifAllowHTML;
        protected bool _ifAllowUrl;
        protected bool _ifAllowUploadImage;
        #endregion

        #region properties
        public int GroupId
        {
            get { return this._groupId; }
        }
        public int ForumId
        {
            get { return this._forumId; }
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
        public bool IfPostNotNeedModeration
        {
            get { return this._ifPostNotNeedModeration; }
        }
        #endregion

        public GroupPermissionForForumBase(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            _conn = conn;
            _transaction = transaction;
        }
    }
}
