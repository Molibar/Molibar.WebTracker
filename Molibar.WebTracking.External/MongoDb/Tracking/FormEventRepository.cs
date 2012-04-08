using System.Collections.Generic;
using Molibar.Infrastructure.Mapper;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.Domain.Repositories;
using MongoDB.Bson;

namespace Molibar.WebTracking.External.MongoDb.Tracking
{
    public class FormEventRepository : IFormEventRepository
    {
        public const string COLLECTION_NAME = "FormEvents";

        private readonly IMongoDbProxy _mongoDbProxy;
        private readonly IEntityMapper _entityMapper;

        public FormEventRepository(IMongoDbProxy mongoDbProxy, IEntityMapper entityMapper)
        {
            _mongoDbProxy = mongoDbProxy;
            _entityMapper = entityMapper;
        }

        public void Initialize()
        {
            if (!_mongoDbProxy.CollectionExists(COLLECTION_NAME))
            {
                _mongoDbProxy.CreateCollection(COLLECTION_NAME);
            }
        }


        public void Insert(FormEvent formEvent)
        {
            var bsonDocument = _entityMapper.Map<BsonDocument>(formEvent);
            _mongoDbProxy.Insert(COLLECTION_NAME, new[] { bsonDocument });
        }


        public void Insert(IEnumerable<FormEvent> formEntries)
        {
            var bsonDocuments = _entityMapper.Map<IEnumerable<BsonDocument>>(formEntries);
            _mongoDbProxy.Insert(COLLECTION_NAME, bsonDocuments);
        }
    }
}
