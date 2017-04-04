using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace BoltAdmin_MVC.Models
{
    public class UserUploadsBE
    {
        public ObjectId _id { get; set; }
        public Int32 fileSize { get; set; }
        public string fileType { get; set; }
        public string fileName { get; set; }
        public int __v { get; set; }
    }
}