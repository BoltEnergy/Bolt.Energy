
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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.UI.UserControl;
using Com.Comm100.Forum.Language;
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using System.Web.UI.HtmlControls;

namespace Com.Comm100.Forum.UI.UserPanel
{
    public partial class UserMasterPage : System.Web.UI.MasterPage
    {
        public string ImagePath
        {
            get {
                return ((UIBasePage)this.Page).ImagePath;
            }
        }
        public int SiteId
        {
            get
            {
                return ((UIBasePage)this.Page).SiteId;
            }
        }

        public LanguageProxy Proxy
        {
            get { return ((Com.Comm100.Forum.UI.UIBasePage)this.Page).Proxy; }
        }

        public ForumHeader forumHeader
        {
            get { return this.ForumHeader1; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(SiteId, 0);
                if (!forumFeature.IfEnableFavorite)
                    panelFavorite.Visible = false;
                
                if (!forumFeature.IfEnableMessage)
                    panelMessage.Visible = false;

                SiteSettingWithPermissionCheck tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, 0);
                HtmlMeta metaKeywords = new HtmlMeta();
                metaKeywords.Name = "Keywords";
                metaKeywords.Content = tmpSiteSetting.MetaKeywords;
                Page.Header.Controls.Add(metaKeywords);
                HtmlMeta metaDescription = new HtmlMeta();
                metaDescription.Name = "Description";
                metaDescription.Content = tmpSiteSetting.MetaDescription;
                Page.Header.Controls.Add(metaDescription);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SetMenu(EnumUserMenuType enumMenuType)
        {
            switch (enumMenuType)
            {
                case EnumUserMenuType.Profile:
                    itemProfile.Attributes["class"] = "tdUserPanelHighLight";
                    break;
                case EnumUserMenuType.Signature:
                    itemSignature.Attributes["class"] = "tdUserPanelHighLight";
                    break;
                case EnumUserMenuType.Avatar:
                    itemAvatar.Attributes["class"] = "tdUserPanelHighLight";
                    break;
                case EnumUserMenuType.Password:
                    itemPassword.Attributes["class"] = "tdUserPanelHighLight";
                    break;
                //case EnumUserMenuType.MyTopics:
                //    itemMyTopics.Attributes["class"] = "tdUserPanelHighLight";
                //    break;
                case EnumUserMenuType.MyPosts:
                    itemMyPosts.Attributes["class"] = "tdUserPanelHighLight";
                    break;
                case EnumUserMenuType.MyFavorites:
                    itemMyFavorites.Attributes["class"] = "tdUserPanelHighLight";
                    break;
              
                case EnumUserMenuType.SendMessage:
                    itemSendMessage.Attributes["class"] = "tdUserPanelHighLight";
                    break;
                case EnumUserMenuType.InBox:
                    itemInbox.Attributes["class"] = "tdUserPanelHighLight";
                    break;
                case EnumUserMenuType.OutBox:
                    itemOutbox.Attributes["class"] = "tdUserPanelHighLight";
                    break;
            }
        }
    }
}
