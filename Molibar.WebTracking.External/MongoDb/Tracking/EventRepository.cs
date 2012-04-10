using System.Collections.Generic;
using Molibar.Infrastructure.Mapper;
using MongoDB.Bson;

namespace Molibar.WebTracking.External.MongoDb.Tracking
{
    public abstract class EventRepository
    {
        protected readonly IMongoDbProxy MongoDbProxy;
        protected readonly IEntityMapper EntityMapper;

        protected abstract string CollectionName { get; }

        protected EventRepository(IMongoDbProxy mongoDbProxy, IEntityMapper entityMapper)
        {
            MongoDbProxy = mongoDbProxy;
            EntityMapper = entityMapper;
        }

        public void Initialize()
        {
            if (!MongoDbProxy.CollectionExists(CollectionName))
            {
                MongoDbProxy.CreateCollection(CollectionName);
            }
        }

        public void Insert(IEnumerable<BsonDocument> bsonDocuments)
        {
            MongoDbProxy.Insert(CollectionName, bsonDocuments);
        }
    }
}