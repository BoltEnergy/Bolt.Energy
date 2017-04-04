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
    public class UserPermissionSettingWithPermissionCheck : UserPermissionSetting
    {
        UserOrOperator _operatingUserOrOperator;

        public UserPermissionSettingWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int siteId)
            : base(conn, transaction, siteId)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public override void Update(bool ifAllowViewForum, bool ifAllowViewTopic, bool ifAllowPost, int minIntervalForPost, int maxLengthOfPost, bool ifPostNotNeedModeration, bool ifAllowCustomizeAvatar, int maxLengthofSignature, bool ifSignatureAllowUrl, bool ifSignatureAllowUploadImage, bool ifAllowUrl, bool ifAllowUploadImage, bool ifAllowUploadAttachment, int maxCountOfAttacmentsForOnePost, int maxSizeOfOneAttachment, int maxSizeOfAllAttachments, int maxCountOfMessageSendOneDay, bool ifAllowSearch, int minIntervalForSearch)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            base.Update(ifAllowViewForum, ifAllowViewTopic, ifAllowPost, minIntervalForPost, maxLengthOfPost, ifPostNotNeedModeration, ifAllowCustomizeAvatar, maxLengthofSignature, ifSignatureAllowUrl, ifSignatureAllowUploadImage, ifAllowUrl, ifAllowUploadImage, ifAllowUploadAttachment, maxCountOfAttacmentsForOnePost, maxSizeOfOneAttachment, maxSizeOfAllAttachments, maxCountOfMessageSendOneDay, ifAllowSearch, minIntervalForSearch);
        }
    }
}
