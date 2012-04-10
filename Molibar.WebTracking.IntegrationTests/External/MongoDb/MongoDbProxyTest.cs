using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FizzWare.NBuilder;
using Molibar.Infrastructure.Mapper.AutoMapper;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.External.Models.Profiles;
using Molibar.WebTracking.External.MongoDb;
using MongoDB.Bson;
using NUnit.Framework;

namespace Molibar.WebTracking.IntegrationTests.External.MongoDb
{
    [TestFixture]
    public class MongoDbProxyTest
    {
        private MongoDbProxy _mongoDbProxy;
        private string _collectionName;
        private EntityMapper _entityMapper;

        [SetUp]
        public void SetUp()
        {
            Mapper.AddProfile(new FormEventBsonDocumentProfile());
            _entityMapper = new EntityMapper(Mapper.Engine);

            _mongoDbProxy = new MongoDbProxy();
            _collectionName = "DefaultTestCollection";
            if (!_mongoDbProxy.CollectionExists(_collectionName))
                _mongoDbProxy.CreateCollection(_collectionName);
        }

        [TearDown]
        public void TearDown()
        {
            _mongoDbProxy.RemoveCollection(_collectionName);
        }

        [Test]
        public void ShouldAddAndRemove_1000_Elements()
        {
            // Arrange
            var expectedCount = 123;
            var formEvents = Builder<FormEvent>.CreateListOfSize(expectedCount)
                .All().With(x=> x.Id = BsonObjectId.GenerateNewId().ToString())
                .Build();
            var bsonDocuments = _entityMapper.Map<IList<BsonDocument>>(formEvents);

            // Act
            _mongoDbProxy.Insert(_collectionName, bsonDocuments);
            var all = _mongoDbProxy.FindAll(_collectionName);
            var countAfterInsertion = all.Count();
            _mongoDbProxy.RemoveDocuments(_collectionName);
            all = _mongoDbProxy.FindAll(_collectionName);
            var countAfterDeletion = all.Count();

            // Assert
            Assert.That(countAfterInsertion, Is.EqualTo(expectedCount));
            Assert.That(countAfterDeletion, Is.EqualTo(0));
        }

        [Test]
        public void ShouldBeAbleToCreateAndRemoveCollections()
        {
            // Arrange
            var collectionName = "TestCollection";

            // Act
            var existsAtFirst = _mongoDbProxy.CollectionExists(collectionName);
            _mongoDbProxy.CreateCollection(collectionName);
            var existsLater = _mongoDbProxy.CollectionExists(collectionName);
            _mongoDbProxy.RemoveCollection(collectionName);
            var existsAfterRemoval = _mongoDbProxy.CollectionExists(collectionName);

            // Assert
            Assert.That(existsAtFirst, Is.False);
            Assert.That(existsLater, Is.True);
            Assert.That(existsAfterRemoval, Is.False);
        }

        [Test]
        public void ShouldBeAbleToGetAllBsonDocumentsThatWasInserted()
        {
            // Arrange
            var bsonDocuments = GetBsonDocuments();
            _mongoDbProxy.Insert(_collectionName, bsonDocuments);

            // Act
            var foundBsonDocuments = _mongoDbProxy.FindAll(_collectionName);

            // Assert
            Assert.That(foundBsonDocuments, Is.Not.Null);
            Assert.That(foundBsonDocuments.Count(), Is.EqualTo(bsonDocuments.Count()));
        }

        [Test]
        public void ShouldBeAbleToInsertMultipleBsonDocuments()
        {
            // Arrange
            var bsonDocuments = GetBsonDocuments();

            // Act
            _mongoDbProxy.Insert(_collectionName, bsonDocuments);

            // Assert
            var foundBsonDocuments = _mongoDbProxy.FindAll(_collectionName);
            Assert.That(foundBsonDocuments, Is.Not.Null);
            Assert.That(foundBsonDocuments.Count(), Is.EqualTo(bsonDocuments.Count()));
        }

        [Test]
        public void ShouldReturnChristerWhenFindIsCalledWithZipCode_17163()
        {
            // Arrange
            var expected = "Christer";
            var bsonDocuments = GetBsonDocuments();
            _mongoDbProxy.Insert(_collectionName, bsonDocuments);

            // Act
            var foundBsonDocuments = _mongoDbProxy.Find(_collectionName, "Address.ZipCode", "171 63");

            // Assert
            Assert.That(foundBsonDocuments.Count(), Is.EqualTo(1));
            var person = foundBsonDocuments.First();
            Assert.That(person["Name"].AsString, Is.EqualTo(expected));
        }

        private IEnumerable<BsonDocument> GetBsonDocuments()
        {
            return new[]
                       {
                           new BsonDocument
                               {
                                   {"Name", "Hans"},
                                   {"Age", 36},
                                   {
                                       "Address",
                                       new BsonDocument
                                           {
                                               {"HouseNameOrNumber", 10},
                                               {"Street", "English Street"},
                                               {"ZipCode", "E3 4TA"},
                                               {"Country", "United Kingdom"}
                                           }
                                       }
                               },
                           new BsonDocument
                               {
                                   {"Name", "Christer"},
                                   {"Age", 36},
                                   {
                                       "Address",
                                       new BsonDocument
                                           {
                                               {"HouseNameOrNumber", 13},
                                               {"Street", "Storgatan"},
                                               {"ZipCode", "171 63"},
                                               {"Country", "Sweden"}
                                           }
                                       }
                               },
                           new BsonDocument
                               {
                                   {"Name", "Ali"},
                                   {"Age", 26},
                                   {
                                       "Address",
                                       new BsonDocument
                                           {
                                               {"HouseNameOrNumber", "Holden House"},
                                               {"Street", "Rathbone Place"},
                                               {"ZipCode", "AB C12"},
                                               {"Country", "United Kingdom"}
                                           }
                                       }
                               }
                       };
        }
    }
}