using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Platform
{
    public static class PlatformServiceCollectionExtensions
    {
        public static IServiceCollection AddPlatform(this IServiceCollection services)
        {
            services.AddMvc(options => { })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            return services;
        }
    }
}
