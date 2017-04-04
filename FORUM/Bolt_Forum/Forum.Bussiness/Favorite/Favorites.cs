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
    public abstract class Favorites
    {
        protected SqlConnectionWithSiteId _conn;
        protected SqlTransaction _transaction;

        private int _userOrOperatorId;

        public int UserOrOperatorId
        {
            get { return this._userOrOperatorId; }
        }

        public Favorites(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        {
            this._conn = conn;
            this._transaction = transaction;
            this._userOrOperatorId = userOrOperatorId;
        }

        protected FavoriteWithPermissionCheck[] GetFavoritesByPaging(out int count, int pageIndex, int pageSize, UserOrOperator operatingUserOrOperator)
        {
            count = DataAccess.FavoriteAccess.GetCountOfFavorites(this._conn, this._transaction,this._userOrOperatorId);
            DataTable table = DataAccess.FavoriteAccess.GetFavoritesAndPaging(this._conn, this._transaction, this._userOrOperatorId, pageIndex, pageSize);
            List<FavoriteWithPermissionCheck> Favorites = new List<FavoriteWithPermissionCheck>();
            foreach (DataRow  dr in table.Rows)
            {
                FavoriteWithPermissionCheck tmpFavrite = CreateFavoriteObject(dr, operatingUserOrOperator);
                Favorites.Add(tmpFavrite);
            }
            return Favorites.ToArray<FavoriteWithPermissionCheck>();
        }

        protected FavoriteWithPermissionCheck CreateFavoriteObject(DataRow dr, UserOrOperator operatingUserOrOperator)
        {
            FavoriteWithPermissionCheck favorite;
            int topicId = Convert.ToInt32(dr["TopicId"]);
            string subject = Convert.ToString(dr["Subject"]);
            DateTime postTime = Convert.ToDateTime(dr["PostTime"]);
            int postUserOrOperatorId = Convert.ToInt32(dr["PostUserOrOperatorId"]);
            string postUserOrOperatorName = Convert.ToString(dr["PostUserOrOperatorName"]);
            bool postUserOrOperatorIfDeleted = Convert.ToBoolean(dr["PostUserOrOperatorIfDeleted"]);
            int lastPostId = Convert.ToInt32(dr["LastPostId"]);
            DateTime lastPostTime = Convert.ToDateTime(dr["LastPostTime"]);
            int lastPostUserOrOperatorId = Convert.ToInt32(dr["LastPostUserOrOperatorId"]);
            string lastPostUserOrOperatorName = Convert.ToString(dr["LastPostUserOrOperatorName"]);
            bool lastPostUserOrOperatorIfDeleted = Convert.ToBoolean(dr["LastPostUserOrOperatorIfDeleted"]);
            int numberOfReplies = Convert.ToInt32(dr["NumberOfReplies"]);
            int numberOfHits = Convert.ToInt32(dr["NumberOfHits"]);
            int userOrOperatorId = Convert.ToInt32(dr["UserOrOperatorId"]);
            int forumId = Convert.ToInt32(dr["CurrentForumId"]);
            if (forumId == 0)
            {
                TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, topicId, operatingUserOrOperator);
                forumId = topic.ForumId;
            }
            bool ifMarkedAsAnswer = Convert.ToBoolean(dr["IfMarkedAsAnswer"]);
            bool ifClosed = Convert.ToBoolean(dr["IfClosed"]);
            string[] strTemp = Convert.ToString(dr["ParticipatorIds"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] participatorIds = new int[strTemp.Length];
            int j = 0;
            if (strTemp.Length == 0)
            {
                participatorIds = new int[] { };
            }
            else
            {
                foreach (string item in strTemp)
                {
                    participatorIds[j++] = Convert.ToInt32(item);
                }
            }
            bool ifParticipant = dr["ParticipatorIds"].ToString().Contains(operatingUserOrOperator.Id.ToString()) ? true : false;
            favorite = new FavoriteWithPermissionCheck(this._conn, this._transaction, operatingUserOrOperator, topicId, subject, postTime, postUserOrOperatorId, postUserOrOperatorName, postUserOrOperatorIfDeleted, lastPostId, lastPostTime, lastPostUserOrOperatorId, lastPostUserOrOperatorName, lastPostUserOrOperatorIfDeleted, numberOfReplies, numberOfHits, userOrOperatorId, forumId, ifMarkedAsAnswer, ifClosed, participatorIds, ifParticipant);
            return favorite;

        }

        public virtual void Delete(UserOrOperator operatingOperator, int topicId)
        {
            CheckSystemEnabledFavorite(operatingOperator);
            if (!this.IfUserFavoriteTopic(operatingOperator, topicId))
            {
                ExceptionHelper.ThrowForumFavoriteNotExistException(topicId);
            }
            FavoriteWithPermissionCheck favorite = new FavoriteWithPermissionCheck(this._conn, this._transaction, operatingOperator, topicId, this._userOrOperatorId);
            favorite.Delete();
            /*2.0 stategy */
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, topicId,
              operatingOperator);
            UserOrOperator Author = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction,
                operatingOperator, topic.PostUserOrOperatorId);
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingOperator, _conn.SiteId);
            scoreStrategySetting.UseAfterRemoveTopicFromFavorites(Author);
            /*2.0 reputation strategy*/
            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingOperator, _conn.SiteId);
            reputationStrategySetting.UseAfterRemoveTopicFromFavorites(Author);
        }

        public virtual void Add(UserOrOperator operatingOperator, int topicId,int forumId)
        {
            CheckSystemEnabledFavorite(operatingOperator);
            if (this.IfUserFavoriteTopic(operatingOperator, topicId))
            {
                ExceptionHelper.ThrowForumFavortieIsExsitException(topicId);
            }
            bool ifAnnoucement;
            TopicBase topicbase = TopicFactory.CreateTopic(_conn, _transaction, topicId, operatingOperator, out ifAnnoucement);
            if (!ifAnnoucement)
                forumId = 0;
            Favorite.Add(_conn, _transaction, operatingOperator.Id, topicId,forumId);
            /*2.0 stategy */
            //TopicWithPermissionCheck topic = new TopicWithPermissionCheck(_conn, _transaction, topicId,
            //    operatingOperator);

            UserOrOperator Author = UserOrOperatorFactory.CreateUserOrOperator(_conn, _transaction,
                operatingOperator, topicbase.PostUserOrOperatorId);
            ScoreStrategySettingWithPermissionCheck scoreStrategySetting = new ScoreStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingOperator, _conn.SiteId);
            scoreStrategySetting.UseAfterAddTopicToFavorites(Author);
            /*2.0 reputation strategy*/
            ReputationStrategySettingWithPermissionCheck reputationStrategySetting = new ReputationStrategySettingWithPermissionCheck(
                _conn, _transaction, operatingOperator, _conn.SiteId);
            reputationStrategySetting.UseAfterAddTopicToFavorites(Author);
        }

        public virtual bool IfUserFavoriteTopic(UserOrOperator operatingOperator, int topicId)
        {
           return FavoriteAccess.IfUserFavoriteTopic(_conn, _transaction, operatingOperator.Id, topicId);
        }

        public void CheckSystemEnabledFavorite(UserOrOperator operatingUserOrOperator)
        {
            ForumFeatureWithPermissionCheck forumFeature = new ForumFeatureWithPermissionCheck(_conn, _transaction,
                operatingUserOrOperator);
             if (!forumFeature.IfEnableFavorite)
             {
                 ExceptionHelper.ThrowForumSettingsCloseFavoriteFunction();
             }
        }
    }
}
