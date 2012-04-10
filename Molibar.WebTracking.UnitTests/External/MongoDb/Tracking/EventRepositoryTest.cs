using Molibar.Infrastructure.Mapper;
using Molibar.WebTracking.External.MongoDb;
using NUnit.Framework;
using Rhino.Mocks;

namespace Molibar.WebTracking.UnitTests.External.MongoDb.Tracking
{
    [TestFixture]
    public class EventRepositoryTest
    {
        protected IMongoDbProxy _mongoDbProxy;
        protected IEntityMapper _entityMapper;
        protected MockRepository _mockRepository;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository();
            _mongoDbProxy = _mockRepository.StrictMock<IMongoDbProxy>();
            _entityMapper = _mockRepository.StrictMock<IEntityMapper>();
        }
    }
}