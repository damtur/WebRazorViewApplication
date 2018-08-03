using Microsoft.AspNetCore.Builder;

namespace Platform
{
    public static class PlatformApplicationBuilderExtensions
    {
        public static void UsePlatform(this IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc(); 
        }
    }
}
