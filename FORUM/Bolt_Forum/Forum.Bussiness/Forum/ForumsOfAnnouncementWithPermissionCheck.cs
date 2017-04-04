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
    public class ForumsOfAnnouncementWithPermissionCheck : ForumsOfAnnouncement
    {
        UserOrOperator _operatingUserOrOperator;

        public ForumsOfAnnouncementWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int topicId)
            : base(conn, transaction, topicId)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public ForumWithPermissionCheck[] GetAllForums(out string[] forumPath)
        {
            return base.GetAllForums(_operatingUserOrOperator, out forumPath);
        }
        public ForumWithPermissionCheck[] GetAllForums()
        {
            string[] forumPath;
            return base.GetAllForums(_operatingUserOrOperator, out forumPath);
        }
        public void DeleteForumsAndAnnoucementRelation()
        {
            base.DeleteForumsAndAnnoucementRelation();
        }

        public void AddForumsAndAnnoucementRelation(int forumId)
        {
            base.AddForumsAndAnnoucementRelation(forumId);
        }

        public void DeleteForumsAndAnnouncementRelation(int moderatorId)
        {
            base.DeleteForumsAndAnnoucementRelation(moderatorId);
        }

        public void DeleteForumsAndAnnouncementRelationWithForumId(int forumId)
        {
            base.DeleteForumsAndAnnoucementRelationWithForumId(forumId);
        }
    }
}
