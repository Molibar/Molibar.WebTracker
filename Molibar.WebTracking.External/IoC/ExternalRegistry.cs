using AutoMapper;
using Molibar.WebTracking.External.MongoDb;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Molibar.WebTracking.External.IoC
{
    public class ExternalRegistry : Registry
    {
        public ExternalRegistry()
        {
            ObjectFactory.Configure(cfg =>
            {
                cfg.Scan(scan =>
                {
                    cfg.For<IMongoDbProxy>().Use(x => new MongoDbProxy());
                    scan.AddAllTypesOf<Profile>();
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            });
        }
    }
}