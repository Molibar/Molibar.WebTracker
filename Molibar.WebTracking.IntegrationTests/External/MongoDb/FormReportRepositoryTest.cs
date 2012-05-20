using System;
using AutoMapper;
using Molibar.WebTracking.External.Models;
using Molibar.WebTracking.External.Models.Profiles;
using Molibar.WebTracking.External.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using NUnit.Framework;

namespace Molibar.WebTracking.IntegrationTests.External.MongoDb
{
    [TestFixture]
    public class FormReportRepositoryTest
    {

        private MongoDbProxy _mongoDbProxy;
        private string _collectionName;

        [SetUp]
        public void SetUp()
        {
            Mapper.AddProfile(new FormEventBsonDocumentProfile());

            _mongoDbProxy = new MongoDbProxy("TestDatabase");
            _collectionName = "FormEvents";
            if (!_mongoDbProxy.CollectionExists(_collectionName))
                _mongoDbProxy.CreateCollection(_collectionName);
        }

        [TearDown]
        public void TearDown()
        {
            _mongoDbProxy.DropDatabase();
        }

        [Test]
        public void PlayingWithSearches()
        {
            /*{ "_id" : ObjectId("4fa7341d393751187ca8d053"), "VisitGuid" : BinData(3,"NFQW3oo
GxEiNj3e99NQRSg=="), "Url" : "http://localhost:50342/Demo", "PageId" : "DemoForm
", "EventType" : "focusout", "ElementId" : "StringValue", "ElementType" : "text"
, "ElementValue" : "Ninja", "ValueValid" : true, "ClientDateTime" : ISODate("201
2-05-07T01:31:57.039Z"), "DateTime" : ISODate("2012-05-07T02:31:57.041Z") }*/
            /*focusin, change, focusout, blur*/

            // Arrange
            var guid = Guid.NewGuid();
            var collection = _mongoDbProxy.MongoDatabase.GetCollection(_collectionName);
            var focusin = new FormEventDataModel
                              {
                                  VisitGuid = guid.ToString(),
                                  Url = "http://localhost:50342/Demo",
                                  PageId = "DemoForm",
                                  ElementId = "StringValue",
                                  EventType = "focusin",
                                  ElementType = "text",
                                  ElementValue = "",
                                  ClientDateTime = DateTime.Now.AddSeconds(-100)
                              };
            collection.Insert(focusin);
            var change = new FormEventDataModel
                             {
                                 VisitGuid = guid.ToString(),
                                 Url = "http://localhost:50342/Demo",
                                 PageId = "DemoForm",
                                 ElementId = "StringValue",
                                 EventType = "change",
                                 ElementType = "text",
                                 ElementValue = "Ninja",
                                 ClientDateTime = DateTime.Now.AddSeconds(-80)
                             };
            collection.Insert(change);
            var focusout = new FormEventDataModel
                               {
                                   VisitGuid = guid.ToString(),
                                   Url = "http://localhost:50342/Demo",
                                   PageId = "DemoForm",
                                   ElementId = "StringValue",
                                   EventType = "focusout",
                                   ElementType = "text",
                                   ElementValue = "Ninja",
                                   ClientDateTime = DateTime.Now.AddSeconds(-80)
                               };
            collection.Insert(focusout);
            var blur = new FormEventDataModel
                           {
                               VisitGuid = guid.ToString(),
                               Url = "http://localhost:50342/Demo",
                               PageId = "DemoForm",
                               ElementId = "StringValue",
                               EventType = "blur",
                               ElementType = "text",
                               ElementValue = "Ninja",
                               ClientDateTime = DateTime.Now.AddSeconds(-80)
                           };
            collection.Insert(blur);


            var result = collection.MapReduce(
                new BsonJavaScript(@"
function() {
 emit(1, { id: this.VisitGuid + this.ElementId} );
}"),
                new BsonJavaScript(
                    @"
function(key, values) {
 var sum = 0;
 values.forEach(function(prev) {
  sum += prev;
 });
 return sum;
}"));



            var queryComplete = Query.And(
                Query.EQ("ElementId", "StringValue"),
                Query.EQ("ElementType", "text")
                );

            // Act
            var elements = _mongoDbProxy.MongoDatabase
                .GetCollection<FormEventDataModel>(_collectionName)
                .Find(queryComplete);

            // Assert
            foreach (var x in elements)
            {
                var ninja = x;
            }
        }
    }
}