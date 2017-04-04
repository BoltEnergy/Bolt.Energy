using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Com.Comm100.Forum.UI.Common
{
    public class UserPermission
    {
        /*User Status*/
        private bool _IfAdmin;
        private bool _IfModerator;
        /*User Permission*/
        private bool _AllowViewForum;
        private bool _AllowViewTopicOrPost;
        private bool _AllowPostTopicOrPost;
        private bool _AllowCustomizeAvatar;
        private int _MaxLengthofSignature;
        private int _MinIntervalTimeforPosting;
        private int _MaxLengthofTopicOrPost;
        private bool _AllowHTML;
        private bool _AllowURL;
        private bool _AllowInsertImage;
        private bool _AllowAttachment;
        private int _MaxAttachmentsinOnePost;
        private int _MaxSizeoftheAttachment;
        private int _MaxSizeofalltheAttachments;
        private int _MaxMessagesSentinOneDay;
        private bool _AllowSearch;
        private int _MinIntervalTimeforSearching;
        private bool _PostModerationNotRequired;

        public bool AllowViewForum;
        public bool AllowViewTopicOrPost;
        public bool AllowPostTopicOrPost;
        public bool AllowCustomizeAvatar;
        public int MaxLengthofSignature;
        public int MinIntervalTimeforPosting;
        public int MaxLengthofTopicOrPost;
        public bool AllowHTML;
        public bool AllowURL;
        public bool AllowInsertImage;
        public bool AllowAttachment;
        public int MaxAttachmentsinOnePost;
        public int MaxSizeoftheAttachment;
        public int MaxSizeofalltheAttachments;
        public int MaxMessagesSentinOneDay;
        public bool AllowSearch;
        public int MinIntervalTimeforSearching;
        public bool PostModerationNotRequired;
    }
}
