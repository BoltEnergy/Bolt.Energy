﻿#if OPENSOURCE
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

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class FavoritesOfSite
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        public FavoritesOfSite(SqlConnectionWithSiteId conn, SqlTransaction transacion)
        {
            _conn = conn;
            _transaction = transacion;
        }

        public int GetCountByTopicId(int topicId)
        {
            return FavoriteAccess.GetCountOfFavoritesByTopicId(_conn, _transaction, topicId);
        }
    }
}
