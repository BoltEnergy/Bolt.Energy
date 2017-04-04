using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoltAdmin_MVC.Models
{
    public class Projects
    {
        public ObjectId _id { get; set; }
        public ObjectId projectOwner { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string status { get; set; }
        public string projectType { get; set; }
        public string energyMix { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string programCategory { get; set; }
        public string capacity { get; set; }
        public Boolean featured { get; set; }
        [BsonElementAttribute("uploads")]
        public IEnumerable<ObjectId> uploads { get; set; }
        public object utilityDistricts { get; set; }
        [BsonElementAttribute("availability")]
        public IEnumerable<string> availability { get; set; }
        public int __v { get; set; }
    }
}