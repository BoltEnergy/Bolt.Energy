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

namespace Com.Comm100.Framework.FieldLength
{
    public class ForumDBFieldLength
    {
        #region Category
        public static readonly int Category_nameFieldLength = 128;
        public static readonly int Category_descriptionFieldLength = 512;
        #endregion

        #region Forum
        public static readonly int Forum_nameFieldLength = 128;
        public static readonly int Forum_descriptionFieldLength = 512;
        #endregion

        #region Draft
        public static readonly int Draft_subjectFieldLength = 128;
        public static readonly int Draft_contentFieldLength = 1073741823;//2^30-1
        #endregion

        #region Topic
        public static readonly int Topic_subjectFieldLength = 128;
        public static readonly int Topic_contentFieldLength = 1073741823;//2^30-1
        #endregion

        #region Post
        public static readonly int Post_subjectFieldLength = 128;
        public static readonly int Post_contentFieldLength = 1073741823;//2^30-1
        #endregion

        #region SiteSetting
        public static readonly int SiteSetting_closeReasonFieldLength = 512;
        public static readonly int SiteSetting_forumNameFieldLength = 64;
        public static readonly int SiteSetting_metaKeywordsFieldLength = 2048;
        public static readonly int SiteSetting_metaDescriptionFieldLength = 2048;
        public static readonly int SiteSetting_contactDetailsFieldLength = 1073741823;//2^30-1
        #endregion

        #region Registration Settings
        public static readonly int RegistrationSetting_illegalDisplayNamesFieldLength = 8000;
        public static readonly int RegistrationSetting_displayNameRegularExpressionFieldLength = 2048;
        public static readonly int RegistrationSetting_displayNameRegularExpressionInstructionLength = 2048;
        public static readonly int RegistrationSetting_greetingMessageFieldLength = 1073741823;//2^30-1
        public static readonly int RegistrationSetting_AgreementFieldLength = 1073741823;//2^30-1
        #endregion

        #region Styles
        public static readonly int Styles_pageHeaderFieldLength = 1073741823;//2^30-1
        public static readonly int Styles_pageFooterFieldLength = 1073741823;//2^30-1
        #endregion

        #region Prohibited Words
        public static readonly int ProhibitedWords_characterToReplaceProhibitedWordFieldLength = 10;
        public static readonly int ProhibitedWords_prohibitedWordsFieldLength = 8000;
        #endregion Prohibited Words

        #region SMTP Settings
        public static readonly int SMTPSettings_SMTPServerFieldLength = 512;
        public static readonly int SMTPSettings_SMTPUserNameFieldLength = 128;
        public static readonly int SMTPSettings_SMTPPasswordFieldLength = 128;
        public static readonly int SMTPSettings_SMTPFromEmailAddressFieldLength = 256;
        public static readonly int SMTPSettings_SMTPFromNameFieldLength = 128;
        #endregion SMTP Settings

        #region User
        public static readonly int User_emailFieldLength = 64;
        public static readonly int User_nameFieldLength = 20;
        public static readonly int User_nameFIeldLengthInDatabase = 128;
        public static readonly int User_firstNameFieldLength = 32;
        public static readonly int User_lastNameFieldLength = 32;
        public static readonly int User_occupationFieldLength = 32;
        public static readonly int User_companyFieldLength = 32;
        public static readonly int User_phoneNumberFieldLength = 32;
        public static readonly int User_faxNumberFieldLength = 32;
        public static readonly int User_interestsFieldLength = 32;
        public static readonly int User_homePageFieldLength = 2048;
        public static readonly int User_passwordFieldLength = 16;
        public static readonly int User_signatureFieldLength = 4000;
        #endregion

        #region Message
        public static readonly int Message_subjectFieldLength = 128;
        public static readonly int Message_messageFieldLength = 3072;
        #endregion Message

        #region Ban
        public static readonly int Ban_noteFieldLength = 2048;
        #endregion Ban

        #region Abuse
        public static readonly int Abuse_noteFieldLength = 2048;
        #endregion Abuse

        #region Attachment
        public static readonly int Attachment_originalNameFieldLength = 256;
        public static readonly int Attachment_descriptionFieldLength = 256;
        #endregion Attachment

        #region Group
        public static readonly int Group_nameFieldLength = 128;
        public static readonly int Group_descriptionFieldLength = 1024;
        #endregion

        #region InMessage
        public static readonly int InMessage_subjectFieldLength = 128;
        public static readonly int InMessage_messageFieldLength = 3072;
        #endregion InMessage

        #region OutMessage
        public static readonly int OutMessage_subjectFieldLength = 128;
        public static readonly int OutMessage_messageFieldLength = 1024;
        #endregion OutMessage

        #region PollOption
        public static readonly int PollOption_optionTextFieldLength = 512;
        #endregion PollOption

    }
}
