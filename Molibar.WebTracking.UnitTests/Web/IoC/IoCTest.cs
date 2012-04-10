using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Molibar.WebTracking.UnitTests.Web.IoC
{
    [TestFixture]
    class IoCTest
    {
        [Test]
        public void Test()
        {
            WebTracking.Web.IoC.IoC.Initialize();
        }
    }
}
