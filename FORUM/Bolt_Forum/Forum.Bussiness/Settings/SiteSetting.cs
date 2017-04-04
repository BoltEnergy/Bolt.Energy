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
using System.Data;
using System.Data.SqlClient;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.DataAccess;
using System.Configuration;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class SiteSetting
    {
        protected SqlConnectionWithSiteId _siteConn;
        protected SqlTransaction _siteTransaction;
        protected SqlConnection _generalConn;
        protected SqlTransaction _generalTransaction;

        #region private fields
        private int _siteId;
        private string _siteName;
        private string _closeReason;

        /*---------------2.0----------------*/
        private int _pageSize;
        private Int16 _siteStatus;
        private string _metaKeywords;
        private string _metaDescription;



        #endregion

        #region property

        public string GetAppSetting(string keyName)
        {
            return (ConfigurationManager.AppSettings[keyName] != null ? Convert.ToString(ConfigurationManager.AppSettings[keyName]) : "");
        }

        public int SiteId
        {
            get { return this._siteId; }
        }
        public string SiteName
        {
            get
            {
                return GetAppSetting("ForumName");
                //    return System.Web.HttpUtility.HtmlDecode(this._siteName); 
            }
        }
        public string CloseReason
        {
            get { return this._closeReason; }
        }
        /*-----------------2.0------------------*/
        public int PageSize
        {
            get { return this._pageSize; }
        }
        public EnumSiteStatus SiteStatus
        {
            get { return (EnumSiteStatus)this._siteStatus; }
        }
        public string MetaKeywords
        {
            get { return this._metaKeywords; }
        }
        public string MetaDescription
        {
            get { return this._metaDescription; }
        }



        #endregion

        public SiteSetting(SqlConnectionWithSiteId siteConn, SqlTransaction siteTransaction, SqlConnection generalConn, SqlTransaction generalTransaction)
        {
            this._siteConn = siteConn;
            this._siteTransaction = siteTransaction;
            this._generalConn = generalConn;
            this._generalTransaction = generalTransaction;
            this._siteId = siteConn.SiteId;

            DataTable table = SettingsAccess.GetSiteSettingBySiteId(_siteConn, _siteTransaction, _siteId);
            if (table.Rows.Count <= 0)
            {
                ExceptionHelper.ThrowForumSiteSettingNotExist();
            }
            else
            {
                _siteName = Convert.ToString(table.Rows[0]["ForumName"].ToString());
                _metaKeywords = Convert.ToString(table.Rows[0]["MetaKeywords"]);
                _metaDescription = Convert.ToString(table.Rows[0]["MetaDescription"]);
                _pageSize = Convert.ToInt32(table.Rows[0]["PageSize"]);
                _siteStatus = Convert.ToInt16(table.Rows[0]["SiteStatus"]);
                _closeReason = Convert.ToString(table.Rows[0]["ForumCloseReason"]);

                if (_siteName == "")
                {
                    table = SiteAccess.GetSiteInfoBySiteId(_generalConn, _generalTransaction, _siteId);
                    if (table.Rows.Count <= 0)
                        ExceptionHelper.ThrowSiteNotExistException();
                    else
                        _siteName = Convert.ToString(table.Rows[0]["Name"].ToString());
                }

            }
        }


        private void CheckFieldsLength(string siteName, string closeReason, string metaKeywords, string metaDescription)
        {
            if (siteName.Length == 0)
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("Site Name");
            else if (siteName.Length > ForumDBFieldLength.SiteSetting_forumNameFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Site Name", ForumDBFieldLength.SiteSetting_forumNameFieldLength);

            if (closeReason.Length > ForumDBFieldLength.SiteSetting_closeReasonFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Close Reason", ForumDBFieldLength.SiteSetting_closeReasonFieldLength);

            if (metaKeywords.Length > ForumDBFieldLength.SiteSetting_metaKeywordsFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Meta Keywords", ForumDBFieldLength.SiteSetting_metaKeywordsFieldLength);

            if (metaDescription.Length > ForumDBFieldLength.SiteSetting_metaDescriptionFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Meta Description", ForumDBFieldLength.SiteSetting_metaDescriptionFieldLength);
        }

        private void CheckFieldsLength_colse(string siteName, string closeReason, string metaKeywords, string metaDescription)
        {
            if (closeReason.Length == 0)
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("Close Reason");

            CheckFieldsLength(siteName, closeReason, metaKeywords, metaDescription);
        }

        public virtual void Update(string siteName, string metaKeywords, string metaDescription, int pageSize, EnumSiteStatus siteStatus, string closeReason)
        {
            if (siteStatus == EnumSiteStatus.Close)
                CheckFieldsLength_colse(siteName, closeReason, metaKeywords, metaDescription);
            else
                CheckFieldsLength(siteName, closeReason, metaKeywords, metaDescription);
            SettingsAccess.UpdateSiteSettings(_siteConn, _siteTransaction, siteName, metaKeywords, metaDescription, pageSize, siteStatus, closeReason);
        }



    }
}
