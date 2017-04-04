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

namespace Com.Comm100.Forum.Bussiness
{
    public class StyleTemplateWithPermissionCheck : StyleTemplate
    {
        UserOrOperator _operatingUserOrOperator;

        public StyleTemplateWithPermissionCheck(SqlConnection conn, SqlTransaction transaction, int id, UserOrOperator operatingUserOrOperator)
            :base(conn, transaction, id)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public StyleTemplateWithPermissionCheck(SqlConnection conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int id, string name, string templateUrl, bool isDefault, string templateThumbnailURl)
            :base(conn,transaction,id,name,templateUrl,isDefault,templateThumbnailURl)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        //public void CheckupdateTemplatePermission()
        //{
        //    if (CommFun.IfOperator(_operatingUserOrOperator))
        //    {
        //        if (!((OperatorWithPermissionCheck)_operatingUserOrOperator).IfAdmin)
        //            ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        //    }
        //}
    }
}
