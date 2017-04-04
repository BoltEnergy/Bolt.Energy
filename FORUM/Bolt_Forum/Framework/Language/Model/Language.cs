#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Language;

namespace Com.Comm100.Language
{
    public class Language
    {
        private string[] _texts;
        private Dictionary<int, string> _exceptionTexts;
        private Dictionary<int, string> _functionTexts;
        private EnumLanguage _enumLanguage;


        public Language(EnumLanguage language, int maxTextId)
        {
            _enumLanguage = language;
            _texts = new string[maxTextId];
            _exceptionTexts = new Dictionary<int, string>();
            _functionTexts = new Dictionary<int, string>();
        }

        public void AddText(EnumText enumText, string text)
        {
            _texts[(int)enumText] = text;
        }

        public string GetText(EnumText enumText)
        {
            return _texts[(int)enumText] as string;
        }

        public void AddExceptionText(EnumErrorCode enumErrorCode, string text)
        {
            _exceptionTexts[(int)enumErrorCode] = text;
        }

        public string GetExceptionText(EnumErrorCode enumErrorCode)
        {
            return _exceptionTexts[(int)enumErrorCode];
        }

        public void AddFunctionText(EnumFunctionLanguageCode enumFunctionLanguageCode, string text)
        {
            _functionTexts[(int)enumFunctionLanguageCode] = text;
        }

        public string GetFunctionText(EnumFunctionLanguageCode enumFunctionLanguageCode)
        {
            return _functionTexts[(int)enumFunctionLanguageCode];
        }
    }
}
