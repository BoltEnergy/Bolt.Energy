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
using Com.Comm100.Framework.Database;
using System.Data.SqlClient;
using System.Data;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public class ModeratorsOfAnnoucement
    {
        private int _annoucementId;
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        public int AnnoucementId { get {return _annoucementId;} }

        public ModeratorsOfAnnoucement(SqlConnectionWithSiteId conn,SqlTransaction transaction,int annoucmentId)
        {
            _conn = conn;
            _transaction = transaction;
            _annoucementId = annoucmentId;
        }

        public ModeratorWithPermissionCheck[] GetAllModreators(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = ModeratorAccess.GetModeratorByAnnoucementId(_conn, _transaction, _annoucementId);
            ModeratorWithPermissionCheck[] moderator = new ModeratorWithPermissionCheck[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                byte[] customizeavatar = table.Rows[i]["CustomizeAvatar"] is System.DBNull ? null : (byte[])(table.Rows[i]["CustomizeAvatar"]);
                
                #region Create a moderator
                moderator[i] = new ModeratorWithPermissionCheck(_conn, _transaction, operatingUserOrOperator, 0,
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
                    , Convert.ToBoolean(table.Rows[i]["IfForumAdmin"]), Convert.ToInt32(table.Rows[i]["ForumScore"]),
                    Convert.ToInt32(table.Rows[i]["ForumReputation"]));
                #endregion

            }
            return moderator;
        }

        public bool IfModerator(UserOrOperator operatingUserOrOperator)
        {
            return ModeratorAccess.IfModeratorOfAnnoucement(_conn, _transaction, _annoucementId, operatingUserOrOperator.Id);
        }
    }
}
