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
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
#if !PocketPC && !SILVERLIGHT
using System.Web;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Language;
using Com.Comm100.Language;
#endif

namespace Com.Comm100.Framework.Common
{
    public class CommonFunctions
    {
#if !PocketPC && !SILVERLIGHT
        public static int[] GetPostContentImageIds(string content)
        {
            if (content.Trim().Length > 0)
            {
                string pattern = @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""]?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.MatchCollection matchs = regex.Matches(content);

                int i = 0;
                string[] urlList = new string[matchs.Count];
                foreach (System.Text.RegularExpressions.Match match in matchs)
                {
                    urlList[i++] = match.Groups["imgUrl"].Value;
                }
                if (i > 0)
                {
                    int start = -1;
                    int end = -1;
                    string pa1 = "&id=";
                    string pa2 = "?id=";
                    string pa3 = "&amp;id=";
                    int imageId = 0;
                    int[] imageIds = new int[urlList.Length];
                    int j = 0;
                    foreach (string url in urlList)
                    {
                        imageId = 0;
                        if (url.ToLower().IndexOf(pa1) >= 0)
                        {
                            start = url.ToLower().IndexOf(pa1) + 4;
                        }
                        else if (url.ToLower().IndexOf(pa2) >= 0)
                        {
                            start = url.ToLower().IndexOf(pa2) + 4;
                        }
                        else if (url.ToLower().IndexOf(pa3) >= 0)
                        {
                            start = url.ToLower().IndexOf(pa3) + 8;
                        }
                        if (start > -1)
                        {
                            end = url.ToLower().IndexOf("&", start);
                            if (end > start)
                            {
                                int.TryParse(url.ToLower().Substring(start, end - start), out imageId);
                            }
                            else
                            {
                                int.TryParse(url.ToLower().Substring(start), out imageId);
                            }
                        }
                        imageIds[j++] = imageId;
                    }
                    return imageIds;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public static string GetLanguageType(EnumLanguage language, EnumLanguage AppSettingsLanguage)
        {
            if (AppSettingsLanguage == EnumLanguage.enumEnglish)
            {
                #region Get Language Type
                switch (language)
                {
                    case EnumLanguage.enumArabic: { return "Arabic"; }
                    case EnumLanguage.enumDutch: { return "Dutch"; }
                    case EnumLanguage.enumEnglish: { return "English"; }
                    case EnumLanguage.enumFrench: { return "French"; }
                    case EnumLanguage.enumGerman: { return "German"; }
                    case EnumLanguage.enumItalian: { return "Italian"; }
                    case EnumLanguage.enumJapaness: { return "Japanese"; }
                    case EnumLanguage.enumKorean: { return "Korean"; }
                    case EnumLanguage.enumPortuguese: { return "Portuguese"; }
                    case EnumLanguage.enumRussian: { return "Russian"; }
                    case EnumLanguage.enumSimplifiedChinese: { return "Simplified Chinese"; }
                    case EnumLanguage.enumSpanish: { return "Spanish"; }
                    case EnumLanguage.enumSwedish: { return "Swedish"; }
                    case EnumLanguage.enumTraditionalChinese: { return "Traditional Chinese"; }
                    case EnumLanguage.enumAfrikaans: { return "Afrikaans"; }
                    case EnumLanguage.enumAlbanian: { return "Albanian"; }
                    case EnumLanguage.enumBelarusian: { return "Belarusian"; }
                    case EnumLanguage.enumBulgarian: { return "Bulgarian"; }
                    case EnumLanguage.enumCatalan: { return "Catalan"; }
                    case EnumLanguage.enumCroatian: { return "Croatian"; }
                    case EnumLanguage.enumCzech: { return "Czech"; }
                    case EnumLanguage.enumDanish: { return "Danish"; }
                    case EnumLanguage.enumEstonian: { return "Estonian"; }
                    case EnumLanguage.enumFilipino: { return "Filipino"; }
                    case EnumLanguage.enumFinnish: { return "Finnish"; }
                    case EnumLanguage.enumGalician: { return "Galician"; }
                    case EnumLanguage.enumGreek: { return "Greek"; }
                    case EnumLanguage.enumHaitian: { return "Haitian"; }
                    case EnumLanguage.enumCreole: { return "Creole"; }
                    case EnumLanguage.enumHebrew: { return "Hebrew"; }
                    case EnumLanguage.enumHindi: { return "Hindi"; }
                    case EnumLanguage.enumHungarian: { return "Hungarian"; }
                    case EnumLanguage.enumIcelandic: { return "Icelandic"; }
                    case EnumLanguage.enumIndonesian: { return "Indonesian"; }
                    case EnumLanguage.enumIrish: { return "Irish"; }
                    case EnumLanguage.enumLatvian: { return "Latvian"; }
                    case EnumLanguage.enumLithuanian: { return "Lithuanian"; }
                    case EnumLanguage.enumMacedonian: { return "Macedonian"; }
                    case EnumLanguage.enumMalay: { return "Malay"; }
                    case EnumLanguage.enumMaltese: { return "Maltese"; }
                    case EnumLanguage.enumNorwegian: { return "Norwegian"; }
                    case EnumLanguage.enumPersian: { return "Persian"; }
                    case EnumLanguage.enumPolish: { return "Polish"; }
                    case EnumLanguage.enumRomanian: { return "Romanian"; }
                    case EnumLanguage.enumSerbian: { return "Serbian"; }
                    case EnumLanguage.enumSlovak: { return "Slovak"; }
                    case EnumLanguage.enumSlovenian: { return "Slovenian"; }
                    case EnumLanguage.enumSwahili: { return "Swahili"; }
                    case EnumLanguage.enumThai: { return "Thai"; }
                    case EnumLanguage.enumTurkish: { return "Turkish"; }
                    case EnumLanguage.enumUkrainian: { return "Ukrainian"; }
                    case EnumLanguage.enumVietnamese: { return "Vietnamese"; }
                    case EnumLanguage.enumWelsh: { return "Welsh"; }
                    case EnumLanguage.enumYiddish: { return "Yiddish"; }
                    
                    
                    default: return "";
                }
                #endregion
            }
            else if (AppSettingsLanguage == EnumLanguage.enumSimplifiedChinese)
            {
                #region Get Language Type
                switch (language)
                {
                    case EnumLanguage.enumArabic: { return "阿拉伯语"; }
                    case EnumLanguage.enumDutch: { return "荷兰语"; }
                    case EnumLanguage.enumEnglish: { return "英语"; }
                    case EnumLanguage.enumFrench: { return "法语"; }
                    case EnumLanguage.enumGerman: { return "德语"; }
                    case EnumLanguage.enumItalian: { return "意大利语"; }
                    case EnumLanguage.enumJapaness: { return "日语"; }
                    case EnumLanguage.enumKorean: { return "韩语"; }
                    case EnumLanguage.enumPortuguese: { return "葡萄牙语"; }
                    case EnumLanguage.enumRussian: { return "俄语"; }
                    case EnumLanguage.enumSimplifiedChinese: { return "简体中文"; }
                    case EnumLanguage.enumSpanish: { return "西班牙语"; }
                    case EnumLanguage.enumSwedish: { return "瑞典语"; }
                    case EnumLanguage.enumTraditionalChinese: { return "繁体中文"; }
                    case EnumLanguage.enumAfrikaans: { return "南非荷兰语"; }
                    case EnumLanguage.enumAlbanian: { return "阿尔巴尼亚语"; }
                    case EnumLanguage.enumBelarusian: { return "白俄罗斯语"; }
                    case EnumLanguage.enumBulgarian: { return "保加利亚语"; }
                    case EnumLanguage.enumCatalan: { return "加泰罗尼亚语"; }
                    case EnumLanguage.enumCroatian: { return "克罗地亚语"; }
                    case EnumLanguage.enumCzech: { return "捷克语"; }
                    case EnumLanguage.enumDanish: { return "丹麦语"; }
                    case EnumLanguage.enumEstonian: { return "爱沙尼亚语"; }
                    case EnumLanguage.enumFilipino: { return "菲律宾语"; }
                    case EnumLanguage.enumFinnish: { return "芬兰语"; }
                    case EnumLanguage.enumGalician: { return "加利西亚语"; }
                    case EnumLanguage.enumGreek: { return "希腊语"; }
                    case EnumLanguage.enumHaitian: { return "海地语"; }
                    case EnumLanguage.enumCreole: { return "克里奥尔语"; }
                    case EnumLanguage.enumHebrew: { return "希伯莱语"; }
                    case EnumLanguage.enumHindi: { return "印地语"; }
                    case EnumLanguage.enumHungarian: { return "匈牙利语"; }
                    case EnumLanguage.enumIcelandic: { return "冰岛语"; }
                    case EnumLanguage.enumIndonesian: { return "印度尼西亚语"; }
                    case EnumLanguage.enumIrish: { return "爱尔兰语"; }
                    case EnumLanguage.enumLatvian: { return "拉脱维亚语"; }
                    case EnumLanguage.enumLithuanian: { return "立陶宛语"; }
                    case EnumLanguage.enumMacedonian: { return "马其顿语"; }
                    case EnumLanguage.enumMalay: { return "马来西亚语"; }
                    case EnumLanguage.enumMaltese: { return "马耳他语"; }
                    case EnumLanguage.enumNorwegian: { return "挪威语"; }
                    case EnumLanguage.enumPersian: { return "波斯语"; }
                    case EnumLanguage.enumPolish: { return "波兰语"; }
                    case EnumLanguage.enumRomanian: { return "罗马尼亚语"; }
                    case EnumLanguage.enumSerbian: { return "塞尔维亚语"; }
                    case EnumLanguage.enumSlovak: { return "斯洛伐克语"; }
                    case EnumLanguage.enumSlovenian: { return "斯洛文尼亚语"; }
                    case EnumLanguage.enumSwahili: { return "斯瓦希里语"; }
                    case EnumLanguage.enumThai: { return "泰语"; }
                    case EnumLanguage.enumTurkish: { return "土耳其语"; }
                    case EnumLanguage.enumUkrainian: { return "乌克兰语"; }
                    case EnumLanguage.enumVietnamese: { return "越南语"; }
                    case EnumLanguage.enumWelsh: { return "威尔士语"; }
                    case EnumLanguage.enumYiddish: { return "意第绪语"; }
                    default: return "";
                }
                #endregion
            }
            return "";
        }

#endif  

        public static string StatusImage(bool ifRead, bool ifClose, bool ifMarkedAsAnswer, bool ifParticipant)
        {
            string strTmp = "";
            if ((ifRead) && (!ifClose) && (!ifMarkedAsAnswer) && (ifParticipant))
            {
                strTmp = "<img src=\"Images/Status/participate_normal.gif\" width=\"19\" height=\"19\" >";//???? 
            }
            else if ((ifRead) && (ifClose) && (!ifMarkedAsAnswer) && (ifParticipant))
            {
                strTmp = "<img src=\"Images/Status/participate_close.gif\" width=\"19\" height=\"19\" >";//???? 
            }
            else if ((ifRead) && (!ifClose) && (ifMarkedAsAnswer) && (ifParticipant))
            {
                strTmp = "<img src=\"Images/Status/participate_mark.gif\" width=\"19\" height=\"19\" >";//????
            }
            else if ((!ifRead) && (!ifClose) && (!ifMarkedAsAnswer) && (ifParticipant))
            {
                strTmp = "<img src=\"Images/Status/participate_normal.gif\" width=\"19\" height=\"19\" >";//??participant
            }
            else if (!ifRead && (ifClose) && (!ifMarkedAsAnswer) && (ifParticipant))
            {
                strTmp = "<img src=\"Images/Status/participate_close.gif\" width=\"19\" height=\"19\" >";//??participant
            }
            else if ((!ifRead) && (!ifClose) && (ifMarkedAsAnswer) && (ifParticipant))
            {
                strTmp = "<img src=\"Images/Status/participate_mark.gif\" width=\"19\" height=\"19\" >";//??participant
            }
            else if ((ifRead) && (!ifClose) && (!ifMarkedAsAnswer) && (!ifParticipant))
            {
                strTmp = "<img src=\"Images/Status/read_normal.gif\" width=\"19\" height=\"19\" >";//???? 
            }
            else if ((ifRead) && (ifClose) && (!ifMarkedAsAnswer) && (!ifParticipant))
            {
                strTmp = "<img src=\"Images/Status/read_close.gif\" width=\"19\" height=\"19\" >";//???? 
            }
            else if ((ifRead) && (!ifClose) && (ifMarkedAsAnswer) && (!ifParticipant))
            {
                strTmp = "<img src=\"Images/Status/read_mark.gif\" width=\"19\" height=\"19\" >";//????
            }
            else if ((!ifRead) && (!ifClose) && (!ifMarkedAsAnswer) && (!ifParticipant))
            {
                strTmp = "<img src=\"Images/Status/noread_normal.gif\" width=\"19\" height=\"19\" >";//????
            }
            else if ((!ifRead) && (ifClose) && (!ifMarkedAsAnswer) && (!ifParticipant))
            {
                strTmp = "<img src=\"Images/Status/noread_close.gif\" width=\"19\" height=\"19\" >";//???? 
            }
            else if ((!ifRead) && (!ifClose) && (ifMarkedAsAnswer) && (!ifParticipant))
            {
                strTmp = "<img src=\"Images/Status/noread_mark.gif\" width=\"19\" height=\"19\" >";//????
            }
            else if ((!ifRead) && (ifClose) && (ifMarkedAsAnswer) && (ifParticipant))
            {
                strTmp = "<img src=\"Images/Status/participate_close.gif\" width=\"19\" height=\"19\" >";//close??
            }
            else if ((ifRead) && (ifClose) && (ifMarkedAsAnswer) && (ifParticipant))
            {
                strTmp = "<img src=\"Images/Status/participate_close.gif\" width=\"19\" height=\"19\" >";//close?? 
            }
            else if ((ifRead) && (ifClose) && (ifMarkedAsAnswer) && (!ifParticipant))
            {
                strTmp = "<img src=\"Images/Status/read_close.gif\" width=\"19\" height=\"19\" >";//???? 
            }
            else if ((!ifRead) && (ifClose) && (ifMarkedAsAnswer) && (!ifParticipant))
            {
                strTmp = "<img src=\"Images/Status/noread_close.gif\" width=\"19\" height=\"19\" >";//????
            }
            return strTmp;
        }

#if !PocketPC && !SILVERLIGHT

        public static string ReadCookies(string cookiesAttribute)
        {
            string CookiesValue = "";
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[cookiesAttribute];
            if (cookie != null)
            {
                CookiesValue = Convert.ToString(System.Web.HttpContext.Current.Request.Cookies[cookiesAttribute].Value);
            }
            return CookiesValue;

        }
        public static void WriteCookies(string cookiesAttribute, string cookiesValue)
        {

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[cookiesAttribute];
            if (cookie != null)
            {
                System.Web.HttpContext.Current.Response.Cookies[cookiesAttribute].Value = cookiesValue; ;
                System.Web.HttpContext.Current.Response.Cookies[cookiesAttribute].Expires = DateTime.Now.AddMonths(12);
            }
            else
            {
                cookie = new HttpCookie(cookiesAttribute);
                cookie.Value = cookiesValue; ;
                cookie.Expires = DateTime.Now.AddMonths(12);
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            }

        }

#endif
        public static string SqlReplace(string src)
        {
            src = src.Replace("/", "//");
            src = src.Replace("%", "/%");
            src = src.Replace("_", "/_");
            src = src.Replace("[", "/[");
            src = src.Replace("]", "/]");
            src = src.Replace("<", "/<");
            src = src.Replace(">", "/>");
            src = src.Replace("^", "/^");
          //  src = src.Replace("'","''");
            return src;
        }
        public static string URLReplace(string src)
        {
            //src = System.Web.HttpUtility.UrlEncode(src);
            src = src.Replace("&", "");
            src = src.Replace("?", "-");
            src = src.Replace(" ", "-");
            src = src.Replace("'", "-");
            src = src.Replace("\"", "-");
            src = src.Replace("/", "-");
            src = src.Replace("\\", "-");
            src = src.Replace("|", "-");
            src = src.Replace("`", "-");
            src = src.Replace(":", "-");
            src = src.Replace(".", "-");
            src = src.Replace("^", "-");
            src = src.Replace("%", "");
            src = src.Replace("*", "");
            src = src.Replace("#", "");
            src = src.Replace("<", "(");
            src = src.Replace(">", ")");
            src = src.Replace("{", "[");
            src = src.Replace("}", "]");
            src = src.Replace("+", "");
            return src;
        }
        
    }
}
