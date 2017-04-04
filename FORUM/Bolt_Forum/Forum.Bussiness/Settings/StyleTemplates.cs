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

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class StyleTemplates
    {
        protected SqlConnection _conn;
        protected SqlTransaction _transaction;
        
        public StyleTemplates(SqlConnection conn, SqlTransaction transaction)
        {
            this._conn = conn;
            this._transaction = transaction;
        }

        public virtual StyleTemplateWithPermissionCheck[] GetAllStyleTemplateUrl(UserOrOperator operatingOperator)
        {          
            DataTable table = StyleTemplateAccess.GetAllStyleTemplates(_conn);
            StyleTemplateWithPermissionCheck[] styleTemplates = new StyleTemplateWithPermissionCheck[table.Rows.Count];
            
            for (int i = 0; i < table.Rows.Count; i++)
            {
                int id = Convert.ToInt32(table.Rows[i]["Id"]);
                string name = Convert.ToString(table.Rows[i]["Name"]);
                string templateUrl = Convert.ToString(table.Rows[i]["TemplateUrl"]);
                bool isDefault = Convert.ToBoolean(table.Rows[i]["IsDefault"]);
                string templateThumbnailURl = Convert.ToString(table.Rows[i]["TemplateThumbnailURl"]);
                styleTemplates[i] = new StyleTemplateWithPermissionCheck(_conn, _transaction,operatingOperator, id, name, templateUrl, isDefault, templateThumbnailURl);
            }
            return styleTemplates;
        }      
    }
}
