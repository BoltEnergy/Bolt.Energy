
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
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.Bussiness;
using System.Data.SqlClient;
using Com.Comm100.Framework.Database;
using System.Web.Security;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.UI.Common;

namespace Com.Comm100.Forum.UI.UserPanel
{
    public class UserBasePage : Com.Comm100.Forum.UI.UIBasePage
    {
        
        protected override void OnInit(EventArgs e)
        {
            this.ClearPageCache();
            base.OnInit(e);
        }

        public override bool IfValidateUserBanned
        {
            get
            {
                return true;
            }
        }

        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
            InitUserPermissionInPage();
            if (Session.IsNewSession)
            {
                string sCookieHeader = Request.Headers["Cookie"];
                if (null != sCookieHeader && sCookieHeader.IndexOf("ASP.NET_SessionId") >= 0)
                {
                    if (Request.IsAuthenticated)
                    {
                        FormsAuthentication.SignOut();
                    }
                    Response.Redirect("~/Login.aspx?siteId=" + SiteId + "&Timeout=&ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.PathAndQuery));
                }
            }
            if (this.CurrentUserOrOperator == null || this.CurrentUserOrOperator.UserOrOperatorId == 0)
            {
                string currentUrl = this.Request.Url.PathAndQuery;
                currentUrl = System.Web.HttpUtility.UrlEncode(currentUrl);
                Response.Redirect("~/Login.aspx?siteId=" + SiteId + "&ReturnUrl=" + currentUrl);
            }
        }

        protected void ClearPageCache()
        {
            Response.BufferOutput = true;
            Response.Expires = 0;
            Response.ExpiresAbsolute = DateTime.UtcNow.AddMinutes(-1);
            Response.CacheControl = "no-cache";
        }

        private UserPermissionCache _userPermissionInSignaturePage;
        public UserPermissionCache UserPermissionInSignature
        {
            get { return _userPermissionInSignaturePage; }
        }
        private void InitUserPermissionInPage()
        {
            if (IfGuest)
            {
                GuestUserPermissionSettingWithPermissionCheck guestUser = SettingsProcess.GetGuestUserPermission(SiteId, UserOrOperatorId);
                _userPermissionInSignaturePage = new UserPermissionCache(UserOrOperatorId, null, false, false,
                    0, false, 0, 0, 0,
                    0, guestUser.IfAllowGuestUserSearch, guestUser.GuestUserSearchInterval, guestUser.IfAllowGuestUserViewForum,
                    true, false, 0, 0, false, false, false,false,false);
            }

            else if (IfAdmin() || IfModerator())
            {
                _userPermissionInSignaturePage = new UserPermissionCache(UserOrOperatorId, null, true, true,
                    int.MaxValue, true, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, true, 0, true, true, true,
                    0, int.MaxValue, true, true,true,true,true);
            }
            else
            {
                _userPermissionInSignaturePage = new  UserPermissionCache(UserOrOperatorId, null, false,
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
                                //this.UserPermissionCache.IfAllowHTML,
                                this.UserPermissionCache.IfAllowUrl,
                                this.UserPermissionCache.IfAllowUploadImage,
                                this.UserPermissionCache.IfSignatureAllowInsertImage,
                                //this.UserPermissionCache.IfSignatureAllowHTML,
                                this.UserPermissionCache.IfSignatureAllowUrl);
            }
        }
        
    }
}
