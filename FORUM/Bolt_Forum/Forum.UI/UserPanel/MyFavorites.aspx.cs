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
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Language;
using Com.Comm100.Forum.UI.Common;

namespace Com.Comm100.Forum.UI.UserPanel
{
    public partial class UserMyFavorites : UserBasePage
    {
        string ErrorLoad;
        string ErrorDelete;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Topic_FavoriteLoadError];
                ErrorDelete = Proxy[EnumText.enumForum_Topic_FavoriteDeleteError];
            }
            catch(Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IfError)
            {
                try
                {
                    SiteSetting tmpSiteSetting = SettingsProcess.GetSiteSettingBySiteId(this.SiteId, this.UserOrOperatorId);
                    Master.Page.Title = String.Format(Proxy[EnumText.enumForum_Public_UserPanelBrowserTitle], System.Web.HttpUtility.HtmlEncode(tmpSiteSetting.SiteName.ToString()));
                    CheckFavoritePermission();
                    if (!IsPostBack)
                    {
                        ((UserMasterPage)Master).SetMenu(EnumUserMenuType.MyFavorites); 
                        aspnetPager.PageIndex = 0;
                        this.RefreshData();
                    }

                }
                catch (Exception exp)
                {
                    this.IfError = true;
                    this.lblMessage.Text = ErrorLoad + exp.Message; //have problem about language!
                    LogHelper.WriteExceptionLog(exp);
                }
            }
        }
        protected void CheckFavoritePermission()
        {
            ForumFeatureWithPermissionCheck forumFeature = SettingsProcess.GetForumFeature(SiteId, UserOrOperatorId);
            if (!forumFeature.IfEnableFavorite)
            {
                ExceptionHelper.ThrowForumSettingsCloseFavoriteFunction();
            }
        }
        protected void RefreshData()
        {
            int recordCount;
            FavoriteWithPermissionCheck[] myFavorites = FavoriteProcess.GetFavoritesByUserIdAndPaging(this.SiteId, this.UserOrOperatorId, out recordCount, aspnetPager.PageIndex + 1, aspnetPager.PageSize);
            if (myFavorites.Length <= 0)
            {
                aspnetPager.PageIndex = 0;
                myFavorites = FavoriteProcess.GetFavoritesByUserIdAndPaging(this.SiteId, this.UserOrOperatorId, out recordCount, aspnetPager.PageIndex + 1, aspnetPager.PageSize);
            }
            aspnetPager.CWCDataBind(RepeaterMyFavorites, myFavorites, recordCount);
        }

        protected void aspnetPager_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
        {
            if (!IfError)
            {
                try
                {
                    //this.aspnetPager.PageIndex=0;
                    RefreshData();
                }
                catch (Exception exp)
                {
                    IfError = true;
                    lblMessage.Text = ErrorLoad + exp.Message;//have some questions!
                    LogHelper.WriteExceptionLog(exp);
                }
            }
        }

        protected void aspnetPager_ChangePageSize(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.ChangePageSizeEventArgs ce)
        {
            if (!IfError)
            {
                try
                {
                    RefreshData();
                }
                catch (Exception exp)
                {
                    IfError = true;
                    lblMessage.Text = Proxy[EnumText.enumForum_Topic_PageMyPostsErrorLoad] + exp.Message;
                    LogHelper.WriteExceptionLog(exp);
                }
            }
        }

        protected void RepeaterMyFavorites_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    try
                    {
                        string[] ids = e.CommandArgument.ToString().Split('_');
                        FavoriteProcess.DeleteFavorite(this.SiteId, this.UserOrOperatorId, Convert.ToInt32(ids[0]), Convert.ToInt32(ids[1])); //,Convert.ToInt32(e.CommandArgument)); 
                    }
                    catch(Exception exp)
                    {
                        lblMessage.Text = ErrorDelete + exp.Message;
                        LogHelper.WriteExceptionLog(exp);
                    }
                }
                RefreshData();

            }
            catch (Exception exp)
            {
                lblMessage.Text =ErrorLoad+ exp.Message;
                LogHelper.WriteExceptionLog(exp); 
            }
        }
        
        public bool TopicIfRead(int topicId)
        {
            bool ifRead = false;
            string strReadTopicId = "";
            strReadTopicId = Framework.Common.CommonFunctions.ReadCookies("ReadTopicId");

            string[] readTopicIds = strReadTopicId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < readTopicIds.Length; i++)
            {
                if (readTopicIds[i] == Convert.ToString(topicId))
                {
                    ifRead = true;
                    break;
                }
            }

            return ifRead;
        }
        public string ForumPath(int forumId,int topicOrAnnoucementId)
        {
            //return TopicProcess.GetTopicPath(
            //        this.UserOrOperatorId,
            //        SiteId, topicId);
            ForumWithPermissionCheck forum = ForumProcess.GetForumByForumId(SiteId, this.UserOrOperatorId, forumId);
            CategoryWithPermissionCheck category = CategoryProcess.GetCategoryByForumId(this.UserOrOperatorId, this.SiteId, forumId);
            //TopicBase topicOrAnnoucement = TopicProcess.GetTopicByTopicId(SiteId, this.UserOrOperatorId, topicOrAnnoucementId);

            return category.Name + "/" + forum.Name;// +"/" + topicOrAnnoucement.Subject;
            //return forum.Name + "/" + topicOrAnnoucement.Subject;
        }
        public string ImageStutas(int topicId, bool ifClosed,bool ifMarkedAsAnswer, bool ifParticpant)
        {
           //string img =  CommonFunctions.StatusImage(TopicIfRead(topicId),
           //     ifClosed,
           //     ifMarkedAsAnswer,
           //     ifParticpant);
            string imagePath = "../" + this.ImagePath;
            string img = WebUtility.StatusImage(imagePath,
                 TopicIfRead(topicId), ifClosed, ifMarkedAsAnswer, ifParticpant);
           //return img.Replace("Images/Status/", "../Images/Status/");
            return img;
        }
   
    }
}
