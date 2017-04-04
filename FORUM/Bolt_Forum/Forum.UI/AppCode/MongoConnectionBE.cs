using MongoDB.Driver;
using System;

namespace Com.Comm100.Forum.UI
{
    public class MongoConnectionBE
    {
        public MongoClient _client { get; set; }
        public MongoServer _server { get; set; }
        public MongoDatabase _db { get; set; }
    }
}