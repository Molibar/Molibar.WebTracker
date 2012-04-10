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
    public class PageEventBsonDocumentProfileTests
    {
        private EntityMapper _entityMapper;

        [SetUp]
        public void Setup()
        {
            Mapper.AddProfile(new PageEventBsonDocumentProfile());
            _entityMapper = new EntityMapper(Mapper.Engine);
        }

        [Test]
        public void AssertConfigurationIsValid()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [Test]
        public void ShouldMapPageEventToBsonDocument()
        {
            // Arrange
            var pageEvent = Builder<PageEvent>.CreateNew()
                .With(x => x.Id = BsonObjectId.GenerateNewId().ToString())
                .With(x => x.ClientDateTime = new DateTime(2012, 1, 1, 12, 13, 14))
                .With(x => x.DateTime = new DateTime(2012, 1, 1, 12, 13, 15)).Build();

            // Act
            var result = _entityMapper.Map<BsonDocument>(pageEvent);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result["_id"], Is.EqualTo(BsonObjectId.Create(pageEvent.Id)));
            Assert.That(result["VisitGuid"].AsGuid, Is.EqualTo(pageEvent.VisitGuid));
            Assert.That(result["Url"].AsString, Is.EqualTo(pageEvent.Url));
            Assert.That(result["PageId"].AsString, Is.EqualTo(pageEvent.PageId));
            Assert.That(result["ElementId"].AsString, Is.EqualTo(pageEvent.ElementId));
            Assert.That(result["EventType"].AsString, Is.EqualTo(pageEvent.EventType));
            Assert.That(result["ElementType"].AsString, Is.EqualTo(pageEvent.ElementType));
            Assert.That(result["X"].AsInt32, Is.EqualTo(pageEvent.X));
            Assert.That(result["Y"].AsInt32, Is.EqualTo(pageEvent.Y));

            Assert.That(result["ClientDateTime"].AsDateTime.Ticks, Is.EqualTo(pageEvent.ClientDateTime.Ticks));
            Assert.That(result["DateTime"].AsDateTime.Ticks, Is.EqualTo(pageEvent.DateTime.Ticks));
        }

        [Test]
        public void ShouldMapBsonDocumentToPageEvent()
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
                                       {"X", 123},
                                       {"Y", 456},
                                       {"ClientDateTime", clientDateTime},
                                       {"DateTime", dateTime}
                                   };

            // Act
            var result = _entityMapper.Map<PageEvent>(bsonDocument);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(bsonDocument["_id"].AsObjectId.ToString()));
            Assert.That(result.VisitGuid, Is.EqualTo(bsonDocument["VisitGuid"].AsGuid));
            Assert.That(result.Url, Is.EqualTo(bsonDocument["Url"].AsString));
            Assert.That(result.PageId, Is.EqualTo(bsonDocument["PageId"].AsString));
            Assert.That(result.ElementId, Is.EqualTo(bsonDocument["ElementId"].AsString));
            Assert.That(result.EventType, Is.EqualTo(bsonDocument["EventType"].AsString));
            Assert.That(result.ElementType, Is.EqualTo(bsonDocument["ElementType"].AsString));
            Assert.That(result.X, Is.EqualTo(bsonDocument["X"].AsInt32));
            Assert.That(result.Y, Is.EqualTo(bsonDocument["Y"].AsInt32));
            Assert.That(result.ClientDateTime, Is.EqualTo(bsonDocument["ClientDateTime"].AsDateTime));
            Assert.That(result.DateTime, Is.EqualTo(bsonDocument["DateTime"].AsDateTime));
        }
    }
}