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
using Com.Comm100.Framework.Database;
using System.Data.SqlClient;
using Com.Comm100.Forum.Bussiness;

namespace Com.Comm100.Forum.Process
{
    public class SearchProcess
    {
        public static void Search(int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUsrOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                    conn, null, null, operatingUserOrOperatorId);
                SearchWithPermissionCheck search = new SearchWithPermissionCheck(
                    conn, transaction, operatingUsrOrOperator);

                search.SearchResult();
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
    }
}
