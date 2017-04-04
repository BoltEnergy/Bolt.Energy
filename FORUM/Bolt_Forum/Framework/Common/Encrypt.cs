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
using System.Security.Cryptography;
using System.IO;

namespace Com.Comm100.Framework.Common
{
    public class Encrypt
    {
        #region
        /// <summary>
        /// MD5 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptPassword(string password)
        {
            ////32 bit
            //MD5 md5Hasher = MD5.Create();
            //byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(password));
            //StringBuilder sBuilder = new StringBuilder();

            //for (int i = 0; i < data.Length; i++)
            //{
            //    sBuilder.Append(data[i].ToString("x2"));
            //}
            //return sBuilder.ToString(); 
 
            //16 bit
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string encrypt = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(password)), 4, 8);
            encrypt = encrypt.Replace("-", "");
            return encrypt;

        }
        #endregion 

    }
}
