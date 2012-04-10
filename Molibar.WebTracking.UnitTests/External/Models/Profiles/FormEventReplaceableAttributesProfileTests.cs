using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Amazon.SimpleDB.Model;
using AutoMapper;
using FizzWare.NBuilder;
using Molibar.Infrastructure.Mapper.AutoMapper;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.External.Models.Profiles;
using Molibar.WebTracking.External.SimpleDb;
using NUnit.Framework;

namespace Molibar.WebTracking.UnitTests.External.Models.Profiles
{
    [TestFixture]
    public class FormEventReplaceableAttributesProfileTests
    {
        private EntityMapper _entityMapper;

        [SetUp]
        public void Setup()
        {
            Mapper.AddProfile(new FormEventReplaceableAttributesProfile());
            _entityMapper = new EntityMapper(Mapper.Engine);
        }

        [Test]
        public void AssertConfigurationIsValid()
        {
            Mapper.AssertConfigurationIsValid();
        }


        [Test]
        public void ShouldMapFormEventToReplaceAttributes()
        {
            // Arrange
            var formEvent = Builder<FormEvent>.CreateNew()
                .With(x => x.ClientDateTime = DateTime.Now).Build();
            var s = formEvent.ClientDateTime.ToString(SimpleDbProxy.DATE_FORMAT_STRING);
            // Act
            var result = _entityMapper.Map<List<ReplaceableAttribute>>(formEvent);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(GetReplaceableAttributeValue(result, "Id"), Is.EqualTo(formEvent.Id));
            Assert.That(GetReplaceableAttributeValue(result, "VisitGuid"), Is.EqualTo(formEvent.VisitGuid.ToString()));
            Assert.That(GetReplaceableAttributeValue(result, "Url"), Is.EqualTo(formEvent.Url));
            Assert.That(GetReplaceableAttributeValue(result, "PageId"), Is.EqualTo(formEvent.PageId));
            Assert.That(GetReplaceableAttributeValue(result, "EventType"), Is.EqualTo(formEvent.EventType));
            Assert.That(GetReplaceableAttributeValue(result, "ElementId"), Is.EqualTo(formEvent.ElementId));
            Assert.That(GetReplaceableAttributeValue(result, "ElementValue"), Is.EqualTo(formEvent.ElementValue));
            Assert.That(GetReplaceableAttributeValue(result, "ValueValid"), Is.EqualTo(formEvent.ValueValid.ToString(CultureInfo.InvariantCulture)));
            Assert.That(GetReplaceableAttributeValue(result, "ClientDateTime"), Is.EqualTo(formEvent.ClientDateTime.ToString(SimpleDbProxy.DATE_FORMAT_STRING)));
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
            replaceableAttributes.Add(new ReplaceableAttribute { Name = "PageId", Value = "PageId" });
            replaceableAttributes.Add(new ReplaceableAttribute { Name = "EventType", Value = "EventType" });
            replaceableAttributes.Add(new ReplaceableAttribute { Name = "ElementId", Value = "ElementId" });
            replaceableAttributes.Add(new ReplaceableAttribute { Name = "ElementValue", Value = "ElementValue" });
            replaceableAttributes.Add(new ReplaceableAttribute { Name = "ValueValid", Value = true.ToString(CultureInfo.InvariantCulture) });
            replaceableAttributes.Add(new ReplaceableAttribute { Name = "ClientDateTime", Value = DateTime.Now.Subtract(new TimeSpan(1234)).ToString(SimpleDbProxy.DATE_FORMAT_STRING) });
            replaceableAttributes.Add(new ReplaceableAttribute{Name = "DateTime", Value = DateTime.Now.ToString(SimpleDbProxy.DATE_FORMAT_STRING)});

            // Act
            var result = _entityMapper.Map<FormEvent>(replaceableAttributes);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "Id")));
            Assert.That(result.VisitGuid.ToString(), Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "VisitGuid")));
            Assert.That(result.Url, Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "Url")));
            Assert.That(result.PageId, Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "PageId")));
            Assert.That(result.EventType, Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "EventType")));
            Assert.That(result.ElementId, Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "ElementId")));
            Assert.That(result.ElementValue, Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "ElementValue")));
            Assert.That(result.ValueValid.ToString(), Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "ValueValid")));
            Assert.That(result.ClientDateTime.ToString(SimpleDbProxy.DATE_FORMAT_STRING), Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "ClientDateTime")));
            Assert.That(result.DateTime.ToString(SimpleDbProxy.DATE_FORMAT_STRING), Is.EqualTo(GetReplaceableAttributeValue(replaceableAttributes, "DateTime")));
        }


        [Test]
        public void ShouldReturnNullStringWhenEnsureValueIsCalledWithNull()
        {
            // Arrange
            var expected = "<null>";
            string value = null;

            // Act
            var result = FormEventReplaceableAttributesProfile.EnsureValue(value);

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
            var result = FormEventReplaceableAttributesProfile.EnsurePaddedIntValue(value);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }


        [Test]
        public void ShouldReturnPaddedStringWhenEnsurePaddedIntValueIsCalledWithValue()
        {
            // Arrange
            var expected = "0000006";
            int? value = 6;

            // Act
            var result = FormEventReplaceableAttributesProfile.EnsurePaddedIntValue(value);

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
