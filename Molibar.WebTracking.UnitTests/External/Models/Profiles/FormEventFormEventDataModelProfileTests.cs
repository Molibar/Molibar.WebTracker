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
    public class FormEventFormEventDataModelProfileTests
    {
        private EntityMapper _entityMapper;

        [SetUp]
        public void Setup()
        {
            Mapper.AddProfile(new FormEventFormEventDobProfile());
            _entityMapper = new EntityMapper(Mapper.Engine);
        }

        [Test]
        public void AssertConfigurationIsValid()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [Test]
        public void ShouldMapFormEventToFormEventDob()
        {
            // Arrange
            var formEvent = Builder<FormEvent>.CreateNew()
                .With(x => x.Id = ObjectId.GenerateNewId().ToString())
                .With(x => x.ClientDateTime = new DateTime(2012, 1, 1, 12, 13, 14))
                .With(x => x.DateTime = new DateTime(2012, 1, 1, 12, 13, 15)).Build();

            // Act
            var result = _entityMapper.Map<FormEventDataModel>(formEvent);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(ObjectId.Parse(formEvent.Id)));
            Assert.That(result.VisitGuid, Is.EqualTo(formEvent.VisitGuid));
            Assert.That(result.Url, Is.EqualTo(formEvent.Url));
            Assert.That(result.PageId, Is.EqualTo(formEvent.PageId));
            Assert.That(result.ElementId, Is.EqualTo(formEvent.ElementId));
            Assert.That(result.EventType, Is.EqualTo(formEvent.EventType));
            Assert.That(result.ElementType, Is.EqualTo(formEvent.ElementType));
            Assert.That(result.ElementValue, Is.EqualTo(formEvent.ElementValue));
            Assert.That(result.ValueValid, Is.EqualTo(formEvent.ValueValid));
            Assert.That(result.ClientDateTime, Is.EqualTo(formEvent.ClientDateTime));
            Assert.That(result.DateTime, Is.EqualTo(formEvent.DateTime));
        }

        [Test]
        public void ShouldMapForEventDataModelToFormEvent()
        {
            // Arrange
            var clientDateTime = new DateTime(1234, 5, 6, 17, 18, 1, 90);
            var dateTime = new DateTime(1234, 5, 6, 17, 18, 19, 20);
            var formEventDataModel = Builder<FormEventDataModel>.CreateNew()
                .With(x => x.Id = ObjectId.GenerateNewId())
                .With(x => x.ClientDateTime = clientDateTime)
                .With(x => x.DateTime = dateTime).Build();


            // Act
            var result = _entityMapper.Map<FormEvent>(formEventDataModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(formEventDataModel.Id.ToString()));
            Assert.That(result.VisitGuid, Is.EqualTo(formEventDataModel.VisitGuid));
            Assert.That(result.Url, Is.EqualTo(formEventDataModel.Url));
            Assert.That(result.PageId, Is.EqualTo(formEventDataModel.PageId));
            Assert.That(result.ElementId, Is.EqualTo(formEventDataModel.ElementId));
            Assert.That(result.EventType, Is.EqualTo(formEventDataModel.EventType));
            Assert.That(result.ElementType, Is.EqualTo(formEventDataModel.ElementType));
            Assert.That(result.ElementValue, Is.EqualTo(formEventDataModel.ElementValue));
            Assert.That(result.ValueValid, Is.EqualTo(formEventDataModel.ValueValid));
            Assert.That(result.ClientDateTime, Is.EqualTo(formEventDataModel.ClientDateTime));
            Assert.That(result.DateTime, Is.EqualTo(formEventDataModel.DateTime));
        }
    }
}