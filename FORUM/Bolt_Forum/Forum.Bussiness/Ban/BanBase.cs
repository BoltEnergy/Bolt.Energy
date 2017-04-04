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
using Com.Comm100.Framework.FieldLength;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class BanBase
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region protected fields
        protected int _id;
        protected DateTime _banStartDate;
        protected DateTime _banEndDate;
        protected string _note;
        protected int _operatedUserOrOperatorId;
        protected bool _ifDeleted;
        protected DateTime _deleteDate;
        #endregion

        #region properties
        public int Id
        {
            get { return this._id; }
        }
        public DateTime BanStartDate
        {
            get { return this._banStartDate; }
        }
        public DateTime BanEndDate
        {
            get { return this._banEndDate; }
        }
        public string Note
        {
            get { return this._note; }
        }
        public int OperatedUserOrOperatorId
        {
            get { return _operatedUserOrOperatorId; }
        }
        public abstract string UserOrIP { get; }
        public bool IfDeleted
        {
            get { return this._ifDeleted; }
        }
        public DateTime DeleteDate
        {
            get { return this._deleteDate; }
        }
        #endregion

        public BanBase(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            this._conn = conn;
            this._transaction = transaction;
        }

        protected static void CheckFields(DateTime startDate, DateTime endDate, string note)
        {
            if (note.Length > ForumDBFieldLength.Ban_noteFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Note",
                    ForumDBFieldLength.Ban_noteFieldLength);
            }
            if (startDate > endDate)
            {
                ExceptionHelper.ThrowPublicDateFromEarlierThanToDateException();
            }
        }

        protected static void CheckFields(DateTime startDate, DateTime endDate, string note, Int64 banStartIP, Int64 banEndIP)
        {
            CheckFields(startDate, endDate, note);
            if (banStartIP > banEndIP)
            {
                ExceptionHelper.ThrowForumBanStartIPCannotLargerThanEndIP();
            }
        }

        protected static void MakeSureUserOrOperatorExists(int userOrOperatorId)
        { }

        public static int Add(SqlConnectionWithSiteId conn, SqlTransaction transaction,  DateTime startDate, DateTime endDate, string note,int operatedUserOrOperatorId, Int64 banStartIP, Int64 banEndIP)
        {
            CheckFields(startDate, endDate, note, banStartIP, banEndIP);
            int banId =BanAccess.AddBan(conn, transaction, startDate, endDate, note, operatedUserOrOperatorId, banStartIP, banEndIP);
            /*2.0 stategy */
            UserOrOperator operatoredUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatedUserOrOperatorId);
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                conn, transaction, operatoredUserOrOperator, conn.SiteId);
            scoreStrategySetting.UseAfterBanUserOrOperator(operatoredUserOrOperator);
            /*2.0 reputation strategy*/
            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                conn, transaction, operatoredUserOrOperator, conn.SiteId);
            reputationStrategySetting.UseAfterBanUserOrOperator(operatoredUserOrOperator);
            return banId;
        }

        public static int Add(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            DateTime startDate, DateTime endDate, string note,int operatedUserOrOperatorId, int banUserOrOperatorId)
        {
            CheckFields(startDate, endDate, note);
            int banId = BanAccess.AddBan(conn, transaction, startDate, endDate, note, operatedUserOrOperatorId, banUserOrOperatorId);
            /*2.0 stategy */
            UserOrOperator operatoredUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, banUserOrOperatorId);
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                conn, transaction, null, conn.SiteId);
            scoreStrategySetting.UseAfterBanUserOrOperator(operatoredUserOrOperator);
            /*2.0 reputation strategy*/
            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                conn, transaction, operatoredUserOrOperator, conn.SiteId);
            reputationStrategySetting.UseAfterBanUserOrOperator(operatoredUserOrOperator);
            return banId;
        }

        public virtual void Delete()
        {
            if (this._ifDeleted) ExceptionHelper.ThrowForumBanNotExistException(_id);
            BanAccess.DeleteBan(_conn, _transaction, _id);
        }

        public abstract void Update<T>(T[] parameters, DateTime banStartDate, DateTime banEndDate, string note,int operatedUserOrOperatorId);
    }
}
