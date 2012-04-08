using AutoMapper;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Molibar.WebTracking.Domain.IoC
{
    public class DomainRegistry : Registry
    {
        public DomainRegistry()
        {
            ObjectFactory.Configure(cfg =>
            {
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