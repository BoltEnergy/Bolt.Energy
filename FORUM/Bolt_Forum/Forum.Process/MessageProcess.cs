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
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
namespace Com.Comm100.Forum.Process
{
   public class MessageProcess
   {
       public static InMessageWithPermissionCheck GetInMessageById(int siteId, int operatingUserOrOperatorId,
           int id)
       {
           SqlConnectionWithSiteId conn = null;

           try
           {
               conn = DbHelper.GetSqlConnection(siteId);
               DbHelper.OpenConn(conn);
               UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
               InMessageWithPermissionCheck inMessage = new InMessageWithPermissionCheck(conn,null,operatingUserOrOperator,id);
               return inMessage;
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
       public static InMessageWithPermissionCheck[] GetInMessagesByIdAndPaging(int siteId, int operatingUserOrOperatorId, out int count, int pageIndex, int pageSize)
       {
           SqlConnectionWithSiteId conn = null;
          
           try 
           {
               conn = DbHelper.GetSqlConnection(siteId);
               DbHelper.OpenConn(conn);
               UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
               InMessagesOfRecieverWithPermissionCheck  MyInMessages = operatingUserOrOperator.GetInMessages();
               return MyInMessages.GetMessagesByPaging(out count, pageIndex, pageSize);             
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
       public static void DeleteInMessage(int siteId, int operatingUserOrOperatorId, int inMessageId)
       {
           SqlConnectionWithSiteId conn = null;
           SqlTransaction transaction = null;
           try
           {
               conn = DbHelper.GetSqlConnection(siteId);
               DbHelper.OpenConn(conn);
               UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
               InMessagesOfRecieverWithPermissionCheck inMessages = new InMessagesOfRecieverWithPermissionCheck(conn, transaction, operatingUserOrOperator, operatingUserOrOperator.Id);
               inMessages.Delete(operatingUserOrOperator, inMessageId);
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
       public static void UpdateIfView(int siteId, int operatingUserOrOperatorId, int inMessageId)
       {
           SqlConnectionWithSiteId conn = null;
           SqlTransaction transaction = null;
           try
           {
               conn = DbHelper.GetSqlConnection(siteId);
               DbHelper.OpenConn(conn);
               UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
               InMessagesOfRecieverWithPermissionCheck inMessages = new InMessagesOfRecieverWithPermissionCheck(conn, transaction, operatingUserOrOperator, operatingUserOrOperator.Id);
               inMessages.UpdateIfView(operatingUserOrOperator, inMessageId);
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

       public static void SendMessage(int siteId, int operatingUserOrOperatorId, int recieverId, string subject, string message)
       {
           SqlConnectionWithSiteId conn = null;
           SqlTransaction transaction = null;
           try
           {
               subject = subject.Trim();
               message = message.Trim();
               conn = DbHelper.GetSqlConnection(siteId);
               DbHelper.OpenConn(conn);
               transaction = conn.SqlConn.BeginTransaction();
               UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
               operatingUserOrOperator.SendMessage(recieverId, subject, message, DateTime.UtcNow);
               transaction.Commit();
           }
           catch (System.Exception)
           {
               DbHelper.RollbackTransaction(transaction);
               throw;
           }
           finally
           {
               DbHelper.CloseConn(conn);
           }
       }

       public static void SendMessage(int siteId, int operatingUserOrOperatorId,
            string subject, string message,
            int fromUserOrOperatorId, List<int> toUserOrOperatorIds, List<int> toReputationGroups,List<int> toUserGroups,
            bool ifAdminGroup, bool ifModeratorGroup)
       {
           SqlConnectionWithSiteId conn = null;
           SqlTransaction transaction = null;
           try
           {
               subject = subject.Trim();
               message = message.Trim();
               conn = DbHelper.GetSqlConnection(siteId);
               DbHelper.OpenConn(conn);
               transaction = conn.SqlConn.BeginTransaction();
               UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
               if (operatingUserOrOperator.IfForumAdmin)
               {
                   AdministratorWithPermissionCheck administrator = new AdministratorWithPermissionCheck(conn, transaction,
                       operatingUserOrOperator, operatingUserOrOperatorId);
                   administrator.SendMessage(toUserGroups, toReputationGroups, toUserOrOperatorIds, ifAdminGroup, ifModeratorGroup,
                       subject, message, DateTime.UtcNow, operatingUserOrOperator);
               }
               else
               {
                   OperatorWithPermissionCheck operatoringOperator = operatingUserOrOperator as OperatorWithPermissionCheck;
                   operatoringOperator.SendMessage(toUserGroups, toReputationGroups, toUserOrOperatorIds, ifAdminGroup, ifModeratorGroup,
                       subject, message, DateTime.UtcNow, operatingUserOrOperator);
                   
               }
               transaction.Commit();
           }
           catch (System.Exception)
           {
               DbHelper.RollbackTransaction(transaction);
               throw;
           }
           finally
           {
               DbHelper.CloseConn(conn);
           }
       }

       public static OutMessageWithPermissionCheck[] GetOutMessagesByIdAndPaging(int siteId, int operatingUserOrOperatorId, out int count, int pageIndex, int pageSize)
       {
           SqlConnectionWithSiteId conn = null;

           try
           {
               conn = DbHelper.GetSqlConnection(siteId);
               DbHelper.OpenConn(conn);
               UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
               OutMessagesOfSenderWithPermissionCheck MyOutMessages = operatingUserOrOperator.GetOutMessages();
               return MyOutMessages.GetMessagesByPaging(out count, pageIndex, pageSize);
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
       public static void DeleteOutMessage(int siteId, int operatingUserOrOperatorId, int outMessageId)
       {
           SqlConnectionWithSiteId conn = null;
           SqlTransaction transaction = null;
           try
           {
               conn = DbHelper.GetSqlConnection(siteId);
               DbHelper.OpenConn(conn);
               UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
               OutMessagesOfSenderWithPermissionCheck outMessages = new OutMessagesOfSenderWithPermissionCheck(conn, transaction, operatingUserOrOperator, operatingUserOrOperator.Id);
               outMessages.Delete(outMessageId);
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

       public static UserOrOperator[] GetRecieversOfOutMessage(int siteId, int operatingUserOrOperatorId, int outMessageId)
       {
           SqlConnectionWithSiteId conn = null;

           try
           {
               conn = DbHelper.GetSqlConnection(siteId);
               DbHelper.OpenConn(conn);
               UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
               OutMessageWithPermissionCheck outMessage = new OutMessageWithPermissionCheck(conn, null, operatingUserOrOperator, outMessageId);
               UsersOrOperatorsOfOutMessage usersAndOperators = outMessage.GetRecieveUsersAndOperators();
               return usersAndOperators.GetAllUsersOrOperators(operatingUserOrOperator);
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

       public static UserGroupWithPermissionCheck[] GetUserGroupsOfOutMessage(int siteId, int operatingUserOrOperatorId, int outMessageId)
       {
           SqlConnectionWithSiteId conn = null;
           try
           {
               conn = DbHelper.GetSqlConnection(siteId);
               DbHelper.OpenConn(conn);
               UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
               OutMessageWithPermissionCheck outMessage = new OutMessageWithPermissionCheck(conn, null, operatingUserOrOperator, outMessageId);
               UserGroupsOfOutMessage userGroups = outMessage.GetUserGroups();
               return userGroups.GetAllGroups(operatingUserOrOperator);     
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

       public static UserReputationGroupWithPermissionCheck[] GetUserReputationGroupsOfOutMessage(
           int siteId, int operatingUserOrOperatorId, int outMessageId)
       {
           SqlConnectionWithSiteId conn = null;
           try
           {
               conn = DbHelper.GetSqlConnection(siteId);
               DbHelper.OpenConn(conn);
               UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
               OutMessageWithPermissionCheck outMessage = new OutMessageWithPermissionCheck(conn, null, operatingUserOrOperator, outMessageId);
               UserReputationGroupsOfOutMessage userReputationGroups = outMessage.GetUserReputationGroups();
               return userReputationGroups.GetAllGroups(operatingUserOrOperator);
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

       public static int GetCountOfUnReadInMessages(int siteId, int operatingUserOrOperatorId)
       {
           SqlConnectionWithSiteId conn = null;
           try
           {
               conn = DbHelper.GetSqlConnection(siteId);
               DbHelper.OpenConn(conn);
               UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
               InMessagesOfRecieverWithPermissionCheck inMessages = new InMessagesOfRecieverWithPermissionCheck(
                   conn, null, operatingUserOrOperator, operatingUserOrOperatorId);
               return inMessages.GetCountOfUnReadInMessages();
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

    }
}
