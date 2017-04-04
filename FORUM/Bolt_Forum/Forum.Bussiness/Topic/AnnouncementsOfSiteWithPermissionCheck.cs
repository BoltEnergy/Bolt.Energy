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

namespace Com.Comm100.Forum.Bussiness
{
    public class AnnouncementsOfSiteWithPermissionCheck : AnnouncementsOfSite
    {
        UserOrOperator _operatingUserOrOperator;

        public AnnouncementsOfSiteWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator)
            : base(conn, transaction)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }

        public AnnouncementWithPermissionCheck[] GetAnnouncementsByQueryAndPaging(int pageIndex, int pageSize, 
            string subject,string orderField,string orderMethod, out int count)
        {
            return base.GetAnnouncementsByQueryAndPaging(_operatingUserOrOperator, pageIndex, pageSize, 
                 subject, orderField, orderMethod, out count);
        }

        public void Delete(int id)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            base.Delete(id, _operatingUserOrOperator);
        }

        public int Add(string subject,DateTime PostTime, string content, int[] ForumIds)
        {
            CommFun.CheckAdminPanelCommonPermission(_operatingUserOrOperator);
            //check forum is exsit
            foreach (int forumId in ForumIds)
            {
                ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, forumId,
                    _operatingUserOrOperator);
            }
            return base.Add(_conn, _transaction, subject, _operatingUserOrOperator, PostTime,  content, ForumIds);
        }

        #region Moderator
        public void DeleteAnnouncementByModerator(int topicId)
        {
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, topicId, _operatingUserOrOperator);
            CommFun.CheckModeratorPanelCommonPermission(_operatingUserOrOperator, topic.ForumId);
            base.DeleteAnnouncementByModerator(_operatingUserOrOperator, topicId);
        }
        public void DeleteAnnoucementByModeratorOrAdmin(int forumId,int topicId)
        {
            CheckDeleteAnnoucementByModeratorOrAdmin(forumId, topicId);
            base.DeleteAnnoucementByModeratorOrAdmin(_operatingUserOrOperator,forumId, topicId);
        }
        private void CheckDeleteAnnoucementByModeratorOrAdmin(int forumId,int topicId)
        {
            AnnouncementWithPermissionCheck announcement = new AnnouncementWithPermissionCheck(_conn, _transaction, _operatingUserOrOperator,
                topicId);
            ForumWithPermissionCheck forum = new ForumWithPermissionCheck(_conn, _transaction, forumId, _operatingUserOrOperator);
            CommFun.CheckCommonPermissionInUI(_operatingUserOrOperator);
            if (!CommFun.IfAdminInUI(_operatingUserOrOperator) &&
                !CommFun.IfModeratorInUI(_conn,_transaction,forumId,_operatingUserOrOperator))
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        }
        #endregion

    }
}
