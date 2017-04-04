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
using System.Data.SqlClient;
using Com.Comm100.Framework.Database;
using System.Data;
using System.Collections;

namespace Com.Comm100.Framework.Common
{
    class IpLocationInfo
    {
        public string _country;
        public string _state;
        public string _city;
    }

    public class IpHelper
    {
        public static Hashtable hashIP = new Hashtable();

        public static string LongIP2DottedIP(long ipNumber)
        {
            int[] ips = new int[4];

            for (int i = 0; i < 4; i++)
            {
                double temp = ipNumber / (Math.Pow(256, (3 - i)));
                double remaider = ipNumber % (Math.Pow(256, (3 - i)));
                ips[i] = (int)temp;

                ipNumber = (long)remaider;
            }

            return string.Format("{0}.{1}.{2}.{3}", ips[0], ips[1], ips[2], ips[3]);
        }

        public static long DottedIP2LongIP(string DottedIP)
        {
            if (string.IsNullOrEmpty(DottedIP))
                return 0;
            int i;
            string[] arrDec;
            double num = 0;
            if (DottedIP == "")
            {
                return 0;
            }
            else
            {
                arrDec = DottedIP.Split('.');
                for (i = arrDec.Length - 1; i >= 0; i--)
                {
                    int temp;
                    try
                    {
                        temp = int.Parse(arrDec[i]);
                    }
                    catch (Exception)
                    {
                        temp = 0;
                    }
                    num += ((temp % 256 * Math.Pow(256, (3 - i))));
                }
                return (long)num;
            }
        }
    }
}
