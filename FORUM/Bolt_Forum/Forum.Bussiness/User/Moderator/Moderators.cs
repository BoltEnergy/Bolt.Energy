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
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class Moderators
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        private int _forumId;

        public int ForumId
        {
            get { return this._forumId; }
        }

        public Moderators(SqlConnectionWithSiteId conn, SqlTransaction transaction, int forumId)
        {
            this._conn = conn;
            this._transaction = transaction;
            this._forumId = forumId;

        }

        protected ModeratorWithPermissionCheck[] GetAllModerators(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = new DataTable();
            table = ModeratorAccess.GetModeratorsByForumId(_conn, _transaction, _forumId);
            ModeratorWithPermissionCheck[] moderator = new ModeratorWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                byte[] customizeavatar = table.Rows[i]["CustomizeAvatar"] is System.DBNull ? null : (byte[])(table.Rows[i]["CustomizeAvatar"]);

                moderator[i] = new ModeratorWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, Convert.ToInt32(table.Rows[i]["ForumId"]),
                    Convert.ToInt32(table.Rows[i]["UserOrOperatorId"]),
                    Convert.ToString(table.Rows[i]["Email"]), Convert.ToString(table.Rows[i]["Name"]),
                    Convert.ToString(table.Rows[i]["Password"]), Convert.ToString(table.Rows[i]["Description"]),
                    Convert.ToBoolean(table.Rows[i]["IfAdmin"]), Convert.ToBoolean(table.Rows[i]["IfDeleted"]),
                    Convert.ToBoolean(table.Rows[i]["IfActive"]), Convert.ToString(table.Rows[i]["ForgetPasswordGUIDTag"]),
                    Convert.ToDateTime(table.Rows[i]["ForgetPasswordTagTime"]), Convert.ToString(table.Rows[i]["ForgetPasswordGUIDTag"]),
                    Convert.ToInt16(table.Rows[i]["ModerateStatus"]), Convert.ToInt16(table.Rows[i]["EmailVerificationStatus"]),
                    Convert.ToInt32(table.Rows[i]["Posts"]), Convert.ToDateTime(table.Rows[i]["JoinedTime"]),
                    Convert.ToInt64(table.Rows[i]["JoinedIP"]), Convert.ToDateTime(table.Rows[i]["LastLoginTime"]),
                    Convert.ToInt64(table.Rows[i]["LastLoginIP"]), Convert.ToBoolean(table.Rows[i]["IfShowEmail"]),
                    Convert.ToString(table.Rows[i]["FirstName"]), Convert.ToString(table.Rows[i]["LastName"]),
                    Convert.ToBoolean(table.Rows[i]["IfShowUserName"]), Convert.ToInt16(table.Rows[i]["Age"]),
                    Convert.ToBoolean(table.Rows[i]["IfShowAge"]), Convert.ToInt16(table.Rows[i]["Gender"]),
                    Convert.ToBoolean(table.Rows[i]["IfShowGender"]), Convert.ToString(table.Rows[i]["Occupation"]),
                    Convert.ToBoolean(table.Rows[i]["IfShowOccupation"]), Convert.ToString(table.Rows[i]["Company"]),
                    Convert.ToBoolean(table.Rows[i]["IfShowCompany"]), Convert.ToString(table.Rows[i]["PhoneNumber"]),
                    Convert.ToBoolean(table.Rows[i]["IfShowPhoneNumber"]), Convert.ToString(table.Rows[i]["FaxNumber"]),
                    Convert.ToBoolean(table.Rows[i]["IfShowFaxNumber"]), Convert.ToString(table.Rows[i]["Interests"]),
                    Convert.ToBoolean(table.Rows[i]["IfShowInterests"]), Convert.ToString(table.Rows[i]["HomePage"]),
                    Convert.ToBoolean(table.Rows[i]["IfShowHomePage"]), Convert.ToString(table.Rows[i]["Signature"]),
                    Convert.ToBoolean(table.Rows[i]["IfCustomizeAvatar"]), Convert.ToString(table.Rows[i]["SystemAvatar"]), customizeavatar
                    ,Convert.ToBoolean(table.Rows[i]["IfForumAdmin"]),Convert.ToInt32(table.Rows[i]["ForumScore"]),
                    Convert.ToInt32(table.Rows[i]["ForumReputation"]));

            }
            return moderator;

        }
        public virtual int Add(int userOrOperatorId)
        {
            UsersOrOperatorsOfSite usersOrOperators = new UsersOrOperatorsOfSite(_conn, _transaction);
            if (!usersOrOperators.IfUserOrOperatorExist(userOrOperatorId))
                ExceptionHelper.ThrowUserIdNotExist(userOrOperatorId);
            return ModeratorAccess.AddModerator(_conn, _transaction, _forumId, userOrOperatorId);
        }

        public virtual void Delete(int moderatorId)
        {
            Moderator.Delete(_conn, _transaction, _forumId, moderatorId);
        }
        public virtual void DeleteAllModerators(UserOrOperator operatingOperator)
        {
            Moderator[] moderatorArray = this.GetAllModerators(operatingOperator);
            foreach (Moderator moderator in moderatorArray)
            {
                Moderator.Delete(_conn, _transaction, _forumId, moderator.Id);
            }
        }
        public virtual ForumWithPermissionCheck GetForum(UserOrOperator operatingOperator)
        {
            return new ForumWithPermissionCheck(_conn, _transaction, _forumId, operatingOperator);
        }

        public virtual bool IfModerator(int operatingUserOrOperator)
        {
            //this.forum
            return ModeratorAccess.IfModeratorOfForum(_conn, _transaction, _forumId, operatingUserOrOperator);
        }
    }
}
