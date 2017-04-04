#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.Bussiness;
using System.IO;

namespace Com.Comm100.Forum.Process
{
    public class UserControlProcess
    {
        public static void UpdateUserOrOperatorProfile(int siteId, int userOrOperatorId, bool ifOperator,
            string email, string displayName, string firstName, string lastName, int age, EnumGender gender,
            string company, string occupation, string phone, string fax, string interests, string homepage,string score,string reputation,
            bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender, bool ifShowOccupation,
            bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests, bool ifShowHomePage)
        {
            SqlConnectionWithSiteId conn = null;

            #region Filed Trim
            email = email.Trim();
            displayName = displayName.Trim();
            firstName = firstName.Trim();
            lastName = lastName.Trim();
            company = company.Trim();
            occupation = occupation.Trim();
            phone = phone.Trim();
            fax = fax.Trim();
            interests = interests.Trim();
            homepage = homepage.Trim();
            #endregion
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, userOrOperatorId);
                userOrOperator.UpdateProfile(email, displayName, firstName, lastName, age, gender, company, occupation,
                                            phone, fax, interests, homepage,score,reputation, ifShowEmail, ifShowUserName, ifShowAge,
                                            ifShowGender, ifShowOccupation, ifShowCompany, ifShowPhone, ifShowFax,
                                            ifShowInterests, ifShowHomePage);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void UpdateUserOrOperatorSignature(int siteId, int userOrOperatorId, bool ifOperator, string signature)
        {
            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            signature = signature.Trim();

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                transaction = conn.SqlConn.BeginTransaction();

                UserOrOperator operatingOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, userOrOperatorId);
                PostImagesWithPermissionCheck postImages = new PostImagesWithPermissionCheck(conn, transaction, userOrOperatorId, (Int16)Com.Comm100.Framework.Enum.Forum.EnumForumPostImageUseType.Signature, operatingOperator);
                int[] imageIDs = Com.Comm100.Framework.Common.CommonFunctions.GetPostContentImageIds(signature);
                postImages.AttachToPost(imageIDs);
                
                operatingOperator.UpdateSignature(signature);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void UpdateUserOrOperatorAvatar(int siteId, int userOrOperatorId, bool ifOperator, byte[] avatar)
        {
            #region Update Database
            SqlConnectionWithSiteId conn = null;
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, userOrOperatorId);
                UserOrOperator userOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, operatingOperator, userOrOperatorId);
                userOrOperator.UpdateAvatar(avatar);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
            #endregion
            #region Update avatar File To Temp File
            FileStream fs = null;
            try
            {
                string strDirectory = System.Web.HttpContext.Current.Server.MapPath(@"~/" +
                                        ConstantsHelper.ForumAvatarTemporaryFolder + @"/" + siteId.ToString());
                if (Directory.Exists(strDirectory) == false)
                    Directory.CreateDirectory(strDirectory);
                string strPath = strDirectory + @"\" + userOrOperatorId.ToString() + ConstantsHelper.User_Avatar_FileType;
                string strPathTemp = strPath + ".Temp";
                try
                {
                    fs = File.Create(strPathTemp);
                    fs.Write(avatar, 0, avatar.Length);
                }
                catch( Exception){throw;}
                finally
                {
                    if (fs != null)
                        fs.Close();
                }
                File.Delete(strPath);
                File.Move(strPathTemp, strPath);
            }
            catch(Exception)
            {
                throw;
            }
           
            #endregion
        }

        public static void UpdateUserOrOperatorAvatarAsSystemProvided(int siteId, int userOrOperatorId, bool ifOperator, string avatar)
        {
            SqlConnectionWithSiteId conn = null;

            avatar = avatar.Trim();

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                //Database only Save System Avatar's FileName
                string strAvatarFileName = Path.GetFileName(avatar);
                UserOrOperator operatingOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, userOrOperatorId);
                operatingOperator.UpdateAvatarAsSystemProvided(strAvatarFileName);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static void ResetUserOrOperatorPassword(int siteId, int userOrOperatorId, bool ifOperator,
            string oldPassword, string newPassword)
        {
            SqlConnectionWithSiteId conn = null;

            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, userOrOperatorId);
                operatingOperator.ResetPassword(oldPassword, newPassword);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static TopicWithPermissionCheck[] GetMyTopicsByPaging(int siteId, int operatingUserOrOperatorId, bool ifOperator, int pageIndex, int pageSize)
        {

            SqlConnectionWithSiteId conn = null;
            SqlTransaction transaction = null;
            
            try  
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingUserOrOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, transaction, null, operatingUserOrOperatorId);
                return null;//operatingUserOrOperator.GetTopicsWhichInvolvedByPaging(pageIndex, pageSize, operatingUserOrOperator);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }

        }

        //public static int GetCountOfMyTopics(int siteId, int operatingUserOrOperatorId,bool ifOperator)
        //{
        //    SqlConnectionWithSiteId conn= null;
        //    SqlTransaction sqlTransaction = null;
        //    try
        //    {
        //        conn = DbHelper.GetSqlConnection(siteId);
        //        DbHelper.OpenConn(conn);
        //        UserOrOperator operatingUserOrOperator = ProcessUtil.GetUserOrOperator(conn, sqlTransaction, operatingUserOrOperatorId);
        //        return operatingUserOrOperator.GetCountOfTopicsWhichInvolved();
        //    }
        //    catch (System.Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        DbHelper.CloseConn(conn);
        //    }
        //}

        public static PostWithPermissionCheck[] GetMyPostsByPaging(int siteId, int operatingUserOrOperatorId,
            int pageIndex, int pageSize,string keyword,DateTime startDate, DateTime endDate,out int count)
        {
            SqlConnectionWithSiteId conn = null;
            
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);
                UserOrOperator operatingOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, 
                    operatingUserOrOperatorId);
                PostsOfUserOrOperatorWithPermissionCheck postsOfUserOrOperator = operatingOperator.GetPosts();
                return postsOfUserOrOperator.GetPostsByQueryAndPaging(pageIndex, pageSize, keyword, 
                    startDate, endDate,out count); 

            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }

        public static int GetCountOfPosts(int siteId, int operatingUserOrOperatorId, bool ifOperator)
        {
            SqlConnectionWithSiteId conn = null;
            
            try
            {
                conn = DbHelper.GetSqlConnection(siteId);
                DbHelper.OpenConn(conn);

                UserOrOperator operatingOperator = UserOrOperatorFactory.CreateUserOrOperator(conn, null, null, operatingUserOrOperatorId);
                return 0;//operatingOperator.GetCountOfTopicsWhichPosted();
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                DbHelper.CloseConn(conn);
            }
        }
    }
}
