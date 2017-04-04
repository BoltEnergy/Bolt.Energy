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
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Common;


namespace Com.Comm100.Forum.Process
{
    public class CategoryProcess
    {
        public static CategoryWithPermissionCheck[] GetAllCategories(int operatingUserOrOperatorId,bool ifOperator,int siteId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = null;
                if (operatingUserOrOperatorId > 0)
                    operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                CategoriesWithPermissionCheck categories = new CategoriesWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return categories.GetAllCategories();
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

        public static CategoryWithPermissionCheck[] GetNotClosedCategories(int operatingUserOrOperatorId, int siteId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = null;
                if (operatingUserOrOperatorId > 0)
                    operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                CategoriesWithPermissionCheck categories = new CategoriesWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return categories.GetNotClosedCategories();
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

        public static int AddCategory(int operatingUserOrOperatorId, bool ifOperator, int siteId, string name, string description,EnumCategoryStatus categoryStatus)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            name = name.Trim();
            description = description.Trim();

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                //transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                CategoriesWithPermissionCheck categories = new CategoriesWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                int categoryId = categories.Add(name, description,categoryStatus);
                //transaction.Commit();
                return categoryId;

            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        public static void UpdateCategory(int operatingUserOrOperatorId, bool ifOperator, int siteId, int id, string name, string description,EnumCategoryStatus categoryStatus)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            name = name.Trim();
            description = description.Trim();

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                //transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                CategoryWithPermissionCheck category = new CategoryWithPermissionCheck(conn, transaction, id, operatingUserOrOperator);
                category.Update(name, description,categoryStatus);
                //transaction.Commit();

            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        public static void DeleteCategory(int operatingUserOrOperatorId, bool ifOperator, int siteId, int id)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                //transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                CategoriesWithPermissionCheck categories = new CategoriesWithPermissionCheck(conn, transaction, operatingUserOrOperator);

                categories.Delete(id);

                //transaction.Commit();

            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        public static void SortCategories(int operatingUserOrOperatorId, bool ifOperator, int siteId, int id, EnumSortMoveDirection sortDirection)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                //transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                CategoriesWithPermissionCheck categories = new CategoriesWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                categories.Sort(id,sortDirection);
                //transaction.Commit();

            }
            catch (System.Exception)
            {
                //DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        public static CategoryWithPermissionCheck GetCategoryById(int operatingUserOrOperatorId, bool ifOperator, int siteId, int id)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                return new CategoryWithPermissionCheck(conn, transaction, id, operatingUserOrOperator);

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

        public static CategoryWithPermissionCheck GetCategoryByForumId(int operatingUserOrOperatorId, int siteId,
            int forumId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                ForumWithPermissionCheck forum = new ForumWithPermissionCheck(conn, transaction, forumId, operatingUserOrOperator);
                CategoryWithPermissionCheck category = new CategoryWithPermissionCheck(conn,transaction,
                    forum.CategoryId,operatingUserOrOperator);
                return category;
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
