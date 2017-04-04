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
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Common;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using Com.Comm100.Framework.ASPNETState;

namespace Com.Comm100.Forum.Bussiness
{
    public class CommFun
    {
        #region Public Static Function IfOperator
        public static bool IfOperator(UserOrOperator userOrOperator)
        {
            if (userOrOperator.GetType() == typeof(Com.Comm100.Forum.Bussiness.OperatorWithPermissionCheck))
                return true;
            else
                return false;
        }
        #endregion Public Static Function IfOperator

        #region Public Static Function IfUser
        public static bool IfUser(UserOrOperator userOrOperator)
        {
            if (userOrOperator.GetType() == typeof(Com.Comm100.Forum.Bussiness.UserWithPermissionCheck))
                return true;
            else
                return false;
        }
        #endregion Public Static Function IfUser

        #region Public Static Function IfGuest
        public static bool IfGuest()
        {
            if (HttpContext.Current.Session["CurrentUser"] == null)
                return true;
            else
                return false;
        }
        #endregion Public Static Function IfGuest

        #region Public Static Function CheckUserOrOperator
        public static void CheckUserOrOperator(UserOrOperator operatingUserOrOperator)
        {
            if (operatingUserOrOperator == null)
            {
                ExceptionHelper.ThrowForumOperatingUserOrOperatorCanNotBeNullException();
            }
            else if (operatingUserOrOperator.IfDeleted)
            {
                ExceptionHelper.ThrowForumUserOrOperatorNotExistWithNameException(operatingUserOrOperator.DisplayName);
            }
            else if (!operatingUserOrOperator.IfActive)
            {
                ExceptionHelper.ThrowForumUserOrOperatorNotActiveWithNameException(operatingUserOrOperator.DisplayName);
            }
        }
        #endregion Public Static Function CheckUserOrOperator

        #region Public Static Function CheckAdminPanelCommonPermission
        public static void CheckAdminPanelCommonPermission(UserOrOperator operatingUserOrOperator)
        {
            CheckUserOrOperator(operatingUserOrOperator);

            //operatingUserOrOperator.

            if (CommFun.IfOperator(operatingUserOrOperator))
            {
                OperatorWithPermissionCheck operatingOperator = operatingUserOrOperator as OperatorWithPermissionCheck;
                if (!operatingOperator.IfAdmin)
                {
                    if (!operatingUserOrOperator.IfForumAdmin)
                        ExceptionHelper.ThrowForumOnlyAdministratorsHavePermissionException();
                }
            }
            else
            {
                if (!operatingUserOrOperator.IfForumAdmin)
                    ExceptionHelper.ThrowForumOnlyAdministratorsHavePermissionException();
            }
        }
        public static void CheckAdminPanelOrModeratorPanelCommomPermission(UserOrOperator operatingUserOrOperator, int forumId)
        {
            CheckUserOrOperator(operatingUserOrOperator);

            if (CommFun.IfOperator(operatingUserOrOperator))
            {
                OperatorWithPermissionCheck operatingOperator = operatingUserOrOperator as OperatorWithPermissionCheck;
                if (!operatingOperator.IfModerator(forumId))// if not moderator
                {
                    if (!operatingOperator.IfAdmin)
                    {
                        if (!operatingUserOrOperator.IfForumAdmin)
                            ExceptionHelper.ThrowForumOnlyModeratorsOrAdminstratorsHavePermissionException();
                    }
                }
            }
            else
            {
                UserWithPermissionCheck operatingUser = operatingUserOrOperator as UserWithPermissionCheck;
                if (!operatingUser.IfModerator(forumId))
                {
                    if (!operatingUser.IfForumAdmin)
                        ExceptionHelper.ThrowForumOnlyModeratorsOrAdminstratorsHavePermissionException();
                }
            }
        }
        #endregion Public Static Function CheckAdminPanelCommonPermission

        #region Public Static Functiion CheckModeratorPanelCommonPermission
        public static void CheckModeratorPanelCommonPermission(UserOrOperator operatingUserOrOperator, int forumId)
        {
            CheckUserOrOperator(operatingUserOrOperator);

            if (CommFun.IfOperator(operatingUserOrOperator))
            {
                OperatorWithPermissionCheck operatingOperator = operatingUserOrOperator as OperatorWithPermissionCheck;
                if (!operatingOperator.IfModerator(forumId))// if not moderator
                {
                    //if (!operatingOperator.IfAdmin)
                    //{
                    //    if (!operatingUserOrOperator.IfForumAdmin)
                    ExceptionHelper.ThrowForumOnlyModeratorsHavePermissionException();
                    //}
                }
            }
            else
            {
                UserWithPermissionCheck operatingUser = operatingUserOrOperator as UserWithPermissionCheck;
                if (!operatingUser.IfModerator(forumId))
                {
                    //if (!operatingUser.IfForumAdmin)
                    ExceptionHelper.ThrowForumOnlyModeratorsHavePermissionException();
                }
            }
        }
        #endregion Public Static Functiion CheckModeratorPanelCommonPermission

