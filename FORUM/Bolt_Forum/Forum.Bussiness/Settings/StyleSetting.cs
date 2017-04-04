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

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class StyleSetting
    {
        protected SqlConnectionWithSiteId _siteConn;
        protected SqlTransaction _siteTransaction;

        protected SqlConnection _generalConn;
        protected SqlTransaction _generalTransaction;

        #region private fields
        private int _siteId;
        private int _styleTemplateId;
        private bool _ifAdvancedMode;
        private string _pageHeader;
        private string _pageFooter;
        private bool _ifCustomizeLogo;
        private byte[] _customizeLogo;
        private string _systemLogo;
        #endregion

        #region property
        public int SiteId
        {
            get { return this._siteId; }
        }
        public StyleTemplate StyleTemplate
        {
            get { return null;/*here instantiation a styleTemplate object*/ }
        }
        public bool IfAdvancedMode
        {
            get { return this._ifAdvancedMode; }
        }
        public string PageHeader
        {
            get { return this._pageHeader; }
        }
        public string PageFooter
        {
            get { return this._pageFooter; }
        }
        public bool IfCustomizeLogo
        {
            get { return this._ifCustomizeLogo; }
        }
        public byte[] CustomizeLogo
        {
            get { return this._customizeLogo; }
        }
        public string SystemLogo
        {
            get { return this._systemLogo; }
        }
        #endregion

        public StyleSetting(SqlConnectionWithSiteId siteConn, SqlTransaction siteTransaction, SqlConnection generalConn, SqlTransaction generalTransaction)
        {
            this._siteConn = siteConn;
            this._siteTransaction = siteTransaction;
            this._generalConn = generalConn;
            this._generalTransaction = generalTransaction;

            this._siteId = siteConn.SiteId;

            DataTable table = SettingsAccess.GetStyleSettingBySiteId(_siteConn, _siteTransaction, _siteId);

            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowForumStyleSettingNotExist();
            }
            else 
            {
                _styleTemplateId = Convert.ToInt32(table.Rows[0]["TemplateID"]);
                if (_styleTemplateId == 0)
                {
                    _styleTemplateId = 1;
                }
                _ifAdvancedMode = Convert.ToBoolean(table.Rows[0]["IfCustomizePage"]);
                _pageHeader = Convert.ToString(table.Rows[0]["PageHeader"]);
                _pageFooter = Convert.ToString(table.Rows[0]["PageFooter"]);
                
            }

            table = SettingsAccess.GetCustomLogoInfoBySiteId(_generalConn, _generalTransaction, _siteId);
            if (table.Rows.Count <= 0)
            {
                ExceptionHelper.ThrowSiteNotExistException();
            }
            else
            {
                _ifCustomizeLogo = Convert.ToBoolean(table.Rows[0]["IfCustomizeLogo"]);

                if (_ifCustomizeLogo)
                    _customizeLogo = table.Rows[0]["CustomizeLogo"] is System.DBNull ? null : (byte[])(table.Rows[0]["CustomizeLogo"]);
                else
                    _customizeLogo = null;
            }
        }

        //public StyleSetting(SqlConnection conn, SqlTransaction transaction, int siteId, int styleTemplateId, bool ifAdvancedMode, string pageHeader, string pageFooter,
        //    bool ifCustomizeLogo, byte[] customizeLogo, string systemLogo)
        //{
        //    this._conn = conn;
        //    this._transaction = transaction;
        //    this._siteId = siteId;
        //    this._styleTemplateId = styleTemplateId;
        //    this._ifAdvancedMode = ifAdvancedMode;
        //    this._pageHeader = pageHeader;
        //    this._pageFooter = pageFooter;
        //    this._ifCustomizeLogo = ifCustomizeLogo;
        //    this._customizeLogo = customizeLogo;
        //    this._systemLogo = systemLogo;
        //}

        protected void UpdateHeaderAndFooter(bool ifAdvancedMode, string pageHeader, string pageFooter, UserOrOperator operatingUserOrOperator)
        {
            if(ifAdvancedMode)
                CheckFieldsLength(pageHeader, pageFooter);
            //Upload Images
            PostImagesWithPermissionCheck postImages = new PostImagesWithPermissionCheck(_siteConn, null, -1, (int)Com.Comm100.Framework.Enum.Forum.EnumForumPostImageUseType.PageHeaderAndFooter, operatingUserOrOperator);
            int[] imageIDs = Com.Comm100.Framework.Common.CommonFunctions.GetPostContentImageIds(pageHeader + " " + pageFooter);
            postImages.AttachToPost(imageIDs);

            SettingsAccess.UpdateStyleSetting(_siteConn, _siteTransaction, _siteId, ifAdvancedMode, pageHeader, pageFooter); 
        }

        public virtual void UpdateTemplate(int templateID)
        {
            SettingsAccess.UpdateTemplateID(this._siteConn, this._siteTransaction, _siteId, templateID);
        }

        protected StyleTemplateWithPermissionCheck GetStyleTemplate(UserOrOperator operatingOperator)
        {
            return new StyleTemplateWithPermissionCheck(this._generalConn, this._generalTransaction, this._styleTemplateId, operatingOperator);
        }

        private void CheckFieldsLength(string pageHeader, string pageFooter)
        {
            if (pageHeader.Length > ForumDBFieldLength.Styles_pageHeaderFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("PageHeader", ForumDBFieldLength.Styles_pageHeaderFieldLength);
            
            if (pageFooter.Length > ForumDBFieldLength.Styles_pageFooterFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("PageFooter", ForumDBFieldLength.Styles_pageFooterFieldLength);
        }

    }
}
