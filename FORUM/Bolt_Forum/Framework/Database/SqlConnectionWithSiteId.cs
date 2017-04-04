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
using System.Data.SqlClient;
using System.Diagnostics;

namespace Com.Comm100.Framework.Database
{
    public class SqlConnectionWithSiteId
    {
        private int _siteId;
        private SqlConnection _sqlConn;

        public SqlConnectionWithSiteId(int siteId, SqlConnection sqlConn)
        {
            _siteId = siteId;
            Debug.Assert(sqlConn != null);

            _sqlConn = sqlConn;
        }


        public int SiteId
        {
            get { return _siteId; }
        }

        public SqlConnection SqlConn
        {
            get { return _sqlConn; }
        }
    }
}
