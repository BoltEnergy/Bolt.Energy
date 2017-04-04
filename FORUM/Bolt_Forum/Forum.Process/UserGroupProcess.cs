#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.Bussiness;

namespace Com.Comm100.Forum.Process
{
    public class UserGroupProcess
    {        
        public static bool IfExistUserGroupName(string name, int siteId)
        {
            SqlConnectionWithSiteId conn = null;
            name = name.Trim();
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserGroupsWithPermissionCheck userGroups = new UserGroupsWithPermissionCheck(conn, null, null);
                int userGroupsCout = userGroups.GetCountOfUserGroupsByName(name);
                if (userGroupsCout == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
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
        public static int AddGroup(string name, string description, int siteId, int operatingOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                name = name.Trim();
                description = description.Trim();

                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingOperatorId);

                UserGroupsWithPermissionCheck userGroups = new UserGroupsWithPermissionCheck(conn, transaction, userOrOperator);
                int result =userGroups.Add(name, description);
                transaction.Commit();

                return result;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }            
        }


      
        public static void AddUserGroupToForum(int groupId, int forumId, int siteId, int operatingOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();
                
                UserOrOperator user = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingOperatorId);
                UserGroupsOfForumWithPermissionCheck userGroupsOfForum = new UserGroupsOfForumWithPermissionCheck(conn, transaction, user, forumId);
                userGroupsOfForum.Add(groupId);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

       
        public static void Delete(int groupId, int forumId, int siteId, int operatingOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingOperatorId);
                UserGroupsOfForumWithPermissionCheck userGroupsOfForum = new UserGroupsOfForumWithPermissionCheck(conn, transaction, userOrOperator, forumId);
                userGroupsOfForum.Delete(groupId);

