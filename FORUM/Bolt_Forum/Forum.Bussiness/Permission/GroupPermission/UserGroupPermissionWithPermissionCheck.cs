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
    public class UserGroupPermissionWithPermissionCheck : UserGroupPermission
    {
        UserOrOperator _operatingUserOrOperator;

        public UserGroupPermissionWithPermissionCheck(UserOrOperator operatingUserOrOperator,int groupId,
            bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost,
            bool ifAllowCustomizeAvatar, int maxLengthofSignature,bool ifSignatureAllowHTML,bool ifSignatureAllowUrl,bool ifSignatureAllowInserImage, 
            int minIntervalForPost,int maxLengthOfPost, 
            bool ifAllowHTML, bool ifAllowUrl, bool ifAllowUploadImage, bool ifAllowUploadAttachment,
            int maxCountOfAttacmentsForOnePost, int maxSizeOfOneAttachments, int maxSizeOfAllAttachments, int maxCountOfMessageSendOneDay,
            bool ifAllowSearch, int minIntervalForSearch, bool ifPostNotNeedModeration, SqlConnectionWithSiteId conn, SqlTransaction transaction
            ):
            base(groupId,ifAllowViewForum,ifAllowViewTopic,ifAllowPost,ifAllowCustomizeAvatar,maxLengthofSignature,ifSignatureAllowHTML,ifSignatureAllowUrl,ifSignatureAllowInserImage,
            minIntervalForPost,maxLengthOfPost,ifAllowHTML,ifAllowUrl,ifAllowUploadImage,ifAllowUploadAttachment,maxCountOfAttacmentsForOnePost,
            maxSizeOfOneAttachments,maxSizeOfAllAttachments,maxCountOfMessageSendOneDay,ifAllowSearch,minIntervalForSearch,
            ifPostNotNeedModeration,conn,transaction)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;             
        }

        public UserGroupPermissionWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int groupId)
            : base(conn, transaction, groupId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }
        
        private void CheckPermission()
        {
            CommFun.CheckAdminPanelCommonPermission(this._operatingUserOrOperator);
        }

        public void Update(
            bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost,
            bool ifAllowCustomizeAvatar, int maxLengthofSignature, bool ifSignatureAllowUrl,bool ifSignatureAllowInsertImage,
            int minIntervalForPost,int maxLengthOfPost, 
            bool ifAllowUrl, bool ifAllowUploadImage, bool ifAllowUploadAttachment,
            int maxCountOfAttacmentsForOnePost, int maxSizeOfOneAttachments, int maxSizeOfAllAttachments, int maxCountOfMessageSendOneDay,
            bool ifAllowSearch, int minIntervalForSearch, bool ifPostNotNeedModeration
            )
        {
            CheckPermission();
            base.Update(_operatingUserOrOperator,ifAllowViewForum, ifAllowViewTopic, ifAllowPost, ifAllowCustomizeAvatar,
                maxLengthofSignature,ifSignatureAllowUrl,ifSignatureAllowInsertImage, minIntervalForPost, maxLengthOfPost, ifAllowUrl, ifAllowUploadImage, ifAllowUploadAttachment
                , maxCountOfAttacmentsForOnePost, maxSizeOfOneAttachments, maxSizeOfAllAttachments, maxCountOfMessageSendOneDay, ifAllowSearch
                , minIntervalForSearch, ifPostNotNeedModeration);
        }
        public void Delete()
        {
            CheckPermission();
            base.Delete(_operatingUserOrOperator);
        }
    }
}
