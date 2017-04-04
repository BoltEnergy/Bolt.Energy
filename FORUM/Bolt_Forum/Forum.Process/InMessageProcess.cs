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
   public class InMessageProcess
   {
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
               //FavoritesWithPermissionCheck favorites = new FavoritesWithPermissionCheck(conn, transaction, operatingUserOrOperator, operatingUserOrOperator.Id);
               //favorites.Delete(operatingUserOrOperator, topicId);
               InMessagesOfRecieverWithPermissionCheck inMessages = new InMessagesOfRecieverWithPermissionCheck(conn, transaction, operatingUserOrOperator, operatingUserOrOperator.Id);
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

    }
}
