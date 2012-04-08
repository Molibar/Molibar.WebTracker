using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Amazon.SimpleDB.Model;
using AutoMapper;
using Molibar.Common;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.External.SimpleDb;
using MongoDB.Bson;

namespace Molibar.WebTracking.External.Models.Profiles
{
    public class FormEventProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<BsonDocument, FormEvent>().ConvertUsing(BsonDocumentToFormEvent);
            CreateMap<FormEvent, BsonDocument>().ConvertUsing(FormEventToBsonDocument);

            CreateMap<List<ReplaceableAttribute>, FormEvent>().ConvertUsing(ReplaceableAttributeToFormEvent);
            CreateMap<FormEvent, List<ReplaceableAttribute>>().ConvertUsing(FormEventToReplaceableAttribute);
        }

        private FormEvent BsonDocumentToFormEvent(BsonDocument bsonDocument)
        {
            return new FormEvent
                       {
                           Id = bsonDocument["_id"].AsObjectId.ToString(),
                           VisitGuid = bsonDocument["VisitGuid"].AsGuid,
                           Url = bsonDocument["Url"].AsString,
                           FormName = bsonDocument["FormName"].AsString,
                           EventName = bsonDocument["EventName"].AsString,
                           ElementId = bsonDocument["ElementId"].AsString,
                           ElementValue = bsonDocument["ElementValue", BsonNull.Value].AsString,
                           ValueValid = bsonDocument["ValueValid"].AsBoolean,
                           TimeInMillis = bsonDocument["TimeInMillis", 0].AsInt32,
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
                           {"FormName", formEvent.FormName},
                           {"EventName", formEvent.EventName},
                           {"ElementId", formEvent.ElementId},
                           {"ElementValue", formEvent.ElementValue, formEvent.ElementValue != null},
                           {"ValueValid", BsonBoolean.Create(formEvent.ValueValid)},
                           {"TimeInMillis", formEvent.TimeInMillis},
                           {"DateTime", formEvent.DateTime}
                       };
        }



        protected FormEvent ReplaceableAttributeToFormEvent(List<ReplaceableAttribute> replaceableAttributes)
        {
            return new FormEvent
                       {
                           Id = replaceableAttributes.First(x => x.Name.Equals("Id")).Value,
                           VisitGuid = Guid.Parse(replaceableAttributes.First(x => x.Name.Equals("VisitGuid")).Value),
                           Url = replaceableAttributes.First(x => x.Name.Equals("Url")).Value,
                           FormName = replaceableAttributes.First(x => x.Name.Equals("FormName")).Value,
                           EventName = replaceableAttributes.First(x => x.Name.Equals("EventName")).Value,
                           ElementId = replaceableAttributes.First(x => x.Name.Equals("ElementId")).Value,
                           ElementValue = replaceableAttributes.First(x => x.Name.Equals("ElementValue")).Value,
                           ValueValid = DataConverter.ToBoolean(replaceableAttributes.First(x => x.Name.Equals("ValueValid")).Value),
                           TimeInMillis = DataConverter.ToInt32(replaceableAttributes.First(x => x.Name.Equals("TimeInMillis")).Value),
                           DateTime = DataConverter.ToDateTime(replaceableAttributes.First(x => x.Name.Equals("DateTime")).Value)
                       };
        }

        protected List<ReplaceableAttribute> FormEventToReplaceableAttribute(FormEvent formEvent)
        {
            return new List<ReplaceableAttribute>(
                new[]
                    {
                        new ReplaceableAttribute { Name = "Id", Value = formEvent.Id },
                        new ReplaceableAttribute { Name = "VisitGuid", Value = formEvent.VisitGuid.ToString() },
                        new ReplaceableAttribute { Name = "Url", Value = EnsureValue(formEvent.Url) },
                        new ReplaceableAttribute { Name = "FormName", Value = EnsureValue(formEvent.FormName) },
                        new ReplaceableAttribute { Name = "EventName", Value = EnsureValue(formEvent.EventName) },
                        new ReplaceableAttribute { Name = "ElementId", Value = EnsureValue(formEvent.ElementId) },
                        new ReplaceableAttribute { Name = "ElementValue", Value = EnsureValue(formEvent.ElementValue) },
                        new ReplaceableAttribute { Name = "ValueValid", Value = formEvent.ValueValid.ToString(CultureInfo.InvariantCulture) },
                        new ReplaceableAttribute { Name = "TimeInMillis", Value = EnsurePaddedIntValue(formEvent.TimeInMillis) },
                        new ReplaceableAttribute { Name = "DateTime", Value = formEvent.DateTime.ToString(SimpleDbProxy.DATE_FORMAT_STRING) }
                    }
                );
        }

        internal static string EnsureValue(string value)
        {
            if (value == null) return "<null>";
            return value;
        }

        internal static string EnsurePaddedIntValue(int? value)
        {
            if (!value.HasValue) return "<null>";
            return StringTools.PadValue(value.Value, 7);
        }
    }
}
