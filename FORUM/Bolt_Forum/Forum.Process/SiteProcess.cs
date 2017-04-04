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
using Com.Comm100.Framework.Common;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Enum;

namespace Com.Comm100.Forum.Process
{
    public class SiteProcess
    {
        public static SiteWithPermissionCheck GetSiteById(int operatingUserOrOperatorId,int siteId)
        {
            UserOrOperator operatingUserOrOperator = UserProcess.GetUserOrOpertorById(siteId,operatingUserOrOperatorId);
            SqlConnection conn = null;
            try
            {
                conn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(conn);
                return new SiteWithPermissionCheck(conn, null, siteId, operatingUserOrOperator);
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

        public static int GetSiteAppTypesById(int siteId)
        {
            SqlConnection conn = null;
            try
            {
                conn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(conn);

                SiteWithPermissionCheck tmpSite = new SiteWithPermissionCheck(conn, null, siteId, null);
                return tmpSite.ApplicationTypes;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }

        }

        public static void UpdateSiteLogo(int operatingUserOrOperatorId, int siteId, bool ifCustomizeLogo, byte[] CustomizeLogo)
        {
            UserOrOperator operatingUserOrOperator = UserProcess.GetUserOrOpertorById(siteId, operatingUserOrOperatorId);
            SqlConnection conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(conn);
                transaction = conn.BeginTransaction(System.Data.IsolationLevel.Serializable);

                SiteWithPermissionCheck tmpSite = new SiteWithPermissionCheck(conn, transaction, siteId, operatingUserOrOperator);
                tmpSite.UpdateSiteLogo(ifCustomizeLogo, CustomizeLogo);
                transaction.Commit();
            }
            catch (Exception)
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
