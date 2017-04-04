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
using System.IO;

namespace Com.Comm100.Framework.Common
{
    public enum EnumLogLevel
    {
        Off = 0,
        Error = 1,
        Debug = 2
    }


    public class LogHelper
    {
        private static EnumLogLevel _enumLogLevel = EnumLogLevel.Error;
        private static object _lockLogLevel = new object();

        public static void WriteExceptionLog(System.Exception exp)
        {
            if (exp is ExceptionWithCode)
            {
                LogHelper.Debug(exp.Message + "\r\n" + exp.StackTrace);
            }
            else
            {
                LogHelper.Error(exp.Message + "\r\n" + exp.StackTrace);
            }
        }

        public static void SetLogLevel(EnumLogLevel logLevel)
        {
            lock (LogHelper._lockLogLevel)
            {
                LogHelper._enumLogLevel = logLevel;
            }
        }

        public static EnumLogLevel GetLogLevel()
        {
            lock (LogHelper._lockLogLevel)
            {
                return LogHelper._enumLogLevel;
            }
        }

        public static string GetLogFileName()
        {
            string baseDirectory = FileHelper.GetBaseDirectory();
            string pathSeparator = FileHelper.GetPathSeparator();
            return baseDirectory + "logs" + pathSeparator + DateTime.Now.ToString("yy-MM-dd") + ".log";
        }

        public static void MakeSureLogPathExist()
        {
            string logPath = FileHelper.GetBaseDirectory() + "logs";
            if (Directory.Exists(logPath) == false)
            {
                Directory.CreateDirectory(logPath);
            }
        }

        private static void WriteLog(string message)
        {
            StreamWriter writer = null;
            try
            {
                MakeSureLogPathExist();
                string strLogFileName = LogHelper.GetLogFileName();
                if (File.Exists(strLogFileName) == true)
                {
                    writer = File.AppendText(strLogFileName);
                }
                else
                {
                    writer = File.CreateText(strLogFileName);
                }

                writer.WriteLine(message);
                writer.WriteLine();

            }
            catch (Exception exp)
            {
                string error = exp.Message;
            }
            finally
            {
                try
                {
                    if (writer != null)
                        writer.Close();
                }
                catch
                { }
            }
        }

        public static void Error(string message)
        {
            try
            {               
                if ((int)LogHelper.GetLogLevel() >= (int)EnumLogLevel.Error)
                {
                     StringBuilder sb = new StringBuilder("");
                    sb.Append(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));
                    sb.Append(" [Error] ");
                    sb.Append(message);
                    LogHelper.WriteLog(sb.ToString());
                }
            }
            catch (System.Exception)
            { }
        }

        public static void Debug(string message)
        {
            try
            {
                if ((int)LogHelper.GetLogLevel() >= (int)EnumLogLevel.Debug)
                {
                    StringBuilder sb = new StringBuilder("");
                    sb.Append(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));
                    sb.Append(" [Debug] ");
                    sb.Append(message);
                    LogHelper.WriteLog(sb.ToString());
                }
            }
            catch (System.Exception)
            { }
        }    
    }
}
