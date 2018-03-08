using System;
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

        private static string DetermineDeploymentEnvironment(string directory)
        {
            var deploymentEnvironment = "Production";
#if DEBUG
            deploymentEnvironment = "Development";
#endif


            if (directory != null)
            {
                directory = directory.TrimEnd('\\', '/');

                if (directory.EndsWith(@"\uat", StringComparison.OrdinalIgnoreCase))
                {
                    deploymentEnvironment = "UAT";
                }
                else if (directory.EndsWith(@"\qa", StringComparison.OrdinalIgnoreCase))
                {
                    deploymentEnvironment = "QA";
                }
                else if (directory.EndsWith(@"\dev", StringComparison.OrdinalIgnoreCase))
                {
                    deploymentEnvironment = "Development";
                }
                else if (directory.EndsWith(@"uat1", StringComparison.OrdinalIgnoreCase))
                {
                    deploymentEnvironment = "UAT1";
                }
                else if (directory.EndsWith(@"uat2", StringComparison.OrdinalIgnoreCase))
                {
                    deploymentEnvironment = "UAT2";
                }
                else if (directory.EndsWith(@"DataBusAPI", StringComparison.OrdinalIgnoreCase))
                {
                    deploymentEnvironment = "Production";
                }
            }

            return deploymentEnvironment;
        }
    }
}