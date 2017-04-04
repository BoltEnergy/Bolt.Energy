#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif

using System;
using System.Collections.Generic;
using System.Web;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.UI.Common;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.UI.Common
{
    public class SearchBasePage : UIBasePage
    {
        public DateTime LastSearchtime()
        {
            if (HttpContext.Current.Session["LastSearchTime"] == null)
                return new DateTime();
            else
                return (DateTime)HttpContext.Current.Session["LastSearchTime"];
        }

        /*Use In Search Page*/
        private UserPermissionCache _userPermissionInSearchPage;
        public UserPermissionCache UserPermissionInSearchPage { get { return _userPermissionInSearchPage; } }


        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
            InitUserPermissionInPage();
        }

        private void InitUserPermissionInPage()
        {
            if (IfGuest)
            {
                GuestUserPermissionSettingWithPermissionCheck guestUser = SettingsProcess.GetGuestUserPermission(SiteId, UserOrOperatorId);
                _userPermissionInSearchPage = new UserPermissionCache(UserOrOperatorId, null, false, false,
                    0, false, 0, 0, 0,
                    0, guestUser.IfAllowGuestUserSearch, guestUser.GuestUserSearchInterval, guestUser.IfAllowGuestUserViewForum,
                    true, false, 0, 0, false, false, false);
            }

            else if (IfAdmin() || IfModerator())
            {
                _userPermissionInSearchPage = new UserPermissionCache(UserOrOperatorId, null, true, true,
                    int.MaxValue, true, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, true, 0, true, true, true,
                    0, int.MaxValue, true, true, true);
            }
            else
            {
                _userPermissionInSearchPage = new UserPermissionCache(UserOrOperatorId, null, false,
                                                    this.UserPermissionCache.IfAllowCustomizeAvatar,
                                                    this.UserPermissionCache.MaxLengthofSignature,
                                                    this.UserPermissionCache.IfAllowUploadAttachment,
                                                    this.UserPermissionCache.MaxCountOfAttacmentsForOnePost,
                                                    this.UserPermissionCache.MaxSizeOfOneAttachment,
                                                    this.UserPermissionCache.MaxSizeOfAllAttachments,
                                                    this.UserPermissionCache.MaxCountOfMessageSendOneDay,
                                                    this.UserPermissionCache.IfAllowSearch,
                                                    this.UserPermissionCache.MinIntervalForSearch,
                                                    this.UserPermissionCache.IfAllowViewForum,
                                                    this.UserPermissionCache.IfAllowViewTopic,
                                                    this.UserPermissionCache.IfAllowPost,
                                                    this.UserPermissionCache.MinIntervalForPost,
                                                    this.UserPermissionCache.MaxLengthOfPost,
                                                    this.UserPermissionCache.IfPostNotNeedModeration,
                                                    this.UserPermissionCache.IfAllowUrl,
                                                    this.UserPermissionCache.IfAllowUploadImage);
            }
        }

        protected string MyEncode(string source)
        {
            return System.Web.HttpUtility.UrlEncode(EncryptionDecryptionHelper.Encode(source));
        }

        protected string MyDecode(string source)
        {
            return System.Web.HttpUtility.UrlDecode(EncryptionDecryptionHelper.Decode(source));
        }

        protected string GetQueryString(string url, string key)
        {
            key = key + "=";
            if (url.Contains(key))
            {
                int startIndex = url.IndexOf(key);
                int endLen = url.Remove(0, startIndex).IndexOf("&") - key.Length;
                if (endLen < 0)
                    endLen = url.Length - startIndex;
                return System.Web.HttpUtility.UrlDecode(url.Substring(startIndex + key.Length, endLen));
            }
            else
            {
                ExceptionHelper.ThrowSystemQuerystringNullException(key);
                return "";
            }
        }
    }
}
