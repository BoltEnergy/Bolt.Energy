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

namespace Com.Comm100.Framework.HelpDocument
{
    public class ForumHelpDocument
    {
       
        #region Forum List
        public static readonly string helpMessageForumList = "Forum is the online discussion area for forum participants to ask questions and share ideas.";
        public static readonly string helpMessageForumNew = "Add a new forum. when adding a new forum, you need to choose a category and appoint a moderator for this new forum.";
        public static readonly string helpMessageForumEdit = "Edit the selected forum.";
        #endregion 
        
        #region Styles
        public static readonly string helpMessageHEaderFooterSetting = "Customize your own logo, and page headers and footers according to your need."
                                                                    + "Easy Mode is a simple custom mode that allows you only to customize your own logo in the header. You can upload your own logo. Or you can just take the default logo."
                                                                    + "Advanced Mode is a full custom mode that allows you to customize your forum header and footer. In this page, you can write your own header and footer using HTML.";
        #endregion
    }
}
