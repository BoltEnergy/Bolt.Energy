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

namespace Com.Comm100.Framework.Common
{
    public class ConstantsHelper
    {
        
        public static readonly string CacheKey_InactivedOrDeletedUserId = "CacheKey_InactivedOrDeletedUserId_{0}_{1}";
        public static readonly string Email_Format = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        
        #region Forum
        public static readonly string ForumAvatarTemporaryFolder = "Temp/Avatar";
        public static readonly string ForumSystemAvatarFolder = "Images/SystemAvatar";
        public static readonly string ForumLogoTemporaryFolder = "Temp/TempLogo";

        public static readonly string Default_Logo_Path = "images/DefaultLogo/DefaultLogo.gif";

        public static readonly int User_Avatar_Width = 160;
        public static readonly int User_Avatar_Height = 60;
        public static readonly int User_Avatar_MaxSize = 50;    //50k
        public static readonly string User_Avatar_ContentType = "image/jpeg";
        public static readonly string User_Avatar_FileType = ".jpg";
        public static readonly string User_Avatar_Default = "000.gif";

        public static readonly int Post_Image_MaxSize = 2048;    //2M

        public static DateTime Forum_DefaultTime = Convert.ToDateTime("1900-01-01");

        #endregion
    }
}
