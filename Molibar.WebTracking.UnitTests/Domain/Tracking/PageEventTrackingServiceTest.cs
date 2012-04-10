using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.Domain.Repositories;
using Molibar.WebTracking.Domain.Tracking;
using NUnit.Framework;
using Rhino.Mocks;

namespace Molibar.WebTracking.UnitTests.Domain.Tracking
{
    [TestFixture]
    class PageEventTrackingServiceTest
    {
        private IPageEventRepository _pageEventRepository;
        private PageEventTrackingService _pageEventTrackingService;
        private MockRepository _mockRepository;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository();
            _pageEventRepository = _mockRepository.StrictMock<IPageEventRepository>();
            _pageEventTrackingService = new PageEventTrackingService(_pageEventRepository);
        }

        [Test]
        public void ShouldCallAddOnThePageEventRepository()
        {
            // Arrange
            var pageEvent = new PageEvent();
            _pageEventRepository.Expect(x => x.Insert(pageEvent));
            _pageEventRepository.Replay();

            // Act
            var result = _pageEventTrackingService.Add(pageEvent);

            // Assert
            Assert.That(result, Is.EqualTo(pageEvent));
            _pageEventRepository.VerifyAllExpectations();
        }
    }
}