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

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class PostImages
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        private int _postId;
        private int _useType;
        public int PostId
        {
            get { return this._postId; }
        }
        public int UseType
        {
            get { return this._useType; }
        }

        public PostImages(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId,int useType)
        {
            _conn = conn;
            _transaction = transaction;
            _postId = postId;
            _useType = useType;
        }

        public virtual int Add(string name,string type,byte[] file)
        {
            DateTime uploadTime = DateTime.UtcNow;
            return PostImage.Add(this._conn, this._transaction, this._postId, name, type, file, uploadTime,this._useType);
        }

        public virtual void Delete()
        {
            if (this._postId == 0)
            {
                DateTime nowTime = DateTime.UtcNow;
                PostImageAccess.DeleteNotUsedPostImages(_conn, _transaction, nowTime);
            }
            else
            {
                PostImageAccess.DeletePostImages(_conn, _transaction, _postId,_useType);
            }
        }

        public virtual void AttachToPost(int[] imageIds)
        {
            PostImageAccess.AttachToPost(this._conn, this._transaction, this._postId,this._useType, imageIds);
            PostImageAccess.ClearNoUsedAfterAttach(this._conn, this._transaction, this._postId, this._useType, imageIds);
        }

    }
}
