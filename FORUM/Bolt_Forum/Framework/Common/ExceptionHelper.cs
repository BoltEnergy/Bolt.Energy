#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using Com.Comm100.Language;

namespace Com.Comm100.Framework.Common
{
    public class ExceptionHelper
    {
        #region Public
        public static void ThrowPublicDateFormatWrongException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumPublicDateFormatWrong);

        }
        public static void ThrowPublicTimeFormatWrongException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumPublicTimeFormatWrong);
            //string message = "Time format is wrong.";
            //throw new ExceptionWithCode(EnumErrorCode.enumPublicTimeFormatWrong, message);
        }
        public static void ThrowPublicDateTimeFormatWrongException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumPublicDateTimeFormatWrong);
            //string message = "Date or time format is wrong.";
            //throw new ExceptionWithCode(EnumErrorCode.enumPublicDateTimeFormatWrong, message);
        }
        public static void ThrowPublicDateFromEarlierThanToDateException()
        {

            throw new ExceptionWithCode(EnumErrorCode.enumDateFromEarlierThanToDate);
            //string message = "The 'To' time can not be earlier than the 'From' time.";
            //throw new ExceptionWithCode(EnumErrorCode.enumDateFromEarlierThanToDate, message);
        }
        public static void ThrowPublicDateToLaterThanNowException()
        {
            throw new ExceptionWithCode(EnumErrorCode.PublicDateToLaterThanNow);
            //string message = "The 'To' time can not be later than 'now' ";
            //throw new ExceptionWithCode(EnumErrorCode.PublicDateToLaterThanNow, message);
        }

        public static void ThrowPublicInputStringIsNotAnIntegerException(string inputString)
        {
            throw new ExceptionWithCode(inputString, EnumErrorCode.enumPublicInputStringIsNotAnInteger);
        }
        #endregion        

        #region System
        public static void ThrowSystemNoErrorException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumSystemNoError);
        }
        public static void ThrowSystemErrorException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumSystemException);
        }
        public static void ThrowSystemFieldLengthExceededException(string fieldName, int fieldLength)
        {
            throw new ExceptionWithCode(fieldName, fieldLength, EnumErrorCode.enumSystemFieldLengthExceeded);
        }
        public static void ThrowSystemAttachmentsLengthExceededException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumSystemAttachmentsLengthExceeded);
        }
        public static void ThrowSystemFieldCanNotBeNull(string fieldName)
        {
            throw new ExceptionWithCode(fieldName, EnumErrorCode.enumSystemFieldCanNotBeNull);
        }
        public static void ThrowSystemFieldNotEqualException(string fieldName, int fieldLength)
        {
            throw new ExceptionWithCode(fieldName, fieldLength, EnumErrorCode.enumSystemFieldNotEqual);
        }
        public static void ThrowSystemNotEnoughPermissionException(string operatorName)
        {
            throw new ExceptionWithCode(operatorName, EnumErrorCode.enumSystemNotEnoughPermission);
        }
        public static void ThrowSystemSessionTimeOutException(string sessionName)
        {
            throw new ExceptionWithCode(sessionName, EnumErrorCode.enumSystemSessionTimeOut);
        }
        public static void ThrowSystemQuerystringNullException(string querystringName)
        {
            throw new ExceptionWithCode(querystringName, EnumErrorCode.enumSystemQuerystringNull);
        }
        public static void ThrowSystemRegisterFaildException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumSystemRegisterFaild);
        }

        public static void ThrowSystemProductTypeNotCompatibleException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumSystemProductTypeNotCompatible);
        }

        public static void ThrowSystemProductVersionNotCompatibleException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumSystemProductVersionNotCompatible);
        }

        public static void ThrowSystemDatabaseVersionNotCompatibleException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumSystemDatabaseVersionNotCompatible);
        }
        #endregion

        #region Site
        public static void ThrowSiteNotExistException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumSiteNotExist);
        }
        public static void ThrowSiteNotExistWithEmailAndPasswordException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumSiteNotExistWithEmailAndPassword);
        }
        public static void ThrowSiteEmailCanNotBeDuplicatedException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumSiteEmailCanNotBeDuplicated);
        }
        #endregion

        #region StyleTemplate
        public static void ThrowStyleTemplateNotExistException(int styleTemplateId)
        {
            throw new ExceptionWithCode(styleTemplateId, EnumErrorCode.enumStyleTemplateNotExist);
        }
        #endregion

        #region Operator
        public static void ThrowOperatorNotLoginException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumOperatorNotLogin);
        }
        public static void ThrowOperatorEmailNotUniqueException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumOperatorEmailNotUnique);
        }
        public static void ThrowOperatorNameNotUniqueException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumOperatorNameNotUnique);
        }
        public static void ThrowOperatorNotExistException(int operatorId)
        {
            throw new ExceptionWithCode(operatorId, EnumErrorCode.enumOperatorNotExist);
        }
        public static void ThrowOperatorDeletedException(int operatorId)
        {
            throw new ExceptionWithCode(operatorId, EnumErrorCode.enumOperatorDeleted);
        }
        public static void ThrowOperatorNoAdminExcepton()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumOperatorNoAdmin);
        }
        public static void ThrowOperatorNotExistWithSiteIdAndEmailAndPWDException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumOperatorNotExistWithSiteIdAndEmailAndPWD);
        }

        public static void ThrowOperatorNotExistWithSiteIdAndEmailException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumOperatorNotExistWithSiteIdAndEmail);
        }
        public static void ThrowOperatorNotExistWithOperatorIdAndSiteIdException(string operatorName, int siteId)
        {
            throw new ExceptionWithCode(operatorName, siteId, EnumErrorCode.enumOperatorNotExistWithOperatorIdAndSiteId);
        }
        public static void ThrowOperatorNotExistWithEmailException(string email)
        {
            throw new ExceptionWithCode(email, EnumErrorCode.enumOperatorNotExistWithEmail);
        }
        public static void ThrowOperatorHasBeenDeletedWithIdException(int Id)
        {
            throw new ExceptionWithCode(Id, EnumErrorCode.enumOperatorHasBeenDeletedWithId);
        }
        public static void ThrowOperatorHasBeenDeletedWithEmailException(string email)
        {
            throw new ExceptionWithCode(email, EnumErrorCode.enumOperatorHasBeenDeletedWithEmail);
        }
        public static void ThrowOperatorNotActiveWithEmailException(string email)
        {
            throw new ExceptionWithCode(email, EnumErrorCode.enumOperatorNotActiveWithEmail);
        }
        public static void ThrowOperatorNotActiveWithIdException(int id)
        {
            throw new ExceptionWithCode(id, EnumErrorCode.enumOperatorNotActiveWithId);
        }

        public static void ThrowOperatorNotExistWithSiteIdAndEmailException(int siteId, string email)
        {
            throw new ExceptionWithCode(siteId, email, EnumErrorCode.enumOperatorNotExistWithSiteIdAndEmail);
        }

        public static void ThrowOperatorCannotDeleteYourselfException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumOperatorCannotDeleteYourself);
        }

        public static void ThrowOperatorCannotDisableYourselfException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumOperatorCannotDisableYourself);
        }
        public static void ThrowOperatorOldPasswordIsWrongException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumOperatorOldPasswordIsWrong);
        }
        public static void ThrowOperatorDisplayNameNotUniqueException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumOperatorDisplayNameNotUnique);
        }
        public static void ThrowOperatorRestePasswordTimeExpiredException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumOperatorRestePasswordTimeExpired);
        }
        public static void ThrowOperatorForgetPasswordTagIsIncorrectException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumOperatorForgetPasswordTagIsIncorrect);
        }
        public static void ThrowOperatorAlreadyResetPasswordForForgettingPasswordException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumOperatorAlreadyResetPasswordForForgettingPassword);
        }
        #endregion

        #region Forum Exception Function
        #region Forum Category
        public static void ThrowCategoryNotExistException(int categoryId)
        {
            throw new ExceptionWithCode(categoryId, EnumErrorCode.enumCategoryNotExist);
        }
        public static void ThrowCategoryIsUsingException(int categoryId)
        {
            throw new ExceptionWithCode(categoryId, EnumErrorCode.enumCategoryIsUsing);
        }
        #endregion

        #region Forum Draft
        public static void ThrowDraftNotExistException(int draftId)
        {
            throw new ExceptionWithCode(draftId, EnumErrorCode.enumDraftNotExist);
        }

        public static void ThrowDraftNotExistInTopicException(int topicId)
        {
            throw new ExceptionWithCode(topicId, EnumErrorCode.enmuDraftNotExistInTopic);
        }
        #endregion

        #region Forum Topic
        public static void ThrowTopicNotExistException(int topicId)
        {
            throw new ExceptionWithCode(topicId, EnumErrorCode.enumTopicNotExist);
        }

        public static void ThrowTopicIsClosedException(int topicId)
        {
            throw new ExceptionWithCode(topicId, EnumErrorCode.enumTopicIsClosed);
        }

        public static void ThrowTopicHasBeenMovedException(int topicId)
        {
            throw new ExceptionWithCode(topicId, EnumErrorCode.enumTopicHasBeenMoved);
        }
        public static void ThrowAnnouncementNotExsitException(int topicId)
        {
            throw new ExceptionWithCode(topicId, EnumErrorCode.enumAnnouncementNotExsit);
        }
        public static void ThrowForumTopicNotInRecycleBinException(int topicId)
        {
            throw new ExceptionWithCode(topicId, EnumErrorCode.enumForumTopicNotInRecycleBin);
        }
        #endregion

        #region Forum Answer
        public static void ThrowAnswerNotExistException(int TopicId)
        {
            throw new ExceptionWithCode(TopicId, EnumErrorCode.enumAnswerNotExit);
        }
        #endregion

        #region Forum Forum
        public static void ThrowForumNotExistException(int forumId)
        {
            throw new ExceptionWithCode(forumId, EnumErrorCode.enumForumNotExist);
        }
        public static void ThrowForumIsHiddenException(int forumId)
        {
            throw new ExceptionWithCode(forumId, EnumErrorCode.enumForumIsHidden);
        }
        public static void ThrowForumIsLockedException(int forumId)
        {
            throw new ExceptionWithCode(forumId, EnumErrorCode.enumForumIsLocked);
        }
        public static void ThrowForumUserWithoutPermissionViewForumException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserWithoutPermissionViewForum);
        }
        public static void ThrowForumPostNeedingPayTopicNotAllowException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumPostNeedingPayTopicNotAllow);
        }
        public static void ThrowForumPostNeedingReplayTopicNotAllowException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumPostNeedingReplayTopicNotAllow);
        }
        #endregion

        #region Forum User
        public static void ThrowAvatarFileNotExistsException(string filePath)
        {
            throw new ExceptionWithCode(filePath, EnumErrorCode.enumAvatarFileNotExists);
        }
        public static void ThrowAvatarFileSizeIsTooLargeException(string fileName)
        {
            throw new ExceptionWithCode(fileName, EnumErrorCode.enumAvatarFileSizeIsTooLarge);
        }
        public static void ThrowAvatarFormatErrorException(string fileName)
        {
            throw new ExceptionWithCode(fileName, EnumErrorCode.enumAvatarFormatError);
        }
        public static void ThrowUserEmailNotUniqueException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumUserEmailNotUnique);
        }
        public static void ThrowUserDisplayNameNotUniqueException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumUserDisplayNameNotUnique);
        }
        public static void ThrowMessageReceiverIsRequiredException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumMessageReceiverIsRequired);
        }
        public static void ThrowUserPasswordErrorException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumUserPasswordError);
        }

        public static void ThrowUserNotExistWithSiteIdAndEmailException(int siteId, string email)
        {
            throw new ExceptionWithCode(siteId, email, EnumErrorCode.enumUserNotExistWithSiteIdAndEmail);
        }
        public static void ThrowUserNotExistWithSiteIdAndEmailAndPWDException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumUserNotExistWithSiteIdAndEmailAndPWD);
        }
        public static void ThrowUserNotEmailVerificatedException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumUserNotEmailVerificated);
        }
        public static void ThrowUserNotModeratedException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumUserNotModerated);
        }
        public static void ThrowUserRefusedException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumUserRefused);
        }
        public static void ThrowUserIdNotExist(int userId)
        {
            throw new ExceptionWithCode(userId, EnumErrorCode.enumUserIdNotExist);
        }

        public static void ThrowUserNotExistWithEmailException(string email)
        {
            throw new ExceptionWithCode(email, EnumErrorCode.enumUserNotExistWithEmail);
        }

        public static void ThrowUserHasBeenDeletedWithIdException(int Id)
        {
            throw new ExceptionWithCode(Id, EnumErrorCode.enumUserHasBeenDeletedWithId);
        }
        public static void ThrowUserNotActiveWithEmailException(string email)
        {
            throw new ExceptionWithCode(email, EnumErrorCode.enumUserNotActiveWithEmail);
        }
        public static void ThrowUserNotActiveWithIdException(int id)
        {
            throw new ExceptionWithCode(id, EnumErrorCode.enumUserNotActiveWithId);
        }
        public static void ThrowUserNotLoginException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumUserNotLogin);
        }
        public static void ThrowUserEmailFormatWrongException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumUserEmailFormatWrong);
        }
        public static void ThrowUserDisplayNameFormatWrongException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumUserDisplayNameFormatWrong);
        }
        public static void ThrowUserIllegalDispalyName(string name)
        {
            throw new ExceptionWithCode(name, EnumErrorCode.enumUserIllegalDispalyName);
        }
        public static void ThrowUserDisplayNameLength(int min, int max)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumUserDisplayNameLength, min, max);
        }
        public static void ThrowUserDisplayNameFormatErrorAndShowInstruction(string instruction)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumUserDisplayNameFormatErrorAndShowInstruction, instruction);
        }

        public static void ThrowForumUserWithoutPermissionAllowCustomizeAvatarException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserWithoutPermissionAllowCustomizeAvatar);
        }
        public static void ThrowForumUserWithoutPermissionMaxLengthofSignatureException(long length)
        {
            throw new ExceptionWithCode(length, EnumErrorCode.enumForumUserWithoutPermissionMaxLengthofSignature);
        }
        public static void ThrowForumUserWithoutPermissionAllowHTMLException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserWithoutPermissionAllowHTML);
        }
        public static void ThrowForumUserWithoutPermissionAllowURLException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserWithoutPermissionAllowURL);
        }
        public static void ThrowForumUserWithoutPermissionAllowSearchException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserWithoutPermissionAllowSearch);
        }
        public static void ThrowForumUserWithoutPermissionMinIntervalTimeforSearchingException(int minIntervalTime)
        {
            throw new ExceptionWithCode(minIntervalTime, EnumErrorCode.enumForumUserWithoutPermissionMinIntervalTimeforSearching);
        }
        public static void ThrowForumUserOrOperatorNotActiveWithIdException(int id)
        {
            throw new ExceptionWithCode(id, EnumErrorCode.enumForumUserOrOperatorNotActiveWithId);
        }
        public static void ThrowForumUserOrOperatorNotActiveWithNameException(string name)
        {
            throw new ExceptionWithCode(name, EnumErrorCode.enumForumUserOrOperatorNotActiveWithName);
        }
        public static void ThrowForumUserOrOperatorNotActiveWithEmailException(string email)
        {
            throw new ExceptionWithCode(email, EnumErrorCode.enumForumUserOrOperatorNotActiveWithEmail);
        }
        public static void ThrowForumOnlyAdministratorsHavePermissionException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumOnlyAdministratorsHavePermission);
        }
        public static void ThrowForumOnlyModeratorsHavePermissionException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumOnlyModeratorsHavePermission);
        }
        public static void ThrowForumOnlyModeratorsOrAdminstratorsHavePermissionException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumOnlyModeratorsOrAdminstratorsHavePermission);
        }

        public static void ThrowForumUserCannotDeleteHimselfException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserCannotDeleteHimSelf);
        }
        public static void ThrowForumMemberOfGroupNotExistWithUserIdAndGroupId(int userId, int groupId)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumMemberOfGroupNotExistWithUserIdAndGroupId, userId, groupId);
        }
        public static void ThrowForumModeratorOfForumNotExistWithUserIdAndForumId(int userId, int forumId)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumModeratorOfForumNotExistWithUserIdAndForumId, userId, forumId);
        }
        public static void ThrowForumUseOrOperatorHaveBeenBannedException(string displayName)
        {
            throw new ExceptionWithCode(displayName, EnumErrorCode.enumForumUseOrOperatorHaveBeenBanned);
        }
        public static void ThrowForumUseOrOperatorHaveBeenModerated(string displayName)
        {
            throw new ExceptionWithCode(displayName, EnumErrorCode.enumForumUseOrOperatorHaveBeenModerated);
        }
        public static void ThrowForumUserOrOperatorScoreIsNotEnoughException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserOrOperatorScoreIsNotEnough);
        }
        public static void ThrowForumUserOrOperatorHavePaidException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserOrOperatorHavePaid);
        }
        public static void ThrowForumUserOrOperatorHaveNotPaidException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserOrOperatorHaveNotPaid);
        }
        public static void ThrowForumUserHaveNoPermissionToVisit()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserHaveNoPermissionToVisit);
        }
        #region Create User Or Operator
        public static void ThrowForumUserOrOperatorNotExistWithIdException(int id)
        {
            throw new ExceptionWithCode(id, EnumErrorCode.enumForumUserOrOperatorNotExistWithId);
        }
        public static void ThrowForumUserOrOperatorNotExistWithEmailException(string email)
        {
            throw new ExceptionWithCode(email, EnumErrorCode.enumForumUserOrOperatorNotExistWithEmail);
        }
        public static void ThrowForumUserOrOperatorNotExistWithNameException(string name)
        {
            throw new ExceptionWithCode(name, EnumErrorCode.enumForumUserOrOperatorNotExistWithName);
        }
        public static void ThrowForumOperatingUserOrOperatorCanNotBeNullException()
        {
            throw new ExceptionWithCode(EnumErrorCode.ForumOperatingUserOrOperatorCanNotBeNull);
        }
        #endregion Create User Or Operator

        #region Forum administrator
        public static void ThrowUserNotAdministrator(int userId)
        {
            throw new ExceptionWithCode(userId, EnumErrorCode.enumUserNotAdministrator);
        }

        public static void ThrowUserOnlyOneAdministartor()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserOnlyOneAdministrator);
        }

        public static void ThrowForumAdministratorNotExistWithId(int userId)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumAdministratorNotExistWithId, userId);
        }
        #endregion Forum administrator

        #endregion

        #region Forum Register
        public static void ThrowRegisterFailedException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumRegisterFailed);
        }

        public static void ThrowEmailVerificationGuidTagWrong()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumEmailVerificationGuidTagWrong);
        }

        public static void ThrowRegisterEmailRepeatedException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumRegisterEmailRepeated);
        }

        public static void ThrowRegisterNameRepeatedException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumRegisterNameRepeated);
        }

        public static void ThrowRegisterEmailOrNameRepeatedException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumRegisterEmailOrNameRepeated);
        }

        public static void ThrowRegisterSendVerificationEmailFailed()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumRegisterSendVerificationEmailFailed);
        }

        #endregion

        #region Forum Post

        public static void ThrowPostNotExistException(int postId)
        {
            throw new ExceptionWithCode(postId, EnumErrorCode.enumPostNotExist);
        }

        public static void ThrowFirstPostNotExistException(int topicId)
        {
            throw new ExceptionWithCode(topicId, EnumErrorCode.enumTopicFirstPostNotExist);
        }

        public static void ThrowLastPostNotExistException(int topicId)
        {
            throw new ExceptionWithCode(topicId, EnumErrorCode.enumTopicLastPostNotExist);
        }

        public static void ThrowForumPostNoPermissionException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumPostNoPermission);
        }
        public static void ThrowForumUserWithoutPermissionAllowViewTopicOrPostException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserWithoutPermissionAllowViewTopicOrPost);
        }
        public static void ThrowForumUserWithoutPermissionAllowPostTopicOrPostException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserWithoutPermissionAllowPostTopicOrPost);
        }
        public static void ThrowForumUserWithoutPermissionMinIntervalTimeforPostingException(int minIntervaltime)
        {
            throw new ExceptionWithCode(minIntervaltime, EnumErrorCode.enumForumUserWithoutPermissionMinIntervalTimeforPosting);
        }
        public static void ThrowForumUserWithoutPermissionMaxLengthofTopicOrPostException(long maxLengthOfTopicOrPost)
        {
            throw new ExceptionWithCode(maxLengthOfTopicOrPost, EnumErrorCode.enumForumUserWithoutPermissionMaxLengthofTopicOrPost);
        }
        public static void ThrowForumUserWithoutPermissionPostModerationNotRequiredException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserWithoutPermissionPostModerationNotRequired);
        }
        public static void ThrowForumPostNotInRecycleBinException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumPostNotInRecycleBin);
        }
        public static void ThrowForumPostNotWaitingForModerationException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumPostNotWaitingForModeration);
        }
        public static void ThrowForumPostNotWaingForModerationOrRejectedException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumPostNotWaingForModerationOrRejected);
        }
        public static void ThrowForumPostIsAbuseingException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumPostIsAbuseing);
        }
        public static void ThrowForumPostNotAbusedException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumPostNotAbused);
        }
        public static void ThrowForumPostAbuseStautsIsAbusedAndApprovedException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumPostAbuseStautsIsAbusedAndApproved);
        }
        public static void ThrowForumPostModerationStautsIsRejected()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumPostModerationStautsIsRejected);
        }
        #endregion

        #region Forum PostImage

        public static void ThrowPostImageNotExistException(int imageId)
        {
            throw new ExceptionWithCode(imageId, EnumErrorCode.enumPostImageNotExist);
        }
        public static void ThrowPostImageFileNotExistException(string path)
        {
            throw new ExceptionWithCode(path, EnumErrorCode.enumPostImageFileNotExists);
        }
        public static void ThrowPostImageFileSizeIsTooLargeException(string fileName)
        {
            throw new ExceptionWithCode(fileName, EnumErrorCode.enumPostImageFileSizeIsTooLarge);
        }
        public static void ThrowPostImageFormatErrorException(string fileName)
        {
            throw new ExceptionWithCode(fileName, EnumErrorCode.enumPostImageFormatError);
        }
        public static void ThrowForumUserWithoutPermissionAllowInsertImageException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserWithoutPermissionAllowInsertImage);
        }
        #endregion

        #region Forum Login
        public static void ThrowForumUserNotExistWithSiteIdAndEmailAndPWDException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserNotExistWithSiteIdAndEmailAndPWD);
        }
        #endregion

        #region Abuse
        public static void ThrowForumAbuseNotExistException(int id)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumAbuseNotExist, id);
        }
        #endregion Abuse

        #region Poll
        public static void ThrowForumPollNotExistException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumPollNotExist);
        }
        public static void ThrowForumPollHaveVotedException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumPollHaveVoted);
        }
        public static void ThrowForumPollHaveExpireException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumPollHaveExpired);
        }
        public static void ThrowForumPollOptionCountLessThanMaxChoiceException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumPollOptionCountLessThanMaxChoice);
        }
        #endregion Poll

        #region Ban
        public static void ThrowForumBanNotExistException(int id)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumBanNotExist, id);
        }

        public static void ThrowForumBanedUserNotExistException(int id)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumBanedUserNotExist, id);
        }

        public static void ThrowForumBanedIPNotExistException(long ip)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumBanedIPNotExist, IpHelper.LongIP2DottedIP(ip));
        }

        public static void ThrowForumBanCannotAddWithUserId(int id)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumBanCannotAddWithUserId, id);
        }

        public static void ThrowForumBanStartIPCannotLargerThanEndIP()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumBanStartIPCannotLargerThanEndIP);
        }
        #endregion Ban

        #region Attachment
        public static void ThrowForumAttachmentNotExistException(int id)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumAttachmentNotExist, id);
        }
        public static void ThrowForumUserWithoutPermissionAllowAttachmentException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserWithoutPermissionAllowAttachment);
        }
        public static void ThrowForumUserWithoutPermissionMaxAttachmentsinOnePostException(int count)
        {
            throw new ExceptionWithCode(count, EnumErrorCode.enumForumUserWithoutPermissionMaxAttachmentsinOnePost);
        }
        public static void ThrowForumUserWithoutPermissionMaxSizeoftheAttachmentException(long size)
        {
            throw new ExceptionWithCode(size, EnumErrorCode.enumForumUserWithoutPermissionMaxSizeoftheAttachment);
        }
        public static void ThrowForumUserWithoutPermissionMaxSizeofalltheAttachmentsException(long size)
        {
            throw new ExceptionWithCode(size, EnumErrorCode.enumForumUserWithoutPermissionMaxSizeofalltheAttachments);
        }

        #endregion

        #region Favorite
        public static void ThrowForumFavoriteNotExistException(int id)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumFavoriteNotExist, id);
        }
        public static void ThrowForumFavortieIsExsitException(int id)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumFavoriteIsExist,id);
        }
        #endregion

        #region Subscribe
        public static void ThrowForumSubscribeNotExistException(int id)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumSubscribeNotExistInTopic, id);
        }
        public static void ThrowForumUnSubscribeSingleTopicEmailMisMatch()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUnSubscribeSingleTopicEmailMisMatch);
        }
        #endregion

        #region Message
        public static void ThrowForumInMessageNotExistException(int id)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumInMessageNotExist, id);
        }
        public static void ThrowForumOutMessageNotExistException(int id)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumOutMessageNotExist, id);
        }
        public static void ThrowForumUserWithoutPermissionMaxMessagesSentinOneDayException(int count)
        {
            throw new ExceptionWithCode(count, EnumErrorCode.enumForumUserWithoutPermissionMaxMessagesSentinOneDay);
        }
        #endregion

        #region Group
      
        public static void ThrowForumUserGroupNotExistWithId(int groupId)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserGroupNotExistWithId, groupId);
        }
        public static void ThrowForumReputationGroupNotExistWithId(int groupId)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumReputationGroupNotExistWithId, groupId);
        }
        public static void ThrowForumAllForumUserGroupCannotBeDeleted()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumAllForumUserGroupCannotBeDeleted);
        }
        public static void ThrowForumUserGroupOfForumNotExistWithGroupIdAndForumId(int groupId, int forumId)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserGroupOfForumNotExistWithGroupIdAndForumId, groupId, forumId);
        }
        public static void ThrowForumReputationGroupOfForumNotExistWithGroupIdAndForumId(int groupId, int forumId)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumReputationGroupOfForumNotExistWithGroupIdAndForumId, groupId, forumId);
        }
        public static void ThrowForumUserGroupOfForumHaveExistedWithGroupIdAndForumId(int groupId, int forumId)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserGroupOfForumHaveExistedWithGroupIdAndForumId, groupId, forumId);
        }
        public static void ThrowForumReputationGroupOfForumHaveExistedWithGroupIdAndForumId(int groupId, int forumId)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumReputationGroupOfForumHaveExistedWithGroupIdAndForumId, groupId, forumId);
        }
        public static void ThrowForumReputationGroupInvalidReputationRangeException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumReputationGroupInvalidReputationRange);
        }
        public static void ThrowForumReputationGroupRepititiveRangeException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumReputationGroupRepititiveRange);
        }
        public static void ThrowForumGroupIsNotReputationGroupWithId(int gorupId)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumGroupIsNotReputationGroupWithId, gorupId);
        }
        #endregion Group

        #region Permission
        public static void ThrowForumPermissionNotExistWithGroupIdException(int groupId)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumPermissionNotExistWithGroupId, groupId);
        }
        public static void ThrowForumPermissionNotExistWithGroupIdAndForumId(int groupId, int forumId)
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumPermissionNotExistWithGroupIdAndForumId, groupId, forumId);
        }
        public static void ThrowForumGetPermissionError()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumGetPermissionError);
        }
        #endregion Permission

        #region Forum Settings
        public static void ThrowForumSettingsCloseMessageFunction()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumSettingsCloseMessageFunction);
        }
        public static void ThrowForumSettingsCloseFavoriteFunction()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumSettingsCloseFavoriteFunction);
        }
        public static void ThrowForumSettingsCloseSubscribeFunction()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumSettingsCloseSubscribeFunction);
        }
        public static void ThrowForumSettingsCloseHotTopicFunction()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumSettingsCloseHotTopicFunction);
        }
        public static void ThrowForumSettingsCloseGroupPermissionFunction()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumSettingsCloseGroupPermissionFunction);
        }
        public static void ThrowForumSettingsCloseReputationPermissionFunction()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumSettingsCloseReputationPermissionFunction);
        }
        public static void ThrowForumSettingsCloseScoreFunctio()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumSettingsCloseScoreFunction);
        }
        public static void ThrowForumSettingsCloseReputationFunctio()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumSettingsCloseReputationFunction);
        }
        public static void ThrowForumSettingsCloseGroupsPermissionAndReputationGroup()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumSettingsCloseGroupPermissionAndReputationGroup);
        }
        public static void ThrowForumFeatureDisableReputationPermission()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumFeatureDisableReputationPermission);
        }
        public static void ThrowForumFeatureNotExist()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumFeatureNotExist);
        }
        public static void ThrowForumGuestUserPermissionSettingNotExist()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumGuestUserPermissionSettingNotExist);
        }
        public static void ThrowForumHotTopicStrategySettingNotExist()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumHotTopicStrategySettingNotExist);
        }
        public static void ThrowForumProhibitedWordsSettingNotExist()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumProhibitedWordsSettingNotExist);
        }
        public static void ThrowForumRegistrationSettingNotExist()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumRegistrationSettingNotExist);
        }
        public static void ThrowForumReputationStrategySettingNotExist()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumReputationStrategySettingNotExist);
        }
        public static void ThrowForumScoreStrategySettingNotExist()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumScoreStrategySettingNotExist);
        }
        public static void ThrowForumSiteSettingNotExist()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumSiteSettingNotExist);
        }
        public static void ThrowForumStyleSettingNotExist()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumStyleSettingNotExist);
        }
        public static void ThrowForumUserPermissionSettingNotExist()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumUserPermissionSettingNotExist);
        }
        public static void ThrowForumRegistrationSettingDisplayNameMinLengthLargerThanMaxLength()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumRegistrationSettingDisplayNameMinLengthLargerThanMaxLength);
        }
        public static void ThrowForumSMTPSettingsNotExist()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumSMTPSettingsNotExist);
        }
        public static void ThrowForumSMTPAuthenticationRequiredUserNameAndPassword()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumSMTPAuthenticationRequiredUserNameAndPassword);
        }
        public static void ThrowForumSiteIsVisitOnlyException()
        {
            throw new ExceptionWithCode(EnumErrorCode.enumForumSiteIsVisitOnly);
        }
        #endregion Forum Settings

        #endregion Forum Exception Function

        
    }
}
