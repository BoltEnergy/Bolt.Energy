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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.FieldLength;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class InMessage : MessageBase
    {
        #region private fields
        private int _toUserOrOperatorId; 
        private string _toUserOrOperatorDisplayName;
        private bool _ifView;
        #endregion

        #region properties
        public int ToUserOrOperatorId
        {
            get { return this._toUserOrOperatorId; }
        }
        public string ToUserOrOperatorDisplayName
        {
            get { return this._toUserOrOperatorDisplayName; }
        }
        public bool IfView
        {
            get { return this._ifView; }
        }
        #endregion

        public InMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id)
            : base(conn, transaction)
        {
            DataTable table = new DataTable();
            table = InMessageAccess.GetInMessageById(conn, transaction, id);
            if (table.Rows.Count == 0)
            {
                 ExceptionHelper.ThrowForumInMessageNotExistException(id);
            }
            else
            {
                _id = id;
                _subject = Convert.ToString(table.Rows[0]["Subject"]);
                _message = Convert.ToString(table.Rows[0]["Message"]);
                _createDate = Convert.ToDateTime(table.Rows[0]["CreateDate"]);
                _fromUserOrOperatorId = Convert.ToInt32(table.Rows[0]["FromUserId"]);
                _fromUserOrOperatorDisplayName = Convert.ToString(table.Rows[0]["FromUserName"]);
                _toUserOrOperatorId = Convert.ToInt32(table.Rows[0]["ToUserId"]);
                _toUserOrOperatorDisplayName = Convert.ToString(table.Rows[0]["ToUserName"]);
                _ifView = Convert.ToBoolean(table.Rows[0]["IfView"]);
            }
        }

        public InMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int id, string subject, string message, DateTime createDate, int fromUserOrOperatorId,
            string fromUserOrOperatorDisplayName, int toUserOrOperatorId, string toUserOrOperatorDisplayName, bool ifView)
            : base(conn, transaction)
        {          
            this._id = id;
            this._subject = subject;
            this._message = message;
            this._createDate = createDate;
            this._fromUserOrOperatorId = fromUserOrOperatorId;
            this._fromUserOrOperatorDisplayName = fromUserOrOperatorDisplayName;
            this._toUserOrOperatorId = toUserOrOperatorId;
            this._toUserOrOperatorDisplayName = toUserOrOperatorDisplayName;
            this._ifView = ifView;
        }

       public override void Delete()
      {
            InMessageAccess.DeleteInMessage(this._conn, this._transaction, this._id);
       }
       public virtual void UpdateIfView()
       {
           InMessageAccess.UpdateIfView(_conn, _transaction , _id);
       }
    }
}
