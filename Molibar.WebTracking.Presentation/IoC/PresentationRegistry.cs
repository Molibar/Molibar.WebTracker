using AutoMapper;
using Molibar.WebTracking.Domain.IoC;
using Molibar.WebTracking.External.IoC;
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