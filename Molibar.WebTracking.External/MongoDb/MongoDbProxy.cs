using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Molibar.WebTracking.External.MongoDb
{
    public interface IMongoDbProxy
    {
        bool CollectionExists(string collectionName);
        void CreateCollection(string collectionName);
        void Insert(string collectionName, IEnumerable<BsonDocument> bsonDocuments);
        MongoCursor<BsonDocument> FindAll(string collectionName);
        MongoCursor<BsonDocument> Find(string collectionName, string name, string value);
        void RemoveDocuments(string collectionName);
    }

    public class MongoDbProxy : IMongoDbProxy
    {
        private MongoDatabase _mongoDatabase;

        public MongoDbProxy()
        {
            _mongoDatabase = MongoDatabase.Create("mongodb://127.0.0.1:27017/WebTrackerTest");
        }

        public bool CollectionExists(string collectionName)
        {
            return _mongoDatabase.CollectionExists(collectionName);
        }

        public void CreateCollection(string collectionName)
        {
            _mongoDatabase.CreateCollection(collectionName);
        }

        public bool RemoveCollection(string collectionName)
        {
            var commandResult = _mongoDatabase.DropCollection(collectionName);
            return commandResult.Ok;
        }

        public void Insert(string collectionName, IEnumerable<BsonDocument> bsonDocuments)
        {
            var collection = _mongoDatabase.GetCollection(collectionName);
            collection.InsertBatch(bsonDocuments);
        }

        public MongoCursor<BsonDocument> FindAll(string collectionName)
        {
            var collection = _mongoDatabase.GetCollection(collectionName);
            return collection.FindAll();
        }

        public MongoCursor<BsonDocument> Find(string collectionName, string name, string value)
        {
            var collection = _mongoDatabase.GetCollection(collectionName);
            var queryDocument = new QueryDocument(name, value);
            return collection.Find(queryDocument);
        }

        public void RemoveDocuments(string collectionName)
        {
            var collection = _mongoDatabase.GetCollection(collectionName);
            collection.RemoveAll();
        }
    }
}
