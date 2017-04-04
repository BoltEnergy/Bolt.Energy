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
    public class PostImageProcess
    {
        public static PostImageWithPermissionCheck GetPostImageById(int siteId, int operatingUserOrOperatorId, bool ifOperator, int imageId)
        {
            SqlConnectionWithSiteId conn = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                PostImageWithPermissionCheck postImage = new PostImageWithPermissionCheck(conn, null, imageId, operatingUserOrOperator);

                return postImage;
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

        public static int AddPostImage(int siteId, int operatingUserOrOperatorId, bool ifOperator, int postId,int useType, string name, string type, byte[] file)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            DateTime postTime = DateTime.UtcNow;

            name = name.Trim();
            type = type.Trim();

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);

                PostImagesWithPermissionCheck postImages = new PostImagesWithPermissionCheck(conn, transaction, postId, useType, operatingUserOrOperator);

                int imageId = postImages.Add(name, type, file);

                transaction.Commit();

                return imageId;
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

    }
}
