using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Platform;

namespace WebApplication
{
    public class Startup : IPlatformStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPlatform();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UsePlatform();
            
        }
    }
}
