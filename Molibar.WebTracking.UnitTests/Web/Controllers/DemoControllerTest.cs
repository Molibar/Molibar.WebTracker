using System;
using Molibar.WebTracking.Web.Controllers;
using Molibar.WebTracking.Web.Models.Demo;
using NUnit.Framework;

namespace Molibar.WebTracking.UnitTests.Web.Controllers
{
    [TestFixture]
    class DemoControllerTest
    {
        private DemoController _demoController;

        [SetUp]
        public void SetUp()
        {
            _demoController = new DemoController();
        }

        [Test]
        public void ShouldReturnActionResult_Index()
        {
            // Arrange

            // Act
            var actionResult = _demoController.Index();

            // Assert
            Assert.That(actionResult, Is.Not.Null);
        }

        [Test]
        public void ShouldReturnActionResult_For_Submit()
        {
            // Arrange
            var demoPostModel = new DemoPostModel
                                    {
                                        VisitGuid = Guid.NewGuid(),
                                        Selected = "Maybe",
                                        StringValue = "Fluff",
                                        Checkbox = true
                                    };

            // Act
            var actionResult = _demoController.Submit(demoPostModel);

            // Assert
            Assert.That(actionResult, Is.Not.Null);
        }
    }
}
