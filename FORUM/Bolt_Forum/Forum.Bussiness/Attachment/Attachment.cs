#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.FieldLength;
namespace Com.Comm100.Forum.Bussiness
{
    //public enum EnumAttachmentType {Post,Draft}
    public abstract class Attachment
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        #region private fields
        private int _id;
        private int _postId;
        private int _uploadUserOrOperatorId;
        private int _size;
        private byte[] _attachment;
        private bool _ifPayScoreRequired;
        private int _score;
        private string _name;
        private string _description;
        private int _type;
        #endregion

        #region properties
        public int Id
        {
            get { return this._id; }
        }
        public int PostId
        {
            get { return this._postId; }
        }
        public int UploadUserOrOperatorId
        {
            get { return this._uploadUserOrOperatorId; }
        }
        public int Size
        {
            get { return this._size; }
        }
        public string AttachmentPath
        {
            get { return string.Empty; }
        }
        public bool IfPayScoreRequired
        {
            get { return this._ifPayScoreRequired; }
        }
        public int Score
        {
            get { return this._score; }
        }
        public string Name
        {
            get { return _name; }
        }
        public string Description
        {
            get { return _description; }
        }
        public byte[] AttachmentFile
        {
            get { return _attachment; }
        }
        public EnumAttachmentType AttchmentType
        {
            get { return (EnumAttachmentType)_type; }
        }
        #endregion

        public Attachment(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id)
        {
            _conn = conn;
            _transaction = transaction;
            _id = id;

            DataTable dt = AttachmentAccess.GetAttachmentById(_conn, _transaction, _id);
            if (dt.Rows.Count == 0)
            {               
                ExceptionHelper.ThrowForumAttachmentNotExistException(id);
            }
            foreach (DataRow dr in dt.Rows)
            {
                _postId = Convert.ToInt32(dr["AttachId"]);
                _uploadUserOrOperatorId = Convert.ToInt32(dr["UserId"]);
                _size = Convert.ToInt32(dr["Size"]);
                if (!Convert.IsDBNull(dr["Attachment"])) { _attachment = (byte[])dr["Attachment"]; }
                _ifPayScoreRequired = Convert.ToBoolean(dr["IfPayScoreRequired"]);
                _score = Convert.ToInt32(dr["Score"]);
                _description = Convert.ToString(dr["Description"]);
                _name = Convert.ToString(dr["OriginalName"]);
                _type = Convert.ToInt32(dr["Type"]);
            }
        }

        public Attachment(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id,
            int postId, int uploadUserOrOperatorId, int size, byte[] attachment, bool ifPayScoreRequired, int score
            ,string name,string description,int type)
        {
            _conn = conn;
            _transaction = transaction;
            _id = id;

            _postId = postId;
            _uploadUserOrOperatorId = uploadUserOrOperatorId;
            _size = size;
            _attachment = attachment;
            _ifPayScoreRequired = ifPayScoreRequired;
            _score = score;
            _name = name;
            _description = description;
            _type = type;
        }

        public static int Add(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int forumId,int postId, int uploadUserOrOperatorId, int size, byte[] attachment,
            bool ifPayScoreRequired, int score,string name, string description,
            Guid guid,EnumAttachmentType type)
        {
            CheckFieldsLength(name, description);
            return AttachmentAccess.AddAttachement(conn, transaction,
                postId, uploadUserOrOperatorId, size, 
                attachment, ifPayScoreRequired, score,name,description,guid,(int)type);
        }

        protected void Update(int postId,int score,string description,EnumAttachmentType type)
        {
            CheckFieldsLength(description);
            AttachmentAccess.UpdateAttachment(_conn, _transaction, _id, postId, score, description,(int)type);
        }

        protected void Delete()
        {
            /*Delete Attachment*/
            AttachmentAccess.DeleteAttahcment(_conn, _transaction, _id);
            /*Delete Attachment History*/
            //AttachmentAccess.DeleteAttachmentHistory(_conn, _transaction, _id);
            PayHistoriesOfAttachment histories = new PayHistoriesOfAttachment(_conn, _transaction,
                _id);
            histories.Delete();
        }

        private static void CheckFieldsLength(string name, string description)
        {
            if (name.Length == 0)
            {
                ExceptionHelper.ThrowSystemFieldCanNotBeNull("Name");
            }
            else
            {
                if (name.Length > ForumDBFieldLength.Attachment_originalNameFieldLength)
                    ExceptionHelper.ThrowSystemFieldLengthExceededException("Name", ForumDBFieldLength.Attachment_originalNameFieldLength); 
            }
            if (description.Length > ForumDBFieldLength.Attachment_descriptionFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Description", ForumDBFieldLength.Attachment_descriptionFieldLength);
            }
        }
        private static void CheckFieldsLength(string description)
        {
            if (description.Length > ForumDBFieldLength.Attachment_descriptionFieldLength)
            {
                ExceptionHelper.ThrowSystemFieldLengthExceededException("Description", ForumDBFieldLength.Attachment_descriptionFieldLength);
            } 
        }
    }
}
