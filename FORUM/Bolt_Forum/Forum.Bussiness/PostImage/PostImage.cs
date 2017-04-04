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
using System.IO;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class PostImage
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        private int _id;
        private string _name;
        private int _postId;
        private string _type;
        private byte[] _file;
        private DateTime _uploadTime;
        private int _useType;

        public int Id
        {
            get { return this._id; }
        }
        public string Name
        {
            get { return this._name; }
        }
        public int PostId
        {
            get { return this._postId; }
        }
        public string Type
        {
            get { return this._type; }
        }
        public byte[] File
        {
            get { return this._file; }
        }
        public DateTime UploadTime
        {
            get { return this._uploadTime; }
        }
        public int UseType
        {
            get { return this._useType; }
        }

        public PostImage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int imageId)
        {
            _conn = conn;
            _transaction = transaction;

            DataTable table = new DataTable();
            table = PostImageAccess.GetPostImageById(conn, transaction, imageId);
           
            if (table.Rows.Count == 0)
            {
                ExceptionHelper.ThrowPostImageNotExistException(imageId);
            }
            else
            {
                this._id = imageId;
                this._postId = Convert.ToInt32(table.Rows[0]["PostId"]);
                this._name = Convert.ToString(table.Rows[0]["Name"]);
                this._type = Convert.ToString(table.Rows[0]["Type"]);
                this._file = (byte[])table.Rows[0]["Image"];
                this._uploadTime = Convert.ToDateTime(table.Rows[0]["UploadTime"]);
                this._useType = Convert.ToInt32(table.Rows[0]["UseType"]);
                
            }
        }

        public PostImage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int imageId, int postId, string name, string type,byte[] file,DateTime uploadTime,int useType)
        {
            this._conn = conn;
            this._transaction = transaction;

            this._id = imageId;
            this._postId = postId;
            this._name = name;
            this._type = type;
            this._file = file;
            this._uploadTime = uploadTime;
            this._useType = useType;
        }

        public static int Add(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId, string name, string type, byte[] file,DateTime uploadTime,int useType)
        {
            CheckFieldsLength(file,name);
            return PostImageAccess.AddPostImage(conn, transaction, postId, name, type, file, uploadTime,useType);
        }

        private static void CheckFieldsLength(byte[] file,string name)
        {
             if (file == null || file.Length == 0)
                 ExceptionHelper.ThrowPostImageFileNotExistException(name);

        }

    }
}
