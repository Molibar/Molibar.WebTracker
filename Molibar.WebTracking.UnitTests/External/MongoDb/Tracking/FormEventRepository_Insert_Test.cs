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
        public void ShouldCallInsertForTheBsonDocumentSentIn()
        {
            // Arrange
            var formEvent = new FormEvent();
            var bsonDocument = new BsonDocument();
            _entityMapper.Expect(x => x.Map<BsonDocument>(formEvent)).Return(bsonDocument);
            _mongoDbProxy.Expect(x => x.Insert(FormEventRepository.COLLECTION_NAME, new[] { bsonDocument }));

            _mockRepository.ReplayAll();

            // Act
            _formEventRepository.Insert(formEvent);

            // Assert
            _mockRepository.VerifyAll();
        }

        [Test]
        public void ShouldCallInsertForTheEnumerationOfBsonDocumentSentIn()
        {
            // Arrange
            var formEvents = new List<FormEvent>(new [] { new FormEvent() });
            var bsonDocuments = new[] { new BsonDocument() };
            _entityMapper.Expect(x => x.Map<IEnumerable<BsonDocument>>(formEvents)).Return(bsonDocuments);
            _mongoDbProxy.Expect(x => x.Insert(FormEventRepository.COLLECTION_NAME, bsonDocuments));

            _mockRepository.ReplayAll();

            // Act
            _formEventRepository.Insert(formEvents);

            // Assert
            _mockRepository.VerifyAll();
        }
    }
}