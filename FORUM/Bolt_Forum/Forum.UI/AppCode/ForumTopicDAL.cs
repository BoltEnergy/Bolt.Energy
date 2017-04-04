using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;

namespace Com.Comm100.Forum.UI
{

    public class ForumTopicDAL
    {
        StringBuilder strbldr = new StringBuilder();
        public ForumTopicDAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region "Public Variables"

        public int LoginId;
        public string UserName;
        public string Password;
        public string Address;
        public string Phone;
        #endregion

        #region"Check User Name & Password Module"

        /// <summary>
        /// It will check the Authenticated User and Password.
        /// </summary>
        /// <returns></returns>

        public DataTable CheckLogin()
        {
            try
            {
                SqlParameter[] _param = new SqlParameter[2];
                _param[0] = new SqlParameter("@UserName", UserName);
                //_param[0] = new SqlParameter("@email", Email);
                _param[1] = new SqlParameter("@Password", Password);
                return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "sp_CheckLogin", _param).Tables[0];
            }

            catch (Exception objException)
            {
                //throw the execption to the caller
                throw new Exception(objException.Message);
            }
        }

        #endregion
   
        public DataTable GetUserData(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] _param = new SqlParameter[1];
                _param[0] = new SqlParameter("@id", id);
                return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "sp_GetAdminUser", _param).Tables[0];
            }
            catch (Exception objException)
            {
                //throw the execption to the caller
                throw new Exception(objException.Message);
            }
        }


        public DataTable GetAllUser()
        {
            DataTable dt = new DataTable();
            try
            {
                return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "GetAllUsers").Tables[0];
            }
            catch (Exception objException)
            {
                //throw the execption to the caller
                throw new Exception(objException.Message);
            }
        }


        public int DeleteMyPost(int userId, int TopicId)
        {
            try
            {
                SqlParameter[] _param = new SqlParameter[2];
                _param[0] = new SqlParameter(Constants.UserId, userId);
                _param[1] = new SqlParameter(Constants.TopicId, TopicId);
                int i = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "DeleteTopicByTopicId", _param);
                return i;
            }
            catch (Exception objException)
            {
                //throw the execption to the caller
                throw new Exception(objException.Message);
            }
        }

    }
}