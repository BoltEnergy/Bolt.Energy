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
    public class QueueEmailRecipients
    {
        private SqlConnection _conn;
        private SqlTransaction _transaction;

        private int _queueEmailId;
        public int QueueEmailId
        {
            get { return this._queueEmailId; }
        }

        public QueueEmailRecipients(SqlConnection conn, SqlTransaction transaction, int queueEmailId)
        {
            this._conn = conn;
            this._transaction = transaction;
            this._queueEmailId = queueEmailId;
        }

        public List<string> GetAllRecipients()
        {
            DataTable table = QueueEmailRecipientsAccess.GetAllRecipientsByQueueEmailId(_conn, _transaction, _queueEmailId);
            List<string> recipients = new List<string>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                recipients.Add(table.Rows[i]["Email"].ToString());
            }
            return recipients;
        }

        public void CreateRecientsList(int siteId, int topicId)
        {
            QueueEmailRecipientsAccess.CreateRecipientsList(_conn, _transaction, siteId, topicId, _queueEmailId);
        }

        public void Delete(string emailAddress)
        {
            QueueEmailRecipientsAccess.DeleteRecipient(_conn, _transaction, _queueEmailId, emailAddress);
        }

        public void DeleteAll()
        {
            QueueEmailRecipientsAccess.DeleteRecipientsByQueueEmailId(_conn, _transaction, _queueEmailId);
        }
    }
}
