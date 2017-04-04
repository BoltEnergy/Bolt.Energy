
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
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.UI;
using System.Web.UI.HtmlControls;
using Com.Comm100.Forum.UI.Common;

namespace Forum.UI
{
    public partial class MainMasterPage : System.Web.UI.MasterPage
    {
        public int SiteId
        {
            get
            {
                return ((UIBasePage)this.Page).SiteId;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SiteSettingWithPermissionCheck tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, 0);
            hlhome.NavigateUrl = WebUtility.GetAppSetting(Constants.WK_BaseURL);
            HtmlMeta metaKeywords = new HtmlMeta();
            metaKeywords.Name = "keywords";
            metaKeywords.Content = tmpSiteSetting.MetaKeywords;
            Page.Header.Controls.Add(metaKeywords);
            HtmlMeta metaDescription = new HtmlMeta();
            metaDescription.Name = "description";
            metaDescription.Content = tmpSiteSetting.MetaDescription;
            Page.Header.Controls.Add(metaDescription);          
            //this.Page.Title = this.Page.Title.Replace(" - Comm100", "");

            if (SiteSession.DirectLoginUser != null)
            {
                achBoltAccount.HRef = WebUtility.GetAppSetting("BoltURL") + "/#/profile/" + SiteSession.DirectLoginUser.MongoUID;
            }

            if (SiteSession.CurrentUser != null)
            {
                ltrusername.Text = SiteSession.CurrentUser.UserName;
                liuname.Visible = true;
            }
            else
                liuname.Visible = false;

            //
            achAbout.HRef = WebUtility.GetAppSetting("AboutUs");
            achContact.HRef = WebUtility.GetAppSetting("ContactUs");
        }

        protected void lblogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect(WebUtility.GetAppSetting("BoltLoginURL"));
        }
        protected void lblogin_Click(object sender, EventArgs e)
        {
            Response.Redirect(WebUtility.GetAppSetting("BoltLoginURL"));
        }
        
    }
}
