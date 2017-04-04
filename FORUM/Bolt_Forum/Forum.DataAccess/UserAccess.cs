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
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.DataAccess
{
    public class UserAccess
    {        
        public static DataTable GetNotDeletedUsersByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, string orderField, string EmailOrdisplayNameKeyWord)
        {

            EmailOrdisplayNameKeyWord = CommonFunctions.SqlReplace(EmailOrdisplayNameKeyWord);

            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;

            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("select * from (");
            strSQL.Append(string.Format(" select ROW_NUMBER() over(order by {0}) as [row], * from t_User{1}", orderField, conn.SiteId));
            strSQL.Append(string.Format(" where UserType={0} and IfDeleted=0 and IfVerified=1", Convert.ToInt16(EnumUserType.User)));
            strSQL.Append(" and (Name like '%'+@Name+'%' escape '/' ");
            strSQL.Append(" or Email like '%'+@Email+'%' escape '/') ");
            strSQL.Append(" ) temp");
            strSQL.Append(string.Format(" where temp.row between {0} and {1}", startRowNum, endRowNum));

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@Name", EmailOrdisplayNameKeyWord);
            cmd.Parameters.AddWithValue("@Email", EmailOrdisplayNameKeyWord);


            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static int GetCountOfNotDeletedUsersByQuery(SqlConnectionWithSiteId conn, SqlTransaction transaction, string EmailOrdisplayNameKeyWord)
        {
            EmailOrdisplayNameKeyWord = CommonFunctions.SqlReplace(EmailOrdisplayNameKeyWord);

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select count(Id) from t_User" + conn.SiteId.ToString());
            strSQL.Append(string.Format(" where UserType={0} and IfDeleted=0 and IfVerified=1  ", Convert.ToInt16(EnumUserType.User)));
            strSQL.Append(" and (Name like '%'+@Name+'%' escape '/' or Email like '%'+@Email+'%' escape '/')");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@Name", EmailOrdisplayNameKeyWord);
            cmd.Parameters.AddWithValue("@Email", EmailOrdisplayNameKeyWord);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable GetNotModeratedUsersByPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, string orderField, string emailOrDisplayNameKeyWord)
        {

            emailOrDisplayNameKeyWord = CommonFunctions.SqlReplace(emailOrDisplayNameKeyWord);
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from (");
            strSQL.Append(string.Format(" select ROW_NUMBER() over(order by {0}) as [row], * from t_User{1}", orderField, conn.SiteId));
            strSQL.Append(" where IfDeleted=0 and ModerateStatus=" + Convert.ToInt16(EnumUserModerateStatus.enumNotModerated) + " and UserType=2");
            strSQL.Append(" and (Name like '%'+@emailOrDisplayNameKeyWord+'%' escape '/' or Email like '%'+@emailOrDisplayNameKeyWord+'%')");
            strSQL.Append(" ) t where [row] between @startRowNum and  @endRowNum");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@emailOrDisplayNameKeyWord", emailOrDisplayNameKeyWord);
            cmd.Parameters.AddWithValue("@startRowNum", startRowNum);
            cmd.Parameters.AddWithValue("@endRowNum", endRowNum);


            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;

        }

        public static int GetCountOfNotModeratedUsersBySearch(SqlConnectionWithSiteId conn, SqlTransaction transaction, string emailOrDisplayNameKeyWord)
        {
            emailOrDisplayNameKeyWord = CommonFunctions.SqlReplace(emailOrDisplayNameKeyWord);

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(string.Format("select count(Id) from t_User{0}" ,conn.SiteId.ToString() ));
            strSQL.Append(" where IfDeleted=0 and ModerateStatus=" + Convert.ToInt16(EnumUserModerateStatus.enumNotModerated).ToString() + " and UserType=2 ");
            strSQL.Append(" and (Name like '%'+@emailOrDisplayNameKeyWord+'%' escape '/' or Email like '%'+@emailOrDisplayNameKeyWord+'%' escape '/' )");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@emailOrDisplayNameKeyWord", emailOrDisplayNameKeyWord);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }


        public static DataTable GetUserOrOperatorById(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int userOrOperatorId)//-----------------------------------this for search post by topic keyword------//
        { 

            StringBuilder strSQL = new StringBuilder("select u.*,r.Posts from t_User" + conn.SiteId + " u");
            strSQL.Append(" left join (select PostUserOrOperatorId,COUNT(Id) as Posts ");
            strSQL.Append(" from t_Forum_Post" + conn.SiteId);
            strSQL.Append(" where IfDeleted=0 ");
            strSQL.Append(" and TopicId not in (");
            strSQL.Append(string.Format(" (select distinct TopicId from t_Forum_Announcement where SiteId = {0}) ", conn.SiteId));
            strSQL.Append(" union ");
            strSQL.Append(string.Format(" (select Id from t_Forum_Topic{0} b where IfDeleted = 1)) ", conn.SiteId));
            strSQL.Append(" group by PostUserOrOperatorId) r");
            strSQL.Append(" on u.Id=r.PostUserOrOperatorId");
            strSQL.Append(" where Id=@Id and UserType <> @UserType");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@Id", userOrOperatorId);
            cmd.Parameters.AddWithValue("@UserType", (Int16)EnumUserType.Contact);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetNotDeletedUserOrOperatorByEmail(SqlConnectionWithSiteId conn, SqlTransaction transaction, string email)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select *  from t_User" + conn.SiteId + " where IfDeleted=0 and Email=@Email and UserType <> @UserType");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@UserType", (Int16)EnumUserType.Contact);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetNotDeletedUserOrOperatorByEmailwoPwd(SqlConnectionWithSiteId conn, SqlTransaction transaction, string email)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select *  from t_User" + conn.SiteId + " where IfDeleted=0 and Email=@Email and UserType <> @UserType");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@UserType", (Int16)EnumUserType.Contact);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetNotDeletedUserOrOperatorByName(SqlConnectionWithSiteId conn, SqlTransaction transaction, string name)
        {


            StringBuilder strSQL = new StringBuilder("select * ");
            strSQL.Append(" from t_User" + conn.SiteId + " where IfDeleted=0 and Name=@name and UserType <> @UserType");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("UserType", (Int16)EnumUserType.Contact);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetNotDeletedUserById(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int userId)
        {

            StringBuilder strSQL = new StringBuilder("select Id,Email,UserType,Name,Password,Description,IfAdmin,IfDeleted,IfActive");
            strSQL.Append(",ForgetPasswordGUIDTag,ForgetPasswordTagTime,IfVisitorEnterSiteSoundOn,IfIncomingChatSoundOn");
            strSQL.Append(",IfTransferringChatSoundOn,IfNewReponseSoundOn,IfOperatorMessageSoundOn,IfChatEndedSoundOn,IfVerified");
            strSQL.Append(",ModerateStatus,EmailVerificationStatus,Posts,JoinedTime,JoinedIP,LastLoginTime,LastLoginIP,IfShowEmail");
            strSQL.Append(",FirstName,LastName,IfShowUserName,Age,IfShowAge,Gender,IfShowGender,Occupation,IfShowOccupation");
            strSQL.Append(",Company,IfShowCompany,PhoneNumber,IfShowPhoneNumber,FaxNumber,IfShowFaxNumber,Interests,IfShowInterests");
            strSQL.Append(",HomePage,IfShowHomePage,IfCustomizeAvatar,SystemAvatar,CustomizeAvatar,Signature from t_User" + conn.SiteId + " where Id=@Id and IfDeleted=0 and UserType=2");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@Id", userId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetUserById(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int userId)
        {

            StringBuilder strSQL = new StringBuilder("select u.*,r.Posts from t_User" + conn.SiteId + " u");
            strSQL.Append(" left join (select PostUserOrOperatorId,COUNT(Id) as Posts ");
            strSQL.Append(" from t_Forum_Post"+conn.SiteId);
            strSQL.Append(" where IfDeleted=0 ");
            strSQL.Append(" and TopicId not in (");
	        strSQL.Append(string.Format(" (select distinct TopicId from t_Forum_Announcement where SiteId = {0}) ",conn.SiteId));
	        strSQL.Append(" union ");
	        strSQL.Append(string.Format(" (select Id from t_Forum_Topic{0} b where IfDeleted = 1))",conn.SiteId));
            strSQL.Append(" group by PostUserOrOperatorId) r");
            strSQL.Append(" on u.Id=r.PostUserOrOperatorId");
            strSQL.Append(" where Id=@Id and UserType=2");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@Id", userId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetNotDeletedOperatorById(SqlConnectionWithSiteId conn, SqlTransaction transaction, int operatorId)
        {

            StringBuilder strSQL = new StringBuilder("select Id,Email,UserType,Name,Password,Description,IfAdmin,IfDeleted,IfActive");
            strSQL.Append(",ForgetPasswordGUIDTag,ForgetPasswordTagTime,IfVisitorEnterSiteSoundOn,IfIncomingChatSoundOn");
            strSQL.Append(",IfTransferringChatSoundOn,IfNewReponseSoundOn,IfOperatorMessageSoundOn,IfChatEndedSoundOn,IfVerified");
            strSQL.Append(",ModerateStatus,EmailVerificationStatus,Posts,JoinedTime,JoinedIP,LastLoginTime,LastLoginIP,IfShowEmail");
            strSQL.Append(",FirstName,LastName,IfShowUserName,Age,IfShowAge,Gender,IfShowGender,Occupation,IfShowOccupation");
            strSQL.Append(",Company,IfShowCompany,PhoneNumber,IfShowPhoneNumber,FaxNumber,IfShowFaxNumber,Interests,IfShowInterests");
            strSQL.Append(",HomePage,IfShowHomePage,IfCustomizeAvatar,SystemAvatar,Signature from t_User" + conn.SiteId + " where Id=@Id and IfDeleted=0 and UserType=1 ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@Id", operatorId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetOperatorById(SqlConnectionWithSiteId conn, SqlTransaction transaction, int operatorId)
        {

            StringBuilder strSQL = new StringBuilder("select Id,Email,UserType,Name,Password,Description,IfAdmin,IfDeleted,IfActive");
            strSQL.Append(",ForgetPasswordGUIDTag,ForgetPasswordTagTime,IfVisitorEnterSiteSoundOn,IfIncomingChatSoundOn");
            strSQL.Append(",IfTransferringChatSoundOn,IfNewReponseSoundOn,IfOperatorMessageSoundOn,IfChatEndedSoundOn,IfVerified");
            strSQL.Append(",ModerateStatus,EmailVerificationStatus,Posts,JoinedTime,JoinedIP,LastLoginTime,LastLoginIP,IfShowEmail");
            strSQL.Append(",FirstName,LastName,IfShowUserName,Age,IfShowAge,Gender,IfShowGender,Occupation,IfShowOccupation");
            strSQL.Append(",Company,IfShowCompany,PhoneNumber,IfShowPhoneNumber,FaxNumber,IfShowFaxNumber,Interests,IfShowInterests");
            strSQL.Append(",HomePage,IfShowHomePage,IfCustomizeAvatar,SystemAvatar,Signature from t_User" + conn.SiteId + " where Id=@Id and UserType=1 ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@Id", operatorId);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetNotDeletedUserByEmail(SqlConnectionWithSiteId conn, SqlTransaction transaction, string email)
        {

            StringBuilder strSQL = new StringBuilder("select Id,Email,UserType,Name,Password,Description,IfAdmin,IfDeleted,IfActive");
            strSQL.Append(",ForgetPasswordGUIDTag,ForgetPasswordTagTime,IfVisitorEnterSiteSoundOn,IfIncomingChatSoundOn");
            strSQL.Append(",IfTransferringChatSoundOn,IfNewReponseSoundOn,IfOperatorMessageSoundOn,IfChatEndedSoundOn,IfVerified");
            strSQL.Append(",ModerateStatus,EmailVerificationStatus,Posts,JoinedTime,JoinedIP,LastLoginTime,LastLoginIP,IfShowEmail");
            strSQL.Append(",FirstName,LastName,IfShowUserName,Age,IfShowAge,Gender,IfShowGender,Occupation,IfShowOccupation");
            strSQL.Append(",Company,IfShowCompany,PhoneNumber,IfShowPhoneNumber,FaxNumber,IfShowFaxNumber,Interests,IfShowInterests");
            strSQL.Append(",HomePage,IfShowHomePage,IfCustomizeAvatar,SystemAvatar,CustomizeAvatar,Signature from t_User" + conn.SiteId + " where Email=@Email and IfDeleted=0 and UserType=2 ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@Email", email);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetUserByEmail(SqlConnectionWithSiteId conn, SqlTransaction transaction, string email)
        {

            StringBuilder strSQL = new StringBuilder("select * from t_User" + conn.SiteId + " where Email=@Email and UserType=2 ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@Email", email);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static byte[] GetUserOrOperatorAvatar(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        {

            StringBuilder strSQL = new StringBuilder("select CustomizeAvatar from t_User" + conn.SiteId + " where Id=@Id and IfDeleted=0 ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@Id", userOrOperatorId);

            object avatar = cmd.ExecuteScalar();

            if (avatar is DBNull)
                return null;
            else
                return (byte[])avatar;
        }

        public static DataTable GetNotDeletedOperatorByEmail(SqlConnectionWithSiteId conn, SqlTransaction transaction, string email)
        {

            StringBuilder strSQL = new StringBuilder("select Id,Email,UserType,Name,Password,Description,IfAdmin,IfDeleted,IfActive");
            strSQL.Append(",ForgetPasswordGUIDTag,ForgetPasswordTagTime,IfVisitorEnterSiteSoundOn,IfIncomingChatSoundOn");
            strSQL.Append(",IfTransferringChatSoundOn,IfNewReponseSoundOn,IfOperatorMessageSoundOn,IfChatEndedSoundOn,IfVerified");
            strSQL.Append(",ModerateStatus,EmailVerificationStatus,Posts,JoinedTime,JoinedIP,LastLoginTime,LastLoginIP,IfShowEmail");
            strSQL.Append(",FirstName,LastName,IfShowUserName,Age,IfShowAge,Gender,IfShowGender,Occupation,IfShowOccupation");
            strSQL.Append(",Company,IfShowCompany,PhoneNumber,IfShowPhoneNumber,FaxNumber,IfShowFaxNumber,Interests,IfShowInterests");
            strSQL.Append(",HomePage,IfShowHomePage,IfCustomizeAvatar,SystemAvatar,CustomizeAvatar,Signature from t_User" + conn.SiteId + " where Email=@Email and IfDeleted=0 and UserType=1 ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@Email", email);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static DataTable GetOperatorByEmail(SqlConnectionWithSiteId conn, SqlTransaction transaction, string email)
        {

            StringBuilder strSQL = new StringBuilder("select Id,Email,UserType,Name,Password,Description,IfAdmin,IfDeleted,IfActive");
            strSQL.Append(",ForgetPasswordGUIDTag,ForgetPasswordTagTime,IfVisitorEnterSiteSoundOn,IfIncomingChatSoundOn");
            strSQL.Append(",IfTransferringChatSoundOn,IfNewReponseSoundOn,IfOperatorMessageSoundOn,IfChatEndedSoundOn,IfVerified");
            strSQL.Append(",ModerateStatus,EmailVerificationStatus,Posts,JoinedTime,JoinedIP,LastLoginTime,LastLoginIP,IfShowEmail");
            strSQL.Append(",FirstName,LastName,IfShowUserName,Age,IfShowAge,Gender,IfShowGender,Occupation,IfShowOccupation");
            strSQL.Append(",Company,IfShowCompany,PhoneNumber,IfShowPhoneNumber,FaxNumber,IfShowFaxNumber,Interests,IfShowInterests");
            strSQL.Append(",HomePage,IfShowHomePage,IfCustomizeAvatar,SystemAvatar,CustomizeAvatar,Signature from t_User" + conn.SiteId + " where Email=@Email and UserType=1 ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@Email", email);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static void UpdateUseProfile(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int userId, string email, string displayName, string firstName, string lastName, int age,
            EnumGender gender, string company, string occupation, string phone, string fax, string interests,
            string homepage,string score,string reputation, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender,
            bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests,
            bool ifShowHomePage)
        {

            StringBuilder strSQL = new StringBuilder("Update t_User" + conn.SiteId.ToString() + " set ");
            strSQL.Append("Email = @email, Name = @displayName, FirstName = @firstName, LastName = @lastName, ");
            strSQL.Append("Age = @age, Gender = @gender, Company = @company, Occupation = @occupation, ");
            strSQL.Append("PhoneNumber = @phone, FaxNumber = @fax, Interests = @interests, Homepage = @homepage,ForumScore=@score,ForumReputation=@reputation, ");
            strSQL.Append("IfShowEmail = @ifShowEmail, IfShowUserName = @ifShowUserName, IfShowAge = @ifShowAge, ");
            strSQL.Append("IfShowGender = @ifShowGender, IfShowOccupation = @ifShowOccupation, ");
            strSQL.Append("IfShowCompany = @ifShowCompany, IfShowPhoneNumber = @ifShowPhone, IfShowFaxNumber = @ifShowFax, ");
            strSQL.Append("IfShowInterests = @ifShowInterests, IfShowHomePage = @ifShowHomePage ");
            strSQL.Append("where Id = @userOrOperatorId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region cmd.AddWithValue(...)
            cmd.Parameters.AddWithValue("@userOrOperatorId", userId);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@company", company);
            cmd.Parameters.AddWithValue("@occupation", occupation);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@fax", fax);
            cmd.Parameters.AddWithValue("@interests", interests);
            cmd.Parameters.AddWithValue("@homepage", homepage);
            cmd.Parameters.AddWithValue("@score", score);
            cmd.Parameters.AddWithValue("@reputation", reputation);
            cmd.Parameters.AddWithValue("@ifShowEmail", ifShowEmail);
            cmd.Parameters.AddWithValue("@ifShowUserName", ifShowUserName);
            cmd.Parameters.AddWithValue("@ifShowAge", ifShowAge);
            cmd.Parameters.AddWithValue("@ifShowGender", ifShowGender);
            cmd.Parameters.AddWithValue("@ifShowOccupation", ifShowOccupation);
            cmd.Parameters.AddWithValue("@ifShowCompany", ifShowCompany);
            cmd.Parameters.AddWithValue("@ifShowPhone", ifShowPhone);
            cmd.Parameters.AddWithValue("@ifShowFax", ifShowFax);
            cmd.Parameters.AddWithValue("@ifShowInterests", ifShowInterests);
            cmd.Parameters.AddWithValue("@ifShowHomePage", ifShowHomePage);
            #endregion

            cmd.ExecuteNonQuery();
        }

        public static void UpdateOperatorProfile(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int operatorId, string firstName, string lastName, int age,
            EnumGender gender, string company, string occupation, string phone, string fax, string interests,
            string homepage,string score,string reputation, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender,
            bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests,
            bool ifShowHomePage)
        {

            StringBuilder strSQL = new StringBuilder("Update t_User" + conn.SiteId.ToString() + " set ");
            strSQL.Append("FirstName = @firstName, LastName = @lastName, ");
            strSQL.Append("Age = @age, Gender = @gender, Company = @company, Occupation = @occupation, ");
            strSQL.Append("PhoneNumber = @phone, FaxNumber = @fax, Interests = @interests, Homepage = @homepage,ForumScore=@score,ForumReputation=@reputation, ");
            strSQL.Append("IfShowEmail = @ifShowEmail, IfShowUserName = @ifShowUserName, IfShowAge = @ifShowAge, ");
            strSQL.Append("IfShowGender = @ifShowGender, IfShowOccupation = @ifShowOccupation, ");
            strSQL.Append("IfShowCompany = @ifShowCompany, IfShowPhoneNumber = @ifShowPhone, IfShowFaxNumber = @ifShowFax, ");
            strSQL.Append("IfShowInterests = @ifShowInterests, IfShowHomePage = @ifShowHomePage ");
            strSQL.Append("where Id = @operatorId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region cmd.AddWithValue(...)
            cmd.Parameters.AddWithValue("@operatorId", operatorId);
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@company", company);
            cmd.Parameters.AddWithValue("@occupation", occupation);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@fax", fax);
            cmd.Parameters.AddWithValue("@interests", interests);
            cmd.Parameters.AddWithValue("@homepage", homepage);
            cmd.Parameters.AddWithValue("@score",score);
            cmd.Parameters.AddWithValue("@reputation",reputation);
            cmd.Parameters.AddWithValue("@ifShowEmail", ifShowEmail);
            cmd.Parameters.AddWithValue("@ifShowUserName", ifShowUserName);
            cmd.Parameters.AddWithValue("@ifShowAge", ifShowAge);
            cmd.Parameters.AddWithValue("@ifShowGender", ifShowGender);
            cmd.Parameters.AddWithValue("@ifShowOccupation", ifShowOccupation);
            cmd.Parameters.AddWithValue("@ifShowCompany", ifShowCompany);
            cmd.Parameters.AddWithValue("@ifShowPhone", ifShowPhone);
            cmd.Parameters.AddWithValue("@ifShowFax", ifShowFax);
            cmd.Parameters.AddWithValue("@ifShowInterests", ifShowInterests);
            cmd.Parameters.AddWithValue("@ifShowHomePage", ifShowHomePage);
            #endregion

            cmd.ExecuteNonQuery();
        }

        public static void UpdateUserOrOperatorSignature(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int userOrOperatorId, string signature)
        {

            StringBuilder strSQL = new StringBuilder("Update t_User" + conn.SiteId.ToString() + " set ");
            strSQL.Append("Signature = @signature where Id = @userOrOperatorId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@signature", signature);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateUserOrOperatorEmailVerificationGUIDTag(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int userId, string emailVerificationGuidTag)
        {

            DateTime forgetPasswordTagTime = DateTime.UtcNow;

            StringBuilder strSQL = new StringBuilder("Update t_User" + conn.SiteId.ToString() + " set ");
            strSQL.Append("ForgetPasswordGUIDTag = @emailVerificationGuidTag, ForgetPasswordTagTime = @forgetPasswordTagTime where Id = @userId;");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@emailVerificationGuidTag", emailVerificationGuidTag);
            cmd.Parameters.AddWithValue("@forgetPasswordTagTime", forgetPasswordTagTime);
            cmd.Parameters.AddWithValue("@userId", userId);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateUserOrOperatorAvatar(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int userOrOperatorId, bool ifCustomizeAvatar, string systemAvatar, byte[] customizeAvatar)
        {

            StringBuilder strSQL = new StringBuilder("Update t_User" + conn.SiteId.ToString() + " set ");
            strSQL.Append("IfCustomizeAvatar = @ifCustomizeAvatar, SystemAvatar = @systemAvatar, " +
                          "CustomizeAvatar = @customizeAvatar where Id = @userOrOperatorId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@ifCustomizeAvatar", ifCustomizeAvatar);
            cmd.Parameters.AddWithValue("@systemAvatar", systemAvatar);
            cmd.Parameters.AddWithValue("@customizeAvatar", customizeAvatar);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateUserOrOperatorPassword(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int userOrOperatorId, string password)
        {

            StringBuilder strSQL = new StringBuilder("Update t_User" + conn.SiteId.ToString() + " set ");
            strSQL.Append("Password = @password where Id = @userOrOperatorId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@password", password);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateUserOrOperatorLastLoginTimeToCurrentTime(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int userOrOperatorId)
        {
            DateTime dateTime = DateTime.UtcNow;

            StringBuilder strSQL = new StringBuilder("Update t_User" + conn.SiteId + " set ");
            strSQL.Append("LastLoginTime = @lastLoginTime where Id = @userOrOperatorId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@lastLoginTime", dateTime);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateUserOrOperatorLastLoginIp(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, long ip)
        {

            StringBuilder strSQL = new StringBuilder("Update t_User" + conn.SiteId + " set ");
            strSQL.Append("LastLoginIP = @lastLoginIp where Id = @userOrOperatorId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);
            cmd.Parameters.AddWithValue("@lastLoginIp", ip);

            cmd.ExecuteNonQuery();
        }

        public static void IncreaseUserOrOperatorNumberOfPostsByOne(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            int userOrOperatorId)
        {

            StringBuilder strSQL = new StringBuilder("Update t_User" + conn.SiteId.ToString() + " set");
            strSQL.Append(" Posts=Posts+1 where Id=@userOrOperatorId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);

            cmd.ExecuteNonQuery();
        }

        //9.22 new add
        public static void DecreaseAutorPostsNumberByOne(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        {

            StringBuilder strSQL = new StringBuilder("Update t_User" + conn.SiteId.ToString() + " set");
            strSQL.Append(" Posts=Posts-1 where Id=@userOrOperatorId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);

            cmd.ExecuteNonQuery();
        }

        public static void SetUserVerified(SqlConnectionWithSiteId conn, SqlTransaction transactiion, int userId)
        {

            StringBuilder strSQL = new StringBuilder("");
            strSQL.Append("update t_user" + conn.SiteId + " set IfVerified=1 where Id=@id");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transactiion);
            cmd.Parameters.AddWithValue("@id", userId);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateUserModerateStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userId, EnumUserModerateStatus moderateStatus)
        {

            StringBuilder strSQL = new StringBuilder("Update t_User" + conn.SiteId.ToString() + " set ");
            strSQL.Append("ModerateStatus = @ModerateStatus where Id = @userId;");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@ModerateStatus", moderateStatus);

            cmd.ExecuteNonQuery();
        }

        public static void SetUserEmailVerificationPassing(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userId)
        {

            StringBuilder strSQL = new StringBuilder("Update t_User" + conn.SiteId.ToString() + " set ");
            strSQL.Append(" EmailVerificationStatus = @EmailVerificationStatus where Id = @userId;");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@EmailVerificationStatus", Convert.ToInt16(2));
            cmd.Parameters.AddWithValue("@userId", userId);

            cmd.ExecuteNonQuery();
        }

        public static int AddUser(SqlConnectionWithSiteId conn, SqlTransaction transaction,
            string email, string password, string displayName, long ip,
            short moderateStatus, short emailVerification, string defalutAvatarFilePath)
        {
            DateTime dateTime = DateTime.UtcNow;

            bool ifVerify = false;
            if (moderateStatus != 1 && emailVerification != 1)
                ifVerify = true;
            else
                ifVerify = false;

            short userType = 2;

            StringBuilder strSQL = new StringBuilder("Insert into t_User" + conn.SiteId.ToString());
            strSQL.Append("(Email, Password, Name, JoinedTime, JoinedIP, IfVerified, ModerateStatus, EmailVerificationStatus, UserType,SystemAvatar)");
            strSQL.Append(" values(@email, @password, @displayName, @joinedTime, @joinedIP, ");
            strSQL.Append("@ifVerified, @moderateStatus, @emailVerification, @userType, @SystemAvatar);");
            strSQL.Append("select @@identity");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@joinedTime", dateTime);
            cmd.Parameters.AddWithValue("@joinedIP", ip);
            cmd.Parameters.AddWithValue("@ifVerified", ifVerify);
            cmd.Parameters.AddWithValue("@moderateStatus", moderateStatus);
            cmd.Parameters.AddWithValue("@emailVerification", emailVerification);
            cmd.Parameters.AddWithValue("@userType", userType);
            cmd.Parameters.AddWithValue("@SystemAvatar", defalutAvatarFilePath);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void DeleteUser(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userId)
        {

            StringBuilder strSQL = new StringBuilder("Update t_User" + conn.SiteId + " set IfDeleted=1,DeleteTime=@DeleteTime where Id=@id");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@id", userId);
            cmd.Parameters.AddWithValue("@DeleteTime", DateTime.UtcNow);

            cmd.ExecuteNonQuery();
        }

        public static int GetCountOfNotDeletedUsersByDisplayName(SqlConnectionWithSiteId conn, SqlTransaction transaction, string displayName)
        {
            ///
            ///Need two GetCountOfNotDeletedUsersByDisplayName.
            ///One for verify user name.
            ///One for search.
            ///

            StringBuilder strSQL = new StringBuilder("select count(Id) from t_User" + conn.SiteId.ToString() + " where Name=@name and IfDeleted=0 and(UserType=1 or UserType=2) ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@name", displayName);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static int GetCountOfNotDeletedUsersByEmail(SqlConnectionWithSiteId conn, SqlTransaction transaction, string email)
        {

            StringBuilder strSQL = new StringBuilder("select count(Id) from t_User" + conn.SiteId.ToString() + " where Email=@Email and IfDeleted=0 and(UserType=1 or UserType=2)");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@Email", email);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static DataTable GetUserTypeById(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        {

            StringBuilder strSQL = new StringBuilder("select userType ,IfDeleted from t_User" + conn.SiteId + " where Id=@Id ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@Id", userOrOperatorId);

            DataTable table = new DataTable();

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }
        public static DataTable GetNotDeletedUserTypeByEmail(SqlConnectionWithSiteId conn, SqlTransaction transaction, string email)
        {

            StringBuilder strSQL = new StringBuilder("select Id,userType from t_User" + conn.SiteId + " where Email=@Email And IfDeleted=0 And (UserType=1 or UserType=2) ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@Email", email);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        /*---------------------2.0-----------------------*/
        public static int GetCountOfNotDeletedUsersOrOperatorsById(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        {
            return 0;
        }


        #region administrator
        public static DataTable GetAllAdministrators(string orderfield, string order, SqlConnectionWithSiteId conn)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select * from t_User{0} where IfForumAdmin = 1 and IfDeleted = 0 and UserType<>@typeOfContact and (IfVerified=1 or UserType=@typeOfUser) order by {1} {2}", conn.SiteId, orderfield, order));

            DataTable table = new DataTable();

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn);
            cmd.Parameters.AddWithValue("@typeOfContact", Convert.ToInt16(EnumUserType.Contact));
            cmd.Parameters.AddWithValue("@typeOfUser", Convert.ToInt16(EnumUserType.User));

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetAllAdministrators(SqlConnectionWithSiteId conn)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select * from t_User{0} where IfForumAdmin = 1 and UserType<>@typeOfContact and (IfVerified=1 or UserType=@typeOfOperator) ",conn.SiteId));

            DataTable table = new DataTable();

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn);
            cmd.Parameters.AddWithValue("@typeOfContact", Convert.ToInt16(EnumUserType.Contact));
            cmd.Parameters.AddWithValue("@typeOfUser", Convert.ToInt16(EnumUserType.User));

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetAllNotAdministratorsByKeyWordAndUserType
            (string keyword, EnumUserType userType, string orderField, string order, SqlConnectionWithSiteId conn)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select * from t_User{0} ", conn.SiteId, orderField, order));
            strSQL.Append("where ");
            strSQL.Append("(name like '%' + @name + '%' ");
            strSQL.Append("Or email like '%' + @email + '%' ) ");
            strSQL.Append("and usertype = @usertype ");
            strSQL.Append("and ifadmin = 0 ");
            strSQL.Append(string.Format("order by {0} {1}", orderField, order));

            DataTable table = new DataTable();

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn);

            cmd.Parameters.AddWithValue("@email", keyword);
            cmd.Parameters.AddWithValue("@name", keyword);
            cmd.Parameters.AddWithValue("@usertype", userType);

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static int GetCountOfNotDeletedUsersOrOperatorsWhichisAdministrator(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select count(id) from t_user{0} ", conn.SiteId));
            strSQL.Append("where IfForumAdmin = 1 and IfDeleted = 0");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void AddAdministrator(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        { 
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("update t_user{0} set ", conn.SiteId));
            strSQL.Append("IfForumAdmin = 1 ");
            strSQL.Append("where id = @id");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@id", userOrOperatorId);

            cmd.ExecuteNonQuery();
        }

        public static void DeleteAdministrator(int administratorId, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("update t_user{0} set ", conn.SiteId));
            strSQL.Append("IfForumAdmin = 0 ");
            strSQL.Append("where id = @id");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@id", administratorId);

            cmd.ExecuteNonQuery();
        }

        public static DataTable GetAdministratorById(SqlConnectionWithSiteId conn, SqlTransaction transactioin, int userOrOperatorId)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select * from t_user{0} ", conn.SiteId));
            strSQL.Append("where id = @id and IfForumAdmin = 1");

            DataTable table = new DataTable();

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transactioin);

            cmd.Parameters.AddWithValue("@id", userOrOperatorId);

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetNotAdministratorUsersAndNotDelete(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select * from t_user{0} ", conn.SiteId));
            strSQL.Append("where IfDeleted = 0 ");
            strSQL.Append("and ifadmin = 0 ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            DataTable table = new DataTable();

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }

        public static DataTable GetNotAdministratorUsersAndNotDeleteByKeyWord(string keyword, SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select * from t_user{0} ", conn.SiteId));
            strSQL.Append("where IfDeleted = 0 ");
            strSQL.Append("and ifadmin = 0 ");
            strSQL.Append("and ");
            strSQL.Append("(name like '%' + @name + '%' ");
            strSQL.Append("Or email like '%' + @email + '%' ) ");
        

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@email", keyword);
            cmd.Parameters.AddWithValue("@name", keyword);

            DataTable table = new DataTable();

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }


        public static DataTable GetAllUserNotDelete(SqlConnectionWithSiteId conn, SqlTransaction transaction)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(string.Format("select * from t_user{0} ", conn.SiteId));
            strSQL.Append("where ifdeleted = 0 ");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            DataTable table = new DataTable();

            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);

            return table;
        }
        #endregion administrator

        public static void SetUserOrOperatorActiveStatus(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, bool ifActive)
        {

            StringBuilder strSQL = new StringBuilder("update t_User" + conn.SiteId + " set IfActive=@IfActive where Id=@Id");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("IfActive", ifActive);
            cmd.Parameters.Add("@Id", userOrOperatorId);

            cmd.ExecuteNonQuery();
        }


        public static void UpdateUser(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userId,
            string email, string displayName, string firstName, string lastName, int age,
            EnumGender gender, string company, string occupation, string phone, string fax, string interests,
            string homepage, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender,
            bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests,
            bool ifShowHomePage, int score, int reputation)
        {
            StringBuilder strSQL = new StringBuilder("");
            #region Build SQl
            strSQL.Append(string.Format("Update t_User{0} Set ", conn.SiteId));
            strSQL.Append(" Email=@email,");
            strSQL.Append(" Name=@displayName,");
            strSQL.Append(" FirstName=@firstName,");
            strSQL.Append(" LastName=@lastName,");
            strSQL.Append(" Age=@age,");
            strSQL.Append(" Gender=@gender,");
            strSQL.Append(" Company=@company,");
            strSQL.Append(" Occupation=@occupation,");
            strSQL.Append(" PhoneNumber=@phone,");
            strSQL.Append(" FaxNumber=@fax,");
            strSQL.Append(" Interests=@interests,");
            strSQL.Append(" Homepage=@homepage,");
            strSQL.Append(" IfShowEmail=@ifShowEmail,");
            strSQL.Append(" IfShowUserName=@ifShowUserName,");
            strSQL.Append(" IfShowAge=@ifShowAge,");
            strSQL.Append(" IfShowGender=@ifShowGender,");
            strSQL.Append(" IfShowOccupation=@ifShowOccupation,");
            strSQL.Append(" IfShowCompany=@ifShowCompany,");
            strSQL.Append(" IfShowPhoneNumber=@ifShowPhone,");
            strSQL.Append(" IfShowFaxNumber=@ifShowFax,");
            strSQL.Append(" IfShowInterests=@ifShowInterests,");
            strSQL.Append(" IfShowHomePage=@ifShowHomePage,");
            strSQL.Append(" ForumScore=@score,");
            strSQL.Append(" ForumReputation=@reputation");
            strSQL.Append(" where Id=@userId");
            #endregion Build SQl

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Parameters
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@company", company);
            cmd.Parameters.AddWithValue("@occupation", occupation);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@fax", fax);
            cmd.Parameters.AddWithValue("@interests", interests);
            cmd.Parameters.AddWithValue("@homepage", homepage);
            cmd.Parameters.AddWithValue("@ifShowEmail", ifShowEmail);
            cmd.Parameters.AddWithValue("@ifShowUserName", ifShowUserName);
            cmd.Parameters.AddWithValue("@ifShowAge", ifShowAge);
            cmd.Parameters.AddWithValue("@ifShowGender", ifShowGender);
            cmd.Parameters.AddWithValue("@ifShowOccupation", ifShowOccupation);
            cmd.Parameters.AddWithValue("@ifShowCompany", ifShowCompany);
            cmd.Parameters.AddWithValue("@ifShowPhone", ifShowPhone);
            cmd.Parameters.AddWithValue("@ifShowFax", ifShowFax);
            cmd.Parameters.AddWithValue("@ifShowInterests", ifShowInterests);
            cmd.Parameters.AddWithValue("@ifShowHomePage", ifShowHomePage);
            cmd.Parameters.AddWithValue("@score", score);
            cmd.Parameters.AddWithValue("@reputation", reputation);
            cmd.Parameters.AddWithValue("@userId", userId);
            #endregion Add Parameters

            cmd.ExecuteNonQuery();
        }


        public static int GetCountOfNotDeletedUserOrOperatorById(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId)
        {

            StringBuilder strSQL = new StringBuilder("select count(Id) from t_User" + conn.SiteId + " where IfDeleted=0 and Id=@userOrOperatorId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);

            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);

            return (int)cmd.ExecuteScalar();
        }


        public static int AddUser(SqlConnectionWithSiteId conn, SqlTransaction transaction, 
            string email, string displayName, string password, string firstName, string lastName, int age,
            EnumGender gender, string company, string occupation, string phone, string fax, string interests,
            string homepage, bool ifShowEmail, bool ifShowUserName, bool ifShowAge, bool ifShowGender,
            bool ifShowOccupation, bool ifShowCompany, bool ifShowPhone, bool ifShowFax, bool ifShowInterests,
            bool ifShowHomePage, string defalutAvatarFilePath)
        {
            DateTime joinedTime = DateTime.UtcNow;
            StringBuilder strSQL = new StringBuilder("");
            #region Build SQL
            strSQL.Append("insert t_User" + conn.SiteId + "(UserType,IfVerified,ModerateStatus,EmailVerificationStatus,");
            strSQL.Append("Email,Name,Password,FirstName,LastName,Age,Gender,Company,Occupation,PhoneNumber,FaxNumber,Interests,HomePage,JoinedTime,");
            strSQL.Append("IfShowEmail,IfShowUserName,IfShowAge,IfShowGender,IfShowOccupation,IfShowCompany,IfShowPhoneNumber,IfShowFaxNumber,IfShowInterests,IfShowHomePage,");
            strSQL.Append("IfCustomizeAvatar,SystemAvatar)");
            strSQL.Append("values(@userType,@ifVerified,@moderateStatus,@emailVerificationStatus,");
            strSQL.Append("@email,@displayName,@password,@firstName,@lastName,@age,@gender,@company,@occupation,@phone,@fax,@interests,@homepage,@joinedTime,");
            strSQL.Append("@ifShowEmail,@ifShowUserName,@ifShowAge,@ifShowGender,@ifShowOccupation,@ifShowCompany,@ifShowPhone,@ifShowFax,@ifShowInterests,@ifShowHomePage,");
            strSQL.Append("@IfCustomizeAvatar,@SystemAvatar);");
            strSQL.Append("select @@identity");
            #endregion Build SQL

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Parameters
            cmd.Parameters.AddWithValue("@userType", EnumUserType.User);
            cmd.Parameters.AddWithValue("@ifVerified", true);
            cmd.Parameters.AddWithValue("@moderateStatus", EnumUserModerateStatus.enumDoNotNeed);
            cmd.Parameters.AddWithValue("@emailVerificationStatus", EnumUserEmailVerificationStatus.enumDoNotNeed);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@company", company);
            cmd.Parameters.AddWithValue("@occupation", occupation);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@fax", fax);
            cmd.Parameters.AddWithValue("@interests", interests);
            cmd.Parameters.AddWithValue("@homepage", homepage);
            cmd.Parameters.AddWithValue("@joinedTime", joinedTime);
            cmd.Parameters.AddWithValue("@ifShowEmail", ifShowEmail);
            cmd.Parameters.AddWithValue("@ifShowUserName", ifShowUserName);
            cmd.Parameters.AddWithValue("@ifShowAge", ifShowAge);
            cmd.Parameters.AddWithValue("@ifShowGender", ifShowGender);
            cmd.Parameters.AddWithValue("@ifShowOccupation", ifShowOccupation);
            cmd.Parameters.AddWithValue("@ifShowCompany", ifShowCompany);
            cmd.Parameters.AddWithValue("@ifShowPhone", ifShowPhone);
            cmd.Parameters.AddWithValue("@ifShowFax", ifShowFax);
            cmd.Parameters.AddWithValue("@ifShowInterests", ifShowInterests);
            cmd.Parameters.AddWithValue("@ifShowHomePage", ifShowHomePage);
            cmd.Parameters.AddWithValue("@IfCustomizeAvatar", false);
            cmd.Parameters.AddWithValue("@SystemAvatar", defalutAvatarFilePath);
            #endregion Add Parameters
            return Convert.ToInt32(cmd.ExecuteScalar());
        }


        public static DataTable GetRecieversOfOutMessage(SqlConnectionWithSiteId conn, SqlTransaction transaction, int outMessageId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select b.* from");
            strSQL.Append(" t_Forum_UserOfOutMessage" + conn.SiteId + " a, t_User" + conn.SiteId + "  b where a.OutMessageId = @OutMessageId and a.UserId = b.Id; ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@OutMessageId", outMessageId);            
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(),LoadOption.Upsert);
            return table;
        }

        #region Get Not Deleted And Not Banned UserOrOperators Which Is Not Administrator
        public static DataTable GetNotDeletedAndNotBannedUsersOrOperatorsWhichisNotAdministratorByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, string emailOrDisplayNameKeyword, string userTypeCondition, string orderCondition)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;
            emailOrDisplayNameKeyword = CommonFunctions.SqlReplace(emailOrDisplayNameKeyword);

            StringBuilder strSQL = new StringBuilder("");
            #region Build SQL
            strSQL.Append("select UserOrOperatorId into #temp FROM t_Forum_Ban ");
            strSQL.Append(" where Type=" + Convert.ToInt16(EnumBanType.User) + " and SiteId=" + conn.SiteId + " and StartDate <= @now AND EndDate >=@now");
            strSQL.Append(" and IfDeleted='false' ");

            strSQL.Append(" select MIN(UserOrOperatorId) as [Id] into #BanUserOrOperator from #temp group by UserOrOperatorId ");
            strSQL.Append(" drop table #temp ");

            strSQL.Append(" select ROW_NUMBER() over(order by " + orderCondition + ") as [row], * into #result from t_User" + conn.SiteId);
            strSQL.Append(" where IfDeleted=0 and IfForumAdmin=0 and UserType<>@typeOfContact and (IfVerified=1 or UserType=@typeOfOperator) ");//IfVerified = (case UserType when @typeOfUser then 1 else 0 end) ");
            strSQL.Append(" and (Email like '%'+@emailOrDisplayNameKeyword+'%' escape '/' or Name like '%'+@emailOrDisplayNameKeyword+'%' escape '/') " + userTypeCondition);
            strSQL.Append(" and Id not in (select * from #BanUserOrOperator) ");
            strSQL.Append(" drop table #BanUserOrOperator ");

            strSQL.Append(" select * from  #result where row between @startRowNum and @endRowNum");
            #endregion Build SQL

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Parameters
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@emailOrDisplayNameKeyword", emailOrDisplayNameKeyword);
            cmd.Parameters.AddWithValue("@typeOfContact", Convert.ToInt16(EnumUserType.Contact));
            cmd.Parameters.AddWithValue("@typeOfUser", Convert.ToInt16(EnumUserType.User));
            cmd.Parameters.AddWithValue("@startRowNum", startRowNum);
            cmd.Parameters.AddWithValue("@typeOfOperator", Convert.ToInt16(EnumUserType.Operator));
            cmd.Parameters.AddWithValue("@endRowNum", endRowNum);
            #endregion Add Parameters

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table; 
        }

        public static int GetCountOfNotDeletedAndNotBannedUsersOrOperatorsWhichisNotAdministratorByQuery(SqlConnectionWithSiteId conn, SqlTransaction transaction, string emailOrDisplayNameKeyword, string userTypeCondition)
        {
            emailOrDisplayNameKeyword = CommonFunctions.SqlReplace(emailOrDisplayNameKeyword);

            StringBuilder strSQL = new StringBuilder("");
            #region Build SQL
            strSQL.Append("select UserOrOperatorId into #temp FROM t_Forum_Ban ");
            strSQL.Append(" where Type=" + Convert.ToInt16(EnumBanType.User) + " and SiteId=" + conn.SiteId + " and StartDate <= @now AND EndDate >=@now");
            strSQL.Append(" and IfDeleted='false' ");

            strSQL.Append(" select MIN(UserOrOperatorId) as [Id] into #BanUserOrOperator from #temp group by UserOrOperatorId ");
            strSQL.Append(" drop table #temp ");

            strSQL.Append(" select ROW_NUMBER() over(order by joinedTime desc) as [row], * into #result  from t_User" + conn.SiteId);
            strSQL.Append(" where IfDeleted=0 and IfForumAdmin=0 and UserType<>@typeOfContact and (IfVerified=1 or UserType=@typeOfOperator) ");//IfVerified = (case UserType when @typeOfUser then 1 else 0 end)");
            strSQL.Append(" and (Email like '%'+@emailOrDisplayNameKeyword+'%' escape '/' or Name like '%'+@emailOrDisplayNameKeyword+'%' escape '/') " + userTypeCondition);
            strSQL.Append(" and Id not in (select * from #BanUserOrOperator) ");
            strSQL.Append(" drop table #BanUserOrOperator ");

            strSQL.Append(" select count(Id) from  #result ");
            strSQL.Append(" drop table #result ");
            #endregion Build SQL

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Parameters
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@typeOfContact", Convert.ToInt16(EnumUserType.Contact));
            cmd.Parameters.AddWithValue("@typeOfUser", Convert.ToInt16(EnumUserType.User));
            cmd.Parameters.AddWithValue("@typeOfOperator", Convert.ToInt16(EnumUserType.Operator));
            cmd.Parameters.AddWithValue("@emailOrDisplayNameKeyword", emailOrDisplayNameKeyword);
            #endregion Add Parameters

            return Convert.ToInt32(cmd.ExecuteScalar());

        }
        #endregion 

        #region Get Not Deleted And Not Banned UserOrOperators which Is Not In User Group
        public static DataTable GetNotDeletedAndNotBannedUsersOrOperatorsWhichisNotInUserGroupByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userGroupId, int pageIndex, int pageSize, string emailOrDisplayNameKeyword, string userTypeCondition, string orderCondition)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;
            emailOrDisplayNameKeyword = CommonFunctions.SqlReplace(emailOrDisplayNameKeyword);

            StringBuilder strSQL = new StringBuilder("");
            #region Build SQL
            strSQL.Append(" select UserOrOperatorId into #TmpBanUserOrOperatorId from t_Forum_Ban ");
            strSQL.Append(" where Type=" + Convert.ToInt16(EnumBanType.User) + " and SiteId=" + conn.SiteId + " and StartDate <= @now AND EndDate >=@now ");
            strSQL.Append(" and IfDeleted='false' ");

            strSQL.Append(" select MIN(UserOrOperatorId) as [Id] into #BanUserOrOperatorId from #TmpBanUserOrOperatorId group by UserOrOperatorId ");
            strSQL.Append(" drop table #TmpBanUserOrOperatorId ");

            strSQL.Append(" select UserId as [Id] into #MemberId from t_Forum_MemberOfUserGroup" + conn.SiteId + " where GroupId=@userGroupId ");

            strSQL.Append(" select ROW_NUMBER() over(order by " + orderCondition + ") as [row], * into #result  from t_User" + conn.SiteId);
            strSQL.Append(" where IfDeleted=0  and (IfVerified=1 or UserType=@typeOfOperator) ");//IfVerified = (case UserType when @typeOfUser then 1 else 0 end) ");
            strSQL.Append(" and (Email like '%'+@emailOrDisplayNameKeyword+'%' escape '/' or Name like '%'+@emailOrDisplayNameKeyword+'%' escape '/') " + userTypeCondition);
            strSQL.Append(" and Id not in (select * from #BanUserOrOperatorId) ");
            strSQL.Append(" and Id not in (select * from #MemberId) ");

            strSQL.Append(" drop table #BanUserOrOperatorId ");
            strSQL.Append(" drop table #MemberId ");

            strSQL.Append(" select * from  #result where row between @startRowNum and @endRowNum");
            #endregion Build SQL

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Parameters
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@userGroupId", userGroupId);
            cmd.Parameters.AddWithValue("@typeOfContact", Convert.ToInt16(EnumUserType.Contact));
            cmd.Parameters.AddWithValue("@typeOfUser", Convert.ToInt16(EnumUserType.User));
            cmd.Parameters.AddWithValue("@typeOfOperator", Convert.ToInt16(EnumUserType.Operator));
            cmd.Parameters.AddWithValue("@emailOrDisplayNameKeyword", emailOrDisplayNameKeyword);
            cmd.Parameters.AddWithValue("@startRowNum", startRowNum);
            cmd.Parameters.AddWithValue("@endRowNum", endRowNum);
            #endregion Add Parameters

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table; 
        }

        public static int GetCountOfNotDeletedAndNotBannedUsersOrOperatorsWhichisNotInUserGroupByQuery(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userGroupId, string emailOrDisplayNameKeyword, string userTypeCondition)
        {
            emailOrDisplayNameKeyword = CommonFunctions.SqlReplace(emailOrDisplayNameKeyword);

            StringBuilder strSQL = new StringBuilder("");
            #region Build SQL
            strSQL.Append(" select UserOrOperatorId into #TmpBanUserOrOperatorId from t_Forum_Ban ");
            strSQL.Append(" where Type=" + Convert.ToInt16(EnumBanType.User) + " and SiteId=" + conn.SiteId + " and StartDate <= @now AND EndDate >=@now ");
            strSQL.Append(" and IfDeleted='false' ");

            strSQL.Append(" select MIN(UserOrOperatorId) as [Id] into #BanUserOrOperatorId from #TmpBanUserOrOperatorId group by UserOrOperatorId ");
            strSQL.Append(" drop table #TmpBanUserOrOperatorId ");

            strSQL.Append(" select UserId as [Id] into #MemberId from t_Forum_MemberOfUserGroup" + conn.SiteId + " where GroupId=@userGroupId ");

            strSQL.Append(" select ROW_NUMBER() over(order by joinedTime desc) as [row], * into #result  from t_User" + conn.SiteId);
            strSQL.Append(" where IfDeleted=0 and UserType<>@typeOfContact and (IfVerified=1 or UserType=@typeOfOperator) ");//IfVerified = (case UserType when @typeOfUser then 1 else 0 end) ");
            strSQL.Append(" and (Email like '%'+@emailOrDisplayNameKeyword+'%' escape '/' or Name like '%'+@emailOrDisplayNameKeyword+'%' escape '/') " + userTypeCondition);
            strSQL.Append(" and Id not in (select * from #BanUserOrOperatorId) ");
            strSQL.Append(" and Id not in (select * from #MemberId) ");

            strSQL.Append(" drop table #BanUserOrOperatorId ");
            strSQL.Append(" drop table #MemberId ");

            strSQL.Append(" select count(Id) from  #result ");
            #endregion Build SQL

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Parameters
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@userGroupId", userGroupId);
            cmd.Parameters.AddWithValue("@typeOfContact", Convert.ToInt16(EnumUserType.Contact));
            cmd.Parameters.AddWithValue("@typeOfUser", Convert.ToInt16(EnumUserType.User));
            cmd.Parameters.AddWithValue("@typeOfOperator", Convert.ToInt16(EnumUserType.Operator));
            cmd.Parameters.AddWithValue("@emailOrDisplayNameKeyword", emailOrDisplayNameKeyword);
            #endregion Add Parameters

            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        #endregion 

        #region Get Not Deleted And Not Banned UserOrOperators By Email or Display Name
        public static DataTable GetNotDeletedAndNotBannedUserOrOperatorsByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, string emailOrDisplayNameKeyword, string userTypeCondition, string orderCondition)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;
            emailOrDisplayNameKeyword = CommonFunctions.SqlReplace(emailOrDisplayNameKeyword);
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(" select UserOrOperatorId into #TmpBanUserOrOperatorId from t_Forum_Ban ");
            strSQL.Append(" where Type=" + Convert.ToInt16(EnumBanType.User) + " and SiteId=" + conn.SiteId );
            strSQL.Append(" and IfDeleted='false' ");

            strSQL.Append(" select UserOrOperatorId as [Id] into #BanUserOrOperatorId from #TmpBanUserOrOperatorId group by UserOrOperatorId ");
            strSQL.Append(" drop table #TmpBanUserOrOperatorId ");

            strSQL.Append(" select ROW_NUMBER() over(order by " + orderCondition + ") as [row], * into #result  from t_User" + conn.SiteId);
            strSQL.Append(" where IfDeleted=0 and UserType<>@typeOfContact and (IfVerified=1 or UserType=@typeOfOperator) ");//IfVerified = (case UserType when @typeOfUser then 1 else 0 end) ");
            strSQL.Append(" and (Name like '%'+@emailOrDisplayNameKeyword+'%' escape '/' or Email like '%'+@emailOrDisplayNameKeyword+'%' escape '/') " + userTypeCondition);
            strSQL.Append(" and (Id not in (select * from #BanUserOrOperatorId) )");

            strSQL.Append(" drop table #BanUserOrOperatorId ");

            strSQL.Append(" select * from  #result where row between @startRowNum and @endRowNum");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Parameters
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@typeOfContact", Convert.ToInt16(EnumUserType.Contact));
            cmd.Parameters.AddWithValue("@typeOfUser", Convert.ToInt16(EnumUserType.User));
            cmd.Parameters.AddWithValue("@typeOfOperator", Convert.ToInt16(EnumUserType.Operator));
            cmd.Parameters.AddWithValue("@emailOrDisplayNameKeyword", emailOrDisplayNameKeyword);
            cmd.Parameters.AddWithValue("@startRowNum", startRowNum);
            cmd.Parameters.AddWithValue("@endRowNum", endRowNum);
            #endregion Add Parameters

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table; 
        }
        
        public static int GetCountOfNotDeletedAndNotBannedUserOrOperatorsByQuery(
            SqlConnectionWithSiteId conn, SqlTransaction transaction, 
            string emailOrDisplayNameKeyword, string userTypeCondition)
        {
            emailOrDisplayNameKeyword = CommonFunctions.SqlReplace(emailOrDisplayNameKeyword);
            StringBuilder strSQL = new StringBuilder();
            #region Build SQL
            strSQL.Append(" select UserOrOperatorId into #TmpBanUserOrOperatorId from t_Forum_Ban ");
            strSQL.Append(" where Type=" + Convert.ToInt16(EnumBanType.User) + " and SiteId=" + conn.SiteId );
            strSQL.Append(" and IfDeleted='false' ");

            strSQL.Append(" select UserOrOperatorId as [Id] into #BanUserOrOperatorId from #TmpBanUserOrOperatorId group by UserOrOperatorId ");
            strSQL.Append(" drop table #TmpBanUserOrOperatorId ");

            strSQL.Append(" select ROW_NUMBER() over(order by joinedTime desc) as [row], * into #result  from t_User" + conn.SiteId);
            strSQL.Append(" where IfDeleted=0 and UserType<>@typeOfContact and (IfVerified=1 or UserType=@typeOfOperator) ");//IfVerified = (case UserType when @typeOfUser then 1 else 0 end) ");
            strSQL.Append(" and (Name like '%'+@emailOrDisplayNameKeyword+'%' escape '/' or Email like '%'+@emailOrDisplayNameKeyword+'%' escape '/') " + userTypeCondition);
            strSQL.Append(" and (Id not in (select * from #BanUserOrOperatorId) )");

            strSQL.Append(" drop table #BanUserOrOperatorId ");

            strSQL.Append(" select COUNT(Id) from  #result ");
            #endregion
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Parameters
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@typeOfContact", Convert.ToInt16(EnumUserType.Contact));
            cmd.Parameters.AddWithValue("@typeOfUser", Convert.ToInt16(EnumUserType.User));
            cmd.Parameters.AddWithValue("@typeOfOperator", Convert.ToInt16(EnumUserType.Operator));
            cmd.Parameters.AddWithValue("@emailOrDisplayNameKeyword", emailOrDisplayNameKeyword);
            #endregion Add Parameters

            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        #endregion 

        #region Get Not Deleted And Not Banned UserOrOperators By Display Name
        public static DataTable GetNotDeletedAndNotBannedUserOrOperatorsByQueryNameAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, string displayNameKeyword, string userTypeCondition, string orderCondition)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;
            displayNameKeyword = CommonFunctions.SqlReplace(displayNameKeyword);
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append(" select UserOrOperatorId into #TmpBanUserOrOperatorId from t_Forum_Ban ");
            strSQL.Append(" where Type=" + Convert.ToInt16(EnumBanType.User) + " and SiteId=" + conn.SiteId + " and StartDate <= " + DateTime.UtcNow.ToShortDateString() + " AND EndDate >=" + DateTime.UtcNow.ToShortDateString());
            strSQL.Append(" and IfDeleted='false' ");

            strSQL.Append(" select MIN(UserOrOperatorId) as [Id] into #BanUserOrOperatorId from #TmpBanUserOrOperatorId group by UserOrOperatorId ");
            strSQL.Append(" drop table #TmpBanUserOrOperatorId ");

            strSQL.Append(" select ROW_NUMBER() over(order by " + orderCondition + ") as [row], * into #result  from t_User" + conn.SiteId);
            strSQL.Append(" where IfDeleted=0 and (IfVerified=1 or UserType=@typeOfOperator) ");//IfVerified = (case UserType when @typeOfUser then 1 else 0 end) ");
            //strSQL.Append(" where IfDeleted=0 and UserType<>@typeOfContact and IfVerified = (case UserType when @typeOfUser then 1 else 0 end) ");
            strSQL.Append(" and (Name like '%'+@emailOrDisplayNameKeyword+'%' escape '/') " + userTypeCondition);
            strSQL.Append(" and (Id not in (select * from #BanUserOrOperatorId) )");

            strSQL.Append(" drop table #BanUserOrOperatorId ");

            strSQL.Append(" select * from  #result where row between @startRowNum and @endRowNum");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Parameters
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@typeOfContact", Convert.ToInt16(EnumUserType.Contact));
            cmd.Parameters.AddWithValue("@typeOfUser", Convert.ToInt16(EnumUserType.User));
            cmd.Parameters.AddWithValue("@typeOfOperator", Convert.ToInt16(EnumUserType.Operator));
            cmd.Parameters.AddWithValue("@emailOrDisplayNameKeyword", displayNameKeyword);
            cmd.Parameters.AddWithValue("@startRowNum", startRowNum);
            cmd.Parameters.AddWithValue("@endRowNum", endRowNum);
            #endregion Add Parameters

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }

        public static int GetCountOfNotDeletedAndNotBannedUserOrOperatorsByQueryName(
            SqlConnectionWithSiteId conn, SqlTransaction transaction,
            string displayNameKeyword, string userTypeCondition)
        {
            displayNameKeyword = CommonFunctions.SqlReplace(displayNameKeyword);
            StringBuilder strSQL = new StringBuilder();
            #region Build SQL
            strSQL.Append(" select UserOrOperatorId into #TmpBanUserOrOperatorId from t_Forum_Ban ");
            strSQL.Append(" where Type=" + Convert.ToInt16(EnumBanType.User) + " and SiteId=" + conn.SiteId + " and StartDate <= " + DateTime.UtcNow.ToShortDateString() + " AND EndDate >=" + DateTime.UtcNow.ToShortDateString());
            strSQL.Append(" and IfDeleted='false' ");

            strSQL.Append(" select MIN(UserOrOperatorId) as [Id] into #BanUserOrOperatorId from #TmpBanUserOrOperatorId group by UserOrOperatorId ");
            strSQL.Append(" drop table #TmpBanUserOrOperatorId ");

            strSQL.Append(" select ROW_NUMBER() over(order by joinedTime desc) as [row], * into #result  from t_User" + conn.SiteId);
            strSQL.Append(" where IfDeleted=0 and (IfVerified=1 or UserType=@typeOfOperator) ");//IfVerified = (case UserType when @typeOfUser then 1 else 0 end) ");
            //strSQL.Append(" where IfDeleted=0 and UserType<>@typeOfContact and IfVerified = (case UserType when @typeOfUser then 1 else 0 end) ");
            
            strSQL.Append(" and (Name like '%'+@emailOrDisplayNameKeyword+'%' escape '/' ) " + userTypeCondition);
            strSQL.Append(" and (Id not in (select * from #BanUserOrOperatorId) )");

            strSQL.Append(" drop table #BanUserOrOperatorId ");

            strSQL.Append(" select COUNT(Id) from  #result ");
            #endregion
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Parameters
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@typeOfContact", Convert.ToInt16(EnumUserType.Contact));
            cmd.Parameters.AddWithValue("@typeOfUser", Convert.ToInt16(EnumUserType.User));
            cmd.Parameters.AddWithValue("@typeOfOperator", Convert.ToInt16(EnumUserType.Operator));
            cmd.Parameters.AddWithValue("@emailOrDisplayNameKeyword", displayNameKeyword);
            #endregion Add Parameters

            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        #endregion 

        #region Get All UserOrOperators (operators & register users)
        public static DataTable GetUserOrOperatorsByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, int pageIndex, int pageSize, string emailOrDisplayNameKeyword, string userTypeCondition,string orderCondition)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;
            emailOrDisplayNameKeyword = CommonFunctions.SqlReplace(emailOrDisplayNameKeyword);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" select ROW_NUMBER() over(order by "+orderCondition+",IfAdmin desc ) as [row], * into #result  from t_User" + conn.SiteId);
            strSQL.Append(" where UserType<>@typeOfContact and ( IfVerified ='true' or UserType=@typeOfOperator ) ");
            strSQL.Append(" and (Email like '%'+@emailOrDisplayNameKeyword+'%' escape '/' or Name like '%'+@emailOrDisplayNameKeyword+'%' escape '/') " + userTypeCondition);

            strSQL.Append(" select * from #result where row between @startRowNum and @endRowNum ");
            strSQL.Append(" drop table #result ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Parameters
            cmd.Parameters.AddWithValue("@typeOfContact", Convert.ToInt16(EnumUserType.Contact));
            cmd.Parameters.AddWithValue("@typeOfOperator", Convert.ToInt16(EnumUserType.Operator));
            cmd.Parameters.AddWithValue("@emailOrDisplayNameKeyword", emailOrDisplayNameKeyword);
            cmd.Parameters.AddWithValue("@startRowNum", startRowNum);
            cmd.Parameters.AddWithValue("@endRowNum", endRowNum);
            #endregion Add parameters
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;            
        }
        public static int GetCountOfUserOrOperatorsByQuery(SqlConnectionWithSiteId conn, SqlTransaction transaction, string emailOrDisplayNameKeyword, string userTypeCondition)
        {
            emailOrDisplayNameKeyword = CommonFunctions.SqlReplace(emailOrDisplayNameKeyword);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" select  COUNT(*)  from t_User" + conn.SiteId);
            strSQL.Append(" where UserType<>@typeOfContact and ( IfVerified ='true' or UserType=@typeOfOperator ) ");
            strSQL.Append(" and (Email like '%'+@emailOrDisplayNameKeyword+'%' escape '/' or Name like '%'+@emailOrDisplayNameKeyword+'%' escape '/') " + userTypeCondition);
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Parameters
            cmd.Parameters.AddWithValue("@typeOfContact", Convert.ToInt16(EnumUserType.Contact));
            cmd.Parameters.AddWithValue("@typeOfOperator", Convert.ToInt16(EnumUserType.Operator));
            cmd.Parameters.AddWithValue("@emailOrDisplayNameKeyword", emailOrDisplayNameKeyword);
            #endregion Add parameters
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        #endregion 

        #region Get Register Users which need Email Verify 
        public static DataTable GetUsersWhichNeedEmailVerifyByQueryAndPaging(SqlConnectionWithSiteId conn, SqlTransaction transaction, string emailOrDisplayNameKeyword, string orderCondition, int pageIndex, int pageSize)
        {
            int startRowNum = (pageIndex - 1) * pageSize + 1;
            int endRowNum = pageIndex * pageSize;
            emailOrDisplayNameKeyword = CommonFunctions.SqlReplace(emailOrDisplayNameKeyword);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" select ROW_NUMBER() over(order by " + orderCondition + " ) as [row],* into #result from t_User" + conn.SiteId);
            strSQL.Append(" where UserType = @typeOfUser and IfVerified='false' and EmailVerificationStatus=@needVerification and IfDeleted='false' and ModerateStatus <>@refused");
            strSQL.Append(" and (Email like '%'+@emailOrDisplayNameKeyword+'%' escape '/' or Name like '%'+@emailOrDisplayNameKeyword+'%' escape '/') ");
            strSQL.Append(" select * from #result where row between @startRowNum and @endRowNum ");
            strSQL.Append(" drop table #result ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(),conn.SqlConn,transaction);
            #region Add Parameters
            cmd.Parameters.AddWithValue("@typeOfUser", Convert.ToInt16(EnumUserType.User));
            cmd.Parameters.AddWithValue("@needVerification", Convert.ToInt16(EnumUserEmailVerificationStatus.enumNotVerified));
            cmd.Parameters.AddWithValue("@refused", Convert.ToInt16(EnumUserModerateStatus.enumRefused));
            cmd.Parameters.AddWithValue("@emailOrDisplayNameKeyword", emailOrDisplayNameKeyword);
            cmd.Parameters.AddWithValue("@startRowNum", startRowNum);
            cmd.Parameters.AddWithValue("@endRowNum", endRowNum);
            #endregion Add parameters
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader(), LoadOption.Upsert);
            return table;
        }
        public static int GetCountOfUsersWhichNeedEmailVerify(SqlConnectionWithSiteId conn, SqlTransaction transaction, string emailOrDisplayNameKeyword)
        {
            emailOrDisplayNameKeyword = CommonFunctions.SqlReplace(emailOrDisplayNameKeyword);
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" select COUNT(*) from t_User"+conn.SiteId);
            strSQL.Append(" where UserType = @typeOfUser and IfVerified='false' and EmailVerificationStatus=@needVerification  and IfDeleted='false'");
            strSQL.Append(" and (Email like '%'+@emailOrDisplayNameKeyword+'%' escape '/' or Name like '%'+@emailOrDisplayNameKeyword+'%' escape '/') ");
            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            #region Add Parameters
            cmd.Parameters.AddWithValue("@typeOfUser", Convert.ToInt16(EnumUserType.User));
            cmd.Parameters.AddWithValue("@needVerification", Convert.ToInt16(EnumUserEmailVerificationStatus.enumNotVerified));
            cmd.Parameters.AddWithValue("@emailOrDisplayNameKeyword", emailOrDisplayNameKeyword);
            #endregion Add Parameters
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        
        #endregion 

        public static void UpdateUserOrOperatorScore(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, int addValue)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("update t_User" + conn.SiteId + " set ForumScore=ForumScore+@addValue where Id=@userOrOperatorId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@addValue", addValue);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateUserOrOperatorReputation(SqlConnectionWithSiteId conn, SqlTransaction transaction, int userOrOperatorId, int addValue)
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("update t_User" + conn.SiteId + " set ForumReputation=ForumReputation+@addValue where Id=@userOrOperatorId");

            SqlCommand cmd = new SqlCommand(strSQL.ToString(), conn.SqlConn, transaction);
            cmd.Parameters.AddWithValue("@addValue", addValue);
            cmd.Parameters.AddWithValue("@userOrOperatorId", userOrOperatorId);

            cmd.ExecuteNonQuery();
        }
    
    }
}
