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
using System.Data.SqlClient;
using Com.Comm100.Framework.Database;

namespace Com.Comm100.Forum.Bussiness
{
    public class AnnoucementsFactory
    {
        public static AnnouncementsBase CreateAnnoucement(
             int forumId, SqlConnectionWithSiteId conn, 
            SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
        {
            AnnouncementsBase annoucement;
            if (forumId > 0)
            {
                annoucement = new AnnouncementsOfForumWithPermissionCheck(
                    conn, transaction, operatingUserOrOperator, forumId);
            }
            else
            {
                annoucement = new AnnouncementsOfSiteWithPermissionCheck(
                    conn, transaction, operatingUserOrOperator);
            }
            return annoucement;
        }
    }
}
