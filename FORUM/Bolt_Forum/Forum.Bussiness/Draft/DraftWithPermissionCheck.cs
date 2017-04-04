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

namespace Com.Comm100.Forum.Bussiness
{
    public class DraftWithPermissionCheck : Draft
    {
        UserOrOperator _operatingUserOrOperator;

        public DraftWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int draftId, UserOrOperator operatingOperator)
            : base(conn, transaction, draftId)
        {
            this._operatingUserOrOperator = operatingOperator;
        }

        public DraftWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int topicId, int nothing, UserOrOperator operatingOperator)
            : base(conn, transaction, topicId,nothing)
        {
            this._operatingUserOrOperator = operatingOperator;
        }

        public DraftWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int draftId, UserOrOperator operatingOperator, int topicId, string subject, string content,
            int createOperatorId, string createOperatorName, DateTime createTime, int lastUpdateOperatorId, string lastUpdateOperatorName, DateTime lastUpdateTime)
            : base(conn, transaction, draftId, topicId, subject, content, createOperatorId, createOperatorName, createTime, lastUpdateOperatorId, lastUpdateOperatorName, lastUpdateTime)
        {
            this._operatingUserOrOperator = operatingOperator;
        }

        public void Update(string subject, string content, DateTime updateTime)
        {
            CheckPermission();
            base.Update(subject, content, updateTime, _operatingUserOrOperator);
        }
       
        public override void Delete()
        {
            CheckPermission();
            base.Delete();
        }

        #region Private Function CheckPermission
        private void CheckPermission()
        {
            if (_operatingUserOrOperator == null)
            {
                ExceptionHelper.ThrowForumOperatingUserOrOperatorCanNotBeNullException();
            }
            else if (_operatingUserOrOperator.IfDeleted)
            {
                ExceptionHelper.ThrowForumUserOrOperatorNotExistWithNameException(_operatingUserOrOperator.DisplayName);
            }
            else if (!_operatingUserOrOperator.IfActive)
            {
                ExceptionHelper.ThrowForumUserOrOperatorNotActiveWithNameException(_operatingUserOrOperator.DisplayName);
            }
            if (!CommFun.IfOperator(_operatingUserOrOperator))
            {
                if (!_operatingUserOrOperator.IfForumAdmin)
                {
                    ExceptionHelper.ThrowForumOnlyAdministratorsHavePermissionException();
                }
            }
        }
        #endregion Private Function CheckPermission

        public TopicWithPermissionCheck GetTopic()
        {
            return base.GetTopic(_operatingUserOrOperator);
        }
    }
}
