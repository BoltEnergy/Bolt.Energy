using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoltAdmin_MVC.Models
{
    public class AdminLoginBE
    {
        public ObjectId _id { get; set; }
        [Required]
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

    }
}