#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
#if !PocketPC && !SILVERLIGHT
using Com.Comm100.Framework.Language;
#endif

namespace Com.Comm100.Framework.Common
{
    public enum EnumErrorCode
    {
        /*System 0-999*/
        enumSystemNoError = 0,
        enumSystemException = 1,
        enumSystemFieldLengthExceeded = 2,
        enumSystemFieldCanNotBeNull = 3,
        enumSystemNotEnoughPermission = 4,
        enumSystemSessionTimeOut = 5,
        enumSystemQuerystringNull = 6,
        enumSystemRegisterFaild = 7,
        enumSystemProductTypeNotCompatible = 8,
        enumSystemProductVersionNotCompatible = 9,
        enumSystemDatabaseVersionNotCompatible = 11,
        enumSystemFieldNotEqual = 12,
        enumSystemAttachmentsLengthExceeded = 13,
        /*Server Process  1000-1999*/
        enumServerSiteLocked = 1000,
        enumServerCurrentOperatorNotOnline = 1001,
        enumServerSiteNotActive = 1002,
        enumServerOperatorNotOnline = 1003,
        enumServerVisitorNotInSite = 1004,
        enumServerOperatorNotChattingWithTheVisitor = 1005,
        enumServerSiteUnlockFailed = 1006,
        enumServerOperatorAlreadyLogined = 1007,
        enumServerSiteNotExist = 1008,
        enumServerExceedMaxiumNumOfOperatorsInChat = 1009,
        enumServerVisitorNotExist = 1010,
        enumServerRegisterEmailOrNameRepeated = 1011,
        enumServerRegisterSendVerificationEmailFailed = 1012,
        enumServerRegisterFailed = 1013,
        enumServerEmailVerificationGuidTagWrong = 1014,
        enumServerOperatorHasRequstVoiceChat = 1015,
        enumServerVisitorOrAnotherOperatorHasRequestVoiceChat = 1016,
        enumServerOperatorNotVoiceChattingWithTheVisitor = 1017,
        enumServerAnotherOperatorHasRequestCobrowse = 1018,
        enumServerOperatorNotCobrowseOwner = 1019,
        enumServerOperatorSessionTimeout = 1020,
        enumServerOperatorAlreadyVoiceChattingWithAnotherVisitor = 1021,

        /*Application 3000-3999*/
        /*Operator 3100-3199*/
        enumOperatorNotExistWithSiteIdAndEmailAndPWD = 3100,
        enumOperatorNotExist = 3101,
        enumOperatorEmailNotUnique = 3102,
        enumOperatorDeleted = 3103,
        enumOperatorNoAdmin = 3104,
        enumOperatorNotLogin = 3105,
        enumOperatorNotExistWithOperatorIdAndSiteId = 3106,
        enumOperatorNotExistWithEmail = 3107,
        enumOperatorHasBeenDeletedWithId = 3108,
        enumOperatorHasBeenDeletedWithEmail = 3109,
        enumOperatorNotActiveWithEmail = 3110,
        enumOperatorNotExistWithSiteIdAndEmail = 3111,
        enumOperatorCannotDeleteYourself = 3112,
        enumOperatorCannotDisableYourself = 3113,
        enumOperatorOldPasswordIsWrong = 3114,
        enumOperatorNotActiveWithId = 3115,
        enumOperatorDisplayNameNotUnique = 3116,
        enumOperatorNameNotUnique = 3117,
        enumOperatorRestePasswordTimeExpired = 3118,
        enumOperatorForgetPasswordTagIsIncorrect = 3119,
        enumOperatorAlreadyResetPasswordForForgettingPassword = 3120,
        /*Site 3200-3299*/
        enumSiteNotExist = 3200,
        enumStyleTemplateNotExist = 3201,
        enumSiteNotExistWithEmailAndPassword = 3202,
        enumSiteEmailCanNotBeDuplicated = 3203,
        
        /*Public 4000-4999 */
        enumPublicDateFormatWrong = 4001,
        enumPublicTimeFormatWrong = 4002,
        enumPublicDateTimeFormatWrong = 4003,
        enumDateFromEarlierThanToDate = 4004,
        PublicDateToLaterThanNow = 4005,

        enumPublicInputStringIsNotAnInteger=4006,

        #region Forum Exception Code 60000~69999
        /*Forum Category 60000-60099*/
        enumCategoryNotExist = 60000,
        enumCategoryIsUsing = 60001,

