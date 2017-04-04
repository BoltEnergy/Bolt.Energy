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
    public abstract class BanIP : BanBase
    {
        #region private fields
        private Int64 _banStartIP;
        private Int64 _banEndIP;
        #endregion

        #region properties
        public Int64 BanStartIP
        {
            get { return this._banStartIP; }
        }
        public Int64 BanEndIP
        {
            get { return this._banEndIP; }
        }
        public override string UserOrIP
        {
            get 
            { 
                string ip=string.Empty;
                if (_banStartIP == _banEndIP)
                    ip = IpHelper.LongIP2DottedIP(_banStartIP);
                else
                    ip = IpHelper.LongIP2DottedIP(_banStartIP) + "~" + IpHelper.LongIP2DottedIP(_banEndIP);
                return ip;
                //throw new NotImplementedException(); 
            }
        }
        #endregion

        public BanIP(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id, DateTime banStartDate, DateTime banEndDate, 
            string note, int operatedUserOrOperatorId, Int64 banStartIP, Int64 banEndIP, bool ifDeleted, DateTime deleteDate)
            : base(conn, transaction)
        {
            this._id = id;
            this._banStartDate = banStartDate;
            this._banEndDate = banEndDate;
            this._note = note;
            this._banStartIP = banStartIP;
            this._banEndIP = banEndIP;
            this._operatedUserOrOperatorId = operatedUserOrOperatorId;
            this._ifDeleted = ifDeleted;
            this._deleteDate = deleteDate;
        }

        public override void Update<T>(T[] parameters, DateTime banStartDate, DateTime banEndDate, string note, int operatedUserOrOperatorId)
        {
            if (this._ifDeleted) ExceptionHelper.ThrowForumBanNotExistException(_id);
            if (parameters.Length == 2)
            {
                long startIP = Convert.ToInt64(parameters[0]);
                long endIP = Convert.ToInt64(parameters[1]);
                BanBase.CheckFields(banStartDate, banEndDate, note, startIP, endIP);
                BanAccess.UpdateBan(_conn, _transaction, _id, banStartDate, banEndDate, note, operatedUserOrOperatorId, startIP, endIP);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
