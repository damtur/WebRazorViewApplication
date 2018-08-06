using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Platform
{
    public abstract class PlatformStartup : IStartup
    {
        protected abstract void ConfigureMicroserviceServices(IServiceCollection services);       

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            ConfigureMicroserviceServices(services);
            var provider = services.BuildServiceProvider(validateScopes: true);

            return provider;
        }

        public abstract void Configure(IApplicationBuilder app);
    }
}