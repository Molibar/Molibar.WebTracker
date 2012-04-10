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
    internal class PageEventRepositort_Insert_Test : PageEventRepositoryTest
    {
        [Test]
        public void ShouldCallInsertForTheBsonDocumentSentIn()
        {
            // Arrange
            var pageEvent = new PageEvent();
            var bsonDocument = new BsonDocument();
            _entityMapper.Expect(x => x.Map<BsonDocument>(pageEvent)).Return(bsonDocument);
            _mongoDbProxy.Expect(x => x.Insert(PageEventRepository.COLLECTION_NAME, new[] { bsonDocument }));

            _mockRepository.ReplayAll();

            // Act
            _pageEventRepository.Insert(pageEvent);

            // Assert
            _mockRepository.VerifyAll();
        }

        [Test]
        public void ShouldCallInsertForTheEnumerationOfBsonDocumentSentIn()
        {
            // Arrange
            var pageEvents = new List<PageEvent>(new [] { new PageEvent() });
            var bsonDocuments = new[] { new BsonDocument() };
            _entityMapper.Expect(x => x.Map<IEnumerable<BsonDocument>>(pageEvents)).Return(bsonDocuments);
            _mongoDbProxy.Expect(x => x.Insert(PageEventRepository.COLLECTION_NAME, bsonDocuments));

            _mockRepository.ReplayAll();

            // Act
            _pageEventRepository.Insert(pageEvents);

            // Assert
            _mockRepository.VerifyAll();
        }
    }
}