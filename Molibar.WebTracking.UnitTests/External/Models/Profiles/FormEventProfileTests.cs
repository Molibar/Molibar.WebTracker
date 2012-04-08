using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Amazon.SimpleDB.Model;
using AutoMapper;
using FizzWare.NBuilder;
using Molibar.Common;
using Molibar.Infrastructure.Mapper.AutoMapper;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.External.Models.Profiles;
using Molibar.WebTracking.External.SimpleDb;
using MongoDB.Bson;
using NUnit.Framework;

namespace Molibar.WebTracking.UnitTests.External.Models.Profiles
{
    [TestFixture]
    public class FormEventProfileTests
    {
        private EntityMapper _entityMapper;

        [SetUp]
        public void Setup()
        {
            Mapper.AddProfile(new FormEventProfile());
            _entityMapper = new EntityMapper(Mapper.Engine);
        }

        [Test]
        public void AssertConfigurationIsValid()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [Test]
        public void ShouldMapFormEventToBsonDocument()
        {
            // Arrange
            var formEvent = Builder<FormEvent>.CreateNew()
                .With(x => x.Id = BsonObjectId.GenerateNewId().ToString())
                .With(x => x.TimeInMillis = 6)
                .With(x => x.DateTime = DateTime.Now).Build();

            // Act
            var result = _entityMapper.Map<BsonDocument>(formEvent);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result["_id"], Is.EqualTo(BsonObjectId.Create(formEvent.Id)));
            Assert.That(result["VisitGuid"].AsGuid, Is.EqualTo(formEvent.VisitGuid));
            Assert.That(result["Url"].AsString, Is.EqualTo(formEvent.Url));
            Assert.That(result["FormName"].AsString, Is.EqualTo(formEvent.FormName));
            Assert.That(result["EventName"].AsString, Is.EqualTo(formEvent.EventName));
            Assert.That(result["ElementId"].AsString, Is.EqualTo(formEvent.ElementId));
            Assert.That(result["ElementValue"].AsString, Is.EqualTo(formEvent.ElementValue));
            Assert.That(result["ValueValid"].AsBoolean, Is.EqualTo(formEvent.ValueValid));
            Assert.That(result["TimeInMillis"].AsInt32, Is.EqualTo(formEvent.TimeInMillis));
            Assert.That(result["DateTime"].AsDateTime.Ticks, Is.EqualTo(formEvent.DateTime.Ticks));
        }

