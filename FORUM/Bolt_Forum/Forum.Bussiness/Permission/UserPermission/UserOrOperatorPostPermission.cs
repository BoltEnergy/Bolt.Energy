#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2009                                                                               //
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
    public class UserOrOperatorPostPermission : UserOrOperatorPermissionBase, IPostPermission, IHTMLPermission, IImagePermission
    {
        private int _forumId;
        private bool _ifAllowPost;
        private bool _ifPostNotNeedModeration;
        private int _minIntervalForPost;
        private int _maxLengthOfPost;
        private bool _ifAllowHTML;
        private bool _ifAllowUrl;
        private bool _ifAllowUpdateImage;

        public int ForumId
        {
            get { return this._forumId; }
        }
        public bool IfAllowPost 
        {
            get { return this._ifAllowPost; }
        }
        public bool IfPostNotNeedModeration 
        {
            get { return this._ifPostNotNeedModeration; }
        }
        public int MinIntervalForPost 
        {
            get { return this._minIntervalForPost; }
        } //unit: second
        public int MaxLengthOfPost 
        {
            get { return this._maxLengthOfPost; }
        }
        public bool IfAllowHTML
        {
            get { return this._ifAllowHTML; }
        }
        public bool IfAllowUrl
        {
            get { return this._ifAllowUrl; }
        }
        public bool IfAllowUploadImage
        {
            get { return this._ifAllowUpdateImage; }
        }

        public UserOrOperatorPostPermission(SqlConnection generalConn, SqlTransaction generalTransaction, SqlConnectionWithSiteId siteConn, SqlTransaction siteTransaction, UserOrOperator userOrOperator, int forumId)
            : base(generalConn, generalTransaction, siteConn, siteTransaction, userOrOperator)
        { }

        public void CheckPermission()
        {
            //注意：在论坛关闭、论坛不能访问、用户被禁用等情况下，用户也是不能发帖的
        }
    }
}
