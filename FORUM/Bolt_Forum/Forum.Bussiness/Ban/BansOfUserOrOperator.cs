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
    public abstract class BansOfUserOrOperator : Bans
    {
        private int _userOrOperatorId;

        public int UserOrOperatorId
        {
            get { return this._userOrOperatorId; }
        }

        public BansOfUserOrOperator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
            : base(conn, transaction)
        {
            _userOrOperatorId = userOrOperatorId;
        }

        protected BanUserOrOperatorWithPermissionCheck[] GetAllBans(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = new DataTable();
            table = BanAccess.GetBansOfUserOrOperator(_conn, _transaction, _userOrOperatorId);
            List<BanUserOrOperatorWithPermissionCheck> bans = new List<BanUserOrOperatorWithPermissionCheck>();
            foreach (DataRow dr in table.Rows)
            {
                BanUserOrOperatorWithPermissionCheck banUser = (BanUserOrOperatorWithPermissionCheck)this.CreateBanObject(dr, operatingUserOrOperator);
                bans.Add(banUser);
            }
            return bans.ToArray<BanUserOrOperatorWithPermissionCheck>();
        }

        protected BanUserOrOperatorWithPermissionCheck[] GetAllNotDeletedBans(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = new DataTable();
            table = BanAccess.GetNotDeletedBansOfUserOrOperator(_conn, _transaction, _userOrOperatorId);
            List<BanUserOrOperatorWithPermissionCheck> bans = new List<BanUserOrOperatorWithPermissionCheck>();
            foreach (DataRow dr in table.Rows)
            {
                BanUserOrOperatorWithPermissionCheck banUser = (BanUserOrOperatorWithPermissionCheck)this.CreateBanObject(dr, operatingUserOrOperator);
                bans.Add(banUser);
            }
            return bans.ToArray<BanUserOrOperatorWithPermissionCheck>();
        }

        public void DeleteAll()
        {
            BanAccess.DeleteBansByUserOrOperatorId(_conn, _transaction, _userOrOperatorId);

            /*2.0 stategy */
            UserOrOperator operatoredUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                _conn, _transaction, null, _userOrOperatorId);
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                _conn, _transaction, null, _conn.SiteId);
            scoreStrategySetting.UserAfterUnbanUserOrOperator(operatoredUserOrOperator);
            /*2.0 reputation strategy*/
            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                _conn, _transaction, null, _conn.SiteId);
            reputationStrategySetting.UserAfterUnbanUserOrOperator(operatoredUserOrOperator);
        }

        public void DeleteAll(int forumId,UserOrOperator operatingUserOrOperator)
        {
            CommFun.CheckCommonPermissionInUI(operatingUserOrOperator);
            if(!CommFun.IfAdminInUI(operatingUserOrOperator) &&
               !CommFun.IfModeratorInUI(_conn,_transaction,forumId,operatingUserOrOperator))
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(operatingUserOrOperator.DisplayName);
            BanAccess.DeleteBansByUserOrOperatorId(_conn, _transaction, _userOrOperatorId);
            /*2.0 stategy */
            UserOrOperator operatoredUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(
                _conn, _transaction, null, _userOrOperatorId);
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            scoreStrategySetting.UserAfterUnbanUserOrOperator(operatoredUserOrOperator);
            /*2.0 reputation strategy*/
            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            reputationStrategySetting.UserAfterUnbanUserOrOperator(operatoredUserOrOperator);
        }
    }
}
