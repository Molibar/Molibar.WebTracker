using System.Collections.Generic;
using System.Linq;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.External.MongoDb.Tracking;
using MongoDB.Bson;
using NUnit.Framework;
using Rhino.Mocks;

namespace Molibar.WebTracking.UnitTests.External.MongoDb.Tracking
{
    [TestFixture]
    internal class FormEventRepository_Insert_Test : FormEventRepositoryTest
    {
        [Test]
        public void ShouldCallInsertForTheFormEventSentIn()
        {
            // Arrange
            var formEvent = new FormEvent();
            _mongoDbProxy.Expect(x => x.Insert(FormEventRepository.COLLECTION_NAME, new[] { formEvent }));

            _mockRepository.ReplayAll();

            // Act
            _formEventRepository.Insert(formEvent);

            // Assert
            _mockRepository.VerifyAll();
        }

        [Test]
        public void ShouldCallInsertForTheEnumerationOfFormEventsSentIn()
        {
            // Arrange
            var formEvents = new List<FormEvent>(new [] { new FormEvent() });
            _mongoDbProxy.Expect(x => x.Insert(FormEventRepository.COLLECTION_NAME, formEvents));

            _mockRepository.ReplayAll();

            // Act
            _formEventRepository.Insert(formEvents);

            // Assert
            _mockRepository.VerifyAll();
        }
    }
}