#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2009                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public class UserOrOperatorMessagePermission : UserOrOperatorPermissionBase, IMessagePermission
    {
        #region private fields
        private bool _ifEnableSendMessage;
        private int _maxCountOfMessageSendOneDay;
        #endregion 

        public bool IfEnableSendMessage
        {
            get { return this._ifEnableSendMessage; }
        }

        public int MaxCountOfMessageSendOneDay
        {
            get { return this._maxCountOfMessageSendOneDay; }
        }

        public UserOrOperatorMessagePermission(SqlConnection generalConn, SqlTransaction generalTransaction, SqlConnectionWithSiteId siteConn, SqlTransaction siteTransaction, UserOrOperator userOrOperator)
            : base(generalConn, generalTransaction, siteConn, siteTransaction, userOrOperator)
        { }

        public void CheckPermission()
        { }
    }
}
