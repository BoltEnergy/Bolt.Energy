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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.DataAccess
{
    public class ConfigAccess
    {
        public static DataTable GetForumFeature(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder(" select IfEnableMessage, IfEnableFavorite, IfEnableSubscribe, IfEnableScore,IfEnableReputation, IfEnableHotTopic, IfEnableGroupPermission, IfEnableReputationPermission ");
            strSQL.Append(" from t_Forum_Config where SiteId=@SiteId ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@SiteId", conn.SiteId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        
        #region Score Strategy
        public static DataTable GetScoreStrategy(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL=new StringBuilder("select SiteId,");
            strSQL.Append("IfEnableScore,");
            strSQL.Append("ScoreForRegistration,");
            strSQL.Append("ScoreForFirstLogin,");
            strSQL.Append("ScoreForAddModerator,");
            strSQL.Append("ScoreForRemoveModerator,");
            strSQL.Append("ScroeForBan,");
            strSQL.Append("ScoreForUnban,");
            strSQL.Append("ScoreForNewTopic,");
            strSQL.Append("ScoreForTopicMarkedAsFeature,");
            strSQL.Append("ScoreForTopicMarkedAsSticky,");
            strSQL.Append("ScoreForTopicDeleted,");
            strSQL.Append("ScoreForTopicRestored,");
            strSQL.Append("ScoreForTopicAddedIntoFavorites,");
            strSQL.Append("ScoreForTopicRemovedFromFavorites,");
            strSQL.Append("ScoreForTopicViewed,");
            strSQL.Append("ScoreForTopicReplied,");
            strSQL.Append("ScoreForTopicVerifiedAsSpam,");
            strSQL.Append("ScoreForVote,");
            strSQL.Append("ScoreForPollVoted,");
            strSQL.Append("ScoreForNewPost,");
            strSQL.Append("ScoreForPostDeleted,");
            strSQL.Append("ScoreForPostRestored,");
            strSQL.Append("ScoreForPostVerifiedAsSpam,");
            strSQL.Append("ScoreForPostMarkedAsAnswer,");
            strSQL.Append("ScoreForReportAbuse,");
            strSQL.Append("ScoreForSearch");
            strSQL.Append(" from t_Forum_Config where SiteId=@siteId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static void UpdateScoreStrategy(SqlConnectionWithSiteId conn, SqlTransaction transaction,
                            int registration,
                            int firstLoginEveryDay,
                            int addModerator,
                            int removeModerator,
                            int ban,
                            int unban,
                            int newTopic,
                            int topicMarkedAsFeature,
                            int topicMarkedAsSticky,
                            int topicDeleted,
                            int topicRestored,
                            int topicAddedIntoFavorites,
                            int topicRemovedFromFavorites,
                            int topicViewed,
                            int topicReplied,
                            int topicVerifiedAsSpam,
                            int vote,
                            int pollVoted,
                            int newPost,
                            int postDeleted,
                            int postRestored,
                            int postVerifiedAsSpam,
                            int postMarkedAsAnswer,
                            int reportAbuse,
                            int search)
        {
            #region SQL
            StringBuilder strSQL = new StringBuilder("update t_Forum_Config set ");
            strSQL.Append("ScoreForRegistration=@registration,");
            strSQL.Append("ScoreForFirstLogin=@firstLoginEveryDay,");
            strSQL.Append("ScoreForAddModerator=@addModerator,");
            strSQL.Append("ScoreForRemoveModerator=@removeModerator,");
            strSQL.Append("ScroeForBan=@ban,");
            strSQL.Append("ScoreForUnban=@unban,");
            strSQL.Append("ScoreForNewTopic=@newTopic,");
            strSQL.Append("ScoreForTopicMarkedAsFeature=@topicMarkedAsFeature,");
            strSQL.Append("ScoreForTopicMarkedAsSticky=@topicMarkedAsSticky,");
            strSQL.Append("ScoreForTopicDeleted=@topicDeleted,");
            strSQL.Append("ScoreForTopicRestored=@topicRestored,");
            strSQL.Append("ScoreForTopicAddedIntoFavorites=@topicAddedIntoFavorites,");
            strSQL.Append("ScoreForTopicRemovedFromFavorites=@topicRemovedFromFavorites,");
            strSQL.Append("ScoreForTopicViewed=@topicViewed,");
            strSQL.Append("ScoreForTopicReplied=@topicReplied,");
            strSQL.Append("ScoreForTopicVerifiedAsSpam=@topicVerifiedAsSpam,");
            strSQL.Append("ScoreForVote=@vote,");
            strSQL.Append("ScoreForPollVoted=@pollVoted,");
            strSQL.Append("ScoreForNewPost=@newPost,");
            strSQL.Append("ScoreForPostDeleted=@postDeleted,");
            strSQL.Append("ScoreForPostRestored=@postRestored,");
            strSQL.Append("ScoreForPostVerifiedAsSpam=@postVerifiedAsSpam,");
            strSQL.Append("ScoreForPostMarkedAsAnswer=@postMarkedAsAnswer,");
            strSQL.Append("ScoreForReportAbuse=@reportAbuse,");
            strSQL.Append("ScoreForSearch=@search");
            strSQL.Append(" Where SiteId=@siteId");
            #endregion
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Parameters
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@registration", registration);
            cmd.Parameters.AddWithValue("@firstLoginEveryDay", firstLoginEveryDay);
            cmd.Parameters.AddWithValue("@addModerator", addModerator);
            cmd.Parameters.AddWithValue("@removeModerator", removeModerator);
            cmd.Parameters.AddWithValue("@ban", ban);
            cmd.Parameters.AddWithValue("@unban", unban);
            cmd.Parameters.AddWithValue("@newTopic", newTopic);
            cmd.Parameters.AddWithValue("@topicMarkedAsFeature", topicMarkedAsFeature);
            cmd.Parameters.AddWithValue("@topicMarkedAsSticky", topicMarkedAsSticky);
            cmd.Parameters.AddWithValue("@topicDeleted", topicDeleted);
            cmd.Parameters.AddWithValue("@topicRestored", topicRestored);
            cmd.Parameters.AddWithValue("@topicAddedIntoFavorites", topicAddedIntoFavorites);
            cmd.Parameters.AddWithValue("@topicRemovedFromFavorites", topicRemovedFromFavorites);
            cmd.Parameters.AddWithValue("@topicViewed", topicViewed);
            cmd.Parameters.AddWithValue("@topicReplied", topicReplied);
            cmd.Parameters.AddWithValue("@topicVerifiedAsSpam", topicVerifiedAsSpam);
            cmd.Parameters.AddWithValue("@vote", vote);
            cmd.Parameters.AddWithValue("@pollVoted", pollVoted);
            cmd.Parameters.AddWithValue("@newPost", newPost);
            cmd.Parameters.AddWithValue("@postDeleted", postDeleted);
            cmd.Parameters.AddWithValue("@postRestored", postRestored);
            cmd.Parameters.AddWithValue("@postVerifiedAsSpam", postVerifiedAsSpam);
            cmd.Parameters.AddWithValue("@postMarkedAsAnswer", postMarkedAsAnswer);
            cmd.Parameters.AddWithValue("@reportAbuse", reportAbuse);
            cmd.Parameters.AddWithValue("@search", search);
            #endregion
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Reputation Strategy
        public static DataTable GetReputationStrategy(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder("select SiteId,");
            strSQL.Append("IfEnableReputation,");
            strSQL.Append("ReputationForRegistration,");
            strSQL.Append("ReputationForFirstLogin,");
            strSQL.Append("ReputationForAddModerator,");
            strSQL.Append("ReputationForRemoveModerator,");
            strSQL.Append("ReputationForBan,");
            strSQL.Append("ReputationForUnban,");
            strSQL.Append("ReputationForNewTopic,");
            strSQL.Append("ReputationForTopicMarkedAsFeature,");
            strSQL.Append("ReputationForTopicMarkedAsSticky,");
            strSQL.Append("ReputationForTopicDeleted,");
            strSQL.Append("ReputationForTopicRestored,");
            strSQL.Append("ReputationForTopicAddedIntoFavorites,");
            strSQL.Append("ReputationForTopicRemovedFromFavorites,");
            strSQL.Append("ReputationForTopicViewed,");
            strSQL.Append("ReputationForTopicReplied,");
            strSQL.Append("ReputationForTopicVerifiedAsSpam,");
            strSQL.Append("ReputationForVote,");
            strSQL.Append("ReputationForPollVoted,");
            strSQL.Append("ReputationForNewPost,");
            strSQL.Append("ReputationForPostDeleted,");
            strSQL.Append("ReputationForPostRestored,");
            strSQL.Append("ReputationForPostVerifiedAsSpam,");
            strSQL.Append("ReputationForPostMarkedAsAnswer,");
            strSQL.Append("ReputationForReportAbuse,");
            strSQL.Append("ReputationForSearch");
            strSQL.Append(" from t_Forum_Config where SiteId=@siteId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static void UpdateReputationStrategy(SqlConnectionWithSiteId conn, SqlTransaction transaction,
                                            int registration,
                            int firstLoginEveryDay,
                            int addModerator,
                            int removeModerator,
                            int ban,
                            int unban,
                            int newTopic,
                            int topicMarkedAsFeature,
                            int topicMarkedAsSticky,
                            int topicDeleted,
                            int topicRestored,
                            int topicAddedIntoFavorites,
                            int topicRemovedFromFavorites,
                            int topicViewed,
                            int topicReplied,
                            int topicVerifiedAsSpam,
                            int vote,
                            int pollVoted,
                            int newPost,
                            int postDeleted,
                            int postRestored,
                            int postVerifiedAsSpam,
                            int postMarkedAsAnswer,
                            int reportAbuse,
                            int search)
        {
            #region SQL
            StringBuilder strSQL = new StringBuilder("update t_Forum_Config set ");
            strSQL.Append("ReputationForRegistration=@registration,");
            strSQL.Append("ReputationForFirstLogin=@firstLoginEveryDay,");
            strSQL.Append("ReputationForAddModerator=@addModerator,");
            strSQL.Append("ReputationForRemoveModerator=@removeModerator,");
            strSQL.Append("ReputationForBan=@ban,");
            strSQL.Append("ReputationForUnban=@unban,");
            strSQL.Append("ReputationForNewTopic=@newTopic,");
            strSQL.Append("ReputationForTopicMarkedAsFeature=@topicMarkedAsFeature,");
            strSQL.Append("ReputationForTopicMarkedAsSticky=@topicMarkedAsSticky,");
            strSQL.Append("ReputationForTopicDeleted=@topicDeleted,");
            strSQL.Append("ReputationForTopicRestored=@topicRestored,");
            strSQL.Append("ReputationForTopicAddedIntoFavorites=@topicAddedIntoFavorites,");
            strSQL.Append("ReputationForTopicRemovedFromFavorites=@topicRemovedFromFavorites,");
            strSQL.Append("ReputationForTopicViewed=@topicViewed,");
            strSQL.Append("ReputationForTopicReplied=@topicReplied,");
            strSQL.Append("ReputationForTopicVerifiedAsSpam=@topicVerifiedAsSpam,");
            strSQL.Append("ReputationForVote=@vote,");
            strSQL.Append("ReputationForPollVoted=@pollVoted,");
            strSQL.Append("ReputationForNewPost=@newPost,");
            strSQL.Append("ReputationForPostDeleted=@postDeleted,");
            strSQL.Append("ReputationForPostRestored=@postRestored,");
            strSQL.Append("ReputationForPostVerifiedAsSpam=@postVerifiedAsSpam,");
            strSQL.Append("ReputationForPostMarkedAsAnswer=@postMarkedAsAnswer,");
            strSQL.Append("ReputationForReportAbuse=@reportAbuse,");
            strSQL.Append("ReputationForSearch=@search");
            strSQL.Append(" Where SiteId=@siteId");
            #endregion
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Parameters
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@registration", registration);
            cmd.Parameters.AddWithValue("@firstLoginEveryDay", firstLoginEveryDay);
            cmd.Parameters.AddWithValue("@addModerator", addModerator);
            cmd.Parameters.AddWithValue("@removeModerator", removeModerator);
            cmd.Parameters.AddWithValue("@ban", ban);
            cmd.Parameters.AddWithValue("@unban", unban);
            cmd.Parameters.AddWithValue("@newTopic", newTopic);
            cmd.Parameters.AddWithValue("@topicMarkedAsFeature", topicMarkedAsFeature);
            cmd.Parameters.AddWithValue("@topicMarkedAsSticky", topicMarkedAsSticky);
            cmd.Parameters.AddWithValue("@topicDeleted", topicDeleted);
            cmd.Parameters.AddWithValue("@topicRestored", topicRestored);
            cmd.Parameters.AddWithValue("@topicAddedIntoFavorites", topicAddedIntoFavorites);
            cmd.Parameters.AddWithValue("@topicRemovedFromFavorites", topicRemovedFromFavorites);
            cmd.Parameters.AddWithValue("@topicViewed", topicViewed);
            cmd.Parameters.AddWithValue("@topicReplied", topicReplied);
            cmd.Parameters.AddWithValue("@topicVerifiedAsSpam", topicVerifiedAsSpam);
            cmd.Parameters.AddWithValue("@vote", vote);
            cmd.Parameters.AddWithValue("@pollVoted", pollVoted);
            cmd.Parameters.AddWithValue("@newPost", newPost);
            cmd.Parameters.AddWithValue("@postDeleted", postDeleted);
            cmd.Parameters.AddWithValue("@postRestored", postRestored);
            cmd.Parameters.AddWithValue("@postVerifiedAsSpam", postVerifiedAsSpam);
            cmd.Parameters.AddWithValue("@postMarkedAsAnswer", postMarkedAsAnswer);
            cmd.Parameters.AddWithValue("@reportAbuse", reportAbuse);
            cmd.Parameters.AddWithValue("@search", search);
            #endregion
            cmd.ExecuteNonQuery(); 
        }
        #endregion

        #region SMTP Settings
        public static DataTable GetSMTPSettings(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder("select SiteId,");
            strSQL.Append("ForumSMTPServer,");
		    strSQL.Append("ForumSMTPPort,");
		    strSQL.Append("IfForumSMTPAuthentication,");
		    strSQL.Append("ForumSMTPUserName,");
		    strSQL.Append("ForumSMTPPassword,");
		    strSQL.Append("ForumSMTPFromEmailAddress,");
		    strSQL.Append("ForumSMTPFromName,");
		    strSQL.Append("IfForumSMTPSSL");
            strSQL.Append(" from t_Forum_Config where SiteId=@siteId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static void UpdateSMTPSettings(SqlConnectionWithSiteId conn, SqlTransaction transaction, string smtpServer, int smtpPort, bool ifAuthentication, string smtpUserName, string smtpPassword,
                string fromEmailAddress, string fromName, bool ifSSL)
        {
            StringBuilder strSQL = new StringBuilder("update t_Forum_Config set ");
            strSQL.Append("ForumSMTPServer=@smtpServer,");
            strSQL.Append("ForumSMTPPort=@smtpPort,");
            strSQL.Append("IfForumSMTPAuthentication=@ifAuthentication,");
            strSQL.Append("ForumSMTPUserName=@smtpUserName,");
            strSQL.Append("ForumSMTPPassword=@smtpPassword,");
            strSQL.Append("ForumSMTPFromEmailAddress=@fromEmailAddress,");
            strSQL.Append("ForumSMTPFromName=@fromName,");
            strSQL.Append("IfForumSMTPSSL=@ifSSL");
            strSQL.Append(" where SiteId=@siteId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@smtpServer", smtpServer);
            cmd.Parameters.AddWithValue("@smtpPort", smtpPort);
            cmd.Parameters.AddWithValue("@ifAuthentication", ifAuthentication);
            cmd.Parameters.AddWithValue("@smtpUserName", smtpUserName);
            cmd.Parameters.AddWithValue("@smtpPassword", smtpPassword);
            cmd.Parameters.AddWithValue("@fromEmailAddress", fromEmailAddress);
            cmd.Parameters.AddWithValue("fromName", fromName);
            cmd.Parameters.AddWithValue("@ifSSL", ifSSL);
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Hot Topic Strategy
        public static DataTable GetHotTopicStrategy(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder("select SiteId");
            strSQL.Append(",[HotTopicParameterGreaterThanOrEqualViews]");
            strSQL.Append(",[HotTopicParameterGreaterThanOrEqualPosts]");
            strSQL.Append(",[HotTopicLogicalBetweenViewsAndrPosts]");
            strSQL.Append(" from t_Forum_Config where SiteId=@siteId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static void UpdateHotTopicStrategy(SqlConnectionWithSiteId conn, SqlTransaction transaction, int parameterGreaterThanOrEqualViews, int parameterGreaterThanOrEqualPosts, EnumLogical logicalBetweenViewsAndPosts)
        {
            StringBuilder strSQL = new StringBuilder("update t_Forum_Config set ");
            strSQL.Append("HotTopicParameterGreaterThanOrEqualViews=@parameterGreaterThanOrEqualViews,");
            strSQL.Append("HotTopicParameterGreaterThanOrEqualPosts=@parameterGreaterThanOrEqualPosts,");
            strSQL.Append("HotTopicLogicalBetweenViewsAndrPosts=@logicalBetweenViewsAndrPosts");
            strSQL.Append(" where SiteId=@siteId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@parameterGreaterThanOrEqualViews", parameterGreaterThanOrEqualViews);
            cmd.Parameters.AddWithValue("@parameterGreaterThanOrEqualPosts", parameterGreaterThanOrEqualPosts);
            cmd.Parameters.AddWithValue("@logicalBetweenViewsAndrPosts", Convert.ToInt16(logicalBetweenViewsAndPosts));
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Prohibited Words
        public static DataTable GetProhibitedWords(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder("select SiteId");
            strSQL.Append(",[IfEnabledProhibitedWords]");
            strSQL.Append(",[CharacterToReplaceProhibitedWord]");
            strSQL.Append(",[ProhibitedWords]");
            strSQL.Append(" from t_Forum_Config where SiteId=@siteId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        public static void UpdateProhibitedWords(SqlConnectionWithSiteId conn, SqlTransaction transaction, bool ifEnabledProhibitedWords, string characterToReplaceProhibitedWord, string prohibitedWords)
        {
            StringBuilder strSQL = new StringBuilder("update t_Forum_Config set ");
            strSQL.Append("IfEnabledProhibitedWords=@ifEnabledProhibitedWords,");
            strSQL.Append("CharacterToReplaceProhibitedWord=@characterToReplaceProhibitedWord,");
            strSQL.Append("ProhibitedWords=@prohibitedWords");
            strSQL.Append(" where SiteId=@siteId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@ifEnabledProhibitedWords", ifEnabledProhibitedWords);
            cmd.Parameters.AddWithValue("@characterToReplaceProhibitedWord", characterToReplaceProhibitedWord);
            cmd.Parameters.AddWithValue("@prohibitedWords", prohibitedWords);
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Guest User Permission
        public static DataTable GetGuestUserPermission(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder("select SiteId");
            strSQL.Append(",[IfAllowGuestUserViewForum]");
            strSQL.Append(",[IfAllowGuestUserSearch]");
            strSQL.Append(",[GuestUserSearchInterval]");
            strSQL.Append(" from t_Forum_Config where SiteId=@siteId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        public static void UpdateGuestUserPermission(SqlConnectionWithSiteId conn, SqlTransaction transaction, bool ifAllowGuestUserViewForum, bool ifAllowGuestUserSearch, int guestUserSearchInterval)
        {
            StringBuilder strSQL = new StringBuilder("update t_Forum_Config set ");
            strSQL.Append("IfAllowGuestUserViewForum=@ifAllowGuestUserViewForum,");
            strSQL.Append("IfAllowGuestUserSearch=@ifAllowGuestUserSearch,");
            strSQL.Append("GuestUserSearchInterval=@guestUserSearchInterval");
            strSQL.Append(" where SiteId=@siteId");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@siteId", conn.SiteId);
            cmd.Parameters.AddWithValue("@ifAllowGuestUserViewForum", ifAllowGuestUserViewForum);
            cmd.Parameters.AddWithValue("@ifAllowGuestUserSearch", ifAllowGuestUserSearch);
            cmd.Parameters.AddWithValue("@guestUserSearchInterval", guestUserSearchInterval);
            cmd.ExecuteNonQuery();
        }
        #endregion
    }
}
