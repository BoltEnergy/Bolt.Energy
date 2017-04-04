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

namespace Com.Comm100.Forum.Bussiness
{
    public class AttachmentsOfPostWithPermissionCheck : AttachmentsOfPost
    {
        UserOrOperator _operatingUserOrOperator;

        public AttachmentsOfPostWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int postId)
            : base(conn, transaction, postId)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public override int Add(int forumId, int attachmentsOfPostCount ,byte[] attachment, int size, int uploadUserOrOperatorId
          , bool ifPayScoreRequired, int score, string name, string description,
          Guid guid)
        {
            bool ifModerator = CommFun.IfModeratorInUI(_conn, _transaction, forumId, _operatingUserOrOperator);
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            AttachmentsOfUserOrOperatorWithPermissionCheck attachementsOfUser = new AttachmentsOfUserOrOperatorWithPermissionCheck(
                _conn, _transaction, _operatingUserOrOperator, uploadUserOrOperatorId );
            CommFun.CommonAddAttachmentPermissionCheck(forumId, _operatingUserOrOperator, attachmentsOfPostCount, attachment, guid,
                attachementsOfUser,ifModerator);

            return base.Add(forumId, attachmentsOfPostCount, attachment, size, uploadUserOrOperatorId,
                ifPayScoreRequired, score, name, description, guid);
        }

        public AttachmentWithPermissionCheck[] GetAllAttachments()
        {
            return base.GetAllAttachments(_operatingUserOrOperator);
        }
    }
}
