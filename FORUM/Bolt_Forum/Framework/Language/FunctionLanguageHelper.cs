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

namespace Com.Comm100.Framework.Language
{
    public enum EnumFunctionLanguageCode
    {
        //DateTimeHelper
        DateTimeHelper_DateTransferToString_DateTime = 1,
        DateTimeHelper_DateTransferToString_Hour=2,
        DateTimeHelper_DateTransferToString_Hours = 3,
        DateTimeHelper_DateTransferToString_Minute = 4,
        DateTimeHelper_DateTransferToString_Minutes = 5,
        //ASPNetPager
        ASPNetPager_DateTransferToString_PageTop = 6,
    }
    public class FunctionLanguageHelper
    {
        private LanguageProxy _languageProxy;

        public FunctionLanguageHelper()
        {
           this. _languageProxy = new LanguageProxy();
        }

        public string GetText(EnumFunctionLanguageCode enumCode, object[] paras)
        {
            if (paras != null && paras.Length > 0)
            {
                return string.Format(this._languageProxy.GetFunctionText(enumCode), paras);
            }
            return this._languageProxy.GetFunctionText(enumCode);
        }

        public string GetText(EnumFunctionLanguageCode enumCode, object[] paras, object[] paras2,object[] paras3)
        {
            if ((paras != null && paras.Length > 0) && (paras2 != null && paras2.Length > 0) && (paras3 != null && paras3.Length > 0))
            {
                return string.Format(this._languageProxy.GetFunctionText(enumCode), paras);
            }
            return this._languageProxy.GetFunctionText(enumCode);
        }

       
    }
}
