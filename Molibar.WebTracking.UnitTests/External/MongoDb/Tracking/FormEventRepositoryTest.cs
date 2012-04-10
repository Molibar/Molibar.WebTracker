using Molibar.WebTracking.External.MongoDb.Tracking;
using NUnit.Framework;

namespace Molibar.WebTracking.UnitTests.External.MongoDb.Tracking
{
    [TestFixture]
    internal class FormEventRepositoryTest : EventRepositoryTest
    {
        protected FormEventRepository _formEventRepository;

        [SetUp]
        public void SetUp()
        {
            _formEventRepository = new FormEventRepository(_mongoDbProxy, _entityMapper);
        }
    }
}
