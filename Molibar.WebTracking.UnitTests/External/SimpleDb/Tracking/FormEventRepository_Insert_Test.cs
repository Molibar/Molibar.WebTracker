using System.Collections.Generic;
using Amazon.SimpleDB.Model;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.External.SimpleDb.Tracking;
using NUnit.Framework;
using Rhino.Mocks;

namespace Molibar.WebTracking.UnitTests.External.SimpleDb.Tracking
{
    [TestFixture]
    internal class FormEventRepository_Insert_Test : FormEventRepositoryTest
    {
        [Test]
        public void ShouldCallInsertForTheReplaceableAttributesSentIn()
        {
            // Arrange
            var formEvent = new FormEvent();
            var replaceableAttributes = new List<ReplaceableAttribute>();
            _entityMapper.Expect(x => x.Map<List<ReplaceableAttribute>>(formEvent)).Return(replaceableAttributes);
            _simpleDbProxy.Expect(x=> x.Put(FormEventRepository.DOMAIN_NAME, "", replaceableAttributes)).IgnoreArguments();

            _mockRepository.ReplayAll();

            // Act
            _formEventRepository.Insert(formEvent);

            // Assert
            _mockRepository.VerifyAll();
        }
    }
}