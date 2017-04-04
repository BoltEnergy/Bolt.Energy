#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Language;
using Com.Comm100.Framework.Language;

namespace Com.Comm100.Framework.Common
{
    public class DateTimeHelper
    {
        public static DateTime LocalToUTC(DateTime localDateTime)
        {
          
            return localDateTime.ToUniversalTime();
        }        
        
        public static DateTime UTCToLocal(DateTime utcDateTime)
        {
            double timezoneOffset;
            if (System.Web.HttpContext.Current.Session["TimezoneOffset"] != null)
            {
                timezoneOffset = Convert.ToDouble(System.Web.HttpContext.Current.Session["TimezoneOffset"]);
                return utcDateTime.AddMinutes(-timezoneOffset);
            }
            else
            {
                SessionUser currentUser = (SessionUser)System.Web.HttpContext.Current.Session["CurrentUser"];
                if (currentUser == null)
                {
                    return utcDateTime.ToLocalTime();
                }
                else
                {
                    timezoneOffset = currentUser.TimezoneOffset;
                    return utcDateTime.AddMinutes(-timezoneOffset);
                }
            }
        }
         
        public static DateTime LocalDateStringToUtc(string localDateString)
        {
            double timezoneOffset;

            DateTime utcDate = Convert.ToDateTime(localDateString + "GMT", new System.Globalization.CultureInfo(LanguageHelper.GetLanguageName(EnumLanguage.enumEnglish)));
            if (System.Web.HttpContext.Current.Session["TimezoneOffset"] != null)
            {
                timezoneOffset = Convert.ToDouble(System.Web.HttpContext.Current.Session["TimezoneOffset"]);
            }
            else
            {
                SessionUser currentUser = (SessionUser)System.Web.HttpContext.Current.Session["CurrentUser"];
                timezoneOffset = currentUser.TimezoneOffset;
            }

            utcDate = utcDate.AddMinutes(timezoneOffset).ToUniversalTime();
            return utcDate;
        }        

        public static string DateFormate(DateTime dtTime)
        {
            LanguageProxy proxy = new LanguageProxy();

            string strDateTime = "";
            DateTime localTime = UTCToLocal(dtTime);
            //strDateTime = localTime.ToString(proxy[EnumText.enumPublic_DateFormatWithHour]);
            strDateTime = localTime.ToString("MMM") +" "+ localTime.Day.ToString();
            return strDateTime;
        }

        public static string DateFormateAsMMDDYYYYHHmmss(DateTime dtTime)
        {
            LanguageProxy proxy = new LanguageProxy();

            string strDateTime = "";
            strDateTime = dtTime.ToString(proxy[EnumText.enumPublic_DateFormatWithHour]);
            return strDateTime;
        }

        public static string DateFormateWithoutHours(DateTime dtTime)
        {
            LanguageProxy proxy = new LanguageProxy();
            string strDateTime = "";
            strDateTime = dtTime.ToString(proxy[EnumText.enumPublic_DateFormat]);
            return strDateTime;
        }

        public static string DateTransferToString(DateTime dtTime)
        {
            LanguageProxy proxy = new LanguageProxy();
            string strDateTime = "";
            TimeSpan tmSpan = DateTime.UtcNow.Subtract(dtTime);
            // DateTime localTime = UTCToLocal(dtTime);
            Com.Comm100.Framework.Language.FunctionLanguageHelper flh = new Com.Comm100.Framework.Language.FunctionLanguageHelper();
            if (tmSpan.Days >= 1)
            {
                DateTime localTime = UTCToLocal(dtTime);
                strDateTime = localTime.ToString(proxy[EnumText.enumPublic_DateFormatWithHour]);

            }
            else if (tmSpan.Hours >= 1)
            {
                if (tmSpan.Hours == 1)
                {
                    strDateTime = flh.GetText(Com.Comm100.Framework.Language.EnumFunctionLanguageCode.DateTimeHelper_DateTransferToString_Hour, null);
                }
                else
                {

                    strDateTime = flh.GetText(Com.Comm100.Framework.Language.EnumFunctionLanguageCode.DateTimeHelper_DateTransferToString_Hours, new object[] { tmSpan.Hours });
                }
            }
            else
            {
                if (tmSpan.Minutes == 1)
                {
                    strDateTime = flh.GetText(Com.Comm100.Framework.Language.EnumFunctionLanguageCode.DateTimeHelper_DateTransferToString_Minute, null);
                }
                else
                {
                    strDateTime = flh.GetText(Com.Comm100.Framework.Language.EnumFunctionLanguageCode.DateTimeHelper_DateTransferToString_Minutes, new object[] { tmSpan.Minutes });
                }
            }
            strDateTime = dtTime.ToString("MMM") + " " + dtTime.Day.ToString();
            return strDateTime;

        }
    }
}
