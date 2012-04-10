using System;
using AutoMapper;
using FizzWare.NBuilder;
using Molibar.Infrastructure.Mapper.AutoMapper;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.External.Models.Profiles;
using MongoDB.Bson;
using NUnit.Framework;

namespace Molibar.WebTracking.UnitTests.External.Models.Profiles
{
    [TestFixture]
    public class FormEventBsonDocumentProfileTests
    {
        private EntityMapper _entityMapper;

        [SetUp]
        public void Setup()
        {
            Mapper.AddProfile(new FormEventBsonDocumentProfile());
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
                .With(x => x.ClientDateTime = new DateTime(2012, 1, 1, 12, 13, 14))
                .With(x => x.DateTime = new DateTime(2012, 1, 1, 12, 13, 15)).Build();

            // Act
            var result = _entityMapper.Map<BsonDocument>(formEvent);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result["_id"], Is.EqualTo(BsonObjectId.Create(formEvent.Id)));
            Assert.That(result["VisitGuid"].AsGuid, Is.EqualTo(formEvent.VisitGuid));
            Assert.That(result["Url"].AsString, Is.EqualTo(formEvent.Url));
            Assert.That(result["PageId"].AsString, Is.EqualTo(formEvent.PageId));
            Assert.That(result["ElementId"].AsString, Is.EqualTo(formEvent.ElementId));
            Assert.That(result["EventType"].AsString, Is.EqualTo(formEvent.EventType));
            Assert.That(result["ElementType"].AsString, Is.EqualTo(formEvent.ElementType));
            Assert.That(result["ElementValue"].AsString, Is.EqualTo(formEvent.ElementValue));
            Assert.That(result["ValueValid"].AsBoolean, Is.EqualTo(formEvent.ValueValid));
            Assert.That(result["ClientDateTime"].AsDateTime.Ticks, Is.EqualTo(formEvent.ClientDateTime.Ticks));
            Assert.That(result["DateTime"].AsDateTime.Ticks, Is.EqualTo(formEvent.DateTime.Ticks));
        }

        [Test]
        public void ShouldMapBsonDocumentToFormEvent()
        {
            // Arrange
            var clientDateTime = new DateTime(1234, 5, 6, 17, 18, 1, 90);
            var dateTime = new DateTime(1234, 5, 6, 17, 18, 19, 20);
            var bsonDocument = new BsonDocument
                       {
                           {"_id", BsonObjectId.Create(1234,1234,1234,1234)},
                           {"VisitGuid", Guid.NewGuid()},
                           {"Url", "Url"},
                           {"PageId", "PageId"},
                           {"ElementId", "ElementId"},
                           {"EventType", "EventType"},
                           {"ElementType", "ElementType"},
                           {"ElementValue", "ElementValue"},
                           {"ValueValid", true},
                           {"ClientDateTime", clientDateTime},
                           {"DateTime", dateTime}
                       };

            // Act
            var result = _entityMapper.Map<FormEvent>(bsonDocument);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(bsonDocument["_id"].AsObjectId.ToString()));
            Assert.That(result.VisitGuid, Is.EqualTo(bsonDocument["VisitGuid"].AsGuid));
            Assert.That(result.Url, Is.EqualTo(bsonDocument["Url"].AsString));
            Assert.That(result.PageId, Is.EqualTo(bsonDocument["PageId"].AsString));
            Assert.That(result.ElementId, Is.EqualTo(bsonDocument["ElementId"].AsString));
            Assert.That(result.EventType, Is.EqualTo(bsonDocument["EventType"].AsString));
            Assert.That(result.ElementType, Is.EqualTo(bsonDocument["ElementType"].AsString));
            Assert.That(result.ElementValue, Is.EqualTo(bsonDocument["ElementValue"].AsString));
            Assert.That(result.ValueValid, Is.EqualTo(bsonDocument["ValueValid"].AsBoolean));
            Assert.That(result.ClientDateTime, Is.EqualTo(bsonDocument["ClientDateTime"].AsDateTime));
            Assert.That(result.DateTime, Is.EqualTo(bsonDocument["DateTime"].AsDateTime));
        }
    }
}
