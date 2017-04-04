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

namespace Com.Comm100.Framework.Common
{
    public class StringHelper
    {
        public static int[] GetIntArrayFromString(string strs)
        {
            return GetIntArrayFromString(strs,';');
        }

        public static int[] GetIntArrayFromString(string strs, char splitChar)
        {
            string[] strArray = strs.Split(new char[] { splitChar }, StringSplitOptions.RemoveEmptyEntries);
            int[] intArray = new int[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                if (!int.TryParse(strArray[i], out intArray[i]))
                {
                    throw new InvalidCastException();
                }
            }
            return intArray;
        }

        public static string GetMarkedLengthOfString(string str, int length)
        {
            if (str.Length > length)
            {
                str = str.Substring(0, length);
                str = str + "...";
            }
            else
            { 
            }
            return str;
        }

    }
}
