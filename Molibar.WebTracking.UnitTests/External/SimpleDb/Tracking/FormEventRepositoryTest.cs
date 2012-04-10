using Molibar.Infrastructure.Mapper;
using Molibar.WebTracking.External.SimpleDb.Tracking;
using Molibar.WebTracking.External.SimpleDb;
using NUnit.Framework;
using Rhino.Mocks;

namespace Molibar.WebTracking.UnitTests.External.SimpleDb.Tracking
{
    [TestFixture]
    internal class FormEventRepositoryTest
    {
        protected ISimpleDbProxy _simpleDbProxy;
        protected IEntityMapper _entityMapper;
        protected MockRepository _mockRepository;
        protected FormEventRepository _formEventRepository;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository();
            _simpleDbProxy = _mockRepository.StrictMock<ISimpleDbProxy>();
            _entityMapper = _mockRepository.StrictMock<IEntityMapper>();
            _formEventRepository = new FormEventRepository(_simpleDbProxy, _entityMapper);
        }
    }
}
