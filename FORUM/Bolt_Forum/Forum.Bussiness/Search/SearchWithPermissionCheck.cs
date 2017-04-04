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
using Com.Comm100.Framework.Common;
using System.Web;

namespace Com.Comm100.Forum.Bussiness
{
    public class SearchWithPermissionCheck : Search
    {
        UserOrOperator _operatingUserOrOperator;

        public SearchWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, string subject1, string displayName,
             DateTime startTime, DateTime endTime, string status, bool ifSticky, bool ifAnswered,
             UserOrOperator operatingUserOrOperator)
            : base(conn, transaction, subject1, displayName,
              startTime, endTime, status, ifSticky, ifAnswered)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public SearchWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction,
           UserOrOperator operatingUserOrOperator, string keywords, bool ifAllForums, bool ifCategory,
           bool ifForum, int id, DateTime BeginDate, DateTime EndDate, int searchMethod)
            : base(conn, transaction, keywords, ifAllForums, ifCategory, ifForum, id, BeginDate, EndDate, searchMethod)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public SearchWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction,
          UserOrOperator operatingUserOrOperator):base(conn, transaction)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public TopicWithPermissionCheck[] GetTopicsByPagingAndSearchOptionsWithCategoryId(out int searchRusultCount, int pageIndex, int pageSize, int categoryId)
        {
            CommFun.UserPermissionCache().CheckIfAllowSearchPermission(_operatingUserOrOperator);
            CommFun.UserPermissionCache().CheckMinIntervalTimeforSearchingPermission(_operatingUserOrOperator);

            return base.GetTopicsByPagingAndSearchOptionsWithCategoryId(out searchRusultCount, pageIndex, pageSize, categoryId, this._operatingUserOrOperator);
        }

        public TopicWithPermissionCheck[] GetTopicsByPagingAndSearchOptionsWithForumId(out int searchRusultCount, int pageIndex, int pageSize, int forumId)
        {
            CommFun.UserPermissionCache().CheckIfAllowSearchPermission(_operatingUserOrOperator);
            CommFun.UserPermissionCache().CheckMinIntervalTimeforSearchingPermission(_operatingUserOrOperator);

            return base.GetTopicsByPagingAndSearchOptionsWithForumId(out searchRusultCount, pageIndex, pageSize, forumId, this._operatingUserOrOperator);
        }

        public TopicWithPermissionCheck[] GetTopicsByPagingAndSearchOptionsWithoutId(out int searchResultCount, int pageIndex, int pageSize)
        {
            CommFun.UserPermissionCache().CheckIfAllowSearchPermission(_operatingUserOrOperator);
            CommFun.UserPermissionCache().CheckMinIntervalTimeforSearchingPermission(_operatingUserOrOperator);

            return base.GetTopicsByPagingAndSearchOptionsWithoutId(out searchResultCount, pageIndex, pageSize, this._operatingUserOrOperator);
        }

        public void SearchResult()
        {
            if (CommFun.IfGuest())
            {
                CheckSearchPermissionWhenUserIsGuest(_operatingUserOrOperator, true);
                return;
            }
            else
            {
                CommFun.UserPermissionCache().CheckIfAllowSearchPermission(_operatingUserOrOperator);
                CommFun.UserPermissionCache().CheckMinIntervalTimeforSearchingPermission(_operatingUserOrOperator);
            }
            base.SearchResult(_operatingUserOrOperator);
        }

        public TopicWithPermissionCheck[] GetTopicsByPagingAndSearchOptions(
            out int searchResultCount, int pageIndex, int pageSize, UserOrOperator operatingOperator)
        {
            if (CommFun.IfGuest())
            {
                CheckSearchPermissionWhenUserIsGuest(operatingOperator,false);
            }
            else
            {
                CommFun.UserPermissionCache().CheckIfAllowSearchPermission(operatingOperator);
                //CommFun.UserPermissionCache().CheckMinIntervalTimeforSearchingPermission(operatingOperator);
            }
            return base.GetTopicsByPagingAndSearchOptions(out searchResultCount, pageIndex, pageSize, operatingOperator);
        }

        public virtual PostWithPermissionCheck[] GetPostsByPagingAndSearchOptions(
           out int searchResultCount, int pageIndex, int pageSize, UserOrOperator operatingOperator)
        {
            if (CommFun.IfGuest())
            {
                CheckSearchPermissionWhenUserIsGuest(operatingOperator,false);
            }
            else
            {
                CommFun.UserPermissionCache().CheckIfAllowSearchPermission(operatingOperator);
                //CommFun.UserPermissionCache().CheckMinIntervalTimeforSearchingPermission(operatingOperator);
            }
            return base.GetPostsByPagingAndSearchOptions(out searchResultCount, pageIndex, pageSize, operatingOperator);
        }

        public void CheckSearchPermissionWhenUserIsGuest(UserOrOperator operatingOperator,bool IfCheckMinIntervalTime)
        {
            GuestUserPermissionSettingWithPermissionCheck guestUser = new GuestUserPermissionSettingWithPermissionCheck(
                    _conn, _transaction, operatingOperator);
            /*Allow Search*/
            if (!guestUser.IfAllowGuestUserSearch)
                ExceptionHelper.ThrowForumUserWithoutPermissionAllowSearchException();
            /*Min Interval Time*/
            if (IfCheckMinIntervalTime)
            {
                DateTime lastSearchTime = CommFun.LastSearchtime();
                if (lastSearchTime == new DateTime())
                {
                    HttpContext.Current.Session["LastSearchTime"] = DateTime.UtcNow;
                    return;
                }
                if (DateTime.UtcNow - lastSearchTime < TimeSpan.FromSeconds(guestUser.GuestUserSearchInterval))
                    ExceptionHelper.ThrowForumUserWithoutPermissionMinIntervalTimeforSearchingException(
                        guestUser.GuestUserSearchInterval);
            }
        }

    }
}
