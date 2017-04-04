using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoltAdmin_MVC.Models
{
    public class DisplayProducers
    {
        public ObjectId _id { get; set; }
        public ObjectId owner { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string energyType { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string approvalNumber { get; set; }
        public DateTime created { get; set; }

        [BsonElementAttribute("comments")]
        public IEnumerable<ObjectId> comments { get; set; }

        [BsonElementAttribute("updates")]
        public IEnumerable<ObjectId> updates { get; set; }

        [BsonElementAttribute("projects")]
        public IEnumerable<ObjectId> projects { get; set; }
        public object certifications { get; set; }
        public object uploads { get; set; }
        public object availability { get; set; }
        public int __v { get; set; }
        public string zip { get; set; }
        public List<Comments> Listcomments { get; set; }
    }
}