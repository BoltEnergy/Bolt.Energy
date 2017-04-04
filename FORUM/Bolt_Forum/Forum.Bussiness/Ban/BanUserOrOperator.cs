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
    public abstract class BanUserOrOperator : BanBase
    {
        #region private fields
        private int _userOrOperatorId;
        private string _userOrOperatorName;
        #endregion

        #region properties
        public int UserOrOperatorId
        {
            get { return this._userOrOperatorId; }
        }
        public override string  UserOrIP
        {
            get { return this._userOrOperatorName; }
        }
        #endregion

        public BanUserOrOperator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id)
            : base(conn, transaction)
        {
            DataTable table = BanAccess.GetBanById(conn, transaction, id);

            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowForumBanNotExistException(id);
            }
            else
            {
                this._id = id;
                this._banStartDate = Convert.ToDateTime(table.Rows[0]["StartDate"]);
                this._banEndDate = Convert.ToDateTime(table.Rows[0]["EndDate"]);
                this._note=Convert.ToString(table.Rows[0]["Note"]);
                this._operatedUserOrOperatorId = Convert.ToInt32(table.Rows[0]["OperatedUserOrOperatorId"]);
                this._userOrOperatorId = Convert.ToInt32(table.Rows[0]["UserOrOperatorId"]);
                this._userOrOperatorName = Convert.ToString(table.Rows[0]["Name"]);
                this._ifDeleted = Convert.ToBoolean(table.Rows[0]["IfDeleted"]);
                this._deleteDate = Convert.ToDateTime(table.Rows[0]["DeleteDate"]);
            }
        }

        public BanUserOrOperator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id, 
            DateTime banStartDate, DateTime banEndDate, string note,int operatedUserOrOperatorId, 
            int userOrOperatorId, string userOrOperatorName, bool ifDeleted, DateTime deleteDate)
            : base(conn, transaction)
        {
            this._id = id;
            this._banStartDate = banStartDate;
            this._banEndDate = banEndDate;
            this._note = note;
            this._operatedUserOrOperatorId = operatedUserOrOperatorId;
            this._userOrOperatorId = userOrOperatorId;
            this._userOrOperatorName = userOrOperatorName;
            this._ifDeleted = ifDeleted;
            this._deleteDate = deleteDate;
        }

        private void MakeSureUserOrOpeartorExists(int userOrOperatorId)
        {
            UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, null, userOrOperatorId);
            if (userOrOperator == null)
                ExceptionHelper.ThrowForumUserOrOperatorNotActiveWithIdException(userOrOperatorId);
            else if(userOrOperator.IfDeleted)
                ExceptionHelper.ThrowForumUserOrOperatorNotActiveWithIdException(userOrOperatorId);
        }

        protected UserOrOperator GetBanUserOrOperator(UserOrOperator operatingUserOrOperator)
        {
            return UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, operatingUserOrOperator, _userOrOperatorId);
        }

        public override void Delete()
        {
            base.Delete();
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

        public override void Update<T>(T[] parameters, DateTime banStartDate, DateTime banEndDate, string note, int operatedUserOrOperatorId)
        {
            if (this._ifDeleted) ExceptionHelper.ThrowForumBanNotExistException(_id);
            if (parameters.Length == 1)
            {
                int  banUserOrOperatorId = Convert.ToInt32(parameters[0]);
                MakeSureUserOrOperatorExists(banUserOrOperatorId);
                BanAccess.UpdateBan(_conn, _transaction, _id, banStartDate, banEndDate, note, operatedUserOrOperatorId, banUserOrOperatorId);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
