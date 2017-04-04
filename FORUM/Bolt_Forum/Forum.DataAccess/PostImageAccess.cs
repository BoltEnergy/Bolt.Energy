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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.DataAccess
{
    public class PostImageAccess
    {
        public static DataTable GetPostImageById(SqlConnectionWithSiteId conn, SqlTransaction transaction, int imageId)
        {

            StringBuilder strSQL = new StringBuilder("select * from t_Forum_Images" + conn.SiteId.ToString());
            strSQL.Append(" where Id=@ImageId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@ImageId", imageId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        public static int AddPostImage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId, string name, string type, byte[] image,DateTime uploadTime,int useType)
        {
            StringBuilder strSQL = null;

            SqlCommand cmd = null;

            strSQL = new StringBuilder(" Insert into t_Forum_Images" + conn.SiteId.ToString() + " ([Name],[PostId],[Type],[Image],[UploadTime],[UseType])");
            strSQL.Append(" values (@Name,@PostId,@Type,@Image,@UploadTime,@UseType);");
            strSQL.Append("select @@identity");

            cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@PostId", postId);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@Image", image);
            cmd.Parameters.AddWithValue("@UploadTime", uploadTime);
            cmd.Parameters.AddWithValue("@UseType", useType);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void DeletePostImages(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId,int useType)
        {

            StringBuilder strSQL = new StringBuilder("Delete t_Forum_Images" + conn.SiteId + " where PostId = @PostId And UseType=@UseType ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@PostId", postId);
            cmd.Parameters.AddWithValue("@UseType", useType);

            cmd.ExecuteNonQuery();
        }

        public static void DeleteNotUsedPostImages(SqlConnectionWithSiteId conn, SqlTransaction transaction,DateTime nowTime)
        {

            StringBuilder strSQL = new StringBuilder("Delete t_Forum_Images" + conn.SiteId + " where UseType=0 And datediff(day,UploadTime,@NowTime)>=2 ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@NowTime", nowTime);

            cmd.ExecuteNonQuery();
        }

        public static void AttachToPost(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId,int useType,int[] imageIds)
        {
            if (imageIds != null && imageIds.Length > 0)
            {
                StringBuilder strSQL;
                SqlCommand cmd;
                for (int i = 0; i < imageIds.Length; i++)
                {

                    strSQL = new StringBuilder("Update t_Forum_Images" + conn.SiteId + " Set PostId=@PostId,UseType=@UseType where UseType=0 And Id=@ImageId ");
                    cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
                    cmd.Parameters.AddWithValue("@PostId", postId);
                    cmd.Parameters.AddWithValue("@UseType", useType);
                    cmd.Parameters.AddWithValue("@ImageId", imageIds[i]);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ClearNoUsedAfterAttach(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId,int useType, int[] imageIds)
        {
            StringBuilder strSQL;
            SqlCommand cmd;
            string imageIdString = "";
            if (imageIds != null)
            {
                foreach (int imageId in imageIds)
                {
                    if (imageIdString.Length > 0) imageIdString += ",";
                    imageIdString += imageId;
                }
            }

            if (imageIdString.Length > 0)
            {

                strSQL = new StringBuilder("Delete t_Forum_Images" + conn.SiteId + " where PostId=@PostId And UseType=@UseType And Id not in (" + imageIdString + ") ");
                cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
                cmd.Parameters.AddWithValue("@PostId", postId);
                cmd.Parameters.AddWithValue("@UseType", useType);

                cmd.ExecuteNonQuery();
            }
            else
            {

                strSQL = new StringBuilder("Delete t_Forum_Images" + conn.SiteId + " where PostId=@PostId And UseType=@UseType ");
                cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
                cmd.Parameters.AddWithValue("@PostId", postId);
                cmd.Parameters.AddWithValue("@UseType", useType);

                cmd.ExecuteNonQuery();
            }

        }

    }
}
