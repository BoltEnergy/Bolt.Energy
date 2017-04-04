#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.Process
{
    public class SettingsProcess
    {
        #region Public Static Function GetSiteSettingBySiteId
        public static SiteSettingWithPermissionCheck GetSiteSettingBySiteId(int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId siteConn = null;
            SqlConnection generalConn = null;
            try
            {
                siteConn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(siteConn);
                generalConn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(generalConn);

                UserOrOperator operatingUserOrOperator = null;
                if (operatingUserOrOperatorId > 0)
                    operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(siteConn, null, null, operatingUserOrOperatorId);
                SiteSettingWithPermissionCheck siteSettings = new SiteSettingWithPermissionCheck(siteConn, null, generalConn, null , operatingUserOrOperator);
                return siteSettings;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(siteConn);
                DbHelper.CloseConn(generalConn);
            }
        }
        #endregion

        #region Public Static Function UpdateSiteSetting

        public static void UpdateSiteSetting(int siteId, int operatingUserOrOperatorId, string siteName, string metaKeywords, string metaDescription, int pageSize, EnumSiteStatus siteStatus, string closeReason)
        {
            SqlConnectionWithSiteId siteConn = null;
            SqlConnection generalConn = null;
            try
            {
                siteName = siteName.Trim();
                metaKeywords = metaKeywords.Trim();
                metaDescription = metaDescription.Trim();
                closeReason = closeReason.Trim();

                siteConn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(siteConn);
                generalConn = DbHelper.GetGeneralDatabaeConnectin();
                DbHelper.OpenConn(generalConn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(siteConn, null, null, operatingUserOrOperatorId);
                SiteSettingWithPermissionCheck siteSettings = new SiteSettingWithPermissionCheck(siteConn, null, generalConn, null, operatingUserOrOperator);
                siteSettings.Update(siteName, metaKeywords, metaDescription, pageSize, siteStatus, closeReason);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(siteConn);
                DbHelper.CloseConn(generalConn);
            }
        }


        #endregion

        #region Registration
        public static RegistrationSettingWithPermissionCheck GetRegistrationSettingBySiteId(int operatingUserOrOperatorId, int siteId)
        {
            SqlConnectionWithSiteId conn = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                RegistrationSettingWithPermissionCheck tmpGetRegistrationSetting = new RegistrationSettingWithPermissionCheck(conn, null, operatingUserOrOperator);     
                return tmpGetRegistrationSetting;  
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }   
        }

        public static void UpdateRegistrationSetting(int operatingUserOrOperatorId, int siteId, bool ifModerateNewUser, bool ifVerifyEmail, 
            bool ifAllowNewUser, int displayNameMinLength, int displayNameMaxLength,
            string illegalDisplayNames, string displayNameRegularExpression, string displayNameInstruction, string greetingMessage, string agreement)
        {
            SqlConnectionWithSiteId conn = null;
            try
            {
                illegalDisplayNames = illegalDisplayNames.Trim();
                displayNameRegularExpression = displayNameRegularExpression.Trim();
                displayNameInstruction = displayNameInstruction.Trim();
                greetingMessage = greetingMessage.Trim();
                agreement = agreement.Trim();

                if (displayNameMinLength > displayNameMaxLength)
                    ExceptionHelper.ThrowForumRegistrationSettingDisplayNameMinLengthLargerThanMaxLength();

                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUseOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                RegistrationSettingWithPermissionCheck tmpUpdateRegistrationSetting = new RegistrationSettingWithPermissionCheck(conn, null, operatingUseOrOperator);
                tmpUpdateRegistrationSetting.Update(ifModerateNewUser, ifVerifyEmail, ifAllowNewUser, displayNameMinLength, displayNameMaxLength, illegalDisplayNames,
                    displayNameRegularExpression, displayNameInstruction, greetingMessage, agreement);
            }
            catch(System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        #endregion

        #region Forum Feature
        public static ForumFeatureWithPermissionCheck GetForumFeature(int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);

                ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return forumFeature;
                
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }     
        }
        public static void UpdateForumFeature(int siteId, int operatingUserOrOperatorId, bool ifEnableMessage, bool ifEnableFavorite, bool ifEnableSubscribe, bool ifEnableScore, bool ifEnableReputation,
            bool ifEnableHotTopic, bool ifEnableGroupPermission, bool ifEnableReputationPermission)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                forumFeature.Update(ifEnableMessage, ifEnableFavorite, ifEnableSubscribe,ifEnableScore, ifEnableReputation,
                ifEnableHotTopic, ifEnableGroupPermission, ifEnableReputationPermission);

            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }


        }
        #endregion

        #region Score Strategy
        public static ScoreStrategySettingWithPermissionCheck GetScoreStrategy(int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(conn, transaction, operatingUserOrOperator, siteId);
                return scoreStrategySetting;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }

        }
        public static void UpdateScoreStrategy(int siteId,int operatingUserOrOperatorId,
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
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(conn, transaction, operatingUserOrOperator, siteId);
                scoreStrategySetting.Update(registration, firstLoginEveryDay, addModerator, removeModerator, ban, unban, newTopic, topicMarkedAsFeature, topicMarkedAsSticky, topicDeleted, topicRestored, topicAddedIntoFavorites, topicRemovedFromFavorites, topicViewed, topicReplied, topicVerifiedAsSpam, vote, pollVoted, newPost, postDeleted, postRestored, postVerifiedAsSpam, postMarkedAsAnswer, reportAbuse, search);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        #endregion

        #region Reputation Strategy
        public static ReputationStrategySettingWithPermissionCheck GetReputationStrategy(int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(conn, transaction, operatingUserOrOperator, siteId);
                return reputationStrategySetting;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void UpdateReputationStrategy(int siteId,int operatingUserOrOperatorId,
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
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                ReputationStrategySettingWithPermissionCheck scoreStrategySetting = new ReputationStrategySettingWithPermissionCheck(conn, transaction, operatingUserOrOperator, siteId);
                scoreStrategySetting.Update(registration, firstLoginEveryDay, addModerator, removeModerator, ban, unban, newTopic, topicMarkedAsFeature, topicMarkedAsSticky, topicDeleted, topicRestored, topicAddedIntoFavorites, topicRemovedFromFavorites, topicViewed, topicReplied, topicVerifiedAsSpam, vote, pollVoted, newPost, postDeleted, postRestored, postVerifiedAsSpam, postMarkedAsAnswer, reportAbuse, search);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        #endregion

        #region Hot Topic Strategy
        public static HotTopicStrategySettingWithPermissionCheck GetHotTopicStrategy(int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                HotTopicStrategySettingWithPermissionCheck hotTopicStrategySetting = new HotTopicStrategySettingWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return hotTopicStrategySetting;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        public static void UpdateHotTopicStrategy(int siteId, int operatingUserOrOperatorId, int parameterGreaterThanOrEqualViews, int parameterGreaterThanOrEqualPosts, EnumLogical logicalBetweenViewsAndPosts)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                HotTopicStrategySettingWithPermissionCheck hotTopicStrategySetting = new HotTopicStrategySettingWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                hotTopicStrategySetting.Update(parameterGreaterThanOrEqualViews, parameterGreaterThanOrEqualPosts, logicalBetweenViewsAndPosts);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        #endregion

        #region Prohibited Words
        public static ProhibitedWordsSettingWithPermissionCheck GetProhibitedWords(int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                ProhibitedWordsSettingWithPermissionCheck prohibitedWordsSetting = new ProhibitedWordsSettingWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return prohibitedWordsSetting;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        public static void UpdateProhibitedWords(int siteId, int operatingUserOrOperatorId, bool ifEnabledProhibitedWords, string characterToReplaceProhibitedWord, string prohibitedWords)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                ProhibitedWordsSettingWithPermissionCheck prohibitedWordsSetting = new ProhibitedWordsSettingWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                prohibitedWordsSetting.Update(ifEnabledProhibitedWords, characterToReplaceProhibitedWord, prohibitedWords);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        #endregion

        #region User Permission
        public static UserPermissionSettingWithPermissionCheck GetUserPermission(int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UserPermissionSettingWithPermissionCheck userPermissionSetting = new UserPermissionSettingWithPermissionCheck(conn, transaction, operatingUserOrOperator,siteId);
                return userPermissionSetting;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        public static void UpdateUserPermission(int siteId, int operatingUserOrOperatorId,
            bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost, int minIntervalForPost, 
            int maxLengthOfPost, bool ifPostNotNeedModeration, bool ifAllowCustomizeAvatar, int maxLengthofSignature,
            bool ifSignatureAllowUrl, bool ifSignatureAllowInsertImage,
            bool ifAllowUrl, bool ifAllowUploadImage, bool ifAllowUploadAttachment, 
            int maxCountOfAttacmentsForOnePost, int maxSizeOfOneAttachment, int maxSizeOfAllAttachments,
            int maxCountOfMessageSendOneDay, bool ifAllowSearch, int minIntervalForSearch)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UserPermissionSettingWithPermissionCheck userPermissionSetting = new UserPermissionSettingWithPermissionCheck(conn, transaction, operatingUserOrOperator,siteId);
                userPermissionSetting.Update(ifAllowViewForum, ifAllowViewTopic, 
                    ifAllowPost, minIntervalForPost, maxLengthOfPost, ifPostNotNeedModeration,
                    ifAllowCustomizeAvatar, maxLengthofSignature, ifSignatureAllowUrl,  ifSignatureAllowInsertImage, ifAllowUrl, ifAllowUploadImage,
                    ifAllowUploadAttachment, maxCountOfAttacmentsForOnePost, maxSizeOfOneAttachment,
                    maxSizeOfAllAttachments, maxCountOfMessageSendOneDay,
                    ifAllowSearch, minIntervalForSearch);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        #endregion

        #region Guest User Permission
        public static GuestUserPermissionSettingWithPermissionCheck GetGuestUserPermission(int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = null;
                if (operatingUserOrOperatorId > 0)
                    operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                GuestUserPermissionSettingWithPermissionCheck guestUserPermissionSetting = new GuestUserPermissionSettingWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                return guestUserPermissionSetting;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        public static void UpdateGuestUserPermission(int siteId, int operatingUserOrOperatorId, bool ifAllowGuestUserViewForum, bool ifAllowGuestUserSearch, int guestUserSearchInterval)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                GuestUserPermissionSettingWithPermissionCheck guestUserPermissionSetting = new GuestUserPermissionSettingWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                guestUserPermissionSetting.Update(ifAllowGuestUserViewForum, ifAllowGuestUserSearch, guestUserSearchInterval);;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        #endregion

        #region SMTP
        public static SMTPSettingWithPermissionCheck GetSMTP(int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                SMTPSettingWithPermissionCheck smtpSetting = new SMTPSettingWithPermissionCheck(conn, transaction, operatingUserOrOperator, siteId);
                return smtpSetting;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        public static void UpdateSMTP(int siteId, int operatingUserOrOperatorId, string smtpServer, int smtpPort, bool ifAuthentication, string smtpUserName, string smtpPassword, string fromEmailAddress, string fromName, bool ifSSL)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                SMTPSettingWithPermissionCheck smtpSetting = new SMTPSettingWithPermissionCheck(conn, transaction, operatingUserOrOperator, siteId);
                smtpSetting.Update(smtpServer,smtpPort,ifAuthentication,smtpUserName,smtpPassword,fromEmailAddress,fromName,ifSSL);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        #endregion
    }
}
