using AutoMapper;
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
                    scan.AddAllTypesOf<Profile>();
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            });
        }
    }
}