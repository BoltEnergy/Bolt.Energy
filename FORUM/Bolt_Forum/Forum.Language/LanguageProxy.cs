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
using System.Diagnostics;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.Language
{
    public class LanguageProxy
    {
        /*Auto select language*/
        public LanguageProxy()
        {
            this._language = LanguageHelper.GetCurrentLanguageEnum();         
        }

        /*By Site Info*/
        public LanguageProxy(Com.Comm100.Language.EnumLanguage enumLanguage)
        {
            this._language = enumLanguage;
        }

        public string this[EnumText enumText]
        {
            get{return this.GetText(enumText);}
        }

        #region property
        public Com.Comm100.Language.EnumLanguage _language = Com.Comm100.Language.EnumLanguage.enumEnglish;
        #endregion property


        #region method
        private string GetText(EnumText enumText)
        {
            if (LanguageHelper.LanguageList[(int)_language] == null)
            {
                _language = Com.Comm100.Language.EnumLanguage.enumEnglish;
            }

            return LanguageHelper.LanguageList[(int)_language].GetText(enumText);
        }

        public string GetExceptionText(EnumErrorCode enumErrorCode)
        {
            if (LanguageHelper.LanguageList[(int)_language] == null)
            {
                _language = Com.Comm100.Language.EnumLanguage.enumEnglish;
            }

            return LanguageHelper.LanguageList[(int)_language].GetExceptionText(enumErrorCode);
        }

        public string GetFunctionText(Com.Comm100.Framework.Language.EnumFunctionLanguageCode enumFunctionLanguageCode)
        {
            if (LanguageHelper.LanguageList[(int)_language] == null)
            {
                _language = Com.Comm100.Language.EnumLanguage.enumEnglish;
            }

            return LanguageHelper.LanguageList[(int)_language].GetFunctionText(enumFunctionLanguageCode);
        }

        #endregion method
    }
}
