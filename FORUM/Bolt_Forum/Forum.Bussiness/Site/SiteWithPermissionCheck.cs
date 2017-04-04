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
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public class SiteWithPermissionCheck : Site
    {
        UserOrOperator _operatingUserOrOperator;

        public SiteWithPermissionCheck(SqlConnection conn, SqlTransaction transaction, int id, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction, id)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public override void UpdateSiteLogo(bool ifCustomizeLogo, byte[] customizeLogo)
        {
            CheckUpdateSiteLogoPermission();
            base.UpdateSiteLogo(ifCustomizeLogo, customizeLogo);
        }

        private void CheckUpdateSiteLogoPermission()
        {
            if (!_operatingUserOrOperator.IfForumAdmin)
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        }
    }
}
