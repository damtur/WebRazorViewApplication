using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Platform;

namespace WebApplication
{
    public class Startup : PlatformStartup
    {
        protected override void ConfigureMicroserviceServices(IServiceCollection services)
        {
            services.AddPlatform();
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.UsePlatform();
        }
    }
}
