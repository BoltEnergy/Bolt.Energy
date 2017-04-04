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
using System.Collections;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Common;
using System.Web;

namespace Com.Comm100.Forum.Bussiness
{
    [Serializable]
    public class UserPermissionCache
    {
        private int _userOrOperatorId;
        private Hashtable _userForumPermissions;

        /*base permission*/
        private bool _ifAdministrator;
        private bool _ifAllowCustomizeAvatar;
        private int _maxLengthofSignature;
        private bool _ifAllowUploadAttachment;
        private int _maxCountOfAttacmentsForOnePost;
        private int _maxSizeOfOneAttachment;//unit:Kbyte
        private int _maxSizeOfAllAttachments;//unit:Kbyte
        private int _maxCountOfMessageSendOneDay;
        private bool _ifAllowSearch;
        private int _minIntervalForSearch;

        //private bool _ifSignatureAllowHTML;
        private bool _ifSignatureAllowUrl;
        private bool _ifSignatureAllowInsertImage;

        /*forum permission*/
        private bool _ifAllowViewForum;//
        private bool _ifAllowViewTopic;//
        private bool _ifAllowPost;//
        private int _minIntervalForPost; //unit: second
        private int _maxLengthOfPost;//
        private bool _ifPostNotNeedModeration;//
        //private bool _ifAllowHTML;//
        private bool _ifAllowUrl;//
        private bool _ifAllowUploadImage;//

        #region forum permission properties
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

        //public bool IfSignatureAllowHTML
        //{
        //    get { return _ifSignatureAllowHTML; }
        //    set { _ifSignatureAllowHTML = value; }
        //}

        public bool IfSignatureAllowUrl
        {
            get { return _ifSignatureAllowUrl; }
            set { _ifSignatureAllowUrl = value; }
        }
        public bool IfSignatureAllowInsertImage
        {
            get { return _ifSignatureAllowInsertImage; }
            set { _ifSignatureAllowInsertImage = value; }
        }

        #region Custom Property
        public int UserOrOperatorId
        {
            get { return this._userOrOperatorId; }
        }

        public bool IfAdministrator
        {
            get { return this._ifAdministrator; }
        }
        
