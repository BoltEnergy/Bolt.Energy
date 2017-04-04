using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoltAdmin_MVC.Models
{
    public class Insertlogs
    {
        public ObjectId _id { get; set; }
        public DateTime Date { get; set; }
        public string message { get; set; }
    }

}