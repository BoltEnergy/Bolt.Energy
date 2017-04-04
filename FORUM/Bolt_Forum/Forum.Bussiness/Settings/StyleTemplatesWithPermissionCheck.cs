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

namespace Com.Comm100.Forum.Bussiness
{
    public class StyleTemplatesWithPermissionCheck : StyleTemplates
    {
        UserOrOperator _operatingUserOrOperator;

        public StyleTemplatesWithPermissionCheck(SqlConnection conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
            :base(conn, transaction)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        //public StyleTemplatesWithPermissionCheck()
        //    :base(null,null)
        //{ 
            
        //}

        public override StyleTemplateWithPermissionCheck[] GetAllStyleTemplateUrl(UserOrOperator operatingUserOrOperator)
        {
            return base.GetAllStyleTemplateUrl(operatingUserOrOperator);
        }
    }
}
