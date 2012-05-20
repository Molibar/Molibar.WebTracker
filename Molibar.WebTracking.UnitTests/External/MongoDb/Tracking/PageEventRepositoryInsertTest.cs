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
    internal class PageEventRepositort_Insert_Test : PageEventRepositoryTest
    {
        [Test]
        public void ShouldCallInsertForThePageEventSentIn()
        {
            // Arrange
            var pageEvent = new PageEvent();
            var pageEventDataModel = new PageEventDataModel();
            _entityMapper.Expect(x => x.Map<PageEventDataModel>(pageEvent)).Return(pageEventDataModel);
            _mongoDbProxy.Expect(x => x.Insert(PageEventRepository.COLLECTION_NAME, new[] { pageEventDataModel }));

            _mockRepository.ReplayAll();

            // Act
            _pageEventRepository.Insert(pageEvent);

            // Assert
            _mockRepository.VerifyAll();
        }

        [Test]
        public void ShouldCallInsertForTheEnumerationOfPageEventsSentIn()
        {
            // Arrange
            var pageEvents = new List<PageEvent>(new[] { new PageEvent() });
            var pageEventDataModels = new List<PageEventDataModel>(new[] { new PageEventDataModel() });
            _entityMapper.Expect(x => x.Map<IEnumerable<PageEventDataModel>>(pageEvents)).Return(pageEventDataModels);
            _mongoDbProxy.Expect(x => x.Insert(PageEventRepository.COLLECTION_NAME, pageEventDataModels));

            _mockRepository.ReplayAll();

            // Act
            _pageEventRepository.Insert(pageEvents);

            // Assert
            _mockRepository.VerifyAll();
        }
    }
}