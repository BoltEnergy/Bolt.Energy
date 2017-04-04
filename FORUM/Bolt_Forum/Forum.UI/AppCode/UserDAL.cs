using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Com.Comm100.Forum.UI
{
    public class UserDAL
    {
        public int AddUpdateUserInfo(LoginUserBE user)
        {
            try
            {
                SqlParameter[] _param = new SqlParameter[4];
                _param[0] = new SqlParameter(Constants.Email, user.Email);
                _param[1] = new SqlParameter(Constants.FirstName, user.FirstName);
                _param[2] = new SqlParameter(Constants.LastName, user.LastName);
                _param[3] = new SqlParameter(Constants.MongoUID, user.MongoUID);

                Int32 id = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "AddUpdateUser", _param));
                return id;
            }
            catch (Exception objException)
            {
                //throw the execption to the caller
                throw new Exception(objException.Message);
            }
        }

        public Boolean IfUserExist(LoginUserBE user)
        {
            try
            {
                SqlParameter[] _param = new SqlParameter[1];
                _param[0] = new SqlParameter(Constants.Email, user.Email);

                Int32 id = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "IfUserExists", _param));
                if(id>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
 
            }
            catch (Exception objexception)
            {
                throw new Exception(objexception.Message);
            }
        }
    }
}