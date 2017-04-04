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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Database;

namespace Com.Comm100.Forum.DataAccess
{
    public class StyleTemplateAccess
    {
        public static DataTable GetAllStyleTemplates(SqlConnection conn)
        {   

            StringBuilder strSQL = new StringBuilder("select *");
            strSQL.Append(" from t_TemplateURL");
            strSQL.Append(" order by Id asc");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn);


            DataTable table = new DataTable();            
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetStyleTemplateById(SqlConnection conn, SqlTransaction transaction, int siteID)
        {

            StringBuilder strSQL = new StringBuilder("select Name, TemplateThumbnailUrl, TemplateUrl");
            strSQL.Append(" from t_TemplateURL");
            strSQL.Append(" where Id=@Id");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn, transaction);
            cmd.Parameters.AddWithValue("@Id", siteID);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;                    
        }

    }
}