        /*Forum Draft 60100-60199*/
        enumDraftNotExist = 60100,
        enmuDraftNotExistInTopic = 60101,

        /*Forum Topic 60200-60299*/
        enumTopicNotExist = 60200,
        enumTopicIsClosed = 60201,
        enumTopicHasBeenMoved = 60202,
        enumAnnouncementNotExsit = 60203,
        enumForumTopicNotInRecycleBin = 60204,

        /*Forum Answer 60300-60399*/
        enumAnswerNotExit = 60300,

        /*Forum Froum 60400-60499*/
        enumForumNotExist = 60400,
        enumForumIsHidden = 60401,
        enumForumIsLocked = 60402,
        enumForumUserWithoutPermissionViewForum = 60403,
        enumForumPostNeedingPayTopicNotAllow = 60404,
        enumForumPostNeedingReplayTopicNotAllow = 60405,

        //User   60500-60599
        enumAvatarFileNotExists = 60500,
        enumAvatarFileSizeIsTooLarge = 60501,
        enumAvatarFormatError = 60502,
        enumUserEmailNotUnique = 60503,
        enumUserDisplayNameNotUnique = 60504,
        enumUserPasswordError = 60505,
        enumUserNotExist = 60506,
        enumUserNotExistWithSiteIdAndEmail = 60507,
        enumUserNotExistWithSiteIdAndEmailAndPWD = 60508,
        enumUserNotEmailVerificated = 60509,
        enumUserNotModerated = 60510,
        enumUserRefused = 60511,
        enumUserIdNotExist = 60512,
        enumUserNotExistWithEmail = 60513,
        enumUserHasBeenDeletedWithId = 60514,
        enumUserNotActiveWithEmail = 60515,
        enumUserNotActiveWithId = 60516,
        enumUserNotLogin = 60517,
        enumForumUserNotExistWithSiteIdAndEmailAndPWD = 60518,
        enumUserNotAdministrator = 60519,
        enumForumUserOnlyOneAdministrator = 60520,

        enumForumAdministratorNotExistWithId = 60521,
        enumForumUserCannotDeleteHimSelf = 60522,
        enumForumMemberOfGroupNotExistWithUserIdAndGroupId = 60523,
        enumForumModeratorOfForumNotExistWithUserIdAndForumId = 60524,

        enumForumUserWithoutPermissionAllowCustomizeAvatar = 60551,
        enumForumUserWithoutPermissionMaxLengthofSignature = 60552,
        enumForumUserWithoutPermissionAllowHTML = 60553,
        enumForumUserWithoutPermissionAllowURL = 60554,
        enumForumUserWithoutPermissionAllowInsertImage = 60555,

        enumForumUserWithoutPermissionAllowSearch = 60556,
        enumForumUserWithoutPermissionMinIntervalTimeforSearching = 60557,
        enumForumUserOrOperatorNotExistWithId = 60558,
        enumForumUserOrOperatorNotExistWithEmail = 60559,
        enumForumUserOrOperatorNotExistWithName = 60560,
        enumForumOperatingUserOrOperatorCanNotBeNull = 60561,
        enumForumUserOrOperatorNotActiveWithId = 60562,
        enumForumUserOrOperatorNotActiveWithName = 60563,
        enumForumUserOrOperatorNotActiveWithEmail = 60564,

        enumForumOnlyAdministratorsHavePermission = 60565,
        enumForumOnlyModeratorsOrAdminstratorsHavePermission = 60566,
        enumForumUseOrOperatorHaveBeenBanned = 60567,
        enumForumUserOrOperatorScoreIsNotEnough =60568,
        enumForumUserOrOperatorHavePaid=60569,
        enumForumUserOrOperatorHaveNotPaid=60570,
        ForumOperatingUserOrOperatorCanNotBeNull=60571,
        enumForumUserHaveNoPermissionToVisit = 60572,
        enumForumUseOrOperatorHaveBeenModerated = 60573,
        enumForumOnlyModeratorsHavePermission = 60574,
        enumForumMessageReceiverIsRequired = 60575,


        //Forum Post 60600-60699
        enumPostNotExist = 60600,
        enumTopicFirstPostNotExist = 60601,
        enumTopicLastPostNotExist = 60602,

