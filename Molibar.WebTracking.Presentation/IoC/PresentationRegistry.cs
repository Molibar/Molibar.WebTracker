using AutoMapper;
using Molibar.WebTracking.Domain.IoC;
using Molibar.WebTracking.Domain.Repositories;
using Molibar.WebTracking.External.IoC;
using Molibar.WebTracking.External.MongoDb.Tracking;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Molibar.WebTracking.Presentation.IoC
{
    public class PresentationRegistry : Registry
    {
        public PresentationRegistry()
        {
            ObjectFactory.Configure(cfg =>
            {
                cfg.For<IFormEventRepository>().Use<FormEventRepository>();
                cfg.AddRegistry<DomainRegistry>();
                cfg.AddRegistry<ExternalRegistry>();
                cfg.Scan(scan =>
                {
                    scan.AddAllTypesOf<Profile>();
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            });
        }
    }
}