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

namespace Com.Comm100.Forum.Bussiness
{
    public class SiteSettingWithPermissionCheck : SiteSetting
    {
        UserOrOperator _operatingUserOrOperator;

        public SiteSettingWithPermissionCheck(SqlConnectionWithSiteId siteConn, SqlTransaction siteTransaction, SqlConnection generalConn, SqlTransaction generalTransaction,
             UserOrOperator operatingUserOrOperator)
            : base(siteConn, siteTransaction, generalConn, generalTransaction)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }


        public override void Update(string siteName, string metaKeywords, string metaDescription, int pageSize, EnumSiteStatus siteStatus, string closeReason)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            base.Update(siteName, metaKeywords, metaDescription, pageSize, siteStatus, closeReason);
        }


    }
}
