using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.Domain.Repositories;
using Molibar.WebTracking.Domain.Tracking;
using NUnit.Framework;
using Rhino.Mocks;

namespace Molibar.WebTracking.UnitTests.Domain.Tracking
{
    [TestFixture]
    class FormEventTrackingServiceTest
    {
        private IFormEventRepository _formEventRepository;
        private FormEventTrackingService _formEventTrackingService;
        private MockRepository _mockRepository;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository();
            _formEventRepository = _mockRepository.StrictMock<IFormEventRepository>();
            _formEventTrackingService = new FormEventTrackingService(_formEventRepository);
        }

        [Test]
        public void ShouldCallAddOnTheFormEventRepository()
        {
            // Arrange
            var formEvent = new FormEvent();
            _formEventRepository.Expect(x => x.Insert(formEvent));
            _formEventRepository.Replay();

            // Act
            var result = _formEventTrackingService.Add(formEvent);

            // Assert
            Assert.That(result, Is.EqualTo(formEvent));
            _formEventRepository.VerifyAllExpectations();
        }
    }
}
