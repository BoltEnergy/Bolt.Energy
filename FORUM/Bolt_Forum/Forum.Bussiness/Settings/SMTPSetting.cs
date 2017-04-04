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
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class SMTPSetting
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        private int _siteId;
        private string _smtpServer;
        private int _smtpPort;
        private bool _ifAuthentication;
        private string _smtpUserName;
        private string _smtpPassword;
        private string _fromEmailAddress;
        private string _fromName;
        private bool _ifSSL;
        #endregion

        #region properties
        public int SiteId
        {
            get { return this._siteId; }
        }
        public string SMTPServer
        {
            get { return this._smtpServer; }
        }
        public int SMTPPort
        {
            get { return this._smtpPort; }
        }
        public bool IfAuthentication
        {
            get { return this._ifAuthentication; }
        }
        public string SMTPUserName
        {
            get { return this._smtpUserName; }
        }
        public string SMTPPassword
        {
            get { return this._smtpPassword; }
        }
        public string FromEmailAddress
        {
            get { return this._fromEmailAddress; }
        }
        public string FromName
        {
            get { return this._fromName; }
        }
        public bool IfSSL
        {
            get { return this._ifSSL; }
        }
        #endregion

        public SMTPSetting(SqlConnectionWithSiteId conn, SqlTransaction transaction, int siteId)
        {
            _conn = conn;
            _transaction = transaction;
            DataTable table = ConfigAccess.GetSMTPSettings(conn, transaction);
            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowForumSMTPSettingsNotExist();
            }
            else
            {
                _siteId = conn.SiteId;
                _smtpServer = Convert.ToString(table.Rows[0]["ForumSMTPServer"]);
                _smtpPort = Convert.ToInt32(table.Rows[0]["ForumSMTPPort"]);
                _ifAuthentication = Convert.ToBoolean(table.Rows[0]["IfForumSMTPAuthentication"]);
                _smtpUserName = Convert.ToString(table.Rows[0]["ForumSMTPUserName"]);
                _smtpPassword = Convert.ToString(table.Rows[0]["ForumSMTPPassword"]);
                _fromEmailAddress = Convert.ToString(table.Rows[0]["ForumSMTPFromEmailAddress"]);
                _fromName = Convert.ToString(table.Rows[0]["ForumSMTPFromName"]);
                _ifSSL = Convert.ToBoolean(table.Rows[0]["IfForumSMTPSSL"]);
            }
        }

        private void CheckAuthentication(bool ifAuthentication, string smtpUserName, string smtpPassword)
        {
            if (ifAuthentication == true && (smtpUserName == "" || smtpPassword == ""))
            {
                ExceptionHelper.ThrowForumSMTPAuthenticationRequiredUserNameAndPassword();
            }
        }

        public virtual void Update(string smtpServer, int smtpPort, bool ifAuthentication, string smtpUserName, string smtpPassword,
                string fromEmailAddress, string fromName, bool ifSSL)
        {
            CheckAuthentication(ifAuthentication, smtpUserName, smtpPassword);
            ConfigAccess.UpdateSMTPSettings(_conn, _transaction, smtpServer, smtpPort, ifAuthentication, smtpUserName, smtpPassword, fromEmailAddress, fromName, ifSSL);
        }
    }
}
