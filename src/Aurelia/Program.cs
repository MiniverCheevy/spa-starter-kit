using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var environment = EnvironmentBuilder.GetEnvironment();
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseEnvironment(environment)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}