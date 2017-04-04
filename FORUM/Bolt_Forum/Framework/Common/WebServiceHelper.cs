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
using System.Web;
using System.Net;
using System.IO;
using System.Web.Services.Description;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Text;
using System.Reflection;
using System.Data;

namespace Com.Comm100.Framework.Common
{
    public class WebServiceHelper
    {
        private WebServiceHelper() { }
        
        private static object InvokeWebService(string url, string methodName, object[] args)
        {
            return WebServiceHelper.InvokeWebService(url, null, methodName, args);
        }

        private static object InvokeWebService(string url, string className, string methodName, object[] args)
        {
            string @namespace = "EnterpriseServerBase.WebService.DynamicWebCalling";
            if ((className == null) || (className == ""))
            {
                className = WebServiceHelper.GetWsClassName(url);
            }
            try
            {
                
                WebClient wc = new WebClient();
              //  wc.Credentials = new System.Net.NetworkCredential("admin", "admin");
                Stream stream = wc.OpenRead(url + "?WSDL");
                ServiceDescription sd = ServiceDescription.Read(stream);
                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                CodeNamespace cn = new CodeNamespace(@namespace);

               
                CodeCompileUnit ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                CSharpCodeProvider csc = new CSharpCodeProvider();
                //ICodeCompiler icc = csc.CreateCompiler();

                
                CompilerParameters cplist = new CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");

                
                CompilerResults cr = csc.CompileAssemblyFromDom(cplist, ccu);
                if (true == cr.Errors.HasErrors)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (CompilerError ce in cr.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }

                
                Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(@namespace + "." + className, true, true);
                object obj = Activator.CreateInstance(t);
                MethodInfo mi = t.GetMethod(methodName);

                return mi.Invoke(obj, args);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
            }
        }
        private static string GetWsClassName(string wsUrl)
        {
            string[] parts = wsUrl.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        }

        public static string GetComm100News(Enum.EnumApplicationType type)
        {
            Com.Comm100.Language.EnumLanguage language = Com.Comm100.Language.LanguageHelper.GetCurrentLanguageEnum();

            string url = System.Web.Configuration.WebConfigurationManager.AppSettings["CpanelUrl"];            
            if (!url.ToLower().StartsWith("http://") && !url.ToLower().StartsWith("https://"))
            {
                url = "http://" + url;
            }
            if (!url.EndsWith("/"))
            {
                url += "/";
            }
            url += "Comm100News.asmx";
            //string url = "http://localhost/ExcelOperationWebService/ExcelService.asmx";

            string method = "GetNews";
            object[] args = new object[] { (int)type, (int)language };

            object returnValue = WebServiceHelper.InvokeWebService(url, method, args);

            return returnValue == null ? "" : returnValue.ToString();
        }
    }


}

