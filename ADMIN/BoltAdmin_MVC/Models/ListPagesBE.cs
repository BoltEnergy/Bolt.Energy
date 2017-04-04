using MongoDB.Bson;
using System;

namespace BoltAdmin_MVC.Models
{
    public class ListPagesBE
    {
        public ObjectId _id { get; set; }
        public string pageVanityUrl { get; set; }
    }
}