        #region Public Static Function IfBannedInUI
        public static bool IfBannedInUI(UserOrOperator operatingUserOrOperator, int forumId)
        {
            /*2.0 only effect for users and operators */
            //bool ifBanned = false;
            if (operatingUserOrOperator.IfForumAdmin) // if forum admin
            {
                //ifBanned = false;                
                return false;
            }

            if (CommFun.IfOperator(operatingUserOrOperator)) // if system admin
            {
                OperatorWithPermissionCheck operatingOperator = operatingUserOrOperator as OperatorWithPermissionCheck;
                if (operatingOperator.IfAdmin)//ifBanned = false;
                    return false;
            }
            //bool ifModerator = false;
            //if (forumId == 0) ifModerator = operatingUserOrOperator.IfModerator();
            //else ifModerator = operatingUserOrOperator.IfModerator(forumId);
            //if (ifModerator)// if moderator
            //{
            //    //ifBanned = false;
            //    return false;
            //}
            //else// if neither admin nor moderator
            //{
            if (operatingUserOrOperator.IfBanById())
            {//ifBanned = true;
                return true;
            }
            //}
            return false;
            //return ifBanned;
        }
        public static void CheckIfUserOrOperatorBanned(UserOrOperator operatingUserOrOperator, int forumId)
        {
            if (IfBannedInUI(operatingUserOrOperator, forumId))
                ExceptionHelper.ThrowForumBanCannotAddWithUserId(operatingUserOrOperator.Id); 
               
        }
        #endregion Public Static Function IfBannedInUI

        #region Public Static Function CheckUserPanelCommonPermission
        public static void CheckUserPanelCommonPermission(UserOrOperator operatingUserOrOperator)
        {
            CheckUserOrOperator(operatingUserOrOperator);
            if (IfBannedInUI(operatingUserOrOperator, 0))
                ExceptionHelper.ThrowForumUseOrOperatorHaveBeenBannedException(operatingUserOrOperator.DisplayName);
        }
        #endregion Public Static Function CheckUserPanelCommonPermission

        #region Public Static Function CheckCommonPermissionInUI
        public static void CheckCommonPermissionInUI(UserOrOperator operatingUserOrOperator)
        {
            CheckUserOrOperator(operatingUserOrOperator);
            if (IfBannedInUI(operatingUserOrOperator, 0))
                ExceptionHelper.ThrowForumUseOrOperatorHaveBeenBannedException(operatingUserOrOperator.DisplayName);
        }

        public static bool IfAdminInUI(UserOrOperator operatingUserOrOperator)
        {
            if (operatingUserOrOperator == null)
                return false;

            CheckUserOrOperator(operatingUserOrOperator);

            if (operatingUserOrOperator.IfForumAdmin)
                return true;
            if (CommFun.IfOperator(operatingUserOrOperator))
            {
                OperatorWithPermissionCheck operatingOperator = operatingUserOrOperator as OperatorWithPermissionCheck;
                if (operatingOperator.IfAdmin)
                    return true;
            }

            return false;
        }

        public static bool IfModeratorInUI(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId, UserOrOperator operatingUserOrOperator)
        {
            if (operatingUserOrOperator == null)
                return false;
            ModeratorsWithPermisisonCheck moderators = new ModeratorsWithPermisisonCheck(conn, transaction, forumId, operatingUserOrOperator);
            return moderators.IfModerator(operatingUserOrOperator.Id);
            // Moderator[] moderator = moderators.GetAllModerators();

            //bool ifModerator = false;
            //for (int i = 0; i < moderator.Length; i++)
            //{
            //    if (operatingUserOrOperator != null)
            //    {
            //        if (moderator[i].Id == operatingUserOrOperator.Id)
            //        {
            //            ifModerator = true;
            //            break;
            //        }
            //    }
            //}
            //return ifModerator;
        }

        public static bool IfModeratorOfSiteInUI(UserOrOperator operatingUserOrOperator)
        {
            if (operatingUserOrOperator == null)
                return false;
            return operatingUserOrOperator.IfModerator();
        }
        #endregion

