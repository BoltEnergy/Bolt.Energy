using MongoDB.Driver;
using System;

namespace BoltAdmin_MVC.Models
{
    public class MongoConnectionBE
    {
        public MongoClient _client { get; set; }
        public MongoServer _server { get; set; }
        public MongoDatabase _db { get; set; }
    }
}