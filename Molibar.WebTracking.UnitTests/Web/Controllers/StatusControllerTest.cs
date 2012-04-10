using Molibar.WebTracking.Web.Controllers;
using NUnit.Framework;

namespace Molibar.WebTracking.UnitTests.Web.Controllers
{
    [TestFixture]
    class StatusControllerTest
    {
        private StatusController _statusController;

        [SetUp]
        public void SetUp()
        {
            _statusController = new StatusController();
        }

        [Test]
        public void ShouldReturnActionResult()
        {
            // Arrange

            // Act
            var actionResult = _statusController.Index();

            // Assert
            Assert.That(actionResult, Is.Not.Null);
        }
    }
}