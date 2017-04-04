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
using System.Text.RegularExpressions;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class ProhibitedWordsSetting
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        private int _siteId;
        private bool _ifEnabledProhibitedWords;
        private string _characterToReplaceProhibitedWord;
        private string[] _prohibitedWords;
        private string _originalProhibitedWords;
        #endregion

        #region properties
        public int SiteId
        {
            get { return this._siteId; }
        }
        public bool IfEnabledProhibitedWords
        {
            get { return this._ifEnabledProhibitedWords; }
        }
        public string CharacterToReplaceProhibitedWord
        {
            get { return this._characterToReplaceProhibitedWord; }
        }
        public string[] ProhibitedWords
        {
            get { return this._prohibitedWords; }
        }
        public string OriginalProhibitedWords
        {
            get { return this._originalProhibitedWords; }
        }
        #endregion

        public ProhibitedWordsSetting(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            _conn = conn;
            _transaction = transaction;
            _siteId = conn.SiteId;
            DataTable table = ConfigAccess.GetProhibitedWords(conn, transaction);
            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowForumProhibitedWordsSettingNotExist();
            }
            else
            {
                _ifEnabledProhibitedWords = Convert.ToBoolean(table.Rows[0]["IfEnabledProhibitedWords"]);
                _characterToReplaceProhibitedWord = Convert.ToString(table.Rows[0]["CharacterToReplaceProhibitedWord"]);
                _prohibitedWords = SplitProhibitedWords(Convert.ToString(table.Rows[0]["ProhibitedWords"]));
                _originalProhibitedWords = Convert.ToString(table.Rows[0]["ProhibitedWords"]);
            }
        }

        private void CheckFields(string characterToReplaceProhibitedWord, string prohibitedWords)
        {
            if (characterToReplaceProhibitedWord.Length > ForumDBFieldLength.ProhibitedWords_characterToReplaceProhibitedWordFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Character To Replace Prohibited Word", ForumDBFieldLength.ProhibitedWords_characterToReplaceProhibitedWordFieldLength);
            if (prohibitedWords.Length > ForumDBFieldLength.ProhibitedWords_prohibitedWordsFieldLength)
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Prohibited Words", ForumDBFieldLength.ProhibitedWords_prohibitedWordsFieldLength);
        }

        private string[] SplitProhibitedWords(string prohibitedWords)
        {
            string[] prohibitedWordsArray = prohibitedWords.ToLower()
                    .Replace(";", ",").Replace(" ", ",").Replace("\r\n", ",").Replace("\n", ",").Replace("\r", ",").Replace("-", ",")
                    .Replace("_", ",").Replace("+", ",").Replace("/", ",").Replace("\\", ",").Replace("|", ",").Trim(',')
                    .Split(',');//; \n-_+ / \ |
            List<string> finalProhibitedWordsList = new List<string>();
            foreach (string str in prohibitedWordsArray)
            {
                if (str != "")
                    finalProhibitedWordsList.Add(str);
            }
            return finalProhibitedWordsList.ToArray();
        }

        protected void Update(bool ifEnabledProhibitedWords, string characterToReplaceProhibitedWord, string prohibitedWords)
        {
            CheckFields(characterToReplaceProhibitedWord,prohibitedWords);
            ConfigAccess.UpdateProhibitedWords(_conn, _transaction, ifEnabledProhibitedWords, characterToReplaceProhibitedWord, prohibitedWords);
        }

        public string ReplaceProhibitedWords(string content)
        {
            //ystem.Text.StringBuilder sb = new System.Text.StringBuilder(content.ToLower());
            if (this.IfEnabledProhibitedWords)
            {
                foreach (var str in this.ProhibitedWords)
                {
                    content = Regex.Replace(content, str, this.CharacterToReplaceProhibitedWord, RegexOptions.IgnoreCase);
                    //sb.Replace(str, this.CharacterToReplaceProhibitedWord);
                }
            }
            return content;
            //return sb.ToString();
        }

        public string HrmlReplaceProhibitedWords(string HtmlContent)
        {
            char[] contentChars = HtmlContent.ToCharArray();
            List<char> resultChars = new List<char>();
            List<char> tempChars = new List<char>();
            bool ifBeginToReplace = false;
            foreach (var ch in contentChars)
            {
                if (ch == '>')
                {
                    ifBeginToReplace = true;
                    tempChars.Clear();
                    resultChars.Add(ch);
                }
                else if (ch == '<')
                {
                    ifBeginToReplace = false;
                    resultChars.AddRange(ReplaceProhibitedWords(new string(tempChars.ToArray())).ToCharArray());
                    resultChars.Add(ch);
                }
                else if (ifBeginToReplace == true)
                {
                    tempChars.Add(ch);
                }
                else if (ifBeginToReplace == false)
                {
                    resultChars.Add(ch);
                }
            }
            return new string(resultChars.ToArray());
        }

         
    }
}