        [Test]
        public void ShouldMapBsonDocumentToFormEvent()
        {
            // Arrange
            var dateTime = new DateTime(1234, 5, 6, 17, 18, 19, 20);
            var bsonDocument = new BsonDocument
                       {
                           {"_id", BsonObjectId.Create(1234,1234,1234,1234)},
                           {"VisitGuid", Guid.NewGuid()},
                           {"Url", "Url"},
                           {"FormName", "FormName"},
                           {"EventName", "EventName"},
                           {"ElementId", "ElementId"},
                           {"ElementValue", "ElementValue"},
                           {"ValueValid", true},
                           {"TimeInMillis", 1234},
                           {"DateTime", dateTime}
                       };

            // Act
            var result = _entityMapper.Map<FormEvent>(bsonDocument);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(bsonDocument["_id"].AsObjectId.ToString()));
            Assert.That(result.VisitGuid, Is.EqualTo(bsonDocument["VisitGuid"].AsGuid));
            Assert.That(result.Url, Is.EqualTo(bsonDocument["Url"].AsString));
            Assert.That(result.FormName, Is.EqualTo(bsonDocument["FormName"].AsString));
            Assert.That(result.EventName, Is.EqualTo(bsonDocument["EventName"].AsString));
            Assert.That(result.ElementId, Is.EqualTo(bsonDocument["ElementId"].AsString));
            Assert.That(result.ElementValue, Is.EqualTo(bsonDocument["ElementValue"].AsString));
            Assert.That(result.ValueValid, Is.EqualTo(bsonDocument["ValueValid"].AsBoolean));
            Assert.That(result.TimeInMillis, Is.EqualTo(bsonDocument["TimeInMillis"].AsInt32));
            Assert.That(result.DateTime, Is.EqualTo(bsonDocument["DateTime"].AsDateTime));
        }

        [Test]
        public void ShouldMapFormEventToReplaceAttributes()
        {
            // Arrange
            var formEvent = Builder<FormEvent>.CreateNew()
                .With(x => x.TimeInMillis = 6).Build();

            // Act
            var result = _entityMapper.Map<List<ReplaceableAttribute>>(formEvent);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(GetReplaceableAttributeValue(result, "Id"), Is.EqualTo(formEvent.Id));
            Assert.That(GetReplaceableAttributeValue(result, "VisitGuid"), Is.EqualTo(formEvent.VisitGuid.ToString()));
            Assert.That(GetReplaceableAttributeValue(result, "Url"), Is.EqualTo(formEvent.Url));
            Assert.That(GetReplaceableAttributeValue(result, "FormName"), Is.EqualTo(formEvent.FormName));
            Assert.That(GetReplaceableAttributeValue(result, "EventName"), Is.EqualTo(formEvent.EventName));
            Assert.That(GetReplaceableAttributeValue(result, "ElementId"), Is.EqualTo(formEvent.ElementId));
            Assert.That(GetReplaceableAttributeValue(result, "ElementValue"), Is.EqualTo(formEvent.ElementValue));
            Assert.That(GetReplaceableAttributeValue(result, "ValueValid"), Is.EqualTo(formEvent.ValueValid.ToString(CultureInfo.InvariantCulture)));
            Assert.That(GetReplaceableAttributeValue(result, "TimeInMillis"), Is.EqualTo("000000" + formEvent.TimeInMillis.Value.ToString(CultureInfo.InvariantCulture)));
            Assert.That(GetReplaceableAttributeValue(result, "DateTime"), Is.EqualTo(formEvent.DateTime.ToString(SimpleDbProxy.DATE_FORMAT_STRING)));
        }

        [Test]
        public void ShouldMapReplaceAttributesToFormEvent()
        {
            // Arrange
            var replaceableAttributes = new List<ReplaceableAttribute>();
            replaceableAttributes.Add(new ReplaceableAttribute { Name = "Id", Value = "Id" });
            replaceableAttributes.Add(new ReplaceableAttribute {Name = "VisitGuid", Value = Guid.NewGuid().ToString()});
            replaceableAttributes.Add(new ReplaceableAttribute {Name = "Url", Value = "Url"});
            replaceableAttributes.Add(new ReplaceableAttribute { Name = "FormName", Value = "FormName" });
            replaceableAttributes.Add(new ReplaceableAttribute { Name = "EventName", Value = "EventName" });
            replaceableAttributes.Add(new ReplaceableAttribute { Name = "ElementId", Value = "ElementId" });
            replaceableAttributes.Add(new ReplaceableAttribute { Name = "ElementValue", Value = "ElementValue" });
            replaceableAttributes.Add(new ReplaceableAttribute { Name = "ValueValid", Value = true.ToString(CultureInfo.InvariantCulture) });
            replaceableAttributes.Add(new ReplaceableAttribute {Name = "TimeInMillis", Value = "0000006"});
            replaceableAttributes.Add(new ReplaceableAttribute{Name = "DateTime", Value = DateTime.Now.ToString(SimpleDbProxy.DATE_FORMAT_STRING)});

            // Act
            var result = _entityMapper.Map<FormEvent>(replaceableAttributes);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "Id")));
            Assert.That(result.VisitGuid.ToString(), Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "VisitGuid")));
            Assert.That(result.Url, Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "Url")));
            Assert.That(result.FormName, Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "FormName")));
            Assert.That(result.EventName, Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "EventName")));
            Assert.That(result.ElementId, Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "ElementId")));
            Assert.That(result.ElementValue, Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "ElementValue")));
            Assert.That(result.ValueValid.ToString(), Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "ValueValid")));
            Assert.That(result.TimeInMillis.ToString(), Is.EqualTo(DataConverter.ToInt32(
                GetReplaceableAttributeValue(replaceableAttributes, "TimeInMillis")
                ).ToString(CultureInfo.InvariantCulture)));
            Assert.That(result.DateTime.ToString(SimpleDbProxy.DATE_FORMAT_STRING), Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "DateTime")));
        }


        [Test]
        public void ShouldReturnNullStringWhenEnsureValueIsCalledWithNull()
        {
            // Arrange
            var expected = "<null>";
            string value = null;

            // Act
            var result = FormEventProfile.EnsureValue(value);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }


        [Test]
        public void ShouldReturnNullStringWhenEnsurePaddedIntValueIsCalledWithNull()
        {
            // Arrange
            var expected = "<null>";
            int? value = null;

            // Act
            var result = FormEventProfile.EnsurePaddedIntValue(value);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        private static string GetReplaceableAttributeValue(List<ReplaceableAttribute> result, string name)
        {
            var replaceableAttribute = result.FirstOrDefault(x=> x.Name.Equals(name));
            return replaceableAttribute != null ? replaceableAttribute.Value : null;
        }
    }
}
