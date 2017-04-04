using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoltAdmin_MVC.Models
{
    public class Uploads
    {
        public ObjectId _id { get; set; }
        public string fileSize { get; set; }
        public string fileType { get; set; }
        public string fileName { get; set; }
        public int _v { get; set; }
    }
}