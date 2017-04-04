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
    public class SiteAccess
    {
        public static DataTable GetSiteInfoBySiteId(SqlConnection conn, SqlTransaction transaction, int siteId)
        {
            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("select * from t_Site where ID=@siteId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn, transaction);
            cmd.Parameters.AddWithValue("@siteId", siteId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static void UpdateSiteLogo(SqlConnection conn, SqlTransaction transaction, int siteId, bool ifCustomizeLogo, byte[] customizeLogo)
        {


            StringBuilder strSQL = new StringBuilder("Update t_Site set");
            strSQL.Append(" IfCustomizeLogo = @ifCustomizeLogo, CustomizeLogo = @customizeLogo");
            strSQL.Append(" where ID = @siteId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn, transaction);
            cmd.Parameters.AddWithValue("@ifCustomizeLogo", ifCustomizeLogo);
            cmd.Parameters.AddWithValue("@customizeLogo", customizeLogo);
            cmd.Parameters.AddWithValue("@siteId", siteId);

            cmd.ExecuteNonQuery();
        }
    }
}
