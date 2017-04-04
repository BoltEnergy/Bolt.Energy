#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2009                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif

using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Forum.UI.Common;
using Com.Comm100.Framework.ASPNETState;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Language;
using Com.Comm100.Language;

namespace Com.Comm100.Forum.UI.Common
{
    public class BasePage : System.Web.UI.Page
    {
        public DateTime OriginalComparisonDate = DateTime.Parse("1900-01-02");

        protected void InitTimezoneOffset()
        {
            if (Session["TimezoneOffset"] == null)
            {
                string strTimezoneOffset = CommonFunctions.ReadCookies("TimezoneOffset");
                if (strTimezoneOffset != null && strTimezoneOffset.Length > 0)
                {
                    double timezoneOffset = Convert.ToDouble(strTimezoneOffset);
                    Session["TimezoneOffset"] = timezoneOffset;
                }
            }
        }

        #region Proxy
        private LanguageProxy _proxy;
        public LanguageProxy Proxy
        {
            get
            {
                if (this._proxy == null)
                {
                    int language = int.Parse(WebConfigurationManager.AppSettings["EnumLanguage"]);
                    this._proxy = new LanguageProxy((EnumLanguage)language);
                }
                //this._proxy = new LanguageProxy();

                return _proxy;
            }
            set { this._proxy = value; }
        }
        #endregion Proxy

        #region SiteId
        public int SiteId
        {
            get
            {
#if OPENSOURCE
                return 0;
#else
                try
                {
                    //CheckQueryString("siteid");
                    //return Convert.ToInt32(System.Web.HttpContext.Current.Request.QueryString["siteid"]);
                    /*******for test*******/
                    return 200000;
                }
                catch
                {
                    string dest = HttpUtility.UrlEncode("http://www.comm100.com/forum");
                    string information = "Querystring with name \\'siteId\\' is null";
                    Response.Redirect("~/Redirect.aspx?information=" + information + "&dest=" + dest);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    return 0;
                }
#endif
            }
        }
        #endregion SiteId

        #region Utility Method

        public void CheckQueryString(string queryStringName)
        {
            WebUtility.CheckQueryString(queryStringName);
        }

        public string GetStandardString(string str, int n)
        {
            int truelength = GetTrueLength(str);

            if (truelength > n)
            {
                str = GetStringWithStandardLength(str, n - 3) + "...";
            }

            return str;
        }

        public string GetStandardString(string str)
        {
            return GetStandardString(str, 70);
        }

        private int GetTrueLength(string str)
        {
            int len = 0;
            int asc;
            string strWord = "";

            for (int i = 0; i < str.Length; i++)
            {
                strWord = str.Substring(i, 1);
                asc = Convert.ToChar(strWord);

                if (asc < 0 || asc > 127)
                {
                    len += 2;
                }
                else
                {
                    len += 1;
                }
            }

            return len;
        }

        private string GetStringWithStandardLength(string str, int n)
        {
            int len = 0;
            int asc;
            string strWord = "";
            string finalstr = "";

            for (int i = 0; i < str.Length; i++)
            {
                strWord = str.Substring(i, 1);
                asc = Convert.ToChar(strWord);

                if (asc < 0 || asc > 127)
                {
                    len += 2;
                }
                else
                {
                    len += 1;
                }

                if (len == n)
                {
                    finalstr += strWord;
                    break;
                }
                else if (len > n)
                {
                    break;
                }
                else
                {
                    finalstr += strWord;
                }
            }

            return finalstr;
        }

        public string UrlWithAuthorityAndApplicationPath
        {
            get
            {
                string url = "http://";
                url += Request.Url.Authority + Request.ApplicationPath;
                if (!url.EndsWith("/"))
                {
                    url += "/";
                }
                return url;
            }
        }

        public string GetForumStatusName(Com.Comm100.Framework.Enum.Forum.EnumForumStatus status)
        {
            string statusName = "";
            switch (status)
            {
                case EnumForumStatus.Hide:
                    statusName = Proxy[Com.Comm100.Language.EnumText.enumForum_Forums_StatusHide];
                    break;
                case EnumForumStatus.Lock:
                    statusName = Proxy[Com.Comm100.Language.EnumText.enumForum_Forums_StatusLock];
                    break;
                case EnumForumStatus.Open:
                    statusName = Proxy[Com.Comm100.Language.EnumText.enumForum_Forums_StatusOpen];
                    break;
                default:
                    break;
            }
            return statusName;
        }

        public string GetTooltipString(string src)
        {
            return WebUtility.GetTooltipString(src);
        }

        public string GetButtonIMGDir()
        {
            string languageNum = WebConfigurationManager.AppSettings["EnumLanguage"].ToString();
            string dir = "Images/btnImages/";
            switch (languageNum)
            {
                case "0":
                    break;              //English
                case "1":
                    dir = dir + "zh-cn/";//SimplifiedChinese
                    break;
                case "2":
                    dir = dir + "zh-tw/";//TraditionalChinese
                    break;
                case "3":
                    dir = dir + "es/";   //Spanish
                    break;
                case "4":
                    dir = dir + "jp/";   //Japaness
                    break;
                case "5":
                    dir = dir + "fr/";   //French
                    break;
                case "6":
                    dir = dir + "de/";   //German
                    break;
                case "7":
                    dir = dir + "ar/";   //Arabic
                    break;
                case "8":
                    dir = dir + "pg/";   //Portuguese
                    break;
                case "9":
                    dir = dir + "ko/";   //Korean
                    break;
                case "10":
                    dir = dir + "it/";       //Italian
                    break;
                case "11":
                    dir = dir + "ru/";       //Russian
                    break;
                case "12":
                    dir = dir + "sw/";       //Swedish
                    break;
                case "13":
                    dir = dir + "du/";    //Dutch
                    break;
            }
            return dir;
        }
        #endregion

    }
}
