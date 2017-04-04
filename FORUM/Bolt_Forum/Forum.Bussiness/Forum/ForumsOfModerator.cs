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
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class ForumsOfModerator : ForumsBase
    {
        private int _moderatorId;

        public int ModeratorId
        {
            get { return this._moderatorId; }
        }

        public ForumsOfModerator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int moderatorId)
            : base(conn, transaction)
        {
            this._moderatorId = moderatorId;
        }

        protected ForumWithPermissionCheck[] GetAllForums(UserOrOperator operatingUsrOrOperator)
        {
            DataTable table = ForumAccess.GetAllForumsOfModeratorId(_conn, _transaction, _moderatorId);
            ForumWithPermissionCheck[] forums = new ForumWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {

                forums[i] = new ForumWithPermissionCheck(_conn, _transaction, Convert.ToInt32(table.Rows[i]["Id"]), operatingUsrOrOperator,
                    Convert.ToInt32(table.Rows[i]["CategoryId"]), Convert.ToInt32(table.Rows[i]["OrderId"]),
                    Convert.ToString(table.Rows[i]["Name"]), (EnumForumStatus)Convert.ToInt16((table.Rows[i]["Status"])),
                    Convert.ToInt32(table.Rows[i]["NumberOfTopics"]), Convert.ToInt32(table.Rows[i]["NumberOfPosts"]),
                    Convert.ToString(table.Rows[i]["Description"]), Convert.ToInt32(table.Rows[i]["LastPostId"]),
                    Convert.ToInt32(table.Rows[i]["LastPostTopicId"]),
                    Convert.ToString(table.Rows[i]["LastPostSubject"]),
                    0,"",false,new DateTime(),
                    //table.Rows[i]["LastPostCreatedUserOrOperatorId"] is System.DBNull ? 0 : Convert.ToInt32(table.Rows[i]["LastPostCreatedUserOrOperatorId"]),
                    //table.Rows[i]["LastPostCreatedUserOrOperatorName"] is System.DBNull ? "" : Convert.ToString(table.Rows[i]["LastPostCreatedUserOrOperatorName"]),
                    //table.Rows[i]["LastPostCreatedUserOrOperatorIfDeleted"] is System.DBNull ? false : Convert.ToBoolean(table.Rows[i]["LastPostCreatedUserOrOperatorIfDeleted"]),
                    //Convert.ToDateTime(table.Rows[i]["LastPostPostTime"]),
                    Convert.ToBoolean(table.Rows[i]["IfAllowPostNeedingReplayTopic"]),
                    Convert.ToBoolean(table.Rows[i]["IfAllowPostNeedingPayTopic"]));
            }
            return forums;
        }
    }
}
