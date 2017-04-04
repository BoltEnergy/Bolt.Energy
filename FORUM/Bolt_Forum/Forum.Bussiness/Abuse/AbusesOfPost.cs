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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public abstract class AbusesOfPost : AbusesBase
    {
        private int _postId;

        public int PostId { get { return _postId; } }

        public AbusesOfPost(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId)
            :base(conn, transaction)
        {
            _postId = postId;
        }

        private bool IfCanAdd()
        {
            int countOfNotCanceled = AbuseAccess.GetCountOfNotCanceledAbusesByPostId(_conn, _transaction, _postId);
            return countOfNotCanceled <= 0;
        }

        public virtual int Add(int abuseUserOrOperatorId, string note, DateTime abuseDate,UserOrOperator operatingUserOrOpertaor)
        {
            int abuseId = 0;
            //if (IfCanAdd())
                abuseId = Abuse.Add(_conn, _transaction, _postId, abuseUserOrOperatorId, abuseDate, note);
            /*2.0 reputation strategy*/
            PostWithPermissionCheck post = new PostWithPermissionCheck(_conn,_transaction,_postId,operatingUserOrOpertaor);
            UserOrOperator ReportedUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, operatingUserOrOpertaor,
                post.PostUserOrOperatorId);
            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingUserOrOpertaor, _conn.SiteId);
            reputationStrategySetting.UseAfterReportAbuse(ReportedUserOrOperator);
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(_conn,
                _transaction, operatingUserOrOpertaor, _conn.SiteId);
            scoreStrategySetting.UseAfterReportAbuse(ReportedUserOrOperator);
            return abuseId;
        }

        protected void ApproveAubsesOfPost(UserOrOperator operatingUserOrOperator)
        {
            foreach (Abuse abuse in this.GetAllAbuses(operatingUserOrOperator))
            {
                abuse.Approve();
            }

            PostWithPermissionCheck post = new PostWithPermissionCheck(_conn, _transaction, _postId, operatingUserOrOperator);

            /*2.0 reputation strategy*/

            UserOrOperator postAuthor = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction, operatingUserOrOperator,
                post.PostUserOrOperatorId);

            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            reputationStrategySetting.UseAfterApproveAbuseReport(post, postAuthor);
            /*2.0 */
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
               _conn, _transaction, operatingUserOrOperator, _conn.SiteId);
            scoreStrategySetting.UseAfterApproveAbuseReport(post, postAuthor);
        }

        public void RefuseAbusesOfPost(UserOrOperator operatingUserOrOperator)
        {
            foreach (Abuse abuse in this.GetAllAbuses(operatingUserOrOperator))
            {
                abuse.Refuse();
            }
        }

        public int GetCountOfAbuse()
        {
            return AbuseAccess.GetCountOfAbusesByPostId(_conn, _transaction, _postId);
        }

        protected AbuseWithPermissionCheck[] GetAllAbuses(UserOrOperator operatingUserOrOperator)
        {
            DataTable table = AbuseAccess.GetAbusesByPostId(_conn, _transaction, _postId);
            List<AbuseWithPermissionCheck> abuses = new List<AbuseWithPermissionCheck>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                abuses.Add(CreateAbuseObject(table.Rows[i], operatingUserOrOperator));
            }
            return abuses.ToArray();
        }

        protected AbuseWithPermissionCheck[] GetAllAbusesOfUser(UserOrOperator operatingUserOrOperator,int userId)
        {
            DataTable table = AbuseAccess.GetAbusesOfUserByPostId(_conn, _transaction, _postId,userId);
            List<AbuseWithPermissionCheck> abuses = new List<AbuseWithPermissionCheck>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                abuses.Add(CreateAbuseObject(table.Rows[i], operatingUserOrOperator));
            }
            return abuses.ToArray();
        }
    }
}
