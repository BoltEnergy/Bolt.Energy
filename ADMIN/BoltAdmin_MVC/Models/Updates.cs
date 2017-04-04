using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoltAdmin_MVC.Models
{
    public class Updates
    {
        public ObjectId _id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public Boolean visible { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public string projectname { get; set; }
        public string projectid { get; set; }
        public int __v { get; set; }
    }
}