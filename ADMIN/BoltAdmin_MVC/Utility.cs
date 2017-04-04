using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevOne.Security.Cryptography.BCrypt;
using BoltAdmin_MVC.Models;

namespace BoltAdmin_MVC
{
    public static class Utility
    {
        public static string Encryptpassword(string password)
        {
            string hashedPassword = DevOne.Security.Cryptography.BCrypt.BCryptHelper.HashPassword(password, DevOne.Security.Cryptography.BCrypt.BCryptHelper.GenerateSalt(12));
            return hashedPassword;
        }
        public static bool CheckPassword(string enteredPassword, string hashedPassword)
        {
            bool pwdHash = DevOne.Security.Cryptography.BCrypt.BCryptHelper.CheckPassword(enteredPassword, hashedPassword);
            return pwdHash;
        }
    }
}