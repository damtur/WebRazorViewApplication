using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Platform
{
    public interface IPlatformStartup
    {
        void ConfigureServices(IServiceCollection services);

        void Configure(IApplicationBuilder app);
    }
}