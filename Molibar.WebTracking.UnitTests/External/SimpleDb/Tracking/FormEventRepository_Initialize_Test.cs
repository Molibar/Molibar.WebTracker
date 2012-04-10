using Molibar.WebTracking.External.SimpleDb.Tracking;
using NUnit.Framework;
using Rhino.Mocks;

namespace Molibar.WebTracking.UnitTests.External.SimpleDb.Tracking
{
    [TestFixture]
    internal class FormEventRepository_Initialize_Test : FormEventRepositoryTest
    {
        [Test]
        public void ShouldCreateCollectionOnInitializeWhen_CollectionExists_Returns_False()
        {
            // Arrange
            _simpleDbProxy.Expect(x => x.DomainExists(FormEventRepository.DOMAIN_NAME)).Return(false);
            _simpleDbProxy.Expect(x => x.CreateDomain(FormEventRepository.DOMAIN_NAME));

            _simpleDbProxy.Replay();

            // Act
            _formEventRepository.Initialize();

            // Assert
            _simpleDbProxy.VerifyAllExpectations();
        }

        [Test]
        public void ShouldNotCreateCollectionOnInitializeWhen_CollectionExists_Returns_True()
        {
            // Arrange
            _simpleDbProxy.Expect(x => x.DomainExists(FormEventRepository.DOMAIN_NAME)).Return(true);
            _simpleDbProxy.Expect(x => x.CreateDomain(FormEventRepository.DOMAIN_NAME)).Repeat.Never();

            _simpleDbProxy.Replay();

            // Act
            _formEventRepository.Initialize();

            // Assert
            _simpleDbProxy.VerifyAllExpectations();
        }
    }
}