        enumPostImageNotExist = 60603,
        enumPostImageFileNotExists = 60604,
        enumPostImageFileSizeIsTooLarge = 60605,
        enumPostImageFormatError = 60606,
        enumForumPostNoPermission = 60607,
        enumForumUserWithoutPermissionMinIntervalTimeforPosting = 60608,
        enumForumUserWithoutPermissionAllowViewTopicOrPost = 60609,
        enumForumUserWithoutPermissionAllowPostTopicOrPost = 60610,
        enumForumUserWithoutPermissionMaxLengthofTopicOrPost = 60611,
        enumForumUserWithoutPermissionPostModerationNotRequired = 60612,
        enumForumPostNotInRecycleBin= 60613,
        enumForumPostNotWaitingForModeration=60614,
        enumForumPollHaveVoted = 60615,
        enumForumPollHaveExpired = 60616,
        enumForumPostIsAbuseing = 60617,
        enumForumPostNotAbused = 60618,
        enumForumPostNotWaingForModerationOrRejected =60619,
        enumForumPostModerationStautsIsRejected =60620,
        enumForumPostAbuseStautsIsAbusedAndApproved=60621,

        //Forum Register 60700-60799
        enumForumRegisterFailed = 60700,
        enumForumEmailVerificationGuidTagWrong = 60701,
        enumForumRegisterEmailOrNameRepeated = 60702,
        enumForumRegisterSendVerificationEmailFailed = 60703,
        enumUserEmailFormatWrong = 60704,
        enumUserDisplayNameFormatWrong = 60705,

        enumUserIllegalDispalyName = 60706,
        enumUserDisplayNameLength = 60707,
        enumUserDisplayNameFormatErrorAndShowInstruction = 60708,

        enumForumRegisterEmailRepeated = 60709,
        enumForumRegisterNameRepeated = 60710,

        //Forum Abuse 60800~60899
        enumForumAbuseNotExist = 60800,

        //Forum Ban 60900~60999
        enumForumBanNotExist = 60900,
        enumForumBanCannotAddWithUserId = 60901,
        enumForumBanStartIPCannotLargerThanEndIP = 60902,
        enumForumBanedUserNotExist = 60900,
        enumForumBanedIPNotExist = 60900,


        //Forum Poll 61000~61099
        enumForumPollNotExist = 61000,
        enumForumPollOptionCountLessThanMaxChoice = 61001,

        //Forum Attachment 61100~61199
        enumForumAttachmentNotExist = 61100,
        enumForumUserWithoutPermissionAllowAttachment = 61101,
        enumForumUserWithoutPermissionMaxAttachmentsinOnePost = 61102,
        enumForumUserWithoutPermissionMaxSizeoftheAttachment = 61103,
        enumForumUserWithoutPermissionMaxSizeofalltheAttachments = 61104,
        //Forum Favorite  61200~61299
        enumForumFavoriteNotExist = 61200,
        enumForumFavoriteIsExist=61201,

        //Forum Subscribe 61300~61399
        enumForumSubscribeNotExistInTopic = 61300,
        enumForumUnSubscribeSingleTopicEmailMisMatch = 61301,
        //Forum Message 61400~61499
        enumForumInMessageNotExist = 61400,
        enumForumOutMessageNotExist = 61401,
        enumForumUserWithoutPermissionMaxMessagesSentinOneDay = 61402,

        //Forum Group 61500~61599
        enumForumUserGroupNotExistWithId = 61500,
        enumForumReputationGroupNotExistWithId = 61501,
        enumForumAllForumUserGroupCannotBeDeleted = 61502,
        enumForumUserGroupOfForumNotExistWithGroupIdAndForumId = 61503,
        enumForumReputationGroupOfForumNotExistWithGroupIdAndForumId = 61504,
        enumForumUserGroupOfForumHaveExistedWithGroupIdAndForumId = 61505,
        enumForumReputationGroupOfForumHaveExistedWithGroupIdAndForumId = 61506,
        enumForumReputationGroupInvalidReputationRange = 61507,
        enumForumReputationGroupRepititiveRange = 61508,
        enumForumGroupIsNotReputationGroupWithId = 61509,

        //Forum User Permission 61600~61699
        enumForumPermissionNotExistWithGroupId = 61600,
        enumForumPermissionNotExistWithGroupIdAndForumId = 61601,
        enumForumGetPermissionError = 61602,

