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

namespace Com.Comm100.Forum.Bussiness
{
    public class AttachmentsOfDraftWithPermissionCheck : AttahchmentsOfDraft
    {
        UserOrOperator _operatingUserOrOperator;

        public AttachmentsOfDraftWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int postId)
            : base(conn, transaction, postId)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public override int Add(int forumId, int attachmentsOfPostCount,byte[] attachment, int size, int uploadUserOrOperatorId
          , bool ifPayScoreRequired, int score, string name, string description,
          Guid guid)
        {
            return base.Add(forumId, attachmentsOfPostCount,attachment, size, uploadUserOrOperatorId, 
                ifPayScoreRequired, score, name, description,guid);
        }
        public AttachmentWithPermissionCheck[] GetAllAttachments()
        {
            return base.GetAllAttachments(_operatingUserOrOperator);
        }
    }
}
