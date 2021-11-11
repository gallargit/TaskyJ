using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskyJ.DataRepo
{
    public class MongoDBHelper
    {
        private static MongoClient mongoClient = null;
        private static IMongoDatabase mongoDb = null;

        private static readonly Lazy<MongoDBHelper> lazy = new Lazy<MongoDBHelper>(() => new MongoDBHelper());
        public static MongoDBHelper Instance { get { return lazy.Value; } }

        private MongoDBHelper()
        {
        }

        static MongoDBHelper()
        {
            Init();
        }

        public static void Init()
        {
            mongoClient = new MongoClient("mongodb://localhost:27017");
            mongoDb = mongoClient.GetDatabase("local");
        }

        public static IEnumerable<string> GetAll(string table)
        {
            var lst = new List<string>();
            var collection = mongoDb.GetCollection<BsonDocument>(table);
            var existingDocuments = collection.Find(new BsonDocument()).ToList();
            foreach (var document in existingDocuments)
            {
                document.Elements.Where(s => s.Name == "_id").ToList().ForEach(s => document.RemoveElement(s));
                lst.Add(document.ToJson());
            }
            return lst.AsEnumerable();
        }

        public static void Add(string table, string jsondata)
        {
            var data = BsonDocument.Parse(jsondata);
            Remove(table, jsondata);
            mongoDb.GetCollection<BsonDocument>(table).InsertOne(data);
        }

        public static void Remove(string table, string jsondata)
        {
            var data = BsonDocument.Parse(jsondata);
            var id = 0;
            try
            {
                id = data.GetElement("ID").Value.AsInt32;
            }
            catch { }
            var collection = mongoDb.GetCollection<BsonDocument>(table);
            if (id > 0)
            {
                var existingDocuments = collection.Find(new BsonDocument { { "ID", id } }).ToList();
                existingDocuments.ForEach(d => collection.DeleteOne(d));
            }
        }
    }
}
