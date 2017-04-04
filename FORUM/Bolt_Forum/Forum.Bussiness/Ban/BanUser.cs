#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2009                                                                               //
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
    public abstract class BanUser : Ban
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

        public BanUser(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id, DateTime banStartDate, DateTime banEndDate, string note, int userOrOperatorId, string userOrOperatorName)
            : base(conn, transaction)
        { }

        private void MakeSureUserOrOpeartorExists(int userOrOperatorId)
        { }

        public IUserOrOperator GetBanUserOrOperator()
        {
            return null;
        }

        public override void Update<T>(T[] parameters, DateTime banStartDate, DateTime banEndDate, int[] categoryIds, int[] forumIds, string note)
        {
            throw new NotImplementedException();
        }
    }
}
