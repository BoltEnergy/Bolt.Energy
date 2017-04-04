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
    public abstract class AttachmentsOfPost : AttachmentsBase
    {
        private int _postId;

        public int PostId
        {
            get { return this._postId; }
        }

        public AttachmentsOfPost(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId)
            : base(conn, transaction)
        {
            _postId = postId;
        }

        public override int Add(int forumId,int attachmentsOfPostCount,byte[] attachment, int size, int uploadUserOrOperatorId
            , bool ifPayScoreRequired, int score, string name,string description,
            Guid guid)
        {
            return AttachmentWithPermissionCheck.Add(
                _conn, _transaction, forumId,_postId, 
                uploadUserOrOperatorId, size, attachment
                , ifPayScoreRequired, score,name,description,guid,EnumAttachmentType.AttachToPost);
        }


        protected override AttachmentWithPermissionCheck[] GetAllAttachments(
            UserOrOperator operatingUserOrOperator)
        {
            DataTable dt = AttachmentAccess.GetAllAttachmentsOfPost(_conn, _transaction, _postId,(int)EnumAttachmentType.AttachToPost);
            List<AttachmentWithPermissionCheck> attachements = new List<AttachmentWithPermissionCheck>();
            foreach(DataRow dr in dt.Rows)
            {
                AttachmentWithPermissionCheck attachement = CreateAttachmentObject(dr,operatingUserOrOperator);
                attachements.Add(attachement);
            }
            return attachements.ToArray();
        }

        public bool IfHasAttachment()
        {
            return AttachmentAccess.IfHasAttachment(_conn,_transaction,_postId,(int)EnumAttachmentType.AttachToPost);
        }
       
    }
}
