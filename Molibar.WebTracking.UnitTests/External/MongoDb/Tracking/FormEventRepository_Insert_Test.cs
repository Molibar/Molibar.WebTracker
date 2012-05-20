using System.Collections.Generic;
using System.Linq;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.External.Models;
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
            var formEventDataModel = new FormEventDataModel();
            _entityMapper.Expect(x => x.Map<FormEventDataModel>(formEvent)).Return(formEventDataModel);
            _mongoDbProxy.Expect(x => x.Insert(FormEventRepository.COLLECTION_NAME, new[] { formEventDataModel }));

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
            var formEvents = new List<FormEvent>(new[] { new FormEvent() });
            var formEventDataModels = new List<FormEventDataModel>(new[] { new FormEventDataModel() });
            _entityMapper.Expect(x => x.Map<IEnumerable<FormEventDataModel>>(formEvents)).Return(formEventDataModels);
            _mongoDbProxy.Expect(x => x.Insert(FormEventRepository.COLLECTION_NAME, formEventDataModels));

            _mockRepository.ReplayAll();

            // Act
            _formEventRepository.Insert(formEvents);

            // Assert
            _mockRepository.VerifyAll();
        }
    }
}