using Molibar.WebTracking.External.MongoDb.Tracking;
using NUnit.Framework;

namespace Molibar.WebTracking.UnitTests.External.MongoDb.Tracking
{
    [TestFixture]
    internal class PageEventRepositoryTest : EventRepositoryTest
    {
        protected PageEventRepository _pageEventRepository;

        [SetUp]
        public void SetUp()
        {
            base.SetUp();
            _pageEventRepository = new PageEventRepository(_mongoDbProxy, _entityMapper);
        }
    }
}