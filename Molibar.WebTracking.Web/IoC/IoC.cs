using AutoMapper;
using Molibar.Infrastructure.IoC;
using Molibar.Infrastructure.IoC.StructureMap;
using Molibar.Infrastructure.Logging;
using Molibar.Infrastructure.Mapper.AutoMapper;
using Molibar.WebTracking.Presentation.IoC;
using StructureMap;

namespace Molibar.WebTracking.Web.IoC
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Configure(
                cfg =>
                    {
                        cfg.AddRegistry(new FrameworkRegistry("MediaIngenuity.Tracking"));
                        Log.InfoMessage(typeof(IoC), "-- Application Started ----");
                        cfg.AddRegistry<PresentationRegistry>();
                        cfg.AddRegistry<DefaultRegistry>();
                        cfg.AddRegistry<AutomapperRegistry>();
                        cfg.Scan(scan =>
                                     {
                                         scan.AddAllTypesOf<Profile>();
                                         scan.TheCallingAssembly();
                                         scan.WithDefaultConventions();
                                     });
                    });

            var configuration = ObjectFactory.GetInstance<IConfiguration>();
            foreach (var profile in ObjectFactory.GetAllInstances<Profile>())
            {
                configuration.AddProfile(profile);
            }

            return ObjectFactory.Container;
        }
    }
}