using AutoMapper;
using Molibar.WebTracking.Domain.Model;
using MongoDB.Bson;

namespace Molibar.WebTracking.External.Models.Profiles
{
    public class FormEventBsonDocumentProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<BsonDocument, FormEvent>().ConvertUsing(BsonDocumentToFormEvent);
            CreateMap<FormEvent, BsonDocument>().ConvertUsing(FormEventToBsonDocument);
        }

        private FormEvent BsonDocumentToFormEvent(BsonDocument bsonDocument)
        {
            return new FormEvent
                       {
                           Id = bsonDocument["_id"].AsObjectId.ToString(),
                           VisitGuid = bsonDocument["VisitGuid"].AsGuid,
                           Url = bsonDocument["Url"].AsString,
                           PageId = bsonDocument["PageId"].AsString,
                           ElementId = bsonDocument["ElementId"].AsString,
                           EventType = bsonDocument["EventType"].AsString,
                           ElementType = bsonDocument["ElementType", BsonNull.Value].AsString,
                           ElementValue = bsonDocument["ElementValue", BsonNull.Value].AsString,
                           
                           ValueValid = bsonDocument["ValueValid"].AsBoolean,
                           ClientDateTime = bsonDocument["ClientDateTime"].AsDateTime,
                           DateTime = bsonDocument["DateTime"].AsDateTime
                       };
        }

        private BsonDocument FormEventToBsonDocument(FormEvent formEvent)
        {
            return new BsonDocument
                       {
                           {"_id", BsonObjectId.Create(formEvent.Id), formEvent.Id != null},
                           {"VisitGuid", formEvent.VisitGuid},
                           {"Url", formEvent.Url},
                           {"PageId", formEvent.PageId},
                           {"EventType", formEvent.EventType},
                           {"ElementId", formEvent.ElementId},
                           {"ElementType", formEvent.ElementType, formEvent.ElementType != null},
                           {"ElementValue", formEvent.ElementValue, formEvent.ElementValue != null},
                           {"ValueValid", BsonBoolean.Create(formEvent.ValueValid)},
                           {"ClientDateTime", formEvent.ClientDateTime},
                           {"DateTime", formEvent.DateTime}
                       };
        }

    }
}