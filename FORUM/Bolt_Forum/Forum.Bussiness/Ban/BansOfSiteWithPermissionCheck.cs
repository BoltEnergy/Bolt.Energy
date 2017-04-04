#if OPENSOURCE
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
    public class BansOfSiteWithPermissionCheck : BansOfSite
    {
        private UserOrOperator _operatingUserOrOperator;

        public BansOfSiteWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public override int Add(DateTime startDate, DateTime endDate, string note,int operatedUserOrOperatorId, int banUserOrOperatorId)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            return base.Add(startDate, endDate, note, operatedUserOrOperatorId, banUserOrOperatorId);
        }

        public int AddBanUserInUI(int forumId,DateTime startDate, DateTime endDate, string note, int operatedUserOrOperatorId, int banUserOrOperatorId)
        {
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            if (!CommFun.IfAdminInUI(_operatingUserOrOperator) &&
                !CommFun.IfModeratorInUI(_conn, _transaction, forumId, _operatingUserOrOperator))
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
            return base.Add(startDate, endDate, note, operatedUserOrOperatorId, banUserOrOperatorId);
        }

        public override int Add(DateTime startDate, DateTime endDate, string note,int operatedUserOrOperatorId, long banStartIP, long banEndIP)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            return base.Add(startDate, endDate, note, operatedUserOrOperatorId, banStartIP, banEndIP);
        }
    }
}
