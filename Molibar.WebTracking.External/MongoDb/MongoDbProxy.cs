using System.Collections.Generic;
using MongoDB.Driver;

namespace Molibar.WebTracking.External.MongoDb
{
    public interface IMongoDbProxy
    {
        bool CollectionExists(string collectionName);
        void CreateCollection(string collectionName);
        void Insert<T>(string collectionName, IEnumerable<T> bsonDocuments);
        MongoCursor<T> FindAll<T>(string collectionName);
        MongoCursor<T> Find<T>(string collectionName, string name, string value);
        void RemoveDocuments(string collectionName);
    }

    public class MongoDbProxy : IMongoDbProxy
    {
        public MongoDatabase MongoDatabase { get; set; }

        public MongoDbProxy(string databaseName = "WebTracker")
        {
            var settings =
                new MongoServerSettings
                    {
                        Server = new MongoServerAddress("localhost", 27017),
                        SafeMode = SafeMode.True
                    };
            var mongoServer = new MongoServer(settings);
            MongoDatabase = mongoServer.GetDatabase(databaseName);

            //_mongoServer = MongoServer.Create("mongodb://127.0.0.1:27017/?safe=true");
            //_mongoDatabase = _mongoServer.GetDatabase("WebTracker");
        }

        internal void DropDatabase()
        {
            MongoDatabase.Drop();
        }

        public bool RemoveCollection(string collectionName)
        {
            var commandResult = MongoDatabase.DropCollection(collectionName);
            return commandResult.Ok;
        }

        public bool CollectionExists(string collectionName)
        {
            return MongoDatabase.CollectionExists(collectionName);
        }

        public void CreateCollection(string collectionName)
        {
            MongoDatabase.CreateCollection(collectionName);
        }

        public void Insert<T>(string collectionName, IEnumerable<T> document)
        {
            var collection = MongoDatabase.GetCollection(collectionName);
            collection.InsertBatch(document);
        }

        public MongoCursor<T> FindAll<T>(string collectionName)
        {
            var collection = MongoDatabase.GetCollection(typeof(T), collectionName);
            return collection.FindAllAs<T>();
        }

        public MongoCursor<T> Find<T>(string collectionName, string name, string value)
        {
            var collection = MongoDatabase.GetCollection(collectionName);
            var queryDocument = new QueryDocument(name, value);
            return collection.FindAs<T>(queryDocument);
        }

        public void RemoveDocuments(string collectionName)
        {
            var collection = MongoDatabase.GetCollection(collectionName);
            collection.RemoveAll();
        }
    }
}
