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
using Com.Comm100.Framework.Database;
using System.Data.SqlClient;
using Com.Comm100.Framework.Enum.Forum;
using System.Data;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class AttahchmentsOfDraft : AttachmentsBase
    {
        private int _draftId;
        public int DraftId { get { return _draftId; } }

        public AttahchmentsOfDraft(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int draftId)
            : base(conn, transaction)
        {
            _draftId = draftId;
        }
        public override int Add(int forumId,int attachmentsOfPostCount, byte[] attachment, int size, int uploadUserOrOperatorId
            , bool ifPayScoreRequired, int score, string name, string description,
            Guid guid)
        {
            return AttachmentWithPermissionCheck.Add(
                _conn, _transaction, forumId, _draftId,
                uploadUserOrOperatorId, size, attachment
                , ifPayScoreRequired, score, name, description,
                guid, EnumAttachmentType.AttachToDraft);
        }

        protected override AttachmentWithPermissionCheck[] GetAllAttachments(
            UserOrOperator operatingUserOrOperator)
        {
            DataTable dt = AttachmentAccess.GetAllAttachmentsOfPost(_conn, _transaction, _draftId,
                (int)EnumAttachmentType.AttachToDraft);
            List<AttachmentWithPermissionCheck> attachements = new List<AttachmentWithPermissionCheck>();
            foreach (DataRow dr in dt.Rows)
            {
                AttachmentWithPermissionCheck attachement = CreateAttachmentObject(dr, operatingUserOrOperator);
                attachements.Add(attachement);
            }
            return attachements.ToArray();
        }

        //public bool IfHasAttachment()
        //{
        //    return AttachmentAccess.IfHasAttachment(_conn, _transaction, _draftId, (int)EnumAttachmentType.AttachToDraft);
        //}
    }
}
