using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoltAdmin_MVC.Models
{
    public class Content
    {
        public ObjectId _id { get; set; }
        public string pageTitle { get; set; }
        [Required]
        public string pageVanityUrl { get; set; }
        public string pageContent { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
    }
}