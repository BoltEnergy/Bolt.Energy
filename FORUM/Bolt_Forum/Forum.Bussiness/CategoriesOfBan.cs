#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2009                                                                               //
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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class CategoriesOfBan
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        private int _banId;

        public CategoriesOfBan(SqlConnectionWithSiteId conn, SqlTransaction transaction, int banId)
        { }

        public virtual CategoryWithPermissionCheck[] GetAllCategories(IUserOrOperator operatingOperator)
        {
            return null;
        }

        private void MakeSureCategoryExists(int categoryId)
        { }

        public virtual int AddCategoryOfBan(int categoryId)
        {
            return 0;
        }

        public virtual void DeleteCategoryOfBan(int categoryId)
        {
 
        }
    }
}
