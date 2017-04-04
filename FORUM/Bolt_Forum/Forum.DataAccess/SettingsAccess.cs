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
using System.Data;
using System.Data.SqlClient;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Database;

namespace Com.Comm100.Forum.DataAccess
{
    public class SettingsAccess
    {
        public static DataTable GetSiteSettingBySiteId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int siteId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select ForumName,SiteStatus,ForumCloseReason,MetaKeywords,MetaDescription,PageSize  from t_Forum_Config");
            strSQL.Append(" where");
            strSQL.Append(" SiteId=@siteId"); 
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@siteId", siteId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }



        public static void UpdateSiteSettings(SqlConnectionWithSiteId conn, SqlTransaction transaction, string siteName, string metaKeywords, string metaDesctiption, int pageSize, EnumSiteStatus siteStatus, string closeReason)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" Update t_Forum_Config Set ForumName=@ForumName, MetaKeywords=@MetaKeywords, MetaDescription=@MetaDescription, PageSize=@PageSize, SiteStatus=@SiteStatus, ForumCloseReason=@ForumCloseReason ");
            strSQL.Append(" where SiteId=@SiteId ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ForumName", siteName);
            cmd.Parameters.AddWithValue("@MetaKeywords", metaKeywords);
            cmd.Parameters.AddWithValue("@MetaDescription", metaDesctiption);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            cmd.Parameters.AddWithValue("@SiteStatus", Convert.ToInt16(siteStatus));
            cmd.Parameters.AddWithValue("@ForumCloseReason", closeReason);
            cmd.Parameters.AddWithValue("@SiteId", conn.SiteId);

