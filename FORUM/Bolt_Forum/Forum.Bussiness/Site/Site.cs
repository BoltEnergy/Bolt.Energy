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
    public abstract class Site
    {
        protected SqlConnection _conn;
        protected SqlTransaction _transaction;

        #region Private Fields
        protected int _id;
        protected string _name;
        protected string _email;
        protected Boolean _ifCustomizeLogo;
        protected byte[] _customizeLogo;
        protected int _applicationTypes;

        #endregion Private Fields

        #region Public Properties
        public int Id
        {
            get { return this._id; }
        }
        public string Name
        {
            get { return this._name; }
        }
        public string Email
        {
            get { return this._email; }
        }
        public Boolean IfCustomizeLogo
        {
            get { return this._ifCustomizeLogo; }
        }
        public byte[] CustomizeLogo
        {
            get { return this._customizeLogo; }
        }
        public int ApplicationTypes
        {
            get { return this._applicationTypes; }
        }
        #endregion Public Properties

        public Site(SqlConnection conn, SqlTransaction transaction, int siteId)
        {
            _conn = conn;
            _transaction = transaction;

            DataTable table = SiteAccess.GetSiteInfoBySiteId(conn, transaction, siteId);

            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowSiteNotExistException();
            }
            else
            {
                this._id = siteId;
                this._name = Convert.ToString(table.Rows[0]["Name"]);
                this._ifCustomizeLogo = Convert.ToBoolean(table.Rows[0]["IfCustomizeLogo"]);
                if (this._ifCustomizeLogo)
                    this._customizeLogo = table.Rows[0]["CustomizeLogo"] is System.DBNull ? null : (byte[])(table.Rows[0]["CustomizeLogo"]);
                else
                    this._customizeLogo = null;
                this._email = Convert.ToString(table.Rows[0]["Email"]);
                //this._applicationTypes = Convert.ToInt32(table.Rows[0]["ApplicationTypes"]);
            }
        }

        public virtual void UpdateSiteLogo(bool ifCustomizeLogo, byte[] customizeLogo)
        {
            SiteAccess.UpdateSiteLogo(_conn, _transaction, _id, ifCustomizeLogo, customizeLogo);
        }
    }
}
