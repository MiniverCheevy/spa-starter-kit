using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Web
{
    public class EnvironmentBuilder
    {
        public static string GetEnvironment()
        {
            var environment = "Production";
#if DEBUG
            environment = "Development";
#endif
            //Case must match file name
            environment = CheckForDemoEnvironment(environment);
            environment = CheckForDeveloperEnvironment(environment);

            return environment;
        }

        private static string CheckForDemoEnvironment(string environment)
        {
            var machineName = Environment.MachineName.ToLower();

            if (!machineName.StartsWith("sh-demo-web") && !machineName.StartsWith("demo2"))
                return environment;

            var directory = Directory.GetCurrentDirectory();
            Console.WriteLine(directory);
            if (directory.ToLower().EndsWith(@"\uat"))
            {
                environment = "UAT";
            }
            else if (directory.ToLower().EndsWith(@"\qa"))
            {
                environment = "QA";
            }
            return environment;
        }

        private static string CheckForDeveloperEnvironment(string environment)
        {
            

           
            return environment;
        }
    }
}

