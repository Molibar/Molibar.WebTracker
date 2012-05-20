using System;
using AutoMapper;
using FizzWare.NBuilder;
using Molibar.Infrastructure.Mapper.AutoMapper;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.External.Models;
using Molibar.WebTracking.External.Models.Profiles;
using MongoDB.Bson;
using NUnit.Framework;

namespace Molibar.WebTracking.UnitTests.External.Models.Profiles
{
    [TestFixture]
    public class PageEventPageEventDataModelProfileTests
    {
        private EntityMapper _entityMapper;

        [SetUp]
        public void Setup()
        {
            Mapper.AddProfile(new PageEventPageEventDataModelProfile());
            _entityMapper = new EntityMapper(Mapper.Engine);
        }

        [Test]
        public void AssertConfigurationIsValid()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [Test]
        public void ShouldMapPageEventToPageEventDataModel()
        {
            // Arrange
            var pageEvent = Builder<PageEvent>.CreateNew()
                .With(x => x.Id = ObjectId.GenerateNewId().ToString())
                .With(x => x.ClientDateTime = new DateTime(2012, 1, 1, 12, 13, 14))
                .With(x => x.DateTime = new DateTime(2012, 1, 1, 12, 13, 15)).Build();

            // Act
            var result = _entityMapper.Map<PageEventDataModel>(pageEvent);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(ObjectId.Parse(pageEvent.Id)));
            Assert.That(result.VisitGuid, Is.EqualTo(pageEvent.VisitGuid.ToString()));
            Assert.That(result.Url, Is.EqualTo(pageEvent.Url));
            Assert.That(result.PageId, Is.EqualTo(pageEvent.PageId));
            Assert.That(result.ElementId, Is.EqualTo(pageEvent.ElementId));
            Assert.That(result.EventType, Is.EqualTo(pageEvent.EventType));
            Assert.That(result.ElementType, Is.EqualTo(pageEvent.ElementType));
            Assert.That(result.X, Is.EqualTo(pageEvent.X));
            Assert.That(result.Y, Is.EqualTo(pageEvent.Y));
            Assert.That(result.ClientDateTime, Is.EqualTo(pageEvent.ClientDateTime));
            Assert.That(result.DateTime, Is.EqualTo(pageEvent.DateTime));
        }

        [Test]
        public void ShouldMapForEventDataModelToPageEvent()
        {
            // Arrange
            var clientDateTime = new DateTime(1234, 5, 6, 17, 18, 1, 90);
            var dateTime = new DateTime(1234, 5, 6, 17, 18, 19, 20);
            var pageEventDataModel = Builder<PageEventDataModel>.CreateNew()
                .With(x => x.Id = ObjectId.GenerateNewId())
                .With(x => x.VisitGuid = Guid.NewGuid().ToString())
                .With(x => x.ClientDateTime = clientDateTime)
                .With(x => x.DateTime = dateTime).Build();


            // Act
            var result = _entityMapper.Map<PageEvent>(pageEventDataModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(pageEventDataModel.Id.ToString()));
            Assert.That(result.VisitGuid.ToString(), Is.EqualTo(pageEventDataModel.VisitGuid));
            Assert.That(result.Url, Is.EqualTo(pageEventDataModel.Url));
            Assert.That(result.PageId, Is.EqualTo(pageEventDataModel.PageId));
            Assert.That(result.ElementId, Is.EqualTo(pageEventDataModel.ElementId));
            Assert.That(result.EventType, Is.EqualTo(pageEventDataModel.EventType));
            Assert.That(result.ElementType, Is.EqualTo(pageEventDataModel.ElementType));
            Assert.That(result.X, Is.EqualTo(pageEventDataModel.X));
            Assert.That(result.Y, Is.EqualTo(pageEventDataModel.Y));
            Assert.That(result.ClientDateTime, Is.EqualTo(pageEventDataModel.ClientDateTime));
            Assert.That(result.DateTime, Is.EqualTo(pageEventDataModel.DateTime));
        }
    }
}