            cmd.ExecuteNonQuery();
        }


        public static DataTable GetStyleSettingBySiteId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int siteId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select TemplateID, IfCustomizePage, PageHeader, PageFooter");
            strSQL.Append(" from t_Forum_Config");
            strSQL.Append(" where");
            strSQL.Append(" SiteId=@siteId"); 
            
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", siteId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }


        public static DataTable GetCustomLogoInfoBySiteId(SqlConnection conn, SqlTransaction transaction, int siteId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select IfCustomizeLogo, CustomizeLogo");
            strSQL.Append(" from t_Site");
            strSQL.Append(" where");
            strSQL.Append(" ID=@siteId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn, transaction);
            cmd.Parameters.AddWithValue("@siteId", siteId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }


        public static DataTable GetRegistrationSettingBySiteId(SqlConnectionWithSiteId conn, SqlTransaction transaction, int siteId)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" select IfModerateNewUser,IfVerifyEmail,IfAllowNewUser,DisplayNameMinLength,DisplayNameMaxLength,IllegalDisplayNames, ");
            strSQL.Append(" DisplayNameRegularExpression,DisplayNameRegularExpressionInstruction,GreetingMessage,ForumUserAgreement ");
            strSQL.Append(" from t_Forum_Config ");
            strSQL.Append(" where");
            strSQL.Append(" SiteId=@siteId"); 
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);


            DataTable table = new DataTable();

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;            
        }

        public static void UpdateSiteSetting(SqlConnection conn, SqlTransaction transaction,
             int siteId, string siteName, string closeReason, bool ifCloseSite, bool ifForumRequireLogin)
        {


            StringBuilder strSQL = new StringBuilder("Update t_Site");
            strSQL.Append(" set");
            strSQL.Append(" ForumCloseReason=@closeReason, IfForumClosed=@ifCloseSite, ForumName=@siteName,IfForumRequireLogin=@IfForumRequireLogin ");
            strSQL.Append(" where");
            strSQL.Append(" ID=@siteId"); 
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn, transaction);
            cmd.Parameters.AddWithValue("@closeReason", closeReason);
            cmd.Parameters.AddWithValue("@ifCloseSite", ifCloseSite);
            cmd.Parameters.AddWithValue("@siteId",siteId);
            cmd.Parameters.AddWithValue("@siteName", siteName);
            cmd.Parameters.AddWithValue("@IfForumRequireLogin", ifForumRequireLogin);

            cmd.ExecuteNonQuery();            
        }

        public static void UpdateStyleSetting(SqlConnectionWithSiteId conn, SqlTransaction transaction, int siteId, bool ifAdvancedMode, string pageHeader, string pageFooter)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("Update t_Forum_Config");
            strSQL.Append(" set");
            strSQL.Append(" IfCustomizePage = @ifAdvancedMode ");
            if(ifAdvancedMode)
                strSQL.Append(" , PageHeader =  @pageHeader, PageFooter = @pageFooter");
            strSQL.Append(" where SiteId=@siteId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ifAdvancedMode", ifAdvancedMode);
            if (ifAdvancedMode)
            {
                cmd.Parameters.AddWithValue("@pageHeader", pageHeader);
                cmd.Parameters.AddWithValue("@pageFooter", pageFooter);
            }
            cmd.Parameters.AddWithValue("@siteId", siteId);

            cmd.ExecuteNonQuery();           
        }
        public static void UpdateRegistrationSetting(SqlConnectionWithSiteId conn, SqlTransaction transaction,
             int siteId, bool ifModerateNewUser, bool ifVerifyEmail, bool ifAllowNewUser, int displayNameMinLength, int displayNameMaxLength,
            string illegalDisplayNames, string displayNameRegularExpression, string displayNameInstruction, string greetingMessage, string agreement)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" Update t_Forum_Config");
            strSQL.Append(" set");
            strSQL.Append(" IfModerateNewUser=@IfModerateNewUser,IfVerifyEmail=@IfVerifyEmail,");
            strSQL.Append(" IfAllowNewUser=@IfAllowNewUser,DisplayNameMinLength=@DisplayNameMinLength,");
            strSQL.Append(" DisplayNameMaxLength=@DisplayNameMaxLength,IllegalDisplayNames=@IllegalDisplayNames,");
            strSQL.Append(" DisplayNameRegularExpression=@DisplayNameRegularExpression,DisplayNameRegularExpressionInstruction=@DisplayNameInstruction,");
            strSQL.Append(" GreetingMessage=@WelcomeMessage,ForumUserAgreement=@ForumUserAgreement");
            strSQL.Append(" where");
            strSQL.Append(" SiteId=@siteId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId",siteId);
            cmd.Parameters.AddWithValue("@IfModerateNewUser", ifModerateNewUser);
            cmd.Parameters.AddWithValue("@IfVerifyEmail", ifVerifyEmail);
            cmd.Parameters.AddWithValue("@IfAllowNewUser", ifAllowNewUser);
            cmd.Parameters.AddWithValue("@DisplayNameMinLength", displayNameMinLength);
            cmd.Parameters.AddWithValue("@DisplayNameMaxLength", displayNameMaxLength);
            cmd.Parameters.AddWithValue("@IllegalDisplayNames", illegalDisplayNames);
            cmd.Parameters.AddWithValue("@DisplayNameRegularExpression", displayNameRegularExpression);
            cmd.Parameters.AddWithValue("@DisplayNameInstruction", displayNameInstruction);
            cmd.Parameters.AddWithValue("@WelcomeMessage", greetingMessage);
            cmd.Parameters.AddWithValue("@ForumUserAgreement", agreement);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateTemplateID(SqlConnectionWithSiteId conn, SqlTransaction transaction,int siteId,int templateID)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("Update t_Forum_Config ");
            strSQL.Append(" set ");
            strSQL.Append(" TemplateID = @TemplateID ");
            strSQL.Append(" where");
            strSQL.Append(" SiteId=@siteId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@TemplateID", templateID);
            cmd.Parameters.AddWithValue("@siteId", siteId);

            cmd.ExecuteNonQuery();
        }

        public static DataTable GetStyleTemplateById(SqlConnection conn, SqlTransaction transaction, int siteID)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select");
            strSQL.Append(" TemplateID");
            strSQL.Append(" from t_Site");
            strSQL.Append(" where Id=@Id");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn, transaction);

            cmd.Parameters.AddWithValue("@Id", siteID);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static void SaveRules(SqlConnection conn, SqlTransaction transaction, int siteID,string ruleContent)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" update t_Site");
            strSQL.Append(" set ForumUserAgreement=@ruleContent");
            strSQL.Append(" where Id=@Id");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn, transaction);
            cmd.Parameters.AddWithValue("@ruleContent", ruleContent);
            cmd.Parameters.AddWithValue("@Id", siteID);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateForumFeature(SqlConnectionWithSiteId conn, SqlTransaction transaction, bool ifEnableMessage, bool ifEnableFavorite, bool ifEnableSubscribe, bool ifEnableScore, bool ifEnableReputation,
            bool ifEnableHotTopic, bool ifEnableGroupPermission, bool ifEnableReputationPermission)
        {
            StringBuilder strSQL = new StringBuilder("Update t_Forum_Config set ");
            strSQL.Append("ifEnableMessage=@ifEnableMessage, ifEnableFavorite=@ifEnableFavorite, ");
            strSQL.Append("ifEnableSubscribe=@ifEnableSubscribe, ifEnableScore=@ifEnableScore, ");
            strSQL.Append("ifEnableReputation=@ifEnableReputation, ifEnableHotTopic=@ifEnableHotTopic, ");
            strSQL.Append("ifEnableGroupPermission=@ifEnableGroupPermission, ifEnableReputationPermission=@ifEnableReputationPermission ");
            strSQL.Append(" where SiteId=@SiteId ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region cmd.AddWithValue(...)
               cmd.Parameters.AddWithValue("@ifEnableMessage", ifEnableMessage);
               cmd.Parameters.AddWithValue("@ifEnableFavorite", ifEnableFavorite);
               cmd.Parameters.AddWithValue("@ifEnableSubscribe", ifEnableSubscribe);
               cmd.Parameters.AddWithValue("@ifEnableScore", ifEnableScore);
               cmd.Parameters.AddWithValue("@ifEnableReputation", ifEnableReputation);
               cmd.Parameters.AddWithValue("@ifEnableHotTopic", ifEnableHotTopic);
               cmd.Parameters.AddWithValue("@ifEnableGroupPermission", ifEnableGroupPermission);
               cmd.Parameters.AddWithValue("@ifEnableReputationPermission", ifEnableReputationPermission);
               cmd.Parameters.AddWithValue("@SiteId", conn.SiteId);
            #endregion
            cmd.ExecuteNonQuery();
        }
        public static DataTable GetForumFeature(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("Select ifEnableMessage, ifEnableFavorite, ifEnableSubscribe, ifEnableScore, ifEnableReputation, ");
            strSQL.Append("ifEnableHotTopic, ifEnableGroupPermission, ifEnableReputationPermission From t_Forum_Config ");
            strSQL.Append("Where SiteId=@siteId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

       
    }
}
