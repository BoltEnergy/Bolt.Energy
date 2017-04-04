#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Collections.Generic;
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
    public abstract class AttachmentsOfUserOrOperator : AttachmentsBase
    {
        private int _userOrOperatorId;

        public int UserOrOperatorId
        {
            get { return this._userOrOperatorId; }
        }

        public AttachmentsOfUserOrOperator(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int userOrOperatorId)
            : base(conn, transaction)
        {
            _userOrOperatorId = userOrOperatorId;
        }

        public int GetSizeOfAllTempAttachments(
            UserOrOperator operatingUserOrOperator, Guid guid, EnumAttachmentType type)
        {
            int allsize = 0;
            foreach (var t in this.GetAllTempAttachments(operatingUserOrOperator, guid, type))
            {
                allsize += t.Size;
            }
            return allsize;
        }

        public int GetCountOfAllTempAttachments(
            UserOrOperator operatingUserOrOperator, Guid guid, EnumAttachmentType type)
        {
            return this.GetAllTempAttachments(operatingUserOrOperator, guid, type).Length;
        }

        public AttachmentWithPermissionCheck[] GetAllTempAttachments(
            UserOrOperator operatingUserOrOperator, Guid guid, EnumAttachmentType type)
        {
            DataTable dt = AttachmentAccess.GetAllTempAttachmentsByUser(
                _conn, _transaction, _userOrOperatorId, guid, (int)type);
            List<AttachmentWithPermissionCheck> attachements = new List<AttachmentWithPermissionCheck>();
            foreach (DataRow dr in dt.Rows)
            {
                AttachmentWithPermissionCheck attachement = CreateAttachmentObject(dr, operatingUserOrOperator);
                attachements.Add(attachement);
            }
            return attachements.ToArray();
        }

        public void DeleteAllTempAttachments(
            UserOrOperator operatingUserOrOperator, EnumAttachmentType type)
        {
            //DataTable dt = AttachmentAccess.GetAllTempAttachmentsByUser(
            //    _conn, _transaction, _userOrOperatorId, ,(int)type);

            //foreach (DataRow dr in dt.Rows)
            //{
            //    AttachmentWithPermissionCheck attachement = CreateAttachmentObject(dr, operatingUserOrOperator);
            //    attachement.Delete();
            //}
        }
    }
}
