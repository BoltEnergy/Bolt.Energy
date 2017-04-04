using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Com.Comm100.Forum.UI
{
    public class UserBE
    {
        public ObjectId _id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        [BsonElementAttribute("uploads")]
        public IEnumerable<ObjectId> uploads { get; set; }
        [BsonElementAttribute("created")]
        public DateTime created { get; set; }
        public bool isAdmin { get; set; }
        public string accountType { get; set; }
        [BsonElementAttribute("modified")]
        public DateTime modified { get; set; }
        public int __v { get; set; }
        public List<UserUploadsBE> userUploads { get; set; }
    }

    public class UserUploadsBE
    {
        public ObjectId _id { get; set; }
        public Int32 fileSize { get; set; }
        public string fileType { get; set; }
        public string fileName { get; set; }
        public int __v { get; set; }
    }


    public class LoginUserBE
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IpAddress { get; set; }        
        public string MongoUID { get; set; }
        public string AccountType { get; set; }
    }






}