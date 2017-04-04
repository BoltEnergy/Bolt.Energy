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
    public class SubscribeWithPermissionCheck : Subscribe
    {
        UserOrOperator _operatingUserOrOperator;

        public override bool IfParticipant
        {
            get 
            {
                bool blTmp = false;
                if (base.ParticipatorIds != null)
                {
                    for (int i = 0; i < base.ParticipatorIds.Length; i++)
                    {

                        if (_operatingUserOrOperator != null)
                        {
                            if (_operatingUserOrOperator.Id == ParticipatorIds[i])
                            {
                                blTmp = true;
                                break;
                            }
                        }
                    }
                }
                return blTmp;/*need to archive*/
            }
        }

        public SubscribeWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int topicId, int userOrOperatorId)
            : base(conn, transaction, topicId, userOrOperatorId)
        {
            _operatingUserOrOperator = operatingUserOrOperator;
        }
        public SubscribeWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, UserOrOperator operatingUserOrOperator, int topicId, string subject, DateTime postTime,
            int postUserOrOperatorId, string postUserOrOperatorName, bool postUserOrOperatorIfDeleted, int lastPostId,
            DateTime lastPostTime, int lastPostUserOrOperatorId, string lastPostUserOrOperatorName, bool lastPostUserOrOperatorIfDeleted,
            int numberOfReplies, int numberOfHits, int forumId, int userOrOperatorId, bool ifMarkedAsAnswer, bool ifClosed, int[] participatorIds, DateTime subscribeDate)
            : base(conn, transaction,topicId, subject, postTime, postUserOrOperatorId, postUserOrOperatorName, postUserOrOperatorIfDeleted, lastPostId, 
            lastPostTime,  lastPostUserOrOperatorId,  lastPostUserOrOperatorName,lastPostUserOrOperatorIfDeleted, numberOfReplies,
            numberOfHits, forumId, userOrOperatorId, ifMarkedAsAnswer, ifClosed, participatorIds, subscribeDate)
        {
            _operatingUserOrOperator = operatingUserOrOperator; 
        }
    }
}
