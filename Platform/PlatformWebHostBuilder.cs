using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Platform
{
    /// <summary>
    /// This is the builder to be used to standardize creation of all Marvel webapps. It is called from a service's Program.cs file
    /// as you would call the default WebHostBuilder. However, it doesn't implement the full IWebHostBuilder interface (only the
    /// Build() method) because we want to keep tight consistency.
    /// </summary>
    public class PlatformWebHostBuilder<TStartup>
        where TStartup : IPlatformStartup, new()
    {
        /// <summary>
        /// This wraps the application's startup class, ensuring that we are validating scopes (preventing singleton services from
        /// requesting scoped services). Currently, the only way this appears possible is to inherit from <see cref="IStartup"/>. This
        /// allows us to return our own <see cref="IServiceProvider"/>, which we can create with the validateScopes option via an
        /// overload to the BuildServiceProvider extension. This should be much easier to configure once this is released:
        /// https://github.com/aspnet/Hosting/commit/d57d729d13c73750027944dda25082693d6b6d7f
        /// </summary>
        internal class StartupWrapper : IStartup
        {
            private readonly TStartup _startup;

            public StartupWrapper()
            {
                _startup = new TStartup();
            }

            public IServiceProvider ConfigureServices(IServiceCollection services)
            {
                // Pass a reference to the actual startup class that can be used for configurations like MVC.
                PlatformServiceCollectionExtensions.SetStartupInstance(services, _startup);

                // Configure the actual startup services. This should include the marvel platform.
                _startup.ConfigureServices(services);

                var provider = services.BuildServiceProvider(validateScopes: true);

                return provider;
            }

            public void Configure(IApplicationBuilder app)
            {
                _startup.Configure(app);
            }
        }

        private readonly IWebHostBuilder _builder;

        public PlatformWebHostBuilder()
        {
            _builder = WebHost.CreateDefaultBuilder()
               .UseStartup<StartupWrapper>();
        }

        public IWebHost Build()
        {
            return _builder.Build();
        }
    }
}