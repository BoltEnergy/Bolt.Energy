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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;


namespace Com.Comm100.Forum.Bussiness
{
    public class BanIPWithPermissionCheck : BanIP
    {
        private UserOrOperator _operatingUserOrOperator;

        public BanIPWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, 
            int id, DateTime banStartDate, DateTime banEndDate, string note,int operatedUserOrOperatorId, Int64 banStartIP, Int64 banEndIP,
            bool ifDeleted, DateTime deleteDate)
            : base(conn, transaction, id,banStartDate, banEndDate, note,operatedUserOrOperatorId, banStartIP, banEndIP, ifDeleted, deleteDate)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public override void Delete()
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            base.Delete();
        }

        public override void Update<T>(T[] parameters, DateTime banStartDate, DateTime banEndDate, string note, int operatedUserOrOperatorId)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            BanBase.CheckFields(banStartDate, banEndDate, note);
            base.Update<T>(parameters, banStartDate, banEndDate, note, operatedUserOrOperatorId);
        }
    }
}
