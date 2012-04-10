using System.Collections.Generic;
using Amazon;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;

namespace Molibar.WebTracking.External.SimpleDb
{
    public interface ISimpleDbProxy
    {
        void CreateDomain(string domainName);
        bool DomainExists(string domainName);
        void Put(string domainName, string itemName, List<ReplaceableAttribute> replaceableAttributes);
    }

    public class SimpleDbProxy : ISimpleDbProxy
    {
        private AmazonSimpleDB _simpleDbClient;
        public const string DATE_FORMAT_STRING = "yyyy-MM-ddTHH:mm:ss";

        public SimpleDbProxy()
        {
            _simpleDbClient = AWSClientFactory.CreateAmazonSimpleDBClient(new AmazonSimpleDBConfig());
        }

        public void CreateDomain(string domainName)
        {
            var createDomainRequest = new CreateDomainRequest
                                          {
                                              DomainName = domainName
                                          };
            _simpleDbClient.CreateDomain(createDomainRequest);
        }

        public bool DomainExists(string domainName)
        {
            var sdbRequest = new ListDomainsRequest();
            var sdbResponse = _simpleDbClient.ListDomains(sdbRequest);
            return sdbResponse.ListDomainsResult.DomainName.Exists(x => x.Equals(domainName));
        }

        public void Put(string domainName, string itemName, List<ReplaceableAttribute> replaceableAttributes)
        {
            var putAttributesRequest = new PutAttributesRequest
                                 {
                                     DomainName = domainName,
                                     ItemName = itemName,
                                     Attribute = replaceableAttributes
                                 };
            var putAttributesResponse = _simpleDbClient.PutAttributes(putAttributesRequest);
        }
    }
}
