using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Com.Comm100.Forum.UI
{
    public class MongoCon
    {

        public MongoConnectionBE CreateConnection()
        {
            MongoConnectionBE newConnection = new MongoConnectionBE();
            newConnection._client = new MongoClient(ConfigurationManager.AppSettings[Constants.WK_MongoConnectionString]);
            newConnection._server = newConnection._client.GetServer();
            newConnection._db = newConnection._server.GetDatabase(ConfigurationManager.AppSettings[Constants.WK_Db]);
            return newConnection;
        }


        public UserBE Validateuser(string email, string password, Boolean isEncrypted = false)
        {
            MongoConnectionBE con = CreateConnection();
            var query_id = Query.EQ("email", email);

            UserBE currUser = con._db.GetCollection<UserBE>("users").FindOne(query_id);
            if (currUser != null)
            {
                if (isEncrypted)
                {
                    if (password == currUser.password)
                    {
                        return currUser;
                    }
                    else
                        return null;

                }
                else if (Utility.CheckPassword(password, currUser.password))
                {
                    return currUser;
                }
            }
            return null;
        }


    }
}