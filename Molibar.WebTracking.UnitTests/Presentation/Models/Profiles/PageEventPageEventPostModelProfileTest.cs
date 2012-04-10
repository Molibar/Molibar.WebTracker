using System;
using AutoMapper;
using FizzWare.NBuilder;
using Molibar.Infrastructure.Mapper.AutoMapper;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.Presentation.Models;
using Molibar.WebTracking.Presentation.Models.Profiles;
using NUnit.Framework;

namespace Molibar.WebTracking.UnitTests.Presentation.Models.Profiles
{
    [TestFixture]
    class PageEventPageEventPostModelProfileTest
    {
        private EntityMapper _entityMapper;

        [SetUp]
        public void Setup()
        {
            Mapper.AddProfile(new PageEventPageEventPostModelProfile());
            _entityMapper = new EntityMapper(Mapper.Engine);
        }

        [Test]
        public void AssertConfigurationIsValid()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [Test]
        public void ShouldCorrectlyMap_PageEvent_To_PageEventPostModel()
        {
            // Arrange
            var pageEvent = Builder<PageEvent>.CreateNew().Build();

            // Act
            var result = _entityMapper.Map<PageEventPostModel>(pageEvent);

            // Assert
            Assert.That(result.VisitGuid, Is.EqualTo(pageEvent.VisitGuid));
            Assert.That(result.Url, Is.EqualTo(pageEvent.Url));
            Assert.That(result.PageId, Is.EqualTo(pageEvent.PageId));
            Assert.That(result.ElementId, Is.EqualTo(pageEvent.ElementId));
            Assert.That(result.EventType, Is.EqualTo(pageEvent.EventType));
            Assert.That(result.ElementType, Is.EqualTo(pageEvent.ElementType));
            Assert.That(result.X, Is.EqualTo(pageEvent.X));
            Assert.That(result.Y, Is.EqualTo(pageEvent.Y));
            Assert.That(result.MillisSince1970, Is.EqualTo(EventEventPostModelProfile.DateToJavascriptTicks(pageEvent.ClientDateTime)));
        }

        [Test]
        public void ShouldCorrectlyMap_PageEventPostModel_To_PageEvent()
        {
            // Arrange
            var pageEvent = Builder<PageEventPostModel>.CreateNew()
                .With(x => x.MillisSince1970 = 3600L * 24L * 365L * 1000L).Build();

            // Act
            var result = _entityMapper.Map<PageEvent>(pageEvent);

            // Assert
            Assert.That(result.VisitGuid, Is.EqualTo(pageEvent.VisitGuid));
            Assert.That(result.Url, Is.EqualTo(pageEvent.Url));
            Assert.That(result.PageId, Is.EqualTo(pageEvent.PageId));
            Assert.That(result.ElementId, Is.EqualTo(pageEvent.ElementId));
            Assert.That(result.EventType, Is.EqualTo(pageEvent.EventType));
            Assert.That(result.ElementType, Is.EqualTo(pageEvent.ElementType));
            Assert.That(result.X, Is.EqualTo(pageEvent.X));
            Assert.That(result.Y, Is.EqualTo(pageEvent.Y));
            Assert.That(result.ClientDateTime, Is.EqualTo(EventEventPostModelProfile.JavascriptTicksToDate(pageEvent.MillisSince1970)));
            Assert.That(result.DateTime, Is.GreaterThan(DateTime.UtcNow.Subtract(new TimeSpan(1, 0, 0, 0, 0))));
        }
    }
}