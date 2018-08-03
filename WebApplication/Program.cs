using Microsoft.AspNetCore.Hosting;
using Platform;

namespace WebApplication
{
    public class Program
    {
        public static void Main()
        {
            new PlatformWebHostBuilder<Startup>()
                .Build()
                .Run();            
        }
    }
}
