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
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;

namespace Com.Comm100.Forum.Bussiness
{
    public class AnnouncementWithPermissionCheck : Announcement
    {
        UserOrOperator _operatingUserOrOperator;

        public AnnouncementWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int id)
            : base(conn, transaction, id)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public AnnouncementWithPermissionCheck(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int id,string subject,DateTime postTime,
            int postUserOrOperatorId,string postUserOrOperatorName,bool postUserOrOperatorIfDeleted,
            int lastPostId,DateTime lastPostTime,
            int lastPostUserOrOperatorId,string lastPostUserOrOperatorName,
            bool lastPostUserOrOperatorIfDeleted,
            int numberOfReplies,int numberOfHits,string content
            ):base(conn,transaction,id,subject,postTime,postUserOrOperatorId,postUserOrOperatorName,
            postUserOrOperatorIfDeleted,lastPostId,lastPostTime,lastPostUserOrOperatorId,
            lastPostUserOrOperatorName,lastPostUserOrOperatorIfDeleted,numberOfReplies,
            numberOfHits,content) 
        {
        }

        public ForumsOfAnnouncementWithPermissionCheck GetForums()
        {
            return base.GetForums(_operatingUserOrOperator);
        }
       
        public void IncreaseNumberOfRepliesByOne(bool ifTopic)
        {
            base.IncreaseNumberOfRepliesByOne(ifTopic, _operatingUserOrOperator);
        }
        public void IncreaseNumberOfHitsByOne()
        {
            base.IncreaseNumberOfHitsByOne();
        }
        public  void DecreaseNumberOfRepliesByOne()
        {
            base.DecreaseNumberOfRepliesByOne(_operatingUserOrOperator);
        }
        public void UpdateLastPostInfo(int lastPostUserOrOperatorId, int lastPostId, 
            DateTime lastPostTime, string subject)
        {
            base.UpdateLastPostInfo(lastPostUserOrOperatorId, lastPostId, lastPostTime, subject, _operatingUserOrOperator);
        }
        public void Delete()
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            base.Delete(_operatingUserOrOperator);
        }
        public void Update(string subject, UserOrOperator operatingUserOrOperator,
            DateTime PostTime, string content, int[] ForumIds)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            //check forum is exsit
            foreach (int forumId in ForumIds)
            {
                ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, forumId,
                    _operatingUserOrOperator);
            }
            base.Update(subject, operatingUserOrOperator, PostTime, content, ForumIds);
        }

    }
}
