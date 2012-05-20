using System.Collections.Generic;
using Molibar.Infrastructure.Mapper;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.Domain.Repositories;
using Molibar.WebTracking.External.Models;
using MongoDB.Bson;

namespace Molibar.WebTracking.External.MongoDb.Tracking
{
    public class FormEventRepository : EventRepository, IFormEventRepository
    {
        public const string COLLECTION_NAME = "PageEvents";
        protected override string CollectionName { get { return COLLECTION_NAME; } }

        public FormEventRepository(IMongoDbProxy mongoDbProxy, IEntityMapper entityMapper)
            : base(mongoDbProxy, entityMapper)
        {
        }

        public void Insert(FormEvent formEvent)
        {
            var formEventDataModel = EntityMapper.Map<FormEventDataModel>(formEvent);
            MongoDbProxy.Insert(CollectionName, new[] { formEventDataModel });
        }

        public void Insert(IEnumerable<FormEvent> formEvents)
        {
            var formEventDataModels = EntityMapper.Map<IEnumerable<FormEventDataModel>>(formEvents);
            MongoDbProxy.Insert(CollectionName, formEventDataModels);
        }
    }
}
