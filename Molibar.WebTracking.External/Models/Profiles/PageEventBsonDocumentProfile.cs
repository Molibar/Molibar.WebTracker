using AutoMapper;
using Molibar.WebTracking.Domain.Model;
using MongoDB.Bson;

namespace Molibar.WebTracking.External.Models.Profiles
{
    public class PageEventBsonDocumentProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<BsonDocument, PageEvent>().ConvertUsing(BsonDocumentToPageEvent);
            CreateMap<PageEvent, BsonDocument>().ConvertUsing(PageEventToBsonDocument);
        }

        private PageEvent BsonDocumentToPageEvent(BsonDocument bsonDocument)
        {
            return new PageEvent
                       {
                           Id = bsonDocument["_id"].AsObjectId.ToString(),
                           VisitGuid = bsonDocument["VisitGuid"].AsGuid,
                           Url = bsonDocument["Url"].AsString,

                           PageId = bsonDocument["PageId"].AsString,
                           ElementId = bsonDocument["ElementId"].AsString,
                           EventType = bsonDocument["EventType"].AsString,
                           ElementType = bsonDocument["ElementType"].AsString,
                           X = bsonDocument["X"].AsInt32,
                           Y = bsonDocument["Y"].AsInt32,

                           ClientDateTime = bsonDocument["ClientDateTime"].AsDateTime,
                           DateTime = bsonDocument["DateTime"].AsDateTime
                       };
        }

        private BsonDocument PageEventToBsonDocument(PageEvent pageEvent)
        {
            return new BsonDocument
                       {
                           {"_id", BsonObjectId.Create(pageEvent.Id), pageEvent.Id != null},
                           {"VisitGuid", pageEvent.VisitGuid},
                           {"Url", pageEvent.Url},
                           {"PageId", pageEvent.PageId},
                           {"ElementId", pageEvent.ElementId},
                           {"EventType", pageEvent.EventType},
                           {"ElementType", pageEvent.ElementType, pageEvent.ElementType != null},
                           {"X", pageEvent.X},
                           {"Y", pageEvent.Y},
                           {"ClientDateTime", pageEvent.ClientDateTime},
                           {"DateTime", pageEvent.DateTime}
                       };
        }

    }
}