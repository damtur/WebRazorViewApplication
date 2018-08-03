using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Platform
{
    public static class PlatformServiceCollectionExtensions
    {
        private delegate IPlatformStartup GetStartupInstance();

        public static IServiceCollection AddPlatform(this IServiceCollection services)
        {
            var getStartup = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(GetStartupInstance))?.ImplementationInstance as GetStartupInstance;
            if (getStartup == null)
            {
                throw new InvalidOperationException($"Startup instance was not set via {nameof(PlatformServiceCollectionExtensions)}.{nameof(SetStartupInstance)}. This " +
                    $"should normally be handled within {typeof(PlatformWebHostBuilder<>).Name}. If you are writing a test, you may need call {nameof(SetStartupInstance)} explicitly.");
            }
            var startup = getStartup();

            services.AddMvc(options => { })
                .AddApplicationPart(startup.GetType().GetTypeInfo().Assembly)         
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            return services;
        }


        /// <summary>
        /// This provides a consistent way of referencing the startup class from within AddHudlPlatform, without having to pass it in as another
        /// parameter. This is used primarily by the <see cref="PlatformWebHostBuilder{TStartup}.StartupWrapper" /> class, to allow MVC service configuration to access the
        /// app's startup assembly. It is also used from tests.
        /// </summary>
        internal static void SetStartupInstance(IServiceCollection services, IPlatformStartup startup)
        {
            // Pass a reference to the actual startup class that can be used for configurations like MVC.
            services.AddSingleton<GetStartupInstance>(() => startup);
        }
    }
}
