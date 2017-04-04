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
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;
using Com.Comm100.Framework.FieldLength;
using System.Web;
using Com.Comm100.Framework.ASPNETState;

namespace Com.Comm100.Forum.Bussiness
{
    public class TopicFactory
    {
        public static TopicBase CreateTopic(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int topicId, UserOrOperator operatingUserOrOperator, out bool ifAnnoucement)
        {
            TopicBase topic = null;
            ifAnnoucement = IfAnnouncement(conn, transaction, topicId);
            if (ifAnnoucement == true)
            {
                topic = new AnnouncementWithPermissionCheck(
                    conn, transaction, operatingUserOrOperator, topicId);
            }
            else
            {
                topic = new TopicWithPermissionCheck(
                    conn, transaction, topicId,operatingUserOrOperator);
            }
            return topic;
        }

        private static bool IfAnnouncement(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int topicId)
        {
            //return false;
            return TopicAccess.IfAnnoucement(conn, transaction, topicId);
        }
    }
}
