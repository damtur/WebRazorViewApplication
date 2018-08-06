using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Platform
{
    public class PlatformWebHostBuilder<TStartup>
        where TStartup : PlatformStartup, new()
    {
        private readonly IWebHostBuilder _builder;

        public PlatformWebHostBuilder()
        {            
            _builder = WebHost.CreateDefaultBuilder()
                .UseStartup(typeof(TStartup));
        }

        public IWebHost Build()
        {
            return _builder.Build();
        }
    }
}