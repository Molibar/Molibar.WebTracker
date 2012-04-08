using System;
using Molibar.WebTracking.External.SimpleDb;
using NUnit.Framework;

namespace Molibar.WebTracking.UnitTests.External.SimpleDb
{
    [TestFixture]
    class SimpleDbProxyTest
    {

        [Test]
        public void VerifyingExpectedDateConversion()
        {
            // Arrange
            var expected = "1234-05-06T17:18:19";
            var dateTime = new DateTime(1234, 5, 6, 17, 18, 19, 20);

            // Act
            var result = dateTime.ToString(SimpleDbProxy.DATE_FORMAT_STRING);


            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
