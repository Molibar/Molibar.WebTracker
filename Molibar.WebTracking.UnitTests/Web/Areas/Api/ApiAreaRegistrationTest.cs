using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Molibar.WebTracking.Web.Areas.Api;
using NUnit.Framework;

namespace Molibar.WebTracking.UnitTests.Web.Areas.Api
{
    [TestFixture]
    class ApiAreaRegistrationTest
    {
        private ApiAreaRegistration _apiAreaRegistration;
        private RouteCollection _routeCollection;
        private AreaRegistrationContext _areaRegistrationContext;

        [SetUp]
        public void SetUp()
        {
            _routeCollection = new RouteCollection();
            _apiAreaRegistration = new ApiAreaRegistration();
            _areaRegistrationContext = new AreaRegistrationContext(_apiAreaRegistration.AreaName, _routeCollection);
        }

        [Test]
        public void Test()
        {
            // Arrange

            // Act
            _apiAreaRegistration.RegisterArea(_areaRegistrationContext);

            // Assert
            Assert.That(_areaRegistrationContext.Routes.Count, Is.EqualTo(2));
        }
    }
}
