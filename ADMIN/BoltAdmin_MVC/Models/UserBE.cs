using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace BoltAdmin_MVC.Models
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
    
}