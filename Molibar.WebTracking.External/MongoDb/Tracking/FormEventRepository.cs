using System.Collections.Generic;
using Molibar.Infrastructure.Mapper;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.Domain.Repositories;
using MongoDB.Bson;

namespace Molibar.WebTracking.External.MongoDb.Tracking
{
    public class FormEventRepository : EventRepository, IFormEventRepository
    {
        public const string COLLECTION_NAME = "FormEvents";
        protected override string CollectionName { get { return COLLECTION_NAME; } }

        public FormEventRepository(IMongoDbProxy mongoDbProxy, IEntityMapper entityMapper)
            : base(mongoDbProxy, entityMapper)
        {
        }

        public void Insert(FormEvent formEvent)
        {
            var bsonDocument = EntityMapper.Map<BsonDocument>(formEvent);
            Insert(new[] { bsonDocument });
        }

        public void Insert(IEnumerable<FormEvent> formEntries)
        {
            var bsonDocuments = EntityMapper.Map<IEnumerable<BsonDocument>>(formEntries);
            Insert(bsonDocuments);
        }
    }
}
