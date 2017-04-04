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
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.Process
{
   public  class FavoriteProcess
    {
        public static FavoriteWithPermissionCheck[] GetFavoritesByUserIdAndPaging(int siteId, int operatingUserOrOperatorId,out int count, int pageIndex, int pageSize)
        {
            SqlConnectionWithSiteId conn = null;                   
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                FavoritesWithPermissionCheck favorites = new FavoritesWithPermissionCheck(conn, null, operatingUserOrOperator, operatingUserOrOperatorId); //operatingUserOrOperator.GetFavorites();
                return  favorites.GetFavoritesByPaging(out count, pageIndex, pageSize);
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
        public static void DeleteFavorite(int siteId, int operatingUserOrOperatorId,int forumId, int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                FavoritesWithPermissionCheck favorites = new FavoritesWithPermissionCheck(conn, transaction, operatingUserOrOperator, operatingUserOrOperatorId);
                favorites.Delete(operatingUserOrOperator,forumId, topicId);
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
        public static void AddFavorite(int siteId, int operatingUserOrOperatorId,int forumId, int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                FavoritesWithPermissionCheck favorites = new FavoritesWithPermissionCheck(conn, transaction, operatingUserOrOperator, operatingUserOrOperatorId);
                favorites.Add(operatingUserOrOperator,forumId ,topicId);
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
        public static bool IfUserFavoriteTopic(int siteId, int operatingUserOrOperatorId, int topicId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn, transaction, topicId, operatingUserOrOperator);
                return topic.IfFavorite(operatingUserOrOperator, operatingUserOrOperatorId);
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
