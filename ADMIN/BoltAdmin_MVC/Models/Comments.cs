using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoltAdmin_MVC.Models
{
    public class Comments
    {
        public ObjectId _id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public ObjectId postedBy { get; set; }
        public object replies { get; set; }
        public DateTime created { get; set; }
        public int __v { get; set; }
    }
}