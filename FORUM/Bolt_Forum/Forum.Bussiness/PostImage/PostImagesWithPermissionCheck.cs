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
    public class PostImagesWithPermissionCheck : PostImages
    {
        UserOrOperator _operatingUserOrOperator;

        public PostImagesWithPermissionCheck(SqlConnectionWithSiteId conn, SqlTransaction transaction, int postId, int useType, UserOrOperator operatingUserOrOperator)
            :base(conn,transaction,postId,useType)
        {
            this._operatingUserOrOperator = operatingUserOrOperator;
        }

        public override int Add(string name, string type, byte[] file)
        {
            this.CheckAddPermission();
            return base.Add(name, type, file);
        }

        public override void Delete()
        {
            this.CheckDeletePermission();
            base.Delete();
        }

        public override void AttachToPost(int[] imageIds)
        {
            this.CheckAttachPermission();
            base.AttachToPost(imageIds);
        }

        private void CheckAddPermission()
        {
            if (this._operatingUserOrOperator == null)
                ExceptionHelper.ThrowUserNotLoginException();
        }

        private void CheckAttachPermission()
        {
            if (this._operatingUserOrOperator == null)
                ExceptionHelper.ThrowUserNotLoginException();
        }

        private void CheckDeletePermission()
        {
            if (base.PostId > 0 && !this.IfAdmin() && !this.IfModerators() && !this.IfOwn())
                ExceptionHelper.ThrowSystemNotEnoughPermissionException(_operatingUserOrOperator.DisplayName);
        }

        private bool IfAdmin()
        {
            //bool ifAdmin = false;
            //if (_operatingUserOrOperator != null)
            //{
            //    if (CommFun.IfOperator(_operatingUserOrOperator))
            //    {
            //        if (((OperatorWithPermissionCheck)_operatingUserOrOperator).IfAdmin)
            //        {
            //            ifAdmin = true;
            //        }
            //    }
            //}
            //return ifAdmin;
            return CommFun.IfAdminInUI(_operatingUserOrOperator);
        }
        private bool IfModerators()
        {
            PostWithPermissionCheck post = new PostWithPermissionCheck(this._conn, this._transaction, PostId, this._operatingUserOrOperator);
            TopicWithPermissionCheck topic = new TopicWithPermissionCheck(this._conn, this._transaction, post.TopicId, _operatingUserOrOperator);
            //ModeratorsWithPermisisonCheck moderators = new ModeratorsWithPermisisonCheck(this._conn, this._transaction, topic.ForumId, _operatingUserOrOperator);
            //Moderator[] moderator = moderators.GetAllModerators();

            //bool ifModerator = false;
            //for (int i = 0; i < moderator.Length; i++)
            //{
            //    if (_operatingUserOrOperator != null)
            //    {
            //        if (moderator[i].Id == this._operatingUserOrOperator.Id)
            //        {
            //            ifModerator = true;
            //            break;
            //        }
            //    }
            //}
            //return ifModerator;
            return CommFun.IfModeratorInUI(_conn,_transaction,topic.ForumId,_operatingUserOrOperator);
        }
        private bool IfOwn()
        {
            PostWithPermissionCheck post = new PostWithPermissionCheck(this._conn, this._transaction, PostId, this._operatingUserOrOperator);

            bool ifOwn = false;
            if (_operatingUserOrOperator != null)
            {
                if (this._operatingUserOrOperator.Id == post.PostUserOrOperatorId)
                {
                    ifOwn = true;
                }
            }
            return ifOwn;
        }

    }
}
