using System.Collections.Generic;
using Molibar.Infrastructure.Mapper;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.Domain.Repositories;
using Molibar.WebTracking.External.Models;
using MongoDB.Bson;

namespace Molibar.WebTracking.External.MongoDb.Tracking
{
    public class PageEventRepository : EventRepository, IPageEventRepository
    {
        public const string COLLECTION_NAME = "PageEvents";
        protected override string CollectionName { get { return COLLECTION_NAME; } }

        public PageEventRepository(IMongoDbProxy mongoDbProxy, IEntityMapper entityMapper)
            : base(mongoDbProxy, entityMapper)
        {
        }

        public void Insert(PageEvent pageEvent)
        {
            var pageEventDataModel = EntityMapper.Map<PageEventDataModel>(pageEvent);
            MongoDbProxy.Insert(CollectionName, new[] { pageEventDataModel });
        }

        public void Insert(IEnumerable<PageEvent> pageEvents)
        {
            var pageEventDataModels = EntityMapper.Map<IEnumerable<PageEventDataModel>>(pageEvents);
            MongoDbProxy.Insert(CollectionName, pageEventDataModels);
        }
    }
}