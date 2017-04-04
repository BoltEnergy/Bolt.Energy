#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using Com.Comm100.Forum.Bussiness;
using System.Data.SqlClient;
using Com.Comm100.Framework.Database;
using System;
using Com.Comm100.Forum;
using System.Data;

namespace Com.Comm100.Forum.Process
{
    public class StyleProcess
    {
        public static StyleSettingWithPermissionCheck GetStyleSettingBySiteId(int siteId, int operatingOperatorId)
        {
            SqlConnection generalConn = null;
            SqlConnectionWithSiteId siteConn = null;

            try
            {
                generalConn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(generalConn);

                siteConn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(siteConn);

                UserOrOperator operatingUserOrOperator = null;//ProcessUtil.GetUserOrOperator(connWithSiteId, null, operatingOperatorId);//(connWithSiteId, null, operatingOperatorId);

                StyleSettingWithPermissionCheck tmpStyleSetting = new StyleSettingWithPermissionCheck(siteConn, null, generalConn, null, operatingUserOrOperator);
                
                return tmpStyleSetting;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(generalConn);
                DbHelper.CloseConn(siteConn);
            }
        }
        public static StyleTemplateWithPermissionCheck[] GetAllStyleTemplates(int operatingUserOrOperatorId, int siteId)
        {
            SqlConnection generalConn = null;
            SqlConnectionWithSiteId siteConn = null;

            try
            {
                generalConn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(generalConn);

                siteConn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(siteConn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(siteConn, null, null, operatingUserOrOperatorId);
                StyleTemplatesWithPermissionCheck styleTemplates = new StyleTemplatesWithPermissionCheck(generalConn, null, operatingUserOrOperator);
                return styleTemplates.GetAllStyleTemplateUrl(operatingUserOrOperator);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(generalConn);
                DbHelper.CloseConn(siteConn);
            }
        }
        public static StyleTemplateWithPermissionCheck GetStyleTemplateBySiteId(int operatingOperatorId, int siteId)
        {
            SqlConnection generalConn = null;
            SqlConnectionWithSiteId siteConn = null;

            try
            {
                generalConn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(generalConn);

                siteConn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(siteConn);

                StyleSettingWithPermissionCheck styleSetting = new StyleSettingWithPermissionCheck(siteConn, null, generalConn, null, null);
                StyleTemplateWithPermissionCheck styleTemplate = styleSetting.GetStyleTemplate();
                return styleTemplate;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(generalConn);
                DbHelper.CloseConn(siteConn);
            }
        }
        public static StyleTemplateWithPermissionCheck GetTemplateByTemplateId(int siteId, int operatingUserOrOperatorId, int templateId)
        {
            SqlConnection generalConn = null;
            SqlConnectionWithSiteId siteConn = null;
            try
            {
                generalConn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(generalConn);

                siteConn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(siteConn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(siteConn, null, null, operatingUserOrOperatorId);
                StyleTemplateWithPermissionCheck styleTemplate = new StyleTemplateWithPermissionCheck(generalConn, null, templateId, operatingUserOrOperator);
                return styleTemplate;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(generalConn);
                DbHelper.CloseConn(siteConn);
            }

        }
        public static void UpdateTemplate(int siteId, int templateID, int operatingUserOrOperatorId)
        {
            SqlConnection generalConn = null;
            SqlConnectionWithSiteId siteConn = null;
            try
            {
                generalConn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(generalConn);

                siteConn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(siteConn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(siteConn, null, null, operatingUserOrOperatorId);
                StyleSettingWithPermissionCheck styleSetting = new StyleSettingWithPermissionCheck(siteConn, null, generalConn, null, operatingUserOrOperator);
                styleSetting.UpdateTemplate(templateID);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(generalConn);
                DbHelper.CloseConn(siteConn);
            }
        }
        public static void UpdateHeaderAndFooter(int siteId, int operatingUserOrOperatorId, bool ifAdvancedMode, string pageHeader,
            string pageFooter)
        {
            SqlConnection generalConn = null;
            SqlConnectionWithSiteId siteConn = null;
            
            try
            {
                generalConn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(generalConn);

                siteConn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(siteConn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(siteConn, null, null, operatingUserOrOperatorId);

                

                StyleSettingWithPermissionCheck tmpStyleSetting = new StyleSettingWithPermissionCheck(siteConn, null, generalConn, null, operatingUserOrOperator);
                tmpStyleSetting.UpdateHeaderAndFooter(ifAdvancedMode, pageHeader, pageFooter);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(generalConn);
                DbHelper.CloseConn(siteConn);
            }
        }
    }
}