        #region Common New Or Edit Topic/Post Permission Check
        public static void CommonPostOrTopicNewOrEditPermissionWithNoPostModerationCheck(
            PostsOfUserOrOperatorWithPermissionCheck postsOfUser,ref string content, int forumId
            , UserOrOperator operatingUserOrOperator, bool ifModerator)
        {
            /*If Allow View Forum Permission*/
            CommFun.UserPermissionCache().CheckIfAllowViewForumPermission(forumId, operatingUserOrOperator, ifModerator);
            /*If Allow View Topic/Post*/
            CommFun.UserPermissionCache().CheckIfAllowViewTopicPermission(forumId, operatingUserOrOperator, ifModerator);
            /*If Allow Post Topic/Post*/
            CommFun.UserPermissionCache().CheckIfAllowPostTopicOrPostPermission(forumId, operatingUserOrOperator, ifModerator);
            /*Min Interval Time for Posting*/
            CommFun.UserPermissionCache().CheckMinIntervalTimeForPostingPermission(
                postsOfUser.GetLastPostTime(operatingUserOrOperator), forumId,
                operatingUserOrOperator, ifModerator);
            /*Max Length of Topic/Post*/
            CommFun.UserPermissionCache().CheckMaxLengthOfTopicOrPost(forumId, content.Length, operatingUserOrOperator, ifModerator);
            /*Allow Url*/
            bool ifAllowUrl = CommFun.UserPermissionCache().IfAllowURLPermission(forumId, operatingUserOrOperator, ifModerator);
            if (!ifAllowUrl)
                content = CommFun.NoPermissionALink(true, content);
            /*Allow Image*/
            bool ifAllowImage = CommFun.UserPermissionCache().IfAllowInsertImagePermission(forumId, operatingUserOrOperator, ifModerator);
            if (!ifAllowImage)
                content = CommFun.NoPermissionImg(content);
            ///*Allow Html*/
            //bool ifAllowHtml = CommFun.UserPermissionCache().IfAllowHtmlPermission(forumId, operatingUserOrOperator, ifModerator);
            //if (!ifAllowHtml)
            //    content = System.Web.HttpUtility.HtmlEncode(content);
        }
        public static void CommonAddAttachmentPermissionCheck(int forumId, UserOrOperator operatingUserOrOperator,
            int attachmentsOfPostCount,
            byte[] attachment, Guid guid, AttachmentsOfUserOrOperatorWithPermissionCheck attachementsOfUser, bool ifModerator)
        {
            /*If Allow View Forum Permission*/
            CommFun.UserPermissionCache().CheckIfAllowViewForumPermission(forumId, operatingUserOrOperator, ifModerator);
            /*If Allow View Topic/Post*/
            CommFun.UserPermissionCache().CheckIfAllowViewTopicPermission(forumId, operatingUserOrOperator, ifModerator);
            /*If Allow Post Topic/Post*/
            CommFun.UserPermissionCache().CheckIfAllowPostTopicOrPostPermission(forumId, operatingUserOrOperator, ifModerator);

            /*Allow Attach*/
            CommFun.UserPermissionCache().CheckIfAllowAttachmentPermission(operatingUserOrOperator,ifModerator);
            /*Max Size Of each attachment*/
            CommFun.UserPermissionCache().CheckMaxSizeoftheAttachmentPermission(attachment.Length,
                operatingUserOrOperator,ifModerator);
            /*Max Attachments In One Post*/
            int CountOfAttachementsInOnePost = attachmentsOfPostCount;
                //attachementsOfUser.GetCountOfAllTempAttachments(
                //operatingUserOrOperator, guid, Framework.Enum.Forum.EnumAttachmentType.AttachToPost);
            CommFun.UserPermissionCache().CheckMaxAttachmentsinOnePostPermission(CountOfAttachementsInOnePost,
                operatingUserOrOperator, ifModerator);
            /*Attachments Size */
            int SizeOfAllAttachments = attachementsOfUser.GetSizeOfAllTempAttachments(operatingUserOrOperator,
                guid, Framework.Enum.Forum.EnumAttachmentType.AttachToPost);
            CommFun.UserPermissionCache().CheckMaxSizeofalltheAttachmentsPermission(SizeOfAllAttachments,
                operatingUserOrOperator, ifModerator);
        }
        #endregion

        #region Common Edit Topic/Post 
        public static void CheckPostStatusWhenEditTopicOrPost(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            PostWithPermissionCheck post,int forumId, UserOrOperator operatingUserOrOperator)
        {

            if (post.ModerationStatus == EnumPostOrTopicModerationStatus.Rejected
                && !CommFun.IfAdminInUI(operatingUserOrOperator)
                && !CommFun.IfModeratorInUI(conn, transaction, forumId, operatingUserOrOperator))
            {
                ExceptionHelper.ThrowForumPostModerationStautsIsRejected();
            }
            if (post.GetAbuseStatus() == EnumPostAbuseStatus.AbusedAndApproved)
            {
                ExceptionHelper.ThrowForumPostAbuseStautsIsAbusedAndApprovedException();
            }
        }
        #endregion 

