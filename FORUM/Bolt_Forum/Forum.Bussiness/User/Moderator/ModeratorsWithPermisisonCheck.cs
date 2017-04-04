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
    public class ModeratorsWithPermisisonCheck : Moderators
    {
        UserOrOperator _operatingUserOrOperator;

        public ModeratorsWithPermisisonCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction, forumId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public override int Add(int moderatorId)
        {
            CheckPermission();
            return base.Add(moderatorId);
        }
        public override void Delete(int moderatorId)
        {
            CheckPermission();
            base.Delete(moderatorId);
        }
        public void DeleteAllModerators()
        {
            CheckPermission();
            base.DeleteAllModerators(this._operatingUserOrOperator);
        }
        public ModeratorWithPermissionCheck[] GetAllModerators()
        {
            return base.GetAllModerators(_operatingUserOrOperator);
        }
        public override bool IfModerator(int userOrOperatorId)
        {
            return base.IfModerator(userOrOperatorId);
        }
        public ForumWithPermissionCheck GetForum()
        {
            return base.GetForum(_operatingUserOrOperator);
        }

        private void CheckPermission()
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
        }
    }
}
