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
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.Bussiness
{
    [Serializable]
    public class UserForumPermissionItem
    {
        #region private fields
        private int _userOrOperatorId;
        private int _forumId;

        //private bool _ifModerator;

        private bool _ifAllowViewForum;//
        private bool _ifAllowViewTopic;//
        private bool _ifAllowPost;//
        private int _minIntervalForPost; //unit: second
        private int _maxLengthOfPost;//
        private bool _ifPostNotNeedModeration;//
        //private bool _ifAllowHTML;//
        private bool _ifAllowUrl;//
        private bool _ifAllowUploadImage;//
        #endregion

        #region properties
        public int UserOrOperatorId
        {
            get { return this._userOrOperatorId; }
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

        //public bool IfModerator
        //{
        //    get { return this._ifModerator; }
        //}

        public UserForumPermissionItem(int userOrOperatorId, bool ifAdministrator, bool ifModerator, bool ifAllowViewForum,
            bool ifAllowViewTopic, bool ifAllowPost, int minIntervalForPost, int maxLengthOfPost, bool ifPostNotNeedModeration, bool ifAllowCustomizeAvatar,
            int maxLengthofSignature, bool ifAllowUrl, bool ifAllowUploadImage, bool ifAllowUploadAttachment, int maxCountOfAttacmentsForOnePost,
            int maxSizeOfOneAttachment, int maxSizeOfAllAttachments, int maxCountOfMessageSendOneDay, bool ifAllowSearch, int minIntervalForSearch)
        {
 
        }

        public UserForumPermissionItem(int userOrOperatorId,int forumId, bool ifModerator, bool ifAllowViewForum,
            bool ifAllowViewTopic, bool ifAllowPost, int minIntervalForPost, int maxLengthOfPost,
            bool ifPostNotNeedModeration, bool ifAllowUrl, bool ifAllowUploadImage)
        {
            _userOrOperatorId = userOrOperatorId;
            _forumId = forumId;
            //_ifModerator = ifModerator;
            _ifAllowViewForum = ifAllowViewForum;
            _ifAllowViewTopic = ifAllowViewTopic;
            _ifAllowPost = ifAllowPost;
            _minIntervalForPost = minIntervalForPost;
            _maxLengthOfPost = maxLengthOfPost;
            _ifPostNotNeedModeration = ifPostNotNeedModeration;
            //_ifAllowHTML = ifAllowHTML;
            _ifAllowUrl = ifAllowUrl;
            _ifAllowUploadImage = ifAllowUploadImage;
        }
    }
}
