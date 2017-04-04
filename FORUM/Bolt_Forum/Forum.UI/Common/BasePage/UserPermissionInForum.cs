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
using System.Web;

namespace Com.Comm100.Forum.UI.Common
{
    public class UserPermissionInFourm
    {
        #region
        ///*User Status*/
        //private int _forumId;
        //public int ForumId {get{return _forumId;} }
        //private bool _ifAdmin;
        //public bool  IfAdmin{get{return _ifAdmin;}}
        //private bool _ifModerator;
        //public bool IfModerator{get{return _ifModerator;}}
        ///*User Permission*/
        //private bool _AllowViewForum;
        //private bool _AllowViewTopicOrPost;
        //private bool _AllowPostTopicOrPost;
        //private bool _AllowCustomizeAvatar;
        //private int _MaxLengthofSignature;
        //private int _MinIntervalTimeforPosting;
        //private int _MaxLengthofTopicOrPost;
        //private bool _AllowHTML;
        //private bool _AllowURL;
        //private bool _AllowInsertImage;
        //private bool _AllowAttachment;
        //private int _MaxAttachmentsinOnePost;
        //private int _MaxSizeoftheAttachment;
        //private int _MaxSizeofalltheAttachments;
        //private int _MaxMessagesSentinOneDay;
        //private bool _AllowSearch;
        //private int _MinIntervalTimeforSearching;
        //private bool _PostModerationNotRequired;
        #endregion

        public int ForumId { get; set; }

        public bool IfAdmin { get; set; }
        public bool IfModerator { get; set; }

        public bool IfAllowViewForum { get; set; }
        public bool IfAllowViewTopicOrPost { get; set; }
        public bool IfAllowPostTopicOrPost { get; set; }
        public bool IfAllowCustomizeAvatar { get; set; }
        public int MaxLengthofSignature { get; set; }
        public int MinIntervalTimeforPosting { get; set; }
        public int MaxLengthofTopicOrPost { get; set; }
        //public bool IfAllowHTML { get; set; }
        public bool IfAllowURL { get; set; }
        public bool IfAllowInsertImage { get; set; }
        public bool IfAllowAttachment { get; set; }
        public int MaxAttachmentsinOnePost { get; set; }
        public int MaxSizeoftheAttachment { get; set; }
        public int MaxSizeofalltheAttachments { get; set; }
        public int MaxMessagesSentinOneDay { get; set; }
        public bool IfAllowSearch { get; set; }
        public int MinIntervalTimeforSearching { get; set; }
        public bool IfPostModerationNotRequired { get; set; }

        public UserPermissionInFourm(int forumId, bool ifAdmin, bool ifModerator,
            bool ifAllowViewForum, bool ifAllowViewTopicOrPost, bool ifAllowPostTopicOrPost,
            bool ifAllowcustomizeAvatar, int maxLengthofSignature, int minIntervalTimeforPosting, int maxLengthofTopicOrPost,
            bool ifAllowURL, bool ifAllowInsertImage, bool ifAllowAttachment, int maxAttachmentsinOnePost, int maxSizeoftheAttachment,
            int maxSizeofalltheAttachments, int maxMessagesSentinOneDay, bool ifAllowSearch, int minIntervalTimeforSearching, bool ifPostModerationNotRequired)
        {
            ForumId = forumId;

            IfAdmin = ifAdmin;
            IfModerator = ifModerator;

            IfAllowViewForum = ifAllowViewForum;
            IfAllowViewTopicOrPost = ifAllowViewTopicOrPost;
            IfAllowPostTopicOrPost = ifAllowPostTopicOrPost;
            IfAllowCustomizeAvatar = ifAllowcustomizeAvatar;
            MaxLengthofSignature = maxLengthofSignature;
            MinIntervalTimeforPosting = minIntervalTimeforPosting;
            MaxLengthofTopicOrPost = maxLengthofTopicOrPost;
            //IfAllowHTML = ifAllowHTML;
            IfAllowURL = ifAllowURL;
            IfAllowInsertImage = ifAllowInsertImage;
            IfAllowAttachment = ifAllowAttachment;
            MaxAttachmentsinOnePost = maxAttachmentsinOnePost;
            MaxSizeoftheAttachment = maxSizeoftheAttachment;
            MaxSizeofalltheAttachments = maxSizeofalltheAttachments;
            MaxMessagesSentinOneDay = maxMessagesSentinOneDay;
            IfAllowSearch = ifAllowSearch;
            MinIntervalTimeforSearching = minIntervalTimeforSearching;
            IfPostModerationNotRequired = ifPostModerationNotRequired;
        }
    }
}
