﻿#if OPENSOURCE
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

namespace Com.Comm100.Forum.Bussiness
{
    [Serializable]
    public class GroupPermission
    {
        public bool IfAllowViewForum { get; set; }
        public bool IfAllowViewTopic { get; set; }
        public bool IfAllowPost { get; set; }
        public int MinIntervalTimeForPosting { get; set; }
        public int MaxLengthOfTopicPost { get; set; }
        public bool IfAllowHtml { get; set; }
        public bool IfAllowUrl { get; set; }
        public bool IfAllowInsertImage { get; set; }
        public bool IfPostModerationNotRequired { get; set; }
        public int GroupId { get; set; }

        public GroupPermission() { }
    }
}
