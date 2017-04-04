#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2009                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Database;
using System.Data.SqlClient;

namespace Com.Comm100.Forum.Process
{
    public class ProcessUtil
    {
        public static UserOrOperator GetUserOrOperator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int operatingOperatorId)
        {
            UserOrOperator operatingOperator = null;
            if (operatingOperatorId > 0)
            {

                UserWithPermissionCheck user = new UserWithPermissionCheck(conn, transaction, operatingOperatorId, operatingOperator);//(conn, transaction, operatingOperatorId);

                if (user.IfDeleted)
                {
                    ExceptionHelper.ThrowUserHasBeenDeletedWithIdException(operatingOperator.Id);
                }
                else if (!user.IfActive)
                {
                    ExceptionHelper.ThrowUserNotActiveWithIdException(operatingOperator.Id);
                }
                operatingOperator = user;
                
            }

            return operatingOperator;
        }
        public static OperatorWithPermissionCheck GetOperator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int operatingOperatorId)
        {
            OperatorWithPermissionCheck operatingOperator = null;
            if (operatingOperatorId > 0)
            {
                operatingOperator = new OperatorWithPermissionCheck(conn, transaction, operatingOperatorId);

                if (operatingOperator.IfDeleted)
                {
                    ExceptionHelper.ThrowOperatorHasBeenDeletedWithIdException(operatingOperator.Id);
                }
                else if (!operatingOperator.IfActive)
                {
                    ExceptionHelper.ThrowOperatorNotActiveWithIdException(operatingOperator.Id);
                }
            }
            return operatingOperator;
        }

    }
}
