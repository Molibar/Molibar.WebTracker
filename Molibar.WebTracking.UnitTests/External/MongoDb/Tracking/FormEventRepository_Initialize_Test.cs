using Molibar.WebTracking.External.MongoDb.Tracking;
using NUnit.Framework;
using Rhino.Mocks;

namespace Molibar.WebTracking.UnitTests.External.MongoDb.Tracking
{
    [TestFixture]
    internal class FormEventRepository_Initialize_Test : FormEventRepositoryTest
    {
        [Test]
        public void ShouldCreateCollectionOnInitializeWhen_CollectionExists_Returns_False()
        {
            // Arrange
            _mongoDbProxy.Expect(x => x.CollectionExists(FormEventRepository.COLLECTION_NAME)).Return(false);
            _mongoDbProxy.Expect(x => x.CreateCollection(FormEventRepository.COLLECTION_NAME));

            _mongoDbProxy.Replay();

            // Act
            _formEventRepository.Initialize();

            // Assert
            _mongoDbProxy.VerifyAllExpectations();
        }

        [Test]
        public void ShouldNotCreateCollectionOnInitializeWhen_CollectionExists_Returns_True()
        {
            // Arrange
            _mongoDbProxy.Expect(x => x.CollectionExists(FormEventRepository.COLLECTION_NAME)).Return(true);
            _mongoDbProxy.Expect(x => x.CreateCollection(FormEventRepository.COLLECTION_NAME)).Repeat.Never();

            _mongoDbProxy.Replay();

            // Act
            _formEventRepository.Initialize();

            // Assert
            _mongoDbProxy.VerifyAllExpectations();
        }
    }
}