﻿#if OPENSOURCE
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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public class AnnouncementsOfForumWithPermissionCheck : AnnouncementsOfForum
    {
        UserOrOperator _operatingUserOrOperator;

        public AnnouncementsOfForumWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int forumId)
            : base(conn, transaction, forumId)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public AnnouncementWithPermissionCheck[] GetAnnouncementsByQueryAndPaging(UserOrOperator operatingUserOrOperator,
          int pageIndex, int pageSize, string subject, string orderField, string orderMethod, out int count)
        {
            return base.GetAnnouncementsByQueryAndPaging(operatingUserOrOperator,
                pageIndex,pageSize,subject,orderField,orderMethod,out count);
        }

        public AnnouncementWithPermissionCheck[] GetAllAnnoucementsOfForum()
        {
            return base.GetAllAnnoucementsOfForum();
        }
       
    }
}