                //UserGroupWithPermissionCheck ug =new UserGroupWithPermissionCheck(conn, transaction, userOrOperator, groupId);
                //ug.Delete(forumId);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void UpdateGroup(int siteId, int operatingUserOrOperatorId, int id, string name, string description)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                name = name.Trim();
                description = description.Trim();
                
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UserGroupWithPermissionCheck ug =
                    new UserGroupWithPermissionCheck(conn, transaction, operatingUserOrOperator, id);
                ug.Update(name, description);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }     
        }

        public static void Delete(int siteId, int operatingUserOrOperatorId, int groupId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UserGroupsWithPermissionCheck ugs =
                    new UserGroupsWithPermissionCheck(conn, transaction, operatingUserOrOperator);
                ugs.Delete(groupId);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }     
        }

        

        public static UserGroupWithPermissionCheck[] GetAllUserGroups(int siteId, int operatingOperatorId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);

                DbHelper.OpenConn(conn);

                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingOperatorId);

                UserGroupsWithPermissionCheck ug =
                    new UserGroupsWithPermissionCheck(conn, null, userOrOperator);

                return ug.GetAllUserGroups(userOrOperator);

            }
            catch
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }     
        }

        public static UserGroupWithPermissionCheck[] GetUserGroupsByForumId(int forumId, int siteId, int operatingOperatorId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);

                DbHelper.OpenConn(conn);

                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingOperatorId);

                UserGroupsWithPermissionCheck ugs =
                    new UserGroupsWithPermissionCheck(conn, null, userOrOperator);

                return ugs.GetGroupsByForumId(forumId, userOrOperator);

            }
            catch
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static UserGroupWithPermissionCheck GetGroupById(int siteId, int operatingUserOrOperatorId, int groupId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);

                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                return new UserGroupWithPermissionCheck(conn, null, operatingUserOrOperator, groupId);
            }
            catch
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static UserGroupPermissionWithPermissionCheck GetGroupPermissionByGroupId(int groupId, int siteId, int operatingOperatorId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator user = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingOperatorId);

                UserGroupWithPermissionCheck group = new UserGroupWithPermissionCheck(conn, null, user, groupId);

                return group.GetPermission();
            }
            catch
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static UserGroupPermissionForForumWithPermissionCheck GetUserGroupPermissionForForum(int siteId, int operatingUserOrOperatorId, int groupId, int forumId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UserGroupOfForumWithPermissionCheck userGroupOfForum = new UserGroupOfForumWithPermissionCheck(conn, transaction, operatingUserOrOperator, forumId, groupId);
                return userGroupOfForum.GetPermission();

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

        public static int GetCountOfMembersByUserGroupId(int siteId, int operatingUserOrOperatorId, int groupId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);

                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                UserGroupWithPermissionCheck userGroup = new UserGroupWithPermissionCheck(conn, null, operatingUserOrOperator, groupId);
                MembersOfUserGroupWithPermissionCheck members = userGroup.GetMembers();

                return members.GetCount();
            }
            catch
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void UpdateGroupPermission(
            int groupId, int siteId, bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost,
            bool ifAllowCustomizeAvatar, int maxLengthofSignature, 
           bool ifSignatureAllowUrl,bool ifSignatureAllowInsertImage,
            int minIntervalForPost,
            int maxLengthOfPost, bool ifAllowUrl, bool ifAllowUploadImage, bool ifAllowUploadAttachment,
            int maxCountOfAttacmentsForOnePost, int maxSizeOfOneAttachments, int maxSizeOfAllAttachments, int maxCountOfMessageSendOneDay,
            bool ifAllowSearch, int minIntervalForSearch, bool ifPostNotNeedModeration,int operatingOperatorId)
        {
            SqlConnectionWithSiteId conn = null;

            SqlTransaction transaction = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingOperatorId);
                UserGroupWithPermissionCheck userGroup = new UserGroupWithPermissionCheck(conn, transaction, operatingUserOrOperator, groupId);

                UserGroupPermissionWithPermissionCheck groupPermission = userGroup.GetPermission();

                groupPermission.Update(ifAllowViewForum, ifAllowViewTopic, ifAllowPost, ifAllowCustomizeAvatar,
                maxLengthofSignature,ifSignatureAllowUrl,ifSignatureAllowInsertImage,
                minIntervalForPost, maxLengthOfPost, ifAllowUrl, ifAllowUploadImage, ifAllowUploadAttachment, 
                maxCountOfAttacmentsForOnePost, maxSizeOfOneAttachments, maxSizeOfAllAttachments, maxCountOfMessageSendOneDay, ifAllowSearch, 
                minIntervalForSearch, ifPostNotNeedModeration);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static UserGroupWithPermissionCheck[] GetUserGroupsExceptAllForumUser(int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);

                DbHelper.OpenConn(conn);

                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                UserGroupsWithPermissionCheck groups =
                    new UserGroupsWithPermissionCheck(conn, null, userOrOperator);

                return groups.GetUserGroupsExceptAllForumUser(userOrOperator);

            }
            catch
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }     
        }

        public static UserGroupWithPermissionCheck[] GetUserGroupsWhichContainExistUser(int siteId, int operatingUserOrOperatorId, int userId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);

                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                UserWithPermissionCheck user = new UserWithPermissionCheck(conn, null, userId, 0, operatingUserOrOperator);
                UserGroupsOfUserOrOperatorWithPermissionCheck userGroups = user.GetUserGroups();

                return userGroups.GetAllUserGroups();

            }
            catch
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }     
        }

        public static UserGroupWithPermissionCheck[] GetGroupsNotInForum(int forumId, int siteId, int operatingUserOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);

                DbHelper.OpenConn(conn);

                UserOrOperator user = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);

                UserGroupsWithPermissionCheck group = new UserGroupsWithPermissionCheck(conn, null, user);

                return group.GetGroupsNotInForum(forumId, user);
            }
            catch 
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static MemberOfUserGroupWithPermissionCheck[] GetMembersOfUserGroupByQueryAndPaging(int siteId, int operatingUserOrOperatorId, int userGroupId, int pageIndex, int pageSize, string emailOrDisplayNameKeyword, string orderField, out int recordCount)
        {
            SqlConnectionWithSiteId conn = null;
            try
            {
                emailOrDisplayNameKeyword = emailOrDisplayNameKeyword.Trim();
                orderField = orderField.Trim();
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                UserGroupWithPermissionCheck userGroup = new UserGroupWithPermissionCheck(conn, null, operatingUserOrOperator, userGroupId);
                MembersOfUserGroupWithPermissionCheck members = userGroup.GetMembers();
                return members.GetMembersByQueryAndPaging(pageIndex, pageSize, emailOrDisplayNameKeyword, orderField, out recordCount);
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

        public static void DeleteUserOrOperatorFromUserGroup(int siteId, int operatingUserOrOperatorId, int userGroupId, int userOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UserGroupWithPermissionCheck userGroup = new UserGroupWithPermissionCheck(conn, transaction, operatingUserOrOperator, userGroupId);
                MembersOfUserGroupWithPermissionCheck members = userGroup.GetMembers();
                members.Delete(userOrOperatorId);
                transaction.Commit();
            }
            catch (Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static UserOrOperator[] GetNotDeletedAndNotBannedUsersOrOperatorsWhichisNotInUserGroupByQueryAndPaging(int siteId, int operatingUserOrOperatorId,
            int userGroupId, int pageIndex, int pageSize, string emailOrDisplayNameKeyword, EnumUserType userType, bool ifGetAll, bool ifGetAdmin, out int recordsCount, string orderField, string orderDirection)
        {
            SqlConnectionWithSiteId conn = null;
            try
            {
                emailOrDisplayNameKeyword = emailOrDisplayNameKeyword.Trim();
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                UsersOrOperatorsOfSite usersOrOperators = new UsersOrOperatorsOfSite(conn, null);
                return usersOrOperators.GetNotDeletedAndNotBannedUsersOrOperatorsWhichisNotInUserGroupByQueryAndPaging(userGroupId, pageIndex, pageSize, emailOrDisplayNameKeyword,
                    userType, ifGetAll, ifGetAdmin, out recordsCount, operatingUserOrOperator, orderField, orderDirection);
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

        public static void AddUserOrOperatorToUserGroup(int siteId, int operatingUserOrOperatorId, int userGroupId, int userOrOperatorId)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                UserGroupWithPermissionCheck userGroup = new UserGroupWithPermissionCheck(conn, transaction, operatingUserOrOperator, userGroupId);
                MembersOfUserGroupWithPermissionCheck members = userGroup.GetMembers();
                members.Add(userOrOperatorId);
                transaction.Commit();
            }
            catch (Exception)
            {
                DbHelper.RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
        
    }
}
