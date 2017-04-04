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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.FieldLength;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class RegistrationSetting
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        private int _siteId;
        private bool _ifModerateNewUser;
        private bool _ifVerifyEmail;
        /*--------------2.0----------------*/
        private bool _ifAllowNewUser;
        private int _displayNameMinLength;
        private int _displayNameMaxLength;
        private string[] _illegalDisplayNames;
        private string _displayNameRegularExpression;
        private string _displayNameInstruction;
        private string _greetingMessage;
        private string _agreement;
        #endregion

        #region property
        public int SiteId
        {
            get { return this._siteId; }
        }
        public bool IfModerateNewUser
        {
            get { return this._ifModerateNewUser; }
        }
        public bool IfVerifyEmail
        {
            get { return this._ifVerifyEmail; }
        }
        /*-------------------2.0-------------------*/
        public bool IfAllowNewUser
        {
            get { return this._ifAllowNewUser; }
        }
        public int DisplayNameMinLength
        {
            get { return this._displayNameMinLength; }
        }
        public int DisplayNameMaxLength
        {
            get { return this._displayNameMaxLength; }
        }
        public string[] IllegalDisplayNames
        {
            get { return this._illegalDisplayNames; }
        }
        public string DisplayNameRegularExpression
        {
            get { return this._displayNameRegularExpression; }
        }
        public string DisplayNameInstruction
        {
            get { return this._displayNameInstruction; }
        }
        public string GreetingMessage
        {
            get { return this._greetingMessage; }
        }
        public string Agreement
        {
            get { return this._agreement; }
        }
        #endregion

        public RegistrationSetting(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            this._conn = conn;
            this._transaction = transaction;
            this._siteId = conn.SiteId;
            DataTable table = SettingsAccess.GetRegistrationSettingBySiteId(_conn, _transaction, _siteId);
            if (table.Rows.Count <= 0)
            {
                ExceptionHelper.ThrowForumRegistrationSettingNotExist();
            }
            else
            {
                _ifModerateNewUser = Convert.ToBoolean(table.Rows[0]["IfModerateNewUser"]);
                _ifVerifyEmail = Convert.ToBoolean(table.Rows[0]["IfVerifyEmail"]);
                _ifAllowNewUser = Convert.ToBoolean(table.Rows[0]["IfAllowNewUser"]);
                _displayNameMinLength = Convert.ToInt32(table.Rows[0]["DisplayNameMinLength"]);
                _displayNameMaxLength = Convert.ToInt32(table.Rows[0]["DisplayNameMaxLength"]);
                string illegalDisplayNames = Convert.ToString(table.Rows[0]["IllegalDisplayNames"]);
                if (!string.IsNullOrEmpty(illegalDisplayNames)) _illegalDisplayNames = illegalDisplayNames.Split(',');
                _displayNameRegularExpression = Convert.ToString(table.Rows[0]["DisplayNameRegularExpression"]);
                _displayNameInstruction = Convert.ToString(table.Rows[0]["DisplayNameRegularExpressionInstruction"]);
                _greetingMessage = Convert.ToString(table.Rows[0]["GreetingMessage"]);
                _agreement = Convert.ToString(table.Rows[0]["ForumUserAgreement"]);
            }
        }

        private void CheckFieldsLength(string illegalDisplayNames, string displayNameRegularExpression, string displayNameInstruction, string greetingMessage, string agreement)
        {
            if (illegalDisplayNames.Length > ForumDBFieldLength.RegistrationSetting_illegalDisplayNamesFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Not Allowed Display Names", ForumDBFieldLength.RegistrationSetting_illegalDisplayNamesFieldLength);
            if (displayNameRegularExpression.Length > ForumDBFieldLength.RegistrationSetting_displayNameRegularExpressionFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Display Name Regular Expression", ForumDBFieldLength.RegistrationSetting_displayNameRegularExpressionFieldLength);
            if (displayNameInstruction.Length > ForumDBFieldLength.RegistrationSetting_displayNameRegularExpressionInstructionLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Display Name Regular Expression Instruction", ForumDBFieldLength.RegistrationSetting_displayNameRegularExpressionInstructionLength);
            if (greetingMessage.Length > ForumDBFieldLength.RegistrationSetting_greetingMessageFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Greeting Message", ForumDBFieldLength.RegistrationSetting_greetingMessageFieldLength);
            if (agreement.Length > ForumDBFieldLength.RegistrationSetting_AgreementFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Agreement", ForumDBFieldLength.RegistrationSetting_AgreementFieldLength);
        }

        public virtual void Update(bool ifModerateNewUser, bool ifVerifyEmail, bool ifAllowNewUser, int displayNameMinLength, int displayNameMaxLength,
            string illegalDisplayNames, string displayNameRegularExpression, string displayNameInstruction, string greetingMessage, string agreement)
        {
            CheckFieldsLength(illegalDisplayNames, displayNameRegularExpression, displayNameInstruction, greetingMessage, agreement);
            SettingsAccess.UpdateRegistrationSetting(_conn, _transaction, _siteId, ifModerateNewUser, ifVerifyEmail, ifAllowNewUser, displayNameMinLength, displayNameMaxLength, illegalDisplayNames, displayNameRegularExpression, displayNameInstruction, greetingMessage, agreement);
        }
    }
}