        //Forum Settings 61700~61799
        enumForumSettingsCloseMessageFunction = 61700,
        enumForumSettingsCloseFavoriteFunction = 61701,
        enumForumSettingsCloseSubscribeFunction = 61702,
        enumForumSettingsCloseHotTopicFunction = 61703,
        enumForumSettingsCloseGroupPermissionFunction = 61704,
        enumForumSettingsCloseReputationPermissionFunction = 61705,
        enumForumSettingsCloseScoreFunction = 61706,
        enumForumSettingsCloseReputationFunction = 61707,
        enumForumSettingsCloseGroupPermissionAndReputationGroup = 61708,
        enumForumFeatureDisableReputationPermission = 61709,
        enumForumFeatureNotExist = 61710,
        enumForumGuestUserPermissionSettingNotExist = 61711,
        enumForumHotTopicStrategySettingNotExist = 61712,
        enumForumProhibitedWordsSettingNotExist = 61713,
        enumForumRegistrationSettingNotExist = 61714,
        enumForumReputationStrategySettingNotExist = 61715,
        enumForumScoreStrategySettingNotExist = 61716,
        enumForumSiteSettingNotExist = 61717,
        enumForumStyleSettingNotExist = 61718,
        enumForumUserPermissionSettingNotExist = 61719,
        enumForumRegistrationSettingDisplayNameMinLengthLargerThanMaxLength = 61720,
        enumForumSMTPSettingsNotExist = 61721,
        enumForumSMTPAuthenticationRequiredUserNameAndPassword = 61722,
        enumForumSiteIsVisitOnly =61723,


        #endregion Forum Exception Code 60000~69999
    }

#if !PocketPC && !SILVERLIGHT
    public class ExceptionWithCode : Exception
    {
        private EnumErrorCode _enumErrorCode;
        private string _message;

        public ExceptionWithCode(EnumErrorCode enumErrorCode, string message)
        {
            _message = message;
            _enumErrorCode = enumErrorCode;
        }

        public ExceptionWithCode(EnumErrorCode enumErrorCode)
        {
            LanguageProxy languageProxy = new LanguageProxy();
            _message = languageProxy.GetExceptionText(enumErrorCode);
            _enumErrorCode = enumErrorCode;
        }

        public ExceptionWithCode(int para, EnumErrorCode enumErrorCode)
        {
            LanguageProxy languageProxy = new LanguageProxy();
            _message = string.Format(languageProxy.GetExceptionText(enumErrorCode), para);
            _enumErrorCode = enumErrorCode;
        }

        public ExceptionWithCode(long para, EnumErrorCode enumErrorCode)
        {
            LanguageProxy languageProxy = new LanguageProxy();
            _message = string.Format(languageProxy.GetExceptionText(enumErrorCode), para);
            _enumErrorCode = enumErrorCode;
        }

        public ExceptionWithCode(string para, EnumErrorCode enumErrorCode)
        {
            LanguageProxy languageProxy = new LanguageProxy();
            _message = string.Format(languageProxy.GetExceptionText(enumErrorCode), para);
            _enumErrorCode = enumErrorCode;
        }

        public ExceptionWithCode(string paraStr, int paraInt, EnumErrorCode enumErrorCode)
        {
            LanguageProxy languageProxy = new LanguageProxy();
            _message = string.Format(languageProxy.GetExceptionText(enumErrorCode), paraStr, paraInt);
            _enumErrorCode = enumErrorCode;
        }

        public ExceptionWithCode(int paraInt, string paraStr, EnumErrorCode enumErrorCode)
        {
            LanguageProxy languageProxy = new LanguageProxy();
            _message = string.Format(languageProxy.GetExceptionText(enumErrorCode), paraInt, paraStr);
            _enumErrorCode = enumErrorCode;
        }

        public ExceptionWithCode(int paraInt1, int paraInt2, EnumErrorCode enumErrorCode)
        {
            LanguageProxy languageProxy = new LanguageProxy();
            _message = string.Format(languageProxy.GetExceptionText(enumErrorCode), paraInt1, paraInt2);
            _enumErrorCode = enumErrorCode;
        }

        public ExceptionWithCode(EnumErrorCode enumErrorCode, params object[] argvs)
        {
            LanguageProxy languageProxy = new LanguageProxy();
            _message = string.Format(languageProxy.GetExceptionText(enumErrorCode), argvs);
            _enumErrorCode = enumErrorCode;
        }

        public EnumErrorCode GetErrorCode()
        {
            return _enumErrorCode;
        }


        public override string Message { get { return _message; } }

    }
#endif
}
