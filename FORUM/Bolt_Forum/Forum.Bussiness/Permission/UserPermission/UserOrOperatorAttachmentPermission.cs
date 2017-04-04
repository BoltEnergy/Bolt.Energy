#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2009                                                                               //
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
    public class UserOrOperatorAttachmentPermission : UserOrOperatorPermissionBase, IAttachmentPermission
    {
        private bool _ifAllowUploadAttachment;
        private int _maxCountOfAttachmentsForOnePost;
        private int _maxSizeOfOneAttachment;
        private int _maxSizeOfAllAttachments;

        public bool IfAllowUploadAttachment 
        {
            get { return this._ifAllowUploadAttachment; }
        }
        public int MaxCountOfAttacmentsForOnePost 
        {
            get { return this._maxCountOfAttachmentsForOnePost; }
        }
        public int MaxSizeOfOneAttachment 
        {
            get { return this._maxSizeOfOneAttachment; }
        }//unit:byte
        public int MaxSizeOfAllAttachments 
        {
            get { return this._maxSizeOfAllAttachments; }
        }//unit:byte

        public UserOrOperatorAttachmentPermission(SqlConnection generalConn, SqlTransaction generalTransaction, SqlConnectionWithSiteId siteConn, SqlTransaction siteTransaction, UserOrOperator userOrOperator)
            : base(generalConn, generalTransaction, siteConn, siteTransaction, userOrOperator)
        { }

        public void CheckPermission()
        { }
    }
}
