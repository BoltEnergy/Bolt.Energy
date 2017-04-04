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
    public class QueueEmails
    {
        private SqlConnection _conn;
        private SqlTransaction _transaction;

        public QueueEmails(SqlConnection conn, SqlTransaction transaction)
        {
            this._conn = conn;
            this._transaction = transaction;
        }

        public void Add(int siteId, int topicId, int postId, EnumQueueEmailType type, DateTime createDate)
        {
            int queueEmailId = QueueEmailAccess.AddQueueEmail(_conn, _transaction, siteId, postId, type, createDate);
            QueueEmailRecipients recipients = new QueueEmailRecipients(_conn, _transaction, queueEmailId);
            recipients.CreateRecientsList(siteId, topicId);
        }

        public QueueEmail[] GetAllScheduledQueueEmails()
        {
            DataTable table = QueueEmailAccess.GetAllScheduledQueueEmails(_conn, _transaction);
            QueueEmail[] queueEmails = new QueueEmail[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                queueEmails[i] = CreateQueueEmailObject(table.Rows[i]);
            }
            return queueEmails;
        }

        private QueueEmail CreateQueueEmailObject(DataRow dr)
        {
            int id = Convert.ToInt32(dr["Id"]);
            int siteId = Convert.ToInt32(dr["SiteId"]);
            int postId = Convert.ToInt32(dr["PostId"]);
            Int16 type = Convert.ToInt16(dr["Type"]);
            Int16 status = Convert.ToInt16(dr["Status"]);
            DateTime createDate = Convert.ToDateTime(dr["CreateDate"]);

            return new QueueEmail(_conn, _transaction, id, siteId, postId, type, status, createDate);
        }
    }
}
