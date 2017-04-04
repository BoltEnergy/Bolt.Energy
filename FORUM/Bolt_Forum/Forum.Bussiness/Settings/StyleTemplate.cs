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
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class StyleTemplate
    {
        protected SqlConnection _conn;
        protected SqlTransaction _transaction;

        #region private fields
        private int _id;
        private string _name;
        private string _thumbnailPath;
        private string _path;
        private bool _isDefault;
        #endregion

        #region property
        public int Id
        {
            get { return this._id; }
        }
        public string Name
        {
            get { return this._name; }
        }
        public string ThumbnailPath
        {
            get { return this._thumbnailPath; }
        }
        public string Path
        {
            get { return this._path; }
        }
        #endregion

        public StyleTemplate(SqlConnection conn, SqlTransaction transaction, int id)
        {
            this._conn = conn;
            this._transaction = transaction;

            DataTable table = StyleTemplateAccess.GetStyleTemplateById(conn,transaction,id);
            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowStyleTemplateNotExistException(id);
            }
            else
            {
                _id = id;
                _name = Convert.ToString(table.Rows[0]["Name"]);
                _thumbnailPath=Convert.ToString(table.Rows[0]["TemplateThumbnailUrl"]);
                _path = Convert.ToString(table.Rows[0]["TemplateUrl"]);
                //_isDefault = Convert.ToBoolean(table.Rows[0]["IsDefault"]);
            }
        }
        public StyleTemplate(SqlConnection conn, SqlTransaction transaction, int id, string name, string templateUrl, bool isDefault, string templateThumbnailURl)
        {
            this._conn = conn;
            this._transaction = transaction;
            
            this._id = id;
            this._name = name;
            this._path = templateUrl;
            this._isDefault = isDefault;
            this._thumbnailPath = templateThumbnailURl;
        }

    }
}
