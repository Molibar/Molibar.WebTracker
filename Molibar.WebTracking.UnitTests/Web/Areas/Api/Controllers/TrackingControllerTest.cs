using Molibar.Infrastructure.Mapper;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.Domain.Tracking;
using Molibar.WebTracking.Presentation.Models;
using Molibar.WebTracking.Web.Areas.Api.Controllers;
using NUnit.Framework;
using Rhino.Mocks;

namespace Molibar.WebTracking.UnitTests.Web.Areas.Api.Controllers
{
    [TestFixture]
    class TrackingControllerTest
    {
        private TrackingController _trackingController;
        private IFormEventTrackingService _formEventTrackingService;
        private IEntityMapper _entityMapper;
        private MockRepository _mockRepository;
        private IPageEventTrackingService _pageEventTrackingService;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository();

            _formEventTrackingService = _mockRepository.StrictMock<IFormEventTrackingService>();
            _pageEventTrackingService = _mockRepository.StrictMock<IPageEventTrackingService>();
            _entityMapper = _mockRepository.StrictMock<IEntityMapper>();
            _trackingController = new TrackingController(_formEventTrackingService, _pageEventTrackingService, _entityMapper);
        }

        [Test]
        public void ShouldReturnJsonResult_FormEvent()
        {
            // Arrange
            var formEventPostModel = new FormEventPostModel();
            var formEvent = new FormEvent();

            _entityMapper.Expect(x => x.Map<FormEvent>(formEventPostModel)).Return(formEvent);
            _formEventTrackingService.Expect(x => x.Add(formEvent));

            _mockRepository.ReplayAll();

            // Act
            var jsonResult = _trackingController.FormEvent(formEventPostModel);

            // Assert
            Assert.That(jsonResult, Is.Not.Null);
            _mockRepository.VerifyAll();
        }

        [Test]
        public void ShouldReturnJsonResult_PageEvent()
        {
            // Arrange
            var pageEventPostModel = new PageEventPostModel();
            var pageEvent = new PageEvent();

            _entityMapper.Expect(x => x.Map<PageEvent>(pageEventPostModel)).Return(pageEvent);
            _pageEventTrackingService.Expect(x => x.Add(pageEvent));

            _mockRepository.ReplayAll();

            // Act
            var jsonResult = _trackingController.PageEvent(pageEventPostModel);

            // Assert
            Assert.That(jsonResult, Is.Not.Null);
            _mockRepository.VerifyAll();
        }
    }
}
