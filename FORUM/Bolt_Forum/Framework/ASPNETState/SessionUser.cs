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
using Com.Comm100.Framework.Enum;

namespace Com.Comm100.Framework.ASPNETState
{
    [Serializable]
    public class SessionUser
    {
        private int _userOrOperatorId;
        private string _userName;
        private int _siteId;
        private bool _ifOperator;
        private double _timezoneOffset;
        private EnumApplicationType _currentApplictionType;
        private bool _ifAdminLogin;
        private bool _ifModeratorLogin;
        private bool _ifForumAdministratorLogin;
        private Int64 _ip;
        private string _userid;

        public SessionUser(int userOrOperatorId, int siteId, bool ifOperator, double timezoneOffset, EnumApplicationType currentApplictionType, string userName = "")
        {
            this._siteId = siteId;
            this._userOrOperatorId = userOrOperatorId;
            this._ifOperator = ifOperator;
            this._timezoneOffset = timezoneOffset;
            this._currentApplictionType = currentApplictionType;
            this._userName = userName;
        }

        public int UserOrOperatorId
        {
            get { return this._userOrOperatorId; }
        }

        public string UserName
        {
            get { return this._userName; }
            set { _userName = value; }

        }

        public int SiteId
        {
            get { return this._siteId; }
        }

        public bool IfOperator
        {
            get { return this._ifOperator; }
        }

        public double TimezoneOffset
        {
            get { return this._timezoneOffset; }
        }

        public EnumApplicationType CurrentApplicationType
        {
            get { return this._currentApplictionType; }
        }

        public bool IfAdminLogin
        {
            get { return this._ifAdminLogin; }
            set { this._ifAdminLogin = value; }
        }

        public bool IfModeratorLogin
        {
            get { return this._ifModeratorLogin; }
            set { this._ifModeratorLogin = value; }
        }

        public bool IfForumAdministratorLogin
        {
            get { return this._ifForumAdministratorLogin; }
            set { this._ifForumAdministratorLogin = value; }
        }

        public Int64 IP
        {
            get { return this._ip; }
            set { this._ip = value; }
        }

        public string UserID
        {
            get { return this._userid; }
            set { _userid = value; }
        }
    }
}
