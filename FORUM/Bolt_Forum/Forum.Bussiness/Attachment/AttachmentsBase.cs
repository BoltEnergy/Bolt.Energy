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

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class AttachmentsBase
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        public AttachmentsBase(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            _conn = conn;
            _transaction = transaction;
        }
        public virtual int Add(int forumId,int attachmentsOfPostCount, byte[] attachment, int size, int uploadUserOrOperatorId
            , bool ifPayScoreRequired, int score, string name, string description,
            Guid guid){return -1;}
        protected virtual AttachmentWithPermissionCheck[] GetAllAttachments(
           UserOrOperator operatingUserOrOperator){ return null;}
        protected AttachmentWithPermissionCheck CreateAttachmentObject(DataRow dr, UserOrOperator operatingUserOrOperator)
        {
            byte[] Attchement = null;if(Convert.IsDBNull(dr["Attachment"])){Attchement = (byte[])dr["Attachment"];}
            return new AttachmentWithPermissionCheck(_conn, _transaction, operatingUserOrOperator,
                    Convert.ToInt32(dr["Id"]),
                    Convert.ToInt32(dr["AttachId"]),
                    Convert.ToInt32(dr["UserId"]),
                    Convert.ToInt32(dr["Size"]),
                    Attchement,
                    Convert.ToBoolean(dr["IfPayScoreRequired"]),
                    Convert.ToInt32(dr["Score"]),
                    Convert.ToString(dr["OriginalName"]),
                    Convert.ToString(dr["Description"]),
                    Convert.ToInt32(dr["Type"]));
        }
    }
}
