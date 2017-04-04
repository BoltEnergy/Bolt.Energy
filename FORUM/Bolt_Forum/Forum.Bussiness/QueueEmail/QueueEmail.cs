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

namespace Com.Comm100.Forum.Bussiness
{
    public class QueueEmail
    {
        private SqlConnection _conn;
        private SqlTransaction _transaction;

        #region Private Fields
        private int _id;
        private int _siteId;
        private int _postId;
        private Int16 _type;
        private Int16 _status;
        private DateTime _createDate;
        #endregion

        #region Properties
        public int Id
        {
            get { return this._id; }
        }
        public int SiteId
        {
            get { return this._siteId; }
        }
        public int PostId
        {
            get { return this._postId; }
        }
        public EnumQueueEmailType Type
        {
            get { return (EnumQueueEmailType)this._type; }
        }
        public EnumQueueEmailStatus Status
        {
            get { return (EnumQueueEmailStatus)this._status; }
        }
        public DateTime CreateDate
        {
            get { return this._createDate; }
        }
        #endregion

        public QueueEmail(SqlConnection conn, SqlTransaction transaction, int id)
        {
            DataTable table = QueueEmailAccess.GetQueueEmailById(conn, transaction, id);
            if (table.Rows.Count <= 0)
            {
                //throw Exception
            }
            else
            {
                this._conn = conn;
                this._transaction = transaction;
                this._id = id;
                this._siteId = Convert.ToInt32(table.Rows[0]["SiteId"]);
                this._postId = Convert.ToInt32(table.Rows[0]["PostId"]);
                this._type = Convert.ToInt16(table.Rows[0]["Type"]);
                this._status = Convert.ToInt16(table.Rows[0]["Status"]);
                this._createDate = Convert.ToDateTime(table.Rows[0]["CreateDate"]);
            }
        }

        public QueueEmail(SqlConnection conn, SqlTransaction transaction, int id, int siteId, int postId, Int16 type, Int16 status, DateTime createDate)
        {
            this._conn = conn;
            this._transaction = transaction;
            this._siteId = siteId;
            this._postId = postId;
            this._type = type;
            this._status = status;
            this._createDate = createDate;
        }

        public void UpdateStatusToRunning()
        {
            QueueEmailAccess.UpdateQueueEmailStatus(_conn, _transaction, _id, EnumQueueEmailStatus.Running);
        }

        public void UpdateStatusToScheduled()
        {
            QueueEmailAccess.UpdateQueueEmailStatus(_conn, _transaction, _id, EnumQueueEmailStatus.Scheduled);
            GetRecipients().DeleteAll();
        }

        public void Delete()
        {
            QueueEmailAccess.DeleteQueueEmail(_conn, _transaction, _id);
        }

        public QueueEmailRecipients GetRecipients()
        {
            return new QueueEmailRecipients(_conn, _transaction, _id);
        }
    }
}
