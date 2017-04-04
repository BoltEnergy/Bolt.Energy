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
    public class StyleSettingWithPermissionCheck : StyleSetting
    {
        UserOrOperator _operatingUserOrOperator;

        public StyleSettingWithPermissionCheck(SqlConnectionWithSiteId siteConn, SqlTransaction siteTransaction, SqlConnection generalConn, SqlTransaction generalTransaction, UserOrOperator operatingUserOrOperator)
            :base(siteConn, siteTransaction, generalConn, generalTransaction)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        //public StyleSettingWithPermissionCheck(SqlConnection conn, SqlTransaction transaction, int siteId, UserOrOperator operatingUserOrOperator, int styleTemplateId, bool ifAdvancedMode, string pageHeader, string pageFooter,
        //    bool ifCustomizeLogo, byte[] customizeLogo, string systemLogo)
        //    : base(conn, transaction, siteId, styleTemplateId, ifAdvancedMode, pageHeader, pageFooter, ifCustomizeLogo, customizeLogo, systemLogo)
        //{
        //    this._operatingUserOrOperator = operatingUserOrOperator;
        //}

        public void UpdateHeaderAndFooter(bool ifAdvancedMode, string pageHeader, string pageFooter)
        {
            CheckUpdatePermission();
            base.UpdateHeaderAndFooter(ifAdvancedMode, pageHeader, pageFooter, _operatingUserOrOperator);
        }

        public override void UpdateTemplate(int templateID)
        {
            CheckUpdatePermission();
            base.UpdateTemplate(templateID);
        }

        public StyleTemplateWithPermissionCheck GetStyleTemplate()
        {
            return base.GetStyleTemplate(_operatingUserOrOperator);
        }

        private void CheckUpdatePermission()
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
        }
    }
}
