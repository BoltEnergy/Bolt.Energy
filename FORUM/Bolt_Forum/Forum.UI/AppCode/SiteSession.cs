using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.ASPNETState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Com.Comm100.Forum.UI
{
    public class SiteSession
    {

        public static SessionUser CurrentUser
        {
            get { return HttpContext.Current.Session[Constants.SS_CurrentUser] == null ? null : (SessionUser)HttpContext.Current.Session[Constants.SS_CurrentUser]; }
            set { HttpContext.Current.Session[Constants.SS_CurrentUser] = value; }
        }

        public static LoginUserBE DirectLoginUser
        {
            get { return HttpContext.Current.Session[Constants.SS_DirectLoginUser] == null ? null : (LoginUserBE)HttpContext.Current.Session[Constants.SS_DirectLoginUser]; }
            set { HttpContext.Current.Session[Constants.SS_DirectLoginUser] = value; }
        }

        public static UserPermissionCache UserPermissionList
        {
            get { return HttpContext.Current.Session[Constants.SS_UserPermissionList] == null ? null : (UserPermissionCache)HttpContext.Current.Session[Constants.SS_UserPermissionList]; }
            set { HttpContext.Current.Session[Constants.SS_UserPermissionList] = value; }
        }


        public static void Remove(string keyName)
        {
            HttpContext.Current.Session.Remove(keyName);
        }



    }
}