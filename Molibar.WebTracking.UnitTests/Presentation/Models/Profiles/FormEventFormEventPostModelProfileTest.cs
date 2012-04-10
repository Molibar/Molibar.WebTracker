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
    class FormEventFormEventPostModelProfileTest
    {
        private EntityMapper _entityMapper;

        [SetUp]
        public void Setup()
        {
            Mapper.AddProfile(new FormEventFormEventPostModelProfile());
            _entityMapper = new EntityMapper(Mapper.Engine);
        }

        [Test]
        public void AssertConfigurationIsValid()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [Test]
        public void ShouldCorrectlyMap_FormEvent_To_FormEventPostModel()
        {
            // Arrange
            var formEvent = Builder<FormEvent>.CreateNew().Build();

            // Act
            var result = _entityMapper.Map<FormEventPostModel>(formEvent);

            // Assert
            Assert.That(result.VisitGuid, Is.EqualTo(formEvent.VisitGuid));
            Assert.That(result.Url, Is.EqualTo(formEvent.Url));
            Assert.That(result.PageId, Is.EqualTo(formEvent.PageId));
            Assert.That(result.ElementId, Is.EqualTo(formEvent.ElementId));
            Assert.That(result.EventType, Is.EqualTo(formEvent.EventType));
            Assert.That(result.ElementType, Is.EqualTo(formEvent.ElementType));
            Assert.That(result.ElementValue, Is.EqualTo(formEvent.ElementValue));
            Assert.That(result.ValueValid, Is.EqualTo(formEvent.ValueValid));
            Assert.That(result.MillisSince1970, Is.EqualTo(EventEventPostModelProfile.DateToJavascriptTicks(formEvent.ClientDateTime)));
        }

        [Test]
        public void ShouldCorrectlyMap_FormEventPostModel_To_FormEvent()
        {
            // Arrange
            var formEvent = Builder<FormEventPostModel>.CreateNew()
                .With(x=> x.MillisSince1970 = 3600L*24L*365L*1000L).Build();

            // Act
            var result = _entityMapper.Map<FormEvent>(formEvent);

            // Assert
            Assert.That(result.VisitGuid, Is.EqualTo(formEvent.VisitGuid));
            Assert.That(result.Url, Is.EqualTo(formEvent.Url));
            Assert.That(result.PageId, Is.EqualTo(formEvent.PageId));
            Assert.That(result.ElementId, Is.EqualTo(formEvent.ElementId));
            Assert.That(result.EventType, Is.EqualTo(formEvent.EventType));
            Assert.That(result.ElementType, Is.EqualTo(formEvent.ElementType));
            Assert.That(result.ElementValue, Is.EqualTo(formEvent.ElementValue));
            Assert.That(result.ValueValid, Is.EqualTo(formEvent.ValueValid));
            Assert.That(result.ClientDateTime, Is.EqualTo(EventEventPostModelProfile.JavascriptTicksToDate(formEvent.MillisSince1970)));
            Assert.That(result.DateTime, Is.GreaterThan(DateTime.UtcNow.Subtract(new TimeSpan(1, 0, 0, 0, 0))));
        }
    }
}