        public bool IfAllowCustomizeAvatar
        {
            get { return this._ifAllowCustomizeAvatar; }
        }
        public int MaxLengthofSignature
        {
            get { return this._maxLengthofSignature; }
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

        #endregion

        public Hashtable UserForumPermissionsList
        {
            get { return _userForumPermissions; }
            set { _userForumPermissions = value; }
        }

        public UserPermissionCache(int userOrOperatorId, Hashtable userForumPermissions,
            bool ifAdministrator, bool ifAllowCustomizeAvatar, int maxLengthofSignature,
            bool ifAllowUploadAttachment, int maxCountOfAttacmentsForOnePost, int maxSizeOfOneAttachment,
            int maxSizeOfAllAttachments, int maxCountOfMessageSendOneDay, bool ifAllowSearch, int minIntervalForSearch,
            bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost, int minIntervalForPost, int maxLengthOfPost,
            bool ifPostNotNeedModeration, bool ifAllowUrl, bool ifAllowUploadImage)
        {
            _userOrOperatorId = userOrOperatorId;
            _userForumPermissions = userForumPermissions;
            _ifAdministrator = ifAdministrator;
            _ifAllowCustomizeAvatar = ifAllowCustomizeAvatar;
            _maxLengthofSignature = maxLengthofSignature;
            _ifAllowUploadAttachment = ifAllowUploadAttachment;
            _maxCountOfAttacmentsForOnePost = maxCountOfAttacmentsForOnePost;
            _maxSizeOfOneAttachment = maxSizeOfOneAttachment;
            _maxSizeOfAllAttachments = maxSizeOfAllAttachments;
            _maxCountOfMessageSendOneDay = maxCountOfMessageSendOneDay;
            _ifAllowSearch = ifAllowSearch;
            _minIntervalForSearch = minIntervalForSearch;

            /*forum permission*/
            _ifAllowViewForum = ifAllowViewForum;//
            _ifAllowViewTopic = ifAllowViewTopic;//
            _ifAllowPost = ifAllowPost;//
            _minIntervalForPost = minIntervalForPost; //unit: second
            _maxLengthOfPost = maxLengthOfPost;//
            _ifPostNotNeedModeration = ifPostNotNeedModeration;//
            //_ifAllowHTML = ifAllowHTML;//
            _ifAllowUrl = ifAllowUrl;//
            _ifAllowUploadImage = ifAllowUploadImage;//
        }

        public UserPermissionCache(int userOrOperatorId, Hashtable userForumPermissions,
            bool ifAdministrator, bool ifAllowCustomizeAvatar, int maxLengthofSignature,
            bool ifAllowUploadAttachment, int maxCountOfAttacmentsForOnePost, int maxSizeOfOneAttachment,
            int maxSizeOfAllAttachments, int maxCountOfMessageSendOneDay, bool ifAllowSearch, int minIntervalForSearch,
            bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost, int minIntervalForPost, int maxLengthOfPost,
            bool ifPostNotNeedModeration, bool ifAllowUrl, bool ifAllowUploadImage
            , bool ifSignatureAllowInsertImage, bool ifSignatureAllowUrl)
        {
            _userOrOperatorId = userOrOperatorId;
            _userForumPermissions = userForumPermissions;
            _ifAdministrator = ifAdministrator;
            _ifAllowCustomizeAvatar = ifAllowCustomizeAvatar;
            _maxLengthofSignature = maxLengthofSignature;
            _ifAllowUploadAttachment = ifAllowUploadAttachment;
            _maxCountOfAttacmentsForOnePost = maxCountOfAttacmentsForOnePost;
            _maxSizeOfOneAttachment = maxSizeOfOneAttachment;
            _maxSizeOfAllAttachments = maxSizeOfAllAttachments;
            _maxCountOfMessageSendOneDay = maxCountOfMessageSendOneDay;
            _ifAllowSearch = ifAllowSearch;
            _minIntervalForSearch = minIntervalForSearch;

            /*forum permission*/
            _ifAllowViewForum = ifAllowViewForum;//
            _ifAllowViewTopic = ifAllowViewTopic;//
            _ifAllowPost = ifAllowPost;//
            _minIntervalForPost = minIntervalForPost; //unit: second
            _maxLengthOfPost = maxLengthOfPost;//
            _ifPostNotNeedModeration = ifPostNotNeedModeration;//
            //_ifAllowHTML = ifAllowHTML;//
            _ifAllowUrl = ifAllowUrl;//
            _ifAllowUploadImage = ifAllowUploadImage;//
            //_ifSignatureAllowHTML = ifSignatureAllowHTML;
            _ifSignatureAllowInsertImage = ifSignatureAllowInsertImage;
            _ifSignatureAllowUrl = ifSignatureAllowUrl;
        }

        private bool IfAdmin(UserOrOperator operatingUserOrOperator)
        {
            if (operatingUserOrOperator.IfForumAdmin)
                return true;
            if (operatingUserOrOperator is OperatorWithPermissionCheck)
            {
                if ((operatingUserOrOperator as OperatorWithPermissionCheck).IfAdmin == true)
                    return true;
            }
            return false;
        }

        public void CheckIfAllowViewForumPermission(int forumId, UserOrOperator operatingUserOrOperator, bool ifModerator)
        {
            UserForumPermissionItem item = _userForumPermissions[forumId] as UserForumPermissionItem;
            if (!CommFun.IfGuest() && !IfAdmin(operatingUserOrOperator) && !ifModerator
                && !item.IfAllowViewForum)
                ExceptionHelper.ThrowForumUserWithoutPermissionViewForumException();
        }

        public void CheckIfAllowViewTopicPermission(int forumId, UserOrOperator operatingUserOrOperator, bool ifModerator)
        {
            CheckIfAllowViewForumPermission(forumId, operatingUserOrOperator, ifModerator);
            UserForumPermissionItem item = _userForumPermissions[forumId] as UserForumPermissionItem;
            if (!CommFun.IfGuest() && !IfAdmin(operatingUserOrOperator) && !ifModerator
                && !item.IfAllowViewTopic)
                ExceptionHelper.ThrowForumUserWithoutPermissionAllowViewTopicOrPostException();
        }
        public void CheckIfAllowPostTopicOrPostPermission(int forumId, UserOrOperator operatingUserOrOperator, bool ifModerator)
        {
            CheckIfAllowViewForumPermission(forumId, operatingUserOrOperator, ifModerator);
            UserForumPermissionItem item = _userForumPermissions[forumId] as UserForumPermissionItem;
            if (!CommFun.IfGuest() && !IfAdmin(operatingUserOrOperator) && !ifModerator
                && !item.IfAllowPost)
                ExceptionHelper.ThrowForumUserWithoutPermissionAllowPostTopicOrPostException();
        }
        public void CheckIfAllowCustomizeAvatarPermission(UserOrOperator operatingUserOrOperator)
        {
            if(!CommFun.IfGuest()&&!IfAdmin(operatingUserOrOperator)&&!this.IfAllowCustomizeAvatar)
                ExceptionHelper.ThrowForumUserWithoutPermissionAllowCustomizeAvatarException();
        }
        public void CheckMaxLengthofSignature(UserOrOperator operatingUserOrOperator,long length)
        {
            if (!CommFun.IfGuest() && !IfAdmin(operatingUserOrOperator) && length>this.MaxLengthofSignature)
                ExceptionHelper.ThrowForumUserWithoutPermissionMaxLengthofSignatureException(this.MaxLengthofSignature);
        }
        public void CheckMinIntervalTimeForPostingPermission(DateTime lastPostTime, int forumId, UserOrOperator operatingUserOrOperator, bool ifModerator)
        {
            UserForumPermissionItem item = _userForumPermissions[forumId] as UserForumPermissionItem;
            TimeSpan tsInterval = DateTime.UtcNow - lastPostTime;
            if (!CommFun.IfGuest() && !IfAdmin(operatingUserOrOperator) && !ifModerator
                && (tsInterval < TimeSpan.FromSeconds(item.MinIntervalForPost)))
                ExceptionHelper.ThrowForumUserWithoutPermissionMinIntervalTimeforPostingException(
                    (int)( item.MinIntervalForPost-tsInterval.TotalSeconds));
        }
        public void CheckMaxLengthOfTopicOrPost(int forumId, int lengthOfContent, UserOrOperator operatingUserOrOperator, bool ifModerator)
        {
            UserForumPermissionItem item = _userForumPermissions[forumId] as UserForumPermissionItem;
            if (!CommFun.IfGuest() && !IfAdmin(operatingUserOrOperator) && !ifModerator
                && (lengthOfContent>item.MaxLengthOfPost))
                ExceptionHelper.ThrowForumUserWithoutPermissionMaxLengthofTopicOrPostException(
                    item.MaxLengthOfPost);
        }
        //public bool IfAllowHtmlPermission(int forumId, UserOrOperator operatingUserOrOperator, bool ifModerator)
        //{
        //    UserForumPermissionItem item = _userForumPermissions[forumId] as UserForumPermissionItem;
        //    if (IfAdmin(operatingUserOrOperator) || ifModerator)
        //        return true;
        //    return item.IfAllowHTML;
        //}
        public bool IfAllowURLPermission(int forumId, UserOrOperator operatingUserOrOperator, bool ifModerator)
        {
            //IfAllowHtmlPermission(forumId, operatingUserOrOperator, ifModerator);
            UserForumPermissionItem item = _userForumPermissions[forumId] as UserForumPermissionItem;
            if (IfAdmin(operatingUserOrOperator) || ifModerator)
                return true;
            return item.IfAllowUrl;
        }
        public bool IfAllowInsertImagePermission(int forumId, UserOrOperator operatingUserOrOperator, bool ifModerator)
        {
            //IfAllowHtmlPermission(forumId, operatingUserOrOperator, ifModerator);
            UserForumPermissionItem item = _userForumPermissions[forumId] as UserForumPermissionItem;
            if (IfAdmin(operatingUserOrOperator) || ifModerator)
                return true;
            //if (!item.IfAllowHTML)
            //    return false;
            return item.IfAllowUploadImage;
        }

        //public bool IfSignatureAllowHtmlPermission(UserOrOperator operatingUserOrOperator)
        //{
        //    if (IfAdmin(operatingUserOrOperator))
        //        return true;
        //    return this._ifSignatureAllowHTML;
        //}

        public bool IfSignatureAllowURLPermission(UserOrOperator operatingUserOrOperator)
        {
            //IfSignatureAllowHtmlPermission(operatingUserOrOperator);
            if (IfAdmin(operatingUserOrOperator))
                return true;
            return this._ifSignatureAllowUrl;
        }

        public bool IfSignatureAllowInsertImagePermission(UserOrOperator operatingUserOrOperator)
        {
            //IfSignatureAllowHtmlPermission(operatingUserOrOperator);
            if (IfAdmin(operatingUserOrOperator))
                return true;
            return this._ifSignatureAllowInsertImage;
        }

        public void CheckIfAllowAttachmentPermission(UserOrOperator operatingUserOrOperator, bool ifModerator)
        {
            if (!CommFun.IfGuest() && !IfAdmin(operatingUserOrOperator) && !ifModerator && !this.IfAllowUploadAttachment)
                ExceptionHelper.ThrowForumUserWithoutPermissionAllowAttachmentException();
        }
        public void CheckMaxAttachmentsinOnePostPermission(int CountOfAttachmentsForOnePost,UserOrOperator operatingUserOrOperator,
            bool ifModerator)
        {
            CheckIfAllowAttachmentPermission(operatingUserOrOperator,ifModerator);
            if (!CommFun.IfGuest() && !IfAdmin(operatingUserOrOperator) && !ifModerator
                && this.MaxCountOfAttacmentsForOnePost <= CountOfAttachmentsForOnePost)
                ExceptionHelper.ThrowForumUserWithoutPermissionMaxAttachmentsinOnePostException(
                    this.MaxCountOfAttacmentsForOnePost);
        }
        public void CheckMaxSizeoftheAttachmentPermission(int SizeOftheAttachment,UserOrOperator operatingUserOrOperator,
            bool ifModerator)
        {
            CheckIfAllowAttachmentPermission(operatingUserOrOperator,ifModerator);
            if (!CommFun.IfGuest() && !IfAdmin(operatingUserOrOperator) && !ifModerator
                && this.MaxSizeOfOneAttachment * 1024 < SizeOftheAttachment )
                ExceptionHelper.ThrowForumUserWithoutPermissionMaxSizeoftheAttachmentException(
                    this.MaxSizeOfOneAttachment);
        }
        public void CheckMaxSizeofalltheAttachmentsPermission(int SizeofalltheAttachments, UserOrOperator operatingUserOrOperator,
            bool ifModerator)
        {
            if (!CommFun.IfGuest() && !IfAdmin(operatingUserOrOperator) && !ifModerator
                && this.MaxSizeOfAllAttachments * 1024 < SizeofalltheAttachments )
                ExceptionHelper.ThrowForumUserWithoutPermissionMaxSizeofalltheAttachmentsException(
                    this.MaxSizeOfAllAttachments);
        }
        public void CheckMaxMessagesSentinOneDayPermission(int maxCountOfMessagesSentinOneDay, UserOrOperator operatingUserOrOperator)
        {
            if (!CommFun.IfGuest() && !IfAdmin(operatingUserOrOperator)
                && this.MaxCountOfMessageSendOneDay <= maxCountOfMessagesSentinOneDay)
                ExceptionHelper.ThrowForumUserWithoutPermissionMaxMessagesSentinOneDayException(
                    this.MaxCountOfMessageSendOneDay);
        }
        public void CheckIfAllowSearchPermission(UserOrOperator operatingUserOrOperator)
        {
            if (!CommFun.IfGuest() && !IfAdmin(operatingUserOrOperator) && !this.IfAllowSearch)
                ExceptionHelper.ThrowForumUserWithoutPermissionAllowSearchException();
        }
        public void CheckMinIntervalTimeforSearchingPermission(UserOrOperator operatingUserOrOperator)
        {
            CheckIfAllowSearchPermission(operatingUserOrOperator);
            DateTime lastSearchTime = CommFun.LastSearchtime();
            if (lastSearchTime == new DateTime())
            {
                HttpContext.Current.Session["LastSearchTime"] = DateTime.UtcNow;
                return;
            }
            if (!CommFun.IfGuest() && !IfAdmin(operatingUserOrOperator))
            {
                TimeSpan tsIntervalTime = DateTime.UtcNow - lastSearchTime;
                if (tsIntervalTime < TimeSpan.FromSeconds(this.MinIntervalForSearch))
                    ExceptionHelper.ThrowForumUserWithoutPermissionMinIntervalTimeforSearchingException(
                        (int)(MinIntervalForSearch-tsIntervalTime.TotalSeconds));
            }
            HttpContext.Current.Session["LastSearchTime"] = DateTime.UtcNow;
        }
        public bool IfPostModerationNotRequiredPermission(int forumId, UserOrOperator operatingUserOrOperator, bool ifModerator)
        {
            UserForumPermissionItem item = _userForumPermissions[forumId] as UserForumPermissionItem;
            if (IfAdmin(operatingUserOrOperator) || ifModerator)
                return true;
            return item.IfPostNotNeedModeration;
        }
        //public bool IfAllowUploadAttachment(int forumId)
        //{
        //    return false;
        //}

        //public bool IfAllowHTMLOfForum(int forumId)
        //{
        //    return (_userForumPermissions[forumId] as UserForumPermissionItem).IfAllowHTML;
        //}

        //public bool IfAllowUrlOfForum(int forumId)
        //{
        //    return (_userForumPermissions[forumId] as UserForumPermissionItem).IfAllowUrl;
        //}
    }
}
