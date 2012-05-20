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
        public void ShouldCallInsertForThePageEventSentIn()
        {
            // Arrange
            var pageEvent = new PageEvent();
            _mongoDbProxy.Expect(x => x.Insert(PageEventRepository.COLLECTION_NAME, new[] { pageEvent }));

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
            _mongoDbProxy.Expect(x => x.Insert(PageEventRepository.COLLECTION_NAME, pageEvents));

            _mockRepository.ReplayAll();

            // Act
            _pageEventRepository.Insert(pageEvents);

            // Assert
            _mockRepository.VerifyAll();
        }
    }
}