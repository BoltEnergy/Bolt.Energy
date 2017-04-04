using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Com.Comm100.Forum.UI
{
    public class Constants
    {

        #region sqlpaarameters

        public const string UserId = "UserId";
        public const string MongoUID = "MongoUID";
        public const string TopicId = "TopicId";
        public const string Email = "Email";
        public const string FirstName = "FirstName";
        public const string LastName = "LastName";
        

        #endregion

        #region sessionName

        public const string SS_CurrentUser = "CurrentUser";
        public const string SS_DirectLoginUser = "DirectLoginUser";
        public const string SS_UserPermissionList = "UserPermissionList";

        

        #endregion

       

        #region appsetting

        public const string WK_BaseURL = "BaseURL";
        public const string WK_ForumId = "forumId";
        public const string WK_SiteId = "siteId";
        public const string WK_Defaultlang = "Defaultlang";

        public const string WK_CheckLogin = "CheckLogin";

        public const string WK_MongoConnectionString = "MongoConnectionString";


        public const string WK_Db = "db";

        

        
        

        public const string WK_UserPassword = "UserPassword";

        #endregion

        #region other basic 

        public const string Remote_address = "remote_addr";

        #endregion


    }
}