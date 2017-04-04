
#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Data;
using System.Configuration;
using System.Web.Configuration;

namespace Com.Comm100.Forum.UI.Common
{
    public class ForumConfig
    {
        //public static readonly string CustomizeAvatarFolder = WebConfigurationManager.AppSettings["CustomizeAvatarFolder"].ToString();
        //public static readonly string CustomizeLogoFolder = WebConfigurationManager.AppSettings["CustomizeLogoFolder"].ToString();

        private static ForumConfig _instance = null;
        private ForumConfig()
        {
        }
        public static ForumConfig GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ForumConfig();
            }
            return _instance;
        }

        public string AdminUrl
        {
            get 
            {
                return WebConfigurationManager.AppSettings["AdminUrl"].ToString();
            }
        }

        public Int32 ForumId
        {
            get
            {
                return Convert.ToInt32(WebConfigurationManager.AppSettings["forumId"]);
            }
        }
        
    }
}
