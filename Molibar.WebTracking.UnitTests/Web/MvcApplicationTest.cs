using System.Linq;
using System.Web.Routing;
using Molibar.Infrastructure.Logging;
using Molibar.Infrastructure.Logging.ForTesting;
using Molibar.WebTracking.Web;
using NUnit.Framework;

namespace Molibar.WebTracking.UnitTests.Web
{
    [TestFixture]
    class MvcApplicationTest : MvcApplication
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void ShouldInitializeContainer()
        {
            // Arrange

            // Act
            this.InitializeContainer();

            // Assert
        }

        [Test]
        public void ShouldRegisterRoutes()
        {
            // Arrange
            var routes = new RouteCollection();

            // Act
            RegisterRoutes(routes);

            // Assert
            Assert.That(routes.Count, Is.EqualTo(2));
        }
    }
}