        #region Common Edit Signature Permission Check
        public static void CommonEditSignaturePermissionWithImageOrLinkCheck(
            ref string content, UserOrOperator operatingUserOrOperator)
        {
            //bool ifSignatureAllowHtml = CommFun.UserPermissionCache().IfSignatureAllowHtmlPermission
            //    (operatingUserOrOperator);
            //if (!ifSignatureAllowHtml)
            //    content = System.Web.HttpUtility.HtmlEncode(content);

            bool ifSigntureAllowURL = CommFun.UserPermissionCache().IfSignatureAllowURLPermission
                (operatingUserOrOperator);
            if (!ifSigntureAllowURL)
                content = CommFun.NoPermissionALink(true, content);

            bool ifSigntureAllowImage = CommFun.UserPermissionCache().IfSignatureAllowInsertImagePermission
                (operatingUserOrOperator);
            if (!ifSigntureAllowImage)
                content = CommFun.NoPermissionImg(content);
        }
        #endregion

        #region Public Static Function Check In Forum
        public static bool IfForumIsClosedInTopicPage(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            UserOrOperator operatingUserOrOperator,
            int TopicId)
        {
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(conn, transaction, TopicId, operatingUserOrOperator);
            return IfForumIsClosedInForumPage(conn, transaction, operatingUserOrOperator, topic.ForumId);
        }

        public static bool IfForumIsClosedInForumPage(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            UserOrOperator operatingUserOrOperator, int ForumId)
        {
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(conn, transaction, ForumId, operatingUserOrOperator);

            bool ifForumClosed = false;
            if (forum.Status != EnumForumStatus.Open)
            {
                ifForumClosed = true;
            }
            return ifForumClosed;
        }
        public static bool IfForumIsClosedInForumPage(ForumWithPermissionCheck forum)
        {
            bool ifForumClosed = false;
            if (forum.Status != EnumForumStatus.Open)
            {
                ifForumClosed = true;
            }
            return ifForumClosed;
        }
        #endregion

        public static DateTime LastSearchtime()
        {
            if (HttpContext.Current.Session["LastSearchTime"] == null)
                return new DateTime();
            else
                return (DateTime)HttpContext.Current.Session["LastSearchTime"];
        }

        public static UserPermissionCache UserPermissionCache()
        {
            if (HttpContext.Current.Session["UserPermissionList"] != null)
                return HttpContext.Current.Session["UserPermissionList"] as UserPermissionCache;
            return null;
        }

        public static string StripHtml(string strHtml)
        {
            string[] aryReg = {
                                 @"<head[^>]*>[\s\S]*?</[^>]*head>",
                                 @"<script[^>]*>[\s\S]*?</[^>]*script>",
                                 @"<style[^>]*>[\s\S]*?</[^>]*style>",
                                 @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""''])(\\[""''tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
                                 @"([\r\n])[\s]+",
                                 @"&(quot|#34);",
                                 @"&(amp|#38);",
                                 @"&(lt|#60);",
                                 @"&(gt|#62);",
                                 @"&(nbsp|#160);",
                                 @"&(iexcl|#161);",
                                 @"&(cent|#162);",
                                 @"&(pound|#163);",
                                 @"&(copy|#169);",
                                 @"&#(\d+)",
                                 @"<!--.*\n-->"

                              };
            string[] aryRep = {
                                 "",
                                 "",
                                 "",
                                 "",
                                 "",
                                 "\"",
                                 "&",
                                 "<",
                                 ">",
                                 "",
                                 "\xa1",
                                 "\xa2",
                                 "\xa3",
                                 "\xa9",
                                 "",
                                 ""
                              };
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);

            }

            return strOutput;
        }

        public static string NoPermissionImg(string html)
        {
            string reg = @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>";
            Regex regex = new Regex(reg, RegexOptions.IgnoreCase);
            Match result = regex.Match(html);
            string output = html;
            while (result.Success)
            {
                output = regex.Replace(output, HttpUtility.HtmlEncode(result.ToString()));
                result = result.NextMatch();
            }
            return output;

        }

        public static string NoPermissionALink(bool IsPermissionImg, string html)
        {
            string[] reg = {
                              @"<a\b[^<>]*?\bhref[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<aUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>(.[\s\t\r\n]*)*</a>",
                              @"&lt;img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*[^a]&gt;" 
                           };
            string output = html;
            Regex regex1 = new Regex(reg[0], RegexOptions.IgnoreCase);
            Match result1 = regex1.Match(output);
            while (result1.Success)
            {
                output = regex1.Replace(output, HttpUtility.HtmlEncode(result1.ToString()));
                result1 = result1.NextMatch();
            }
            if (IsPermissionImg)
            {
                Regex regex2 = new Regex(reg[1], RegexOptions.IgnoreCase);
                Match result2 = regex2.Match(output);
                while (result2.Success)
                {
                    output = regex2.Replace(output, HttpUtility.HtmlDecode(result2.ToString()));
                    result2 = result2.NextMatch();
                }

            }
            return output;
        }

    }
}
