#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;

namespace Com.Comm100.Forum.UI
{
    public partial class ClearSession : Com.Comm100.Forum.UI.UIBasePage
    {
        public override bool IfValidateForumClosed
        {
            get
            {
                return false;
            }
        }
        public override bool IfValidateIPBanned
        {
            get
            {
                return false;
            }
        }

        public override bool IfValidateUserBanned
        {
            get
            {
                return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
#if OPENSOURCE
#else
                CheckQueryString("siteId");
#endif
                CheckQueryString("userId");
                CheckQueryString("action");
                bool ifOperator = CurrentUserOrOperator.IfOperator;
                int siteId = this.SiteId;//Convert.ToInt32(Request.QueryString["siteId"]);
                int userId = Convert.ToInt32(Request.QueryString["userId"]);
                if (ifOperator)
                {
                    UserOrOperator user = UserProcess.GetNotDeletedUserOrOperatorById(siteId, UserOrOperatorId);
                    //if (user.IfAdmin)
                    //{
                    //    string key = string.Format(ConstantsHelper.CacheKey_InactivedOrDeletedUserId, siteId, userId);
                    //    if (Request.QueryString["action"].ToLower() == "restore")
                    //    {
                    //        if (Cache[key] != null)
                    //        {
                    //            Cache.Remove(key);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        List<string> sessionIdList = new List<string>();
                    //        Cache.Insert(key, sessionIdList, null, DateTime.Now.AddMinutes(Session.Timeout), TimeSpan.Zero);
                    //    }
                    //    if (Request.QueryString["action"].ToLower() == "delete")
                    //    {
                    //        UserProcess.RemoveModeratorForOperator(siteId, UserOrOperatorId, userId);
                    //    }
                    //}
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
